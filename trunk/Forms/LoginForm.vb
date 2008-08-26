Imports System.Net
Imports System.Threading

Class LoginForm

    Public LoggingIn As Boolean
    Private Request As LoginRequest

    Private Sub LoginForm_Load() Handles Me.Load
        SyncContext = SynchronizationContext.Current
        Icon = My.Resources.icon_red_button
        Height = 270

        LoadLocalConfig()

#If DEBUG Then
        'If the app is in debug mode add a localhost wiki to the project list
        If Not Config.Projects.Contains("localhost;localhost") Then Config.Projects.Add("localhost;localhost")
#End If

        For Each Item As String In Config.Projects
            If Item.Contains(";") Then Project.Items.Add(Item.Substring(0, Item.IndexOf(";")))
        Next Item

        If Project.Items.Count > 0 Then Project.SelectedIndex = 0

        Proxy.Checked = Config.ProxyEnabled
        ProxyPort.Text = Config.ProxyPort
        If ProxyPort.Text.Length = 0 Then ProxyPort.Text = "80"
        ProxyAddress.Text = Config.ProxyServer
        ProxyDomain.Text = Config.ProxyUserDomain
        ProxyUsername.Text = Config.ProxyUsername

        Version.Text = "Version " & VersionString(Config.Version)
        If Config.RememberMe Then Username.Text = Config.Username
        If Username.Text = "" Then Username.Focus() Else Password.Focus()
    End Sub

    Private Sub LoginForm_Shown() Handles Me.Shown
        If Username.Text = "" Then Username.Focus() Else Password.Focus()
    End Sub

    Private Sub LoginForm_KeyDown(ByVal s As Object, ByVal e As KeyEventArgs) Handles Me.KeyDown
        If e.KeyCode = Keys.Escape Then End
    End Sub

    Private Sub Username_KeyDown(ByVal s As Object, ByVal e As KeyEventArgs) Handles Username.KeyDown
        If e.KeyCode = Keys.Enter Then Password.Focus()
    End Sub

    Private Sub Password_KeyDown(ByVal s As Object, ByVal e As KeyEventArgs) Handles Password.KeyDown
        If e.KeyCode = Keys.Enter Then OK_Click()
    End Sub

    Private Sub Password_TextChanged() Handles Password.TextChanged
        OK.Enabled = (Username.Text <> "" AndAlso Password.Text <> "")
    End Sub

    Private Sub Username_TextChanged() Handles Username.TextChanged
        OK.Enabled = (Username.Text <> "" AndAlso Password.Text <> "")
    End Sub

    Private Sub OK_Click() Handles OK.Click
        LoggingIn = True

        For Each Item As String In Config.Projects
            If Item.StartsWith(Project.Text & ";") Then
                Config.Project = Item.Substring(Item.IndexOf(";") + 1)
                Exit For
            End If
        Next Item

        Config.SitePath = "http://" & Config.Project & "/"
        Config.IrcMode = (Config.Project <> "localhost")
        If Config.Project.Contains(".org") Then Config.IrcChannel = "#" & _
            Config.Project.Substring(0, Config.Project.IndexOf(".org"))

        For Each Item As Control In Controls
            If Not ArrayContains(New Control() {Title, Version, Status, Progress, Cancel}, Item) _
                Then Item.Enabled = False
        Next Item

        Cancel.Text = "Cancel"

        Login.Password = Password.Text
        Config.ProxyEnabled = Proxy.Checked
        Config.ProxyPort = ProxyPort.Text
        ProxyAddress.Text = ProxyAddress.Text.Replace("http://", "")
        Config.ProxyServer = ProxyAddress.Text
        Config.ProxyUserDomain = ProxyDomain.Text
        Config.ProxyUsername = ProxyUsername.Text
        Config.Username = (Username.Text.Substring(0, 1).ToUpper & Username.Text.Substring(1)).Replace("_", " ")

        Try
            Login.ConfigureProxy(Proxy.Checked, ProxyAddress.Text, ProxyPort.Text, ProxyUsername.Text, _
                ProxyPassword.Text, ProxyDomain.Text)

        Catch ex As Exception
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
            Abort("Cancelled.")
            OK.Focus()
        Else
            End
        End If
    End Sub

    Private Sub ShowProxySettings_Click() Handles ShowProxySettings.Click
        Height += 160 'Resize form
        ProxyGroup.Visible = True

        'Switch the show proxy button for a hide proxy button
        HideProxySettings.Visible = True
        ShowProxySettings.Visible = False
    End Sub

    Private Sub HideProxySettings_Click() Handles HideProxySettings.Click
        Height -= 160 'Resize form
        ProxyGroup.Visible = False

        'Switch the hide proxy button for a show proxy button
        HideProxySettings.Visible = False
        ShowProxySettings.Visible = True
    End Sub

    Private Sub Proxy_CheckedChanged() Handles Proxy.CheckedChanged
        For Each Item As Control In ProxyGroup.Controls
            If Item IsNot Proxy Then Item.Enabled = Proxy.Checked
        Next Item
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

    Sub Abort(ByVal MessageObject As Object)
        LoggingIn = False
        Status.Text = CStr(MessageObject)
        Cancel.Text = "Exit"
        Progress.Value = 0

        For Each Item As Control In Controls
            If Not ArrayContains(New Control() {Title, Version, Status, Progress, Cancel}, Item) _
                Then Item.Enabled = True
        Next Item
    End Sub

End Class
