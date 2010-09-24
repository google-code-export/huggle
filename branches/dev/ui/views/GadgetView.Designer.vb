<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class GadgetView
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
        Me.GadgetName = New System.Windows.Forms.LinkLabel()
        Me.GadgetProps = New System.Windows.Forms.FlowLayoutPanel()
        Me.GadgetType = New System.Windows.Forms.Label()
        Me.GadgetDescription = New System.Windows.Forms.Label()
        Me.Count = New System.Windows.Forms.Label()
        Me.GadgetImage = New System.Windows.Forms.PictureBox()
        Me.List = New System.Windows.Forms.ListViewEx()
        Me.GadgetNameColumn = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.GadgetTypeColumn = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.GadgetDescriptionColumn = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.Title = New System.Windows.Forms.Label()
        Me.GadgetProps.SuspendLayout()
        CType(Me.GadgetImage, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'GadgetName
        '
        Me.GadgetName.AutoSize = True
        Me.GadgetName.Font = New System.Drawing.Font("Tahoma", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GadgetName.ForeColor = System.Drawing.SystemColors.HotTrack
        Me.GadgetName.LinkBehavior = System.Windows.Forms.LinkBehavior.HoverUnderline
        Me.GadgetName.LinkColor = System.Drawing.SystemColors.HotTrack
        Me.GadgetName.Location = New System.Drawing.Point(3, 5)
        Me.GadgetName.Name = "GadgetName"
        Me.GadgetName.Size = New System.Drawing.Size(47, 18)
        Me.GadgetName.TabIndex = 4
        Me.GadgetName.TabStop = True
        Me.GadgetName.Text = "Name"
        Me.GadgetName.VisitedLinkColor = System.Drawing.SystemColors.HotTrack
        '
        'GadgetProps
        '
        Me.GadgetProps.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.GadgetProps.AutoSize = True
        Me.GadgetProps.Controls.Add(Me.GadgetName)
        Me.GadgetProps.Controls.Add(Me.GadgetType)
        Me.GadgetProps.Controls.Add(Me.GadgetDescription)
        Me.GadgetProps.FlowDirection = System.Windows.Forms.FlowDirection.TopDown
        Me.GadgetProps.Location = New System.Drawing.Point(70, 306)
        Me.GadgetProps.Name = "GadgetProps"
        Me.GadgetProps.Padding = New System.Windows.Forms.Padding(0, 5, 0, 0)
        Me.GadgetProps.Size = New System.Drawing.Size(543, 78)
        Me.GadgetProps.TabIndex = 22
        Me.GadgetProps.Visible = False
        '
        'GadgetType
        '
        Me.GadgetType.AutoSize = True
        Me.GadgetType.Location = New System.Drawing.Point(3, 23)
        Me.GadgetType.Name = "GadgetType"
        Me.GadgetType.Padding = New System.Windows.Forms.Padding(0, 2, 0, 2)
        Me.GadgetType.Size = New System.Drawing.Size(31, 17)
        Me.GadgetType.TabIndex = 5
        Me.GadgetType.Text = "Type"
        '
        'GadgetDescription
        '
        Me.GadgetDescription.AutoSize = True
        Me.GadgetDescription.Location = New System.Drawing.Point(3, 40)
        Me.GadgetDescription.Name = "GadgetDescription"
        Me.GadgetDescription.Padding = New System.Windows.Forms.Padding(0, 2, 0, 2)
        Me.GadgetDescription.Size = New System.Drawing.Size(60, 17)
        Me.GadgetDescription.TabIndex = 2
        Me.GadgetDescription.Text = "Description"
        '
        'Count
        '
        Me.Count.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Count.Location = New System.Drawing.Point(550, 6)
        Me.Count.Name = "Count"
        Me.Count.Size = New System.Drawing.Size(66, 13)
        Me.Count.TabIndex = 20
        Me.Count.Text = "0 items"
        Me.Count.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'GadgetImage
        '
        Me.GadgetImage.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.GadgetImage.Image = Global.Resources.mediawiki_gadget
        Me.GadgetImage.Location = New System.Drawing.Point(3, 306)
        Me.GadgetImage.Name = "GadgetImage"
        Me.GadgetImage.Size = New System.Drawing.Size(64, 78)
        Me.GadgetImage.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage
        Me.GadgetImage.TabIndex = 21
        Me.GadgetImage.TabStop = False
        Me.GadgetImage.Visible = False
        '
        'List
        '
        Me.List.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.List.Columns.AddRange(New System.Windows.Forms.ColumnHeader() {Me.GadgetNameColumn, Me.GadgetTypeColumn, Me.GadgetDescriptionColumn})
        Me.List.FlexibleColumn = 2
        Me.List.FullRowSelect = True
        Me.List.GridLines = True
        Me.List.Location = New System.Drawing.Point(3, 25)
        Me.List.Name = "List"
        Me.List.ShowGroups = False
        Me.List.Size = New System.Drawing.Size(610, 272)
        Me.List.SortOnColumnClick = True
        Me.List.TabIndex = 19
        Me.List.UseCompatibleStateImageBehavior = False
        Me.List.View = System.Windows.Forms.View.Details
        '
        'GadgetNameColumn
        '
        Me.GadgetNameColumn.Text = "Name"
        Me.GadgetNameColumn.Width = 120
        '
        'GadgetTypeColumn
        '
        Me.GadgetTypeColumn.Text = "Type"
        Me.GadgetTypeColumn.Width = 100
        '
        'GadgetDescriptionColumn
        '
        Me.GadgetDescriptionColumn.Text = "Description"
        Me.GadgetDescriptionColumn.Width = 367
        '
        'Title
        '
        Me.Title.AutoSize = True
        Me.Title.Font = New System.Drawing.Font("Segoe UI", 14.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Title.Location = New System.Drawing.Point(-2, -3)
        Me.Title.Name = "Title"
        Me.Title.Size = New System.Drawing.Size(81, 25)
        Me.Title.TabIndex = 23
        Me.Title.Text = "Gadgets"
        '
        'GadgetView
        '
        Me.Controls.Add(Me.Title)
        Me.Controls.Add(Me.GadgetProps)
        Me.Controls.Add(Me.Count)
        Me.Controls.Add(Me.GadgetImage)
        Me.Controls.Add(Me.List)
        Me.Name = "GadgetView"
        Me.Size = New System.Drawing.Size(616, 388)
        Me.GadgetProps.ResumeLayout(False)
        Me.GadgetProps.PerformLayout()
        CType(Me.GadgetImage, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Private WithEvents GadgetName As System.Windows.Forms.LinkLabel
    Private WithEvents GadgetProps As System.Windows.Forms.FlowLayoutPanel
    Private WithEvents GadgetType As System.Windows.Forms.Label
    Private WithEvents GadgetDescription As System.Windows.Forms.Label
    Private WithEvents Count As System.Windows.Forms.Label
    Private WithEvents GadgetImage As System.Windows.Forms.PictureBox
    Private WithEvents List As System.Windows.Forms.ListViewEx
    Private WithEvents GadgetNameColumn As System.Windows.Forms.ColumnHeader
    Private WithEvents GadgetTypeColumn As System.Windows.Forms.ColumnHeader
    Private WithEvents GadgetDescriptionColumn As System.Windows.Forms.ColumnHeader
    Private WithEvents Title As System.Windows.Forms.Label

End Class
