<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FamilyPropertiesForm
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
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
        Me.WikiColumn = New System.Windows.Forms.ColumnHeader
        Me.GlobalUserProps = New System.Windows.Forms.FlowLayoutPanel
        Me.FamilyName = New System.Windows.Forms.LinkLabel
        Me.CentralWiki = New System.Windows.Forms.Label
        Me.WikiCount = New System.Windows.Forms.Label
        Me.Extensions = New System.Windows.Forms.Label
        Me.FamilyLogo = New System.Windows.Forms.PictureBox
        Me.WikiList = New System.Windows.Forms.ListViewEx
        Me.GlobalUserProps.SuspendLayout()
        CType(Me.FamilyLogo, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'WikiColumn
        '
        Me.WikiColumn.Text = "Name"
        Me.WikiColumn.Width = 131
        '
        'GlobalUserProps
        '
        Me.GlobalUserProps.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.GlobalUserProps.Controls.Add(Me.FamilyName)
        Me.GlobalUserProps.Controls.Add(Me.CentralWiki)
        Me.GlobalUserProps.Controls.Add(Me.WikiCount)
        Me.GlobalUserProps.Controls.Add(Me.Extensions)
        Me.GlobalUserProps.FlowDirection = System.Windows.Forms.FlowDirection.TopDown
        Me.GlobalUserProps.Location = New System.Drawing.Point(146, 12)
        Me.GlobalUserProps.Name = "GlobalUserProps"
        Me.GlobalUserProps.Size = New System.Drawing.Size(306, 128)
        Me.GlobalUserProps.TabIndex = 3
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
        Me.FamilyName.Size = New System.Drawing.Size(75, 20)
        Me.FamilyName.TabIndex = 0
        Me.FamilyName.TabStop = True
        Me.FamilyName.Text = "Username"
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
        'WikiCount
        '
        Me.WikiCount.AutoSize = True
        Me.WikiCount.Location = New System.Drawing.Point(3, 35)
        Me.WikiCount.Name = "WikiCount"
        Me.WikiCount.Padding = New System.Windows.Forms.Padding(0, 1, 0, 1)
        Me.WikiCount.Size = New System.Drawing.Size(45, 15)
        Me.WikiCount.TabIndex = 1
        Me.WikiCount.Text = "Wikis: 0"
        '
        'Extensions
        '
        Me.Extensions.AutoSize = True
        Me.Extensions.Location = New System.Drawing.Point(3, 50)
        Me.Extensions.Name = "Extensions"
        Me.Extensions.Padding = New System.Windows.Forms.Padding(0, 1, 0, 1)
        Me.Extensions.Size = New System.Drawing.Size(124, 15)
        Me.Extensions.TabIndex = 2
        Me.Extensions.Text = "Global extensions in use:"
        '
        'FamilyLogo
        '
        Me.FamilyLogo.Image = Global.Resources.family_icon
        Me.FamilyLogo.Location = New System.Drawing.Point(12, 12)
        Me.FamilyLogo.Name = "FamilyLogo"
        Me.FamilyLogo.Size = New System.Drawing.Size(128, 128)
        Me.FamilyLogo.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage
        Me.FamilyLogo.TabIndex = 1
        Me.FamilyLogo.TabStop = False
        '
        'WikiList
        '
        Me.WikiList.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.WikiList.Columns.AddRange(New System.Windows.Forms.ColumnHeader() {Me.WikiColumn})
        Me.WikiList.FullRowSelect = True
        Me.WikiList.GridLines = True
        Me.WikiList.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.None
        Me.WikiList.Location = New System.Drawing.Point(12, 146)
        Me.WikiList.Name = "WikiList"
        Me.WikiList.ShowGroups = False
        Me.WikiList.Size = New System.Drawing.Size(440, 204)
        Me.WikiList.SortOnColumnClick = True
        Me.WikiList.TabIndex = 1
        Me.WikiList.UseCompatibleStateImageBehavior = False
        Me.WikiList.View = System.Windows.Forms.View.Details
        '
        'FamilyPropertiesForm
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(464, 362)
        Me.Controls.Add(Me.GlobalUserProps)
        Me.Controls.Add(Me.WikiList)
        Me.Controls.Add(Me.FamilyLogo)
        Me.Name = "FamilyPropertiesForm"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Family properties"
        Me.GlobalUserProps.ResumeLayout(False)
        Me.GlobalUserProps.PerformLayout()
        CType(Me.FamilyLogo, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Private WithEvents FamilyName As System.Windows.Forms.LinkLabel
    Private WithEvents GlobalUserProps As System.Windows.Forms.FlowLayoutPanel
    Private WithEvents FamilyLogo As System.Windows.Forms.PictureBox
    Private WithEvents WikiCount As System.Windows.Forms.Label
    Private WithEvents Extensions As System.Windows.Forms.Label
    Private WithEvents CentralWiki As System.Windows.Forms.Label
    Private WithEvents WikiList As System.Windows.Forms.ListViewEx
    Private WithEvents WikiColumn As System.Windows.Forms.ColumnHeader
End Class
