'Namespace Huggle.Queries.Editing

'    Class UserMessageQuery : Inherits Query

'        Private User As User, Action, Avoid, Message, Summary, Title As String
'        Private AutoSign, AutoSummary, Bot, Minor, Watch As Boolean

'        Public Sub New(ByVal User As User, ByVal Summary As String, ByVal Message As String, _
'            Optional ByVal Title As String = Nothing, Optional ByVal Action As String = Nothing, _
'            Optional ByVal Avoid As String = Nothing, Optional ByVal Watch As Boolean = False, _
'            Optional ByVal Minor As Boolean = False, Optional ByVal Bot As Boolean = False, _
'            Optional ByVal AutoSign As Boolean = False, Optional ByVal AutoSummary As Boolean = True)

'            Me.Action = Action
'            Me.AutoSign = AutoSign
'            Me.AutoSummary = AutoSummary
'            Me.Avoid = Avoid
'            Me.Bot = Bot
'            Me.Message = Message
'            Me.Minor = Minor
'            Me.Summary = Summary
'            Me.Title = Title
'            Me.User = User
'            Me.Watch = Watch
'        End Sub

'        Protected Overrides Function Process() As Result
'            Dim FailMsg As String = Msg("usermessage-fail", User)

'            Message = Msg("usermessage-progress", User)

'            Dim Result As New RevisionRequest(User.Talkpage)
'            Result.Start()
'            If Result.IsError Then Return Failed(FailMsg, Result.ErrorMessage)

'            If Not IsAllowed(Result.Edit.Text, Action, Bot) Then Return Failed(FailMsg, Msg("error-disallowed"))

'            Dim Text As String = Result.Edit.Text
'            If Text Is Nothing Then Text = ""

'            If Avoid IsNot Nothing AndAlso Text.ToLowerI.Contains(Avoid.ToLowerI) _
'                Then Return Failed(FailMsg, Msg("usermessage-duplicate"))

'            Text &= LF & LF
'            If Not String.IsNullOrEmpty(Title) Then Text &= "== " & Title & " ==" & LF & LF
'            Text &= Message
'            If AutoSign Then Text &= " ~~~~"

'            Dim EditResult As New EditRequest _
'                (User.Talkpage, Text, Summary, Minor:=Minor, Watch:=Watch, SummaryTag:=AutoSummary)
'            EditResult.Start()
'            If EditResult.IsError Then Return Failed(FailMsg, EditResult.ErrorMessage)

'            Return Success()
'        End Function

'    End Class

'End Namespace
