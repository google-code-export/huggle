Imports Huggle.Actions
Imports System.Windows.Forms

Namespace Huggle.UI

    Public Class ChangePasswordForm : Inherits HuggleForm

        Private Session As Session

        Public Sub New(ByVal session As Session)
            InitializeComponent()
            Me.Session = session
        End Sub

        Private Sub _Load()
            AccountDisplay.Text = Session.User.FullName
        End Sub

        Private Sub CancelButton_Click() Handles Cancel.Click
            DialogResult = DialogResult.Cancel
            Close()
        End Sub

        Private Sub Input_TextChanged() Handles OldPassword.TextChanged, NewPassword.TextChanged, RetypePassword.TextChanged
            OK.Enabled = (OldPassword.Text <> "" AndAlso NewPassword.Text <> "" AndAlso RetypePassword.Text <> "")
        End Sub

        Private Sub OK_Click() Handles OK.Click
            If NewPassword.Text <> RetypePassword.Text Then App.ShowError(New Result(Msg("changepassword-mismatch"))) : Return

            Dim process As New ChangePassword(Session, Scramble(NewPassword.Text, Hash(Session.User)))
            App.UserWaitForProcess(process)
            If process.IsErrored Then App.ShowError(process.Result)

            If process.IsSuccess Then
                DialogResult = DialogResult.OK
                Close()
            End If
        End Sub

    End Class

End Namespace