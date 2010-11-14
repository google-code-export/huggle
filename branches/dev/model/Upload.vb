Imports System.Collections.Generic

Namespace Huggle

    'Represents a media upload

    Friend Class Upload : Inherits LogItem

        Public Sub New(ByVal id As Integer, ByVal wiki As Wiki)
            MyBase.New(id, wiki)
        End Sub

        Public ReadOnly Property File() As File
            Get
                Return Wiki.Files.Item(Page)
            End Get
        End Property

        Public Overrides ReadOnly Property Target() As String
            Get
                Return File.Name
            End Get
        End Property

    End Class

End Namespace
