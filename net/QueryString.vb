Imports System
Imports System.Collections.Generic
Imports System.Text
Imports System.Web.HttpUtility

Namespace Huggle

    <Diagnostics.DebuggerDisplay("{ToString()}")>
    Friend Class QueryString

        Private _Values As Dictionary(Of String, Object)

        Public Sub New(ByVal ParamArray values As Object())
            _Values = values.ToDictionary(Of String, Object)()
        End Sub

        Public ReadOnly Property Contains(ByVal name As String) As Boolean
            Get
                Return (Values.ContainsKey(name))
            End Get
        End Property

        Default Public ReadOnly Property Value(ByVal name As String) As Object
            Get
                If Values.ContainsKey(name) Then Return Values(name) Else Return Nothing
            End Get
        End Property

        Public ReadOnly Property Values() As Dictionary(Of String, Object)
            Get
                Return _Values
            End Get
        End Property

        Public Sub Add(ByVal name As String)
            Values.Merge(name, "")
        End Sub

        Public Sub Add(ByVal name As String, ByVal value As Object)
            If value IsNot Nothing Then Values.Merge(name, value)
        End Sub

        Public Sub Merge(ByVal ParamArray items As Object())
            Merge(items.ToDictionary(Of String, Object))
        End Sub

        Public Sub Merge(ByVal items As Dictionary(Of String, Object))
            For Each item As KeyValuePair(Of String, Object) In items
                Values.Merge(item.Key, item.Value)
            Next item
        End Sub

        Public Sub Remove(ByVal name As String)
            If Values.ContainsKey(name) Then Values.Remove(name)
        End Sub

        Public Overrides Function ToString() As String
            Dim items As New List(Of String)

            Dim keys As List(Of String) = Values.Keys.ToList
            keys.Sort()

            For Each key As String In keys
                Dim value As Object = Values(key)
                If value Is Nothing Then Continue For
                If TypeOf value Is IList(Of String) Then value = DirectCast(value, IList(Of String)).Join("|")
                If TypeOf value Is Boolean Then value = If(CBool(value), 1, 0)

                'Hide passwords/tokens in debug log
                If key.ToLowerI.EndsWithI("password") OrElse key.ToLowerI.EndsWithI("retype") _
                    OrElse key.ToLowerI.EndsWithI("token") Then value = "******"

                If value.ToString = "" Then items.Add(key) Else items.Add(key & "=" & value.ToString)
            Next key

            Return items.Join(" & ")
        End Function

        Public Function ToUrlString() As String
            If Values.Count = 0 Then Return ""
            Dim items As New List(Of String)

            For Each item As KeyValuePair(Of String, Object) In Values
                Dim value As Object = item.Value
                If value Is Nothing Then Continue For
                If TypeOf value Is IList(Of String) Then value = DirectCast(value, IList(Of String)).Join("|")
                If TypeOf value Is Boolean Then value = If(CBool(value), 1, 0)
                Dim str As String = UrlEncode(value.ToString)

                If str = "" Then items.Add(UrlEncode(item.Key)) Else items.Add(UrlEncode(item.Key) & "=" & str)
            Next item

            Return items.Join("&")
        End Function

        Public Function ToMultipart(ByVal boundary As String, ByVal filename As String) As Byte()
            Dim header As String = ""
            Dim fileparam As String = Nothing
            Dim footer As String = "--" & boundary & "--" & CRLF

            For Each item As KeyValuePair(Of String, Object) In Values
                If TypeOf item.Value Is Byte() Then
                    fileparam = item.Key
                Else
                    header &= "--" & boundary & CRLF & "Content-Disposition: form-data; name=""" &
                        item.Key & """" & CRLF & CRLF & item.Value.ToString & CRLF
                End If
            Next item

            Dim file As Byte() = {}

            If fileparam IsNot Nothing Then
                header &= "--" & boundary & CRLF
                header &= "Content-Disposition: form-data; name=""" & fileparam & """; filename=""" &
                    filename & """" & CRLF
                header &= "Content-Type: " & MimeType(filename) & CRLF
                header &= CRLF
                file = CType(Values(fileparam), Byte())
            End If

            Dim headerbytes As Byte() = Encoding.UTF8.GetBytes(header)
            Dim footerbytes As Byte() = Encoding.UTF8.GetBytes(footer)
            Dim data As Byte() = New Byte(headerbytes.Length + file.Length + footerbytes.Length) {}

            Buffer.BlockCopy(headerbytes, 0, data, 0, headerbytes.Length)
            Buffer.BlockCopy(file, 0, data, headerbytes.Length, file.Length)
            Buffer.BlockCopy(footerbytes, 0, data, headerbytes.Length + file.Length, footer.Length)

            Return data
        End Function

    End Class

End Namespace