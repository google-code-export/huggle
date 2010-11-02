Namespace Huggle.UI
    <Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
    Partial Class UserGroupView
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
            Me.GroupColumn = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
            Me.DescriptionColumn = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
            Me.CountColumn = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
            Me.Count = New System.Windows.Forms.Label()
            Me.List = New System.Windows.Forms.EnhancedListView()
            Me.Title = New System.Windows.Forms.Label()
            Me.RightsList = New System.Windows.Forms.EnhancedListView()
            Me.RightColumn = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
            Me.GroupName = New System.Windows.Forms.Label()
            Me.Splitter = New System.Windows.Forms.EnhancedSplitContainer()
            Me.RightsCount = New System.Windows.Forms.Label()
            Me.Splitter.Panel1.SuspendLayout()
            Me.Splitter.Panel2.SuspendLayout()
            Me.Splitter.SuspendLayout()
            Me.SuspendLayout()
            '
            'GroupColumn
            '
            Me.GroupColumn.Text = "Group"
            Me.GroupColumn.Width = 113
            '
            'DescriptionColumn
            '
            Me.DescriptionColumn.Text = "Description"
            Me.DescriptionColumn.Width = 141
            '
            'CountColumn
            '
            Me.CountColumn.Text = "Size"
            Me.CountColumn.Width = 58
            '
            'Count
            '
            Me.Count.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
            Me.Count.Location = New System.Drawing.Point(275, 9)
            Me.Count.Name = "Count"
            Me.Count.Size = New System.Drawing.Size(60, 13)
            Me.Count.TabIndex = 16
            Me.Count.Text = "0 groups"
            Me.Count.TextAlign = System.Drawing.ContentAlignment.TopRight
            '
            'List
            '
            Me.List.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                        Or System.Windows.Forms.AnchorStyles.Left) _
                        Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
            Me.List.Columns.AddRange(New System.Windows.Forms.ColumnHeader() {Me.GroupColumn, Me.DescriptionColumn, Me.CountColumn})
            Me.List.FlexibleColumn = 1
            Me.List.FullRowSelect = True
            Me.List.GridLines = True
            Me.List.HideSelection = False
            Me.List.Location = New System.Drawing.Point(0, 28)
            Me.List.Name = "List"
            Me.List.ShowGroups = False
            Me.List.Size = New System.Drawing.Size(335, 297)
            Me.List.SortOnColumnClick = True
            Me.List.TabIndex = 15
            Me.List.UseCompatibleStateImageBehavior = False
            Me.List.View = System.Windows.Forms.View.Details
            '
            'Title
            '
            Me.Title.AutoSize = True
            Me.Title.Font = New System.Drawing.Font("Segoe UI", 14.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.Title.Location = New System.Drawing.Point(0, 0)
            Me.Title.Name = "Title"
            Me.Title.Size = New System.Drawing.Size(114, 25)
            Me.Title.TabIndex = 23
            Me.Title.Text = "User groups"
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
            Me.RightsList.Location = New System.Drawing.Point(0, 28)
            Me.RightsList.Name = "RightsList"
            Me.RightsList.ShowGroups = False
            Me.RightsList.Size = New System.Drawing.Size(194, 297)
            Me.RightsList.SortOnColumnClick = True
            Me.RightsList.TabIndex = 24
            Me.RightsList.UseCompatibleStateImageBehavior = False
            Me.RightsList.View = System.Windows.Forms.View.Details
            '
            'RightColumn
            '
            Me.RightColumn.Text = ""
            Me.RightColumn.Width = 171
            '
            'GroupName
            '
            Me.GroupName.AutoSize = True
            Me.GroupName.Font = New System.Drawing.Font("Segoe UI", 14.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.GroupName.Location = New System.Drawing.Point(-5, 0)
            Me.GroupName.Name = "GroupName"
            Me.GroupName.Size = New System.Drawing.Size(117, 25)
            Me.GroupName.TabIndex = 25
            Me.GroupName.Text = "Group name"
            '
            'Splitter
            '
            Me.Splitter.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                        Or System.Windows.Forms.AnchorStyles.Left) _
                        Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
            Me.Splitter.Location = New System.Drawing.Point(3, 3)
            Me.Splitter.Name = "Splitter"
            '
            'Splitter.Panel1
            '
            Me.Splitter.Panel1.Controls.Add(Me.List)
            Me.Splitter.Panel1.Controls.Add(Me.Title)
            Me.Splitter.Panel1.Controls.Add(Me.Count)
            '
            'Splitter.Panel2
            '
            Me.Splitter.Panel2.Controls.Add(Me.RightsCount)
            Me.Splitter.Panel2.Controls.Add(Me.RightsList)
            Me.Splitter.Panel2.Controls.Add(Me.GroupName)
            Me.Splitter.Size = New System.Drawing.Size(533, 325)
            Me.Splitter.SplitterDistance = 335
            Me.Splitter.TabIndex = 26
            '
            'RightsCount
            '
            Me.RightsCount.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
            Me.RightsCount.Location = New System.Drawing.Point(134, 9)
            Me.RightsCount.Name = "RightsCount"
            Me.RightsCount.Size = New System.Drawing.Size(60, 13)
            Me.RightsCount.TabIndex = 27
            Me.RightsCount.Text = "0 rights"
            Me.RightsCount.TextAlign = System.Drawing.ContentAlignment.TopRight
            '
            'UserGroupView
            '
            Me.Controls.Add(Me.Splitter)
            Me.Name = "UserGroupView"
            Me.Size = New System.Drawing.Size(539, 331)
            Me.Splitter.Panel1.ResumeLayout(False)
            Me.Splitter.Panel1.PerformLayout()
            Me.Splitter.Panel2.ResumeLayout(False)
            Me.Splitter.Panel2.PerformLayout()
            Me.Splitter.ResumeLayout(False)
            Me.ResumeLayout(False)

        End Sub
        Private WithEvents Count As System.Windows.Forms.Label
        Private WithEvents List As System.Windows.Forms.EnhancedListView
        Private WithEvents Title As System.Windows.Forms.Label
        Private WithEvents RightsList As System.Windows.Forms.EnhancedListView
        Private WithEvents GroupName As System.Windows.Forms.Label
        Private WithEvents RightColumn As System.Windows.Forms.ColumnHeader
        Private WithEvents Splitter As System.Windows.Forms.EnhancedSplitContainer
        Private WithEvents RightsCount As System.Windows.Forms.Label
        Private WithEvents GroupColumn As System.Windows.Forms.ColumnHeader
        Private WithEvents DescriptionColumn As System.Windows.Forms.ColumnHeader
        Private WithEvents CountColumn As System.Windows.Forms.ColumnHeader

    End Class
End Namespace