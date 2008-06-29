Class RevertAndWarnForm

    Public User As User

    Private Sub WarningForm_Load(ByVal s As Object, ByVal e As EventArgs) Handles Me.Load
        Text = "Revert and warn " & User.Name

        Summary.Items.AddRange(ManualRevertSummaries.ToArray)

        WarnLog.Columns.Add("", 300)
        WarnLog.Items.Add("Retrieving warnings, please wait...")
        WarnType.Items.AddRange(Config.WarningSeries.ToArray)
        If WarnType.Items.Count > 0 Then WarnType.SelectedIndex = 0
        WarnType.Focus()

        Level2.Visible = (Config.WarningMode <> "1")
        Level3.Visible = (Config.WarningMode <> "1" AndAlso Config.WarningMode <> "2")
        LevelFinal.Visible = (Config.WarningMode <> "1" AndAlso Config.WarningMode <> "2" _
            AndAlso Config.WarningMode <> "3")

        Dim NewWarnLogRequest As New WarningLogRequest
        NewWarnLogRequest.Target = WarnLog
        NewWarnLogRequest.ThisUser = User
        NewWarnLogRequest.Start()
    End Sub

    Private Sub Close_Click(ByVal s As Object, ByVal e As EventArgs) Handles Cancel.Click
        DialogResult = DialogResult.Cancel
        Close()
    End Sub

    Private Sub OK_Click(ByVal s As Object, ByVal e As EventArgs) Handles OK.Click
        Dim Level As UserL

        If LevelAuto.Checked Then Level = UserL.None
        If Level1.Checked Then Level = UserL.Warn1
        If Level2.Checked Then Level = UserL.Warn2
        If Level3.Checked Then Level = UserL.Warn3
        If LevelFinal.Checked Then Level = UserL.WarnFinal

        Main.RevertAndWarn(WarnType.Text, Level)
        DialogResult = DialogResult.OK
        Close()
    End Sub

    Private Sub WarningForm_FormClosing(ByVal s As Object, ByVal e As FormClosingEventArgs) Handles Me.FormClosing
        If DialogResult <> DialogResult.OK Then DialogResult = DialogResult.Cancel
    End Sub

    Private Sub WarningForm_KeyDown(ByVal s As Object, ByVal e As KeyEventArgs) Handles Me.KeyDown
        Select Case e.KeyCode
            Case Keys.Escape : Close_Click(Nothing, Nothing)
            Case Keys.Enter : OK_Click(Nothing, Nothing)
        End Select
    End Sub

End Class