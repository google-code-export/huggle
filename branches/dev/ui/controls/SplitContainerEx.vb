Namespace System.Windows.Forms

    Public Class EnhancedSplitContainer : Inherits SplitContainer

        Private Sub _MouseDown() Handles Me.MouseDown
            IsSplitterFixed = True
        End Sub

        Private Sub _MouseUp() Handles Me.MouseUp
            IsSplitterFixed = False
        End Sub

        Private Sub _MouseMove(ByVal sender As Object, ByVal e As MouseEventArgs) Handles Me.MouseMove
            If IsSplitterFixed Then
                If (e.Button = MouseButtons.Left) Then
                    If Orientation = Orientation.Vertical Then
                        If e.X > 0 AndAlso e.X < Width Then
                            SplitterDistance = e.X
                            Refresh()
                        End If
                    Else
                        If e.Y > 0 AndAlso e.Y < Height Then
                            SplitterDistance = e.Y
                            Refresh()
                        End If
                    End If
                Else
                    IsSplitterFixed = False
                End If
            End If
        End Sub

    End Class

End Namespace