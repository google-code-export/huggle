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
    Private SubFamilies As New Dictionary(Of String, List(Of Wiki))
    Private User As User
    Private Wiki As Wiki

    Public ReadOnly Property Session As Session
        Get
            Return _Session
        End Get
    End Property

    Private Sub _Load() Handles Me.Load
        Try
            Logo.Image = Resources.HuggleLogo
            Icon = Resources.Icon
            Text = "{0} {1}".FormatWith(App.Name, App.Version)

            Dim topWiki As Wiki = App.Wikis.Global
            If Config.Global.TopWiki IsNot Nothing Then topWiki = Config.Global.TopWiki
            If Config.Local.LastLogin IsNot Nothing Then topWiki = Config.Local.LastLogin.Wiki

            PopulateSelectors()

            FamilySelector.SelectedItem = SubFamilyName(topWiki)
            WikiSelector.SelectedItem = topWiki

            If Config.Local.LastLogin IsNot Nothing Then
                If Config.Local.LastLogin.IsAnonymous Then
                    If Wiki.AnonymousLogin Then Account.SelectedIndex = 0
                Else
                    If Not Account.Items.Contains(Config.Local.LastLogin) Then Account.Items.Add(Config.Local.LastLogin)
                    Account.SelectedItem = Config.Local.LastLogin
                End If
            End If

            RememberMe.Checked = Config.Local.AutoLogin
            Secure.Checked = Config.Local.LoginSecure

        Catch ex As SystemException
            App.ShowError(Result.FromException(ex))
            DialogResult = DialogResult.Abort
            Close()
        End Try
    End Sub

    Private Function SubFamilyName(ByVal wiki As Wiki) As String
        Dim result As String = If(wiki.Family Is Nothing, Msg("a-other"), wiki.Family.Name)
        If wiki.Type IsNot Nothing Then result = UcFirst(wiki.Type)
        Return result
    End Function

    Private Sub _Shown() Handles Me.Shown
        If Account.Text.Length = 0 Then
            Account.Focus()
        ElseIf Password.Enabled AndAlso Password.Text.Length = 0 Then
            Password.Focus()
        Else
            Login.Focus()
        End If

        Credentials_TextChanged()
    End Sub

    Private Sub Credentials_TextChanged() Handles Password.TextChanged, Account.TextChanged
        Password.Enabled = (Account.Text <> Msg("login-anonymous"))
        Login.Enabled = (Account.Text = Msg("login-anonymous") OrElse (Account.Text <> "" AndAlso Password.Text <> ""))
        If (Account.SelectedIndex = -1 OrElse Account.SelectedIndex >= 2) AndAlso Account.Text.Length > 0 _
            Then User = Wiki.Users.FromString(Account.Text)
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

    Private Sub Account_GotFocus() Handles Account.GotFocus
        If Account.SelectedItem IsNot Nothing AndAlso Account.SelectedItem.ToString = Msg("login-anonymous") Then
            Account.ForeColor = SystemColors.ControlText
            Account.SelectedItem = Nothing
        End If
    End Sub

    Private Sub Account_KeyDown(ByVal s As Object, ByVal e As KeyEventArgs) Handles Account.KeyDown
        If e.KeyCode = Keys.Enter Then
            e.SuppressKeyPress = True
            If Account.Text.Length > 0 Then Password.Focus()
        End If
    End Sub

    Private Sub Account_SelectedIndexChanged() Handles Account.SelectedIndexChanged
        Password.Clear()

        If Account.SelectedItem IsNot Nothing _
            AndAlso Account.SelectedItem.ToString = Msg("login-anonymous") Then

            User = Wiki.Users.Anonymous
            Account.ForeColor = Color.Gray
            Password.Enabled = False

        ElseIf Account.Text.Length > 0 Then
            User = Wiki.Users.FromString(Account.Text)

            If User IsNot Nothing Then
                If User.Config.IsDefault Then User.Config.LoadLocal()

                If User.Password IsNot Nothing Then
                    Password.Text = "********"
                    FakePassword = True
                    Login.Focus()
                Else
                    Password.Focus()
                End If

                Account.ForeColor = SystemColors.ControlText
                Password.Enabled = True
            End If
        End If
    End Sub

    Private Sub FamilySelector_SelectedIndexChanged() Handles FamilySelector.SelectedIndexChanged
        WikiSelector.BeginUpdate()
        WikiSelector.Items.Clear()

        If FamilySelector.SelectedItem IsNot Nothing Then
            For Each wiki As Wiki In SubFamilies(FamilySelector.SelectedItem.ToString)
                If wiki.IsHidden OrElse Not wiki.IsPublicReadable OrElse Not wiki.IsPublicEditable Then Continue For
                WikiSelector.Items.Add(wiki)
            Next wiki

            If WikiSelector.Items.Count > 0 Then WikiSelector.SelectedIndex = 0
        End If

        WikiSelector.ResizeDropDown()
        WikiSelector.EndUpdate()
    End Sub

    Private Sub WikiSelector_SelectedIndexChanged() Handles WikiSelector.SelectedIndexChanged
        If WikiSelector.SelectedItem Is Nothing Then
            Wiki = Nothing
            Account.Items.Clear()

        ElseIf TypeOf WikiSelector.SelectedItem Is Wiki Then
            Wiki = CType(WikiSelector.SelectedItem, Wiki)
            Wiki.Config.LoadLocal()
            Account.Items.Clear()

            For Each user As User In Wiki.Users.All
                If Not user.IsAnonymous AndAlso Not user.IsDefault Then
                    user.Config.LoadLocal()
                    If Not user.Config.IsDefault Then Account.Items.Add(user)
                End If
            Next user

            CreateAccount.Visible = Wiki.IsPublicEditable
            If Wiki.AnonymousLogin Then Account.Items.Insert(0, Msg("login-anonymous"))

            ResizeDropDown(Account)
            User = Wiki.Users.FromString(Account.Text)
            LastSelectedWiki = CType(WikiSelector.SelectedItem, Wiki)
        Else
            Wiki = Nothing
            Account.Items.Clear()
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
            (New Result(Msg("login-fail", Msg("login-error-badusername")))) : Return False

        If user.DisplayName = user.Name Then user.DisplayName = Account.Text
        If Not FakePassword Then user.Password = Scramble(Password.Text, Hash(user))

        Dim session As Session = App.Sessions(user)
        session.IsSecure = (Secure.Enabled AndAlso Secure.Checked)

        Config.Local.LastLogin = user
        Config.Local.LoginSecure = session.IsSecure
        Config.Local.Save()

        Dim loginAction As New Login(session, "Login")
        App.UserWaitForProcess(loginAction)

        Return loginAction.IsSuccess
    End Function

    Private Sub PopulateSelectors()
        For Each wiki As Wiki In App.Wikis.All
            Dim familyName As String = SubFamilyName(wiki)
            If Not SubFamilies.ContainsKey(familyName) Then SubFamilies.Add(familyName, New List(Of Wiki))
            SubFamilies(familyName).Merge(wiki)
        Next wiki

        For Each subFamily As List(Of Wiki) In SubFamilies.Values
            subFamily.Sort(AddressOf CompareWikis)
        Next subFamily

        Dim subFamilyNames As List(Of String) = SubFamilies.Keys.ToList
        subFamilyNames.Sort()

        FamilySelector.BeginUpdate()
        FamilySelector.Items.AddRange(subFamilyNames.ToArray)
        FamilySelector.ResizeDropDown()
        FamilySelector.EndUpdate()
    End Sub

    Private Sub AddWiki_LinkClicked() Handles AddWiki.LinkClicked
        Dim form As New WikiAddForm()
        form.ShowDialog()

        If form.Wiki Is Nothing Then
            WikiSelector.SelectedItem = LastSelectedWiki
        Else
            PopulateSelectors()
            FamilySelector.SelectedItem = SubFamilyName(form.Wiki)
            WikiSelector.SelectedItem = form.Wiki

            If form.User Is Nothing Then
                Account.Focus()
            Else
                Account.Text = form.User.Name
                Password.Text = "********"
                FakePassword = True
                Login.Focus()
            End If
        End If
    End Sub

    Private Sub CreateAccount_LinkClicked() Handles CreateAccount.LinkClicked
        Dim createForm As New AccountCreateForm(Wiki.Users.Anonymous.Session)

        If createForm.ShowDialog = DialogResult.Cancel Then
            Account.SelectedItem = Nothing

            'Restore previous selection
            If User IsNot Nothing Then _
                If User.IsAnonymous Then Account.SelectedIndex = 0 Else Account.Text = User.Name
        Else
            'Add new account to list
            If Not Account.Items.Contains(createForm.NewUser) Then Account.Items.Add(createForm.NewUser)
            Account.SelectedItem = createForm.NewUser
            Password.Text = "********"
            FakePassword = True
            Login.Focus()
        End If

        If Not Wiki.IsPublicEditable Then WikiSelector_SelectedIndexChanged()
    End Sub

End Class