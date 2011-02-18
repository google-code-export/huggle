﻿Namespace Huggle.Actions

    Friend Class Logout : Inherits Query

        Public Sub New(ByVal session As Session)
            MyBase.New(session, Msg("logout-desc", session.User.FullName))
        End Sub

        Public Overrides Sub Start()
            'Don't send a logout request if the wiki is using unified login. Unified login logs out
            '*all* an account's login sessions, not just the active one. So logging out your primary 
            'account in Huggle would also destroy your browser's session, which is undesirable.
            If Not User.IsUnified Then
                OnProgress(Msg("logout-progress", User.FullName))

                Dim req As New ApiRequest(Session, Description, New QueryString("action", "logout"))
                req.Start()
                If req.IsFailed Then OnFail(req.Result) : Return
            End If

            Session.Discard()

            'Deactivate the recent changes feed if there are no accounts using it
            If App.Sessions.ActiveForWiki(User.Wiki).Count = 0 Then
                Wiki.Rc.Enabled = False
                If Wiki.Family IsNot Nothing AndAlso Wiki.Family.Feed IsNot Nothing Then Wiki.Family.Feed.RemoveWiki(Wiki)
            End If

            OnSuccess()
        End Sub

    End Class

End Namespace