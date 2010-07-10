<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class AccountPropertiesForm
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
        Me.Tabs = New System.Windows.Forms.TabControl()
        Me.GeneralTab = New System.Windows.Forms.TabPage()
        Me.AccountProps = New System.Windows.Forms.FlowLayoutPanel()
        Me.AccountName = New System.Windows.Forms.LinkLabel()
        Me.AccountWiki = New System.Windows.Forms.Label()
        Me.AccountGlobal = New System.Windows.Forms.Label()
        Me.AccountID = New System.Windows.Forms.Label()
        Me.AccountCreated = New System.Windows.Forms.Label()
        Me.AccountGender = New System.Windows.Forms.Label()
        Me.AccountEmail = New System.Windows.Forms.Label()
        Me.AccountEmailAuthenticated = New System.Windows.Forms.Label()
        Me.AccountContributions = New System.Windows.Forms.Label()
        Me.SetPreferences = New System.Windows.Forms.LinkLabel()
        Me.AccountImage = New System.Windows.Forms.PictureBox()
        Me.ChangeGroups = New System.Windows.Forms.LinkLabel()
        Me.Tabs.SuspendLayout()
        Me.GeneralTab.SuspendLayout()
        Me.AccountProps.SuspendLayout()
        CType(Me.AccountImage, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'Tabs
        '
        Me.Tabs.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Tabs.Controls.Add(Me.GeneralTab)
        Me.Tabs.Location = New System.Drawing.Point(12, 12)
        Me.Tabs.Name = "Tabs"
        Me.Tabs.SelectedIndex = 0
        Me.Tabs.Size = New System.Drawing.Size(445, 300)
        Me.Tabs.TabIndex = 0
        '
        'GeneralTab
        '
        Me.GeneralTab.Controls.Add(Me.AccountProps)
        Me.GeneralTab.Controls.Add(Me.AccountImage)
        Me.GeneralTab.Location = New System.Drawing.Point(4, 22)
        Me.GeneralTab.Name = "GeneralTab"
        Me.GeneralTab.Padding = New System.Windows.Forms.Padding(3)
        Me.GeneralTab.Size = New System.Drawing.Size(437, 274)
        Me.GeneralTab.TabIndex = 0
        Me.GeneralTab.Text = "General"
        Me.GeneralTab.UseVisualStyleBackColor = True
        '
        'AccountProps
        '
        Me.AccountProps.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.AccountProps.Controls.Add(Me.AccountName)
        Me.AccountProps.Controls.Add(Me.AccountWiki)
        Me.AccountProps.Controls.Add(Me.AccountGlobal)
        Me.AccountProps.Controls.Add(Me.AccountID)
        Me.AccountProps.Controls.Add(Me.AccountCreated)
        Me.AccountProps.Controls.Add(Me.AccountGender)
        Me.AccountProps.Controls.Add(Me.AccountEmail)
        Me.AccountProps.Controls.Add(Me.AccountEmailAuthenticated)
        Me.AccountProps.Controls.Add(Me.AccountContributions)
        Me.AccountProps.Controls.Add(Me.SetPreferences)
        Me.AccountProps.Controls.Add(Me.ChangeGroups)
        Me.AccountProps.FlowDirection = System.Windows.Forms.FlowDirection.TopDown
        Me.AccountProps.Location = New System.Drawing.Point(134, 6)
        Me.AccountProps.Name = "AccountProps"
        Me.AccountProps.Size = New System.Drawing.Size(297, 265)
        Me.AccountProps.TabIndex = 3
        '
        'AccountName
        '
        Me.AccountName.AutoSize = True
        Me.AccountName.Font = New System.Drawing.Font("Tahoma", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.AccountName.LinkBehavior = System.Windows.Forms.LinkBehavior.HoverUnderline
        Me.AccountName.LinkColor = System.Drawing.SystemColors.HotTrack
        Me.AccountName.Location = New System.Drawing.Point(3, 0)
        Me.AccountName.Name = "AccountName"
        Me.AccountName.Padding = New System.Windows.Forms.Padding(0, 3, 0, 0)
        Me.AccountName.Size = New System.Drawing.Size(75, 21)
        Me.AccountName.TabIndex = 0
        Me.AccountName.TabStop = True
        Me.AccountName.Text = "Username"
        '
        'AccountWiki
        '
        Me.AccountWiki.AutoSize = True
        Me.AccountWiki.Location = New System.Drawing.Point(3, 21)
        Me.AccountWiki.Name = "AccountWiki"
        Me.AccountWiki.Padding = New System.Windows.Forms.Padding(0, 4, 0, 0)
        Me.AccountWiki.Size = New System.Drawing.Size(31, 17)
        Me.AccountWiki.TabIndex = 6
        Me.AccountWiki.Text = "Wiki:"
        '
        'AccountGlobal
        '
        Me.AccountGlobal.AutoSize = True
        Me.AccountGlobal.Location = New System.Drawing.Point(3, 38)
        Me.AccountGlobal.Name = "AccountGlobal"
        Me.AccountGlobal.Padding = New System.Windows.Forms.Padding(0, 4, 0, 0)
        Me.AccountGlobal.Size = New System.Drawing.Size(82, 17)
        Me.AccountGlobal.TabIndex = 5
        Me.AccountGlobal.Text = "Global account:"
        '
        'AccountID
        '
        Me.AccountID.AutoSize = True
        Me.AccountID.Location = New System.Drawing.Point(3, 55)
        Me.AccountID.Name = "AccountID"
        Me.AccountID.Padding = New System.Windows.Forms.Padding(0, 4, 0, 0)
        Me.AccountID.Size = New System.Drawing.Size(46, 17)
        Me.AccountID.TabIndex = 2
        Me.AccountID.Text = "User ID:"
        '
        'AccountCreated
        '
        Me.AccountCreated.AutoSize = True
        Me.AccountCreated.Location = New System.Drawing.Point(3, 72)
        Me.AccountCreated.Name = "AccountCreated"
        Me.AccountCreated.Padding = New System.Windows.Forms.Padding(0, 4, 0, 0)
        Me.AccountCreated.Size = New System.Drawing.Size(61, 17)
        Me.AccountCreated.TabIndex = 2
        Me.AccountCreated.Text = "Registered:"
        '
        'AccountGender
        '
        Me.AccountGender.AutoSize = True
        Me.AccountGender.Location = New System.Drawing.Point(3, 89)
        Me.AccountGender.Name = "AccountGender"
        Me.AccountGender.Padding = New System.Windows.Forms.Padding(0, 4, 0, 0)
        Me.AccountGender.Size = New System.Drawing.Size(45, 17)
        Me.AccountGender.TabIndex = 3
        Me.AccountGender.Text = "Gender:"
        '
        'AccountEmail
        '
        Me.AccountEmail.AutoSize = True
        Me.AccountEmail.Location = New System.Drawing.Point(3, 106)
        Me.AccountEmail.Name = "AccountEmail"
        Me.AccountEmail.Padding = New System.Windows.Forms.Padding(0, 4, 0, 0)
        Me.AccountEmail.Size = New System.Drawing.Size(78, 17)
        Me.AccountEmail.TabIndex = 4
        Me.AccountEmail.Text = "E-mail address:"
        '
        'AccountEmailAuthenticated
        '
        Me.AccountEmailAuthenticated.AutoSize = True
        Me.AccountEmailAuthenticated.Location = New System.Drawing.Point(3, 123)
        Me.AccountEmailAuthenticated.Name = "AccountEmailAuthenticated"
        Me.AccountEmailAuthenticated.Padding = New System.Windows.Forms.Padding(0, 4, 0, 0)
        Me.AccountEmailAuthenticated.Size = New System.Drawing.Size(122, 17)
        Me.AccountEmailAuthenticated.TabIndex = 8
        Me.AccountEmailAuthenticated.Text = "E-mail confirmation date:"
        Me.AccountEmailAuthenticated.Visible = False
        '
        'AccountContributions
        '
        Me.AccountContributions.AutoSize = True
        Me.AccountContributions.Location = New System.Drawing.Point(3, 140)
        Me.AccountContributions.Name = "AccountContributions"
        Me.AccountContributions.Padding = New System.Windows.Forms.Padding(0, 4, 0, 0)
        Me.AccountContributions.Size = New System.Drawing.Size(71, 17)
        Me.AccountContributions.TabIndex = 7
        Me.AccountContributions.Text = "Contributions:"
        '
        'SetPreferences
        '
        Me.SetPreferences.AutoSize = True
        Me.SetPreferences.LinkBehavior = System.Windows.Forms.LinkBehavior.HoverUnderline
        Me.SetPreferences.LinkColor = System.Drawing.SystemColors.HotTrack
        Me.SetPreferences.Location = New System.Drawing.Point(3, 157)
        Me.SetPreferences.Name = "SetPreferences"
        Me.SetPreferences.Padding = New System.Windows.Forms.Padding(0, 12, 0, 0)
        Me.SetPreferences.Size = New System.Drawing.Size(82, 25)
        Me.SetPreferences.TabIndex = 4
        Me.SetPreferences.TabStop = True
        Me.SetPreferences.Text = "Set preferences"
        '
        'AccountImage
        '
        Me.AccountImage.Image = Global.Resources.user_icon
        Me.AccountImage.Location = New System.Drawing.Point(6, 6)
        Me.AccountImage.Name = "AccountImage"
        Me.AccountImage.Size = New System.Drawing.Size(122, 176)
        Me.AccountImage.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage
        Me.AccountImage.TabIndex = 1
        Me.AccountImage.TabStop = False
        '
        'ChangeGroups
        '
        Me.ChangeGroups.AutoSize = True
        Me.ChangeGroups.LinkBehavior = System.Windows.Forms.LinkBehavior.HoverUnderline
        Me.ChangeGroups.LinkColor = System.Drawing.SystemColors.HotTrack
        Me.ChangeGroups.Location = New System.Drawing.Point(3, 182)
        Me.ChangeGroups.Name = "ChangeGroups"
        Me.ChangeGroups.Padding = New System.Windows.Forms.Padding(0, 4, 0, 0)
        Me.ChangeGroups.Size = New System.Drawing.Size(79, 17)
        Me.ChangeGroups.TabIndex = 9
        Me.ChangeGroups.TabStop = True
        Me.ChangeGroups.Text = "Change groups"
        '
        'AccountPropertiesForm
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(469, 324)
        Me.Controls.Add(Me.Tabs)
        Me.Name = "AccountPropertiesForm"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "AccountPropertiesForm"
        Me.Tabs.ResumeLayout(False)
        Me.GeneralTab.ResumeLayout(False)
        Me.AccountProps.ResumeLayout(False)
        Me.AccountProps.PerformLayout()
        CType(Me.AccountImage, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Private WithEvents Tabs As System.Windows.Forms.TabControl
    Private WithEvents GeneralTab As System.Windows.Forms.TabPage
    Private WithEvents AccountName As System.Windows.Forms.LinkLabel
    Private WithEvents AccountProps As System.Windows.Forms.FlowLayoutPanel
    Private WithEvents AccountID As System.Windows.Forms.Label
    Private WithEvents AccountEmail As System.Windows.Forms.Label
    Private WithEvents AccountGlobal As System.Windows.Forms.Label
    Private WithEvents SetPreferences As System.Windows.Forms.LinkLabel
    Private WithEvents AccountWiki As System.Windows.Forms.Label
    Private WithEvents AccountImage As System.Windows.Forms.PictureBox
    Private WithEvents AccountContributions As System.Windows.Forms.Label
    Private WithEvents AccountCreated As System.Windows.Forms.Label
    Private WithEvents AccountGender As System.Windows.Forms.Label
    Private WithEvents AccountEmailAuthenticated As System.Windows.Forms.Label
    Private WithEvents ChangeGroups As System.Windows.Forms.LinkLabel
End Class
