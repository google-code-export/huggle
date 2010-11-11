Imports Huggle.Actions
Imports System
Imports System.Collections.Generic
Imports System.Text.RegularExpressions
Imports System.Web.HttpUtility

Namespace Huggle

    <Diagnostics.DebuggerDisplay("{Name}")>
    Friend Class Wiki

        'Represents a MediaWiki wiki

        Private _AbuseFilters As AbuseFilterCollection
        Private _ApiModules As ApiModuleCollection
        Private _Categories As CategoryCollection
        Private _ChangeTags As ChangeTagCollection
        Private _Code As String
        Private _Config As WikiConfig
        Private _Diffs As DiffCollection
        Private _ExpansionCache As Dictionary(Of String, String)
        Private _Extensions As ExtensionCollection
        Private _FeedPatterns As Dictionary(Of String, Regex)
        Private _FileExtensions As List(Of String)
        Private _Files As FileCollection
        Private _Gadgets As GadgetCollection
        Private _Interwikis As Dictionary(Of String, Wiki)
        Private _Logs As LogsCollection
        Private _MagicWordAliases As Dictionary(Of String, String)
        Private _MagicWords As Dictionary(Of String, String)
        Private _Messages As Dictionary(Of String, String)
        Private _Pages As PageCollection
        Private _Preferences As List(Of String)
        Private _Queues As QueueCollection
        Private _Rc As RcSource
        Private _RecentChanges As Dictionary(Of Integer, QueueItem)
        Private _ReviewFlags As ReviewFlagCollection
        Private _Revisions As RevisionCollection
        Private _Skins As Dictionary(Of String, WikiSkin)
        Private _Spaces As SpaceCollection
        Private _SpamLists As SpamListCollection
        Private _Threads As CommentCollection
        Private _UserGroups As UserGroupCollection
        Private _Users As UserCollection

        Public Event Action As SimpleEventHandler(Of QueueItem)

        Public Sub New(ByVal code As String)
            _Code = code
            _Engine = "MediaWiki"
            _Name = code
        End Sub

        Public ReadOnly Property AbuseFilters() As AbuseFilterCollection
            Get
                If _AbuseFilters Is Nothing Then _AbuseFilters = New AbuseFilterCollection(Me)
                Return _AbuseFilters
            End Get
        End Property

        Public Property AccountConfirmation As Boolean = True
        Public Property ActiveUsers As Integer = -1
        Public Property Administrators As Integer
        Public Property AnonymousLogin As Boolean = True

        Public ReadOnly Property ApiModules As ApiModuleCollection
            Get
                If _ApiModules Is Nothing Then _ApiModules = New ApiModuleCollection(Me)
                Return _ApiModules
            End Get
        End Property

        Public ReadOnly Property Categories() As CategoryCollection
            Get
                If _Categories Is Nothing Then _Categories = New CategoryCollection(Me)
                Return _Categories
            End Get
        End Property

        Public ReadOnly Property ChangeTags() As ChangeTagCollection
            Get
                If _ChangeTags Is Nothing Then _ChangeTags = New ChangeTagCollection(Me)
                Return _ChangeTags
            End Get
        End Property

        Public Property Channel As String

        Public ReadOnly Property Code() As String
            Get
                Return _Code
            End Get
        End Property

        Public Property Config() As WikiConfig
            Get
                If _Config Is Nothing Then
                    If Family IsNot Nothing AndAlso Not IsDefault Then
                        _Config = Family.Wikis.Default.Config.Copy(Me)
                    ElseIf Code = "default" Then
                        _Config = New WikiConfig(Me)
                    Else
                        _Config = App.Wikis.Default.Config.Copy(Me)
                    End If
                End If

                Return _Config
            End Get
            Set(ByVal value As WikiConfig)
                _Config = value
            End Set
        End Property

        Public Property ContentPages As Integer = -1
        Public Property CurrentConfirmation As Confirmation

        Public ReadOnly Property Diffs() As DiffCollection
            Get
                If _Diffs Is Nothing Then _Diffs = New DiffCollection(Me)
                Return _Diffs
            End Get
        End Property

        Public Property Engine As String
        Public Property Exists As Boolean

        Public ReadOnly Property ExpansionCache() As Dictionary(Of String, String)
            Get
                If _ExpansionCache Is Nothing Then _ExpansionCache = New Dictionary(Of String, String)
                Return _ExpansionCache
            End Get
        End Property

        Public ReadOnly Property Extensions() As ExtensionCollection
            Get
                If _Extensions Is Nothing Then _Extensions = New ExtensionCollection(Me)
                Return _Extensions
            End Get
        End Property

        Public Property Family() As Family

        Public ReadOnly Property FeedPatterns() As Dictionary(Of String, Regex)
            Get
                If _FeedPatterns Is Nothing Then _FeedPatterns = New Dictionary(Of String, Regex)
                Return _FeedPatterns
            End Get
        End Property

        Public ReadOnly Property FileExtensions() As List(Of String)
            Get
                If _FileExtensions Is Nothing Then _FileExtensions = New List(Of String)
                Return _FileExtensions
            End Get
        End Property

        Public Property FileUrl As Uri

        Public ReadOnly Property Gadgets() As GadgetCollection
            Get
                If _Gadgets Is Nothing Then _Gadgets = New GadgetCollection(Me)
                Return _Gadgets
            End Get
        End Property

        Public ReadOnly Property HomeUrl() As Uri
            Get
                If ShortUrl IsNot Nothing Then Return ShortUrl
                Return New Uri(Url.ToString & "index.php")
            End Get
        End Property

        Public ReadOnly Property Interwikis() As Dictionary(Of String, Wiki)
            Get
                If _Interwikis Is Nothing Then _Interwikis = New Dictionary(Of String, Wiki)
                Return _Interwikis
            End Get
        End Property

        Public Property IsCustom As Boolean
        Public Property IsDefault As Boolean
        Public Property IsHidden As Boolean
        Public Property IsLoaded As Boolean
        Public Property IsPublicEditable As Boolean = True
        Public Property IsPublicReadable As Boolean = True

        Public ReadOnly Property IsWikimedia() As Boolean
            Get
                Return (Family Is App.Families.Wikimedia)
            End Get
        End Property

        Public Property Lag As Integer
        Public Property Language As Language
        Public Property License As String
        Public Property LicenseUrl As Uri

        Public ReadOnly Property Logs() As LogsCollection
            Get
                If _Logs Is Nothing Then _Logs = New LogsCollection(Me)
                Return _Logs
            End Get
        End Property

        Public ReadOnly Property MagicWordAliases() As Dictionary(Of String, String)
            Get
                If _MagicWordAliases Is Nothing Then _MagicWordAliases = New Dictionary(Of String, String)
                Return _MagicWordAliases
            End Get
        End Property

        Public ReadOnly Property MagicWords() As Dictionary(Of String, String)
            Get
                If _MagicWords Is Nothing Then _MagicWords = New Dictionary(Of String, String)
                Return _MagicWords
            End Get
        End Property

        Public Property MainPage As Page

        Public ReadOnly Property Files() As FileCollection
            Get
                If _Files Is Nothing Then _Files = New FileCollection(Me)
                Return _Files
            End Get
        End Property

        Public ReadOnly Property Message(ByVal name As String) As String
            Get
                If Messages.ContainsKey(name) Then Return Messages(name) Else Return Nothing
            End Get
        End Property

        Public ReadOnly Property Messages() As Dictionary(Of String, String)
            Get
                If _Messages Is Nothing Then _Messages = New Dictionary(Of String, String)
                Return _Messages
            End Get
        End Property

        Public Property Name As String

        Public ReadOnly Property Pages() As PageCollection
            Get
                If _Pages Is Nothing Then _Pages = New PageCollection(Me)
                Return _Pages
            End Get
        End Property

        Public Property Preferences() As List(Of String)
            Get
                Return _Preferences
            End Get
            Set(ByVal value As List(Of String))
                _Preferences = value
            End Set
        End Property

        Public ReadOnly Property Queues() As QueueCollection
            Get
                If _Queues Is Nothing Then _Queues = New QueueCollection(Me)
                Return _Queues
            End Get
        End Property

        Public ReadOnly Property Rc() As RcSource
            Get
                If _Rc Is Nothing Then _Rc = New RcSource(Me)
                Return _Rc
            End Get
        End Property

        Public ReadOnly Property RecentChanges() As Dictionary(Of Integer, QueueItem)
            Get
                If _RecentChanges Is Nothing Then _RecentChanges = New Dictionary(Of Integer, QueueItem)
                Return _RecentChanges
            End Get
        End Property

        Public ReadOnly Property ReviewFlags() As ReviewFlagCollection
            Get
                If _ReviewFlags Is Nothing Then _ReviewFlags = New ReviewFlagCollection(Me)
                Return _ReviewFlags
            End Get
        End Property

        Public ReadOnly Property Revisions() As RevisionCollection
            Get
                If _Revisions Is Nothing Then _Revisions = New RevisionCollection(Me)
                Return _Revisions
            End Get
        End Property

        Public Property SecureUrl As Uri

        Public ReadOnly Property ServerTime() As Date
            Get
                Return Date.UtcNow + Config.ServerTimeOffset
            End Get
        End Property

        Public Property ShortUrl As Uri

        Public ReadOnly Property Skins() As Dictionary(Of String, WikiSkin)
            Get
                If _Skins Is Nothing Then _Skins = New Dictionary(Of String, WikiSkin)
                Return _Skins
            End Get
        End Property

        Public ReadOnly Property Spaces() As SpaceCollection
            Get
                If _Spaces Is Nothing Then _Spaces = New SpaceCollection(Me)
                Return _Spaces
            End Get
        End Property

        Public ReadOnly Property SpamLists As SpamListCollection
            Get
                If _SpamLists Is Nothing Then _SpamLists = New SpamListCollection(Me)
                Return _SpamLists
            End Get
        End Property

        Public ReadOnly Property Threads() As CommentCollection
            Get
                If _Threads Is Nothing Then _Threads = New CommentCollection(Me)
                Return _Threads
            End Get
        End Property

        Public Property TitleList As TitleList
        Public Property Type As String
        Public Property Url As Uri

        Public ReadOnly Property UserGroups() As UserGroupCollection
            Get
                If _UserGroups Is Nothing Then _UserGroups = New UserGroupCollection(Me)
                Return _UserGroups
            End Get
        End Property

        Public ReadOnly Property UserRights As List(Of String)
            Get
                Dim result As New List(Of String)

                For Each group As UserGroup In UserGroups.All
                    For Each right As String In group.Rights
                        result.Merge(right)
                    Next right
                Next group

                Return result
            End Get
        End Property

        Public ReadOnly Property Users() As UserCollection
            Get
                If _Users Is Nothing Then _Users = New UserCollection(Me)
                Return _Users
            End Get
        End Property

        Public Function FindUserWithRight(ByVal right As String) As User
            If Users.Anonymous.HasRight(right) Then Return Users.Anonymous

            For Each user As User In Users.All
                If user.HasRight(right) Then Return user
            Next user

            Return Nothing
        End Function

        Public Function InterwikiFor(ByVal wiki As Wiki) As String
            If wiki Is Me Then Return Nothing

            If IsWikimedia AndAlso wiki.IsWikimedia Then
                If Type = wiki.Type AndAlso Type <> "special" Then Return wiki.Language.Code
                If Type Is Nothing AndAlso wiki.Type = "wikipedia" Then Return wiki.Language.Code
                If Language Is wiki.Language AndAlso Type <> "special" Then Return TypePrefix(wiki.Type)
                If wiki.Type Is Nothing OrElse wiki.Type = "special" Then Return wiki.Code
                Return TypePrefix(wiki.Type) & ":" & wiki.Language.Code
            End If

            For Each item As KeyValuePair(Of String, Wiki) In Interwikis
                If item.Value Is wiki Then Return item.Key
            Next item

            Return Nothing
        End Function

        Public Overrides Function ToString() As String
            Return _Name
        End Function

        Private Shared Function TypePrefix(ByVal Type As String) As String
            Select Case Type
                Case "wikipedia" : Return "w"
                Case "wiktionary" : Return "t"
                Case "wikibooks" : Return "b"
                Case "wikisource" : Return "s"
                Case "wikiversity" : Return "v"
                Case Else : Return Nothing
            End Select
        End Function

    End Class

    Friend Class WikiCollection

        Private ReadOnly _All As New Dictionary(Of String, Wiki)
        Private ReadOnly _Default As Wiki

        Public Sub New()
            _Default = New Wiki("default")
            _Default.IsDefault = True
            _Default.IsHidden = True
        End Sub

        Public ReadOnly Property All() As IList(Of Wiki)
            Get
                Return _All.Values.ToList.AsReadOnly
            End Get
        End Property

        Public ReadOnly Property Contains(ByVal code As String) As Boolean
            Get
                Return _All.ContainsKey(code)
            End Get
        End Property

        Public ReadOnly Property [Default]() As Wiki
            Get
                Return _Default
            End Get
        End Property

        Public Property [Global]() As Wiki

        Default Public ReadOnly Property Item(ByVal code As String) As Wiki
            Get
                If Not _All.ContainsKey(code) Then _All.Add(code, New Wiki(code))
                Return _All(code)
            End Get
        End Property

        Public Sub Remove(ByVal wiki As Wiki)
            _All.Unmerge(wiki.Code)
        End Sub

    End Class

    <Diagnostics.DebuggerDisplay("{Code}")>
    Friend Class WikiSkin

        Private _Code As String
        Private _Name As String
        Private _Wiki As Wiki

        Public Sub New(ByVal wiki As Wiki, ByVal code As String, ByVal name As String)
            _Code = code
            _Name = name
            _Wiki = wiki
        End Sub

        Public ReadOnly Property Code() As String
            Get
                Return _Code
            End Get
        End Property

        Public ReadOnly Property Name() As String
            Get
                Return _Name
            End Get
        End Property

        Public ReadOnly Property Wiki() As Wiki
            Get
                Return _Wiki
            End Get
        End Property

        Public Overrides Function ToString() As String
            Return Name & If(Wiki.Config.DefaultSkin = Code, " ({0})".FormatForUser(Msg("a-default")), "")
        End Function

    End Class

End Namespace
