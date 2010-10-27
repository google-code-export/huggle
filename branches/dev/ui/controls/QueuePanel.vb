Imports Huggle.Actions
Imports System
Imports System.Drawing
Imports System.Drawing.Drawing2D
Imports System.Windows.Forms

Namespace Huggle.UI

    Class QueuePanel

        Private Gfx As BufferedGraphics, CanRender As Boolean
        Private _Mode As QueueMode
        Private _Wiki As Wiki

        Private ReadOnly StringFormat As New StringFormat With {.Trimming = StringTrimming.EllipsisCharacter}

        Private WithEvents _Queue As Queue

        Public Event ItemsChanged As EventHandler(Of Queue, EventArgs)
        Public Event ItemSelected As EventHandler(Of Queue, QueuePanelItemSelectedEventArgs)
        Public Event OptionsClicked As EventHandler(Of QueuePanel, EventArgs)
        Public Event SelectedQueueChanged As EventHandler(Of QueuePanel, EventArgs)

        Public Sub New(ByVal wiki As Wiki)
            InitializeComponent()
            _Wiki = wiki
            Queues.Items.AddRange(wiki.Queues.All.ToArray)

            If wiki.Queues.Default IsNot Nothing AndAlso Queues.Items.Contains(wiki.Queues.Default) _
                Then Queues.SelectedItem = wiki.Queues.Default Else If Queues.Items.Count > 0 Then Queues.SelectedIndex = 0

            If App.Languages.Current IsNot Nothing Then App.Languages.Current.Localize(Me)
        End Sub

        Public Property Mode() As QueueMode
            Get
                Return _Mode
            End Get
            Private Set(ByVal value As QueueMode)
                _Mode = value
            End Set
        End Property

        Public Property Queue() As Queue
            Get
                Return _Queue
            End Get
            Set(ByVal value As Queue)
                If Queue IsNot Nothing Then Queue.Selected = False
                _Queue = value

                If Queue IsNot Nothing AndAlso Queues.Items.Contains(Queue) Then
                    Queue.Selected = True
                    DiffMode.Checked = Queue.Mode = QueueMode.Diff
                    ViewMode.Checked = Queue.Mode = QueueMode.View
                    EditMode.Checked = Queue.Mode = QueueMode.Edit

                    Queues.SelectedItem = Queue
                    Mode = Queue.Mode
                    If Queue.EnableOnSelect Then Queue.Source.Reset()
                End If
            End Set
        End Property

        Public ReadOnly Property Wiki() As Wiki
            Get
                Return _Wiki
            End Get
        End Property

        Private Sub _HandleCreated() Handles Me.HandleCreated
            CanRender = True
        End Sub

        Private Sub _HandleDestroyed() Handles Me.HandleDestroyed
            CanRender = False
            StringFormat.Dispose()
        End Sub

        Private Sub _MouseDown(ByVal s As Object, ByVal e As MouseEventArgs) Handles Me.MouseDown
            If Queue IsNot Nothing Then
                Dim Index As Integer = (e.Y - 2 - Count.Bottom) \ 20 + ScrollBar.Value

                If Index > -1 AndAlso Index < Queue.Items.Count _
                    Then RaiseEvent ItemSelected(Queue, New QueuePanelItemSelectedEventArgs(Queue.Items(Index)))
            End If
        End Sub

        Private Sub _Paint() Handles Me.Paint, ScrollBar.Scroll
            If Queue Is Nothing Then Return
            If Gfx Is Nothing Then Gfx = BufferedGraphicsManager.Current.Allocate(CreateGraphics, DisplayRectangle)
            Dim X, Y As Integer
            Dim Length As Integer = Math.Min(Queue.Items.Count - 1, -1 + (Height - Count.Bottom - 4) \ 20)

            Gfx.Graphics.Clear(Color.FromKnownColor(KnownColor.Control))
            If Queue.Refreshing Then Count.Text = Msg("main-queue-query") _
                Else Count.Text = Msg("main-queue-count", CStr(Queue.Items.Count))

            For i As Integer = 0 To Length
                If ScrollBar.Value + i > Queue.Items.Count - 1 Then
                    ScrollBar.Value -= 1
                    Exit For
                End If

                Dim Item As QueueItem = Queue.Items(i + ScrollBar.Value)

                X = ScrollBar.Left - 20
                Y = (i * 20) + Count.Bottom + 4

                Gfx.Graphics.FillRectangle(Brushes.DarkGray, 2, Y - 1, ScrollBar.Left - 5, 18)

                Using backgroundPen As New Pen(Item.LabelBackColor)
                    Gfx.Graphics.FillRectangle(backgroundPen.Brush, 3, Y, ScrollBar.Left - 7, 16)
                End Using

                Using labelFont As New Font(Font, Item.LabelStyle)
                    Gfx.Graphics.DrawString(Item.Label, labelFont, Brushes.Black, _
                        New Rectangle(4, Y + 1, X - 3, 14), StringFormat)
                End Using

                If Item.Icon IsNot Nothing Then Gfx.Graphics.DrawImageUnscaled(Item.Icon, X, Y)
            Next i

            If CanRender Then Gfx.Render()
        End Sub

        Private Sub _Resize() Handles Me.Resize
            Gfx = BufferedGraphicsManager.Current.Allocate(CreateGraphics, DisplayRectangle)
        End Sub

        Private Sub Add_Click() Handles Add.Click
            'Dim i As Integer = 1

            'While Queue.All.ContainsKey("Queue" & CStr(i))
            '    i += 1
            'End While

            'Queue = New Queue(Wiki, "Queue" & CStr(i))

            'Queues.Items.Add(Queue)
            'Queues.SelectedItem = Queue

            'RaiseEvent OptionsClicked(Me, EventArgs.Empty)
        End Sub

        Private Sub Options_Click() Handles Options.Click
            RaiseEvent OptionsClicked(Me, EventArgs.Empty)
        End Sub

        Private Sub Queue_ItemsChanged() Handles _Queue.ItemsChanged
            RaiseEvent ItemsChanged(Queue, EventArgs.Empty)

            'Preload revisions
            If Queue.Selected AndAlso Queue.Preload Then
                For i As Integer = 0 To Math.Min(Queue.Preloads, Queue.Items.Count) - 1

                    If TypeOf Queue.Items(i) Is Revision Then
                        Dim rev As Revision = CType(Queue.Items(i), Revision)

                        Select Case Mode
                            Case QueueMode.Diff
                                Dim diff As Diff = rev.Wiki.Diffs(rev)

                                If diff IsNot Nothing AndAlso diff.CacheState = CacheState.NotCached Then
                                    diff.CacheState = CacheState.Loading
                                    Dim QuickInfoRequest As New DiffQuery(App.Sessions(rev.Wiki), diff)
                                    QuickInfoRequest.Start()
                                End If

                            Case QueueMode.Edit
                                'If rev.TextCacheState = CacheState.NotCached Then
                                '    Dim RevisionRequest As New RevisionInfoQuery(App.Sessions(rev.Wiki), rev)
                                '    RevisionRequest.Start()
                                'End If

                                'Case QueueMode.View
                                '    If Revision.HtmlState = CacheState.NotCached Then
                                '        Dim HtmlRequest As New Queries.Info.(Revision)
                                '        HtmlRequest.Start()
                                '    End If
                        End Select
                    End If
                Next i
            End If

            CallOnMainThread(AddressOf UpdateItems)
        End Sub

        Private Sub Queues_SelectedIndexChanged() Handles Queues.SelectedIndexChanged
            Queue = CType(Queues.SelectedItem, Queue)
            RaiseEvent SelectedQueueChanged(Me, EventArgs.Empty)
            UpdateItems()
        End Sub

        Public Sub Reset() Handles ResetButton.Click
            If Queue IsNot Nothing Then Queue.Clear()
        End Sub

        Private Sub UpdateItems() Handles Me.SizeChanged
            If Queue IsNot Nothing Then
                Dim QueueHeight As Integer = (Height \ 20) - 2

                If QueueHeight < Queue.Items.Count Then
                    ScrollBar.Visible = True
                    ScrollBar.Maximum = Math.Max(0, Queue.Items.Count - 2)
                    ScrollBar.SmallChange = 1
                    ScrollBar.LargeChange = Math.Max(1, QueueHeight)
                Else
                    ScrollBar.Visible = False
                    ScrollBar.Value = 0
                End If
            End If

            _Paint()
        End Sub

        Private Sub Mode_Click(ByVal sender As Object, ByVal e As System.EventArgs) _
            Handles DiffMode.Click, ViewMode.Click, EditMode.Click

            DiffMode.Checked = (sender Is DiffMode)
            ViewMode.Checked = (sender Is ViewMode)
            EditMode.Checked = (sender Is EditMode)

            If DiffMode.Checked Then Mode = QueueMode.Diff
            If EditMode.Checked Then Mode = QueueMode.Edit
            If ViewMode.Checked Then Mode = QueueMode.View
        End Sub

        Private Sub Enable_Click() Handles Enable.Click

        End Sub

    End Class

    Class QueuePanelItemSelectedEventArgs : Inherits EventArgs

        Private _Item As QueueItem

        Public Sub New(ByVal Item As QueueItem)
            _Item = Item
        End Sub

        Public ReadOnly Property Item() As QueueItem
            Get
                Return _Item
            End Get
        End Property

    End Class

End Namespace