﻿Imports Huggle.Actions
Imports Huggle.Scripting
Imports System
Imports System.Collections.Generic
Imports System.Drawing

Namespace Huggle

    'Represents a page or revision queue

    <Diagnostics.DebuggerDisplay("{Name}")> _
    Public Class Queue

        Private _DefaultMode As QueueMode
        Private _Enabled As Boolean
        Private _EnabledOnSelect As Boolean
        Private _Filter As String
        Private _Items As SortedList(Of QueueItem)
        Private _MaximumAge As TimeSpan
        Private _MaximumSize As Integer
        Private _Mode As QueueMode
        Private _Name As String
        Private _Preload As Boolean
        Private _QueryReAdd As Boolean
        Private _ReEvaluate As Boolean
        Private _Refreshing As Boolean
        Private _RemoveContribs As Boolean
        Private _RemoveHistory As Boolean
        Private _RemoveViewed As Boolean
        Private _Selected As Boolean
        Private _SortOrder As QueueSortOrder
        Private WithEvents _Source As IQueueSource
        Private _Wiki As Wiki

        Private Viewed As New List(Of QueueItem)

        Private Shared WithEvents Timer As New Windows.Forms.Timer

        Public Event ItemsChanged As EventHandler(Of Queue, EventArgs)

        Shared Sub New()
            Timer.Interval = 10000
            Timer.Start()
        End Sub

        Public Sub New(ByVal wiki As Wiki, ByVal name As String)
            _Enabled = True
            _Items = New SortedList(Of QueueItem)(AddressOf CompareByQuality)
            _MaximumSize = 5000
            _Name = name
            _Preload = True
            _ReEvaluate = True
            _RemoveHistory = True
            _RemoveViewed = True
            _SortOrder = QueueSortOrder.Quality
            _Source = wiki.Rc
            _Wiki = wiki
        End Sub

        Public Property DefaultMode() As QueueMode
            Get
                Return _DefaultMode
            End Get
            Set(ByVal value As QueueMode)
                _DefaultMode = value
            End Set
        End Property

        Public Property Enabled() As Boolean
            Get
                Return _Enabled
            End Get
            Set(ByVal value As Boolean)
                _Enabled = value
            End Set
        End Property

        Public Property EnableOnSelect() As Boolean
            Get
                Return _EnabledOnSelect
            End Get
            Set(ByVal value As Boolean)
                _EnabledOnSelect = value
            End Set
        End Property

        Public Property Filter() As String
            Get
                Return _Filter
            End Get
            Set(ByVal value As String)
                _Filter = value
            End Set
        End Property

        Public ReadOnly Property Items() As SortedList(Of QueueItem)
            Get
                Return _Items
            End Get
        End Property

        Public Property MaximumAge() As TimeSpan
            Get
                Return _MaximumAge
            End Get
            Set(ByVal value As TimeSpan)
                _MaximumAge = value
                Timer.Enabled = (value > TimeSpan.Zero)
            End Set
        End Property

        Public Property MaximumSize() As Integer
            Get
                Return _MaximumSize
            End Get
            Set(ByVal value As Integer)
                _MaximumSize = value
            End Set
        End Property

        Public Property Mode() As QueueMode
            Get
                Return _Mode
            End Get
            Set(ByVal value As QueueMode)
                _Mode = value
            End Set
        End Property

        Public Property Name() As String
            Get
                Return _Name
            End Get
            Set(ByVal value As String)
                Wiki.Queues.Rename(Me, value)
                _Name = value
            End Set
        End Property

        Public Property Preload() As Boolean
            Get
                Return _Preload
            End Get
            Set(ByVal value As Boolean)
                _Preload = value
            End Set
        End Property

        Public Shared ReadOnly Property Preloads() As Integer
            Get
                Return 2
            End Get
        End Property

        Public Property QueryReAdd() As Boolean
            Get
                Return _QueryReAdd
            End Get
            Set(ByVal value As Boolean)
                _QueryReAdd = value
            End Set
        End Property

        Public Property ReEvaluate() As Boolean
            Get
                Return _ReEvaluate
            End Get
            Set(ByVal value As Boolean)
                _ReEvaluate = value
            End Set
        End Property

        Public ReadOnly Property Refreshing() As Boolean
            Get
                Return _Refreshing
            End Get
        End Property

        Public Property RemoveContribs() As Boolean
            Get
                Return _RemoveContribs
            End Get
            Set(ByVal value As Boolean)
                _RemoveContribs = value
            End Set
        End Property

        Public Property RemoveHistory() As Boolean
            Get
                Return _RemoveHistory
            End Get
            Set(ByVal value As Boolean)
                _RemoveHistory = value
            End Set
        End Property

        Public Property RemoveViewed() As Boolean
            Get
                Return _RemoveViewed
            End Get
            Set(ByVal value As Boolean)
                _RemoveViewed = value
            End Set
        End Property

        Public Property Selected() As Boolean
            Get
                Return _Selected
            End Get
            Set(ByVal value As Boolean)
                _Selected = value
            End Set
        End Property

        Public Property SortOrder() As QueueSortOrder
            Get
                Return _SortOrder
            End Get
            Set(ByVal value As QueueSortOrder)
                _SortOrder = value

                Select Case SortOrder
                    Case QueueSortOrder.Quality : Items.Comparer = AddressOf CompareByQuality
                    Case QueueSortOrder.Time : Items.Comparer = AddressOf CompareByTime
                End Select
            End Set
        End Property

        Public Property Source() As IQueueSource
            Get
                Return _Source
            End Get
            Set(ByVal value As IQueueSource)
                _Source = value
                If Source IsNot Nothing Then _Source.Reset()
            End Set
        End Property

        Public ReadOnly Property SourceType() As QueueSourceType
            Get
                If TypeOf Source Is RcSource Then
                    Return QueueSourceType.Rc
                ElseIf TypeOf Source Is ListSource Then
                    Return QueueSourceType.List
                ElseIf TypeOf Source Is QuerySource Then
                    Return QueueSourceType.Query
                End If

                Return Nothing
            End Get
        End Property

        Public ReadOnly Property Wiki() As Wiki
            Get
                Return _Wiki
            End Get
        End Property

        Public Sub Clear()
            Items.Clear()
            If Source IsNot Nothing Then Source.Reset()
            CreateThread(AddressOf OnItemsChanged)
        End Sub

        Private Sub EditStateChanged(ByVal rev As Revision, ByVal e As EventArgs)
            EvaluateItem(rev, False)
        End Sub

        Private Sub EvaluateItem(ByVal item As QueueItem, ByVal isNew As Boolean)
            If Viewed.Contains(item) AndAlso Not QueryReAdd Then Return

            Dim match As Boolean, infoNeeded As New List(Of BatchInfo)

            'Filter check
            If Filter Is Nothing Then
                match = True
            Else
                Dim Evaluator As New Evaluator(App.Sessions(Wiki), "Filter: " & Name, Filter)
                Evaluator.ReadOnly = True

                Try
                    Evaluator.Context = item
                    Evaluator.Start()
                    Dim result As Object = Evaluator.Value.Value
                    infoNeeded = Evaluator.InfoNeeded
                    match = (result IsNot Nothing AndAlso TypeOf result Is Boolean AndAlso CBool(result))

                Catch ex As ScriptException
                    Log.Write(ex.Message)
                End Try
            End If

            'Request extra info
            If Not match AndAlso infoNeeded.Count > 0 Then
                For Each info As BatchInfo In infoNeeded
                    'BatchQuery.CurrentFor(info.Wiki).Add(info)
                Next info
            End If

            'Avoid re-adding old revisions
            If TypeOf item Is Revision Then
                Dim rev As Revision = CType(item, Revision)
                If Not rev.IsTop AndAlso RemoveHistory Then match = False
                If rev.User IsNot Nothing AndAlso RemoveContribs AndAlso Not (rev Is rev.User.LastEdit) _
                    Then match = False
            End If

            If match AndAlso Not Items.Contains(item) Then
                If SortOrder = QueueSortOrder.Time Then Items.Insert(0, item) Else Items.Add(item)
                If Items.Count > MaximumSize Then Items.RemoveAt(MaximumSize)

            ElseIf match Then
                Items.Remove(item)
                Items.Add(item)

            ElseIf Not match AndAlso Not isNew AndAlso Items.Contains(item) Then
                Items.Remove(item)
            End If
        End Sub

        Private Sub Source_Action(ByVal sender As Object, ByVal e As QueueItem) Handles _Source.Action
            If Enabled AndAlso (Selected OrElse Not EnableOnSelect) Then
                If TypeOf e Is Revision Then
                    Dim rev As Revision = CType(e, Revision)

                    If ReEvaluate Then AddHandler rev.StateChanged, AddressOf EditStateChanged

                    If RemoveHistory AndAlso rev.Prev IsNot Nothing _
                        AndAlso Items.Contains(rev.Prev) Then Items.Remove(rev.Prev)

                    If RemoveContribs AndAlso rev.PrevByUser IsNot Nothing _
                        AndAlso Items.Contains(rev.PrevByUser) Then Items.Remove(rev.PrevByUser)
                End If

                EvaluateItem(e, True)
            End If
        End Sub

        Private Sub Source_Resetting() Handles _Source.Resetting
            Items.Clear()
            CreateThread(AddressOf OnItemsChanged)
        End Sub

        Private Sub Source_Update() Handles _Source.Update
            CreateThread(AddressOf OnItemsChanged)
        End Sub

        Private Sub OnItemsChanged()
            RaiseEvent ItemsChanged(Me, EventArgs.Empty)
        End Sub

        Public Overrides Function ToString() As String
            Return _Name
        End Function

        Public Sub View(ByVal Item As QueueItem)
            If Items.Contains(Item) Then
                Viewed.Add(Item)

                If RemoveViewed Then
                    Items.Remove(Item)
                    CreateThread(AddressOf OnItemsChanged)
                End If
            End If
        End Sub

        Private Shared Sub Timer_Tick() Handles Timer.Tick
            For Each wiki As Wiki In App.Wikis.All
                For Each queue As Queue In wiki.Queues.All
                    If queue.MaximumAge > TimeSpan.Zero Then
                        Dim i As Integer = 0

                        While i < queue.Items.Count - 1
                            Dim Item As QueueItem = queue.Items(i)

                            If TypeOf Item Is Revision AndAlso CType(Item, Revision).Time.Add(queue.MaximumAge) < wiki.ServerTime _
                                OrElse TypeOf Item Is LogItem AndAlso CType(Item, LogItem).Time.Add(queue.MaximumAge) < wiki.ServerTime _
                                Then queue.Items.RemoveAt(i) _
                                Else i += 1
                        End While
                    End If
                Next queue
            Next wiki
        End Sub

        Private Function CompareByQuality(ByVal x As QueueItem, ByVal y As QueueItem) As Integer
            If TypeOf x Is Revision AndAlso TypeOf y Is Revision _
                Then Return Revision.CompareByQuality(CType(x, Revision), CType(y, Revision))

            If TypeOf x Is Revision Then Return -1
            If TypeOf y Is Revision Then Return 1

            Return CompareByTime(x, y)
        End Function

        Private Function CompareByTime(ByVal x As QueueItem, ByVal y As QueueItem) As Integer
            If TypeOf x Is Revision AndAlso TypeOf y Is Revision _
                Then Return Date.Compare(CType(x, Revision).Time, CType(y, Revision).Time)
            Return 0
        End Function

    End Class

    Public Class QueueCollection

        Private Wiki As Wiki
        Private ReadOnly _All As New Dictionary(Of String, Queue)
        Private _Default As Queue

        Public Sub New(ByVal wiki As Wiki)
            Me.Wiki = wiki
        End Sub

        Public ReadOnly Property All() As IList(Of Queue)
            Get
                Return _All.Values.ToList.AsReadOnly
            End Get
        End Property

        Public Property [Default]() As Queue
            Get
                Return _Default
            End Get
            Set(ByVal value As Queue)
                _Default = value
            End Set
        End Property

        Default Public ReadOnly Property Item(ByVal name As String) As Queue
            Get
                If Not _All.ContainsKey(name) Then _All.Add(name, New Queue(Wiki, name))
                Return _All(name)
            End Get
        End Property

        Public Sub Rename(ByVal queue As Queue, ByVal newName As String)
            _All.Unmerge(queue.Name)
            _All.Merge(newName, queue)
        End Sub

    End Class

    Public Enum QueueFilterMatch As Integer
        : Require : Exclude
    End Enum

    Public Enum QueueMode As Integer
        : Diff : View : Edit
    End Enum

    Public Enum QueueSortOrder As Integer
        : Time : Quality
    End Enum

    Public Enum QueueSourceType As Integer
        : Rc : Query : List
    End Enum

End Namespace