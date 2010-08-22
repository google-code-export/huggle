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
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Username = New System.Windows.Forms.TextBox()
        Me.Password = New System.Windows.Forms.TextBox()
        Me.OK = New System.Windows.Forms.Button()
        Me.Cancel = New System.Windows.Forms.Button()
        Me.ConfirmImage = New System.Windows.Forms.PictureBox()
        Me.ConfirmInput = New System.Windows.Forms.TextBox()
        Me.ConfirmLabel = New System.Windows.Forms.Label()
        Me.WikiLabel = New System.Windows.Forms.Label()
        Me.WikiDisplay = New System.Windows.Forms.Label()
        Me.RetypePasswordLabel = New System.Windows.Forms.Label()
        Me.RetypePassword = New System.Windows.Forms.TextBox()
        Me.CheckStatusDisplay = New System.Windows.Forms.Label()
        Me.ConfirmRefresh = New System.Windows.Forms.LinkLabel()
        Me.Indicator = New Huggle.Controls.WaitControl()
        CType(Me.ConfirmImage, System.ComponentModel.ISupportInitialize).BeginInit()
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
        'Username
        '
        Me.Username.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Username.Location = New System.Drawing.Point(108, 31)
        Me.Username.Name = "Username"
        Me.Username.Size = New System.Drawing.Size(174, 20)
        Me.Username.TabIndex = 3
        '
        'Password
        '
        Me.Password.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Password.Location = New System.Drawing.Point(108, 57)
        Me.Password.Name = "Password"
        Me.Password.PasswordChar = Global.Microsoft.VisualBasic.ChrW(9679)
        Me.Password.Size = New System.Drawing.Size(174, 20)
        Me.Password.TabIndex = 5
        '
        'OK
        '
        Me.OK.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.OK.Enabled = False
        Me.OK.Location = New System.Drawing.Point(126, 230)
        Me.OK.Name = "OK"
        Me.OK.Size = New System.Drawing.Size(75, 23)
        Me.OK.TabIndex = 13
        Me.OK.Text = "OK"
        Me.OK.UseVisualStyleBackColor = True
        '
        'Cancel
        '
        Me.Cancel.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Cancel.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.Cancel.Location = New System.Drawing.Point(207, 230)
        Me.Cancel.Name = "Cancel"
        Me.Cancel.Size = New System.Drawing.Size(75, 23)
        Me.Cancel.TabIndex = 14
        Me.Cancel.Text = "Cancel"
        Me.Cancel.UseVisualStyleBackColor = True
        '
        'ConfirmImage
        '
        Me.ConfirmImage.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ConfirmImage.BackColor = System.Drawing.SystemColors.Control
        Me.ConfirmImage.Location = New System.Drawing.Point(12, 128)
        Me.ConfirmImage.Name = "ConfirmImage"
        Me.ConfirmImage.Size = New System.Drawing.Size(270, 70)
        Me.ConfirmImage.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage
        Me.ConfirmImage.TabIndex = 3
        Me.ConfirmImage.TabStop = False
        '
        'ConfirmInput
        '
        Me.ConfirmInput.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ConfirmInput.Location = New System.Drawing.Point(108, 204)
        Me.ConfirmInput.Name = "ConfirmInput"
        Me.ConfirmInput.Size = New System.Drawing.Size(174, 20)
        Me.ConfirmInput.TabIndex = 12
        '
        'ConfirmLabel
        '
        Me.ConfirmLabel.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.ConfirmLabel.AutoSize = True
        Me.ConfirmLabel.Location = New System.Drawing.Point(12, 207)
        Me.ConfirmLabel.Name = "ConfirmLabel"
        Me.ConfirmLabel.Size = New System.Drawing.Size(95, 13)
        Me.ConfirmLabel.TabIndex = 11
        Me.ConfirmLabel.Text = "Confirmation code:"
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
        Me.CheckStatusDisplay.Size = New System.Drawing.Size(0, 13)
        Me.CheckStatusDisplay.TabIndex = 9
        '
        'ConfirmRefresh
        '
        Me.ConfirmRefresh.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ConfirmRefresh.AutoSize = True
        Me.ConfirmRefresh.BackColor = System.Drawing.Color.White
        Me.ConfirmRefresh.LinkBehavior = System.Windows.Forms.LinkBehavior.HoverUnderline
        Me.ConfirmRefresh.LinkColor = System.Drawing.SystemColors.HotTrack
        Me.ConfirmRefresh.Location = New System.Drawing.Point(238, 183)
        Me.ConfirmRefresh.Name = "ConfirmRefresh"
        Me.ConfirmRefresh.Size = New System.Drawing.Size(44, 13)
        Me.ConfirmRefresh.TabIndex = 10
        Me.ConfirmRefresh.TabStop = True
        Me.ConfirmRefresh.Text = "Refresh"
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
        Me.ClientSize = New System.Drawing.Size(294, 265)
        Me.Controls.Add(Me.ConfirmRefresh)
        Me.Controls.Add(Me.Indicator)
        Me.Controls.Add(Me.CheckStatusDisplay)
        Me.Controls.Add(Me.WikiDisplay)
        Me.Controls.Add(Me.ConfirmLabel)
        Me.Controls.Add(Me.ConfirmInput)
        Me.Controls.Add(Me.ConfirmImage)
        Me.Controls.Add(Me.Cancel)
        Me.Controls.Add(Me.OK)
        Me.Controls.Add(Me.RetypePassword)
        Me.Controls.Add(Me.Password)
        Me.Controls.Add(Me.RetypePasswordLabel)
        Me.Controls.Add(Me.Username)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.WikiLabel)
        Me.Controls.Add(Me.Label1)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.MaximizeBox = False
        Me.Name = "AccountCreateForm"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Create account"
        CType(Me.ConfirmImage, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Private WithEvents OK As System.Windows.Forms.Button
    Private WithEvents Cancel As System.Windows.Forms.Button
    Private WithEvents Label1 As System.Windows.Forms.Label
    Private WithEvents Label2 As System.Windows.Forms.Label
    Private WithEvents Username As System.Windows.Forms.TextBox
    Private WithEvents Password As System.Windows.Forms.TextBox
    Private WithEvents ConfirmImage As System.Windows.Forms.PictureBox
    Private WithEvents ConfirmInput As System.Windows.Forms.TextBox
    Private WithEvents ConfirmLabel As System.Windows.Forms.Label
    Private WithEvents WikiLabel As System.Windows.Forms.Label
    Private WithEvents WikiDisplay As System.Windows.Forms.Label
    Private WithEvents RetypePasswordLabel As System.Windows.Forms.Label
    Private WithEvents RetypePassword As System.Windows.Forms.TextBox
    Private WithEvents CheckStatusDisplay As System.Windows.Forms.Label
    Private WithEvents Indicator As Huggle.Controls.WaitControl
    Private WithEvents ConfirmRefresh As System.Windows.Forms.LinkLabel
End Class
