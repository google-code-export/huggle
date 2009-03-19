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
        Me.OK = New System.Windows.Forms.Button
        Me.Cancel = New System.Windows.Forms.Button
        Me.Options = New System.Windows.Forms.GroupBox
        Me.ProxyEnabled = New System.Windows.Forms.CheckBox
        Me.ProxyDomain = New System.Windows.Forms.TextBox
        Me.Label6 = New System.Windows.Forms.Label
        Me.Label5 = New System.Windows.Forms.Label
        Me.Label3 = New System.Windows.Forms.Label
        Me.Label4 = New System.Windows.Forms.Label
        Me.ProxyPassword = New System.Windows.Forms.TextBox
        Me.ProxyUsername = New System.Windows.Forms.TextBox
        Me.Label2 = New System.Windows.Forms.Label
        Me.ProxyPort = New System.Windows.Forms.TextBox
        Me.Label1 = New System.Windows.Forms.Label
        Me.ProxyAddress = New System.Windows.Forms.TextBox
        Me.Project = New System.Windows.Forms.ComboBox
        Me.ProjectLabel = New System.Windows.Forms.Label
        Me.PasswordLabel = New System.Windows.Forms.Label
        Me.UsernameLabel = New System.Windows.Forms.Label
        Me.Password = New System.Windows.Forms.TextBox
        Me.Username = New System.Windows.Forms.TextBox
        Me.TitleLabel = New System.Windows.Forms.Label
        Me.Version = New System.Windows.Forms.Label
        Me.Status = New System.Windows.Forms.Label
        Me.Progress = New System.Windows.Forms.ProgressBar
        Me.ShowProxySettings = New System.Windows.Forms.Button
        Me.HideProxySettings = New System.Windows.Forms.Button
        Me.Options.SuspendLayout()
        Me.SuspendLayout()
        '
        'OK
        '
        Me.OK.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.OK.Enabled = False
        Me.OK.Location = New System.Drawing.Point(164, 176)
        Me.OK.Name = "OK"
        Me.OK.Size = New System.Drawing.Size(75, 23)
        Me.OK.TabIndex = 1
        Me.OK.Text = "Login"
        Me.OK.UseVisualStyleBackColor = True
        '
        'Cancel
        '
        Me.Cancel.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Cancel.Location = New System.Drawing.Point(246, 176)
        Me.Cancel.Name = "Cancel"
        Me.Cancel.Size = New System.Drawing.Size(75, 23)
        Me.Cancel.TabIndex = 2
        Me.Cancel.Text = "Exit"
        Me.Cancel.UseVisualStyleBackColor = True
        '
        'Options
        '
        Me.Options.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Options.Controls.Add(Me.ProxyEnabled)
        Me.Options.Controls.Add(Me.ProxyDomain)
        Me.Options.Controls.Add(Me.Label6)
        Me.Options.Controls.Add(Me.Label5)
        Me.Options.Controls.Add(Me.Label3)
        Me.Options.Controls.Add(Me.Label4)
        Me.Options.Controls.Add(Me.ProxyPassword)
        Me.Options.Controls.Add(Me.ProxyUsername)
        Me.Options.Controls.Add(Me.Label2)
        Me.Options.Controls.Add(Me.ProxyPort)
        Me.Options.Controls.Add(Me.Label1)
        Me.Options.Controls.Add(Me.ProxyAddress)
        Me.Options.Controls.Add(Me.Project)
        Me.Options.Controls.Add(Me.ProjectLabel)
        Me.Options.Controls.Add(Me.PasswordLabel)
        Me.Options.Controls.Add(Me.UsernameLabel)
        Me.Options.Controls.Add(Me.Password)
        Me.Options.Controls.Add(Me.Username)
        Me.Options.Location = New System.Drawing.Point(10, 71)
        Me.Options.Name = "Options"
        Me.Options.Size = New System.Drawing.Size(312, 99)
        Me.Options.TabIndex = 0
        Me.Options.TabStop = False
        '
        'ProxyEnabled
        '
        Me.ProxyEnabled.AutoSize = True
        Me.ProxyEnabled.Location = New System.Drawing.Point(70, 123)
        Me.ProxyEnabled.Name = "ProxyEnabled"
        Me.ProxyEnabled.Size = New System.Drawing.Size(100, 17)
        Me.ProxyEnabled.TabIndex = 17
        Me.ProxyEnabled.Text = "Proxy Enabled?"
        Me.ProxyEnabled.UseVisualStyleBackColor = True
        '
        'ProxyDomain
        '
        Me.ProxyDomain.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ProxyDomain.Location = New System.Drawing.Point(185, 172)
        Me.ProxyDomain.Name = "ProxyDomain"
        Me.ProxyDomain.Size = New System.Drawing.Size(99, 20)
        Me.ProxyDomain.TabIndex = 12
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Location = New System.Drawing.Point(133, 175)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(46, 13)
        Me.Label6.TabIndex = 11
        Me.Label6.Text = "Domain:"
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label5.Location = New System.Drawing.Point(8, 107)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(86, 13)
        Me.Label5.TabIndex = 6
        Me.Label5.Text = "Proxy settings"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(8, 227)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(56, 13)
        Me.Label3.TabIndex = 15
        Me.Label3.Text = "Password:"
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(6, 201)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(58, 13)
        Me.Label4.TabIndex = 13
        Me.Label4.Text = "Username:"
        '
        'ProxyPassword
        '
        Me.ProxyPassword.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ProxyPassword.Location = New System.Drawing.Point(70, 224)
        Me.ProxyPassword.Name = "ProxyPassword"
        Me.ProxyPassword.PasswordChar = Global.Microsoft.VisualBasic.ChrW(42)
        Me.ProxyPassword.Size = New System.Drawing.Size(214, 20)
        Me.ProxyPassword.TabIndex = 16
        '
        'ProxyUsername
        '
        Me.ProxyUsername.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ProxyUsername.Location = New System.Drawing.Point(70, 198)
        Me.ProxyUsername.Name = "ProxyUsername"
        Me.ProxyUsername.Size = New System.Drawing.Size(214, 20)
        Me.ProxyUsername.TabIndex = 14
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(35, 175)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(29, 13)
        Me.Label2.TabIndex = 9
        Me.Label2.Text = "Port:"
        '
        'ProxyPort
        '
        Me.ProxyPort.Location = New System.Drawing.Point(70, 172)
        Me.ProxyPort.Name = "ProxyPort"
        Me.ProxyPort.Size = New System.Drawing.Size(47, 20)
        Me.ProxyPort.TabIndex = 10
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(16, 149)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(48, 13)
        Me.Label1.TabIndex = 7
        Me.Label1.Text = "Address:"
        '
        'ProxyAddress
        '
        Me.ProxyAddress.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ProxyAddress.Location = New System.Drawing.Point(70, 146)
        Me.ProxyAddress.Name = "ProxyAddress"
        Me.ProxyAddress.Size = New System.Drawing.Size(214, 20)
        Me.ProxyAddress.TabIndex = 8
        '
        'Project
        '
        Me.Project.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.Project.FormattingEnabled = True
        Me.Project.Location = New System.Drawing.Point(70, 15)
        Me.Project.MaxDropDownItems = 20
        Me.Project.Name = "Project"
        Me.Project.Size = New System.Drawing.Size(143, 21)
        Me.Project.TabIndex = 1
        '
        'ProjectLabel
        '
        Me.ProjectLabel.AutoSize = True
        Me.ProjectLabel.Location = New System.Drawing.Point(21, 18)
        Me.ProjectLabel.Name = "ProjectLabel"
        Me.ProjectLabel.Size = New System.Drawing.Size(43, 13)
        Me.ProjectLabel.TabIndex = 0
        Me.ProjectLabel.Text = "Project:"
        '
        'PasswordLabel
        '
        Me.PasswordLabel.AutoSize = True
        Me.PasswordLabel.Location = New System.Drawing.Point(8, 71)
        Me.PasswordLabel.Name = "PasswordLabel"
        Me.PasswordLabel.Size = New System.Drawing.Size(56, 13)
        Me.PasswordLabel.TabIndex = 4
        Me.PasswordLabel.Text = "Password:"
        '
        'UsernameLabel
        '
        Me.UsernameLabel.AutoSize = True
        Me.UsernameLabel.Location = New System.Drawing.Point(6, 45)
        Me.UsernameLabel.Name = "UsernameLabel"
        Me.UsernameLabel.Size = New System.Drawing.Size(58, 13)
        Me.UsernameLabel.TabIndex = 2
        Me.UsernameLabel.Text = "Username:"
        '
        'Password
        '
        Me.Password.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Password.Location = New System.Drawing.Point(70, 68)
        Me.Password.Name = "Password"
        Me.Password.PasswordChar = Global.Microsoft.VisualBasic.ChrW(42)
        Me.Password.Size = New System.Drawing.Size(214, 20)
        Me.Password.TabIndex = 5
        '
        'Username
        '
        Me.Username.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Username.Location = New System.Drawing.Point(70, 42)
        Me.Username.Name = "Username"
        Me.Username.Size = New System.Drawing.Size(214, 20)
        Me.Username.TabIndex = 3
        '
        'TitleLabel
        '
        Me.TitleLabel.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TitleLabel.Font = New System.Drawing.Font("Tahoma", 27.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TitleLabel.Location = New System.Drawing.Point(10, -4)
        Me.TitleLabel.Name = "TitleLabel"
        Me.TitleLabel.Size = New System.Drawing.Size(312, 55)
        Me.TitleLabel.TabIndex = 7
        Me.TitleLabel.Text = "huggle lite"
        Me.TitleLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Version
        '
        Me.Version.AutoSize = True
        Me.Version.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Version.Location = New System.Drawing.Point(9, 51)
        Me.Version.Name = "Version"
        Me.Version.Size = New System.Drawing.Size(49, 13)
        Me.Version.TabIndex = 8
        Me.Version.Text = "Version"
        '
        'Status
        '
        Me.Status.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Status.AutoSize = True
        Me.Status.Location = New System.Drawing.Point(9, 205)
        Me.Status.Name = "Status"
        Me.Status.Size = New System.Drawing.Size(10, 13)
        Me.Status.TabIndex = 5
        Me.Status.Text = " "
        '
        'Progress
        '
        Me.Progress.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Progress.Enabled = False
        Me.Progress.Location = New System.Drawing.Point(12, 226)
        Me.Progress.Maximum = 10
        Me.Progress.Name = "Progress"
        Me.Progress.Size = New System.Drawing.Size(310, 19)
        Me.Progress.Step = 1
        Me.Progress.Style = System.Windows.Forms.ProgressBarStyle.Continuous
        Me.Progress.TabIndex = 6
        '
        'ShowProxySettings
        '
        Me.ShowProxySettings.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.ShowProxySettings.Location = New System.Drawing.Point(11, 176)
        Me.ShowProxySettings.Name = "ShowProxySettings"
        Me.ShowProxySettings.Size = New System.Drawing.Size(109, 23)
        Me.ShowProxySettings.TabIndex = 3
        Me.ShowProxySettings.Text = "Proxy settings >>"
        Me.ShowProxySettings.UseVisualStyleBackColor = True
        '
        'HideProxySettings
        '
        Me.HideProxySettings.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.HideProxySettings.Location = New System.Drawing.Point(12, 176)
        Me.HideProxySettings.Name = "HideProxySettings"
        Me.HideProxySettings.Size = New System.Drawing.Size(109, 23)
        Me.HideProxySettings.TabIndex = 4
        Me.HideProxySettings.Text = "<< Proxy settings"
        Me.HideProxySettings.UseVisualStyleBackColor = True
        Me.HideProxySettings.Visible = False
        '
        'LoginForm
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(334, 257)
        Me.Controls.Add(Me.HideProxySettings)
        Me.Controls.Add(Me.ShowProxySettings)
        Me.Controls.Add(Me.Status)
        Me.Controls.Add(Me.Progress)
        Me.Controls.Add(Me.TitleLabel)
        Me.Controls.Add(Me.Version)
        Me.Controls.Add(Me.Options)
        Me.Controls.Add(Me.Cancel)
        Me.Controls.Add(Me.OK)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.KeyPreview = True
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "LoginForm"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Huggle lite"
        Me.Options.ResumeLayout(False)
        Me.Options.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents OK As System.Windows.Forms.Button
    Friend WithEvents Cancel As System.Windows.Forms.Button
    Friend WithEvents Options As System.Windows.Forms.GroupBox
    Friend WithEvents PasswordLabel As System.Windows.Forms.Label
    Friend WithEvents UsernameLabel As System.Windows.Forms.Label
    Friend WithEvents Password As System.Windows.Forms.TextBox
    Friend WithEvents Username As System.Windows.Forms.TextBox
    Friend WithEvents TitleLabel As System.Windows.Forms.Label
    Friend WithEvents Version As System.Windows.Forms.Label
    Friend WithEvents Status As System.Windows.Forms.Label
    Friend WithEvents Progress As System.Windows.Forms.ProgressBar
    Friend WithEvents Project As System.Windows.Forms.ComboBox
    Friend WithEvents ProjectLabel As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents ProxyPort As System.Windows.Forms.TextBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents ProxyAddress As System.Windows.Forms.TextBox
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents ProxyPassword As System.Windows.Forms.TextBox
    Friend WithEvents ProxyUsername As System.Windows.Forms.TextBox
    Friend WithEvents ProxyDomain As System.Windows.Forms.TextBox
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents ShowProxySettings As System.Windows.Forms.Button
    Friend WithEvents HideProxySettings As System.Windows.Forms.Button
    Friend WithEvents ProxyEnabled As System.Windows.Forms.CheckBox
End Class
