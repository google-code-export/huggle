Namespace Huggle.UI
    <Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
    Partial Class AbuseFilterView
        Inherits Viewer

        'UserControl overrides dispose to clean up the component list.
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
            Me.Description = New System.Windows.Forms.LinkLabel()
            Me.Modified = New System.Windows.Forms.Label()
            Me.Status = New System.Windows.Forms.Label()
            Me.Splitter = New System.Windows.Forms.SplitContainer()
            Me.Title = New System.Windows.Forms.Label()
            Me.CreateFilter = New System.Windows.Forms.LinkLabel()
            Me.FilterList = New System.Windows.Forms.EnhancedListView()
            Me.IdColumn = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
            Me.NameColumn = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
            Me.StatusColumn = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
            Me.ActionsColumn = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
            Me.CountColumn = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
            Me.SelLayout = New System.Windows.Forms.FlowLayoutPanel()
            Me.VisibilitySel = New System.Windows.Forms.ComboBox()
            Me.StatusSel = New System.Windows.Forms.ComboBox()
            Me.ActionSel = New System.Windows.Forms.ComboBox()
            Me.FilterCount = New System.Windows.Forms.Label()
            Me.Details = New System.Windows.Forms.TabControl()
            Me.DescriptionTab = New System.Windows.Forms.TabPage()
            Me.AbuseFilterProps = New System.Windows.Forms.FlowLayoutPanel()
            Me.Actions = New System.Windows.Forms.Label()
            Me.RateLimit = New System.Windows.Forms.Label()
            Me.Progress = New System.Windows.Forms.Label()
            Me.Indicator = New Huggle.UI.WaitControl()
            Me.PatternTab = New System.Windows.Forms.TabPage()
            Me.Pattern = New System.Windows.Forms.TextBox()
            Me.NotesTab = New System.Windows.Forms.TabPage()
            Me.Notes = New System.Windows.Forms.TextBox()
            Me.EditFilter = New System.Windows.Forms.LinkLabel()
            Me.FilterImage = New System.Windows.Forms.PictureBox()
            Me.RestrictedIcon = New System.Windows.Forms.PictureBox()
            Me.Restricted = New System.Windows.Forms.Label()
            Me.Splitter.Panel1.SuspendLayout()
            Me.Splitter.Panel2.SuspendLayout()
            Me.Splitter.SuspendLayout()
            Me.SelLayout.SuspendLayout()
            Me.Details.SuspendLayout()
            Me.DescriptionTab.SuspendLayout()
            Me.AbuseFilterProps.SuspendLayout()
            Me.PatternTab.SuspendLayout()
            Me.NotesTab.SuspendLayout()
            CType(Me.FilterImage, System.ComponentModel.ISupportInitialize).BeginInit()
            CType(Me.RestrictedIcon, System.ComponentModel.ISupportInitialize).BeginInit()
            Me.SuspendLayout()
            '
            'Description
            '
            Me.Description.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                        Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
            Me.Description.AutoSize = True
            Me.Description.Font = New System.Drawing.Font("Tahoma", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.Description.ForeColor = System.Drawing.SystemColors.HotTrack
            Me.Description.LinkBehavior = System.Windows.Forms.LinkBehavior.HoverUnderline
            Me.Description.LinkColor = System.Drawing.SystemColors.HotTrack
            Me.Description.Location = New System.Drawing.Point(3, 1)
            Me.Description.Name = "Description"
            Me.Description.Size = New System.Drawing.Size(222, 18)
            Me.Description.TabIndex = 5
            Me.Description.TabStop = True
            Me.Description.Text = "Description      "
            Me.Description.VisitedLinkColor = System.Drawing.SystemColors.HotTrack
            '
            'Modified
            '
            Me.Modified.AutoSize = True
            Me.Modified.Location = New System.Drawing.Point(3, 35)
            Me.Modified.Name = "Modified"
            Me.Modified.Padding = New System.Windows.Forms.Padding(0, 3, 0, 0)
            Me.Modified.Size = New System.Drawing.Size(222, 16)
            Me.Modified.TabIndex = 6
            Me.Modified.Text = "Last modified by foo at 00:00, 1 January 2000"
            '
            'Status
            '
            Me.Status.AutoSize = True
            Me.Status.Location = New System.Drawing.Point(3, 19)
            Me.Status.Name = "Status"
            Me.Status.Padding = New System.Windows.Forms.Padding(0, 3, 0, 0)
            Me.Status.Size = New System.Drawing.Size(40, 16)
            Me.Status.TabIndex = 6
            Me.Status.Text = "Status:"
            '
            'Splitter
            '
            Me.Splitter.BackColor = System.Drawing.Color.Transparent
            Me.Splitter.Dock = System.Windows.Forms.DockStyle.Fill
            Me.Splitter.FixedPanel = System.Windows.Forms.FixedPanel.Panel2
            Me.Splitter.Location = New System.Drawing.Point(0, 0)
            Me.Splitter.Name = "Splitter"
            Me.Splitter.Orientation = System.Windows.Forms.Orientation.Horizontal
            '
            'Splitter.Panel1
            '
            Me.Splitter.Panel1.BackColor = System.Drawing.Color.Transparent
            Me.Splitter.Panel1.Controls.Add(Me.Title)
            Me.Splitter.Panel1.Controls.Add(Me.CreateFilter)
            Me.Splitter.Panel1.Controls.Add(Me.FilterList)
            Me.Splitter.Panel1.Controls.Add(Me.SelLayout)
            Me.Splitter.Panel1.Controls.Add(Me.FilterCount)
            '
            'Splitter.Panel2
            '
            Me.Splitter.Panel2.Controls.Add(Me.Details)
            Me.Splitter.Panel2.Controls.Add(Me.EditFilter)
            Me.Splitter.Panel2.Controls.Add(Me.FilterImage)
            Me.Splitter.Panel2.Controls.Add(Me.RestrictedIcon)
            Me.Splitter.Panel2.Controls.Add(Me.Restricted)
            Me.Splitter.Size = New System.Drawing.Size(562, 432)
            Me.Splitter.SplitterDistance = 278
            Me.Splitter.TabIndex = 8
            '
            'Title
            '
            Me.Title.AutoSize = True
            Me.Title.Font = New System.Drawing.Font("Segoe UI", 14.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.Title.Location = New System.Drawing.Point(-2, -3)
            Me.Title.Name = "Title"
            Me.Title.Size = New System.Drawing.Size(116, 25)
            Me.Title.TabIndex = 15
            Me.Title.Text = "Abuse filters"
            '
            'CreateFilter
            '
            Me.CreateFilter.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
            Me.CreateFilter.LinkBehavior = System.Windows.Forms.LinkBehavior.HoverUnderline
            Me.CreateFilter.LinkColor = System.Drawing.SystemColors.HotTrack
            Me.CreateFilter.Location = New System.Drawing.Point(450, 6)
            Me.CreateFilter.Name = "CreateFilter"
            Me.CreateFilter.Size = New System.Drawing.Size(108, 13)
            Me.CreateFilter.TabIndex = 11
            Me.CreateFilter.TabStop = True
            Me.CreateFilter.Text = "Create new filter"
            Me.CreateFilter.TextAlign = System.Drawing.ContentAlignment.TopRight
            Me.CreateFilter.Visible = False
            '
            'FilterList
            '
            Me.FilterList.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                        Or System.Windows.Forms.AnchorStyles.Left) _
                        Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
            Me.FilterList.Columns.AddRange(New System.Windows.Forms.ColumnHeader() {Me.IdColumn, Me.NameColumn, Me.StatusColumn, Me.ActionsColumn, Me.CountColumn})
            Me.FilterList.FlexibleColumn = 0
            Me.FilterList.FullRowSelect = True
            Me.FilterList.GridLines = True
            Me.FilterList.HideSelection = False
            Me.FilterList.Location = New System.Drawing.Point(3, 55)
            Me.FilterList.Name = "FilterList"
            Me.FilterList.ShowGroups = False
            Me.FilterList.Size = New System.Drawing.Size(555, 220)
            Me.FilterList.SortOnColumnClick = True
            Me.FilterList.TabIndex = 1
            Me.FilterList.UseCompatibleStateImageBehavior = False
            Me.FilterList.View = System.Windows.Forms.View.Details
            '
            'IdColumn
            '
            Me.IdColumn.Text = "ID"
            Me.IdColumn.Width = 65
            '
            'NameColumn
            '
            Me.NameColumn.Text = "Name"
            Me.NameColumn.Width = 220
            '
            'StatusColumn
            '
            Me.StatusColumn.Text = "Status"
            Me.StatusColumn.Width = 110
            '
            'ActionsColumn
            '
            Me.ActionsColumn.Text = "Actions"
            Me.ActionsColumn.Width = 87
            '
            'CountColumn
            '
            Me.CountColumn.Text = "Count"
            Me.CountColumn.Width = 50
            '
            'SelLayout
            '
            Me.SelLayout.BackColor = System.Drawing.Color.Transparent
            Me.SelLayout.Controls.Add(Me.VisibilitySel)
            Me.SelLayout.Controls.Add(Me.StatusSel)
            Me.SelLayout.Controls.Add(Me.ActionSel)
            Me.SelLayout.Location = New System.Drawing.Point(3, 25)
            Me.SelLayout.Name = "SelLayout"
            Me.SelLayout.Size = New System.Drawing.Size(443, 28)
            Me.SelLayout.TabIndex = 14
            '
            'VisibilitySel
            '
            Me.VisibilitySel.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
            Me.VisibilitySel.FormattingEnabled = True
            Me.VisibilitySel.Items.AddRange(New Object() {"(Any visibility)", "Public", "Private"})
            Me.VisibilitySel.Location = New System.Drawing.Point(3, 3)
            Me.VisibilitySel.Name = "VisibilitySel"
            Me.VisibilitySel.Size = New System.Drawing.Size(140, 21)
            Me.VisibilitySel.TabIndex = 0
            '
            'StatusSel
            '
            Me.StatusSel.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
            Me.StatusSel.FormattingEnabled = True
            Me.StatusSel.Items.AddRange(New Object() {"(Any status)", "Enabled", "Disabled", "Deleted"})
            Me.StatusSel.Location = New System.Drawing.Point(149, 3)
            Me.StatusSel.Name = "StatusSel"
            Me.StatusSel.Size = New System.Drawing.Size(140, 21)
            Me.StatusSel.TabIndex = 1
            '
            'ActionSel
            '
            Me.ActionSel.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
            Me.ActionSel.FormattingEnabled = True
            Me.ActionSel.Items.AddRange(New Object() {"(All actions)", "Tag", "Warn", "Limit rate", "Disallow", "Remove confirmation", "Remove groups", "Block", "Block range"})
            Me.ActionSel.Location = New System.Drawing.Point(295, 3)
            Me.ActionSel.Name = "ActionSel"
            Me.ActionSel.Size = New System.Drawing.Size(140, 21)
            Me.ActionSel.TabIndex = 2
            '
            'FilterCount
            '
            Me.FilterCount.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
            Me.FilterCount.Location = New System.Drawing.Point(492, 31)
            Me.FilterCount.Name = "FilterCount"
            Me.FilterCount.Size = New System.Drawing.Size(66, 13)
            Me.FilterCount.TabIndex = 12
            Me.FilterCount.Text = "0 items"
            Me.FilterCount.TextAlign = System.Drawing.ContentAlignment.TopRight
            '
            'Details
            '
            Me.Details.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                        Or System.Windows.Forms.AnchorStyles.Left) _
                        Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
            Me.Details.Controls.Add(Me.DescriptionTab)
            Me.Details.Controls.Add(Me.PatternTab)
            Me.Details.Controls.Add(Me.NotesTab)
            Me.Details.Location = New System.Drawing.Point(77, 3)
            Me.Details.Name = "Details"
            Me.Details.SelectedIndex = 0
            Me.Details.Size = New System.Drawing.Size(482, 144)
            Me.Details.TabIndex = 14
            '
            'DescriptionTab
            '
            Me.DescriptionTab.Controls.Add(Me.AbuseFilterProps)
            Me.DescriptionTab.Controls.Add(Me.Progress)
            Me.DescriptionTab.Controls.Add(Me.Indicator)
            Me.DescriptionTab.Location = New System.Drawing.Point(4, 22)
            Me.DescriptionTab.Name = "DescriptionTab"
            Me.DescriptionTab.Size = New System.Drawing.Size(474, 118)
            Me.DescriptionTab.TabIndex = 3
            Me.DescriptionTab.Text = "Description"
            Me.DescriptionTab.UseVisualStyleBackColor = True
            '
            'AbuseFilterProps
            '
            Me.AbuseFilterProps.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                        Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
            Me.AbuseFilterProps.AutoSize = True
            Me.AbuseFilterProps.BackColor = System.Drawing.Color.Transparent
            Me.AbuseFilterProps.Controls.Add(Me.Description)
            Me.AbuseFilterProps.Controls.Add(Me.Status)
            Me.AbuseFilterProps.Controls.Add(Me.Modified)
            Me.AbuseFilterProps.Controls.Add(Me.Actions)
            Me.AbuseFilterProps.Controls.Add(Me.RateLimit)
            Me.AbuseFilterProps.FlowDirection = System.Windows.Forms.FlowDirection.TopDown
            Me.AbuseFilterProps.Location = New System.Drawing.Point(0, 0)
            Me.AbuseFilterProps.Name = "AbuseFilterProps"
            Me.AbuseFilterProps.Padding = New System.Windows.Forms.Padding(0, 1, 0, 0)
            Me.AbuseFilterProps.Size = New System.Drawing.Size(471, 96)
            Me.AbuseFilterProps.TabIndex = 9
            '
            'Actions
            '
            Me.Actions.AutoSize = True
            Me.Actions.Location = New System.Drawing.Point(3, 51)
            Me.Actions.Name = "Actions"
            Me.Actions.Padding = New System.Windows.Forms.Padding(0, 3, 0, 0)
            Me.Actions.Size = New System.Drawing.Size(45, 16)
            Me.Actions.TabIndex = 7
            Me.Actions.Text = "Actions:"
            '
            'RateLimit
            '
            Me.RateLimit.AutoSize = True
            Me.RateLimit.Location = New System.Drawing.Point(3, 67)
            Me.RateLimit.Name = "RateLimit"
            Me.RateLimit.Padding = New System.Windows.Forms.Padding(0, 3, 0, 0)
            Me.RateLimit.Size = New System.Drawing.Size(53, 16)
            Me.RateLimit.TabIndex = 8
            Me.RateLimit.Text = "Rate limit:"
            Me.RateLimit.Visible = False
            '
            'Progress
            '
            Me.Progress.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
            Me.Progress.AutoSize = True
            Me.Progress.Location = New System.Drawing.Point(24, 97)
            Me.Progress.Name = "Progress"
            Me.Progress.Size = New System.Drawing.Size(109, 13)
            Me.Progress.TabIndex = 13
            Me.Progress.Text = "Loading filter details..."
            Me.Progress.Visible = False
            '
            'Indicator
            '
            Me.Indicator.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
            Me.Indicator.BackColor = System.Drawing.SystemColors.ControlLightLight
            Me.Indicator.Location = New System.Drawing.Point(6, 95)
            Me.Indicator.MaximumSize = New System.Drawing.Size(16, 16)
            Me.Indicator.MinimumSize = New System.Drawing.Size(16, 16)
            Me.Indicator.Name = "Indicator"
            Me.Indicator.Size = New System.Drawing.Size(16, 16)
            Me.Indicator.TabIndex = 12
            Me.Indicator.TabStop = False
            Me.Indicator.TextPosition = Huggle.UI.WaitControl.WaitTextPosition.None
            Me.Indicator.Visible = False
            '
            'PatternTab
            '
            Me.PatternTab.Controls.Add(Me.Pattern)
            Me.PatternTab.Location = New System.Drawing.Point(4, 22)
            Me.PatternTab.Name = "PatternTab"
            Me.PatternTab.Padding = New System.Windows.Forms.Padding(3)
            Me.PatternTab.Size = New System.Drawing.Size(474, 118)
            Me.PatternTab.TabIndex = 0
            Me.PatternTab.Text = "Pattern"
            Me.PatternTab.UseVisualStyleBackColor = True
            '
            'Pattern
            '
            Me.Pattern.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                        Or System.Windows.Forms.AnchorStyles.Left) _
                        Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
            Me.Pattern.BackColor = System.Drawing.SystemColors.Window
            Me.Pattern.Font = New System.Drawing.Font("Consolas", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.Pattern.Location = New System.Drawing.Point(6, 6)
            Me.Pattern.Multiline = True
            Me.Pattern.Name = "Pattern"
            Me.Pattern.ReadOnly = True
            Me.Pattern.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
            Me.Pattern.Size = New System.Drawing.Size(466, 102)
            Me.Pattern.TabIndex = 0
            '
            'NotesTab
            '
            Me.NotesTab.Controls.Add(Me.Notes)
            Me.NotesTab.Location = New System.Drawing.Point(4, 22)
            Me.NotesTab.Name = "NotesTab"
            Me.NotesTab.Padding = New System.Windows.Forms.Padding(3)
            Me.NotesTab.Size = New System.Drawing.Size(474, 118)
            Me.NotesTab.TabIndex = 1
            Me.NotesTab.Text = "Notes"
            Me.NotesTab.UseVisualStyleBackColor = True
            '
            'Notes
            '
            Me.Notes.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                        Or System.Windows.Forms.AnchorStyles.Left) _
                        Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
            Me.Notes.BackColor = System.Drawing.SystemColors.Window
            Me.Notes.Font = New System.Drawing.Font("Consolas", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.Notes.Location = New System.Drawing.Point(6, 6)
            Me.Notes.Multiline = True
            Me.Notes.Name = "Notes"
            Me.Notes.ReadOnly = True
            Me.Notes.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
            Me.Notes.Size = New System.Drawing.Size(465, 102)
            Me.Notes.TabIndex = 1
            '
            'EditFilter
            '
            Me.EditFilter.BackColor = System.Drawing.Color.Transparent
            Me.EditFilter.LinkBehavior = System.Windows.Forms.LinkBehavior.HoverUnderline
            Me.EditFilter.LinkColor = System.Drawing.SystemColors.HotTrack
            Me.EditFilter.Location = New System.Drawing.Point(8, 69)
            Me.EditFilter.Name = "EditFilter"
            Me.EditFilter.Size = New System.Drawing.Size(64, 13)
            Me.EditFilter.TabIndex = 11
            Me.EditFilter.TabStop = True
            Me.EditFilter.Text = "Edit filter"
            Me.EditFilter.TextAlign = System.Drawing.ContentAlignment.TopCenter
            Me.EditFilter.Visible = False
            '
            'FilterImage
            '
            Me.FilterImage.BackColor = System.Drawing.Color.Transparent
            Me.FilterImage.Image = Global.Resources.abuse_filter
            Me.FilterImage.Location = New System.Drawing.Point(7, 4)
            Me.FilterImage.Name = "FilterImage"
            Me.FilterImage.Size = New System.Drawing.Size(64, 64)
            Me.FilterImage.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize
            Me.FilterImage.TabIndex = 4
            Me.FilterImage.TabStop = False
            Me.FilterImage.Visible = False
            '
            'RestrictedIcon
            '
            Me.RestrictedIcon.Location = New System.Drawing.Point(79, 6)
            Me.RestrictedIcon.Name = "RestrictedIcon"
            Me.RestrictedIcon.Size = New System.Drawing.Size(16, 16)
            Me.RestrictedIcon.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize
            Me.RestrictedIcon.TabIndex = 16
            Me.RestrictedIcon.TabStop = False
            Me.RestrictedIcon.Visible = False
            '
            'Restricted
            '
            Me.Restricted.AutoSize = True
            Me.Restricted.Location = New System.Drawing.Point(96, 8)
            Me.Restricted.Name = "Restricted"
            Me.Restricted.Size = New System.Drawing.Size(284, 13)
            Me.Restricted.TabIndex = 15
            Me.Restricted.Text = "User right ""abusefilter-view-private"" required to view details"
            Me.Restricted.Visible = False
            '
            'AbuseFilterView
            '
            Me.Controls.Add(Me.Splitter)
            Me.Name = "AbuseFilterView"
            Me.Size = New System.Drawing.Size(562, 432)
            Me.Splitter.Panel1.ResumeLayout(False)
            Me.Splitter.Panel1.PerformLayout()
            Me.Splitter.Panel2.ResumeLayout(False)
            Me.Splitter.Panel2.PerformLayout()
            Me.Splitter.ResumeLayout(False)
            Me.SelLayout.ResumeLayout(False)
            Me.Details.ResumeLayout(False)
            Me.DescriptionTab.ResumeLayout(False)
            Me.DescriptionTab.PerformLayout()
            Me.AbuseFilterProps.ResumeLayout(False)
            Me.AbuseFilterProps.PerformLayout()
            Me.PatternTab.ResumeLayout(False)
            Me.PatternTab.PerformLayout()
            Me.NotesTab.ResumeLayout(False)
            Me.NotesTab.PerformLayout()
            CType(Me.FilterImage, System.ComponentModel.ISupportInitialize).EndInit()
            CType(Me.RestrictedIcon, System.ComponentModel.ISupportInitialize).EndInit()
            Me.ResumeLayout(False)

        End Sub
        Private WithEvents Count As System.Windows.Forms.Label
        Private WithEvents List As System.Windows.Forms.EnhancedListView
        Private WithEvents TagNameColumn As System.Windows.Forms.ColumnHeader
        Private WithEvents TagDescriptionColumn As System.Windows.Forms.ColumnHeader
        Private WithEvents TagCountColumn As System.Windows.Forms.ColumnHeader
        Private WithEvents Description As System.Windows.Forms.LinkLabel
        Private WithEvents Modified As System.Windows.Forms.Label
        Private WithEvents Status As System.Windows.Forms.Label
        Private WithEvents Splitter As System.Windows.Forms.SplitContainer
        Private WithEvents SelLayout As System.Windows.Forms.FlowLayoutPanel
        Private WithEvents VisibilitySel As System.Windows.Forms.ComboBox
        Private WithEvents StatusSel As System.Windows.Forms.ComboBox
        Private WithEvents ActionSel As System.Windows.Forms.ComboBox
        Private WithEvents FilterCount As System.Windows.Forms.Label
        Private WithEvents FilterList As System.Windows.Forms.EnhancedListView
        Private WithEvents IdColumn As System.Windows.Forms.ColumnHeader
        Private WithEvents NameColumn As System.Windows.Forms.ColumnHeader
        Private WithEvents StatusColumn As System.Windows.Forms.ColumnHeader
        Private WithEvents ActionsColumn As System.Windows.Forms.ColumnHeader
        Private WithEvents CountColumn As System.Windows.Forms.ColumnHeader
        Private WithEvents CreateFilter As System.Windows.Forms.LinkLabel
        Private WithEvents Details As System.Windows.Forms.TabControl
        Private WithEvents DescriptionTab As System.Windows.Forms.TabPage
        Private WithEvents AbuseFilterProps As System.Windows.Forms.FlowLayoutPanel
        Private WithEvents Actions As System.Windows.Forms.Label
        Private WithEvents RateLimit As System.Windows.Forms.Label
        Private WithEvents PatternTab As System.Windows.Forms.TabPage
        Private WithEvents Pattern As System.Windows.Forms.TextBox
        Private WithEvents NotesTab As System.Windows.Forms.TabPage
        Private WithEvents Notes As System.Windows.Forms.TextBox
        Private WithEvents EditFilter As System.Windows.Forms.LinkLabel
        Private WithEvents FilterImage As System.Windows.Forms.PictureBox
        Private WithEvents Title As System.Windows.Forms.Label
        Private WithEvents Progress As System.Windows.Forms.Label
        Private WithEvents Indicator As WaitControl
        Private WithEvents Restricted As System.Windows.Forms.Label
        Private WithEvents RestrictedIcon As System.Windows.Forms.PictureBox

    End Class
End Namespace