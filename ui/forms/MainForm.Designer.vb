Namespace Huggle.UI
    <Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
    Partial Class MainForm
        Inherits Huggle.UI.HuggleForm

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
            Me.MenuBar = New System.Windows.Forms.MenuStrip()
            Me.SystemMenu = New System.Windows.Forms.ToolStripMenuItem()
            Me.SystemLogout = New System.Windows.Forms.ToolStripMenuItem()
            Me.SystemExit = New System.Windows.Forms.ToolStripMenuItem()
            Me.WikiToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
            Me.WikiFamilyProperties = New System.Windows.Forms.ToolStripMenuItem()
            Me.WikiProperties = New System.Windows.Forms.ToolStripMenuItem()
            Me.AssessPageToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
            Me.UserToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
            Me.AccountGlobalProperties = New System.Windows.Forms.ToolStripMenuItem()
            Me.AccountProperties = New System.Windows.Forms.ToolStripMenuItem()
            Me.PageToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
            Me.UserToolStripMenuItem1 = New System.Windows.Forms.ToolStripMenuItem()
            Me.UserChangeGroups = New System.Windows.Forms.ToolStripMenuItem()
            Me.RevisionToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
            Me.ToolStripMenuItem1 = New System.Windows.Forms.ToolStripMenuItem()
            Me.QueryWindowToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
            Me.HelpToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
            Me.HelpManual = New System.Windows.Forms.ToolStripMenuItem()
            Me.ToolStripSeparator4 = New System.Windows.Forms.ToolStripSeparator()
            Me.HelpAbout = New System.Windows.Forms.ToolStripMenuItem()
            Me.OuterPanel = New System.Windows.Forms.ToolStripContainer()
            Me.LogSplit = New System.Windows.Forms.EnhancedSplitContainer()
            Me.QueueSplit = New System.Windows.Forms.EnhancedSplitContainer()
            Me.MaintenanceToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
            Me.ClearCloudToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
            Me.MenuBar.SuspendLayout()
            Me.OuterPanel.ContentPanel.SuspendLayout()
            Me.OuterPanel.SuspendLayout()
            Me.LogSplit.Panel1.SuspendLayout()
            Me.LogSplit.SuspendLayout()
            Me.QueueSplit.SuspendLayout()
            Me.SuspendLayout()
            '
            'MenuBar
            '
            Me.MenuBar.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.SystemMenu, Me.WikiToolStripMenuItem, Me.UserToolStripMenuItem, Me.PageToolStripMenuItem, Me.UserToolStripMenuItem1, Me.RevisionToolStripMenuItem, Me.ToolStripMenuItem1, Me.HelpToolStripMenuItem})
            Me.MenuBar.Location = New System.Drawing.Point(0, 0)
            Me.MenuBar.Name = "MenuBar"
            Me.MenuBar.Size = New System.Drawing.Size(640, 24)
            Me.MenuBar.TabIndex = 1
            '
            'SystemMenu
            '
            Me.SystemMenu.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.SystemLogout, Me.MaintenanceToolStripMenuItem, Me.SystemExit})
            Me.SystemMenu.Name = "SystemMenu"
            Me.SystemMenu.Size = New System.Drawing.Size(57, 20)
            Me.SystemMenu.Text = "System"
            '
            'SystemLogout
            '
            Me.SystemLogout.Name = "SystemLogout"
            Me.SystemLogout.Size = New System.Drawing.Size(152, 22)
            Me.SystemLogout.Text = "Log out"
            '
            'SystemExit
            '
            Me.SystemExit.Name = "SystemExit"
            Me.SystemExit.Size = New System.Drawing.Size(152, 22)
            Me.SystemExit.Text = "Exit"
            '
            'WikiToolStripMenuItem
            '
            Me.WikiToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.WikiFamilyProperties, Me.WikiProperties, Me.AssessPageToolStripMenuItem})
            Me.WikiToolStripMenuItem.Name = "WikiToolStripMenuItem"
            Me.WikiToolStripMenuItem.Size = New System.Drawing.Size(42, 20)
            Me.WikiToolStripMenuItem.Text = "Wiki"
            '
            'WikiFamilyProperties
            '
            Me.WikiFamilyProperties.Name = "WikiFamilyProperties"
            Me.WikiFamilyProperties.Size = New System.Drawing.Size(174, 22)
            Me.WikiFamilyProperties.Text = "Family Properties..."
            '
            'WikiProperties
            '
            Me.WikiProperties.Name = "WikiProperties"
            Me.WikiProperties.Size = New System.Drawing.Size(174, 22)
            Me.WikiProperties.Text = "Properties..."
            '
            'AssessPageToolStripMenuItem
            '
            Me.AssessPageToolStripMenuItem.Name = "AssessPageToolStripMenuItem"
            Me.AssessPageToolStripMenuItem.Size = New System.Drawing.Size(174, 22)
            Me.AssessPageToolStripMenuItem.Text = "Assess page"
            '
            'UserToolStripMenuItem
            '
            Me.UserToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.AccountGlobalProperties, Me.AccountProperties})
            Me.UserToolStripMenuItem.Name = "UserToolStripMenuItem"
            Me.UserToolStripMenuItem.Size = New System.Drawing.Size(64, 20)
            Me.UserToolStripMenuItem.Text = "Account"
            '
            'AccountGlobalProperties
            '
            Me.AccountGlobalProperties.Name = "AccountGlobalProperties"
            Me.AccountGlobalProperties.Size = New System.Drawing.Size(173, 22)
            Me.AccountGlobalProperties.Text = "Global Properties..."
            '
            'AccountProperties
            '
            Me.AccountProperties.Name = "AccountProperties"
            Me.AccountProperties.Size = New System.Drawing.Size(173, 22)
            Me.AccountProperties.Text = "Properties..."
            '
            'PageToolStripMenuItem
            '
            Me.PageToolStripMenuItem.Name = "PageToolStripMenuItem"
            Me.PageToolStripMenuItem.Size = New System.Drawing.Size(45, 20)
            Me.PageToolStripMenuItem.Text = "Page"
            '
            'UserToolStripMenuItem1
            '
            Me.UserToolStripMenuItem1.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.UserChangeGroups})
            Me.UserToolStripMenuItem1.Name = "UserToolStripMenuItem1"
            Me.UserToolStripMenuItem1.Size = New System.Drawing.Size(42, 20)
            Me.UserToolStripMenuItem1.Text = "User"
            '
            'UserChangeGroups
            '
            Me.UserChangeGroups.Name = "UserChangeGroups"
            Me.UserChangeGroups.Size = New System.Drawing.Size(165, 22)
            Me.UserChangeGroups.Text = "Change Groups..."
            '
            'RevisionToolStripMenuItem
            '
            Me.RevisionToolStripMenuItem.Name = "RevisionToolStripMenuItem"
            Me.RevisionToolStripMenuItem.Size = New System.Drawing.Size(63, 20)
            Me.RevisionToolStripMenuItem.Text = "Revision"
            '
            'ToolStripMenuItem1
            '
            Me.ToolStripMenuItem1.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.QueryWindowToolStripMenuItem})
            Me.ToolStripMenuItem1.Name = "ToolStripMenuItem1"
            Me.ToolStripMenuItem1.Size = New System.Drawing.Size(51, 20)
            Me.ToolStripMenuItem1.Text = "Query"
            '
            'QueryWindowToolStripMenuItem
            '
            Me.QueryWindowToolStripMenuItem.Name = "QueryWindowToolStripMenuItem"
            Me.QueryWindowToolStripMenuItem.Size = New System.Drawing.Size(153, 22)
            Me.QueryWindowToolStripMenuItem.Text = "Query Window"
            '
            'HelpToolStripMenuItem
            '
            Me.HelpToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.HelpManual, Me.ToolStripSeparator4, Me.HelpAbout})
            Me.HelpToolStripMenuItem.Name = "HelpToolStripMenuItem"
            Me.HelpToolStripMenuItem.Size = New System.Drawing.Size(44, 20)
            Me.HelpToolStripMenuItem.Text = "Help"
            '
            'HelpManual
            '
            Me.HelpManual.Name = "HelpManual"
            Me.HelpManual.Size = New System.Drawing.Size(116, 22)
            Me.HelpManual.Text = "Manual"
            '
            'ToolStripSeparator4
            '
            Me.ToolStripSeparator4.Name = "ToolStripSeparator4"
            Me.ToolStripSeparator4.Size = New System.Drawing.Size(113, 6)
            '
            'HelpAbout
            '
            Me.HelpAbout.Name = "HelpAbout"
            Me.HelpAbout.Size = New System.Drawing.Size(116, 22)
            Me.HelpAbout.Text = "About..."
            '
            'OuterPanel
            '
            '
            'OuterPanel.ContentPanel
            '
            Me.OuterPanel.ContentPanel.Controls.Add(Me.LogSplit)
            Me.OuterPanel.ContentPanel.Size = New System.Drawing.Size(640, 362)
            Me.OuterPanel.Dock = System.Windows.Forms.DockStyle.Fill
            Me.OuterPanel.Location = New System.Drawing.Point(0, 24)
            Me.OuterPanel.Name = "OuterPanel"
            Me.OuterPanel.Size = New System.Drawing.Size(640, 387)
            Me.OuterPanel.TabIndex = 3
            Me.OuterPanel.Text = "ToolStripContainer1"
            '
            'LogSplit
            '
            Me.LogSplit.Dock = System.Windows.Forms.DockStyle.Fill
            Me.LogSplit.FixedPanel = System.Windows.Forms.FixedPanel.Panel2
            Me.LogSplit.Location = New System.Drawing.Point(0, 0)
            Me.LogSplit.Name = "LogSplit"
            Me.LogSplit.Orientation = System.Windows.Forms.Orientation.Horizontal
            '
            'LogSplit.Panel1
            '
            Me.LogSplit.Panel1.Controls.Add(Me.QueueSplit)
            Me.LogSplit.Panel1MinSize = 0
            Me.LogSplit.Panel2MinSize = 0
            Me.LogSplit.Size = New System.Drawing.Size(640, 362)
            Me.LogSplit.SplitterDistance = 260
            Me.LogSplit.TabIndex = 1
            '
            'QueueSplit
            '
            Me.QueueSplit.Dock = System.Windows.Forms.DockStyle.Fill
            Me.QueueSplit.FixedPanel = System.Windows.Forms.FixedPanel.Panel1
            Me.QueueSplit.Location = New System.Drawing.Point(0, 0)
            Me.QueueSplit.Name = "QueueSplit"
            Me.QueueSplit.Size = New System.Drawing.Size(640, 260)
            Me.QueueSplit.SplitterDistance = 200
            Me.QueueSplit.TabIndex = 0
            '
            'MaintenanceToolStripMenuItem
            '
            Me.MaintenanceToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ClearCloudToolStripMenuItem})
            Me.MaintenanceToolStripMenuItem.Name = "MaintenanceToolStripMenuItem"
            Me.MaintenanceToolStripMenuItem.Size = New System.Drawing.Size(152, 22)
            Me.MaintenanceToolStripMenuItem.Text = "Maintenance"
            '
            'ClearCloudToolStripMenuItem
            '
            Me.ClearCloudToolStripMenuItem.Name = "ClearCloudToolStripMenuItem"
            Me.ClearCloudToolStripMenuItem.Size = New System.Drawing.Size(152, 22)
            Me.ClearCloudToolStripMenuItem.Text = "Clear Cloud"
            '
            'MainForm
            '
            Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
            Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
            Me.ClientSize = New System.Drawing.Size(640, 411)
            Me.Controls.Add(Me.OuterPanel)
            Me.Controls.Add(Me.MenuBar)
            Me.MainMenuStrip = Me.MenuBar
            Me.MinimumSize = New System.Drawing.Size(480, 360)
            Me.Name = "MainForm"
            Me.Text = "Huggle"
            Me.MenuBar.ResumeLayout(False)
            Me.MenuBar.PerformLayout()
            Me.OuterPanel.ContentPanel.ResumeLayout(False)
            Me.OuterPanel.ResumeLayout(False)
            Me.OuterPanel.PerformLayout()
            Me.LogSplit.Panel1.ResumeLayout(False)
            Me.LogSplit.ResumeLayout(False)
            Me.QueueSplit.ResumeLayout(False)
            Me.ResumeLayout(False)
            Me.PerformLayout()

        End Sub
        Friend WithEvents SystemMenu As System.Windows.Forms.ToolStripMenuItem
        Friend WithEvents SystemLogout As System.Windows.Forms.ToolStripMenuItem
        Friend WithEvents SystemExit As System.Windows.Forms.ToolStripMenuItem
        Friend WithEvents WikiToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
        Friend WithEvents UserToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
        Friend WithEvents WikiProperties As System.Windows.Forms.ToolStripMenuItem
        Friend WithEvents PageToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
        Friend WithEvents UserToolStripMenuItem1 As System.Windows.Forms.ToolStripMenuItem
        Friend WithEvents RevisionToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
        Friend WithEvents HelpToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
        Friend WithEvents AccountProperties As System.Windows.Forms.ToolStripMenuItem
        Private WithEvents HelpAbout As System.Windows.Forms.ToolStripMenuItem
        Friend WithEvents ToolStripSeparator4 As System.Windows.Forms.ToolStripSeparator
        Private WithEvents HelpManual As System.Windows.Forms.ToolStripMenuItem
        Friend WithEvents OuterPanel As System.Windows.Forms.ToolStripContainer
        Private WithEvents LogSplit As System.Windows.Forms.EnhancedSplitContainer
        Private WithEvents QueueSplit As System.Windows.Forms.EnhancedSplitContainer
        Private WithEvents MenuBar As System.Windows.Forms.MenuStrip
        Friend WithEvents AccountGlobalProperties As System.Windows.Forms.ToolStripMenuItem
        Friend WithEvents WikiFamilyProperties As System.Windows.Forms.ToolStripMenuItem
        Private WithEvents UserChangeGroups As System.Windows.Forms.ToolStripMenuItem
        Friend WithEvents AssessPageToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
        Friend WithEvents ToolStripMenuItem1 As System.Windows.Forms.ToolStripMenuItem
        Friend WithEvents QueryWindowToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
        Friend WithEvents MaintenanceToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
        Friend WithEvents ClearCloudToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    End Class
End Namespace