Imports System
Imports System.Collections.Generic

Namespace Huggle.Wikitext

    Public Class FileLink

        'Represents a file link

        Private _File As File, _Selection As Selection

        Public Sub New(ByVal File As File, ByVal Selection As Selection)
            _File = File
            _Selection = Selection
        End Sub

        Public ReadOnly Property File() As File
            Get
                Return _File
            End Get
        End Property

        Public ReadOnly Property Selection() As Selection
            Get
                Return _Selection
            End Get
        End Property

        Public Overrides Function ToString() As String
            Return "[[" & File.Page.Title & "]]"
        End Function

    End Class

    Public Class FileLinkCollection

        Private Document As Document
        Private Items As List(Of FileLink)

        Public Sub New(ByVal Document As Document)
            Me.Document = Document
        End Sub

        Public ReadOnly Property All() As IList(Of FileLink)
            Get
                Return Items.AsReadOnly
            End Get
        End Property

        Public ReadOnly Property Contains(ByVal Item As FileLink) As Boolean
            Get
                Return (Items.Contains(Item))
            End Get
        End Property

        Public ReadOnly Property Count() As Integer
            Get
                Return All.Count
            End Get
        End Property

        Default Public ReadOnly Property Item(ByVal Index As Integer) As FileLink
            Get
                If Items.Count > Index Then Return Items(Index) Else Return Nothing
            End Get
        End Property

        Public Sub Remove(ByVal Index As Integer)
            Document.Text = Document.Text.Remove(All(Index).Selection)
        End Sub

    End Class



End Namespace