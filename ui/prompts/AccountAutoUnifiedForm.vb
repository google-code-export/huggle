Imports System
Imports System.Windows.Forms

Namespace Huggle.UI

    Friend Class AccountAutoUnifiedForm : Inherits HuggleForm

        Private Requester As String
        Private User As User

        Public Sub New(ByVal user As User, ByVal requester As String)
            ThrowNull(user, "user")
            Me.Requester = requester
            Me.User = user

            InitializeComponent()
        End Sub

        Private Sub _FormClosing() Handles Me.FormClosing
            If DialogResult = DialogResult.OK Then
                User.GlobalUser.Config.AutoUnifiedLogin = AutoUnifiedLogin.Checked
                User.GlobalUser.Config.SaveLocal()
            End If
        End Sub

        Private Sub _Load() Handles Me.Load
            Icon = Resources.Icon
            Request.Text = Request.Text.FormatForUser(Requester, User.FullName, User.GlobalUser.FullName)
        End Sub

    End Class

End Namespace