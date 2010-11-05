Imports Huggle.Actions
Imports System
Imports System.Collections.Generic
Imports System.Windows.Forms

Namespace Huggle.UI

    Friend Class AccountCreateForm : Inherits HuggleForm

        Private _NewUser As User
        Private _Session As Session

        Private CheckResults As New Dictionary(Of String, CheckStatus)
        Private Confirmation As Confirmation
        Private CurrentQuery As UsernameCheckQuery
        Private Status As CheckStatus

        Private WithEvents CheckTimer As New Timer With {.Interval = 800}

        Friend Sub New(ByVal session As Session)
            InitializeComponent()
            _Session = session
        End Sub

        Friend ReadOnly Property NewUser() As User
            Get
                Return _NewUser
            End Get
        End Property

        Friend ReadOnly Property Session() As Session
            Get
                Return _Session
            End Get
        End Property

        Private Sub _Load() Handles Me.Load
            Try
                Icon = Resources.Icon
                WikiDisplay.Text = Session.Wiki.Name
                GetConfirmation()

            Catch ex As SystemException
                App.ShowError(Result.FromException(ex))
                DialogResult = DialogResult.Abort
                Close()
            End Try
        End Sub

        Private Sub ConfirmRefresh_LinkClicked() Handles ConfirmRefresh.LinkClicked
            Session.Wiki.CurrentConfirmation = Nothing
            GetConfirmation()
        End Sub

        Private Sub OK_Click() Handles OK.Click
            _NewUser = Session.Wiki.Users.FromString(Username.Text)

            NewUser.Password = Scramble(Session.User.FullName, Password.Text, Hash(NewUser))
            If Confirmation IsNot Nothing Then Confirmation.Answer = ConfirmInput.Text
            Session.Wiki.CurrentConfirmation = Nothing

            Dim create As New CreateAccount(Session, NewUser, Confirmation)

            App.UserWaitForProcess(create)
            If create.IsErrored Then App.ShowError(create.Result)
            If create.IsFailed Then Return

            DialogResult = DialogResult.OK
            Close()
        End Sub

        Private Sub Username_TextChanged() Handles Username.TextChanged
            CheckTimer.Stop()
            Status = CheckStatus.None

            If Username.Text.Length > 0 Then
                Dim name As String = UserCollection.SanitizeName(Username.Text)

                If name Is Nothing Then
                    Status = CheckStatus.Invalid
                ElseIf CheckResults.ContainsKey(name) Then
                    Status = CheckResults(name)
                Else
                    'Check title blacklists
                    If Session.Wiki.Family IsNot Nothing AndAlso Session.Wiki.Family.GlobalTitleBlacklist IsNot Nothing _
                        AndAlso Session.Wiki.Family.GlobalTitleBlacklist.IsMatch _
                        (Session, name, TitleListAction.CreateAccount) Then

                        Status = CheckStatus.GlobalBlacklisted

                    ElseIf Session.Wiki.TitleList IsNot Nothing _
                        AndAlso Session.Wiki.TitleList.IsMatch _
                        (Session, name, TitleListAction.CreateAccount) Then

                        Status = CheckStatus.LocalBlacklisted
                    End If

                    If Status = CheckStatus.None Then CheckTimer.Start()
                End If
            End If

            UpdateStatus()
        End Sub

        Private Sub Username_LostFocus() Handles Username.LostFocus
            If Username.Text.Length > 0 Then
                Dim name As String = UserCollection.SanitizeName(Username.Text)

                If name Is Nothing Then
                    Status = CheckStatus.Invalid
                ElseIf CheckResults.ContainsKey(name) Then
                    Status = CheckResults(name)
                Else
                    DoCheck()
                End If
            End If
        End Sub

        Private Sub DoCheck() Handles CheckTimer.Tick
            CheckTimer.Stop()
            Status = CheckStatus.Checking
            Indicator.Start()
            CurrentQuery = New UsernameCheckQuery(Session, Username.Text)
            AddHandler CurrentQuery.Complete, AddressOf CheckDone
            CreateThread(AddressOf CurrentQuery.Start)
            UpdateStatus()
        End Sub

        Private Sub CheckDone(ByVal sender As Object, ByVal e As EventArgs(Of Process))
            Indicator.Stop()

            If e.Value Is CurrentQuery Then
                Status = CType(e.Value, UsernameCheckQuery).Status
                UpdateStatus()
                If Status <> CheckStatus.Error Then CheckResults.Merge(UserCollection.SanitizeName(Username.Text), Status)
                InputChanged()
            End If
        End Sub

        Private Sub UpdateStatus()
            Select Case Status
                Case CheckStatus.Invalid
                    CheckStatusDisplay.Text = Msg("usernamecheck-invalid")
                    Indicator.BackgroundImage = Resources.mini_no

                Case CheckStatus.LocalBlacklisted
                    CheckStatusDisplay.Text = Msg("usernamecheck-localblacklisted")
                    Indicator.BackgroundImage = Resources.mini_no

                Case CheckStatus.GlobalBlacklisted
                    CheckStatusDisplay.Text = Msg("usernamecheck-globalblacklisted")
                    Indicator.BackgroundImage = Resources.mini_no

                Case CheckStatus.OK
                    CheckStatusDisplay.Text = Msg("usernamecheck-ok")
                    Indicator.BackgroundImage = Resources.mini_yes

                Case CheckStatus.Used
                    CheckStatusDisplay.Text = Msg("usernamecheck-used")
                    Indicator.BackgroundImage = Resources.mini_no

                Case CheckStatus.Error
                    CheckStatusDisplay.Text = Msg("usernamecheck-error")
                    Indicator.BackgroundImage = Resources.mini_question

                Case CheckStatus.Checking
                    CheckStatusDisplay.Text = Msg("usernamecheck-progress")
                    Indicator.BackgroundImage = Nothing

                Case CheckStatus.None
                    CheckStatusDisplay.Text = ""
                    Indicator.BackgroundImage = Nothing
            End Select

            InputChanged()
        End Sub

        Private Sub InputChanged() Handles ConfirmInput.TextChanged, Password.TextChanged, RetypePassword.TextChanged
            OK.Enabled = (Status = CheckStatus.None OrElse Status = CheckStatus.OK OrElse Status = CheckStatus.Checking) _
                AndAlso Username.Text.Length > 0 AndAlso Password.Text.Length > 0 _
                AndAlso RetypePassword.Text = Password.Text _
                AndAlso (Not ConfirmInput.Visible OrElse ConfirmInput.Text.Length > 0)
        End Sub

        Private Sub GetConfirmation()
            If Session.Wiki.AccountConfirmation Then
                If Session.Wiki.CurrentConfirmation Is Nothing Then
                    Dim creationCheck As New PreCreateAccount(Session.Wiki)

                    App.UserWaitForProcess(creationCheck)

                    If creationCheck.IsFailed Then
                        DialogResult = DialogResult.Cancel
                        Close()
                        Return
                    End If

                    Session.Wiki.CurrentConfirmation = creationCheck.Confirmation
                End If
            End If

            Confirmation = Session.Wiki.CurrentConfirmation

            If Confirmation Is Nothing OrElse Confirmation.Answer IsNot Nothing Then
                'Hide confirmation controls and resize/reposition form
                ConfirmImage.Visible = False
                ConfirmInput.Visible = False
                ConfirmLabel.Visible = False
                ConfirmRefresh.Visible = False

                Dim delta As Integer = (OK.Top - CheckStatusDisplay.Bottom - 6)
                Height -= delta
                Top += delta \ 2

            Else
                'Fit confirmation code to form
                ConfirmImage.Image = Confirmation.Image
                Width = 300 + Math.Max(0, ConfirmImage.Image.Width - ConfirmImage.Width)
                ConfirmRefresh.Left = ConfirmImage.Left + (ConfirmImage.Width \ 2) _
                    + (ConfirmImage.Image.Width \ 2) - ConfirmRefresh.Width
            End If
        End Sub

    End Class

End Namespace