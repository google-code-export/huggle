'Imports System.Globalization
'Imports System.Web.HttpUtility

'Namespace Huggle.Requests.Editing

'    'Base class for deletion discussion requests

'    MustInherit Class XfdRequest : Inherits Request

'        Private Page As Page, Reason As String, Notify As Boolean

'        Private Shared _DiscussionSummary, _Message, _MessageSummary, _MessageTitle, _TagSummary As String
'        Private Shared _MultiSubpageFormat As String = " ({0} nomination)"

'        Protected RootPage, LogPage As Page

'        Public Property DiscussionSummary() As String
'            Get
'                Return _DiscussionSummary
'            End Get
'            Set(ByVal value As String)
'                _DiscussionSummary = value
'            End Set
'        End Property

'        Public Property Message() As String
'            Get
'                Return _Message
'            End Get
'            Set(ByVal value As String)
'                _Message = value
'            End Set
'        End Property

'        Public Property MessageSummary() As String
'            Get
'                Return _MessageSummary
'            End Get
'            Set(ByVal value As String)
'                _MessageSummary = value
'            End Set
'        End Property

'        Public Property MessageTitle() As String
'            Get
'                Return _MessageTitle
'            End Get
'            Set(ByVal value As String)
'                _MessageTitle = value
'            End Set
'        End Property

'        Public Property MultiSubpageFormat() As String
'            Get
'                Return _MultiSubpageFormat
'            End Get
'            Set(ByVal value As String)
'                _MultiSubpageFormat = value
'            End Set
'        End Property

'        Public Property TagSummary() As String
'            Get
'                Return _TagSummary
'            End Get
'            Set(ByVal value As String)
'                _TagSummary = value
'            End Set
'        End Property

'        Public Sub New(ByVal Page As Page, ByVal Reason As String, ByVal Notify As Boolean)
'            Me.Notify = Notify
'            Me.Page = Page
'            Me.Reason = Reason
'        End Sub

'        Protected ReadOnly Property LogDate() As String
'            Get
'                Return CStr(Date.UtcNow.Year) & " " & _
'                    DateTimeFormatInfo.CurrentInfo.MonthNames(Date.UtcNow.Month - 1) & " " & CStr(Date.UtcNow.Day)
'            End Get
'        End Property

'        Protected Function GetSubpageName(ByVal Name As String, ByVal Page As Page) As Request
'            'Check for previous nominations of the same page
'            Dim Subpage As String = Name

'            Dim Result As New ApiRequest(Context, MakeQuery( _
'                "action", "query", _
'                "list", "allpages", _
'                "apnamespace", Page.Space.Number, _
'                "apprefix", Page.BaseName & "/" & Name))

'            If Result.IsError Then Return Failed(Result.ErrorMessage)

'            If Result.Text.Contains("<allpages>") Then
'                Dim ResultText As String = Result.Text.FromFirst("<allpages>").ToLast("</allpages>")

'                If ResultText.Contains(Page.BaseName & "/" & Name & """") Then
'                    Subpage = Name & MultiSubpageFormat.FormatWith(Ordinal(2))

'                    Dim i As Integer = 2

'                    While ResultText.Contains(Page.BaseName & "/" & Subpage)
'                        i += 1
'                        Subpage = Name & MultiSubpageFormat.FormatWith(Ordinal(i))
'                    End While
'                End If
'            End If

'            Result.Text = Subpage
'            Return Result
'        End Function

'        Private Function Ordinal(ByVal n As Integer) As String
'            If (n Mod 100) \ 10 = 1 Then Return CStr(n) & "th"

'            Select Case n Mod 10
'                Case 1 : Return CStr(n) & "st"
'                Case 2 : Return CStr(n) & "nd"
'                Case 3 : Return CStr(n) & "rd"
'            End Select

'            Return CStr(n) & "th"
'        End Function

'        Protected Function DoNotify() As Request

'            If Page.Creator Is Nothing Then
'                'Get page history to find creator
'                Dim Result As New Info.HistoryRequest(Page)
'                Result.Start()
'                If Result.IsError Then Return Failed(Result.ErrorMessage)
'            End If

'            If Page.Creator Is Nothing Then
'                Return Failed(Msg("notify-fail", Page.Name), Msg("notify-unknowncreator"))

'            ElseIf Page.Creator IsNot User.Me Then
'                'Notify page creator of deletion discussion
'                'TODO: Document this action on {{bots}} template

'                Dim Request As New UserMessageRequest( _
'                    User:=Page.Creator, _
'                    Title:=MessageTitle.FormatWith(Page.Name), _
'                    Action:="xfd", _
'                    Avoid:=Page.Name, _
'                    Minor:=Config.Minor("xfdnote"), _
'                    Watch:=Config.Watch("xfdnote"), _
'                    Summary:=MessageSummary.FormatWith(Page.Name), _
'                    Message:=Message.FormatWith(Page.Name, LogPage.Name))
'                Request.Start()
'            End If

'            Return Success()
'        End Function

'        Protected Function TagPage(ByVal Tag As String, ByVal Avoid As String, ByVal Redirect As Boolean) As Request

'            LogProgress(Msg("reqdelete-tagprogress", Page.Name))

'            Dim Result As New RevisionRequest(Page)
'            Result.Start()
'            If Result.IsError Then Return Failed(Result.ErrorMessage)

'            Dim Text As String = Result.Edit.Text

'            If Text.Contains("missing=""") Then
'                Return Failed(Msg("error-pagemissing"))
'            End If

'            If Text.ToLower.StartsWith("#redirect [[") Then
'                Return Failed(Msg("error-rfdneeded"))

'            ElseIf Text.Contains(Avoid) Then
'                Return Failed(Msg("reqdelete-duplicate"))
'            End If

'            Text = Tag & LF & Text

'            Dim EditResult As New EditRequest(Page, Text, TagSummary.Replace("$1", LogPage.Name), _
'                Minor:=Config.Minor("deletetag"), Watch:=Config.Watch("deletetag"))
'            EditResult.Start()
'            If EditResult.IsError Then Return Failed(EditResult.ErrorMessage)

'            Return Success()
'        End Function

'        Protected Function CreateDiscussionSubpage(ByVal Name As String, ByVal Text As String) As Request
'            LogProgress(Msg("reqdelete-subpageprogress", Name))

'            Dim Result As New EditRequest(Page.FromString(RootPage.Name & "/" & Name), Text, _
'                DiscussionSummary.FormatWith(Page.Name), Minor:=Config.Minor("xfd"), Watch:=Config.Watch("xfd"))

'            Result.Start()
'            If Result.IsError Then Return Failed(Result.ErrorMessage)
'            Return Success()
'        End Function

'        Protected Function UpdateLog(ByVal LogPage As Page, ByVal Summary As String, _
'            Optional ByVal Section As String = Nothing) As Request

'            LogProgress(Msg("reqdelete-logprogress", Page.Name))

'            Dim Result As New RevisionRequest(LogPage, Section:=Section)

'            If Result.IsError Then Return Failed(Result.ErrorMessage)

'            Dim Text As String = Result.Edit.Text
'            If Text Is Nothing Then Text = ""

'            'Text = Callback(Text)

'            Dim EditResult As New EditRequest(LogPage, Text, Summary, Section:=Section, _
'                Minor:=Config.Minor("deletereq"), Watch:=Config.Watch("deletereq"))
'            Result.Start()
'            If Result.IsError Then Return Failed(Result.ErrorMessage)

'            Return Success()
'        End Function

'    End Class

'End Namespace
