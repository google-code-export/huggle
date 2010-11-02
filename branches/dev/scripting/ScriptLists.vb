Imports System
Imports System.Collections
Imports System.Collections.Generic
Imports System.Text.RegularExpressions

Namespace Huggle.Scripting

    Partial Class Evaluator

        Private Function ListFunc(ByVal Context As Object, ByVal Func As Token, ByVal Arg As Token()) As Token

            Select Case Func.AsString
                Case "list"
                    Dim Result As New ArrayList

                    For Each Item As Token In Arg
                        Result.Add(Item.Value)
                    Next Item

                    Return New Token(Result)

                Case "sort"
                    Dim Result As ArrayList = Arg(0).AsList

                    Try
                        Result.Sort(Comparer)
                    Catch ex As InvalidOperationException
                        Throw New ScriptException(Msg("query-sorterror"))
                    End Try

                    Return New Token(Result)

                Case "unique"
                    Dim Result As New ArrayList
                    Dim Strs As New List(Of String)

                    For Each Item As Object In Arg(0).AsList
                        Dim Str As String = MakeString(Item)

                        If Not Strs.Contains(Str) Then
                            Strs.Add(Str)
                            Result.Add(Item)
                        End If
                    Next Item

                    Return New Token(Result)

                    'Case "group"
                    '    Dim List As ArrayList = Arg(0).List
                    '    Dim Result As New Table

                    '    Result.Rows = New List(Of TableRow)
                    '    Result.Columns = New List(Of String)
                    '    Result.ColumnTypes = New List(Of String)
                    '    Result.Columns.Add("Item")
                    '    Result.Columns.Add("Count")
                    '    Result.ColumnTypes.Add(New Token(List(0)).ValueType)
                    '    Result.ColumnTypes.Add("Number")

                    '    Dim Dictionary As New Dictionary(Of String, KeyValuePair(Of Object, Integer))

                    '    For Each Item As Object In List
                    '        Dim Str As String = MakeString(Item)

                    '        If Dictionary.ContainsKey(Str) Then
                    '            Dictionary(Str) = New KeyValuePair(Of Object, Integer) _
                    '                (Dictionary(Str).Key, Dictionary(Str).Value + 1)
                    '        Else
                    '            Dictionary.Add(Str, New KeyValuePair(Of Object, Integer)(Item, 1))
                    '        End If
                    '    Next Item

                    '    For Each Item As KeyValuePair(Of Object, Integer) In Dictionary.Values
                    '        Dim NewRow As New TableRow
                    '        NewRow.Columns = Result.Columns
                    '        NewRow.Items = New ArrayList
                    '        NewRow.Items.Add(Item.Key)
                    '        NewRow.Items.Add(Item.Value)
                    '        Result.Rows.Add(NewRow)
                    '    Next Item

                    '    Return New Token(Result)

                Case "exclude"
                    Dim Result As ArrayList = Arg(0).AsList

                    For Each Item As Object In Arg(1).AsList
                        If Result.Contains(Item) Then Result.Remove(Item)
                    Next Item

                    Return New Token(Result)

                Case "union"
                    Dim Result As ArrayList = Arg(0).AsList
                    Result.AddRange(Arg(1).AsList)
                    Return New Token(Result)

                Case "append"
                    Dim Result As ArrayList = Arg(0).AsList
                    Result.Add(Arg(1).Value)
                    Return New Token(Result)

                Case "containsany"
                    Dim First As ArrayList = Arg(0).AsList
                    Dim Second As ArrayList = Arg(1).AsList

                    For Each Item As Object In First
                        If Second.Contains(Item) Then Return New Token(True)
                    Next Item

                    Return New Token(False)

                Case "containspattern"
                    Dim List As ArrayList = Arg(0).AsList
                    Dim Pattern As New Regex("^" & Arg(1).AsString & "$")

                    For Each Item As Object In List
                        If Pattern.IsMatch(Item.ToString) Then Return New Token(True)
                    Next Item

                    Return New Token(False)

                Case "join"
                    Dim List As ArrayList = Arg(0).AsList
                    Dim Separator As String = Arg(1).AsString
                    Dim Result As String = ""

                    For Each Item As Object In List
                        Result &= Item.ToString & Separator
                    Next Item

                    If Result <> "" Then Result = Result.Substring(0, Result.Length - Separator.Length)
                    Return New Token(Result)

                Case "first" : Return New Token(Arg(0).AsList(0))
                Case "item" : Return New Token(Arg(0).AsList(CInt(Arg(1).AsNumber)))

                Case "rest"
                    Dim List As ArrayList = Arg(0).AsList
                    If List.Count = 0 Then Throw New ScriptException(Msg("query-listempty", Func.ToString))
                    Return New Token(List.GetRange(1, List.Count - 1))

                Case "last"
                    Dim List As ArrayList = Arg(0).AsList
                    Return New Token(List(List.Count - 1))

                Case "limit"
                    Dim List As ArrayList = Arg(0).AsList
                    Return New Token(List.GetRange(0, CInt(Arg(1).AsNumber)))

                Case "range"
                    Dim List As ArrayList = Arg(0).AsList
                    Return New Token(List.GetRange(CInt(Arg(1).AsNumber), CInt(Arg(2).AsNumber)))

                    'Case "reverse"
                    '    If Arg(0).ValueType = "List" Then
                    '        Dim Result As New ArrayList(Arg(0).List)
                    '        Result.Reverse()
                    '        Return New Token(Result)
                    '    ElseIf Arg(0).ValueType = "Table" Then
                    '        Dim Result As Table = CType(Arg(0).Value, Table)
                    '        Result.Rows.Reverse()
                    '        Return New Token(Result)
                    '    End If

                Case "movingavg"
                    Dim List As ArrayList = Arg(0).AsList
                    Dim Result As New ArrayList
                    Dim Size As Integer = CInt(Arg(1).AsNumber)

                    For i As Integer = Size - 1 To List.Count - 1
                        Dim Sum As Double = 0

                        For j As Integer = 0 To Size - 1
                            Sum += CDbl(List(i - j))
                        Next j

                        Result.Add(Sum / Size)
                    Next i

                    Return New Token(Result)

                    'Tables
                    'Case "sortby"
                    '    Dim Table As Table = CType(Arg(0).Value, Table)
                    '    Dim Column As Integer

                    '    If Arg(1).ValueType = "Number" Then Column = CInt(Arg(1).Number - 1) _
                    '        Else Column = Table.Columns.IndexOfI(Arg(1).String)

                    '    If Column = -1 Then
                    '        For i As Integer = 0 To Table.Columns.Count - 1
                    '            If Arg(1).String.ToLower = Table.Columns(i).ToLower Then
                    '                Column = i
                    '                Exit For
                    '            End If
                    '        Next i
                    '    End If

                    '    If Column = -1 Then Throw New ScriptException("Table has no column named '" & Arg(0).String)

                    '    Table.Rows.Sort(TableSorter(Column, Table.ColumnTypes(Column)))
                    '    Return New Token(Table)

                    'Case "column"
                    '    Dim Table As Table = CType(Arg(0).Value, Table)
                    '    Dim Column As Integer

                    '    If Arg(1).ValueType = "Number" Then Column = CInt(Arg(1).Number - 1) _
                    '        Else Column = Table.Columns.IndexOfI(Arg(1).String)

                    '    If Column = -1 Then
                    '        For i As Integer = 0 To Table.Columns.Count - 1
                    '            If Arg(1).String.ToLower = Table.Columns(i).ToLower Then
                    '                Column = i
                    '                Exit For
                    '            End If
                    '        Next i
                    '    End If

                    '    If Column = -1 Then Throw New ScriptException("Table has no column named '" & Arg(1).String)

                    '    Dim Result As New ArrayList

                    '    For Each Item As TableRow In Table.Rows
                    '        Result.Add(Item.Items(Column))
                    '    Next Item

                    '    Return New Token(Result)

                    'Case "columncount"
                    '    Return New Token(CType(Arg(0).Value, Table).Columns.Count)
            End Select

            Return Nothing
        End Function

    End Class

End Namespace
