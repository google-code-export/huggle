Imports Huggle.Queries
Imports Huggle.Wikitext
Imports System
Imports System.Collections.Generic
Imports System.Text.RegularExpressions

Namespace Huggle.Scripting

    Partial Class Evaluator

        Private Function MiscFunc(ByVal context As Object, ByVal func As Token, ByVal arg As Token()) As Token

            Select Case func.AsString
                Case "contains"
                    If arg(0).ValueType = "String" _
                        Then Return New Token(arg(0).AsString.Contains(arg(1).AsString)) _
                        Else Return New Token(arg(0).AsList.Contains(arg(1).Value))

                Case "name"
                    Select Case arg(0).ValueType
                        Case "Space" : Return New Token(arg(0).AsSpace.Name)
                        Case "User" : Return New Token(arg(0).AsUser.Name)
                        Case "Category" : Return New Token(CType(arg(0).Value, Category).Name)
                        Case "Media" : Return New Token(arg(0).AsMedia.Name)
                        Case "Transclusion" : Return New Token(CType(arg(0).Value, Transclusion).Page.Title)
                    End Select

                Case "exists"
                    Select Case arg(0).ValueType
                        Case "Page"
                            If Not arg(0).AsPage.ExistsKnown Then
                                InfoNeeded.Add(New BatchInfo(Wiki, arg(0).AsPage, "info"))
                                Return Undefined
                            Else
                                Return New Token(arg(0).AsPage.Exists)
                            End If

                            Return New Token(arg(0).AsPage.Exists)
                        Case "Media" : Return New Token(arg(0).AsMedia.Exists)
                    End Select

                Case "isrevert"
                    Select Case arg(0).ValueType
                        Case "Revision" : Return New Token(arg(0).AsRevision.IsRevert)
                        Case "MediaRevision" : Return New Token(CType(arg(0).Value, FileRevision).IsRevert)
                    End Select

                Case "count"
                    Select Case arg(0).ValueType
                        Case "Category"
                            Dim Cat As Category = CType(arg(0).Value, Category)

                            If Cat.Count = -1 Then
                                InfoNeeded.Add(New BatchInfo(Wiki, Cat.Page, "info"))
                                Return Undefined
                            Else
                                Return New Token(Cat.Count)
                            End If

                            Return New Token(CType(arg(0).Value, Category).Count)
                        Case "Dictionary" : Return New Token(arg(0).AsDictionary.Count)
                        Case "List" : Return New Token(arg(0).AsList.Count)
                        Case "Table" : Return New Token(CType(arg(0).Value, Table).Rows.Count)
                    End Select
            End Select

            Return Nothing
        End Function

    End Class

End Namespace
