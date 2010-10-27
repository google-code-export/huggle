Namespace Huggle.UI
    <Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
    Partial Class ReviewForm
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
            Me.OK = New System.Windows.Forms.Button
            Me.Cancel = New System.Windows.Forms.Button
            Me.CommentField = New System.Windows.Forms.TextBox
            Me.LevelPanel = New System.Windows.Forms.FlowLayoutPanel
            Me.CommentLabel = New System.Windows.Forms.Label
            Me.SuspendLayout()
            '
            'OK
            '
            Me.OK.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
            Me.OK.DialogResult = System.Windows.Forms.DialogResult.OK
            Me.OK.Location = New System.Drawing.Point(116, 165)
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
            Me.Cancel.Location = New System.Drawing.Point(197, 165)
            Me.Cancel.Name = "Cancel"
            Me.Cancel.Size = New System.Drawing.Size(75, 23)
            Me.Cancel.TabIndex = 0
            Me.Cancel.Text = "Cancel"
            Me.Cancel.UseVisualStyleBackColor = True
            '
            'CommentField
            '
            Me.CommentField.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left) _
                        Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
            Me.CommentField.Location = New System.Drawing.Point(12, 114)
            Me.CommentField.MaxLength = 250
            Me.CommentField.Multiline = True
            Me.CommentField.Name = "CommentField"
            Me.CommentField.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
            Me.CommentField.Size = New System.Drawing.Size(260, 45)
            Me.CommentField.TabIndex = 1
            '
            'LevelPanel
            '
            Me.LevelPanel.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                        Or System.Windows.Forms.AnchorStyles.Left) _
                        Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
            Me.LevelPanel.Location = New System.Drawing.Point(12, 12)
            Me.LevelPanel.Name = "LevelPanel"
            Me.LevelPanel.Size = New System.Drawing.Size(260, 83)
            Me.LevelPanel.TabIndex = 2
            '
            'CommentLabel
            '
            Me.CommentLabel.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
            Me.CommentLabel.AutoSize = True
            Me.CommentLabel.Location = New System.Drawing.Point(12, 98)
            Me.CommentLabel.Name = "CommentLabel"
            Me.CommentLabel.Size = New System.Drawing.Size(54, 13)
            Me.CommentLabel.TabIndex = 3
            Me.CommentLabel.Text = "Comment:"
            '
            'ReviewForm
            '
            Me.AcceptButton = Me.OK
            Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
            Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
            Me.CancelButton = Me.Cancel
            Me.ClientSize = New System.Drawing.Size(284, 200)
            Me.Controls.Add(Me.CommentLabel)
            Me.Controls.Add(Me.LevelPanel)
            Me.Controls.Add(Me.CommentField)
            Me.Controls.Add(Me.Cancel)
            Me.Controls.Add(Me.OK)
            Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
            Me.MaximizeBox = False
            Me.Name = "ReviewForm"
            Me.Text = "Review"
            Me.ResumeLayout(False)
            Me.PerformLayout()

        End Sub
        Private WithEvents OK As System.Windows.Forms.Button
        Private WithEvents Cancel As System.Windows.Forms.Button
        Private WithEvents CommentField As System.Windows.Forms.TextBox
        Private WithEvents LevelPanel As System.Windows.Forms.FlowLayoutPanel
        Private WithEvents CommentLabel As System.Windows.Forms.Label
    End Class
End Namespace