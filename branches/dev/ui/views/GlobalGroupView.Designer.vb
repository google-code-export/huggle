Namespace Huggle.UI
    <Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
    Partial Class GlobalGroupView
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
            Me.NameColumn = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
            Me.Count = New System.Windows.Forms.Label()
            Me.GroupList = New System.Windows.Forms.EnhancedListView()
            Me.Title = New System.Windows.Forms.Label()
            Me.Splitter = New System.Windows.Forms.EnhancedSplitContainer()
            Me.GroupName = New System.Windows.Forms.Label()
            Me.SubSplitter = New System.Windows.Forms.EnhancedSplitContainer()
            Me.WikiCount = New System.Windows.Forms.Label()
            Me.RightsCount = New System.Windows.Forms.Label()
            Me.RightsLabel = New System.Windows.Forms.Label()
            Me.Applicability = New System.Windows.Forms.Label()
            Me.RightsList = New System.Windows.Forms.EnhancedListView()
            Me.RightColumn = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
            Me.WikiList = New System.Windows.Forms.EnhancedListView()
            Me.WikiColumn = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
            Me.Splitter.Panel1.SuspendLayout()
            Me.Splitter.Panel2.SuspendLayout()
            Me.Splitter.SuspendLayout()
            Me.SubSplitter.Panel1.SuspendLayout()
            Me.SubSplitter.Panel2.SuspendLayout()
            Me.SubSplitter.SuspendLayout()
            Me.SuspendLayout()
            '
            'NameColumn
            '
            Me.NameColumn.Text = "Name"
            Me.NameColumn.Width = 247
            '
            'Count
            '
            Me.Count.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
            Me.Count.Location = New System.Drawing.Point(210, 9)
            Me.Count.Name = "Count"
            Me.Count.Size = New System.Drawing.Size(60, 13)
            Me.Count.TabIndex = 16
            Me.Count.Text = "0 items"
            Me.Count.TextAlign = System.Drawing.ContentAlignment.TopRight
            '
            'GroupList
            '
            Me.GroupList.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                        Or System.Windows.Forms.AnchorStyles.Left) _
                        Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
            Me.GroupList.Columns.AddRange(New System.Windows.Forms.ColumnHeader() {Me.NameColumn})
            Me.GroupList.FlexibleColumn = 0
            Me.GroupList.FullRowSelect = True
            Me.GroupList.GridLines = True
            Me.GroupList.Location = New System.Drawing.Point(0, 28)
            Me.GroupList.Name = "GroupList"
            Me.GroupList.ShowGroups = False
            Me.GroupList.Size = New System.Drawing.Size(270, 303)
            Me.GroupList.SortOnColumnClick = True
            Me.GroupList.TabIndex = 15
            Me.GroupList.UseCompatibleStateImageBehavior = False
            Me.GroupList.View = System.Windows.Forms.View.Details
            '
            'Title
            '
            Me.Title.AutoSize = True
            Me.Title.Font = New System.Drawing.Font("Segoe UI", 14.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.Title.Location = New System.Drawing.Point(-2, 0)
            Me.Title.Name = "Title"
            Me.Title.Size = New System.Drawing.Size(172, 25)
            Me.Title.TabIndex = 23
            Me.Title.Text = "Global user groups"
            '
            'Splitter
            '
            Me.Splitter.Dock = System.Windows.Forms.DockStyle.Fill
            Me.Splitter.Location = New System.Drawing.Point(0, 0)
            Me.Splitter.Name = "Splitter"
            '
            'Splitter.Panel1
            '
            Me.Splitter.Panel1.Controls.Add(Me.GroupList)
            Me.Splitter.Panel1.Controls.Add(Me.Title)
            Me.Splitter.Panel1.Controls.Add(Me.Count)
            '
            'Splitter.Panel2
            '
            Me.Splitter.Panel2.Controls.Add(Me.GroupName)
            Me.Splitter.Panel2.Controls.Add(Me.SubSplitter)
            Me.Splitter.Size = New System.Drawing.Size(539, 331)
            Me.Splitter.SplitterDistance = 270
            Me.Splitter.TabIndex = 25
            '
            'GroupName
            '
            Me.GroupName.AutoSize = True
            Me.GroupName.Font = New System.Drawing.Font("Segoe UI", 14.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.GroupName.Location = New System.Drawing.Point(-5, 0)
            Me.GroupName.Name = "GroupName"
            Me.GroupName.Size = New System.Drawing.Size(63, 25)
            Me.GroupName.TabIndex = 24
            Me.GroupName.Text = "group"
            '
            'SubSplitter
            '
            Me.SubSplitter.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                        Or System.Windows.Forms.AnchorStyles.Left) _
                        Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
            Me.SubSplitter.Location = New System.Drawing.Point(0, 0)
            Me.SubSplitter.Name = "SubSplitter"
            Me.SubSplitter.Orientation = System.Windows.Forms.Orientation.Horizontal
            '
            'SubSplitter.Panel1
            '
            Me.SubSplitter.Panel1.Controls.Add(Me.WikiCount)
            Me.SubSplitter.Panel1.Controls.Add(Me.RightsCount)
            Me.SubSplitter.Panel1.Controls.Add(Me.RightsLabel)
            Me.SubSplitter.Panel1.Controls.Add(Me.Applicability)
            Me.SubSplitter.Panel1.Controls.Add(Me.RightsList)
            '
            'SubSplitter.Panel2
            '
            Me.SubSplitter.Panel2.Controls.Add(Me.WikiList)
            Me.SubSplitter.Size = New System.Drawing.Size(265, 331)
            Me.SubSplitter.SplitterDistance = 185
            Me.SubSplitter.TabIndex = 18
            '
            'WikiCount
            '
            Me.WikiCount.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
            Me.WikiCount.Location = New System.Drawing.Point(205, 172)
            Me.WikiCount.Name = "WikiCount"
            Me.WikiCount.Size = New System.Drawing.Size(60, 13)
            Me.WikiCount.TabIndex = 26
            Me.WikiCount.Text = "0 items"
            Me.WikiCount.TextAlign = System.Drawing.ContentAlignment.TopRight
            '
            'RightsCount
            '
            Me.RightsCount.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
            Me.RightsCount.Location = New System.Drawing.Point(205, 29)
            Me.RightsCount.Name = "RightsCount"
            Me.RightsCount.Size = New System.Drawing.Size(60, 13)
            Me.RightsCount.TabIndex = 25
            Me.RightsCount.Text = "0 items"
            Me.RightsCount.TextAlign = System.Drawing.ContentAlignment.TopRight
            '
            'RightsLabel
            '
            Me.RightsLabel.AutoSize = True
            Me.RightsLabel.Location = New System.Drawing.Point(-3, 29)
            Me.RightsLabel.Name = "RightsLabel"
            Me.RightsLabel.Size = New System.Drawing.Size(170, 13)
            Me.RightsLabel.TabIndex = 19
            Me.RightsLabel.Text = "This group has the following rights:"
            '
            'Applicability
            '
            Me.Applicability.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
            Me.Applicability.AutoSize = True
            Me.Applicability.Location = New System.Drawing.Point(-3, 172)
            Me.Applicability.Name = "Applicability"
            Me.Applicability.Size = New System.Drawing.Size(196, 13)
            Me.Applicability.TabIndex = 18
            Me.Applicability.Text = "This group applies to the following wikis:"
            '
            'RightsList
            '
            Me.RightsList.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                        Or System.Windows.Forms.AnchorStyles.Left) _
                        Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
            Me.RightsList.Columns.AddRange(New System.Windows.Forms.ColumnHeader() {Me.RightColumn})
            Me.RightsList.FlexibleColumn = 0
            Me.RightsList.FullRowSelect = True
            Me.RightsList.GridLines = True
            Me.RightsList.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.None
            Me.RightsList.Location = New System.Drawing.Point(0, 45)
            Me.RightsList.Name = "RightsList"
            Me.RightsList.ShowGroups = False
            Me.RightsList.Size = New System.Drawing.Size(265, 119)
            Me.RightsList.SortOnColumnClick = True
            Me.RightsList.TabIndex = 16
            Me.RightsList.UseCompatibleStateImageBehavior = False
            Me.RightsList.View = System.Windows.Forms.View.Details
            '
            'RightColumn
            '
            Me.RightColumn.Text = "Right"
            Me.RightColumn.Width = 242
            '
            'WikiList
            '
            Me.WikiList.Columns.AddRange(New System.Windows.Forms.ColumnHeader() {Me.WikiColumn})
            Me.WikiList.Dock = System.Windows.Forms.DockStyle.Fill
            Me.WikiList.FlexibleColumn = 0
            Me.WikiList.FullRowSelect = True
            Me.WikiList.GridLines = True
            Me.WikiList.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.None
            Me.WikiList.Location = New System.Drawing.Point(0, 0)
            Me.WikiList.Name = "WikiList"
            Me.WikiList.ShowGroups = False
            Me.WikiList.Size = New System.Drawing.Size(265, 142)
            Me.WikiList.SortOnColumnClick = True
            Me.WikiList.TabIndex = 17
            Me.WikiList.UseCompatibleStateImageBehavior = False
            Me.WikiList.View = System.Windows.Forms.View.Details
            '
            'WikiColumn
            '
            Me.WikiColumn.Text = "Wiki"
            Me.WikiColumn.Width = 242
            '
            'GlobalGroupView
            '
            Me.Controls.Add(Me.Splitter)
            Me.Name = "GlobalGroupView"
            Me.Size = New System.Drawing.Size(539, 331)
            Me.Splitter.Panel1.ResumeLayout(False)
            Me.Splitter.Panel1.PerformLayout()
            Me.Splitter.Panel2.ResumeLayout(False)
            Me.Splitter.Panel2.PerformLayout()
            Me.Splitter.ResumeLayout(False)
            Me.SubSplitter.Panel1.ResumeLayout(False)
            Me.SubSplitter.Panel1.PerformLayout()
            Me.SubSplitter.Panel2.ResumeLayout(False)
            Me.SubSplitter.ResumeLayout(False)
            Me.ResumeLayout(False)

        End Sub
        Private WithEvents Count As System.Windows.Forms.Label
        Private WithEvents GroupList As System.Windows.Forms.EnhancedListView
        Private WithEvents Title As System.Windows.Forms.Label
        Private WithEvents GroupColumn As System.Windows.Forms.ColumnHeader
        Private WithEvents NameColumn As System.Windows.Forms.ColumnHeader
        Private WithEvents Splitter As System.Windows.Forms.EnhancedSplitContainer
        Private WithEvents RightsCount As System.Windows.Forms.Label
        Private WithEvents GroupName As System.Windows.Forms.Label
        Private WithEvents SubSplitter As System.Windows.Forms.EnhancedSplitContainer
        Private WithEvents RightsList As System.Windows.Forms.EnhancedListView
        Private WithEvents RightColumn As System.Windows.Forms.ColumnHeader
        Private WithEvents Applicability As System.Windows.Forms.Label
        Private WithEvents WikiList As System.Windows.Forms.EnhancedListView
        Private WithEvents WikiColumn As System.Windows.Forms.ColumnHeader
        Private WithEvents RightsLabel As System.Windows.Forms.Label
        Private WithEvents WikiCount As System.Windows.Forms.Label

    End Class
End Namespace