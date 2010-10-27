Namespace Huggle.UI
    <Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
    Partial Class ConfirmationForm
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
            Me.ConfirmLabel = New System.Windows.Forms.Label()
            Me.ConfirmInput = New System.Windows.Forms.TextBox()
            Me.ConfirmImage = New System.Windows.Forms.PictureBox()
            Me.Cancel = New System.Windows.Forms.Button()
            Me.OK = New System.Windows.Forms.Button()
            Me.Explanation = New System.Windows.Forms.Label()
            Me.ConfirmRefresh = New System.Windows.Forms.LinkLabel()
            CType(Me.ConfirmImage, System.ComponentModel.ISupportInitialize).BeginInit()
            Me.SuspendLayout()
            '
            'ConfirmLabel
            '
            Me.ConfirmLabel.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
            Me.ConfirmLabel.AutoSize = True
            Me.ConfirmLabel.Location = New System.Drawing.Point(12, 102)
            Me.ConfirmLabel.Name = "ConfirmLabel"
            Me.ConfirmLabel.Size = New System.Drawing.Size(95, 13)
            Me.ConfirmLabel.TabIndex = 2
            Me.ConfirmLabel.Text = "Confirmation code:"
            '
            'ConfirmInput
            '
            Me.ConfirmInput.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left) _
                        Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
            Me.ConfirmInput.Location = New System.Drawing.Point(108, 99)
            Me.ConfirmInput.Name = "ConfirmInput"
            Me.ConfirmInput.Size = New System.Drawing.Size(164, 20)
            Me.ConfirmInput.TabIndex = 3
            '
            'ConfirmImage
            '
            Me.ConfirmImage.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                        Or System.Windows.Forms.AnchorStyles.Left) _
                        Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
            Me.ConfirmImage.BackColor = System.Drawing.SystemColors.Control
            Me.ConfirmImage.Location = New System.Drawing.Point(12, 29)
            Me.ConfirmImage.Name = "ConfirmImage"
            Me.ConfirmImage.Size = New System.Drawing.Size(260, 64)
            Me.ConfirmImage.TabIndex = 13
            Me.ConfirmImage.TabStop = False
            '
            'Cancel
            '
            Me.Cancel.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
            Me.Cancel.Location = New System.Drawing.Point(197, 125)
            Me.Cancel.Name = "Cancel"
            Me.Cancel.Size = New System.Drawing.Size(75, 23)
            Me.Cancel.TabIndex = 5
            Me.Cancel.Text = "Cancel"
            Me.Cancel.UseVisualStyleBackColor = True
            '
            'OK
            '
            Me.OK.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
            Me.OK.Location = New System.Drawing.Point(116, 125)
            Me.OK.Name = "OK"
            Me.OK.Size = New System.Drawing.Size(75, 23)
            Me.OK.TabIndex = 4
            Me.OK.Text = "OK"
            Me.OK.UseVisualStyleBackColor = True
            '
            'Explanation
            '
            Me.Explanation.AutoSize = True
            Me.Explanation.Location = New System.Drawing.Point(9, 9)
            Me.Explanation.Name = "Explanation"
            Me.Explanation.Size = New System.Drawing.Size(198, 13)
            Me.Explanation.TabIndex = 0
            Me.Explanation.Text = "This action requires a confirmation code."
            '
            'ConfirmRefresh
            '
            Me.ConfirmRefresh.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
            Me.ConfirmRefresh.AutoSize = True
            Me.ConfirmRefresh.BackColor = System.Drawing.Color.White
            Me.ConfirmRefresh.LinkBehavior = System.Windows.Forms.LinkBehavior.HoverUnderline
            Me.ConfirmRefresh.LinkColor = System.Drawing.SystemColors.HotTrack
            Me.ConfirmRefresh.Location = New System.Drawing.Point(228, 80)
            Me.ConfirmRefresh.Name = "ConfirmRefresh"
            Me.ConfirmRefresh.Size = New System.Drawing.Size(44, 13)
            Me.ConfirmRefresh.TabIndex = 1
            Me.ConfirmRefresh.TabStop = True
            Me.ConfirmRefresh.Text = "Refresh"
            '
            'ConfirmationForm
            '
            Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
            Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
            Me.ClientSize = New System.Drawing.Size(284, 160)
            Me.Controls.Add(Me.ConfirmRefresh)
            Me.Controls.Add(Me.Explanation)
            Me.Controls.Add(Me.ConfirmLabel)
            Me.Controls.Add(Me.ConfirmInput)
            Me.Controls.Add(Me.ConfirmImage)
            Me.Controls.Add(Me.Cancel)
            Me.Controls.Add(Me.OK)
            Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
            Me.MaximizeBox = False
            Me.Name = "ConfirmationForm"
            Me.Text = "Confirmation code required"
            CType(Me.ConfirmImage, System.ComponentModel.ISupportInitialize).EndInit()
            Me.ResumeLayout(False)
            Me.PerformLayout()

        End Sub
        Private WithEvents ConfirmLabel As System.Windows.Forms.Label
        Private WithEvents ConfirmInput As System.Windows.Forms.TextBox
        Private WithEvents ConfirmImage As System.Windows.Forms.PictureBox
        Private WithEvents Cancel As System.Windows.Forms.Button
        Private WithEvents OK As System.Windows.Forms.Button
        Private WithEvents Explanation As System.Windows.Forms.Label
        Private WithEvents ConfirmRefresh As System.Windows.Forms.LinkLabel
    End Class
End Namespace