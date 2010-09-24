<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class ChangeTagView
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
        Me.List = New System.Windows.Forms.ListViewEx()
        Me.TagNameColumn = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.TagDescriptionColumn = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.TagCountColumn = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.Title = New System.Windows.Forms.Label()
        Me.SuspendLayout()
        '
        'Count
        '
        Me.Count.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Count.Location = New System.Drawing.Point(480, 6)
        Me.Count.Name = "Count"
        Me.Count.Size = New System.Drawing.Size(40, 13)
        Me.Count.TabIndex = 17
        Me.Count.Text = "0 items"
        Me.Count.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'List
        '
        Me.List.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.List.Columns.AddRange(New System.Windows.Forms.ColumnHeader() {Me.TagNameColumn, Me.TagDescriptionColumn, Me.TagCountColumn})
        Me.List.FlexibleColumn = 1
        Me.List.FullRowSelect = True
        Me.List.GridLines = True
        Me.List.Location = New System.Drawing.Point(3, 25)
        Me.List.Name = "List"
        Me.List.ShowGroups = False
        Me.List.Size = New System.Drawing.Size(514, 392)
        Me.List.SortOnColumnClick = True
        Me.List.TabIndex = 16
        Me.List.UseCompatibleStateImageBehavior = False
        Me.List.View = System.Windows.Forms.View.Details
        '
        'TagNameColumn
        '
        Me.TagNameColumn.Text = "Name"
        Me.TagNameColumn.Width = 156
        '
        'TagDescriptionColumn
        '
        Me.TagDescriptionColumn.Text = "Description"
        Me.TagDescriptionColumn.Width = 285
        '
        'TagCountColumn
        '
        Me.TagCountColumn.Text = "Count"
        Me.TagCountColumn.Width = 50
        '
        'Title
        '
        Me.Title.AutoSize = True
        Me.Title.Font = New System.Drawing.Font("Segoe UI", 14.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Title.Location = New System.Drawing.Point(-2, -3)
        Me.Title.Name = "Title"
        Me.Title.Size = New System.Drawing.Size(51, 25)
        Me.Title.TabIndex = 23
        Me.Title.Text = "Tags"
        '
        'ChangeTagView
        '
        Me.Controls.Add(Me.Title)
        Me.Controls.Add(Me.Count)
        Me.Controls.Add(Me.List)
        Me.Name = "ChangeTagView"
        Me.Size = New System.Drawing.Size(520, 417)
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Private WithEvents Count As System.Windows.Forms.Label
    Private WithEvents List As System.Windows.Forms.ListViewEx
    Private WithEvents TagNameColumn As System.Windows.Forms.ColumnHeader
    Private WithEvents TagDescriptionColumn As System.Windows.Forms.ColumnHeader
    Private WithEvents TagCountColumn As System.Windows.Forms.ColumnHeader
    Private WithEvents Title As System.Windows.Forms.Label

End Class
