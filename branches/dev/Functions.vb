Imports Huggle.Scripting
Imports Huggle.Wikitext
Imports System
Imports System.Collections
Imports System.Collections.Generic
Imports System.Drawing
Imports System.IO
Imports System.IO.Compression
Imports System.Security.Cryptography
Imports System.Text
Imports System.Text.RegularExpressions
Imports System.Threading
Imports System.Web.HttpUtility
Imports System.Windows.Forms

Namespace Huggle

    Friend Module Functions

        Public ReadOnly CRLF As String = Convert.ToChar(13) & Convert.ToChar(10)
        Public ReadOnly CR As Char = Convert.ToChar(13)
        Public ReadOnly LF As Char = Convert.ToChar(10)
        Public ReadOnly Tab As Char = Convert.ToChar(9)

        Public ReadOnly C1 As Char = Convert.ToChar(31)
        Public ReadOnly C2 As Char = Convert.ToChar(30)

        Public Const BlpIconHtml As String =
            "<img src='http://upload.wikimedia.org/wikipedia/commons/thumb/4/41/" &
            "Crystal_Clear_app_personal_gray.png/32px-Crystal_Clear_app_personal_gray.png' " &
            "alt='Biography of living person' width='32' height='32' border='0' />"

        'Can't use Date.MinValue for optional date parameters, have to do this instead
        Public Const DateMinValue As Date = #1/1/1900#

        Public ReadOnly RangePattern As New Regex(
            "\d{1,3}\.\d{1,3}\.\d{1,3}\.\d{1,3}/\d{2}", RegexOptions.Compiled)

        'Get a formatted message string localized to the user's language
        Public Function Msg(ByVal name As String, ByVal ParamArray params As Object()) As String
            If App.Languages.Current Is Nothing Then Return "[" & name & "] "
            If Not name.Contains("-") Then name = "a-" & name
            Return App.Languages.Current.Message(name, params)
        End Function

        Public Function MD5Hash(ByVal data As Byte()) As Byte()
            Using hasher As HashAlgorithm = MD5.Create
                Return hasher.ComputeHash(data)
            End Using
        End Function

        Public Delegate Function Expression() As Boolean

        Public Function Compress(ByVal data As Byte()) As Byte()
            Using output As New MemoryStream
                Dim compressStream As New GZipStream(output, CompressionMode.Compress)
                compressStream.Write(data, 0, data.Length)
                Return output.ToArray
            End Using
        End Function

        Public Function Uncompress(ByVal data As Byte()) As Byte()
            Try
                Using input As New MemoryStream(data)
                    Dim decompressStream As New GZipStream(input, CompressionMode.Decompress)
                    Dim output() As Byte
                    Dim total As Integer = 0
                    Dim read As Integer = 0

                    Do
                        ReDim Preserve output(total + 256)
                        read = decompressStream.Read(output, total, 256)
                        total += read
                    Loop Until read = 0

                    ReDim Preserve output(total)
                    Return output
                End Using

            Catch ex As InvalidDataException
                Return Nothing
            End Try
        End Function

        Public Function PathCombine(ByVal ParamArray components As String()) As String
            If components.Length = 0 Then Return ""

            Dim result As String = components(0)

            For i As Integer = 1 To components.Length - 1
                result = Path.Combine(result, components(i))
            Next i

            Return result
        End Function

        Public ReadOnly Property DesignTime() As Boolean
            Get
                Return System.ComponentModel.LicenseManager.UsageMode = System.ComponentModel.LicenseUsageMode.Designtime
            End Get
        End Property

        Public Sub ResizeDropDown(ByVal control As ComboBox)
            'Adjust the width of a ComboBox drop-down to match the widest text it contains
            Dim width As Integer = control.Width
            Dim graphics As Graphics = control.CreateGraphics

            For Each item As Object In control.Items
                width = Math.Max(width, CInt(TextRenderer.MeasureText(graphics, item.ToString, control.Font).Width) + 8)
            Next item

            control.DropDownWidth = width
        End Sub

        Public Function ImageToIcon(ByVal image As Image) As Drawing.Icon
            'Have to pass a handle to achieve this
            Using bitmap As New Bitmap(image)
                Return Icon.FromHandle(bitmap.GetHicon)
            End Using
        End Function

        Public Function MakeWebPage(ByVal html As String, Optional ByVal title As String = "") As String
            Return Nothing 'My.Resources.WebPage _
            '.Replace("$TITLE", HtmlEncode(Title)) _
            '.Replace("$USER", UrlEncode(Account.Current.User.Name)) _
            '.Replace("$PATH", Wiki.Current.Url) _
            '.Replace("$CONTENT", Html)
        End Function

        Public Function FullDateString(ByVal time As Date) As String
            Dim utc As Date = time.ToUniversalTime
            Return CStr(time.Year) & "-" & CStr(time.Month).PadLeft(2, "0"c) & "-" & CStr(time.Day).PadLeft(2, "0"c) &
                " " & CStr(time.Hour).PadLeft(2, "0"c) & ":" & CStr(time.Minute).PadLeft(2, "0"c) & ":" &
                CStr(time.Second).PadLeft(2, "0"c)
        End Function

        Public Function MakeWebError(ByVal text As String) As String
            Return "<div class='huggle-error'>" & text & "</div>"
        End Function

        Public Function MimeType(ByVal filename As String) As String
            Select Case filename.FromLast(".")
                Case "bmp" : Return "image/bmp"
                Case "djvu" : Return "image/x.djvu"
                Case "gif" : Return "image/gif"
                Case "jpg", "jpeg" : Return "image/jpeg"
                Case "mid" : Return "audio/mid"
                Case "oga", "ogg", "ogv" : Return "audio/ogg"
                Case "pdf" : Return "application/pdf"
                Case "png" : Return "image/png"
                Case "svg" : Return "image/svg+xml"
                Case "tif", "tiff" : Return "image/tiff"
                Case "txt" : Return "text/plain"
                Case "xcf" : Return "image/x-xcf"
            End Select

            Return "application/octet-stream"
        End Function

        Public Function CeilingToSigFigs(ByVal number As Double, ByVal digits As Integer) As Double
            Dim scale As Double = Math.Pow(10, Math.Ceiling(Math.Log10(number)) - digits)
            Return scale * Math.Ceiling(number / scale)
        End Function

        Public Function Dictionary(ByVal ParamArray items() As Object) As Dictionary(Of String, String)
            Return items.ToDictionary
        End Function

        Public Function FuzzyAge(ByVal time As Date, ByVal now As Date) As String
            Dim parts As New List(Of String)

            Dim years As Integer = 0

            While time.AddYears(1) < now
                years += 1
                time = time.AddYears(1)
            End While

            Dim months As Integer = 0

            While time.AddMonths(1) < now
                months += 1
                time = time.AddYears(1)
            End While

            Dim span As TimeSpan = now - time

            If years > 0 Then parts.Add(PluralMsg("time-year", years))
            If months > 0 AndAlso years < 2 Then parts.Add(PluralMsg("time-month", months))
            If years > 0 Then Return parts.Join(" ")

            If span.Days > 0 AndAlso months < 2 Then parts.Add(PluralMsg("time-day", span.Days))
            If months > 0 Then Return parts.Join(" ")

            If span.Hours > 0 AndAlso span.Days < 2 Then parts.Add(PluralMsg("time-hour", span.Hours))
            If span.Days > 0 Then Return parts.Join(" ")

            If span.Minutes > 0 AndAlso span.Hours < 2 Then parts.Add(PluralMsg("time-minute", span.Minutes))
            If span.Hours > 0 Then Return parts.Join(" ")

            If span.Seconds > 0 AndAlso span.Minutes < 2 AndAlso span.Hours = 0 Then parts.Add(PluralMsg("time-second", span.Seconds))
            Return parts.Join(" ")
        End Function

        Public Function FullFuzzyAge(ByVal time As Date, ByVal now As Date) As String
            Dim parts As New List(Of String)

            Dim years As Integer = 0

            While time.AddYears(1) < now
                years += 1
                time = time.AddYears(1)
            End While

            Dim months As Integer = 0

            While time.AddMonths(1) < now
                months += 1
                time = time.AddYears(1)
            End While

            Dim span As TimeSpan = now - time

            If years > 0 Then parts.Add(PluralMsg("time-year", years))
            If months > 0 Then parts.Add(PluralMsg("time-month", months))
            If span.Days > 0 Then parts.Add(PluralMsg("time-day", span.Days))
            If span.Hours > 0 Then parts.Add(PluralMsg("time-hour", span.Hours))
            If span.Minutes > 0 Then parts.Add(PluralMsg("time-minute", span.Minutes))
            If span.Seconds > 0 Then parts.Add(PluralMsg("time-second", span.Seconds))

            Return parts.Join(" ")
        End Function

        Public Function FuzzyTime(ByVal time As TimeSpan) As String
            Dim parts As New List(Of String)

            If time.Days > 0 Then parts.Add(PluralMsg("time-day", time.Days))
            If time.Hours > 0 Then parts.Add(PluralMsg("time-hour", time.Hours))
            If time.Minutes > 0 Then parts.Add(PluralMsg("time-minute", time.Minutes))
            Dim seconds As Double = time.Seconds + (time.Milliseconds / 1000)
            If seconds > 0 Then parts.Add(PluralMsg("time-second", time.Seconds))

            Return parts.Join(" ")
        End Function

        Public Function FuzzyTimeShort(ByVal time As TimeSpan) As String
            Dim parts As New List(Of String)

            If time.Days > 0 Then parts.Add(PluralMsg("time-shortday", time.Days))
            If time.Hours > 0 Then parts.Add(PluralMsg("time-shorthour", time.Hours))
            If time.Minutes > 0 Then parts.Add(PluralMsg("time-shortminute", time.Minutes))
            Dim seconds As Double = time.Seconds + (time.Milliseconds / 1000)
            If seconds > 0 Then parts.Add(PluralMsg("time-shortsecond", time.Seconds))

            Return parts.Join(" ")
        End Function

        Public ReadOnly Property Hash(ByVal user As User) As Byte()
            Get
                'This is not cryptographically secure. It is not meant to be. It is
                'meant to deter casual discovery of the user's password. It is of no
                'use if the user's computer is compromised, but in that case the attacker
                'could simply install a keylogger and capture the password that way.
                'But they probably wouldn't go to such trouble, as users not using the secure
                'server (which is most of them) are sending their password across the Web
                'in plaintext every time they log in to a MediaWiki wiki.

                Using MD5 As MD5 = MD5.Create
                    Return MD5.ComputeHash(Encoding.UTF8.GetBytes _
                        (Windows.Forms.Application.ProductName & user.Name & user.Wiki.Code & _
                        My.Computer.Info.OSFullName & Config.Local.Uid & "fnord"))
                End Using
            End Get
        End Property

        Private Function PluralMsg(ByVal msgKey As String, ByVal value As Integer) As String
            Return Msg(msgKey & If(value <> 1, "-s", ""), value)
        End Function

        Public Function GetValidFileName(ByVal str As String) As String
            For Each item As Char In IO.Path.GetInvalidFileNameChars
                str = str.Replace(item, "_")
            Next item

            Return str
        End Function

        Public Sub OpenWebBrowser(ByVal url As Uri)
            Diagnostics.Process.Start(url.ToString)
        End Sub

        Public Function Scramble(ByVal source As String, ByVal data As String, ByVal key As Byte()) As Byte()
            Return ProtectedData.Protect(Encoding.UTF8.GetBytes(data), key, DataProtectionScope.LocalMachine)
        End Function

        Public Function Unscramble(ByVal source As String, ByRef data As Byte(), ByVal key As Byte()) As String
            If data Is Nothing Then Return Nothing

            Try
                Return Encoding.UTF8.GetString(ProtectedData.Unprotect(data, key, DataProtectionScope.LocalMachine))
            Catch ex As CryptographicException
                Log.Debug("Discarded unreadable stored password for {0}".FormatI(source))
                data = Nothing
                Return Nothing
            End Try
        End Function

        Public Function WikiUrl(ByVal wiki As Wiki, ByVal title As String, ByVal ParamArray params As String()) As Uri

            Dim paramString As String = ""

            If params.Length > 0 Then
                For i As Integer = 0 To params.Length - 1
                    params(i) = params(i).Replace(" ", "_")
                    If i Mod 2 = 0 Then paramString &= "&" & params(i) Else paramString &= "=" & params(i)
                Next i

                paramString = paramString.Substring(1)
            End If

            title = title.Replace(" ", "_")

            If wiki.ShortUrl Is Nothing _
                Then Return New Uri(wiki.Url.ToString & "index.php?title=" & title & paramString) _
                Else Return New Uri(wiki.ShortUrl.ToString & title & "?" & paramString.Substring(1))
        End Function

        'Constructs a string of the form "foo", "foo and bar" or "foo, bar and baz"
        'in the specified language
        Public Function NaturalLanguageList(ByVal language As Language, ByVal ParamArray items() As Object) As String
            Select Case items.Length
                Case 0 : Return Nothing
                Case 1 : Return items(0).ToString
                Case 2 : Return items(0).ToString & " " & language.Message("a-and") & " " & items(1).ToString
                Case Else
                    Dim Result As String = ""

                    For i As Integer = 0 To items.Length - 2
                        Result &= items(i).ToString & ", "
                    Next i

                    Return NaturalLanguageList(language, Result, items(items.Length - 1))
            End Select
        End Function

        Public Function ParameterList(ByVal items() As Object) As ParameterCollection
            Return Nothing
        End Function

        Public Function StripHtml(ByVal text As String) As String
            If text Is Nothing Then Return Nothing

            While text.Contains("<") AndAlso text.FromFirst("<").Contains(">")
                text = text.ToFirst("<") & text.FromFirst("<").FromFirst(">")
            End While

            Return text
        End Function

        Public Function PlainTextFromHtml(ByVal text As String) As String
            If text Is Nothing Then Return Nothing

            While text.Contains("<") AndAlso text.FromFirst("<").Contains(">")
                text = text.ToFirst("<") & " " & text.FromFirst("<").FromFirst(">")
            End While

            While text.Contains("  ")
                text = text.Replace("  ", " ")
            End While

            Return text
        End Function

        Public Function LcFirst(ByVal str As String) As String
            If str Is Nothing Then Return Nothing
            Return str.Substring(0, 1).ToLowerI & str.Substring(1)
        End Function

        Public Function UcFirst(ByVal str As String) As String
            If str Is Nothing Then Return Nothing
            If str.Length = 1 Then Return str.ToUpperI
            Return str.Substring(0, 1).ToUpperI & str.Substring(1)
        End Function

        Public Function ValueListToRegex(ByVal list As List(Of String)) As Regex
            If list Is Nothing Then Return Nothing

            Dim patterns As New List(Of String)

            For Each item As String In list
                patterns.Add(Regex.Escape(item))
            Next item

            Return New Regex("(" & String.Join("|", patterns.ToArray) & ")", RegexOptions.Compiled)
        End Function

        Public Function GetHtmlAttributeFromName(ByVal html As String,
            ByVal name As String, ByVal attribute As String) As String

            Dim pattern As New Regex("<[^ ]+ name=""" & Regex.Escape(name) & """[^>]+" &
                Regex.Escape(attribute) & "=""([^""]+)""", RegexOptions.Compiled Or RegexOptions.IgnoreCase)

            Dim match As Match = pattern.Match(html)
            If match.Success Then Return HtmlDecode(match.Groups(1).Value)
            Return Nothing
        End Function

        Public Function GetHtmlTextFromName(ByVal html As String, ByVal name As String) As String
            Dim pattern As New Regex("<[^ ]+ name=""" & Regex.Escape(name) & """[^>]+>([^<]*)<",
                RegexOptions.Compiled Or RegexOptions.IgnoreCase)

            Dim match As Match = pattern.Match(html)
            If match.Success Then Return HtmlDecode(match.Groups(1).Value)
            Return Nothing
        End Function

        Function MakeString(ByVal item As Object) As String
            If item Is Nothing Then Return String.Empty

            If TypeOf item Is ArrayList Then
                Dim list As ArrayList = DirectCast(item, ArrayList)
                Dim strs(list.Count - 1) As String

                For i As Integer = 0 To list.Count - 1
                    strs(i) = list(i).ToString
                Next i

                Return String.Join(", ", strs)

            ElseIf TypeOf item Is Table Then
                Dim table As Table = DirectCast(item, Table)
                Dim result As String = "Table ["

                For Each column As TableColumn In table.Columns
                    result &= column.Cells(0).Content & ", "
                Next column

                result = result.Substring(0, result.Length - 2)
                result &= "] (" & table.Rows.Count & " items)"

                Return result
            End If

            Return item.ToString
        End Function

        Public Enum CacheState As Integer
            : NotCached : Loading : Cached : NewPage
        End Enum

        Public Enum Filters As Integer
            : Exclude : Require : None
        End Enum

        Public Enum MultiFilters As Integer
            : None : All : Any
        End Enum

        Public Function ComparePairs(ByVal x As Pair, ByVal y As Pair) As Integer
            If TypeOf x.First Is Integer AndAlso TypeOf y.First Is Integer Then Return CInt(y.First) - CInt(x.First)
            If TypeOf x.First Is String AndAlso TypeOf y.First Is String _
                Then Return String.Compare(CStr(x.First), CStr(y.First), StringComparison.Ordinal)
            Return 0
        End Function

        Public Structure CustomRevert

            Public Summary As String
            Public Rev As Revision
            Public Target As Revision

        End Structure

        Public Structure Pair

            Public Sub New(ByVal first As Object, ByVal second As Object)
                Me.First = first
                Me.Second = second
            End Sub

            Dim First As Object
            Dim Second As Object

        End Structure

        Public Structure Selection

            Private _Length As Integer
            Private _Start As Integer

            Public Shared ReadOnly Empty As New Selection(-1, 0)

            Public Sub New(ByVal start As Integer, ByVal length As Integer)
                _Start = start
                _Length = length
            End Sub

            Public ReadOnly Property [End]() As Integer
                Get
                    Return _Start + _Length
                End Get
            End Property

            Public ReadOnly Property IsEmpty() As Boolean
                Get
                    Return (Start = -1)
                End Get
            End Property

            Public ReadOnly Property Start() As Integer
                Get
                    Return _Start
                End Get
            End Property

            Public ReadOnly Property Length() As Integer
                Get
                    Return _Length
                End Get
            End Property

        End Structure

        Public Function ColorFromHSL(ByVal H As Double, ByVal S As Double, ByVal L As Double) As Color
            Dim R, G, B As Double

            If L > 0 Then
                If S = 0 Then
                    R = L
                    G = L
                    B = L
                Else
                    Dim Temp1, Temp2 As Double

                    Temp2 = If(L <= 0.5, L * (1 + S), L + S - (L * S))
                    Temp1 = 2 * L - Temp2

                    Dim T As Double() = {H + 1.0 / 3.0, H, H - 1.0 / 3.0}
                    Dim RGB As Double() = {0, 0, 0}

                    For i As Integer = 0 To 2
                        If T(i) < 0 Then T(i) += 1
                        If T(i) > 1 Then T(i) -= 1

                        If T(i) < 1 / 6 Then
                            RGB(i) = Temp1 + (Temp2 - Temp1) * T(i) * 6

                        ElseIf T(i) < 1 / 2 Then
                            RGB(i) = Temp2

                        ElseIf T(i) < 2 / 3 Then
                            RGB(i) = Temp1 + (Temp2 - Temp1) * ((2 / 3) - T(i)) * 6

                        Else
                            RGB(i) = Temp1
                        End If
                    Next i

                    R = RGB(0)
                    G = RGB(1)
                    B = RGB(2)
                End If
            End If

            Return Color.FromArgb(CInt(255 * R), CInt(255 * G), CInt(255 * B))
        End Function

        Structure Range

            Private _Lower, _Upper As Double

            Public Sub New(ByVal Lower As Double, ByVal Upper As Double)
                _Lower = Lower
                _Upper = Upper
            End Sub

            Public ReadOnly Property Lower() As Double
                Get
                    Return _Lower
                End Get
            End Property

            Public ReadOnly Property Upper() As Double
                Get
                    Return _Upper
                End Get
            End Property

            Public Overrides Function ToString() As String
                Return CStr(Lower) & " - " & CStr(Upper)
            End Function

        End Structure

        Public Enum ConflictAction As Integer
            : Ignore : Retry : Abort : Prompt
        End Enum

        Public Enum WatchAction As Integer
            : NoChange : Watch : Unwatch : Preferences
        End Enum

        Public Function List(Of T)(ByVal ParamArray items() As T) As List(Of T)
            Return New List(Of T)(items)
        End Function

    End Module

    <Diagnostics.DebuggerDisplay("{Format}"), Serializable()>
    Friend Class QueryException : Inherits HuggleException

        Public Sub New(ByVal message As String)
            MyBase.New(message)
        End Sub

        Public Sub New(ByVal message As String, ByVal innerException As QueryException)
            MyBase.New(message, innerException)
        End Sub

        Public Overrides Function ToString() As String
            Return Format
        End Function

        Public ReadOnly Property Format() As String
            Get
                Dim result As String = Message
                Dim ex As Exception = InnerException

                While ex IsNot Nothing
                    result &= ": " & ex.Message
                    ex = ex.InnerException
                End While

                If Not result.EndsWithI(".") Then result &= "."
                Return result
            End Get
        End Property

        Public ReadOnly Property FormatMultiline() As String
            Get
                Dim result As String = Message
                Dim ex As Exception = InnerException

                While ex IsNot Nothing
                    result &= ":" & CRLF & ex.Message
                    ex = ex.InnerException
                End While

                If Not result.EndsWithI(".") Then result &= "."
                Return result
            End Get
        End Property

    End Class

    <Diagnostics.DebuggerDisplay("{ToString()}")> _
    Public Structure TS

        Private State As Integer

        Private Shared ReadOnly _False As New TS With {.State = 1}
        Private Shared ReadOnly _True As New TS With {.State = 2}
        Private Shared ReadOnly _Undefined As New TS With {.State = 0}

        Public Shared ReadOnly Property [False]() As TS
            Get
                Return _False
            End Get
        End Property

        Public Shared ReadOnly Property [True]() As TS
            Get
                Return _True
            End Get
        End Property

        Public Shared ReadOnly Property Undefined() As TS
            Get
                Return _Undefined
            End Get
        End Property

        Public Shared Operator =(ByVal a As TS, ByVal b As TS) As Boolean
            Return a.State = b.State
        End Operator

        Public Shared Operator <>(ByVal a As TS, ByVal b As TS) As Boolean
            Return a.State <> b.State
        End Operator

        Public Shared Operator IsFalse(ByVal item As TS) As Boolean
            Return item.State = 1
        End Operator

        Public Shared Operator IsTrue(ByVal item As TS) As Boolean
            Return item.State = 2
        End Operator

        Public ReadOnly Property IsFalse() As Boolean
            Get
                Return State = 1
            End Get
        End Property

        Public ReadOnly Property IsTrue() As Boolean
            Get
                Return State = 2
            End Get
        End Property

        Public ReadOnly Property IsUndefined() As Boolean
            Get
                Return State = 0
            End Get
        End Property

        Public Overrides Function Equals(ByVal obj As Object) As Boolean
            Return (TypeOf obj Is TS AndAlso CType(obj, TS).State = State)
        End Function

        Public Overrides Function GetHashCode() As Integer
            Return State.GetHashCode
        End Function

        Public Overrides Function ToString() As String
            Select Case State
                Case 1 : Return Boolean.FalseString
                Case 2 : Return Boolean.TrueString
                Case Else : Return "Undefined"
            End Select
        End Function

        Public Shared Widening Operator CType(ByVal x As Boolean) As TS
            Return If(x, TS.True, TS.False)
        End Operator

        Public Shared Narrowing Operator CType(ByVal x As TS) As Boolean
            If x.IsTrue Then Return False
            If x.IsFalse Then Return True
            Throw New ArgumentException
        End Operator

    End Structure

End Namespace