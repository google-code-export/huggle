<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class MainForm
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
        Me.MenuBar = New System.Windows.Forms.MenuStrip()
        Me.SystemMenu = New System.Windows.Forms.ToolStripMenuItem()
        Me.SystemLogout = New System.Windows.Forms.ToolStripMenuItem()
        Me.SystemExit = New System.Windows.Forms.ToolStripMenuItem()
        Me.WikiToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.WikiFamilyProperties = New System.Windows.Forms.ToolStripMenuItem()
        Me.WikiProperties = New System.Windows.Forms.ToolStripMenuItem()
        Me.UserToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.AccountGlobalProperties = New System.Windows.Forms.ToolStripMenuItem()
        Me.AccountProperties = New System.Windows.Forms.ToolStripMenuItem()
        Me.PageToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.UserToolStripMenuItem1 = New System.Windows.Forms.ToolStripMenuItem()
        Me.RevisionToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.HelpToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.HelpManual = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripSeparator4 = New System.Windows.Forms.ToolStripSeparator()
        Me.HelpAbout = New System.Windows.Forms.ToolStripMenuItem()
        Me.OuterPanel = New System.Windows.Forms.ToolStripContainer()
        Me.LogSplit = New SplitContainerEx()
        Me.QueueSplit = New SplitContainerEx()
        Me.UserChangeGroups = New System.Windows.Forms.ToolStripMenuItem()
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
        Me.MenuBar.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.SystemMenu, Me.WikiToolStripMenuItem, Me.UserToolStripMenuItem, Me.PageToolStripMenuItem, Me.UserToolStripMenuItem1, Me.RevisionToolStripMenuItem, Me.HelpToolStripMenuItem})
        Me.MenuBar.Location = New System.Drawing.Point(0, 0)
        Me.MenuBar.Name = "MenuBar"
        Me.MenuBar.Size = New System.Drawing.Size(640, 24)
        Me.MenuBar.TabIndex = 1
        '
        'SystemMenu
        '
        Me.SystemMenu.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.SystemLogout, Me.SystemExit})
        Me.SystemMenu.Name = "SystemMenu"
        Me.SystemMenu.Size = New System.Drawing.Size(57, 20)
        Me.SystemMenu.Text = "System"
        '
        'SystemLogout
        '
        Me.SystemLogout.Name = "SystemLogout"
        Me.SystemLogout.Size = New System.Drawing.Size(115, 22)
        Me.SystemLogout.Text = "Log out"
        '
        'SystemExit
        '
        Me.SystemExit.Name = "SystemExit"
        Me.SystemExit.Size = New System.Drawing.Size(115, 22)
        Me.SystemExit.Text = "Exit"
        '
        'WikiToolStripMenuItem
        '
        Me.WikiToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.WikiFamilyProperties, Me.WikiProperties})
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
        'RevisionToolStripMenuItem
        '
        Me.RevisionToolStripMenuItem.Name = "RevisionToolStripMenuItem"
        Me.RevisionToolStripMenuItem.Size = New System.Drawing.Size(63, 20)
        Me.RevisionToolStripMenuItem.Text = "Revision"
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
        Me.HelpManual.Size = New System.Drawing.Size(152, 22)
        Me.HelpManual.Text = "Manual"
        '
        'ToolStripSeparator4
        '
        Me.ToolStripSeparator4.Name = "ToolStripSeparator4"
        Me.ToolStripSeparator4.Size = New System.Drawing.Size(149, 6)
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
        'UserChangeGroups
        '
        Me.UserChangeGroups.Name = "UserChangeGroups"
        Me.UserChangeGroups.Size = New System.Drawing.Size(165, 22)
        Me.UserChangeGroups.Text = "Change Groups..."
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
    Private WithEvents LogSplit As SplitContainerEx
    Private WithEvents QueueSplit As SplitContainerEx
    Private WithEvents MenuBar As System.Windows.Forms.MenuStrip
    Friend WithEvents AccountGlobalProperties As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents WikiFamilyProperties As System.Windows.Forms.ToolStripMenuItem
    Private WithEvents UserChangeGroups As System.Windows.Forms.ToolStripMenuItem
End Class
