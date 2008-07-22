Class ProtectForm

    Public ThisPage As Page

    Private Sub ProtectForm_Load(ByVal s As Object, ByVal e As EventArgs) Handles Me.Load
        Text = "Protect " & ThisPage.Name
        Reason.Text = Config.ProtectionReason

        ProtectionLog.Columns.Add("", 300)
        ProtectionLog.Items.Add("Retrieving protection log, please wait...")

        Dim NewRequest As New ProtectionLogRequest
        NewRequest.Target = ProtectionLog
        NewRequest.ThisPage = ThisPage
        NewRequest.Start()
    End Sub

    Private Sub ProtectForm_KeyDown(ByVal s As Object, ByVal e As KeyEventArgs) Handles MyBase.KeyDown
        If e.KeyCode = Keys.Escape Then Close()
    End Sub

    Private Sub OK_Click(ByVal s As Object, ByVal e As EventArgs) Handles OK.Click
        Dim NewRequest As New ProtectRequest
        NewRequest.ThisPage = ThisPage
        NewRequest.Summary = Reason.Text
        If SemiProtection.Checked Then NewRequest.EditLevel = "autoconfirmed" _
            Else If FullProtection.Checked Then NewRequest.EditLevel = "sysop"
        If MoveProtection.Checked Then NewRequest.MoveLevel = "sysop"
        If NewRequest.EditLevel <> "sysop" AndAlso NewRequest.MoveLevel <> "sysop" _
            Then NewRequest.MoveLevel = NewRequest.EditLevel
        NewRequest.Expiry = Expiry.Text
        NewRequest.Start()

        Close()
    End Sub

    Private Sub Cancel_Click(ByVal s As Object, ByVal e As EventArgs) Handles Cancel.Click
        Close()
    End Sub

    Private Sub Reason_TextChanged(ByVal s As Object, ByVal e As EventArgs) Handles Reason.TextChanged
        OK.Enabled = (Reason.Text <> "")
    End Sub

    Private Sub ProtectionLog_EnabledChanged(ByVal s As Object, ByVal e As EventArgs) _
        Handles ProtectionLog.EnabledChanged

        CurrentLevel.Text = "Current protection level: "

        If ThisPage.EditLevel = "" AndAlso ThisPage.MoveLevel = "" Then CurrentLevel.Text &= "no protection"
        If ThisPage.EditLevel <> "" Then CurrentLevel.Text &= "edit:" & ThisPage.EditLevel & " "
        If ThisPage.MoveLevel <> "" Then CurrentLevel.Text &= "move:" & ThisPage.MoveLevel
    End Sub

    Private Sub SemiProtection_CheckedChanged(ByVal s As Object, ByVal e As EventArgs) _
        Handles SemiProtection.CheckedChanged

        If SemiProtection.Checked Then MoveProtection.Checked = False
    End Sub

    Private Sub FullProtection_CheckedChanged(ByVal s As Object, ByVal e As EventArgs) _
        Handles FullProtection.CheckedChanged

        If FullProtection.Checked Then MoveProtection.Checked = True
    End Sub

End Class