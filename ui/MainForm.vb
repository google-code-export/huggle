Imports Huggle.Actions
Imports System
Imports System.Drawing
Imports System.Windows.Forms

Namespace Huggle.UI

    Public Class MainForm

        Private Session As Session

        Public Sub New(ByVal session As Session)
            InitializeComponent()
            Me.Session = session
            Dim queryPanel As New QueryPanel(session)
            queryPanel.Dock = DockStyle.Fill
            QueueSplit.Panel2.Controls.Add(queryPanel)
        End Sub

        Private Sub _FormClosed() Handles Me.FormClosed
            If DialogResult <> DialogResult.OK Then DialogResult = DialogResult.Abort
        End Sub

        Private Sub _Load() Handles Me.Load
            Try
                Icon = Resources.Icon
                Text = Windows.Forms.Application.ProductName
                If Config.Local.WindowLocation <> Point.Empty Then Location = Config.Local.WindowLocation
                If Config.Local.WindowSize <> Size.Empty Then Size = Config.Local.WindowSize
                WindowState = If(Config.Local.WindowMaximized, FormWindowState.Maximized, FormWindowState.Normal)

                QueueSplit.Panel1.Controls.Add(New QueuePanel(Session.Wiki) With {.Dock = DockStyle.Fill})
                LogSplit.Panel2.Controls.Add(New LogPanel With {.Dock = DockStyle.Fill})

            Catch ex As SystemException
                App.ShowError(Result.FromException(ex))
                DialogResult = DialogResult.Abort
                Close()
            End Try
        End Sub

        Private Sub SystemExit_Click() Handles SystemExit.Click
            Close()
        End Sub

        Private Sub SystemLogout_Click() Handles SystemLogout.Click
            Config.Local.AutoLogin = False

            Dim logout As New Logout(Session)
            App.UserWaitForProcess(logout)
            If logout.IsFailed Then App.ShowError(logout.Result)

            DialogResult = DialogResult.OK
            Close()
        End Sub

        Private Sub WikiProperties_Click() Handles WikiProperties.Click
            Dim form As New WikiPropertiesForm(Session)
            form.Show()
        End Sub

        Private Sub AccountProperties_Click() Handles AccountProperties.Click
            Dim form As New AccountPropertiesForm(Session.User)
            form.Show()
        End Sub

        Private Sub HelpAbout_Click() Handles HelpAbout.Click
            Dim form As New AboutForm
            form.ShowDialog()
        End Sub

        Private Sub HelpManual_Click() Handles HelpManual.Click
            OpenWebBrowser(Config.Internal.ManualUrl)
        End Sub

        Private Sub AccountGlobalProperties_Click() Handles AccountGlobalProperties.Click
            If Session.User.GlobalUser IsNot Nothing Then
                Dim form As New GlobalUserPropertiesForm(Session.User.GlobalUser)
                form.Show()
            End If
        End Sub

        Private Sub WikiFamilyProperties_Click() Handles WikiFamilyProperties.Click
            If Session.Wiki.Family IsNot Nothing Then
                Dim form As New FamilyPropertiesForm(Session)
                form.Show()
            End If
        End Sub

        Private Sub UserChangeGroups_Click() Handles UserChangeGroups.Click
            Dim form As New UserGroupsForm(Session, Nothing)
            form.Show()
        End Sub

        Private Sub AssessPageToolStripMenuItem_Click() Handles AssessPageToolStripMenuItem.Click
            Dim form As New AssessForm(Session)
            form.Show()
        End Sub

    End Class

End Namespace