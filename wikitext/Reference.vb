Namespace Huggle.Wikitext

    Public Class Reference

        Private _Name, _Text As String, _Selection As Selection

        Public Sub New(ByVal Name As String, ByVal Text As String, ByVal Selection As Selection)
            _Name = Name
            _Text = Text
            _Selection = Selection
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
