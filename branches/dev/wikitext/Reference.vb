Namespace Huggle.Wikitext

    Friend Class Reference

        Private _Name As String
        Private _Selection As Selection
        Private _Text As String

        Public Sub New(ByVal name As String, ByVal text As String, ByVal selection As Selection)
            _Name = name
            _Text = text
            _Selection = selection
        End Sub

        Public ReadOnly Property Name() As String
            Get
                Return _Name
            End Get
        End Property

        Public ReadOnly Property Selection() As Selection
            Get
                Return _Selection
            End Get
        End Property

        Public ReadOnly Property Text() As String
            Get
                Return _Text
            End Get
        End Property

    End Class

End Namespace
