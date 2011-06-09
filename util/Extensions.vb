Imports System
Imports System.Collections
Imports System.Collections.Generic
Imports System.Drawing
Imports System.Runtime.CompilerServices
Imports System.Web.HttpUtility
Imports System.Text.RegularExpressions
Imports System.Windows.Forms
Imports System.Xml

'Extension methods are a language feature that depend on
'System.Runtime.CompilerServices.ExtensionAttribute, which is new in .Net 3.5.
'This is a workaround to make extension methods work in .Net 2.0 by
'creating the attribute manually. It doesn't actually have to do anything.
'Visual Studio 2008 is required, as previous versions do not understand the language feature.

Namespace System.Runtime.CompilerServices

    <AttributeUsageAttribute(AttributeTargets.Assembly Or AttributeTargets.Class Or AttributeTargets.Method)>
    Public NotInheritable Class ExtensionAttribute : Inherits Attribute

    End Class

End Namespace

Namespace System

    Friend Delegate Sub Action()
    Friend Delegate Function [Function](Of T, TReturn)(ByVal param As T) As TReturn
    Friend Delegate Sub SimpleEventHandler(Of T)(ByVal sender As Object, ByVal e As EventArgs(Of T))

    Friend Class EventArgs(Of T) : Inherits EventArgs

        Private _Value As T

        Public Sub New(ByVal value As T)
            _Value = value
        End Sub

        Public ReadOnly Property Value As T
            Get
                Return _Value
            End Get
        End Property

    End Class

End Namespace

