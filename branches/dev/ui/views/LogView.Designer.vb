Namespace Huggle.UI
    <Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
    Partial Class LogView
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
            Me.LogList = New System.Windows.Forms.EnhancedListView()
            Me.CustomFilter = New System.Windows.Forms.Button()
            Me.FilterTypeSelector = New System.Windows.Forms.EnhancedComboBox()
            Me.FilterSubtypeSelector = New System.Windows.Forms.EnhancedComboBox()
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
            Me.Title.Size = New System.Drawing.Size(43, 25)
            Me.Title.TabIndex = 23
            Me.Title.Text = "Log"
            '
            'LogList
            '
            Me.LogList.AllowColumnReorder = True
            Me.LogList.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                        Or System.Windows.Forms.AnchorStyles.Left) _
                        Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
            Me.LogList.FlexibleColumn = 5
            Me.LogList.FullRowSelect = True
            Me.LogList.GridLines = True
            Me.LogList.HideSelection = False
            Me.LogList.Location = New System.Drawing.Point(3, 53)
            Me.LogList.Name = "LogList"
            Me.LogList.SelectedValue = Nothing
            Me.LogList.ShowGroups = False
            Me.LogList.Size = New System.Drawing.Size(716, 386)
            Me.LogList.SortOnColumnClick = True
            Me.LogList.TabIndex = 15
            Me.LogList.UseCompatibleStateImageBehavior = False
            Me.LogList.View = System.Windows.Forms.View.Details
            Me.LogList.VirtualMode = True
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
            'FilterTypeSelector
            '
            Me.FilterTypeSelector.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
            Me.FilterTypeSelector.FormattingEnabled = True
            Me.FilterTypeSelector.Location = New System.Drawing.Point(3, 26)
            Me.FilterTypeSelector.Name = "FilterTypeSelector"
            Me.FilterTypeSelector.Size = New System.Drawing.Size(147, 21)
            Me.FilterTypeSelector.TabIndex = 24
            '
            'FilterSubtypeSelector
            '
            Me.FilterSubtypeSelector.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
            Me.FilterSubtypeSelector.FormattingEnabled = True
            Me.FilterSubtypeSelector.Location = New System.Drawing.Point(156, 26)
            Me.FilterSubtypeSelector.Name = "FilterSubtypeSelector"
            Me.FilterSubtypeSelector.Size = New System.Drawing.Size(147, 21)
            Me.FilterSubtypeSelector.TabIndex = 25
            '
            'LogView
            '
            Me.Controls.Add(Me.FilterSubtypeSelector)
            Me.Controls.Add(Me.FilterTypeSelector)
            Me.Controls.Add(Me.Title)
            Me.Controls.Add(Me.Count)
            Me.Controls.Add(Me.LogList)
            Me.Controls.Add(Me.CustomFilter)
            Me.Name = "LogView"
            Me.Size = New System.Drawing.Size(722, 442)
            Me.ResumeLayout(False)
            Me.PerformLayout()

        End Sub
        Private WithEvents Count As System.Windows.Forms.Label
        Private WithEvents Title As System.Windows.Forms.Label
        Private WithEvents LogList As System.Windows.Forms.EnhancedListView
        Private WithEvents CustomFilter As System.Windows.Forms.Button
        Friend WithEvents FilterTypeSelector As System.Windows.Forms.EnhancedComboBox
        Friend WithEvents FilterSubtypeSelector As System.Windows.Forms.EnhancedComboBox

    End Class
End Namespace