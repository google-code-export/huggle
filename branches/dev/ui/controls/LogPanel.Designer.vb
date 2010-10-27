Namespace Huggle.UI
    <Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
    Partial Class LogPanel
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
            Me.components = New System.ComponentModel.Container
            Me.LogList = New System.Windows.Forms.EnhancedListView
            Me.LogMenu = New System.Windows.Forms.ContextMenuStrip(Me.components)
            Me.LogCopy = New System.Windows.Forms.ToolStripMenuItem
            Me.LogMenu.SuspendLayout()
            Me.SuspendLayout()
            '
            'LogList
            '
            Me.LogList.Dock = System.Windows.Forms.DockStyle.Fill
            Me.LogList.FullRowSelect = True
            Me.LogList.GridLines = True
            Me.LogList.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.None
            Me.LogList.Location = New System.Drawing.Point(0, 0)
            Me.LogList.Name = "LogList"
            Me.LogList.ShowGroups = False
            Me.LogList.Size = New System.Drawing.Size(436, 150)
            Me.LogList.TabIndex = 0
            Me.LogList.UseCompatibleStateImageBehavior = False
            Me.LogList.View = System.Windows.Forms.View.Details
            '
            'LogMenu
            '
            Me.LogMenu.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.LogCopy})
            Me.LogMenu.Name = "LogMenu"
            Me.LogMenu.Size = New System.Drawing.Size(153, 48)
            '
            'LogCopy
            '
            Me.LogCopy.Name = "LogCopy"
            Me.LogCopy.Size = New System.Drawing.Size(152, 22)
            Me.LogCopy.Text = "Copy"
            '
            'LogPanel
            '
            Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
            Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
            Me.Controls.Add(Me.LogList)
            Me.Name = "LogPanel"
            Me.Size = New System.Drawing.Size(436, 150)
            Me.LogMenu.ResumeLayout(False)
            Me.ResumeLayout(False)

        End Sub
        Private WithEvents LogList As System.Windows.Forms.EnhancedListView
        Friend WithEvents LogMenu As System.Windows.Forms.ContextMenuStrip
        Friend WithEvents LogCopy As System.Windows.Forms.ToolStripMenuItem

    End Class
End Namespace