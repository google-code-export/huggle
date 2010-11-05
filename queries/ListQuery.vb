Imports System
Imports System.Collections.Generic
Imports System.Text.RegularExpressions
Imports System.Web.HttpUtility

Namespace Huggle.Actions

    'Abstract class for getting a list of pages through the API

    Friend MustInherit Class ListQuery : Inherits Query

        Private _Continues As Dictionary(Of String, Object)
        Private _ExpectDuplicates As Boolean
        Private _From As String
        Private _InputType As String
        Private _IsAtEnd As Boolean
        Private _Items As List(Of QueueItem)
        Private _Limit As Integer
        Private _OutputType As String
        Private _PartialResults As Boolean
        Private _Query As QueryString
        Private _Result As Result
        Private _Simple As Boolean
        Private _StopAt As String
        Private _Strings As List(Of String)
        Private _UseCache As Boolean

        Private Initialized As Boolean
        Private Name As String
        Private Prefix As String
        Private Remaining As Integer
        Private Request As ApiRequest

        Private Shared ReadOnly Cache As New Dictionary(Of String, CacheItem)

        Friend Event ListProgress(ByVal sender As Object, ByVal e As ListProgressEventArgs)

        Protected Sub New(ByVal session As Session, ByVal inputType As String, ByVal name As String, _
            ByVal prefix As String, ByVal query As QueryString, ByVal description As String)

            MyBase.New(session, description)

            _InputType = inputType
            _Limit = user.ApiLimit
            _Query = If(query Is Nothing, New QueryString, query)

            Me.Name = name
            Me.Prefix = prefix
        End Sub

        Friend ReadOnly Property CacheKey() As String
            Get
                Return List(Wiki.Code, Prefix, If(From, "")).Join("-")
            End Get
        End Property

        Friend ReadOnly Property Continues() As Dictionary(Of String, Object)
            Get
                Return _Continues
            End Get
        End Property

        Friend Property ExpectDuplicates() As Boolean
            Get
                Return _ExpectDuplicates
            End Get
            Set(ByVal value As Boolean)
                _ExpectDuplicates = value
            End Set
        End Property

        Friend Property From() As String
            Get
                Return _From
            End Get
            Set(ByVal value As String)
                _From = value
            End Set
        End Property

        Friend ReadOnly Property InputType() As String
            Get
                Return _InputType
            End Get
        End Property

        Friend Property IsAtEnd() As Boolean
            Get
                Return _IsAtEnd
            End Get
            Private Set(ByVal value As Boolean)
                _IsAtEnd = value
            End Set
        End Property

        Friend Property Items() As List(Of QueueItem)
            Get
                Return _Items
            End Get
            Set(ByVal value As List(Of QueueItem))
                _Items = value
            End Set
        End Property

        Friend Property Limit() As Integer
            Get
                Return _Limit
            End Get
            Set(ByVal value As Integer)
                _Limit = value
            End Set
        End Property

        Friend Property OutputType() As String
            Get
                Return _OutputType
            End Get
            Set(ByVal value As String)
                _OutputType = value
            End Set
        End Property

        Friend Property PartialResults() As Boolean
            Get
                Return _PartialResults
            End Get
            Set(ByVal value As Boolean)
                _PartialResults = value
            End Set
        End Property

        Protected Property Query() As QueryString
            Get
                Return _Query
            End Get
            Set(ByVal value As QueryString)
                _Query = value
            End Set
        End Property

        Friend Property Simple() As Boolean
            Get
                Return _Simple
            End Get
            Set(ByVal value As Boolean)
                _Simple = value
            End Set
        End Property

        Protected Property StopAt() As String
            Get
                Return _StopAt
            End Get
            Set(ByVal value As String)
                _StopAt = value
            End Set
        End Property

        Friend Property Strings() As List(Of String)
            Get
                Return _Strings
            End Get
            Private Set(ByVal value As List(Of String))
                _Strings = value
            End Set
        End Property

        Friend Property UseCache() As Boolean
            Get
                Return _UseCache
            End Get
            Set(ByVal value As Boolean)
                _UseCache = value
            End Set
        End Property

        Friend Overrides Sub Start()
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

                fullQuery.Merge(New Object() { _
                    "generator", Name, _
                    "g" & Prefix & "from", From, _
                    "g" & Prefix & "limit", limitStr, _
                    "prop", type}.ToDictionary(Of String, Object))

                Dim values As New Dictionary(Of String, Object)(Query.Values)
                Query = New QueryString

                For Each item As KeyValuePair(Of String, Object) In values
                    If item.Key.StartsWithI(Prefix) Then Query.Add("g" & item.Key, item.Value) _
                        Else Query.Add(item.Key, item.Value)
                Next item
            Else
                Dim type As String = If(InputType = "revisions", "list", InputType)

                fullQuery.Merge( _
                    type, Name, _
                    Prefix & "from", From, _
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

        Friend Overridable Sub DoOne()
            If Not Initialized Then Initialize()

            'Make request
            If Request IsNot Nothing Then
                Request.Start()
                If Request.Result.IsError Then OnFail(Request.Result) : Return
                _Continues = Request.Continues

                'Process results
                If Simple Then
                    If Not ExpectDuplicates Then
                        Strings = Request.Strings
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

                IsAtEnd = (Request.IsAtEnd OrElse Items.Count >= Limit OrElse _
                    (Not String.IsNullOrEmpty(StopAt) AndAlso Continues.ContainsKey(Prefix & "continue") _
                     AndAlso Continues(Prefix & "continue").ToString >= StopAt))

                If Continues IsNot Nothing AndAlso Request.Items.Count > 0 AndAlso PartialResults _
                    Then OnListProgress(Msg("list-query-progress"), Items)
            Else
                IsAtEnd = (Items.Count >= Limit)
            End If

            If Items.Count > Limit Then Items = Items.GetRange(0, Limit)
        End Sub

        Protected Sub OnListProgress(ByVal message As String, ByVal partialResult As List(Of QueueItem))
            RaiseEvent ListProgress(Me, New ListProgressEventArgs(Me, message, partialResult))
        End Sub

        Friend Sub SetOptions(ByVal options As Dictionary(Of String, String))

            For Each item As KeyValuePair(Of String, String) In options
                Select Case item.Key
                    Case "cache" : UseCache = item.Value.ToBoolean
                    Case "from" : From = item.Value
                    Case "limit" : Limit = CInt(item.Value)
                    Case "simple" : Simple = item.Value.ToBoolean
                    Case "stop" : StopAt = item.Value
                    Case "type" : OutputType = item.Value.ToLowerI
                End Select

                SetOption(item.Key, item.Value)
            Next item
        End Sub

        Protected Overridable Sub SetOption(ByVal name As String, ByVal value As String)
        End Sub

    End Class

    Friend Class ListProgressEventArgs : Inherits EventArgs

        Private _Message As String
        Private _PartialResult As List(Of QueueItem)
        Private _Query As ListQuery

        Friend Sub New(ByVal query As ListQuery, ByVal message As String, ByVal partialResult As List(Of QueueItem))
            _Message = message
            _PartialResult = partialResult
            _Query = query
        End Sub

        Friend ReadOnly Property Message() As String
            Get
                Return _Message
            End Get
        End Property

        Friend ReadOnly Property PartialResult() As List(Of QueueItem)
            Get
                Return _PartialResult
            End Get
        End Property

        Friend ReadOnly Property Query As ListQuery
            Get
                Return _Query
            End Get
        End Property

    End Class

    Friend Class CacheItem

        Private _Continues As Dictionary(Of String, Object)
        Private _Items As List(Of QueueItem)

        Friend Sub New(ByVal continues As Dictionary(Of String, Object), ByVal items As List(Of QueueItem))
            _Continues = continues
            _Items = items
        End Sub

        Friend Property Continues() As Dictionary(Of String, Object)
            Get
                Return _Continues
            End Get
            Private Set(ByVal value As Dictionary(Of String, Object))
                _Continues = value
            End Set
        End Property

        Friend Property Items() As List(Of QueueItem)
            Get
                Return _Items
            End Get
            Private Set(ByVal value As List(Of QueueItem))
                _Items = value
            End Set
        End Property

    End Class

End Namespace