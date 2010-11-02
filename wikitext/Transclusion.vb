Imports System
Imports System.Collections.Generic

Namespace Huggle.Wikitext

    <Diagnostics.DebuggerDisplay("{Wikitext}")>
    Public Class Transclusion

        Private Document As Document

        Private _Linebreaks As Boolean
        Private _Page As Page
        Private _Parameters As ParameterCollection
        Private _ParamSelections As Dictionary(Of String, Selection)
        Private _Selection As Selection
        Private _Spaces As Boolean
        Private _Subst As Boolean

        Public Sub New(ByVal document As Document, ByVal page As Page, ByVal parameters As List(Of Parameter))
            Me.Document = document
            _Page = page
            _Parameters = New ParameterCollection(Me, parameters)
        End Sub

        Public Sub New(ByVal wiki As Wiki, ByVal templateName As String, ByVal ParamArray parameters As Object())
            _Page = wiki.Pages(wiki.Spaces.Template, templateName)
            _Parameters = New ParameterCollection(Me, parameters)
        End Sub

        Public Property Linebreaks() As Boolean
            Get
                Return _Linebreaks
            End Get
            Set(ByVal value As Boolean)
                _Linebreaks = value

                Document.Text = Document.Text.Remove(Selection)
                Document.Text = Document.Text.Insert(Selection.Start, Wikitext)
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

                Document.Text = Document.Text.Remove(Selection)
                Document.Text = Document.Text.Insert(Selection.Start, Wikitext)
            End Set
        End Property

        Public Property Subst() As Boolean
            Get
                Return _Subst
            End Get
            Set(ByVal value As Boolean)
                _Subst = value

                Document.Text = Document.Text.Remove(Selection)
                Document.Text = Document.Text.Insert(Selection.Start, Wikitext)
            End Set
        End Property

        Public ReadOnly Property Wikitext() As String
            Get
                Dim result As String = "{{"
                If Subst Then result &= "subst:"
                result &= Parsing.TransclusionName(Page)

                If Linebreaks Then result &= LF

                For i As Integer = 0 To Parameters.Count
                    result &= "|"

                    Dim name As String = Parameters(i).Name
                    Dim value As String = Parameters(i).Value
                    Dim p As Integer

                    'Determine whether parameter is numbered
                    'Use explicit number for numbered parameter if needed
                    If name Is Nothing Then name = CStr(i + 1)

                    If Not (Integer.TryParse(name, p) AndAlso p = i + 1 AndAlso Not value.Contains("=")) _
                        Then result &= name & "="

                    result &= value
                    If Linebreaks Then result &= LF
                Next i

                result &= "}}"

                Return result
            End Get
        End Property

        Public Overrides Function ToString() As String
            Return Wikitext
        End Function

    End Class

    Public Class TransclusionCollection

        Private Document As Document
        Private Items As List(Of Transclusion)

        Public Sub New(ByVal document As Document)
            Me.Document = document
        End Sub

        Public ReadOnly Property All() As IList(Of Transclusion)
            Get
                Return Items.AsReadOnly
            End Get
        End Property

        Public ReadOnly Property Contains(ByVal item As Transclusion) As Boolean
            Get
                Return (Items.Contains(item))
            End Get
        End Property

        Public ReadOnly Property Contains(ByVal page As Page) As Boolean
            Get
                Return (First(page) IsNot Nothing)
            End Get
        End Property

        Public ReadOnly Property Count() As Integer
            Get
                Return Items.Count
            End Get
        End Property

        Public ReadOnly Property First(ByVal page As Page) As Transclusion
            Get
                For Each transclusion As Transclusion In Items
                    If transclusion.Page Is page Then Return transclusion
                Next transclusion

                Return Nothing
            End Get
        End Property

        Default Public ReadOnly Property Item(ByVal index As Integer) As Transclusion
            Get
                If Items.Count > index Then Return Items(index) Else Return Nothing
            End Get
        End Property

        Public Sub Append(ByVal transclusion As Transclusion)
            Insert(transclusion, Document.Text.Length)
        End Sub

        Public Sub Insert(ByVal transclusion As Transclusion, ByVal index As Integer)

        End Sub

        Public Sub Prepend(ByVal transclusion As Transclusion)
            Insert(transclusion, 0)
        End Sub

        Public Sub Remove(ByVal index As Integer)
            Document.Text = Document.Text.Remove(Items(index).Selection)
        End Sub

    End Class

    <Diagnostics.DebuggerDisplay("{Wikitext}")>
    Public Class Parameter

        Private Document As Document

        Private _Name As String
        Private _Selection As Selection
        Private _Value As String

        Public Sub New(ByVal name As String, ByVal value As String)
            _Name = name
            _Value = value
        End Sub

        Public Sub New(ByVal document As Document, ByVal name As String,
            ByVal value As String, ByVal selection As Selection)

            Me.Document = document

            _Name = name
            _Selection = selection
            _Value = value
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

        Public Sub New(ByVal document As Document, ByVal items As List(Of Parameter))
            Me.Document = document
            If items Is Nothing Then Me.Items = New List(Of Parameter) Else Me.Items = items
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

        Public ReadOnly Property Contains(ByVal name As String) As Boolean
            Get
                Return (Item(name) IsNot Nothing)
            End Get
        End Property

        Public ReadOnly Property Count() As Integer
            Get
                Return Items.Count
            End Get
        End Property

        Default Public ReadOnly Property Item(ByVal index As Integer) As Parameter
            Get
                If Items.Count > index Then Return Items(index) Else Return Nothing
            End Get
        End Property

        Default Public ReadOnly Property Item(ByVal name As String) As Parameter
            Get
                For Each param As Parameter In Items
                    If param.Name = name Then Return param
                Next param

                Return Nothing
            End Get
        End Property

        Public Sub Remove(ByVal name As String)
            If Item(name) Is Nothing Then Return


        End Sub

        Public Sub [Set](ByVal name As String, ByVal value As String)

        End Sub

    End Class

End Namespace
