Namespace Huggle.Queries

    Friend Class Watch : Inherits Query

        Private _Page As Page

        Public Sub New(ByVal session As Session, ByVal page As Page, ByVal action As WatchAction)
            MyBase.New(session, Msg("watch-desc", page))
            _Action = action
            _Page = page.SubjectPage
        End Sub

        Public Property Action() As WatchAction

        Public ReadOnly Property Page() As Page
            Get
                Return _Page
            End Get
        End Property

        Public Overrides Sub Start()

            'Get token
            If Not Session.HasTokens Then
                Dim tokenQuery As New TokenQuery(Session)
                tokenQuery.Start()
                If tokenQuery.IsErrored Then OnFail(tokenQuery.Result) : Return
            End If

            If Action = WatchAction.Preferences Then _
                If User.Preferences.WatchEdits Then Action = WatchAction.Watch _
                Else Action = WatchAction.NoChange

            If Action = WatchAction.NoChange Then OnSuccess() : Return
            If Action = WatchAction.Watch AndAlso Page.IsWatchedBy(User) Then OnSuccess() : Return
            If Action = WatchAction.Unwatch AndAlso Not Page.IsWatchedBy(User) Then OnSuccess() : Return

            OnProgress(Msg("watch-progress", Page))

            'Create query string
            Dim query As New QueryString(
                "title", Page.Title,
                "token", Session.Tokens("watch"),
                "action", If(Action = WatchAction.Watch, "watch", "unwatch"))

            'Watch the page
            Dim req As New ApiRequest(Session, Description, query)
            req.Start()
            If req.Result.IsError Then OnFail(req.Result.Message) : Return

            If Action = WatchAction.Watch Then User.Watchlist.Merge(Page.Title) _
                Else User.Watchlist.Unmerge(Page.Title)

            OnSuccess()
        End Sub

    End Class

End Namespace