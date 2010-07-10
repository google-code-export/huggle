<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class AboutForm
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
        Me.Logo = New System.Windows.Forms.PictureBox
        Me.Label2 = New System.Windows.Forms.Label
        Me.MediaWiki = New System.Windows.Forms.PictureBox
        Me.ManualLink = New System.Windows.Forms.LinkLabel
        Me.SourceLink = New System.Windows.Forms.LinkLabel
        Me.FeedbackLink = New System.Windows.Forms.LinkLabel
        Me.LayoutPanel = New System.Windows.Forms.FlowLayoutPanel
        Me.LinkLabel1 = New System.Windows.Forms.LinkLabel
        Me.LinkLabel2 = New System.Windows.Forms.LinkLabel
        Me.LinkLabel3 = New System.Windows.Forms.LinkLabel
        CType(Me.Logo, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MediaWiki, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.LayoutPanel.SuspendLayout()
        Me.SuspendLayout()
        '
        'Logo
        '
        Me.Logo.Anchor = System.Windows.Forms.AnchorStyles.Top
        Me.Logo.Location = New System.Drawing.Point(0, 0)
        Me.Logo.Name = "Logo"
        Me.Logo.Size = New System.Drawing.Size(270, 80)
        Me.Logo.TabIndex = 0
        Me.Logo.TabStop = False
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(3, 5)
        Me.Label2.Name = "Label2"
        Me.Label2.Padding = New System.Windows.Forms.Padding(0, 5, 0, 0)
        Me.Label2.Size = New System.Drawing.Size(69, 18)
        Me.Label2.TabIndex = 2
        Me.Label2.Text = "Version 1.0.0"
        '
        'MediaWiki
        '
        Me.MediaWiki.Anchor = System.Windows.Forms.AnchorStyles.Bottom
        Me.MediaWiki.Cursor = System.Windows.Forms.Cursors.Hand
        Me.MediaWiki.Location = New System.Drawing.Point(67, 227)
        Me.MediaWiki.Name = "MediaWiki"
        Me.MediaWiki.Size = New System.Drawing.Size(128, 40)
        Me.MediaWiki.TabIndex = 3
        Me.MediaWiki.TabStop = False
        '
        'ManualLink
        '
        Me.ManualLink.AutoSize = True
        Me.ManualLink.LinkBehavior = System.Windows.Forms.LinkBehavior.HoverUnderline
        Me.ManualLink.LinkColor = System.Drawing.SystemColors.HotTrack
        Me.ManualLink.Location = New System.Drawing.Point(3, 110)
        Me.ManualLink.Name = "ManualLink"
        Me.ManualLink.Padding = New System.Windows.Forms.Padding(10, 10, 0, 0)
        Me.ManualLink.Size = New System.Drawing.Size(52, 23)
        Me.ManualLink.TabIndex = 4
        Me.ManualLink.TabStop = True
        Me.ManualLink.Text = "Manual"
        Me.ManualLink.VisitedLinkColor = System.Drawing.SystemColors.HotTrack
        '
        'SourceLink
        '
        Me.SourceLink.AutoSize = True
        Me.SourceLink.LinkBehavior = System.Windows.Forms.LinkBehavior.HoverUnderline
        Me.SourceLink.LinkColor = System.Drawing.SystemColors.HotTrack
        Me.SourceLink.Location = New System.Drawing.Point(132, 110)
        Me.SourceLink.Name = "SourceLink"
        Me.SourceLink.Padding = New System.Windows.Forms.Padding(10, 10, 0, 0)
        Me.SourceLink.Size = New System.Drawing.Size(78, 23)
        Me.SourceLink.TabIndex = 4
        Me.SourceLink.TabStop = True
        Me.SourceLink.Text = "Source code"
        Me.SourceLink.VisitedLinkColor = System.Drawing.SystemColors.HotTrack
        '
        'FeedbackLink
        '
        Me.FeedbackLink.AutoSize = True
        Me.FeedbackLink.LinkBehavior = System.Windows.Forms.LinkBehavior.HoverUnderline
        Me.FeedbackLink.LinkColor = System.Drawing.SystemColors.HotTrack
        Me.FeedbackLink.Location = New System.Drawing.Point(61, 110)
        Me.FeedbackLink.Name = "FeedbackLink"
        Me.FeedbackLink.Padding = New System.Windows.Forms.Padding(10, 10, 0, 0)
        Me.FeedbackLink.Size = New System.Drawing.Size(65, 23)
        Me.FeedbackLink.TabIndex = 4
        Me.FeedbackLink.TabStop = True
        Me.FeedbackLink.Text = "Feedback"
        Me.FeedbackLink.VisitedLinkColor = System.Drawing.SystemColors.HotTrack
        '
        'LayoutPanel
        '
        Me.LayoutPanel.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.LayoutPanel.Controls.Add(Me.Label2)
        Me.LayoutPanel.Controls.Add(Me.LinkLabel2)
        Me.LayoutPanel.Controls.Add(Me.LinkLabel1)
        Me.LayoutPanel.Controls.Add(Me.LinkLabel3)
        Me.LayoutPanel.Controls.Add(Me.ManualLink)
        Me.LayoutPanel.Controls.Add(Me.FeedbackLink)
        Me.LayoutPanel.Controls.Add(Me.SourceLink)
        Me.LayoutPanel.Location = New System.Drawing.Point(0, 79)
        Me.LayoutPanel.Name = "LayoutPanel"
        Me.LayoutPanel.Padding = New System.Windows.Forms.Padding(0, 5, 0, 0)
        Me.LayoutPanel.Size = New System.Drawing.Size(270, 142)
        Me.LayoutPanel.TabIndex = 5
        '
        'LinkLabel1
        '
        Me.LinkLabel1.AutoSize = True
        Me.LinkLabel1.LinkArea = New System.Windows.Forms.LinkArea(31, 5)
        Me.LinkLabel1.LinkBehavior = System.Windows.Forms.LinkBehavior.HoverUnderline
        Me.LinkLabel1.LinkColor = System.Drawing.SystemColors.HotTrack
        Me.LinkLabel1.Location = New System.Drawing.Point(3, 41)
        Me.LinkLabel1.Name = "LinkLabel1"
        Me.LinkLabel1.Padding = New System.Windows.Forms.Padding(0, 5, 0, 0)
        Me.LinkLabel1.Size = New System.Drawing.Size(201, 22)
        Me.LinkLabel1.TabIndex = 6
        Me.LinkLabel1.TabStop = True
        Me.LinkLabel1.Text = "Developed by Matthew Britton ('Gurch')"
        Me.LinkLabel1.UseCompatibleTextRendering = True
        Me.LinkLabel1.VisitedLinkColor = System.Drawing.SystemColors.HotTrack
        '
        'LinkLabel2
        '
        Me.LinkLabel2.AutoSize = True
        Me.LinkLabel2.LinkArea = New System.Windows.Forms.LinkArea(0, 0)
        Me.LinkLabel2.LinkBehavior = System.Windows.Forms.LinkBehavior.HoverUnderline
        Me.LinkLabel2.LinkColor = System.Drawing.SystemColors.HotTrack
        Me.LinkLabel2.Location = New System.Drawing.Point(3, 23)
        Me.LinkLabel2.Name = "LinkLabel2"
        Me.LinkLabel2.Padding = New System.Windows.Forms.Padding(0, 5, 0, 0)
        Me.LinkLabel2.Size = New System.Drawing.Size(229, 18)
        Me.LinkLabel2.TabIndex = 7
        Me.LinkLabel2.Text = "A query and monitoring tool for MediaWiki wikis"
        Me.LinkLabel2.VisitedLinkColor = System.Drawing.SystemColors.HotTrack
        '
        'LinkLabel3
        '
        Me.LinkLabel3.AutoSize = True
        Me.LinkLabel3.LinkArea = New System.Windows.Forms.LinkArea(124, 11)
        Me.LinkLabel3.LinkBehavior = System.Windows.Forms.LinkBehavior.HoverUnderline
        Me.LinkLabel3.LinkColor = System.Drawing.SystemColors.HotTrack
        Me.LinkLabel3.Location = New System.Drawing.Point(3, 63)
        Me.LinkLabel3.Name = "LinkLabel3"
        Me.LinkLabel3.Padding = New System.Windows.Forms.Padding(0, 5, 0, 0)
        Me.LinkLabel3.Size = New System.Drawing.Size(262, 47)
        Me.LinkLabel3.TabIndex = 8
        Me.LinkLabel3.TabStop = True
        Me.LinkLabel3.Text = "This application may be distributed freely and used for any purpose. Some incorpo" & _
            "rated media is subject to licensing terms; see details."
        Me.LinkLabel3.UseCompatibleTextRendering = True
        Me.LinkLabel3.VisitedLinkColor = System.Drawing.SystemColors.HotTrack
        '
        'AboutForm
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(270, 274)
        Me.Controls.Add(Me.LayoutPanel)
        Me.Controls.Add(Me.MediaWiki)
        Me.Controls.Add(Me.Logo)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "AboutForm"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "About Huggle"
        CType(Me.Logo, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MediaWiki, System.ComponentModel.ISupportInitialize).EndInit()
        Me.LayoutPanel.ResumeLayout(False)
        Me.LayoutPanel.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
    Private WithEvents Logo As System.Windows.Forms.PictureBox
    Private WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents MediaWiki As System.Windows.Forms.PictureBox
    Private WithEvents ManualLink As System.Windows.Forms.LinkLabel
    Friend WithEvents SourceLink As System.Windows.Forms.LinkLabel
    Friend WithEvents FeedbackLink As System.Windows.Forms.LinkLabel
    Private WithEvents LayoutPanel As System.Windows.Forms.FlowLayoutPanel
    Private WithEvents LinkLabel2 As System.Windows.Forms.LinkLabel
    Private WithEvents LinkLabel1 As System.Windows.Forms.LinkLabel
    Private WithEvents LinkLabel3 As System.Windows.Forms.LinkLabel
End Class
