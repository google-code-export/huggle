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
        Me.ConfirmationLabel = New System.Windows.Forms.Label
        Me.ConfirmationInput = New System.Windows.Forms.TextBox
        Me.ConfirmationImage = New System.Windows.Forms.PictureBox
        Me.Cancel = New System.Windows.Forms.Button
        Me.OK = New System.Windows.Forms.Button
        Me.Explanation = New System.Windows.Forms.Label
        CType(Me.ConfirmationImage, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'ConfirmationLabel
        '
        Me.ConfirmationLabel.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.ConfirmationLabel.AutoSize = True
        Me.ConfirmationLabel.Location = New System.Drawing.Point(12, 102)
        Me.ConfirmationLabel.Name = "ConfirmationLabel"
        Me.ConfirmationLabel.Size = New System.Drawing.Size(95, 13)
        Me.ConfirmationLabel.TabIndex = 14
        Me.ConfirmationLabel.Text = "Confirmation code:"
        '
        'ConfirmationInput
        '
        Me.ConfirmationInput.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ConfirmationInput.Location = New System.Drawing.Point(108, 99)
        Me.ConfirmationInput.Name = "ConfirmationInput"
        Me.ConfirmationInput.Size = New System.Drawing.Size(164, 20)
        Me.ConfirmationInput.TabIndex = 15
        '
        'ConfirmationImage
        '
        Me.ConfirmationImage.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ConfirmationImage.BackColor = System.Drawing.SystemColors.Control
        Me.ConfirmationImage.Location = New System.Drawing.Point(12, 29)
        Me.ConfirmationImage.Name = "ConfirmationImage"
        Me.ConfirmationImage.Size = New System.Drawing.Size(260, 64)
        Me.ConfirmationImage.TabIndex = 13
        Me.ConfirmationImage.TabStop = False
        '
        'Cancel
        '
        Me.Cancel.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Cancel.Location = New System.Drawing.Point(197, 125)
        Me.Cancel.Name = "Cancel"
        Me.Cancel.Size = New System.Drawing.Size(75, 23)
        Me.Cancel.TabIndex = 17
        Me.Cancel.Text = "Cancel"
        Me.Cancel.UseVisualStyleBackColor = True
        '
        'OK
        '
        Me.OK.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.OK.Location = New System.Drawing.Point(116, 125)
        Me.OK.Name = "OK"
        Me.OK.Size = New System.Drawing.Size(75, 23)
        Me.OK.TabIndex = 16
        Me.OK.Text = "OK"
        Me.OK.UseVisualStyleBackColor = True
        '
        'Explanation
        '
        Me.Explanation.AutoSize = True
        Me.Explanation.Location = New System.Drawing.Point(9, 9)
        Me.Explanation.Name = "Explanation"
        Me.Explanation.Size = New System.Drawing.Size(198, 13)
        Me.Explanation.TabIndex = 18
        Me.Explanation.Text = "This action requires a confirmation code."
        '
        'ConfirmationForm
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(284, 160)
        Me.Controls.Add(Me.Explanation)
        Me.Controls.Add(Me.ConfirmationLabel)
        Me.Controls.Add(Me.ConfirmationInput)
        Me.Controls.Add(Me.ConfirmationImage)
        Me.Controls.Add(Me.Cancel)
        Me.Controls.Add(Me.OK)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.MaximizeBox = False
        Me.Name = "ConfirmationForm"
        Me.Text = "Confirmation code required"
        CType(Me.ConfirmationImage, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Private WithEvents ConfirmationLabel As System.Windows.Forms.Label
    Private WithEvents ConfirmationInput As System.Windows.Forms.TextBox
    Private WithEvents ConfirmationImage As System.Windows.Forms.PictureBox
    Private WithEvents Cancel As System.Windows.Forms.Button
    Private WithEvents OK As System.Windows.Forms.Button
    Private WithEvents Explanation As System.Windows.Forms.Label
End Class
