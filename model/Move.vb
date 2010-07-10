Namespace Huggle

    Public Class Move : Inherits LogItem

        'Represents a page move

        Private _Destination As String
        Private _Source As String

        Public Sub New(ByVal time As Date, ByVal source As String, ByVal destination As String, _
            ByVal user As User, ByVal comment As String, ByVal id As Integer, ByVal rcid As Integer)

            MyBase.New(user.Wiki, id, rcid)
            Me.Action = "move"
            Me.Comment = Comment
            Me.Time = time
            Me.User = user

            _Destination = destination
            _Source = source
        End Sub

        Public ReadOnly Property Destination() As String
            Get
                Return _Destination
            End Get
        End Property

        Public ReadOnly Property Source() As String
            Get
                Return _Source
            End Get
        End Property

        Public Overrides ReadOnly Property Target() As String
            Get
                Return _Source
            End Get
        End Property

    End Class

End Namespace
