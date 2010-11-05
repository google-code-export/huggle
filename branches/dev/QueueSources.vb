Imports Huggle.Actions
Imports System
Imports System.Collections
Imports System.Collections.Generic
Imports System.Threading

Namespace Huggle

    Friend Module QueueSources

        Private _RecentChanges As RcSource

        Friend Interface IQueueSource

            Event Action As SimpleEventHandler(Of QueueItem)
            Event Resetting As EventHandler
            Event Update As EventHandler

            Sub Reset()
            Sub ForceUpdate()

            Property Enabled() As Boolean

        End Interface

        Friend Class RcSource : Implements IQueueSource

            Friend Event Action As SimpleEventHandler(Of QueueItem) Implements IQueueSource.Action
            Friend Event Resetting As EventHandler Implements IQueueSource.Resetting
            Friend Event Update As EventHandler Implements IQueueSource.Update

            Private Const RequestInterval As Integer = 5000
            Private Const InitialBlockSize As Integer = 500

            Private WithEvents AbuseInfoTimer As New Windows.Forms.Timer
            Private WithEvents AbuseInfoQuery As AbuseInfoQuery
            Private WithEvents Feed As Feed
            Private WithEvents RcTimer As New Windows.Forms.Timer
            Private WithEvents RcQuery As ChangesQuery

            Private FirstQuery As Boolean

            Private _Enabled As Boolean
            Private WithEvents _Wiki As Wiki

            Friend Sub New(ByVal wiki As Wiki)
                _Enabled = True
                _Wiki = wiki
                AbuseInfoTimer.Interval = RequestInterval
                RcTimer.Interval = RequestInterval
                FirstQuery = True
            End Sub

            Friend Property Enabled() As Boolean Implements IQueueSource.Enabled
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

            Friend Property Wiki() As Wiki
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

                    CreateThread(AddressOf RcQuery.Start)
                End If
            End Sub

            Private Sub AbuseInfoQuery_Done() Handles AbuseInfoQuery.Complete
                AbuseInfoTimer.Start()
            End Sub

            Private Sub RcQuery_Done() Handles RcQuery.Complete
                If RcQuery.IsFailed Then App.ShowError(RcQuery.Result)
                RcTimer.Start()
            End Sub

            Friend Sub ForceUpdate() Implements IQueueSource.ForceUpdate
                AbuseLogTimer_Tick()
                RcTimer_Tick()
            End Sub

            Friend Sub Reset() Implements IQueueSource.Reset
            End Sub

        End Class

        Friend Class ListSource : Implements IQueueSource

            Friend Event Action As SimpleEventHandler(Of QueueItem) Implements IQueueSource.Action
            Friend Event Resetting As EventHandler Implements IQueueSource.Resetting
            Friend Event Update As EventHandler Implements IQueueSource.Update

            Private _Enabled As Boolean
            Private PendingItems As New List(Of QueueItem)
            Private WithEvents InfoRequest As PageInfoQuery

            Friend Sub New(ByVal list As List(Of QueueItem))
                _List = list
            End Sub

            Friend Property Enabled() As Boolean Implements IQueueSource.Enabled
                Get
                    Return _Enabled
                End Get
                Set(ByVal value As Boolean)
                    _Enabled = value
                End Set
            End Property

            Friend Property List() As List(Of QueueItem)

            Private Sub InfoRequest_Done() Handles InfoRequest.Complete
                For Each page As Page In PendingItems
                    If page.LastRev IsNot Nothing Then
                        PendingItems.Remove(page)
                        RaiseEvent Action(Me, New EventArgs(Of QueueItem)(page.LastRev))
                    End If
                Next page

                RaiseEvent Update(Me, EventArgs.Empty)

                If PendingItems.Count > 0 Then
                    'InfoRequest = New PageInfoQuery(PendingItems)
                    'InfoRequest.Start()
                End If
            End Sub

            Friend Sub ForceUpdate() Implements IQueueSource.ForceUpdate
            End Sub

            Friend Sub Reset() Implements IQueueSource.Reset
                PendingItems = New List(Of QueueItem)(_List)
                InfoRequest_Done()
            End Sub

        End Class

        Friend Class QuerySource : Implements IQueueSource

            Friend Event Action As SimpleEventHandler(Of QueueItem) Implements IQueueSource.Action
            Friend Event Resetting As EventHandler Implements IQueueSource.Resetting
            Friend Event Update As EventHandler Implements IQueueSource.Update

            Private _Query As String

            Friend Sub New(ByVal query As String)
                _Query = query
            End Sub

            Friend Property Enabled() As Boolean Implements IQueueSource.Enabled

            Friend ReadOnly Property Query() As String
                Get
                    Return _Query
                End Get
            End Property

            Friend Sub ForceUpdate() Implements IQueueSource.ForceUpdate
            End Sub

            Friend Sub Reset() Implements IQueueSource.Reset
            End Sub

        End Class

    End Module

End Namespace