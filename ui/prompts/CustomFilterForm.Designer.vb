Namespace Huggle.UI
    <Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
    Partial Class CustomFilterForm
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
            Me.OK = New System.Windows.Forms.Button()
            Me.Cancel = New System.Windows.Forms.Button()
            Me.FilterInput = New System.Windows.Forms.RichTextBox()
            Me.FilterLabel = New System.Windows.Forms.Label()
            Me.SuspendLayout()
            '
            'OK
            '
            Me.OK.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
            Me.OK.DialogResult = System.Windows.Forms.DialogResult.OK
            Me.OK.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.OK.Location = New System.Drawing.Point(240, 206)
            Me.OK.Name = "OK"
            Me.OK.Size = New System.Drawing.Size(75, 23)
            Me.OK.TabIndex = 0
            Me.OK.Text = "OK"
            Me.OK.UseVisualStyleBackColor = True
            '
            'Cancel
            '
            Me.Cancel.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
            Me.Cancel.DialogResult = System.Windows.Forms.DialogResult.Cancel
            Me.Cancel.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.Cancel.Location = New System.Drawing.Point(321, 206)
            Me.Cancel.Name = "Cancel"
            Me.Cancel.Size = New System.Drawing.Size(75, 23)
            Me.Cancel.TabIndex = 1
            Me.Cancel.Text = "Cancel"
            Me.Cancel.UseVisualStyleBackColor = True
            '
            'FilterInput
            '
            Me.FilterInput.AcceptsTab = True
            Me.FilterInput.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                        Or System.Windows.Forms.AnchorStyles.Left) _
                        Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
            Me.FilterInput.Location = New System.Drawing.Point(12, 25)
            Me.FilterInput.Name = "FilterInput"
            Me.FilterInput.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.Vertical
            Me.FilterInput.Size = New System.Drawing.Size(384, 175)
            Me.FilterInput.TabIndex = 2
            Me.FilterInput.Text = ""
            '
            'FilterLabel
            '
            Me.FilterLabel.AutoSize = True
            Me.FilterLabel.Location = New System.Drawing.Point(9, 9)
            Me.FilterLabel.Name = "FilterLabel"
            Me.FilterLabel.Size = New System.Drawing.Size(32, 13)
            Me.FilterLabel.TabIndex = 3
            Me.FilterLabel.Text = "Filter:"
            '
            'CustomFilterForm
            '
            Me.AcceptButton = Me.OK
            Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
            Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
            Me.CancelButton = Me.Cancel
            Me.ClientSize = New System.Drawing.Size(408, 241)
            Me.Controls.Add(Me.FilterLabel)
            Me.Controls.Add(Me.FilterInput)
            Me.Controls.Add(Me.Cancel)
            Me.Controls.Add(Me.OK)
            Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
            Me.MaximizeBox = False
            Me.MinimizeBox = False
            Me.Name = "CustomFilterForm"
            Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
            Me.Text = "Huggle"
            Me.ResumeLayout(False)
            Me.PerformLayout()

        End Sub
        Private WithEvents OK As System.Windows.Forms.Button
        Private WithEvents Cancel As System.Windows.Forms.Button
        Private WithEvents FilterInput As System.Windows.Forms.RichTextBox
        Private WithEvents FilterLabel As System.Windows.Forms.Label
    End Class
End Namespace