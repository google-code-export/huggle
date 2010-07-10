Imports Huggle.Queries
Imports System
Imports System.Drawing
Imports System.IO
Imports System.Net
Imports System.Security
Imports System.Text
Imports System.Windows.Forms

Namespace Huggle.Actions

    Public Class CreateAccount : Inherits Query

        Private _NewUser As User

        Public Sub New(ByVal session As Session, ByVal newUser As User)
            MyBase.New(session, Msg("createaccount-desc", session.Wiki, newUser))
            Interactive = True
        End Sub

        Public ReadOnly Property NewUser() As User
            Get
                Return _NewUser
            End Get
        End Property

        Public Overrides Sub Start()
            OnProgress(Msg("createaccount-progress", NewUser.FullName))

            'Construct query string
            Dim query As New QueryString _
                ("title", "Special:UserLogin", "action", "submitlogin", "type", "signup")

            Dim data As New QueryString( _
                "wpName", NewUser.Name, _
                "wpPassword", NewUser.Password, _
                "wpRetype", NewUser.Password, _
                "wpRemember", 1, _
                "wpCreateaccount", "Create account")

            If Wiki.CreationCheck IsNot Nothing Then
                data.Merge("wpCaptchaId", Wiki.CreationCheck.ConfirmId, _
                    "wpCaptchaWord", Wiki.CreationCheck.ConfirmAnswer)
                Wiki.CreationCheck = Nothing
            End If

            'No API for account creation, must use Special:CreateAccount
            Dim createreq As New UIRequest(Session, Description, query, data)
            createreq.Start()

            If createreq.Result.IsError Then OnFail(createreq.Result.Message) : Return

            'Parse error messages
            If Not createreq.Response.Contains("<li id=""pt-userpage"">") Then
                If Not createreq.Response.Contains("<div class=""errorbox"">") Then
                    OnFail(Msg("error-unknown"))
                Else
                    OnFail(PlainTextFromHtml(createreq.Response.FromFirst("<div class=""errorbox"">") _
                        .FromFirst("</h2>").ToFirst("</div>").Trim))
                End If

                Return
            End If

            'Clear cookies of the creating account, they will have been overwritten by the new account's cookies
            If Session.User IsNot NewUser Then
                Session.Cookies = New CookieContainer
                User.IsLoaded = False
            End If

            NewUser.IsUsed = True
            Config.Local.Save()
            OnSuccess()
        End Sub

    End Class

End Namespace
