<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class QueryPanel
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
        Me.Result = New DataView
        Me.Query = New System.Windows.Forms.RichTextBox
        Me.Run = New System.Windows.Forms.Button
        Me.Progress = New System.Windows.Forms.Label
        Me.Indicator = New Huggle.Controls.WaitControl
        Me.SuspendLayout()
        '
        'Result
        '
        Me.Result.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Result.Location = New System.Drawing.Point(3, 163)
        Me.Result.Name = "Result"
        Me.Result.Size = New System.Drawing.Size(539, 131)
        Me.Result.TabIndex = 0
        Me.Result.Value = ""
        '
        'Query
        '
        Me.Query.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Query.Font = New System.Drawing.Font("Courier New", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Query.Location = New System.Drawing.Point(3, 3)
        Me.Query.Name = "Query"
        Me.Query.Size = New System.Drawing.Size(539, 125)
        Me.Query.TabIndex = 1
        Me.Query.Text = ""
        '
        'Run
        '
        Me.Run.Enabled = False
        Me.Run.Location = New System.Drawing.Point(3, 134)
        Me.Run.Name = "Run"
        Me.Run.Size = New System.Drawing.Size(75, 23)
        Me.Run.TabIndex = 2
        Me.Run.Text = "Run"
        Me.Run.UseVisualStyleBackColor = True
        '
        'Progress
        '
        Me.Progress.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Progress.Location = New System.Drawing.Point(39, 215)
        Me.Progress.Name = "Progress"
        Me.Progress.Size = New System.Drawing.Size(491, 18)
        Me.Progress.TabIndex = 3
        Me.Progress.Visible = False
        '
        'Indicator
        '
        Me.Indicator.Location = New System.Drawing.Point(17, 215)
        Me.Indicator.MaximumSize = New System.Drawing.Size(16, 16)
        Me.Indicator.MinimumSize = New System.Drawing.Size(16, 16)
        Me.Indicator.Name = "Indicator"
        Me.Indicator.Size = New System.Drawing.Size(16, 16)
        Me.Indicator.TabIndex = 5
        Me.Indicator.TabStop = False
        Me.Indicator.Visible = False
        '
        'QueryPanel
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.Controls.Add(Me.Indicator)
        Me.Controls.Add(Me.Progress)
        Me.Controls.Add(Me.Run)
        Me.Controls.Add(Me.Query)
        Me.Controls.Add(Me.Result)
        Me.Name = "QueryPanel"
        Me.Size = New System.Drawing.Size(545, 297)
        Me.ResumeLayout(False)

    End Sub
    Private WithEvents Query As System.Windows.Forms.RichTextBox
    Private WithEvents Result As DataView
    Private WithEvents Run As System.Windows.Forms.Button
    Private WithEvents Progress As System.Windows.Forms.Label
    Friend WithEvents Indicator As Huggle.Controls.WaitControl

End Class
