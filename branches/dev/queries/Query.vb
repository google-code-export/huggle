Namespace Huggle.Actions

    Public MustInherit Class Query : Inherits Process

        Private _Session As Session

        Protected Sub New(ByVal session As Session, ByVal description As String)
            _Session = session
            MyBase.Description = description
        End Sub

        Public Property Session() As Session
            Get
                Return _Session
            End Get
            Protected Set(ByVal value As Session)
                _Session = value
            End Set
        End Property

        Public ReadOnly Property User() As User
            Get
                If Session Is Nothing Then Return Nothing
                Return Session.User
            End Get
        End Property

        Public ReadOnly Property Wiki() As Wiki
            Get
                If Session Is Nothing Then Return Nothing
                Return Session.User.Wiki
            End Get
        End Property

    End Class

End Namespace
