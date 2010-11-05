Imports System.Collections.Generic
Imports System.ComponentModel

Namespace System.Windows.Forms

    Friend Class EnhancedListView : Inherits ListView

        Private LastSortColumn As Integer
        Private LastSortOrder As Boolean = True

        Private _LinkColumn As Boolean
        Private _SortMethods As New Dictionary(Of Integer, SortMethod)

        Friend Sub New()
            Me.DoubleBuffered = True
            Me.FullRowSelect = True
            Me.HideSelection = False
            Me.GridLines = True
            Me.ShowGroups = False
            Me.View = Forms.View.Details
        End Sub

        <DefaultValue(-1)>
        Friend Property FlexibleColumn() As Integer

        <Browsable(False)> _
        Friend ReadOnly Property SortMethods() As Dictionary(Of Integer, SortMethod)
            Get
                Return _SortMethods
            End Get
        End Property

        <DefaultValue(False)>
        Friend Property SortOnColumnClick() As Boolean

        Friend Sub AddRow(ByVal ParamArray cells() As String)
            Items.Add(New ListViewItem(cells) With {.UseItemStyleForSubItems = False})
        End Sub

        Friend Sub InsertRow(ByVal index As Integer, ByVal ParamArray cells() As String)
            Items.Insert(0, New ListViewItem(cells))
        End Sub

        Friend Property SelectedValue As String
            Get
                If SelectedItems.Count = 0 Then Return Nothing
                Return SelectedItems(0).Text
            End Get
            Set(ByVal value As String)
                SelectedIndices.Clear()

                For i As Integer = 0 To Items.Count - 1
                    If Items(i).Text = value Then
                        SelectedIndices.Add(i)
                        SelectedItems(0).EnsureVisible()
                        Exit For
                    End If
                Next i
            End Set
        End Property

        Friend Sub SortBy(ByVal column As Integer, Optional ByVal descending As Boolean = False, _
            Optional ByVal comparison As Comparison(Of String) = Nothing)

            If comparison Is Nothing Then comparison = AddressOf String.Compare

            If SortMethods IsNot Nothing AndAlso SortMethods.ContainsKey(column) Then
                Select Case SortMethods(column)
                    Case SortMethod.Integer : comparison =
                        Function(x As String, y As String)
                            Dim ix, iy As Integer

                            If Integer.TryParse(x, ix) Then
                                If Integer.TryParse(y, iy) Then Return ix - iy
                                Return -1
                            End If

                            Return 1
                        End Function

                    Case SortMethod.Date : comparison = Function(x As String, y As String) Date.Compare(CDate(x), CDate(y))
                    Case SortMethod.String : comparison = Function(x As String, y As String) String.Compare(x, y, StringComparison.Ordinal)
                End Select
            End If

            Dim list As List(Of ListViewItem) = Items.ToList(Of ListViewItem)()
            list.Sort(New ListViewItemComparer(column, descending, comparison))

            BeginUpdate()
            Items.Clear()

            For Each item As ListViewItem In list
                Items.Add(item)
            Next item

            EndUpdate()

            LastSortColumn = column
            LastSortOrder = descending
        End Sub

        Private Sub _ColumnClick(ByVal sender As Object, ByVal e As ColumnClickEventArgs) Handles Me.ColumnClick
            If SortOnColumnClick Then SortBy(e.Column, If(LastSortColumn = e.Column, Not LastSortOrder, False))
        End Sub

        Private Sub _Resize() Handles Me.Resize
            If FlexibleColumn > -1 Then
                Dim columnWidths As Integer = 23

                For i As Integer = 0 To Columns.Count - 1
                    If i <> FlexibleColumn Then columnWidths += Columns(i).Width
                Next i

                Dim newWidth As Integer = Width - columnWidths
                SuspendLayout()
                If newWidth >= 40 AndAlso Columns.Count > FlexibleColumn Then Columns(FlexibleColumn).Width = newWidth
            End If
        End Sub

        Private Class ListViewItemComparer : Inherits Comparer(Of ListViewItem)

            Private Column As Integer
            Private Comparison As Comparison(Of String)
            Private Order As Boolean

            Friend Sub New(ByVal column As Integer, ByVal order As Boolean, ByVal comparison As Comparison(Of String))
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
