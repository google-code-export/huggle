Namespace Huggle.Queries

    'Get recent changes

    Class ChangesQuery : Inherits Query

        Private _Limit As Integer

        Public Sub New(ByVal session As Session)
            MyBase.New(session, Msg("changes-desc"))
            Limit = User.ApiLimit
        End Sub

        Public Property Limit() As Integer
            Get
                Return _Limit
            End Get
            Set(ByVal value As Integer)
                _Limit = value
            End Set
        End Property

        Public Overrides Sub Start()
            OnProgress(Msg("changes-progress"))
            OnStarted()

            'Don't ask for patrolled status unless user has permission to view
            'TODO: figure out why MediaWiki insists on concealing this information from those who can't patrol
            Dim query As New QueryString(
                "action", "query",
                "list", "recentchanges",
                "rclimit", "max",
                "rcprop", "user|comment|flags|timestamp|title|ids|sizes|redirect|loginfo" &
                    If(User.HasRight("patrol"), "|patrolled", ""))

            'Abuse filter has its own module, can't get it through standard logs
            If User.HasRight("abuselog") Then
                query.Add("list", "abuselog|recentchanges")
                query.Add("afllimit", "max")
                query.Add("aflprop", "ids|filter|user|ip|title|action|result|timestamp")
            End If

            Dim req As New ApiRequest(Session, Description, query)
            req.Limit = Limit
            req.DoMultiple()
            If req.IsFailed Then OnFail(req.Result)

            OnSuccess()
        End Sub

    End Class

End Namespace