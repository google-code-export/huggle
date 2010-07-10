Imports System.Text.RegularExpressions

Namespace Huggle.Wikitext

    Public NotInheritable Class Parsing

        'Contains various regular expressions for what approximates to parsing of wikitext documents

        Private Shared ReadOnly LinkSpace As String = "( |_|%20)*"
        Private Shared ReadOnly TransclusionSpace As String = "(\n| |_)*"

        Public Shared ReadOnly SectionPattern As New Regex("(?:\n|^)=+ *(.+?) *(=+) *(?:\n|$)", RegexOptions.Compiled)
        Public Shared ReadOnly SortkeyPattern As New Regex _
            ("\{\{DEFAULTSORT:([^\}]+)\}\}", RegexOptions.IgnoreCase Or RegexOptions.Compiled)

        Private Sub New()
        End Sub

        Public Shared Function CategoryPattern(ByVal Category As Category) As String
            Return LinkPattern(Category.Page)
        End Function

        Public Shared Function BaseCatPattern(ByVal Wiki As Wiki) As String
            Return "\[\[" & LinkSpace & NamePattern(Wiki.Spaces.Category.Name & ":") & _
                "[^\|\]]+" & LinkSpace & "(\|[^\]]*?)?\]\]"
        End Function

        Public Shared Function BaseInterlangPattern(ByVal Wiki As Wiki) As String
            Return "\[\[" & LinkSpace & "(^:[\]]+)" & LinkSpace & "\]\]"
        End Function

        Public Shared Function LinkPattern(ByVal Page As Page) As String
            Return "\[\[" & LinkSpace & TitlePattern(Page) & LinkSpace & "(\|[^\]]*?)?\]\]"
        End Function

        Public Shared Function NamePattern(ByVal Name As String) As String
            Return "[" & Regex.Escape(Name.Substring(0, 1).ToUpper & Name.Substring(0, 1).ToLower) & _
                "]" & Regex.Replace(Regex.Escape(Name.Substring(1)), "(\\ |_|%20)+", LinkSpace)
        End Function

        Public Shared Function TitlePattern(ByVal Page As Page) As String
            If Page.IsArticle Then Return NamePattern(Page.Name)

            Return "[" & Page.Space.Name.Substring(0, 1).ToUpper & Page.Space.Name.Substring(0, 1).ToLower & "]" & _
                Regex.Escape(Page.Space.Name.Substring(1)) & LinkSpace & ":" & LinkSpace & NamePattern(Page.Name)
        End Function

        Public Shared Function TransclusionPattern(ByVal Page As Page) As String
            Return "\{\{" & TransclusionSpace & NamePattern(TransclusionName(Page)) & TransclusionSpace & "(\}\}|\|)"
        End Function

        Public Shared Function LinkName(ByVal Page As Page) As String
            If Page.Space Is Page.Wiki.Spaces.File OrElse Page.Space Is Page.Wiki.Spaces.Category _
                Then Return ":" & Page.Title Else Return Page.Title
        End Function

        Public Shared Function TransclusionName(ByVal Page As Page) As String
            If Page.Space Is Page.Wiki.Spaces.Template Then Return Page.Name.ToLower()(0) & Page.Name.Substring(1)
            If Page.Space.IsArticleSpace Then Return ":" & Page.Title.ToLower()(0) & Page.Title.Substring(1)
            Return Page.Title
        End Function

    End Class

End Namespace