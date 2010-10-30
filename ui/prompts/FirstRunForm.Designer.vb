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
            Me.ContinueButton = New System.Windows.Forms.Button()
            Me.LanguageLabel = New System.Windows.Forms.Label()
            Me.Cancel = New System.Windows.Forms.Button()
            Me.Translate = New System.Windows.Forms.LinkLabel()
            Me.Proxy = New System.Windows.Forms.CheckBox()
            Me.MainLayout = New System.Windows.Forms.EnhancedFlowLayoutPanel()
            Me.LanguageLayout = New System.Windows.Forms.EnhancedFlowLayoutPanel()
            Me.LanguageSelector = New System.Windows.Forms.EnhancedComboBox()
            Me.DetectProxy = New System.Windows.Forms.RadioButton()
            Me.ManualProxy = New System.Windows.Forms.RadioButton()
            Me.ProxyLayout = New System.Windows.Forms.EnhancedFlowLayoutPanel()
            Me.ProxyHostLabel = New System.Windows.Forms.Label()
            Me.ProxyHost = New System.Windows.Forms.TextBox()
            Me.ProxyPortLabel = New System.Windows.Forms.Label()
            Me.ProxyPort = New System.Windows.Forms.NumericUpDown()
            Me.MainLayout.SuspendLayout()
            Me.LanguageLayout.SuspendLayout()
            Me.ProxyLayout.SuspendLayout()
            CType(Me.ProxyPort, System.ComponentModel.ISupportInitialize).BeginInit()
            Me.SuspendLayout()
            '
            'ContinueButton
            '
            Me.ContinueButton.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
            Me.ContinueButton.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
            Me.ContinueButton.DialogResult = System.Windows.Forms.DialogResult.OK
            Me.ContinueButton.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.ContinueButton.Location = New System.Drawing.Point(146, 186)
            Me.ContinueButton.Name = "ContinueButton"
            Me.ContinueButton.Size = New System.Drawing.Size(75, 23)
            Me.ContinueButton.TabIndex = 0
            Me.ContinueButton.Text = "Continue"
            Me.ContinueButton.UseVisualStyleBackColor = True
            '
            'LanguageLabel
            '
            Me.LanguageLabel.AutoSize = True
            Me.LanguageLabel.Location = New System.Drawing.Point(0, 6)
            Me.LanguageLabel.Margin = New System.Windows.Forms.Padding(0, 6, 0, 0)
            Me.LanguageLabel.Name = "LanguageLabel"
            Me.LanguageLabel.Size = New System.Drawing.Size(58, 13)
            Me.LanguageLabel.TabIndex = 0
            Me.LanguageLabel.Text = "Language:"
            Me.LanguageLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
            '
            'Cancel
            '
            Me.Cancel.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
            Me.Cancel.DialogResult = System.Windows.Forms.DialogResult.Cancel
            Me.Cancel.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.Cancel.Location = New System.Drawing.Point(227, 186)
            Me.Cancel.Name = "Cancel"
            Me.Cancel.Size = New System.Drawing.Size(75, 23)
            Me.Cancel.TabIndex = 1
            Me.Cancel.Text = "Cancel"
            Me.Cancel.UseVisualStyleBackColor = True
            '
            'Translate
            '
            Me.Translate.AutoSize = True
            Me.Translate.LinkArea = New System.Windows.Forms.LinkArea(76, 21)
            Me.Translate.LinkBehavior = System.Windows.Forms.LinkBehavior.HoverUnderline
            Me.Translate.LinkColor = System.Drawing.SystemColors.HotTrack
            Me.Translate.Location = New System.Drawing.Point(9, 39)
            Me.Translate.Margin = New System.Windows.Forms.Padding(3)
            Me.Translate.Name = "Translate"
            Me.Translate.Size = New System.Drawing.Size(260, 30)
            Me.Translate.TabIndex = 1
            Me.Translate.TabStop = True
            Me.Translate.Text = "If your native language is not listed or only partially translated, you can help " & _
                "with translation."
            Me.Translate.UseCompatibleTextRendering = True
            '
            'Proxy
            '
            Me.Proxy.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                        Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
            Me.Proxy.AutoSize = True
            Me.Proxy.Location = New System.Drawing.Point(9, 75)
            Me.Proxy.Name = "Proxy"
            Me.Proxy.Size = New System.Drawing.Size(286, 17)
            Me.Proxy.TabIndex = 2
            Me.Proxy.Text = "Connect to the Internet through a proxy server"
            Me.Proxy.UseVisualStyleBackColor = True
            '
            'MainLayout
            '
            Me.MainLayout.AutoSize = True
            Me.MainLayout.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
            Me.MainLayout.BackColor = System.Drawing.SystemColors.Control
            Me.MainLayout.Controls.Add(Me.LanguageLayout)
            Me.MainLayout.Controls.Add(Me.Translate)
            Me.MainLayout.Controls.Add(Me.Proxy)
            Me.MainLayout.Controls.Add(Me.DetectProxy)
            Me.MainLayout.Controls.Add(Me.ManualProxy)
            Me.MainLayout.Controls.Add(Me.ProxyLayout)
            Me.MainLayout.Dock = System.Windows.Forms.DockStyle.Top
            Me.MainLayout.FlowDirection = System.Windows.Forms.FlowDirection.TopDown
            Me.MainLayout.Location = New System.Drawing.Point(0, 0)
            Me.MainLayout.Margin = New System.Windows.Forms.Padding(0)
            Me.MainLayout.Name = "MainLayout"
            Me.MainLayout.Padding = New System.Windows.Forms.Padding(6, 3, 6, 3)
            Me.MainLayout.Size = New System.Drawing.Size(314, 176)
            Me.MainLayout.TabIndex = 0
            Me.MainLayout.WrapContents = False
            '
            'LanguageLayout
            '
            Me.LanguageLayout.AutoSize = True
            Me.LanguageLayout.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
            Me.LanguageLayout.Controls.Add(Me.LanguageLabel)
            Me.LanguageLayout.Controls.Add(Me.LanguageSelector)
            Me.LanguageLayout.Location = New System.Drawing.Point(9, 6)
            Me.LanguageLayout.Name = "LanguageLayout"
            Me.LanguageLayout.Size = New System.Drawing.Size(208, 27)
            Me.LanguageLayout.TabIndex = 0
            '
            'LanguageSelector
            '
            Me.LanguageSelector.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
            Me.LanguageSelector.FormattingEnabled = True
            Me.LanguageSelector.Location = New System.Drawing.Point(58, 3)
            Me.LanguageSelector.Margin = New System.Windows.Forms.Padding(0, 3, 0, 3)
            Me.LanguageSelector.Name = "LanguageSelector"
            Me.LanguageSelector.Size = New System.Drawing.Size(150, 21)
            Me.LanguageSelector.TabIndex = 1
            '
            'DetectProxy
            '
            Me.DetectProxy.AutoSize = True
            Me.DetectProxy.Checked = True
            Me.DetectProxy.Location = New System.Drawing.Point(30, 98)
            Me.DetectProxy.Margin = New System.Windows.Forms.Padding(24, 3, 3, 3)
            Me.DetectProxy.Name = "DetectProxy"
            Me.DetectProxy.Size = New System.Drawing.Size(200, 17)
            Me.DetectProxy.TabIndex = 3
            Me.DetectProxy.TabStop = True
            Me.DetectProxy.Text = "Detect and use system proxy settings"
            Me.DetectProxy.UseVisualStyleBackColor = True
            Me.DetectProxy.Visible = False
            '
            'ManualProxy
            '
            Me.ManualProxy.AutoSize = True
            Me.ManualProxy.Location = New System.Drawing.Point(30, 121)
            Me.ManualProxy.Margin = New System.Windows.Forms.Padding(24, 3, 3, 3)
            Me.ManualProxy.Name = "ManualProxy"
            Me.ManualProxy.Size = New System.Drawing.Size(148, 17)
            Me.ManualProxy.TabIndex = 4
            Me.ManualProxy.Text = "Use the following settings:"
            Me.ManualProxy.UseVisualStyleBackColor = True
            Me.ManualProxy.Visible = False
            '
            'ProxyLayout
            '
            Me.ProxyLayout.AutoSize = True
            Me.ProxyLayout.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
            Me.ProxyLayout.Controls.Add(Me.ProxyHostLabel)
            Me.ProxyLayout.Controls.Add(Me.ProxyHost)
            Me.ProxyLayout.Controls.Add(Me.ProxyPortLabel)
            Me.ProxyLayout.Controls.Add(Me.ProxyPort)
            Me.ProxyLayout.Enabled = False
            Me.ProxyLayout.Location = New System.Drawing.Point(30, 144)
            Me.ProxyLayout.Margin = New System.Windows.Forms.Padding(24, 3, 3, 3)
            Me.ProxyLayout.Name = "ProxyLayout"
            Me.ProxyLayout.Size = New System.Drawing.Size(265, 26)
            Me.ProxyLayout.TabIndex = 5
            Me.ProxyLayout.Visible = False
            '
            'ProxyHostLabel
            '
            Me.ProxyHostLabel.AutoSize = True
            Me.ProxyHostLabel.Location = New System.Drawing.Point(0, 6)
            Me.ProxyHostLabel.Margin = New System.Windows.Forms.Padding(0, 6, 0, 0)
            Me.ProxyHostLabel.Name = "ProxyHostLabel"
            Me.ProxyHostLabel.Size = New System.Drawing.Size(41, 13)
            Me.ProxyHostLabel.TabIndex = 0
            Me.ProxyHostLabel.Text = "Server:"
            Me.ProxyHostLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
            '
            'ProxyHost
            '
            Me.ProxyHost.Location = New System.Drawing.Point(41, 3)
            Me.ProxyHost.Margin = New System.Windows.Forms.Padding(0, 3, 0, 3)
            Me.ProxyHost.Name = "ProxyHost"
            Me.ProxyHost.Size = New System.Drawing.Size(133, 20)
            Me.ProxyHost.TabIndex = 1
            '
            'ProxyPortLabel
            '
            Me.ProxyPortLabel.AutoSize = True
            Me.ProxyPortLabel.Location = New System.Drawing.Point(180, 6)
            Me.ProxyPortLabel.Margin = New System.Windows.Forms.Padding(6, 6, 0, 0)
            Me.ProxyPortLabel.Name = "ProxyPortLabel"
            Me.ProxyPortLabel.Size = New System.Drawing.Size(29, 13)
            Me.ProxyPortLabel.TabIndex = 2
            Me.ProxyPortLabel.Text = "Port:"
            Me.ProxyPortLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
            '
            'ProxyPort
            '
            Me.ProxyPort.Location = New System.Drawing.Point(209, 3)
            Me.ProxyPort.Margin = New System.Windows.Forms.Padding(0, 3, 0, 3)
            Me.ProxyPort.Maximum = New Decimal(New Integer() {65535, 0, 0, 0})
            Me.ProxyPort.Name = "ProxyPort"
            Me.ProxyPort.Size = New System.Drawing.Size(56, 20)
            Me.ProxyPort.TabIndex = 3
            Me.ProxyPort.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
            Me.ProxyPort.Value = New Decimal(New Integer() {80, 0, 0, 0})
            '
            'FirstRunForm
            '
            Me.AcceptButton = Me.ContinueButton
            Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
            Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
            Me.CancelButton = Me.Cancel
            Me.ClientSize = New System.Drawing.Size(314, 221)
            Me.Controls.Add(Me.Cancel)
            Me.Controls.Add(Me.ContinueButton)
            Me.Controls.Add(Me.MainLayout)
            Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
            Me.MaximizeBox = False
            Me.MinimizeBox = False
            Me.Name = "FirstRunForm"
            Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
            Me.Text = "Huggle"
            Me.MainLayout.ResumeLayout(False)
            Me.MainLayout.PerformLayout()
            Me.LanguageLayout.ResumeLayout(False)
            Me.LanguageLayout.PerformLayout()
            Me.ProxyLayout.ResumeLayout(False)
            Me.ProxyLayout.PerformLayout()
            CType(Me.ProxyPort, System.ComponentModel.ISupportInitialize).EndInit()
            Me.ResumeLayout(False)
            Me.PerformLayout()

        End Sub
        Private WithEvents ContinueButton As System.Windows.Forms.Button
        Private WithEvents LanguageLabel As System.Windows.Forms.Label
        Private WithEvents Cancel As System.Windows.Forms.Button
        Private WithEvents Translate As System.Windows.Forms.LinkLabel
        Private WithEvents Proxy As System.Windows.Forms.CheckBox
        Private WithEvents MainLayout As System.Windows.Forms.EnhancedFlowLayoutPanel
        Private WithEvents LanguageLayout As System.Windows.Forms.EnhancedFlowLayoutPanel
        Private WithEvents LanguageSelector As System.Windows.Forms.EnhancedComboBox
        Private WithEvents DetectProxy As System.Windows.Forms.RadioButton
        Private WithEvents ManualProxy As System.Windows.Forms.RadioButton
        Private WithEvents ProxyLayout As System.Windows.Forms.EnhancedFlowLayoutPanel
        Private WithEvents ProxyHostLabel As System.Windows.Forms.Label
        Private WithEvents ProxyPortLabel As System.Windows.Forms.Label
        Private WithEvents ProxyHost As System.Windows.Forms.TextBox
        Private WithEvents ProxyPort As System.Windows.Forms.NumericUpDown
    End Class
End Namespace