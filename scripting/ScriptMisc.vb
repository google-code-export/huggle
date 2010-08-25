﻿Imports Huggle.Actions
Imports Huggle.Wikitext
Imports System
Imports System.Collections.Generic
Imports System.Text.RegularExpressions

Namespace Huggle.Scripting

    Partial Class Evaluator

        Private Function MiscFunc(ByVal Context As Object, ByVal Func As Token, ByVal Arg As Token()) As Token

            Select Case Func.String
                Case "contains"
                    If Arg(0).ValueType = "String" _
                        Then Return New Token(Arg(0).String.Contains(Arg(1).String)) _
                        Else Return New Token(Arg(0).List.Contains(Arg(1).Value))

                Case "name"
                    Select Case Arg(0).ValueType
                        Case "Space" : Return New Token(Arg(0).Space.Name)
                        Case "User" : Return New Token(Arg(0).User.Name)
                        Case "Category" : Return New Token(CType(Arg(0).Value, Category).Name)
                        Case "Media" : Return New Token(Arg(0).Media.Name)
                        Case "Transclusion" : Return New Token(CType(Arg(0).Value, Transclusion).Page.Title)
                    End Select

                Case "exists"
                    Select Case Arg(0).ValueType
                        Case "Page"
                            If Not Arg(0).Page.ExistsKnown Then
                                InfoNeeded.Add(New BatchInfo(Wiki, Arg(0).Page, "info"))
                                Return Undefined
                            Else
                                Return New Token(Arg(0).Page.Exists)
                            End If

                            Return New Token(Arg(0).Page.Exists)
                        Case "Media" : Return New Token(Arg(0).Media.Exists)
                    End Select

                Case "isrevert"
                    Select Case Arg(0).ValueType
                        Case "Revision" : Return New Token(Arg(0).Revision.IsRevert)
                        Case "MediaRevision" : Return New Token(CType(Arg(0).Value, MediaRevision).IsRevert)
                    End Select

                Case "count"
                    Select Case Arg(0).ValueType
                        Case "Category"
                            Dim Cat As Category = CType(Arg(0).Value, Category)

                            If Cat.Count = -1 Then
                                InfoNeeded.Add(New BatchInfo(Wiki, Cat.Page, "info"))
                                Return Undefined
                            Else
                                Return New Token(Cat.Count)
                            End If

                            Return New Token(CType(Arg(0).Value, Category).Count)
                        Case "Dictionary" : Return New Token(Arg(0).Dictionary.Count)
                        Case "List" : Return New Token(Arg(0).List.Count)
                        Case "Table" : Return New Token(CType(Arg(0).Value, Table).Rows.Count)
                    End Select
            End Select

            Return Nothing
        End Function

    End Class

End Namespace