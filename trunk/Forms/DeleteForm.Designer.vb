<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class DeleteForm
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(DeleteForm))
        Me.Cancel = New System.Windows.Forms.Button
        Me.OK = New System.Windows.Forms.Button
        Me.Label1 = New System.Windows.Forms.Label
        Me.Reason = New System.Windows.Forms.ComboBox
        Me.DeleteLog = New System.Windows.Forms.ListView
        Me.Label2 = New System.Windows.Forms.Label
        Me.SuspendLayout()
        '
        'Cancel
        '
        Me.Cancel.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Cancel.Location = New System.Drawing.Point(507, 181)
        Me.Cancel.Name = "Cancel"
        Me.Cancel.Size = New System.Drawing.Size(75, 23)
        Me.Cancel.TabIndex = 5
        Me.Cancel.Text = "Cancel"
        Me.Cancel.UseVisualStyleBackColor = True
        '
        'OK
        '
        Me.OK.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.OK.Location = New System.Drawing.Point(426, 181)
        Me.OK.Name = "OK"
        Me.OK.Size = New System.Drawing.Size(75, 23)
        Me.OK.TabIndex = 4
        Me.OK.Text = "OK"
        Me.OK.UseVisualStyleBackColor = True
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(12, 15)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(163, 13)
        Me.Label1.TabIndex = 0
        Me.Label1.Text = "Reason (leave empty for default):"
        '
        'Reason
        '
        Me.Reason.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Reason.FormattingEnabled = True
        Me.Reason.Location = New System.Drawing.Point(182, 12)
        Me.Reason.MaxDropDownItems = 20
        Me.Reason.Name = "Reason"
        Me.Reason.Size = New System.Drawing.Size(400, 21)
        Me.Reason.TabIndex = 1
        '
        'DeleteLog
        '
        Me.DeleteLog.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.DeleteLog.FullRowSelect = True
        Me.DeleteLog.GridLines = True
        Me.DeleteLog.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable
        Me.DeleteLog.Location = New System.Drawing.Point(13, 61)
        Me.DeleteLog.MultiSelect = False
        Me.DeleteLog.Name = "DeleteLog"
        Me.DeleteLog.ShowGroups = False
        Me.DeleteLog.Size = New System.Drawing.Size(569, 111)
        Me.DeleteLog.TabIndex = 3
        Me.DeleteLog.UseCompatibleStateImageBehavior = False
        Me.DeleteLog.View = System.Windows.Forms.View.Details
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(12, 45)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(66, 13)
        Me.Label2.TabIndex = 2
        Me.Label2.Text = "Deletion log:"
        '
        'DeleteForm
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(594, 216)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.DeleteLog)
        Me.Controls.Add(Me.Reason)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.OK)
        Me.Controls.Add(Me.Cancel)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.KeyPreview = True
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "DeleteForm"
        Me.ShowIcon = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Delete page"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents Cancel As System.Windows.Forms.Button
    Friend WithEvents OK As System.Windows.Forms.Button
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents Reason As System.Windows.Forms.ComboBox
    Friend WithEvents DeleteLog As System.Windows.Forms.ListView
    Friend WithEvents Label2 As System.Windows.Forms.Label
End Class
