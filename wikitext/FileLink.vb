Imports System
Imports System.Collections.Generic

Namespace Huggle.Wikitext

    <Diagnostics.DebuggerDisplay("{File}")>
    Public Class FileLink

        'Represents a file link

        Private _File As File
        Private _Selection As Selection

        Public Sub New(ByVal file As File, ByVal selection As Selection)
            _File = file
            _Selection = selection
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

        Public Sub New(ByVal document As Document)
            Me.Document = document
        End Sub

        Public ReadOnly Property All() As IList(Of FileLink)
            Get
                Return Items.AsReadOnly
            End Get
        End Property

        Public ReadOnly Property Contains(ByVal item As FileLink) As Boolean
            Get
                Return (Items.Contains(item))
            End Get
        End Property

        Public ReadOnly Property Count() As Integer
            Get
                Return All.Count
            End Get
        End Property

        Default Public ReadOnly Property Item(ByVal index As Integer) As FileLink
            Get
                If Items.Count > index Then Return Items(index) Else Return Nothing
            End Get
        End Property

        Public Sub Remove(ByVal index As Integer)
            Document.Text = Document.Text.Remove(All(index).Selection)
        End Sub

    End Class



End Namespace