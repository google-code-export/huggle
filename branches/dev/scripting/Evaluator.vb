Imports Huggle.Actions
Imports System
Imports System.Collections
Imports System.Collections.Generic
Imports System.Text.RegularExpressions

Namespace Huggle.Scripting

    'Handles evaluation of script expressions

    <Diagnostics.DebuggerDisplay("{Name}")> _
    Friend Class Evaluator : Inherits Query

        Private _Cancelled As Boolean
        Private _CancelReason As String
        Private _Context As Object
        Private _Expression As String
        Private _Immediate As Boolean
        Private _InfoNeeded As List(Of BatchInfo)
        Private _Name As String
        Private _Previous As Object
        Private _ReadOnly As Boolean
        Private _Value As Token

        Private CurrentQuery As Action
        Private DefaultQueryLimit As Integer = 1000
        Private Identifiers As Dictionary(Of String, Object)
        Private NextBatch As New List(Of BatchInfo)
        Private SentQueries As List(Of String)
        Private ThreadMode As Boolean

        Private Shared ReadOnly Comparer As New ItemComparer
        Private Shared ReadOnly Undefined As New Token("undefined")

        Private Shared ReadOnly StandardIds As Dictionary(Of String, Object) = New Object() { _
            "pi", Math.PI, _
            "e", Math.E, _
            "undefined", Nothing _
            }.ToDictionary(Of String, Object)()

        Private WithEvents BatchQuery As BatchQuery

        Public Sub New(ByVal session As Session, ByVal name As String, ByVal expression As String)
            MyBase.New(session, Msg("script-desc", name))
            _Expression = expression
            _Name = name

            SentQueries = New List(Of String)
        End Sub

        Public ReadOnly Property Name() As String
            Get
                Return _Name
            End Get
        End Property

        Public Property Cancelled() As Boolean
            Get
                Return _Cancelled
            End Get
            Set(ByVal value As Boolean)
                _Cancelled = value
            End Set
        End Property

        Public Property CancelReason() As String
            Get
                Return _CancelReason
            End Get
            Private Set(ByVal value As String)
                _CancelReason = value
            End Set
        End Property

        Public Property Context() As Object
            Get
                Return _Context
            End Get
            Set(ByVal value As Object)
                _Context = value
            End Set
        End Property

        Public ReadOnly Property Expression() As String
            Get
                Return _Expression
            End Get
        End Property

        Public Property Immediate() As Boolean
            Get
                Return _Immediate
            End Get
            Set(ByVal value As Boolean)
                _Immediate = value
            End Set
        End Property

        Public ReadOnly Property InfoNeeded() As List(Of BatchInfo)
            Get
                Return _InfoNeeded
            End Get
        End Property

        Public Property Previous() As Object
            Get
                Return _Previous
            End Get
            Set(ByVal value As Object)
                _Previous = value
            End Set
        End Property

        Public Property [ReadOnly]() As Boolean
            Get
                Return _ReadOnly
            End Get
            Set(ByVal value As Boolean)
                _ReadOnly = value
            End Set
        End Property

        Public Property Value() As Token
            Get
                Return _Value
            End Get
            Private Set(ByVal newValue As Token)
                _Value = newValue
            End Set
        End Property

        Public Overrides Sub Start()
            'Try
            '    BatchQuery = New BatchQuery(User)
            '    OnStarted()
            '    OnProgress(Msg("eval-progress"))
            '    Dim token As Token = (New Parser).Parse(Expression)

            '    Do
            '        'Create and execute batch request for any required information
            '        If NextBatch.Count > 0 Then
            '            BatchQuery.Clear()
            '            BatchQuery.AddRange(NextBatch)
            '            NextBatch = BatchQuery.Prepare
            '            If BatchQuery.Queries.Count = 0 Then OnFail("Extra queries broken") : Return
            '            OnProgress(Msg("eval-queries"))
            '            BatchQuery.Execute(Name)
            '            If Cancelled Then Return
            '        End If

            '        'Set up identifiers
            '        Identifiers = New Dictionary(Of String, Object)(StandardIds)
            '        Identifiers.Merge("queryname", Name)
            '        If Previous IsNot Nothing Then Identifiers.Add("result", Previous)
            '        _InfoNeeded = New List(Of BatchInfo)

            '        'Evaluate expression
            '        Value = EvalToken(Context, token)

            '        'If result is a stream, read out the whole stream 
            '        If TypeOf Value.Value Is Pipe Then Value = New Token(CType(Value.Value, Pipe).ReadAll)

            '    Loop Until NextBatch.Count = 0

            '    OnSuccess()

            'Catch ex As ScriptException
            '    OnFail(Result.FromException(ex))
            'End Try
        End Sub

        Public Overrides Function ToString() As String
            Return _Expression
        End Function

        Public Function EvalToken(ByVal context As Object, ByVal token As Token) As Token
            If token Is Nothing Then Return New Token(False)
            If Cancelled Then Throw New ScriptException(Msg("error-cancelled"))

            Try
                Select Case token.Type
                    Case TokenType.Constant : Return token

                    Case TokenType.Identifier
                        If context IsNot Nothing AndAlso CStr(token.Value) = "this" Then Return New Token(context)

                        If Identifiers.ContainsKey(token.AsString.ToLowerI) Then
                            If Identifiers(token.AsString.ToLowerI) Is Nothing Then
                                If InfoNeeded IsNot Nothing AndAlso TypeOf context Is QueueItem _
                                    Then InfoNeeded.Add(New BatchInfo(Wiki, DirectCast(context, QueueItem), token.AsString))
                                Return Undefined
                            Else
                                Return New Token(Identifiers(token.AsString.ToLowerI))
                            End If
                        End If

                        Return token

                    Case TokenType.Function, TokenType.EmptyFunction, TokenType.Operator, TokenType.UnaryOperator
                        Dim args As New List(Of Token)
                        Dim arg As Token = token.FirstChild

                        While arg IsNot Nothing
                            args.Insert(0, arg)
                            arg = arg.NextSibling
                        End While

                        Return EvalFunction(context, token, args.ToArray)
                End Select

                Throw New ScriptException("Unexpected token")

            Catch ex As ScriptException
                If token.ToString = ";" Then Throw _
                    Else Throw New ScriptException(Msg("query-errorformat", ex.Message, token.ToString))

            Catch ex As IndexOutOfRangeException
                Throw New ScriptException(Msg("query-arguments", token.ToString))

            Catch ex As SystemException
                Throw New ScriptException(Msg("query-errorformat", ex.Message, token.ToString))
            End Try
        End Function

        Private Function EvalFunction _
            (ByVal Context As Object, ByVal Func As Token, ByVal Original As Token()) As Token

            'User-defined functions
            If Identifiers.ContainsKey(Func.AsString) AndAlso TypeOf Identifiers(Func.AsString) Is Func _
                Then Return ApplyFunc(Context, CType(Identifiers(Func.AsString), Func), Original)

            Dim Result As Token

            Result = SpecialFunc(Context, Func, Original)
            If Result IsNot Nothing Then Return Result

            'Evaluate args
            Dim Arg(Original.Length - 1) As Token

            For i As Integer = 0 To Original.Length - 1
                Arg(i) = EvalToken(Context, Original(i))
                If Arg(i).ValueType = "Token" Then Return Undefined
                If Arg(i).ValueType = "String" AndAlso Arg(i).ToString = "undefined" Then Return Undefined

                'Short circuiting boolean operations
                If (Func.AsString = "&" OrElse Func.AsString = "and") AndAlso Not Arg(i).AsBool Then Return New Token(False)
                If (Func.AsString = "|" OrElse Func.AsString = "or") AndAlso Arg(i).AsBool Then Return New Token(True)
            Next i

            'Get named args
            Dim Named As New Dictionary(Of String, Token)

            For i As Integer = 0 To Original.Length - 1
                If Original(i).Type = TokenType.Operator AndAlso Original(i).ToString = ":" _
                    Then Named.Merge(Original(i).FirstChild.NextSibling.ToString, _
                    EvalToken(Context, Original(i).FirstChild))
            Next i

            Dim answer As Token

            answer = ActionFunc(Context, Func, Arg, Named)
            If answer IsNot Nothing Then Return answer

            answer = StandardFunc(Context, Func, Arg)
            If answer IsNot Nothing Then Return answer

            answer = MiscFunc(Context, Func, Arg)
            If answer IsNot Nothing Then Return answer

            answer = ListFunc(Context, Func, Arg)
            If answer IsNot Nothing Then Return answer

            answer = New Token(UserFunc(Context, Func, Arg, Named))
            If answer.Value IsNot Nothing Then Return answer

            answer = WikiFunc(Context, Func, Arg, Original)
            If answer IsNot Nothing Then Return answer

            Throw New ScriptException(Msg("query-wrongfunction", Func.ToString))
        End Function

        Private Function ApplyFunc(ByVal Context As Object, ByVal Func As Func, ByVal Args As Token()) As Token
            'Save identifiers
            Dim oldIds As New Dictionary(Of String, Object)

            For Each item As String In Func.ArgNames
                If Identifiers.ContainsKey(item) Then oldIds.Merge(item, Identifiers(item))
            Next item

            For i As Integer = 0 To Func.ArgNames.Count - 1
                Identifiers.Merge(Func.ArgNames(i), EvalToken(Context, Args(i)).Value)
            Next i

            Dim result As Token = EvalToken(Context, Func.Token)

            'Load identifiers
            For Each item As KeyValuePair(Of String, Object) In oldIds
                Identifiers.Merge(item.Key, item.Value)
            Next item

            Return result
        End Function

        Private Function EvalProperty(ByVal Context As Object, ByVal Item As Token, ByVal Prop As Token) As Token

            If Prop.Type = TokenType.Function Then
                Dim Func As Token = Prop
                Dim Args(Func.Arguments) As Token

                Args(0) = Item

                If Func.Arguments >= 1 Then
                    Dim Arg As Token = Func.FirstChild

                    For i As Integer = Func.Arguments To 1 Step -1
                        Args(i) = Arg
                        Arg = Arg.NextSibling
                    Next i
                End If

                Return EvalFunction(Context, Prop, Args)
            End If

            Dim propName As String = Prop.AsString.ToLowerI

            'If TypeOf Item.Value Is TableRow Then
            '    Dim Row As TableRow = CType(Item.Value, TableRow)

            '    For i As Integer = 0 To Row.Columns.Count - 1
            '        If Row.Columns(i).ToLowerI = PropName Then Return New Token(Row.Items(i))
            '    Next i
            'End If

            'If TypeOf Item.Value Is Table Then
            '    Dim Table As Table = CType(Item.Value, Table)
            '    Dim Column As Integer = -1

            '    For i As Integer = 0 To Table.Columns.Count - 1
            '        If Table.Columns(i).ToLowerI = PropName Then Column = i
            '    Next i

            '    If Column > -1 Then
            '        Dim Result As New ArrayList

            '        For i As Integer = 0 To Table.Rows.Count - 1
            '            Result.Add(Table.Rows(i).Items(Column))
            '        Next i

            '        Return New Token(Result)
            '    End If
            'End If

            Return EvalFunction(Context, Prop, New Token() {Item})
        End Function

        Private Function ListQuery(ByVal context As Object, ByVal query As ListQuery, _
            Optional ByVal args() As Token = Nothing) As Token

            query.Limit = DefaultQueryLimit
            query.UseCache = True

            Dim options As New Dictionary(Of String, String)

            If args IsNot Nothing Then
                For i As Integer = 0 To args.Length - 1
                    If args(i).Type = TokenType.Operator AndAlso args(i).ToString = ":" _
                        Then options.Merge(args(i).FirstChild.NextSibling.ToString, _
                        EvalToken(context, args(i).FirstChild).Value.ToString)
                Next i
            End If

            query.SetOptions(options)

            'App.Invoke(AddressOf OnProgress, New EvalProgressEventArgs(Msg("list-progress-" & _
            '    Query.GetType.Name.Remove("Request").ToLowerI)))

            Return New Token(New QueryPipe(Me, query))
        End Function

        Public Sub DoProgress(ByVal message As String)
            OnProgress(message)
        End Sub

        Private Sub RequestInfo(ByVal type As String)
            NextBatch.Add(New BatchInfo(Wiki, type))
        End Sub

        Private Sub RequestInfo(ByVal item As QueueItem, ByVal type As String)
            NextBatch.Add(New BatchInfo(Wiki, item, type))
        End Sub

        'Private Function TableSorter(ByVal Column As Integer, ByVal Type As String) As Comparison(Of TableRow)
        '    Return New Comparison(Of TableRow) _
        '        (Function(x As TableRow, y As TableRow) CompareRows(x.Items(Column), y.Items(Column), Type))
        'End Function

        Private Function CompareRows(ByVal x As Object, ByVal y As Object, ByVal Type As String) As Integer
            Select Case Type
                Case "String" : Return String.Compare(CStr(x), CStr(y), StringComparison.Ordinal)
                Case "Number" : Return Math.Sign(CDbl(x) - CDbl(y))
                Case "Boolean" : If CBool(x) Then Return 1 Else If CBool(y) Then Return -1 Else Return 0
                Case "Range" : Return Math.Sign(CType(x, Range).Lower - CType(y, Range).Lower)
                Case Else : Return 0
            End Select
        End Function

        Private Function DefaultFor(ByVal Obj As Object) As Object
            If TypeOf Obj Is Integer OrElse TypeOf Obj Is Double _
                OrElse TypeOf Obj Is Long OrElse TypeOf Obj Is Single Then Return 0

            If TypeOf Obj Is Boolean Then Return False
            If TypeOf Obj Is String Then Return ""
            If TypeOf Obj Is ArrayList Then Return New ArrayList
            If TypeOf Obj Is Hashtable Then Return New Hashtable
            'If TypeOf Obj Is Table Then Return New Table
            Return New Object
        End Function

        Private Function ValuesEqual(ByVal x As Object, ByVal y As Object) As Boolean
            If (TypeOf x Is Short OrElse TypeOf x Is Integer OrElse TypeOf x Is Long OrElse TypeOf x Is Single _
                OrElse TypeOf x Is Double) AndAlso (TypeOf y Is Short OrElse TypeOf y Is Integer _
                OrElse TypeOf y Is Long OrElse TypeOf y Is Single OrElse TypeOf y Is Double) _
                Then Return CDbl(x) = CDbl(y)

            Return x.Equals(y)
        End Function

    End Class

End Namespace
