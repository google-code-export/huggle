Imports System
Imports System.Collections.Generic
Imports System.Net
Imports System.Text
Imports System.Web.HttpUtility
Imports System.Xml

Namespace Huggle

    'Represents a read or write request made to MediaWiki's API (api.php)

    Public Class ApiRequest : Inherits Request

        Private _Continues As New Dictionary(Of String, Object)
        Private _Items As New List(Of QueueItem)
        Private _NewItems As New List(Of QueueItem)
        Private _LoginResponse As LoginResponse
        Private _LoginToken As String
        Private _Query As QueryString
        Private _Response As XmlDocument
        Private _Strings As New List(Of String)
        Private _Warnings As New List(Of String)

        Private Shared ReadOnly MaxRequests As Integer = 100

        Public Sub New(ByVal session As Session, ByVal description As String, ByVal query As QueryString)
            MyBase.New(session)
            _Limit = session.User.ApiLimit
            _Query = query

            MyBase.Description = description
            Processing = True
        End Sub

        Public ReadOnly Property Continues() As Dictionary(Of String, Object)
            Get
                Return _Continues
            End Get
        End Property

        Public Property Filename() As String

        Public Property IsAtEnd() As Boolean

        Public Property IsSimple() As Boolean

        Public ReadOnly Property Items() As List(Of QueueItem)
            Get
                Return _Items
            End Get
        End Property

        Public Property Limit() As Integer

        Public ReadOnly Property LoginResponse() As LoginResponse
            Get
                Return _LoginResponse
            End Get
        End Property

        Public Shared ReadOnly Property MaxSlowLength() As Integer
            Get
                Return 50
            End Get
        End Property

        Public ReadOnly Property NewItems() As List(Of QueueItem)
            Get
                Return _NewItems
            End Get
        End Property

        Public Property Processing() As Boolean

        Public ReadOnly Property Query() As QueryString
            Get
                Return _Query
            End Get
        End Property

        Public Shadows ReadOnly Property Response() As XmlDocument
            Get
                Return _Response
            End Get
        End Property

        Public ReadOnly Property Strings() As List(Of String)
            Get
                Return _Strings
            End Get
        End Property

        Public ReadOnly Property User() As User
            Get
                Return Session.User
            End Get
        End Property

        Public ReadOnly Property Warnings() As List(Of String)
            Get
                Return _Warnings
            End Get
        End Property

        Public ReadOnly Property Wiki() As Wiki
            Get
                Return Session.User.Wiki
            End Get
        End Property

        Public Overrides Sub Start()
            Url = If(Session.IsSecure, Wiki.SecureUrl, Wiki.Url)
            If Url Is Nothing Then OnFail(Msg("error-wikibadurl", Wiki)) : Return
            Url = New Uri(Url.ToString & "api.php")

            Query.Merge(Continues)
            Query.Add("format", "xml")

            For Each item As Object In Query.Values.Values
                If TypeOf item Is Byte() Then IsMultipart = True
            Next item

            If IsMultipart Then
                Boundary = "----------" & App.Randomness.Next.ToStringI
                Data = Query.ToMultipart(Filename, Boundary)
            Else
                Data = Encoding.UTF8.GetBytes(Query.ToUrlString)
            End If

            Cookies = Session.Cookies

            Dim logString As String = Query.ToString
            If logString.Length > 80 Then logString = logString.Substring(0, 77) & "..."
            Log.Debug("API request: {0}".FormatI(logString))

            MyBase.Start()

            If IsCancelled Then OnFail(Msg("error-cancelled")) : Return

            If Result.IsError Then
                'When the URL is at fault, make it clear which wiki the URL is for
                Select Case Result.Code
                    Case "badurl" : Result = New Result(Msg("error-wikibadurl", Wiki))
                    Case "httperror" : Result.Append(Msg("error-wikinotfound", Wiki, Wiki.Url))
                End Select

                OnFail(Result) : Return
            End If

            Dim responseText As String = Encoding.UTF8.GetString(MyBase.Response.ToArray)
            MyBase.Response.Close()

            'Check for disabled API
            If responseText.StartsWithI("MediaWiki API is not enabled") Then OnFail(Msg("error-apidisabled")) : Return

            'Check for PHP source code output
            If responseText.StartsWithI("<?php") Then OnFail(Msg("error-serverconfig", Wiki)) : Return

            'Check for MediaWiki/Wikimedia error message
            Dim errorCheck As Result = CheckForWikiError(responseText)
            If errorCheck.IsError Then OnFail(errorCheck) : Return

            'Validate XML
            _Response = New XmlDocument

            Try
                Response.LoadXml(responseText)

            Catch ex As XmlException
                'Interpret parse error messages
                If responseText.Contains("<b>Parse error</b>") _
                    Then OnFail(Msg("error-internal", "Parse error")) : Return

                OnFail(Msg("error-invalidxml")) : Return
            End Try

            Continues.Clear()
            ProcessApi(Response.DocumentElement)
            IsAtEnd = (Continues.Count = 0)

            If Not IsFailed Then OnSuccess()
        End Sub

        Public Sub DoMultiple()
            Dim remainingRequests As Integer = MaxRequests
            Continues.Clear()
            Items.Clear()

            Do
                remainingRequests -= 1
                Start()

            Loop Until Result.IsError OrElse (Limit > 0 AndAlso Items.Count >= Limit) _
                OrElse IsAtEnd OrElse remainingRequests = 0

            If Limit > 0 Then
                If Items.Count > Limit Then Items.RemoveRange(Limit - 1, Items.Count - Limit)
                If NewItems.Count > Limit Then NewItems.RemoveRange(Limit - 1, NewItems.Count - Limit)
            End If
        End Sub

    End Class

    <Diagnostics.DebuggerDisplay("{Result}")> _
    Public Class LoginResponse

        Public Sub New()
            Result = "error"
        End Sub

        Public Property Result() As String
        Public Property Token() As String
        Public Property Wait() As TimeSpan

        Public Overrides Function ToString() As String
            Return Result
        End Function

    End Class

End Namespace
