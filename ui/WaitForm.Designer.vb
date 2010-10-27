Namespace Huggle.UI
    <Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
    Partial Class WaitForm
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
            Me.Indicator = New WaitControl
            Me.Label = New System.Windows.Forms.Label
            Me.Cancel = New System.Windows.Forms.Button
            Me.SuspendLayout()
            '
            'Indicator
            '
            Me.Indicator.Location = New System.Drawing.Point(12, 14)
            Me.Indicator.MaximumSize = New System.Drawing.Size(16, 16)
            Me.Indicator.MinimumSize = New System.Drawing.Size(16, 16)
            Me.Indicator.Name = "Indicator"
            Me.Indicator.Size = New System.Drawing.Size(16, 16)
            Me.Indicator.TabIndex = 0
            Me.Indicator.TabStop = False
            '
            'Label
            '
            Me.Label.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                        Or System.Windows.Forms.AnchorStyles.Left) _
                        Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
            Me.Label.AutoSize = True
            Me.Label.Location = New System.Drawing.Point(34, 15)
            Me.Label.Name = "Label"
            Me.Label.Size = New System.Drawing.Size(10, 13)
            Me.Label.TabIndex = 1
            Me.Label.Text = " "
            '
            'Cancel
            '
            Me.Cancel.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
            Me.Cancel.DialogResult = System.Windows.Forms.DialogResult.Cancel
            Me.Cancel.Location = New System.Drawing.Point(187, 39)
            Me.Cancel.Name = "Cancel"
            Me.Cancel.Size = New System.Drawing.Size(75, 23)
            Me.Cancel.TabIndex = 2
            Me.Cancel.Text = "Cancel"
            Me.Cancel.UseVisualStyleBackColor = True
            '
            'WaitForm
            '
            Me.AcceptButton = Me.Cancel
            Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
            Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
            Me.ClientSize = New System.Drawing.Size(274, 74)
            Me.Controls.Add(Me.Cancel)
            Me.Controls.Add(Me.Label)
            Me.Controls.Add(Me.Indicator)
            Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
            Me.KeyPreview = True
            Me.MaximizeBox = False
            Me.MinimizeBox = False
            Me.Name = "WaitForm"
            Me.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide
            Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
            Me.Text = "Please wait"
            Me.ResumeLayout(False)
            Me.PerformLayout()

        End Sub
        Private WithEvents Cancel As System.Windows.Forms.Button
        Private WithEvents Indicator As WaitControl
        Private WithEvents Label As System.Windows.Forms.Label
    End Class
End Namespace