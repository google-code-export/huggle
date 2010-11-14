Imports System

Namespace Huggle

    'Represents an unrecognized log item

    Friend Class UnknownLogItem : Inherits LogItem

        Public Sub New(ByVal id As Integer, ByVal wiki As Wiki)
            MyBase.New(id, wiki)
        End Sub

        Public Overrides ReadOnly Property Description As String
            Get
                Return "{0}(?) {1}".FormatI(Action, Id)
            End Get
        End Property

        Public Property Title As String

        Public Overrides ReadOnly Property Target As String
            Get
                If Title Is Nothing Then Return Id.ToStringI
                Return Title
            End Get
        End Property

    End Class

End Namespace