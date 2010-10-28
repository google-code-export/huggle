Namespace Huggle.UI
    <Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
    Partial Class RunForm
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
            Dim TreeNode27 As System.Windows.Forms.TreeNode = New System.Windows.Forms.TreeNode("Node1")
            Dim TreeNode28 As System.Windows.Forms.TreeNode = New System.Windows.Forms.TreeNode("Node0", New System.Windows.Forms.TreeNode() {TreeNode27})
            Dim TreeNode29 As System.Windows.Forms.TreeNode = New System.Windows.Forms.TreeNode("Node3")
            Dim TreeNode30 As System.Windows.Forms.TreeNode = New System.Windows.Forms.TreeNode("Node4")
            Dim TreeNode31 As System.Windows.Forms.TreeNode = New System.Windows.Forms.TreeNode("Node5")
            Dim TreeNode32 As System.Windows.Forms.TreeNode = New System.Windows.Forms.TreeNode("Node2", New System.Windows.Forms.TreeNode() {TreeNode29, TreeNode30, TreeNode31})
            Dim TreeNode33 As System.Windows.Forms.TreeNode = New System.Windows.Forms.TreeNode("Node7")
            Dim TreeNode34 As System.Windows.Forms.TreeNode = New System.Windows.Forms.TreeNode("Node8")
            Dim TreeNode35 As System.Windows.Forms.TreeNode = New System.Windows.Forms.TreeNode("Node6", New System.Windows.Forms.TreeNode() {TreeNode33, TreeNode34})
            Dim TreeNode36 As System.Windows.Forms.TreeNode = New System.Windows.Forms.TreeNode("Node10")
            Dim TreeNode37 As System.Windows.Forms.TreeNode = New System.Windows.Forms.TreeNode("Node11")
            Dim TreeNode38 As System.Windows.Forms.TreeNode = New System.Windows.Forms.TreeNode("Node12")
            Dim TreeNode39 As System.Windows.Forms.TreeNode = New System.Windows.Forms.TreeNode("Node9", New System.Windows.Forms.TreeNode() {TreeNode36, TreeNode37, TreeNode38})
            Me.Start = New System.Windows.Forms.Button
            Me.OptionsGroup = New System.Windows.Forms.GroupBox
            Me.CheckBox1 = New System.Windows.Forms.CheckBox
            Me.Label2 = New System.Windows.Forms.Label
            Me.Button1 = New System.Windows.Forms.Button
            Me.ProjectLabel = New System.Windows.Forms.Label
            Me.AccountLabel = New System.Windows.Forms.Label
            Me.ComboBox2 = New System.Windows.Forms.ComboBox
            Me.Project = New System.Windows.Forms.ComboBox
            Me.Account = New System.Windows.Forms.ComboBox
            Me.ScheduleGroup = New System.Windows.Forms.GroupBox
            Me.Panel3 = New System.Windows.Forms.Panel
            Me.StartDefault = New System.Windows.Forms.RadioButton
            Me.StartTime = New System.Windows.Forms.DateTimePicker
            Me.StartAt = New System.Windows.Forms.RadioButton
            Me.StartStartup = New System.Windows.Forms.RadioButton
            Me.Panel2 = New System.Windows.Forms.Panel
            Me.UntilStopped = New System.Windows.Forms.RadioButton
            Me.UntilCount = New System.Windows.Forms.Label
            Me.UntilTime = New System.Windows.Forms.DateTimePicker
            Me.NumericUpDown2 = New System.Windows.Forms.NumericUpDown
            Me.RadioButton1 = New System.Windows.Forms.RadioButton
            Me.UntilAt = New System.Windows.Forms.RadioButton
            Me.StartLabel = New System.Windows.Forms.Label
            Me.RepeatInterval = New System.Windows.Forms.NumericUpDown
            Me.RepeatUnit = New System.Windows.Forms.ComboBox
            Me.Repeat = New System.Windows.Forms.CheckBox
            Me.SpecialGroup = New System.Windows.Forms.GroupBox
            Me.ScriptList = New System.Windows.Forms.TreeView
            Me.OptionsGroup.SuspendLayout()
            Me.ScheduleGroup.SuspendLayout()
            Me.Panel3.SuspendLayout()
            Me.Panel2.SuspendLayout()
            CType(Me.NumericUpDown2, System.ComponentModel.ISupportInitialize).BeginInit()
            CType(Me.RepeatInterval, System.ComponentModel.ISupportInitialize).BeginInit()
            Me.SuspendLayout()
            '
            'Start
            '
            Me.Start.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
            Me.Start.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.Start.Location = New System.Drawing.Point(528, 419)
            Me.Start.Name = "Start"
            Me.Start.Size = New System.Drawing.Size(104, 29)
            Me.Start.TabIndex = 10
            Me.Start.Text = "Start"
            Me.Start.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
            Me.Start.UseVisualStyleBackColor = True
            '
            'OptionsGroup
            '
            Me.OptionsGroup.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                        Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
            Me.OptionsGroup.Controls.Add(Me.CheckBox1)
            Me.OptionsGroup.Controls.Add(Me.Label2)
            Me.OptionsGroup.Controls.Add(Me.Button1)
            Me.OptionsGroup.Controls.Add(Me.ProjectLabel)
            Me.OptionsGroup.Controls.Add(Me.AccountLabel)
            Me.OptionsGroup.Controls.Add(Me.ComboBox2)
            Me.OptionsGroup.Controls.Add(Me.Project)
            Me.OptionsGroup.Controls.Add(Me.Account)
            Me.OptionsGroup.Location = New System.Drawing.Point(173, 130)
            Me.OptionsGroup.Name = "OptionsGroup"
            Me.OptionsGroup.Size = New System.Drawing.Size(458, 81)
            Me.OptionsGroup.TabIndex = 8
            Me.OptionsGroup.TabStop = False
            Me.OptionsGroup.Text = "General options"
            '
            'CheckBox1
            '
            Me.CheckBox1.AutoSize = True
            Me.CheckBox1.Location = New System.Drawing.Point(353, 54)
            Me.CheckBox1.Name = "CheckBox1"
            Me.CheckBox1.Size = New System.Drawing.Size(103, 17)
            Me.CheckBox1.TabIndex = 7
            Me.CheckBox1.Text = "Read-only mode"
            Me.CheckBox1.UseVisualStyleBackColor = True
            '
            'Label2
            '
            Me.Label2.AutoSize = True
            Me.Label2.Location = New System.Drawing.Point(6, 54)
            Me.Label2.Name = "Label2"
            Me.Label2.Size = New System.Drawing.Size(173, 13)
            Me.Label2.TabIndex = 6
            Me.Label2.Text = "If permission not granted is required"
            '
            'Button1
            '
            Me.Button1.Location = New System.Drawing.Point(352, 18)
            Me.Button1.Name = "Button1"
            Me.Button1.Size = New System.Drawing.Size(98, 23)
            Me.Button1.TabIndex = 5
            Me.Button1.Text = "Permissions..."
            Me.Button1.UseVisualStyleBackColor = True
            '
            'ProjectLabel
            '
            Me.ProjectLabel.AutoSize = True
            Me.ProjectLabel.Location = New System.Drawing.Point(6, 23)
            Me.ProjectLabel.Name = "ProjectLabel"
            Me.ProjectLabel.Size = New System.Drawing.Size(43, 13)
            Me.ProjectLabel.TabIndex = 3
            Me.ProjectLabel.Text = "Project:"
            Me.ProjectLabel.TextAlign = System.Drawing.ContentAlignment.TopRight
            '
            'AccountLabel
            '
            Me.AccountLabel.AutoSize = True
            Me.AccountLabel.Location = New System.Drawing.Point(168, 22)
            Me.AccountLabel.Name = "AccountLabel"
            Me.AccountLabel.Size = New System.Drawing.Size(50, 13)
            Me.AccountLabel.TabIndex = 3
            Me.AccountLabel.Text = "Account:"
            Me.AccountLabel.TextAlign = System.Drawing.ContentAlignment.TopRight
            '
            'ComboBox2
            '
            Me.ComboBox2.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
            Me.ComboBox2.FormattingEnabled = True
            Me.ComboBox2.Items.AddRange(New Object() {"prompt", "ignore", "stop script"})
            Me.ComboBox2.Location = New System.Drawing.Point(185, 50)
            Me.ComboBox2.Name = "ComboBox2"
            Me.ComboBox2.Size = New System.Drawing.Size(120, 21)
            Me.ComboBox2.TabIndex = 2
            '
            'Project
            '
            Me.Project.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
            Me.Project.FormattingEnabled = True
            Me.Project.Location = New System.Drawing.Point(49, 19)
            Me.Project.Name = "Project"
            Me.Project.Size = New System.Drawing.Size(110, 21)
            Me.Project.TabIndex = 2
            '
            'Account
            '
            Me.Account.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
            Me.Account.FormattingEnabled = True
            Me.Account.Location = New System.Drawing.Point(218, 19)
            Me.Account.Name = "Account"
            Me.Account.Size = New System.Drawing.Size(110, 21)
            Me.Account.TabIndex = 2
            '
            'ScheduleGroup
            '
            Me.ScheduleGroup.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                        Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
            Me.ScheduleGroup.Controls.Add(Me.Panel3)
            Me.ScheduleGroup.Controls.Add(Me.Panel2)
            Me.ScheduleGroup.Controls.Add(Me.StartLabel)
            Me.ScheduleGroup.Controls.Add(Me.RepeatInterval)
            Me.ScheduleGroup.Controls.Add(Me.RepeatUnit)
            Me.ScheduleGroup.Controls.Add(Me.Repeat)
            Me.ScheduleGroup.Location = New System.Drawing.Point(173, 8)
            Me.ScheduleGroup.Name = "ScheduleGroup"
            Me.ScheduleGroup.Size = New System.Drawing.Size(458, 116)
            Me.ScheduleGroup.TabIndex = 9
            Me.ScheduleGroup.TabStop = False
            Me.ScheduleGroup.Text = "Schedule"
            '
            'Panel3
            '
            Me.Panel3.Controls.Add(Me.StartDefault)
            Me.Panel3.Controls.Add(Me.StartTime)
            Me.Panel3.Controls.Add(Me.StartAt)
            Me.Panel3.Controls.Add(Me.StartStartup)
            Me.Panel3.Location = New System.Drawing.Point(9, 40)
            Me.Panel3.Margin = New System.Windows.Forms.Padding(0)
            Me.Panel3.Name = "Panel3"
            Me.Panel3.Size = New System.Drawing.Size(198, 72)
            Me.Panel3.TabIndex = 5
            '
            'StartDefault
            '
            Me.StartDefault.AutoSize = True
            Me.StartDefault.Checked = True
            Me.StartDefault.Location = New System.Drawing.Point(3, 3)
            Me.StartDefault.Name = "StartDefault"
            Me.StartDefault.Size = New System.Drawing.Size(107, 17)
            Me.StartDefault.TabIndex = 11
            Me.StartDefault.TabStop = True
            Me.StartDefault.Text = "when I click Start"
            Me.StartDefault.UseVisualStyleBackColor = True
            '
            'StartTime
            '
            Me.StartTime.CustomFormat = "d MMM yyyy HH:mm:ss"
            Me.StartTime.Format = System.Windows.Forms.DateTimePickerFormat.Custom
            Me.StartTime.Location = New System.Drawing.Point(36, 48)
            Me.StartTime.MaxDate = New Date(2099, 12, 31, 0, 0, 0, 0)
            Me.StartTime.MinDate = New Date(2000, 1, 1, 0, 0, 0, 0)
            Me.StartTime.Name = "StartTime"
            Me.StartTime.Size = New System.Drawing.Size(156, 20)
            Me.StartTime.TabIndex = 4
            '
            'StartAt
            '
            Me.StartAt.AutoSize = True
            Me.StartAt.Location = New System.Drawing.Point(3, 49)
            Me.StartAt.Name = "StartAt"
            Me.StartAt.Size = New System.Drawing.Size(34, 17)
            Me.StartAt.TabIndex = 12
            Me.StartAt.Text = "at"
            Me.StartAt.UseVisualStyleBackColor = True
            '
            'StartStartup
            '
            Me.StartStartup.AutoSize = True
            Me.StartStartup.Location = New System.Drawing.Point(3, 26)
            Me.StartStartup.Name = "StartStartup"
            Me.StartStartup.Size = New System.Drawing.Size(138, 17)
            Me.StartStartup.TabIndex = 11
            Me.StartStartup.Text = "every time Huggle starts"
            Me.StartStartup.UseVisualStyleBackColor = True
            '
            'Panel2
            '
            Me.Panel2.Controls.Add(Me.UntilStopped)
            Me.Panel2.Controls.Add(Me.UntilCount)
            Me.Panel2.Controls.Add(Me.UntilTime)
            Me.Panel2.Controls.Add(Me.NumericUpDown2)
            Me.Panel2.Controls.Add(Me.RadioButton1)
            Me.Panel2.Controls.Add(Me.UntilAt)
            Me.Panel2.Location = New System.Drawing.Point(227, 40)
            Me.Panel2.Margin = New System.Windows.Forms.Padding(0)
            Me.Panel2.Name = "Panel2"
            Me.Panel2.Size = New System.Drawing.Size(227, 72)
            Me.Panel2.TabIndex = 5
            '
            'UntilStopped
            '
            Me.UntilStopped.AutoSize = True
            Me.UntilStopped.Checked = True
            Me.UntilStopped.Location = New System.Drawing.Point(3, 3)
            Me.UntilStopped.Name = "UntilStopped"
            Me.UntilStopped.Size = New System.Drawing.Size(85, 17)
            Me.UntilStopped.TabIndex = 11
            Me.UntilStopped.TabStop = True
            Me.UntilStopped.Text = "until stopped"
            Me.UntilStopped.UseVisualStyleBackColor = True
            '
            'UntilCount
            '
            Me.UntilCount.AutoSize = True
            Me.UntilCount.Location = New System.Drawing.Point(70, 28)
            Me.UntilCount.Name = "UntilCount"
            Me.UntilCount.Size = New System.Drawing.Size(31, 13)
            Me.UntilCount.TabIndex = 14
            Me.UntilCount.Text = "times"
            '
            'UntilTime
            '
            Me.UntilTime.CustomFormat = "d MMM yyyy HH:mm:ss"
            Me.UntilTime.Format = System.Windows.Forms.DateTimePickerFormat.Custom
            Me.UntilTime.Location = New System.Drawing.Point(46, 48)
            Me.UntilTime.MaxDate = New Date(2099, 12, 31, 0, 0, 0, 0)
            Me.UntilTime.MinDate = New Date(2000, 1, 1, 0, 0, 0, 0)
            Me.UntilTime.Name = "UntilTime"
            Me.UntilTime.Size = New System.Drawing.Size(156, 20)
            Me.UntilTime.TabIndex = 10
            '
            'NumericUpDown2
            '
            Me.NumericUpDown2.Location = New System.Drawing.Point(21, 25)
            Me.NumericUpDown2.Maximum = New Decimal(New Integer() {9999, 0, 0, 0})
            Me.NumericUpDown2.Minimum = New Decimal(New Integer() {1, 0, 0, 0})
            Me.NumericUpDown2.Name = "NumericUpDown2"
            Me.NumericUpDown2.Size = New System.Drawing.Size(46, 20)
            Me.NumericUpDown2.TabIndex = 13
            Me.NumericUpDown2.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
            Me.NumericUpDown2.Value = New Decimal(New Integer() {1, 0, 0, 0})
            '
            'RadioButton1
            '
            Me.RadioButton1.AutoSize = True
            Me.RadioButton1.Location = New System.Drawing.Point(3, 26)
            Me.RadioButton1.Name = "RadioButton1"
            Me.RadioButton1.Size = New System.Drawing.Size(28, 17)
            Me.RadioButton1.TabIndex = 11
            Me.RadioButton1.Text = " "
            Me.RadioButton1.UseVisualStyleBackColor = True
            '
            'UntilAt
            '
            Me.UntilAt.AutoSize = True
            Me.UntilAt.Location = New System.Drawing.Point(3, 49)
            Me.UntilAt.Name = "UntilAt"
            Me.UntilAt.Size = New System.Drawing.Size(44, 17)
            Me.UntilAt.TabIndex = 12
            Me.UntilAt.Text = "until"
            Me.UntilAt.UseVisualStyleBackColor = True
            '
            'StartLabel
            '
            Me.StartLabel.AutoSize = True
            Me.StartLabel.Location = New System.Drawing.Point(6, 21)
            Me.StartLabel.Name = "StartLabel"
            Me.StartLabel.Size = New System.Drawing.Size(57, 13)
            Me.StartLabel.TabIndex = 15
            Me.StartLabel.Text = "Start script"
            '
            'RepeatInterval
            '
            Me.RepeatInterval.Enabled = False
            Me.RepeatInterval.Location = New System.Drawing.Point(318, 17)
            Me.RepeatInterval.Maximum = New Decimal(New Integer() {1000, 0, 0, 0})
            Me.RepeatInterval.Minimum = New Decimal(New Integer() {1, 0, 0, 0})
            Me.RepeatInterval.Name = "RepeatInterval"
            Me.RepeatInterval.Size = New System.Drawing.Size(40, 20)
            Me.RepeatInterval.TabIndex = 8
            Me.RepeatInterval.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
            Me.RepeatInterval.Value = New Decimal(New Integer() {5, 0, 0, 0})
            '
            'RepeatUnit
            '
            Me.RepeatUnit.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
            Me.RepeatUnit.Enabled = False
            Me.RepeatUnit.FormattingEnabled = True
            Me.RepeatUnit.Items.AddRange(New Object() {"seconds", "minutes", "hours", "days"})
            Me.RepeatUnit.Location = New System.Drawing.Point(361, 16)
            Me.RepeatUnit.Name = "RepeatUnit"
            Me.RepeatUnit.Size = New System.Drawing.Size(68, 21)
            Me.RepeatUnit.TabIndex = 7
            '
            'Repeat
            '
            Me.Repeat.AutoSize = True
            Me.Repeat.Location = New System.Drawing.Point(229, 18)
            Me.Repeat.Name = "Repeat"
            Me.Repeat.Size = New System.Drawing.Size(90, 17)
            Me.Repeat.TabIndex = 5
            Me.Repeat.Text = "Repeat every"
            Me.Repeat.UseVisualStyleBackColor = True
            '
            'SpecialGroup
            '
            Me.SpecialGroup.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                        Or System.Windows.Forms.AnchorStyles.Left) _
                        Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
            Me.SpecialGroup.Location = New System.Drawing.Point(173, 217)
            Me.SpecialGroup.Name = "SpecialGroup"
            Me.SpecialGroup.Size = New System.Drawing.Size(458, 198)
            Me.SpecialGroup.TabIndex = 7
            Me.SpecialGroup.TabStop = False
            Me.SpecialGroup.Text = "Script options"
            '
            'ScriptList
            '
            Me.ScriptList.FullRowSelect = True
            Me.ScriptList.Location = New System.Drawing.Point(4, 6)
            Me.ScriptList.Name = "ScriptList"
            TreeNode27.Name = "Node1"
            TreeNode27.Text = "Node1"
            TreeNode28.Name = "Node0"
            TreeNode28.Text = "Node0"
            TreeNode29.Name = "Node3"
            TreeNode29.Text = "Node3"
            TreeNode30.Name = "Node4"
            TreeNode30.Text = "Node4"
            TreeNode31.Name = "Node5"
            TreeNode31.Text = "Node5"
            TreeNode32.Name = "Node2"
            TreeNode32.Text = "Node2"
            TreeNode33.Name = "Node7"
            TreeNode33.Text = "Node7"
            TreeNode34.Name = "Node8"
            TreeNode34.Text = "Node8"
            TreeNode35.Name = "Node6"
            TreeNode35.Text = "Node6"
            TreeNode36.Name = "Node10"
            TreeNode36.Text = "Node10"
            TreeNode37.Name = "Node11"
            TreeNode37.Text = "Node11"
            TreeNode38.Name = "Node12"
            TreeNode38.Text = "Node12"
            TreeNode39.Name = "Node9"
            TreeNode39.Text = "Node9"
            Me.ScriptList.Nodes.AddRange(New System.Windows.Forms.TreeNode() {TreeNode28, TreeNode32, TreeNode35, TreeNode39})
            Me.ScriptList.ShowLines = False
            Me.ScriptList.ShowPlusMinus = False
            Me.ScriptList.Size = New System.Drawing.Size(164, 442)
            Me.ScriptList.TabIndex = 6
            '
            'TaskForm
            '
            Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
            Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
            Me.ClientSize = New System.Drawing.Size(634, 454)
            Me.Controls.Add(Me.Start)
            Me.Controls.Add(Me.OptionsGroup)
            Me.Controls.Add(Me.ScheduleGroup)
            Me.Controls.Add(Me.SpecialGroup)
            Me.Controls.Add(Me.ScriptList)
            Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
            Me.MaximizeBox = False
            Me.Name = "TaskForm"
            Me.Text = "Run script"
            Me.OptionsGroup.ResumeLayout(False)
            Me.OptionsGroup.PerformLayout()
            Me.ScheduleGroup.ResumeLayout(False)
            Me.ScheduleGroup.PerformLayout()
            Me.Panel3.ResumeLayout(False)
            Me.Panel3.PerformLayout()
            Me.Panel2.ResumeLayout(False)
            Me.Panel2.PerformLayout()
            CType(Me.NumericUpDown2, System.ComponentModel.ISupportInitialize).EndInit()
            CType(Me.RepeatInterval, System.ComponentModel.ISupportInitialize).EndInit()
            Me.ResumeLayout(False)

        End Sub
        Private WithEvents Start As System.Windows.Forms.Button
        Private WithEvents OptionsGroup As System.Windows.Forms.GroupBox
        Private WithEvents CheckBox1 As System.Windows.Forms.CheckBox
        Private WithEvents Label2 As System.Windows.Forms.Label
        Friend WithEvents Button1 As System.Windows.Forms.Button
        Private WithEvents ProjectLabel As System.Windows.Forms.Label
        Private WithEvents AccountLabel As System.Windows.Forms.Label
        Private WithEvents ComboBox2 As System.Windows.Forms.ComboBox
        Private WithEvents Project As System.Windows.Forms.ComboBox
        Private WithEvents Account As System.Windows.Forms.ComboBox
        Private WithEvents ScheduleGroup As System.Windows.Forms.GroupBox
        Private WithEvents Panel3 As System.Windows.Forms.Panel
        Private WithEvents StartDefault As System.Windows.Forms.RadioButton
        Private WithEvents StartTime As System.Windows.Forms.DateTimePicker
        Friend WithEvents StartAt As System.Windows.Forms.RadioButton
        Private WithEvents StartStartup As System.Windows.Forms.RadioButton
        Private WithEvents Panel2 As System.Windows.Forms.Panel
        Private WithEvents UntilStopped As System.Windows.Forms.RadioButton
        Private WithEvents UntilCount As System.Windows.Forms.Label
        Private WithEvents UntilTime As System.Windows.Forms.DateTimePicker
        Private WithEvents NumericUpDown2 As System.Windows.Forms.NumericUpDown
        Private WithEvents RadioButton1 As System.Windows.Forms.RadioButton
        Friend WithEvents UntilAt As System.Windows.Forms.RadioButton
        Private WithEvents StartLabel As System.Windows.Forms.Label
        Private WithEvents RepeatInterval As System.Windows.Forms.NumericUpDown
        Private WithEvents RepeatUnit As System.Windows.Forms.ComboBox
        Private WithEvents Repeat As System.Windows.Forms.CheckBox
        Private WithEvents SpecialGroup As System.Windows.Forms.GroupBox
        Private WithEvents ScriptList As System.Windows.Forms.TreeView
    End Class
End Namespace