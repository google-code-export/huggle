Imports System.Collections.Generic

Namespace Huggle.Actions

    Public Class ResetPreferences : Inherits Query

        Public Sub New(ByVal session As Session)
            MyBase.New(session, Msg("resetprefs-desc"))
        End Sub

        Public Overrides Sub Start()
            If User.IsAnonymous Then OnFail(Msg("setprefs-anon")) : Return
            OnStarted()
            OnProgress(Msg("resetprefs-progress", User.FullName))

            'Get token
            If Session.EditToken Is Nothing Then
                Dim tokenQuery As New TokenQuery(Session)
                tokenQuery.Start()
                If tokenQuery.IsFailed Then OnFail(tokenQuery.Result) : Return
            End If

            'Reset preferences, must use UI for this
            Dim req As New UIRequest(Session, Description, New QueryString("title", "Special:Preferences/reset"), _
                New QueryString("wpEditToken", Session.EditToken))

            req.Start()
            If req.IsFailed Then OnFail(req.Result) : Return

            If Not req.Response.Contains("<div class=""successbox"">") Then OnFail(Msg("error-unknown")) : Return

            'Load preferences again to see what they were reset to
            Dim req2 As New ApiRequest(Session, Description, New QueryString( _
                "action", "query", _
                "meta", "userinfo", _
                "uiprop", "options"))

            req2.Start()

            OnSuccess()
        End Sub

    End Class

End Namespace
