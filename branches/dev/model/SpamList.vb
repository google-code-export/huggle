Imports System
Imports System.Collections.Generic
Imports System.IO
Imports System.Text.RegularExpressions

Namespace Huggle

    Friend Class SpamListCollection

        Private _All As New Dictionary(Of Page, SpamList)
        Private _Wiki As Wiki

        Public Sub New(ByVal wiki As Wiki)
            _Wiki = wiki
        End Sub

        Public ReadOnly Property All As IList(Of SpamList)
            Get
                Return _All.Values.ToList.AsReadOnly
            End Get
        End Property

        Default Public ReadOnly Property FromPage(ByVal page As Page) As SpamList
            Get
                If page.Wiki IsNot Wiki Then Return Nothing

                If Not _All.ContainsKey(page) Then _All.Add(page, New SpamList(page))
                Return _All(page)
            End Get
        End Property

        Public Function IsMatch(ByVal session As Session, ByVal link As String) As Boolean
            Dim allMatches As List(Of SpamListEntry) = Matches(session, link)

            For Each entry As SpamListEntry In allMatches
                If entry.List.Action = SpamListAction.Permit Then Return False
            Next entry

            Return (allMatches.Count > 0)
        End Function

        Public Function Matches(ByVal session As Session, ByVal link As String) As List(Of SpamListEntry)
            Dim result As New List(Of SpamListEntry)

            For Each list As SpamList In All
                Dim match As SpamListEntry = list.Match(session, link)
                If match IsNot Nothing Then result.Add(match)
            Next list

            Return result
        End Function

        Public ReadOnly Property Wiki As Wiki
            Get
                Return _Wiki
            End Get
        End Property

    End Class

    <Diagnostics.DebuggerDisplay("{ToString()}")>
    Friend Class SpamList

        Private _Entries As List(Of SpamListEntry)
        Private _IsLoaded As Boolean
        Private _Page As Page

        Public Sub New(ByVal page As Page)
            _Page = page
        End Sub

        Public Property Action As SpamListAction

        Public Property Entries As List(Of SpamListEntry)
            Get
                If Not IsLoaded Then Return Nothing
                Return _Entries
            End Get
            Set(ByVal value As List(Of SpamListEntry))
                _Entries = value
            End Set
        End Property

        Public Property IsCustom As Boolean

        Public ReadOnly Property IsLoaded As Boolean
            Get
                Return _IsLoaded
            End Get
        End Property

        Public Function IsMatch(ByVal session As Session, ByVal link As String) As Boolean
            Return (Match(session, link) IsNot Nothing)
        End Function

        Public Function Match(ByVal session As Session, ByVal link As String) As SpamListEntry
            For Each entry As SpamListEntry In Entries
                If entry.IsMatch(session, link) Then Return entry
            Next entry

            Return Nothing
        End Function

        Public ReadOnly Property Page As Page
            Get
                Return _Page
            End Get
        End Property

        Public Overrides Function ToString() As String
            Return Page.Title
        End Function

        Public Sub Load(ByVal text As String)
            _Entries = Parse(text)
            _IsLoaded = True
        End Sub

        Private Function Parse(ByVal text As String) As List(Of SpamListEntry)
            Log.Debug("Parsing spam blacklist '{0}'".FormatI(Page.FullTitle))

            Dim result As New List(Of SpamListEntry)
            If text Is Nothing Then Return result

            Using reader As New StringReader(text)
                Dim line As String = reader.ReadLine

                While line IsNot Nothing
                    Dim lineComment As String = Nothing
                    If line.Contains("#") Then lineComment = line.FromFirst("#").Substring(1).Trim
                    Dim pattern As String = line.ToFirst("#").Trim

                    If pattern.Length > 0 Then
                        Try
                            Dim entry As New SpamListEntry(Me, New Regex(pattern))
                            entry.Comment = lineComment
                            entry.DisplayText = pattern.Replace("\.", ".").Remove("\b")

                        Catch ex As ArgumentException
                            Log.Debug("Warning: spam blacklist pattern '{0}' on '{1}' is malformed, check syntax".
                                FormatI(pattern, Page.FullTitle))
                        End Try
                    End If

                    line = reader.ReadLine
                End While
            End Using

            Return result
        End Function

    End Class

    <Diagnostics.DebuggerDisplay("{ToString()}")>
    Friend Class SpamListEntry

        Private _List As SpamList
        Private _Pattern As Regex

        Public Sub New(ByVal list As SpamList, ByVal pattern As Regex)
            _List = list
            _Pattern = pattern
        End Sub

        Public Property Comment As String
        Public Property DisplayText As String

        Public ReadOnly Property IsMatch(ByVal session As Session, ByVal link As String) As Boolean
            Get
                Return Pattern.IsMatch(link)
            End Get
        End Property

        Public Property LastModifiedAt As Date
        Public Property LastModifiedBy As User

        Public ReadOnly Property List As SpamList
            Get
                Return _List
            End Get
        End Property

        Public ReadOnly Property Pattern As Regex
            Get
                Return _Pattern
            End Get
        End Property

        Public Overrides Function ToString() As String
            Return DisplayText
        End Function

    End Class

    Public Enum SpamListAction As Integer
        : Deny : Permit
    End Enum

End Namespace
