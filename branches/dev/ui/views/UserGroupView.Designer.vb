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
            Me.CountColumn = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
            Me.GroupCount = New System.Windows.Forms.Label()
            Me.GroupList = New System.Windows.Forms.EnhancedListView()
            Me.Title = New System.Windows.Forms.Label()
            Me.RightsList = New System.Windows.Forms.EnhancedListView()
            Me.RightColumn = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
            Me.GroupName = New System.Windows.Forms.Label()
            Me.Splitter = New System.Windows.Forms.EnhancedSplitContainer()
            Me.RightsLabel = New System.Windows.Forms.Label()
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
            'CountColumn
            '
            Me.CountColumn.Text = "Size"
            Me.CountColumn.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
            Me.CountColumn.Width = 58
            '
            'GroupCount
            '
            Me.GroupCount.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
            Me.GroupCount.Location = New System.Drawing.Point(241, 9)
            Me.GroupCount.Name = "GroupCount"
            Me.GroupCount.Size = New System.Drawing.Size(60, 13)
            Me.GroupCount.TabIndex = 16
            Me.GroupCount.Text = "0 items"
            Me.GroupCount.TextAlign = System.Drawing.ContentAlignment.TopRight
            '
            'GroupList
            '
            Me.GroupList.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                        Or System.Windows.Forms.AnchorStyles.Left) _
                        Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
            Me.GroupList.Columns.AddRange(New System.Windows.Forms.ColumnHeader() {Me.GroupColumn, Me.CountColumn})
            Me.GroupList.FlexibleColumn = 0
            Me.GroupList.FullRowSelect = True
            Me.GroupList.GridLines = True
            Me.GroupList.HideSelection = False
            Me.GroupList.Location = New System.Drawing.Point(0, 28)
            Me.GroupList.Name = "GroupList"
            Me.GroupList.ShowGroups = False
            Me.GroupList.Size = New System.Drawing.Size(301, 297)
            Me.GroupList.SortOnColumnClick = True
            Me.GroupList.TabIndex = 15
            Me.GroupList.UseCompatibleStateImageBehavior = False
            Me.GroupList.View = System.Windows.Forms.View.Details
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
            Me.RightsList.Location = New System.Drawing.Point(0, 52)
            Me.RightsList.Name = "RightsList"
            Me.RightsList.ShowGroups = False
            Me.RightsList.Size = New System.Drawing.Size(228, 273)
            Me.RightsList.SortOnColumnClick = True
            Me.RightsList.TabIndex = 24
            Me.RightsList.UseCompatibleStateImageBehavior = False
            Me.RightsList.View = System.Windows.Forms.View.Details
            '
            'RightColumn
            '
            Me.RightColumn.Text = ""
            Me.RightColumn.Width = 205
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
            Me.Splitter.Panel1.Controls.Add(Me.GroupList)
            Me.Splitter.Panel1.Controls.Add(Me.Title)
            Me.Splitter.Panel1.Controls.Add(Me.GroupCount)
            '
            'Splitter.Panel2
            '
            Me.Splitter.Panel2.Controls.Add(Me.RightsLabel)
            Me.Splitter.Panel2.Controls.Add(Me.RightsCount)
            Me.Splitter.Panel2.Controls.Add(Me.RightsList)
            Me.Splitter.Panel2.Controls.Add(Me.GroupName)
            Me.Splitter.Size = New System.Drawing.Size(533, 325)
            Me.Splitter.SplitterDistance = 301
            Me.Splitter.TabIndex = 26
            '
            'RightsLabel
            '
            Me.RightsLabel.AutoSize = True
            Me.RightsLabel.Location = New System.Drawing.Point(-3, 32)
            Me.RightsLabel.Name = "RightsLabel"
            Me.RightsLabel.Size = New System.Drawing.Size(170, 13)
            Me.RightsLabel.TabIndex = 28
            Me.RightsLabel.Text = "This group has the following rights:"
            '
            'RightsCount
            '
            Me.RightsCount.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
            Me.RightsCount.Location = New System.Drawing.Point(168, 32)
            Me.RightsCount.Name = "RightsCount"
            Me.RightsCount.Size = New System.Drawing.Size(60, 13)
            Me.RightsCount.TabIndex = 27
            Me.RightsCount.Text = "0 items"
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
        Private WithEvents GroupCount As System.Windows.Forms.Label
        Private WithEvents GroupList As System.Windows.Forms.EnhancedListView
        Private WithEvents Title As System.Windows.Forms.Label
        Private WithEvents RightsList As System.Windows.Forms.EnhancedListView
        Private WithEvents GroupName As System.Windows.Forms.Label
        Private WithEvents RightColumn As System.Windows.Forms.ColumnHeader
        Private WithEvents Splitter As System.Windows.Forms.EnhancedSplitContainer
        Private WithEvents RightsCount As System.Windows.Forms.Label
        Private WithEvents GroupColumn As System.Windows.Forms.ColumnHeader
        Private WithEvents CountColumn As System.Windows.Forms.ColumnHeader
        Private WithEvents RightsLabel As System.Windows.Forms.Label

    End Class
End Namespace