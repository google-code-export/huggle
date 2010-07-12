Imports System
Imports System.Drawing
Imports System.IO

Namespace Huggle.Actions

    Public Class PreCreateAccount : Inherits Query

        Private _Confirmation As Confirmation

        Public Sub New(ByVal wiki As Wiki)
            MyBase.New(wiki.Users.Anonymous.Session, Msg("createaccount-desc"))
        End Sub

        Public ReadOnly Property Confirmation As Confirmation
            Get
                Return _Confirmation
            End Get
        End Property

        Public Overrides Sub Start()
            OnProgress(Msg("createaccount-confirm", Wiki))
            Dim req As New UIRequest(Session, Description, New QueryString("title", "Special:UserLogin/signup"), Nothing)
            req.Start()

            If req.Result.IsError Then OnFail(req.Result.Message) : Return
            
            If req.Response.Contains("?title=Special:Captcha/image") Then
                'Image captcha, find the image and fetch it
                Wiki.AccountConfirmation = True

                Dim confirmId As String = req.Response.FromFirst("?title=Special:Captcha/image").FromFirst("=").ToFirst("""")

                Dim imageQuery As New QueryString( _
                    "title", "Special:Captcha/image", _
                    "wpCaptchaId", confirmId)

                Dim imageReq As New FileRequest(Session, New Uri(Wiki.Url.ToString & "index.php?" & imageQuery.ToUrlString))

                imageReq.Start()
                If imageReq.Result.IsError Then OnFail(imageReq.Result) : Return

                Dim confirmImage As Image

                Try
                    confirmImage = New Bitmap(imageReq.File)
                Catch ex As SystemException
                    OnFail(Msg("createaccount-brokenconfirm")) : Return
                Finally
                    imageReq.File.Close()
                End Try

                _Confirmation = New Confirmation(confirmId, confirmImage)

                OnSuccess()

            ElseIf req.Response.Contains("<input name=""wpCaptchaWord""") Then
                'Math captcha
                Wiki.AccountConfirmation = True

                'Fortunately for our user, computers are good at math...
                Dim expression As String = req.Response.FromFirst("<label for=""wpCaptchaWord"">").ToFirst("</label>")
                Dim evaluator As New Scripting.Evaluator(Session, "Math captcha", expression)
                evaluator.Start()
                If evaluator.Result.IsError Then OnFail("createaccount-brokenconfirm")
                Confirmation.Answer = CStr(CInt(evaluator.Value.Number))
                OnSuccess()

            ElseIf req.Response.Contains("<form name=""userlogin2""") Then
                Wiki.AccountConfirmation = False
                OnSuccess()

            Else
                Wiki.IsPublicEditable = False
                OnFail(Msg("createaccount-disabled"))
            End If

            Config.Global.SaveLocal()
        End Sub

    End Class

End Namespace
