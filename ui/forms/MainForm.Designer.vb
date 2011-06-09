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
            Me.SystemMaintenance = New System.Windows.Forms.ToolStripMenuItem()
            Me.SystemClearCloud = New System.Windows.Forms.ToolStripMenuItem()
            Me.SystemTest = New System.Windows.Forms.ToolStripMenuItem()
            Me.SystemExit = New System.Windows.Forms.ToolStripMenuItem()
            Me.WikiMenu = New System.Windows.Forms.ToolStripMenuItem()
            Me.WikiProperties = New System.Windows.Forms.ToolStripMenuItem()
            Me.WikiFamilyProperties = New System.Windows.Forms.ToolStripMenuItem()
            Me.WikiRevisions = New System.Windows.Forms.ToolStripMenuItem()
            Me.WikiLog = New System.Windows.Forms.ToolStripMenuItem()
            Me.AccountMenu = New System.Windows.Forms.ToolStripMenuItem()
            Me.AccountGlobalProperties = New System.Windows.Forms.ToolStripMenuItem()
            Me.AccountProperties = New System.Windows.Forms.ToolStripMenuItem()
            Me.PageMenu = New System.Windows.Forms.ToolStripMenuItem()
            Me.PageFeedback = New System.Windows.Forms.ToolStripMenuItem()
            Me.UserMenu = New System.Windows.Forms.ToolStripMenuItem()
            Me.UserChangeGroups = New System.Windows.Forms.ToolStripMenuItem()
            Me.RevisionMenu = New System.Windows.Forms.ToolStripMenuItem()
            Me.QueryMenu = New System.Windows.Forms.ToolStripMenuItem()
            Me.QueryWindow = New System.Windows.Forms.ToolStripMenuItem()
            Me.HelpMenu = New System.Windows.Forms.ToolStripMenuItem()
            Me.HelpManual = New System.Windows.Forms.ToolStripMenuItem()
            Me.Separator1 = New System.Windows.Forms.ToolStripSeparator()
            Me.HelpAbout = New System.Windows.Forms.ToolStripMenuItem()
            Me.OuterPanel = New System.Windows.Forms.ToolStripContainer()
            Me.LogSplit = New System.Windows.Forms.EnhancedSplitContainer()
            Me.QueueSplit = New System.Windows.Forms.EnhancedSplitContainer()
            Me.Separator2 = New System.Windows.Forms.ToolStripSeparator()
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
            Me.MenuBar.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.SystemMenu, Me.WikiMenu, Me.AccountMenu, Me.PageMenu, Me.UserMenu, Me.RevisionMenu, Me.QueryMenu, Me.HelpMenu})
            Me.MenuBar.Location = New System.Drawing.Point(0, 0)
            Me.MenuBar.Name = "MenuBar"
            Me.MenuBar.Size = New System.Drawing.Size(640, 24)
            Me.MenuBar.TabIndex = 1
            '
            'SystemMenu
            '
            Me.SystemMenu.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.SystemLogout, Me.SystemMaintenance, Me.SystemTest, Me.Separator2, Me.SystemExit})
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
            'SystemMaintenance
            '
            Me.SystemMaintenance.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.SystemClearCloud})
            Me.SystemMaintenance.Name = "SystemMaintenance"
            Me.SystemMaintenance.Size = New System.Drawing.Size(152, 22)
            Me.SystemMaintenance.Text = "Maintenance"
            '
            'SystemClearCloud
            '
            Me.SystemClearCloud.Name = "SystemClearCloud"
            Me.SystemClearCloud.Size = New System.Drawing.Size(136, 22)
            Me.SystemClearCloud.Text = "Clear Cloud"
            '
            'SystemTest
            '
            Me.SystemTest.Name = "SystemTest"
            Me.SystemTest.Size = New System.Drawing.Size(152, 22)
            Me.SystemTest.Text = "Test"
            '
            'SystemExit
            '
            Me.SystemExit.Name = "SystemExit"
            Me.SystemExit.Size = New System.Drawing.Size(152, 22)
            Me.SystemExit.Text = "Exit"
            '
            'WikiMenu
            '
            Me.WikiMenu.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.WikiProperties, Me.WikiFamilyProperties, Me.WikiRevisions, Me.WikiLog})
            Me.WikiMenu.Name = "WikiMenu"
            Me.WikiMenu.Size = New System.Drawing.Size(42, 20)
            Me.WikiMenu.Text = "Wiki"
            '
            'WikiProperties
            '
            Me.WikiProperties.Name = "WikiProperties"
            Me.WikiProperties.Size = New System.Drawing.Size(165, 22)
            Me.WikiProperties.Text = "Properties"
            '
            'WikiFamilyProperties
            '
            Me.WikiFamilyProperties.Name = "WikiFamilyProperties"
            Me.WikiFamilyProperties.Size = New System.Drawing.Size(165, 22)
            Me.WikiFamilyProperties.Text = "Family Properties"
            '
            'WikiRevisions
            '
            Me.WikiRevisions.Name = "WikiRevisions"
            Me.WikiRevisions.Size = New System.Drawing.Size(165, 22)
            Me.WikiRevisions.Text = "Revisions"
            '
            'WikiLog
            '
            Me.WikiLog.Name = "WikiLog"
            Me.WikiLog.Size = New System.Drawing.Size(165, 22)
            Me.WikiLog.Text = "Log"
            '
            'AccountMenu
            '
            Me.AccountMenu.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.AccountGlobalProperties, Me.AccountProperties})
            Me.AccountMenu.Name = "AccountMenu"
            Me.AccountMenu.Size = New System.Drawing.Size(64, 20)
            Me.AccountMenu.Text = "Account"
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
            'PageMenu
            '
            Me.PageMenu.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.PageFeedback})
            Me.PageMenu.Name = "PageMenu"
            Me.PageMenu.Size = New System.Drawing.Size(45, 20)
            Me.PageMenu.Text = "Page"
            '
            'PageFeedback
            '
            Me.PageFeedback.Name = "PageFeedback"
            Me.PageFeedback.Size = New System.Drawing.Size(152, 22)
            Me.PageFeedback.Text = "Assess page"
            '
            'UserMenu
            '
            Me.UserMenu.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.UserChangeGroups})
            Me.UserMenu.Name = "UserMenu"
            Me.UserMenu.Size = New System.Drawing.Size(42, 20)
            Me.UserMenu.Text = "User"
            '
            'UserChangeGroups
            '
            Me.UserChangeGroups.Name = "UserChangeGroups"
            Me.UserChangeGroups.Size = New System.Drawing.Size(165, 22)
            Me.UserChangeGroups.Text = "Change Groups..."
            '
            'RevisionMenu
            '
            Me.RevisionMenu.Name = "RevisionMenu"
            Me.RevisionMenu.Size = New System.Drawing.Size(63, 20)
            Me.RevisionMenu.Text = "Revision"
            '
            'QueryMenu
            '
            Me.QueryMenu.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.QueryWindow})
            Me.QueryMenu.Name = "QueryMenu"
            Me.QueryMenu.Size = New System.Drawing.Size(51, 20)
            Me.QueryMenu.Text = "Query"
            '
            'QueryWindow
            '
            Me.QueryWindow.Name = "QueryWindow"
            Me.QueryWindow.Size = New System.Drawing.Size(153, 22)
            Me.QueryWindow.Text = "Query Window"
            '
            'HelpMenu
            '
            Me.HelpMenu.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.HelpManual, Me.Separator1, Me.HelpAbout})
            Me.HelpMenu.Name = "HelpMenu"
            Me.HelpMenu.Size = New System.Drawing.Size(44, 20)
            Me.HelpMenu.Text = "Help"
            '
            'HelpManual
            '
            Me.HelpManual.Name = "HelpManual"
            Me.HelpManual.Size = New System.Drawing.Size(152, 22)
            Me.HelpManual.Text = "Manual"
            '
            'Separator1
            '
            Me.Separator1.Name = "Separator1"
            Me.Separator1.Size = New System.Drawing.Size(149, 6)
            '
            'HelpAbout
            '
            Me.HelpAbout.Name = "HelpAbout"
            Me.HelpAbout.Size = New System.Drawing.Size(152, 22)
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
            'Separator2
            '
            Me.Separator2.Name = "Separator2"
            Me.Separator2.Size = New System.Drawing.Size(149, 6)
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
        Public WithEvents SystemMenu As System.Windows.Forms.ToolStripMenuItem
        Public WithEvents SystemLogout As System.Windows.Forms.ToolStripMenuItem
        Public WithEvents SystemExit As System.Windows.Forms.ToolStripMenuItem
        Public WithEvents WikiMenu As System.Windows.Forms.ToolStripMenuItem
        Public WithEvents AccountMenu As System.Windows.Forms.ToolStripMenuItem
        Public WithEvents WikiProperties As System.Windows.Forms.ToolStripMenuItem
        Public WithEvents PageMenu As System.Windows.Forms.ToolStripMenuItem
        Public WithEvents UserMenu As System.Windows.Forms.ToolStripMenuItem
        Public WithEvents RevisionMenu As System.Windows.Forms.ToolStripMenuItem
        Public WithEvents HelpMenu As System.Windows.Forms.ToolStripMenuItem
        Public WithEvents AccountProperties As System.Windows.Forms.ToolStripMenuItem
        Private WithEvents HelpAbout As System.Windows.Forms.ToolStripMenuItem
        Public WithEvents Separator1 As System.Windows.Forms.ToolStripSeparator
        Private WithEvents HelpManual As System.Windows.Forms.ToolStripMenuItem
        Public WithEvents OuterPanel As System.Windows.Forms.ToolStripContainer
        Private WithEvents LogSplit As System.Windows.Forms.EnhancedSplitContainer
        Private WithEvents QueueSplit As System.Windows.Forms.EnhancedSplitContainer
        Private WithEvents MenuBar As System.Windows.Forms.MenuStrip
        Public WithEvents AccountGlobalProperties As System.Windows.Forms.ToolStripMenuItem
        Public WithEvents WikiFamilyProperties As System.Windows.Forms.ToolStripMenuItem
        Private WithEvents UserChangeGroups As System.Windows.Forms.ToolStripMenuItem
        Public WithEvents QueryMenu As System.Windows.Forms.ToolStripMenuItem
        Public WithEvents QueryWindow As System.Windows.Forms.ToolStripMenuItem
        Public WithEvents SystemMaintenance As System.Windows.Forms.ToolStripMenuItem
        Public WithEvents SystemClearCloud As System.Windows.Forms.ToolStripMenuItem
        Friend WithEvents WikiRevisions As System.Windows.Forms.ToolStripMenuItem
        Public WithEvents PageFeedback As System.Windows.Forms.ToolStripMenuItem
        Friend WithEvents WikiLog As System.Windows.Forms.ToolStripMenuItem
        Friend WithEvents SystemTest As System.Windows.Forms.ToolStripMenuItem
        Friend WithEvents Separator2 As System.Windows.Forms.ToolStripSeparator
    End Class
End Namespace