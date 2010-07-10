<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class GlobalUserPropertiesForm
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
        Me.GlobalUserProps = New System.Windows.Forms.FlowLayoutPanel
        Me.Username = New System.Windows.Forms.LinkLabel
        Me.UserContributions = New System.Windows.Forms.Label
        Me.UserHome = New System.Windows.Forms.Label
        Me.UserId = New System.Windows.Forms.Label
        Me.UserCreated = New System.Windows.Forms.Label
        Me.UserStatus = New System.Windows.Forms.Label
        Me.UserGroups = New System.Windows.Forms.Label
        Me.WikiCount = New System.Windows.Forms.Label
        Me.SetGlobalPreferences = New System.Windows.Forms.LinkLabel
        Me.AccountImage = New System.Windows.Forms.PictureBox
        Me.WikiList = New System.Windows.Forms.ListViewEx
        Me.WikiColumn = New System.Windows.Forms.ColumnHeader
        Me.MethodColumn = New System.Windows.Forms.ColumnHeader
        Me.CreatedColumn = New System.Windows.Forms.ColumnHeader
        Me.ContributionsColumn = New System.Windows.Forms.ColumnHeader
        Me.GlobalUserProps.SuspendLayout()
        CType(Me.AccountImage, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'GlobalUserProps
        '
        Me.GlobalUserProps.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.GlobalUserProps.Controls.Add(Me.Username)
        Me.GlobalUserProps.Controls.Add(Me.UserContributions)
        Me.GlobalUserProps.Controls.Add(Me.UserHome)
        Me.GlobalUserProps.Controls.Add(Me.UserId)
        Me.GlobalUserProps.Controls.Add(Me.UserCreated)
        Me.GlobalUserProps.Controls.Add(Me.UserStatus)
        Me.GlobalUserProps.Controls.Add(Me.UserGroups)
        Me.GlobalUserProps.Controls.Add(Me.WikiCount)
        Me.GlobalUserProps.Controls.Add(Me.SetGlobalPreferences)
        Me.GlobalUserProps.FlowDirection = System.Windows.Forms.FlowDirection.TopDown
        Me.GlobalUserProps.Location = New System.Drawing.Point(146, 12)
        Me.GlobalUserProps.Name = "GlobalUserProps"
        Me.GlobalUserProps.Size = New System.Drawing.Size(306, 146)
        Me.GlobalUserProps.TabIndex = 3
        '
        'Username
        '
        Me.Username.AutoSize = True
        Me.Username.BackColor = System.Drawing.Color.Transparent
        Me.Username.Font = New System.Drawing.Font("Tahoma", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Username.LinkBehavior = System.Windows.Forms.LinkBehavior.HoverUnderline
        Me.Username.LinkColor = System.Drawing.SystemColors.HotTrack
        Me.Username.Location = New System.Drawing.Point(3, 0)
        Me.Username.Name = "Username"
        Me.Username.Padding = New System.Windows.Forms.Padding(0, 1, 0, 1)
        Me.Username.Size = New System.Drawing.Size(75, 20)
        Me.Username.TabIndex = 0
        Me.Username.TabStop = True
        Me.Username.Text = "Username"
        '
        'UserContributions
        '
        Me.UserContributions.AutoSize = True
        Me.UserContributions.Location = New System.Drawing.Point(3, 20)
        Me.UserContributions.Name = "UserContributions"
        Me.UserContributions.Padding = New System.Windows.Forms.Padding(0, 1, 0, 1)
        Me.UserContributions.Size = New System.Drawing.Size(80, 15)
        Me.UserContributions.TabIndex = 1
        Me.UserContributions.Text = "Contributions: 0"
        '
        'UserHome
        '
        Me.UserHome.AutoSize = True
        Me.UserHome.Location = New System.Drawing.Point(3, 35)
        Me.UserHome.Name = "UserHome"
        Me.UserHome.Padding = New System.Windows.Forms.Padding(0, 1, 0, 1)
        Me.UserHome.Size = New System.Drawing.Size(59, 15)
        Me.UserHome.TabIndex = 2
        Me.UserHome.Text = "Home wiki:"
        '
        'UserId
        '
        Me.UserId.AutoSize = True
        Me.UserId.Location = New System.Drawing.Point(3, 50)
        Me.UserId.Name = "UserId"
        Me.UserId.Padding = New System.Windows.Forms.Padding(0, 1, 0, 1)
        Me.UserId.Size = New System.Drawing.Size(64, 15)
        Me.UserId.TabIndex = 4
        Me.UserId.Text = "Account ID:"
        '
        'UserCreated
        '
        Me.UserCreated.AutoSize = True
        Me.UserCreated.Location = New System.Drawing.Point(3, 65)
        Me.UserCreated.Name = "UserCreated"
        Me.UserCreated.Padding = New System.Windows.Forms.Padding(0, 1, 0, 1)
        Me.UserCreated.Size = New System.Drawing.Size(89, 15)
        Me.UserCreated.TabIndex = 3
        Me.UserCreated.Text = "Account created:"
        '
        'UserStatus
        '
        Me.UserStatus.AutoSize = True
        Me.UserStatus.Location = New System.Drawing.Point(3, 80)
        Me.UserStatus.Name = "UserStatus"
        Me.UserStatus.Padding = New System.Windows.Forms.Padding(0, 1, 0, 1)
        Me.UserStatus.Size = New System.Drawing.Size(81, 15)
        Me.UserStatus.TabIndex = 5
        Me.UserStatus.Text = "Account status:"
        '
        'UserGroups
        '
        Me.UserGroups.AutoSize = True
        Me.UserGroups.Location = New System.Drawing.Point(3, 95)
        Me.UserGroups.Name = "UserGroups"
        Me.UserGroups.Padding = New System.Windows.Forms.Padding(0, 1, 0, 1)
        Me.UserGroups.Size = New System.Drawing.Size(75, 15)
        Me.UserGroups.TabIndex = 6
        Me.UserGroups.Text = "Global groups:"
        '
        'WikiCount
        '
        Me.WikiCount.AutoSize = True
        Me.WikiCount.Location = New System.Drawing.Point(3, 110)
        Me.WikiCount.Name = "WikiCount"
        Me.WikiCount.Padding = New System.Windows.Forms.Padding(0, 1, 0, 1)
        Me.WikiCount.Size = New System.Drawing.Size(130, 15)
        Me.WikiCount.TabIndex = 1
        Me.WikiCount.Text = "Local accounts on 0 wikis"
        '
        'SetGlobalPreferences
        '
        Me.SetGlobalPreferences.AutoSize = True
        Me.SetGlobalPreferences.LinkBehavior = System.Windows.Forms.LinkBehavior.HoverUnderline
        Me.SetGlobalPreferences.LinkColor = System.Drawing.SystemColors.HotTrack
        Me.SetGlobalPreferences.Location = New System.Drawing.Point(3, 125)
        Me.SetGlobalPreferences.Name = "SetGlobalPreferences"
        Me.SetGlobalPreferences.Padding = New System.Windows.Forms.Padding(0, 1, 0, 1)
        Me.SetGlobalPreferences.Size = New System.Drawing.Size(113, 15)
        Me.SetGlobalPreferences.TabIndex = 7
        Me.SetGlobalPreferences.TabStop = True
        Me.SetGlobalPreferences.Text = "Set global preferences"
        Me.SetGlobalPreferences.VisitedLinkColor = System.Drawing.SystemColors.HotTrack
        '
        'AccountImage
        '
        Me.AccountImage.Image = Global.Resources.globaluser_icon
        Me.AccountImage.Location = New System.Drawing.Point(12, 12)
        Me.AccountImage.Name = "AccountImage"
        Me.AccountImage.Size = New System.Drawing.Size(128, 128)
        Me.AccountImage.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize
        Me.AccountImage.TabIndex = 1
        Me.AccountImage.TabStop = False
        '
        'WikiList
        '
        Me.WikiList.Activation = System.Windows.Forms.ItemActivation.OneClick
        Me.WikiList.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.WikiList.Columns.AddRange(New System.Windows.Forms.ColumnHeader() {Me.WikiColumn, Me.MethodColumn, Me.CreatedColumn, Me.ContributionsColumn})
        Me.WikiList.FlexibleColumn = 0
        Me.WikiList.FullRowSelect = True
        Me.WikiList.GridLines = True
        Me.WikiList.Location = New System.Drawing.Point(12, 164)
        Me.WikiList.Name = "WikiList"
        Me.WikiList.ShowGroups = False
        Me.WikiList.Size = New System.Drawing.Size(440, 186)
        Me.WikiList.SortOnColumnClick = True
        Me.WikiList.TabIndex = 1
        Me.WikiList.UseCompatibleStateImageBehavior = False
        Me.WikiList.View = System.Windows.Forms.View.Details
        '
        'WikiColumn
        '
        Me.WikiColumn.Text = "Wiki"
        Me.WikiColumn.Width = 100
        '
        'MethodColumn
        '
        Me.MethodColumn.Text = "Method"
        '
        'CreatedColumn
        '
        Me.CreatedColumn.Text = "Created"
        Me.CreatedColumn.Width = 137
        '
        'ContributionsColumn
        '
        Me.ContributionsColumn.Text = "Contribs"
        Me.ContributionsColumn.Width = 53
        '
        'GlobalUserPropertiesForm
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(464, 362)
        Me.Controls.Add(Me.GlobalUserProps)
        Me.Controls.Add(Me.WikiList)
        Me.Controls.Add(Me.AccountImage)
        Me.Name = "GlobalUserPropertiesForm"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Global account properties"
        Me.GlobalUserProps.ResumeLayout(False)
        Me.GlobalUserProps.PerformLayout()
        CType(Me.AccountImage, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Private WithEvents Username As System.Windows.Forms.LinkLabel
    Private WithEvents GlobalUserProps As System.Windows.Forms.FlowLayoutPanel
    Private WithEvents WikiCount As System.Windows.Forms.Label
    Private WithEvents AccountImage As System.Windows.Forms.PictureBox
    Private WithEvents UserContributions As System.Windows.Forms.Label
    Private WithEvents UserHome As System.Windows.Forms.Label
    Private WithEvents UserId As System.Windows.Forms.Label
    Private WithEvents UserCreated As System.Windows.Forms.Label
    Private WithEvents UserStatus As System.Windows.Forms.Label
    Private WithEvents WikiList As System.Windows.Forms.ListViewEx
    Private WithEvents ContributionsColumn As System.Windows.Forms.ColumnHeader
    Private WithEvents UserGroups As System.Windows.Forms.Label
    Private WithEvents SetGlobalPreferences As System.Windows.Forms.LinkLabel
    Private WithEvents WikiColumn As System.Windows.Forms.ColumnHeader
    Private WithEvents MethodColumn As System.Windows.Forms.ColumnHeader
    Private WithEvents CreatedColumn As System.Windows.Forms.ColumnHeader
End Class
