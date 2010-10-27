Namespace Huggle.UI
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
            Me.WikiLabel = New System.Windows.Forms.Label()
            Me.PasswordLabel = New System.Windows.Forms.Label()
            Me.AccountLabel = New System.Windows.Forms.Label()
            Me.Password = New System.Windows.Forms.TextBox()
            Me.Logo = New System.Windows.Forms.PictureBox()
            Me.Account = New System.Windows.Forms.EnhancedComboBox()
            Me.Secure = New System.Windows.Forms.CheckBox()
            Me.RememberMe = New System.Windows.Forms.CheckBox()
            Me.CreateAccount = New System.Windows.Forms.LinkLabel()
            Me.AddWiki = New System.Windows.Forms.LinkLabel()
            Me.TypeLabel = New System.Windows.Forms.Label()
            Me.FamilySelector = New System.Windows.Forms.EnhancedComboBox()
            Me.WikiSelector = New System.Windows.Forms.EnhancedComboBox()
            CType(Me.Logo, System.ComponentModel.ISupportInitialize).BeginInit()
            Me.SuspendLayout()
            '
            'Login
            '
            Me.Login.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
            Me.Login.Location = New System.Drawing.Point(101, 249)
            Me.Login.Name = "Login"
            Me.Login.Size = New System.Drawing.Size(80, 23)
            Me.Login.TabIndex = 8
            Me.Login.Text = "Login"
            '
            'Cancel
            '
            Me.Cancel.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
            Me.Cancel.DialogResult = System.Windows.Forms.DialogResult.Cancel
            Me.Cancel.Location = New System.Drawing.Point(187, 249)
            Me.Cancel.Name = "Cancel"
            Me.Cancel.Size = New System.Drawing.Size(80, 23)
            Me.Cancel.TabIndex = 9
            Me.Cancel.Text = "Cancel"
            Me.Cancel.UseVisualStyleBackColor = True
            '
            'WikiLabel
            '
            Me.WikiLabel.Location = New System.Drawing.Point(-2, 124)
            Me.WikiLabel.Name = "WikiLabel"
            Me.WikiLabel.Size = New System.Drawing.Size(71, 17)
            Me.WikiLabel.TabIndex = 0
            Me.WikiLabel.Text = "Wiki:"
            Me.WikiLabel.TextAlign = System.Drawing.ContentAlignment.TopRight
            '
            'PasswordLabel
            '
            Me.PasswordLabel.Location = New System.Drawing.Point(-2, 178)
            Me.PasswordLabel.Name = "PasswordLabel"
            Me.PasswordLabel.Size = New System.Drawing.Size(71, 17)
            Me.PasswordLabel.TabIndex = 4
            Me.PasswordLabel.Text = "Password:"
            Me.PasswordLabel.TextAlign = System.Drawing.ContentAlignment.TopRight
            '
            'AccountLabel
            '
            Me.AccountLabel.Location = New System.Drawing.Point(-2, 151)
            Me.AccountLabel.Name = "AccountLabel"
            Me.AccountLabel.Size = New System.Drawing.Size(71, 17)
            Me.AccountLabel.TabIndex = 2
            Me.AccountLabel.Text = "Account:"
            Me.AccountLabel.TextAlign = System.Drawing.ContentAlignment.TopRight
            '
            'Password
            '
            Me.Password.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                        Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
            Me.Password.Location = New System.Drawing.Point(71, 175)
            Me.Password.MaxLength = 255
            Me.Password.Name = "Password"
            Me.Password.PasswordChar = Global.Microsoft.VisualBasic.ChrW(9679)
            Me.Password.Size = New System.Drawing.Size(150, 20)
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
            Me.Logo.Size = New System.Drawing.Size(279, 80)
            Me.Logo.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage
            Me.Logo.TabIndex = 14
            Me.Logo.TabStop = False
            '
            'Account
            '
            Me.Account.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                        Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
            Me.Account.FormattingEnabled = True
            Me.Account.Location = New System.Drawing.Point(71, 148)
            Me.Account.MaxDropDownItems = 20
            Me.Account.Name = "Account"
            Me.Account.Size = New System.Drawing.Size(150, 21)
            Me.Account.Sorted = True
            Me.Account.TabIndex = 3
            '
            'Secure
            '
            Me.Secure.AutoSize = True
            Me.Secure.Location = New System.Drawing.Point(78, 201)
            Me.Secure.Name = "Secure"
            Me.Secure.Size = New System.Drawing.Size(112, 17)
            Me.Secure.TabIndex = 6
            Me.Secure.Text = "Use secure server"
            Me.Secure.UseVisualStyleBackColor = True
            '
            'RememberMe
            '
            Me.RememberMe.AutoSize = True
            Me.RememberMe.Location = New System.Drawing.Point(78, 224)
            Me.RememberMe.Name = "RememberMe"
            Me.RememberMe.Size = New System.Drawing.Size(94, 17)
            Me.RememberMe.TabIndex = 7
            Me.RememberMe.Text = "Remember me"
            Me.RememberMe.UseVisualStyleBackColor = True
            '
            'CreateAccount
            '
            Me.CreateAccount.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
            Me.CreateAccount.AutoSize = True
            Me.CreateAccount.LinkBehavior = System.Windows.Forms.LinkBehavior.HoverUnderline
            Me.CreateAccount.LinkColor = System.Drawing.SystemColors.HotTrack
            Me.CreateAccount.Location = New System.Drawing.Point(227, 151)
            Me.CreateAccount.Name = "CreateAccount"
            Me.CreateAccount.Size = New System.Drawing.Size(38, 13)
            Me.CreateAccount.TabIndex = 15
            Me.CreateAccount.TabStop = True
            Me.CreateAccount.Text = "Create"
            '
            'AddWiki
            '
            Me.AddWiki.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
            Me.AddWiki.AutoSize = True
            Me.AddWiki.LinkBehavior = System.Windows.Forms.LinkBehavior.HoverUnderline
            Me.AddWiki.LinkColor = System.Drawing.SystemColors.HotTrack
            Me.AddWiki.Location = New System.Drawing.Point(227, 124)
            Me.AddWiki.Name = "AddWiki"
            Me.AddWiki.Size = New System.Drawing.Size(47, 13)
            Me.AddWiki.TabIndex = 16
            Me.AddWiki.TabStop = True
            Me.AddWiki.Text = "Add wiki"
            '
            'TypeLabel
            '
            Me.TypeLabel.Location = New System.Drawing.Point(-2, 97)
            Me.TypeLabel.Name = "TypeLabel"
            Me.TypeLabel.Size = New System.Drawing.Size(71, 17)
            Me.TypeLabel.TabIndex = 19
            Me.TypeLabel.Text = "Wiki type:"
            Me.TypeLabel.TextAlign = System.Drawing.ContentAlignment.TopRight
            '
            'FamilySelector
            '
            Me.FamilySelector.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
            Me.FamilySelector.FormattingEnabled = True
            Me.FamilySelector.Location = New System.Drawing.Point(71, 93)
            Me.FamilySelector.MaxDropDownItems = 20
            Me.FamilySelector.Name = "FamilySelector"
            Me.FamilySelector.Size = New System.Drawing.Size(150, 21)
            Me.FamilySelector.TabIndex = 18
            '
            'WikiSelector
            '
            Me.WikiSelector.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
            Me.WikiSelector.FormattingEnabled = True
            Me.WikiSelector.Location = New System.Drawing.Point(71, 120)
            Me.WikiSelector.MaxDropDownItems = 20
            Me.WikiSelector.Name = "WikiSelector"
            Me.WikiSelector.Size = New System.Drawing.Size(150, 21)
            Me.WikiSelector.TabIndex = 17
            '
            'LoginForm
            '
            Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None
            Me.CancelButton = Me.Cancel
            Me.ClientSize = New System.Drawing.Size(279, 284)
            Me.Controls.Add(Me.TypeLabel)
            Me.Controls.Add(Me.FamilySelector)
            Me.Controls.Add(Me.WikiSelector)
            Me.Controls.Add(Me.AddWiki)
            Me.Controls.Add(Me.CreateAccount)
            Me.Controls.Add(Me.RememberMe)
            Me.Controls.Add(Me.Secure)
            Me.Controls.Add(Me.Account)
            Me.Controls.Add(Me.Logo)
            Me.Controls.Add(Me.Cancel)
            Me.Controls.Add(Me.Login)
            Me.Controls.Add(Me.WikiLabel)
            Me.Controls.Add(Me.Password)
            Me.Controls.Add(Me.AccountLabel)
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
        Private WithEvents AccountLabel As System.Windows.Forms.Label
        Private WithEvents Password As System.Windows.Forms.TextBox
        Private WithEvents WikiLabel As System.Windows.Forms.Label
        Private WithEvents Logo As System.Windows.Forms.PictureBox
        Private WithEvents ActivityIndicator As WaitControl
        Private WithEvents Account As System.Windows.Forms.EnhancedComboBox
        Private WithEvents Secure As System.Windows.Forms.CheckBox
        Private WithEvents RememberMe As System.Windows.Forms.CheckBox
        Private WithEvents CreateAccount As System.Windows.Forms.LinkLabel
        Private WithEvents AddWiki As System.Windows.Forms.LinkLabel
        Private WithEvents FamilySelector As System.Windows.Forms.EnhancedComboBox
        Private WithEvents TypeLabel As System.Windows.Forms.Label
        Private WithEvents WikiSelector As System.Windows.Forms.EnhancedComboBox
    End Class
End Namespace