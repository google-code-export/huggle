Namespace Huggle.UI
    <Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
    Partial Class AbuseFilterDetailView
        Inherits System.Windows.Forms.UserControl

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
            Me.Notes = New System.Windows.Forms.TextBox()
            Me.Views = New System.Windows.Forms.ListBox()
            Me.DescriptionPanel = New System.Windows.Forms.FlowLayoutPanel()
            Me.Description = New System.Windows.Forms.LinkLabel()
            Me.Status = New System.Windows.Forms.Label()
            Me.Modified = New System.Windows.Forms.Label()
            Me.Actions = New System.Windows.Forms.Label()
            Me.RateLimit = New System.Windows.Forms.Label()
            Me.TagsPanel = New System.Windows.Forms.Panel()
            Me.TagDetail = New System.Windows.Forms.Label()
            Me.ViewPanel = New System.Windows.Forms.Panel()
            Me.Pattern = New System.Windows.Forms.TextBox()
            Me.Splitter = New System.Windows.Forms.SplitContainer()
            Me.PrivateFilter = New System.Windows.Forms.Label()
            Me.EditFilter = New System.Windows.Forms.LinkLabel()
            Me.TagsList = New System.Windows.Forms.EnhancedListView()
            Me.TagColumn = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
            Me.Wait = New Huggle.UI.WaitControl()
            Me.DescriptionPanel.SuspendLayout()
            Me.TagsPanel.SuspendLayout()
            Me.ViewPanel.SuspendLayout()
            Me.Splitter.Panel1.SuspendLayout()
            Me.Splitter.Panel2.SuspendLayout()
            Me.Splitter.SuspendLayout()
            Me.SuspendLayout()
            '
            'Notes
            '
            Me.Notes.BackColor = System.Drawing.SystemColors.Window
            Me.Notes.Font = New System.Drawing.Font("Consolas", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.Notes.Location = New System.Drawing.Point(9, 215)
            Me.Notes.Multiline = True
            Me.Notes.Name = "Notes"
            Me.Notes.ReadOnly = True
            Me.Notes.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
            Me.Notes.Size = New System.Drawing.Size(294, 65)
            Me.Notes.TabIndex = 2
            Me.Notes.Visible = False
            '
            'Views
            '
            Me.Views.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed
            Me.Views.FormattingEnabled = True
            Me.Views.ItemHeight = 20
            Me.Views.Location = New System.Drawing.Point(0, 0)
            Me.Views.Name = "Views"
            Me.Views.Size = New System.Drawing.Size(81, 84)
            Me.Views.TabIndex = 1
            '
            'DescriptionPanel
            '
            Me.DescriptionPanel.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                        Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
            Me.DescriptionPanel.AutoSize = True
            Me.DescriptionPanel.BackColor = System.Drawing.Color.Transparent
            Me.DescriptionPanel.Controls.Add(Me.Description)
            Me.DescriptionPanel.Controls.Add(Me.Status)
            Me.DescriptionPanel.Controls.Add(Me.Modified)
            Me.DescriptionPanel.Controls.Add(Me.Actions)
            Me.DescriptionPanel.Controls.Add(Me.RateLimit)
            Me.DescriptionPanel.FlowDirection = System.Windows.Forms.FlowDirection.TopDown
            Me.DescriptionPanel.Location = New System.Drawing.Point(9, 17)
            Me.DescriptionPanel.Name = "DescriptionPanel"
            Me.DescriptionPanel.Padding = New System.Windows.Forms.Padding(0, 1, 0, 0)
            Me.DescriptionPanel.Size = New System.Drawing.Size(446, 96)
            Me.DescriptionPanel.TabIndex = 0
            Me.DescriptionPanel.Visible = False
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
            Me.Description.TabIndex = 0
            Me.Description.TabStop = True
            Me.Description.Text = "Description      "
            Me.Description.VisitedLinkColor = System.Drawing.SystemColors.HotTrack
            '
            'Status
            '
            Me.Status.AutoSize = True
            Me.Status.Location = New System.Drawing.Point(3, 19)
            Me.Status.Name = "Status"
            Me.Status.Padding = New System.Windows.Forms.Padding(0, 3, 0, 0)
            Me.Status.Size = New System.Drawing.Size(40, 16)
            Me.Status.TabIndex = 1
            Me.Status.Text = "Status:"
            '
            'Modified
            '
            Me.Modified.AutoSize = True
            Me.Modified.Location = New System.Drawing.Point(3, 35)
            Me.Modified.Name = "Modified"
            Me.Modified.Padding = New System.Windows.Forms.Padding(0, 3, 0, 0)
            Me.Modified.Size = New System.Drawing.Size(222, 16)
            Me.Modified.TabIndex = 2
            Me.Modified.Text = "Last modified by foo at 00:00, 1 January 2000"
            '
            'Actions
            '
            Me.Actions.AutoSize = True
            Me.Actions.Location = New System.Drawing.Point(3, 51)
            Me.Actions.Name = "Actions"
            Me.Actions.Padding = New System.Windows.Forms.Padding(0, 3, 0, 0)
            Me.Actions.Size = New System.Drawing.Size(45, 16)
            Me.Actions.TabIndex = 3
            Me.Actions.Text = "Actions:"
            '
            'RateLimit
            '
            Me.RateLimit.AutoSize = True
            Me.RateLimit.Location = New System.Drawing.Point(3, 67)
            Me.RateLimit.Name = "RateLimit"
            Me.RateLimit.Padding = New System.Windows.Forms.Padding(0, 3, 0, 0)
            Me.RateLimit.Size = New System.Drawing.Size(53, 16)
            Me.RateLimit.TabIndex = 4
            Me.RateLimit.Text = "Rate limit:"
            Me.RateLimit.Visible = False
            '
            'TagsPanel
            '
            Me.TagsPanel.Controls.Add(Me.TagDetail)
            Me.TagsPanel.Controls.Add(Me.TagsList)
            Me.TagsPanel.Location = New System.Drawing.Point(6, 299)
            Me.TagsPanel.Name = "TagsPanel"
            Me.TagsPanel.Size = New System.Drawing.Size(297, 98)
            Me.TagsPanel.TabIndex = 3
            '
            'TagDetail
            '
            Me.TagDetail.AutoSize = True
            Me.TagDetail.Location = New System.Drawing.Point(3, 6)
            Me.TagDetail.Name = "TagDetail"
            Me.TagDetail.Size = New System.Drawing.Size(126, 13)
            Me.TagDetail.TabIndex = 0
            Me.TagDetail.Text = "Tags applied by this filter:"
            '
            'ViewPanel
            '
            Me.ViewPanel.Controls.Add(Me.Pattern)
            Me.ViewPanel.Controls.Add(Me.DescriptionPanel)
            Me.ViewPanel.Controls.Add(Me.TagsPanel)
            Me.ViewPanel.Controls.Add(Me.Notes)
            Me.ViewPanel.Dock = System.Windows.Forms.DockStyle.Fill
            Me.ViewPanel.Location = New System.Drawing.Point(0, 0)
            Me.ViewPanel.Name = "ViewPanel"
            Me.ViewPanel.Size = New System.Drawing.Size(546, 416)
            Me.ViewPanel.TabIndex = 0
            '
            'Pattern
            '
            Me.Pattern.BackColor = System.Drawing.SystemColors.Window
            Me.Pattern.Font = New System.Drawing.Font("Consolas", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.Pattern.Location = New System.Drawing.Point(9, 133)
            Me.Pattern.Multiline = True
            Me.Pattern.Name = "Pattern"
            Me.Pattern.ReadOnly = True
            Me.Pattern.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
            Me.Pattern.Size = New System.Drawing.Size(294, 65)
            Me.Pattern.TabIndex = 1
            Me.Pattern.Visible = False
            '
            'Splitter
            '
            Me.Splitter.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                        Or System.Windows.Forms.AnchorStyles.Left) _
                        Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
            Me.Splitter.FixedPanel = System.Windows.Forms.FixedPanel.Panel2
            Me.Splitter.IsSplitterFixed = True
            Me.Splitter.Location = New System.Drawing.Point(87, 0)
            Me.Splitter.Name = "Splitter"
            Me.Splitter.Orientation = System.Windows.Forms.Orientation.Horizontal
            '
            'Splitter.Panel1
            '
            Me.Splitter.Panel1.Controls.Add(Me.ViewPanel)
            Me.Splitter.Panel1MinSize = 20
            '
            'Splitter.Panel2
            '
            Me.Splitter.Panel2.Controls.Add(Me.PrivateFilter)
            Me.Splitter.Panel2.Controls.Add(Me.Wait)
            Me.Splitter.Panel2MinSize = 20
            Me.Splitter.Size = New System.Drawing.Size(546, 445)
            Me.Splitter.SplitterDistance = 416
            Me.Splitter.TabIndex = 2
            '
            'PrivateFilter
            '
            Me.PrivateFilter.AutoSize = True
            Me.PrivateFilter.Location = New System.Drawing.Point(3, 5)
            Me.PrivateFilter.Name = "PrivateFilter"
            Me.PrivateFilter.Size = New System.Drawing.Size(428, 13)
            Me.PrivateFilter.TabIndex = 1
            Me.PrivateFilter.Text = "Some details of this filter have been hidden, and you do not have permission to v" & _
                "iew them"
            '
            'EditFilter
            '
            Me.EditFilter.BackColor = System.Drawing.Color.Transparent
            Me.EditFilter.LinkBehavior = System.Windows.Forms.LinkBehavior.HoverUnderline
            Me.EditFilter.LinkColor = System.Drawing.SystemColors.HotTrack
            Me.EditFilter.Location = New System.Drawing.Point(0, 90)
            Me.EditFilter.Name = "EditFilter"
            Me.EditFilter.Size = New System.Drawing.Size(81, 13)
            Me.EditFilter.TabIndex = 0
            Me.EditFilter.TabStop = True
            Me.EditFilter.Text = "Edit filter"
            Me.EditFilter.TextAlign = System.Drawing.ContentAlignment.TopCenter
            '
            'TagsList
            '
            Me.TagsList.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                        Or System.Windows.Forms.AnchorStyles.Left) _
                        Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
            Me.TagsList.Columns.AddRange(New System.Windows.Forms.ColumnHeader() {Me.TagColumn})
            Me.TagsList.FlexibleColumn = 0
            Me.TagsList.FullRowSelect = True
            Me.TagsList.GridLines = True
            Me.TagsList.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.None
            Me.TagsList.HideSelection = False
            Me.TagsList.Location = New System.Drawing.Point(3, 22)
            Me.TagsList.Name = "TagsList"
            Me.TagsList.SelectedValue = Nothing
            Me.TagsList.ShowGroups = False
            Me.TagsList.Size = New System.Drawing.Size(291, 73)
            Me.TagsList.TabIndex = 1
            Me.TagsList.UseCompatibleStateImageBehavior = False
            Me.TagsList.View = System.Windows.Forms.View.Details
            '
            'TagColumn
            '
            Me.TagColumn.Text = "Tag"
            Me.TagColumn.Width = 268
            '
            'Wait
            '
            Me.Wait.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                        Or System.Windows.Forms.AnchorStyles.Left) _
                        Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
            Me.Wait.Location = New System.Drawing.Point(3, 7)
            Me.Wait.Name = "Wait"
            Me.Wait.Size = New System.Drawing.Size(80, 16)
            Me.Wait.TabIndex = 0
            Me.Wait.TabStop = False
            Me.Wait.TextPosition = Huggle.UI.WaitControl.WaitTextPosition.Horizontal
            '
            'AbuseFilterDetailView
            '
            Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
            Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
            Me.Controls.Add(Me.EditFilter)
            Me.Controls.Add(Me.Splitter)
            Me.Controls.Add(Me.Views)
            Me.Name = "AbuseFilterDetailView"
            Me.Size = New System.Drawing.Size(633, 445)
            Me.DescriptionPanel.ResumeLayout(False)
            Me.DescriptionPanel.PerformLayout()
            Me.TagsPanel.ResumeLayout(False)
            Me.TagsPanel.PerformLayout()
            Me.ViewPanel.ResumeLayout(False)
            Me.ViewPanel.PerformLayout()
            Me.Splitter.Panel1.ResumeLayout(False)
            Me.Splitter.Panel2.ResumeLayout(False)
            Me.Splitter.Panel2.PerformLayout()
            Me.Splitter.ResumeLayout(False)
            Me.ResumeLayout(False)

        End Sub

        Private WithEvents Notes As System.Windows.Forms.TextBox
        Private WithEvents DescriptionPanel As System.Windows.Forms.FlowLayoutPanel
        Private WithEvents Description As System.Windows.Forms.LinkLabel
        Private WithEvents Status As System.Windows.Forms.Label
        Private WithEvents Modified As System.Windows.Forms.Label
        Private WithEvents Actions As System.Windows.Forms.Label
        Private WithEvents RateLimit As System.Windows.Forms.Label
        Private WithEvents TagsList As System.Windows.Forms.EnhancedListView
        Private WithEvents TagColumn As System.Windows.Forms.ColumnHeader
        Friend WithEvents TagsPanel As System.Windows.Forms.Panel
        Private WithEvents TagDetail As System.Windows.Forms.Label
        Private WithEvents ViewPanel As System.Windows.Forms.Panel
        Private WithEvents Splitter As System.Windows.Forms.SplitContainer
        Private WithEvents Wait As Huggle.UI.WaitControl
        Private WithEvents Views As System.Windows.Forms.ListBox
        Private WithEvents Pattern As System.Windows.Forms.TextBox
        Private WithEvents PrivateFilter As System.Windows.Forms.Label
        Private WithEvents EditFilter As System.Windows.Forms.LinkLabel

    End Class
End Namespace