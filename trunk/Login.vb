Imports System.Net
Imports System.Text.Encoding
Imports System.Text.RegularExpressions
Imports System.Threading
Imports System.Web.HttpUtility

Module Login

    Private CaptchaId, CaptchaWord, SessionCookie As String, Form As LoginForm
    Public Password As String

    Public Sub StartLogin(ByVal _Form As LoginForm)
        Form = _Form
        'Separate thread to make Web requests
        Dim InitialiseThread As New Thread(AddressOf Initialise)
        InitialiseThread.IsBackground = True
        InitialiseThread.Start()
    End Sub

    Public Function DoLogin() As String
        Dim Client As New WebClient, Result As String = "", Retries As Integer = 3

        If CaptchaId Is Nothing Then
            'Get login form, to check whether captcha is needed
            Do
                Client.Headers.Add(HttpRequestHeader.UserAgent, UserAgent)
                Client.Headers.Add(HttpRequestHeader.ContentType, "application/x-www-form-urlencoded")
                If Cookie IsNot Nothing Then Client.Headers.Add(HttpRequestHeader.Cookie, Cookie)

                Retries -= 1

                Try
                    Result = UTF8.GetString(Client.DownloadData(SitePath & "w/index.php?title=Special:Userlogin"))

                Catch ex As WebException
                    Thread.Sleep(1000)
                End Try

            Loop Until IsWikiPage(Result) OrElse Retries = 0

            If Retries = 0 Then Return "failed"

            If Client.ResponseHeaders(HttpResponseHeader.SetCookie) IsNot Nothing Then
                SessionCookie = Client.ResponseHeaders(HttpResponseHeader.SetCookie)
                SessionCookie = SessionCookie.Substring(0, SessionCookie.IndexOf(";") + 1)
            End If
            
            If Result.Contains("<div class='captcha'>") Then
                CaptchaId = Result.Substring(Result.IndexOf("id=""wpCaptchaId"" value=""") + 24)
                CaptchaId = CaptchaId.Substring(0, CaptchaId.IndexOf(""""))

                Return "captcha-needed"
            End If
        End If

        Do
            Client.Headers.Add(HttpRequestHeader.UserAgent, UserAgent)
            Client.Headers.Add(HttpRequestHeader.ContentType, "application/x-www-form-urlencoded")
            Client.Headers.Add(HttpRequestHeader.Cookie, SessionCookie)

            Retries -= 1

            Try
                'Pass username/password in post data
                Result = UTF8.GetString(Client.UploadData(SitePath & _
                    "w/index.php?title=Special:Userlogin&action=submitlogin&type=login", UTF8.GetBytes( _
                    "wpName=" & UrlEncode(Config.Username) & _
                    "&wpRemember=1" & _
                    "&wpPassword=" & UrlEncode(Password) & _
                    "&wpCaptchaId=" & CaptchaId & _
                    "&wpCaptchaWord=" & CaptchaWord)))

            Catch ex As WebException
                Thread.Sleep(1000)
            End Try

        Loop Until IsWikiPage(Result) OrElse Retries = 0

        If Retries = 0 Then Return "failed"
        If Result.Contains("<div id=""userloginForm"">") Then Return "wrong-password"

        Dim CookiePrefix As String, LoginCookie As String = Client.ResponseHeaders(HttpResponseHeader.SetCookie)

        If Config.Project = "localhost" Then CookiePrefix = "wikidb" _
            Else CookiePrefix = Config.Project.Substring(0, Config.Project.IndexOf(".")) & "wiki"

        Dim Userid As String = LoginCookie.Substring(LoginCookie.IndexOf(CookiePrefix & "UserID=") _
            + CookiePrefix.Length + 7)
        Userid = Userid.Substring(0, Userid.IndexOf(";"))

        'SUL-enabled accounts work differently
        If LoginCookie.Contains("centralauth_User=") Then

            Dim CaUserName As String = LoginCookie.Substring(LoginCookie.IndexOf("centralauth_User=") + 17)
            CaUserName = CaUserName.Substring(0, CaUserName.IndexOf(";"))

            Dim CaToken As String = LoginCookie.Substring(LoginCookie.IndexOf("centralauth_Token=") + 18)
            CaToken = CaToken.Substring(0, CaToken.IndexOf(";"))

            Cookie = CookiePrefix & "UserID=" & UrlEncode(Userid) & "; " & CookiePrefix & "UserName=" & _
                UrlEncode(Config.Username) & "; " & "centralauth_User=" & UrlEncode(Config.Username) & "; " & _
                "centralauth_Token=" & UrlEncode(CaToken) & ";"

            If SessionCookie IsNot Nothing Then Cookie &= " " & SessionCookie

            If LoginCookie.Contains("centralauth_Session=") Then
                Dim CaSession As String = LoginCookie.Substring(LoginCookie.IndexOf("centralauth_Session=") + 20)
                CaSession = CaSession.Substring(0, CaSession.IndexOf(";"))
                Cookie &= " centralauth_Session=" & UrlEncode(CaSession) & ";"
            End If

        Else
            Dim Token As String = LoginCookie.Substring(LoginCookie.IndexOf(CookiePrefix & "Token=") _
                + CookiePrefix.Length + 6)
            Token = Token.Substring(0, Token.IndexOf(";"))

            Cookie = CookiePrefix & "UserID=" & UrlEncode(Userid) & "; " & CookiePrefix & "UserName=" & _
            UrlEncode(Config.Username) & "; " & CookiePrefix & "Token=" & UrlEncode(Token) & "; " & SessionCookie
        End If

        MyUser = GetUser(Config.Username)

        Return "success"
    End Function

    Private Sub Initialise()
        Dim Client As New WebClient, Retries As Integer = 3, Result As String = ""

        'Log in... can't use the API here because it locks you out after a wrong password
        UpdateStatus("Logging in...")

        Select Case DoLogin()
            Case "failed"
                Abort("Unable to log in.")
                Exit Sub

            Case "captcha-needed"
                Callback(AddressOf CaptchaNeeded)
                Exit Sub

            Case "wrong-password"
                If CaptchaId Is Nothing Then Abort("Incorrect password.") _
                    Else Abort("Incorrect password or confirmation code.")
                CaptchaId = Nothing
                Exit Sub
        End Select

        'Connect to IRC, if required (on separate thread)
        If Config.IrcMode AndAlso (Config.IrcChannel IsNot Nothing) Then IrcConnect()

        'Get global configuration
        UpdateStatus("Checking project configuration...")

        Dim NewConfigRequest As New GetConfigRequest

        If Not NewConfigRequest.GetConfig(True) Then
            Abort("Failed to load project configuration page.")
            Exit Sub
        End If

        If Not Config.EnabledForAll Then
            Abort("Huggle is currently disabled on this project.")
            Exit Sub

        ElseIf Not VersionOK Then
            Abort("Version is too old. " & Config.MinVersion & " or later required.")
            Exit Sub
        End If

        'Get user configuration
        UpdateStatus("Checking user configuration...")

        Dim UserConfigResult As Boolean = NewConfigRequest.GetConfig(False)

        If Config.RequireConfig AndAlso Not UserConfigResult Then
            ConfigChanged = True
            Abort("Huggle is not enabled for your account, check configuration subpage.")
            Exit Sub
        End If

        For Each Item As String In Config.TemplateMessagesGlobal
            If Not Config.TemplateMessages.Contains(Item) Then Config.TemplateMessages.Add(Item)
        Next Item

        If Config.WarnSummary2 Is Nothing Then Config.WarnSummary2 = Config.WarnSummary
        If Config.WarnSummary3 Is Nothing Then Config.WarnSummary3 = Config.WarnSummary
        If Config.WarnSummary4 Is Nothing Then Config.WarnSummary4 = Config.WarnSummary

        'Get user information and groups
        UpdateStatus("Checking user rights...")

        Result = GetText(SitePath & "w/api.php?action=query&format=xml&meta=userinfo&uiprop=rights|editcount")

        If Result.Contains("<userinfo ") AndAlso Result.Contains("<rights>") Then
            Result = Result.Substring(Result.IndexOf("<userinfo "))
            Result = Result.Substring(0, Result.IndexOf("</userinfo>"))

            If Result.Contains("anon=""""") Then
                Abort("Failed to retrieve user rights.")
                Exit Sub
            End If

            Dim EditCount As String = Result.Substring(Result.IndexOf("editcount=""") + 11)
            EditCount = EditCount.Substring(0, EditCount.IndexOf(""""))

            If CInt(EditCount) < Config.RequireEdits Then
                Abort("Use of Huggle requires at least " & CStr(Config.RequireEdits) & " edits.")
                Exit Sub
            End If

            Result = Result.Substring(Result.IndexOf("<rights>") + 8)
            Result = Result.Substring(0, Result.IndexOf("</rights>"))

            Dim Rights As New List(Of String)(Result.Split(New String() {"<r>"}, StringSplitOptions.RemoveEmptyEntries))
            Dim Autoconfirmed, AdminAvailable As Boolean

            For Each Item As String In Rights
                Item = Item.Replace("</r>", "").Trim(" "c, CChar(vbLf), CChar(vbCr)).ToLower
                If Item = "rollback" Then RollbackAvailable = True
                If Item = "autoconfirmed" Then Autoconfirmed = True
                If Item = "block" Then AdminAvailable = True
            Next Item

            Administrator = AdminAvailable AndAlso Config.UseAdminFunctions

            If Config.RequireAdmin AndAlso Not AdminAvailable Then
                Abort("Use of Huggle requires an administrator account.")
                Exit Sub
            End If

            If Not Autoconfirmed AndAlso Config.Project = "en.wikipedia" Then
                Abort("Use of Huggle requires that your account is autoconfirmed.")
                Exit Sub
            End If

            If Config.RequireRollback AndAlso Not RollbackAvailable Then
                Abort("Use of Huggle requires rollback.")
                Exit Sub
            End If

        Else
            Abort("Failed to retrive user rights.")
            Exit Sub
        End If

        If Config.RequireTime > 0 Then
            'Get account creation date
            UpdateStatus("Checking account creation date...")

            Result = GetText(SitePath & _
                "w/api.php?action=query&format=xml&list=logevents&letype=newusers&letitle=User:" & _
                UrlEncode(Config.Username))

            'We know the user exists, so if we get an empty result the user must have been created in 2005 or
            'earlier, before the log existed
            If Not Result.Contains("<logevents />") AndAlso Result.Contains("<logevents>") Then
                Dim CreationDateString As String = Result.Substring(Result.IndexOf("<logevents>"))
                CreationDateString = Result.Substring(Result.IndexOf("timestamp=""") + 11)
                CreationDateString = Result.Substring(0, Result.IndexOf(""""))

                Dim CreationDate As Date
                Date.TryParse(CreationDateString, CreationDate)

                If CreationDate.AddDays(Config.RequireTime) < Date.UtcNow Then
                    Abort("Accounts must be at least " & CStr(Config.RequireTime) & " days old to use Huggle")
                    Exit Sub
                End If
            End If
        End If

        'Check user list. If approval required, deny access to users not on the list, otherwise add them
        If Config.UserListLocation IsNot Nothing Then
            UpdateStatus("Checking user list...")

            Dim UserList As String = GetText(GetPage(Config.UserListLocation))

            If UserList Is Nothing AndAlso Config.Approval Then
                Abort("Failed to load user list.")
                Exit Sub
            End If

            If UserList Is Nothing Then UserList = ""

            If Not UserList.Contains("[[Special:Contributions/" & MyUser.Name & "|" & MyUser.Name & "]]") Then

                If Config.Approval Then
                    Abort("User is not approved to use Huggle.")
                    Exit Sub
                End If

                Dim Matches As MatchCollection = _
                    New Regex("\* \[\[Special:Contributions/([^\|]+)\|[^\|]+\]\]").Matches(UserList)
                Dim ListedUsers As New List(Of String)

                For Each Item As Match In Matches
                    ListedUsers.Add(Item.Groups(1).Value)
                Next Item

                ListedUsers.Add(MyUser.Name)
                ListedUsers.Sort(AddressOf CompareUsernames)

                Dim Data As EditData = GetEdit(GetPage(Config.UserListLocation))
                Data.Text = "{{/Header}}" & vbCrLf

                For Each Item As String In ListedUsers
                    Data.Text &= "* [[Special:Contributions/" & Item & "|" & Item & "]]" & vbCrLf
                Next Item

                Data.Minor = True
                Data.Summary = Config.UserlistUpdateSummary.Replace("$1", MyUser.Name)
                PostEdit(Data)
            End If
        End If

        If Config.WhitelistLocation IsNot Nothing Then
            'Get whitelist
            UpdateStatus("Retrieving user whitelist...")

            Result = GetText(SitePath & "w/api.php?titles=" & Config.WhitelistLocation & _
                "&format=xml&action=query&prop=revisions&rvlimit=1&rvprop=content", "<api>")

            If Result Is Nothing Then
                Abort("Failed to load user whitelist.")
                Exit Sub
            End If

            If Result.Contains("<rev>") Then
                Result = Result.Substring(Result.IndexOf("<rev>") + 5)
                Result = Result.Substring(0, Result.IndexOf("</rev>"))
                Result = HtmlDecode(Result)

                Whitelist.AddRange(Result.Split(CChar(vbLf)))
            End If

            WhitelistLoaded = True
        End If

        'In case user is not already on the whitelist (usually will be)
        MyUser.Level = UserL.Ignore

        'Get bot list
        UpdateStatus("Retrieving bot list...")

        Result = GetText(SitePath & "w/api.php?action=query&format=xml&list=allusers&augroup=bot&aulimit=500")

        If Result IsNot Nothing AndAlso Result.Contains("<allusers>") Then
            Result = Result.Substring(Result.IndexOf("<allusers>"))
            Result = Result.Substring(Result.IndexOf(">") + 1)
            Result = Result.Substring(0, Result.IndexOf("</allusers>"))
            Result = HtmlDecode(Result)

            For Each Item As String In Result.Split(New String() {"<u"}, StringSplitOptions.RemoveEmptyEntries)
                If Item.Contains("""") Then
                    Dim Username As String = Item.Substring(Item.IndexOf("""") + 1)
                    Username = Username.Substring(0, Username.IndexOf(""""))
                    GetUser(Username).Level = UserL.Ignore
                    GetUser(Username).Bot = True
                End If
            Next Item
        End If

        'Get watchlist
        UpdateStatus("Retrieving watchlist...")

        Result = GetText(SitePath & "w/index.php?title=Special:Watchlist/raw", "<textarea ")

        If Result Is Nothing Then
            Abort("Failed to load watchlist.")
            Exit Sub
        End If

        Result = Result.Substring(Result.IndexOf("<textarea "))
        Result = Result.Substring(Result.IndexOf(">") + 1)
        Result = Result.Substring(0, Result.IndexOf("</textarea>"))
        Result = HtmlDecode(Result)

        For Each Item As String In Result.Split(New String() {vbLf}, StringSplitOptions.RemoveEmptyEntries)
            Dim ThisPage As Page = GetPage(Item)
            If Not Watchlist.Contains(ThisPage) Then Watchlist.Add(ThisPage)
        Next Item

        Callback(AddressOf Form.Done)
    End Sub

    Function CompareUsernames(ByVal a As String, ByVal b As String) As Integer
        Return String.Compare(a, b, System.StringComparison.OrdinalIgnoreCase)
    End Function

    Private Sub CaptchaNeeded(ByVal O As Object)
        Dim NewCaptchaForm As New CaptchaForm
        NewCaptchaForm.CaptchaId = CaptchaId

        If NewCaptchaForm.ShowDialog <> DialogResult.OK Then
            CaptchaId = Nothing
            CaptchaWord = Nothing
            Abort("Captcha not solved.")
            Exit Sub
        End If

        CaptchaWord = NewCaptchaForm.Answer.Text
        StartLogin(Form)
    End Sub

    Private Sub UpdateStatus(ByVal Message As String)
        Callback(AddressOf Form.UpdateStatus, CObj(Message))
    End Sub

    Private Sub Abort(ByVal Message As String)
        Callback(AddressOf Form.Abort, CObj(Message))
    End Sub

End Module