Namespace Huggle.Actions

    'Get recent changes

    Class ChangesQuery : Inherits Query

        Private _Limit As Integer

        Friend Sub New(ByVal session As Session)
            MyBase.New(session, Msg("changes-desc"))
            Limit = User.ApiLimit
        End Sub

        Friend Property Limit() As Integer
            Get
                Return _Limit
            End Get
            Set(ByVal value As Integer)
                _Limit = value
            End Set
        End Property

        Friend Overrides Sub Start()
            OnProgress(Msg("changes-progress"))
            OnStarted()

            'Don't ask for patrolled status unless user has permission to view
            'TODO: figure out why MediaWiki insists on concealing this information from those who can't patrol
            Dim query As New QueryString( _
                "action", "query", _
                "list", "recentchanges", _
                "rclimit", "max", _
                "rcprop", "user|comment|flags|timestamp|title|ids|sizes|redirect|loginfo" _
                    & If(User.HasRight("patrol"), "|patrolled", ""))

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