Namespace Huggle.UI
    <Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
    Partial Class AccountAutoUnifiedForm
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
            Me.Request = New System.Windows.Forms.Label
            Me.Cancel = New System.Windows.Forms.Button
            Me.OK = New System.Windows.Forms.Button
            Me.AutoUnifiedLogin = New System.Windows.Forms.CheckBox
            Me.SuspendLayout()
            '
            'Request
            '
            Me.Request.Location = New System.Drawing.Point(12, 9)
            Me.Request.Name = "Request"
            Me.Request.Size = New System.Drawing.Size(360, 49)
            Me.Request.TabIndex = 1
            Me.Request.Text = "{0} wishes to use the account {1}. As you are logged in globally as {2}, a passwo" & _
                "rd is not needed, but your permission is."
            '
            'Cancel
            '
            Me.Cancel.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
            Me.Cancel.DialogResult = System.Windows.Forms.DialogResult.Cancel
            Me.Cancel.Location = New System.Drawing.Point(297, 94)
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
            Me.OK.Location = New System.Drawing.Point(216, 94)
            Me.OK.Name = "OK"
            Me.OK.Size = New System.Drawing.Size(75, 23)
            Me.OK.TabIndex = 2
            Me.OK.Text = "Allow"
            Me.OK.UseVisualStyleBackColor = True
            '
            'AutoUnifiedLogin
            '
            Me.AutoUnifiedLogin.AutoSize = True
            Me.AutoUnifiedLogin.Location = New System.Drawing.Point(15, 64)
            Me.AutoUnifiedLogin.Name = "AutoUnifiedLogin"
            Me.AutoUnifiedLogin.Size = New System.Drawing.Size(295, 17)
            Me.AutoUnifiedLogin.TabIndex = 3
            Me.AutoUnifiedLogin.Text = "In future, automatically use my local accounts as required"
            Me.AutoUnifiedLogin.UseVisualStyleBackColor = True
            '
            'AccountAutoUnifiedForm
            '
            Me.AcceptButton = Me.OK
            Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
            Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
            Me.CancelButton = Me.Cancel
            Me.ClientSize = New System.Drawing.Size(384, 129)
            Me.Controls.Add(Me.AutoUnifiedLogin)
            Me.Controls.Add(Me.OK)
            Me.Controls.Add(Me.Cancel)
            Me.Controls.Add(Me.Request)
            Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
            Me.MaximizeBox = False
            Me.MinimizeBox = False
            Me.Name = "AccountAutoUnifiedForm"
            Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
            Me.Text = "Account access"
            Me.ResumeLayout(False)
            Me.PerformLayout()

        End Sub
        Private WithEvents Request As System.Windows.Forms.Label
        Friend WithEvents Cancel As System.Windows.Forms.Button
        Friend WithEvents AutoUnifiedLogin As System.Windows.Forms.CheckBox
        Private WithEvents OK As System.Windows.Forms.Button
    End Class
End Namespace