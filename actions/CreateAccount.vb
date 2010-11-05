Imports System
Imports System.Drawing
Imports System.IO
Imports System.Net
Imports System.Security
Imports System.Text
Imports System.Text.RegularExpressions
Imports System.Windows.Forms

Namespace Huggle.Actions

    Friend Class CreateAccount : Inherits Query

        Private Confirmation As Confirmation
        Private NewUser As User

        Friend Sub New(ByVal session As Session, ByVal newUser As User, ByVal confirmation As Confirmation)
            MyBase.New(session, Msg("createaccount-desc", session.Wiki, newUser))
            Interactive = True

            Me.Confirmation = confirmation
            Me.NewUser = newUser
        End Sub

        Friend Overrides Sub Start()
            OnProgress(Msg("createaccount-progress", NewUser.FullName))

            'Obtain a login token
            Dim tokenReq As New UIRequest(Session, Description, New QueryString(
                "title", "Special:UserLogin", "type", "signup"), Nothing)

            tokenReq.Start()
            If tokenReq.IsFailed Then OnFail(tokenReq.Result) : Return

            Dim tokenMatch As Match = Regex.Match(tokenReq.Response, _
                "<[^>]+name=""wpCreateaccountToken"" value=""([^""]+)""[^>]+>", RegexOptions.Compiled)
            Dim token As String = Nothing

            If tokenMatch.Success Then token = tokenMatch.Groups(1).Value

            'Construct query string
            Dim query As New QueryString(
                "title", "Special:UserLogin", "action", "submitlogin", "type", "signup")

            Dim data As New QueryString(
                "wpName", NewUser.Name,
                "wpPassword", Unscramble(NewUser.FullName, NewUser.Password, Hash(NewUser)),
                "wpRetype", Unscramble(NewUser.FullName, NewUser.Password, Hash(NewUser)),
                "wpRemember", 1,
                "wpCreateaccount", "Create account")

            If Confirmation IsNot Nothing _
                Then data.Merge("wpCaptchaId", Confirmation.Id, "wpCaptchaWord", Confirmation.Answer)

            If token IsNot Nothing Then data.Merge("wpCreateaccountToken", token)

            'No API for account creation, must use Special:CreateAccount
            Dim createreq As New UIRequest(Session, Description, query, data)
            createreq.Start()

            If createreq.Result.IsError Then OnFail(createreq.Result) : Return

            'Parse error messages
            If Not createreq.Response.Contains("<li id=""pt-userpage"">") Then
                If Not createreq.Response.Contains("<div class=""errorbox"">") _
                    Then OnFail(Msg("error-unknown")) Else OnFail(PlainTextFromHtml( _
                    createreq.Response.FromFirst("<div class=""errorbox"">").ToFirst("</div>").Trim))

                Return
            End If

            'Clear cookies of the creating account, they will have been overwritten by the new account's cookies
            If Session.User IsNot NewUser Then
                Session.Cookies = New CookieContainer
                User.IsLoaded = False
            End If

            NewUser.IsUsed = True
            Config.Local.SaveLocal()
            OnSuccess()
        End Sub

    End Class

End Namespace
