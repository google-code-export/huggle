Imports System.Collections.Generic
Imports System.Windows.Forms

Namespace Huggle.UI

    'This should be abstract, but for some reason the Windows Forms designer
    'cannot design controls that inherit from abstract base types

    Public Class HuggleForm : Inherits Form

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

        Private Sub _HandleCreated() Handles Me.HandleCreated
            _IsAvailable = True
        End Sub

        Private Sub _HandleDestroyed() Handles Me.HandleDestroyed
            _IsAvailable = False
        End Sub

    End Class

End Namespace