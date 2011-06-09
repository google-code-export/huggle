Namespace Huggle.Queries

    'Delete a page

    Class Delete : Inherits Query

        Private _Page As Page

        Public Sub New(ByVal session As Session, ByVal page As Page)
            MyBase.New(session, Msg("delete-desc", page))
            _Comment = Comment
            _Page = page
        End Sub

        Public Property Comment() As String

        Public ReadOnly Property Page() As Page
            Get
                Return _Page
            End Get
        End Property

        Public Property Watch() As WatchAction

        Public Overrides Sub Start()
            OnProgress(Msg("delete-progress", Page))

            'Get token
            If Not Session.HasTokens Then
                Dim tokenQuery As New TokenQuery(Session)
                tokenQuery.Start()
                If tokenQuery.IsErrored Then OnFail(tokenQuery.Result) : Return
            End If

            'Create query string
            Dim query As New QueryString(
                "action", "delete",
                "title", Page,
                "reason", Comment,
                "token", Session.Tokens("delete"),
                "watchlist", Watch.ToString.ToLowerI)

            'Delete the page
            Dim req As New ApiRequest(Session, Description, query)
            req.Start()
            If req.Result.IsError Then OnFail(req.Result)

            'Update watchlist
            If Watch = WatchAction.Watch OrElse (Watch = WatchAction.Preferences _
                AndAlso User.Preferences.WatchDeletions) Then User.Watchlist.Merge(Page.SubjectPage.Title)
            If Watch = WatchAction.Unwatch Then User.Watchlist.Unmerge(Page.SubjectPage.Title)

            OnSuccess()
        End Sub

    End Class

End Namespace
