Imports Huggle.Actions
Imports System
Imports System.Collections
Imports System.Collections.Generic

Namespace Huggle.Scripting

    <Diagnostics.DebuggerDisplay("{Value}")> _
    Friend Class Token

        'Represents a symbol or value during parsing and evaluation

        Friend Sub New(ByVal value As Object, Optional ByVal type As TokenType = TokenType.Constant)
            Me.Value = value
            Me.Type = type
        End Sub

        Friend Property Arguments() As Integer

        Friend Function AsBool() As Boolean
            Return [As](Of Boolean)()
        End Function

        Friend Function AsDictionary() As Hashtable
            Return [As](Of Hashtable)()
        End Function

        Friend Function AsList() As ArrayList
            Return [As](Of ArrayList)()
        End Function

        Friend Function AsMedia() As File
            Return [As](Of File)()
        End Function

        Friend Function AsNumber() As Double
            If TypeOf Value Is Double Then Return CDbl(Value)
            If TypeOf Value Is Integer Then Return CInt(Value)
            If TypeOf Value Is Long Then Return CLng(Value)
            Throw New ScriptException(Msg("query-typemismatch", "Number", ValueType.ToString))
        End Function

        Friend Function AsPage() As Page
            Return [As](Of Page)()
        End Function

        Friend Function AsQItem() As QueueItem
            Return [As](Of QueueItem)()
        End Function

        Friend Function AsRevision() As Revision
            Return [As](Of Revision)()
        End Function

        Friend Function AsSpace() As Space
            Return [As](Of Space)()
        End Function

        Friend Function AsString() As String
            If TypeOf Value Is String Then Return CStr(Value)
            If Value IsNot Nothing Then Return Value.ToString
            Throw New ScriptException(Msg("query-typemismatch", "String", ValueType.ToString))
        End Function

        Friend Function AsTime() As Date
            Return [As](Of Date)()
        End Function

        Friend Function AsUser() As User
            Return [As](Of User)()
        End Function

        Friend Property FirstChild() As Token

        Friend Property NextSibling() As Token

        Friend Property Parent() As Token

        Friend Property Precedence() As Integer

        Friend Property RightAssociative() As Boolean

        Friend Property Type() As TokenType

        Private Function [As](Of T)() As T
            If TypeOf Value Is T Then Return CType(Value, T)
            Throw New ScriptException(Msg("query-typemismatch", GetType(T).Name, ValueType.ToString))
        End Function

        Friend Property Value() As Object

        Friend ReadOnly Property ValueType() As String
            Get
                If TypeOf Value Is Double OrElse TypeOf Value Is Integer OrElse TypeOf Value Is Long Then
                    Return "Number"
                ElseIf TypeOf Value Is ArrayList Then
                    Return "List"
                ElseIf TypeOf Value Is Hashtable Then
                    Return "Dictionary"
                ElseIf TypeOf Value Is Date Then
                    Return "Time"
                ElseIf TypeOf Value Is Func Then
                    Return "Function"
                ElseIf Value Is Nothing Then
                    Return "null"
                Else
                    Return Value.GetType.Name
                End If
            End Get
        End Property

    End Class

    Friend Class ItemComparer : Implements IComparer

        Friend Function Compare(ByVal x As Object, ByVal y As Object) As Integer Implements IComparer.Compare
            If x Is Nothing Then Return -1
            If y Is Nothing Then Return 1

            If TypeOf x Is String Then Return String.Compare(CStr(x), CStr(y), StringComparison.Ordinal)
            If TypeOf x Is Boolean Then If CBool(x) Then Return 1 Else If CBool(y) Then Return -1 Else Return 0

            If (TypeOf x Is Short OrElse TypeOf x Is Integer OrElse TypeOf x Is Long OrElse TypeOf x Is Single _
                OrElse TypeOf x Is Double) AndAlso (TypeOf y Is Short OrElse TypeOf y Is Integer _
                OrElse TypeOf y Is Long OrElse TypeOf y Is Single OrElse TypeOf y Is Double) _
                Then Return Math.Sign(CDbl(y) - CDbl(x))

            If TypeOf x Is Range AndAlso TypeOf y Is Range _
                Then Return Math.Sign(CType(x, Range).Lower - CType(y, Range).Lower)

            Return String.Compare(x.ToString, y.ToString, StringComparison.Ordinal)
        End Function

    End Class

    Friend Structure FunctionData
        Dim Count As Integer
        Dim [Function] As Token
    End Structure

    Friend Enum TokenType As Integer
        Constant
        [Operator]
        UnaryOperator
        [Function]
        EmptyFunction
        Argument
        Identifier
        GroupStart
        GroupEnd
        ArgCount
    End Enum

    Friend Class Func

        Friend ArgNames As New List(Of String)
        Friend Token As Token

        Public Overrides Function ToString() As String
            Return "function(" & ArgNames.Join(", ") & ")"
        End Function

    End Class

    <Serializable()>
    Friend Class ScriptException : Inherits HuggleException

        'Represents an error raised during parsing or evaluation of a script

        Friend Sub New(ByVal message As String)
            MyBase.New(message)
        End Sub

    End Class

    <Serializable()>
    Friend Class TaskCancelledException : Inherits HuggleException

        Friend Sub New()
            MyBase.New(Msg("script-usercancelled"))
        End Sub

    End Class

    Friend Class EvalDoneEventArgs : Inherits EventArgs

        Private _Result As Result

        Friend Sub New(ByVal result As Result)
            _Result = result
        End Sub

        Friend ReadOnly Property Result() As Result
            Get
                Return _Result
            End Get
        End Property

    End Class

    Friend Class EvalProgressEventArgs : Inherits EventArgs

        Private _Message As String

        Friend Sub New(ByVal message As String)
            _Message = message
        End Sub

        Friend ReadOnly Property Message() As String
            Get
                Return _Message
            End Get
        End Property

    End Class

    Friend Class ScriptTable

        Friend Property Columns As List(Of String)
        Friend Property ColumnTypes As List(Of String)
        Friend Property Rows As List(Of ScriptTableRow)

    End Class

    Friend Class ScriptTableRow

        Friend Property Columns As List(Of String)
        Friend Property Items As ArrayList

    End Class

    Friend MustInherit Class Pipe : Implements IEnumerable(Of Object)

        Private _Description As String
        Private _Evaluator As Evaluator

        Protected Sub New(ByVal evaluator As Evaluator, ByVal description As String)
            _Description = description
            _Evaluator = evaluator
        End Sub

        Friend ReadOnly Property Description() As String
            Get
                Return _Description
            End Get
        End Property

        Protected ReadOnly Property Evaluator() As Evaluator
            Get
                Return _Evaluator
            End Get
        End Property

        Friend Function ReadAll() As ArrayList
            Evaluator.DoProgress(Msg("eval-stream", Description))

            Dim result As New ArrayList

            For Each item As Object In Me
                result.Add(item)
                If Evaluator.IsCancelled Then Throw New ScriptException(Msg("error-cancelled"))
            Next item

            Return result
        End Function

        Friend MustOverride Function GetEnumerator() As IEnumerator(Of Object) Implements IEnumerable(Of Object).GetEnumerator

        Friend Function GetEnumerator1() As IEnumerator Implements IEnumerable.GetEnumerator
            Return GetEnumerator()
        End Function

    End Class

    Friend MustInherit Class StreamEnumerator : Implements IEnumerator(Of Object)

        Protected Sub New()
        End Sub

        Friend MustOverride ReadOnly Property Current() As Object Implements IEnumerator(Of Object).Current, IEnumerator.Current
        Friend MustOverride Function MoveNext() As Boolean Implements IEnumerator.MoveNext
        Friend MustOverride Sub Reset() Implements IEnumerator.Reset

        Friend Sub Dispose() Implements IDisposable.Dispose
            Dispose(True)
            GC.SuppressFinalize(Me)
        End Sub

        Private Sub Dispose(ByVal disposing As Boolean)
        End Sub

    End Class

    Friend Class QueryPipe : Inherits Pipe

        Private Query As ListQuery

        Friend Sub New(ByVal evaluator As Evaluator, ByVal query As ListQuery)
            MyBase.New(evaluator, query.Description)
            Me.Query = query
        End Sub

        Friend Overrides Function GetEnumerator() As IEnumerator(Of Object)
            Return New QueryPipeEnumerator(Query)
        End Function

    End Class

    Friend Class QueryPipeEnumerator : Inherits StreamEnumerator

        Private _Current As Object
        Private Index As Integer
        Private Query As ListQuery

        Friend Sub New(ByVal query As ListQuery)
            Me.Query = query
        End Sub

        Friend Overrides ReadOnly Property Current() As Object
            Get
                Return _Current
            End Get
        End Property

        Friend Overrides Function MoveNext() As Boolean
            If Query.Items.Count <= Index Then
                If Query.IsAtEnd Then Return False
                Query.DoOne()
                If Query.Result.IsError Then Throw New ScriptException(Query.Result.LogMessage)
            End If

            If Index = Query.Items.Count AndAlso Query.IsAtEnd Then Return False

            _Current = Query.Items(Index)
            Index += 1
            Return True
        End Function

        Friend Overrides Sub Reset()
            Index = 0
        End Sub

    End Class

    Friend Class TransformPipe : Inherits Pipe

        Private Source As IEnumerable
        Private Transform As Token

        Friend Sub New(ByVal evaluator As Evaluator, ByVal source As IEnumerable, ByVal transform As Token)
            MyBase.New(evaluator, "Transform")
            Me.Source = source
            Me.Transform = transform
        End Sub

        Friend Overrides Function GetEnumerator() As IEnumerator(Of Object)
            Return New TransformPipeEnumerator(Evaluator, Source.GetEnumerator, Transform)
        End Function

    End Class

    Friend Class TransformPipeEnumerator : Inherits StreamEnumerator

        Private _Current As Object

        Private Evaluator As Evaluator
        Private Source As IEnumerator
        Private Transform As Token

        Friend Sub New(ByVal evaluator As Evaluator, ByVal source As IEnumerator, ByVal transform As Token)
            Me.Evaluator = evaluator
            Me.Source = source
            Me.Transform = transform
        End Sub

        Friend Overrides ReadOnly Property Current() As Object
            Get
                Return _Current
            End Get
        End Property

        Friend Overrides Function MoveNext() As Boolean
            If Source.MoveNext() Then
                _Current = Evaluator.EvalToken(Source.Current, Transform).Value
                Return True
            Else
                _Current = Nothing
                Return False
            End If
        End Function

        Friend Overrides Sub Reset()
            Source.Reset()
        End Sub

    End Class

    Friend Class FilterPipe : Inherits Pipe

        Private Filter As Token
        Private Source As IEnumerable

        Friend Sub New(ByVal evaluator As Evaluator, ByVal source As IEnumerable, ByVal filter As Token)
            MyBase.New(evaluator, "Filter")
            Me.Filter = filter
            Me.Source = source
        End Sub

        Friend Overrides Function GetEnumerator() As IEnumerator(Of Object)
            Return New FilterPipeEnumerator(Evaluator, Source.GetEnumerator, Filter)
        End Function

    End Class

    Friend Class FilterPipeEnumerator : Inherits StreamEnumerator

        Private _Current As Object

        Private Evaluator As Evaluator
        Private Filter As Token
        Private Source As IEnumerator

        Friend Sub New(ByVal evaluator As Evaluator, ByVal source As IEnumerator, ByVal filter As Token)
            Me.Evaluator = evaluator
            Me.Filter = filter
            Me.Source = source
        End Sub

        Friend Overrides ReadOnly Property Current() As Object
            Get
                Return _Current
            End Get
        End Property

        Friend Overrides Function MoveNext() As Boolean
            While Source.MoveNext
                If Evaluator.EvalToken(Source.Current, Filter).AsBool Then
                    _Current = Source.Current
                    Return True
                End If
            End While

            _Current = Nothing
            Return False
        End Function

        Friend Overrides Sub Reset()
            Source.Reset()
        End Sub

    End Class

End Namespace
