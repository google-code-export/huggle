Namespace Huggle.UI
    <Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
    Partial Class UserRightsView
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
            Dim RightColumn As System.Windows.Forms.ColumnHeader
            Dim GrantedColumn As System.Windows.Forms.ColumnHeader
            Me.Count = New System.Windows.Forms.Label()
            Me.List = New System.Windows.Forms.EnhancedListView()
            Me.Title = New System.Windows.Forms.Label()
            RightColumn = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
            GrantedColumn = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
            Me.SuspendLayout()
            '
            'RightColumn
            '
            RightColumn.Text = "Right"
            RightColumn.Width = 169
            '
            'GrantedColumn
            '
            GrantedColumn.Text = "Granted to"
            GrantedColumn.Width = 353
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
            '
            'List
            '
            Me.List.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                        Or System.Windows.Forms.AnchorStyles.Left) _
                        Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
            Me.List.Columns.AddRange(New System.Windows.Forms.ColumnHeader() {RightColumn, GrantedColumn})
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
            '
            'Title
            '
            Me.Title.AutoSize = True
            Me.Title.Font = New System.Drawing.Font("Segoe UI", 14.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.Title.Location = New System.Drawing.Point(-2, -3)
            Me.Title.Name = "Title"
            Me.Title.Size = New System.Drawing.Size(103, 25)
            Me.Title.TabIndex = 23
            Me.Title.Text = "User rights"
            '
            'UserRightsView
            '
            Me.Controls.Add(Me.Title)
            Me.Controls.Add(Me.Count)
            Me.Controls.Add(Me.List)
            Me.Name = "UserRightsView"
            Me.Size = New System.Drawing.Size(539, 331)
            Me.ResumeLayout(False)
            Me.PerformLayout()

        End Sub
        Private WithEvents Count As System.Windows.Forms.Label
        Private WithEvents List As System.Windows.Forms.EnhancedListView
        Private WithEvents Title As System.Windows.Forms.Label

    End Class
End Namespace