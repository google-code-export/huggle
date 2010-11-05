Imports System
Imports System.Windows.Forms

Namespace Huggle.UI

    Friend Class AccountLoginForm : Inherits HuggleForm

        Private Requester As String
        Private User As User

        Friend Sub New(ByVal user As User, ByVal requester As String)
            InitializeComponent()
            Me.Requester = requester
            Me.User = user
        End Sub

        Private Sub _FormClosing() Handles Me.FormClosing
            If DialogResult = DialogResult.OK Then
                User.Password = Scramble(User.FullName, Password.Text, Hash(User))
                User.Config.RememberPassword = RememberPassword.Checked
            End If
        End Sub

        Private Sub _Load() Handles Me.Load
            Try
                Icon = Resources.Icon
                Text = Windows.Forms.Application.ProductName
                Request.Text = Request.Text.FormatForUser(Requester, User.FullName)

            Catch ex As SystemException
                App.ShowError(Result.FromException(ex))
                Close()
            End Try
        End Sub

    End Class

End Namespace