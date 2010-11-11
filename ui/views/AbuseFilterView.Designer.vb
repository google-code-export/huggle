Namespace Huggle.UI
    <Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
    Partial Friend Class AbuseFilterView
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
            Me.FilterDetailView = New Huggle.UI.AbuseFilterDetailView()
            Me.Splitter.Panel1.SuspendLayout()
            Me.Splitter.Panel2.SuspendLayout()
            Me.Splitter.SuspendLayout()
            Me.SelLayout.SuspendLayout()
            Me.SuspendLayout()
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
            Me.Splitter.Panel2.Controls.Add(Me.FilterDetailView)
            Me.Splitter.Size = New System.Drawing.Size(562, 432)
            Me.Splitter.SplitterDistance = 270
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
            Me.CreateFilter.Location = New System.Drawing.Point(451, 6)
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
            Me.FilterList.FlexibleColumn = 1
            Me.FilterList.FullRowSelect = True
            Me.FilterList.GridLines = True
            Me.FilterList.HideSelection = False
            Me.FilterList.Location = New System.Drawing.Point(3, 55)
            Me.FilterList.Name = "FilterList"
            Me.FilterList.SelectedValue = Nothing
            Me.FilterList.ShowGroups = False
            Me.FilterList.Size = New System.Drawing.Size(556, 215)
            Me.FilterList.SortOnColumnClick = True
            Me.FilterList.TabIndex = 1
            Me.FilterList.UseCompatibleStateImageBehavior = False
            Me.FilterList.View = System.Windows.Forms.View.Details
            Me.FilterList.VirtualMode = True
            '
            'IdColumn
            '
            Me.IdColumn.Text = "ID"
            Me.IdColumn.Width = 40
            '
            'NameColumn
            '
            Me.NameColumn.Text = "Name"
            Me.NameColumn.Width = 238
            '
            'StatusColumn
            '
            Me.StatusColumn.Text = "Status"
            Me.StatusColumn.Width = 100
            '
            'ActionsColumn
            '
            Me.ActionsColumn.Text = "Actions"
            Me.ActionsColumn.Width = 100
            '
            'CountColumn
            '
            Me.CountColumn.Text = "Count"
            Me.CountColumn.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
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
            Me.VisibilitySel.Location = New System.Drawing.Point(3, 3)
            Me.VisibilitySel.Name = "VisibilitySel"
            Me.VisibilitySel.Size = New System.Drawing.Size(140, 21)
            Me.VisibilitySel.TabIndex = 0
            '
            'StatusSel
            '
            Me.StatusSel.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
            Me.StatusSel.FormattingEnabled = True
            Me.StatusSel.Location = New System.Drawing.Point(149, 3)
            Me.StatusSel.Name = "StatusSel"
            Me.StatusSel.Size = New System.Drawing.Size(140, 21)
            Me.StatusSel.TabIndex = 1
            '
            'ActionSel
            '
            Me.ActionSel.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
            Me.ActionSel.FormattingEnabled = True
            Me.ActionSel.Location = New System.Drawing.Point(295, 3)
            Me.ActionSel.Name = "ActionSel"
            Me.ActionSel.Size = New System.Drawing.Size(140, 21)
            Me.ActionSel.TabIndex = 2
            '
            'FilterCount
            '
            Me.FilterCount.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
            Me.FilterCount.Location = New System.Drawing.Point(493, 31)
            Me.FilterCount.Name = "FilterCount"
            Me.FilterCount.Size = New System.Drawing.Size(66, 13)
            Me.FilterCount.TabIndex = 12
            Me.FilterCount.Text = "0 items"
            Me.FilterCount.TextAlign = System.Drawing.ContentAlignment.TopRight
            '
            'FilterDetailView
            '
            Me.FilterDetailView.Dock = System.Windows.Forms.DockStyle.Fill
            Me.FilterDetailView.Filter = Nothing
            Me.FilterDetailView.Location = New System.Drawing.Point(0, 0)
            Me.FilterDetailView.Name = "FilterDetailView"
            Me.FilterDetailView.Session = Nothing
            Me.FilterDetailView.Size = New System.Drawing.Size(562, 158)
            Me.FilterDetailView.TabIndex = 0
            '
            'AbuseFilterView
            '
            Me.Controls.Add(Me.Splitter)
            Me.Name = "AbuseFilterView"
            Me.Size = New System.Drawing.Size(562, 432)
            Me.Splitter.Panel1.ResumeLayout(False)
            Me.Splitter.Panel1.PerformLayout()
            Me.Splitter.Panel2.ResumeLayout(False)
            Me.Splitter.ResumeLayout(False)
            Me.SelLayout.ResumeLayout(False)
            Me.ResumeLayout(False)

        End Sub
        Private WithEvents Count As System.Windows.Forms.Label
        Private WithEvents List As System.Windows.Forms.EnhancedListView
        Private WithEvents TagNameColumn As System.Windows.Forms.ColumnHeader
        Private WithEvents TagDescriptionColumn As System.Windows.Forms.ColumnHeader
        Private WithEvents TagCountColumn As System.Windows.Forms.ColumnHeader
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
        Private WithEvents Title As System.Windows.Forms.Label
        Public WithEvents FilterDetailView As Huggle.UI.AbuseFilterDetailView

    End Class
End Namespace