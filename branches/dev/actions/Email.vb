Namespace Huggle.Queries

    'E-mail a user

    Class EmailQuery : Inherits Query

        Private _CopyMe As Boolean
        Private _Subject As String
        Private _Target As User
        Private _Text As String

        Public Sub New(ByVal session As Session, ByVal target As User, ByVal subject As String, ByVal text As String)
            MyBase.New(session, Msg("email-desc"))

            _Subject = subject
            _Target = target
            _Text = text
        End Sub

        Public Property CopyMe() As Boolean
            Get
                Return _CopyMe
            End Get
            Set(ByVal value As Boolean)
                _CopyMe = value
            End Set
        End Property

        Public ReadOnly Property Subject() As String
            Get
                Return _Subject
            End Get
        End Property

        Public ReadOnly Property Target() As User
            Get
                Return _Target
            End Get
        End Property

        Public ReadOnly Property Text() As String
            Get
                Return _Text
            End Get
        End Property

        Public Overrides Sub Start()
            If User.ExtendedInfoKnown AndAlso Not User.IsEmailable Then OnFail(Msg("email-notemailable")) : Return

            OnProgress(Msg("email-progress", Target))
            OnStarted()

            'Get token
            If Not Session.HasTokens Then
                Dim tokenQuery As New TokenQuery(Session)
                tokenQuery.Start()
                If tokenQuery.IsFailed Then OnFail(tokenQuery.Result) : Return
            End If

            'Create query string
            Dim query As New QueryString(
                "action", "emailuser",
                "target", User.Name,
                "subject", Subject,
                "text", Text,
                "token", Session.Tokens("email"))

            If CopyMe Then query.Add("ccme")

            'Send e-mail
            Dim req As New ApiRequest(Session, Description, query)
            req.Start()
            If req.IsFailed Then OnFail(req.Result)

            OnSuccess()
        End Sub

    End Class

End Namespace
