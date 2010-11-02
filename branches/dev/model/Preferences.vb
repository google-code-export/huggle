Imports System
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

        Public Sub LoadFromMwFormat(ByVal prefs As Dictionary(Of String, String))
            SearchNamespaces.Clear()

            For Each item As KeyValuePair(Of String, String) In prefs
                Try
                    Dim value As String = item.Value

                    Select Case item.Key
                        Case "cols" : EditorColumns = CInt(value)
                        Case "contextchars" : SearchContextChars = CInt(value)
                        Case "contextlines" : SearchContextLines = CInt(value)
                        Case "date" : DateFormat = value
                        Case "diffonly" : DiffOnly = value.ToBoolean
                        Case "disablesuggest" : SearchSuggestions = Not value.ToBoolean
                        Case "editfont" : EditorFont = value
                        Case "editondblclick" : EditOnDoubleClick = value.ToBoolean
                        Case "editsection" : SectionEditLinks = value.ToBoolean
                        Case "editsectiononrightclick" : SectionEditOnRightClick = value.ToBoolean
                        Case "editwidth" : EditorFullWidth = value.ToBoolean
                        Case "extendwatchlist" : WatchlistShowAllChanges = value.ToBoolean
                        Case "externaldiff" : ExternalDiff = value.ToBoolean
                        Case "externaleditor" : ExternalEditor = value.ToBoolean
                        Case "fancysig" : RawSignature = value.ToBoolean
                        Case "forceeditsummary" : ForceEditSummary = value.ToBoolean
                        Case "gender" : Gender = value
                        Case "hideminor" : RcHideMinor = value.ToBoolean
                        Case "hidepatrolled" : RcHidePatrolled = value.ToBoolean
                        Case "highlightbroken" : AlternateLinks = Not value.ToBoolean
                        Case "imagesize" : ImageSize = CInt(value)
                        Case "justify" : Justify = value.ToBoolean
                        Case "language" : Language = value
                        Case "math" : MathOption = CInt(value)
                        Case "minordefault" : MinorDefault = value.ToBoolean
                        Case "newpageshidepatrolled" : NewHidePatrolled = value.ToBoolean
                        Case "nickname" : Signature = value
                        Case "nocache" : DisableCaching = value.ToBoolean
                        Case "norollbackdiff" : RollbackDiff = Not value.ToBoolean
                        Case "numberheadings" : NumberHeadings = value.ToBoolean
                        Case "previewonfirst" : PreviewFirstEdit = value.ToBoolean
                        Case "previewontop" : PreviewAtTop = value.ToBoolean
                        Case "rcdays" : RcDays = CInt(value)
                        Case "rows" : EditorRows = CInt(value)
                        Case "searchlimit" : SearchResults = CInt(value)
                        Case "showhiddencats" : HiddenCategories = value.ToBoolean
                        Case "showjumplinks" : JumpLinks = value.ToBoolean
                        Case "showtoc" : Toc = value.ToBoolean
                        Case "showtoolbar" : Toolbar = value.ToBoolean
                        Case "skin" : Skin = value
                        Case "stubthreshold" : StubThreshold = CInt(value)
                        Case "thumbsize" : ThumbnailSize = CInt(value)
                        Case "timecorrection" : TimeZone = value
                        Case "underline" : UnderlineLinks = CInt(value)
                        Case "useeditwarning" : EditWarning = value.ToBoolean
                        Case "uselivepreview" : LivePreview = value.ToBoolean
                        Case "usenewrc" : EnhancedRc = value.ToBoolean
                        Case "watchcreations" : WatchCreations = value.ToBoolean
                        Case "watchdefault" : WatchEdits = value.ToBoolean
                        Case "watchdeletion" : WatchDeletions = value.ToBoolean
                        Case "watchlistdays" : WatchlistAge = CInt(value)
                        Case "watchlisthideanons" : WatchlistHideAnonymous = value.ToBoolean
                        Case "watchlisthidebots" : WatchlistHideBots = value.ToBoolean
                        Case "watchlisthideliu" : WatchlistHideUsers = value.ToBoolean
                        Case "watchlisthideminor" : WatchlistHideMinor = value.ToBoolean
                        Case "watchlisthideown" : WatchlistHideOwn = value.ToBoolean
                        Case "watchlisthidepatrolled" : WatchlistHidePatrolled = value.ToBoolean
                        Case "watchlisttoken" : WatchlistToken = value
                        Case "watchmoves" : WatchMoves = value.ToBoolean
                        Case "wllimit" : RcItems = CInt(value)

                        Case Else
                            If item.Key.StartsWithI("searchNs") AndAlso value <> "" _
                                Then SearchNamespaces.Merge(item.Key.FromFirst("searchNs")) _
                                Else Other.Merge(item.Key, value)
                    End Select

                Catch ex As SystemException
                    Log.Debug("Error parsing value for MediaWiki preference '{0}'".FormatI(item.Key))
                End Try
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

        Private Shared Function BoolStr(ByVal value As Boolean) As String
            If value Then Return "1" Else Return "0"
        End Function

    End Class

End Namespace
