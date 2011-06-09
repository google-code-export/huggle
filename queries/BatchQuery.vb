Imports Huggle.Scripting
Imports System
Imports System.Collections.Generic
Imports System.Windows.Forms

Namespace Huggle.Queries

    'Fetch specified information about specified items
    'using as few queries as possible for efficiency

    Class BatchQuery : Inherits Query

        Private _Items As New List(Of BatchInfo)
        Private _Queries As New List(Of Process)

        Private IsExecuting As Boolean

        Private Shared WithEvents Timer As New Timer

        Private Const MaxExtraQueries As Integer = 10
        Private Shared ReadOnly Current As Dictionary(Of Wiki, BatchQuery)

        Public Sub New(ByVal session As Session)
            MyBase.New(session, Msg("batch-desc"))
        End Sub

        Public ReadOnly Property Items() As List(Of BatchInfo)
            Get
                Return _Items
            End Get
        End Property

        Public ReadOnly Property Queries() As List(Of Process)
            Get
                Return _Queries
            End Get
        End Property

        Public Sub Prepare()
            Dim result As New List(Of BatchInfo)

            'Each type of query we might make
            Dim Categories As Boolean
            Dim CategoryInfo As Boolean
            Dim CategoryMembership As New List(Of Category)
            Dim Diffs As New List(Of Diff)
            Dim Expansion As New List(Of String)
            Dim Externals As Boolean
            Dim Langlinks As Boolean
            Dim LastRev As Boolean
            Dim Links As Boolean
            Dim Media As Boolean
            Dim MediaInfo As Boolean
            Dim PageCreation As New List(Of Page)
            Dim PageDeletedRevs As New List(Of Page)
            Dim PageInfo As New List(Of Page)
            Dim PageText As New List(Of Page)
            Dim Redirects As New List(Of Page)
            Dim RedirectTarget As New List(Of Page)
            Dim RevisionChange As New List(Of Revision)
            Dim RevisionInfo As New List(Of Revision)
            Dim RevisionText As New List(Of Revision)
            Dim Stats As Boolean
            Dim Transclusions As Boolean
            Dim UserDeletedRevs As New List(Of User)
            Dim UserInfo As New List(Of User)

            Dim extraQueries As Integer = 0

            'Convert list of information to list of queries needed
            For Each info As BatchInfo In Items
                If info.Item Is Nothing Then
                    If info.Type = "stats" Then Stats = True
                    If info.Type.StartsWithI("expand-") Then Expansion.Add(info.Type.FromFirst("expand-"))

                ElseIf TypeOf info.Item Is Page Then
                    Dim Page As Page = CType(info.Item, Page)

                    If info.Type.StartsWithI("category-") Then
                        Dim Category As Category = info.Wiki.Categories(info.Type.FromFirst("category-"))

                        CategoryMembership.Merge(Category)
                        PageInfo.Merge(Page)
                    End If

                    Select Case info.Type
                        Case "assessment" : Categories = True : PageInfo.Merge(Page)
                        Case "categories" : Categories = True : PageInfo.Merge(Page)
                        Case "categoryinfo" : CategoryInfo = True : PageInfo.Merge(Page)
                        Case "creation"
                            If extraQueries < MaxExtraQueries Then
                                PageCreation.Merge(Page)
                                extraQueries += 1
                            Else
                                result.Add(info)
                            End If

                        Case "deletedrevs" : PageDeletedRevs.Merge(Page)
                        Case "externallinks" : Externals = True : PageInfo.Merge(Page)
                        Case "info" : PageInfo.Merge(Page)
                        Case "langlinks" : Langlinks = True : PageInfo.Merge(Page)
                        Case "lastrev" : LastRev = True
                        Case "links" : Links = True : PageInfo.Merge(Page)
                        Case "media" : Media = True : PageInfo.Merge(Page)
                        Case "mediainfo" : MediaInfo = True : PageInfo.Merge(Page)
                        Case "transclusions" : Transclusions = True : PageInfo.Merge(Page)

                        Case "target" : If RedirectTarget.Count < ApiRequest.MaxSlowLength _
                            Then RedirectTarget.Merge(Page) Else result.Add(info)

                        Case "pairexists" : If Page.IsTalkPage Then PageInfo.Merge(Page.SubjectPage) _
                            Else PageInfo.Merge(Page.TalkPage)

                        Case "redirects"
                            If extraQueries < MaxExtraQueries Then
                                Redirects.Merge(Page)
                                extraQueries += 1
                            Else
                                result.Add(info)
                            End If

                        Case "sections", "text" : If PageText.Count < ApiRequest.MaxSlowLength _
                            Then PageText.Merge(Page) Else result.Add(info)
                    End Select

                ElseIf TypeOf info.Item Is User Then
                    Dim User As User = CType(info.Item, User)

                    Select Case info.Type
                        Case "autoconfirmation", "creation", "editcount", "emailable", "groups", "info"
                            If UserInfo.Count < ApiRequest.MaxSlowLength Then UserInfo.Merge(User) Else result.Add(info)

                        Case "sanctions", "shared", "talkpage" : If PageInfo.Count < ApiRequest.MaxSlowLength _
                            Then PageInfo.Merge(User.Talkpage) Else result.Add(info)

                        Case "userpage" : If PageInfo.Count < ApiRequest.MaxSlowLength _
                            Then PageInfo.Merge(User.Userpage) Else result.Add(info)

                        Case "deletedrevs" : If UserDeletedRevs.Count < ApiRequest.MaxSlowLength _
                            Then UserDeletedRevs.Merge(User) Else result.Add(info)
                    End Select

                ElseIf TypeOf info.Item Is Revision Then
                    Dim rev As Revision = CType(info.Item, Revision)

                    Select Case info.Type
                        Case "bytes"
                            If rev.Id > rev.Wiki.Config.PageSizeTransition Then
                                If RevisionInfo.Count < ApiRequest.MaxSlowLength Then RevisionInfo.Merge(rev) Else result.Add(info)
                            Else
                                If RevisionText.Count < ApiRequest.MaxSlowLength Then RevisionText.Merge(rev) Else result.Add(info)
                            End If

                        Case "change"
                            If rev.IsCreation Then
                                If RevisionText.Count < ApiRequest.MaxSlowLength Then RevisionText.Merge(rev) Else result.Add(info)

                            ElseIf rev.Prev Is Nothing Then
                                If RevisionInfo.Count < ApiRequest.MaxSlowLength Then RevisionInfo.Merge(rev) Else result.Add(info)

                            ElseIf rev.Prev.Id > rev.Wiki.Config.PageSizeTransition Then
                                If RevisionInfo.Count < ApiRequest.MaxSlowLength Then RevisionText.Merge(rev.Prev) Else result.Add(info)

                            Else
                                If RevisionText.Count < ApiRequest.MaxSlowLength Then RevisionText.Merge(rev.Prev) Else result.Add(info)
                            End If

                        Case "info" : If RevisionInfo.Count < ApiRequest.MaxSlowLength _
                            Then RevisionInfo.Merge(rev) Else result.Add(info)

                        Case "text" : If RevisionText.Count < ApiRequest.MaxSlowLength _
                            Then RevisionText.Merge(rev) Else result.Add(info)
                    End Select
                End If
            Next info

            'Make the requests
            Queries.Clear()

            If PageText.Count > 0 Then Queries.Add(New PageInfoQuery(Session, PageText) With {.Content = True})

            If PageInfo.Count > 0 Then Queries.Add(New PageInfoQuery(Session, PageInfo) With {
                .Categories = Categories, .Externals = Externals, .LangLinks = Langlinks, .Links = Links,
                .Media = Media, .Revision = LastRev, .Transclusions = Transclusions})

            If PageCreation.Count > 0 Then
                For Each Page As Page In PageCreation
                    Queries.Add(New PageDetailQuery(Session, Page) With {.Revs = RevType.First})
                Next Page
            End If

            'If PageDeletedRevs.Count > 0 Then Queries.Add(New DeletedHistoryQuery(Session, PageDeletedRevs))
            If UserInfo.Count > 0 Then Queries.Add(New UserInfoQuery(Session, UserInfo))

            'If UserDeletedRevs.Count > 0 Then
            '    For Each User As User In UserDeletedRevs
            '        Queries.Add(New DeletedContribsQuery(Session, User))
            '    Next User
            'End If

            If Diffs.Count > 0 Then
                For Each Diff As Diff In Diffs
                    Queries.Add(New DiffQuery(Session, Diff))
                Next Diff
            End If

            'If RevisionChange.Count > 0 Then
            '    For Each rev As Revision In RevisionChange
            '        Queries.Add(New HistoryQuery(Session, rev.Page, 2, False))
            '    Next rev
            'End If

            If RevisionInfo.Count > 0 Then Queries.Add(New RevisionInfoQuery(Session, RevisionInfo.ToArray))
            If RedirectTarget.Count > 0 Then Queries.Add(New RedirectsQuery(Session, RedirectTarget))
            If RevisionText.Count > 0 Then Queries.Add(New RevisionDetailQuery(Session, RevisionText, True))
            If Expansion.Count > 0 Then Queries.Add(New ExpandTemplatesQuery(Session, Expansion))

            _Items = result
        End Sub

        Public Sub Execute(ByVal context As Object)
            If Not IsExecuting Then
                IsExecuting = True
                App.DoParallel(Queries)
                IsExecuting = False
            End If
        End Sub

        Public Shared ReadOnly Property CurrentFor(ByVal Wiki As Wiki) As BatchQuery
            Get
                'If Not Current.ContainsKey(Wiki) Then Current.Add(Wiki, New BatchQuery(Wiki.Account))
                Return Current(Wiki)
            End Get
        End Property

        Public Shared Property Interval() As Integer
            Get
                Return Timer.Interval
            End Get
            Set(ByVal value As Integer)
                Timer.Interval = value
            End Set
        End Property

        Public Shared Sub ResetState()
            Timer.Stop()
            Current.Clear()
            Timer.Start()
        End Sub

        Public Shared Sub Timer_Tick() Handles Timer.Tick
            'Execute all pending requests
            For Each request As BatchQuery In Current.Values
                If request.Items.Count > 0 Then
                    Timer.Stop()
                    request.Prepare()
                    request.Execute("Queue filters")
                    Timer.Start()
                End If
            Next request

            Dim keys As New List(Of Wiki)(Current.Keys)

            For Each wiki As Wiki In keys
                Current.Remove(wiki)
                'Current.Add(Wiki, New BatchQuery(Wiki.Account))
            Next wiki
        End Sub

        Public Overrides Sub Start()

        End Sub

    End Class

    Friend Structure BatchInfo

        'Represents a piece of information about a wiki or something in it
        'to be requested by the query batcher

        Private _Item As QueueItem
        Private _Type As String
        Private _Wiki As Wiki

        Public Sub New(ByVal wiki As Wiki, ByVal type As String)
            _Wiki = wiki
            _Type = type
        End Sub

        Public Sub New(ByVal wiki As Wiki, ByVal item As QueueItem, ByVal type As String)
            _Wiki = wiki
            _Item = item
            _Type = type
        End Sub

        Public ReadOnly Property Item() As QueueItem
            Get
                Return _Item
            End Get
        End Property

        Public ReadOnly Property Type() As String
            Get
                Return _Type
            End Get
        End Property

        Public ReadOnly Property Wiki() As Wiki
            Get
                Return _Wiki
            End Get
        End Property

    End Structure

End Namespace

