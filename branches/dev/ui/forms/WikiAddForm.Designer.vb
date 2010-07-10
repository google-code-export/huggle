<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class WikiAddForm
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
        Me.Explanation = New System.Windows.Forms.Label()
        Me.Url = New System.Windows.Forms.TextBox()
        Me.AccountLabel = New System.Windows.Forms.Label()
        Me.UsernameLabel = New System.Windows.Forms.Label()
        Me.PasswordLabel = New System.Windows.Forms.Label()
        Me.Password = New System.Windows.Forms.TextBox()
        Me.Username = New System.Windows.Forms.TextBox()
        Me.Cancel = New System.Windows.Forms.Button()
        Me.OK = New System.Windows.Forms.Button()
        Me.UrlLabel = New System.Windows.Forms.Label()
        Me.SuspendLayout()
        '
        'Explanation
        '
        Me.Explanation.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Explanation.Location = New System.Drawing.Point(12, 9)
        Me.Explanation.Name = "Explanation"
        Me.Explanation.Size = New System.Drawing.Size(322, 30)
        Me.Explanation.TabIndex = 0
        Me.Explanation.Text = "Use this form to add a wiki to the wiki list. Wikis must be running a recent vers" & _
            "ion of MediaWiki and have the MediaWiki API enabled." & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10)
        '
        'Url
        '
        Me.Url.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Url.Location = New System.Drawing.Point(74, 51)
        Me.Url.Name = "Url"
        Me.Url.Size = New System.Drawing.Size(260, 20)
        Me.Url.TabIndex = 3
        '
        'AccountLabel
        '
        Me.AccountLabel.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.AccountLabel.Location = New System.Drawing.Point(12, 87)
        Me.AccountLabel.Name = "AccountLabel"
        Me.AccountLabel.Size = New System.Drawing.Size(322, 18)
        Me.AccountLabel.TabIndex = 4
        Me.AccountLabel.Text = "If an account is required to read the wiki, enter details here:"
        '
        'UsernameLabel
        '
        Me.UsernameLabel.AutoSize = True
        Me.UsernameLabel.Location = New System.Drawing.Point(15, 111)
        Me.UsernameLabel.Name = "UsernameLabel"
        Me.UsernameLabel.Size = New System.Drawing.Size(58, 13)
        Me.UsernameLabel.TabIndex = 5
        Me.UsernameLabel.Text = "Username:"
        '
        'PasswordLabel
        '
        Me.PasswordLabel.AutoSize = True
        Me.PasswordLabel.Location = New System.Drawing.Point(17, 136)
        Me.PasswordLabel.Name = "PasswordLabel"
        Me.PasswordLabel.Size = New System.Drawing.Size(56, 13)
        Me.PasswordLabel.TabIndex = 7
        Me.PasswordLabel.Text = "Password:"
        '
        'Password
        '
        Me.Password.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Password.Location = New System.Drawing.Point(74, 133)
        Me.Password.Name = "Password"
        Me.Password.PasswordChar = Global.Microsoft.VisualBasic.ChrW(9679)
        Me.Password.Size = New System.Drawing.Size(195, 20)
        Me.Password.TabIndex = 8
        '
        'Username
        '
        Me.Username.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Username.Location = New System.Drawing.Point(74, 108)
        Me.Username.Name = "Username"
        Me.Username.Size = New System.Drawing.Size(195, 20)
        Me.Username.TabIndex = 6
        '
        'Cancel
        '
        Me.Cancel.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Cancel.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.Cancel.Location = New System.Drawing.Point(259, 162)
        Me.Cancel.Name = "Cancel"
        Me.Cancel.Size = New System.Drawing.Size(75, 23)
        Me.Cancel.TabIndex = 10
        Me.Cancel.Text = "Cancel"
        Me.Cancel.UseVisualStyleBackColor = True
        '
        'OK
        '
        Me.OK.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.OK.Enabled = False
        Me.OK.Location = New System.Drawing.Point(178, 162)
        Me.OK.Name = "OK"
        Me.OK.Size = New System.Drawing.Size(75, 23)
        Me.OK.TabIndex = 9
        Me.OK.Text = "Continue"
        Me.OK.UseVisualStyleBackColor = True
        '
        'UrlLabel
        '
        Me.UrlLabel.AutoSize = True
        Me.UrlLabel.Location = New System.Drawing.Point(8, 54)
        Me.UrlLabel.Name = "UrlLabel"
        Me.UrlLabel.Size = New System.Drawing.Size(65, 13)
        Me.UrlLabel.TabIndex = 2
        Me.UrlLabel.Text = "URL of wiki:"
        '
        'WikiAddForm
        '
        Me.AcceptButton = Me.OK
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.CancelButton = Me.Cancel
        Me.ClientSize = New System.Drawing.Size(346, 197)
        Me.Controls.Add(Me.OK)
        Me.Controls.Add(Me.Cancel)
        Me.Controls.Add(Me.Username)
        Me.Controls.Add(Me.Password)
        Me.Controls.Add(Me.PasswordLabel)
        Me.Controls.Add(Me.UrlLabel)
        Me.Controls.Add(Me.UsernameLabel)
        Me.Controls.Add(Me.AccountLabel)
        Me.Controls.Add(Me.Url)
        Me.Controls.Add(Me.Explanation)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "WikiAddForm"
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Add wiki"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Private WithEvents Explanation As System.Windows.Forms.Label
    Private WithEvents Url As System.Windows.Forms.TextBox
    Private WithEvents AccountLabel As System.Windows.Forms.Label
    Private WithEvents PasswordLabel As System.Windows.Forms.Label
    Private WithEvents Cancel As System.Windows.Forms.Button
    Private WithEvents OK As System.Windows.Forms.Button
    Private WithEvents UsernameLabel As System.Windows.Forms.Label
    Private WithEvents Password As System.Windows.Forms.TextBox
    Private WithEvents Username As System.Windows.Forms.TextBox
    Private WithEvents UrlLabel As System.Windows.Forms.Label
End Class
