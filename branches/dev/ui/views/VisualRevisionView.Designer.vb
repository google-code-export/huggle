Namespace Huggle.UI
    <Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
    Partial Class VisualRevisionView
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
            Me.Title = New System.Windows.Forms.Label()
            Me.ViewPanel = New System.Windows.Forms.Panel()
            Me.QueueSelector = New System.Windows.Forms.EnhancedComboBox()
            Me.QueueLabel = New System.Windows.Forms.Label()
            Me.GroupLabel = New System.Windows.Forms.Label()
            Me.GroupSelector = New System.Windows.Forms.EnhancedComboBox()
            Me.SuspendLayout()
            '
            'Title
            '
            Me.Title.AutoSize = True
            Me.Title.Font = New System.Drawing.Font("Segoe UI", 14.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.Title.Location = New System.Drawing.Point(-2, -3)
            Me.Title.Name = "Title"
            Me.Title.Size = New System.Drawing.Size(90, 25)
            Me.Title.TabIndex = 23
            Me.Title.Text = "Revisions"
            '
            'ViewPanel
            '
            Me.ViewPanel.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                        Or System.Windows.Forms.AnchorStyles.Left) _
                        Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
            Me.ViewPanel.Location = New System.Drawing.Point(3, 52)
            Me.ViewPanel.Name = "ViewPanel"
            Me.ViewPanel.Size = New System.Drawing.Size(716, 387)
            Me.ViewPanel.TabIndex = 29
            '
            'QueueSelector
            '
            Me.QueueSelector.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
            Me.QueueSelector.FormattingEnabled = True
            Me.QueueSelector.Location = New System.Drawing.Point(51, 25)
            Me.QueueSelector.Name = "QueueSelector"
            Me.QueueSelector.Size = New System.Drawing.Size(121, 21)
            Me.QueueSelector.TabIndex = 30
            '
            'QueueLabel
            '
            Me.QueueLabel.AutoSize = True
            Me.QueueLabel.Location = New System.Drawing.Point(3, 28)
            Me.QueueLabel.Name = "QueueLabel"
            Me.QueueLabel.Size = New System.Drawing.Size(42, 13)
            Me.QueueLabel.TabIndex = 31
            Me.QueueLabel.Text = "Queue:"
            '
            'GroupLabel
            '
            Me.GroupLabel.AutoSize = True
            Me.GroupLabel.Location = New System.Drawing.Point(219, 28)
            Me.GroupLabel.Name = "GroupLabel"
            Me.GroupLabel.Size = New System.Drawing.Size(53, 13)
            Me.GroupLabel.TabIndex = 32
            Me.GroupLabel.Text = "Group by:"
            '
            'GroupSelector
            '
            Me.GroupSelector.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
            Me.GroupSelector.FormattingEnabled = True
            Me.GroupSelector.Location = New System.Drawing.Point(278, 25)
            Me.GroupSelector.Name = "GroupSelector"
            Me.GroupSelector.Size = New System.Drawing.Size(121, 21)
            Me.GroupSelector.TabIndex = 33
            '
            'RevisionView
            '
            Me.Controls.Add(Me.GroupSelector)
            Me.Controls.Add(Me.GroupLabel)
            Me.Controls.Add(Me.QueueLabel)
            Me.Controls.Add(Me.QueueSelector)
            Me.Controls.Add(Me.ViewPanel)
            Me.Controls.Add(Me.Title)
            Me.Name = "RevisionView"
            Me.Size = New System.Drawing.Size(722, 442)
            Me.ResumeLayout(False)
            Me.PerformLayout()

        End Sub
        Private WithEvents Title As System.Windows.Forms.Label
        Friend WithEvents ViewPanel As System.Windows.Forms.Panel
        Private WithEvents QueueSelector As System.Windows.Forms.EnhancedComboBox
        Private WithEvents QueueLabel As System.Windows.Forms.Label
        Private WithEvents GroupLabel As System.Windows.Forms.Label
        Private WithEvents GroupSelector As System.Windows.Forms.EnhancedComboBox

    End Class
End Namespace