Imports Huggle.Net
Imports System
Imports System.Drawing
Imports System.IO
Imports System.Text
Imports System.Text.RegularExpressions

Namespace Huggle.Queries

    Friend Class PreCreateAccount : Inherits Query

        Private _Confirmation As Confirmation

        Public Sub New(ByVal wiki As Wiki)
            MyBase.New(App.Sessions(wiki.Users.Anonymous), Msg("createaccount-desc"))
        End Sub

        Public ReadOnly Property Confirmation As Confirmation
            Get
                Return _Confirmation
            End Get
        End Property

        Public Overrides Sub Start()
            OnProgress(Msg("createaccount-confirm", Wiki))

            'Load title blacklist
            If Not Wiki.IsLoaded AndAlso Wiki.TitleList Is Nothing Then
                Wiki.TitleList = New TitleList(Wiki.Pages("MediaWiki:Titleblacklist"))

                Dim titleReq As New PageDetailQuery(Session, Wiki.TitleList.Location)
                titleReq.Start()
            End If

            Dim req As New UIRequest(Session, Description, New QueryString("title", "Special:UserLogin/signup"), Nothing)
            req.Start()

            If req.Result.IsError Then OnFail(req.Result.Message) : Return

            Static imageConfPattern As New Regex("\?title=Special:Captcha/image&amp;wpCaptchaId=(\d+)", RegexOptions.Compiled)
            Dim imageConfMatch As Match = imageConfPattern.Match(req.Response)

            Static mathConfPattern As New Regex("<label +for=""wpCaptchaWord"">([^<]+)</label>", RegexOptions.Compiled)
            Dim mathConfMatch As Match = mathConfPattern.Match(req.Response)

            Static creationFormPattern As New Regex("<form +name=""userlogin2""", RegexOptions.Compiled)
            Dim creationFormMatch As Match = creationFormPattern.Match(req.Response)

            If imageConfMatch.Success Then
                'Image captcha, find the image and fetch it
                Wiki.AccountConfirmation = True
                Dim confirmId As String = imageConfMatch.Groups(1).Value

                Dim imageQuery As New QueryString(
                    "title", "Special:Captcha/image",
                    "wpCaptchaId", confirmId)

                Dim imageReq As New FileRequest(New Uri(Wiki.Url, "index.php?" & imageQuery.ToUrlString))
                imageReq.Start()

                If imageReq.Result.IsError Then
                    If imageReq.Response IsNot Nothing Then
                        'Try to read response as text of error message
                        Dim response As String = Encoding.UTF8.GetString(imageReq.Response.ToArray)
                        OnFail(ExtractHttpErrorMessage(response)) : Return
                    End If

                    OnFail(imageReq.Result) : Return
                End If

                Dim confirmImage As Image

                Try
                    confirmImage = New Bitmap(imageReq.Response)
                Catch ex As SystemException
                    OnFail(Msg("createaccount-brokenconfirm")) : Return
                Finally
                    imageReq.Response.Close()
                End Try

                _Confirmation = New Confirmation(confirmId, confirmImage)

                OnSuccess()

            ElseIf mathConfMatch.Success Then
                'Math captcha
                Wiki.AccountConfirmation = True

                'Fortunately for our user, computers are good at math...
                Dim expression As String = mathConfMatch.Groups(1).Value
                Dim evaluator As New Scripting.Evaluator(Session, "Math captcha", expression)
                evaluator.Start()
                If evaluator.Result.IsError Then OnFail("createaccount-brokenconfirm")
                Confirmation.Answer = CInt(evaluator.Value.AsNumber).ToString
                OnSuccess()

            ElseIf creationFormMatch.Success Then
                Wiki.AccountConfirmation = False
                OnSuccess()

            Else
                Wiki.IsPublicEditable = False
                OnFail(Msg("createaccount-disabled"))
            End If

            Config.Global.SaveLocal()
        End Sub

        Private Function ExtractHttpErrorMessage(ByVal response As String) As String
            If response.Contains("<body>") Then response = response.FromFirst("<body>").ToFirst("</body>")
            Return StripHtml(response).Trim(" "c, CR, LF)
        End Function

    End Class

End Namespace
