﻿Imports Huggle.Actions
Imports System
Imports System.Collections
Imports System.Collections.Generic

Namespace Huggle.Scripting

    <Diagnostics.DebuggerDisplay("{Value}")> _
        Public Class Token

        'Represents a symbol or value during parsing and evaluation

        Private _FirstChild, _NextSibling, _Parent As Token
        Private _RightAssociative As Boolean
        Private _Arguments As Integer
        Private _Precedence As Integer
        Private _Type As TokenType
        Private _Value As Object

        Public Sub New(ByVal value As Object, Optional ByVal type As TokenType = TokenType.Constant)
            Me.Value = value
            Me.Type = type
        End Sub

        Public Property Arguments() As Integer
            Get
                Return _Arguments
            End Get
            Set(ByVal value As Integer)
                _Arguments = value
            End Set
        End Property

        Public ReadOnly Property Bool() As Boolean
            Get
                If TypeOf Value Is Boolean Then Return CBool(Value)
                If Value.ToString = "undefined" Then Return False
                Throw New ScriptException(Msg("query-typemismatch", "Boolean", ValueType.ToString))
            End Get
        End Property

        Public ReadOnly Property Dictionary() As Hashtable
            Get
                If TypeOf Value Is Hashtable Then Return CType(Value, Hashtable)
                Throw New ScriptException(Msg("query-typemismatch", "Dictionary", ValueType.ToString))
            End Get
        End Property

        Public Property FirstChild() As Token
            Get
                Return _FirstChild
            End Get
            Set(ByVal value As Token)
                _FirstChild = value
            End Set
        End Property

        Public ReadOnly Property List() As ArrayList
            Get
                If TypeOf Value Is ArrayList Then Return CType(Value, ArrayList)

                Throw New ScriptException(Msg("query-typemismatch", "List", ValueType.ToString))
            End Get
        End Property

        Public ReadOnly Property Media() As Media
            Get
                If TypeOf Value Is Media Then Return CType(Value, Media)
                Throw New ScriptException(Msg("query-typemismatch", "Page", ValueType.ToString))
            End Get
        End Property

        Public Property NextSibling() As Token
            Get
                Return _NextSibling
            End Get
            Set(ByVal value As Token)
                _NextSibling = value
            End Set
        End Property

        Public ReadOnly Property Number() As Double
            Get
                If TypeOf Value Is Double Then Return CDbl(Value)
                If TypeOf Value Is Integer Then Return CInt(Value)
                If TypeOf Value Is Long Then Return CLng(Value)
                Throw New ScriptException(Msg("query-typemismatch", "Number", ValueType.ToString))
            End Get
        End Property

        Public ReadOnly Property Page() As Page
            Get
                If TypeOf Value Is Page Then Return CType(Value, Page)
                Throw New ScriptException(Msg("query-typemismatch", "Page", ValueType.ToString))
            End Get
        End Property

        Public Property Parent() As Token
            Get
                Return _Parent
            End Get
            Set(ByVal value As Token)
                _Parent = value
            End Set
        End Property

        Public Property Precedence() As Integer
            Get
                Return _Precedence
            End Get
            Set(ByVal value As Integer)
                _Precedence = value
            End Set
        End Property

        Public ReadOnly Property QItem() As QueueItem
            Get
                If TypeOf Value Is QueueItem Then Return CType(Value, QueueItem)
                Throw New ScriptException(Msg("query-typemismatch", "queue item", ValueType.ToString))
            End Get
        End Property

        Public ReadOnly Property Revision() As Revision
            Get
                If TypeOf Value Is Revision Then Return CType(Value, Revision)
                Throw New ScriptException(Msg("query-typemismatch", "Revision", ValueType.ToString))
            End Get
        End Property

        Public Property RightAssociative() As Boolean
            Get
                Return _RightAssociative
            End Get
            Set(ByVal value As Boolean)
                _RightAssociative = value
            End Set
        End Property

        Public ReadOnly Property Space() As Space
            Get
                If TypeOf Value Is Space Then Return CType(Value, Space)
                Throw New ScriptException(Msg("query-typemismatch", "Space", ValueType.ToString))
            End Get
        End Property

        Public ReadOnly Property [String]() As String
            Get
                If TypeOf Value Is String Then Return CStr(Value)
                If Value IsNot Nothing Then Return Value.ToString
                Throw New ScriptException(Msg("query-typemismatch", "String", ValueType.ToString))
            End Get
        End Property

        Public ReadOnly Property Time() As Date
            Get
                If TypeOf Value Is Date Then Return CDate(Value)
                Throw New ScriptException(Msg("query-typemismatch", "Time", ValueType.ToString))
            End Get
        End Property

        Public Property Type() As TokenType
            Get
                Return _Type
            End Get
            Set(ByVal value As TokenType)
                _Type = value
            End Set
        End Property

        Public ReadOnly Property User() As User
            Get
                If TypeOf Value Is User Then Return CType(Value, User)
                Throw New ScriptException(Msg("query-typemismatch", "User", ValueType.ToString))
            End Get
        End Property

        Public Property Value() As Object
            Get
                Return _Value
            End Get
            Set(ByVal value As Object)
                _Value = value
            End Set
        End Property

        Public ReadOnly Property ValueType() As String
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

    Public Class ItemComparer : Implements IComparer

        Public Function Compare(ByVal x As Object, ByVal y As Object) As Integer Implements IComparer.Compare

            If TypeOf x Is String Then Return String.Compare(CStr(x), CStr(y))
            If TypeOf x Is Boolean Then If CBool(x) Then Return 1 Else If CBool(y) Then Return -1 Else Return 0

            If (TypeOf x Is Short OrElse TypeOf x Is Integer OrElse TypeOf x Is Long OrElse TypeOf x Is Single _
                OrElse TypeOf x Is Double) AndAlso (TypeOf y Is Short OrElse TypeOf y Is Integer _
                OrElse TypeOf y Is Long OrElse TypeOf y Is Single OrElse TypeOf y Is Double) _
                Then Return Math.Sign(CDbl(y) - CDbl(x))

            If TypeOf x Is Range AndAlso TypeOf y Is Range _
                Then Return Math.Sign(CType(x, Range).Lower - CType(y, Range).Lower)

            Return String.Compare(x.ToString, y.ToString)
        End Function

    End Class

    Public Structure FunctionData
        Dim Count As Integer
        Dim [Function] As Token
    End Structure

    Public Enum TokenType As Integer
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

    Public Class Func

        Public ArgNames As New List(Of String)
        Public Token As Token

        Public Overrides Function ToString() As String
            Return "function(" & ArgNames.Join(", ") & ")"
        End Function

    End Class

    <Serializable()>
    Public Class ScriptException : Inherits ApplicationException

        'Represents an error raised during parsing or evaluation of a script

        Public Sub New(ByVal message As String)
            MyBase.New(message)
        End Sub

    End Class

    <Serializable()>
    Public Class TaskCancelledException : Inherits ApplicationException

        Public Sub New()
            MyBase.New(Msg("script-usercancelled"))
        End Sub

    End Class

    Public Class EvalDoneEventArgs : Inherits EventArgs

        Private _Result As Result

        Public Sub New(ByVal result As Result)
            _Result = result
        End Sub

        Public ReadOnly Property Result() As Result
            Get
                Return _Result
            End Get
        End Property

    End Class

    Public Class EvalProgressEventArgs : Inherits EventArgs

        Private _Message As String

        Public Sub New(ByVal message As String)
            _Message = message
        End Sub

        Public ReadOnly Property Message() As String
            Get
                Return _Message
            End Get
        End Property

    End Class

    Public Class ScriptTable

        Public Columns As List(Of String)
        Public ColumnTypes As List(Of String)
        Public Rows As List(Of ScriptTableRow)

    End Class

    Public Class ScriptTableRow

        Public Columns As List(Of String)
        Public Items As ArrayList

    End Class

    Public MustInherit Class Pipe : Implements IEnumerable(Of Object)

        Private _Description As String
        Private _Evaluator As Evaluator

        Protected Sub New(ByVal evaluator As Evaluator, ByVal description As String)
            _Description = description
            _Evaluator = evaluator
        End Sub

        Public ReadOnly Property Description() As String
            Get
                Return _Description
            End Get
        End Property

        Protected ReadOnly Property Evaluator() As Evaluator
            Get
                Return _Evaluator
            End Get
        End Property

        Public Function ReadAll() As ArrayList
            Evaluator.DoProgress(Msg("eval-stream", Description))

            Dim result As New ArrayList

            For Each item As Object In Me
                result.Add(item)
                If Evaluator.IsCancelled Then Throw New ScriptException(Msg("error-cancelled"))
            Next item

            Return result
        End Function

        Public MustOverride Function GetEnumerator() As IEnumerator(Of Object) Implements IEnumerable(Of Object).GetEnumerator

        Public Function GetEnumerator1() As IEnumerator Implements IEnumerable.GetEnumerator
            Return GetEnumerator()
        End Function

    End Class

    Public MustInherit Class StreamEnumerator : Implements IEnumerator(Of Object)

        Protected Sub New()
        End Sub

        Public MustOverride ReadOnly Property Current() As Object Implements IEnumerator(Of Object).Current, IEnumerator.Current
        Public MustOverride Function MoveNext() As Boolean Implements IEnumerator.MoveNext
        Public MustOverride Sub Reset() Implements IEnumerator.Reset

        Public Sub Dispose() Implements IDisposable.Dispose
            Dispose(True)
            GC.SuppressFinalize(Me)
        End Sub

        Private Sub Dispose(ByVal disposing As Boolean)
        End Sub

    End Class

    Public Class QueryPipe : Inherits Pipe

        Private Query As ListQuery

        Public Sub New(ByVal evaluator As Evaluator, ByVal query As ListQuery)
            MyBase.New(evaluator, query.Description)
            Me.Query = query
        End Sub

        Public Overrides Function GetEnumerator() As IEnumerator(Of Object)
            Return New QueryPipeEnumerator(Query)
        End Function

    End Class

    Public Class QueryPipeEnumerator : Inherits StreamEnumerator

        Private _Current As Object
        Private Index As Integer
        Private Query As ListQuery

        Public Sub New(ByVal query As ListQuery)
            Me.Query = query
        End Sub

        Public Overrides ReadOnly Property Current() As Object
            Get
                Return _Current
            End Get
        End Property

        Public Overrides Function MoveNext() As Boolean
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

        Public Overrides Sub Reset()
            Index = 0
        End Sub

    End Class

    Public Class TransformPipe : Inherits Pipe

        Private Source As IEnumerable
        Private Transform As Token

        Public Sub New(ByVal evaluator As Evaluator, ByVal source As IEnumerable, ByVal transform As Token)
            MyBase.New(evaluator, "Transform")
            Me.Source = source
            Me.Transform = transform
        End Sub

        Public Overrides Function GetEnumerator() As IEnumerator(Of Object)
            Return New TransformPipeEnumerator(Evaluator, Source.GetEnumerator, Transform)
        End Function

    End Class

    Public Class TransformPipeEnumerator : Inherits StreamEnumerator

        Private _Current As Object

        Private Evaluator As Evaluator
        Private Source As IEnumerator
        Private Transform As Token

        Public Sub New(ByVal evaluator As Evaluator, ByVal source As IEnumerator, ByVal transform As Token)
            Me.Evaluator = evaluator
            Me.Source = source
            Me.Transform = transform
        End Sub

        Public Overrides ReadOnly Property Current() As Object
            Get
                Return _Current
            End Get
        End Property

        Public Overrides Function MoveNext() As Boolean
            If Source.MoveNext() Then
                _Current = Evaluator.EvalToken(Source.Current, Transform).Value
                Return True
            Else
                _Current = Nothing
                Return False
            End If
        End Function

        Public Overrides Sub Reset()
            Source.Reset()
        End Sub

    End Class

    Public Class FilterPipe : Inherits Pipe

        Private Filter As Token
        Private Source As IEnumerable

        Public Sub New(ByVal evaluator As Evaluator, ByVal source As IEnumerable, ByVal filter As Token)
            MyBase.New(evaluator, "Filter")
            Me.Filter = filter
            Me.Source = source
        End Sub

        Public Overrides Function GetEnumerator() As IEnumerator(Of Object)
            Return New FilterPipeEnumerator(Evaluator, Source.GetEnumerator, Filter)
        End Function

    End Class

    Public Class FilterPipeEnumerator : Inherits StreamEnumerator

        Private _Current As Object

        Private Evaluator As Evaluator
        Private Filter As Token
        Private Source As IEnumerator

        Public Sub New(ByVal evaluator As Evaluator, ByVal source As IEnumerator, ByVal filter As Token)
            Me.Evaluator = evaluator
            Me.Filter = filter
            Me.Source = source
        End Sub

        Public Overrides ReadOnly Property Current() As Object
            Get
                Return _Current
            End Get
        End Property

        Public Overrides Function MoveNext() As Boolean
            While Source.MoveNext
                If Evaluator.EvalToken(Source.Current, Filter).Bool Then
                    _Current = Source.Current
                    Return True
                End If
            End While

            _Current = Nothing
            Return False
        End Function

        Public Overrides Sub Reset()
            Source.Reset()
        End Sub

    End Class

End Namespace