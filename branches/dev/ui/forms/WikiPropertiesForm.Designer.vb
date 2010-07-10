<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class WikiPropertiesForm
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
        Me.SpacesTab = New System.Windows.Forms.TabPage
        Me.SpacesList = New System.Windows.Forms.ListViewEx
        Me.SpaceNumberColumn = New System.Windows.Forms.ColumnHeader
        Me.SpaceNameColumn = New System.Windows.Forms.ColumnHeader
        Me.SpaceInfoColumn = New System.Windows.Forms.ColumnHeader
        Me.SpacesCount = New System.Windows.Forms.Label
        Me.GadgetsTab = New System.Windows.Forms.TabPage
        Me.GadgetProps = New System.Windows.Forms.FlowLayoutPanel
        Me.GadgetName = New System.Windows.Forms.LinkLabel
        Me.GadgetType = New System.Windows.Forms.Label
        Me.GadgetDescription = New System.Windows.Forms.Label
        Me.GadgetImage = New System.Windows.Forms.PictureBox
        Me.GadgetsCount = New System.Windows.Forms.Label
        Me.GadgetsList = New System.Windows.Forms.ListViewEx
        Me.GadgetNameColumn = New System.Windows.Forms.ColumnHeader
        Me.GadgetTypeColumn = New System.Windows.Forms.ColumnHeader
        Me.GadgetDescriptionColumn = New System.Windows.Forms.ColumnHeader
        Me.FlagsTab = New System.Windows.Forms.TabPage
        Me.FlagsCount = New System.Windows.Forms.Label
        Me.FlagsList = New System.Windows.Forms.ListViewEx
        Me.FlagNameColumn = New System.Windows.Forms.ColumnHeader
        Me.FlagDisplayNameColumn = New System.Windows.Forms.ColumnHeader
        Me.FlagLevelsColumn = New System.Windows.Forms.ColumnHeader
        Me.FlagQualityLevelColumn = New System.Windows.Forms.ColumnHeader
        Me.PristineLevelColumn = New System.Windows.Forms.ColumnHeader
        Me.Tabs = New System.Windows.Forms.TabControl
        Me.GeneralTab = New System.Windows.Forms.TabPage
        Me.WikiName = New System.Windows.Forms.LinkLabel
        Me.WikiLogo = New System.Windows.Forms.PictureBox
        Me.GeneralList = New System.Windows.Forms.ListViewEx
        Me.PropertyColumn = New System.Windows.Forms.ColumnHeader
        Me.ValueColumn = New System.Windows.Forms.ColumnHeader
        Me.ExtensionsTab = New System.Windows.Forms.TabPage
        Me.ExtensionsCount = New System.Windows.Forms.Label
        Me.ExtProps = New System.Windows.Forms.FlowLayoutPanel
        Me.ExtName = New System.Windows.Forms.LinkLabel
        Me.ExtDescription = New System.Windows.Forms.Label
        Me.ExtVersion = New System.Windows.Forms.Label
        Me.ExtAuthor = New System.Windows.Forms.Label
        Me.ExtensionsList = New System.Windows.Forms.ListViewEx
        Me.NameColumn = New System.Windows.Forms.ColumnHeader
        Me.TypeColumn = New System.Windows.Forms.ColumnHeader
        Me.VersionColumn = New System.Windows.Forms.ColumnHeader
        Me.ExtImage = New System.Windows.Forms.PictureBox
        Me.UserGroupsTab = New System.Windows.Forms.TabPage
        Me.UserGroupsCount = New System.Windows.Forms.Label
        Me.UserGroupsList = New System.Windows.Forms.ListViewEx
        Me.GroupColumn = New System.Windows.Forms.ColumnHeader
        Me.Rights = New System.Windows.Forms.ColumnHeader
        Me.ChangeTagsTab = New System.Windows.Forms.TabPage
        Me.ChangeTagsCount = New System.Windows.Forms.Label
        Me.ChangeTagsList = New System.Windows.Forms.ListViewEx
        Me.TagNameColumn = New System.Windows.Forms.ColumnHeader
        Me.TagDescriptionColumn = New System.Windows.Forms.ColumnHeader
        Me.TagCountColumn = New System.Windows.Forms.ColumnHeader
        Me.SpacesTab.SuspendLayout()
        Me.GadgetsTab.SuspendLayout()
        Me.GadgetProps.SuspendLayout()
        CType(Me.GadgetImage, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.FlagsTab.SuspendLayout()
        Me.Tabs.SuspendLayout()
        Me.GeneralTab.SuspendLayout()
        CType(Me.WikiLogo, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.ExtensionsTab.SuspendLayout()
        Me.ExtProps.SuspendLayout()
        CType(Me.ExtImage, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.UserGroupsTab.SuspendLayout()
        Me.ChangeTagsTab.SuspendLayout()
        Me.SuspendLayout()
        '
        'SpacesTab
        '
        Me.SpacesTab.Controls.Add(Me.SpacesList)
        Me.SpacesTab.Controls.Add(Me.SpacesCount)
        Me.SpacesTab.Location = New System.Drawing.Point(4, 22)
        Me.SpacesTab.Name = "SpacesTab"
        Me.SpacesTab.Padding = New System.Windows.Forms.Padding(3)
        Me.SpacesTab.Size = New System.Drawing.Size(541, 378)
        Me.SpacesTab.TabIndex = 6
        Me.SpacesTab.Text = "Namespaces"
        Me.SpacesTab.UseVisualStyleBackColor = True
        '
        'SpacesList
        '
        Me.SpacesList.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.SpacesList.Columns.AddRange(New System.Windows.Forms.ColumnHeader() {Me.SpaceNumberColumn, Me.SpaceNameColumn, Me.SpaceInfoColumn})
        Me.SpacesList.FlexibleColumn = 1
        Me.SpacesList.FullRowSelect = True
        Me.SpacesList.GridLines = True
        Me.SpacesList.Location = New System.Drawing.Point(3, 23)
        Me.SpacesList.Name = "SpacesList"
        Me.SpacesList.ShowGroups = False
        Me.SpacesList.Size = New System.Drawing.Size(532, 345)
        Me.SpacesList.SortOnColumnClick = True
        Me.SpacesList.TabIndex = 18
        Me.SpacesList.UseCompatibleStateImageBehavior = False
        Me.SpacesList.View = System.Windows.Forms.View.Details
        '
        'SpaceNumberColumn
        '
        Me.SpaceNumberColumn.Text = "#"
        Me.SpaceNumberColumn.Width = 30
        '
        'SpaceNameColumn
        '
        Me.SpaceNameColumn.Text = "Name"
        Me.SpaceNameColumn.Width = 257
        '
        'SpaceInfoColumn
        '
        Me.SpaceInfoColumn.Text = "Properties"
        Me.SpaceInfoColumn.Width = 223
        '
        'SpacesCount
        '
        Me.SpacesCount.AutoSize = True
        Me.SpacesCount.Location = New System.Drawing.Point(3, 7)
        Me.SpacesCount.Name = "SpacesCount"
        Me.SpacesCount.Size = New System.Drawing.Size(40, 13)
        Me.SpacesCount.TabIndex = 17
        Me.SpacesCount.Text = "0 items"
        '
        'GadgetsTab
        '
        Me.GadgetsTab.Controls.Add(Me.GadgetProps)
        Me.GadgetsTab.Controls.Add(Me.GadgetImage)
        Me.GadgetsTab.Controls.Add(Me.GadgetsCount)
        Me.GadgetsTab.Controls.Add(Me.GadgetsList)
        Me.GadgetsTab.Location = New System.Drawing.Point(4, 22)
        Me.GadgetsTab.Name = "GadgetsTab"
        Me.GadgetsTab.Padding = New System.Windows.Forms.Padding(3)
        Me.GadgetsTab.Size = New System.Drawing.Size(541, 378)
        Me.GadgetsTab.TabIndex = 7
        Me.GadgetsTab.Text = "Gadgets"
        Me.GadgetsTab.UseVisualStyleBackColor = True
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
        Me.GadgetProps.Location = New System.Drawing.Point(69, 294)
        Me.GadgetProps.Name = "GadgetProps"
        Me.GadgetProps.Padding = New System.Windows.Forms.Padding(0, 5, 0, 0)
        Me.GadgetProps.Size = New System.Drawing.Size(466, 78)
        Me.GadgetProps.TabIndex = 18
        Me.GadgetProps.Visible = False
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
        'GadgetImage
        '
        Me.GadgetImage.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.GadgetImage.Image = Global.Resources.mediawiki_gadget
        Me.GadgetImage.Location = New System.Drawing.Point(3, 294)
        Me.GadgetImage.Name = "GadgetImage"
        Me.GadgetImage.Size = New System.Drawing.Size(64, 78)
        Me.GadgetImage.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage
        Me.GadgetImage.TabIndex = 17
        Me.GadgetImage.TabStop = False
        Me.GadgetImage.Visible = False
        '
        'GadgetsCount
        '
        Me.GadgetsCount.AutoSize = True
        Me.GadgetsCount.Location = New System.Drawing.Point(3, 7)
        Me.GadgetsCount.Name = "GadgetsCount"
        Me.GadgetsCount.Size = New System.Drawing.Size(40, 13)
        Me.GadgetsCount.TabIndex = 16
        Me.GadgetsCount.Text = "0 items"
        '
        'GadgetsList
        '
        Me.GadgetsList.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.GadgetsList.Columns.AddRange(New System.Windows.Forms.ColumnHeader() {Me.GadgetNameColumn, Me.GadgetTypeColumn, Me.GadgetDescriptionColumn})
        Me.GadgetsList.FlexibleColumn = 2
        Me.GadgetsList.FullRowSelect = True
        Me.GadgetsList.GridLines = True
        Me.GadgetsList.Location = New System.Drawing.Point(3, 25)
        Me.GadgetsList.Name = "GadgetsList"
        Me.GadgetsList.ShowGroups = False
        Me.GadgetsList.Size = New System.Drawing.Size(532, 263)
        Me.GadgetsList.SortOnColumnClick = True
        Me.GadgetsList.TabIndex = 1
        Me.GadgetsList.UseCompatibleStateImageBehavior = False
        Me.GadgetsList.View = System.Windows.Forms.View.Details
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
        Me.GadgetDescriptionColumn.Width = 290
        '
        'FlagsTab
        '
        Me.FlagsTab.Controls.Add(Me.FlagsCount)
        Me.FlagsTab.Controls.Add(Me.FlagsList)
        Me.FlagsTab.Location = New System.Drawing.Point(4, 22)
        Me.FlagsTab.Name = "FlagsTab"
        Me.FlagsTab.Padding = New System.Windows.Forms.Padding(3)
        Me.FlagsTab.Size = New System.Drawing.Size(541, 378)
        Me.FlagsTab.TabIndex = 5
        Me.FlagsTab.Text = "Flagged revisions"
        Me.FlagsTab.UseVisualStyleBackColor = True
        '
        'FlagsCount
        '
        Me.FlagsCount.AutoSize = True
        Me.FlagsCount.Location = New System.Drawing.Point(3, 7)
        Me.FlagsCount.Name = "FlagsCount"
        Me.FlagsCount.Size = New System.Drawing.Size(40, 13)
        Me.FlagsCount.TabIndex = 16
        Me.FlagsCount.Text = "0 items"
        '
        'FlagsList
        '
        Me.FlagsList.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.FlagsList.Columns.AddRange(New System.Windows.Forms.ColumnHeader() {Me.FlagNameColumn, Me.FlagDisplayNameColumn, Me.FlagLevelsColumn, Me.FlagQualityLevelColumn, Me.PristineLevelColumn})
        Me.FlagsList.FlexibleColumn = 1
        Me.FlagsList.FullRowSelect = True
        Me.FlagsList.GridLines = True
        Me.FlagsList.Location = New System.Drawing.Point(3, 23)
        Me.FlagsList.Name = "FlagsList"
        Me.FlagsList.ShowGroups = False
        Me.FlagsList.Size = New System.Drawing.Size(532, 349)
        Me.FlagsList.SortOnColumnClick = True
        Me.FlagsList.TabIndex = 0
        Me.FlagsList.UseCompatibleStateImageBehavior = False
        Me.FlagsList.View = System.Windows.Forms.View.Details
        '
        'FlagNameColumn
        '
        Me.FlagNameColumn.Text = "Name"
        Me.FlagNameColumn.Width = 99
        '
        'FlagDisplayNameColumn
        '
        Me.FlagDisplayNameColumn.Text = "Display name"
        Me.FlagDisplayNameColumn.Width = 199
        '
        'FlagLevelsColumn
        '
        Me.FlagLevelsColumn.Text = "Levels"
        Me.FlagLevelsColumn.Width = 48
        '
        'FlagQualityLevelColumn
        '
        Me.FlagQualityLevelColumn.Text = """Quality"" level"
        Me.FlagQualityLevelColumn.Width = 81
        '
        'PristineLevelColumn
        '
        Me.PristineLevelColumn.Text = """Pristine"" level"
        Me.PristineLevelColumn.Width = 83
        '
        'Tabs
        '
        Me.Tabs.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Tabs.Controls.Add(Me.GeneralTab)
        Me.Tabs.Controls.Add(Me.SpacesTab)
        Me.Tabs.Controls.Add(Me.ExtensionsTab)
        Me.Tabs.Controls.Add(Me.UserGroupsTab)
        Me.Tabs.Controls.Add(Me.ChangeTagsTab)
        Me.Tabs.Controls.Add(Me.FlagsTab)
        Me.Tabs.Controls.Add(Me.GadgetsTab)
        Me.Tabs.Location = New System.Drawing.Point(6, 4)
        Me.Tabs.Multiline = True
        Me.Tabs.Name = "Tabs"
        Me.Tabs.SelectedIndex = 0
        Me.Tabs.Size = New System.Drawing.Size(549, 404)
        Me.Tabs.SizeMode = System.Windows.Forms.TabSizeMode.FillToRight
        Me.Tabs.TabIndex = 6
        '
        'GeneralTab
        '
        Me.GeneralTab.Controls.Add(Me.WikiName)
        Me.GeneralTab.Controls.Add(Me.WikiLogo)
        Me.GeneralTab.Controls.Add(Me.GeneralList)
        Me.GeneralTab.Location = New System.Drawing.Point(4, 22)
        Me.GeneralTab.Name = "GeneralTab"
        Me.GeneralTab.Padding = New System.Windows.Forms.Padding(3)
        Me.GeneralTab.Size = New System.Drawing.Size(541, 378)
        Me.GeneralTab.TabIndex = 0
        Me.GeneralTab.Text = "General"
        Me.GeneralTab.UseVisualStyleBackColor = True
        '
        'WikiName
        '
        Me.WikiName.AutoSize = True
        Me.WikiName.Font = New System.Drawing.Font("Tahoma", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.WikiName.LinkBehavior = System.Windows.Forms.LinkBehavior.HoverUnderline
        Me.WikiName.LinkColor = System.Drawing.SystemColors.HotTrack
        Me.WikiName.Location = New System.Drawing.Point(132, 10)
        Me.WikiName.Name = "WikiName"
        Me.WikiName.Size = New System.Drawing.Size(127, 18)
        Me.WikiName.TabIndex = 2
        Me.WikiName.TabStop = True
        Me.WikiName.Text = "Wikipedia (English)"
        Me.WikiName.VisitedLinkColor = System.Drawing.SystemColors.HotTrack
        '
        'WikiLogo
        '
        Me.WikiLogo.Image = Global.Resources.mediawiki_wiki
        Me.WikiLogo.Location = New System.Drawing.Point(3, 5)
        Me.WikiLogo.Name = "WikiLogo"
        Me.WikiLogo.Size = New System.Drawing.Size(128, 128)
        Me.WikiLogo.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage
        Me.WikiLogo.TabIndex = 1
        Me.WikiLogo.TabStop = False
        '
        'GeneralList
        '
        Me.GeneralList.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.GeneralList.Columns.AddRange(New System.Windows.Forms.ColumnHeader() {Me.PropertyColumn, Me.ValueColumn})
        Me.GeneralList.FlexibleColumn = 1
        Me.GeneralList.FullRowSelect = True
        Me.GeneralList.GridLines = True
        Me.GeneralList.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.None
        Me.GeneralList.Location = New System.Drawing.Point(3, 137)
        Me.GeneralList.Name = "GeneralList"
        Me.GeneralList.ShowGroups = False
        Me.GeneralList.Size = New System.Drawing.Size(535, 213)
        Me.GeneralList.SortOnColumnClick = True
        Me.GeneralList.TabIndex = 0
        Me.GeneralList.UseCompatibleStateImageBehavior = False
        Me.GeneralList.View = System.Windows.Forms.View.Details
        '
        'PropertyColumn
        '
        Me.PropertyColumn.Text = "Property"
        Me.PropertyColumn.Width = 210
        '
        'ValueColumn
        '
        Me.ValueColumn.Text = "Value"
        Me.ValueColumn.Width = 303
        '
        'ExtensionsTab
        '
        Me.ExtensionsTab.Controls.Add(Me.ExtensionsCount)
        Me.ExtensionsTab.Controls.Add(Me.ExtProps)
        Me.ExtensionsTab.Controls.Add(Me.ExtensionsList)
        Me.ExtensionsTab.Controls.Add(Me.ExtImage)
        Me.ExtensionsTab.Location = New System.Drawing.Point(4, 22)
        Me.ExtensionsTab.Name = "ExtensionsTab"
        Me.ExtensionsTab.Padding = New System.Windows.Forms.Padding(3)
        Me.ExtensionsTab.Size = New System.Drawing.Size(541, 378)
        Me.ExtensionsTab.TabIndex = 1
        Me.ExtensionsTab.Text = "Extensions"
        Me.ExtensionsTab.UseVisualStyleBackColor = True
        '
        'ExtensionsCount
        '
        Me.ExtensionsCount.AutoSize = True
        Me.ExtensionsCount.Location = New System.Drawing.Point(3, 7)
        Me.ExtensionsCount.Name = "ExtensionsCount"
        Me.ExtensionsCount.Size = New System.Drawing.Size(40, 13)
        Me.ExtensionsCount.TabIndex = 13
        Me.ExtensionsCount.Text = "0 items"
        '
        'ExtProps
        '
        Me.ExtProps.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ExtProps.AutoSize = True
        Me.ExtProps.Controls.Add(Me.ExtName)
        Me.ExtProps.Controls.Add(Me.ExtDescription)
        Me.ExtProps.Controls.Add(Me.ExtVersion)
        Me.ExtProps.Controls.Add(Me.ExtAuthor)
        Me.ExtProps.FlowDirection = System.Windows.Forms.FlowDirection.TopDown
        Me.ExtProps.Location = New System.Drawing.Point(68, 287)
        Me.ExtProps.Name = "ExtProps"
        Me.ExtProps.Padding = New System.Windows.Forms.Padding(0, 5, 0, 0)
        Me.ExtProps.Size = New System.Drawing.Size(467, 85)
        Me.ExtProps.TabIndex = 6
        Me.ExtProps.Visible = False
        '
        'ExtName
        '
        Me.ExtName.AutoSize = True
        Me.ExtName.Font = New System.Drawing.Font("Tahoma", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ExtName.ForeColor = System.Drawing.SystemColors.HotTrack
        Me.ExtName.LinkBehavior = System.Windows.Forms.LinkBehavior.HoverUnderline
        Me.ExtName.LinkColor = System.Drawing.SystemColors.HotTrack
        Me.ExtName.Location = New System.Drawing.Point(3, 5)
        Me.ExtName.Name = "ExtName"
        Me.ExtName.Size = New System.Drawing.Size(47, 18)
        Me.ExtName.TabIndex = 4
        Me.ExtName.TabStop = True
        Me.ExtName.Text = "Name"
        Me.ExtName.VisitedLinkColor = System.Drawing.SystemColors.HotTrack
        '
        'ExtDescription
        '
        Me.ExtDescription.AutoSize = True
        Me.ExtDescription.Location = New System.Drawing.Point(3, 23)
        Me.ExtDescription.Name = "ExtDescription"
        Me.ExtDescription.Padding = New System.Windows.Forms.Padding(0, 2, 0, 2)
        Me.ExtDescription.Size = New System.Drawing.Size(60, 17)
        Me.ExtDescription.TabIndex = 2
        Me.ExtDescription.Text = "Description"
        '
        'ExtVersion
        '
        Me.ExtVersion.AutoSize = True
        Me.ExtVersion.Location = New System.Drawing.Point(3, 40)
        Me.ExtVersion.Name = "ExtVersion"
        Me.ExtVersion.Padding = New System.Windows.Forms.Padding(0, 2, 0, 2)
        Me.ExtVersion.Size = New System.Drawing.Size(63, 17)
        Me.ExtVersion.TabIndex = 5
        Me.ExtVersion.Text = "Version: 1.0"
        '
        'ExtAuthor
        '
        Me.ExtAuthor.AutoSize = True
        Me.ExtAuthor.Location = New System.Drawing.Point(3, 57)
        Me.ExtAuthor.Name = "ExtAuthor"
        Me.ExtAuthor.Padding = New System.Windows.Forms.Padding(0, 2, 0, 2)
        Me.ExtAuthor.Size = New System.Drawing.Size(41, 17)
        Me.ExtAuthor.TabIndex = 5
        Me.ExtAuthor.Text = "Author:"
        '
        'ExtensionsList
        '
        Me.ExtensionsList.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ExtensionsList.Columns.AddRange(New System.Windows.Forms.ColumnHeader() {Me.NameColumn, Me.TypeColumn, Me.VersionColumn})
        Me.ExtensionsList.FlexibleColumn = 0
        Me.ExtensionsList.FullRowSelect = True
        Me.ExtensionsList.GridLines = True
        Me.ExtensionsList.Location = New System.Drawing.Point(3, 23)
        Me.ExtensionsList.Name = "ExtensionsList"
        Me.ExtensionsList.ShowGroups = False
        Me.ExtensionsList.Size = New System.Drawing.Size(532, 258)
        Me.ExtensionsList.SortOnColumnClick = True
        Me.ExtensionsList.TabIndex = 1
        Me.ExtensionsList.UseCompatibleStateImageBehavior = False
        Me.ExtensionsList.View = System.Windows.Forms.View.Details
        '
        'NameColumn
        '
        Me.NameColumn.Text = "Name"
        Me.NameColumn.Width = 350
        '
        'TypeColumn
        '
        Me.TypeColumn.Text = "Type"
        Me.TypeColumn.Width = 100
        '
        'VersionColumn
        '
        Me.VersionColumn.Text = "Version"
        '
        'ExtImage
        '
        Me.ExtImage.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.ExtImage.Image = Global.Resources.mediawiki_extension
        Me.ExtImage.Location = New System.Drawing.Point(3, 287)
        Me.ExtImage.Name = "ExtImage"
        Me.ExtImage.Size = New System.Drawing.Size(64, 85)
        Me.ExtImage.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage
        Me.ExtImage.TabIndex = 3
        Me.ExtImage.TabStop = False
        Me.ExtImage.Visible = False
        '
        'UserGroupsTab
        '
        Me.UserGroupsTab.Controls.Add(Me.UserGroupsCount)
        Me.UserGroupsTab.Controls.Add(Me.UserGroupsList)
        Me.UserGroupsTab.Location = New System.Drawing.Point(4, 22)
        Me.UserGroupsTab.Name = "UserGroupsTab"
        Me.UserGroupsTab.Padding = New System.Windows.Forms.Padding(3)
        Me.UserGroupsTab.Size = New System.Drawing.Size(541, 378)
        Me.UserGroupsTab.TabIndex = 2
        Me.UserGroupsTab.Text = "User groups"
        Me.UserGroupsTab.UseVisualStyleBackColor = True
        '
        'UserGroupsCount
        '
        Me.UserGroupsCount.AutoSize = True
        Me.UserGroupsCount.Location = New System.Drawing.Point(3, 7)
        Me.UserGroupsCount.Name = "UserGroupsCount"
        Me.UserGroupsCount.Size = New System.Drawing.Size(40, 13)
        Me.UserGroupsCount.TabIndex = 14
        Me.UserGroupsCount.Text = "0 items"
        '
        'UserGroupsList
        '
        Me.UserGroupsList.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.UserGroupsList.Columns.AddRange(New System.Windows.Forms.ColumnHeader() {Me.GroupColumn, Me.Rights})
        Me.UserGroupsList.FlexibleColumn = 1
        Me.UserGroupsList.FullRowSelect = True
        Me.UserGroupsList.GridLines = True
        Me.UserGroupsList.Location = New System.Drawing.Point(3, 23)
        Me.UserGroupsList.Name = "UserGroupsList"
        Me.UserGroupsList.ShowGroups = False
        Me.UserGroupsList.Size = New System.Drawing.Size(532, 349)
        Me.UserGroupsList.SortOnColumnClick = True
        Me.UserGroupsList.TabIndex = 0
        Me.UserGroupsList.UseCompatibleStateImageBehavior = False
        Me.UserGroupsList.View = System.Windows.Forms.View.Details
        '
        'GroupColumn
        '
        Me.GroupColumn.Text = "Group"
        Me.GroupColumn.Width = 120
        '
        'Rights
        '
        Me.Rights.Text = "Rights"
        Me.Rights.Width = 390
        '
        'ChangeTagsTab
        '
        Me.ChangeTagsTab.Controls.Add(Me.ChangeTagsCount)
        Me.ChangeTagsTab.Controls.Add(Me.ChangeTagsList)
        Me.ChangeTagsTab.Location = New System.Drawing.Point(4, 22)
        Me.ChangeTagsTab.Name = "ChangeTagsTab"
        Me.ChangeTagsTab.Padding = New System.Windows.Forms.Padding(3)
        Me.ChangeTagsTab.Size = New System.Drawing.Size(541, 378)
        Me.ChangeTagsTab.TabIndex = 4
        Me.ChangeTagsTab.Text = "Change tags"
        Me.ChangeTagsTab.UseVisualStyleBackColor = True
        '
        'ChangeTagsCount
        '
        Me.ChangeTagsCount.AutoSize = True
        Me.ChangeTagsCount.Location = New System.Drawing.Point(3, 7)
        Me.ChangeTagsCount.Name = "ChangeTagsCount"
        Me.ChangeTagsCount.Size = New System.Drawing.Size(40, 13)
        Me.ChangeTagsCount.TabIndex = 15
        Me.ChangeTagsCount.Text = "0 items"
        '
        'ChangeTagsList
        '
        Me.ChangeTagsList.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ChangeTagsList.Columns.AddRange(New System.Windows.Forms.ColumnHeader() {Me.TagNameColumn, Me.TagDescriptionColumn, Me.TagCountColumn})
        Me.ChangeTagsList.FlexibleColumn = 1
        Me.ChangeTagsList.FullRowSelect = True
        Me.ChangeTagsList.GridLines = True
        Me.ChangeTagsList.Location = New System.Drawing.Point(3, 23)
        Me.ChangeTagsList.Name = "ChangeTagsList"
        Me.ChangeTagsList.ShowGroups = False
        Me.ChangeTagsList.Size = New System.Drawing.Size(532, 349)
        Me.ChangeTagsList.SortOnColumnClick = True
        Me.ChangeTagsList.TabIndex = 0
        Me.ChangeTagsList.UseCompatibleStateImageBehavior = False
        Me.ChangeTagsList.View = System.Windows.Forms.View.Details
        '
        'TagNameColumn
        '
        Me.TagNameColumn.Text = "Name"
        Me.TagNameColumn.Width = 156
        '
        'TagDescriptionColumn
        '
        Me.TagDescriptionColumn.Text = "Description"
        Me.TagDescriptionColumn.Width = 304
        '
        'TagCountColumn
        '
        Me.TagCountColumn.Text = "Count"
        Me.TagCountColumn.Width = 50
        '
        'WikiPropertiesForm
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(559, 412)
        Me.Controls.Add(Me.Tabs)
        Me.Name = "WikiPropertiesForm"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Wiki"
        Me.SpacesTab.ResumeLayout(False)
        Me.SpacesTab.PerformLayout()
        Me.GadgetsTab.ResumeLayout(False)
        Me.GadgetsTab.PerformLayout()
        Me.GadgetProps.ResumeLayout(False)
        Me.GadgetProps.PerformLayout()
        CType(Me.GadgetImage, System.ComponentModel.ISupportInitialize).EndInit()
        Me.FlagsTab.ResumeLayout(False)
        Me.FlagsTab.PerformLayout()
        Me.Tabs.ResumeLayout(False)
        Me.GeneralTab.ResumeLayout(False)
        Me.GeneralTab.PerformLayout()
        CType(Me.WikiLogo, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ExtensionsTab.ResumeLayout(False)
        Me.ExtensionsTab.PerformLayout()
        Me.ExtProps.ResumeLayout(False)
        Me.ExtProps.PerformLayout()
        CType(Me.ExtImage, System.ComponentModel.ISupportInitialize).EndInit()
        Me.UserGroupsTab.ResumeLayout(False)
        Me.UserGroupsTab.PerformLayout()
        Me.ChangeTagsTab.ResumeLayout(False)
        Me.ChangeTagsTab.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
    Private WithEvents Tabs As System.Windows.Forms.TabControl
    Private WithEvents GeneralTab As System.Windows.Forms.TabPage
    Private WithEvents ExtensionsTab As System.Windows.Forms.TabPage
    Private WithEvents UserGroupsTab As System.Windows.Forms.TabPage
    Private WithEvents GeneralList As System.Windows.Forms.ListViewEx
    Private WithEvents ExtensionsList As System.Windows.Forms.ListViewEx
    Private WithEvents ExtImage As System.Windows.Forms.PictureBox
    Private WithEvents ExtDescription As System.Windows.Forms.Label
    Private WithEvents ExtName As System.Windows.Forms.LinkLabel
    Private WithEvents VersionColumn As System.Windows.Forms.ColumnHeader
    Private WithEvents ChangeTagsTab As System.Windows.Forms.TabPage
    Private WithEvents ExtAuthor As System.Windows.Forms.Label
    Private WithEvents UserGroupsList As System.Windows.Forms.ListViewEx
    Private WithEvents ExtProps As System.Windows.Forms.FlowLayoutPanel
    Private WithEvents ExtVersion As System.Windows.Forms.Label
    Private WithEvents ChangeTagsList As System.Windows.Forms.ListViewEx
    Private WithEvents WikiLogo As System.Windows.Forms.PictureBox
    Private WithEvents WikiName As System.Windows.Forms.LinkLabel
    Private WithEvents PristineLevelColumn As System.Windows.Forms.ColumnHeader
    Private WithEvents FlagsCount As System.Windows.Forms.Label
    Private WithEvents ExtensionsCount As System.Windows.Forms.Label
    Private WithEvents UserGroupsCount As System.Windows.Forms.Label
    Private WithEvents ChangeTagsCount As System.Windows.Forms.Label
    Private WithEvents SpacesCount As System.Windows.Forms.Label
    Private WithEvents SpaceInfoColumn As System.Windows.Forms.ColumnHeader
    Private WithEvents GadgetsCount As System.Windows.Forms.Label
    Private WithEvents GadgetsList As System.Windows.Forms.ListViewEx
    Private WithEvents FlagsList As System.Windows.Forms.ListViewEx
    Private WithEvents PropertyColumn As System.Windows.Forms.ColumnHeader
    Private WithEvents ValueColumn As System.Windows.Forms.ColumnHeader
    Private WithEvents NameColumn As System.Windows.Forms.ColumnHeader
    Private WithEvents TypeColumn As System.Windows.Forms.ColumnHeader
    Private WithEvents GroupColumn As System.Windows.Forms.ColumnHeader
    Private WithEvents Rights As System.Windows.Forms.ColumnHeader
    Private WithEvents TagNameColumn As System.Windows.Forms.ColumnHeader
    Private WithEvents TagDescriptionColumn As System.Windows.Forms.ColumnHeader
    Private WithEvents TagCountColumn As System.Windows.Forms.ColumnHeader
    Private WithEvents FlagsTab As System.Windows.Forms.TabPage
    Private WithEvents FlagNameColumn As System.Windows.Forms.ColumnHeader
    Private WithEvents FlagDisplayNameColumn As System.Windows.Forms.ColumnHeader
    Private WithEvents FlagLevelsColumn As System.Windows.Forms.ColumnHeader
    Private WithEvents FlagQualityLevelColumn As System.Windows.Forms.ColumnHeader
    Private WithEvents SpacesList As System.Windows.Forms.ListViewEx
    Private WithEvents GadgetProps As System.Windows.Forms.FlowLayoutPanel
    Private WithEvents GadgetName As System.Windows.Forms.LinkLabel
    Private WithEvents GadgetDescription As System.Windows.Forms.Label
    Private WithEvents GadgetImage As System.Windows.Forms.PictureBox
    Private WithEvents GadgetType As System.Windows.Forms.Label
    Private WithEvents SpacesTab As System.Windows.Forms.TabPage
    Private WithEvents SpaceNumberColumn As System.Windows.Forms.ColumnHeader
    Private WithEvents SpaceNameColumn As System.Windows.Forms.ColumnHeader
    Private WithEvents GadgetsTab As System.Windows.Forms.TabPage
    Private WithEvents GadgetNameColumn As System.Windows.Forms.ColumnHeader
    Private WithEvents GadgetTypeColumn As System.Windows.Forms.ColumnHeader
    Private WithEvents GadgetDescriptionColumn As System.Windows.Forms.ColumnHeader
End Class
