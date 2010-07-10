<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class AccountLoginForm
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
        Me.Request = New System.Windows.Forms.Label()
        Me.Cancel = New System.Windows.Forms.Button()
        Me.OK = New System.Windows.Forms.Button()
        Me.RememberPassword = New System.Windows.Forms.CheckBox()
        Me.Password = New System.Windows.Forms.TextBox()
        Me.PasswordLabel = New System.Windows.Forms.Label()
        Me.SuspendLayout()
        '
        'Request
        '
        Me.Request.AutoSize = True
        Me.Request.Location = New System.Drawing.Point(12, 9)
        Me.Request.Name = "Request"
        Me.Request.Size = New System.Drawing.Size(212, 13)
        Me.Request.TabIndex = 1
        Me.Request.Text = "{0} is requesting access to the account {1}."
        '
        'Cancel
        '
        Me.Cancel.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Cancel.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.Cancel.Location = New System.Drawing.Point(263, 81)
        Me.Cancel.Name = "Cancel"
        Me.Cancel.Size = New System.Drawing.Size(75, 23)
        Me.Cancel.TabIndex = 2
        Me.Cancel.Text = "Deny"
        Me.Cancel.UseVisualStyleBackColor = True
        '
        'OK
        '
        Me.OK.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.OK.DialogResult = System.Windows.Forms.DialogResult.OK
        Me.OK.Location = New System.Drawing.Point(182, 81)
        Me.OK.Name = "OK"
        Me.OK.Size = New System.Drawing.Size(75, 23)
        Me.OK.TabIndex = 2
        Me.OK.Text = "Allow"
        Me.OK.UseVisualStyleBackColor = True
        '
        'RememberPassword
        '
        Me.RememberPassword.AutoSize = True
        Me.RememberPassword.Location = New System.Drawing.Point(69, 60)
        Me.RememberPassword.Name = "RememberPassword"
        Me.RememberPassword.Size = New System.Drawing.Size(125, 17)
        Me.RememberPassword.TabIndex = 3
        Me.RememberPassword.Text = "Remember password"
        Me.RememberPassword.UseVisualStyleBackColor = True
        '
        'Password
        '
        Me.Password.Location = New System.Drawing.Point(69, 34)
        Me.Password.Name = "Password"
        Me.Password.PasswordChar = Global.Microsoft.VisualBasic.ChrW(9679)
        Me.Password.Size = New System.Drawing.Size(172, 20)
        Me.Password.TabIndex = 4
        '
        'PasswordLabel
        '
        Me.PasswordLabel.AutoSize = True
        Me.PasswordLabel.Location = New System.Drawing.Point(12, 37)
        Me.PasswordLabel.Name = "PasswordLabel"
        Me.PasswordLabel.Size = New System.Drawing.Size(56, 13)
        Me.PasswordLabel.TabIndex = 5
        Me.PasswordLabel.Text = "Password:"
        '
        'AccountLoginForm
        '
        Me.AcceptButton = Me.OK
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.CancelButton = Me.Cancel
        Me.ClientSize = New System.Drawing.Size(350, 116)
        Me.Controls.Add(Me.PasswordLabel)
        Me.Controls.Add(Me.Password)
        Me.Controls.Add(Me.RememberPassword)
        Me.Controls.Add(Me.OK)
        Me.Controls.Add(Me.Cancel)
        Me.Controls.Add(Me.Request)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "AccountLoginForm"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Text = "Huggle"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Private WithEvents Request As System.Windows.Forms.Label
    Friend WithEvents Cancel As System.Windows.Forms.Button
    Friend WithEvents RememberPassword As System.Windows.Forms.CheckBox
    Private WithEvents OK As System.Windows.Forms.Button
    Friend WithEvents Password As System.Windows.Forms.TextBox
    Private WithEvents PasswordLabel As System.Windows.Forms.Label
End Class
