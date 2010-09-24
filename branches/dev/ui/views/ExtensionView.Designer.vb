<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class ExtensionView
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
        Dim NameColumn As System.Windows.Forms.ColumnHeader
        Dim TypeColumn As System.Windows.Forms.ColumnHeader
        Dim VersionColumn As System.Windows.Forms.ColumnHeader
        Me.Count = New System.Windows.Forms.Label()
        Me.Properties = New System.Windows.Forms.FlowLayoutPanel()
        Me.ExtensionName = New System.Windows.Forms.LinkLabel()
        Me.Description = New System.Windows.Forms.Label()
        Me.Version = New System.Windows.Forms.Label()
        Me.Author = New System.Windows.Forms.Label()
        Me.List = New System.Windows.Forms.ListViewEx()
        Me.Image = New System.Windows.Forms.PictureBox()
        Me.Title = New System.Windows.Forms.Label()
        NameColumn = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        TypeColumn = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        VersionColumn = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.Properties.SuspendLayout()
        CType(Me.Image, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'NameColumn
        '
        NameColumn.Text = "Name"
        NameColumn.Width = 378
        '
        'TypeColumn
        '
        TypeColumn.Text = "Type"
        TypeColumn.Width = 100
        '
        'VersionColumn
        '
        VersionColumn.Text = "Version"
        '
        'Count
        '
        Me.Count.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Count.Location = New System.Drawing.Point(500, 6)
        Me.Count.Name = "Count"
        Me.Count.Size = New System.Drawing.Size(67, 13)
        Me.Count.TabIndex = 17
        Me.Count.Text = "0 items"
        Me.Count.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Properties
        '
        Me.Properties.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Properties.AutoSize = True
        Me.Properties.Controls.Add(Me.ExtensionName)
        Me.Properties.Controls.Add(Me.Description)
        Me.Properties.Controls.Add(Me.Version)
        Me.Properties.Controls.Add(Me.Author)
        Me.Properties.FlowDirection = System.Windows.Forms.FlowDirection.TopDown
        Me.Properties.Location = New System.Drawing.Point(68, 339)
        Me.Properties.Name = "Properties"
        Me.Properties.Padding = New System.Windows.Forms.Padding(0, 5, 0, 0)
        Me.Properties.Size = New System.Drawing.Size(496, 85)
        Me.Properties.TabIndex = 16
        Me.Properties.Visible = False
        '
        'ExtensionName
        '
        Me.ExtensionName.AutoSize = True
        Me.ExtensionName.Font = New System.Drawing.Font("Tahoma", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ExtensionName.ForeColor = System.Drawing.SystemColors.HotTrack
        Me.ExtensionName.LinkBehavior = System.Windows.Forms.LinkBehavior.HoverUnderline
        Me.ExtensionName.LinkColor = System.Drawing.SystemColors.HotTrack
        Me.ExtensionName.Location = New System.Drawing.Point(3, 5)
        Me.ExtensionName.Name = "ExtensionName"
        Me.ExtensionName.Size = New System.Drawing.Size(47, 18)
        Me.ExtensionName.TabIndex = 4
        Me.ExtensionName.TabStop = True
        Me.ExtensionName.Text = "Name"
        Me.ExtensionName.VisitedLinkColor = System.Drawing.SystemColors.HotTrack
        '
        'Description
        '
        Me.Description.AutoSize = True
        Me.Description.Location = New System.Drawing.Point(3, 23)
        Me.Description.Name = "Description"
        Me.Description.Padding = New System.Windows.Forms.Padding(0, 2, 0, 2)
        Me.Description.Size = New System.Drawing.Size(60, 17)
        Me.Description.TabIndex = 2
        Me.Description.Text = "Description"
        '
        'Version
        '
        Me.Version.AutoSize = True
        Me.Version.Location = New System.Drawing.Point(3, 40)
        Me.Version.Name = "Version"
        Me.Version.Padding = New System.Windows.Forms.Padding(0, 2, 0, 2)
        Me.Version.Size = New System.Drawing.Size(63, 17)
        Me.Version.TabIndex = 5
        Me.Version.Text = "Version: 1.0"
        '
        'Author
        '
        Me.Author.AutoSize = True
        Me.Author.Location = New System.Drawing.Point(3, 57)
        Me.Author.Name = "Author"
        Me.Author.Padding = New System.Windows.Forms.Padding(0, 2, 0, 2)
        Me.Author.Size = New System.Drawing.Size(41, 17)
        Me.Author.TabIndex = 5
        Me.Author.Text = "Author:"
        '
        'List
        '
        Me.List.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.List.Columns.AddRange(New System.Windows.Forms.ColumnHeader() {NameColumn, TypeColumn, VersionColumn})
        Me.List.FlexibleColumn = 0
        Me.List.FullRowSelect = True
        Me.List.GridLines = True
        Me.List.Location = New System.Drawing.Point(3, 25)
        Me.List.Name = "List"
        Me.List.ShowGroups = False
        Me.List.Size = New System.Drawing.Size(561, 308)
        Me.List.SortOnColumnClick = True
        Me.List.TabIndex = 14
        Me.List.UseCompatibleStateImageBehavior = False
        Me.List.View = System.Windows.Forms.View.Details
        '
        'Image
        '
        Me.Image.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.Image.Image = Global.Resources.mediawiki_extension
        Me.Image.Location = New System.Drawing.Point(3, 339)
        Me.Image.Name = "Image"
        Me.Image.Size = New System.Drawing.Size(64, 85)
        Me.Image.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage
        Me.Image.TabIndex = 15
        Me.Image.TabStop = False
        Me.Image.Visible = False
        '
        'Title
        '
        Me.Title.AutoSize = True
        Me.Title.Font = New System.Drawing.Font("Segoe UI", 14.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Title.Location = New System.Drawing.Point(-2, -3)
        Me.Title.Name = "Title"
        Me.Title.Size = New System.Drawing.Size(101, 25)
        Me.Title.TabIndex = 22
        Me.Title.Text = "Extensions"
        '
        'ExtensionView
        '
        Me.Controls.Add(Me.Title)
        Me.Controls.Add(Me.Count)
        Me.Controls.Add(Me.Properties)
        Me.Controls.Add(Me.List)
        Me.Controls.Add(Me.Image)
        Me.Name = "ExtensionView"
        Me.Size = New System.Drawing.Size(567, 427)
        Me.Properties.ResumeLayout(False)
        Me.Properties.PerformLayout()
        CType(Me.Image, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Private WithEvents Count As System.Windows.Forms.Label
    Private WithEvents Properties As System.Windows.Forms.FlowLayoutPanel
    Private WithEvents ExtensionName As System.Windows.Forms.LinkLabel
    Private WithEvents Description As System.Windows.Forms.Label
    Private WithEvents Version As System.Windows.Forms.Label
    Private WithEvents Author As System.Windows.Forms.Label
    Private WithEvents List As System.Windows.Forms.ListViewEx
    Private WithEvents Image As System.Windows.Forms.PictureBox
    Private WithEvents Title As System.Windows.Forms.Label

End Class
