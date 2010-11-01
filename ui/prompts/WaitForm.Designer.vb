Namespace Huggle.UI
    <Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
    Partial Class WaitForm
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
            Me.Indicator = New Huggle.UI.WaitControl()
            Me.Cancel = New System.Windows.Forms.Button()
            Me.SuspendLayout()
            '
            'Indicator
            '
            Me.Indicator.Location = New System.Drawing.Point(12, 12)
            Me.Indicator.Name = "Indicator"
            Me.Indicator.Size = New System.Drawing.Size(105, 16)
            Me.Indicator.TabIndex = 0
            Me.Indicator.TabStop = False
            Me.Indicator.Text = "Foo"
            Me.Indicator.TextPosition = Huggle.UI.WaitControl.WaitTextPosition.Horizontal
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

        End Sub
        Private WithEvents Cancel As System.Windows.Forms.Button
        Private WithEvents Indicator As WaitControl
    End Class
End Namespace