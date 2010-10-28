Namespace Huggle.UI
    <Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
    Partial Class AccountUpgradeForm
        Inherits Huggle.UI.HuggleForm

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
            Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(AccountUpgradeForm))
            Me.Label1 = New System.Windows.Forms.Label
            Me.Button1 = New System.Windows.Forms.Button
            Me.Button2 = New System.Windows.Forms.Button
            Me.UsernameLabel = New System.Windows.Forms.Label
            Me.PasswordLabel = New System.Windows.Forms.Label
            Me.TextBox1 = New System.Windows.Forms.TextBox
            Me.TextBox2 = New System.Windows.Forms.TextBox
            Me.SuspendLayout()
            '
            'Label1
            '
            Me.Label1.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                        Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
            Me.Label1.Location = New System.Drawing.Point(12, 9)
            Me.Label1.Name = "Label1"
            Me.Label1.Size = New System.Drawing.Size(289, 86)
            Me.Label1.TabIndex = 0
            Me.Label1.Text = resources.GetString("Label1.Text")
            '
            'Button1
            '
            Me.Button1.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
            Me.Button1.Location = New System.Drawing.Point(226, 160)
            Me.Button1.Name = "Button1"
            Me.Button1.Size = New System.Drawing.Size(75, 23)
            Me.Button1.TabIndex = 1
            Me.Button1.Text = "Cancel"
            Me.Button1.UseVisualStyleBackColor = True
            '
            'Button2
            '
            Me.Button2.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
            Me.Button2.Location = New System.Drawing.Point(145, 160)
            Me.Button2.Name = "Button2"
            Me.Button2.Size = New System.Drawing.Size(75, 23)
            Me.Button2.TabIndex = 1
            Me.Button2.Text = "OK"
            Me.Button2.UseVisualStyleBackColor = True
            '
            'UsernameLabel
            '
            Me.UsernameLabel.AutoSize = True
            Me.UsernameLabel.Location = New System.Drawing.Point(24, 107)
            Me.UsernameLabel.Name = "UsernameLabel"
            Me.UsernameLabel.Size = New System.Drawing.Size(58, 13)
            Me.UsernameLabel.TabIndex = 2
            Me.UsernameLabel.Text = "Username:"
            '
            'PasswordLabel
            '
            Me.PasswordLabel.AutoSize = True
            Me.PasswordLabel.Location = New System.Drawing.Point(26, 131)
            Me.PasswordLabel.Name = "PasswordLabel"
            Me.PasswordLabel.Size = New System.Drawing.Size(56, 13)
            Me.PasswordLabel.TabIndex = 2
            Me.PasswordLabel.Text = "Password:"
            '
            'TextBox1
            '
            Me.TextBox1.Location = New System.Drawing.Point(88, 104)
            Me.TextBox1.Name = "TextBox1"
            Me.TextBox1.Size = New System.Drawing.Size(141, 20)
            Me.TextBox1.TabIndex = 3
            '
            'TextBox2
            '
            Me.TextBox2.Location = New System.Drawing.Point(88, 128)
            Me.TextBox2.Name = "TextBox2"
            Me.TextBox2.Size = New System.Drawing.Size(141, 20)
            Me.TextBox2.TabIndex = 3
            '
            'AccountUpgradeForm
            '
            Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
            Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
            Me.ClientSize = New System.Drawing.Size(313, 195)
            Me.Controls.Add(Me.TextBox2)
            Me.Controls.Add(Me.TextBox1)
            Me.Controls.Add(Me.PasswordLabel)
            Me.Controls.Add(Me.UsernameLabel)
            Me.Controls.Add(Me.Button2)
            Me.Controls.Add(Me.Button1)
            Me.Controls.Add(Me.Label1)
            Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
            Me.MaximizeBox = False
            Me.MinimizeBox = False
            Me.Name = "AccountUpgradeForm"
            Me.Text = "Privilege elevation required"
            Me.ResumeLayout(False)
            Me.PerformLayout()

        End Sub
        Friend WithEvents Label1 As System.Windows.Forms.Label
        Friend WithEvents Button1 As System.Windows.Forms.Button
        Friend WithEvents Button2 As System.Windows.Forms.Button
        Friend WithEvents UsernameLabel As System.Windows.Forms.Label
        Private WithEvents PasswordLabel As System.Windows.Forms.Label
        Friend WithEvents TextBox1 As System.Windows.Forms.TextBox
        Friend WithEvents TextBox2 As System.Windows.Forms.TextBox
    End Class
End Namespace