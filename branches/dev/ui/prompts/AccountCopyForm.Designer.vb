<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class AccountCopyForm
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
        Me.Message = New System.Windows.Forms.Label()
        Me.UseDefault = New System.Windows.Forms.RadioButton()
        Me.UseCopy = New System.Windows.Forms.RadioButton()
        Me.Source = New System.Windows.Forms.ComboBox()
        Me.OK = New System.Windows.Forms.Button()
        Me.SuspendLayout()
        '
        'Message
        '
        Me.Message.AutoSize = True
        Me.Message.Location = New System.Drawing.Point(12, 15)
        Me.Message.Name = "Message"
        Me.Message.Size = New System.Drawing.Size(262, 13)
        Me.Message.TabIndex = 0
        Me.Message.Text = "You have used Huggle on {0} before, but not with {1}."
        '
        'UseDefault
        '
        Me.UseDefault.AutoSize = True
        Me.UseDefault.Checked = True
        Me.UseDefault.Location = New System.Drawing.Point(15, 44)
        Me.UseDefault.Name = "UseDefault"
        Me.UseDefault.Size = New System.Drawing.Size(240, 17)
        Me.UseDefault.TabIndex = 1
        Me.UseDefault.TabStop = True
        Me.UseDefault.Text = "Use the wiki's default settings for this account"
        Me.UseDefault.UseVisualStyleBackColor = True
        '
        'UseCopy
        '
        Me.UseCopy.AutoSize = True
        Me.UseCopy.Location = New System.Drawing.Point(15, 67)
        Me.UseCopy.Name = "UseCopy"
        Me.UseCopy.Size = New System.Drawing.Size(153, 17)
        Me.UseCopy.TabIndex = 1
        Me.UseCopy.Text = "Copy settings from account"
        Me.UseCopy.UseVisualStyleBackColor = True
        '
        'Source
        '
        Me.Source.FormattingEnabled = True
        Me.Source.Location = New System.Drawing.Point(174, 66)
        Me.Source.Name = "Source"
        Me.Source.Size = New System.Drawing.Size(162, 21)
        Me.Source.TabIndex = 2
        '
        'OK
        '
        Me.OK.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.OK.DialogResult = System.Windows.Forms.DialogResult.OK
        Me.OK.Location = New System.Drawing.Point(261, 100)
        Me.OK.Name = "OK"
        Me.OK.Size = New System.Drawing.Size(75, 23)
        Me.OK.TabIndex = 3
        Me.OK.Text = "OK"
        Me.OK.UseVisualStyleBackColor = True
        '
        'AccountCopyForm
        '
        Me.AcceptButton = Me.OK
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(348, 135)
        Me.Controls.Add(Me.OK)
        Me.Controls.Add(Me.Source)
        Me.Controls.Add(Me.UseCopy)
        Me.Controls.Add(Me.UseDefault)
        Me.Controls.Add(Me.Message)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "AccountCopyForm"
        Me.Text = "Account Settings"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Private WithEvents Message As System.Windows.Forms.Label
    Private WithEvents UseDefault As System.Windows.Forms.RadioButton
    Private WithEvents UseCopy As System.Windows.Forms.RadioButton
    Private WithEvents Source As System.Windows.Forms.ComboBox
    Private WithEvents OK As System.Windows.Forms.Button
End Class
