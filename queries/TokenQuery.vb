Namespace Huggle.Actions

    'Retrieve edit token

    Class TokenQuery : Inherits Query

        Public Sub New(ByVal session As Session)
            MyBase.New(session, Msg("token-desc"))
        End Sub

        Public Overrides Sub Start()
            OnProgress(Msg("token-progress"))
            OnStarted()

            Dim req As New ApiRequest(Session, Description, New QueryString( _
                "action", "query", _
                "prop", "info", _
                "titles", "-", _
                "intoken", "edit"))

            req.Start()
            If req.IsErrored Then OnFail(req.Result.Wrap(Msg("error-notoken"))) : Return

            'If the request succeeds but no token was extracted, the response is broken or its format has changed
            If Session.EditToken Is Nothing Then OnFail(Msg("error-notoken")) : Return

            OnSuccess()
        End Sub

    End Class

End Namespace
