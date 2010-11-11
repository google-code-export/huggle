Imports System
Imports System.Collections.Generic
Imports System.Text.RegularExpressions

Namespace Huggle

    <Diagnostics.DebuggerDisplay("{ToString()}")>
    Friend Class TitleList

        Private _Entries As List(Of TitleListEntry)
        Private _IsLoaded As Boolean
        Private _Location As Page

        Public Sub New(ByVal location As Page)
            _Location = location
        End Sub

        Public ReadOnly Property Entries As List(Of TitleListEntry)
            Get
                If Not IsLoaded Then Return Nothing

                If _Entries Is Nothing Then _Entries = Parse(Location.Text)
                Return _Entries
            End Get
        End Property

        Public ReadOnly Property IsLoaded As Boolean
            Get
                Return (Location.Text IsNot Nothing)
            End Get
        End Property

        Public ReadOnly Property IsMatch _
            (ByVal session As Session, ByVal title As String, ByVal action As TitleListAction) As Boolean

            Get
                If Entries Is Nothing Then Return False

                For Each entry As TitleListEntry In Entries
                    If entry.IsMatch(session, title, action) Then Return True
                Next entry

                Return False
            End Get
        End Property

        Public ReadOnly Property Location As Page
            Get
                Return _Location
            End Get
        End Property

        Public Overrides Function ToString() As String
            Return Location.Title
        End Function

        Private Function Parse(ByVal text As String) As List(Of TitleListEntry)
            Log.Debug("Parsing title blacklist '{0}'".FormatI(Location.FullTitle))

            Dim result As New List(Of TitleListEntry)
            Dim lastComment As String = Nothing

            If text IsNot Nothing Then
                For Each line As String In text.Split(LF)
                    If line.Contains("#") Then lastComment = line.FromFirst("#").Remove("#").Trim

                    'I totally lifted these regexen from the TitleBlacklist extension source. So sue me.
                    line = Regex.Replace(line, "^\s*([^#]*)\s*((.*)?)$", "$1", RegexOptions.Compiled)

                    Dim match As Match = Regex.Match(line, "^(.*?)(\s*<([^<>]*)>)?$", RegexOptions.Compiled)
                    If Not match.Success Then Continue For

                    Dim pattern As String = match.Groups(1).Value
                    pattern = pattern.Trim.Replace("_", " ")
                    If pattern.Length = 0 Then Continue For

                    Dim options As New List(Of TitleListOption)
                    Dim errorMessage As String = Nothing

                    For Each opt As String In Regex.Split(match.Groups(3).Value, "\s*\|\s*", RegexOptions.Compiled)
                        Select Case opt.Trim.ToLowerI
                            Case "" 'ignore
                            Case "autoconfirmed" : options.Merge(TitleListOption.AutoConfirmed)
                            Case "casesensitive" : options.Merge(TitleListOption.CaseSensitive)
                            Case "moveonly" : options.Merge(TitleListOption.MoveOnly)
                            Case "newaccountonly" : options.Merge(TitleListOption.NewAccountOnly)
                            Case "noedit" : options.Merge(TitleListOption.NoEdit)
                            Case "reupload" : options.Merge(TitleListOption.ReUpload)

                            Case Else : Log.Debug("Ignored unrecognized title blacklist option '{0}' on {1}" _
                                .FormatI(opt.Trim.ToLowerI, Location.FullTitle))
                        End Select

                        If opt.StartsWithIgnoreCase("errmsg") AndAlso opt.Contains("=") Then errorMessage = opt.FromFirst("=")
                    Next opt

                    'Validate pattern

                    Try
                        Dim reg As New Regex("^" & pattern & "$",
                            If(options.Contains(TitleListOption.CaseSensitive), RegexOptions.None, RegexOptions.IgnoreCase))

                    Catch ex As ArgumentException
                        Log.Debug("Warning: title blacklist pattern '{0}' on '{1}' is malformed, check syntax" _
                            .FormatI(pattern, Location.FullTitle))
                        Continue For
                    End Try

                    'Additional pattern validation to find some mistakes
                    If pattern.StartsWithI("^") OrElse pattern.EndsWithI("$") Then
                        Log.Debug("Warning: title blacklist pattern '{0}' on '{1}' has superfluous anchor, check syntax" _
                            .FormatI(pattern, Location.FullTitle))
                    Else
                        Try
                            Dim reg As New Regex(pattern)
                        Catch ex As ArgumentException
                            Log.Debug("Warning: title blacklist pattern '{0}' on '{1}' is malformed without starting anchor, check syntax" _
                                .FormatI(pattern, Location.FullTitle))
                        End Try
                    End If

                    Dim entry As New TitleListEntry("^" & pattern & "$")
                    entry.Comment = lastComment
                    entry.ErrorMessage = errorMessage
                    entry.Options.AddRange(options)

                    result.Add(entry)
                Next line
            End If

            Return result
        End Function

    End Class

    <Diagnostics.DebuggerDisplay("{ToString()}")>
    Friend Class TitleListEntry

        Private _Options As New List(Of TitleListOption)
        Private _Pattern As String

        Public Sub New(ByVal pattern As String)
            _Pattern = pattern
        End Sub

        Public Property Comment As String
        Public Property ErrorMessage As String

        Public ReadOnly Property IsMatch _
            (ByVal session As Session, ByVal title As String, ByVal action As TitleListAction) As Boolean

            Get
                Select Case action
                    Case TitleListAction.Create : If Options.Contains(TitleListOption.MoveOnly) _
                        OrElse Options.Contains(TitleListOption.NewAccountOnly) Then Return False

                    Case TitleListAction.CreateAccount : If Options.Contains(TitleListOption.MoveOnly) Then Return False
                    Case TitleListAction.Edit : If Not Options.Contains(TitleListOption.NoEdit) Then Return False
                    Case TitleListAction.Move : If Options.Contains(TitleListOption.NewAccountOnly) Then Return False

                    Case TitleListAction.ReUpload : If Options.Contains(TitleListOption.MoveOnly) _
                        OrElse Options.Contains(TitleListOption.NewAccountOnly) _
                        OrElse Options.Contains(TitleListOption.ReUpload) Then Return False

                    Case TitleListAction.Upload : If Options.Contains(TitleListOption.MoveOnly) _
                        OrElse Options.Contains(TitleListOption.NewAccountOnly) Then Return False
                End Select

                If session.IsAutoconfirmed AndAlso Options.Contains(TitleListOption.AutoConfirmed) Then Return True

                Dim realPattern As String = Pattern

                'Handle magic words
                For Each match As Match In Regex.Matches(pattern, "{{\s*([a-z]+)\s*:\s*(.+?)\s*}}", RegexOptions.Compiled)
                    Dim word As String = match.Groups(1).Value

                    Select Case word.ToLowerI
                        Case "ns"
                            realPattern = realPattern.Replace(word, session.Wiki.Spaces(CInt(match.Groups(2).Value)).Name)

                        Case "int" 'unimplemented
                    End Select
                Next match

                Return Regex.IsMatch(title, realPattern)
            End Get
        End Property

        Public ReadOnly Property Options As List(Of TitleListOption)
            Get
                Return _Options
            End Get
        End Property

        Public ReadOnly Property Pattern As String
            Get
                Return _Pattern
            End Get
        End Property

        Public Overrides Function ToString() As String
            Dim result As String = Pattern.Substring(1, Pattern.Length - 2)

            If Options.Count > 0 OrElse ErrorMessage IsNot Nothing Then
                result &= " <"
                If Options.Count > 0 Then result &= Options.Join("|").ToLowerI
                If ErrorMessage IsNot Nothing Then result &= "|errmsg=" & ErrorMessage
                result &= ">"
            End If

            Return result
        End Function

    End Class

    Public Enum TitleListOption As Integer
        : AutoConfirmed : CaseSensitive : MoveOnly : NoEdit : NewAccountOnly : ReUpload
    End Enum

    Public Enum TitleListAction As Integer
        : Edit : Create : CreateAccount : Move : ReUpload : Upload
    End Enum

End Namespace
