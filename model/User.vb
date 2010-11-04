Imports System
Imports System.Collections.Generic
Imports System.Drawing
Imports System.IO
Imports System.Net
Imports System.Text
Imports System.Text.RegularExpressions

Namespace Huggle

    <Diagnostics.DebuggerDisplay("{Name}")> _
    Public Class User : Inherits QueueItem

        'Represents a user account or anonymous user

        Private _Abuse As List(Of Abuse)
        Private _Blocks As List(Of Block)
        Private _Config As UserConfig
        Private _FirstEdit As Revision
        Private _GroupChanges As UserGroupChangeCollection
        Private _Groups As List(Of UserGroup)
        Private _IsAnonymous As Boolean
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

        Public Sub New(ByVal wiki As Wiki, ByVal name As String)
            _DisplayName = name
            _Contributions = -1
            If name = "[anonymous]" Then _IsAnonymous = True
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
        Public Property DeletedEditsKnown As Boolean
        Public Property DisplayName As String
        Public Property Contributions As Integer

        Public ReadOnly Property CurrentBlock() As Block
            Get
                Dim result As Block = Nothing

                For Each block As Block In Blocks
                    If result Is Nothing OrElse block.Time > result.Time Then result = block
                Next block

                Return result
            End Get
        End Property

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

        Public ReadOnly Property FullName() As String
            Get
                Return Name & "@" & Wiki.Code
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

        Public ReadOnly Property IsAnonymous() As Boolean
            Get
                If Not Processed Then Process()
                Return _IsAnonymous
            End Get
        End Property

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

        Public Property IsUsed As Boolean

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

        Public ReadOnly Property Name() As String
            Get
                Return _Name
            End Get
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
                If AnonymousRegex.IsMatch(Name) Then _IsAnonymous = True
                _IsIgnored = Wiki.Users.Ignored.Contains(Me)
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

        Public Sub Rename(ByVal newName As String)
            Dim oldName As String = Name
            _Name = newName
            Wiki.Users.Rename(oldName, newName)
            RaiseEvent Renamed(Me, New UserRenamedEventArgs(Me, oldName))
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

    Public Class UserRenamedEventArgs : Inherits EventArgs

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

    Public Class UserCollection

        Private _All As New Dictionary(Of String, User)
        Private _Default As User
        Private _Hidden As User
        Private _Ignored As New List(Of User)
        Private _NewUsers As New List(Of User)

        Private Wiki As Wiki

        Public Sub New(ByVal wiki As Wiki)
            Me.Wiki = wiki
        End Sub

        Public ReadOnly Property All() As IList(Of User)
            Get
                Return _All.Values.ToList.AsReadOnly
            End Get
        End Property

        Public ReadOnly Property Anonymous() As User
            Get
                Return FromName(Nothing)
            End Get
        End Property

        Public Property Count() As Integer = -1

        Public ReadOnly Property [Default]() As User
            Get
                'Default user for config purposes
                If _Default Is Nothing Then
                    _Default = New User(Wiki, "[default]")
                    _Default.IsHidden = True
                End If

                Return _Default
            End Get
        End Property

        Default Public ReadOnly Property FromName(ByVal name As String) As User
            Get
                If name Is Nothing Then name = "[anonymous]"
                If Not _All.ContainsKey(name) Then _All.Add(name, New User(Wiki, name))
                Return _All(name)
            End Get
        End Property

        Public ReadOnly Property Hidden() As User
            Get
                'Representats the author of actions for which the actual author is hidden
                If _Hidden Is Nothing Then
                    _Hidden = New User(Wiki, "[hidden]")
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

        Public ReadOnly Property NewUsers() As List(Of User)
            Get
                Return _NewUsers
            End Get
        End Property

        Public Function FromString(ByVal name As String) As User
            name = SanitizeName(name)
            If name Is Nothing Then Return Nothing
            Return FromName(name)
        End Function

        Public Sub Rename(ByVal oldName As String, ByVal newName As String)
            If _All.ContainsKey(oldName) Then
                Dim user As User = _All(oldName)
                _All.Remove(oldName)
                _All.Add(newName, user)
            End If
        End Sub

        Public Shared Function SanitizeName(ByVal name As String) As String
            If String.IsNullOrEmpty(name) Then Return Nothing

            'Remove navigation fragment
            If name.Contains("#") Then name = name.ToFirst("#")

            name = name.Remove(Tab, CR, LF).Replace("_", " ").Trim

            If String.IsNullOrEmpty(name) Then Return Nothing

            While name.Contains("  ")
                name = name.Replace("  ", " ")
            End While

            For Each badchar As Char In "[]{}|<>#/\".ToCharArray
                If name.Contains(badchar) Then Return Nothing
            Next badchar

            'Disallow path syntax
            If name = "." OrElse name = ".." Then Return Nothing
            If name.StartsWithI(":") OrElse name.StartsWithI("./") OrElse name.StartsWithI("../") Then Return Nothing
            If name.Contains("/./") OrElse name.Contains("/../") Then Return Nothing
            If name.EndsWithI("/.") OrElse name.EndsWithI("/..") Then Return Nothing

            'Disallow HTML entities
            Static HtmlEntityPattern As New Regex("&[a-zA-z0-9];", RegexOptions.Compiled)
            If HtmlEntityPattern.IsMatch(name) Then Return Nothing

            If Encoding.UTF8.GetBytes(name).Length > 255 Then Return Nothing

            'Capitalize
            name = UcFirst(name)

            Return name
        End Function

    End Class

    <Diagnostics.DebuggerDisplay("{Description}")>
    Public Class RateLimit

        Private _Action As String
        Private _Group As String
        Private _Hits As Integer
        Private _Time As TimeSpan

        Public Sub New(ByVal action As String, ByVal group As String, ByVal hits As Integer, ByVal time As TimeSpan)
            _Action = action
            _Group = group
            _Hits = hits
            _Time = time
        End Sub

        Public ReadOnly Property Action() As String
            Get
                Return _Action
            End Get
        End Property

        Public ReadOnly Property Description As String
            Get
                Return Action & ": " & CStr(Hits) & " in " & FuzzyTime(Time)
            End Get
        End Property

        Public ReadOnly Property Hits() As Integer
            Get
                Return _Hits
            End Get
        End Property

        Public ReadOnly Property Group() As String
            Get
                Return _Group
            End Get
        End Property

        Public ReadOnly Property Time As TimeSpan
            Get
                Return _Time
            End Get
        End Property

        Public Overrides Function ToString() As String
            Return Description
        End Function

    End Class

End Namespace