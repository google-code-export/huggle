﻿Namespace Huggle.UI
    <Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
    Partial Class ErrorForm
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
            Me.OK = New System.Windows.Forms.Button()
            Me.Image = New System.Windows.Forms.PictureBox()
            Me.Panel1 = New System.Windows.Forms.Panel()
            Me.MessageBox = New System.Windows.Forms.Label()
            Me.Copy = New System.Windows.Forms.Button()
            Me.Retry = New System.Windows.Forms.Button()
            Me.Buttons = New System.Windows.Forms.FlowLayoutPanel()
            CType(Me.Image, System.ComponentModel.ISupportInitialize).BeginInit()
            Me.Panel1.SuspendLayout()
            Me.Buttons.SuspendLayout()
            Me.SuspendLayout()
            '
            'OK
            '
            Me.OK.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
            Me.OK.DialogResult = System.Windows.Forms.DialogResult.Cancel
            Me.OK.Location = New System.Drawing.Point(165, 3)
            Me.OK.Name = "OK"
            Me.OK.Size = New System.Drawing.Size(75, 23)
            Me.OK.TabIndex = 0
            Me.OK.Text = "OK"
            Me.OK.UseVisualStyleBackColor = True
            '
            'Image
            '
            Me.Image.Anchor = System.Windows.Forms.AnchorStyles.Left
            Me.Image.Location = New System.Drawing.Point(4, 6)
            Me.Image.Name = "Image"
            Me.Image.Size = New System.Drawing.Size(32, 32)
            Me.Image.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize
            Me.Image.TabIndex = 2
            Me.Image.TabStop = False
            '
            'Panel1
            '
            Me.Panel1.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                        Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
            Me.Panel1.Controls.Add(Me.Image)
            Me.Panel1.Location = New System.Drawing.Point(12, 12)
            Me.Panel1.Name = "Panel1"
            Me.Panel1.Size = New System.Drawing.Size(40, 44)
            Me.Panel1.TabIndex = 1
            '
            'MessageBox
            '
            Me.MessageBox.AutoSize = True
            Me.MessageBox.Location = New System.Drawing.Point(58, 12)
            Me.MessageBox.Name = "MessageBox"
            Me.MessageBox.Size = New System.Drawing.Size(0, 13)
            Me.MessageBox.TabIndex = 2
            '
            'Copy
            '
            Me.Copy.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
            Me.Copy.Location = New System.Drawing.Point(3, 3)
            Me.Copy.Name = "Copy"
            Me.Copy.Size = New System.Drawing.Size(75, 23)
            Me.Copy.TabIndex = 3
            Me.Copy.Text = "Copy"
            Me.Copy.UseVisualStyleBackColor = True
            '
            'Retry
            '
            Me.Retry.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
            Me.Retry.DialogResult = System.Windows.Forms.DialogResult.Cancel
            Me.Retry.Location = New System.Drawing.Point(84, 3)
            Me.Retry.Name = "Retry"
            Me.Retry.Size = New System.Drawing.Size(75, 23)
            Me.Retry.TabIndex = 4
            Me.Retry.Text = "Retry"
            Me.Retry.UseVisualStyleBackColor = True
            '
            'Buttons
            '
            Me.Buttons.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
            Me.Buttons.AutoSize = True
            Me.Buttons.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
            Me.Buttons.Controls.Add(Me.OK)
            Me.Buttons.Controls.Add(Me.Retry)
            Me.Buttons.Controls.Add(Me.Copy)
            Me.Buttons.FlowDirection = System.Windows.Forms.FlowDirection.RightToLeft
            Me.Buttons.Location = New System.Drawing.Point(26, 61)
            Me.Buttons.Name = "Buttons"
            Me.Buttons.Size = New System.Drawing.Size(243, 29)
            Me.Buttons.TabIndex = 5
            '
            'ErrorForm
            '
            Me.AcceptButton = Me.OK
            Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
            Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
            Me.CancelButton = Me.OK
            Me.ClientSize = New System.Drawing.Size(274, 94)
            Me.Controls.Add(Me.Buttons)
            Me.Controls.Add(Me.MessageBox)
            Me.Controls.Add(Me.Panel1)
            Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
            Me.KeyPreview = True
            Me.MaximizeBox = False
            Me.MinimizeBox = False
            Me.Name = "ErrorForm"
            Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
            Me.Text = "Huggle"
            CType(Me.Image, System.ComponentModel.ISupportInitialize).EndInit()
            Me.Panel1.ResumeLayout(False)
            Me.Panel1.PerformLayout()
            Me.Buttons.ResumeLayout(False)
            Me.ResumeLayout(False)
            Me.PerformLayout()

        End Sub
        Private WithEvents OK As System.Windows.Forms.Button
        Private WithEvents Image As System.Windows.Forms.PictureBox
        Private WithEvents Panel1 As System.Windows.Forms.Panel
        Private WithEvents MessageBox As System.Windows.Forms.Label
        Private WithEvents Copy As System.Windows.Forms.Button
        Private WithEvents Retry As System.Windows.Forms.Button
        Public WithEvents Buttons As System.Windows.Forms.FlowLayoutPanel
    End Class
End Namespace