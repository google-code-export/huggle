Imports System
Imports System.Collections.Generic
Imports System.Text.RegularExpressions

Namespace Huggle.Scripting

    Partial Class Evaluator

        Private Function StandardFunc(ByVal context As Object, ByVal func As Token, ByVal arg As Token()) As Token

            Select Case func.AsString
                Case "cancel" : Throw New TaskCancelledException

                    'Type conversions
                Case "type" : Return New Token(arg(0).ValueType.ToString)
                Case "string" : Return New Token(CStr(arg(0).Value))
                Case "boolean" : Return New Token(CBool(arg(0).Value))
                Case "number" : Return New Token(CDbl(arg(0).Value))
                Case "revision" : Return New Token(Wiki.Revisions(CInt(arg(0).AsNumber)))
                Case "user" : Return New Token(Wiki.Users.FromString(arg(0).AsString))
                Case "page" : If arg(0).ValueType = "String" Then Return New Token(Wiki.Pages.FromString(arg(0).AsString))
                Case "range" : Return New Token(New Range(arg(0).AsNumber, arg(1).AsNumber))

                Case "media" : If arg(0).ValueType = "Page" _
                    Then Return New Token(Wiki.Files(arg(0).AsPage)) _
                    Else Return New Token(Wiki.Files(arg(0).AsString))

                Case "cat" : If arg(0).ValueType = "Page" _
                    Then Return New Token(Wiki.Categories(arg(0).AsPage)) _
                    Else Return New Token(Wiki.Categories(arg(0).AsString))

                Case "date" : Return New Token(CDate(arg(0).Value))
                Case "is" : Return New Token(arg(0).ValueType.EqualsIgnoreCase(arg(1).AsString))

                    'Parameter name/value separator
                Case ":" : Return New Token(arg(0).Value.ToString & ":" & arg(1).Value.ToString)

                    'Logical
                Case "!", "not" : Return New Token(Not arg(0).AsBool)
                Case "&", "and" : Return New Token(arg(0).AsBool AndAlso arg(1).AsBool)
                Case "|", "or" : Return New Token(arg(0).AsBool OrElse arg(1).AsBool)
                Case "xor" : Return New Token(arg(0).AsBool Xor arg(1).AsBool)

                    'Comparison
                Case "=" : Return New Token(arg(0).ToString = arg(1).ToString)
                Case "!=" : Return New Token(arg(0).ToString <> arg(1).ToString)
                Case ">" : Return New Token(arg(0).AsNumber > arg(1).AsNumber)
                Case "<" : Return New Token(arg(0).AsNumber < arg(1).AsNumber)
                Case ">=" : Return New Token(arg(0).AsNumber >= arg(1).AsNumber)
                Case "<=" : Return New Token(arg(0).AsNumber <= arg(1).AsNumber)

                    'Math
                Case "-", "sub" : If arg.Length = 1 _
                    Then Return New Token(-arg(0).AsNumber) _
                    Else Return New Token(arg(0).AsNumber - arg(1).AsNumber)

                Case "*" : Return New Token(arg(0).AsNumber * arg(1).AsNumber)
                Case "/" : Return New Token(arg(0).AsNumber / arg(1).AsNumber)

                Case "+", "sum"
                    If arg(0).ValueType = "Number" Then
                        If arg.Length = 1 Then
                            Return New Token(+arg(0).AsNumber)
                        Else
                            Dim Result As Double = 0

                            For Each Item As Token In arg
                                Result += Item.AsNumber
                            Next Item

                            Return New Token(Result)
                        End If
                    Else
                        Return New Token(arg(0).AsString & arg(1).AsString)
                    End If

                    'More math
                Case "\", "div" : Return New Token(CLng(arg(0).AsNumber) \ CLng(arg(1).AsNumber))
                Case "%", "mod" : Return New Token(arg(0).AsNumber Mod arg(1).AsNumber)
                Case "^", "pow" : Return New Token(arg(0).AsNumber ^ arg(1).AsNumber)
                Case "abs" : Return New Token(Math.Abs(arg(0).AsNumber))
                Case "sign" : Return New Token(Math.Sign(arg(0).AsNumber))
                Case "sqrt" : Return New Token(Math.Sqrt(arg(0).AsNumber))

                Case "max"
                    Dim Result As Double = Double.MinValue

                    For Each Item As Token In arg
                        If Item.AsNumber > Result Then Result = Item.AsNumber
                    Next Item

                    Return New Token(Result)

                Case "min"
                    Dim Result As Double = Double.MaxValue

                    For Each Item As Token In arg
                        If Item.AsNumber < Result Then Result = Item.AsNumber
                    Next Item

                    Return New Token(Result)

                    'Advanced math
                Case "sin" : Return New Token(Math.Sin(arg(0).AsNumber))
                Case "cos" : Return New Token(Math.Cos(arg(0).AsNumber))
                Case "tan" : Return New Token(Math.Tan(arg(0).AsNumber))
                Case "sinh" : Return New Token(Math.Sinh(arg(0).AsNumber))
                Case "cosh" : Return New Token(Math.Cosh(arg(0).AsNumber))
                Case "tanh" : Return New Token(Math.Tanh(arg(0).AsNumber))
                Case "asin" : Return New Token(Math.Asin(arg(0).AsNumber))
                Case "acos" : Return New Token(Math.Acos(arg(0).AsNumber))
                Case "atan" : Return New Token(Math.Atan(arg(0).AsNumber))
                Case "ln" : Return New Token(Math.Log(arg(0).AsNumber))
                Case "log" : Return New Token(Math.Log(arg(0).AsNumber, arg(1).AsNumber))

                Case "classify"
                    Dim Number As Integer = CInt(arg(0).AsNumber)
                    Dim Interval As Integer = CInt(arg(1).AsNumber)

                    Dim ClassStart As Integer = (Number \ Interval) * Interval

                    Return New Token(New Range(ClassStart, ClassStart + Interval - 1))

                Case "lower"
                    If arg(0).ValueType = "Range" Then Return New Token(CType(arg(0).Value, Range).Lower) _
                        Else Return New Token(arg(0).AsString.ToLowerI)

                Case "upper"
                    If arg(0).ValueType = "Range" Then Return New Token(CType(arg(0).Value, Range).Upper) _
                        Else Return New Token(arg(0).AsString.ToUpperI)

                    'Strings
                Case "lowerfirst" : Return New Token(arg(0).AsString.Substring(0, 1).ToLowerI & arg(0).AsString.Substring(1))
                Case "upperfirst" : Return New Token(arg(0).AsString.Substring(0, 1).ToUpperI & arg(0).AsString.Substring(1))
                Case "length" : Return New Token(arg(0).AsString.Length)
                Case "empty" : Return New Token(arg(0).AsString = "")
                Case "startswith" : Return New Token(arg(0).AsString.StartsWithI(arg(1).AsString))
                Case "endswith" : Return New Token(arg(0).AsString.EndsWithI(arg(1).AsString))

                Case "replace" : Return New Token(arg(0).AsString.Replace(arg(1).AsString, arg(2).AsString))

                    'Regular expressions
                Case "match"
                    If arg.Length >= 3 Then
                        Dim Matches As MatchCollection = New Regex(arg(1).AsString).Matches(arg(0).AsString)
                        If Matches.Count >= arg(2).AsNumber _
                            Then Return New Token(Matches(CInt(arg(2).AsNumber - 1)).Value) Else Return New Token("")
                    Else
                        Dim Match As Match = New Regex(arg(1).AsString).Match(arg(0).AsString)
                        If Match.Success Then Return New Token(Match.Value) Else Return New Token("")
                    End If

                Case "like" : Return New Token(Regex.IsMatch(arg(0).AsString, "^" & arg(1).AsString & "$"))

                Case "replacepattern"
                    Return New Token(Regex.Replace(arg(0).AsString, arg(1).AsString, arg(2).AsString, RegexOptions.Compiled))
            End Select

            Return Nothing
        End Function

    End Class

End Namespace
