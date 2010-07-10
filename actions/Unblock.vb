Namespace Huggle.Actions

    Public Class Unblock : Inherits Query

        Private _Summary As String
        Private _Target As User
        Private _Watch As WatchAction

        Public Sub New(ByVal session As Session, ByVal target As User, ByVal summary As String)
            MyBase.New(session, Msg("unblock-desc", target))
            _Summary = summary
            _Target = target
        End Sub

        Public ReadOnly Property Summary() As String
            Get
                Return _Summary
            End Get
        End Property

        Public ReadOnly Property Target() As User
            Get
                Return _Target
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
            OnProgress(Msg("unblock-progress", Target))
            OnStarted()

            'Get token
            If Session.EditToken Is Nothing Then
                Dim tokenQuery As New TokenQuery(Session)
                tokenQuery.Start()
                If tokenQuery.IsErrored Then OnFail(tokenQuery.Result) : Return
            End If

            'Create query string
            Dim query As New QueryString( _
                "action", "unblock", _
                "reason", Summary, _
                "user", User)

            'Unblock the user
            Dim req As New ApiRequest(Session, Description, query)
            req.Start()
            If req.IsFailed Then OnFail(req.Result) : Return

            'Watch user's userpage
            If Watch = WatchAction.Watch OrElse Watch = WatchAction.Unwatch Then
                Dim watchQuery As New Watch(Session, User.Userpage, Watch)
                watchQuery.Start()
            End If

            OnSuccess()
        End Sub

    End Class

End Namespace