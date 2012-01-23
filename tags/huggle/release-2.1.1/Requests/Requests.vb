Imports System.IO
Imports System.Net
Imports System.Text.Encoding
Imports System.Text.RegularExpressions
Imports System.Threading
Imports System.Web.HttpUtility

Namespace Requests

    MustInherit Class Request

        'Base class of all Web requests

        Private _Query As String, _StartTime As Date, _Mode As Modes, _State As States, _Done As RequestCallback
        Private Shared _Cookies As New CookieContainer
        Protected _Result As New RequestResult

        Public Delegate Sub RequestCallback(ByVal Result As RequestResult)

        Public Sub New()
            _StartTime = Date.Now
            PendingRequests.Add(Me)
            AllRequests.Add(Me)
            Callback(AddressOf UpdateForms)
        End Sub

        Public Shared Sub ClearCookies()
            _Cookies = New CookieContainer
        End Sub

        Public ReadOnly Property StartTime() As Date
            Get
                Return _StartTime
            End Get
        End Property

        Public Property Mode() As Modes
            Get
                Return _Mode
            End Get
            Private Set(ByVal value As Modes)
                _Mode = value
                Callback(AddressOf UpdateForms)
            End Set
        End Property

        Public Property Query() As String
            Get
                Return _Query
            End Get
            Private Set(ByVal value As String)
                _Query = UrlDecode(value)
                Callback(AddressOf UpdateForms)
            End Set
        End Property

        Public Property State() As States
            Get
                Return _State
            End Get
            Private Set(ByVal value As States)
                _State = value
                Callback(AddressOf UpdateForms)
            End Set
        End Property

        Protected Enum LoginResult As Integer
            : None : WrongPassword : NoUser : InvalidUsername : CaptchaNeeded : Failed : Cancelled : Success
        End Enum

        Public Enum Modes As Integer
            : None : [Get] : Post
        End Enum

        Public Enum States As Integer
            : InProgress : Complete : Failed : Cancelled : SpamFilter
        End Enum

        'Complete the request, optionally returning text
        Protected Sub Complete(Optional ByVal Message As String = Nothing, Optional ByVal Text As String = Nothing)
            State = States.Complete
            If Message IsNot Nothing Then Log(Message)
            If Text IsNot Nothing Then _Result = New RequestResult(Text, Nothing)
            Callback(AddressOf EndRequest)
        End Sub

        'Cancel the request
        Public Sub Cancel()
            State = States.Cancelled
            If PendingRequests.Contains(Me) Then PendingRequests.Remove(Me)
            Callback(AddressOf EndRequest)
        End Sub

        'Fail the request, optionally with specific error message
        Protected Sub Fail(Optional ByVal Details As String = Nothing, Optional ByVal Reason As String = Nothing)
            State = States.Failed

            If Reason Is Nothing AndAlso Details Is Nothing Then
                Reason = Msg("error-unknown")
                Details = Msg("error-fail", Truncate(Query, 50))
            End If

            If Reason Is Nothing Then
                Log(Details)
                _Result = New RequestResult(Nothing, Details)
            Else
                Log(Details & ": " & Reason)
                _Result = New RequestResult(Nothing, Details & ": " & Reason)
            End If

            Callback(AddressOf EndRequest)
        End Sub

        'Sort out various things when the request has finished
        Private Sub EndRequest()
            If PendingRequests.Contains(Me) Then PendingRequests.Remove(Me)
            If MainForm IsNot Nothing Then MainForm.Delog(Me)
        End Sub

        'Update details on any open RequestsForm
        Private Sub UpdateForms()
            For Each Item As Form In Application.OpenForms
                If TypeOf Item Is RequestsForm Then CType(Item, RequestsForm).UpdateList(Me)
            Next Item
        End Sub

        'Carry out the request on the same thread and return its result
        Public Function Invoke() As RequestResult
            Process()
            Callback(AddressOf ThreadDone)
            Return _Result
        End Function

        'Start the request in a separate thread, optionally calling a specified subroutine when done
        Public Sub Start(Optional ByVal Done As RequestCallback = Nothing)
            _Done = Done
            Dim RequestThread As New Thread(AddressOf ProcessThread)
            RequestThread.IsBackground = True
            RequestThread.Name = "Process"
            RequestThread.Start()
        End Sub

        'Helper function for above
        Protected Sub ProcessThread()
            Process()
            Callback(AddressOf ThreadDone)
        End Sub

        'Helper function for above
        Private Sub ThreadDone()
            Done()
            If _Done IsNot Nothing Then _Done.Invoke(_Result)
        End Sub

        'The main body of the request
        Protected MustOverride Sub Process()

        'Any processing that must always happen on the main thread (usually because it needs UI access)
        Protected Overridable Sub Done()
        End Sub

        'Add a normal entry to the log
        Protected Sub Log(ByVal Message As String)
            Misc.Log(Message, Me)
        End Sub

        'Add or replace an in-progress entry for this request in the log
        Protected Sub LogProgress(ByVal Message As String)
            Callback(AddressOf LogProgressCallback, CObj(Message))
        End Sub

        'Helper function for above
        Private Sub LogProgressCallback(ByVal MessageObject As Object)
            If MainForm IsNot Nothing Then
                MainForm.Delog(Me)
                MainForm.Log(CStr(MessageObject), Me, True)
                Application.DoEvents()
            End If
        End Sub

        'Undo an edit that had already saved when the request was cancelled
        Protected Sub UndoEdit(ByVal Page As Page)
            If Page.LastEdit IsNot Nothing AndAlso Page.LastEdit.User.IsMe _
                Then DoRevert(Page.LastEdit, Config.UndoSummary, Undoing:=True)
        End Sub

        Protected Sub UndoEdit(ByVal Page As String)
            UndoEdit(GetPage(Page))
        End Sub

        'Make a Web request, setting appropriate headers
        Protected Function DoWebRequest(ByVal Url As String, Optional ByVal PostString As String = Nothing) As String
            Try
                ServicePointManager.Expect100Continue = False
                Dim Request As HttpWebRequest = CType(HttpWebRequest.Create(Url), HttpWebRequest)

                If Not Mono() Then SetAcceptCompression(Request)
                Request.CookieContainer = _Cookies
                Request.Proxy = Proxy
                Request.ReadWriteTimeout = Config.RequestTimeout
                Request.Timeout = Config.RequestTimeout
                Request.UserAgent = Config.UserAgent

                If PostString IsNot Nothing Then
                    Dim PostData As Byte() = UTF8.GetBytes(PostString)

                    Request.ContentLength = PostData.Length
                    Request.ContentType = "application/x-www-form-urlencoded"
                    Request.Method = "POST"

                    Dim RequestStream As Stream = Request.GetRequestStream
                    RequestStream.Write(PostData, 0, PostData.Length)
                    RequestStream.Close()
                End If

                Dim ResponseStream As New StreamReader(Request.GetResponse.GetResponseStream, UTF8)
                Dim Result As String = ResponseStream.ReadToEnd
                ResponseStream.Close()

                Return Result
            Catch e As Exception
                Debug.WriteLine("")
                Return Nothing
            End Try
        End Function

        'Isolate from above; not implemented in Mono
        Private Sub SetAcceptCompression(ByRef Request As HttpWebRequest)
            Request.Headers.Add(HttpRequestHeader.AcceptEncoding, "gzip")
            Request.AutomaticDecompression = DecompressionMethods.GZip
        End Sub

        Protected Function DoLogin() As LoginResult
            Dim Result As String = DoUrlRequest(SitePath() & "index.php?title=Special:UserLogin")

            If Result Is Nothing OrElse Not IsWikiPage(Result) Then Return LoginResult.Failed

            If Result.Contains("<div class='captcha'>") Then
                Login.CaptchaId = Result.Substring(Result.IndexOf("id=""wpCaptchaId"" value=""") + 24)
                Login.CaptchaId = Login.CaptchaId.Substring(0, Login.CaptchaId.IndexOf(""""))

                Return LoginResult.CaptchaNeeded
            End If

            If Result.Contains("<input type=""hidden"" name=""wpLoginToken"" ") Then
                Login.Token = Result.Substring(Result.IndexOf("name=""wpLoginToken"" value=""") + 27)
                Login.Token = Login.Token.Substring(0, Login.Token.IndexOf(""""))
            End If

            Dim PostString As String = "wpName=" & UrlEncode(Config.Username) & "&wpRemember=1" & _
                "&wpPassword=" & UrlEncode(Config.Password) & "&wpCaptchaId=" & Login.CaptchaId & _
                "&wpCaptchaWord=" & Login.CaptchaWord & "&wpLoginToken=" & UrlEncode(Login.Token)

            Result = DoUrlRequest(SitePath() & "index.php?title=Special:UserLogin&action=submitlogin", PostString)

            If Result Is Nothing OrElse Not IsWikiPage(Result) Then Return LoginResult.Failed

            If Result.Contains("<span id=""mw-noname"">") Then Return LoginResult.InvalidUsername
            If Result.Contains("<span id=""mw-nosuchuser"">") Then Return LoginResult.NoUser
            If Result.Contains("<span id=""mw-wrongpasswordempty"">") Then Return LoginResult.WrongPassword
            If Result.Contains("<span id=""mw-wrongpassword"">") Then Return LoginResult.WrongPassword
            If Result.Contains("<div id=""userloginForm"">") Then Return LoginResult.Failed

            Return LoginResult.Success
        End Function

        'Make an arbitrary Web request
        Protected Function DoUrlRequest(ByVal Url As String, Optional ByVal PostString As String = Nothing) As String

            If Url.Contains("?") Then Query = Url.Substring(Url.IndexOf("?") + 1) Else Query = Url
            If PostString Is Nothing Then Mode = Modes.Get Else Mode = Modes.Post

            Dim Retries As Integer = Config.RequestAttempts, Result As String = Nothing

            Dim Break As Integer = 0

            Do
                Break = Break + 1
                If Retries < Config.RequestAttempts Then Thread.Sleep(Config.RequestRetryInterval)
                Retries -= 1

                Try
                    Result = DoWebRequest(Url, PostString)

                Catch ex As WebException
                    If Retries = 0 Then
                        If ex.Status = WebExceptionStatus.Timeout _
                            Then Throw New WebException(Msg("error-timeout")) Else Throw
                    End If
                End Try

                If State = States.Cancelled Then Thread.CurrentThread.Abort()
            Loop Until (Retries = 0 OrElse Result IsNot Nothing) And Break < Misc.GlExcess

            If Retries = 0 Then Return Nothing Else Return Result
        End Function

        'Retrieve/submit data through the MediaWiki API
        Protected Function DoApiRequest(ByVal QueryString As String, _
            Optional ByVal PostString As String = Nothing, Optional ByVal Project As String = Nothing) As ApiResult

            Dim Break As Integer = 0
            Dim ProjectUrl As String
            If Project Is Nothing Then Project = Config.Project
            If Config.Projects.ContainsKey(Project) Then ProjectUrl = Config.Projects(Project) _
                Else ProjectUrl = "http://meta.wikimedia.org/"

            Query = QueryString
            If PostString Is Nothing Then Mode = Modes.Get Else Mode = Modes.Post

            Dim Retries As Integer = Config.RequestAttempts, Result As String = ""

            Do
                Break = Break + 1
                If Retries < Config.RequestAttempts Then Thread.Sleep(Config.RequestRetryInterval)
                Retries -= 1

                Try
                    Result = DoWebRequest(ProjectUrl & Config.WikiPath & "api.php?format=xml&" & QueryString, PostString)

                Catch ex As WebException
                    If ex.Status = WebExceptionStatus.Timeout Then
                        Return New ApiResult(Nothing, "error-timeout", Msg("error-timeout"))
                    Else
                        Return New ApiResult(Nothing, "error-unknown", Msg("error-unknown"))
                    End If
                End Try
            Loop Until (Result <> "" OrElse Retries = 0) And Break < Misc.GlExcess

            If Result.StartsWith("MediaWiki API is not enabled for this site") _
                Then Return New ApiResult(Nothing, "error-apidisabled", Msg("error-apidisabled"))

            If Result = "" Then Return New ApiResult(Nothing, "null", Msg("error-noresponse"))

            If FindString(Result, "<error ") IsNot Nothing Then
                Return New ApiResult(Result, HtmlDecode(FindString(Result, "<error ", "code=""", """")), _
                    HtmlDecode(FindString(Result, "<error ", "info=""", """")))
            ElseIf FindString(Result, "<warnings>", "</warnings>") IsNot Nothing Then
                Return New ApiResult(Result, "warning", _
                    HtmlDecode(StripHTML(FindString(Result, "<warnings>", "</warnings>"))))
            Else
                Return New ApiResult(Result, Nothing, Nothing)
            End If
        End Function

        'Get the text of a page
        Protected Function GetText(ByVal PageName As String, Optional ByVal Revision As String = Nothing, _
            Optional ByVal Section As String = Nothing) As ApiResult

            Dim QueryString As String = "action=query&prop=revisions&rvlimit=1&rvprop=content&titles=" & _
                UrlEncode(Page.SanitizeTitle(PageName))

            If Section IsNot Nothing Then QueryString &= "&rvsection=" & UrlEncode(Section)
            If Revision IsNot Nothing Then QueryString &= "&rvstartid=" & UrlEncode(Revision)

            Return DoApiRequest(QueryString)
        End Function

        Protected Function GetText(ByVal Page As Page, Optional ByVal Revision As String = Nothing, _
            Optional ByVal Section As String = Nothing) As ApiResult

            Return GetText(Page.Name, Revision, Section)
        End Function

        'Make an edit through the MediaWiki API
        Protected Function PostEdit(ByVal Page As Page, ByVal Text As String, ByVal Summary As String, _
            Optional ByVal Section As String = Nothing, Optional ByVal Minor As Boolean = False, _
            Optional ByVal Watch As Boolean = False, Optional ByVal SuppressAutoSummary As Boolean = False, _
            Optional ByVal AllowCreate As Boolean = True) As ApiResult
            Dim BreakA As Integer = 0, BreakB As Integer = 0

            Dim Result As ApiResult, Token As String = Nothing, BadToken As Boolean

            Do
                BreakB = BreakB + 1
                'Get edit token
                Do
                    BreakA = BreakA + 1
                    If Section IsNot Nothing OrElse EditToken Is Nothing Then
                        Result = DoApiRequest("action=query&prop=info&intoken=edit&titles=" & UrlEncode(Page.Name))
                        If Result.Error Then Return Result
                        Token = GetParameter(Result.Text, "edittoken")
                        If Section Is Nothing Then EditToken = Token
                    Else
                        Token = EditToken
                    End If

                    If EditToken.Length < 16 Then
                        'Logged out somehow, logging back in
                        Token = Nothing
                        EditToken = Nothing
                        LogProgress(Msg("error-loggedout"))

                        If DoLogin() <> LoginResult.Success _
                            Then Return New ApiResult(Nothing, "", Msg("error-reloginfail"))
                    End If
                Loop Until Token IsNot Nothing And BreakA < Misc.GlExcess

                'Edit page
                Dim QueryString As String = "title=" & UrlEncode(Page.Name) & "&text=" & UrlEncode(Text) _
                    & "&summary=" & UrlEncode(Summary)

                If Config.Summary IsNot Nothing AndAlso Not SuppressAutoSummary _
                    Then QueryString &= UrlEncode(" " & Config.Summary)

                QueryString &= "&token=" & UrlEncode(Token)

                If Section IsNot Nothing Then QueryString &= "&section=" & UrlEncode(Section)
                If Minor Then QueryString &= "&minor"
                If Watch Then QueryString &= "&watch"

                Result = DoApiRequest("action=edit", QueryString)

                If Result.ErrorCode = "badtoken" Then
                    BadToken = True
                    EditToken = Nothing
                Else
                    BadToken = False
                End If
            Loop Until Not BadToken And BreakB < Misc.GlExcess

            If Not Result.Error AndAlso Not Watchlist.Contains(Page.SubjectPage) Then Watchlist.Add(Page.SubjectPage)

            Return Result
        End Function

        Protected Function PostEdit(ByVal PageName As String, ByVal Text As String, ByVal Summary As String, _
            Optional ByVal Section As String = Nothing, Optional ByVal Minor As Boolean = False, _
            Optional ByVal Watch As Boolean = False, Optional ByVal SuppressAutoSummary As Boolean = False) _
            As ApiResult

            Return PostEdit(GetPage(PageName), Text, Summary, Section, Minor, Watch, SuppressAutoSummary)
        End Function

        Protected Function GetTextFromRev(ByVal Text As String) As String
            Return HtmlDecode(FindString(Text, "<revisions>", "<rev", ">", "</rev"))
        End Function

    End Class

    Class ApiResult

        'Represents the result of a MediaWiki API request

        Private _Text, _ErrorCode, _ErrorMessage As String

        Public Sub New(ByVal Text As String, ByVal ErrorCode As String, ByVal ErrorMessage As String)
            _Text = Text
            _ErrorCode = ErrorCode
            _ErrorMessage = ErrorMessage
        End Sub

        Public ReadOnly Property [Error]() As Boolean
            Get
                Return (_ErrorCode IsNot Nothing)
            End Get
        End Property

        Public ReadOnly Property Text() As String
            Get
                Return _Text
            End Get
        End Property

        Public ReadOnly Property ErrorCode() As String
            Get
                Return _ErrorCode
            End Get
        End Property

        Public ReadOnly Property ErrorMessage() As String
            Get
                'Return a localized error message where possible
                If _ErrorCode Is Nothing Then Return _ErrorMessage
                If MsgExists("api-" & _ErrorCode) Then Return Msg("api-" & _ErrorCode)
                Return _ErrorMessage
            End Get
        End Property

    End Class

    Class RequestResult

        'Represents the result of a Request

        Private _Text, _ErrorMessage As String

        Public Sub New(Optional ByVal Text As String = Nothing, Optional ByVal ErrorMessage As String = Nothing)
            _Text = Text
            _ErrorMessage = ErrorMessage
        End Sub

        Public ReadOnly Property [Error]() As Boolean
            Get
                Return (_ErrorMessage IsNot Nothing)
            End Get
        End Property

        Public Property Text() As String
            Get
                Return _Text
            End Get
            Set(ByVal value As String)
                _Text = value
            End Set
        End Property

        Public ReadOnly Property ErrorMessage() As String
            Get
                Return _ErrorMessage
            End Get
        End Property

    End Class

End Namespace