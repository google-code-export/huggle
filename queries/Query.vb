Namespace Huggle.Actions

    Friend MustInherit Class Query : Inherits Process

        Protected Sub New(ByVal session As Session, ByVal description As String)
            _Session = session
            MyBase.Description = description
        End Sub

        Friend Property Session() As Session

        Friend ReadOnly Property User() As User
            Get
                If Session Is Nothing Then Return Nothing
                Return Session.User
            End Get
        End Property

        Friend ReadOnly Property Wiki() As Wiki
            Get
                If Session Is Nothing Then Return Nothing
                Return Session.User.Wiki
            End Get
        End Property

    End Class

End Namespace
