<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class UserGroupsForm
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
        Me.AddGroup = New System.Windows.Forms.Button()
        Me.RemoveGroup = New System.Windows.Forms.Button()
        Me.AvailableGroupsLabel = New System.Windows.Forms.Label()
        Me.SelectedGroupsLabel = New System.Windows.Forms.Label()
        Me.AvailableGroups = New System.Windows.Forms.ListBox()
        Me.SelectedGroups = New System.Windows.Forms.ListBox()
        Me.OK = New System.Windows.Forms.Button()
        Me.Cancel = New System.Windows.Forms.Button()
        Me.AvailableEmptyLabel = New System.Windows.Forms.Label()
        Me.SelectedEmptyLabel = New System.Windows.Forms.Label()
        Me.CommentLabel = New System.Windows.Forms.Label()
        Me.Comment = New System.Windows.Forms.TextBox()
        Me.SuspendLayout()
        '
        'AddGroup
        '
        Me.AddGroup.Anchor = System.Windows.Forms.AnchorStyles.Top
        Me.AddGroup.Enabled = False
        Me.AddGroup.Location = New System.Drawing.Point(189, 67)
        Me.AddGroup.Name = "AddGroup"
        Me.AddGroup.Size = New System.Drawing.Size(75, 23)
        Me.AddGroup.TabIndex = 3
        Me.AddGroup.Text = "Add >>"
        Me.AddGroup.UseVisualStyleBackColor = True
        '
        'RemoveGroup
        '
        Me.RemoveGroup.Anchor = System.Windows.Forms.AnchorStyles.Top
        Me.RemoveGroup.Enabled = False
        Me.RemoveGroup.Location = New System.Drawing.Point(189, 96)
        Me.RemoveGroup.Name = "RemoveGroup"
        Me.RemoveGroup.Size = New System.Drawing.Size(75, 23)
        Me.RemoveGroup.TabIndex = 4
        Me.RemoveGroup.Text = "<< Remove"
        Me.RemoveGroup.UseVisualStyleBackColor = True
        '
        'AvailableGroupsLabel
        '
        Me.AvailableGroupsLabel.AutoSize = True
        Me.AvailableGroupsLabel.Location = New System.Drawing.Point(10, 12)
        Me.AvailableGroupsLabel.Name = "AvailableGroupsLabel"
        Me.AvailableGroupsLabel.Size = New System.Drawing.Size(88, 13)
        Me.AvailableGroupsLabel.TabIndex = 0
        Me.AvailableGroupsLabel.Text = "Available groups:"
        '
        'SelectedGroupsLabel
        '
        Me.SelectedGroupsLabel.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.SelectedGroupsLabel.AutoSize = True
        Me.SelectedGroupsLabel.Location = New System.Drawing.Point(267, 12)
        Me.SelectedGroupsLabel.Name = "SelectedGroupsLabel"
        Me.SelectedGroupsLabel.Size = New System.Drawing.Size(87, 13)
        Me.SelectedGroupsLabel.TabIndex = 5
        Me.SelectedGroupsLabel.Text = "Selected groups:"
        '
        'AvailableGroups
        '
        Me.AvailableGroups.FormattingEnabled = True
        Me.AvailableGroups.Location = New System.Drawing.Point(13, 28)
        Me.AvailableGroups.Name = "AvailableGroups"
        Me.AvailableGroups.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended
        Me.AvailableGroups.Size = New System.Drawing.Size(170, 134)
        Me.AvailableGroups.TabIndex = 1
        '
        'SelectedGroups
        '
        Me.SelectedGroups.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.SelectedGroups.FormattingEnabled = True
        Me.SelectedGroups.Location = New System.Drawing.Point(270, 28)
        Me.SelectedGroups.Name = "SelectedGroups"
        Me.SelectedGroups.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended
        Me.SelectedGroups.Size = New System.Drawing.Size(170, 134)
        Me.SelectedGroups.TabIndex = 6
        '
        'OK
        '
        Me.OK.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.OK.DialogResult = System.Windows.Forms.DialogResult.OK
        Me.OK.Location = New System.Drawing.Point(285, 205)
        Me.OK.Name = "OK"
        Me.OK.Size = New System.Drawing.Size(75, 23)
        Me.OK.TabIndex = 10
        Me.OK.Text = "OK"
        Me.OK.UseVisualStyleBackColor = True
        '
        'Cancel
        '
        Me.Cancel.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Cancel.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.Cancel.Location = New System.Drawing.Point(366, 205)
        Me.Cancel.Name = "Cancel"
        Me.Cancel.Size = New System.Drawing.Size(75, 23)
        Me.Cancel.TabIndex = 11
        Me.Cancel.Text = "Cancel"
        Me.Cancel.UseVisualStyleBackColor = True
        '
        'AvailableEmptyLabel
        '
        Me.AvailableEmptyLabel.BackColor = System.Drawing.SystemColors.Window
        Me.AvailableEmptyLabel.ForeColor = System.Drawing.SystemColors.GrayText
        Me.AvailableEmptyLabel.Location = New System.Drawing.Point(15, 65)
        Me.AvailableEmptyLabel.Name = "AvailableEmptyLabel"
        Me.AvailableEmptyLabel.Size = New System.Drawing.Size(166, 60)
        Me.AvailableEmptyLabel.TabIndex = 2
        Me.AvailableEmptyLabel.Text = "There are no groups you can add that this user is not already in"
        Me.AvailableEmptyLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.AvailableEmptyLabel.Visible = False
        '
        'SelectedEmptyLabel
        '
        Me.SelectedEmptyLabel.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.SelectedEmptyLabel.BackColor = System.Drawing.SystemColors.Window
        Me.SelectedEmptyLabel.ForeColor = System.Drawing.SystemColors.GrayText
        Me.SelectedEmptyLabel.Location = New System.Drawing.Point(282, 48)
        Me.SelectedEmptyLabel.Name = "SelectedEmptyLabel"
        Me.SelectedEmptyLabel.Size = New System.Drawing.Size(146, 94)
        Me.SelectedEmptyLabel.TabIndex = 7
        Me.SelectedEmptyLabel.Text = "This user is not in any groups" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "that you can remove"
        Me.SelectedEmptyLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.SelectedEmptyLabel.Visible = False
        '
        'CommentLabel
        '
        Me.CommentLabel.AutoSize = True
        Me.CommentLabel.Location = New System.Drawing.Point(12, 179)
        Me.CommentLabel.Name = "CommentLabel"
        Me.CommentLabel.Size = New System.Drawing.Size(54, 13)
        Me.CommentLabel.TabIndex = 8
        Me.CommentLabel.Text = "Comment:"
        '
        'Comment
        '
        Me.Comment.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Comment.Location = New System.Drawing.Point(67, 176)
        Me.Comment.Name = "Comment"
        Me.Comment.Size = New System.Drawing.Size(373, 20)
        Me.Comment.TabIndex = 9
        '
        'UserGroupsForm
        '
        Me.AcceptButton = Me.OK
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.CancelButton = Me.Cancel
        Me.ClientSize = New System.Drawing.Size(453, 240)
        Me.Controls.Add(Me.Comment)
        Me.Controls.Add(Me.CommentLabel)
        Me.Controls.Add(Me.SelectedEmptyLabel)
        Me.Controls.Add(Me.AvailableEmptyLabel)
        Me.Controls.Add(Me.Cancel)
        Me.Controls.Add(Me.OK)
        Me.Controls.Add(Me.SelectedGroups)
        Me.Controls.Add(Me.AvailableGroups)
        Me.Controls.Add(Me.SelectedGroupsLabel)
        Me.Controls.Add(Me.AvailableGroupsLabel)
        Me.Controls.Add(Me.RemoveGroup)
        Me.Controls.Add(Me.AddGroup)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.Name = "UserGroupsForm"
        Me.Text = "User groups for {0}"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Private WithEvents AddGroup As System.Windows.Forms.Button
    Private WithEvents RemoveGroup As System.Windows.Forms.Button
    Private WithEvents AvailableGroupsLabel As System.Windows.Forms.Label
    Private WithEvents SelectedGroupsLabel As System.Windows.Forms.Label
    Private WithEvents OK As System.Windows.Forms.Button
    Private WithEvents Cancel As System.Windows.Forms.Button
    Private WithEvents AvailableEmptyLabel As System.Windows.Forms.Label
    Private WithEvents SelectedEmptyLabel As System.Windows.Forms.Label
    Private WithEvents AvailableGroups As System.Windows.Forms.ListBox
    Private WithEvents SelectedGroups As System.Windows.Forms.ListBox
    Private WithEvents CommentLabel As System.Windows.Forms.Label
    Private WithEvents Comment As System.Windows.Forms.TextBox
End Class
