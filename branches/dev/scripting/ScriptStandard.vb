Imports System
Imports System.Collections.Generic
Imports System.Text.RegularExpressions

Namespace Huggle.Scripting

    Partial Class Evaluator

        Private Function StandardFunc(ByVal Context As Object, ByVal func As Token, ByVal arg As Token()) As Token

            Select Case func.String
                Case "cancel" : Throw New TaskCancelledException

                    'Type conversions
                Case "type" : Return New Token(arg(0).ValueType.ToString)
                Case "string" : Return New Token(CStr(arg(0).Value))
                Case "boolean" : Return New Token(CBool(arg(0).Value))
                Case "number" : Return New Token(CDbl(arg(0).Value))
                Case "revision" : Return New Token(Wiki.Revisions(CInt(arg(0).Number)))
                Case "user" : Return New Token(Wiki.Users.FromString(arg(0).String))
                Case "page" : If arg(0).ValueType = "String" Then Return New Token(Wiki.Pages.FromString(arg(0).String))
                Case "range" : Return New Token(New Range(arg(0).Number, arg(1).Number))

                Case "media" : If arg(0).ValueType = "Page" _
                    Then Return New Token(Wiki.Files(arg(0).Page)) _
                    Else Return New Token(Wiki.Files(arg(0).String))

                Case "cat" : If arg(0).ValueType = "Page" _
                    Then Return New Token(Wiki.Categories(arg(0).Page)) _
                    Else Return New Token(Wiki.Categories(arg(0).String))

                Case "date" : Return New Token(CDate(arg(0).Value))
                Case "is" : Return New Token(arg(0).ValueType.ToLower = arg(1).String.ToLower)

                    'Parameter name/value separator
                Case ":" : Return New Token(arg(0).Value.ToString & ":" & arg(1).Value.ToString)

                    'Logical
                Case "!", "not" : Return New Token(Not arg(0).Bool)
                Case "&", "and" : Return New Token(arg(0).Bool AndAlso arg(1).Bool)
                Case "|", "or" : Return New Token(arg(0).Bool OrElse arg(1).Bool)
                Case "xor" : Return New Token(arg(0).Bool Xor arg(1).Bool)

                    'Comparison
                Case "=" : Return New Token(arg(0).ToString = arg(1).ToString)
                Case "!=" : Return New Token(arg(0).ToString <> arg(1).ToString)
                Case ">" : Return New Token(arg(0).Number > arg(1).Number)
                Case "<" : Return New Token(arg(0).Number < arg(1).Number)
                Case ">=" : Return New Token(arg(0).Number >= arg(1).Number)
                Case "<=" : Return New Token(arg(0).Number <= arg(1).Number)

                    'Math
                Case "-", "sub" : If arg.Length = 1 _
                    Then Return New Token(-arg(0).Number) _
                    Else Return New Token(arg(0).Number - arg(1).Number)

                Case "*" : Return New Token(arg(0).Number * arg(1).Number)
                Case "/" : Return New Token(arg(0).Number / arg(1).Number)

                Case "+", "sum"
                    If arg(0).ValueType = "Number" Then
                        If arg.Length = 1 Then
                            Return New Token(+arg(0).Number)
                        Else
                            Dim Result As Double = 0

                            For Each Item As Token In arg
                                Result += Item.Number
                            Next Item

                            Return New Token(Result)
                        End If
                    Else
                        Return New Token(arg(0).String & arg(1).String)
                    End If

                    'More math
                Case "\", "div" : Return New Token(CLng(arg(0).Number) \ CLng(arg(1).Number))
                Case "%", "mod" : Return New Token(arg(0).Number Mod arg(1).Number)
                Case "^", "pow" : Return New Token(arg(0).Number ^ arg(1).Number)
                Case "abs" : Return New Token(Math.Abs(arg(0).Number))
                Case "sign" : Return New Token(Math.Sign(arg(0).Number))
                Case "sqrt" : Return New Token(Math.Sqrt(arg(0).Number))

                Case "max"
                    Dim Result As Double = Double.MinValue

                    For Each Item As Token In arg
                        If Item.Number > Result Then Result = Item.Number
                    Next Item

                    Return New Token(Result)

                Case "min"
                    Dim Result As Double = Double.MaxValue

                    For Each Item As Token In arg
                        If Item.Number < Result Then Result = Item.Number
                    Next Item

                    Return New Token(Result)

                    'Advanced math
                Case "sin" : Return New Token(Math.Sin(arg(0).Number))
                Case "cos" : Return New Token(Math.Cos(arg(0).Number))
                Case "tan" : Return New Token(Math.Tan(arg(0).Number))
                Case "sinh" : Return New Token(Math.Sinh(arg(0).Number))
                Case "cosh" : Return New Token(Math.Cosh(arg(0).Number))
                Case "tanh" : Return New Token(Math.Tanh(arg(0).Number))
                Case "asin" : Return New Token(Math.Asin(arg(0).Number))
                Case "acos" : Return New Token(Math.Acos(arg(0).Number))
                Case "atan" : Return New Token(Math.Atan(arg(0).Number))
                Case "ln" : Return New Token(Math.Log(arg(0).Number))
                Case "log" : Return New Token(Math.Log(arg(0).Number, arg(1).Number))

                Case "classify"
                    Dim Number As Integer = CInt(arg(0).Number)
                    Dim Interval As Integer = CInt(arg(1).Number)

                    Dim ClassStart As Integer = (Number \ Interval) * Interval

                    Return New Token(New Range(ClassStart, ClassStart + Interval - 1))

                Case "lower"
                    If arg(0).ValueType = "Range" Then Return New Token(CType(arg(0).Value, Range).Lower) _
                        Else Return New Token(arg(0).String.ToLower)

                Case "upper"
                    If arg(0).ValueType = "Range" Then Return New Token(CType(arg(0).Value, Range).Upper) _
                        Else Return New Token(arg(0).String.ToUpper)

                    'Strings
                Case "lowerfirst" : Return New Token(arg(0).String.Substring(0, 1).ToLower & arg(0).String.Substring(1))
                Case "upperfirst" : Return New Token(arg(0).String.Substring(0, 1).ToUpper & arg(0).String.Substring(1))
                Case "length" : Return New Token(arg(0).String.Length)
                Case "empty" : Return New Token(arg(0).String = "")
                Case "startswith" : Return New Token(arg(0).String.StartsWith(arg(1).String))
                Case "endswith" : Return New Token(arg(0).String.EndsWith(arg(1).String))

                Case "replace" : Return New Token(arg(0).String.Replace(arg(1).String, arg(2).String))

                    'Regular expressions
                Case "match"
                    If arg.Length >= 3 Then
                        Dim Matches As MatchCollection = New Regex(arg(1).String).Matches(arg(0).String)
                        If Matches.Count >= arg(2).Number _
                            Then Return New Token(Matches(CInt(arg(2).Number - 1)).Value) Else Return New Token("")
                    Else
                        Dim Match As Match = New Regex(arg(1).String).Match(arg(0).String)
                        If Match.Success Then Return New Token(Match.Value) Else Return New Token("")
                    End If

                Case "like" : Return New Token(Regex.IsMatch(arg(0).String, "^" & arg(1).String & "$"))

                Case "replacepattern"
                    Return New Token(Regex.Replace(arg(0).String, arg(1).String, arg(2).String, RegexOptions.Compiled))
            End Select

            Return Nothing
        End Function

    End Class

End Namespace
