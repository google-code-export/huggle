Imports System
Imports System.Collections.Generic

Namespace Huggle.Wikitext

    <Diagnostics.DebuggerDisplay("{File}")>
    Friend Class FileLink

        'Represents a file link

        Private _File As File
        Private _Selection As Selection

        Friend Sub New(ByVal file As File, ByVal selection As Selection)
            _File = file
            _Selection = selection
        End Sub

        Friend ReadOnly Property File() As File
            Get
                Return _File
            End Get
        End Property

        Friend ReadOnly Property Selection() As Selection
            Get
                Return _Selection
            End Get
        End Property

        Public Overrides Function ToString() As String
            Return "[[" & File.Page.Title & "]]"
        End Function

    End Class

    Friend Class FileLinkCollection

        Private Document As Document
        Private Items As List(Of FileLink)

        Friend Sub New(ByVal document As Document)
            Me.Document = document
        End Sub

        Friend ReadOnly Property All() As IList(Of FileLink)
            Get
                Return Items.AsReadOnly
            End Get
        End Property

        Friend ReadOnly Property Contains(ByVal item As FileLink) As Boolean
            Get
                Return (Items.Contains(item))
            End Get
        End Property

        Friend ReadOnly Property Count() As Integer
            Get
                Return All.Count
            End Get
        End Property

        Default Friend ReadOnly Property Item(ByVal index As Integer) As FileLink
            Get
                If Items.Count > index Then Return Items(index) Else Return Nothing
            End Get
        End Property

        Friend Sub Remove(ByVal index As Integer)
            Document.Text = Document.Text.Remove(All(index).Selection)
        End Sub

    End Class



End Namespace