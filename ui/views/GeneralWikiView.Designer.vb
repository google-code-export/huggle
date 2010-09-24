<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class GeneralWikiView
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
        Me.WikiName = New System.Windows.Forms.LinkLabel()
        Me.WikiLogo = New System.Windows.Forms.PictureBox()
        Me.StatisticsList = New System.Windows.Forms.ListViewEx()
        Me.PropertyColumn = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.ValueColumn = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.AccountProps = New System.Windows.Forms.FlowLayoutPanel()
        Me.Family = New System.Windows.Forms.Label()
        Me.MainPage = New System.Windows.Forms.Label()
        Me.ContentLanguage = New System.Windows.Forms.Label()
        Me.ContentLicense = New System.Windows.Forms.Label()
        Me.SecureServer = New System.Windows.Forms.Label()
        Me.Engine = New System.Windows.Forms.Label()
        Me.Platform = New System.Windows.Forms.Label()
        Me.Database = New System.Windows.Forms.Label()
        Me.StatisticsLabel = New System.Windows.Forms.Label()
        CType(Me.WikiLogo, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.AccountProps.SuspendLayout()
        Me.SuspendLayout()
        '
        'WikiName
        '
        Me.WikiName.AutoSize = True
        Me.WikiName.Font = New System.Drawing.Font("Tahoma", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.WikiName.LinkBehavior = System.Windows.Forms.LinkBehavior.HoverUnderline
        Me.WikiName.LinkColor = System.Drawing.SystemColors.HotTrack
        Me.WikiName.Location = New System.Drawing.Point(3, 0)
        Me.WikiName.Name = "WikiName"
        Me.WikiName.Padding = New System.Windows.Forms.Padding(0, 3, 0, 0)
        Me.WikiName.Size = New System.Drawing.Size(33, 21)
        Me.WikiName.TabIndex = 5
        Me.WikiName.TabStop = True
        Me.WikiName.Text = "Wiki"
        Me.WikiName.VisitedLinkColor = System.Drawing.SystemColors.HotTrack
        '
        'WikiLogo
        '
        Me.WikiLogo.Image = Global.Resources.mediawiki_wiki
        Me.WikiLogo.Location = New System.Drawing.Point(3, 3)
        Me.WikiLogo.Name = "WikiLogo"
        Me.WikiLogo.Size = New System.Drawing.Size(128, 128)
        Me.WikiLogo.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage
        Me.WikiLogo.TabIndex = 4
        Me.WikiLogo.TabStop = False
        '
        'StatisticsList
        '
        Me.StatisticsList.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.StatisticsList.Columns.AddRange(New System.Windows.Forms.ColumnHeader() {Me.PropertyColumn, Me.ValueColumn})
        Me.StatisticsList.FlexibleColumn = 1
        Me.StatisticsList.FullRowSelect = True
        Me.StatisticsList.GridLines = True
        Me.StatisticsList.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.None
        Me.StatisticsList.Location = New System.Drawing.Point(3, 227)
        Me.StatisticsList.Name = "StatisticsList"
        Me.StatisticsList.ShowGroups = False
        Me.StatisticsList.Size = New System.Drawing.Size(443, 154)
        Me.StatisticsList.SortOnColumnClick = True
        Me.StatisticsList.TabIndex = 3
        Me.StatisticsList.UseCompatibleStateImageBehavior = False
        Me.StatisticsList.View = System.Windows.Forms.View.Details
        '
        'PropertyColumn
        '
        Me.PropertyColumn.Text = "Property"
        Me.PropertyColumn.Width = 210
        '
        'ValueColumn
        '
        Me.ValueColumn.Text = "Value"
        Me.ValueColumn.Width = 210
        '
        'AccountProps
        '
        Me.AccountProps.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.AccountProps.Controls.Add(Me.WikiName)
        Me.AccountProps.Controls.Add(Me.Family)
        Me.AccountProps.Controls.Add(Me.MainPage)
        Me.AccountProps.Controls.Add(Me.ContentLanguage)
        Me.AccountProps.Controls.Add(Me.ContentLicense)
        Me.AccountProps.Controls.Add(Me.SecureServer)
        Me.AccountProps.Controls.Add(Me.Engine)
        Me.AccountProps.Controls.Add(Me.Platform)
        Me.AccountProps.Controls.Add(Me.Database)
        Me.AccountProps.FlowDirection = System.Windows.Forms.FlowDirection.TopDown
        Me.AccountProps.Location = New System.Drawing.Point(135, 3)
        Me.AccountProps.Name = "AccountProps"
        Me.AccountProps.Size = New System.Drawing.Size(311, 184)
        Me.AccountProps.TabIndex = 6
        '
        'Family
        '
        Me.Family.AutoSize = True
        Me.Family.Location = New System.Drawing.Point(3, 21)
        Me.Family.Name = "Family"
        Me.Family.Padding = New System.Windows.Forms.Padding(0, 4, 0, 0)
        Me.Family.Size = New System.Drawing.Size(39, 17)
        Me.Family.TabIndex = 7
        Me.Family.Text = "Family:"
        '
        'MainPage
        '
        Me.MainPage.AutoSize = True
        Me.MainPage.Location = New System.Drawing.Point(3, 38)
        Me.MainPage.Name = "MainPage"
        Me.MainPage.Padding = New System.Windows.Forms.Padding(0, 4, 0, 0)
        Me.MainPage.Size = New System.Drawing.Size(60, 17)
        Me.MainPage.TabIndex = 13
        Me.MainPage.Text = "Main page:"
        '
        'ContentLanguage
        '
        Me.ContentLanguage.AutoSize = True
        Me.ContentLanguage.Location = New System.Drawing.Point(3, 55)
        Me.ContentLanguage.Name = "ContentLanguage"
        Me.ContentLanguage.Padding = New System.Windows.Forms.Padding(0, 4, 0, 0)
        Me.ContentLanguage.Size = New System.Drawing.Size(94, 17)
        Me.ContentLanguage.TabIndex = 6
        Me.ContentLanguage.Text = "Content language:"
        '
        'ContentLicense
        '
        Me.ContentLicense.AutoSize = True
        Me.ContentLicense.Location = New System.Drawing.Point(3, 72)
        Me.ContentLicense.Name = "ContentLicense"
        Me.ContentLicense.Padding = New System.Windows.Forms.Padding(0, 4, 0, 0)
        Me.ContentLicense.Size = New System.Drawing.Size(83, 17)
        Me.ContentLicense.TabIndex = 9
        Me.ContentLicense.Text = "Content license:"
        '
        'SecureServer
        '
        Me.SecureServer.AutoSize = True
        Me.SecureServer.Location = New System.Drawing.Point(3, 89)
        Me.SecureServer.Name = "SecureServer"
        Me.SecureServer.Padding = New System.Windows.Forms.Padding(0, 4, 0, 0)
        Me.SecureServer.Size = New System.Drawing.Size(76, 17)
        Me.SecureServer.TabIndex = 8
        Me.SecureServer.Text = "Secure server:"
        '
        'Engine
        '
        Me.Engine.AutoSize = True
        Me.Engine.Location = New System.Drawing.Point(3, 106)
        Me.Engine.Name = "Engine"
        Me.Engine.Padding = New System.Windows.Forms.Padding(0, 4, 0, 0)
        Me.Engine.Size = New System.Drawing.Size(43, 17)
        Me.Engine.TabIndex = 10
        Me.Engine.Text = "Engine:"
        '
        'Platform
        '
        Me.Platform.AutoSize = True
        Me.Platform.Location = New System.Drawing.Point(3, 123)
        Me.Platform.Name = "Platform"
        Me.Platform.Padding = New System.Windows.Forms.Padding(0, 4, 0, 0)
        Me.Platform.Size = New System.Drawing.Size(48, 17)
        Me.Platform.TabIndex = 11
        Me.Platform.Text = "Platform:"
        '
        'Database
        '
        Me.Database.AutoSize = True
        Me.Database.Location = New System.Drawing.Point(3, 140)
        Me.Database.Name = "Database"
        Me.Database.Padding = New System.Windows.Forms.Padding(0, 4, 0, 0)
        Me.Database.Size = New System.Drawing.Size(56, 17)
        Me.Database.TabIndex = 12
        Me.Database.Text = "Database:"
        '
        'StatisticsLabel
        '
        Me.StatisticsLabel.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.StatisticsLabel.AutoSize = True
        Me.StatisticsLabel.Location = New System.Drawing.Point(0, 211)
        Me.StatisticsLabel.Name = "StatisticsLabel"
        Me.StatisticsLabel.Size = New System.Drawing.Size(49, 13)
        Me.StatisticsLabel.TabIndex = 7
        Me.StatisticsLabel.Text = "Statistics"
        '
        'GeneralWikiView
        '
        Me.Controls.Add(Me.StatisticsLabel)
        Me.Controls.Add(Me.AccountProps)
        Me.Controls.Add(Me.WikiLogo)
        Me.Controls.Add(Me.StatisticsList)
        Me.Name = "GeneralWikiView"
        Me.Size = New System.Drawing.Size(449, 384)
        CType(Me.WikiLogo, System.ComponentModel.ISupportInitialize).EndInit()
        Me.AccountProps.ResumeLayout(False)
        Me.AccountProps.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Private WithEvents WikiName As System.Windows.Forms.LinkLabel
    Private WithEvents WikiLogo As System.Windows.Forms.PictureBox
    Private WithEvents StatisticsList As System.Windows.Forms.ListViewEx
    Private WithEvents PropertyColumn As System.Windows.Forms.ColumnHeader
    Private WithEvents ValueColumn As System.Windows.Forms.ColumnHeader
    Private WithEvents AccountProps As System.Windows.Forms.FlowLayoutPanel
    Private WithEvents StatisticsLabel As System.Windows.Forms.Label
    Private WithEvents ContentLanguage As System.Windows.Forms.Label
    Private WithEvents Family As System.Windows.Forms.Label
    Private WithEvents SecureServer As System.Windows.Forms.Label
    Private WithEvents ContentLicense As System.Windows.Forms.Label
    Private WithEvents Engine As System.Windows.Forms.Label
    Private WithEvents Platform As System.Windows.Forms.Label
    Private WithEvents Database As System.Windows.Forms.Label
    Private WithEvents MainPage As System.Windows.Forms.Label

End Class
