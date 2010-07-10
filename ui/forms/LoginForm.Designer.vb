<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class LoginForm
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
        Me.Login = New System.Windows.Forms.Button()
        Me.Cancel = New System.Windows.Forms.Button()
        Me.WikiSelector = New System.Windows.Forms.ComboBox()
        Me.WikiLabel = New System.Windows.Forms.Label()
        Me.PasswordLabel = New System.Windows.Forms.Label()
        Me.UsernameLabel = New System.Windows.Forms.Label()
        Me.Password = New System.Windows.Forms.TextBox()
        Me.Logo = New System.Windows.Forms.PictureBox()
        Me.Username = New System.Windows.Forms.ComboBox()
        Me.Secure = New System.Windows.Forms.CheckBox()
        Me.RememberMe = New System.Windows.Forms.CheckBox()
        CType(Me.Logo, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'Login
        '
        Me.Login.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Login.Location = New System.Drawing.Point(77, 217)
        Me.Login.Name = "Login"
        Me.Login.Size = New System.Drawing.Size(80, 23)
        Me.Login.TabIndex = 6
        Me.Login.Text = "Login"
        '
        'Cancel
        '
        Me.Cancel.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Cancel.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.Cancel.Location = New System.Drawing.Point(163, 217)
        Me.Cancel.Name = "Cancel"
        Me.Cancel.Size = New System.Drawing.Size(80, 23)
        Me.Cancel.TabIndex = 7
        Me.Cancel.Text = "Cancel"
        Me.Cancel.UseVisualStyleBackColor = True
        '
        'WikiSelector
        '
        Me.WikiSelector.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.WikiSelector.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.WikiSelector.DropDownWidth = 166
        Me.WikiSelector.FormattingEnabled = True
        Me.WikiSelector.IntegralHeight = False
        Me.WikiSelector.Location = New System.Drawing.Point(77, 90)
        Me.WikiSelector.MaxDropDownItems = 20
        Me.WikiSelector.Name = "WikiSelector"
        Me.WikiSelector.Size = New System.Drawing.Size(166, 21)
        Me.WikiSelector.TabIndex = 1
        '
        'WikiLabel
        '
        Me.WikiLabel.Location = New System.Drawing.Point(-3, 93)
        Me.WikiLabel.Name = "WikiLabel"
        Me.WikiLabel.Size = New System.Drawing.Size(76, 17)
        Me.WikiLabel.TabIndex = 0
        Me.WikiLabel.Text = "Wiki:"
        Me.WikiLabel.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'PasswordLabel
        '
        Me.PasswordLabel.Location = New System.Drawing.Point(-3, 146)
        Me.PasswordLabel.Name = "PasswordLabel"
        Me.PasswordLabel.Size = New System.Drawing.Size(76, 17)
        Me.PasswordLabel.TabIndex = 4
        Me.PasswordLabel.Text = "Password:"
        Me.PasswordLabel.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'UsernameLabel
        '
        Me.UsernameLabel.Location = New System.Drawing.Point(-3, 120)
        Me.UsernameLabel.Name = "UsernameLabel"
        Me.UsernameLabel.Size = New System.Drawing.Size(76, 17)
        Me.UsernameLabel.TabIndex = 2
        Me.UsernameLabel.Text = "Username:"
        Me.UsernameLabel.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Password
        '
        Me.Password.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Password.Location = New System.Drawing.Point(77, 143)
        Me.Password.MaxLength = 255
        Me.Password.Name = "Password"
        Me.Password.PasswordChar = Global.Microsoft.VisualBasic.ChrW(9679)
        Me.Password.Size = New System.Drawing.Size(166, 20)
        Me.Password.TabIndex = 5
        '
        'Logo
        '
        Me.Logo.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Logo.BackColor = System.Drawing.Color.Transparent
        Me.Logo.ErrorImage = Nothing
        Me.Logo.InitialImage = Nothing
        Me.Logo.Location = New System.Drawing.Point(0, 0)
        Me.Logo.Name = "Logo"
        Me.Logo.Size = New System.Drawing.Size(270, 80)
        Me.Logo.TabIndex = 14
        Me.Logo.TabStop = False
        '
        'Username
        '
        Me.Username.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Username.FormattingEnabled = True
        Me.Username.Location = New System.Drawing.Point(77, 117)
        Me.Username.MaxDropDownItems = 20
        Me.Username.Name = "Username"
        Me.Username.Size = New System.Drawing.Size(166, 21)
        Me.Username.Sorted = True
        Me.Username.TabIndex = 3
        '
        'Secure
        '
        Me.Secure.AutoSize = True
        Me.Secure.Location = New System.Drawing.Point(80, 169)
        Me.Secure.Name = "Secure"
        Me.Secure.Size = New System.Drawing.Size(142, 17)
        Me.Secure.TabIndex = 8
        Me.Secure.Text = "Use secure server (slow)"
        Me.Secure.UseVisualStyleBackColor = True
        '
        'RememberMe
        '
        Me.RememberMe.AutoSize = True
        Me.RememberMe.Location = New System.Drawing.Point(80, 192)
        Me.RememberMe.Name = "RememberMe"
        Me.RememberMe.Size = New System.Drawing.Size(94, 17)
        Me.RememberMe.TabIndex = 9
        Me.RememberMe.Text = "Remember me"
        Me.RememberMe.UseVisualStyleBackColor = True
        '
        'LoginForm
        '
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None
        Me.CancelButton = Me.Cancel
        Me.ClientSize = New System.Drawing.Size(270, 252)
        Me.Controls.Add(Me.RememberMe)
        Me.Controls.Add(Me.Secure)
        Me.Controls.Add(Me.Username)
        Me.Controls.Add(Me.Logo)
        Me.Controls.Add(Me.Cancel)
        Me.Controls.Add(Me.Login)
        Me.Controls.Add(Me.WikiLabel)
        Me.Controls.Add(Me.WikiSelector)
        Me.Controls.Add(Me.Password)
        Me.Controls.Add(Me.UsernameLabel)
        Me.Controls.Add(Me.PasswordLabel)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "LoginForm"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Huggle"
        CType(Me.Logo, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Private WithEvents Login As System.Windows.Forms.Button
    Private WithEvents Cancel As System.Windows.Forms.Button
    Private WithEvents PasswordLabel As System.Windows.Forms.Label
    Private WithEvents UsernameLabel As System.Windows.Forms.Label
    Private WithEvents Password As System.Windows.Forms.TextBox
    Private WithEvents WikiSelector As System.Windows.Forms.ComboBox
    Private WithEvents WikiLabel As System.Windows.Forms.Label
    Private WithEvents Logo As System.Windows.Forms.PictureBox
    Private WithEvents ActivityIndicator As Huggle.Controls.ActivityIndicator
    Private WithEvents Username As System.Windows.Forms.ComboBox
    Private WithEvents Secure As System.Windows.Forms.CheckBox
    Private WithEvents RememberMe As System.Windows.Forms.CheckBox
End Class
