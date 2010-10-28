Namespace Huggle.UI

    <Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
    Partial Class AccountPreferencesForm
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
            Me.GeneralTab = New System.Windows.Forms.TabPage()
            Me.InterfaceLanguage = New System.Windows.Forms.ComboBox()
            Me.CcMe = New System.Windows.Forms.CheckBox()
            Me.Gender = New System.Windows.Forms.ComboBox()
            Me.EnableEmail = New System.Windows.Forms.CheckBox()
            Me.GenderLabel = New System.Windows.Forms.Label()
            Me.RawSignature = New System.Windows.Forms.CheckBox()
            Me.InterfaceLanguageLabel = New System.Windows.Forms.Label()
            Me.EmailAddress = New System.Windows.Forms.TextBox()
            Me.SignatureLabel = New System.Windows.Forms.Label()
            Me.Signature = New System.Windows.Forms.TextBox()
            Me.EmailAddressLabel = New System.Windows.Forms.Label()
            Me.Tabs = New System.Windows.Forms.TabControl()
            Me.AppearenceTab = New System.Windows.Forms.TabPage()
            Me.StubThreshold = New System.Windows.Forms.NumericUpDown()
            Me.AlternateLinks = New System.Windows.Forms.CheckBox()
            Me.StubThresholdSelect = New System.Windows.Forms.CheckBox()
            Me.Justify = New System.Windows.Forms.CheckBox()
            Me.JumpLinks = New System.Windows.Forms.CheckBox()
            Me.NumberHeadings = New System.Windows.Forms.CheckBox()
            Me.HiddenCategories = New System.Windows.Forms.CheckBox()
            Me.DisableCaching = New System.Windows.Forms.CheckBox()
            Me.Toc = New System.Windows.Forms.CheckBox()
            Me.UnderlineLinks = New System.Windows.Forms.ComboBox()
            Me.UnderlineLinksLabel = New System.Windows.Forms.Label()
            Me.MathOptionLabel = New System.Windows.Forms.Label()
            Me.MathOption = New System.Windows.Forms.ComboBox()
            Me.ThumbnailSize = New System.Windows.Forms.ComboBox()
            Me.ThumbnailSizeLabel = New System.Windows.Forms.Label()
            Me.ImageSize = New System.Windows.Forms.ComboBox()
            Me.ImageSizeLabel = New System.Windows.Forms.Label()
            Me.SkinsLabel = New System.Windows.Forms.Label()
            Me.Skins = New System.Windows.Forms.ComboBox()
            Me.GadgetsTab = New System.Windows.Forms.TabPage()
            Me.GadgetsLabel = New System.Windows.Forms.Label()
            Me.Gadgets = New System.Windows.Forms.CheckedListBox()
            Me.OtherTab = New System.Windows.Forms.TabPage()
            Me.ListViewEx1 = New System.Windows.Forms.EnhancedListView()
            Me.Label1 = New System.Windows.Forms.Label()
            Me.CancelBtn = New System.Windows.Forms.Button()
            Me.Save = New System.Windows.Forms.Button()
            Me.Defaults = New System.Windows.Forms.Button()
            Me.GeneralTab.SuspendLayout()
            Me.Tabs.SuspendLayout()
            Me.AppearenceTab.SuspendLayout()
            CType(Me.StubThreshold, System.ComponentModel.ISupportInitialize).BeginInit()
            Me.GadgetsTab.SuspendLayout()
            Me.OtherTab.SuspendLayout()
            Me.SuspendLayout()
            '
            'GeneralTab
            '
            Me.GeneralTab.Controls.Add(Me.InterfaceLanguage)
            Me.GeneralTab.Controls.Add(Me.CcMe)
            Me.GeneralTab.Controls.Add(Me.Gender)
            Me.GeneralTab.Controls.Add(Me.EnableEmail)
            Me.GeneralTab.Controls.Add(Me.GenderLabel)
            Me.GeneralTab.Controls.Add(Me.RawSignature)
            Me.GeneralTab.Controls.Add(Me.InterfaceLanguageLabel)
            Me.GeneralTab.Controls.Add(Me.EmailAddress)
            Me.GeneralTab.Controls.Add(Me.SignatureLabel)
            Me.GeneralTab.Controls.Add(Me.Signature)
            Me.GeneralTab.Controls.Add(Me.EmailAddressLabel)
            Me.GeneralTab.Location = New System.Drawing.Point(4, 22)
            Me.GeneralTab.Name = "GeneralTab"
            Me.GeneralTab.Padding = New System.Windows.Forms.Padding(3)
            Me.GeneralTab.Size = New System.Drawing.Size(442, 293)
            Me.GeneralTab.TabIndex = 0
            Me.GeneralTab.Text = "General"
            Me.GeneralTab.UseVisualStyleBackColor = True
            '
            'InterfaceLanguage
            '
            Me.InterfaceLanguage.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
            Me.InterfaceLanguage.FormattingEnabled = True
            Me.InterfaceLanguage.Location = New System.Drawing.Point(118, 16)
            Me.InterfaceLanguage.Name = "InterfaceLanguage"
            Me.InterfaceLanguage.Size = New System.Drawing.Size(165, 21)
            Me.InterfaceLanguage.TabIndex = 0
            '
            'CcMe
            '
            Me.CcMe.AutoSize = True
            Me.CcMe.Location = New System.Drawing.Point(118, 145)
            Me.CcMe.Name = "CcMe"
            Me.CcMe.Size = New System.Drawing.Size(309, 17)
            Me.CcMe.TabIndex = 5
            Me.CcMe.Text = "When I e-mail another user, send me a copy of the message"
            Me.CcMe.UseVisualStyleBackColor = True
            '
            'Gender
            '
            Me.Gender.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
            Me.Gender.FormattingEnabled = True
            Me.Gender.Location = New System.Drawing.Point(118, 43)
            Me.Gender.Name = "Gender"
            Me.Gender.Size = New System.Drawing.Size(108, 21)
            Me.Gender.TabIndex = 0
            '
            'EnableEmail
            '
            Me.EnableEmail.AutoSize = True
            Me.EnableEmail.Location = New System.Drawing.Point(118, 122)
            Me.EnableEmail.Name = "EnableEmail"
            Me.EnableEmail.Size = New System.Drawing.Size(165, 17)
            Me.EnableEmail.TabIndex = 5
            Me.EnableEmail.Text = "Allow other users to e-mail me"
            Me.EnableEmail.UseVisualStyleBackColor = True
            '
            'GenderLabel
            '
            Me.GenderLabel.AutoSize = True
            Me.GenderLabel.Location = New System.Drawing.Point(70, 46)
            Me.GenderLabel.Name = "GenderLabel"
            Me.GenderLabel.Size = New System.Drawing.Size(45, 13)
            Me.GenderLabel.TabIndex = 1
            Me.GenderLabel.Text = "Gender:"
            '
            'RawSignature
            '
            Me.RawSignature.AutoSize = True
            Me.RawSignature.Location = New System.Drawing.Point(289, 73)
            Me.RawSignature.Name = "RawSignature"
            Me.RawSignature.Size = New System.Drawing.Size(43, 17)
            Me.RawSignature.TabIndex = 4
            Me.RawSignature.Text = "raw"
            Me.RawSignature.UseVisualStyleBackColor = True
            '
            'InterfaceLanguageLabel
            '
            Me.InterfaceLanguageLabel.AutoSize = True
            Me.InterfaceLanguageLabel.Location = New System.Drawing.Point(16, 19)
            Me.InterfaceLanguageLabel.Name = "InterfaceLanguageLabel"
            Me.InterfaceLanguageLabel.Size = New System.Drawing.Size(99, 13)
            Me.InterfaceLanguageLabel.TabIndex = 1
            Me.InterfaceLanguageLabel.Text = "Interface language:"
            '
            'EmailAddress
            '
            Me.EmailAddress.Location = New System.Drawing.Point(118, 96)
            Me.EmailAddress.Name = "EmailAddress"
            Me.EmailAddress.Size = New System.Drawing.Size(165, 20)
            Me.EmailAddress.TabIndex = 3
            '
            'SignatureLabel
            '
            Me.SignatureLabel.AutoSize = True
            Me.SignatureLabel.Location = New System.Drawing.Point(60, 73)
            Me.SignatureLabel.Name = "SignatureLabel"
            Me.SignatureLabel.Size = New System.Drawing.Size(55, 13)
            Me.SignatureLabel.TabIndex = 2
            Me.SignatureLabel.Text = "Signature:"
            '
            'Signature
            '
            Me.Signature.Location = New System.Drawing.Point(118, 70)
            Me.Signature.Name = "Signature"
            Me.Signature.Size = New System.Drawing.Size(165, 20)
            Me.Signature.TabIndex = 3
            '
            'EmailAddressLabel
            '
            Me.EmailAddressLabel.AutoSize = True
            Me.EmailAddressLabel.Location = New System.Drawing.Point(37, 99)
            Me.EmailAddressLabel.Name = "EmailAddressLabel"
            Me.EmailAddressLabel.Size = New System.Drawing.Size(78, 13)
            Me.EmailAddressLabel.TabIndex = 2
            Me.EmailAddressLabel.Text = "E-mail address:"
            '
            'Tabs
            '
            Me.Tabs.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                        Or System.Windows.Forms.AnchorStyles.Left) _
                        Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
            Me.Tabs.Controls.Add(Me.GeneralTab)
            Me.Tabs.Controls.Add(Me.AppearenceTab)
            Me.Tabs.Controls.Add(Me.GadgetsTab)
            Me.Tabs.Controls.Add(Me.OtherTab)
            Me.Tabs.Location = New System.Drawing.Point(12, 12)
            Me.Tabs.Name = "Tabs"
            Me.Tabs.SelectedIndex = 0
            Me.Tabs.Size = New System.Drawing.Size(450, 319)
            Me.Tabs.TabIndex = 6
            '
            'AppearenceTab
            '
            Me.AppearenceTab.Controls.Add(Me.StubThreshold)
            Me.AppearenceTab.Controls.Add(Me.AlternateLinks)
            Me.AppearenceTab.Controls.Add(Me.StubThresholdSelect)
            Me.AppearenceTab.Controls.Add(Me.Justify)
            Me.AppearenceTab.Controls.Add(Me.JumpLinks)
            Me.AppearenceTab.Controls.Add(Me.NumberHeadings)
            Me.AppearenceTab.Controls.Add(Me.HiddenCategories)
            Me.AppearenceTab.Controls.Add(Me.DisableCaching)
            Me.AppearenceTab.Controls.Add(Me.Toc)
            Me.AppearenceTab.Controls.Add(Me.UnderlineLinks)
            Me.AppearenceTab.Controls.Add(Me.UnderlineLinksLabel)
            Me.AppearenceTab.Controls.Add(Me.MathOptionLabel)
            Me.AppearenceTab.Controls.Add(Me.MathOption)
            Me.AppearenceTab.Controls.Add(Me.ThumbnailSize)
            Me.AppearenceTab.Controls.Add(Me.ThumbnailSizeLabel)
            Me.AppearenceTab.Controls.Add(Me.ImageSize)
            Me.AppearenceTab.Controls.Add(Me.ImageSizeLabel)
            Me.AppearenceTab.Controls.Add(Me.SkinsLabel)
            Me.AppearenceTab.Controls.Add(Me.Skins)
            Me.AppearenceTab.Location = New System.Drawing.Point(4, 22)
            Me.AppearenceTab.Name = "AppearenceTab"
            Me.AppearenceTab.Padding = New System.Windows.Forms.Padding(3)
            Me.AppearenceTab.Size = New System.Drawing.Size(442, 293)
            Me.AppearenceTab.TabIndex = 2
            Me.AppearenceTab.Text = "Appearence"
            Me.AppearenceTab.UseVisualStyleBackColor = True
            '
            'StubThreshold
            '
            Me.StubThreshold.Increment = New Decimal(New Integer() {1000, 0, 0, 0})
            Me.StubThreshold.Location = New System.Drawing.Point(259, 162)
            Me.StubThreshold.Maximum = New Decimal(New Integer() {100000, 0, 0, 0})
            Me.StubThreshold.Name = "StubThreshold"
            Me.StubThreshold.Size = New System.Drawing.Size(69, 20)
            Me.StubThreshold.TabIndex = 19
            '
            'AlternateLinks
            '
            Me.AlternateLinks.AutoSize = True
            Me.AlternateLinks.Location = New System.Drawing.Point(20, 191)
            Me.AlternateLinks.Name = "AlternateLinks"
            Me.AlternateLinks.Size = New System.Drawing.Size(316, 17)
            Me.AlternateLinks.TabIndex = 10
            Me.AlternateLinks.Text = "Show links to non-existent pages with a ? instead of a red link"
            Me.AlternateLinks.UseVisualStyleBackColor = True
            '
            'StubThresholdSelect
            '
            Me.StubThresholdSelect.Location = New System.Drawing.Point(20, 152)
            Me.StubThresholdSelect.Name = "StubThresholdSelect"
            Me.StubThresholdSelect.Size = New System.Drawing.Size(248, 39)
            Me.StubThresholdSelect.TabIndex = 17
            Me.StubThresholdSelect.Text = "Show links to small pages in a different color, where ""small"" is shorter than thi" & _
                "s many bytes:"
            Me.StubThresholdSelect.UseVisualStyleBackColor = True
            '
            'Justify
            '
            Me.Justify.AutoSize = True
            Me.Justify.Location = New System.Drawing.Point(219, 237)
            Me.Justify.Name = "Justify"
            Me.Justify.Size = New System.Drawing.Size(111, 17)
            Me.Justify.TabIndex = 16
            Me.Justify.Text = "Justify paragraphs"
            Me.Justify.UseVisualStyleBackColor = True
            '
            'JumpLinks
            '
            Me.JumpLinks.AutoSize = True
            Me.JumpLinks.Location = New System.Drawing.Point(219, 214)
            Me.JumpLinks.Name = "JumpLinks"
            Me.JumpLinks.Size = New System.Drawing.Size(124, 17)
            Me.JumpLinks.TabIndex = 15
            Me.JumpLinks.Text = "Show ""jump to"" links"
            Me.JumpLinks.UseVisualStyleBackColor = True
            '
            'NumberHeadings
            '
            Me.NumberHeadings.AutoSize = True
            Me.NumberHeadings.Location = New System.Drawing.Point(219, 260)
            Me.NumberHeadings.Name = "NumberHeadings"
            Me.NumberHeadings.Size = New System.Drawing.Size(109, 17)
            Me.NumberHeadings.TabIndex = 14
            Me.NumberHeadings.Text = "Number headings"
            Me.NumberHeadings.UseVisualStyleBackColor = True
            '
            'HiddenCategories
            '
            Me.HiddenCategories.AutoSize = True
            Me.HiddenCategories.Location = New System.Drawing.Point(20, 260)
            Me.HiddenCategories.Name = "HiddenCategories"
            Me.HiddenCategories.Size = New System.Drawing.Size(140, 17)
            Me.HiddenCategories.TabIndex = 13
            Me.HiddenCategories.Text = "Show hidden categories"
            Me.HiddenCategories.UseVisualStyleBackColor = True
            '
            'DisableCaching
            '
            Me.DisableCaching.AutoSize = True
            Me.DisableCaching.Location = New System.Drawing.Point(20, 237)
            Me.DisableCaching.Name = "DisableCaching"
            Me.DisableCaching.Size = New System.Drawing.Size(102, 17)
            Me.DisableCaching.TabIndex = 12
            Me.DisableCaching.Text = "Disable caching"
            Me.DisableCaching.UseVisualStyleBackColor = True
            '
            'Toc
            '
            Me.Toc.AutoSize = True
            Me.Toc.Location = New System.Drawing.Point(20, 214)
            Me.Toc.Name = "Toc"
            Me.Toc.Size = New System.Drawing.Size(135, 17)
            Me.Toc.TabIndex = 11
            Me.Toc.Text = "Show table of contents"
            Me.Toc.UseVisualStyleBackColor = True
            '
            'UnderlineLinks
            '
            Me.UnderlineLinks.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
            Me.UnderlineLinks.FormattingEnabled = True
            Me.UnderlineLinks.Items.AddRange(New Object() {"Browser setting (default)", "No", "Yes"})
            Me.UnderlineLinks.Location = New System.Drawing.Point(152, 124)
            Me.UnderlineLinks.MaxDropDownItems = 20
            Me.UnderlineLinks.Name = "UnderlineLinks"
            Me.UnderlineLinks.Size = New System.Drawing.Size(131, 21)
            Me.UnderlineLinks.Sorted = True
            Me.UnderlineLinks.TabIndex = 9
            '
            'UnderlineLinksLabel
            '
            Me.UnderlineLinksLabel.AutoSize = True
            Me.UnderlineLinksLabel.Location = New System.Drawing.Point(71, 127)
            Me.UnderlineLinksLabel.Name = "UnderlineLinksLabel"
            Me.UnderlineLinksLabel.Size = New System.Drawing.Size(79, 13)
            Me.UnderlineLinksLabel.TabIndex = 8
            Me.UnderlineLinksLabel.Text = "Underline links:"
            '
            'MathOptionLabel
            '
            Me.MathOptionLabel.AutoSize = True
            Me.MathOptionLabel.Location = New System.Drawing.Point(81, 100)
            Me.MathOptionLabel.Name = "MathOptionLabel"
            Me.MathOptionLabel.Size = New System.Drawing.Size(69, 13)
            Me.MathOptionLabel.TabIndex = 7
            Me.MathOptionLabel.Text = "Math display:"
            '
            'MathOption
            '
            Me.MathOption.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
            Me.MathOption.FormattingEnabled = True
            Me.MathOption.Items.AddRange(New Object() {"-", "-", "HTML if simple, otherwise image (default)", "Image", "MathML", "TeX"})
            Me.MathOption.Location = New System.Drawing.Point(152, 97)
            Me.MathOption.MaxDropDownItems = 20
            Me.MathOption.Name = "MathOption"
            Me.MathOption.Size = New System.Drawing.Size(181, 21)
            Me.MathOption.Sorted = True
            Me.MathOption.TabIndex = 6
            '
            'ThumbnailSize
            '
            Me.ThumbnailSize.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
            Me.ThumbnailSize.FormattingEnabled = True
            Me.ThumbnailSize.Items.AddRange(New Object() {"120px", "150px", "180px", "200px", "250px", "300px"})
            Me.ThumbnailSize.Location = New System.Drawing.Point(152, 70)
            Me.ThumbnailSize.MaxDropDownItems = 20
            Me.ThumbnailSize.Name = "ThumbnailSize"
            Me.ThumbnailSize.Size = New System.Drawing.Size(83, 21)
            Me.ThumbnailSize.Sorted = True
            Me.ThumbnailSize.TabIndex = 5
            '
            'ThumbnailSizeLabel
            '
            Me.ThumbnailSizeLabel.AutoSize = True
            Me.ThumbnailSizeLabel.Location = New System.Drawing.Point(23, 73)
            Me.ThumbnailSizeLabel.Name = "ThumbnailSizeLabel"
            Me.ThumbnailSizeLabel.Size = New System.Drawing.Size(127, 13)
            Me.ThumbnailSizeLabel.TabIndex = 4
            Me.ThumbnailSizeLabel.Text = "Thumbnail size in articles:"
            '
            'ImageSize
            '
            Me.ImageSize.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
            Me.ImageSize.FormattingEnabled = True
            Me.ImageSize.Items.AddRange(New Object() {"(no limit)", "1024x768", "1280x1024", "320x240", "640x480", "800x600 (default)"})
            Me.ImageSize.Location = New System.Drawing.Point(152, 43)
            Me.ImageSize.MaxDropDownItems = 20
            Me.ImageSize.Name = "ImageSize"
            Me.ImageSize.Size = New System.Drawing.Size(109, 21)
            Me.ImageSize.Sorted = True
            Me.ImageSize.TabIndex = 3
            '
            'ImageSizeLabel
            '
            Me.ImageSizeLabel.AutoSize = True
            Me.ImageSizeLabel.Location = New System.Drawing.Point(12, 46)
            Me.ImageSizeLabel.Name = "ImageSizeLabel"
            Me.ImageSizeLabel.Size = New System.Drawing.Size(138, 13)
            Me.ImageSizeLabel.TabIndex = 2
            Me.ImageSizeLabel.Text = "Image size on image pages:"
            '
            'SkinsLabel
            '
            Me.SkinsLabel.AutoSize = True
            Me.SkinsLabel.Location = New System.Drawing.Point(119, 19)
            Me.SkinsLabel.Name = "SkinsLabel"
            Me.SkinsLabel.Size = New System.Drawing.Size(31, 13)
            Me.SkinsLabel.TabIndex = 1
            Me.SkinsLabel.Text = "Skin:"
            '
            'Skins
            '
            Me.Skins.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
            Me.Skins.FormattingEnabled = True
            Me.Skins.Location = New System.Drawing.Point(152, 16)
            Me.Skins.MaxDropDownItems = 20
            Me.Skins.Name = "Skins"
            Me.Skins.Size = New System.Drawing.Size(131, 21)
            Me.Skins.Sorted = True
            Me.Skins.TabIndex = 0
            '
            'GadgetsTab
            '
            Me.GadgetsTab.Controls.Add(Me.GadgetsLabel)
            Me.GadgetsTab.Controls.Add(Me.Gadgets)
            Me.GadgetsTab.Location = New System.Drawing.Point(4, 22)
            Me.GadgetsTab.Name = "GadgetsTab"
            Me.GadgetsTab.Padding = New System.Windows.Forms.Padding(3)
            Me.GadgetsTab.Size = New System.Drawing.Size(442, 293)
            Me.GadgetsTab.TabIndex = 3
            Me.GadgetsTab.Text = "Gadgets"
            Me.GadgetsTab.UseVisualStyleBackColor = True
            '
            'GadgetsLabel
            '
            Me.GadgetsLabel.AutoSize = True
            Me.GadgetsLabel.Location = New System.Drawing.Point(6, 12)
            Me.GadgetsLabel.Name = "GadgetsLabel"
            Me.GadgetsLabel.Size = New System.Drawing.Size(222, 13)
            Me.GadgetsLabel.TabIndex = 1
            Me.GadgetsLabel.Text = "Enable the following gadgets for this account:"
            '
            'Gadgets
            '
            Me.Gadgets.CheckOnClick = True
            Me.Gadgets.FormattingEnabled = True
            Me.Gadgets.IntegralHeight = False
            Me.Gadgets.Location = New System.Drawing.Point(6, 35)
            Me.Gadgets.Name = "Gadgets"
            Me.Gadgets.Size = New System.Drawing.Size(430, 214)
            Me.Gadgets.Sorted = True
            Me.Gadgets.TabIndex = 0
            '
            'OtherTab
            '
            Me.OtherTab.Controls.Add(Me.ListViewEx1)
            Me.OtherTab.Controls.Add(Me.Label1)
            Me.OtherTab.Location = New System.Drawing.Point(4, 22)
            Me.OtherTab.Name = "OtherTab"
            Me.OtherTab.Padding = New System.Windows.Forms.Padding(3)
            Me.OtherTab.Size = New System.Drawing.Size(442, 293)
            Me.OtherTab.TabIndex = 1
            Me.OtherTab.Text = "Other"
            Me.OtherTab.UseVisualStyleBackColor = True
            '
            'ListViewEx1
            '
            Me.ListViewEx1.FlexibleColumn = 0
            Me.ListViewEx1.FullRowSelect = True
            Me.ListViewEx1.GridLines = True
            Me.ListViewEx1.Location = New System.Drawing.Point(6, 41)
            Me.ListViewEx1.Name = "ListViewEx1"
            Me.ListViewEx1.ShowGroups = False
            Me.ListViewEx1.Size = New System.Drawing.Size(430, 208)
            Me.ListViewEx1.TabIndex = 1
            Me.ListViewEx1.UseCompatibleStateImageBehavior = False
            Me.ListViewEx1.View = System.Windows.Forms.View.Details
            '
            'Label1
            '
            Me.Label1.AutoSize = True
            Me.Label1.Location = New System.Drawing.Point(3, 12)
            Me.Label1.Name = "Label1"
            Me.Label1.Size = New System.Drawing.Size(383, 26)
            Me.Label1.TabIndex = 0
            Me.Label1.Text = "The following preferences unrecognized by Huggle are also set for this account." & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & _
                "They may relate to features not enabled on this wiki."
            '
            'CancelBtn
            '
            Me.CancelBtn.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
            Me.CancelBtn.Location = New System.Drawing.Point(387, 337)
            Me.CancelBtn.Name = "CancelBtn"
            Me.CancelBtn.Size = New System.Drawing.Size(75, 23)
            Me.CancelBtn.TabIndex = 7
            Me.CancelBtn.Text = "Cancel"
            Me.CancelBtn.UseVisualStyleBackColor = True
            '
            'Save
            '
            Me.Save.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
            Me.Save.Location = New System.Drawing.Point(306, 337)
            Me.Save.Name = "Save"
            Me.Save.Size = New System.Drawing.Size(75, 23)
            Me.Save.TabIndex = 7
            Me.Save.Text = "Save"
            Me.Save.UseVisualStyleBackColor = True
            '
            'Defaults
            '
            Me.Defaults.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
            Me.Defaults.Location = New System.Drawing.Point(12, 337)
            Me.Defaults.Name = "Defaults"
            Me.Defaults.Size = New System.Drawing.Size(113, 23)
            Me.Defaults.TabIndex = 8
            Me.Defaults.Text = "Restore defaults"
            Me.Defaults.UseVisualStyleBackColor = True
            '
            'AccountPreferencesForm
            '
            Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
            Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
            Me.ClientSize = New System.Drawing.Size(474, 372)
            Me.Controls.Add(Me.Defaults)
            Me.Controls.Add(Me.Save)
            Me.Controls.Add(Me.CancelBtn)
            Me.Controls.Add(Me.Tabs)
            Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
            Me.Name = "AccountPreferencesForm"
            Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
            Me.Text = "Account Preferences"
            Me.GeneralTab.ResumeLayout(False)
            Me.GeneralTab.PerformLayout()
            Me.Tabs.ResumeLayout(False)
            Me.AppearenceTab.ResumeLayout(False)
            Me.AppearenceTab.PerformLayout()
            CType(Me.StubThreshold, System.ComponentModel.ISupportInitialize).EndInit()
            Me.GadgetsTab.ResumeLayout(False)
            Me.GadgetsTab.PerformLayout()
            Me.OtherTab.ResumeLayout(False)
            Me.OtherTab.PerformLayout()
            Me.ResumeLayout(False)

        End Sub
        Private WithEvents Tabs As System.Windows.Forms.TabControl
        Private WithEvents Gender As System.Windows.Forms.ComboBox
        Private WithEvents GenderLabel As System.Windows.Forms.Label
        Private WithEvents InterfaceLanguageLabel As System.Windows.Forms.Label
        Private WithEvents InterfaceLanguage As System.Windows.Forms.ComboBox
        Private WithEvents SignatureLabel As System.Windows.Forms.Label
        Private WithEvents Signature As System.Windows.Forms.TextBox
        Private WithEvents RawSignature As System.Windows.Forms.CheckBox
        Private WithEvents EmailAddressLabel As System.Windows.Forms.Label
        Private WithEvents EmailAddress As System.Windows.Forms.TextBox
        Private WithEvents EnableEmail As System.Windows.Forms.CheckBox
        Private WithEvents CcMe As System.Windows.Forms.CheckBox
        Private WithEvents CancelBtn As System.Windows.Forms.Button
        Private WithEvents Save As System.Windows.Forms.Button
        Private WithEvents OtherTab As System.Windows.Forms.TabPage
        Private WithEvents Label1 As System.Windows.Forms.Label
        Private WithEvents AppearenceTab As System.Windows.Forms.TabPage
        Private WithEvents SkinsLabel As System.Windows.Forms.Label
        Private WithEvents Skins As System.Windows.Forms.ComboBox
        Private WithEvents GadgetsTab As System.Windows.Forms.TabPage
        Private WithEvents GadgetsLabel As System.Windows.Forms.Label
        Private WithEvents Gadgets As System.Windows.Forms.CheckedListBox
        Private WithEvents MathOptionLabel As System.Windows.Forms.Label
        Private WithEvents MathOption As System.Windows.Forms.ComboBox
        Private WithEvents ThumbnailSize As System.Windows.Forms.ComboBox
        Private WithEvents ThumbnailSizeLabel As System.Windows.Forms.Label
        Private WithEvents ImageSize As System.Windows.Forms.ComboBox
        Private WithEvents ImageSizeLabel As System.Windows.Forms.Label
        Private WithEvents UnderlineLinks As System.Windows.Forms.ComboBox
        Private WithEvents UnderlineLinksLabel As System.Windows.Forms.Label
        Private WithEvents AlternateLinks As System.Windows.Forms.CheckBox
        Private WithEvents Defaults As System.Windows.Forms.Button
        Private WithEvents GeneralTab As System.Windows.Forms.TabPage
        Private WithEvents ListViewEx1 As System.Windows.Forms.EnhancedListView
        Private WithEvents StubThresholdSelect As System.Windows.Forms.CheckBox
        Private WithEvents Justify As System.Windows.Forms.CheckBox
        Private WithEvents JumpLinks As System.Windows.Forms.CheckBox
        Private WithEvents NumberHeadings As System.Windows.Forms.CheckBox
        Private WithEvents HiddenCategories As System.Windows.Forms.CheckBox
        Private WithEvents DisableCaching As System.Windows.Forms.CheckBox
        Private WithEvents Toc As System.Windows.Forms.CheckBox
        Private WithEvents StubThreshold As System.Windows.Forms.NumericUpDown
    End Class

End Namespace