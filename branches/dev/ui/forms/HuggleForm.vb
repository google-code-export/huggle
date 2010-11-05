Imports System.Collections.Generic
Imports System.Windows.Forms

Namespace Huggle.UI

    'This should be abstract, but for some reason the Windows Forms designer
    'cannot design controls that inherit from abstract base types

    Friend Class HuggleForm : Inherits Form

        Private _IsAvailable As Boolean

        Friend Sub New()

        End Sub

        Friend Overridable Function GetKey() As String
            Return Nothing
        End Function

        Friend Overridable Function GetState() As Dictionary(Of String, Object)
            Return Nothing
        End Function

        Friend Overridable Sub RestoreState()

        End Sub

        Protected ReadOnly Property IsAvailable As Boolean
            Get
                Return _IsAvailable
            End Get
        End Property

        Private Sub _HandleCreated() Handles Me.HandleCreated
            _IsAvailable = True
        End Sub

        Private Sub _HandleDestroyed() Handles Me.HandleDestroyed
            _IsAvailable = False
        End Sub

    End Class

End Namespace