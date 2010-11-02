Imports System
Imports System.Collections.Generic
Imports System.Text.RegularExpressions

Namespace Huggle.Scripting

    Public Class Parser

        Private Shared ReadOnly Delimiters As Char() = {""""c}
        Private Shared ReadOnly TokenPattern As New Regex _
            ("([\[\]();~,.]{1}|[+\-*/%\\:&=<>!]{1,2}|[\|^]{1,2}|\b(?:" & _
             "rlike|like|mod|union|filter|exclude|contains|containsany|is|reverse|group|groupby|map|" & _
             "sort|sortby|fold|unique|merge|limit|join|else|containspattern" & _
             ")\b)", RegexOptions.Compiled)

        Private Shared ReadOnly Operators As String() = { _
            "~", "&=", "-=", "+=", ":=", "!=", ";", ",", "[", "(", "+", "-", "/", "\", "*", "!", "&", _
            "|", "^", "%", "<", ">", "=", ">=", "<=", ".", ":", "::", "->", ",", _
            "rlike", "like", "mod", "union", "filter", "exclude", "contains", "containsany", "is", "else", _
            "reverse", "group", "groupby", "map", "sort", "sortby", "fold", "unique", "merge", "limit", "join", _
            "containspattern"}

        Private Shared ReadOnly Unarys As String() = {"group", "reverse", "unique", "sort"}

        Private OpStack As Stack(Of Token)
        Private Postfix As List(Of Token)
        Private ConditionEnd As Integer

        Public Function Parse(ByVal expression As String) As Token
            If expression = "require" Then Return New Token(False)

            Dim expr As String = expression.Remove(Tab, CR, LF)
            Dim escaped As String = ""
            Dim pos, endpos As Integer

            'Interpret escape codes
            expr = expr.Replace("\""", Convert.ToChar(65535))
            expr = expr.Replace("\n", LF)
            expr = expr.Replace("\\", "\")

            pos = expression.IndexOfAny(Delimiters)
            endpos = -1

            While pos >= 0
                If pos > 0 Then escaped &= expr.Substring(endpos + 1, pos - endpos - 1)
                endpos = expr.IndexOfI(expr(pos), pos + 1)
                If endpos = -1 Then Throw New ScriptException(Msg("query-delimiters"))

                Dim token As String = expr.Substring(pos, endpos - pos + 1)

                'Mask string literals
                For i As Integer = 0 To Operators.Length - 1
                    token = token.Replace(Operators(i), Convert.ToChar(65533 - i))
                Next i

                escaped &= token
                pos = expr.IndexOfAny(Delimiters, endpos + 1)
            End While

            escaped &= expr.Substring(endpos + 1, expr.Length - endpos - 1)

            'Tokenize expression
            Dim rawStrings As List(Of String) = TokenPattern.Split(escaped).ToList
            Dim tokens As New List(Of Token)
            Dim previous As Token = Nothing
            Dim strings As New List(Of String)

            'Trim token list
            For Each str As String In rawStrings
                If str.Trim.Length > 0 Then strings.Add(str.Trim)
            Next str

            'Identify tokens
            For i As Integer = 0 To strings.Count - 1
                Dim str As String = strings(i)

                If str = "else" Then str = ","

                If str.Length = 2 AndAlso (str(1) = "-"c OrElse str(1) = "+"c) Then
                    strings(i) = str(1).ToString
                    str = str(0).ToString
                    i -= 1
                End If

                Dim token As New Token(str)
                Dim add As Boolean = True
                Dim doubleValue As Double

                If str.IndexOfAny(Delimiters) = 0 Then
                    token.Type = TokenType.Constant

                    'Unmask string literals
                    For k As Integer = 0 To Operators.Length - 1
                        str = str.Replace(Convert.ToChar(65533 - k), Operators(k))
                    Next k

                    token.Value = str.Substring(1, Math.Max(0, str.Length - 2))

                ElseIf str = "," Then
                    token.Type = TokenType.Argument

                ElseIf Double.TryParse(str, doubleValue) Then
                    token.Value = doubleValue
                    token.Type = TokenType.Constant

                ElseIf str.ToLowerI = "true" Then
                    token.Value = True
                    token.Type = TokenType.Constant

                ElseIf str.ToLowerI = "false" Then
                    token.Value = False
                    token.Type = TokenType.Constant

                ElseIf Operators.Contains(str) AndAlso (previous Is Nothing _
                    OrElse str = "[" OrElse previous.Type <> TokenType.Argument) Then

                    token.Type = TokenType.Operator

                    'Operator precedence
                    Select Case str
                        Case ":" : token.Precedence = 1
                        Case ";" : token.Precedence = 2
                        Case ":=", "-=", "+=" : token.Precedence = 3
                        Case "->" : token.Precedence = 4
                        Case "+", "-" : token.Precedence = 5
                        Case "|" : token.Precedence = 6
                        Case "&" : token.Precedence = 7
                        Case "::", "." : token.Precedence = 9
                        Case "!" : token.Precedence = 10
                        Case Else : token.Precedence = 8
                    End Select

                    'Association
                    token.RightAssociative = (str = "^")

                    'Unary operators
                    If (str = "!" OrElse ((str = "+" OrElse str = "-") _
                        AndAlso (previous Is Nothing OrElse (previous.Type = TokenType.Operator _
                        OrElse previous.ToString = "(")))) Then

                        token.Type = TokenType.UnaryOperator
                        token.Precedence = 16
                        token.RightAssociative = True

                    ElseIf Unarys.Contains(str) Then
                        token.Type = TokenType.UnaryOperator
                        token.RightAssociative = False

                    ElseIf (previous Is Nothing OrElse (previous.Type = TokenType.Operator _
                        OrElse previous.ToString = "(")) AndAlso str <> "(" AndAlso str <> "[" Then

                        token.Type = TokenType.Identifier

                    ElseIf str = "[" Then
                        Dim list As New Token("list")
                        list.Type = TokenType.Function
                        tokens.Add(list)
                        previous = list
                        token.Type = TokenType.GroupStart
                        add = False

                    ElseIf str = "(" Then
                        token.Type = TokenType.GroupStart

                        If previous IsNot Nothing AndAlso (previous.Type = TokenType.Identifier _
                            OrElse previous.Type = TokenType.Operator) AndAlso Char.IsLetter(previous.AsString(0)) Then

                            previous.Type = TokenType.Function
                            add = False
                        End If
                    End If

                ElseIf str = ")" OrElse str = "]" Then
                    token.Type = TokenType.GroupEnd

                Else
                    token.Type = TokenType.Identifier
                End If

                If tokens.Count > 0 AndAlso tokens(tokens.Count - 1).ToString = ";" _
                    AndAlso (str = ")" OrElse str = ";") Then tokens.RemoveAt(tokens.Count - 1)

                If tokens.Count > 0 AndAlso tokens(tokens.Count - 1).ToString = "(" _
                    AndAlso str = ";" Then add = False

                If add Then
                    tokens.Add(token)
                    previous = token
                End If
            Next i

            If tokens.Count > 0 AndAlso tokens(tokens.Count - 1).Type = TokenType.Operator _
                AndAlso tokens(tokens.Count - 1).AsString = ";" Then tokens.RemoveAt(tokens.Count - 1)

            'Convert infix to postfix
            Dim conditionStack As New Stack(Of Token)
            Dim functionStack As New Stack(Of FunctionData)
            Dim currentFn As FunctionData

            currentFn.Count = 0
            currentFn.Function = Nothing

            Postfix = New List(Of Token)
            OpStack = New Stack(Of Token)
            ConditionEnd = -1

            For i As Integer = 0 To tokens.Count - 1
                Dim token As Token = tokens(i)

                Select Case token.Type
                    Case TokenType.Constant, TokenType.Identifier
                        Postfix.Add(token)
                        If currentFn.Function IsNot Nothing Then currentFn.Count += 1

                    Case TokenType.Function
                        If currentFn.Function IsNot Nothing Then currentFn.Count += 1
                        functionStack.Push(currentFn)
                        currentFn.Count = 0
                        currentFn.Function = token
                        OpStack.Push(token)

                    Case TokenType.Argument
                        If currentFn.Function Is Nothing _
                            Then Throw New ScriptException(Msg("query-nofunction"))

                        currentFn.Function.Arguments += 1
                        currentFn.Count = 0
                        PopStack(True)

                        If OpStack.Peek.Type <> TokenType.Function _
                            Then Throw New ScriptException(Msg("query-nofunction"))

                        While conditionStack.Count > 0
                            token = conditionStack.Pop
                        End While

                    Case TokenType.GroupStart
                        OpStack.Push(token)

                    Case TokenType.GroupEnd
                        PopStack(True)

                        While conditionStack.Count > 0
                            token = conditionStack.Pop
                        End While

                        Dim et As TokenType

                        If OpStack.Count > 0 Then
                            token = OpStack.Pop
                            et = token.Type

                        ElseIf token.Type = TokenType.GroupEnd Then
                            Throw New ScriptException(Msg("query-parentheses"))
                        End If

                        If et = TokenType.Function Then
                            If currentFn.Count > 0 Then currentFn.Function.Arguments += 1

                            Postfix.Add(New Token(currentFn.Function.Arguments, TokenType.ArgCount))
                            Postfix.Add(currentFn.Function)
                            currentFn = functionStack.Pop

                        ElseIf et <> TokenType.GroupStart Then
                            Throw New ScriptException(Msg("query-parentheses"))
                        End If

                    Case TokenType.Operator, TokenType.UnaryOperator
                        PushStack(token)
                End Select
            Next i

            PopStack(False)

            While conditionStack.Count > 0
                conditionStack.Pop()
            End While

            While OpStack.Count > 0
                Dim Top As Token = OpStack.Pop
                If Top.ToString = "(" Then Throw New ScriptException(Msg("query-parentheses"))

                Postfix.Add(Top)
            End While

            'Build parse tree
            Dim CurrentNode As Token = Nothing
            Dim CountStack As New Stack(Of Integer)
            Dim ParseRoot As Token = Nothing

            For i As Integer = Postfix.Count - 1 To 0 Step -1
                Dim Token As Token = Postfix(i)

                If ParseRoot Is Nothing Then
                    ParseRoot = Token
                    CurrentNode = ParseRoot
                Else
                    Select Case CurrentNode.Type
                        Case TokenType.UnaryOperator
                            CountStack.Push(1)
                            CurrentNode.FirstChild = Token
                            Token.Parent = CurrentNode
                            CurrentNode = Token

                        Case TokenType.Operator
                            CountStack.Push(2)
                            CurrentNode.FirstChild = Token
                            Token.Parent = CurrentNode
                            CurrentNode = Token

                        Case TokenType.ArgCount
                            If CurrentNode.AsNumber = 0 Then
                                CurrentNode.Parent.NextSibling = Token
                                Token.Parent = CurrentNode.Parent.Parent
                                CurrentNode = Token
                            Else
                                CurrentNode.Parent.FirstChild = Token
                                Token.Parent = CurrentNode.Parent
                                CurrentNode = Token
                            End If

                        Case TokenType.Constant, TokenType.Identifier, TokenType.EmptyFunction
                            If CountStack.Count = 0 Then _
                                Throw New ScriptException(Msg("query-unexpectedend", Token.ToString))

                            CountStack.Push(CountStack.Pop - 1)

                            While (CountStack.Peek = 0)
                                CountStack.Pop()
                                If CountStack.Count = 0 Then Exit While
                                CountStack.Push(CountStack.Pop - 1)
                                CurrentNode = CurrentNode.Parent
                            End While

                            CurrentNode.NextSibling = Token
                            Token.Parent = CurrentNode.Parent
                            CurrentNode = Token

                        Case TokenType.Function
                            If Token.Type = TokenType.ArgCount AndAlso Token.AsNumber = 0 Then
                                CurrentNode.Type = TokenType.EmptyFunction

                            ElseIf Token.Type = TokenType.ArgCount Then
                                CountStack.Push(CInt(Token.AsNumber))
                                Token.Parent = CurrentNode
                                CurrentNode = Token
                            Else
                                Token.Type = TokenType.Constant
                                Token.Parent = CurrentNode
                                CurrentNode = Token
                            End If
                    End Select
                End If
            Next i

            Return ParseRoot
        End Function

        Private Sub PushStack(ByVal Token As Token)
            While OpStack.Count > 0
                Dim Top As Token = OpStack.Peek

                If Token.Precedence > Top.Precedence Then Exit While

                If Top.Type = TokenType.GroupStart OrElse Top.Type = TokenType.Function OrElse _
                    (Token.RightAssociative AndAlso Token.Precedence = Top.Precedence) Then Exit While

                Postfix.Add(OpStack.Pop)
            End While

            OpStack.Push(Token)
        End Sub

        Private Sub PopStack(ByVal GroupEnd As Boolean)
            While OpStack.Count > 0
                Dim Top As Token = OpStack.Peek

                If Not GroupEnd AndAlso OpStack.Count = ConditionEnd Then Exit While

                If Top.Type = TokenType.GroupStart OrElse Top.Type = TokenType.Function Then
                    If OpStack.Count = ConditionEnd Then ConditionEnd = -1
                    Exit While
                End If

                Postfix.Add(OpStack.Pop)
            End While
        End Sub

    End Class

End Namespace