<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class AccountCreateForm
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
        Me.Label1 = New System.Windows.Forms.Label
        Me.Label2 = New System.Windows.Forms.Label
        Me.UsernameInput = New System.Windows.Forms.TextBox
        Me.PasswordInput = New System.Windows.Forms.TextBox
        Me.OK = New System.Windows.Forms.Button
        Me.Cancel = New System.Windows.Forms.Button
        Me.ConfirmationImage = New System.Windows.Forms.PictureBox
        Me.ConfirmationInput = New System.Windows.Forms.TextBox
        Me.ConfirmationLabel = New System.Windows.Forms.Label
        Me.WikiLabel = New System.Windows.Forms.Label
        Me.WikiDisplay = New System.Windows.Forms.Label
        Me.RetypePasswordLabel = New System.Windows.Forms.Label
        Me.RetypePassword = New System.Windows.Forms.TextBox
        Me.CheckStatusDisplay = New System.Windows.Forms.Label
        Me.Indicator = New Huggle.Controls.ActivityIndicator
        CType(Me.ConfirmationImage, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(50, 34)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(58, 13)
        Me.Label1.TabIndex = 2
        Me.Label1.Text = "Username:"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(52, 60)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(56, 13)
        Me.Label2.TabIndex = 4
        Me.Label2.Text = "Password:"
        '
        'UsernameInput
        '
        Me.UsernameInput.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.UsernameInput.Location = New System.Drawing.Point(108, 31)
        Me.UsernameInput.Name = "UsernameInput"
        Me.UsernameInput.Size = New System.Drawing.Size(174, 20)
        Me.UsernameInput.TabIndex = 3
        '
        'PasswordInput
        '
        Me.PasswordInput.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.PasswordInput.Location = New System.Drawing.Point(108, 57)
        Me.PasswordInput.Name = "PasswordInput"
        Me.PasswordInput.PasswordChar = Global.Microsoft.VisualBasic.ChrW(9679)
        Me.PasswordInput.Size = New System.Drawing.Size(174, 20)
        Me.PasswordInput.TabIndex = 5
        '
        'OK
        '
        Me.OK.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.OK.Enabled = False
        Me.OK.Location = New System.Drawing.Point(126, 221)
        Me.OK.Name = "OK"
        Me.OK.Size = New System.Drawing.Size(75, 23)
        Me.OK.TabIndex = 12
        Me.OK.Text = "OK"
        Me.OK.UseVisualStyleBackColor = True
        '
        'Cancel
        '
        Me.Cancel.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Cancel.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.Cancel.Location = New System.Drawing.Point(207, 221)
        Me.Cancel.Name = "Cancel"
        Me.Cancel.Size = New System.Drawing.Size(75, 23)
        Me.Cancel.TabIndex = 13
        Me.Cancel.Text = "Cancel"
        Me.Cancel.UseVisualStyleBackColor = True
        '
        'ConfirmationImage
        '
        Me.ConfirmationImage.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ConfirmationImage.BackColor = System.Drawing.SystemColors.Control
        Me.ConfirmationImage.Location = New System.Drawing.Point(12, 128)
        Me.ConfirmationImage.Name = "ConfirmationImage"
        Me.ConfirmationImage.Size = New System.Drawing.Size(270, 61)
        Me.ConfirmationImage.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage
        Me.ConfirmationImage.TabIndex = 3
        Me.ConfirmationImage.TabStop = False
        '
        'ConfirmationInput
        '
        Me.ConfirmationInput.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ConfirmationInput.Location = New System.Drawing.Point(108, 195)
        Me.ConfirmationInput.Name = "ConfirmationInput"
        Me.ConfirmationInput.Size = New System.Drawing.Size(174, 20)
        Me.ConfirmationInput.TabIndex = 11
        '
        'ConfirmationLabel
        '
        Me.ConfirmationLabel.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.ConfirmationLabel.AutoSize = True
        Me.ConfirmationLabel.Location = New System.Drawing.Point(12, 198)
        Me.ConfirmationLabel.Name = "ConfirmationLabel"
        Me.ConfirmationLabel.Size = New System.Drawing.Size(95, 13)
        Me.ConfirmationLabel.TabIndex = 10
        Me.ConfirmationLabel.Text = "Confirmation code:"
        '
        'WikiLabel
        '
        Me.WikiLabel.AutoSize = True
        Me.WikiLabel.Location = New System.Drawing.Point(77, 9)
        Me.WikiLabel.Name = "WikiLabel"
        Me.WikiLabel.Size = New System.Drawing.Size(31, 13)
        Me.WikiLabel.TabIndex = 0
        Me.WikiLabel.Text = "Wiki:"
        '
        'WikiDisplay
        '
        Me.WikiDisplay.AutoSize = True
        Me.WikiDisplay.Location = New System.Drawing.Point(108, 9)
        Me.WikiDisplay.Name = "WikiDisplay"
        Me.WikiDisplay.Size = New System.Drawing.Size(97, 13)
        Me.WikiDisplay.TabIndex = 1
        Me.WikiDisplay.Text = "Wikipedia (English)"
        '
        'RetypePasswordLabel
        '
        Me.RetypePasswordLabel.AutoSize = True
        Me.RetypePasswordLabel.Location = New System.Drawing.Point(15, 86)
        Me.RetypePasswordLabel.Name = "RetypePasswordLabel"
        Me.RetypePasswordLabel.Size = New System.Drawing.Size(92, 13)
        Me.RetypePasswordLabel.TabIndex = 6
        Me.RetypePasswordLabel.Text = "Retype password:"
        '
        'RetypePassword
        '
        Me.RetypePassword.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.RetypePassword.Location = New System.Drawing.Point(108, 83)
        Me.RetypePassword.Name = "RetypePassword"
        Me.RetypePassword.PasswordChar = Global.Microsoft.VisualBasic.ChrW(9679)
        Me.RetypePassword.Size = New System.Drawing.Size(174, 20)
        Me.RetypePassword.TabIndex = 7
        '
        'CheckStatusDisplay
        '
        Me.CheckStatusDisplay.AutoSize = True
        Me.CheckStatusDisplay.Location = New System.Drawing.Point(126, 107)
        Me.CheckStatusDisplay.Name = "CheckStatusDisplay"
        Me.CheckStatusDisplay.Size = New System.Drawing.Size(123, 13)
        Me.CheckStatusDisplay.TabIndex = 9
        Me.CheckStatusDisplay.Text = "Username already in use"
        '
        'Indicator
        '
        Me.Indicator.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center
        Me.Indicator.Location = New System.Drawing.Point(108, 106)
        Me.Indicator.MaximumSize = New System.Drawing.Size(16, 16)
        Me.Indicator.MinimumSize = New System.Drawing.Size(16, 16)
        Me.Indicator.Name = "Indicator"
        Me.Indicator.Size = New System.Drawing.Size(16, 16)
        Me.Indicator.TabIndex = 8
        Me.Indicator.TabStop = False
        '
        'AccountCreateForm
        '
        Me.AcceptButton = Me.OK
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.CancelButton = Me.Cancel
        Me.ClientSize = New System.Drawing.Size(294, 256)
        Me.Controls.Add(Me.Indicator)
        Me.Controls.Add(Me.CheckStatusDisplay)
        Me.Controls.Add(Me.WikiDisplay)
        Me.Controls.Add(Me.ConfirmationLabel)
        Me.Controls.Add(Me.ConfirmationInput)
        Me.Controls.Add(Me.ConfirmationImage)
        Me.Controls.Add(Me.Cancel)
        Me.Controls.Add(Me.OK)
        Me.Controls.Add(Me.RetypePassword)
        Me.Controls.Add(Me.PasswordInput)
        Me.Controls.Add(Me.RetypePasswordLabel)
        Me.Controls.Add(Me.UsernameInput)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.WikiLabel)
        Me.Controls.Add(Me.Label1)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.MaximizeBox = False
        Me.Name = "AccountCreateForm"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Create account"
        CType(Me.ConfirmationImage, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Private WithEvents OK As System.Windows.Forms.Button
    Private WithEvents Cancel As System.Windows.Forms.Button
    Private WithEvents Label1 As System.Windows.Forms.Label
    Private WithEvents Label2 As System.Windows.Forms.Label
    Private WithEvents UsernameInput As System.Windows.Forms.TextBox
    Private WithEvents PasswordInput As System.Windows.Forms.TextBox
    Private WithEvents ConfirmationImage As System.Windows.Forms.PictureBox
    Private WithEvents ConfirmationInput As System.Windows.Forms.TextBox
    Private WithEvents ConfirmationLabel As System.Windows.Forms.Label
    Private WithEvents WikiLabel As System.Windows.Forms.Label
    Private WithEvents WikiDisplay As System.Windows.Forms.Label
    Private WithEvents RetypePasswordLabel As System.Windows.Forms.Label
    Private WithEvents RetypePassword As System.Windows.Forms.TextBox
    Private WithEvents CheckStatusDisplay As System.Windows.Forms.Label
    Private WithEvents Indicator As Huggle.Controls.ActivityIndicator
End Class
