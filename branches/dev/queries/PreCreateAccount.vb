Imports System
Imports System.Drawing
Imports System.IO

Namespace Huggle.Actions

    Public Class PreCreateAccount : Inherits Query : Implements IDisposable

        Private _ConfirmAnswer As String
        Private _ConfirmId As String
        Private _ConfirmImage As Image

        Public Sub New(ByVal wiki As Wiki)
            MyBase.New(wiki.Users.Anonymous.Session, Msg("createaccount-desc"))
        End Sub

        Public Property ConfirmAnswer() As String
            Get
                Return _ConfirmAnswer
            End Get
            Set(ByVal value As String)
                _ConfirmAnswer = value
            End Set
        End Property

        Public ReadOnly Property ConfirmId() As String
            Get
                Return _ConfirmId
            End Get
        End Property

        Public ReadOnly Property ConfirmImage() As Image
            Get
                Return _ConfirmImage
            End Get
        End Property

        Public Overrides Sub Start()
            OnProgress(Msg("createaccount-confirm", Wiki))
            Dim req As New UIRequest(Session, Description, New QueryString("title", "Special:UserLogin/signup"), Nothing)
            req.Start()

            If req.Result.IsError Then
                OnFail(req.Result.Message)
                Return
            End If

            If req.Response.Contains("?title=Special:Captcha/image") Then
                'Image captcha, find the image and fetch it
                Wiki.AccountConfirmation = True
                _ConfirmId = req.Response.FromFirst("?title=Special:Captcha/image").FromFirst("=").ToFirst("""")

                Dim query As New QueryString( _
                    "title", "Special:Captcha/image", _
                    "wpCaptchaId", ConfirmId)

                Dim imageReq As New FileRequest(Session, New Uri(Wiki.Url.ToString & "index.php?" & query.ToString))

                imageReq.Start()
                If imageReq.Result.IsError Then OnFail(Msg("createaccount-brokenconfirm")) : Return

                Try
                    _ConfirmImage = New Bitmap(imageReq.File)
                Catch ex As SystemException
                    OnFail(Msg("createaccount-brokenconfirm")) : Return
                End Try

                imageReq.File.Close()
                OnSuccess()

            ElseIf req.Response.Contains("<input name=""wpCaptchaWord""") Then
                'Math captcha... just evaluate it
                Wiki.AccountConfirmation = True

                Dim expression As String = req.Response.FromFirst("<label for=""wpCaptchaWord"">").ToFirst("</label>")
                Dim evaluator As New Scripting.Evaluator(Session, "Math captcha", expression)
                evaluator.Start()
                'If evaluator.Result.IsError OrElse evaluator.Result.Value Is Nothing Then OnFail("createaccount-brokenconfirm")
                'ConfirmAnswer = CStr(CInt(evaluator.Result.Value))
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

        Protected Overridable Sub Dispose(ByVal disposing As Boolean)
            Static disposed As Boolean

            If Not disposed Then
                If disposing Then
                    _ConfirmImage.Dispose()
                End If
            End If
            disposed = True
        End Sub

        Public Sub Dispose() Implements IDisposable.Dispose
            Dispose(True)
            GC.SuppressFinalize(Me)
        End Sub

    End Class

End Namespace
