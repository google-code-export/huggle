Namespace Huggle.UI
    <Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
    Partial Class AccountConfigForm
        Inherits Huggle.UI.HuggleForm

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
            Me.GroupBox1 = New System.Windows.Forms.GroupBox
            Me.GroupList = New System.Windows.Forms.ListBox
            Me.RestoreDefaults = New System.Windows.Forms.Button
            Me.Save = New System.Windows.Forms.Button
            Me.Cancel = New System.Windows.Forms.Button
            Me.Summary = New System.Windows.Forms.Label
            Me.SuspendLayout()
            '
            'GroupBox1
            '
            Me.GroupBox1.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                        Or System.Windows.Forms.AnchorStyles.Left) _
                        Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
            Me.GroupBox1.Location = New System.Drawing.Point(132, 34)
            Me.GroupBox1.Name = "GroupBox1"
            Me.GroupBox1.Size = New System.Drawing.Size(450, 299)
            Me.GroupBox1.TabIndex = 11
            Me.GroupBox1.TabStop = False
            Me.GroupBox1.Text = "GroupBox1"
            '
            'GroupList
            '
            Me.GroupList.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                        Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
            Me.GroupList.IntegralHeight = False
            Me.GroupList.Location = New System.Drawing.Point(12, 34)
            Me.GroupList.Name = "GroupList"
            Me.GroupList.Size = New System.Drawing.Size(114, 299)
            Me.GroupList.TabIndex = 10
            '
            'RestoreDefaults
            '
            Me.RestoreDefaults.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
            Me.RestoreDefaults.Location = New System.Drawing.Point(12, 339)
            Me.RestoreDefaults.Name = "RestoreDefaults"
            Me.RestoreDefaults.Size = New System.Drawing.Size(114, 23)
            Me.RestoreDefaults.TabIndex = 9
            Me.RestoreDefaults.Text = "Restore Defaults..."
            Me.RestoreDefaults.UseVisualStyleBackColor = True
            '
            'Save
            '
            Me.Save.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
            Me.Save.Location = New System.Drawing.Point(426, 339)
            Me.Save.Name = "Save"
            Me.Save.Size = New System.Drawing.Size(75, 23)
            Me.Save.TabIndex = 8
            Me.Save.Text = "Save"
            Me.Save.UseVisualStyleBackColor = True
            '
            'Cancel
            '
            Me.Cancel.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
            Me.Cancel.Location = New System.Drawing.Point(507, 339)
            Me.Cancel.Name = "Cancel"
            Me.Cancel.Size = New System.Drawing.Size(75, 23)
            Me.Cancel.TabIndex = 7
            Me.Cancel.Text = "Cancel"
            Me.Cancel.UseVisualStyleBackColor = True
            '
            'Summary
            '
            Me.Summary.AutoSize = True
            Me.Summary.Location = New System.Drawing.Point(12, 9)
            Me.Summary.Name = "Summary"
            Me.Summary.Size = New System.Drawing.Size(345, 13)
            Me.Summary.TabIndex = 6
            Me.Summary.Text = "Use this screen to configure the behavior of Huggle for the account {0}."
            '
            'AccountSettings
            '
            Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
            Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
            Me.ClientSize = New System.Drawing.Size(594, 374)
            Me.Controls.Add(Me.GroupBox1)
            Me.Controls.Add(Me.GroupList)
            Me.Controls.Add(Me.RestoreDefaults)
            Me.Controls.Add(Me.Save)
            Me.Controls.Add(Me.Cancel)
            Me.Controls.Add(Me.Summary)
            Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
            Me.Name = "AccountSettings"
            Me.Text = "Account Settings"
            Me.ResumeLayout(False)
            Me.PerformLayout()

        End Sub
        Private WithEvents GroupBox1 As System.Windows.Forms.GroupBox
        Private WithEvents GroupList As System.Windows.Forms.ListBox
        Private WithEvents RestoreDefaults As System.Windows.Forms.Button
        Private WithEvents Save As System.Windows.Forms.Button
        Private WithEvents Cancel As System.Windows.Forms.Button
        Private WithEvents Summary As System.Windows.Forms.Label
    End Class
End Namespace