Imports Huggle.Actions
Imports System
Imports System.Collections.Generic
Imports System.Drawing
Imports System.Windows.Forms

Namespace Huggle.UI

    Friend Class MainForm : Inherits HuggleForm

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
            Dim form As New AccountPropertiesForm(Session)
            form.Show()
        End Sub

        Private Shared Sub HelpAbout_Click() Handles HelpAbout.Click
            Using form As New AboutForm
                form.ShowDialog()
            End Using
        End Sub

        Private Shared Sub HelpManual_Click() Handles HelpManual.Click
            OpenWebBrowser(InternalConfig.ManualUrl)
        End Sub

        Private Sub AccountGlobalProperties_Click() Handles AccountGlobalProperties.Click
            If Session.User.IsUnified Then
                Dim form As New GlobalUserPropertiesForm(Session)
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

        Private Shared Sub ClearCloudToolStripMenuItem_Click() Handles ClearCloudToolStripMenuItem.Click
            Dim req As New CloudStore("global", "-")
            req.Start()
        End Sub

        Public Overrides Function GetKey() As String
            Return "main"
        End Function

        Public Overrides Function GetState() As Dictionary(Of String, Object)
            Dim result As New Dictionary(Of String, Object)

            result.Add("location", Location.ToString)
            result.Add("size", Size.ToString)
            result.Add("maximized", (WindowState = FormWindowState.Maximized))

            Return result
        End Function

        Private Sub RevisionsToolStripMenuItem_Click() Handles WikiRevisions.Click
            Dim form As New ContainerForm(New RevisionView(Session))
            form.Show()
        End Sub
        
    End Class

End Namespace