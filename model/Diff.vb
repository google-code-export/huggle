﻿Imports System
Imports System.Collections.Generic
Imports System.Web.HttpUtility

Namespace Huggle

    'Represents a diff between two revisions

    <Diagnostics.DebuggerDisplay("{_Key}")>
    Friend Class Diff

        Private ReadOnly _Wiki As Wiki

        Private _AddedText As String
        Private _CacheState As CacheState
        Private _Html As String
        Private _Key As String

        Private WithEvents _Older As Revision
        Private WithEvents _Newer As Revision

        Friend Shared Event [New] As SimpleEventHandler(Of Diff)
        Friend Event StateChanged As SimpleEventHandler(Of Diff)

        Friend Sub New(ByVal key As String, ByVal wiki As Wiki)
            _Key = key
            _Wiki = wiki
        End Sub

        Friend ReadOnly Property AddedText() As String
            Get
                Return _AddedText
            End Get
        End Property

        Friend Property CachedAt As Date

        Friend Property CacheState() As CacheState
            Get
                Return _CacheState
            End Get
            Set(ByVal value As CacheState)
                _CacheState = value
                Log.Write("State for " & Page.Name & ":" & NewId & " is " & CacheState.ToString)
                RaiseEvent StateChanged(Me, New EventArgs(Of Diff)(Me))
            End Set
        End Property

        Friend ReadOnly Property Change() As Integer
            Get
                If _Older Is Nothing OrElse _Newer Is Nothing Then Return Integer.MinValue
                If _Older.Bytes = Integer.MinValue OrElse _Newer.Bytes = Integer.MinValue Then Return Integer.MinValue
                Return _Newer.Bytes - _Older.Bytes
            End Get
        End Property

        Friend Property Exists() As Boolean

        Friend Property Html() As String
            Get
                Return _Html
            End Get
            Set(ByVal value As String)
                'Namespace colouring
                Dim headerColor As String

                Select Case Newer.Page.Space
                    Case Wiki.Spaces.Article : headerColor = "ffffff"
                    Case Wiki.Spaces.User, Wiki.Spaces.UserTalk : headerColor = "d7d7ff"
                    Case Wiki.Spaces.Project, Wiki.Spaces.ProjectTalk : headerColor = "ffd2ff"
                    Case Wiki.Spaces.Talk : headerColor = "cdffcd"
                    Case Else : headerColor = "ffe6d2"
                End Select

                'Change size
                Dim editChange As String = ""

                If Change > Integer.MinValue Then
                    editChange = CStr(Change)
                    If Change > 0 Then editChange = "+" & editChange
                End If

                'Filter info
                Dim filterInfo As String = ""

                If Newer.Abuse IsNot Nothing Then filterInfo = Msg("diff-filterinfo", _
                    Newer.Abuse.Filter.Id, Newer.Abuse.Filter.Description)

                '_Html = My.Resources.DiffPage _
                '    .Replace("$IMAGE", If(Newer.Page.IsBlp, BlpIconHtml, "")) _
                '    .Replace("$CHANGESIZE", EditChange) _
                '    .Replace("$PAGETITLE", Newer.Page.Name) _
                '    .Replace("$SUMMARY", WikiSummaryToHtml(Newer.Summary)) _
                '    .Replace("$DIFF", value) _
                '    .Replace("$HEADERCOLOR", HeaderColor) _
                '    .Replace("$FILTERINFO", FilterInfo)

                'Extract changes from diff
                Dim pos As Integer = _Html.IndexOfI("class=""diff-addedline""")

                While pos >= 0
                    Dim line As String = _Html.Substring(pos).FromFirst(">").ToFirst("<")

                    If line.Contains("class=""diffchange""") Then
                        'Concatenate contents of 'diffchange' spans
                        While line.Contains("class=""diffchange""")
                            _AddedText &= HtmlDecode(StripHtml _
                                (line.FromFirst("class=""diffchange""").FromFirst(">").ToFirst("<"))) & LF
                            line = line.Substring(line.IndexOfI("class=""diffchange""") + 1)
                        End While

                    ElseIf line.Length > 0 Then
                        'No 'diffchange' spans and nothing on the other side => the whole line was added
                        Dim rowStart As String = _Html.Substring(Html.Substring(0, pos).LastIndexOfI("<tr>")) _
                            .ToFirst("class=""diff-addedline""")

                        If rowStart.Contains("<td colspan=""2"">&nbsp;</td>") _
                            Then _AddedText &= HtmlDecode(StripHtml(line)) & LF
                    End If

                    pos = _Html.IndexOfI("class=""diff-addedline""", pos + 1)
                End While

                'Format the page
                _Html = MakeWebPage(_Html, Page.Name)

                RaiseEvent [New](Me, New EventArgs(Of Diff)(Me))
                _Exists = True
                CachedAt = Date.UtcNow

                Dim newKey As String = Newer.Id.ToStringI & "|" & Older.Id.ToStringI

                Wiki.Diffs.Rekey(Me, newKey)
                _Key = newKey
                CacheState = CacheState.Cached
            End Set
        End Property

        Friend ReadOnly Property Key() As String
            Get
                Return _Key
            End Get
        End Property

        Friend ReadOnly Property Newer() As Revision
            Get
                Return _Newer
            End Get
        End Property

        Friend ReadOnly Property NewId() As String
            Get
                If _Newer IsNot Nothing Then Return CStr(_Newer.Id)
                Return "cur"
            End Get
        End Property

        Friend ReadOnly Property Older() As Revision
            Get
                Return _Older
            End Get
        End Property

        Friend ReadOnly Property OldId() As String
            Get
                If _Older IsNot Nothing Then Return CStr(_Older.Id)
                Return "prev"
            End Get
        End Property

        Friend ReadOnly Property Page() As Page
            Get
                If Newer IsNot Nothing Then Return Newer.Page
                Return Wiki.Pages(_Key.FromLast("|"))
            End Get
        End Property

        Friend ReadOnly Property Wiki() As Wiki
            Get
                Return _Wiki
            End Get
        End Property

        Private Sub EditStatesChanged() Handles _Newer.StateChanged, _Older.StateChanged
            RaiseEvent StateChanged(Me, New EventArgs(Of Diff)(Me))
        End Sub

        Public Overrides Function ToString() As String
            Return _Key
        End Function

    End Class

    Friend Class DiffCollection

        Private Wiki As Wiki
        Private ReadOnly _All As New Dictionary(Of String, Diff)

        Private Const CacheTrimInterval As Integer = 60000
        Private ReadOnly CacheTime As New TimeSpan(0, 10, 0)
        Private WithEvents CacheTimer As Windows.Forms.Timer

        Friend Sub New(ByVal wiki As Wiki)
            Me.Wiki = wiki

            CacheTimer = New Windows.Forms.Timer
            CacheTimer.Interval = CacheTrimInterval
            CacheTimer.Start()
        End Sub

        Friend ReadOnly Property All() As IList(Of Diff)
            Get
                Return _All.Values.ToList.AsReadOnly
            End Get
        End Property

        Friend Sub Rekey(ByVal diff As Diff, ByVal newKey As String)
            _All.Unmerge(diff.Key)
            _All.Merge(newKey, diff)
        End Sub

        Friend Sub Remove(ByVal diff As Diff)
            _All.Unmerge(diff.Key)
        End Sub

        Default Friend ReadOnly Property Item(ByVal first As Revision, ByVal second As Revision) As Diff
            Get
                Dim key As String = CStr(Math.Max(first.Id, second.Id) & "|" & Math.Min(first.Id, second.Id))

                If Not _All.ContainsKey(key) Then _All.Add(key, New Diff(key, Wiki))
                Return _All(key)
                Return Nothing
            End Get
        End Property

        Default Friend ReadOnly Property Item(ByVal newer As Revision) As Diff
            Get
                If newer.Prev IsNot Nothing Then Return Item(newer.Prev, newer)
                If Not _All.ContainsKey(CStr(newer.Id) & "|prev") _
                    Then _All.Add(CStr(newer.Id) & "|prev", New Diff(CStr(newer.Id) & "|prev", Wiki))
                Return _All(CStr(newer.Id) & "|prev")
            End Get
        End Property

        Default Friend ReadOnly Property Item(ByVal page As Page) As Diff
            Get
                If page.LastRev IsNot Nothing Then Return Item(page.LastRev)
                If Not _All.ContainsKey("cur|prev|" & page.Title) _
                    Then _All.Add("cur|prev|" & page.Title, New Diff("cur|prev|" & page.Title, Wiki))
                Return _All(CStr("cur|prev|" & page.Title))
            End Get
        End Property

        Private Sub TrimCache() Handles CacheTimer.Tick
            For Each diff As Diff In All
                If diff.CachedAt.Add(CacheTime) > Date.UtcNow Then Remove(diff)
            Next diff
        End Sub

    End Class

End Namespace