Namespace Huggle

    Friend Module Extensions

        <Extension()>
        Public Sub Add(Of T)(ByVal this As List(Of T), ByVal ParamArray items() As T)
            this.AddRange(items)
        End Sub

        <Extension()>
        Public Function AllOfType(Of T, TSub As T)(ByVal this As List(Of T)) As List(Of TSub)
            Dim result As New List(Of TSub)

            For Each item As T In this
                If TypeOf item Is TSub Then result.Add(CType(item, TSub))
            Next item

            Return result
        End Function

        <Extension()>
        Public Function Attribute(ByVal this As XmlNode, ByVal value As String) As String
            If this.Attributes(value) IsNot Nothing Then Return HtmlDecode(this.Attributes(value).Value) Else Return Nothing
        End Function

        <Extension()>
        Public Function Contains(Of T)(ByVal this As T(), ByVal item As T) As Boolean
            Return Array.IndexOf(this, item) > -1
        End Function

        <Extension()>
        Public Function ContainsInstance(Of TList, TType)(ByVal this As List(Of TType)) As Boolean
            For Each item As TType In this
                If TypeOf item Is TList Then Return True
            Next item

            Return False
        End Function

        <Extension()>
        Public Function Duplicate(ByVal this As Char, ByVal count As Integer) As String
            Return "".PadRight(count, this)
        End Function

        <Extension()>
        Public Function FirstWhere(Of T)(ByVal this As List(Of T), ByVal predicate As Predicate(Of T)) As T
            For Each item As T In this
                If predicate(item) Then Return item
            Next item

            Return Nothing
        End Function

        <Extension()>
        Public Function FirstInstance(Of TList, TType As Class)(ByVal this As List(Of TList)) As TType
            For Each item As TList In this
                If TypeOf item Is TType Then Return TryCast(item, TType)
            Next item

            Return Nothing
        End Function

        <Extension()>
        Public Sub ForEach(Of T)(ByVal this As IEnumerable(Of T), ByVal action As Action(Of T))
            For Each item As T In this
                action(item)
            Next item
        End Sub

        <Extension()>
        Public Function FromFirst(ByVal this As String, ByVal value As String) As String

            If this Is Nothing OrElse value Is Nothing Then Return Nothing
            If Not this.Contains(value) Then Return Nothing
            Return this.Substring(this.IndexOfI(value) + value.Length)
        End Function

        <Extension()>
        Public Function FromFirstInclusive(ByVal this As String, ByVal value As String) As String
            If this Is Nothing OrElse value Is Nothing Then Return Nothing
            If Not this.Contains(value) Then Return Nothing
            Return this.Substring(this.IndexOfI(value))
        End Function

        <Extension()>
        Public Function FromLast(ByVal this As String, ByVal value As String) As String

            If this Is Nothing OrElse value Is Nothing Then Return Nothing
            If Not this.Contains(value) Then Return Nothing
            Return this.Substring(this.LastIndexOfI(value) + value.Length)
        End Function

        <Extension()>
        Public Function FromLastInclusive(ByVal this As String, ByVal value As String) As String

            If this Is Nothing OrElse value Is Nothing Then Return Nothing
            If Not this.Contains(value) Then Return Nothing
            Return this.Substring(this.LastIndexOfI(value))
        End Function

        <Extension()>
        Public Function HasAttribute(ByVal this As XmlNode, ByVal value As String) As Boolean
            Return (this.Attributes(value) IsNot Nothing)
        End Function

        <Extension()>
        Public Function IndexOf(Of T)(ByVal this As T(), ByVal item As T) As Integer
            Return System.Array.IndexOf(this, item)
        End Function

        <Extension()>
        Public Function IndexOfPattern(ByVal this As String, ByVal pattern As String,
            Optional ByVal startIndex As Integer = 0) As Integer

            Dim Match As Match = Regex.Match(this.Substring(startIndex), pattern, RegexOptions.Compiled)
            If Match.Success Then Return Match.Index + startIndex Else Return -1
        End Function

        <Extension()>
        Public Function ItemOrNull(Of TKey, TValue)(ByVal this As Dictionary(Of TKey, TValue), ByVal key As TKey) As TValue
            If this.ContainsKey(key) Then Return this(key) Else Return Nothing
        End Function

        <Extension()>
        Public Function Join(Of T)(ByVal this As IList(Of T), Optional ByVal separator As String = "") As String
            Return String.Join(separator, this.ToStringArray)
        End Function

        <Extension()>
        Public Function Map(Of T, T2)(ByVal this As IList(Of T), ByVal action As [Function](Of T, T2)) As IList(Of T2)
            Dim result As New List(Of T2)

            For Each item As T In this
                result.Add(action(item))
            Next item

            Return result
        End Function

        <Extension()>
        Public Sub Merge(Of TKey, TValue)(ByVal this As Dictionary(Of TKey, TValue),
            ByVal mergingDictionary As Dictionary(Of TKey, TValue))

            For Each item As KeyValuePair(Of TKey, TValue) In mergingDictionary
                this.Merge(item)
            Next item
        End Sub

        <Extension()>
        Public Sub Merge(Of T)(ByVal this As IList(Of T), ByVal items As IList(Of T))
            For Each item As T In items
                If Not this.Contains(item) Then this.Add(item)
            Next item
        End Sub

        <Extension()>
        Public Sub Merge(Of T)(ByVal this As IList(Of T), ByVal item As T)
            If Not this.Contains(item) Then this.Add(item)
        End Sub

        <Extension()>
        Public Sub Merge(Of TKey, TValue)(ByVal this As Dictionary(Of TKey, TValue),
            ByVal key As TKey, ByVal value As TValue)

            If Not this.ContainsKey(key) Then this.Add(key, value) Else this(key) = value
        End Sub

        <Extension()>
        Public Sub Merge(Of TKey, TValue)(ByVal this As Dictionary(Of TKey, TValue),
            ByVal item As KeyValuePair(Of TKey, TValue))

            If this.ContainsKey(item.Key) Then this(item.Key) = item.Value Else this.Add(item.Key, item.Value)
        End Sub

        <Extension()>
        Public Function Remove(ByVal this As String, ByVal selection As Selection) As String
            Return this.Remove(selection.Start, selection.Length)
        End Function

        <Extension()>
        Public Function Remove(ByVal this As String, ByVal removeString As String) As String
            Return this.Replace(removeString, "")
        End Function

        <Extension()>
        Public Function Remove(ByVal this As String, ByVal ParamArray strings As String()) As String
            Dim result As String = this

            For Each item As String In strings
                result = result.Replace(item, "")
            Next item

            Return result
        End Function

        <Extension()>
        Public Sub RemoveAll(Of T)(ByVal this As IList(Of T), ByVal items As IList(Of T))
            For Each item As T In items
                this.Remove(item)
            Next item
        End Sub

        <Extension()>
        Public Function Replace(ByVal this As String, ByVal selection As Selection, ByVal replacement As String) As String
            Return this.Remove(selection.Start, selection.Length).Insert(selection.Start, replacement)
        End Function

        <Extension()>
        Public Function Split(ByVal this As String, ByVal separator As String) As String()
            If this Is Nothing Then Return Nothing
            Return this.Split(New String() {separator}, StringSplitOptions.RemoveEmptyEntries)
        End Function

        <Extension()>
        Public Function Substring(ByVal this As String, ByVal selection As Selection) As String
            Return this.Substring(selection.Start, selection.Length)
        End Function

        <Extension()>
        Public Function ToArray(Of T)(ByVal this As IList(Of T)) As T()
            Dim result(this.Count - 1) As T

            For i As Integer = 0 To this.Count - 1
                result(i) = this(i)
            Next i

            Return result
        End Function

        <Extension()>
        Public Function ToArray(Of TKey, TValue)(ByVal this As Dictionary(Of TKey, TValue).KeyCollection) As TKey()
            Return this.ToList.ToArray
        End Function

        <Extension()>
        Public Function ToArray(Of TKey, TValue)(ByVal this As Dictionary(Of TKey, TValue).ValueCollection) As TValue()
            Return this.ToList.ToArray
        End Function

        <Extension()>
        Public Function ToBoolean(ByVal this As String) As Boolean
            If String.IsNullOrEmpty(this) Then Return False

            Dim result As Boolean
            If Boolean.TryParse(this, result) Then Return result

            Select Case this.ToLowerI
                Case "yes", "y", Msg("yes").ToLowerI : Return True
                Case "no", "n", Msg("no").ToLowerI : Return False
            End Select

            Return CBool(this)
        End Function

        <Extension()>
        Public Function ToDate(ByVal this As String) As Date
            If this Is Nothing Then Throw New ArgumentNullException("this")

            Dim result As Date
            If Date.TryParse(this, result) Then Return result

            'Also handle MediaWiki's internal timestamp format
            If this.Length = 14 AndAlso Long.TryParse(this, New Long) Then Return FromWikiTimestamp(this)

            Throw New ArgumentException("Could not parse string as date", "this")
        End Function

        <Extension()>
        Public Function ToDictionary(ByVal this As IList(Of Object)) As Dictionary(Of String, String)
            Dim result As New Dictionary(Of String, String)

            For i As Integer = 0 To this.Count - 2 Step 2
                If this(i) Is Nothing OrElse this(i + 1) Is Nothing Then Continue For
                result.Add(this(i).ToString, this(i + 1).ToString)
            Next i

            Return result
        End Function

        <Extension()>
        Public Function ToDictionary(ByVal this As String, Optional ByVal delimiter As String = ";",
            Optional ByVal separator As String = ":") As Dictionary(Of String, String)

            Dim result As New Dictionary(Of String, String)

            For Each item As String In this.ToList(delimiter)
                item = item.Replace("\" & separator, C2)

                Dim key, value As String

                key = item.ToFirst(separator).Replace(C2, separator).Trim
                If item.Contains(separator) Then value = item.FromFirst(separator).Replace(C2, separator).Trim _
                    Else value = ""

                result.Merge(key, value)
            Next item

            Return result
        End Function

        <Extension()>
        Public Function ToDictionary(Of TKey, TValue)(ByVal this As Object()) As Dictionary(Of TKey, TValue)
            Dim result As New Dictionary(Of TKey, TValue)

            If this.Length Mod 2 = 1 Then ReDim Preserve this(this.Length - 2)

            For i As Integer = 0 To this.Length - 2 Step 2
                If this(i) Is Nothing Then Continue For
                result.Add(CType(this(i), TKey), CType(this(i + 1), TValue))
            Next i

            Return result
        End Function

        <Extension()>
        Public Function ToDictionary(Of TSrcKey, TSrcValue, TDstKey, TDstValue)(
            ByVal this As Dictionary(Of TSrcKey, TSrcValue)) As Dictionary(Of TDstKey, TDstValue)

            Dim result As New Dictionary(Of TDstKey, TDstValue)

            For Each item As KeyValuePair(Of TSrcKey, TSrcValue) In this
                result.Add(CType(CObj(item.Key), TDstKey), CType(CObj(item.Value), TDstValue))
            Next item

            Return result
        End Function

        <Extension()>
        Public Function ToFirst(ByVal this As String, ByVal value As String) As String
            If this Is Nothing OrElse value Is Nothing Then Return Nothing
            If Not this.Contains(value) Then Return this

            Return this.Substring(0, this.IndexOfI(value))
        End Function

        <Extension()>
        Public Function ToFirstInclusive(ByVal this As String, ByVal value As String) As String
            If this Is Nothing OrElse value Is Nothing Then Return Nothing
            If Not this.Contains(value) Then Return this

            Return this.Substring(0, this.IndexOfI(value) + value.Length)
        End Function

        <Extension()>
        Public Function ToLast(ByVal this As String, ByVal value As String) As String
            If this Is Nothing OrElse value Is Nothing Then Return Nothing
            If Not this.Contains(value) Then Return this

            Return this.Substring(0, this.LastIndexOfI(value))
        End Function

        <Extension()>
        Public Function ToLastInclusive(ByVal this As String, ByVal value As String) As String
            If this Is Nothing OrElse value Is Nothing Then Return Nothing
            If Not this.Contains(value) Then Return this

            Return this.Substring(0, this.LastIndexOfI(value) + value.Length)
        End Function

        <Extension()>
        Public Function ToList(Of T)(ByVal this As IEnumerable(Of T)) As List(Of T)
            Return New List(Of T)(this)
        End Function

        <Extension()>
        Public Function ToList(Of T)(ByVal this As IEnumerable) As List(Of T)
            Dim result As New List(Of T)

            For Each item As Object In this
                result.Add(CType(item, T))
            Next item

            Return result
        End Function

        <Extension()>
        Public Function ToList(ByVal this As String, Optional ByVal delimiter As String = ",") As List(Of String)
            Dim result As New List(Of String)

            For Each item As String In this.Replace("\" & delimiter, C1).Split(delimiter)
                item = item.Replace(C1, delimiter)
                If item.Length > 0 Then result.Add(item)
            Next item

            Return result
        End Function

        <Extension()>
        Public Function ToList(Of T)(ByVal this As String, Optional ByVal delimiter As String = ",") As List(Of T)

            Dim result As New List(Of T)

            For Each item As String In this.Replace("\" & delimiter, C1).Split(delimiter)
                item = item.Replace(C1, delimiter)
                If item.Length > 0 Then result.Add(CType(CObj(item), T))
            Next item

            Return result
        End Function

        <Extension()>
        Public Function ToList(Of TKey, TValue)(
            ByVal this As Dictionary(Of TKey, TValue).ValueCollection) As List(Of TValue)

            Return New List(Of TValue)(this)
        End Function

        <Extension()>
        Public Function ToList(Of TKey, TValue)(ByVal this As Dictionary(Of TKey, TValue).KeyCollection) As List(Of TKey)
            Dim result As New List(Of TKey)

            For Each item As TKey In this
                result.Merge(item)
            Next item

            Return result
        End Function

        <Extension()>
        Public Function ToStringArray(Of T)(ByVal this As IList(Of T)) As String()
            Dim result(this.Count - 1) As String

            For i As Integer = 0 To this.Count - 1
                result(i) = If(this(i) Is Nothing, Nothing, this(i).ToString)
            Next i

            Return result
        End Function

        <Extension()>
        Public Function Trim(ByVal this As IList(Of String)) As List(Of String)
            Dim result As New List(Of String)

            For Each item As String In this
                result.Add(item.Trim)
            Next item

            Return result
        End Function

        <Extension()>
        Public Function TryParse(Of T As Structure)(ByRef this As T, ByVal value As String) As Boolean
            Try
                this = CType(System.Enum.Parse(GetType(T), value, True), T)
                Return True
            Catch ex As ArgumentException
                Return False
            End Try
        End Function

        <Extension()>
        Public Sub Unmerge(Of T)(ByVal this As List(Of T), ByVal item As T)
            If this.Contains(item) Then this.Remove(item)
        End Sub

        <Extension()>
        Public Sub Unmerge(Of TKey, TValue)(ByVal this As Dictionary(Of TKey, TValue), ByVal item As TKey)
            If this.ContainsKey(item) Then this.Remove(item)
        End Sub

    End Module

End Namespace