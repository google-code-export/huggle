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
            Dim GroupColumn As System.Windows.Forms.ColumnHeader
            Dim NameColumn As System.Windows.Forms.ColumnHeader
            Me.Count = New System.Windows.Forms.Label()
            Me.List = New System.Windows.Forms.EnhancedListView()
            Me.Title = New System.Windows.Forms.Label()
            Me.Wait = New Huggle.UI.WaitControl()
            GroupColumn = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
            NameColumn = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
            Me.SuspendLayout()
            '
            'GroupColumn
            '
            GroupColumn.Text = "Group"
            GroupColumn.Width = 113
            '
            'NameColumn
            '
            NameColumn.Text = "Name"
            NameColumn.Width = 340
            '
            'Count
            '
            Me.Count.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
            Me.Count.Location = New System.Drawing.Point(479, 6)
            Me.Count.Name = "Count"
            Me.Count.Size = New System.Drawing.Size(60, 13)
            Me.Count.TabIndex = 16
            Me.Count.Text = "0 items"
            Me.Count.TextAlign = System.Drawing.ContentAlignment.TopRight
            Me.Count.Visible = False
            '
            'List
            '
            Me.List.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                        Or System.Windows.Forms.AnchorStyles.Left) _
                        Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
            Me.List.Columns.AddRange(New System.Windows.Forms.ColumnHeader() {GroupColumn, NameColumn})
            Me.List.FlexibleColumn = 1
            Me.List.FullRowSelect = True
            Me.List.GridLines = True
            Me.List.Location = New System.Drawing.Point(3, 25)
            Me.List.Name = "List"
            Me.List.ShowGroups = False
            Me.List.Size = New System.Drawing.Size(533, 303)
            Me.List.SortOnColumnClick = True
            Me.List.TabIndex = 15
            Me.List.UseCompatibleStateImageBehavior = False
            Me.List.View = System.Windows.Forms.View.Details
            Me.List.Visible = False
            '
            'Title
            '
            Me.Title.AutoSize = True
            Me.Title.Font = New System.Drawing.Font("Segoe UI", 14.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.Title.Location = New System.Drawing.Point(-2, -3)
            Me.Title.Name = "Title"
            Me.Title.Size = New System.Drawing.Size(172, 25)
            Me.Title.TabIndex = 23
            Me.Title.Text = "Global user groups"
            Me.Title.Visible = False
            '
            'Wait
            '
            Me.Wait.Location = New System.Drawing.Point(179, 134)
            Me.Wait.Name = "Wait"
            Me.Wait.Size = New System.Drawing.Size(16, 16)
            Me.Wait.TabIndex = 24
            Me.Wait.TabStop = False
            '
            'GlobalGroupView
            '
            Me.Controls.Add(Me.Wait)
            Me.Controls.Add(Me.Title)
            Me.Controls.Add(Me.Count)
            Me.Controls.Add(Me.List)
            Me.Name = "GlobalGroupView"
            Me.Size = New System.Drawing.Size(539, 331)
            Me.ResumeLayout(False)
            Me.PerformLayout()

        End Sub
        Private WithEvents Count As System.Windows.Forms.Label
        Private WithEvents List As System.Windows.Forms.EnhancedListView
        Private WithEvents Title As System.Windows.Forms.Label
        Friend WithEvents Wait As Huggle.UI.WaitControl

    End Class
End Namespace