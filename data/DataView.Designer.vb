Namespace Huggle.UI
    <Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
    Partial Class DataView
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
            Me.Toolbar = New System.Windows.Forms.ToolStrip
            Me.ViewText = New System.Windows.Forms.ToolStripButton
            Me.ViewWikiText = New System.Windows.Forms.ToolStripButton
            Me.ViewHtml = New System.Windows.Forms.ToolStripButton
            Me.ViewWebPage = New System.Windows.Forms.ToolStripButton
            Me.ViewTable = New System.Windows.Forms.ToolStripButton
            Me.ViewImage = New System.Windows.Forms.ToolStripButton
            Me.ViewChart = New System.Windows.Forms.ToolStripButton
            Me.TextData = New System.Windows.Forms.RichTextBox
            Me.WebData = New System.Windows.Forms.WebBrowser
            Me.ListData = New System.Windows.Forms.ListView
            Me.ResultData = New System.Windows.Forms.TextBox
            Me.Toolbar.SuspendLayout()
            Me.SuspendLayout()
            '
            'Toolbar
            '
            Me.Toolbar.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden
            Me.Toolbar.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ViewText, Me.ViewWikiText, Me.ViewHtml, Me.ViewWebPage, Me.ViewTable, Me.ViewImage, Me.ViewChart})
            Me.Toolbar.Location = New System.Drawing.Point(0, 0)
            Me.Toolbar.Name = "Toolbar"
            Me.Toolbar.RenderMode = System.Windows.Forms.ToolStripRenderMode.System
            Me.Toolbar.Size = New System.Drawing.Size(899, 39)
            Me.Toolbar.TabIndex = 1
            '
            'ViewText
            '
            Me.ViewText.AutoSize = False
            Me.ViewText.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
            Me.ViewText.Image = Huggle.Resources.data_text
            Me.ViewText.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None
            Me.ViewText.ImageTransparentColor = System.Drawing.Color.Magenta
            Me.ViewText.Name = "ViewText"
            Me.ViewText.Overflow = System.Windows.Forms.ToolStripItemOverflow.Never
            Me.ViewText.Size = New System.Drawing.Size(36, 36)
            '
            'ViewWikiText
            '
            Me.ViewWikiText.AutoSize = False
            Me.ViewWikiText.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
            Me.ViewWikiText.Image = Huggle.Resources.data_wikitext
            Me.ViewWikiText.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None
            Me.ViewWikiText.ImageTransparentColor = System.Drawing.Color.Magenta
            Me.ViewWikiText.Name = "ViewWikiText"
            Me.ViewWikiText.Overflow = System.Windows.Forms.ToolStripItemOverflow.Never
            Me.ViewWikiText.Size = New System.Drawing.Size(36, 36)
            '
            'ViewHtml
            '
            Me.ViewHtml.AutoSize = False
            Me.ViewHtml.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
            Me.ViewHtml.Image = Huggle.Resources.data_html
            Me.ViewHtml.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None
            Me.ViewHtml.ImageTransparentColor = System.Drawing.Color.Magenta
            Me.ViewHtml.Name = "ViewHtml"
            Me.ViewHtml.Overflow = System.Windows.Forms.ToolStripItemOverflow.Never
            Me.ViewHtml.Size = New System.Drawing.Size(36, 36)
            '
            'ViewWebPage
            '
            Me.ViewWebPage.AutoSize = False
            Me.ViewWebPage.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
            Me.ViewWebPage.Image = Huggle.Resources.data_webpage
            Me.ViewWebPage.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None
            Me.ViewWebPage.ImageTransparentColor = System.Drawing.Color.Magenta
            Me.ViewWebPage.Name = "ViewWebPage"
            Me.ViewWebPage.Overflow = System.Windows.Forms.ToolStripItemOverflow.Never
            Me.ViewWebPage.Size = New System.Drawing.Size(36, 36)
            '
            'ViewTable
            '
            Me.ViewTable.AutoSize = False
            Me.ViewTable.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
            Me.ViewTable.Image = Huggle.Resources.data_table
            Me.ViewTable.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None
            Me.ViewTable.ImageTransparentColor = System.Drawing.Color.Magenta
            Me.ViewTable.Name = "ViewTable"
            Me.ViewTable.Overflow = System.Windows.Forms.ToolStripItemOverflow.Never
            Me.ViewTable.Size = New System.Drawing.Size(36, 36)
            '
            'ViewImage
            '
            Me.ViewImage.AutoSize = False
            Me.ViewImage.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
            Me.ViewImage.Image = Huggle.Resources.data_image
            Me.ViewImage.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None
            Me.ViewImage.ImageTransparentColor = System.Drawing.Color.Magenta
            Me.ViewImage.Name = "ViewImage"
            Me.ViewImage.Overflow = System.Windows.Forms.ToolStripItemOverflow.Never
            Me.ViewImage.Size = New System.Drawing.Size(36, 36)
            '
            'ViewChart
            '
            Me.ViewChart.AutoSize = False
            Me.ViewChart.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
            Me.ViewChart.Image = Huggle.Resources.data_chart
            Me.ViewChart.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None
            Me.ViewChart.ImageTransparentColor = System.Drawing.Color.Magenta
            Me.ViewChart.Name = "ViewChart"
            Me.ViewChart.Size = New System.Drawing.Size(36, 36)
            '
            'TextData
            '
            Me.TextData.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                        Or System.Windows.Forms.AnchorStyles.Left) _
                        Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
            Me.TextData.Font = New System.Drawing.Font("Courier New", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.TextData.Location = New System.Drawing.Point(3, 42)
            Me.TextData.Name = "TextData"
            Me.TextData.Size = New System.Drawing.Size(893, 317)
            Me.TextData.TabIndex = 2
            Me.TextData.Text = ""
            '
            'WebData
            '
            Me.WebData.AllowWebBrowserDrop = False
            Me.WebData.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                        Or System.Windows.Forms.AnchorStyles.Left) _
                        Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
            Me.WebData.Location = New System.Drawing.Point(3, 42)
            Me.WebData.MinimumSize = New System.Drawing.Size(20, 20)
            Me.WebData.Name = "WebData"
            Me.WebData.ScriptErrorsSuppressed = True
            Me.WebData.Size = New System.Drawing.Size(893, 317)
            Me.WebData.TabIndex = 3
            '
            'ListData
            '
            Me.ListData.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                        Or System.Windows.Forms.AnchorStyles.Left) _
                        Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
            Me.ListData.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.ListData.FullRowSelect = True
            Me.ListData.GridLines = True
            Me.ListData.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.None
            Me.ListData.Location = New System.Drawing.Point(3, 42)
            Me.ListData.MultiSelect = False
            Me.ListData.Name = "ListData"
            Me.ListData.ShowGroups = False
            Me.ListData.Size = New System.Drawing.Size(893, 317)
            Me.ListData.TabIndex = 4
            Me.ListData.UseCompatibleStateImageBehavior = False
            Me.ListData.View = System.Windows.Forms.View.Details
            '
            'ResultData
            '
            Me.ResultData.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                        Or System.Windows.Forms.AnchorStyles.Left) _
                        Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
            Me.ResultData.BorderStyle = System.Windows.Forms.BorderStyle.None
            Me.ResultData.Font = New System.Drawing.Font("Courier New", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.ResultData.Location = New System.Drawing.Point(3, 43)
            Me.ResultData.Multiline = True
            Me.ResultData.Name = "ResultData"
            Me.ResultData.ReadOnly = True
            Me.ResultData.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
            Me.ResultData.Size = New System.Drawing.Size(893, 316)
            Me.ResultData.TabIndex = 5
            '
            'DataView
            '
            Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
            Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
            Me.Controls.Add(Me.ResultData)
            Me.Controls.Add(Me.ListData)
            Me.Controls.Add(Me.WebData)
            Me.Controls.Add(Me.TextData)
            Me.Controls.Add(Me.Toolbar)
            Me.Name = "DataView"
            Me.Size = New System.Drawing.Size(899, 362)
            Me.Toolbar.ResumeLayout(False)
            Me.Toolbar.PerformLayout()
            Me.ResumeLayout(False)
            Me.PerformLayout()

        End Sub
        Private WithEvents Toolbar As System.Windows.Forms.ToolStrip
        Private WithEvents ViewText As System.Windows.Forms.ToolStripButton
        Private WithEvents ViewWikiText As System.Windows.Forms.ToolStripButton
        Private WithEvents ViewHtml As System.Windows.Forms.ToolStripButton
        Private WithEvents ViewWebPage As System.Windows.Forms.ToolStripButton
        Private WithEvents ViewTable As System.Windows.Forms.ToolStripButton
        Private WithEvents ViewImage As System.Windows.Forms.ToolStripButton
        Private WithEvents ViewChart As System.Windows.Forms.ToolStripButton
        Friend WithEvents TextData As System.Windows.Forms.RichTextBox
        Private WithEvents WebData As System.Windows.Forms.WebBrowser
        Private WithEvents ListData As System.Windows.Forms.ListView
        Private WithEvents ResultData As System.Windows.Forms.TextBox

    End Class
End Namespace