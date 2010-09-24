<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class SpamListView
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
        Dim TypeColumn As System.Windows.Forms.ColumnHeader
        Me.TypeFilter = New System.Windows.Forms.ComboBox()
        Me.Entries = New System.Windows.Forms.ListViewEx()
        Me.EmptyNote = New System.Windows.Forms.Label()
        Me.GlobalListNote = New System.Windows.Forms.LinkLabel()
        Me.EditBlacklist = New System.Windows.Forms.LinkLabel()
        Me.Title = New System.Windows.Forms.Label()
        Me.Count = New System.Windows.Forms.Label()
        Me.ActionFilter = New System.Windows.Forms.ComboBox()
        PatternColumn = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        TypeColumn = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.SuspendLayout()
        '
        'PatternColumn
        '
        PatternColumn.Text = "Pattern"
        PatternColumn.Width = 329
        '
        'TypeColumn
        '
        TypeColumn.Text = "Type"
        TypeColumn.Width = 120
        '
        'TypeFilter
        '
        Me.TypeFilter.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.TypeFilter.Enabled = False
        Me.TypeFilter.FormattingEnabled = True
        Me.TypeFilter.Location = New System.Drawing.Point(3, 23)
        Me.TypeFilter.Name = "TypeFilter"
        Me.TypeFilter.Size = New System.Drawing.Size(122, 21)
        Me.TypeFilter.TabIndex = 1
        '
        'Entries
        '
        Me.Entries.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Entries.Columns.AddRange(New System.Windows.Forms.ColumnHeader() {PatternColumn, TypeColumn})
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
        Me.EditBlacklist.Location = New System.Drawing.Point(390, 26)
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
        Me.Title.Size = New System.Drawing.Size(96, 25)
        Me.Title.TabIndex = 23
        Me.Title.Text = "Spam lists"
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
        'ActionFilter
        '
        Me.ActionFilter.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.ActionFilter.Enabled = False
        Me.ActionFilter.FormattingEnabled = True
        Me.ActionFilter.Location = New System.Drawing.Point(131, 23)
        Me.ActionFilter.Name = "ActionFilter"
        Me.ActionFilter.Size = New System.Drawing.Size(122, 21)
        Me.ActionFilter.TabIndex = 25
        '
        'SpamListView
        '
        Me.Controls.Add(Me.ActionFilter)
        Me.Controls.Add(Me.Count)
        Me.Controls.Add(Me.Title)
        Me.Controls.Add(Me.EditBlacklist)
        Me.Controls.Add(Me.GlobalListNote)
        Me.Controls.Add(Me.EmptyNote)
        Me.Controls.Add(Me.Entries)
        Me.Controls.Add(Me.TypeFilter)
        Me.Name = "SpamListView"
        Me.Size = New System.Drawing.Size(478, 303)
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Private WithEvents TypeFilter As System.Windows.Forms.ComboBox
    Private WithEvents Entries As System.Windows.Forms.ListViewEx
    Private WithEvents EmptyNote As System.Windows.Forms.Label
    Private WithEvents GlobalListNote As System.Windows.Forms.LinkLabel
    Private WithEvents EditBlacklist As System.Windows.Forms.LinkLabel
    Private WithEvents Title As System.Windows.Forms.Label
    Private WithEvents Count As System.Windows.Forms.Label
    Private WithEvents ActionFilter As System.Windows.Forms.ComboBox

End Class
