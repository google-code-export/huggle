Namespace Huggle.UI
    <Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
    Partial Class FirstRunForm
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
            Me.ContinueButton = New System.Windows.Forms.Button
            Me.LanguageSelector = New System.Windows.Forms.ComboBox
            Me.LanguageLabel = New System.Windows.Forms.Label
            Me.Cancel = New System.Windows.Forms.Button
            Me.TranslateLabel = New System.Windows.Forms.LinkLabel
            Me.Proxy = New System.Windows.Forms.CheckBox
            Me.SuspendLayout()
            '
            'ContinueButton
            '
            Me.ContinueButton.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
            Me.ContinueButton.DialogResult = System.Windows.Forms.DialogResult.OK
            Me.ContinueButton.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.ContinueButton.Location = New System.Drawing.Point(141, 112)
            Me.ContinueButton.Name = "ContinueButton"
            Me.ContinueButton.Size = New System.Drawing.Size(75, 23)
            Me.ContinueButton.TabIndex = 2
            Me.ContinueButton.Text = "Continue"
            Me.ContinueButton.UseVisualStyleBackColor = True
            '
            'LanguageSelector
            '
            Me.LanguageSelector.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                        Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
            Me.LanguageSelector.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
            Me.LanguageSelector.Location = New System.Drawing.Point(94, 8)
            Me.LanguageSelector.MaxDropDownItems = 20
            Me.LanguageSelector.Name = "LanguageSelector"
            Me.LanguageSelector.Size = New System.Drawing.Size(203, 21)
            Me.LanguageSelector.Sorted = True
            Me.LanguageSelector.TabIndex = 1
            '
            'LanguageLabel
            '
            Me.LanguageLabel.Location = New System.Drawing.Point(4, 11)
            Me.LanguageLabel.Name = "LanguageLabel"
            Me.LanguageLabel.Size = New System.Drawing.Size(89, 17)
            Me.LanguageLabel.TabIndex = 0
            Me.LanguageLabel.Text = "Language:"
            Me.LanguageLabel.TextAlign = System.Drawing.ContentAlignment.TopRight
            '
            'Cancel
            '
            Me.Cancel.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
            Me.Cancel.DialogResult = System.Windows.Forms.DialogResult.Cancel
            Me.Cancel.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.Cancel.Location = New System.Drawing.Point(222, 112)
            Me.Cancel.Name = "Cancel"
            Me.Cancel.Size = New System.Drawing.Size(75, 23)
            Me.Cancel.TabIndex = 3
            Me.Cancel.Text = "Cancel"
            Me.Cancel.UseVisualStyleBackColor = True
            '
            'TranslateLabel
            '
            Me.TranslateLabel.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                        Or System.Windows.Forms.AnchorStyles.Left) _
                        Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
            Me.TranslateLabel.LinkArea = New System.Windows.Forms.LinkArea(76, 21)
            Me.TranslateLabel.LinkBehavior = System.Windows.Forms.LinkBehavior.HoverUnderline
            Me.TranslateLabel.LinkColor = System.Drawing.SystemColors.HotTrack
            Me.TranslateLabel.Location = New System.Drawing.Point(34, 39)
            Me.TranslateLabel.Name = "TranslateLabel"
            Me.TranslateLabel.Size = New System.Drawing.Size(263, 29)
            Me.TranslateLabel.TabIndex = 4
            Me.TranslateLabel.TabStop = True
            Me.TranslateLabel.Text = "If your native language is not listed or only partially translated, you can help " & _
                "with translation."
            Me.TranslateLabel.UseCompatibleTextRendering = True
            '
            'Proxy
            '
            Me.Proxy.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                        Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
            Me.Proxy.Location = New System.Drawing.Point(34, 71)
            Me.Proxy.Name = "Proxy"
            Me.Proxy.Size = New System.Drawing.Size(263, 35)
            Me.Proxy.TabIndex = 5
            Me.Proxy.Text = "I connect to the Internet through a proxy server, use my system proxy settings."
            Me.Proxy.UseVisualStyleBackColor = True
            '
            'FirstRunForm
            '
            Me.AcceptButton = Me.ContinueButton
            Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
            Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
            Me.CancelButton = Me.Cancel
            Me.ClientSize = New System.Drawing.Size(309, 147)
            Me.Controls.Add(Me.Proxy)
            Me.Controls.Add(Me.TranslateLabel)
            Me.Controls.Add(Me.LanguageSelector)
            Me.Controls.Add(Me.LanguageLabel)
            Me.Controls.Add(Me.Cancel)
            Me.Controls.Add(Me.ContinueButton)
            Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
            Me.MaximizeBox = False
            Me.MinimizeBox = False
            Me.Name = "FirstRunForm"
            Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
            Me.Text = "Huggle"
            Me.ResumeLayout(False)

        End Sub
        Private WithEvents ContinueButton As System.Windows.Forms.Button
        Private WithEvents LanguageSelector As System.Windows.Forms.ComboBox
        Private WithEvents LanguageLabel As System.Windows.Forms.Label
        Private WithEvents Cancel As System.Windows.Forms.Button
        Private WithEvents TranslateLabel As System.Windows.Forms.LinkLabel
        Private WithEvents Proxy As System.Windows.Forms.CheckBox
    End Class
End Namespace