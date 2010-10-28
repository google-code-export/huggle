Namespace Huggle.UI
    <Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
    Partial Class GlobalPreferencesForm
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
            Me.CopyFromLabel = New System.Windows.Forms.Label()
            Me.CopyFrom = New System.Windows.Forms.ComboBox()
            Me.CopyToLabel = New System.Windows.Forms.Label()
            Me.CopyWhat = New System.Windows.Forms.CheckedListBox()
            Me.CopyTo = New System.Windows.Forms.CheckedListBox()
            Me.AccountLabel = New System.Windows.Forms.Label()
            Me.Account = New System.Windows.Forms.Label()
            Me.CopyWhatLabel = New System.Windows.Forms.Label()
            Me.ShowAll = New System.Windows.Forms.CheckBox()
            Me.CopyToCheck = New System.Windows.Forms.LinkLabel()
            Me.CopyToClear = New System.Windows.Forms.LinkLabel()
            Me.CopyWhatClear = New System.Windows.Forms.LinkLabel()
            Me.CopyWhatCheck = New System.Windows.Forms.LinkLabel()
            Me.PrefsNote = New System.Windows.Forms.Label()
            Me.Cancel = New System.Windows.Forms.Button()
            Me.OK = New System.Windows.Forms.Button()
            Me.PrefsError = New System.Windows.Forms.Label()
            Me.SuspendLayout()
            '
            'CopyFromLabel
            '
            Me.CopyFromLabel.AutoSize = True
            Me.CopyFromLabel.Location = New System.Drawing.Point(12, 31)
            Me.CopyFromLabel.Name = "CopyFromLabel"
            Me.CopyFromLabel.Size = New System.Drawing.Size(116, 13)
            Me.CopyFromLabel.TabIndex = 0
            Me.CopyFromLabel.Text = "Copy preferences from:"
            '
            'CopyFrom
            '
            Me.CopyFrom.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
            Me.CopyFrom.FormattingEnabled = True
            Me.CopyFrom.Location = New System.Drawing.Point(130, 28)
            Me.CopyFrom.Name = "CopyFrom"
            Me.CopyFrom.Size = New System.Drawing.Size(201, 21)
            Me.CopyFrom.TabIndex = 1
            '
            'CopyToLabel
            '
            Me.CopyToLabel.AutoSize = True
            Me.CopyToLabel.Location = New System.Drawing.Point(12, 64)
            Me.CopyToLabel.Name = "CopyToLabel"
            Me.CopyToLabel.Size = New System.Drawing.Size(101, 13)
            Me.CopyToLabel.TabIndex = 2
            Me.CopyToLabel.Text = "Copy to these wikis:"
            '
            'CopyWhat
            '
            Me.CopyWhat.CheckOnClick = True
            Me.CopyWhat.FormattingEnabled = True
            Me.CopyWhat.Location = New System.Drawing.Point(283, 80)
            Me.CopyWhat.Name = "CopyWhat"
            Me.CopyWhat.Size = New System.Drawing.Size(239, 124)
            Me.CopyWhat.Sorted = True
            Me.CopyWhat.TabIndex = 3
            '
            'CopyTo
            '
            Me.CopyTo.CheckOnClick = True
            Me.CopyTo.FormattingEnabled = True
            Me.CopyTo.Location = New System.Drawing.Point(15, 80)
            Me.CopyTo.Name = "CopyTo"
            Me.CopyTo.Size = New System.Drawing.Size(239, 124)
            Me.CopyTo.Sorted = True
            Me.CopyTo.TabIndex = 8
            '
            'AccountLabel
            '
            Me.AccountLabel.AutoSize = True
            Me.AccountLabel.Location = New System.Drawing.Point(78, 9)
            Me.AccountLabel.Name = "AccountLabel"
            Me.AccountLabel.Size = New System.Drawing.Size(50, 13)
            Me.AccountLabel.TabIndex = 11
            Me.AccountLabel.Text = "Account:"
            '
            'Account
            '
            Me.Account.AutoSize = True
            Me.Account.Location = New System.Drawing.Point(127, 9)
            Me.Account.Name = "Account"
            Me.Account.Size = New System.Drawing.Size(67, 13)
            Me.Account.TabIndex = 12
            Me.Account.Text = "<globaluser>"
            '
            'CopyWhatLabel
            '
            Me.CopyWhatLabel.AutoSize = True
            Me.CopyWhatLabel.Location = New System.Drawing.Point(280, 64)
            Me.CopyWhatLabel.Name = "CopyWhatLabel"
            Me.CopyWhatLabel.Size = New System.Drawing.Size(122, 13)
            Me.CopyWhatLabel.TabIndex = 13
            Me.CopyWhatLabel.Text = "Copy these preferences:"
            '
            'ShowAll
            '
            Me.ShowAll.AutoSize = True
            Me.ShowAll.Location = New System.Drawing.Point(12, 210)
            Me.ShowAll.Name = "ShowAll"
            Me.ShowAll.Size = New System.Drawing.Size(239, 30)
            Me.ShowAll.TabIndex = 14
            Me.ShowAll.Text = "Show wikis where no local account exists yet" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "(one will be created automatically)" & _
                ""
            Me.ShowAll.UseVisualStyleBackColor = True
            '
            'CopyToCheck
            '
            Me.CopyToCheck.AutoSize = True
            Me.CopyToCheck.LinkBehavior = System.Windows.Forms.LinkBehavior.HoverUnderline
            Me.CopyToCheck.LinkColor = System.Drawing.SystemColors.HotTrack
            Me.CopyToCheck.Location = New System.Drawing.Point(150, 64)
            Me.CopyToCheck.Name = "CopyToCheck"
            Me.CopyToCheck.Size = New System.Drawing.Size(51, 13)
            Me.CopyToCheck.TabIndex = 15
            Me.CopyToCheck.TabStop = True
            Me.CopyToCheck.Text = "Check all"
            '
            'CopyToClear
            '
            Me.CopyToClear.AutoSize = True
            Me.CopyToClear.LinkBehavior = System.Windows.Forms.LinkBehavior.HoverUnderline
            Me.CopyToClear.LinkColor = System.Drawing.SystemColors.HotTrack
            Me.CopyToClear.Location = New System.Drawing.Point(210, 64)
            Me.CopyToClear.Name = "CopyToClear"
            Me.CopyToClear.Size = New System.Drawing.Size(44, 13)
            Me.CopyToClear.TabIndex = 16
            Me.CopyToClear.TabStop = True
            Me.CopyToClear.Text = "Clear all"
            '
            'CopyWhatClear
            '
            Me.CopyWhatClear.AutoSize = True
            Me.CopyWhatClear.LinkBehavior = System.Windows.Forms.LinkBehavior.HoverUnderline
            Me.CopyWhatClear.LinkColor = System.Drawing.SystemColors.HotTrack
            Me.CopyWhatClear.Location = New System.Drawing.Point(478, 64)
            Me.CopyWhatClear.Name = "CopyWhatClear"
            Me.CopyWhatClear.Size = New System.Drawing.Size(44, 13)
            Me.CopyWhatClear.TabIndex = 18
            Me.CopyWhatClear.TabStop = True
            Me.CopyWhatClear.Text = "Clear all"
            '
            'CopyWhatCheck
            '
            Me.CopyWhatCheck.AutoSize = True
            Me.CopyWhatCheck.LinkBehavior = System.Windows.Forms.LinkBehavior.HoverUnderline
            Me.CopyWhatCheck.LinkColor = System.Drawing.SystemColors.HotTrack
            Me.CopyWhatCheck.Location = New System.Drawing.Point(418, 64)
            Me.CopyWhatCheck.Name = "CopyWhatCheck"
            Me.CopyWhatCheck.Size = New System.Drawing.Size(51, 13)
            Me.CopyWhatCheck.TabIndex = 17
            Me.CopyWhatCheck.TabStop = True
            Me.CopyWhatCheck.Text = "Check all"
            '
            'PrefsNote
            '
            Me.PrefsNote.Location = New System.Drawing.Point(280, 210)
            Me.PrefsNote.Name = "PrefsNote"
            Me.PrefsNote.Size = New System.Drawing.Size(242, 44)
            Me.PrefsNote.TabIndex = 19
            Me.PrefsNote.Text = "Some preferences (for example, gadgets) do not exist on all wikis. If selected, t" & _
                "hey will be copied only to wikis on which they exist."
            '
            'Cancel
            '
            Me.Cancel.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
            Me.Cancel.DialogResult = System.Windows.Forms.DialogResult.Cancel
            Me.Cancel.Location = New System.Drawing.Point(447, 266)
            Me.Cancel.Name = "Cancel"
            Me.Cancel.Size = New System.Drawing.Size(75, 23)
            Me.Cancel.TabIndex = 20
            Me.Cancel.Text = "Cancel"
            Me.Cancel.UseVisualStyleBackColor = True
            '
            'OK
            '
            Me.OK.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
            Me.OK.DialogResult = System.Windows.Forms.DialogResult.OK
            Me.OK.Enabled = False
            Me.OK.Location = New System.Drawing.Point(366, 266)
            Me.OK.Name = "OK"
            Me.OK.Size = New System.Drawing.Size(75, 23)
            Me.OK.TabIndex = 21
            Me.OK.Text = "Continue"
            Me.OK.UseVisualStyleBackColor = True
            '
            'PrefsError
            '
            Me.PrefsError.AutoSize = True
            Me.PrefsError.BackColor = System.Drawing.SystemColors.Window
            Me.PrefsError.ForeColor = System.Drawing.SystemColors.ControlDark
            Me.PrefsError.Location = New System.Drawing.Point(335, 132)
            Me.PrefsError.Name = "PrefsError"
            Me.PrefsError.Size = New System.Drawing.Size(134, 13)
            Me.PrefsError.TabIndex = 22
            Me.PrefsError.Text = "Error retrieving preferences"
            Me.PrefsError.Visible = False
            '
            'GlobalPreferencesForm
            '
            Me.AcceptButton = Me.OK
            Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
            Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
            Me.CancelButton = Me.Cancel
            Me.ClientSize = New System.Drawing.Size(534, 301)
            Me.Controls.Add(Me.PrefsError)
            Me.Controls.Add(Me.OK)
            Me.Controls.Add(Me.Cancel)
            Me.Controls.Add(Me.PrefsNote)
            Me.Controls.Add(Me.CopyWhatClear)
            Me.Controls.Add(Me.CopyWhatCheck)
            Me.Controls.Add(Me.CopyToClear)
            Me.Controls.Add(Me.CopyToCheck)
            Me.Controls.Add(Me.ShowAll)
            Me.Controls.Add(Me.CopyWhatLabel)
            Me.Controls.Add(Me.Account)
            Me.Controls.Add(Me.AccountLabel)
            Me.Controls.Add(Me.CopyTo)
            Me.Controls.Add(Me.CopyWhat)
            Me.Controls.Add(Me.CopyToLabel)
            Me.Controls.Add(Me.CopyFrom)
            Me.Controls.Add(Me.CopyFromLabel)
            Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
            Me.MaximizeBox = False
            Me.MinimizeBox = False
            Me.Name = "GlobalPreferencesForm"
            Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
            Me.Text = "Global preferences"
            Me.ResumeLayout(False)
            Me.PerformLayout()

        End Sub
        Private WithEvents CopyFromLabel As System.Windows.Forms.Label
        Private WithEvents CopyFrom As System.Windows.Forms.ComboBox
        Private WithEvents CopyToLabel As System.Windows.Forms.Label
        Private WithEvents CopyWhat As System.Windows.Forms.CheckedListBox
        Private WithEvents CopyTo As System.Windows.Forms.CheckedListBox
        Private WithEvents AccountLabel As System.Windows.Forms.Label
        Private WithEvents Account As System.Windows.Forms.Label
        Private WithEvents CopyWhatLabel As System.Windows.Forms.Label
        Private WithEvents ShowAll As System.Windows.Forms.CheckBox
        Private WithEvents CopyToCheck As System.Windows.Forms.LinkLabel
        Private WithEvents CopyToClear As System.Windows.Forms.LinkLabel
        Private WithEvents CopyWhatClear As System.Windows.Forms.LinkLabel
        Private WithEvents CopyWhatCheck As System.Windows.Forms.LinkLabel
        Private WithEvents PrefsNote As System.Windows.Forms.Label
        Private WithEvents Cancel As System.Windows.Forms.Button
        Private WithEvents OK As System.Windows.Forms.Button
        Private WithEvents PrefsError As System.Windows.Forms.Label
    End Class

End Namespace