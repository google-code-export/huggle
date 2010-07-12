<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class QueuePanel
    Inherits System.Windows.Forms.UserControl

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
        Me.Queues = New System.Windows.Forms.ComboBox
        Me.Scrollbar = New System.Windows.Forms.VScrollBar
        Me.Count = New System.Windows.Forms.Label
        Me.QueueStrip = New System.Windows.Forms.ToolStrip
        Me.DiffMode = New System.Windows.Forms.ToolStripButton
        Me.ViewMode = New System.Windows.Forms.ToolStripButton
        Me.EditMode = New System.Windows.Forms.ToolStripButton
        Me.ToolStripSeparator1 = New System.Windows.Forms.ToolStripSeparator
        Me.Add = New System.Windows.Forms.ToolStripButton
        Me.Options = New System.Windows.Forms.ToolStripButton
        Me.ResetButton = New System.Windows.Forms.ToolStripButton
        Me.Enable = New System.Windows.Forms.ToolStripButton
        Me.QueueStrip.SuspendLayout()
        Me.SuspendLayout()
        '
        'Queues
        '
        Me.Queues.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Queues.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.Queues.FormattingEnabled = True
        Me.Queues.Location = New System.Drawing.Point(0, 32)
        Me.Queues.MaxDropDownItems = 20
        Me.Queues.Name = "Queues"
        Me.Queues.Size = New System.Drawing.Size(196, 21)
        Me.Queues.TabIndex = 0
        '
        'Scrollbar
        '
        Me.Scrollbar.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Scrollbar.Location = New System.Drawing.Point(179, 56)
        Me.Scrollbar.Name = "Scrollbar"
        Me.Scrollbar.Size = New System.Drawing.Size(17, 210)
        Me.Scrollbar.TabIndex = 1
        Me.Scrollbar.Visible = False
        '
        'Count
        '
        Me.Count.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Count.Location = New System.Drawing.Point(3, 56)
        Me.Count.Name = "Count"
        Me.Count.Size = New System.Drawing.Size(173, 13)
        Me.Count.TabIndex = 2
        '
        'QueueStrip
        '
        Me.QueueStrip.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.QueueStrip.AutoSize = False
        Me.QueueStrip.Dock = System.Windows.Forms.DockStyle.None
        Me.QueueStrip.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden
        Me.QueueStrip.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.DiffMode, Me.ViewMode, Me.EditMode, Me.ToolStripSeparator1, Me.Add, Me.Options, Me.ResetButton, Me.Enable})
        Me.QueueStrip.Location = New System.Drawing.Point(0, 0)
        Me.QueueStrip.Name = "QueueStrip"
        Me.QueueStrip.RenderMode = System.Windows.Forms.ToolStripRenderMode.System
        Me.QueueStrip.Size = New System.Drawing.Size(200, 30)
        Me.QueueStrip.TabIndex = 6
        Me.QueueStrip.Text = "ToolStrip1"
        '
        'DiffMode
        '
        Me.DiffMode.AutoSize = False
        Me.DiffMode.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.DiffMode.Image = Nothing
        Me.DiffMode.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.DiffMode.Name = "DiffMode"
        Me.DiffMode.Size = New System.Drawing.Size(24, 24)
        '
        'ViewMode
        '
        Me.ViewMode.AutoSize = False
        Me.ViewMode.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.ViewMode.Image = Nothing
        Me.ViewMode.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.ViewMode.Name = "ViewMode"
        Me.ViewMode.Size = New System.Drawing.Size(24, 24)
        '
        'EditMode
        '
        Me.EditMode.AutoSize = False
        Me.EditMode.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.EditMode.Image = Nothing
        Me.EditMode.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.EditMode.Name = "EditMode"
        Me.EditMode.Size = New System.Drawing.Size(24, 24)
        '
        'ToolStripSeparator1
        '
        Me.ToolStripSeparator1.Name = "ToolStripSeparator1"
        Me.ToolStripSeparator1.Size = New System.Drawing.Size(6, 30)
        '
        'Add
        '
        Me.Add.AutoSize = False
        Me.Add.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.Add.Image = Nothing
        Me.Add.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.Add.Name = "Add"
        Me.Add.Size = New System.Drawing.Size(24, 24)
        '
        'Options
        '
        Me.Options.AutoSize = False
        Me.Options.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.Options.Image = Nothing
        Me.Options.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.Options.Name = "Options"
        Me.Options.Size = New System.Drawing.Size(24, 24)
        '
        'ResetButton
        '
        Me.ResetButton.AutoSize = False
        Me.ResetButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.ResetButton.Image = Nothing
        Me.ResetButton.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.ResetButton.Name = "ResetButton"
        Me.ResetButton.Size = New System.Drawing.Size(24, 24)
        '
        'Enable
        '
        Me.Enable.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.Enable.Image = Nothing
        Me.Enable.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.Enable.Margin = New System.Windows.Forms.Padding(0, 2, 0, 2)
        Me.Enable.Name = "Enable"
        Me.Enable.Size = New System.Drawing.Size(23, 26)
        '
        'QueuePanel
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.Controls.Add(Me.QueueStrip)
        Me.Controls.Add(Me.Count)
        Me.Controls.Add(Me.Scrollbar)
        Me.Controls.Add(Me.Queues)
        Me.Name = "QueuePanel"
        Me.Size = New System.Drawing.Size(196, 266)
        Me.QueueStrip.ResumeLayout(False)
        Me.QueueStrip.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
    Private WithEvents Queues As System.Windows.Forms.ComboBox
    Private WithEvents Scrollbar As System.Windows.Forms.VScrollBar
    Private WithEvents Count As System.Windows.Forms.Label
    Private WithEvents QueueStrip As System.Windows.Forms.ToolStrip
    Private WithEvents DiffMode As System.Windows.Forms.ToolStripButton
    Private WithEvents ViewMode As System.Windows.Forms.ToolStripButton
    Private WithEvents EditMode As System.Windows.Forms.ToolStripButton
    Private WithEvents ToolStripSeparator1 As System.Windows.Forms.ToolStripSeparator
    Private WithEvents Add As System.Windows.Forms.ToolStripButton
    Private WithEvents Options As System.Windows.Forms.ToolStripButton
    Private WithEvents ResetButton As System.Windows.Forms.ToolStripButton
    Friend WithEvents Enable As System.Windows.Forms.ToolStripButton

End Class
