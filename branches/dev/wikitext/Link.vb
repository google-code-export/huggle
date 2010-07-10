Imports System

Namespace Huggle.Wikitext

    <Diagnostics.DebuggerDisplay("{Page}")> _
    Public Class Link

        Private Document As Document
        Private _Page As Page, _Selection As Selection, _Text As String

        Public Sub New(ByVal Document As Document, ByVal Page As Page, _
            ByVal Selection As Selection, ByVal Text As String)

            Me.Document = Document
            _Page = Page
            _Selection = Selection
            _Text = Text
        End Sub

        Public ReadOnly Property Page() As Page
            Get
                Return _Page
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

        Public Overrides Function ToString() As String
            Return "[[" & Parsing.LinkName(Page) & If(Text Is Nothing, "", Text) & "]]"
        End Function

    End Class

    Public Class LinkCollection

        Private Document As Document

        Public Sub New(ByVal Document As Document)
            Me.Document = Document
        End Sub

    End Class

End Namespace
