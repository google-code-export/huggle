Imports System.Text.Encoding
Imports System.Threading
Imports System.Web.HttpUtility

Namespace Requests

    Class RevertRequest : Inherits Request

        'Revert to something

        Public Edit As Edit, Summary As String

        Public Sub Start()
            If Edit IsNot Nothing AndAlso Edit.Page IsNot Nothing Then
                LogProgress("Reverting edit to '" & Edit.Page.Name & "'...")

                Dim RequestThread As New Thread(AddressOf Process)
                RequestThread.IsBackground = True
                RequestThread.Start()
            End If
        End Sub

        Private Sub Process()
            Dim Data As EditData = GetEditData(Edit.Page, Edit.Id)

            If Data.Error Then
                Callback(AddressOf Failed)
                Exit Sub

            ElseIf Data.Creating Then
                Callback(AddressOf NoPage)
                Exit Sub
            End If

            If Summary IsNot Nothing _
                Then Data.Summary = Summary.Replace("$1", Edit.Page.LastEdit.User.Name).Replace("$2", Edit.User.Name) _
                Else Data.Summary = GetReversionSummary(Edit)

            Data.Minor = Config.MinorReverts
            Data.Watch = Config.WatchReverts

            Data = PostEdit(Data)

            If Data.Error Then
                Callback(AddressOf Failed)

            ElseIf Data.Result.Contains("<div id=""mw-spamprotectiontext"">") Then
                Callback(AddressOf SpamBlacklist)

            Else
                Callback(AddressOf Done)
            End If
        End Sub

        Private Sub Done()
            If Config.WatchReverts Then
                If Not Watchlist.Contains(Edit.Page.SubjectPage) Then Watchlist.Add(Edit.Page.SubjectPage)
                MainForm.UpdateWatchButton()
            End If

            If State = States.Cancelled Then UndoEdit(Edit.Page) Else Complete()
        End Sub

        Private Sub NoPage()
            Log("Did not revert edit to '" & Edit.Page.Name & "' because the page does not exist")
            Fail()
        End Sub

        Private Sub SpamBlacklist()
            If MessageBox.Show("Edit to '" & Edit.Page.Name & "' was blocked by the spam blacklist." & CRLF & _
                "Edit page manually?", "Huggle", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation) _
                = DialogResult.Yes Then

                Dim NewEditForm As New EditForm
                NewEditForm.Page = Edit.Page
                NewEditForm.Show()
            Else
                Log("Did not revert edit to '" & Edit.Page.Name & "' because the spam blacklist would not allow it")
            End If

            Fail()
        End Sub

        Private Sub Failed()
            Log("Failed to revert '" & Edit.Page.Name & "'")
            Fail()
        End Sub

        Private Function GetReversionSummary(ByVal Edit As Edit) As String
            Dim RevertedUsers As New List(Of String)
            Dim ReversionLength As Integer = 0
            Dim RevertingEdit As Edit = Edit.Page.LastEdit
            Dim EndPart, Summary As String

            Dim WReverted As String = Config.MultipleRevertSummaryParts(0)
            Dim WEditBy As String = Config.MultipleRevertSummaryParts(1)
            Dim WEditsBy As String = Config.MultipleRevertSummaryParts(2)
            Dim WAnd As String = Config.MultipleRevertSummaryParts(3)
            Dim WOtherUsers As String = Config.MultipleRevertSummaryParts(4)
            Dim WToLastVersionBy As String = Config.MultipleRevertSummaryParts(5)
            Dim WToAnOlderVersionBy As String = Config.MultipleRevertSummaryParts(6)

            While RevertingEdit IsNot Edit AndAlso RevertingEdit IsNot Nothing
                ReversionLength += 1
                If Not RevertedUsers.Contains(RevertingEdit.User.Name) Then RevertedUsers.Add(RevertingEdit.User.Name)
                RevertingEdit = RevertingEdit.Prev
            End While

            If RevertedUsers.Contains(Edit.User.Name) _
                Then EndPart = " " & WToAnOlderVersionBy & " " & Edit.User.Name _
                Else EndPart = " " & WToLastVersionBy & " " & Edit.User.Name

            Dim MaxLength As Integer = 250 - EndPart.Length

            If ReversionLength = 0 Then Return Nothing
            If ReversionLength = 1 Then Summary = WReverted & " 1 " & WEditBy & " " _
                Else Summary = WReverted & " " & CStr(ReversionLength) & " " & WEditsBy & " "

            If RevertedUsers.Count = 1 Then
                If (Summary & "[[Special:Contributions/" & RevertedUsers(0) & "|" & RevertedUsers(0) & "]]").Length _
                    <= MaxLength Then Summary &= "[[Special:Contributions/" & RevertedUsers(0) & "|" & _
                    RevertedUsers(0) & "]]" Else Summary &= RevertedUsers(0)

                If Summary.Length > MaxLength Then Return Summary
            Else
                For i As Integer = 0 To RevertedUsers.Count - 3
                    Summary &= "[[Special:Contributions/" & RevertedUsers(i) & "|" & RevertedUsers(i) & "]], "
                Next i

                Summary &= "[[Special:Contributions/" & RevertedUsers(RevertedUsers.Count - 2) & "|" _
                    & RevertedUsers(RevertedUsers.Count - 2) & "]] " & WAnd & " " & "[[Special:Contributions/" _
                    & RevertedUsers(RevertedUsers.Count - 1) & "|" & RevertedUsers(RevertedUsers.Count - 1) & "]]"

                If Summary.Length > MaxLength Then
                    Summary = WReverted & " " & CStr(ReversionLength) & " " & WEditsBy & " "

                    Dim Done As Boolean
                    Dim i As Integer

                    For i = 0 To RevertedUsers.Count - 3
                        If (Summary & RevertedUsers(i) & ", " & RevertedUsers(i + 1) _
                            & " and " & RevertedUsers(i + 2)).Length <= MaxLength Then

                            Summary &= RevertedUsers(i) & ", "

                        ElseIf (Summary & RevertedUsers(i) & " " & WAnd & " " _
                            & CStr(RevertedUsers.Count - i - 3) & " " & WOtherUsers).Length <= MaxLength Then

                            Summary &= RevertedUsers(i) & " " & WAnd & " " & CStr(RevertedUsers.Count - i - 3) & _
                                " " & WOtherUsers
                            Done = True
                            Exit For
                        Else
                            Done = True
                            Exit For
                        End If
                    Next i

                    If Not Done Then Summary &= RevertedUsers(i) & " " & WAnd & " " & RevertedUsers(i + 1)
                End If

                If Summary.Length > MaxLength Then Return Summary
            End If

            Summary &= EndPart

            Return Summary
        End Function

    End Class

    Class RollbackRequest : Inherits Request

        'Reverts revisions using rollback function

        Private Shadows Result As ApiResult
        Public Edit As Edit, Summary As String

        Sub Start()
            LogProgress("Reverting edit to '" & Edit.Page.Name & "'...")

            Dim RequestThread As New Thread(AddressOf Process)
            RequestThread.IsBackground = True
            RequestThread.Start()
        End Sub

        Sub Process()
            If Summary Is Nothing Then Summary = Config.RollbackSummary
            If Config.Summary IsNot Nothing Then Summary &= " " & Config.Summary

            Dim QueryString As String = "action=rollback&format=xml&title=" & UrlEncode(Edit.Page.Name) & _
                 "&user=" & UrlEncode(Edit.User.Name) & "&token=" & UrlEncode(Edit.RollbackToken) & _
                 "&summary=" & UrlEncode(Summary)

            Result = PostApi("", QueryString)

            Callback(AddressOf Done)
        End Sub

        Sub Done()
            If Result.Error Then
                Log("Did not rollback '" & Edit.Page.Name & "' – " & HtmlDecode(Result.ErrorInfo))
                Fail()

            Else
                If Config.WatchReverts AndAlso Not Watchlist.Contains(Edit.Page.SubjectPage) Then
                    Dim NewRequest As New WatchRequest
                    NewRequest.Page = Edit.Page
                    NewRequest.Start()
                End If

                If State = States.Cancelled Then UndoEdit(Edit.Page) Else Complete()
            End If
        End Sub
    End Class

    Class FakeRollbackRequest : Inherits Request

        'Finds the last revision not by the same user as the most recent revision, using the API,
        'then reverts to that revision. Like rollback, but much slower. Used when rollback is not available.

        Public Page As Page, ExcludeUser As User, Summary As String

        Public Sub Start()
            LogProgress("Reverting edit to '" & Page.Name & "'...")

            Dim RequestThread As New Thread(AddressOf Process)
            RequestThread.IsBackground = True
            RequestThread.Start()
        End Sub

        Private Sub Process()

            Dim Result As String = GetApi("action=query&format=xml&prop=revisions" & _
                "&titles=" & UrlEncode(Page.Name) & "&rvlimit=1&rvprop=user|content&rvexcludeuser=" & _
                UrlEncode(ExcludeUser.Name))

            If Result Is Nothing Then
                Callback(AddressOf Failed)
                Exit Sub

            ElseIf Not Result.Contains("<revisions>") Then
                Callback(AddressOf NoOtherUser)
                Exit Sub

            ElseIf Result.Contains("missing=""""") Then
                Callback(AddressOf PageMissing)
                Exit Sub

            ElseIf Not Result.Contains("<rev user=""") Then
                Callback(AddressOf NoOtherUser)
                Exit Sub
            End If

            Dim Data As New EditData

            Dim OldUser As String = Result.Substring(Result.IndexOf("<rev user=""") + 11)
            OldUser = HtmlDecode(OldUser.Substring(0, OldUser.IndexOf("""")))

            Data.Page = Page

            If Summary Is Nothing Then Summary = Config.RevertSummary
            Data.Summary = Summary.Replace("$1", ExcludeUser.Name).Replace("$2", OldUser)

            Data.Text = Result.Substring(Result.IndexOf("<rev user=""") + 11)
            Data.Text = Data.Text.Substring(Data.Text.IndexOf(">") + 1)
            Data.Text = HtmlDecode(Data.Text.Substring(0, Data.Text.IndexOf("</rev>")))

            Result = GetApi("action=query&format=xml&prop=info&intoken=edit&titles=" & _
                UrlEncode(Page.Name))

            If Result Is Nothing Then
                Callback(AddressOf Failed)
                Exit Sub

            ElseIf Result.Contains("missing=""""") Then
                Callback(AddressOf PageMissing)
                Exit Sub

            ElseIf Not ((Result.Contains("touched=""") AndAlso Result.Contains("edittoken="""))) Then
                Callback(AddressOf Failed)
                Exit Sub

            End If

            Data.StartTime = Timestamp(Date.UtcNow)

            Data.EditTime = Result.Substring(Result.IndexOf("touched=""") + 9)
            Data.EditTime = Data.EditTime.Substring(0, Data.EditTime.IndexOf(""""))
            Data.EditTime = Data.EditTime.Replace(":", "").Replace("Z", "").Replace("-", "").Replace("T", "")
            Data.EditTime = HtmlDecode(Data.EditTime)

            Data.Token = Result.Substring(Result.IndexOf("edittoken=""") + 11)
            Data.Token = HtmlDecode(Data.Token.Substring(0, Data.Token.IndexOf("""")))

            Data.Minor = Config.MinorReverts
            Data.Watch = Config.WatchReverts

            Data = PostEdit(Data)

            If Data.Error Then Callback(AddressOf Failed) Else Callback(AddressOf Done)
        End Sub

        Private Sub Done()
            If State = States.Cancelled Then UndoEdit(Page) Else Complete()
        End Sub

        Private Sub NoOtherUser()
            Log("Did not revert edits to '" & Page.Name & "', because only one user has edited the page")
            Fail()
        End Sub

        Private Sub PageMissing()
            Log("Did not revert edits to '" & Page.Name & "', because the page does not exist")
            Fail()
        End Sub

        Private Sub Failed()
            Log("Failed to revert edits to '" & Page.Name & "'")
            Fail()
        End Sub

    End Class

    Class UndoRequest : Inherits Request

        'Revert only a single revision

        Public Edit As Edit, Summary As String

        Public Sub Start()
            LogProgress("Reverting edit to '" & Edit.Page.Name & "'...")

            Dim RequestThread As New Thread(AddressOf Process)
            RequestThread.IsBackground = True
            RequestThread.Start()
        End Sub

        Private Sub Process()

            Dim Data As EditData = GetEditData(Edit.Page, Edit.Id, Undo:=True)

            If Data.Error Then
                Callback(AddressOf Failed)
                Exit Sub

            ElseIf Data.CannotUndo Then
                Callback(AddressOf CannotUndo)
                Exit Sub
            End If

            Data.Minor = Config.MinorReverts
            Data.Watch = Config.WatchReverts

            If Summary Is Nothing Then Summary = Config.SingleRevertSummary
            Data.Summary = Summary.Replace("$1", Edit.User.Name)

            If State = States.Cancelled Then Thread.CurrentThread.Abort()

            Data = PostEdit(Data)

            If Data.Error Then Callback(AddressOf Failed) Else Callback(AddressOf Done)
        End Sub

        Private Sub Done()
            If State = States.Cancelled Then UndoEdit(Edit.Page) Else Complete()
        End Sub

        Private Sub NoOtherUser()
            Log("Did not revert edit to '" & Edit.Page.Name & "', because only one user has edited the page")
            Fail()
        End Sub

        Private Sub PageMissing()
            Log("Did not revert edit to '" & Edit.Page.Name & "', because the page does not exist")
            Fail()
        End Sub

        Private Sub CannotUndo()
            If MessageBox.Show("Cannot revert edit to " & Edit.Page.Name & " by " & Edit.User.Name & " as other " & _
                "edits which affect this edit have since been made." & CRLF & "Edit the page manually instead?", _
                "Huggle", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.Yes Then

                Dim NewForm As New EditForm
                NewForm.Page = Edit.Page
                NewForm.Show()
            End If
        End Sub

        Private Sub Failed()
            Log("Failed to revert edit to '" & Edit.Page.Name & "'")
            Fail()
        End Sub

    End Class

End Namespace
