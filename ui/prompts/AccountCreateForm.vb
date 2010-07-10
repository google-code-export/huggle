Imports Huggle
Imports Huggle.Actions
Imports System.Collections.Generic
Imports System.Drawing
Imports System.IO
Imports System.Windows.Forms

Public Class AccountCreateForm

    Private _NewUser As User
    Private _Session As Session

    Private CheckResults As New Dictionary(Of String, CheckStatus)
    Private Confirmation As Image
    Private CurrentQuery As UsernameCheckQuery
    Private Status As CheckStatus

    Private WithEvents CheckTimer As New Timer With {.Interval = 800}

    Public Sub New(ByVal session As Session, ByVal confirmation As Image)
        InitializeComponent()
        Me.Confirmation = confirmation
        _Session = session
    End Sub

    Public ReadOnly Property NewUser() As User
        Get
            Return _NewUser
        End Get
    End Property

    Public ReadOnly Property Session() As Session
        Get
            Return _Session
        End Get
    End Property

    Public ReadOnly Property Wiki() As Wiki
        Get
            Return Session.Wiki
        End Get
    End Property

    Private Sub _Load() Handles Me.Load
        Icon = Resources.Icon
        WikiDisplay.Text = Wiki.Name

        If Wiki.CreationCheck Is Nothing Then Wiki.CreationCheck = New PreCreateAccount(Wiki)

        If Wiki.AccountConfirmation Then
            App.UserWaitForProcess(Wiki.CreationCheck)

            If Wiki.CreationCheck.IsErrored Then App.ShowError(Wiki.CreationCheck.Result)

            If Wiki.CreationCheck.IsFailed Then
                Wiki.CreationCheck = Nothing
                DialogResult = DialogResult.Cancel
                Close()
                Return
            End If
        End If

        If confirmation Is Nothing Then
            ConfirmationImage.Visible = False
            ConfirmationInput.Visible = False
            ConfirmationLabel.Visible = False
            Height -= (OK.Top - CheckStatusDisplay.Bottom - 6)
        Else
            ConfirmationImage.Image = confirmation
            If ConfirmationImage.Width < ConfirmationImage.Image.Width _
                Then Width += (ConfirmationImage.Image.Width - ConfirmationImage.Width)
        End If

        CheckStatusDisplay.Text = ""
    End Sub

    Private Sub Cancel_Click() Handles Cancel.Click
        DialogResult = DialogResult.Cancel
        Close()
    End Sub

    Private Sub OK_Click() Handles OK.Click
        _NewUser = Session.Wiki.Users.FromString(UsernameInput.Text)
        NewUser.Password = Scramble(PasswordInput.Text, Hash(NewUser))
        Wiki.CreationCheck.ConfirmAnswer = ConfirmationInput.Text

        Dim create As New CreateAccount(Session, newUser)
        App.UserWaitForProcess(create)
        If create.IsErrored Then App.ShowError(create.Result)
        If create.IsCancelled Then Return

        DialogResult = DialogResult.OK
        Close()
    End Sub

    Private Sub Username_TextChanged() Handles UsernameInput.TextChanged
        CheckTimer.Stop()
        Status = CheckStatus.None

        If UsernameInput.Text.Length > 0 Then
            Dim name As String = UserCollection.SanitizeName(UsernameInput.Text)

            If name Is Nothing Then
                Status = CheckStatus.Invalid
            ElseIf CheckResults.ContainsKey(name) Then
                Status = CheckResults(name)
            Else
                CheckTimer.Start()
            End If
        End If

        UpdateStatus()
    End Sub

    Private Sub Username_LostFocus() Handles UsernameInput.LostFocus
        If UsernameInput.Text.Length > 0 Then
            Dim name As String = UserCollection.SanitizeName(UsernameInput.Text)

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
        CurrentQuery = New UsernameCheckQuery(Session, UsernameInput.Text)
        AddHandler CurrentQuery.Complete, AddressOf CheckDone
        CurrentQuery.Start()
        UpdateStatus()
    End Sub

    Private Sub CheckDone(ByVal sender As Process)
        Indicator.Stop()

        If sender Is CurrentQuery Then
            Status = CType(sender, UsernameCheckQuery).Status
            UpdateStatus()
            If Status <> CheckStatus.Error Then CheckResults.Merge(UserCollection.SanitizeName(UsernameInput.Text), Status)
            InputChanged()
        End If
    End Sub

    Private Sub UpdateStatus()
        Select Case Status
            Case CheckStatus.Invalid
                CheckStatusDisplay.Text = Msg("usernamecheck-invalid")
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
    End Sub

    Private Sub InputChanged() Handles ConfirmationInput.TextChanged, PasswordInput.TextChanged, RetypePassword.TextChanged
        OK.Enabled = (Status = CheckStatus.None OrElse Status = CheckStatus.OK OrElse Status = CheckStatus.Checking) _
            AndAlso UsernameInput.Text.Length > 0 AndAlso PasswordInput.Text.Length > 0 _
            AndAlso RetypePassword.Text = PasswordInput.Text _
            AndAlso (Not ConfirmationInput.Visible OrElse ConfirmationInput.Text.Length > 0)
    End Sub

End Class