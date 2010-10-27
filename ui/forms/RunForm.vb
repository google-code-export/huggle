Imports System.Drawing
Imports System.Windows.Forms

Namespace Huggle.UI

    Public Class RunForm

        Private Sub Mode_DrawItem(ByVal s As Object, ByVal e As DrawItemEventArgs)
            e.DrawBackground()
            e.DrawFocusRectangle()

            Select Case e.Index

            End Select

            'e.Graphics.DrawString(Mode.Items(e.Index).ToString, e.Font, New Pen(e.ForeColor).Brush, e.Bounds, _
            '    New StringFormat With {.Alignment = StringAlignment.Center, .LineAlignment = StringAlignment.Far})
        End Sub

    End Class

End Namespace