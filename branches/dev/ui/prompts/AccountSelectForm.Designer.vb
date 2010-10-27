Namespace Huggle.UI
    <Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
    Partial Class AccountSelectForm
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
            Me.Request = New System.Windows.Forms.Label
            Me.LayoutPanel = New System.Windows.Forms.FlowLayoutPanel
            Me.Unified = New System.Windows.Forms.RadioButton
            Me.RememberUnified = New System.Windows.Forms.CheckBox
            Me.Anonymous = New System.Windows.Forms.RadioButton
            Me.OtherLogin = New System.Windows.Forms.RadioButton
            Me.LoginPanel = New System.Windows.Forms.Panel
            Me.PasswordLabel = New System.Windows.Forms.Label
            Me.UsernameLabel = New System.Windows.Forms.Label
            Me.RememberOther = New System.Windows.Forms.CheckBox
            Me.Password = New System.Windows.Forms.TextBox
            Me.Username = New System.Windows.Forms.TextBox
            Me.Cancel = New System.Windows.Forms.Button
            Me.OK = New System.Windows.Forms.Button
            Me.LayoutPanel.SuspendLayout()
            Me.LoginPanel.SuspendLayout()
            Me.SuspendLayout()
            '
            'Request
            '
            Me.Request.AutoSize = True
            Me.Request.Location = New System.Drawing.Point(12, 9)
            Me.Request.Name = "Request"
            Me.Request.Size = New System.Drawing.Size(231, 13)
            Me.Request.TabIndex = 0
            Me.Request.Text = "Something is requesting access to the wiki '{0}'."
            '
            'LayoutPanel
            '
            Me.LayoutPanel.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                        Or System.Windows.Forms.AnchorStyles.Left) _
                        Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
            Me.LayoutPanel.Controls.Add(Me.Unified)
            Me.LayoutPanel.Controls.Add(Me.RememberUnified)
            Me.LayoutPanel.Controls.Add(Me.Anonymous)
            Me.LayoutPanel.Controls.Add(Me.OtherLogin)
            Me.LayoutPanel.Controls.Add(Me.LoginPanel)
            Me.LayoutPanel.FlowDirection = System.Windows.Forms.FlowDirection.TopDown
            Me.LayoutPanel.Location = New System.Drawing.Point(12, 31)
            Me.LayoutPanel.Name = "LayoutPanel"
            Me.LayoutPanel.Size = New System.Drawing.Size(316, 181)
            Me.LayoutPanel.TabIndex = 1
            '
            'Unified
            '
            Me.Unified.AutoSize = True
            Me.Unified.Location = New System.Drawing.Point(3, 3)
            Me.Unified.Name = "Unified"
            Me.Unified.Size = New System.Drawing.Size(137, 17)
            Me.Unified.TabIndex = 1
            Me.Unified.TabStop = True
            Me.Unified.Text = "Use unified account {0}"
            Me.Unified.UseVisualStyleBackColor = True
            '
            'RememberUnified
            '
            Me.RememberUnified.AutoSize = True
            Me.RememberUnified.Location = New System.Drawing.Point(3, 26)
            Me.RememberUnified.Name = "RememberUnified"
            Me.RememberUnified.Padding = New System.Windows.Forms.Padding(16, 0, 0, 0)
            Me.RememberUnified.Size = New System.Drawing.Size(257, 17)
            Me.RememberUnified.TabIndex = 5
            Me.RememberUnified.Text = "Use this account on all wikis with unified login"
            Me.RememberUnified.UseVisualStyleBackColor = True
            '
            'Anonymous
            '
            Me.Anonymous.AutoSize = True
            Me.Anonymous.Location = New System.Drawing.Point(3, 49)
            Me.Anonymous.Name = "Anonymous"
            Me.Anonymous.Size = New System.Drawing.Size(143, 17)
            Me.Anonymous.TabIndex = 3
            Me.Anonymous.TabStop = True
            Me.Anonymous.Text = "Use anonymous account"
            Me.Anonymous.UseVisualStyleBackColor = True
            '
            'OtherLogin
            '
            Me.OtherLogin.AutoSize = True
            Me.OtherLogin.Location = New System.Drawing.Point(3, 72)
            Me.OtherLogin.Name = "OtherLogin"
            Me.OtherLogin.Size = New System.Drawing.Size(151, 17)
            Me.OtherLogin.TabIndex = 2
            Me.OtherLogin.TabStop = True
            Me.OtherLogin.Text = "Use the following account:"
            Me.OtherLogin.UseVisualStyleBackColor = True
            '
            'LoginPanel
            '
            Me.LoginPanel.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                        Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
            Me.LoginPanel.Controls.Add(Me.PasswordLabel)
            Me.LoginPanel.Controls.Add(Me.UsernameLabel)
            Me.LoginPanel.Controls.Add(Me.RememberOther)
            Me.LoginPanel.Controls.Add(Me.Password)
            Me.LoginPanel.Controls.Add(Me.Username)
            Me.LoginPanel.Location = New System.Drawing.Point(3, 95)
            Me.LoginPanel.Name = "LoginPanel"
            Me.LoginPanel.Size = New System.Drawing.Size(257, 73)
            Me.LoginPanel.TabIndex = 4
            '
            'PasswordLabel
            '
            Me.PasswordLabel.AutoSize = True
            Me.PasswordLabel.Location = New System.Drawing.Point(15, 27)
            Me.PasswordLabel.Name = "PasswordLabel"
            Me.PasswordLabel.Size = New System.Drawing.Size(56, 13)
            Me.PasswordLabel.TabIndex = 7
            Me.PasswordLabel.Text = "Password:"
            '
            'UsernameLabel
            '
            Me.UsernameLabel.AutoSize = True
            Me.UsernameLabel.Location = New System.Drawing.Point(15, 5)
            Me.UsernameLabel.Name = "UsernameLabel"
            Me.UsernameLabel.Size = New System.Drawing.Size(58, 13)
            Me.UsernameLabel.TabIndex = 7
            Me.UsernameLabel.Text = "Username:"
            '
            'RememberOther
            '
            Me.RememberOther.AutoSize = True
            Me.RememberOther.Location = New System.Drawing.Point(4, 50)
            Me.RememberOther.Name = "RememberOther"
            Me.RememberOther.Padding = New System.Windows.Forms.Padding(12, 0, 0, 0)
            Me.RememberOther.Size = New System.Drawing.Size(207, 17)
            Me.RememberOther.TabIndex = 6
            Me.RememberOther.Text = "Always use this account for this wiki"
            Me.RememberOther.UseVisualStyleBackColor = True
            '
            'Password
            '
            Me.Password.Location = New System.Drawing.Point(77, 24)
            Me.Password.Name = "Password"
            Me.Password.PasswordChar = Global.Microsoft.VisualBasic.ChrW(42)
            Me.Password.Size = New System.Drawing.Size(135, 20)
            Me.Password.TabIndex = 1
            '
            'Username
            '
            Me.Username.Location = New System.Drawing.Point(77, 2)
            Me.Username.Name = "Username"
            Me.Username.Size = New System.Drawing.Size(135, 20)
            Me.Username.TabIndex = 0
            '
            'Cancel
            '
            Me.Cancel.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
            Me.Cancel.Location = New System.Drawing.Point(253, 218)
            Me.Cancel.Name = "Cancel"
            Me.Cancel.Size = New System.Drawing.Size(75, 23)
            Me.Cancel.TabIndex = 2
            Me.Cancel.Text = "Cancel"
            Me.Cancel.UseVisualStyleBackColor = True
            '
            'OK
            '
            Me.OK.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
            Me.OK.Location = New System.Drawing.Point(172, 218)
            Me.OK.Name = "OK"
            Me.OK.Size = New System.Drawing.Size(75, 23)
            Me.OK.TabIndex = 2
            Me.OK.Text = "OK"
            Me.OK.UseVisualStyleBackColor = True
            '
            'SecondaryLoginForm
            '
            Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
            Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
            Me.ClientSize = New System.Drawing.Size(340, 253)
            Me.Controls.Add(Me.Request)
            Me.Controls.Add(Me.OK)
            Me.Controls.Add(Me.Cancel)
            Me.Controls.Add(Me.LayoutPanel)
            Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
            Me.MaximizeBox = False
            Me.Name = "SecondaryLoginForm"
            Me.Text = "Accessing {0}"
            Me.LayoutPanel.ResumeLayout(False)
            Me.LayoutPanel.PerformLayout()
            Me.LoginPanel.ResumeLayout(False)
            Me.LoginPanel.PerformLayout()
            Me.ResumeLayout(False)
            Me.PerformLayout()

        End Sub
        Private WithEvents Request As System.Windows.Forms.Label
        Private WithEvents LayoutPanel As System.Windows.Forms.FlowLayoutPanel
        Private WithEvents Unified As System.Windows.Forms.RadioButton
        Private WithEvents Anonymous As System.Windows.Forms.RadioButton
        Private WithEvents OtherLogin As System.Windows.Forms.RadioButton
        Private WithEvents LoginPanel As System.Windows.Forms.Panel
        Private WithEvents Username As System.Windows.Forms.TextBox
        Private WithEvents RememberUnified As System.Windows.Forms.CheckBox
        Private WithEvents Password As System.Windows.Forms.TextBox
        Private WithEvents PasswordLabel As System.Windows.Forms.Label
        Private WithEvents UsernameLabel As System.Windows.Forms.Label
        Private WithEvents RememberOther As System.Windows.Forms.CheckBox
        Private WithEvents Cancel As System.Windows.Forms.Button
        Private WithEvents OK As System.Windows.Forms.Button
    End Class
End Namespace