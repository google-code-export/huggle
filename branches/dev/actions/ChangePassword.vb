Imports Huggle.Net
Imports System.Collections.Generic
Imports System.IO

Namespace Huggle.Queries

    Friend Class ChangePassword : Inherits Query

        Private NewPassword As Byte()

        Public Sub New(ByVal session As Session, ByVal newPassword As Byte())
            MyBase.New(session, Msg("changepassword-desc"))
            Me.NewPassword = newPassword
        End Sub

        Public Overrides Sub Start()
            If User.IsAnonymous Then OnFail(Msg("changepassword-anon")) : Return
            OnStarted()
            OnProgress(Msg("changepassword-progress", User.FullName))

            'Get token
            If Not Session.HasTokens Then
                Dim tokenQuery As New TokenQuery(Session)
                tokenQuery.Start()
                If tokenQuery.IsFailed Then OnFail(tokenQuery.Result) : Return
            End If

            'Reset preferences, must use UI for this
            Dim req As New UIRequest(Session, Description, New QueryString("title", "Special:Preferences/reset"),
                New QueryString(
                    "token", Session.Tokens("preferences"),
                    "wpName", User.Name,
                    "wpPassword", User.Password,
                    "wpNewPassword", NewPassword,
                    "wpRetype", NewPassword))

            req.Start()
            If req.IsFailed Then OnFail(req.Result) : Return

            'Check for error message
            If req.Response.Contains("<p class=""error"">") Then OnFail(Msg("changepassword-error")) : Return

            OnSuccess()
        End Sub

    End Class

End Namespace
