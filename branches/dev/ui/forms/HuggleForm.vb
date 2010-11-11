Imports System
Imports System.Collections.Generic
Imports System.Windows.Forms

Namespace Huggle.UI

    'This should be abstract, but for some reason the Windows Forms designer
    'cannot design controls that inherit from abstract base types

    Friend Class HuggleForm : Inherits Form

        Private _IsAvailable As Boolean

        Public Sub New()

        End Sub

        Public Overridable Function GetKey() As String
            Return Nothing
        End Function

        Public Overridable Function GetState() As Dictionary(Of String, Object)
            Return Nothing
        End Function

        Public Overridable Sub RestoreState()

        End Sub

        Protected ReadOnly Property IsAvailable As Boolean
            Get
                Return _IsAvailable
            End Get
        End Property

        Protected Overrides Sub OnHandleCreated(ByVal e As EventArgs)
            _IsAvailable = True
            MyBase.OnHandleCreated(e)
        End Sub

        Protected Overrides Sub OnHandleDestroyed(ByVal e As EventArgs)
            _IsAvailable = False
            MyBase.OnHandleDestroyed(e)
        End Sub

        Protected Overrides Sub OnResizeBegin(ByVal e As EventArgs)
            DoResizeBegin(Me)
            MyBase.OnResizeBegin(e)
        End Sub

        Protected Overrides Sub OnResizeEnd(ByVal e As EventArgs)
            DoResizeEnd(Me)
            MyBase.OnResizeEnd(e)
        End Sub

        Private Sub DoResizeBegin(ByVal control As Control)
            Dim listView As EnhancedListView = TryCast(control, EnhancedListView)
            If listView IsNot Nothing Then listView.BeginResize()

            For Each child As Control In control.Controls
                DoResizeBegin(child)
            Next child
        End Sub

        Private Sub DoResizeEnd(ByVal control As Control)
            Dim listView As EnhancedListView = TryCast(control, EnhancedListView)
            If listView IsNot Nothing Then listView.EndResize()

            For Each child As Control In control.Controls
                DoResizeEnd(child)
            Next child
        End Sub

    End Class

End Namespace