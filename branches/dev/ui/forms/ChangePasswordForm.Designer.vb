<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class ChangePasswordForm
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.Cancel = New System.Windows.Forms.Button
        Me.OK = New System.Windows.Forms.Button
        Me.OldPasswordLabel = New System.Windows.Forms.Label
        Me.NewPasswordLabel = New System.Windows.Forms.Label
        Me.RetypePasswordLabel = New System.Windows.Forms.Label
        Me.OldPassword = New System.Windows.Forms.TextBox
        Me.NewPassword = New System.Windows.Forms.TextBox
        Me.RetypePassword = New System.Windows.Forms.TextBox
        Me.AccountLabel = New System.Windows.Forms.Label
        Me.AccountDisplay = New System.Windows.Forms.Label
        Me.SuspendLayout()
        '
        'Cancel
        '
        Me.Cancel.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Cancel.Location = New System.Drawing.Point(227, 117)
        Me.Cancel.Name = "CancelButton"
        Me.Cancel.Size = New System.Drawing.Size(75, 23)
        Me.Cancel.TabIndex = 0
        Me.Cancel.Text = "Cancel"
        Me.Cancel.UseVisualStyleBackColor = True
        '
        'OK
        '
        Me.OK.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.OK.Location = New System.Drawing.Point(146, 117)
        Me.OK.Name = "OK"
        Me.OK.Size = New System.Drawing.Size(75, 23)
        Me.OK.TabIndex = 0
        Me.OK.Text = "OK"
        Me.OK.UseVisualStyleBackColor = True
        '
        'OldPasswordLabel
        '
        Me.OldPasswordLabel.AutoSize = True
        Me.OldPasswordLabel.Location = New System.Drawing.Point(53, 37)
        Me.OldPasswordLabel.Name = "OldPasswordLabel"
        Me.OldPasswordLabel.Size = New System.Drawing.Size(74, 13)
        Me.OldPasswordLabel.TabIndex = 1
        Me.OldPasswordLabel.Text = "Old password:"
        '
        'NewPasswordLabel
        '
        Me.NewPasswordLabel.AutoSize = True
        Me.NewPasswordLabel.Location = New System.Drawing.Point(47, 63)
        Me.NewPasswordLabel.Name = "NewPasswordLabel"
        Me.NewPasswordLabel.Size = New System.Drawing.Size(80, 13)
        Me.NewPasswordLabel.TabIndex = 1
        Me.NewPasswordLabel.Text = "New password:"
        '
        'RetypePasswordLabel
        '
        Me.RetypePasswordLabel.AutoSize = True
        Me.RetypePasswordLabel.Location = New System.Drawing.Point(12, 89)
        Me.RetypePasswordLabel.Name = "RetypePasswordLabel"
        Me.RetypePasswordLabel.Size = New System.Drawing.Size(115, 13)
        Me.RetypePasswordLabel.TabIndex = 2
        Me.RetypePasswordLabel.Text = "Retype new password:"
        '
        'OldPassword
        '
        Me.OldPassword.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.OldPassword.Location = New System.Drawing.Point(129, 34)
        Me.OldPassword.Name = "OldPassword"
        Me.OldPassword.Size = New System.Drawing.Size(173, 20)
        Me.OldPassword.TabIndex = 3
        '
        'NewPassword
        '
        Me.NewPassword.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.NewPassword.Location = New System.Drawing.Point(129, 60)
        Me.NewPassword.Name = "NewPassword"
        Me.NewPassword.Size = New System.Drawing.Size(173, 20)
        Me.NewPassword.TabIndex = 4
        '
        'RetypePassword
        '
        Me.RetypePassword.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.RetypePassword.Location = New System.Drawing.Point(129, 86)
        Me.RetypePassword.Name = "RetypePassword"
        Me.RetypePassword.Size = New System.Drawing.Size(173, 20)
        Me.RetypePassword.TabIndex = 5
        '
        'AccountLabel
        '
        Me.AccountLabel.AutoSize = True
        Me.AccountLabel.Location = New System.Drawing.Point(77, 13)
        Me.AccountLabel.Name = "AccountLabel"
        Me.AccountLabel.Size = New System.Drawing.Size(50, 13)
        Me.AccountLabel.TabIndex = 6
        Me.AccountLabel.Text = "Account:"
        '
        'AccountDisplay
        '
        Me.AccountDisplay.AutoSize = True
        Me.AccountDisplay.Location = New System.Drawing.Point(126, 13)
        Me.AccountDisplay.Name = "AccountDisplay"
        Me.AccountDisplay.Size = New System.Drawing.Size(45, 13)
        Me.AccountDisplay.TabIndex = 7
        Me.AccountDisplay.Text = "<name>"
        '
        'ChangePasswordForm
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(314, 152)
        Me.Controls.Add(Me.AccountDisplay)
        Me.Controls.Add(Me.AccountLabel)
        Me.Controls.Add(Me.RetypePassword)
        Me.Controls.Add(Me.NewPassword)
        Me.Controls.Add(Me.OldPassword)
        Me.Controls.Add(Me.RetypePasswordLabel)
        Me.Controls.Add(Me.NewPasswordLabel)
        Me.Controls.Add(Me.OldPasswordLabel)
        Me.Controls.Add(Me.OK)
        Me.Controls.Add(Me.Cancel)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.Name = "ChangePasswordForm"
        Me.Text = "Change password"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Private WithEvents Cancel As System.Windows.Forms.Button
    Private WithEvents OK As System.Windows.Forms.Button
    Private WithEvents OldPasswordLabel As System.Windows.Forms.Label
    Private WithEvents NewPasswordLabel As System.Windows.Forms.Label
    Private WithEvents RetypePasswordLabel As System.Windows.Forms.Label
    Private WithEvents AccountLabel As System.Windows.Forms.Label
    Private WithEvents OldPassword As System.Windows.Forms.TextBox
    Private WithEvents NewPassword As System.Windows.Forms.TextBox
    Private WithEvents RetypePassword As System.Windows.Forms.TextBox
    Private WithEvents AccountDisplay As System.Windows.Forms.Label
End Class
