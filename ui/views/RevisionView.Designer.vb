Namespace Huggle.UI
    <Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
    Partial Class RevisionView
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
            Me.Count = New System.Windows.Forms.Label()
            Me.Title = New System.Windows.Forms.Label()
            Me.RevisionList = New System.Windows.Forms.EnhancedListView()
            Me.IDColumn = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
            Me.TimeColumn = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
            Me.UserColumn = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
            Me.PageColumn = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
            Me.StatusColumn = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
            Me.ChangeColumn = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
            Me.SummaryColumn = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
            Me.FilterLayout = New System.Windows.Forms.FlowLayoutPanel()
            Me.FilterLabel = New System.Windows.Forms.Label()
            Me.CheckBox1 = New System.Windows.Forms.CheckBox()
            Me.CheckBox9 = New System.Windows.Forms.CheckBox()
            Me.CheckBox2 = New System.Windows.Forms.CheckBox()
            Me.CheckBox3 = New System.Windows.Forms.CheckBox()
            Me.CheckBox4 = New System.Windows.Forms.CheckBox()
            Me.CheckBox10 = New System.Windows.Forms.CheckBox()
            Me.CheckBox5 = New System.Windows.Forms.CheckBox()
            Me.CheckBox6 = New System.Windows.Forms.CheckBox()
            Me.CheckBox12 = New System.Windows.Forms.CheckBox()
            Me.CheckBox13 = New System.Windows.Forms.CheckBox()
            Me.CheckBox7 = New System.Windows.Forms.CheckBox()
            Me.CheckBox8 = New System.Windows.Forms.CheckBox()
            Me.CheckBox11 = New System.Windows.Forms.CheckBox()
            Me.CustomFilter = New System.Windows.Forms.Button()
            Me.FilterLayout.SuspendLayout()
            Me.SuspendLayout()
            '
            'Count
            '
            Me.Count.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
            Me.Count.Location = New System.Drawing.Point(662, 6)
            Me.Count.Name = "Count"
            Me.Count.Size = New System.Drawing.Size(60, 13)
            Me.Count.TabIndex = 16
            Me.Count.Text = "0 items"
            Me.Count.TextAlign = System.Drawing.ContentAlignment.TopRight
            '
            'Title
            '
            Me.Title.AutoSize = True
            Me.Title.Font = New System.Drawing.Font("Segoe UI", 14.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.Title.Location = New System.Drawing.Point(-2, -3)
            Me.Title.Name = "Title"
            Me.Title.Size = New System.Drawing.Size(90, 25)
            Me.Title.TabIndex = 23
            Me.Title.Text = "Revisions"
            '
            'RevisionList
            '
            Me.RevisionList.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                        Or System.Windows.Forms.AnchorStyles.Left) _
                        Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
            Me.RevisionList.Columns.AddRange(New System.Windows.Forms.ColumnHeader() {Me.IDColumn, Me.TimeColumn, Me.UserColumn, Me.PageColumn, Me.StatusColumn, Me.ChangeColumn, Me.SummaryColumn})
            Me.RevisionList.FlexibleColumn = 5
            Me.RevisionList.FullRowSelect = True
            Me.RevisionList.GridLines = True
            Me.RevisionList.HideSelection = False
            Me.RevisionList.Location = New System.Drawing.Point(3, 74)
            Me.RevisionList.Name = "RevisionList"
            Me.RevisionList.SelectedValue = Nothing
            Me.RevisionList.ShowGroups = False
            Me.RevisionList.Size = New System.Drawing.Size(716, 365)
            Me.RevisionList.SortOnColumnClick = True
            Me.RevisionList.TabIndex = 15
            Me.RevisionList.UseCompatibleStateImageBehavior = False
            Me.RevisionList.View = System.Windows.Forms.View.Details
            Me.RevisionList.VirtualMode = True
            '
            'IDColumn
            '
            Me.IDColumn.Text = "ID"
            Me.IDColumn.Width = 73
            '
            'TimeColumn
            '
            Me.TimeColumn.Text = "Time"
            Me.TimeColumn.Width = 118
            '
            'UserColumn
            '
            Me.UserColumn.Text = "User"
            Me.UserColumn.Width = 103
            '
            'PageColumn
            '
            Me.PageColumn.Text = "Page"
            Me.PageColumn.Width = 121
            '
            'StatusColumn
            '
            Me.StatusColumn.Text = "Status"
            Me.StatusColumn.Width = 124
            '
            'ChangeColumn
            '
            Me.ChangeColumn.Text = "Change"
            Me.ChangeColumn.Width = 53
            '
            'SummaryColumn
            '
            Me.SummaryColumn.Text = "Summary"
            Me.SummaryColumn.Width = 214
            '
            'FilterLayout
            '
            Me.FilterLayout.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                        Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
            Me.FilterLayout.Controls.Add(Me.FilterLabel)
            Me.FilterLayout.Controls.Add(Me.CheckBox1)
            Me.FilterLayout.Controls.Add(Me.CheckBox9)
            Me.FilterLayout.Controls.Add(Me.CheckBox2)
            Me.FilterLayout.Controls.Add(Me.CheckBox3)
            Me.FilterLayout.Controls.Add(Me.CheckBox4)
            Me.FilterLayout.Controls.Add(Me.CheckBox10)
            Me.FilterLayout.Controls.Add(Me.CheckBox5)
            Me.FilterLayout.Controls.Add(Me.CheckBox6)
            Me.FilterLayout.Controls.Add(Me.CheckBox12)
            Me.FilterLayout.Controls.Add(Me.CheckBox13)
            Me.FilterLayout.Controls.Add(Me.CheckBox7)
            Me.FilterLayout.Controls.Add(Me.CheckBox8)
            Me.FilterLayout.Controls.Add(Me.CheckBox11)
            Me.FilterLayout.Location = New System.Drawing.Point(3, 25)
            Me.FilterLayout.Name = "FilterLayout"
            Me.FilterLayout.Size = New System.Drawing.Size(607, 43)
            Me.FilterLayout.TabIndex = 28
            '
            'FilterLabel
            '
            Me.FilterLabel.AutoSize = True
            Me.FilterLabel.Dock = System.Windows.Forms.DockStyle.Fill
            Me.FilterLabel.Location = New System.Drawing.Point(3, 0)
            Me.FilterLabel.Name = "FilterLabel"
            Me.FilterLabel.Size = New System.Drawing.Size(32, 23)
            Me.FilterLabel.TabIndex = 32
            Me.FilterLabel.Text = "Filter:"
            Me.FilterLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
            '
            'CheckBox1
            '
            Me.CheckBox1.AutoSize = True
            Me.CheckBox1.Checked = True
            Me.CheckBox1.CheckState = System.Windows.Forms.CheckState.Indeterminate
            Me.CheckBox1.Location = New System.Drawing.Point(41, 3)
            Me.CheckBox1.Name = "CheckBox1"
            Me.CheckBox1.Size = New System.Drawing.Size(81, 17)
            Me.CheckBox1.TabIndex = 0
            Me.CheckBox1.Text = "Anonymous"
            Me.CheckBox1.ThreeState = True
            Me.CheckBox1.UseVisualStyleBackColor = True
            '
            'CheckBox9
            '
            Me.CheckBox9.AutoSize = True
            Me.CheckBox9.Checked = True
            Me.CheckBox9.CheckState = System.Windows.Forms.CheckState.Indeterminate
            Me.CheckBox9.Location = New System.Drawing.Point(128, 3)
            Me.CheckBox9.Name = "CheckBox9"
            Me.CheckBox9.Size = New System.Drawing.Size(65, 17)
            Me.CheckBox9.TabIndex = 8
            Me.CheckBox9.Text = "Assisted"
            Me.CheckBox9.ThreeState = True
            Me.CheckBox9.UseVisualStyleBackColor = True
            '
            'CheckBox2
            '
            Me.CheckBox2.AutoSize = True
            Me.CheckBox2.Checked = True
            Me.CheckBox2.CheckState = System.Windows.Forms.CheckState.Indeterminate
            Me.CheckBox2.Location = New System.Drawing.Point(199, 3)
            Me.CheckBox2.Name = "CheckBox2"
            Me.CheckBox2.Size = New System.Drawing.Size(42, 17)
            Me.CheckBox2.TabIndex = 1
            Me.CheckBox2.Text = "Bot"
            Me.CheckBox2.ThreeState = True
            Me.CheckBox2.UseVisualStyleBackColor = True
            '
            'CheckBox3
            '
            Me.CheckBox3.AutoSize = True
            Me.CheckBox3.Checked = True
            Me.CheckBox3.CheckState = System.Windows.Forms.CheckState.Indeterminate
            Me.CheckBox3.Location = New System.Drawing.Point(247, 3)
            Me.CheckBox3.Name = "CheckBox3"
            Me.CheckBox3.Size = New System.Drawing.Size(63, 17)
            Me.CheckBox3.TabIndex = 2
            Me.CheckBox3.Text = "Content"
            Me.CheckBox3.ThreeState = True
            Me.CheckBox3.UseVisualStyleBackColor = True
            '
            'CheckBox4
            '
            Me.CheckBox4.AutoSize = True
            Me.CheckBox4.Checked = True
            Me.CheckBox4.CheckState = System.Windows.Forms.CheckState.Indeterminate
            Me.CheckBox4.Location = New System.Drawing.Point(316, 3)
            Me.CheckBox4.Name = "CheckBox4"
            Me.CheckBox4.Size = New System.Drawing.Size(77, 17)
            Me.CheckBox4.TabIndex = 3
            Me.CheckBox4.Text = "Discussion"
            Me.CheckBox4.ThreeState = True
            Me.CheckBox4.UseVisualStyleBackColor = True
            '
            'CheckBox10
            '
            Me.CheckBox10.AutoSize = True
            Me.CheckBox10.Checked = True
            Me.CheckBox10.CheckState = System.Windows.Forms.CheckState.Indeterminate
            Me.CheckBox10.Location = New System.Drawing.Point(399, 3)
            Me.CheckBox10.Name = "CheckBox10"
            Me.CheckBox10.Size = New System.Drawing.Size(53, 17)
            Me.CheckBox10.TabIndex = 30
            Me.CheckBox10.Text = "Large"
            Me.CheckBox10.ThreeState = True
            Me.CheckBox10.UseVisualStyleBackColor = True
            '
            'CheckBox5
            '
            Me.CheckBox5.AutoSize = True
            Me.CheckBox5.Checked = True
            Me.CheckBox5.CheckState = System.Windows.Forms.CheckState.Indeterminate
            Me.CheckBox5.Location = New System.Drawing.Point(458, 3)
            Me.CheckBox5.Name = "CheckBox5"
            Me.CheckBox5.Size = New System.Drawing.Size(52, 17)
            Me.CheckBox5.TabIndex = 4
            Me.CheckBox5.Text = "Minor"
            Me.CheckBox5.ThreeState = True
            Me.CheckBox5.UseVisualStyleBackColor = True
            '
            'CheckBox6
            '
            Me.CheckBox6.AutoSize = True
            Me.CheckBox6.Checked = True
            Me.CheckBox6.CheckState = System.Windows.Forms.CheckState.Indeterminate
            Me.CheckBox6.Location = New System.Drawing.Point(516, 3)
            Me.CheckBox6.Name = "CheckBox6"
            Me.CheckBox6.Size = New System.Drawing.Size(48, 17)
            Me.CheckBox6.TabIndex = 5
            Me.CheckBox6.Text = "New"
            Me.CheckBox6.ThreeState = True
            Me.CheckBox6.UseVisualStyleBackColor = True
            '
            'CheckBox12
            '
            Me.CheckBox12.AutoSize = True
            Me.CheckBox12.Checked = True
            Me.CheckBox12.CheckState = System.Windows.Forms.CheckState.Indeterminate
            Me.CheckBox12.Location = New System.Drawing.Point(3, 26)
            Me.CheckBox12.Name = "CheckBox12"
            Me.CheckBox12.Size = New System.Drawing.Size(58, 17)
            Me.CheckBox12.TabIndex = 31
            Me.CheckBox12.Text = "Revert"
            Me.CheckBox12.ThreeState = True
            Me.CheckBox12.UseVisualStyleBackColor = True
            '
            'CheckBox13
            '
            Me.CheckBox13.AutoSize = True
            Me.CheckBox13.Checked = True
            Me.CheckBox13.CheckState = System.Windows.Forms.CheckState.Indeterminate
            Me.CheckBox13.Location = New System.Drawing.Point(67, 26)
            Me.CheckBox13.Name = "CheckBox13"
            Me.CheckBox13.Size = New System.Drawing.Size(70, 17)
            Me.CheckBox13.TabIndex = 33
            Me.CheckBox13.Text = "Reverted"
            Me.CheckBox13.ThreeState = True
            Me.CheckBox13.UseVisualStyleBackColor = True
            '
            'CheckBox7
            '
            Me.CheckBox7.AutoSize = True
            Me.CheckBox7.Checked = True
            Me.CheckBox7.CheckState = System.Windows.Forms.CheckState.Indeterminate
            Me.CheckBox7.Location = New System.Drawing.Point(143, 26)
            Me.CheckBox7.Name = "CheckBox7"
            Me.CheckBox7.Size = New System.Drawing.Size(62, 17)
            Me.CheckBox7.TabIndex = 6
            Me.CheckBox7.Text = "Section"
            Me.CheckBox7.ThreeState = True
            Me.CheckBox7.UseVisualStyleBackColor = True
            '
            'CheckBox8
            '
            Me.CheckBox8.AutoSize = True
            Me.CheckBox8.Checked = True
            Me.CheckBox8.CheckState = System.Windows.Forms.CheckState.Indeterminate
            Me.CheckBox8.Location = New System.Drawing.Point(211, 26)
            Me.CheckBox8.Name = "CheckBox8"
            Me.CheckBox8.Size = New System.Drawing.Size(69, 17)
            Me.CheckBox8.TabIndex = 7
            Me.CheckBox8.Text = "Summary"
            Me.CheckBox8.ThreeState = True
            Me.CheckBox8.UseVisualStyleBackColor = True
            '
            'CheckBox11
            '
            Me.CheckBox11.AutoSize = True
            Me.CheckBox11.Checked = True
            Me.CheckBox11.CheckState = System.Windows.Forms.CheckState.Indeterminate
            Me.CheckBox11.Location = New System.Drawing.Point(286, 26)
            Me.CheckBox11.Name = "CheckBox11"
            Me.CheckBox11.Size = New System.Drawing.Size(45, 17)
            Me.CheckBox11.TabIndex = 30
            Me.CheckBox11.Text = "Tag"
            Me.CheckBox11.ThreeState = True
            Me.CheckBox11.UseVisualStyleBackColor = True
            '
            'CustomFilter
            '
            Me.CustomFilter.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
            Me.CustomFilter.Location = New System.Drawing.Point(616, 24)
            Me.CustomFilter.Name = "CustomFilter"
            Me.CustomFilter.Size = New System.Drawing.Size(103, 23)
            Me.CustomFilter.TabIndex = 8
            Me.CustomFilter.Text = "Custom filter..."
            Me.CustomFilter.UseVisualStyleBackColor = True
            '
            'RevisionView
            '
            Me.Controls.Add(Me.FilterLayout)
            Me.Controls.Add(Me.Title)
            Me.Controls.Add(Me.Count)
            Me.Controls.Add(Me.RevisionList)
            Me.Controls.Add(Me.CustomFilter)
            Me.Name = "RevisionView"
            Me.Size = New System.Drawing.Size(722, 442)
            Me.FilterLayout.ResumeLayout(False)
            Me.FilterLayout.PerformLayout()
            Me.ResumeLayout(False)
            Me.PerformLayout()

        End Sub
        Private WithEvents Count As System.Windows.Forms.Label
        Private WithEvents Title As System.Windows.Forms.Label
        Private WithEvents RevisionList As System.Windows.Forms.EnhancedListView
        Private WithEvents IDColumn As System.Windows.Forms.ColumnHeader
        Private WithEvents UserColumn As System.Windows.Forms.ColumnHeader
        Private WithEvents PageColumn As System.Windows.Forms.ColumnHeader
        Private WithEvents StatusColumn As System.Windows.Forms.ColumnHeader
        Private WithEvents ChangeColumn As System.Windows.Forms.ColumnHeader
        Private WithEvents SummaryColumn As System.Windows.Forms.ColumnHeader
        Private WithEvents CheckBox1 As System.Windows.Forms.CheckBox
        Private WithEvents CheckBox3 As System.Windows.Forms.CheckBox
        Private WithEvents CheckBox4 As System.Windows.Forms.CheckBox
        Private WithEvents CheckBox2 As System.Windows.Forms.CheckBox
        Private WithEvents CheckBox5 As System.Windows.Forms.CheckBox
        Private WithEvents CheckBox6 As System.Windows.Forms.CheckBox
        Private WithEvents CheckBox7 As System.Windows.Forms.CheckBox
        Private WithEvents CheckBox8 As System.Windows.Forms.CheckBox
        Private WithEvents CustomFilter As System.Windows.Forms.Button
        Private WithEvents CheckBox9 As System.Windows.Forms.CheckBox
        Private WithEvents FilterLabel As System.Windows.Forms.Label
        Private WithEvents CheckBox10 As System.Windows.Forms.CheckBox
        Private WithEvents CheckBox12 As System.Windows.Forms.CheckBox
        Private WithEvents CheckBox11 As System.Windows.Forms.CheckBox
        Private WithEvents TimeColumn As System.Windows.Forms.ColumnHeader
        Private WithEvents CheckBox13 As System.Windows.Forms.CheckBox
        Private WithEvents FilterLayout As System.Windows.Forms.FlowLayoutPanel

    End Class
End Namespace