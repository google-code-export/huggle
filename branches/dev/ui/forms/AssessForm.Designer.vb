Namespace Huggle.UI

    <Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
    Partial Class AssessForm
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
            Me.PageInput = New System.Windows.Forms.TextBox()
            Me.PageLabel = New System.Windows.Forms.Label()
            Me.Rating1Label = New System.Windows.Forms.Label()
            Me.Rating2Label = New System.Windows.Forms.Label()
            Me.Rating3Label = New System.Windows.Forms.Label()
            Me.Rating4Label = New System.Windows.Forms.Label()
            Me.Rating1 = New System.Windows.Forms.TextBox()
            Me.Rating2 = New System.Windows.Forms.TextBox()
            Me.Rating3 = New System.Windows.Forms.TextBox()
            Me.Rating4 = New System.Windows.Forms.TextBox()
            Me.Cancel = New System.Windows.Forms.Button()
            Me.OK = New System.Windows.Forms.Button()
            Me.SuspendLayout()
            '
            'PageInput
            '
            Me.PageInput.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                        Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
            Me.PageInput.Location = New System.Drawing.Point(62, 12)
            Me.PageInput.Name = "PageInput"
            Me.PageInput.Size = New System.Drawing.Size(210, 20)
            Me.PageInput.TabIndex = 0
            '
            'PageLabel
            '
            Me.PageLabel.AutoSize = True
            Me.PageLabel.Location = New System.Drawing.Point(24, 15)
            Me.PageLabel.Name = "PageLabel"
            Me.PageLabel.Size = New System.Drawing.Size(35, 13)
            Me.PageLabel.TabIndex = 1
            Me.PageLabel.Text = "Page:"
            '
            'Rating1Label
            '
            Me.Rating1Label.AutoSize = True
            Me.Rating1Label.Location = New System.Drawing.Point(9, 41)
            Me.Rating1Label.Name = "Rating1Label"
            Me.Rating1Label.Size = New System.Drawing.Size(50, 13)
            Me.Rating1Label.TabIndex = 2
            Me.Rating1Label.Text = "Rating 1:"
            '
            'Rating2Label
            '
            Me.Rating2Label.AutoSize = True
            Me.Rating2Label.Location = New System.Drawing.Point(9, 67)
            Me.Rating2Label.Name = "Rating2Label"
            Me.Rating2Label.Size = New System.Drawing.Size(50, 13)
            Me.Rating2Label.TabIndex = 3
            Me.Rating2Label.Text = "Rating 2:"
            '
            'Rating3Label
            '
            Me.Rating3Label.AutoSize = True
            Me.Rating3Label.Location = New System.Drawing.Point(9, 93)
            Me.Rating3Label.Name = "Rating3Label"
            Me.Rating3Label.Size = New System.Drawing.Size(50, 13)
            Me.Rating3Label.TabIndex = 4
            Me.Rating3Label.Text = "Rating 3:"
            '
            'Rating4Label
            '
            Me.Rating4Label.AutoSize = True
            Me.Rating4Label.Location = New System.Drawing.Point(9, 119)
            Me.Rating4Label.Name = "Rating4Label"
            Me.Rating4Label.Size = New System.Drawing.Size(50, 13)
            Me.Rating4Label.TabIndex = 5
            Me.Rating4Label.Text = "Rating 4:"
            '
            'Rating1
            '
            Me.Rating1.Location = New System.Drawing.Point(62, 38)
            Me.Rating1.Name = "Rating1"
            Me.Rating1.Size = New System.Drawing.Size(45, 20)
            Me.Rating1.TabIndex = 6
            Me.Rating1.Text = "0"
            '
            'Rating2
            '
            Me.Rating2.Location = New System.Drawing.Point(62, 64)
            Me.Rating2.Name = "Rating2"
            Me.Rating2.Size = New System.Drawing.Size(45, 20)
            Me.Rating2.TabIndex = 7
            Me.Rating2.Text = "0"
            '
            'Rating3
            '
            Me.Rating3.Location = New System.Drawing.Point(62, 90)
            Me.Rating3.Name = "Rating3"
            Me.Rating3.Size = New System.Drawing.Size(45, 20)
            Me.Rating3.TabIndex = 8
            Me.Rating3.Text = "0"
            '
            'Rating4
            '
            Me.Rating4.Location = New System.Drawing.Point(62, 116)
            Me.Rating4.Name = "Rating4"
            Me.Rating4.Size = New System.Drawing.Size(45, 20)
            Me.Rating4.TabIndex = 9
            Me.Rating4.Text = "0"
            '
            'Cancel
            '
            Me.Cancel.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
            Me.Cancel.DialogResult = System.Windows.Forms.DialogResult.Cancel
            Me.Cancel.Location = New System.Drawing.Point(197, 141)
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
            Me.OK.Location = New System.Drawing.Point(116, 141)
            Me.OK.Name = "OK"
            Me.OK.Size = New System.Drawing.Size(75, 23)
            Me.OK.TabIndex = 11
            Me.OK.Text = "OK"
            Me.OK.UseVisualStyleBackColor = True
            '
            'AssessForm
            '
            Me.AcceptButton = Me.OK
            Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
            Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
            Me.CancelButton = Me.Cancel
            Me.ClientSize = New System.Drawing.Size(284, 176)
            Me.Controls.Add(Me.OK)
            Me.Controls.Add(Me.Cancel)
            Me.Controls.Add(Me.Rating4)
            Me.Controls.Add(Me.Rating3)
            Me.Controls.Add(Me.Rating2)
            Me.Controls.Add(Me.Rating1)
            Me.Controls.Add(Me.Rating4Label)
            Me.Controls.Add(Me.Rating3Label)
            Me.Controls.Add(Me.Rating2Label)
            Me.Controls.Add(Me.Rating1Label)
            Me.Controls.Add(Me.PageLabel)
            Me.Controls.Add(Me.PageInput)
            Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
            Me.MaximizeBox = False
            Me.Name = "AssessForm"
            Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
            Me.Text = "Assess"
            Me.ResumeLayout(False)
            Me.PerformLayout()

        End Sub
        Private WithEvents PageInput As System.Windows.Forms.TextBox
        Private WithEvents PageLabel As System.Windows.Forms.Label
        Private WithEvents Rating1Label As System.Windows.Forms.Label
        Private WithEvents Rating2Label As System.Windows.Forms.Label
        Private WithEvents Rating3Label As System.Windows.Forms.Label
        Private WithEvents Rating4Label As System.Windows.Forms.Label
        Private WithEvents Rating1 As System.Windows.Forms.TextBox
        Private WithEvents Rating2 As System.Windows.Forms.TextBox
        Private WithEvents Rating3 As System.Windows.Forms.TextBox
        Private WithEvents Rating4 As System.Windows.Forms.TextBox
        Private WithEvents Cancel As System.Windows.Forms.Button
        Private WithEvents OK As System.Windows.Forms.Button
    End Class

End Namespace