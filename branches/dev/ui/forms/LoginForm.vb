Imports Huggle
Imports Huggle.Actions
Imports System
Imports System.Collections.Generic
Imports System.ComponentModel
Imports System.Drawing
Imports System.Windows.Forms

Public Class LoginForm

    Private _Session As Session

    Private FakePassword As Boolean
    Private LastSelectedWiki As Wiki
    Private User As User
    Private Wiki As Wiki

    Private Shared ReadOnly Separator As String = "----------------------------------------------"

    Public ReadOnly Property Session As Session
        Get
            Return _Session
        End Get
    End Property

    Private Sub _Load() Handles Me.Load
        Try
            Logo.Image = Resources.HuggleLogo
            Icon = Resources.Icon
            Text = "{0} {1}".FormatWith(Application.ProductName, Application.ProductVersion)

            Dim topWiki As Wiki = App.Wikis.Global
            If Config.Global.TopWiki IsNot Nothing Then topWiki = Config.Global.TopWiki
            If Config.Local.LastLogin IsNot Nothing Then topWiki = Config.Local.LastLogin.Wiki

            'Populate wiki selector
            Dim otherWikis As New List(Of Wiki)
            Dim popularWikis As New List(Of Wiki)
            Dim specialWikis As New List(Of Wiki)

            For Each wiki As Wiki In App.Wikis.All
                If wiki.IsHidden OrElse Not wiki.IsPublicReadable OrElse Not wiki.IsPublicEditable Then Continue For

                If wiki IsNot topWiki AndAlso (wiki.IsPopular OrElse wiki.IsCustom) Then popularWikis.Add(wiki)
                If Not wiki.IsCustom AndAlso Not (wiki.IsPopular AndAlso wiki.Type = "special") _
                    Then If wiki.Type = "special" Then specialWikis.Add(wiki) Else otherWikis.Add(wiki)
            Next wiki

            otherWikis.Sort(AddressOf CompareWikis)
            popularWikis.Sort(AddressOf CompareWikis)
            specialWikis.Sort(AddressOf CompareWikis)

            WikiSelector.Items.Add(topWiki)
            WikiSelector.Items.AddRange(popularWikis.ToArray)
            WikiSelector.Items.Add(Msg("login-addwiki"))
            WikiSelector.Items.Add(Separator)
            WikiSelector.Items.AddRange(otherWikis.ToArray)
            WikiSelector.Items.Add(Separator)
            WikiSelector.Items.AddRange(specialWikis.ToArray)

            WikiSelector.SelectedItem = topWiki

            If Config.Local.LastLogin IsNot Nothing Then
                If Config.Local.LastLogin.IsAnonymous Then
                    If Wiki.AnonymousLogin Then Username.SelectedIndex = 0
                Else
                    If Not Username.Items.Contains(Config.Local.LastLogin) Then Username.Items.Add(Config.Local.LastLogin)
                    Username.SelectedItem = Config.Local.LastLogin
                End If
            End If

            RememberMe.Checked = Config.Local.AutoLogin
            Secure.Checked = Config.Local.LoginSecure

            'Widen wiki list if necessary to accommodate long wiki names
            ResizeDropDown(WikiSelector)

        Catch ex As SystemException
            App.ShowError(Result.FromException(ex))
            DialogResult = DialogResult.Abort
            Close()
        End Try
    End Sub

    Private Sub _Shown() Handles Me.Shown
        If Username.Text = "" Then Username.Focus() Else If Password.Text = "" Then Password.Focus() Else Login.Focus()
        Credentials_TextChanged()
    End Sub

    Private Sub Credentials_TextChanged() Handles Password.TextChanged, Username.TextChanged
        Password.Enabled = (Username.Text <> Msg("login-anonymous"))
        Login.Enabled = (Username.Text = Msg("login-anonymous") OrElse (Username.Text <> "" AndAlso Password.Text <> ""))
        If (Username.SelectedIndex = -1 OrElse Username.SelectedIndex >= 2) AndAlso Username.Text.Length > 0 _
            Then User = Wiki.Users.FromString(Username.Text)
    End Sub

    Private Sub Login_Click() Handles Login.Click
        If DoLogin(User) Then
            _Session = User.Session
            DialogResult = DialogResult.OK
            Close()
        Else
            Login.Focus()
        End If
    End Sub

    Private Sub Password_Enter() Handles Password.Enter
        If FakePassword Then
            Password.Clear()
            FakePassword = False
        End If
    End Sub

    Private Sub Password_KeyDown(ByVal s As Object, ByVal e As KeyEventArgs) Handles Password.KeyDown
        If e.KeyCode = Keys.Enter Then
            e.SuppressKeyPress = True
            Login_Click()
        End If
    End Sub

    Private Sub RememberMe_CheckedChanged() Handles RememberMe.CheckedChanged
        Config.Local.AutoLogin = RememberMe.Checked
    End Sub

    Private Sub Username_GotFocus() Handles Username.GotFocus
        If Username.SelectedItem IsNot Nothing AndAlso Username.SelectedItem.ToString = Msg("login-anonymous") Then
            Username.ForeColor = SystemColors.ControlText
            Username.SelectedItem = Nothing
        End If
    End Sub

    Private Sub Username_KeyDown(ByVal s As Object, ByVal e As KeyEventArgs) Handles Username.KeyDown
        If e.KeyCode = Keys.Enter Then
            e.SuppressKeyPress = True
            If Username.Text.Length > 0 Then Password.Focus()
        End If
    End Sub

    Private Sub Username_SelectedIndexChanged() Handles Username.SelectedIndexChanged
        Password.Clear()

        If Username.SelectedItem IsNot Nothing _
            AndAlso Username.SelectedItem.ToString = Msg("login-anonymous") Then

            User = Wiki.Users.Anonymous
            Username.ForeColor = Color.Gray
            Password.Enabled = False
            Login.Focus()

        ElseIf Username.SelectedItem IsNot Nothing _
            AndAlso Username.SelectedItem.ToString = Msg("login-createaccount") Then

            Dim createForm As New AccountCreateForm(Wiki.Users.Anonymous.Session)

            If createForm.ShowDialog = DialogResult.Cancel Then
                Username.SelectedItem = Nothing

                'Restore previous selection
                If User IsNot Nothing Then _
                    If User.IsAnonymous Then Username.SelectedIndex = 0 Else Username.Text = User.Name
            Else
                'Add new account to list
                If Not Username.Items.Contains(createForm.NewUser) Then Username.Items.Add(createForm.NewUser)
                Username.SelectedItem = createForm.NewUser
                Password.Text = "********"
                FakePassword = True
                Login.Focus()
            End If

            If Not Wiki.IsPublicEditable Then Wikis_SelectedIndexChanged()

        ElseIf Username.Text.Length > 0 Then
            User = Wiki.Users.FromString(Username.Text)

            If User IsNot Nothing Then
                If User.Config.IsDefault Then User.Config.LoadLocal()

                If User.Password IsNot Nothing Then
                    Password.Text = "********"
                    FakePassword = True
                    Login.Focus()
                Else
                    Password.Focus()
                End If

                Username.ForeColor = SystemColors.ControlText
                Password.Enabled = True
            End If
        End If
    End Sub

    Private Sub Wikis_SelectedIndexChanged() Handles WikiSelector.SelectedIndexChanged
        If WikiSelector.SelectedItem Is Nothing Then
            Wiki = Nothing
            Username.Items.Clear()

        ElseIf WikiSelector.SelectedItem.ToString = Separator Then
            WikiSelector.SelectedItem = LastSelectedWiki

        ElseIf WikiSelector.SelectedItem.ToString = Msg("login-addwiki") Then
            Dim form As New WikiAddForm
            form.ShowDialog()

            If form.Wiki Is Nothing Then
                WikiSelector.SelectedItem = LastSelectedWiki
            Else
                If Not WikiSelector.Items.Contains(form.Wiki) Then WikiSelector.Items.Insert(2, form.Wiki)
                WikiSelector.SelectedItem = form.Wiki

                If form.User Is Nothing Then
                    Username.Focus()
                Else
                    Username.Text = form.User.Name
                    Password.Text = "********"
                    FakePassword = True
                    Login.Focus()
                End If
            End If

        Else
            Wiki = CType(WikiSelector.SelectedItem, Wiki)
            Wiki.Config.LoadLocal()
            Username.Items.Clear()

            For Each user As User In Wiki.Users.All
                If Not user.IsAnonymous AndAlso Not user.IsDefault Then
                    user.Config.LoadLocal()
                    If Not user.Config.IsDefault Then Username.Items.Add(user)
                End If
            Next user

            If Wiki.IsPublicEditable Then Username.Items.Insert(0, Msg("login-createaccount"))
            If Wiki.AnonymousLogin Then Username.Items.Insert(0, Msg("login-anonymous"))

            ResizeDropDown(Username)
            User = Wiki.Users.FromString(Username.Text)
            LastSelectedWiki = CType(WikiSelector.SelectedItem, Wiki)
        End If

        Secure.Enabled = (Wiki IsNot Nothing AndAlso Wiki.SecureUrl IsNot Nothing)
    End Sub

    Private Function CompareWikis(ByVal x As Wiki, ByVal y As Wiki) As Integer
        If x.Type <> y.Type Then Return String.Compare(x.Type, y.Type)
        If x.Language IsNot Nothing AndAlso y.Language IsNot Nothing _
            Then Return String.Compare(x.Language.Code, y.Language.Code)
        Return String.Compare(x.Name, y.Name)
    End Function

    Private Function DoLogin(ByVal user As User) As Boolean
        If user Is Nothing Then App.ShowError _
            (New Result({Msg("login-fail", Msg("login-error-badusername"))})) : Return False

        If user.DisplayName = user.Name Then user.DisplayName = Username.Text
        If Not FakePassword Then user.Password = Scramble(Password.Text, Hash(user))

        Dim session As Session = App.Sessions(user)
        session.IsSecure = (Secure.Enabled AndAlso Secure.Checked)

        Config.Local.LastLogin = user
        Config.Local.LoginSecure = session.IsSecure
        Config.Local.Save()

        Dim loginAction As New Login(session, "Login")
        App.UserWaitForProcess(loginAction)

        If loginAction.IsErrored Then App.ShowError(loginAction.Result)
        Return loginAction.IsSuccess
    End Function

End Class