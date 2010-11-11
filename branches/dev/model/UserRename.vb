Namespace Huggle

    'Represents a user rename

    Friend Class UserRename : Inherits LogItem

        Private _Destination, _Source As String

        Public Sub New(ByVal time As Date, ByVal source As String, ByVal destination As String, _
            ByVal user As User, ByVal comment As String, ByVal id As Integer, ByVal rcid As Integer)

            MyBase.New(user.Wiki, id, rcid)
            Me.Action = "rename"
            Me.Comment = Comment
            Me.Time = time
            Me.User = User

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
