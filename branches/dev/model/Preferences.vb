Imports System.Collections.Generic

Namespace Huggle

    'Stores user preferences

    Public Class Preferences

        Private _Other As New Dictionary(Of String, String)
        Private _User As User

        Public AlternateLinks As Boolean
        Public DateFormat As String
        Public DiffOnly As Boolean
        Public DisableCaching As Boolean
        Public EditOnDoubleClick As Boolean
        Public EditorColumns As Integer
        Public EditorFont As String
        Public EditorFullWidth As Boolean
        Public EditorRows As Integer
        Public EditWarning As Boolean
        Public EmailAddress As String
        Public EnhancedRc As Boolean
        Public ExternalEditor As Boolean
        Public ExternalDiff As Boolean
        Public ForceEditSummary As Boolean
        Public Gender As String
        Public HiddenCategories As Boolean
        Public ImageSize As Integer
        Public JumpLinks As Boolean
        Public Justify As Boolean
        Public Language As String
        Public LivePreview As Boolean
        Public MathOption As Integer
        Public MinorDefault As Boolean
        Public NewHidePatrolled As Boolean
        Public NumberHeadings As Boolean
        Public PreviewAtTop As Boolean
        Public PreviewFirstEdit As Boolean
        Public RawSignature As Boolean
        Public RcDays As Integer
        Public RcHideMinor As Boolean
        Public RcHidePatrolled As Boolean
        Public RcItems As Integer
        Public RollbackDiff As Boolean
        Public SearchContextChars As Integer
        Public SearchContextLines As Integer
        Public SearchNamespaces As New List(Of String)
        Public SearchResults As Integer
        Public SearchSuggestions As Boolean
        Public SectionEditLinks As Boolean
        Public SectionEditOnRightClick As Boolean
        Public Signature As String
        Public Skin As String
        Public StubThreshold As Integer
        Public ThumbnailSize As Integer
        Public TimeZone As String
        Public Toc As Boolean
        Public Toolbar As Boolean
        Public UnderlineLinks As Integer
        Public WatchCreations As Boolean
        Public WatchDeletions As Boolean
        Public WatchEdits As Boolean
        Public WatchMoves As Boolean
        Public WatchlistAge As Integer
        Public WatchlistHideAnonymous As Boolean
        Public WatchlistHideBots As Boolean
        Public WatchlistHideMinor As Boolean
        Public WatchlistHideOwn As Boolean
        Public WatchlistHidePatrolled As Boolean
        Public WatchlistHideUsers As Boolean
        Public WatchlistShowAllChanges As Boolean
        Public WatchlistToken As String

        Public Sub New(ByVal user As User)
            _User = user
        End Sub

        Public ReadOnly Property Other() As Dictionary(Of String, String)
            Get
                Return _Other
            End Get
        End Property

        Public ReadOnly Property User() As User
            Get
                Return _User
            End Get
        End Property

        Public Function Clone() As Preferences
            Dim result As Preferences = CType(MemberwiseClone(), Preferences)
            result.SearchNamespaces = New List(Of String)(SearchNamespaces)

            Return result
        End Function

        Public Sub FromMwFormat(ByVal prefs As Dictionary(Of String, String))
            SearchNamespaces.Clear()

            For Each item As KeyValuePair(Of String, String) In prefs
                Dim value As String = item.Value

                Select Case item.Key
                    Case "cols" : EditorColumns = CInt(value)
                    Case "contextchars" : SearchContextChars = CInt(value)
                    Case "contextlines" : SearchContextLines = CInt(value)
                    Case "date" : DateFormat = value
                    Case "diffonly" : DiffOnly = CBool(value)
                    Case "disablesuggest" : SearchSuggestions = Not CBool(value)
                    Case "editfont" : EditorFont = value
                    Case "editondblclick" : EditOnDoubleClick = CBool(value)
                    Case "editsection" : SectionEditLinks = CBool(value)
                    Case "editsectiononrightclick" : SectionEditOnRightClick = CBool(value)
                    Case "editwidth" : EditorFullWidth = CBool(value)
                    Case "extendwatchlist" : WatchlistShowAllChanges = CBool(value)
                    Case "externaldiff" : ExternalDiff = CBool(value)
                    Case "externaleditor" : ExternalEditor = CBool(value)
                    Case "fancysig" : RawSignature = CBool(value)
                    Case "forceeditsummary" : ForceEditSummary = CBool(value)
                    Case "gender" : Gender = value
                    Case "hideminor" : RcHideMinor = CBool(value)
                    Case "hidepatrolled" : RcHidePatrolled = CBool(value)
                    Case "highlightbroken" : AlternateLinks = Not CBool(value)
                    Case "imagesize" : ImageSize = CInt(value)
                    Case "justify" : Justify = CBool(value)
                    Case "language" : Language = value
                    Case "math" : MathOption = CInt(value)
                    Case "minordefault" : MinorDefault = CBool(value)
                    Case "newpageshidepatrolled" : NewHidePatrolled = CBool(value)
                    Case "nickname" : Signature = value
                    Case "nocache" : DisableCaching = CBool(value)
                    Case "norollbackdiff" : RollbackDiff = Not CBool(value)
                    Case "numberheadings" : NumberHeadings = CBool(value)
                    Case "previewonfirst" : PreviewFirstEdit = CBool(value)
                    Case "previewontop" : PreviewAtTop = CBool(value)
                    Case "rcdays" : RcDays = CInt(value)
                    Case "rows" : EditorRows = CInt(value)
                    Case "searchlimit" : SearchResults = CInt(value)
                    Case "showhiddencats" : HiddenCategories = CBool(value)
                    Case "showjumplinks" : JumpLinks = CBool(value)
                    Case "showtoc" : Toc = CBool(value)
                    Case "showtoolbar" : Toolbar = CBool(value)
                    Case "skin" : Skin = value
                    Case "stubthreshold" : StubThreshold = CInt(value)
                    Case "thumbsize" : ThumbnailSize = CInt(value)
                    Case "timecorrection" : TimeZone = value
                    Case "underline" : UnderlineLinks = CInt(value)
                    Case "useeditwarning" : EditWarning = CBool(value)
                    Case "uselivepreview" : LivePreview = CBool(value)
                    Case "usenewrc" : EnhancedRc = CBool(value)
                    Case "watchcreations" : WatchCreations = CBool(value)
                    Case "watchdefault" : WatchEdits = CBool(value)
                    Case "watchdeletion" : WatchDeletions = CBool(value)
                    Case "watchlistdays" : WatchlistAge = CInt(value)
                    Case "watchlisthideanons" : WatchlistHideAnonymous = CBool(value)
                    Case "watchlisthidebots" : WatchlistHideBots = CBool(value)
                    Case "watchlisthideliu" : WatchlistHideUsers = CBool(value)
                    Case "watchlisthideminor" : WatchlistHideMinor = CBool(value)
                    Case "watchlisthideown" : WatchlistHideOwn = CBool(value)
                    Case "watchlisthidepatrolled" : WatchlistHidePatrolled = CBool(value)
                    Case "watchlisttoken" : WatchlistToken = value
                    Case "watchmoves" : WatchMoves = CBool(value)
                    Case "wllimit" : RcItems = CInt(value)

                    Case Else
                        If item.Key.StartsWith("searchNs") AndAlso value <> "" _
                            Then SearchNamespaces.Merge(item.Key.FromFirst("searchNs")) _
                            Else Other.Merge(item.Key, value)
                End Select
            Next item
        End Sub

        Public Function ToMwFormat() As Dictionary(Of String, String)
            Dim prefs As New Dictionary(Of String, String)

            prefs.Add("cols", CStr(EditorColumns))
            prefs.Add("contextchars", CStr(SearchContextChars))
            prefs.Add("contextlines", CStr(SearchContextLines))
            prefs.Add("date", DateFormat)
            prefs.Add("diffonly", BoolStr(DiffOnly))
            prefs.Add("disablesuggest", BoolStr(Not SearchSuggestions))
            prefs.Add("editorfont", EditorFont)
            prefs.Add("editondblclick", BoolStr(EditOnDoubleClick))
            prefs.Add("editsection", BoolStr(SectionEditLinks))
            prefs.Add("editsectiononrightclick", BoolStr(SectionEditOnRightClick))
            prefs.Add("editwidth", BoolStr(EditorFullWidth))
            prefs.Add("extendwatchlist", BoolStr(WatchlistShowAllChanges))
            prefs.Add("externaldiff", BoolStr(ExternalDiff))
            prefs.Add("externaleditor", BoolStr(ExternalEditor))
            prefs.Add("fancysig", BoolStr(RawSignature))
            prefs.Add("forceeditsummary", BoolStr(ForceEditSummary))
            prefs.Add("gender", Gender)
            prefs.Add("hideminor", BoolStr(RcHideMinor))
            prefs.Add("hidepatrolled", BoolStr(RcHidePatrolled))
            prefs.Add("highlightbroken", BoolStr(Not AlternateLinks))
            prefs.Add("imagesize", CStr(ImageSize))
            prefs.Add("justify", BoolStr(Justify))
            prefs.Add("language", Language)
            prefs.Add("math", CStr(MathOption))
            prefs.Add("minordefault", BoolStr(MinorDefault))
            prefs.Add("newpageshidepatrolled", BoolStr(NewHidePatrolled))
            prefs.Add("nickname", Signature)
            prefs.Add("nocache", BoolStr(DisableCaching))
            prefs.Add("norollbackdiff", BoolStr(Not RollbackDiff))
            prefs.Add("numberheadings", BoolStr(NumberHeadings))
            prefs.Add("previewonfirst", BoolStr(PreviewFirstEdit))
            prefs.Add("previewontop", BoolStr(PreviewAtTop))
            prefs.Add("rcdays", CStr(RcDays))
            prefs.Add("rows", CStr(EditorRows))
            prefs.Add("searchlimit", CStr(SearchResults))
            prefs.Add("showhiddencats", BoolStr(HiddenCategories))
            prefs.Add("showjumplinks", BoolStr(JumpLinks))
            prefs.Add("showtoc", BoolStr(Toc))
            prefs.Add("showtoolbar", BoolStr(Toolbar))
            prefs.Add("skin", Skin)
            prefs.Add("stubthreshold", CStr(StubThreshold))
            prefs.Add("thumbsize", CStr(ThumbnailSize))
            prefs.Add("timecorrection", TimeZone)
            prefs.Add("underline", CStr(UnderlineLinks))
            prefs.Add("useeditwarning", BoolStr(EditWarning))
            prefs.Add("uselivepreview", BoolStr(LivePreview))
            prefs.Add("usenewrc", BoolStr(EnhancedRc))
            prefs.Add("watchcreations", BoolStr(WatchCreations))
            prefs.Add("watchdefault", BoolStr(WatchEdits))
            prefs.Add("watchdeletion", BoolStr(WatchDeletions))
            prefs.Add("watchlistdays", CStr(WatchlistAge))
            prefs.Add("watchlisthideanons", BoolStr(WatchlistHideAnonymous))
            prefs.Add("watchlisthidebots", BoolStr(WatchlistHideBots))
            prefs.Add("watchlisthideliu", BoolStr(WatchlistHideUsers))
            prefs.Add("watchlisthideminor", BoolStr(WatchlistHideMinor))
            prefs.Add("watchlisthideown", BoolStr(WatchlistHideOwn))
            prefs.Add("watchlisthidepatrolled", BoolStr(WatchlistHidePatrolled))
            prefs.Add("watchlisttoken", WatchlistToken)
            prefs.Add("watchmoves", BoolStr(WatchMoves))
            prefs.Add("wllimit", CStr(RcItems))

            prefs.Merge(Other)

            Return prefs
        End Function

        Private Function BoolStr(ByVal value As Boolean) As String
            If value Then Return "1" Else Return "0"
        End Function

    End Class

End Namespace
