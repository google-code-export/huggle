Imports System
Imports System.Collections.Generic
Imports System.Drawing
Imports System.Text.RegularExpressions
Imports System.Web.HttpUtility

Namespace Huggle

    <Diagnostics.DebuggerDisplay("{Title}")>
    Friend Class Page : Inherits QueueItem

        'Represents a MediaWiki page

        Private Processed As Boolean

        Private _Assessment As String
        Private _DeletedEdits As New List(Of Revision)
        Private _FirstRev As Revision
        Private _IsIgnored As Boolean
        Private _IsPriorityTalk As Boolean
        Private _IsRedirect As Boolean
        Private _LastRev As Revision
        Private _Logs As New SortedList(Of LogItem)(Function(x As LogItem, y As LogItem) Date.Compare(x.Time, y.Time))
        Private _Owner As User
        Private _RedirectTo As Page
        Private _Space As Space
        Private _Title As String
        Private _Wiki As Wiki

        Private _ExistsKnown As Boolean
        Private _TargetKnown As Boolean

        Private _Categories As New List(Of Category)
        Private _LangLinks As New List(Of String)
        Private _Links As New List(Of Page)
        Private _Media As New List(Of File)
        Private _Redirects As New List(Of Page)
        Private _TranscludedBy As New List(Of Page)
        Private _Transcludes As New List(Of Page)

        Public Event Deleted As SimpleEventHandler(Of Page)
        Public Event Edited As SimpleEventHandler(Of Revision)
        Public Event Moved(ByVal sender As Object, ByVal e As PageMoveEventArgs)
        Public Event [Protected] As SimpleEventHandler(Of Page)
        Public Event StateChanged As SimpleEventHandler(Of Page)
        Public Event HistoryChanged As SimpleEventHandler(Of Page)

        Public Sub New(ByVal wiki As Wiki, ByVal title As String, ByVal space As Space)
            _EditCount = -1
            _Exists = True
            _Space = space
            _Title = title
            _Wiki = wiki
        End Sub

        Public ReadOnly Property Assessment() As String
            Get
                If Not Processed Then Process()
                Return _Assessment
            End Get
        End Property

        Public ReadOnly Property Categories() As List(Of Category)
            Get
                Return _Categories
            End Get
        End Property

        Public Property CategoriesKnown() As Boolean

        Public ReadOnly Property Creator() As User
            Get
                If FirstRev Is Nothing Then Return Nothing
                Return FirstRev.User
            End Get
        End Property

        Public ReadOnly Property DeletedEdits() As List(Of Revision)
            Get
                Return _DeletedEdits
            End Get
        End Property

        Public Property DeletedEditsKnown() As Boolean

        Public Property EditCount() As Integer

        Public ReadOnly Property Edits() As List(Of Revision)
            Get
                Dim _Edits As New List(Of Revision)
                Dim Edit As Revision = LastRev

                While Edit IsNot Nothing AndAlso Edit IsNot Revision.Null
                    _Edits.Add(Edit)
                    If _Edits.Count > 5000 Then Return _Edits
                    Edit = Edit.Prev
                End While

                Return _Edits
            End Get
        End Property

        Public Property Exists() As Boolean

        Public ReadOnly Property ExistsKnown() As Boolean
            Get
                Return _ExistsKnown
            End Get
        End Property

        Public Property ExtendedInfoKnown() As Boolean

        Public Property ExternalLinks() As List(Of String)

        Public Property ExternalLinksKnown() As Boolean

        Public Property FirstRev() As Revision
            Get
                Return _FirstRev
            End Get
            Set(ByVal value As Revision)
                _FirstRev = value
                If FirstRev IsNot Nothing Then FirstRev.Prev = Revision.Null
            End Set
        End Property

        Public ReadOnly Property FullTitle As String
            Get
                Return Wiki.Code & ":" & Title
            End Get
        End Property

        Public Property HasDeletedEdits() As Boolean

        Public Property HistoryKnown() As Boolean

        Public Property Id() As Integer

        Public ReadOnly Property IsArticle() As Boolean
            Get
                Return (Space.IsArticleSpace)
            End Get
        End Property

        Public ReadOnly Property IsArticleTalkPage() As Boolean
            Get
                Return (Space Is Wiki.Spaces.Talk)
            End Get
        End Property

        Public ReadOnly Property IsCreatableBy(ByVal user As User) As Boolean
            Get
                If user.HasRight("protect") Then Return True
                If Space.IsEditRestricted AndAlso Not user.IsInGroup(Wiki.UserGroups("sysop")) Then Return False
                If IsSubjectPage AndAlso Not user.HasRight("createpage") Then Return False
                If IsTalkPage AndAlso Not user.HasRight("createtalk") Then Return False

                For Each item As LogItem In Logs
                    If item.Action = "protect" Then
                        Dim protection As Protection = CType(item, Protection)
                        If protection.Create.Expires < Wiki.ServerTime Then Return True
                        If protection.Create.Level = "sysop" Then Return user.IsInGroup(Wiki.UserGroups("sysop"))
                        If protection.Create.Level = "autoconfirmed" Then Return user.IsInGroup(Wiki.UserGroups("autoconfirmed"))
                    End If
                Next item

                Return True
            End Get
        End Property

        Public ReadOnly Property IsEditableBy(ByVal user As User) As Boolean
            Get
                If user.HasRight("protect") Then Return True
                If Space.IsEditRestricted AndAlso Not user.IsInGroup(Wiki.UserGroups("sysop")) Then Return False

                For Each item As LogItem In Logs
                    If item.Action = "protect" Then
                        Dim protection As Protection = CType(item, Protection)
                        If protection.Edit.Expires < Wiki.ServerTime Then Return True
                        If protection.Edit.Level = "sysop" Then Return user.IsInGroup(Wiki.UserGroups("sysop"))
                        If protection.Edit.Level = "autoconfirmed" Then Return user.IsInGroup(Wiki.UserGroups("autoconfirmed"))
                    End If
                Next item

                Return True
            End Get
        End Property

        Public Property IsIgnored() As Boolean
            Get
                If Not Processed Then Process()
                Return _IsIgnored
            End Get
            Set(ByVal value As Boolean)
                _IsIgnored = value

                If _IsIgnored Then
                    If Not Wiki.Pages.Ignored.Contains(Me) Then Wiki.Pages.Ignored.Add(Me)
                Else
                    If Wiki.Pages.Ignored.Contains(Me) Then Wiki.Pages.Ignored.Remove(Me)
                End If
            End Set
        End Property

        Public ReadOnly Property IsMovableBy(ByVal user As User) As Boolean
            Get
                If Not Space.IsMovable Then Return False
                If Not user.HasRight("move") Then Return False
                If IsRootPage AndAlso Space Is Wiki.Spaces.User _
                    AndAlso Not user.HasRight("move-rootuserpages") Then Return False
                If user.HasRight("protect") Then Return True

                For Each logItem As LogItem In Logs
                    If logItem.Action = "protect" Then
                        Dim Protection As Protection = CType(logItem, Protection)
                        If Protection.Move.Expires < Wiki.ServerTime Then Return True
                        If Protection.Move.Level IsNot Nothing _
                            Then Return user.IsInGroup(Wiki.UserGroups(Protection.Move.Level))
                    End If
                Next logItem

                Return True
            End Get
        End Property

        Public Property IsPatrolled() As Boolean

        Public Property IsPriority() As Boolean

        Public ReadOnly Property IsPriorityTalk() As Boolean
            Get
                If Not Processed Then Process()
                Return _IsPriorityTalk
            End Get
        End Property

        Public Property IsProtected() As Boolean

        Public ReadOnly Property IsProtectableBy(ByVal account As User) As Boolean
            Get
                Return account.HasRight("protect") AndAlso Not Space.IsEditRestricted
            End Get
        End Property

        Public Property IsRedirect() As Boolean
            Get
                Return _IsRedirect
            End Get
            Set(ByVal value As Boolean)
                _IsRedirect = value
                If value = False Then _TargetKnown = True
            End Set
        End Property

        Public Property IsReviewable() As Boolean

        Public ReadOnly Property IsReviewed() As Boolean
            Get
                If FirstRev Is Nothing Then Return False
                Return FirstRev.IsReviewed
            End Get
        End Property

        Public ReadOnly Property IsRootPage() As Boolean
            Get
                Return (RootPage Is Me)
            End Get
        End Property

        Public ReadOnly Property IsSubjectPage() As Boolean
            Get
                Return Space.IsSubjectSpace
            End Get
        End Property

        Public ReadOnly Property IsSubpage() As Boolean
            Get
                Return (Space.HasSubpages AndAlso Name.Contains("/"))
            End Get
        End Property

        Public ReadOnly Property IsTalkPage() As Boolean
            Get
                Return Space.IsTalkSpace
            End Get
        End Property

        Public Property IsWatchedBy(ByVal account As User) As Boolean
            Get
                Return account.Watchlist.Contains(SubjectPage.Name)
            End Get
            Set(ByVal value As Boolean)
                If value Then account.Watchlist.Merge(SubjectPage.Name) Else account.Watchlist.Unmerge(SubjectPage.Name)
            End Set
        End Property

        Public Overrides ReadOnly Property Label() As String
            Get
                Return _Title
            End Get
        End Property

        Public ReadOnly Property LangLinks() As List(Of String)
            Get
                Return _LangLinks
            End Get
        End Property

        Public Property LangLinksKnown() As Boolean

        Public Property LastRev() As Revision
            Get
                If Not Processed Then Process()
                Return _LastRev
            End Get
            Set(ByVal value As Revision)
                _LastRev = value
            End Set
        End Property

        Public ReadOnly Property LatestKnownRev() As Revision
            Get
                If Not Revision.IsKnown(LastRev) Then Return Nothing

                Dim rev As Revision = LastRev

                While Revision.IsKnown(rev.Prev)
                    rev = rev.Prev
                End While

                Return rev
            End Get
        End Property

        Public ReadOnly Property Links() As List(Of Page)
            Get
                Return _Links
            End Get
        End Property

        Public Property LinksKnown() As Boolean

        Public ReadOnly Property Logs() As List(Of LogItem)
            Get
                Return _Logs
            End Get
        End Property

        Public Property LogsKnown() As Boolean

        Public ReadOnly Property Media() As List(Of File)
            Get
                Return _Media
            End Get
        End Property

        Public Property MediaKnown() As Boolean

        Public ReadOnly Property Name() As String
            Get
                If IsArticle Then Return Title Else Return Title.FromFirst(":")
            End Get
        End Property

        Public ReadOnly Property Owner() As User
            Get
                If Not Processed Then Process()
                Return _Owner
            End Get
        End Property

        Public ReadOnly Property ParentPage() As Page
            Get
                If IsSubpage Then Return Wiki.Pages(Title.ToLast("/")) Else Return Nothing
            End Get
        End Property

        Public ReadOnly Property Protection() As Protection
            Get
                For Each logItem As LogItem In Logs
                    If logItem.Action = "protect" Then Return CType(logItem, Protection)
                Next logItem

                Return Nothing
            End Get
        End Property

        Public ReadOnly Property Redirects() As List(Of Page)
            Get
                Return _Redirects
            End Get
        End Property

        Public Property RedirectsKnown() As Boolean

        Public Property Reverted() As Boolean

        Public ReadOnly Property RootPage() As Page
            Get
                Dim Result As Page = Me

                While Result.ParentPage IsNot Nothing
                    Result = Result.ParentPage
                End While

                Return Result
            End Get
        End Property

        Public Property Sections() As New List(Of String)

        Public Property SectionsKnown() As Boolean

        Public Property SessionEdits() As Integer

        Public ReadOnly Property Space() As Space
            Get
                Return _Space
            End Get
        End Property

        Public ReadOnly Property SubjectPage() As Page
            Get
                If IsSubjectPage Then Return Me Else Return Wiki.Pages(Space.SubjectSpace, Name)
            End Get
        End Property

        Public ReadOnly Property SubpageName() As String
            Get
                If IsSubpage Then Return Name.FromLast("/") Else Return Name
            End Get
        End Property

        Public ReadOnly Property TalkPage() As Page
            Get
                If IsTalkPage Then Return Me Else Return Wiki.Pages(Space.TalkSpace, Name)
            End Get
        End Property

        Public Property Target() As Page
            Get
                Return _RedirectTo
            End Get
            Set(ByVal value As Page)
                _RedirectTo = value
                _TargetKnown = True
                IsRedirect = (value IsNot Nothing)
            End Set
        End Property

        Public ReadOnly Property TargetKnown() As Boolean
            Get
                Return _TargetKnown
            End Get
        End Property

        Public ReadOnly Property TranscludedBy() As List(Of Page)
            Get
                Return _TranscludedBy
            End Get
        End Property

        Public ReadOnly Property Transcludes() As List(Of Page)
            Get
                Return _Transcludes
            End Get
        End Property

        Public Property TranscludesKnown() As Boolean

        Public ReadOnly Property Text() As String
            Get
                If LastRev IsNot Nothing Then Return LastRev.Text Else Return Nothing
            End Get
        End Property

        Public ReadOnly Property Title() As String
            Get
                Return _Title
            End Get
        End Property

        Public ReadOnly Property Url() As Uri
            Get
                If Wiki.ShortUrl IsNot Nothing Then Return New Uri(Wiki.ShortUrl.ToString & UrlEncode(Title))
                Return New Uri(Wiki.Url.ToString & "index.php?title=" & UrlEncode(Title))
            End Get
        End Property

        Public Overrides ReadOnly Property Wiki() As Wiki
            Get
                Return _Wiki
            End Get
        End Property

        Public Sub MovedTo(ByVal newTitle As String)
            'Handle a page move
            Dim oldTitle As String = Title
            Wiki.Pages.All.Remove(Title)
            _Title = newTitle
            Wiki.Pages.All.Merge(newTitle, Me)
            RaiseEvent Moved(Me, New PageMoveEventArgs(Me, oldTitle))
        End Sub

        Public Sub OnEdit(ByVal rev As Revision)
            RaiseEvent Edited(Me, New EventArgs(Of Revision)(rev))
        End Sub

        Public Sub OnHistoryChanged()
            RaiseEvent HistoryChanged(Me, New EventArgs(Of Page)(Me))
        End Sub

        Public Sub Process()
            'Assessment class
            If IsTalkPage AndAlso CategoriesKnown Then
                For Each Category As Category In Categories
                    If Category.Name.Contains("-Class ") Then SubjectPage._Assessment = Category.Name.ToFirst("-Class ")
                Next Category
            End If

            _IsPriority = Wiki.Pages.Priority.Contains(Name)
            _IsPriorityTalk = (IsTalkPage AndAlso Wiki.Pages.Priority.Contains(SubjectPage.Name))
            _IsIgnored = Wiki.Pages.Ignored.Contains(Me)
            If (Space Is Wiki.Spaces.User OrElse Space Is Wiki.Spaces.UserTalk) _
                Then _Owner = Wiki.Users(RootPage.Name)
            Processed = True
            RefreshState()
        End Sub

        Public Sub ProcessReverts()
            If Revision.IsKnown(LastRev) Then
                Dim Rev As Revision = LastRev

                While Revision.IsKnown(Rev.Prev)
                    Rev = Rev.Prev
                End While

                While Rev IsNot LastRev
                    Rev.ProcessRevert()
                    Rev = Rev.Next
                End While
            End If

            If Not HistoryKnown Then
                Dim Rev As Revision = FirstRev

                While Revision.IsKnown(Rev)
                    Rev.ProcessRevert()
                    Rev = Rev.Next
                End While
            End If
        End Sub

        Public Sub RefreshState()
            RaiseEvent StateChanged(Me, New EventArgs(Of Page)(Me))
        End Sub

        Public Overrides Function ToString() As String
            Return _Title
        End Function

        Public Overrides ReadOnly Property LabelBackColor() As Color
            Get
                Select Case Space
                    Case Wiki.Spaces.Article : Return Color.White
                    Case Wiki.Spaces.Talk : Return Color.FromArgb(205, 255, 205)
                    Case Wiki.Spaces.User, Wiki.Spaces.UserTalk : Return Color.FromArgb(215, 215, 255)
                    Case Wiki.Spaces.Project, Wiki.Spaces.ProjectTalk : Return Color.FromArgb(255, 210, 255)
                    Case Else : Return Color.FromArgb(255, 230, 210)
                End Select
            End Get
        End Property

        Public Overrides ReadOnly Property Icon() As Image
            Get
                Return Nothing
            End Get
        End Property

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

    Friend Class PageMoveEventArgs : Inherits EventArgs

        Private _OldTitle As String
        Private _Page As Page

        Public Sub New(ByVal page As Page, ByVal oldTitle As String)
            _OldTitle = oldTitle
            _Page = page
        End Sub

        Public ReadOnly Property OldName() As String
            Get
                Return _OldTitle
            End Get
        End Property

        Public ReadOnly Property Page() As Page
            Get
                Return _Page
            End Get
        End Property

    End Class

    Friend Class PageCollection

        Private Wiki As Wiki

        Private ReadOnly _All As New Dictionary(Of String, Page)
        Private ReadOnly _Ignored As New List(Of Page)
        Private ReadOnly _Priority As New List(Of String)

        Public Sub New(ByVal wiki As Wiki)
            Me.Wiki = wiki
        End Sub

        Public ReadOnly Property All() As Dictionary(Of String, Page)
            Get
                Return _All
            End Get
        End Property

        Public Property Count() As Integer = -1

        Public ReadOnly Property Ignored() As List(Of Page)
            Get
                Return _Ignored
            End Get
        End Property

        Public ReadOnly Property Priority() As List(Of String)
            Get
                Return _Priority
            End Get
        End Property

        Default Public ReadOnly Property Item(ByVal title As String) As Page
            Get
                Return FromTitle(title)
            End Get
        End Property

        Default Public ReadOnly Property Item(ByVal space As Integer, ByVal title As String) As Page
            Get
                Return FromNsAndTitle(space, title)
            End Get
        End Property

        Default Public ReadOnly Property Item(ByVal space As Space, ByVal title As String) As Page
            Get
                Return FromNsAndTitle(space, title)
            End Get
        End Property

        Public Function FromNsAndTitle(ByVal space As Integer, ByVal title As String) As Page
            If title Is Nothing Then Throw New ArgumentNullException("title")
            If Not All.ContainsKey(title) Then Return FromNsAndTitle(Wiki.Spaces(space), title)
            Return All(title)
        End Function

        Public Function FromNsAndTitle(ByVal space As Space, ByVal title As String) As Page
            If title Is Nothing Then Throw New ArgumentNullException("title")
            If Not All.ContainsKey(title) Then All.Add(title, New Page(Wiki, title, space))
            Return All(title)
        End Function

        Public Function FromNsAndName(ByVal space As Integer, ByVal name As String) As Page
            Return Item(Wiki.Spaces(space), Wiki.Spaces(space).Name & ":" & name)
        End Function

        Public Function FromNsAndName(ByVal space As Space, ByVal name As String) As Page
            Return Item(space, space.Name & ":" & name)
        End Function

        Public Function FromString(ByVal title As String) As Page

            'Handle interwiki links
            For Each interwiki As KeyValuePair(Of String, Wiki) In Wiki.Interwikis
                If title.ToLowerI.StartsWithI(interwiki.Key & ":") _
                    Then Return interwiki.Value.Pages.FromString(title.Substring(interwiki.Key.Length))
            Next interwiki

            title = SanitizeTitle(title)
            If title Is Nothing Then Return Nothing

            Return Item(title)
        End Function

        Public Function FromTitle(ByVal title As String) As Page
            If title Is Nothing Then Throw New ArgumentNullException("title")
            If Not All.ContainsKey(title) Then Return FromNsAndTitle(Wiki.Spaces.FromTitle(title), title)
            Return All(title)
        End Function

        Public Function SanitizeTitle(ByVal title As String) As String
            If title Is Nothing Then Return Nothing

            'Remove illegal characters
            If title.Contains("#") Then title = title.ToFirst("#")
            title = title.Remove("[", "]", "{", "}", "|", "<", "#", Tab, LF)
            title = title.TrimStart(New Char() {":"c})

            title = title.Replace("_", " ").Trim()

            If String.IsNullOrEmpty(title) Then Return Nothing

            'Capitalize first letter of title
            If Not Wiki.Config.FirstLetterCaseSensitive Then title = UcFirst(title)

            'Handle namespaces
            Dim space As Space = Wiki.Spaces.FromTitle(title)

            'Handle special namespace
            If space.IsSpecial Then Return Nothing

            If Not space.IsArticleSpace Then
                'Normalize namespace name
                title = space.Name & ":" & title.FromFirst(":")

                'Reject titles that are a namespace name followed by a colon
                Dim cPos As Integer = title.IndexOfI(":")
                If cPos = title.Length - 1 Then Return Nothing

                'Capitalize first letter after namespace
                If Not Wiki.Config.FirstLetterCaseSensitive AndAlso cPos >= 0 _
                    Then title = title.Substring(0, cPos + 1) &
                    title.Substring(cPos + 1, 1).ToUpperI & title.Substring(cPos + 2)
            End If

            Return title
        End Function

    End Class

End Namespace