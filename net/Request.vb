Imports Huggle.Actions
Imports System
Imports System.Collections
Imports System.Collections.Generic
Imports System.Globalization
Imports System.IO
Imports System.Net
Imports System.Reflection
Imports System.Text.RegularExpressions
Imports System.Threading
Imports System.Web.HttpUtility

Namespace Huggle

    'Represents an HTTP request

    Friend MustInherit Class Request : Inherits Process

        Private _Response As MemoryStream
        Private _ResponseTime As TimeSpan

        Private Const MaxAttempts As Integer = 3
        Private Shared ReadOnly RetryInterval As New TimeSpan(0, 0, 3)
        Private Shared ReadOnly DefaultTimeout As New TimeSpan(0, 0, 30)

        Public Property Boundary() As String

        Public Property Cookies() As CookieContainer

        Public Property Data() As Byte()

        Public Property IsMultipart() As Boolean

        Protected ReadOnly Property Response() As MemoryStream
            Get
                Return _Response
            End Get
        End Property

        Public ReadOnly Property ResponseTime() As TimeSpan
            Get
                Return _ResponseTime
            End Get
        End Property

        Public Property Timeout As TimeSpan = DefaultTimeout

        Public Property Url() As Uri

        Public Overrides Sub Start()
            OnStarted()
            Result = Result.Success
            Dim retries As Integer = MaxAttempts

            Do
                If retries < MaxAttempts Then Thread.Sleep(RetryInterval)
                retries -= 1

                Try
                    Dim request As HttpWebRequest

                    Try
                        request = CType(WebRequest.Create(Url), HttpWebRequest)

                    Catch ex As SystemException
                        OnFail(New Result(Msg("error-badurl"), "badurl"))
                        Return
                    End Try

                    request.AllowAutoRedirect = True
                    request.AutomaticDecompression = DecompressionMethods.GZip
                    request.CookieContainer = Cookies
                    request.KeepAlive = False
                    request.Proxy = Config.Local.Proxy
                    request.ReadWriteTimeout = CInt(Timeout.TotalMilliseconds)
                    request.Timeout = CInt(Timeout.TotalMilliseconds)
                    request.UserAgent = InternalConfig.UserAgent

                    Dim startTime As Date = Date.Now

                    If Data Is Nothing Then
                        request.Method = "GET"
                    Else
                        request.Method = "POST"
                        If IsMultipart Then request.ContentType = "multipart/form-data boundary=" & Boundary _
                            Else request.ContentType = "application/x-www-form-urlencoded"

                        request.ContentLength = Data.Length
                        Dim requestStream As Stream = request.GetRequestStream
                        requestStream.Write(Data, 0, Data.Length)
                        requestStream.Close()
                    End If

                    Dim webResponse As WebResponse = request.GetResponse

                    Using responseStream As Stream = webResponse.GetResponseStream
                        Dim buffer(255) As Byte, size As Integer
                        _Response = New MemoryStream

                        Do
                            size = responseStream.Read(buffer, 0, buffer.Length)
                            Response.Write(buffer, 0, size)
                        Loop While size > 0
                    End Using

                    _ResponseTime = (Date.Now - startTime)
                    If Not App.IsMono AndAlso Cookies IsNot Nothing Then FixCookieContainer(Cookies, Url)

                    Return

                Catch ex As IOException
                    Result = Result.FromException(ex)

                Catch ex As WebException
                    Select Case ex.Status
                        Case WebExceptionStatus.NameResolutionFailure, WebExceptionStatus.ConnectFailure
                            'Either site is down or user has no internet connection
                            Result = New Result({Msg("error-connection"), Msg("error-connectionhelp")}, "connection")

                        Case WebExceptionStatus.ProtocolError
                            'HTTP error code
                            Dim statusCode As HttpStatusCode = CType(ex.Response, HttpWebResponse).StatusCode

                            Select Case statusCode
                                Case HttpStatusCode.BadGateway
                                    'Wikimedia servers intermittently return 502 Bad Gateway for no obvious reason
                                    Log.Debug("Recieved 502 Bad Gateway, retrying...")

                                Case Else
                                    'Get error code description from enum, break it into words
                                    Result = New Result(Msg("error-http", CInt(statusCode),
                                        Regex.Replace(statusCode.ToString, "([A-Z])", " $1").Trim),
                                        "httperror-" & statusCode)
                            End Select

                        Case WebExceptionStatus.Timeout
                            'Add connection help in case that is the cause of timeout
                            Result = New Result({Msg("error-timeout"), Msg("error-connectionhelp")}, "timeout")

                        Case Else
                            'Some other web exception
                            Result = New Result(Msg("error-webexception", ex.Status.ToString), "webexception")
                    End Select
                End Try

                'Maximum retries exceeded
                If retries = 0 Then Result = New Result({Msg("error-noresponse"), Msg("error-connectionhelp")})

            Loop Until Result.IsError
        End Sub

        Private Shared Sub FixCookieContainer(ByVal cookies As CookieContainer, ByVal url As Uri)
            'CookieContainer has a bug in its domain handling (not fixed until .NET 4),
            'here we use reflection to poke the necessary values. Using something else
            'to handle cookies isn't an option as HttpWebRequest requires a CookieContainer
            Dim old As New CookieCollection
            old.Add(cookies.GetCookies(url))

            Dim domainTable As Hashtable = CType(GetType(CookieContainer).InvokeMember("m_domainTable",
                BindingFlags.NonPublic Or BindingFlags.GetField Or BindingFlags.Instance,
                Nothing, cookies, Nothing, CultureInfo.InvariantCulture), Hashtable)

            For Each key As String In New ArrayList(domainTable.Keys)
                If key.StartsWithI(".") Then domainTable(key.Substring(1)) = domainTable(key)
            Next key

            cookies.Add(url, old)
        End Sub

    End Class

End Namespace