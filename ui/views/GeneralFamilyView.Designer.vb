Namespace Huggle.UI
    <Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
    Partial Class GeneralFamilyView
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
            Me.FamilyLogo = New System.Windows.Forms.PictureBox()
            Me.GlobalUserProps = New System.Windows.Forms.FlowLayoutPanel()
            Me.FamilyName = New System.Windows.Forms.LinkLabel()
            Me.CentralWiki = New System.Windows.Forms.Label()
            Me.Extensions = New System.Windows.Forms.Label()
            Me.WikiList = New System.Windows.Forms.EnhancedListView()
            Me.WikiColumn = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
            Me.Title = New System.Windows.Forms.Label()
            Me.Count = New System.Windows.Forms.Label()
            CType(Me.FamilyLogo, System.ComponentModel.ISupportInitialize).BeginInit()
            Me.GlobalUserProps.SuspendLayout()
            Me.SuspendLayout()
            '
            'FamilyLogo
            '
            Me.FamilyLogo.Image = Huggle.Resources.family_icon
            Me.FamilyLogo.Location = New System.Drawing.Point(4, 3)
            Me.FamilyLogo.Name = "FamilyLogo"
            Me.FamilyLogo.Size = New System.Drawing.Size(128, 128)
            Me.FamilyLogo.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage
            Me.FamilyLogo.TabIndex = 8
            Me.FamilyLogo.TabStop = False
            '
            'GlobalUserProps
            '
            Me.GlobalUserProps.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                        Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
            Me.GlobalUserProps.Controls.Add(Me.FamilyName)
            Me.GlobalUserProps.Controls.Add(Me.CentralWiki)
            Me.GlobalUserProps.Controls.Add(Me.Extensions)
            Me.GlobalUserProps.FlowDirection = System.Windows.Forms.FlowDirection.TopDown
            Me.GlobalUserProps.Location = New System.Drawing.Point(140, 3)
            Me.GlobalUserProps.Name = "GlobalUserProps"
            Me.GlobalUserProps.Size = New System.Drawing.Size(306, 128)
            Me.GlobalUserProps.TabIndex = 9
            '
            'FamilyName
            '
            Me.FamilyName.AutoSize = True
            Me.FamilyName.Font = New System.Drawing.Font("Tahoma", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.FamilyName.LinkBehavior = System.Windows.Forms.LinkBehavior.HoverUnderline
            Me.FamilyName.LinkColor = System.Drawing.SystemColors.HotTrack
            Me.FamilyName.Location = New System.Drawing.Point(3, 0)
            Me.FamilyName.Name = "FamilyName"
            Me.FamilyName.Padding = New System.Windows.Forms.Padding(0, 1, 0, 1)
            Me.FamilyName.Size = New System.Drawing.Size(72, 20)
            Me.FamilyName.TabIndex = 0
            Me.FamilyName.TabStop = True
            Me.FamilyName.Text = "Wikimedia"
            '
            'CentralWiki
            '
            Me.CentralWiki.AutoSize = True
            Me.CentralWiki.Location = New System.Drawing.Point(3, 20)
            Me.CentralWiki.Name = "CentralWiki"
            Me.CentralWiki.Padding = New System.Windows.Forms.Padding(0, 1, 0, 1)
            Me.CentralWiki.Size = New System.Drawing.Size(64, 15)
            Me.CentralWiki.TabIndex = 4
            Me.CentralWiki.Text = "Central wiki:"
            '
            'Extensions
            '
            Me.Extensions.AutoSize = True
            Me.Extensions.Location = New System.Drawing.Point(3, 35)
            Me.Extensions.Name = "Extensions"
            Me.Extensions.Padding = New System.Windows.Forms.Padding(0, 1, 0, 1)
            Me.Extensions.Size = New System.Drawing.Size(124, 15)
            Me.Extensions.TabIndex = 2
            Me.Extensions.Text = "Global extensions in use:"
            '
            'WikiList
            '
            Me.WikiList.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                        Or System.Windows.Forms.AnchorStyles.Left) _
                        Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
            Me.WikiList.Columns.AddRange(New System.Windows.Forms.ColumnHeader() {Me.WikiColumn})
            Me.WikiList.FlexibleColumn = 0
            Me.WikiList.FullRowSelect = True
            Me.WikiList.GridLines = True
            Me.WikiList.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.None
            Me.WikiList.Location = New System.Drawing.Point(4, 162)
            Me.WikiList.Name = "WikiList"
            Me.WikiList.ShowGroups = False
            Me.WikiList.Size = New System.Drawing.Size(440, 219)
            Me.WikiList.SortOnColumnClick = True
            Me.WikiList.TabIndex = 10
            Me.WikiList.UseCompatibleStateImageBehavior = False
            Me.WikiList.View = System.Windows.Forms.View.Details
            '
            'WikiColumn
            '
            Me.WikiColumn.Text = "Name"
            Me.WikiColumn.Width = 417
            '
            'Title
            '
            Me.Title.AutoSize = True
            Me.Title.Font = New System.Drawing.Font("Segoe UI", 14.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.Title.Location = New System.Drawing.Point(3, 134)
            Me.Title.Name = "Title"
            Me.Title.Size = New System.Drawing.Size(57, 25)
            Me.Title.TabIndex = 16
            Me.Title.Text = "Wikis"
            '
            'Count
            '
            Me.Count.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
            Me.Count.Location = New System.Drawing.Point(378, 146)
            Me.Count.Name = "Count"
            Me.Count.Size = New System.Drawing.Size(66, 13)
            Me.Count.TabIndex = 17
            Me.Count.Text = "0 items"
            Me.Count.TextAlign = System.Drawing.ContentAlignment.TopRight
            '
            'GeneralFamilyView
            '
            Me.Controls.Add(Me.Count)
            Me.Controls.Add(Me.Title)
            Me.Controls.Add(Me.WikiList)
            Me.Controls.Add(Me.GlobalUserProps)
            Me.Controls.Add(Me.FamilyLogo)
            Me.Name = "GeneralFamilyView"
            Me.Size = New System.Drawing.Size(449, 384)
            CType(Me.FamilyLogo, System.ComponentModel.ISupportInitialize).EndInit()
            Me.GlobalUserProps.ResumeLayout(False)
            Me.GlobalUserProps.PerformLayout()
            Me.ResumeLayout(False)
            Me.PerformLayout()

        End Sub
        Private WithEvents FamilyLogo As System.Windows.Forms.PictureBox
        Private WithEvents GlobalUserProps As System.Windows.Forms.FlowLayoutPanel
        Private WithEvents FamilyName As System.Windows.Forms.LinkLabel
        Private WithEvents CentralWiki As System.Windows.Forms.Label
        Private WithEvents Extensions As System.Windows.Forms.Label
        Private WithEvents WikiList As System.Windows.Forms.EnhancedListView
        Private WithEvents WikiColumn As System.Windows.Forms.ColumnHeader
        Private WithEvents Title As System.Windows.Forms.Label
        Private WithEvents Count As System.Windows.Forms.Label

    End Class
End Namespace