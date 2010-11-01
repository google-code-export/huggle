Imports System.Collections.Generic

Namespace Huggle.Actions

    Public Class ResetPreferences : Inherits Query

        Public Sub New(ByVal session As Session)
            MyBase.New(session, Msg("query-resetprefs-desc", session.User.FullName))
        End Sub

        Public Overrides Sub Start()
            If User.IsAnonymous Then OnFail(Msg("query-resetprefs-anon")) : Return
            OnStarted()
            OnProgress(Msg("query-resetprefs-progress"))

            'Get token
            If Session.EditToken Is Nothing Then
                Dim tokenQuery As New TokenQuery(Session)
                tokenQuery.Start()
                If tokenQuery.IsFailed Then OnFail(tokenQuery.Result) : Return
            End If

            'Reset preferences, must use UI for this
            Dim req As New UIRequest(Session, Description, New QueryString("title", "Special:Preferences/reset"),
                New QueryString("wpEditToken", Session.EditToken))

            req.Start()
            If req.IsFailed Then OnFail(req.Result) : Return

            If Not req.Response.Contains("<div class=""successbox"">") Then OnFail(Msg("error-scrape")) : Return

            'Load preferences again to see what they were reset to
            Dim req2 As New ApiRequest(Session, Description, New QueryString(
                "action", "query",
                "meta", "userinfo",
                "uiprop", "options"))

            req2.Start()
            If req2.IsFailed Then OnFail(req2.Result) : Return

            User.Config.SaveLocal()
            OnSuccess()
        End Sub

    End Class

End Namespace
