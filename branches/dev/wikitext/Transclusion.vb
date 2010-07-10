Imports System
Imports System.Collections.Generic

Namespace Huggle.Wikitext

    <Diagnostics.DebuggerDisplay("{Wikitext}")> _
    Public Class Transclusion

        Private Document As Document

        Private _Linebreaks As Boolean
        Private _Page As Page
        Private _Parameters As ParameterCollection
        Private _ParamSelections As Dictionary(Of String, Selection)
        Private _Selection As Selection
        Private _Spaces As Boolean
        Private _Subst As Boolean

        Public Sub New(ByVal Document As Document, ByVal Page As Page, ByVal Parameters As List(Of Parameter))
            Me.Document = Document
            _Page = Page
            _Parameters = New ParameterCollection(Me, Parameters)
        End Sub

        Public Sub New(ByVal Wiki As Wiki, ByVal TemplateName As String, ByVal ParamArray Parameters As Object())
            _Page = Wiki.Pages(Wiki.Spaces.Template, TemplateName)
            _Parameters = New ParameterCollection(Me, Parameters)
        End Sub

        Public Property Linebreaks() As Boolean
            Get
                Return _Linebreaks
            End Get
            Set(ByVal value As Boolean)
                _Linebreaks = value

                Document.Text.Remove(Selection)
                Document.Text.Insert(Selection.Start, Wikitext)
            End Set
        End Property

        Public ReadOnly Property Page() As Page
            Get
                Return _Page
            End Get
        End Property

        Public ReadOnly Property Parameters() As ParameterCollection
            Get
                Return _Parameters
            End Get
        End Property

        Public ReadOnly Property ParamSelections() As Dictionary(Of String, Selection)
            Get
                Return _ParamSelections
            End Get
        End Property

        Public ReadOnly Property Selection() As Selection
            Get
                Return _Selection
            End Get
        End Property

        Public Property Spaces() As Boolean
            Get
                Return _Spaces
            End Get
            Set(ByVal value As Boolean)
                _Spaces = value

                Document.Text.Remove(Selection)
                Document.Text.Insert(Selection.Start, Wikitext)
            End Set
        End Property

        Public Property Subst() As Boolean
            Get
                Return _Subst
            End Get
            Set(ByVal value As Boolean)
                _Subst = value

                Document.Text.Remove(Selection)
                Document.Text.Insert(Selection.Start, Wikitext)
            End Set
        End Property

        Public ReadOnly Property Wikitext() As String
            Get
                Dim Result As String = "{{"
                If Subst Then Result &= "subst:"
                Result &= Parsing.TransclusionName(Page)

                If Linebreaks Then Result &= LF

                For i As Integer = 0 To Parameters.Count
                    Result &= "|"

                    Dim Name As String = Parameters(i).Name
                    Dim Value As String = Parameters(i).Value
                    Dim p As Integer

                    'Determine whether parameter is numbered
                    'Use explicit number for numbered parameter if needed
                    If Name Is Nothing Then Name = CStr(i + 1)

                    If Not (Integer.TryParse(Name, p) AndAlso p = i + 1 AndAlso Not Value.Contains("=")) _
                        Then Result &= Name & "="

                    Result &= Value
                    If Linebreaks Then Result &= LF
                Next i

                Result &= "}}"

                Return Result
            End Get
        End Property

        Public Overrides Function ToString() As String
            Return Wikitext
        End Function

    End Class

    Public Class TransclusionCollection

        Private Document As Document
        Private Items As List(Of Transclusion)

        Public Sub New(ByVal Document As Document)
            Me.Document = Document
        End Sub

        Public ReadOnly Property All() As IList(Of Transclusion)
            Get
                Return Items.AsReadOnly
            End Get
        End Property

        Public ReadOnly Property Contains(ByVal Item As Transclusion) As Boolean
            Get
                Return (Items.Contains(Item))
            End Get
        End Property

        Public ReadOnly Property Contains(ByVal Page As Page) As Boolean
            Get
                Return (First(Page) IsNot Nothing)
            End Get
        End Property

        Public ReadOnly Property Count() As Integer
            Get
                Return Items.Count
            End Get
        End Property

        Public ReadOnly Property First(ByVal Page As Page) As Transclusion
            Get
                For Each Item As Transclusion In Items
                    If Item.Page Is Page Then Return Item
                Next Item

                Return Nothing
            End Get
        End Property

        Default Public ReadOnly Property Item(ByVal Index As Integer) As Transclusion
            Get
                If Items.Count > Index Then Return Items(Index) Else Return Nothing
            End Get
        End Property

        Public Sub Append(ByVal Transclusion As Transclusion)
            Insert(Transclusion, Document.Text.Length)
        End Sub

        Public Sub Insert(ByVal Transclusion As Transclusion, ByVal Index As Integer)

        End Sub

        Public Sub Prepend(ByVal Transclusion As Transclusion)
            Insert(Transclusion, 0)
        End Sub

        Public Sub Remove(ByVal Index As Integer)
            Document.Text = Document.Text.Remove(Items(Index).Selection)
        End Sub

    End Class

    <Diagnostics.DebuggerDisplay("{Wikitext}")> _
    Public Class Parameter

        Private Document As Document

        Private _Name As String
        Private _Selection As Selection
        Private _Value As String

        Public Sub New(ByVal Name As String, ByVal Value As String)
            _Name = Name
            _Value = Value
        End Sub

        Public Sub New(ByVal Document As Document, ByVal Name As String, _
            ByVal Value As String, ByVal Selection As Selection)

            Me.Document = Document

            _Name = Name
            _Selection = Selection
            _Value = Value
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

        Public ReadOnly Property Value() As String
            Get
                Return _Value
            End Get
        End Property

        Public ReadOnly Property Wikitext() As String
            Get
                If Name Is Nothing Then Return Value Else Return Name & "=" & Value
            End Get
        End Property

        Public Overrides Function ToString() As String
            Return Wikitext
        End Function

    End Class

    Public Class ParameterCollection

        Private Document As Document
        Private Items As List(Of Parameter)

        Public Sub New(ByVal Document As Document, ByVal Items As List(Of Parameter))
            Me.Document = Document
            If Items Is Nothing Then Me.Items = New List(Of Parameter) Else Me.Items = Items
        End Sub

        Public Sub New(ByVal ParamArray Values() As Object)
            For i As Integer = 0 To (Values.Length \ 2) - 2 Step 2
                Items.Add(New Parameter(Values(i).ToString, Values(i + 1).ToString))
            Next i
        End Sub

        Public ReadOnly Property All() As IList(Of Parameter)
            Get
                Return Items.AsReadOnly
            End Get
        End Property

        Public ReadOnly Property Contains(ByVal Name As String) As Boolean
            Get
                Return (Item(Name) IsNot Nothing)
            End Get
        End Property

        Public ReadOnly Property Count() As Integer
            Get
                Return Items.Count
            End Get
        End Property

        Default Public ReadOnly Property Item(ByVal Index As Integer) As Parameter
            Get
                If Items.Count > Index Then Return Items(Index) Else Return Nothing
            End Get
        End Property

        Default Public ReadOnly Property Item(ByVal Name As String) As Parameter
            Get
                For Each Param As Parameter In Items
                    If Param.Name = Name Then Return Param
                Next Param

                Return Nothing
            End Get
        End Property

        Public Sub Remove(ByVal Name As String)
            If Item(Name) Is Nothing Then Return


        End Sub

        Public Sub [Set](ByVal Name As String, ByVal Value As String)

        End Sub

    End Class

End Namespace
