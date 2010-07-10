<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Form1
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
        Me.Rev = New System.Windows.Forms.TextBox
        Me.Rollback = New System.Windows.Forms.Button
        Me.WebBrowser1 = New System.Windows.Forms.WebBrowser
        Me.SuspendLayout()
        '
        'Rev
        '
        Me.Rev.Location = New System.Drawing.Point(12, 12)
        Me.Rev.Name = "Rev"
        Me.Rev.Size = New System.Drawing.Size(184, 20)
        Me.Rev.TabIndex = 0
        '
        'Rollback
        '
        Me.Rollback.Location = New System.Drawing.Point(202, 9)
        Me.Rollback.Name = "Rollback"
        Me.Rollback.Size = New System.Drawing.Size(91, 23)
        Me.Rollback.TabIndex = 1
        Me.Rollback.Text = "revert"
        Me.Rollback.UseVisualStyleBackColor = True
        '
        'WebBrowser1
        '
        Me.WebBrowser1.Location = New System.Drawing.Point(12, 38)
        Me.WebBrowser1.MinimumSize = New System.Drawing.Size(20, 20)
        Me.WebBrowser1.Name = "WebBrowser1"
        Me.WebBrowser1.Size = New System.Drawing.Size(576, 359)
        Me.WebBrowser1.TabIndex = 2
        '
        'Form1
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(600, 409)
        Me.Controls.Add(Me.WebBrowser1)
        Me.Controls.Add(Me.Rollback)
        Me.Controls.Add(Me.Rev)
        Me.Name = "Form1"
        Me.Text = "Form1"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents Rev As System.Windows.Forms.TextBox
    Friend WithEvents Rollback As System.Windows.Forms.Button
    Friend WithEvents WebBrowser1 As System.Windows.Forms.WebBrowser

End Class
