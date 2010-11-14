Namespace Huggle

    'Represents a user rename

    Friend Class UserRename : Inherits LogItem

        Public Sub New(ByVal id As Integer, ByVal wiki As Wiki)
            MyBase.New(id, wiki)
        End Sub

        Public ReadOnly Property DestinationName() As String
            Get
                Return TargetUser.Name
            End Get
        End Property

        Public ReadOnly Property SourceName() As String
            Get
                Return Page.Owner.Name
            End Get
        End Property

        Public Overrides ReadOnly Property Target() As String
            Get
                Return TargetUser.Name
            End Get
        End Property

    End Class

End Namespace
