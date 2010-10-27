Namespace Huggle.UI
    <Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
    Partial Class AccountElevateForm
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
            Me.Button1 = New System.Windows.Forms.Button
            Me.Button2 = New System.Windows.Forms.Button
            Me.Button3 = New System.Windows.Forms.Button
            Me.SuspendLayout()
            '
            'Label1
            '
            Me.Label1.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                        Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
            Me.Label1.Location = New System.Drawing.Point(12, 9)
            Me.Label1.Name = "Label1"
            Me.Label1.Size = New System.Drawing.Size(285, 43)
            Me.Label1.TabIndex = 0
            Me.Label1.Text = "Something needs an account on ""foo"" with the user right ""bar"" to continue. The cu" & _
                "rrently selected account, ""baz"", does not have this right."
            '
            'Button1
            '
            Me.Button1.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                        Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
            Me.Button1.Location = New System.Drawing.Point(12, 57)
            Me.Button1.Name = "Button1"
            Me.Button1.Size = New System.Drawing.Size(285, 32)
            Me.Button1.TabIndex = 2
            Me.Button1.Text = " ->  Use the privileged account ""Foo"" for this action"
            Me.Button1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
            Me.Button1.UseVisualStyleBackColor = True
            '
            'Button2
            '
            Me.Button2.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                        Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
            Me.Button2.Location = New System.Drawing.Point(12, 90)
            Me.Button2.Name = "Button2"
            Me.Button2.Size = New System.Drawing.Size(285, 32)
            Me.Button2.TabIndex = 2
            Me.Button2.Text = " ->  Select an alternative account to use"
            Me.Button2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
            Me.Button2.UseVisualStyleBackColor = True
            '
            'Button3
            '
            Me.Button3.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                        Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
            Me.Button3.Location = New System.Drawing.Point(12, 123)
            Me.Button3.Name = "Button3"
            Me.Button3.Size = New System.Drawing.Size(285, 32)
            Me.Button3.TabIndex = 2
            Me.Button3.Text = " ->  Cancel the action"
            Me.Button3.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
            Me.Button3.UseVisualStyleBackColor = True
            '
            'AccountElevateForm
            '
            Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
            Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
            Me.ClientSize = New System.Drawing.Size(309, 165)
            Me.Controls.Add(Me.Button3)
            Me.Controls.Add(Me.Button2)
            Me.Controls.Add(Me.Button1)
            Me.Controls.Add(Me.Label1)
            Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
            Me.MaximizeBox = False
            Me.MinimizeBox = False
            Me.Name = "AccountElevateForm"
            Me.Text = "Privilege elevation required"
            Me.ResumeLayout(False)

        End Sub
        Friend WithEvents Label1 As System.Windows.Forms.Label
        Private WithEvents Button1 As System.Windows.Forms.Button
        Private WithEvents Button2 As System.Windows.Forms.Button
        Private WithEvents Button3 As System.Windows.Forms.Button
    End Class
End Namespace