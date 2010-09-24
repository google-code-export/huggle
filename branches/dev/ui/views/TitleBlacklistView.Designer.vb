<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class TitleBlacklistView
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
        Dim PatternColumn As System.Windows.Forms.ColumnHeader
        Dim OptionsColumn As System.Windows.Forms.ColumnHeader
        Me.FilterLabel = New System.Windows.Forms.Label()
        Me.Filter = New System.Windows.Forms.ComboBox()
        Me.Entries = New System.Windows.Forms.ListViewEx()
        Me.MessageColumn = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.EmptyNote = New System.Windows.Forms.Label()
        Me.GlobalListNote = New System.Windows.Forms.LinkLabel()
        Me.EditBlacklist = New System.Windows.Forms.LinkLabel()
        Me.Title = New System.Windows.Forms.Label()
        Me.Count = New System.Windows.Forms.Label()
        PatternColumn = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        OptionsColumn = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.SuspendLayout()
        '
        'PatternColumn
        '
        PatternColumn.Text = "Pattern"
        PatternColumn.Width = 173
        '
        'OptionsColumn
        '
        OptionsColumn.Text = "Options"
        OptionsColumn.Width = 169
        '
        'FilterLabel
        '
        Me.FilterLabel.AutoSize = True
        Me.FilterLabel.Location = New System.Drawing.Point(0, 26)
        Me.FilterLabel.Name = "FilterLabel"
        Me.FilterLabel.Size = New System.Drawing.Size(134, 13)
        Me.FilterLabel.TabIndex = 0
        Me.FilterLabel.Text = "Show entries applicable to:"
        '
        'Filter
        '
        Me.Filter.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.Filter.Enabled = False
        Me.Filter.FormattingEnabled = True
        Me.Filter.Location = New System.Drawing.Point(134, 23)
        Me.Filter.Name = "Filter"
        Me.Filter.Size = New System.Drawing.Size(169, 21)
        Me.Filter.TabIndex = 1
        '
        'Entries
        '
        Me.Entries.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Entries.Columns.AddRange(New System.Windows.Forms.ColumnHeader() {PatternColumn, OptionsColumn, Me.MessageColumn})
        Me.Entries.FlexibleColumn = 0
        Me.Entries.FullRowSelect = True
        Me.Entries.GridLines = True
        Me.Entries.Location = New System.Drawing.Point(3, 50)
        Me.Entries.Name = "Entries"
        Me.Entries.ShowGroups = False
        Me.Entries.Size = New System.Drawing.Size(472, 250)
        Me.Entries.TabIndex = 2
        Me.Entries.UseCompatibleStateImageBehavior = False
        Me.Entries.View = System.Windows.Forms.View.Details
        '
        'MessageColumn
        '
        Me.MessageColumn.Text = "Message"
        Me.MessageColumn.Width = 107
        '
        'EmptyNote
        '
        Me.EmptyNote.Anchor = CType((System.Windows.Forms.AnchorStyles.Left Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.EmptyNote.BackColor = System.Drawing.SystemColors.Window
        Me.EmptyNote.ForeColor = System.Drawing.SystemColors.ControlDarkDark
        Me.EmptyNote.Location = New System.Drawing.Point(6, 121)
        Me.EmptyNote.Name = "EmptyNote"
        Me.EmptyNote.Size = New System.Drawing.Size(466, 16)
        Me.EmptyNote.TabIndex = 3
        Me.EmptyNote.Text = "No entries defined in local blacklist."
        Me.EmptyNote.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.EmptyNote.Visible = False
        '
        'GlobalListNote
        '
        Me.GlobalListNote.Anchor = CType((System.Windows.Forms.AnchorStyles.Left Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.GlobalListNote.BackColor = System.Drawing.SystemColors.Window
        Me.GlobalListNote.LinkArea = New System.Windows.Forms.LinkArea(0, 43)
        Me.GlobalListNote.LinkBehavior = System.Windows.Forms.LinkBehavior.HoverUnderline
        Me.GlobalListNote.LinkColor = System.Drawing.SystemColors.HotTrack
        Me.GlobalListNote.Location = New System.Drawing.Point(6, 137)
        Me.GlobalListNote.Name = "GlobalListNote"
        Me.GlobalListNote.Size = New System.Drawing.Size(466, 18)
        Me.GlobalListNote.TabIndex = 4
        Me.GlobalListNote.TabStop = True
        Me.GlobalListNote.Text = "The global blacklist for {0} still applies."
        Me.GlobalListNote.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.GlobalListNote.Visible = False
        '
        'EditBlacklist
        '
        Me.EditBlacklist.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.EditBlacklist.LinkBehavior = System.Windows.Forms.LinkBehavior.HoverUnderline
        Me.EditBlacklist.LinkColor = System.Drawing.SystemColors.HotTrack
        Me.EditBlacklist.Location = New System.Drawing.Point(393, 26)
        Me.EditBlacklist.Name = "EditBlacklist"
        Me.EditBlacklist.Size = New System.Drawing.Size(85, 13)
        Me.EditBlacklist.TabIndex = 5
        Me.EditBlacklist.TabStop = True
        Me.EditBlacklist.Text = "Edit blacklist"
        Me.EditBlacklist.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Title
        '
        Me.Title.AutoSize = True
        Me.Title.Font = New System.Drawing.Font("Segoe UI", 14.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Title.Location = New System.Drawing.Point(-2, -3)
        Me.Title.Name = "Title"
        Me.Title.Size = New System.Drawing.Size(80, 25)
        Me.Title.TabIndex = 23
        Me.Title.Text = "Blacklist"
        '
        'Count
        '
        Me.Count.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Count.Location = New System.Drawing.Point(415, 6)
        Me.Count.Name = "Count"
        Me.Count.Size = New System.Drawing.Size(63, 14)
        Me.Count.TabIndex = 24
        Me.Count.Text = "0 items"
        Me.Count.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'TitleBlacklistView
        '
        Me.Controls.Add(Me.Count)
        Me.Controls.Add(Me.Title)
        Me.Controls.Add(Me.EditBlacklist)
        Me.Controls.Add(Me.GlobalListNote)
        Me.Controls.Add(Me.EmptyNote)
        Me.Controls.Add(Me.Entries)
        Me.Controls.Add(Me.Filter)
        Me.Controls.Add(Me.FilterLabel)
        Me.Name = "TitleBlacklistView"
        Me.Size = New System.Drawing.Size(478, 303)
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Private WithEvents FilterLabel As System.Windows.Forms.Label
    Private WithEvents Filter As System.Windows.Forms.ComboBox
    Private WithEvents Entries As System.Windows.Forms.ListViewEx
    Private WithEvents EmptyNote As System.Windows.Forms.Label
    Private WithEvents GlobalListNote As System.Windows.Forms.LinkLabel
    Private WithEvents MessageColumn As System.Windows.Forms.ColumnHeader
    Private WithEvents EditBlacklist As System.Windows.Forms.LinkLabel
    Private WithEvents Title As System.Windows.Forms.Label
    Private WithEvents Count As System.Windows.Forms.Label

End Class
