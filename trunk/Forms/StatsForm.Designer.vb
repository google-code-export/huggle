<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class StatsForm
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
        Me.components = New System.ComponentModel.Container
        Me.OK = New System.Windows.Forms.Button
        Me.Label1 = New System.Windows.Forms.Label
        Me.StatsTimer = New System.Windows.Forms.Timer(Me.components)
        Me.Actions = New Huggle.ListView2
        Me.SuspendLayout()
        '
        'OK
        '
        Me.OK.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.OK.Location = New System.Drawing.Point(467, 213)
        Me.OK.Name = "OK"
        Me.OK.Size = New System.Drawing.Size(75, 23)
        Me.OK.TabIndex = 0
        Me.OK.Text = "Close"
        Me.OK.UseVisualStyleBackColor = True
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(12, 9)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(102, 13)
        Me.Label1.TabIndex = 1
        Me.Label1.Text = "Actions this session:"
        '
        'StatsTimer
        '
        Me.StatsTimer.Enabled = True
        Me.StatsTimer.Interval = 500
        '
        'Actions
        '
        Me.Actions.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Actions.FullRowSelect = True
        Me.Actions.GridLines = True
        Me.Actions.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable
        Me.Actions.Location = New System.Drawing.Point(12, 25)
        Me.Actions.MultiSelect = False
        Me.Actions.Name = "Actions"
        Me.Actions.Size = New System.Drawing.Size(530, 182)
        Me.Actions.TabIndex = 2
        Me.Actions.UseCompatibleStateImageBehavior = False
        Me.Actions.View = System.Windows.Forms.View.Details
        '
        'StatsForm
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(554, 248)
        Me.Controls.Add(Me.Actions)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.OK)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.KeyPreview = True
        Me.MaximizeBox = False
        Me.Name = "StatsForm"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Statistics"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents OK As System.Windows.Forms.Button
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents Actions As Huggle.ListView2
    Private WithEvents StatsTimer As System.Windows.Forms.Timer
End Class
