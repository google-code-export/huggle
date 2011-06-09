Namespace Huggle.Queries

    'Retrieve edit token

    Class TokenQuery : Inherits Query

        Public Sub New(ByVal session As Session)
            MyBase.New(session, Msg("token-desc"))
        End Sub

        Public Overrides Sub Start()
            OnProgress(Msg("token-progress"))
            OnStarted()

            Dim req As New ApiRequest(Session, Description, New QueryString(
                "action", "query",
                "prop", "info",
                "titles", "-",
                "intoken", "edit|delete|protect|move|block|unblock|email|import|watch"))

            req.Start()
            If req.IsErrored Then OnFail(req.Result.Wrap(Msg("error-notoken"))) : Return

            'If the request succeeds but no token was extracted, the response is broken or its format has changed
            If Session.Tokens("edit") Is Nothing Then OnFail(Msg("error-notoken")) : Return

            Session.HasTokens = True
            OnSuccess()
        End Sub

    End Class

End Namespace
