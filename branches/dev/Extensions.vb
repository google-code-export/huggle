Imports Huggle
Imports System
Imports System.Collections
Imports System.Collections.Generic
Imports System.Drawing
Imports System.Runtime.CompilerServices
Imports System.Text.RegularExpressions
Imports System.Windows.Forms
Imports System.Xml

'Extension methods are a language feature that depend on
'System.Runtime.CompilerServices.ExtensionAttribute, which is new in .Net 3.5.
'This is a workaround to make extension methods work in .Net 2.0 by
'creating the attribute manually. It doesn't actually have to do anything.
'Visual Studio 2008 is required, as previous versions do not understand the language feature.

Namespace System.Runtime.CompilerServices

    <AttributeUsageAttribute(AttributeTargets.Assembly Or AttributeTargets.Class Or AttributeTargets.Method)> _
    Public NotInheritable Class ExtensionAttribute : Inherits Attribute

    End Class

End Namespace

Namespace System

    Public Delegate Sub Action()

    'Generic event handler type with additional type parameter for the sender
    Public Delegate Sub EventHandler(Of TSender, TEventArgs As EventArgs) _
        (ByVal sender As TSender, ByVal e As TEventArgs)

End Namespace

Public Module Extensions

    <Extension()> _
    Sub Add(Of T)(ByVal list As List(Of T), ByVal ParamArray items() As T)
        list.AddRange(items)
    End Sub

    <Extension()> _
    Function All(Of T, TSub As T)(ByVal List As List(Of T)) As List(Of TSub)
        Dim Result As New List(Of TSub)

        For Each Item As T In List
            If TypeOf Item Is TSub Then Result.Add(CType(Item, TSub))
        Next Item

        Return Result
    End Function

    <Extension()> _
    Function Attribute(ByVal node As XmlNode, ByVal value As String) As String
        If node.Attributes(value) IsNot Nothing Then Return node.Attributes(value).Value Else Return Nothing
    End Function

    <Extension()> _
    Function Contains(Of T)(ByVal Array As T(), ByVal Item As T) As Boolean
        Return System.Array.IndexOf(Array, Item) > -1
    End Function

    <Extension()> _
    Function ContainsInstance(Of T, T2)(ByVal Items As List(Of T2)) As Boolean
        For Each Item As T2 In Items
            If TypeOf Item Is T Then Return True
        Next Item

        Return False
    End Function

    <Extension()> _
    Function ContainsPattern(ByVal Str As String, ByVal Pattern As String) As Boolean
        Return Regex.Match(Str, Pattern, RegexOptions.Compiled).Success
    End Function

    <Extension()> _
    Function First(Of T)(ByVal Items As List(Of T), ByVal Predicate As Predicate(Of T)) As T
        For Each Item As T In Items
            If Predicate(Item) Then Return Item
        Next Item

        Return Nothing
    End Function

    <Extension()> _
    Function First(Of TList, TType As Class)(ByVal Items As List(Of TList)) As TType
        For Each Item As TList In Items
            If TypeOf Item Is TType Then Return TryCast(Item, TType)
        Next Item

        Return Nothing
    End Function

    <Extension()> _
    Sub ForEach(Of T)(ByVal List As IList(Of T), ByVal Action As Action(Of T))
        For Each Item As T In List
            Action(Item)
        Next Item
    End Sub

    <Extension()> _
    Function FormatWith(ByVal Str As String, ByVal ParamArray Params As Object()) As String
        If Str Is Nothing Then Return Nothing
        If Params Is Nothing OrElse Params.Length = 0 Then Return Str

        Try
            Return String.Format(Str, Params)
        Catch ex As FormatException
            Return Str
        End Try
    End Function

    <Extension()> _
    Function FromFirst(ByVal Str As String, ByVal Value As String, _
        Optional ByVal Include As Boolean = False) As String

        If Str Is Nothing OrElse Value Is Nothing Then Return Nothing
        If Not Str.Contains(Value) Then Return Nothing
        If Include Then Return Str.Substring(Str.IndexOf(Value)) _
            Else Return Str.Substring(Str.IndexOf(Value) + Value.Length)
    End Function

    <Extension()> _
    Function FromLast(ByVal Str As String, ByVal Value As String, _
        Optional ByVal Include As Boolean = False) As String

        If Str Is Nothing OrElse Value Is Nothing Then Return Nothing
        If Not Str.Contains(Value) Then Return Nothing
        If Include Then Return Str.Substring(Str.LastIndexOf(Value)) _
            Else Return Str.Substring(Str.LastIndexOf(Value) + Value.Length)
    End Function

    <Extension()> _
    Function HasAttribute(ByVal Node As XmlNode, ByVal Value As String) As Boolean
        Return (Node.Attributes(Value) IsNot Nothing)
    End Function

    <Extension()> _
    Function IndexOf(Of T)(ByVal Array As T(), ByVal Item As T) As Integer
        Return System.Array.IndexOf(Array, Item)
    End Function

    <Extension()> _
    Function IndexOfPattern(ByVal Str As String, ByVal Pattern As String, _
        Optional ByVal StartIndex As Integer = 0) As Integer

        Dim Match As Match = Regex.Match(Str.Substring(StartIndex), Pattern, RegexOptions.Compiled)
        If Match.Success Then Return Match.Index + StartIndex Else Return -1
    End Function

    <Extension()> _
    Function IsEmpty(ByVal Str As String) As Boolean
        Return (Str = "")
    End Function

    <Extension()> _
    Function ItemOrNull(Of TKey, TValue)(ByVal Dictionary As Dictionary(Of TKey, TValue), ByVal Key As TKey) As TValue
        If Dictionary.ContainsKey(Key) Then Return Dictionary(Key) Else Return Nothing
    End Function

    <Extension()> _
    Function Join(Of T)(ByVal list As IList(Of T), Optional ByVal separator As String = "") As String
        Return String.Join(separator, list.ToStringArray)
    End Function

    <Extension()> _
    Sub Merge(Of TKey, TValue) _
        (ByVal Dictionary As Dictionary(Of TKey, TValue), ByVal MergingDictionary As Dictionary(Of TKey, TValue))

        For Each Item As KeyValuePair(Of TKey, TValue) In MergingDictionary
            Dictionary.Merge(Item)
        Next Item
    End Sub

    <Extension()> _
    Sub Merge(Of T)(ByVal List As IList(Of T), ByVal Items As IList(Of T))
        For Each Item As T In Items
            If Not List.Contains(Item) Then List.Add(Item)
        Next Item
    End Sub

    <Extension()> _
    Sub Merge(Of T)(ByVal List As IList(Of T), ByVal Item As T)
        If Not List.Contains(Item) Then List.Add(Item)
    End Sub

    <Extension()> _
    Sub Merge(Of TKey, TValue) _
        (ByVal Dictionary As Dictionary(Of TKey, TValue), ByVal Key As TKey, ByVal Value As TValue)

        If Not Dictionary.ContainsKey(Key) Then Dictionary.Add(Key, Value) Else Dictionary(Key) = Value
    End Sub

    <Extension()> _
    Sub Merge(Of TKey, TValue) _
        (ByVal Dictionary As Dictionary(Of TKey, TValue), ByVal Item As KeyValuePair(Of TKey, TValue))

        If Dictionary.ContainsKey(Item.Key) _
            Then Dictionary(Item.Key) = Item.Value Else Dictionary.Add(Item.Key, Item.Value)
    End Sub

    <Extension()> _
    Function Remove(ByVal Str As String, ByVal Selection As Selection) As String
        Return Str.Remove(Selection.Start, Selection.Length)
    End Function

    <Extension()> _
    Function Remove(ByVal sourceString As String, ByVal removeString As String) As String
        Return sourceString.Replace(removeString, "")
    End Function

    <Extension()> _
    Function Remove(ByVal Str As String, ByVal ParamArray Strings As String()) As String
        Dim Result As String = Str

        For Each Item As String In Strings
            Result = Result.Replace(Item, "")
        Next Item

        Return Result
    End Function

    <Extension()> _
    Sub RemoveAll(Of T)(ByVal List As IList(Of T), ByVal Items As IList(Of T))
        For Each Item As T In Items
            List.Remove(Item)
        Next Item
    End Sub

    <Extension()> _
    Function Replace(ByVal Str As String, ByVal Selection As Selection, ByVal Replacement As String) As String
        Return Str.Remove(Selection.Start, Selection.Length).Insert(Selection.Start, Replacement)
    End Function

    <Extension()> _
    Function Split(ByVal Str As String, ByVal Separator As String) As String()
        If Str Is Nothing Then Return Nothing
        Return Str.Split(New String() {Separator}, StringSplitOptions.RemoveEmptyEntries)
    End Function

    <Extension()> _
    Function Substring(ByVal Str As String, ByVal Selection As Selection) As String
        Return Str.Substring(Selection.Start, Selection.Length)
    End Function

    <Extension()> _
    Function ToArray(Of T)(ByVal List As IList(Of T)) As T()
        Dim Result(List.Count - 1) As T

        For i As Integer = 0 To List.Count - 1
            Result(i) = List(i)
        Next i

        Return Result
    End Function

    <Extension()> _
    Function ToArray(Of TKey, TValue)(ByVal Keys As Dictionary(Of TKey, TValue).KeyCollection) As TKey()
        Return Keys.ToList.ToArray
    End Function

    <Extension()> _
    Function ToArray(Of TKey, TValue)(ByVal Values As Dictionary(Of TKey, TValue).ValueCollection) As TValue()
        Return Values.ToList.ToArray
    End Function

    <Extension()> _
    Function ToBoolean(ByVal Str As String) As Boolean
        Dim Result As Boolean
        If Str Is Nothing Then Return False
        If Str = "" Then Return True

        If Boolean.TryParse(Str, Result) Then Return Result

        Select Case Str.ToLower
            Case "yes", "y", Msg("yes").ToLower : Return True
            Case "no", "n", Msg("no").ToLower : Return False
        End Select

        Return CBool(Str)
    End Function

    <Extension()> _
    Function ToDate(ByVal str As String) As Date
        Dim result As Date
        If Date.TryParse(str, result) Then Return result

        'Also handle MediaWiki's internal timestamp format
        If str.Length = 14 AndAlso Long.TryParse(str, New Long) Then Return FromWikiTimestamp(str)
        Return Date.MinValue
    End Function

    <Extension()> _
    Function ToDictionary(ByVal Items As IList(Of Object)) As Dictionary(Of String, String)
        Dim Result As New Dictionary(Of String, String)

        For i As Integer = 0 To Items.Count - 2 Step 2
            If Items(i) Is Nothing OrElse Items(i + 1) Is Nothing Then Continue For
            Result.Add(Items(i).ToString, Items(i + 1).ToString)
        Next i

        Return Result
    End Function

    <Extension()> _
    Function ToDictionary(ByVal Str As String, Optional ByVal Delimiter As String = ";", _
        Optional ByVal Separator As String = ":") As Dictionary(Of String, String)

        Dim Result As New Dictionary(Of String, String)

        For Each Item As String In Str.ToList(";")
            Item = Item.Replace("\:", C2)

            Dim Key, Value As String

            Key = Item.ToFirst(":").Replace(C2, ":").Trim
            If Item.Contains(":") Then Value = Item.FromFirst(":").Replace(C2, ":").Trim Else Value = ""

            Result.Merge(Key, Value)
        Next Item

        Return Result
    End Function

    <Extension()> _
    Function ToDictionary(Of TKey, TValue)(ByVal items As Object()) As Dictionary(Of TKey, TValue)
        Dim result As New Dictionary(Of TKey, TValue)

        If items.Length Mod 2 = 1 Then ReDim Preserve items(items.Length - 2)

        For i As Integer = 0 To items.Length - 2 Step 2
            If items(i) Is Nothing Then Continue For
            result.Add(CType(items(i), TKey), CType(items(i + 1), TValue))
        Next i

        Return result
    End Function

    <Extension()> _
    Function ToDictionary(Of TSrcKey, TSrcValue, TDstKey, TDstValue) _
        (ByVal dictionary As Dictionary(Of TSrcKey, TSrcValue)) As Dictionary(Of TDstKey, TDstValue)

        Dim result As New Dictionary(Of TDstKey, TDstValue)

        For Each item As KeyValuePair(Of TSrcKey, TSrcValue) In dictionary
            result.Add(CType(CObj(item.Key), TDstKey), CType(CObj(item.Value), TDstValue))
        Next item

        Return result
    End Function

    <Extension()> _
    Function ToFirst(ByVal Str As String, ByVal Value As String, _
        Optional ByVal Include As Boolean = False) As String

        If Str Is Nothing OrElse Value Is Nothing Then Return Nothing
        If Not Str.Contains(Value) Then Return Str
        If Include Then Return Str.Substring(0, Str.IndexOf(Value) + Value.Length) _
            Else Return Str.Substring(0, Str.IndexOf(Value))
    End Function

    <Extension()> _
    Function ToInteger(ByVal Str As String) As Integer
        Return Convert.ToInt32(Str)
    End Function

    <Extension()> _
    Function ToLast(ByVal Str As String, ByVal Value As String, _
        Optional ByVal Include As Boolean = False) As String

        If Str Is Nothing OrElse Value Is Nothing Then Return Nothing
        If Not Str.Contains(Value) Then Return Str
        If Include Then Return Str.Substring(0, Str.LastIndexOf(Value) + Value.Length) _
            Else Return Str.Substring(0, Str.LastIndexOf(Value))
    End Function

    <Extension()> _
    Function ToList(Of T)(ByVal array As T()) As List(Of T)
        Return New List(Of T)(array)
    End Function

    <Extension()> _
    Function ToList(Of T)(ByVal items As IEnumerable(Of T)) As List(Of T)
        Dim result As New List(Of T)

        For Each item As T In items
            result.Add(item)
        Next item

        Return result
    End Function

    <Extension()> _
    Function ToList(Of T)(ByVal items As IEnumerable) As List(Of T)
        Dim result As New List(Of T)

        For Each item As Object In items
            result.Add(CType(item, T))
        Next item

        Return result
    End Function

    <Extension()> _
    Function ToList(ByVal str As String, Optional ByVal delimiter As String = ",") As List(Of String)

        Dim result As New List(Of String)

        For Each item As String In str.Replace("\" & delimiter, C1).Split(delimiter)
            item = item.Replace(C1, delimiter)
            If item.Length > 0 Then result.Add(item)
        Next item

        Return result
    End Function

    <Extension()> _
    Function ToList(Of T)(ByVal Str As String, Optional ByVal Delimiter As String = ",") As List(Of T)

        Dim Result As New List(Of T)

        For Each Item As String In Str.Replace("\" & Delimiter, C1).Split(Delimiter)
            Item = Item.Replace(C1, Delimiter)
            If Item.Length > 0 Then Result.Add(CType(CObj(Item), T))
        Next Item

        Return Result
    End Function

    <Extension()> _
    Function ToList(ByVal Items As Control.ControlCollection) As List(Of Control)
        Dim Result As New List(Of Control)

        For Each Item As Control In Items
            Result.Add(Item)
        Next Item

        Return Result
    End Function

    <Extension()> _
    Function ToList(Of TKey, TValue)(ByVal Values As Dictionary(Of TKey, TValue).ValueCollection) As List(Of TValue)

        Return New List(Of TValue)(Values)
    End Function

    <Extension()> _
    Function ToList(Of TKey, TValue)(ByVal Keys As Dictionary(Of TKey, TValue).KeyCollection) As List(Of TKey)
        Dim Result As New List(Of TKey)

        For Each Item As TKey In Keys
            Result.Merge(Item)
        Next Item

        Return Result
    End Function

    <Extension()> _
    Function ToStringArray(Of T)(ByVal list As IList(Of T)) As String()
        Dim result(list.Count - 1) As String

        For i As Integer = 0 To list.Count - 1
            result(i) = If(list(i) Is Nothing, Nothing, list(i).ToString)
        Next i

        Return result
    End Function

    <Extension()> _
    Function Trim(ByVal items As IList(Of String)) As List(Of String)
        Dim result As New List(Of String)

        For Each item As String In items
            result.Add(item.Trim)
        Next item

        Return result
    End Function

    <Extension()> _
    Function TryParse(ByRef Keys As Keys, ByVal Value As String) As Boolean
        Try
            Keys = CType([Enum].Parse(GetType(Keys), Value, True), Keys)
            Return True
        Catch ex As ArgumentException
            Return False
        End Try
    End Function

    <Extension()> _
    Sub Unmerge(Of T)(ByVal items As List(Of T), ByVal item As T)
        If items.Contains(item) Then items.Remove(item)
    End Sub

    <Extension()> _
    Sub Unmerge(Of TKey, TValue)(ByVal items As Dictionary(Of TKey, TValue), ByVal item As TKey)
        If items.ContainsKey(item) Then items.Remove(item)
    End Sub

End Module