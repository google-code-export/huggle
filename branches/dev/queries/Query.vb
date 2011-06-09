Namespace Huggle.Queries

    Friend MustInherit Class Query : Inherits Process

        Protected Sub New(ByVal session As Session, ByVal description As String)
            ThrowNull(session, "session")
            ThrowNull(description, "description")

            _Description = description
            _Session = session
        End Sub

        Public Property Description As String

        Public Property Session() As Session

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
