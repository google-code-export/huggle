Imports System
Imports System.Collections.Generic
Imports System.Drawing
Imports System.IO
Imports System.Net
Imports System.Text
Imports System.Text.RegularExpressions

Namespace Huggle

    <Diagnostics.DebuggerDisplay("{FullDisplayName}")>
    Friend Class User : Inherits QueueItem

        'Represents a user account or anonymous user

        Private _Abuse As List(Of Abuse)
        Private _Blocks As List(Of Block)
        Private _Config As UserConfig
        Private _FirstEdit As Revision
        Private _GroupChanges As UserGroupChangeCollection
        Private _Groups As List(Of UserGroup)
        Private _Id As Integer
        Private _IsBot As Boolean
        Private _IsIgnored As Boolean
        Private _Logs As List(Of LogItem)
        Private _Name As String
        Private _Preferences As Preferences
        Private _RateLimits As List(Of RateLimit)
        Private _Sanctions As List(Of Sanction)
        Private _Watchlist As List(Of String)
        Private _Wiki As Wiki

        Private Processed As Boolean

        Private Shared ReadOnly AnonymousRegex As New Regex _
            ("\d{1,3}\.\d{1,3}\.\d{1,3}\.\d{1,3}", RegexOptions.Compiled)

        Public Event Edited As SimpleEventHandler(Of Revision)
        Public Event Renamed(ByVal sender As Object, ByVal e As UserRenamedEventArgs)
        Public Event StateChanged As SimpleEventHandler(Of User)
        Public Event ContribsChanged As SimpleEventHandler(Of User)

        Public Sub New(ByVal wiki As Wiki, ByVal id As Integer)
            ThrowNull(wiki, "wiki")

            _DisplayName = "[" & id.ToString & "]"
            _Id = id
            _Wiki = wiki
        End Sub

        Public Sub New(ByVal wiki As Wiki, ByVal name As String)
            ThrowNull(name, "name")
            ThrowNull(wiki, "wiki")

            _DisplayName = name
            _Name = name
            _Wiki = wiki
        End Sub

        Public ReadOnly Property Abuse() As List(Of Abuse)
            Get
                If _Abuse Is Nothing Then _Abuse = New List(Of Abuse)
                Return _Abuse
            End Get
        End Property

        Public Property AbuseKnown As Boolean

        Public ReadOnly Property ApiLimit() As Integer
            Get
                If HasRight("apihighlimits") Then Return 5000 Else Return 500
            End Get
        End Property

        Public ReadOnly Property Blocks() As List(Of Block)
            Get
                If _Blocks Is Nothing Then _Blocks = New List(Of Block)
                Return _Blocks
            End Get
        End Property

        Public Property BlocksKnown As Boolean

        Public ReadOnly Property CanChangeUserRights As Boolean
            Get
                For Each group As UserGroupChange In GroupChanges.All
                    If group.CanAdd OrElse group.CanRemove Then Return True
                Next group

                Return False
            End Get
        End Property

        Public ReadOnly Property CanSelfChangeUserRights As Boolean
            Get
                For Each group As UserGroupChange In GroupChanges.All
                    If group.CanAdd OrElse group.CanAddSelf _
                        OrElse group.CanRemove OrElse group.CanRemoveSelf Then Return True
                Next group

                Return False
            End Get
        End Property

        Public Property Config() As UserConfig
            Get
                If _Config Is Nothing Then
                    If IsDefault Then
                        If Wiki.IsDefault Then
                            _Config = New UserConfig(Me)
                        Else
                            _Config = App.Wikis.Default.Users.Default.Config.Copy(Me)
                        End If
                    Else
                        _Config = Wiki.Users.Default.Config.Copy(Me)
                    End If
                End If

                Return _Config
            End Get
            Set(ByVal value As UserConfig)
                _Config = value
            End Set
        End Property

        Public Property Created As Date

        Public Property Contributions As Integer = -1

        Public ReadOnly Property CurrentBlock() As Block
            Get
                Dim result As Block = Nothing

                For Each block As Block In Blocks
                    If result Is Nothing OrElse block.Time > result.Time Then result = block
                Next block

                Return result
            End Get
        End Property

        Public Property DeletedEditsKnown As Boolean

        Public Property DisplayName As String

        Public ReadOnly Property Edits() As List(Of Revision)
            Get
                Dim result As New List(Of Revision)
                Dim rev As Revision = _LastEdit

                While rev IsNot Nothing AndAlso rev IsNot Revision.Null
                    result.Add(rev)
                    If result.Count > 5000 Then Return result
                    rev = rev.PrevByUser
                End While

                Return result
            End Get
        End Property

        Public Property ExtendedInfoKnown() As Boolean

        Public Property FirstEdit() As Revision
            Get
                Return _FirstEdit
            End Get
            Set(ByVal value As Revision)
                _FirstEdit = value
                If FirstEdit IsNot Nothing Then FirstEdit.PrevByUser = Revision.Null
            End Set
        End Property

        Public ReadOnly Property FullDisplayName() As String
            Get
                Return DisplayName & "@" & Wiki.Name
            End Get
        End Property

        Public ReadOnly Property FullName() As String
            Get
                Return Name & "@" & Wiki.Name
            End Get
        End Property

        Public Property GlobalStatus() As String
        Public Property GlobalUser() As GlobalUser

        Public ReadOnly Property GroupChanges() As UserGroupChangeCollection
            Get
                If _GroupChanges Is Nothing Then _GroupChanges = New UserGroupChangeCollection(Me)
                Return _GroupChanges
            End Get
        End Property

        Public ReadOnly Property Groups As List(Of UserGroup)
            Get
                If _Groups Is Nothing Then _Groups = New List(Of UserGroup)
                Return _Groups
            End Get
        End Property

        Public Property HasDeletedEdits() As Boolean

        Public ReadOnly Property HasRight(ByVal right As String) As Boolean
            Get
                Return Rights.Contains(right)
            End Get
        End Property

        Public Property Id() As Integer
            Get
                Return _Id
            End Get
            Set(ByVal value As Integer)
                _Id = value
                Wiki.Users.UpdateId(Me)
            End Set
        End Property

        Public Property IgnoreCount() As Integer

        Public ReadOnly Property IsAbusive() As Boolean
            Get
                If _Abuse Is Nothing Then Return False

                For Each item As Abuse In Abuse
                    If item.Filter IsNot Nothing AndAlso (Wiki.Config.AbuseFilters Is Nothing _
                        OrElse Wiki.Config.AbuseFilters.Contains(item.Filter.Id)) Then Return True
                Next item

                Return False
            End Get
        End Property

        Public Property IsAnonymous() As Boolean

        Public ReadOnly Property IsAutoconfirmed() As Boolean
            Get
                'Cannot be retrieved programmatically (see MediaWiki bug 16867)
                'Thresholds supplied in config instead
                If IsAnonymous OrElse Not Wiki.Config.Autoconfirm OrElse Not ExtendedInfoKnown Then Return False
                If Created.Add(Wiki.Config.AutoconfirmTime) > Wiki.ServerTime Then Return False
                If Wiki.Config.AutoconfirmEdits > Contributions Then Return False
                Return True
            End Get
        End Property

        Public ReadOnly Property IsBlocked() As Boolean
            Get
                Return (CurrentBlock IsNot Nothing)
            End Get
        End Property

        Public ReadOnly Property IsBot() As Boolean
            Get
                Return _IsBot
            End Get
        End Property

        Public ReadOnly Property IsDefault() As Boolean
            Get
                Return (Wiki.Users.Default Is Me)
            End Get
        End Property

        Public Property IsEmailable As Boolean
        Public Property IsHidden As Boolean

        Public Property IsIgnored() As Boolean
            Get
                If Not Processed Then Process()
                Return _IsIgnored
            End Get
            Set(ByVal value As Boolean)
                _IsIgnored = value
                RaiseEvent StateChanged(Me, New EventArgs(Of User)(Me))
            End Set
        End Property

        Public ReadOnly Property IsInGroup(ByVal group As UserGroup) As Boolean
            Get
                Return Groups.Contains(group)
            End Get
        End Property

        Public Property IsLoaded As Boolean

        Public ReadOnly Property IsPrivileged() As Boolean
            Get
                For Each group As UserGroup In Groups
                    For Each right As String In InternalConfig.PrivilegedRights
                        If group.Rights.Contains(right) Then Return True
                    Next right
                Next group

                Return False
            End Get
        End Property

        Public ReadOnly Property IsReported() As Boolean
            Get
                If _Sanctions Is Nothing Then Return False

                For Each sanction As Sanction In Sanctions
                    If sanction.IsCurrent AndAlso sanction.Type.Name = "report" Then Return True
                Next sanction

                Return False
            End Get
        End Property

        Public Property IsReverted As Boolean
        Public Property IsShared As Boolean

        Public ReadOnly Property IsUnified() As Boolean
            Get
                Return (GlobalUser IsNot Nothing)
            End Get
        End Property

        Public ReadOnly Property IsUsed As Boolean
            Get
                Return Wiki.Users.Used.Contains(Me)
            End Get
        End Property

        Public ReadOnly Property IsWarned() As Boolean
            Get
                If _Sanctions Is Nothing Then Return False

                For Each item As Sanction In Sanctions
                    If item.IsCurrent AndAlso item.IsWarning Then Return True
                Next item

                Return False
            End Get
        End Property

        Public Property LastEdit As Revision

        Public ReadOnly Property LastSanctionTime() As Date
            Get
                Dim result As Date = Date.MinValue

                For Each sanction As Sanction In _Sanctions
                    If sanction.Time > result Then result = sanction.Time
                Next sanction

                Return result
            End Get
        End Property

        Public ReadOnly Property Logs() As List(Of LogItem)
            Get
                If _Logs Is Nothing Then _Logs = New List(Of LogItem)
                Return _Logs
            End Get
        End Property

        Public Property LogsKnown As Boolean

        Public Property Name() As String
            Get
                Return _Name
            End Get
            Set(ByVal value As String)
                Dim oldName As String = _Name
                _Name = value
                Wiki.Users.UpdateName(Me, oldName)
                If oldName IsNot Nothing Then RaiseEvent Renamed(Me, New UserRenamedEventArgs(Me, oldName))
            End Set
        End Property

        Public Property Password As Byte()

        Public Property Preferences() As Preferences
            Get
                If _Preferences Is Nothing Then _Preferences = New Preferences(Me)
                Return _Preferences
            End Get
            Set(ByVal value As Preferences)
                _Preferences = value
            End Set
        End Property

        Public ReadOnly Property Range() As String
            Get
                'Return first two octets of IP address
                If Not _IsAnonymous Then Return Nothing
                Return _Name.ToLast(".").ToLast(".")
            End Get
        End Property

        Public ReadOnly Property RateLimits() As List(Of RateLimit)
            Get
                If _RateLimits Is Nothing Then _RateLimits = New List(Of RateLimit)
                Return _RateLimits
            End Get
        End Property

        Public Property RegisteredTo As String
        Public Property Rights As New List(Of String)

        Public ReadOnly Property Sanction() As Sanction
            Get
                Dim max As Sanction = Nothing

                For Each item As Sanction In Sanctions
                    If item.Time.Add(Wiki.Config.WarningAge) < Wiki.ServerTime Then Return max
                    If item.Type.Level > max.Type.Level Then max = item
                Next item

                Return max
            End Get
        End Property

        Public ReadOnly Property Sanctions() As List(Of Sanction)
            Get
                If _Sanctions Is Nothing Then _Sanctions = New List(Of Sanction)
                Return _Sanctions
            End Get
        End Property

        Public Property SessionEdits As Integer

        Public ReadOnly Property Talkpage() As Page
            Get
                Return Wiki.Pages.FromNsAndName(Wiki.Spaces.UserTalk, Name)
            End Get
        End Property

        Public Property UnificationDate As Date
        Public Property UnificationMethod As String

        Public ReadOnly Property Userpage() As Page
            Get
                Return Wiki.Pages.FromNsAndName(Wiki.Spaces.User, Name)
            End Get
        End Property

        Public ReadOnly Property Watchlist() As List(Of String)
            Get
                If _Watchlist Is Nothing Then _Watchlist = New List(Of String)
                Return _Watchlist
            End Get
        End Property

        Public Overrides ReadOnly Property Wiki() As Wiki
            Get
                Return _Wiki
            End Get
        End Property

        Public Sub OnEdit(ByVal rev As Revision)
            RaiseEvent Edited(Me, New EventArgs(Of Revision)(rev))
        End Sub

        Public Sub OnContribsChanged()
            RaiseEvent ContribsChanged(Me, New EventArgs(Of User)(Me))
        End Sub

        Public Sub Process()
            If Not Processed Then
                If Name IsNot Nothing Then
                    If AnonymousRegex.IsMatch(Name) Then _IsAnonymous = True
                    _IsIgnored = Wiki.Users.Ignored.Contains(Me)
                End If
            End If

            Processed = True
            RefreshState()
        End Sub

        Public Sub ProcessNew()
            Contributions = 0
        End Sub

        Public Sub RefreshState()
            RaiseEvent StateChanged(Me, New EventArgs(Of User)(Me))
        End Sub

        Public Function Can(ByVal featureName As String) As Boolean
            If Not Feature.All.ContainsKey(featureName) Then Return False
            Return Feature.All(featureName).AvailableTo(Me)
        End Function

        Public Overrides Function ToString() As String
            Return _DisplayName
        End Function

        Public Overrides ReadOnly Property FilterVars() As Dictionary(Of String, Object)
            Get
                Return Nothing
            End Get
        End Property

        Public Overrides ReadOnly Property Key() As Integer
            Get
                Return 0
            End Get
        End Property

    End Class

    Friend Class UserRenamedEventArgs : Inherits EventArgs

        Private _OldName As String
        Private _User As User

        Public Sub New(ByVal user As User, ByVal oldName As String)
            _OldName = oldName
            _User = user
        End Sub

        Public ReadOnly Property OldName() As String
            Get
                Return _OldName
            End Get
        End Property

        Public ReadOnly Property User As User
            Get
                Return _User
            End Get
        End Property

    End Class

    Friend Class UserCollection

        Private AllById As New Dictionary(Of Integer, User)
        Private AllByName As New Dictionary(Of String, User)

        Private _Anonymous As User
        Private _Default As User
        Private _Hidden As User
        Private _Ignored As New List(Of User)
        Private _NewUsers As New List(Of User)
        Private _Used As New List(Of User)

        Private Wiki As Wiki

        Public Sub New(ByVal wiki As Wiki)
            ThrowNull(wiki, "wiki")
            Me.Wiki = wiki
        End Sub

        Public ReadOnly Property Anonymous() As User
            Get
                'Represents the user in an anonymous session
                'Revisions from anonymous users are represented by a user with an IP address as a name
                If _Anonymous Is Nothing Then
                    _Anonymous = New User(Wiki, 0)
                    _Anonymous.DisplayName = "[anonymous]"
                    _Anonymous.IsAnonymous = True
                    _Anonymous.IsHidden = True
                End If

                Return _Anonymous
            End Get
        End Property

        Public Property Count() As Integer = -1

        Public ReadOnly Property [Default]() As User
            Get
                'Default user for config purposes
                If _Default Is Nothing Then
                    _Default = New User(Wiki, 0)
                    _Default.DisplayName = "[default]"
                    _Default.IsHidden = True
                End If

                Return _Default
            End Get
        End Property

        Public ReadOnly Property Hidden() As User
            Get
                'Represents the author of actions for which the actual author is hidden
                If _Hidden Is Nothing Then
                    _Hidden = New User(Wiki, 0)
                    _Hidden.DisplayName = "[hidden]"
                    _Hidden.IsHidden = True
                End If

                Return _Hidden
            End Get
        End Property

        Public ReadOnly Property Ignored() As List(Of User)
            Get
                Return _Ignored
            End Get
        End Property

        Default Public ReadOnly Property Item(ByVal name As String) As User
            Get
                Return FromName(name)
            End Get
        End Property

        Public ReadOnly Property NewUsers() As List(Of User)
            Get
                Return _NewUsers
            End Get
        End Property

        Public ReadOnly Property Used As List(Of User)
            Get
                Return _Used
            End Get
        End Property

        Public Function FromId(ByVal id As Integer) As User
            ThrowOutOfRange(id <= 0, "id")
            If Not AllById.ContainsKey(id) Then AllById.Add(id, New User(Wiki, id))
            Return AllById(id)
        End Function

        Public Function FromName(ByVal name As String) As User
            ThrowNull(name, "name")

            If Not AllByName.ContainsKey(name) Then AllByName.Add(name, New User(Wiki, name))
            Return AllByName(name)
        End Function

        Public Function FromString(ByVal name As String) As User
            name = SanitizeName(name)
            If name Is Nothing Then Return Nothing
            Return FromName(name)
        End Function

        Public Sub UpdateName(ByVal user As User, ByVal oldName As String)
            ThrowNull(user, "user")

            AllByName.Unmerge(oldName)
            AllByName.Merge(user.Name, user)
        End Sub

        Public Sub UpdateId(ByVal user As User)
            ThrowNull(user, "user")

            AllById.Merge(user.Id, user)
        End Sub

        Public Function SanitizeName(ByVal name As String) As String
            If name Is Nothing Then Return Nothing

            'Remove navigation fragment
            If name.Contains("#") Then name = name.ToFirst("#")

            'Convert underscores to spaces
            name = name.Replace("_", " ")

            'Remove excess whitespace
            Static multipleSpacePattern As New Regex("  +", RegexOptions.Compiled)
            name = multipleSpacePattern.Replace(name, " ").Trim

            If name.Length = 0 Then Return Nothing

            'Disallow invalid characters and control codes
            Static badChars As String = "[]{}|<>#/\"

            For Each c As Char In name
                If badChars.Contains(c) OrElse Convert.ToInt32(c) < 32 OrElse Convert.ToInt32(c) = 127 Then Return Nothing
            Next c

            'Diallow name beginning with a colon
            If name.StartsWithI(":") Then Return Nothing

            'Disallow path syntax
            If name = "." OrElse name = ".." Then Return Nothing
            If name.StartsWithI("./") OrElse name.StartsWithI("../") Then Return Nothing
            If name.Contains("/./") OrElse name.Contains("/../") Then Return Nothing
            If name.EndsWithI("/.") OrElse name.EndsWithI("/..") Then Return Nothing

            'Disallow HTML entities
            Static htmlEntityPattern As New Regex("&[a-zA-Z0-9];", RegexOptions.Compiled)
            If htmlEntityPattern.IsMatch(name) Then Return Nothing

            'Disallow names that are too long
            If Encoding.UTF8.GetBytes(name).Length > 255 Then Return Nothing

            'Capitalize first letter
            name = name.ToUpperFirstI

            Return name
        End Function

    End Class

End Namespace