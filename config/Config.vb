Imports System
Imports System.Collections.Generic
Imports System.IO
Imports System.Text
Imports System.Text.RegularExpressions
Imports System.Windows.Forms

Namespace Huggle

    Public NotInheritable Class Config

        Private Shared _Global As New GlobalConfig
        Private Shared _Internal As New InternalConfig
        Private Shared _Local As New LocalConfig
        Private Shared _Messages As New MessageConfig

        Public Shared CustomReverts As New Dictionary(Of Page, CustomRevert)

        Private Sub New()
        End Sub

        Public Shared ReadOnly Property [Global]() As GlobalConfig
            Get
                Return _Global
            End Get
        End Property

        Public Shared ReadOnly Property Internal() As InternalConfig
            Get
                Return _Internal
            End Get
        End Property

        Public Shared ReadOnly Property Local() As LocalConfig
            Get
                Return _Local
            End Get
        End Property

        Public Shared ReadOnly Property Messages() As MessageConfig
            Get
                Return _Messages
            End Get
        End Property

        Public Shared Function MakeConfig(ByVal items As Dictionary(Of String, Object)) As String
            If items Is Nothing Then Return ""
            Dim result As New StringBuilder

            Dim sortedKeys As New List(Of String)(items.Keys)
            sortedKeys.Sort(Function(x As String, y As String) String.Compare(x, y, StringComparison.Ordinal))

            For Each key As String In sortedKeys
                Dim item As Object = items(key)
                If item Is Nothing Then Continue For

                result.Append(key & ":")
                Dim str As String

                If TypeOf item Is Dictionary(Of String, Object) Then
                    str = MakeConfig(CType(item, Dictionary(Of String, Object)))
                ElseIf TypeOf item Is List(Of String) Then
                    str = CType(item, List(Of String)).Join(", ")
                ElseIf TypeOf item Is Date Then
                    str = DirectCast(item, DateTime).ToString("u")
                ElseIf TypeOf item Is Boolean Then
                    str = CBool(item).ToString.ToLower
                Else
                    str = Escape(item.ToString)
                End If

                If str.Contains(CRLF) Then result.Append(CRLF & Tab)
                result.Append(str.Replace(CRLF, CRLF & Tab) & CRLF)
            Next key

            Return result.ToString
        End Function

        Private Shared Function Escape(ByVal value As String) As String
            value = value.Replace("\", "\\").Replace("#", "\#").Replace(LF, "\n").Remove(CR)
            Return value
        End Function

        Private Shared Function Unescape(ByVal value As String) As String
            value = value.Replace("\#", "#").Replace("\n", LF).Replace("\\", "\")
            Return value
        End Function

        Public Shared Function EscapeWs(ByVal value As String) As String
            If value.StartsWith(" "c) Then value = "\@" & value
            If value.EndsWith(" "c) Then value = value & "\@"
            Return value
        End Function

        Public Shared Function UnescapeWs(ByVal value As String) As String
            If value.StartsWith("\@") Then value = value.Substring(2)
            If value.EndsWith("\@") Then value = value.Substring(0, value.Length - 2)
            Return value
        End Function

        Public Shared Function ParseConfig(ByVal source As String, ByVal context As String, ByVal text As String) _
            As Dictionary(Of String, String)

            If text.Contains("aa.wikibooks") Then
                Dim a As Long = 0
            End If

            Dim result As New Dictionary(Of String, String)
            If text Is Nothing Then Return result

            Try
                Dim lines As List(Of String) = text.Replace(Tab, " ").Remove(CR).ToList(LF)
                Dim items As New List(Of String)
                Dim indent As Integer
                Dim currentItem As New StringBuilder

                For i As Integer = 0 To lines.Count - 1
                    Dim firstNonSpace As Integer = lines(i).Length

                    For j As Integer = 0 To lines(i).Length - 1
                        If lines(i)(j) <> " "c Then firstNonSpace = j : Exit For
                    Next j

                    If Not (firstNonSpace = lines(i).Length OrElse lines(i)(firstNonSpace) = "#") Then
                        Dim commentTest As Integer = lines(i).Remove("\#").IndexOf("#")
                        If commentTest > -1 Then lines(i) = lines(i).Substring(0, commentTest)

                        If lines(i).StartsWith(" ") AndAlso firstNonSpace >= indent Then
                            If indent = 0 Then indent = firstNonSpace
                            currentItem.Append(LF & lines(i).Substring(indent))

                        ElseIf lines(i).Contains(":") Then
                            indent = 0
                            If currentItem.Length > 0 Then items.Add(currentItem.ToString)
                            currentItem = New StringBuilder(lines(i).Trim)
                        End If
                    End If
                Next i

                If currentItem.Length > 0 Then items.Add(currentItem.ToString)

                For Each item As String In items
                    Dim name As String = item.ToFirst(":").Trim
                    Dim value As String = Unescape(item.FromFirst(":").Trim)

                    If result.ContainsKey(name) Then Log.Debug("Duplicate definition of '{0}' in {1} config" _
                        .FormatWith(If(context Is Nothing, name, context & ":" & name), source))

                    result.Merge(name, value)
                Next item

            Catch ex As SystemException
                Throw New ConfigException(Msg("error-config", context, source), ex)
            End Try

            Return result
        End Function

    End Class

    Public Class ConfigException : Inherits ApplicationException

        Public Sub New(ByVal message As String, ByVal innerException As Exception)
            MyBase.New(message, innerException)
        End Sub

    End Class

End Namespace
