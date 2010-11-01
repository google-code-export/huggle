'Namespace Huggle.Queries.Editing

'    'Request page protection

'    Class ProtectionRequestQuery : Inherits Query

'        Private _Page As Page, _Reason As String, _Type As String

'        Protected Overrides Function Process() As Request
'            Dim FailMsg As String = Msg("reqprotection-fail", _Page.Name)

'            LogProgress(Msg("reqprotection-progress", _Page.Name))

'            Dim Result As New RevisionRequest(Config.ProtectionRequestPage, Section:="1")
'            Result.Start()
'            If Result.IsError Then Return Failed(FailMsg, Result.ErrorMessage)

'            If Not Result.Edit.Text.Contains("{{" & Config.ProtectionRequestPage.Name & "/PRheading}}") _
'                Then Return Failed(FailMsg, Msg("reqprotection-badformat"))

'            If Result.Edit.Text.Contains("|" & _Page.Name & "}}") _
'                Then Return Failed(FailMsg, Msg("reqprotection-alreadyrequested"))

'            Dim Text As String = Result.Edit.Text, Header As String

'            If _Page.IsArticleTalkPage Then
'                Header = "===={{lat|" & _Page.Name & "}}====" & LF
'            ElseIf _Page.IsTalkPage Then
'                Header = "===={{lnt|" & _Page.Space.Name & "|" & _Page.Name & "}}====" & LF
'            ElseIf _Page.IsArticle Then
'                Header = "===={{la|" & _Page.Name & "}}====" & LF
'            Else
'                Header = "===={{ln|" & _Page.Space.Name & "|" & _Page.Name & "}}====" & LF
'            End If

'            Select Case _Type
'                Case "full" : Header &= "'''Full protection'''. "
'                Case "move" : Header &= "'''Move protection'''. "
'                Case "semi" : Header &= "'''Semi-protection'''. "
'            End Select

'            Text = Text.Substring(0, Text.IndexOf("====")) & Header & _Reason & " ~~~~" _
'                & LF & LF & Text.Substring(Text.IndexOf("===="))

'            Dim ProtectionLevel As String

'            Select Case _Type
'                Case "full" : ProtectionLevel = "full protection"
'                Case "move" : ProtectionLevel = "move protection"
'                Case "semi" : ProtectionLevel = "semi-protection"
'                Case Else : ProtectionLevel = "protection"
'            End Select

'            Dim EditResult As New EditRequest(Config.ProtectionRequestPage, Text, _
'                Config.ProtectionRequestSummary.FormatWith(ProtectionLevel, _Page.Name), _
'                Section:="1", Minor:=Config.Minor("protectreq"), Watch:=Config.Watch("protectreq"))

'            If Result.IsError Then Return Failed(FailMsg, Result.ErrorMessage)

'            Return Success()
'        End Function

'    End Class

'End Namespace