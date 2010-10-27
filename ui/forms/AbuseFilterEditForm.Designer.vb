Namespace Huggle.UI

    <Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
    Partial Class AbuseFilterEditForm
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
            Dim ConsequencesTab As System.Windows.Forms.TabPage
            Dim WarningTab As System.Windows.Forms.TabPage
            Dim RateLimitTab As System.Windows.Forms.TabPage
            Dim TagsTab As System.Windows.Forms.TabPage
            Me.RangeblockCheck = New System.Windows.Forms.CheckBox
            Me.DegroupCheck = New System.Windows.Forms.CheckBox
            Me.BlockAutopromoteCheck = New System.Windows.Forms.CheckBox
            Me.BlockCheck = New System.Windows.Forms.CheckBox
            Me.DisallowCheck = New System.Windows.Forms.CheckBox
            Me.WarningLabel = New System.Windows.Forms.Label
            Me.WarningPreview = New System.Windows.Forms.WebBrowser
            Me.WarningSelector = New System.Windows.Forms.ComboBox
            Me.RateLimitGroupLabel = New System.Windows.Forms.Label
            Me.RateLimitTimeLabel = New System.Windows.Forms.Label
            Me.RateLimitCount = New System.Windows.Forms.TextBox
            Me.RateLimitGroup = New System.Windows.Forms.TextBox
            Me.RateLimitTime = New System.Windows.Forms.TextBox
            Me.RateLimitCheck = New System.Windows.Forms.CheckBox
            Me.RateLimitCountLabel = New System.Windows.Forms.Label
            Me.TagsLabel = New System.Windows.Forms.Label
            Me.Tags = New System.Windows.Forms.TextBox
            Me.DescriptionLabel = New System.Windows.Forms.Label
            Me.Description = New System.Windows.Forms.TextBox
            Me.Condition = New System.Windows.Forms.RichTextBox
            Me.ConditionLabel = New System.Windows.Forms.Label
            Me.PrivateCheck = New System.Windows.Forms.CheckBox
            Me.NotesLabel = New System.Windows.Forms.Label
            Me.StatusGroup = New System.Windows.Forms.GroupBox
            Me.DeletedOption = New System.Windows.Forms.RadioButton
            Me.DisabledOption = New System.Windows.Forms.RadioButton
            Me.EnabledOption = New System.Windows.Forms.RadioButton
            Me.Tabs = New System.Windows.Forms.TabControl
            Me.CancelBtn = New System.Windows.Forms.Button
            Me.OK = New System.Windows.Forms.Button
            Me.Notes = New System.Windows.Forms.RichTextBox
            ConsequencesTab = New System.Windows.Forms.TabPage
            WarningTab = New System.Windows.Forms.TabPage
            RateLimitTab = New System.Windows.Forms.TabPage
            TagsTab = New System.Windows.Forms.TabPage
            ConsequencesTab.SuspendLayout()
            WarningTab.SuspendLayout()
            RateLimitTab.SuspendLayout()
            TagsTab.SuspendLayout()
            Me.StatusGroup.SuspendLayout()
            Me.Tabs.SuspendLayout()
            Me.SuspendLayout()
            '
            'ConsequencesTab
            '
            ConsequencesTab.Controls.Add(Me.RangeblockCheck)
            ConsequencesTab.Controls.Add(Me.DegroupCheck)
            ConsequencesTab.Controls.Add(Me.BlockAutopromoteCheck)
            ConsequencesTab.Controls.Add(Me.BlockCheck)
            ConsequencesTab.Controls.Add(Me.DisallowCheck)
            ConsequencesTab.Location = New System.Drawing.Point(4, 22)
            ConsequencesTab.Name = "ConsequencesTab"
            ConsequencesTab.Padding = New System.Windows.Forms.Padding(3)
            ConsequencesTab.Size = New System.Drawing.Size(456, 105)
            ConsequencesTab.TabIndex = 0
            ConsequencesTab.Text = "Consequences"
            ConsequencesTab.UseVisualStyleBackColor = True
            '
            'RangeblockCheck
            '
            Me.RangeblockCheck.AutoSize = True
            Me.RangeblockCheck.Location = New System.Drawing.Point(10, 104)
            Me.RangeblockCheck.Name = "RangeblockCheck"
            Me.RangeblockCheck.Size = New System.Drawing.Size(207, 17)
            Me.RangeblockCheck.TabIndex = 0
            Me.RangeblockCheck.Text = "Block the user's /16 IP address range "
            Me.RangeblockCheck.UseVisualStyleBackColor = True
            '
            'DegroupCheck
            '
            Me.DegroupCheck.AutoSize = True
            Me.DegroupCheck.Location = New System.Drawing.Point(10, 81)
            Me.DegroupCheck.Name = "DegroupCheck"
            Me.DegroupCheck.Size = New System.Drawing.Size(189, 17)
            Me.DegroupCheck.TabIndex = 0
            Me.DegroupCheck.Text = "Remove account from user groups"
            Me.DegroupCheck.UseVisualStyleBackColor = True
            '
            'BlockAutopromoteCheck
            '
            Me.BlockAutopromoteCheck.AutoSize = True
            Me.BlockAutopromoteCheck.Location = New System.Drawing.Point(10, 58)
            Me.BlockAutopromoteCheck.Name = "BlockAutopromoteCheck"
            Me.BlockAutopromoteCheck.Size = New System.Drawing.Size(177, 17)
            Me.BlockAutopromoteCheck.TabIndex = 0
            Me.BlockAutopromoteCheck.Text = "Block autopromotion of account"
            Me.BlockAutopromoteCheck.UseVisualStyleBackColor = True
            '
            'BlockCheck
            '
            Me.BlockCheck.AutoSize = True
            Me.BlockCheck.Location = New System.Drawing.Point(10, 35)
            Me.BlockCheck.Name = "BlockCheck"
            Me.BlockCheck.Size = New System.Drawing.Size(147, 17)
            Me.BlockCheck.TabIndex = 0
            Me.BlockCheck.Text = "Block account indefinitely"
            Me.BlockCheck.UseVisualStyleBackColor = True
            '
            'DisallowCheck
            '
            Me.DisallowCheck.AutoSize = True
            Me.DisallowCheck.Location = New System.Drawing.Point(10, 12)
            Me.DisallowCheck.Name = "DisallowCheck"
            Me.DisallowCheck.Size = New System.Drawing.Size(97, 17)
            Me.DisallowCheck.TabIndex = 0
            Me.DisallowCheck.Text = "Disallow action"
            Me.DisallowCheck.UseVisualStyleBackColor = True
            '
            'WarningTab
            '
            WarningTab.Controls.Add(Me.WarningLabel)
            WarningTab.Controls.Add(Me.WarningPreview)
            WarningTab.Controls.Add(Me.WarningSelector)
            WarningTab.Location = New System.Drawing.Point(4, 22)
            WarningTab.Name = "WarningTab"
            WarningTab.Padding = New System.Windows.Forms.Padding(3)
            WarningTab.Size = New System.Drawing.Size(456, 105)
            WarningTab.TabIndex = 1
            WarningTab.Text = "Warning"
            WarningTab.UseVisualStyleBackColor = True
            '
            'WarningLabel
            '
            Me.WarningLabel.AutoSize = True
            Me.WarningLabel.Location = New System.Drawing.Point(6, 9)
            Me.WarningLabel.Name = "WarningLabel"
            Me.WarningLabel.Size = New System.Drawing.Size(122, 13)
            Me.WarningLabel.TabIndex = 2
            Me.WarningLabel.Text = "Show warning message:"
            '
            'WarningPreview
            '
            Me.WarningPreview.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                        Or System.Windows.Forms.AnchorStyles.Left) _
                        Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
            Me.WarningPreview.Location = New System.Drawing.Point(6, 33)
            Me.WarningPreview.MinimumSize = New System.Drawing.Size(20, 20)
            Me.WarningPreview.Name = "WarningPreview"
            Me.WarningPreview.ScriptErrorsSuppressed = True
            Me.WarningPreview.ScrollBarsEnabled = False
            Me.WarningPreview.Size = New System.Drawing.Size(444, 72)
            Me.WarningPreview.TabIndex = 1
            '
            'WarningSelector
            '
            Me.WarningSelector.FormattingEnabled = True
            Me.WarningSelector.Location = New System.Drawing.Point(134, 6)
            Me.WarningSelector.Name = "WarningSelector"
            Me.WarningSelector.Size = New System.Drawing.Size(288, 21)
            Me.WarningSelector.TabIndex = 0
            '
            'RateLimitTab
            '
            RateLimitTab.Controls.Add(Me.RateLimitGroupLabel)
            RateLimitTab.Controls.Add(Me.RateLimitTimeLabel)
            RateLimitTab.Controls.Add(Me.RateLimitCount)
            RateLimitTab.Controls.Add(Me.RateLimitGroup)
            RateLimitTab.Controls.Add(Me.RateLimitTime)
            RateLimitTab.Controls.Add(Me.RateLimitCheck)
            RateLimitTab.Controls.Add(Me.RateLimitCountLabel)
            RateLimitTab.Location = New System.Drawing.Point(4, 22)
            RateLimitTab.Name = "RateLimitTab"
            RateLimitTab.Size = New System.Drawing.Size(456, 105)
            RateLimitTab.TabIndex = 2
            RateLimitTab.Text = "Rate limit"
            RateLimitTab.UseVisualStyleBackColor = True
            '
            'RateLimitGroupLabel
            '
            Me.RateLimitGroupLabel.AutoSize = True
            Me.RateLimitGroupLabel.Location = New System.Drawing.Point(31, 36)
            Me.RateLimitGroupLabel.Name = "RateLimitGroupLabel"
            Me.RateLimitGroupLabel.Size = New System.Drawing.Size(237, 13)
            Me.RateLimitGroupLabel.TabIndex = 8
            Me.RateLimitGroupLabel.Text = "aggregated within the following groups (optional):"
            '
            'RateLimitTimeLabel
            '
            Me.RateLimitTimeLabel.AutoSize = True
            Me.RateLimitTimeLabel.Location = New System.Drawing.Point(374, 13)
            Me.RateLimitTimeLabel.Name = "RateLimitTimeLabel"
            Me.RateLimitTimeLabel.Size = New System.Drawing.Size(47, 13)
            Me.RateLimitTimeLabel.TabIndex = 7
            Me.RateLimitTimeLabel.Text = "seconds"
            '
            'RateLimitCount
            '
            Me.RateLimitCount.Location = New System.Drawing.Point(183, 11)
            Me.RateLimitCount.Name = "RateLimitCount"
            Me.RateLimitCount.Size = New System.Drawing.Size(35, 20)
            Me.RateLimitCount.TabIndex = 5
            '
            'RateLimitGroup
            '
            Me.RateLimitGroup.Location = New System.Drawing.Point(274, 35)
            Me.RateLimitGroup.Multiline = True
            Me.RateLimitGroup.Name = "RateLimitGroup"
            Me.RateLimitGroup.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
            Me.RateLimitGroup.Size = New System.Drawing.Size(144, 66)
            Me.RateLimitGroup.TabIndex = 5
            '
            'RateLimitTime
            '
            Me.RateLimitTime.AcceptsReturn = True
            Me.RateLimitTime.Location = New System.Drawing.Point(337, 11)
            Me.RateLimitTime.Name = "RateLimitTime"
            Me.RateLimitTime.Size = New System.Drawing.Size(35, 20)
            Me.RateLimitTime.TabIndex = 5
            '
            'RateLimitCheck
            '
            Me.RateLimitCheck.AutoSize = True
            Me.RateLimitCheck.Location = New System.Drawing.Point(15, 13)
            Me.RateLimitCheck.Name = "RateLimitCheck"
            Me.RateLimitCheck.Size = New System.Drawing.Size(171, 17)
            Me.RateLimitCheck.TabIndex = 4
            Me.RateLimitCheck.Text = "Apply consequences only after"
            Me.RateLimitCheck.UseVisualStyleBackColor = True
            '
            'RateLimitCountLabel
            '
            Me.RateLimitCountLabel.AutoSize = True
            Me.RateLimitCountLabel.Location = New System.Drawing.Point(220, 14)
            Me.RateLimitCountLabel.Name = "RateLimitCountLabel"
            Me.RateLimitCountLabel.Size = New System.Drawing.Size(117, 13)
            Me.RateLimitCountLabel.TabIndex = 6
            Me.RateLimitCountLabel.Text = "matching actions within"
            '
            'TagsTab
            '
            TagsTab.Controls.Add(Me.TagsLabel)
            TagsTab.Controls.Add(Me.Tags)
            TagsTab.Location = New System.Drawing.Point(4, 22)
            TagsTab.Name = "TagsTab"
            TagsTab.Padding = New System.Windows.Forms.Padding(3)
            TagsTab.Size = New System.Drawing.Size(456, 105)
            TagsTab.TabIndex = 3
            TagsTab.Text = "Tags"
            TagsTab.UseVisualStyleBackColor = True
            '
            'TagsLabel
            '
            Me.TagsLabel.AutoSize = True
            Me.TagsLabel.Location = New System.Drawing.Point(6, 10)
            Me.TagsLabel.Name = "TagsLabel"
            Me.TagsLabel.Size = New System.Drawing.Size(214, 13)
            Me.TagsLabel.TabIndex = 1
            Me.TagsLabel.Text = "Apply the following tags to matched actions:"
            '
            'Tags
            '
            Me.Tags.Location = New System.Drawing.Point(9, 31)
            Me.Tags.Multiline = True
            Me.Tags.Name = "Tags"
            Me.Tags.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
            Me.Tags.Size = New System.Drawing.Size(211, 68)
            Me.Tags.TabIndex = 0
            '
            'DescriptionLabel
            '
            Me.DescriptionLabel.AutoSize = True
            Me.DescriptionLabel.Location = New System.Drawing.Point(12, 12)
            Me.DescriptionLabel.Name = "DescriptionLabel"
            Me.DescriptionLabel.Size = New System.Drawing.Size(63, 13)
            Me.DescriptionLabel.TabIndex = 0
            Me.DescriptionLabel.Text = "Description:"
            '
            'Description
            '
            Me.Description.Location = New System.Drawing.Point(78, 9)
            Me.Description.Name = "Description"
            Me.Description.Size = New System.Drawing.Size(337, 20)
            Me.Description.TabIndex = 1
            '
            'Condition
            '
            Me.Condition.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                        Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
            Me.Condition.Font = New System.Drawing.Font("Courier New", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.Condition.Location = New System.Drawing.Point(78, 35)
            Me.Condition.Name = "Condition"
            Me.Condition.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.Vertical
            Me.Condition.Size = New System.Drawing.Size(464, 80)
            Me.Condition.TabIndex = 2
            Me.Condition.Text = ""
            '
            'ConditionLabel
            '
            Me.ConditionLabel.AutoSize = True
            Me.ConditionLabel.Location = New System.Drawing.Point(21, 38)
            Me.ConditionLabel.Name = "ConditionLabel"
            Me.ConditionLabel.Size = New System.Drawing.Size(54, 13)
            Me.ConditionLabel.TabIndex = 0
            Me.ConditionLabel.Text = "Condition:"
            '
            'PrivateCheck
            '
            Me.PrivateCheck.AutoSize = True
            Me.PrivateCheck.Location = New System.Drawing.Point(82, 203)
            Me.PrivateCheck.Name = "PrivateCheck"
            Me.PrivateCheck.Size = New System.Drawing.Size(59, 17)
            Me.PrivateCheck.TabIndex = 4
            Me.PrivateCheck.Text = "Private"
            Me.PrivateCheck.UseVisualStyleBackColor = True
            '
            'NotesLabel
            '
            Me.NotesLabel.AutoSize = True
            Me.NotesLabel.Location = New System.Drawing.Point(37, 124)
            Me.NotesLabel.Name = "NotesLabel"
            Me.NotesLabel.Size = New System.Drawing.Size(38, 13)
            Me.NotesLabel.TabIndex = 0
            Me.NotesLabel.Text = "Notes:"
            '
            'StatusGroup
            '
            Me.StatusGroup.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                        Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
            Me.StatusGroup.Controls.Add(Me.DeletedOption)
            Me.StatusGroup.Controls.Add(Me.DisabledOption)
            Me.StatusGroup.Controls.Add(Me.EnabledOption)
            Me.StatusGroup.Location = New System.Drawing.Point(183, 191)
            Me.StatusGroup.Name = "StatusGroup"
            Me.StatusGroup.Size = New System.Drawing.Size(359, 36)
            Me.StatusGroup.TabIndex = 6
            Me.StatusGroup.TabStop = False
            '
            'DeletedOption
            '
            Me.DeletedOption.AutoSize = True
            Me.DeletedOption.Location = New System.Drawing.Point(262, 12)
            Me.DeletedOption.Name = "DeletedOption"
            Me.DeletedOption.Size = New System.Drawing.Size(62, 17)
            Me.DeletedOption.TabIndex = 0
            Me.DeletedOption.TabStop = True
            Me.DeletedOption.Text = "Deleted"
            Me.DeletedOption.UseVisualStyleBackColor = True
            '
            'DisabledOption
            '
            Me.DisabledOption.AutoSize = True
            Me.DisabledOption.Location = New System.Drawing.Point(146, 12)
            Me.DisabledOption.Name = "DisabledOption"
            Me.DisabledOption.Size = New System.Drawing.Size(66, 17)
            Me.DisabledOption.TabIndex = 0
            Me.DisabledOption.TabStop = True
            Me.DisabledOption.Text = "Disabled"
            Me.DisabledOption.UseVisualStyleBackColor = True
            '
            'EnabledOption
            '
            Me.EnabledOption.AutoSize = True
            Me.EnabledOption.Location = New System.Drawing.Point(37, 12)
            Me.EnabledOption.Name = "EnabledOption"
            Me.EnabledOption.Size = New System.Drawing.Size(64, 17)
            Me.EnabledOption.TabIndex = 0
            Me.EnabledOption.TabStop = True
            Me.EnabledOption.Text = "Enabled"
            Me.EnabledOption.UseVisualStyleBackColor = True
            '
            'Tabs
            '
            Me.Tabs.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                        Or System.Windows.Forms.AnchorStyles.Left) _
                        Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
            Me.Tabs.Controls.Add(ConsequencesTab)
            Me.Tabs.Controls.Add(WarningTab)
            Me.Tabs.Controls.Add(RateLimitTab)
            Me.Tabs.Controls.Add(TagsTab)
            Me.Tabs.Location = New System.Drawing.Point(78, 233)
            Me.Tabs.Name = "Tabs"
            Me.Tabs.SelectedIndex = 0
            Me.Tabs.Size = New System.Drawing.Size(464, 131)
            Me.Tabs.TabIndex = 7
            '
            'CancelBtn
            '
            Me.CancelBtn.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
            Me.CancelBtn.Location = New System.Drawing.Point(463, 370)
            Me.CancelBtn.Name = "CancelBtn"
            Me.CancelBtn.Size = New System.Drawing.Size(75, 23)
            Me.CancelBtn.TabIndex = 8
            Me.CancelBtn.Text = "Cancel"
            Me.CancelBtn.UseVisualStyleBackColor = True
            '
            'OK
            '
            Me.OK.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
            Me.OK.Location = New System.Drawing.Point(382, 370)
            Me.OK.Name = "OK"
            Me.OK.Size = New System.Drawing.Size(75, 23)
            Me.OK.TabIndex = 8
            Me.OK.Text = "OK"
            Me.OK.UseVisualStyleBackColor = True
            '
            'Notes
            '
            Me.Notes.Location = New System.Drawing.Point(77, 121)
            Me.Notes.Name = "Notes"
            Me.Notes.Size = New System.Drawing.Size(465, 72)
            Me.Notes.TabIndex = 9
            Me.Notes.Text = ""
            '
            'AbuseFilterEditForm
            '
            Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
            Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
            Me.ClientSize = New System.Drawing.Size(554, 404)
            Me.Controls.Add(Me.Notes)
            Me.Controls.Add(Me.OK)
            Me.Controls.Add(Me.CancelBtn)
            Me.Controls.Add(Me.Tabs)
            Me.Controls.Add(Me.StatusGroup)
            Me.Controls.Add(Me.PrivateCheck)
            Me.Controls.Add(Me.Condition)
            Me.Controls.Add(Me.Description)
            Me.Controls.Add(Me.NotesLabel)
            Me.Controls.Add(Me.ConditionLabel)
            Me.Controls.Add(Me.DescriptionLabel)
            Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
            Me.Name = "AbuseFilterEditForm"
            Me.Text = "Create abuse filter"
            ConsequencesTab.ResumeLayout(False)
            ConsequencesTab.PerformLayout()
            WarningTab.ResumeLayout(False)
            WarningTab.PerformLayout()
            RateLimitTab.ResumeLayout(False)
            RateLimitTab.PerformLayout()
            TagsTab.ResumeLayout(False)
            TagsTab.PerformLayout()
            Me.StatusGroup.ResumeLayout(False)
            Me.StatusGroup.PerformLayout()
            Me.Tabs.ResumeLayout(False)
            Me.ResumeLayout(False)
            Me.PerformLayout()

        End Sub
        Private WithEvents RateLimitGroup As System.Windows.Forms.TextBox
        Private WithEvents DescriptionLabel As System.Windows.Forms.Label
        Private WithEvents Description As System.Windows.Forms.TextBox
        Private WithEvents NotesLabel As System.Windows.Forms.Label
        Private WithEvents PrivateCheck As System.Windows.Forms.CheckBox
        Private WithEvents WarningPreview As System.Windows.Forms.WebBrowser
        Private WithEvents Tags As System.Windows.Forms.TextBox
        Private WithEvents StatusGroup As System.Windows.Forms.GroupBox
        Private WithEvents RateLimitCheck As System.Windows.Forms.CheckBox
        Private WithEvents RateLimitGroupLabel As System.Windows.Forms.Label
        Private WithEvents TagsLabel As System.Windows.Forms.Label
        Private WithEvents Condition As System.Windows.Forms.RichTextBox
        Private WithEvents ConditionLabel As System.Windows.Forms.Label
        Private WithEvents RateLimitCount As System.Windows.Forms.TextBox
        Private WithEvents RateLimitTime As System.Windows.Forms.TextBox
        Private WithEvents DeletedOption As System.Windows.Forms.RadioButton
        Private WithEvents DisabledOption As System.Windows.Forms.RadioButton
        Private WithEvents EnabledOption As System.Windows.Forms.RadioButton
        Private WithEvents Tabs As System.Windows.Forms.TabControl
        Private WithEvents DegroupCheck As System.Windows.Forms.CheckBox
        Private WithEvents BlockAutopromoteCheck As System.Windows.Forms.CheckBox
        Private WithEvents BlockCheck As System.Windows.Forms.CheckBox
        Private WithEvents DisallowCheck As System.Windows.Forms.CheckBox
        Private WithEvents WarningSelector As System.Windows.Forms.ComboBox
        Private WithEvents WarningLabel As System.Windows.Forms.Label
        Private WithEvents RateLimitCountLabel As System.Windows.Forms.Label
        Private WithEvents RateLimitTimeLabel As System.Windows.Forms.Label
        Private WithEvents RangeblockCheck As System.Windows.Forms.CheckBox
        Private WithEvents CancelBtn As System.Windows.Forms.Button
        Private WithEvents OK As System.Windows.Forms.Button
        Private WithEvents Notes As System.Windows.Forms.RichTextBox
    End Class

End Namespace