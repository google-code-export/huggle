﻿Imports System
Imports System.Collections.Generic
Imports System.Text.RegularExpressions
Imports System.Web.HttpUtility

Namespace Huggle.Queries

    'Abstract class for getting a list of pages through the API

    <Diagnostics.DebuggerDisplay("{Name}")>
    Friend MustInherit Class ListQuery : Inherits Query

        Private _Continues As Dictionary(Of String, Object)
        Private _InputType As String
        Private _IsAtEnd As Boolean
        Private _Result As Result
        Private _Strings As List(Of String)

        Private Initialized As Boolean
        Private Name As String
        Private Prefix As String
        Private Remaining As Integer
        Private Request As ApiRequest

        Private Shared ReadOnly Cache As New Dictionary(Of String, CacheItem)

        Public Event ListProgress(ByVal sender As Object, ByVal e As ListProgressEventArgs)

        Protected Sub New(ByVal session As Session, ByVal inputType As String, ByVal name As String,
            ByVal prefix As String, ByVal query As QueryString, ByVal description As String)

            MyBase.New(session, description)

            _InputType = inputType
            _Limit = User.ApiLimit
            _Query = If(query Is Nothing, New QueryString, query)

            Me.Name = name
            Me.Prefix = prefix
        End Sub

        Public ReadOnly Property CacheKey() As String
            Get
                Return List(Wiki.Code, Prefix, If(From, "")).Join("-")
            End Get
        End Property

        Public ReadOnly Property Continues() As Dictionary(Of String, Object)
            Get
                Return _Continues
            End Get
        End Property

        Public Property ExpectDuplicates() As Boolean

        Public Property From() As String

        Public ReadOnly Property InputType() As String
            Get
                Return _InputType
            End Get
        End Property

        Public ReadOnly Property IsAtEnd() As Boolean
            Get
                Return _IsAtEnd
            End Get
        End Property

        Public Property Items() As List(Of QueueItem)

        Public Property Limit() As Integer

        Public Property OutputType() As String

        Public Property PartialResults() As Boolean

        Protected Property Query() As QueryString

        Public Property Simple() As Boolean

        Protected Property StopAt() As String

        Public ReadOnly Property Strings() As List(Of String)
            Get
                Return _Strings
            End Get
        End Property

        Public Property UseCache() As Boolean

        Public Overrides Sub Start()
            Do
                DoOne()
                If IsFailed Then Return
            Loop Until Remaining <= 0 OrElse IsAtEnd

            OnSuccess()
        End Sub

        Private Sub Initialize()
            Remaining = Limit

            'Check cache
            If UseCache AndAlso Cache.ContainsKey(CacheKey) Then
                Dim cached As CacheItem = Cache(CacheKey)

                If cached.Continues Is Nothing OrElse Limit <= cached.Items.Count Then
                    'If we want fewer than are already in the cache, just return those
                    'If there's no continue, then regardless of how many we want, what's in the cache is all there is
                    Items = cached.Items.GetRange(0, Math.Min(Limit, cached.Items.Count))
                    Request = Nothing
                    Return
                Else
                    'If fewer items are cached than the request wants, we must fetch the rest
                    _Continues = cached.Continues
                    Items.Clear()
                    Items.AddRange(cached.Items)
                    Remaining -= cached.Items.Count
                End If
            End If

            'Prepare query
            Dim actualLimit As Integer = Math.Min(Remaining, If(OutputType = "content", 50, User.ApiLimit))
            Dim limitStr As String = If(ExpectDuplicates OrElse Limit > actualLimit, "max", CStr(actualLimit))

            Dim fullQuery As New QueryString

            Dim generator As Boolean = (OutputType = "info") OrElse _
                (InputType <> "revisions" AndAlso (OutputType = "revisions" OrElse OutputType = "content"))

            fullQuery.Add("action", "query")

            If generator Then
                Dim type As String = If(OutputType = "content", "revisions", OutputType)

                fullQuery.Merge(
                    "generator", Name,
                    "g" & Prefix & "from", From,
                    "g" & Prefix & "limit", limitStr,
                    "prop", type)

                Dim values As New Dictionary(Of String, Object)(Query.Values)
                Query = New QueryString

                For Each item As KeyValuePair(Of String, Object) In values
                    If item.Key.StartsWithI(Prefix) Then Query.Add("g" & item.Key, item.Value) _
                        Else Query.Add(item.Key, item.Value)
                Next item
            Else
                Dim type As String = If(InputType = "revisions", "list", InputType)

                fullQuery.Merge(
                    type, Name,
                    Prefix & "from", From,
                    Prefix & "limit", limitStr)
            End If

            fullQuery.Merge(Query.Values)
            If Continues IsNot Nothing Then fullQuery.Merge(Continues)

            If OutputType = "content" AndAlso fullQuery.Contains("rvprop") _
                Then fullQuery.Add("rvprop", CStr(fullQuery("rvprop")) & "|content")

            'Construct request
            Request = New ApiRequest(Session, Description, fullQuery)
            Request.Limit = Remaining
            Request.IsSimple = Simple
            If OutputType = "pages" Then Request.Processing = False

            Initialized = True
        End Sub

        Public Overridable Sub DoOne()
            If Not Initialized Then Initialize()

            'Make request
            If Request IsNot Nothing Then
                Request.Start()
                If Request.Result.IsError Then OnFail(Request.Result) : Return
                _Continues = Request.Continues

                'Process results
                If Simple Then
                    If Not ExpectDuplicates Then
                        _Strings = Request.Strings
                    Else
                        Strings.Merge(Request.Strings)
                        Request.Strings.Clear()
                    End If
                Else
                    For Each item As QueueItem In Request.Items
                        If TypeOf item Is Revision AndAlso (OutputType = "pages" OrElse OutputType = "info") _
                            Then item = DirectCast(item, Revision).Page

                        If TypeOf item Is Page AndAlso InputType = "revisions" Then Continue For

                        If Not ExpectDuplicates OrElse Not Items.Contains(item) Then
                            Items.Add(item)
                            Remaining -= 1
                        End If
                    Next item

                    Request.Items.Clear()
                End If

                Cache.Merge(CacheKey, New CacheItem(Continues, Items))

                _IsAtEnd = (Request.IsAtEnd OrElse Items.Count >= Limit OrElse _
                    (Not String.IsNullOrEmpty(StopAt) AndAlso Continues.ContainsKey(Prefix & "continue") _
                     AndAlso Continues(Prefix & "continue").ToString >= StopAt))

                If Continues IsNot Nothing AndAlso Request.Items.Count > 0 AndAlso PartialResults _
                    Then OnListProgress(Msg("list-query-progress"), Items)
            Else
                _IsAtEnd = (Items.Count >= Limit)
            End If

            If Items.Count > Limit Then Items = Items.GetRange(0, Limit)
        End Sub

        Protected Sub OnListProgress(ByVal message As String, ByVal partialResult As List(Of QueueItem))
            RaiseEvent ListProgress(Me, New ListProgressEventArgs(Me, message, partialResult))
        End Sub

        Public Shadows Sub SetOption(ByVal name As String, ByVal value As String)
            Select Case name
                Case "cache" : UseCache = value.ToBoolean
                Case "from" : From = value
                Case "limit" : Limit = CInt(value)
                Case "simple" : Simple = value.ToBoolean
                Case "stop" : StopAt = value
                Case "type" : OutputType = value.ToLowerI
            End Select

            CustomOption(name, value)
        End Sub

        Public Sub SetOptions(ByVal options As Dictionary(Of String, String))
            For Each item As KeyValuePair(Of String, String) In options
                SetOption(item.Key, item.Value)
            Next item
        End Sub

        Protected Overridable Sub CustomOption(ByVal name As String, ByVal value As String)
        End Sub

        Public Overrides Function ToString() As String
            Return Name
        End Function

    End Class

    Friend Class ListProgressEventArgs : Inherits EventArgs

        Private _Message As String
        Private _PartialResult As List(Of QueueItem)
        Private _Query As ListQuery

        Public Sub New(ByVal query As ListQuery, ByVal message As String, ByVal partialResult As List(Of QueueItem))
            _Message = message
            _PartialResult = partialResult
            _Query = query
        End Sub

        Public ReadOnly Property Message() As String
            Get
                Return _Message
            End Get
        End Property

        Public ReadOnly Property PartialResult() As List(Of QueueItem)
            Get
                Return _PartialResult
            End Get
        End Property

        Public ReadOnly Property Query As ListQuery
            Get
                Return _Query
            End Get
        End Property

    End Class

    Friend Class CacheItem

        Private _Continues As Dictionary(Of String, Object)
        Private _Items As List(Of QueueItem)

        Public Sub New(ByVal continues As Dictionary(Of String, Object), ByVal items As List(Of QueueItem))
            _Continues = continues
            _Items = items
        End Sub

        Public ReadOnly Property Continues() As Dictionary(Of String, Object)
            Get
                Return _Continues
            End Get
        End Property

        Public ReadOnly Property Items() As List(Of QueueItem)
            Get
                Return _Items
            End Get
        End Property

    End Class

End Namespace