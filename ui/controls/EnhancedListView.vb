Imports System.Collections.Generic
Imports System.ComponentModel

Namespace System.Windows.Forms

    Friend Class EnhancedListView : Inherits System.Windows.Forms.ListView

        Private LastManualSortOrder As Boolean = True
        Private SortColumn As Integer
        Private SortOrder As Boolean

        Private Rows As List(Of String())
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
        Public Property FlexibleColumn() As Integer

        <Browsable(False)>
        Public ReadOnly Property HasSelectedItem As Boolean
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
            VirtualListViewItems.Add(
            VirtualItems.Sort(New ListViewItemComparer(SortColumn, SortOrder, GetComparerFor(SortColumn)))
            VirtualListSize += 1
            Refresh()
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
            UpdateVirtualListViewItems()
        End Sub

        Protected Overrides Sub OnMouseClick(ByVal e As MouseEventArgs)
            If e.Button = MouseButtons.Right Then

            Else
                MyBase.OnMouseClick(e)
            End If
        End Sub

        Private Sub ColumnMenuItemClicked(ByVal sender As Object, ByVal e As EventArgs)
            Dim index As Integer = CInt(CType(sender, MenuItem).Tag)

            If ColumnVisibility.ContainsKey(index) Then ColumnVisibility(index) = Not ColumnVisibility(index) _
                Else ColumnVisibility(index) = False
        End Sub

        Protected Overrides Sub OnColumnClick(ByVal e As ColumnClickEventArgs)
            If SortOnColumnClick Then
                Dim order As Boolean

                If SortColumn = e.Column Then
                    LastManualSortOrder = Not LastManualSortOrder
                    order = LastManualSortOrder
                End If

                SortBy(e.Column, order)
            End If

            MyBase.OnColumnClick(e)
        End Sub

        Protected Overrides Sub OnRetrieveVirtualItem(ByVal e As RetrieveVirtualItemEventArgs)
            If Rows Is Nothing Then Return
            If e.ItemIndex < 0 OrElse e.ItemIndex >= Rows.Count Then Return
            e.Item = VirtualListViewItems(e.ItemIndex)
            If e.Item Is Nothing Then Return
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

        Private Sub SortBy(ByVal column As Integer, Optional ByVal descending As Boolean = False,
            Optional ByVal comparison As Comparison(Of String) = Nothing)

            If comparison Is Nothing Then comparison = GetComparerFor(column)
            VirtualListViewItems.Sort(New ListViewItemComparer(column, descending, comparison))

            Refresh()
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

                Case SortMethod.Date : Return Function(x As String, y As String) Date.Compare(CDate(x), CDate(y))
                Case SortMethod.String : Return AddressOf String.Compare
                Case Else : Return Nothing
            End Select
        End Function

        Private Sub Initialize()
            Dim columnMenu As New ContextMenu

            For i As Integer = 0 To Columns.Count - 1
                Dim item As New MenuItem(Columns(i).Text) With {.Name = i.ToString}
                AddHandler item.Click, AddressOf ColumnMenuItemClicked
                columnMenu.MenuItems.Add(item)
                ColumnVisibility(i) = True
            Next i

            ContextMenu = columnMenu
        End Sub

        Private Function MakeListViewItem(ByVal row As String()) As ListViewItem
            If Not HideColumns Then Return New ListViewItem(row)

            Dim result As New ListViewItem

            For i As Integer = 0 To Columns.Count - 1
                If ColumnVisibility.ContainsKey(i) Then _
                    If result.Text Is Nothing Then result.Text = row(i) Else result.SubItems.Add(row(i))
            Next i

            Return result
        End Function

        Private Sub UpdateVirtualListViewItems()
            Dim newItems As New List(Of ListViewItem)

            For Each row As String() In Rows
                'Framework bug
                For i As Integer = 0 To row.Length - 1
                    If row(i).Length = 260 Then row(i) &= " "
                Next i

                newItems.Add(New ListViewItem(row) With {.UseItemStyleForSubItems = False})
            Next row

            newItems.Sort(New ListViewItemComparer(SortColumn, SortOrder, GetComparerFor(SortColumn)))
            VirtualListSize = Rows.Count
            Refresh()
        End Sub

        Private Class ListViewItemComparer : Inherits Comparer(Of ListViewItem)

            Private Column As Integer
            Private Comparison As Comparison(Of String)
            Private Order As Boolean

            Public Sub New(ByVal column As Integer, ByVal order As Boolean, ByVal comparison As Comparison(Of String))
                Me.Column = column
                Me.Comparison = comparison
                Me.Order = order
            End Sub

            Public Overloads Overrides Function Compare(ByVal x As ListViewItem, ByVal y As ListViewItem) As Integer
                If x Is Nothing Then Return -1
                If y Is Nothing Then Return 1

                Dim xValue As String = If(x.SubItems.Count > Column, x.SubItems(Column).Text, "")
                Dim yValue As String = If(y.SubItems.Count > Column, y.SubItems(Column).Text, "")

                If Order Then Return Comparison(yValue, xValue) Else Return Comparison(xValue, yValue)
            End Function

        End Class

    End Class

    Friend Enum SortMethod As Integer
        : [Custom] : [String] : [Integer] : [Date]
    End Enum

End Namespace
