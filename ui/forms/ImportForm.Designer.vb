Namespace Huggle.UI
    <Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
    Partial Class ImportForm
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
            Me.SourceWikiLabel = New System.Windows.Forms.Label
            Me.SourcePageLabel = New System.Windows.Forms.Label
            Me.DestWikiLabel = New System.Windows.Forms.Label
            Me.DestPageLabel = New System.Windows.Forms.Label
            Me.CurrentContent = New System.Windows.Forms.RadioButton
            Me.AllContent = New System.Windows.Forms.RadioButton
            Me.DestWikiInput = New System.Windows.Forms.ComboBox
            Me.SourceWikiInput = New System.Windows.Forms.ComboBox
            Me.SourcePageInput = New System.Windows.Forms.TextBox
            Me.DestPageInput = New System.Windows.Forms.TextBox
            Me.Cancel = New System.Windows.Forms.Button
            Me.OK = New System.Windows.Forms.Button
            Me.AttributeLink = New System.Windows.Forms.CheckBox
            Me.ImportNote = New System.Windows.Forms.Label
            Me.SuspendLayout()
            '
            'SourceWikiLabel
            '
            Me.SourceWikiLabel.Location = New System.Drawing.Point(9, 18)
            Me.SourceWikiLabel.Name = "SourceWikiLabel"
            Me.SourceWikiLabel.Size = New System.Drawing.Size(106, 18)
            Me.SourceWikiLabel.TabIndex = 0
            Me.SourceWikiLabel.Text = "Source wiki:"
            Me.SourceWikiLabel.TextAlign = System.Drawing.ContentAlignment.TopRight
            '
            'SourcePageLabel
            '
            Me.SourcePageLabel.Location = New System.Drawing.Point(9, 42)
            Me.SourcePageLabel.Name = "SourcePageLabel"
            Me.SourcePageLabel.Size = New System.Drawing.Size(106, 13)
            Me.SourcePageLabel.TabIndex = 0
            Me.SourcePageLabel.Text = "Source page:"
            Me.SourcePageLabel.TextAlign = System.Drawing.ContentAlignment.TopRight
            '
            'DestWikiLabel
            '
            Me.DestWikiLabel.Location = New System.Drawing.Point(9, 75)
            Me.DestWikiLabel.Name = "DestWikiLabel"
            Me.DestWikiLabel.Size = New System.Drawing.Size(106, 13)
            Me.DestWikiLabel.TabIndex = 0
            Me.DestWikiLabel.Text = "Destination wiki:"
            Me.DestWikiLabel.TextAlign = System.Drawing.ContentAlignment.TopRight
            '
            'DestPageLabel
            '
            Me.DestPageLabel.Location = New System.Drawing.Point(9, 98)
            Me.DestPageLabel.Name = "DestPageLabel"
            Me.DestPageLabel.Size = New System.Drawing.Size(106, 13)
            Me.DestPageLabel.TabIndex = 0
            Me.DestPageLabel.Text = "Destination page:"
            Me.DestPageLabel.TextAlign = System.Drawing.ContentAlignment.TopRight
            '
            'CurrentContent
            '
            Me.CurrentContent.AutoSize = True
            Me.CurrentContent.Location = New System.Drawing.Point(12, 130)
            Me.CurrentContent.Name = "CurrentContent"
            Me.CurrentContent.Size = New System.Drawing.Size(151, 17)
            Me.CurrentContent.TabIndex = 1
            Me.CurrentContent.TabStop = True
            Me.CurrentContent.Text = "Import current content only"
            Me.CurrentContent.UseVisualStyleBackColor = True
            '
            'AllContent
            '
            Me.AllContent.AutoSize = True
            Me.AllContent.Location = New System.Drawing.Point(12, 151)
            Me.AllContent.Name = "AllContent"
            Me.AllContent.Size = New System.Drawing.Size(114, 17)
            Me.AllContent.TabIndex = 1
            Me.AllContent.TabStop = True
            Me.AllContent.Text = "Import page history"
            Me.AllContent.UseVisualStyleBackColor = True
            '
            'DestWikiInput
            '
            Me.DestWikiInput.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                        Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
            Me.DestWikiInput.FormattingEnabled = True
            Me.DestWikiInput.Location = New System.Drawing.Point(116, 71)
            Me.DestWikiInput.Name = "DestWikiInput"
            Me.DestWikiInput.Size = New System.Drawing.Size(144, 21)
            Me.DestWikiInput.TabIndex = 2
            '
            'SourceWikiInput
            '
            Me.SourceWikiInput.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                        Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
            Me.SourceWikiInput.FormattingEnabled = True
            Me.SourceWikiInput.Location = New System.Drawing.Point(116, 15)
            Me.SourceWikiInput.Name = "SourceWikiInput"
            Me.SourceWikiInput.Size = New System.Drawing.Size(144, 21)
            Me.SourceWikiInput.TabIndex = 2
            '
            'SourcePageInput
            '
            Me.SourcePageInput.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                        Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
            Me.SourcePageInput.Location = New System.Drawing.Point(116, 39)
            Me.SourcePageInput.Name = "SourcePageInput"
            Me.SourcePageInput.Size = New System.Drawing.Size(144, 20)
            Me.SourcePageInput.TabIndex = 3
            '
            'DestPageInput
            '
            Me.DestPageInput.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                        Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
            Me.DestPageInput.Location = New System.Drawing.Point(116, 95)
            Me.DestPageInput.Name = "DestPageInput"
            Me.DestPageInput.Size = New System.Drawing.Size(144, 20)
            Me.DestPageInput.TabIndex = 3
            '
            'Cancel
            '
            Me.Cancel.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
            Me.Cancel.DialogResult = System.Windows.Forms.DialogResult.Cancel
            Me.Cancel.Location = New System.Drawing.Point(185, 250)
            Me.Cancel.Name = "Cancel"
            Me.Cancel.Size = New System.Drawing.Size(75, 23)
            Me.Cancel.TabIndex = 4
            Me.Cancel.Text = "Cancel"
            Me.Cancel.UseVisualStyleBackColor = True
            '
            'OK
            '
            Me.OK.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
            Me.OK.Location = New System.Drawing.Point(104, 250)
            Me.OK.Name = "OK"
            Me.OK.Size = New System.Drawing.Size(75, 23)
            Me.OK.TabIndex = 4
            Me.OK.Text = "OK"
            Me.OK.UseVisualStyleBackColor = True
            '
            'AttributeLink
            '
            Me.AttributeLink.AutoSize = True
            Me.AttributeLink.Location = New System.Drawing.Point(12, 222)
            Me.AttributeLink.Name = "AttributeLink"
            Me.AttributeLink.Size = New System.Drawing.Size(171, 17)
            Me.AttributeLink.TabIndex = 5
            Me.AttributeLink.Text = "Include attribution link on page"
            Me.AttributeLink.UseVisualStyleBackColor = True
            '
            'ImportNote
            '
            Me.ImportNote.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                        Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
            Me.ImportNote.Location = New System.Drawing.Point(9, 181)
            Me.ImportNote.Name = "ImportNote"
            Me.ImportNote.Size = New System.Drawing.Size(251, 30)
            Me.ImportNote.TabIndex = 6
            Me.ImportNote.Text = "Importing page history requires an account with import rights on destination wiki" & _
                "."
            '
            'ImportForm
            '
            Me.AcceptButton = Me.OK
            Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
            Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
            Me.CancelButton = Me.Cancel
            Me.ClientSize = New System.Drawing.Size(272, 285)
            Me.Controls.Add(Me.ImportNote)
            Me.Controls.Add(Me.AttributeLink)
            Me.Controls.Add(Me.OK)
            Me.Controls.Add(Me.Cancel)
            Me.Controls.Add(Me.DestPageInput)
            Me.Controls.Add(Me.SourcePageInput)
            Me.Controls.Add(Me.SourceWikiInput)
            Me.Controls.Add(Me.DestWikiInput)
            Me.Controls.Add(Me.AllContent)
            Me.Controls.Add(Me.CurrentContent)
            Me.Controls.Add(Me.DestPageLabel)
            Me.Controls.Add(Me.DestWikiLabel)
            Me.Controls.Add(Me.SourcePageLabel)
            Me.Controls.Add(Me.SourceWikiLabel)
            Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
            Me.MaximizeBox = False
            Me.Name = "ImportForm"
            Me.Text = "Import"
            Me.ResumeLayout(False)
            Me.PerformLayout()

        End Sub
        Private WithEvents SourceWikiLabel As System.Windows.Forms.Label
        Private WithEvents SourcePageLabel As System.Windows.Forms.Label
        Private WithEvents DestWikiLabel As System.Windows.Forms.Label
        Private WithEvents DestPageLabel As System.Windows.Forms.Label
        Private WithEvents Cancel As System.Windows.Forms.Button
        Private WithEvents OK As System.Windows.Forms.Button
        Private WithEvents AttributeLink As System.Windows.Forms.CheckBox
        Private WithEvents CurrentContent As System.Windows.Forms.RadioButton
        Private WithEvents AllContent As System.Windows.Forms.RadioButton
        Private WithEvents DestWikiInput As System.Windows.Forms.ComboBox
        Private WithEvents SourceWikiInput As System.Windows.Forms.ComboBox
        Private WithEvents SourcePageInput As System.Windows.Forms.TextBox
        Private WithEvents DestPageInput As System.Windows.Forms.TextBox
        Private WithEvents ImportNote As System.Windows.Forms.Label
    End Class
End Namespace