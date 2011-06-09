Imports System
Imports System.Collections.Generic
Imports System.Windows.Forms

Namespace Huggle.UI

    Friend Class LogView : Inherits Viewer

        Private ReadOnly LogTypes As Dictionary(Of String, List(Of String)) = Dictionary(
            Pair("all", List("all")),
            Pair("block", List("block", "reblock", "unblock")),
            Pair("delete", List("delete", "restore", "event", "revision")),
            Pair("move", List("move", "move_redir")),
            Pair("protect", List("protect", "modify", "unprotect")),
            Pair("newusers", List("create", "create2")),
            Pair("rights", List("rights")),
            Pair("patrol", List("patrol")),
            Pair("upload", List("upload", "overwrite", "revert")),
            Pair("other", List("other"))
            )

        Private FilterType As String
        Private FilterSubType As String
        Private Items As New List(Of LogItem)

        Public Sub New(ByVal session As Session)
            MyBase.New(session)
            InitializeComponent()
        End Sub

        Private Sub _Load() Handles Me.Load
            If Wiki.Extensions.Contains(CommonExtension.AbuseFilter) Then LogTypes.Add("abusefilter", List("abuse", "modify"))
            If Wiki.Extensions.Contains(CommonExtension.Moderation) Then LogTypes.Add("review", List("approve", "approve-a", "approve-ia", "approve2", "approve2i", "approve2i-a", "unapprove", "unapprove2"))
            If Wiki.Extensions.Contains(CommonExtension.GlobalBlocking) Then LogTypes.Add("globalblock", List("gblock2", "gunblock", "modify"))
            If Wiki.Extensions.Contains(CommonExtension.RenameUser) Then LogTypes.Add("renameuser", List("renameuser"))

            If Wiki.Extensions.Contains(CommonExtension.UnifiedLogin) Then
                LogTypes.Add("globalauth", List("setstatus"))
                LogTypes("newusers").Add("autocreate")
            End If

            For Each logType As String In LogTypes.Keys
                FilterTypeSelector.Items.Add(Msg("view-log-type-" & logType))
            Next logType

            FilterTypeSelector.SelectedIndex = 0

            AddHandler Wiki.Action, AddressOf Action
        End Sub

        Private Sub CustomFilter_Click() Handles CustomFilter.Click
            Using form As New CustomFilterForm
                form.ShowDialog()
            End Using
        End Sub

        Private Sub Action(ByVal sender As Object, ByVal e As EventArgs(Of QueueItem))
            Dim item As LogItem = TryCast(e.Value, LogItem)

            If item IsNot Nothing Then Items.Add(item)
        End Sub

        Private Sub FilterType_SelectedIndexChanged() Handles FilterTypeSelector.SelectedIndexChanged
            For Each item As KeyValuePair(Of String, List(Of String)) In LogTypes
                If FilterTypeSelector.SelectedItem.ToString = Msg("view-log-" & item.Key) Then
                    FilterType = item.Key
                    Exit For
                End If
            Next item

            FilterSubtypeSelector.Items.Clear()
            FilterSubtypeSelector.Items.Add(Msg("view-log-all"))
            FilterSubtypeSelector.SelectedIndex = 0

            If FilterType IsNot Nothing Then
                If LogTypes(FilterType).Count > 1 Then
                    For Each type As String In LogTypes(FilterType)
                        FilterSubtypeSelector.Items.Add(Msg("view-log-" & FilterType & "-" & type))
                    Next type
                End If
            End If
        End Sub

        Private Sub UpdateList()
            LogList.Clear()

            LogList.Columns.Add(Msg("view-log-column-id"))
            LogList.Columns.Add(Msg("view-log-column-time"))
            LogList.Columns.Add(Msg("view-log-column-action"))
            If FilterType <> "newusers" Then LogList.Columns.Add(Msg("view-log-column-user"))

            Select Case FilterType
                Case "all", "other"
                    LogList.Columns.Add(Msg("view-log-column-target"))
                    LogList.Columns.Add(Msg("view-log-column-status"))

                Case "block"
                    LogList.Columns.Add(Msg("view-log-column-targetuser"))
                    LogList.Columns.Add(Msg("view-log-column-blockexpires"))

                Case "delete"
                    LogList.Columns.Add(Msg("view-log-column-page"))

                Case "move"
                    LogList.Columns.Add(Msg("view-log-column-source"))
                    LogList.Columns.Add(Msg("view-log-column-destination"))

                Case "protect"
                    LogList.Columns.Add(Msg("view-log-column-page"))
                    LogList.Columns.Add(Msg("view-log-column-protcreate"))
                    LogList.Columns.Add(Msg("view-log-column-protedit"))
                    LogList.Columns.Add(Msg("view-log-column-protmove"))
                    LogList.Columns.Add(Msg("view-log-column-protexpires"))

                Case "newusers"
                    LogList.Columns.Add(Msg("view-log-column-creator"))
                    LogList.Columns.Add(Msg("view-log-column-newuser"))

                Case "rights"
                    LogList.Columns.Add(Msg("view-log-column-targetuser"))
                    LogList.Columns.Add(Msg("view-log-column-rightsadded"))
                    LogList.Columns.Add(Msg("view-log-column-rightsremoved"))

                Case "patrol"
                    LogList.Columns.Add(Msg("view-log-column-page"))
                    LogList.Columns.Add(Msg("view-log-column-patrolrev"))

                Case "upload"
                    LogList.Columns.Add(Msg("view-log-column-file"))

            End Select

            If FilterType <> "newusers" Then LogList.Columns.Add(Msg("view-log-column-comment"))

            Dim rows As New List(Of String())

            For Each item As LogItem In Items
                If MatchesFilter(item) Then rows.Add(MakeRow(item))
            Next item
        End Sub

        Private Function MatchesFilter(ByVal item As LogItem) As Boolean
            If FilterType = "all" Then Return True

            If FilterType <> "other" Then
                If item.Action.ToFirst("/") <> FilterType Then Return False
                If FilterSubType = "all" Then Return True
                If FilterSubType <> item.Action.FromFirst("/") Then Return False
            End If

            Return True
        End Function

        Private Function MakeRow(ByVal item As LogItem) As String()
            Dim result As New List(Of String)

            result.Add(item.Id.ToStringI)
            result.Add(FullDateString(item.Time))
            result.Add(item.Action.FromFirst("/"))
            result.Add(item.User.Name)

            For i As Integer = 4 To LogList.Columns.Count - 1
                Select Case LogList.Columns(i).Text
                    Case Msg("view-log-column-page") : result.Add(item.Page.Title)
                    Case Msg("view-log-column-target") : result.Add(item.Target)
                    Case Msg("view-log-column-targetuser") : result.Add(item.TargetUser.Name)
                    Case Msg("view-log-column-file") : result.Add(item.Page.Name)
                    Case Msg("view-log-column-creator") : result.Add(item.User.Name)
                    Case Msg("view-log-column-newuser") : result.Add(item.TargetUser.Name)
                    Case Msg("view-log-column-status") : result.Add(item.Status)

                    Case Msg("view-log-blockexpires") : result.Add(FullDateString(DirectCast(item, Block).Expires))

                    Case Msg("view-log-protexpires") : result.Add(FullDateString(DirectCast(item, Protection).Edit.Expires))
                    Case Msg("view-log-protcreate") : result.Add(DirectCast(item, Protection).Create.Level)
                    Case Msg("view-log-protedit") : result.Add(DirectCast(item, Protection).Edit.Level)
                    Case Msg("view-log-protmove") : result.Add(DirectCast(item, Protection).Move.Level)
                        
                    Case Msg("view-log-patrolrev") : result.Add(If(DirectCast(item, Review).Revision Is Nothing, "", DirectCast(item, Review).Revision.Id.ToStringI))

                    Case Msg("view-log-rightsadded") : result.Add("")
                    Case Msg("view-log-rightsremoved") : result.Add("")

                End Select
            Next i

            Return result.ToArray
        End Function

    End Class

End Namespace
