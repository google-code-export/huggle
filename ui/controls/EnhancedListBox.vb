Imports System.Drawing

Namespace System.Windows.Forms

    Public Class EnhancedListBox : Inherits ListBox

        'ListBox, now with less flickering

        Public Sub New()
            SetStyle(ControlStyles.OptimizedDoubleBuffer Or ControlStyles.ResizeRedraw Or ControlStyles.UserPaint, True)
            DrawMode = Forms.DrawMode.OwnerDrawFixed
        End Sub

        Protected Overrides Sub OnDrawItem(ByVal e As DrawItemEventArgs)
            e.DrawBackground()

            TextRenderer.DrawText(e.Graphics, Items(e.Index).ToString,
                Font, e.Bounds, e.ForeColor, e.BackColor, TextFormatFlags.VerticalCenter)

            MyBase.OnDrawItem(e)
        End Sub

        Protected Overrides Sub OnPaint(ByVal e As PaintEventArgs)
            Dim iRegion As New Region(e.ClipRectangle)
            e.Graphics.FillRegion(SystemBrushes.Window, iRegion)

            If (Items.Count > 0) Then
                For i As Integer = 0 To Items.Count - 1
                    Dim irect As Rectangle = GetItemRectangle(i)
                    If (e.ClipRectangle.IntersectsWith(irect)) Then

                        If ((SelectionMode = SelectionMode.One AndAlso SelectedIndex = i) _
                             OrElse (SelectionMode = SelectionMode.MultiSimple AndAlso SelectedIndices.Contains(i)) _
                             OrElse (SelectionMode = SelectionMode.MultiExtended AndAlso SelectedIndices.Contains(i))) Then

                            OnDrawItem(New DrawItemEventArgs(e.Graphics, Font,
                                irect, i, DrawItemState.Selected, ForeColor, BackColor))
                        Else
                            OnDrawItem(New DrawItemEventArgs(e.Graphics, Font,
                                irect, i, DrawItemState.Default, ForeColor, BackColor))
                        End If

                        iRegion.Complement(irect)
                    End If
                Next i
            End If

            MyBase.OnPaint(e)
        End Sub

    End Class

End Namespace
