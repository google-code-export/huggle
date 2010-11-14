Imports System.Collections.Generic
Imports System.ComponentModel

Namespace System.Windows.Forms

    Friend Class EnhancedListView : Inherits System.Windows.Forms.ListView

        Private ColumnHeaders As List(Of ColumnHeader)
        Private LastManualSortOrder As Boolean = True
        Private Rows As List(Of String())
        Private SortColumn As Integer
        Private SortOrder As Boolean
        Private VirtualListViewItems As List(Of ListViewItem)

        Private _LinkColumn As Boolean
        Private _SortMethods As New Dictionary(Of Integer, SortMethod)
        Private _ColumnVisibility As New Dictionary(Of Integer, Boolean)

        Public Sub New()
            SetStyle(ControlStyles.OptimizedDoubleBuffer Or ControlStyles.AllPaintingInWmPaint, True)
            Me.DoubleBuffered = True
            Me.FullRowSelect = True
            Me.HideSelection = False
            Me.GridLines = True
            Me.ShowGroups = False
            Me.View = Forms.View.Details
            Me.VirtualMode = True
        End Sub

        <Browsable(False)>
        Public ReadOnly Property ColumnVisibility As Dictionary(Of Integer, Boolean)
            Get
                Return _ColumnVisibility
            End Get
        End Property

        <DefaultValue(-1)>
        Public Property FlexibleColumn() As Integer = -1

        <Browsable(False)>
        Public ReadOnly Property HasSelectedItems As Boolean
            Get
                Return (SelectedIndices.Count > 0)
            End Get
        End Property

        <DefaultValue(True)>
        Public Property HideColumns As Boolean = True

        <Browsable(False)>
        Public ReadOnly Property SelectedItem As ListViewItem
            Get
                If SelectedIndices.Count = 0 Then Return Nothing
                Return VirtualListViewItems(SelectedIndices(0))
            End Get
        End Property

        <Browsable(False)>
        Public Property SelectedValue As String
            Get
                If Rows Is Nothing Then Return Nothing
                If SelectedIndices.Count = 0 Then Return Nothing
                Dim item As String() = Rows(SelectedIndices(0))
                Return item(0)
                Return Nothing
            End Get
            Set(ByVal value As String)
                SelectedIndices.Clear()
                If Rows Is Nothing Then Return

                For i As Integer = 0 To Rows.Count - 1
                    If Rows(i)(0) = value Then
                        SelectedIndices.Add(i)
                        VirtualListViewItems(0).EnsureVisible()
                        Exit For
                    End If
                Next i
            End Set
        End Property

        <Browsable(False)>
        Public ReadOnly Property SortMethods() As Dictionary(Of Integer, SortMethod)
            Get
                Return _SortMethods
            End Get
        End Property

        <DefaultValue(False)>
        Public Property SortOnColumnClick() As Boolean

        <Browsable(False)>
        Public Shadows Property VirtualListSize As Integer
            Get
                Return MyBase.VirtualListSize
            End Get
            Set(ByVal value As Integer)
                'Framework bug
                MyBase.VirtualListSize = 0
                MyBase.VirtualListSize = value
            End Set
        End Property

        Public Sub BeginResize()
            Scrollable = False
        End Sub

        Public Sub AddItem(ByVal row As String())
            Rows.Add(row)
            UpdateVirtualList()
        End Sub

        Public Overloads Function Contains(ByVal value As String) As Boolean
            If Rows Is Nothing Then Return False

            For Each item As String() In Rows
                If item(0) = value Then Return True
            Next item

            Return False
        End Function

        Public Sub EndResize()
            Scrollable = True
        End Sub

        Public Sub SetItems(ByVal rows As List(Of String()))
            Me.Rows = rows
            UpdateVirtualList()
        End Sub

        Private Sub ColumnMenuItemClicked(ByVal sender As Object, ByVal e As EventArgs)
            Dim index As Integer = CInt(CType(sender, MenuItem).Name)

            ColumnVisibility(index) = Not ColumnVisibility(index)
            ContextMenu.MenuItems(index).Checked = ColumnVisibility(index)

            UpdateVirtualList()
        End Sub

        Protected Overrides Sub OnColumnClick(ByVal e As ColumnClickEventArgs)
            If SortOnColumnClick Then
                If SortColumn = e.Column Then
                    LastManualSortOrder = Not LastManualSortOrder
                    SortOrder = LastManualSortOrder
                Else
                    SortOrder = False
                    LastManualSortOrder = False
                End If

                SortColumn = e.Column
                UpdateVirtualList()
            End If

            MyBase.OnColumnClick(e)
        End Sub

        Protected Overrides Sub OnResize(ByVal e As EventArgs)
            If FlexibleColumn < 0 Then Return

            Dim columnWidths As Integer = 28

            For i As Integer = 0 To Columns.Count - 1
                If i <> FlexibleColumn Then columnWidths += Columns(i).Width
            Next i

            Dim newWidth As Integer = Width - columnWidths
            SuspendLayout()
            If newWidth >= 40 AndAlso Columns.Count > FlexibleColumn Then Columns(FlexibleColumn).Width = newWidth

            MyBase.OnResize(e)
        End Sub

        Protected Overrides Sub OnRetrieveVirtualItem(ByVal e As RetrieveVirtualItemEventArgs)
            If VirtualListViewItems IsNot Nothing AndAlso e.ItemIndex >= 0 _
                AndAlso e.ItemIndex < VirtualListViewItems.Count Then e.Item = VirtualListViewItems(e.ItemIndex)
            If e.Item Is Nothing Then Throw New Exception("EnhancedListView list not populated.")
        End Sub

        Private Function GetComparerFor(ByVal column As Integer) As Comparison(Of String)
            If Not SortMethods.ContainsKey(column) Then Return AddressOf String.Compare

            Select Case SortMethods(column)
                Case SortMethod.Integer
                    Return Function(x As String, y As String)
                               Dim ix, iy As Integer

                               If Integer.TryParse(x, ix) Then
                                   If Integer.TryParse(y, iy) Then Return ix - iy
                                   Return -1
                               End If

                               Return 1
                           End Function

                Case SortMethod.Date

                    Return _
                        Function(x As String, y As String)
                            Try
                                Return Date.Compare(CDate(x), CDate(y))
                            Catch ex As InvalidCastException
                                Return 0
                            End Try
                        End Function

                Case SortMethod.String : Return AddressOf String.Compare
                Case Else : Return Nothing
            End Select
        End Function

        Private Function MakeListViewItem(ByVal row As String()) As ListViewItem
            If Not HideColumns Then Return New ListViewItem(row) With {.UseItemStyleForSubItems = False}

            Dim items As New List(Of String)

            For i As Integer = 0 To ColumnHeaders.Count - 1
                If Not ColumnVisibility(i) Then Continue For

                'Framework bug
                If row(i) IsNot Nothing AndAlso row(i).Length = 260 Then row(i) &= " "

                items.Add(row(i))
            Next i

            Return New ListViewItem(items.ToArray) With {.UseItemStyleForSubItems = False}
        End Function

        Private Sub UpdateVirtualList()
            If VirtualListViewItems Is Nothing Then
                'Save column headers
                ColumnHeaders = New List(Of ColumnHeader)

                For i As Integer = 0 To Columns.Count - 1
                    ColumnHeaders.Add(Columns(i))
                    If Not ColumnVisibility.ContainsKey(i) Then ColumnVisibility(i) = True
                Next i

                If HideColumns Then
                    ContextMenu = New ContextMenu

                    For i As Integer = 0 To Columns.Count - 1
                        Dim item As New MenuItem(Columns(i).Text) With {.Name = i.ToString}
                        item.Checked = ColumnVisibility(i)
                        AddHandler item.Click, AddressOf ColumnMenuItemClicked
                        ContextMenu.MenuItems.Add(item)
                    Next i
                End If
            End If

            SuspendLayout()
            Columns.Clear()

            Rows.Sort(New RowComparer(SortColumn, SortOrder, GetComparerFor(SortColumn)))

            Dim newItems As New List(Of ListViewItem)

            For i As Integer = 0 To Rows.Count - 1
                newItems.Add(MakeListViewItem(Rows(i)))
            Next i

            For i As Integer = 0 To ColumnHeaders.Count - 1
                If ColumnVisibility(i) Then Columns.Add(ColumnHeaders(i))
            Next i

            VirtualListViewItems = newItems
            VirtualListSize = Rows.Count
            If SelectedIndices.Count > 0 Then VirtualListViewItems(SelectedIndices(0)).EnsureVisible()
            ResumeLayout()
        End Sub

        Private Class RowComparer : Inherits Comparer(Of String())

            Private Column As Integer
            Private Comparison As Comparison(Of String)
            Private Order As Boolean

            Public Sub New(ByVal column As Integer, ByVal order As Boolean, ByVal comparison As Comparison(Of String))
                Me.Column = column
                Me.Comparison = comparison
                Me.Order = order
            End Sub

            Public Overloads Overrides Function Compare(ByVal x As String(), ByVal y As String()) As Integer
                If x Is Nothing Then Return -1
                If y Is Nothing Then Return 1

                Dim xValue As String = If(x.Length > Column, x(Column), "")
                Dim yValue As String = If(y.Length > Column, y(Column), "")

                If Order Then Return Comparison(yValue, xValue) Else Return Comparison(xValue, yValue)
            End Function

        End Class

    End Class

    Friend Enum SortMethod As Integer
        : [Custom] : [String] : [Integer] : [Date]
    End Enum

End Namespace
