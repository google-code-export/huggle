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

    Public Delegate Sub Action()

    'Generic event handler type with additional type parameter for the sender
    Public Delegate Sub EventHandler(Of TSender, TEventArgs As EventArgs) _
        (ByVal sender As TSender, ByVal e As TEventArgs)

End Namespace

Namespace System.Windows.Forms

    <Diagnostics.DebuggerStepThrough()>
    Public Module Extensions

        <Extension()>
        Function ToList(Of T)(ByVal items As ListView.ListViewItemCollection) As List(Of T)
            Dim result As New List(Of T)

            For Each item As Object In items
                result.Add(CType(item, T))
            Next item

            Return result
        End Function

        <Extension()>
        Function ToList(ByVal items As Control.ControlCollection) As List(Of Control)
            Dim result As New List(Of Control)

            For Each item As Control In items
                result.Add(item)
            Next item

            Return result
        End Function

    End Module

End Namespace

Namespace Huggle

    <Diagnostics.DebuggerStepThrough()>
    Public Module Extensions

        <Extension()>
        Sub Add(Of T)(ByVal list As List(Of T), ByVal ParamArray items() As T)
            list.AddRange(items)
        End Sub

        <Extension()>
        Function AllOfType(Of T, TSub As T)(ByVal list As List(Of T)) As List(Of TSub)
            Dim result As New List(Of TSub)

            For Each item As T In list
                If TypeOf item Is TSub Then result.Add(CType(item, TSub))
            Next item

            Return result
        End Function

        <Extension()>
        Function Attribute(ByVal node As XmlNode, ByVal value As String) As String
            If node.Attributes(value) IsNot Nothing Then Return HtmlDecode(node.Attributes(value).Value) Else Return Nothing
        End Function

        <Extension()>
        Function Contains(Of T)(ByVal array As T(), ByVal item As T) As Boolean
            Return System.Array.IndexOf(array, item) > -1
        End Function

        <Extension()>
        Function ContainsInstance(Of TList, TType)(ByVal items As List(Of TType)) As Boolean
            For Each item As TType In items
                If TypeOf item Is TList Then Return True
            Next item

            Return False
        End Function

        <Extension()>
        Function FirstWhere(Of T)(ByVal Items As List(Of T), ByVal predicate As Predicate(Of T)) As T
            For Each item As T In Items
                If predicate(item) Then Return item
            Next item

            Return Nothing
        End Function

        <Extension()>
        Function FirstInstance(Of TList, TType As Class)(ByVal items As List(Of TList)) As TType
            For Each item As TList In items
                If TypeOf item Is TType Then Return TryCast(item, TType)
            Next item

            Return Nothing
        End Function

        <Extension()>
        Sub ForEach(Of T)(ByVal list As IList(Of T), ByVal action As Action(Of T))
            For Each item As T In list
                action(item)
            Next item
        End Sub

        <Extension()>
        Function FormatWith(ByVal str As String, ByVal ParamArray params As Object()) As String
            If str Is Nothing Then Return Nothing
            If params Is Nothing OrElse params.Length = 0 Then Return str

            Try
                Return String.Format(str, params)
            Catch ex As FormatException
                Return str
            End Try
        End Function

        <Extension()>
        Function FromFirst(ByVal str As String, ByVal value As String,
            Optional ByVal include As Boolean = False) As String

            If str Is Nothing OrElse value Is Nothing Then Return Nothing
            If Not str.Contains(value) Then Return Nothing
            If include Then Return str.Substring(str.IndexOf(value)) _
                Else Return str.Substring(str.IndexOf(value) + value.Length)
        End Function

        <Extension()>
        Function FromLast(ByVal str As String, ByVal value As String,
            Optional ByVal include As Boolean = False) As String

            If str Is Nothing OrElse value Is Nothing Then Return Nothing
            If Not str.Contains(value) Then Return Nothing
            If include Then Return str.Substring(str.LastIndexOf(value)) _
                Else Return str.Substring(str.LastIndexOf(value) + value.Length)
        End Function

        <Extension()>
        Function HasAttribute(ByVal node As XmlNode, ByVal value As String) As Boolean
            Return (node.Attributes(value) IsNot Nothing)
        End Function

        <Extension()>
        Function IndexOf(Of T)(ByVal array As T(), ByVal item As T) As Integer
            Return System.Array.IndexOf(array, item)
        End Function

        <Extension()>
        Function IndexOfPattern(ByVal str As String, ByVal pattern As String,
            Optional ByVal startIndex As Integer = 0) As Integer

            Dim Match As Match = Regex.Match(str.Substring(startIndex), pattern, RegexOptions.Compiled)
            If Match.Success Then Return Match.Index + startIndex Else Return -1
        End Function

        <Extension()>
        Function ItemOrNull(Of TKey, TValue)(ByVal dictionary As Dictionary(Of TKey, TValue), ByVal key As TKey) As TValue
            If dictionary.ContainsKey(key) Then Return dictionary(key) Else Return Nothing
        End Function

        <Extension()>
        Function Join(Of T)(ByVal list As IList(Of T), Optional ByVal separator As String = "") As String
            Return String.Join(separator, list.ToStringArray)
        End Function

        <Extension()>
        Sub Merge(Of TKey, TValue)(ByVal dictionary As Dictionary(Of TKey, TValue),
            ByVal mergingDictionary As Dictionary(Of TKey, TValue))

            For Each item As KeyValuePair(Of TKey, TValue) In mergingDictionary
                dictionary.Merge(item)
            Next item
        End Sub

        <Extension()>
        Sub Merge(Of T)(ByVal list As IList(Of T), ByVal items As IList(Of T))
            For Each Item As T In items
                If Not list.Contains(Item) Then list.Add(Item)
            Next Item
        End Sub

        <Extension()>
        Sub Merge(Of T)(ByVal list As IList(Of T), ByVal item As T)
            If Not list.Contains(item) Then list.Add(item)
        End Sub

        <Extension()>
        Sub Merge(Of TKey, TValue)(ByVal dictionary As Dictionary(Of TKey, TValue),
            ByVal key As TKey, ByVal value As TValue)

            If Not Dictionary.ContainsKey(key) Then Dictionary.Add(key, value) Else Dictionary(key) = value
        End Sub

        <Extension()>
        Sub Merge(Of TKey, TValue)(ByVal dictionary As Dictionary(Of TKey, TValue),
            ByVal item As KeyValuePair(Of TKey, TValue))

            If dictionary.ContainsKey(item.Key) _
                Then dictionary(item.Key) = item.Value Else dictionary.Add(item.Key, item.Value)
        End Sub

        <Extension()>
        Function Remove(ByVal str As String, ByVal selection As Selection) As String
            Return str.Remove(selection.Start, selection.Length)
        End Function

        <Extension()>
        Function Remove(ByVal sourceString As String, ByVal removeString As String) As String
            Return sourceString.Replace(removeString, "")
        End Function

        <Extension()>
        Function Remove(ByVal str As String, ByVal ParamArray strings As String()) As String
            Dim result As String = str

            For Each item As String In strings
                result = result.Replace(item, "")
            Next item

            Return result
        End Function

        <Extension()>
        Sub RemoveAll(Of T)(ByVal list As IList(Of T), ByVal items As IList(Of T))
            For Each Item As T In items
                list.Remove(Item)
            Next Item
        End Sub

        <Extension()>
        Function Replace(ByVal str As String, ByVal selection As Selection, ByVal replacement As String) As String
            Return str.Remove(selection.Start, selection.Length).Insert(selection.Start, replacement)
        End Function

        <Extension()>
        Function Split(ByVal str As String, ByVal separator As String) As String()
            If str Is Nothing Then Return Nothing
            Return str.Split(New String() {separator}, StringSplitOptions.RemoveEmptyEntries)
        End Function

        <Extension()>
        Function Substring(ByVal str As String, ByVal selection As Selection) As String
            Return str.Substring(selection.Start, selection.Length)
        End Function

        <Extension()>
        Function ToArray(Of T)(ByVal list As IList(Of T)) As T()
            Dim result(list.Count - 1) As T

            For i As Integer = 0 To list.Count - 1
                result(i) = list(i)
            Next i

            Return result
        End Function

        <Extension()>
        Function ToArray(Of TKey, TValue)(ByVal keys As Dictionary(Of TKey, TValue).KeyCollection) As TKey()
            Return keys.ToList.ToArray
        End Function

        <Extension()>
        Function ToArray(Of TKey, TValue)(ByVal values As Dictionary(Of TKey, TValue).ValueCollection) As TValue()
            Return values.ToList.ToArray
        End Function

        <Extension()>
        Function ToBoolean(ByVal str As String) As Boolean
            If String.IsNullOrEmpty(str) Then Return False

            Dim result As Boolean
            If Boolean.TryParse(str, result) Then Return result

            Select Case str.ToLower
                Case "yes", "y", Msg("yes").ToLower : Return True
                Case "no", "n", Msg("no").ToLower : Return False
            End Select

            Return CBool(str)
        End Function

        <Extension()>
        Function ToDate(ByVal str As String) As Date
            If str Is Nothing Then Throw New ArgumentNullException("str")

            Dim result As Date
            If Date.TryParse(str, result) Then Return result

            'Also handle MediaWiki's internal timestamp format
            If str.Length = 14 AndAlso Long.TryParse(str, New Long) Then Return FromWikiTimestamp(str)

            Throw New ArgumentException("str")
        End Function

        <Extension()>
        Function ToDictionary(ByVal items As IList(Of Object)) As Dictionary(Of String, String)
            Dim result As New Dictionary(Of String, String)

            For i As Integer = 0 To items.Count - 2 Step 2
                If items(i) Is Nothing OrElse items(i + 1) Is Nothing Then Continue For
                result.Add(items(i).ToString, items(i + 1).ToString)
            Next i

            Return result
        End Function

        <Extension()>
        Function ToDictionary(ByVal str As String, Optional ByVal delimiter As String = ";",
            Optional ByVal Separator As String = ":") As Dictionary(Of String, String)

            Dim result As New Dictionary(Of String, String)

            For Each item As String In str.ToList(";")
                item = item.Replace("\:", C2)

                Dim key, value As String

                key = item.ToFirst(":").Replace(C2, ":").Trim
                If item.Contains(":") Then value = item.FromFirst(":").Replace(C2, ":").Trim Else value = ""

                result.Merge(key, value)
            Next item

            Return result
        End Function

        <Extension()>
        Function ToDictionary(Of TKey, TValue)(ByVal items As Object()) As Dictionary(Of TKey, TValue)
            Dim result As New Dictionary(Of TKey, TValue)

            If items.Length Mod 2 = 1 Then ReDim Preserve items(items.Length - 2)

            For i As Integer = 0 To items.Length - 2 Step 2
                If items(i) Is Nothing Then Continue For
                result.Add(CType(items(i), TKey), CType(items(i + 1), TValue))
            Next i

            Return result
        End Function

        <Extension()>
        Function ToDictionary(Of TSrcKey, TSrcValue, TDstKey, TDstValue) _
            (ByVal dictionary As Dictionary(Of TSrcKey, TSrcValue)) As Dictionary(Of TDstKey, TDstValue)

            Dim result As New Dictionary(Of TDstKey, TDstValue)

            For Each item As KeyValuePair(Of TSrcKey, TSrcValue) In dictionary
                result.Add(CType(CObj(item.Key), TDstKey), CType(CObj(item.Value), TDstValue))
            Next item

            Return result
        End Function

        <Extension()>
        Function ToFirst(ByVal str As String, ByVal value As String,
            Optional ByVal include As Boolean = False) As String

            If str Is Nothing OrElse value Is Nothing Then Return Nothing
            If Not str.Contains(value) Then Return str
            If include Then Return str.Substring(0, str.IndexOf(value) + value.Length) _
                Else Return str.Substring(0, str.IndexOf(value))
        End Function

        <Extension()>
        Function ToInteger(ByVal str As String) As Integer
            Return Convert.ToInt32(Str)
        End Function

        <Extension()>
        Function ToLast(ByVal str As String, ByVal value As String,
            Optional ByVal Include As Boolean = False) As String

            If str Is Nothing OrElse value Is Nothing Then Return Nothing
            If Not str.Contains(value) Then Return str
            If Include Then Return str.Substring(0, str.LastIndexOf(value) + value.Length) _
                Else Return str.Substring(0, str.LastIndexOf(value))
        End Function

        <Extension()>
        Function ToList(Of T)(ByVal items As IEnumerable(Of T)) As List(Of T)
            Return New List(Of T)(items)
        End Function

        <Extension()>
        Function ToList(Of T)(ByVal items As IEnumerable) As List(Of T)
            Dim result As New List(Of T)

            For Each item As Object In items
                result.Add(CType(item, T))
            Next item

            Return result
        End Function

        <Extension()>
        Function ToList(ByVal str As String, Optional ByVal delimiter As String = ",") As List(Of String)

            Dim result As New List(Of String)

            For Each item As String In str.Replace("\" & delimiter, C1).Split(delimiter)
                item = item.Replace(C1, delimiter)
                If item.Length > 0 Then result.Add(item)
            Next item

            Return result
        End Function

        <Extension()>
        Function ToList(Of T)(ByVal str As String, Optional ByVal delimiter As String = ",") As List(Of T)

            Dim result As New List(Of T)

            For Each item As String In str.Replace("\" & delimiter, C1).Split(delimiter)
                item = item.Replace(C1, delimiter)
                If item.Length > 0 Then result.Add(CType(CObj(item), T))
            Next item

            Return result
        End Function

        <Extension()>
        Function ToList(Of TKey, TValue)(ByVal values As Dictionary(Of TKey, TValue).ValueCollection) As List(Of TValue)
            Return New List(Of TValue)(values)
        End Function

        <Extension()>
        Function ToList(Of TKey, TValue)(ByVal keys As Dictionary(Of TKey, TValue).KeyCollection) As List(Of TKey)
            Dim result As New List(Of TKey)

            For Each item As TKey In keys
                result.Merge(item)
            Next item

            Return result
        End Function

        <Extension()>
        Function ToStringArray(Of T)(ByVal list As IList(Of T)) As String()
            Dim result(list.Count - 1) As String

            For i As Integer = 0 To list.Count - 1
                result(i) = If(list(i) Is Nothing, Nothing, list(i).ToString)
            Next i

            Return result
        End Function

        <Extension()>
        Function Trim(ByVal items As IList(Of String)) As List(Of String)
            Dim result As New List(Of String)

            For Each item As String In items
                result.Add(item.Trim)
            Next item

            Return result
        End Function

        <Extension()>
        Function TryParse(ByRef keys As Keys, ByVal value As String) As Boolean
            Try
                keys = CType([Enum].Parse(GetType(Keys), value, True), Keys)
                Return True
            Catch ex As ArgumentException
                Return False
            End Try
        End Function

        <Extension()>
        Sub Unmerge(Of T)(ByVal items As List(Of T), ByVal item As T)
            If items.Contains(item) Then items.Remove(item)
        End Sub

        <Extension()>
        Sub Unmerge(Of TKey, TValue)(ByVal items As Dictionary(Of TKey, TValue), ByVal item As TKey)
            If items.ContainsKey(item) Then items.Remove(item)
        End Sub

    End Module

End Namespace