Imports Huggle.Queries
Imports System
Imports System.Collections.Generic
Imports System.ComponentModel
Imports System.Drawing
Imports System.Windows.Forms

Namespace Huggle.UI

    Friend Class LoginForm : Inherits HuggleForm

        Private _Session As Session

        Private LastSelectedWiki As Wiki
        Private WikisByFamily As New Dictionary(Of String, List(Of Wiki))
        Private User As User
        Private Wiki As Wiki

        Public ReadOnly Property Session As Session
            Get
                Return _Session
            End Get
        End Property

        Private Sub _Load() Handles Me.Load
            App.Languages.Current.Localize(Me)
            Logo.Image = Resources.HuggleLogo
            Icon = Resources.Icon
            Text = "{0} {1}".FormatForUser(App.Name, App.VersionString)

            Dim selectedWiki As Wiki = App.Wikis.Global
            If Config.Global.TopWiki IsNot Nothing AndAlso IsShown(Config.Global.TopWiki) _
                Then selectedWiki = Config.Global.TopWiki
            If Config.Local.LastLogin IsNot Nothing AndAlso IsShown(Config.Local.LastLogin.Wiki) _
                Then selectedWiki = Config.Local.LastLogin.Wiki

            PopulateSelectors()

            FamilySelector.SelectedItem = WikiTypeName(selectedWiki)
            WikiSelector.SelectedItem = selectedWiki

            If Config.Local.LastLogin IsNot Nothing Then
                If Config.Local.LastLogin.IsAnonymous Then
                    If Wiki.AnonymousLogin Then Account.SelectedItem = Msg("form-login-anonymous")
                Else
                    If Not Account.Items.Contains(Config.Local.LastLogin) _
                        Then Account.Items.Add(Config.Local.LastLogin)
                    Account.SelectedItem = Config.Local.LastLogin
                End If
            End If

            RememberMe.Checked = Config.Local.AutoLogin
            Secure.Checked = Config.Local.SecureLogin
        End Sub

        Private Shared Function WikiTypeName(ByVal wiki As Wiki) As String
            If wiki.Family Is Nothing Then Return Msg("wikitype-other")
            If wiki.Type Is Nothing Then Return wiki.Family.Name
            If App.Languages.Current.Messages.ContainsKey("wikitype-" & wiki.Type) Then Return Msg("wikitype-" & wiki.Type)
            Return wiki.Type.ToUpperFirstI
        End Function

        Private Shared Function IsShown(ByVal wiki As Wiki) As Boolean
            Return Not wiki.IsHidden AndAlso wiki.IsPublicReadable AndAlso wiki.IsPublicEditable
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
            Password.Enabled = (Account.Text <> Msg("form-login-anonymous"))
            Login.Enabled = (Account.Text = Msg("form-login-anonymous") _
                OrElse (Account.Text <> "" AndAlso Password.Text <> ""))
            If (Account.SelectedIndex = -1 OrElse Account.SelectedIndex >= 2) _
                AndAlso Account.Text.Length > 0 Then User = Wiki.Users.FromString(Account.Text)
            Account.ForeColor = If(Account.Text = Msg("form-login-anonymous"),
                SystemColors.GrayText, SystemColors.ControlText)
        End Sub

        Private Sub Login_Click() Handles Login.Click
            _Session = DoLogin(User)

            If Session IsNot Nothing Then
                DialogResult = DialogResult.OK
                Close()
            Else
                Login.Focus()
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
            If Account.SelectedItem IsNot Nothing AndAlso Account.SelectedItem.ToString = Msg("form-login-anonymous") Then
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

            If Account.SelectedItem IsNot Nothing AndAlso Account.SelectedItem.ToString = Msg("form-login-anonymous") Then
                User = Wiki.Users.Anonymous
                Account.ForeColor = SystemColors.GrayText
                Password.Enabled = False

            ElseIf Account.Text.Length > 0 Then
                User = Wiki.Users.FromString(Account.Text)

                If User IsNot Nothing Then
                    If Not User.Config.IsLoaded Then User.Config.LoadLocal()

                    If User.Password IsNot Nothing Then
                        Password.Text = Unscramble(User.FullName, User.Password, Hash(User))
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
                For Each wiki As Wiki In WikisByFamily(FamilySelector.SelectedItem.ToString)
                    If IsShown(wiki) Then WikiSelector.Items.Add(wiki)
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

                Account.BeginUpdate()
                Account.Items.Clear()

                For Each user As User In Wiki.Users.Used
                    If Not user.IsAnonymous AndAlso Not user.IsDefault Then
                        user.Config.LoadLocal()
                        If user.Config.IsLoaded Then Account.Items.Add(user)
                    End If
                Next user

                CreateAccount.Visible = Wiki.IsPublicEditable
                If Wiki.AnonymousLogin Then Account.Items.Insert(0, Msg("form-login-anonymous"))

                If Account.Items.Count > 1 Then
                    Account.SelectedIndex = 0
                Else
                    Account.Text = Nothing
                    User = Nothing
                End If

                Account.ResizeDropDown()
                Account.EndUpdate()

                LastSelectedWiki = CType(WikiSelector.SelectedItem, Wiki)
            Else
                Wiki = Nothing
                Account.Items.Clear()
            End If

            Secure.Enabled = (Wiki IsNot Nothing AndAlso Wiki.SecureUrl IsNot Nothing)
        End Sub

        Private Function DoLogin(ByVal user As User) As Session
            If user Is Nothing Then App.ShowError _
                (New Result(Msg("login-fail", Msg("login-error-badusername")))) : Return Nothing

            If user.DisplayName = user.Name Then user.DisplayName = Account.Text
            user.Password = Scramble(user.FullName, Password.Text, Hash(user))

            Dim session As Session = App.Sessions(user)
            session.IsSecure = (Secure.Enabled AndAlso Secure.Checked)

            Config.Local.LastLogin = user
            Config.Local.SecureLogin = session.IsSecure
            Config.Local.SaveLocal()

            Dim loginAction As New Login(session, "Login")
            loginAction.Interactive = True
            App.UserWaitForProcess(loginAction)

            If loginAction.IsFailed Then Return Nothing
            Return loginAction.Session
        End Function

        Private Sub PopulateSelectors()
            For Each wiki As Wiki In App.Wikis.All
                Dim familyName As String = WikiTypeName(wiki)

                If Not WikisByFamily.ContainsKey(familyName) Then WikisByFamily.Add(familyName, New List(Of Wiki))
                WikisByFamily(familyName).Add(wiki)
            Next wiki

            For Each family As List(Of Wiki) In WikisByFamily.Values
                family.Sort(AddressOf CompareWikis)
            Next family

            Dim familyNames As List(Of String) = WikisByFamily.Keys.ToList
            familyNames.Sort()
            familyNames.Unmerge(Msg("wikitype-other"))

            FamilySelector.BeginUpdate()
            FamilySelector.Items.Clear()
            FamilySelector.Items.AddRange(familyNames.ToArray)
            If WikisByFamily.ContainsKey(Msg("wikitype-other")) Then FamilySelector.Items.Add(Msg("wikitype-other"))
            FamilySelector.ResizeDropDown()
            FamilySelector.EndUpdate()
        End Sub

        Private Function CompareWikis(ByVal x As Wiki, ByVal y As Wiki) As Integer
            If x.Type <> y.Type Then Return String.Compare(x.Type, y.Type, StringComparison.Ordinal)
            If x.Language IsNot Nothing AndAlso y.Language IsNot Nothing _
                Then Return String.Compare(x.Language.Code, y.Language.Code, StringComparison.Ordinal)
            Return String.Compare(x.Name, y.Name, StringComparison.Ordinal)
        End Function

        Private Sub AddWiki_LinkClicked() Handles AddWiki.LinkClicked
            Using form As New WikiAddForm()
                form.ShowDialog()

                If form.Wiki Is Nothing Then
                    WikiSelector.SelectedItem = LastSelectedWiki
                Else
                    PopulateSelectors()
                    FamilySelector.SelectedItem = WikiTypeName(form.Wiki)
                    WikiSelector.SelectedItem = form.Wiki

                    If form.User Is Nothing Then
                        Account.Focus()
                    Else
                        Account.Text = form.User.Name
                        Password.Text = Unscramble(User.FullName, User.Password, Hash(User))
                        Login.Focus()
                    End If
                End If
            End Using
        End Sub

        Private Sub CreateAccount_LinkClicked() Handles CreateAccount.LinkClicked
            Using form As New AccountCreateForm(App.Sessions(Wiki.Users.Anonymous))
                If form.ShowDialog = DialogResult.Cancel Then
                    Account.SelectedItem = Nothing

                    'Restore previous selection
                    If User IsNot Nothing Then _
                        If User.IsAnonymous Then Account.SelectedIndex = 0 Else Account.Text = User.Name
                Else
                    'Add new account to list
                    If Not Account.Items.Contains(form.NewUser) Then Account.Items.Add(form.NewUser)
                    Account.SelectedItem = form.NewUser
                    Password.Text = Unscramble(form.NewUser.FullName, form.NewUser.Password, Hash(form.NewUser))
                    Login.Focus()
                End If

                If Not Wiki.IsPublicEditable Then WikiSelector_SelectedIndexChanged()
            End Using
        End Sub

    End Class

End Namespace