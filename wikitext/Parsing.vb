﻿Imports System.Text.RegularExpressions

Namespace Huggle.Wikitext

    Public NotInheritable Class Parsing

        'Contains various regular expressions for what approximates to parsing of wikitext documents

        Private Shared ReadOnly LinkSpace As String = "( |_|%20)*"
        Private Shared ReadOnly TransclusionSpace As String = "(\n| |_)*"

        Public Shared ReadOnly SectionPattern As New Regex("(?:\n|^)=+ *(.+?) *(=+) *(?:\n|$)", RegexOptions.Compiled)
        Public Shared ReadOnly SortkeyPattern As New Regex(
            "\{\{DEFAULTSORT:([^\}]+)\}\}", RegexOptions.IgnoreCase Or RegexOptions.Compiled)

        Private Sub New()
        End Sub

        Public Shared Function CategoryPattern(ByVal category As Category) As String
            Return LinkPattern(category.Page)
        End Function

        Public Shared Function BaseCatPattern(ByVal wiki As Wiki) As String
            Return "\[\[" & LinkSpace & NamePattern(wiki.Spaces.Category.Name & ":") &
                "[^\|\]]+" & LinkSpace & "(\|[^\]]*?)?\]\]"
        End Function

        Public Shared Function BaseInterlangPattern(ByVal wiki As Wiki) As String
            Return "\[\[" & LinkSpace & "(^:[\]]+)" & LinkSpace & "\]\]"
        End Function

        Public Shared Function LinkPattern(ByVal page As Page) As String
            Return "\[\[" & LinkSpace & TitlePattern(page) & LinkSpace & "(\|[^\]]*?)?\]\]"
        End Function

        Public Shared Function NamePattern(ByVal name As String) As String
            Return "[" & Regex.Escape(name.Substring(0, 1).ToUpperI & name.Substring(0, 1).ToLowerI) &
                "]" & Regex.Replace(Regex.Escape(name.Substring(1)), "(\\ |_|%20)+", LinkSpace)
        End Function

        Public Shared Function TitlePattern(ByVal page As Page) As String
            If page.IsArticle Then Return NamePattern(page.Name)

            Return "[" & page.Space.Name.Substring(0, 1).ToUpperI &
                page.Space.Name.Substring(0, 1).ToLowerI & "]" &
                Regex.Escape(page.Space.Name.Substring(1)) & LinkSpace & ":" & LinkSpace & NamePattern(page.Name)
        End Function

        Public Shared Function TransclusionPattern(ByVal page As Page) As String
            Return "\{\{" & TransclusionSpace & NamePattern(TransclusionName(page)) & TransclusionSpace & "(\}\}|\|)"
        End Function

        Public Shared Function LinkName(ByVal page As Page) As String
            If page.Space Is page.Wiki.Spaces.File OrElse page.Space Is page.Wiki.Spaces.Category _
                Then Return ":" & page.Title Else Return page.Title
        End Function

        Public Shared Function TransclusionName(ByVal page As Page) As String
            If page.Space Is page.Wiki.Spaces.Template Then Return page.Name.ToLowerI(0) & page.Name.Substring(1)
            If page.Space.IsArticleSpace Then Return ":" & page.Title.ToLowerI(0) & page.Title.Substring(1)
            Return page.Title
        End Function

    End Class

End Namespace