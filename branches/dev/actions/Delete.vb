Namespace Huggle.Actions

    'Delete a page

    Class DeleteQuery : Inherits Query

        Private _Comment As String
        Private _Page As Page
        Private _Watch As WatchAction

        Public Sub New(ByVal session As Session, ByVal page As Page, ByVal comment As String)
            MyBase.New(session, Msg("delete-desc", page))
            _Comment = comment
            _Page = page
        End Sub

        Public ReadOnly Property Comment() As String
            Get
                Return _Comment
            End Get
        End Property

        Public ReadOnly Property Page() As Page
            Get
                Return _Page
            End Get
        End Property

        Public Property Watch() As WatchAction
            Get
                Return _Watch
            End Get
            Set(ByVal value As WatchAction)
                _Watch = value
            End Set
        End Property

        Public Overrides Sub Start()
            OnProgress(Msg("delete-progress", Page))

            'Get token
            If Session.EditToken Is Nothing Then
                Dim tokenQuery As New TokenQuery(Session)
                tokenQuery.Start()
                If tokenQuery.IsErrored Then OnFail(tokenQuery.Result) : Return
            End If

            'Create query string
            Dim query As New QueryString( _
                "action", "delete", _
                "title", Page, _
                "reason", Comment, _
                "token", Session.EditToken, _
                "watchlist", Watch.ToString.ToLower)

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
