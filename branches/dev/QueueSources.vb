Imports Huggle.Actions
Imports System
Imports System.Collections
Imports System.Collections.Generic
Imports System.Threading

Namespace Huggle

    Public Module QueueSources

        Private _RecentChanges As RcSource

        Public Interface IQueueSource

            Event Action As EventHandler(Of QueueItem)
            Event Resetting As EventHandler
            Event Update As EventHandler

            Sub Reset()
            Sub ForceUpdate()

            Property Enabled() As Boolean

        End Interface

        Public Class RcSource : Implements IQueueSource

            Public Event Action As EventHandler(Of QueueItem) Implements IQueueSource.Action
            Public Event Resetting As EventHandler Implements IQueueSource.Resetting
            Public Event Update As EventHandler Implements IQueueSource.Update

            Private Shared ReadOnly RequestInterval As Integer = 5000
            Private Shared ReadOnly InitialBlockSize As Integer = 500

            Private WithEvents AbuseInfoTimer As New Windows.Forms.Timer
            Private WithEvents AbuseInfoQuery As AbuseInfoQuery
            Private WithEvents Feed As Feed
            Private WithEvents RcTimer As New Windows.Forms.Timer
            Private WithEvents RcQuery As ChangesQuery

            Private FirstQuery As Boolean

            Private _Enabled As Boolean
            Private WithEvents _Wiki As Wiki

            Public Sub New(ByVal wiki As Wiki)
                _Enabled = True
                _Wiki = wiki
                AbuseInfoTimer.Interval = RequestInterval
                RcTimer.Interval = RequestInterval
                FirstQuery = True
            End Sub

            Public Property Enabled() As Boolean Implements IQueueSource.Enabled
                Get
                    Return _Enabled
                End Get
                Set(ByVal value As Boolean)
                    _Enabled = value
                    RcTimer.Enabled = value
                    AbuseInfoTimer.Enabled = value
                End Set
            End Property

            'Private Sub _Action(ByVal sender As Object, ByVal item As QueueItem) Handles Feed.Action, RcQuery.Action
            '    If item.Wiki Is Wiki AndAlso Not Wiki.RecentChanges.ContainsKey(item.Key) Then
            '        Wiki.RecentChanges.Add(item.Key, item)

            '        RaiseEvent Action(Me, item)
            '        RaiseEvent Update(Me, EventArgs.Empty)
            '    End If
            'End Sub

            Private ReadOnly Property User() As User
                Get
                    Return App.Sessions(Wiki).User
                End Get
            End Property

            Public Property Wiki() As Wiki
                Get
                    Return _Wiki
                End Get
                Set(ByVal value As Wiki)
                    _Wiki = value
                End Set
            End Property

            Private Sub AbuseLogTimer_Tick() Handles AbuseInfoTimer.Tick
                'TODO: Remove this if abuse log is made available through RC feed
                If Wiki.Family IsNot Nothing AndAlso Wiki.Family.Feed IsNot Nothing AndAlso Wiki.Family.Feed.Available _
                    AndAlso Enabled AndAlso User.HasRight("abuselog-detail") Then

                    AbuseInfoTimer.Stop()
                    AbuseInfoQuery = New AbuseInfoQuery(App.Sessions(Wiki))
                    AbuseInfoQuery.Start()
                End If
            End Sub

            Private Sub RcTimer_Tick() Handles RcTimer.Tick
                If Wiki.Family IsNot Nothing Then Feed = Wiki.Family.Feed

                If Enabled AndAlso (Feed Is Nothing OrElse Not Feed.Available) AndAlso User.HasRight("read") Then
                    RcTimer.Stop()
                    RcQuery = New ChangesQuery(App.Sessions(Wiki))

                    If FirstQuery Then
                        RcQuery.Limit = InitialBlockSize
                        FirstQuery = False
                    End If

                    App.Start(AddressOf RcQuery.Start)
                End If
            End Sub

            Private Sub AbuseInfoQuery_Done() Handles AbuseInfoQuery.Complete
                AbuseInfoTimer.Start()
            End Sub

            Private Sub RcQuery_Done() Handles RcQuery.Complete
                If RcQuery.IsFailed Then App.ShowError(RcQuery.Result)
                RcTimer.Start()
            End Sub

            Public Sub ForceUpdate() Implements IQueueSource.ForceUpdate
                AbuseLogTimer_Tick()
                RcTimer_Tick()
            End Sub

            Public Sub Reset() Implements IQueueSource.Reset
            End Sub

        End Class

        Public Class ListSource : Implements IQueueSource

            Public Event Action As EventHandler(Of QueueItem) Implements IQueueSource.Action
            Public Event Resetting As EventHandler Implements IQueueSource.Resetting
            Public Event Update As EventHandler Implements IQueueSource.Update

            Private _Enabled As Boolean
            Private _List As List(Of QueueItem)
            Private PendingItems As New List(Of QueueItem)
            Private WithEvents InfoRequest As PageInfoQuery

            Public Sub New(ByVal list As List(Of QueueItem))
                _List = list
            End Sub

            Public Property Enabled() As Boolean Implements IQueueSource.Enabled
                Get
                    Return _Enabled
                End Get
                Set(ByVal value As Boolean)
                    _Enabled = value
                End Set
            End Property

            Public Property List() As List(Of QueueItem)
                Get
                    Return _List
                End Get
                Set(ByVal value As List(Of QueueItem))
                    _List = value
                End Set
            End Property

            Private Sub InfoRequest_Done() Handles InfoRequest.Complete
                For Each Item As Page In PendingItems
                    If Item.LastRev IsNot Nothing Then
                        PendingItems.Remove(Item)
                        RaiseEvent Action(Nothing, Item.LastRev)
                    End If
                Next Item

                RaiseEvent Update(Me, EventArgs.Empty)

                If PendingItems.Count > 0 Then
                    'InfoRequest = New PageInfoQuery(PendingItems)
                    'InfoRequest.Start()
                End If
            End Sub

            Public Sub ForceUpdate() Implements IQueueSource.ForceUpdate
            End Sub

            Public Sub Reset() Implements IQueueSource.Reset
                PendingItems = New List(Of QueueItem)(_List)
                InfoRequest_Done()
            End Sub

        End Class

        Public Class QuerySource : Implements IQueueSource

            Public Event Action As EventHandler(Of QueueItem) Implements IQueueSource.Action
            Public Event Resetting As EventHandler Implements IQueueSource.Resetting
            Public Event Update As EventHandler Implements IQueueSource.Update

            Private _Enabled As Boolean
            Private _Query As String

            Public Sub New(ByVal query As String)
                _Query = query
            End Sub

            Public Property Enabled() As Boolean Implements IQueueSource.Enabled
                Get
                    Return _Enabled
                End Get
                Set(ByVal value As Boolean)
                    _Enabled = value
                End Set
            End Property

            Public ReadOnly Property Query() As String
                Get
                    Return _Query
                End Get
            End Property

            Public Sub ForceUpdate() Implements IQueueSource.ForceUpdate
            End Sub

            Public Sub Reset() Implements IQueueSource.Reset
            End Sub

        End Class

    End Module

End Namespace