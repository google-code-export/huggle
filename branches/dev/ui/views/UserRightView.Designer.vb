Namespace Huggle.UI
    <Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
    Partial Class UserRightView
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
            Me.RightColumn = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
            Me.GrantedColumn = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
            Me.Count = New System.Windows.Forms.Label()
            Me.RightsList = New System.Windows.Forms.EnhancedListView()
            Me.DescriptionColumn = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
            Me.Title = New System.Windows.Forms.Label()
            Me.SuspendLayout()
            '
            'RightColumn
            '
            Me.RightColumn.Text = "Right"
            Me.RightColumn.Width = 169
            '
            'GrantedColumn
            '
            Me.GrantedColumn.Text = "Granted to"
            Me.GrantedColumn.Width = 157
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
            'RightsList
            '
            Me.RightsList.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                        Or System.Windows.Forms.AnchorStyles.Left) _
                        Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
            Me.RightsList.Columns.AddRange(New System.Windows.Forms.ColumnHeader() {Me.RightColumn, Me.DescriptionColumn, Me.GrantedColumn})
            Me.RightsList.FlexibleColumn = 1
            Me.RightsList.FullRowSelect = True
            Me.RightsList.GridLines = True
            Me.RightsList.Location = New System.Drawing.Point(3, 25)
            Me.RightsList.Name = "RightsList"
            Me.RightsList.ShowGroups = False
            Me.RightsList.Size = New System.Drawing.Size(533, 303)
            Me.RightsList.SortOnColumnClick = True
            Me.RightsList.TabIndex = 15
            Me.RightsList.UseCompatibleStateImageBehavior = False
            Me.RightsList.View = System.Windows.Forms.View.Details
            '
            'DescriptionColumn
            '
            Me.DescriptionColumn.Text = "Description"
            Me.DescriptionColumn.Width = 184
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
            Me.Controls.Add(Me.RightsList)
            Me.Name = "UserRightsView"
            Me.Size = New System.Drawing.Size(539, 331)
            Me.ResumeLayout(False)
            Me.PerformLayout()

        End Sub
        Private WithEvents Count As System.Windows.Forms.Label
        Private WithEvents RightsList As System.Windows.Forms.EnhancedListView
        Private WithEvents Title As System.Windows.Forms.Label
        Private WithEvents RightColumn As System.Windows.Forms.ColumnHeader
        Private WithEvents GrantedColumn As System.Windows.Forms.ColumnHeader
        Private WithEvents DescriptionColumn As System.Windows.Forms.ColumnHeader

    End Class
End Namespace