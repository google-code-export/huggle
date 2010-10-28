Namespace Huggle.UI
    <Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
    Partial Class WikiPropertiesForm
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
            Me.Views = New System.Windows.Forms.ListBox()
            Me.ViewContainer = New System.Windows.Forms.Panel()
            Me.SuspendLayout()
            '
            'Views
            '
            Me.Views.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                        Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
            Me.Views.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed
            Me.Views.FormattingEnabled = True
            Me.Views.IntegralHeight = False
            Me.Views.ItemHeight = 20
            Me.Views.Location = New System.Drawing.Point(5, 5)
            Me.Views.Name = "Views"
            Me.Views.Size = New System.Drawing.Size(130, 432)
            Me.Views.TabIndex = 0
            '
            'ViewContainer
            '
            Me.ViewContainer.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                        Or System.Windows.Forms.AnchorStyles.Left) _
                        Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
            Me.ViewContainer.Location = New System.Drawing.Point(139, 5)
            Me.ViewContainer.Name = "ViewContainer"
            Me.ViewContainer.Size = New System.Drawing.Size(562, 432)
            Me.ViewContainer.TabIndex = 1
            '
            'WikiPropertiesForm
            '
            Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
            Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
            Me.ClientSize = New System.Drawing.Size(704, 442)
            Me.Controls.Add(Me.ViewContainer)
            Me.Controls.Add(Me.Views)
            Me.Name = "WikiPropertiesForm"
            Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
            Me.Text = "Wiki"
            Me.ResumeLayout(False)

        End Sub
        Private WithEvents Views As System.Windows.Forms.ListBox
        Private WithEvents ViewContainer As System.Windows.Forms.Panel
    End Class
End Namespace