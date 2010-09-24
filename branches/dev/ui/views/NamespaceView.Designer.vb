<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class NamespaceView
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
        Me.List = New System.Windows.Forms.ListViewEx()
        Me.SpaceNumberColumn = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.SpaceNameColumn = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.SpaceInfoColumn = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.Count = New System.Windows.Forms.Label()
        Me.Title = New System.Windows.Forms.Label()
        Me.SuspendLayout()
        '
        'List
        '
        Me.List.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.List.Columns.AddRange(New System.Windows.Forms.ColumnHeader() {Me.SpaceNumberColumn, Me.SpaceNameColumn, Me.SpaceInfoColumn})
        Me.List.FlexibleColumn = 1
        Me.List.FullRowSelect = True
        Me.List.GridLines = True
        Me.List.Location = New System.Drawing.Point(3, 25)
        Me.List.Name = "List"
        Me.List.ShowGroups = False
        Me.List.Size = New System.Drawing.Size(472, 404)
        Me.List.SortOnColumnClick = True
        Me.List.TabIndex = 20
        Me.List.UseCompatibleStateImageBehavior = False
        Me.List.View = System.Windows.Forms.View.Details
        '
        'SpaceNumberColumn
        '
        Me.SpaceNumberColumn.Text = "#"
        Me.SpaceNumberColumn.Width = 30
        '
        'SpaceNameColumn
        '
        Me.SpaceNameColumn.Text = "Name"
        Me.SpaceNameColumn.Width = 196
        '
        'SpaceInfoColumn
        '
        Me.SpaceInfoColumn.Text = "Properties"
        Me.SpaceInfoColumn.Width = 223
        '
        'Count
        '
        Me.Count.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Count.Location = New System.Drawing.Point(418, 6)
        Me.Count.Name = "Count"
        Me.Count.Size = New System.Drawing.Size(60, 13)
        Me.Count.TabIndex = 19
        Me.Count.Text = "0 items"
        Me.Count.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Title
        '
        Me.Title.AutoSize = True
        Me.Title.Font = New System.Drawing.Font("Segoe UI", 14.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Title.Location = New System.Drawing.Point(-2, -3)
        Me.Title.Name = "Title"
        Me.Title.Size = New System.Drawing.Size(118, 25)
        Me.Title.TabIndex = 21
        Me.Title.Text = "Namespaces"
        '
        'NamespaceView
        '
        Me.Controls.Add(Me.Title)
        Me.Controls.Add(Me.List)
        Me.Controls.Add(Me.Count)
        Me.Name = "NamespaceView"
        Me.Size = New System.Drawing.Size(478, 432)
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Private WithEvents List As System.Windows.Forms.ListViewEx
    Private WithEvents SpaceNumberColumn As System.Windows.Forms.ColumnHeader
    Private WithEvents SpaceNameColumn As System.Windows.Forms.ColumnHeader
    Private WithEvents SpaceInfoColumn As System.Windows.Forms.ColumnHeader
    Private WithEvents Count As System.Windows.Forms.Label
    Private WithEvents Title As System.Windows.Forms.Label

End Class
