Imports System.Collections
Imports System.Collections.Generic

Namespace Huggle.Scripting

    Partial Class Evaluator

        Private Function CheckType(Of T)(ByVal name As String, ByVal obj As Object) As T
            If Not TypeOf obj Is T Then Throw New ScriptException(name & " expects " & GetType(T).Name & ", recieved " & obj.GetType.Name)
            Return CType(obj, T)
        End Function

        Private Function SpecialFunc _
            (ByVal Context As Object, ByVal Func As Token, ByVal Arg As Token()) As Token

            Select Case Func.AsString
                'Property accessor
                Case "." : Return EvalProperty(Context, Arg(0), Arg(1))

                Case "function"
                    Dim NewFunc As New Func

                    For i As Integer = 0 To Arg.Length - 2
                        NewFunc.ArgNames.Add(Arg(i).AsString)
                    Next i

                    NewFunc.Token = Arg(Arg.Length - 1)
                    Return New Token(NewFunc)

                Case "apply"
                    Return ApplyFunc(Context, CType(EvalToken(Context, Arg(0)).Value, Func), _
                        Arg.ToList.GetRange(1, Arg.Length - 1).ToArray)

                Case "assign", ":="
                    Dim Identifier As String = Arg(0).AsString
                    Dim Value As Object = EvalToken(Context, Arg(1)).Value

                    Identifiers.Merge(Identifier, Value)
                    Return New Token(Value)

                Case ";", "sequence"
                    For i As Integer = Arg.Length - 2 To 0 Step -1
                        If EvalToken(Context, Arg(i)) Is Undefined Then Return Undefined
                    Next i

                    Return EvalToken(Context, Arg(Arg.Length - 1))

                Case "increment", "+="
                    Dim Identifier As String = Arg(0).AsString
                    Dim Result As Object = CDbl(Identifiers(Identifier)) + EvalToken(Context, Arg(1)).AsNumber

                    Identifiers.Merge(Identifier, Result)
                    Return New Token(Result)

                Case "decrement", "-="
                    Dim Identifier As String = Arg(0).AsString
                    Dim Result As Object = CDbl(Identifiers(Identifier)) - EvalToken(Context, Arg(1)).AsNumber

                    Identifiers.Merge(Identifier, Result)
                    Return New Token(Result)

                Case "&="
                    Dim Identifier As String = Arg(0).AsString
                    Dim Result As String = Identifiers(Identifier).ToString & EvalToken(Context, Arg(1)).AsString
                    Identifiers.Merge(Identifier, Result)
                    Return New Token(Result)

                Case "filter"
                    Return New Token(New FilterPipe(Me, CheckType(Of IEnumerable)("filter", EvalToken(Context, Arg(0)).Value), Arg(1)))

                Case "->", "map"
                    Return New Token(New TransformPipe(Me, CheckType(Of IEnumerable)("map", EvalToken(Context, Arg(0)).Value), Arg(1)))

                Case "fold"
                    Dim List As ArrayList = EvalToken(Context, Arg(0)).AsList
                    Dim FoldFunc As Token = EvalToken(Context, Arg(1))
                    Dim Result As Token = Nothing

                    If Arg.Length >= 3 Then
                        Result = Arg(2)
                    ElseIf List.Count > 0 Then
                        Result = New Token(DefaultFor(List(0)))
                    End If

                    For Each Item As Object In List
                        Result = EvalFunction(Context, FoldFunc, New Token() {Result, New Token(Item)})
                    Next Item

                    Return Result

                Case "table"
                    Dim result As New ScriptTable
                    result.Rows = New List(Of ScriptTableRow)
                    result.Columns = New List(Of String)
                    result.ColumnTypes = New List(Of String)

                    Dim source As IEnumerable = CheckType(Of IEnumerable)("table", EvalToken(Context, Arg(0)).Value)
                    Dim headers As IEnumerable = If(Arg.Length < 2, New ArrayList, CheckType(Of IEnumerable)("table", EvalToken(Context, Arg(1)).Value))
                    Dim headerStrs As New List(Of String)

                    For Each header As Object In headers
                        headerStrs.Add(New Token(header).AsString)
                    Next header

                    Dim columnCount As Integer

                    For Each rowObj As Object In source
                        Dim row As New ScriptTableRow

                        row.Columns = result.Columns
                        row.Items = New ArrayList

                        Dim rowSource As IEnumerable = CheckType(Of IEnumerable)("table", rowObj)

                        For Each cell As Object In rowSource
                            row.items.Add(cell)
                        Next cell

                        If row.Items.Count > columnCount Then columnCount = row.Items.Count
                        result.Rows.Add(row)
                    Next rowObj

                    For i As Integer = 0 To columnCount - 1
                        If headerStrs.Count > i Then result.Columns.Add(headerStrs(i)) _
                            Else result.Columns.Add("Column" & CStr(i + 1))
                    Next i

                    Return New Token(result)

                Case "groupby"
                    Dim List As ArrayList = EvalToken(Context, Arg(0)).AsList
                    Dim Result As New ScriptTable
                    Result.Columns = New List(Of String)
                    Result.Columns.Add("Item")
                    Result.Columns.Add("Count")

                    Result.Rows = New List(Of ScriptTableRow)
                    Dim Dictionary As New Hashtable

                    For Each Item As Object In List
                        Dim ValueToken As Token = EvalToken(Item, Arg(1))
                        Dim Value As Object

                        If ValueToken.ValueType = "Number" _
                            Then Value = ValueToken.Value Else Value = ValueToken.Value.ToString

                        If Dictionary.ContainsKey(Value) _
                            Then Dictionary(Value) = CInt(Dictionary(Value)) + 1 _
                            Else Dictionary.Add(Value, 1)
                    Next Item

                    For Each Item As DictionaryEntry In Dictionary
                        Dim NewRow As New ScriptTableRow
                        NewRow.Items = New ArrayList
                        NewRow.Columns = Result.Columns

                        NewRow.Items.Add(Item.Key)
                        NewRow.Items.Add(Item.Value)
                        Result.Rows.Add(NewRow)
                    Next Item

                    Result.ColumnTypes = New List(Of String)
                    Result.ColumnTypes.Add(New Token(Result.Rows(0).Items(0)).ValueType)
                    Result.ColumnTypes.Add("Number")

                    Return New Token(Result)

                Case "if"
                    If EvalToken(Context, Arg(0)).AsBool Then Return EvalToken(Context, Arg(1)) _
                        Else If Arg.Length = 2 Then Return New Token(False) Else Return EvalToken(Context, Arg(2))

                Case "switch"
                    Dim Var As Object = EvalToken(Context, Arg(0)).Value
                    Dim Result As Token = New Token(False)

                    For i As Integer = 1 To Arg.Length - 2 Step 2
                        Dim OptionVar As Object = EvalToken(Context, Arg(i)).Value

                        If TypeOf OptionVar Is ArrayList Then
                            For Each Item As Object In CType(OptionVar, ArrayList)
                                If ValuesEqual(Item, Var) Then
                                    Result = EvalToken(Context, Arg(i + 1))
                                    Exit For
                                End If
                            Next Item
                        Else
                            If ValuesEqual(OptionVar, Var) Then Result = EvalToken(Context, Arg(i + 1))
                        End If
                    Next i

                    Return Result
            End Select

            Return Nothing
        End Function

    End Class

End Namespace
