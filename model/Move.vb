Namespace Huggle

    Friend Class Move : Inherits LogItem

        'Represents a page move

        Public Sub New(ByVal id As Integer, ByVal wiki As Wiki)
            MyBase.New(id, wiki)
        End Sub

        Public Property DestinationTitle() As String

        Public Property IsOverRedirect As Boolean
        Public Property LeaveRedirect As Boolean = True

        Public Property SourceTitle() As String

        Public Overrides ReadOnly Property Target() As String
            Get
                Return Page.Title
            End Get
        End Property

        Protected Overrides Sub OnSetPage()
        End Sub

    End Class

End Namespace
