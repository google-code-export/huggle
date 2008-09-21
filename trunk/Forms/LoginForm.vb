Imports System.Net
Imports System.Threading

Class LoginForm

    Public LoggingIn As Boolean
    Private Request As LoginRequest

    Private Sub LoginForm_Load() Handles Me.Load
        SyncContext = SynchronizationContext.Current
        Icon = My.Resources.icon_red_button
        Height = 314

        Config = New Configuration
        LoadLocalConfig()
        LoadLanguages()

        For Each Item As String In Config.Languages
            If Config.Messages(Item).ContainsKey("name") Then Language.Items.Add(Config.Messages(Item)("name"))
        Next Item

        Language.SelectedItem = Config.Messages(Config.Language)("name")
        If Language.SelectedIndex = -1 Then Language.SelectedItem = Config.Messages(Config.DefaultLanguage)("name")

#If DEBUG Then
        'If the app is in debug mode add a localhost wiki to the project list
        If Not Config.Projects.Contains("localhost;localhost") Then Config.Projects.Add("localhost;localhost")
#End If

        For Each Item As String In Config.Projects
            If Item.Contains(";") Then Project.Items.Add(Item.Substring(0, Item.IndexOf(";")))
        Next Item

        If Config.Project IsNot Nothing Then Project.SelectedItem = Config.Project
        If Project.SelectedIndex = -1 Then Project.SelectedIndex = 0

        Proxy.Checked = Config.ProxyEnabled
        ProxyPort.Text = Config.ProxyPort
        If ProxyPort.Value = 0 Then ProxyPort.Value = 80
        ProxyAddress.Text = Config.ProxyServer
        ProxyDomain.Text = Config.ProxyUserDomain
        ProxyUsername.Text = Config.ProxyUsername
        Text = "Huggle " & VersionString(Config.Version)

        If Config.RememberMe Then Username.Text = Config.Username
        If Config.RememberPassword Then Password.Text = Config.Password

        If Username.Text = "" Then Username.Focus() Else If Password.Text = "" Then Password.Focus() Else OK.Focus()
    End Sub

    Private Sub LoginForm_Shown() Handles Me.Shown
        If Username.Text = "" Then Username.Focus() Else If Password.Text = "" Then Password.Focus() Else OK.Focus()
    End Sub

    Private Sub LoginForm_KeyDown(ByVal s As Object, ByVal e As KeyEventArgs) Handles Me.KeyDown
        If e.KeyCode = Keys.Escape Then End
    End Sub

    Private Sub OK_Click() Handles OK.Click
        LoggingIn = True

        For Each Item As String In Config.Projects
            If Item.StartsWith(Project.Text & ";") Then
                Config.Project = Item.Substring(0, Item.IndexOf(";"))
                Config.SitePath = "http://" & Item.Substring(Item.IndexOf(";") + 1) & "/"
                Exit For
            End If
        Next Item

        If Config.Project <> "localhost" _
            Then Config.IrcChannel = "#" & Config.Project.Substring(0, Config.Project.Length)

        Config.IrcMode = (Config.Project <> "localhost")
        Config.ProxyEnabled = Proxy.Checked
        Config.ProxyPort = ProxyPort.Text
        Config.ProxyServer = ProxyAddress.Text.Replace("http://", "")
        Config.ProxyUserDomain = ProxyDomain.Text
        Config.ProxyUsername = ProxyUsername.Text
        Config.Username = User.SanitizeName(Username.Text)
        Config.Password = Password.Text

        For Each Item As Control In Controls
            If Not ArrayContains(New Control() {Status, Progress, Cancel}, Item) Then Item.Enabled = False
        Next Item

        Cancel.Text = Msg("cancel")

        Try
            Login.ConfigureProxy(Proxy.Checked, Config.ProxyServer, ProxyPort.Value, ProxyUsername.Text, _
                ProxyPassword.Text, ProxyDomain.Text)

        Catch ex As UriFormatException
            Abort(ex.Message)
        End Try

        Request = New LoginRequest
        Request.LoginForm = Me
        Request.Start()
    End Sub

    Private Sub Cancel_Click() Handles Cancel.Click
        If LoggingIn Then
            Irc.Disconnect()
            Request.Cancel()
            Abort(Msg("login-error-cancelled"))
        Else
            End
        End If
    End Sub

    Private Sub HideProxySettings_Click() Handles HideProxySettings.Click
        Height -= 160
        ProxyGroup.Visible = False
        HideProxySettings.Visible = False
        ShowProxySettings.Visible = True
        ShowProxySettings.Focus()
    End Sub

    Private Sub Language_SelectedIndexChanged() Handles Language.SelectedIndexChanged

        For Each Item As String In Config.Languages
            If Config.Messages(Item)("name") = Language.SelectedItem.ToString Then
                Config.Language = Item
                Exit For
            End If
        Next Item

        Localize(Me, "login")
        ProxyUsernameLabel.Text = Msg("login-username")
        ProxyPasswordLabel.Text = Msg("login-password")
        ShowProxySettings.Text = Msg("login-proxygroup") & " >>"
        HideProxySettings.Text = "<< " & Msg("login-proxygroup")
        OK.Text = Msg("login-start")
        Cancel.Text = Msg("exit")
        Status.Text = ""
    End Sub

    Private Sub Password_KeyDown(ByVal s As Object, ByVal e As KeyEventArgs) Handles Password.KeyDown
        If e.KeyCode = Keys.Enter Then OK_Click()
    End Sub

    Private Sub Password_TextChanged() Handles Password.TextChanged
        OK.Enabled = (Username.Text <> "" AndAlso Password.Text <> "")
    End Sub

    Private Sub ShowProxySettings_Click() Handles ShowProxySettings.Click
        Height += 160
        ProxyGroup.Visible = True
        HideProxySettings.Visible = True
        ShowProxySettings.Visible = False
        HideProxySettings.Focus()
    End Sub

    Private Sub Proxy_CheckedChanged() Handles Proxy.CheckedChanged
        For Each Item As Control In ProxyGroup.Controls
            If Item IsNot Proxy Then Item.Enabled = Proxy.Checked
        Next Item
    End Sub

    Private Sub Username_KeyDown(ByVal s As Object, ByVal e As KeyEventArgs) Handles Username.KeyDown
        If e.KeyCode = Keys.Enter Then Password.Focus()
    End Sub

    Private Sub Username_TextChanged() Handles Username.TextChanged
        OK.Enabled = (Username.Text <> "" AndAlso Password.Text <> "")
    End Sub

    Sub Abort(ByVal MessageObject As Object)
        LoggingIn = False
        Status.Text = CStr(MessageObject)
        Cancel.Text = Msg("exit")
        Progress.Value = 0

        For Each Item As Control In Controls
            If Not ArrayContains(New Control() {Status, Progress, Cancel}, Item) Then Item.Enabled = True
        Next Item

        Dim CurrentLanguage As String = Config.Language

        Config = New Configuration
        LoadLocalConfig()
        LoadLanguages()

        Config.Language = CurrentLanguage

        If Username.Text = "" Then Username.Focus() Else If Password.Text = "" Then Password.Focus() Else OK.Focus()
    End Sub

    Sub Done()
        If Config.StartupMessage AndAlso Config.StartupMessageLocation IsNot Nothing Then
            Dim NewStartupForm As New StartupForm
            NewStartupForm.Show()
            Close()
        Else
            MainForm = New Main
            MainForm.Show()
            MainForm.Initialize()
            Close()
        End If
    End Sub

    Sub UpdateStatus(ByVal MessageObject As Object)
        Status.Text = CStr(MessageObject)
        If Progress.Value <= Progress.Maximum - 1 Then Progress.Value += 1
    End Sub

End Class
