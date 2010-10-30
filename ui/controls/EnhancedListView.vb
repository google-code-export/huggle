Imports System.Collections.Generic
Imports System.ComponentModel

Namespace System.Windows.Forms

    Public Class EnhancedListView : Inherits ListView

        Private LastSortColumn As Integer
        Private LastSortOrder As Boolean = True

        Private _LinkColumn As Boolean
        Private _SortMethods As New Dictionary(Of Integer, SortMethod)

        Public Sub New()
            Me.DoubleBuffered = True
            Me.FullRowSelect = True
            Me.GridLines = True
            Me.ShowGroups = False
            Me.View = Forms.View.Details
        End Sub

        <DefaultValue(-1)>
        Public Property FlexibleColumn() As Integer

        <Browsable(False)> _
        Public ReadOnly Property SortMethods() As Dictionary(Of Integer, SortMethod)
            Get
                Return _SortMethods
            End Get
        End Property

        <DefaultValue(False)>
        Public Property SortOnColumnClick() As Boolean

        Public Sub AddRow(ByVal ParamArray cells() As String)
            Items.Add(New ListViewItem(cells) With {.UseItemStyleForSubItems = False})
        End Sub

        Public Sub InsertRow(ByVal index As Integer, ByVal ParamArray cells() As String)
            Items.Insert(0, New ListViewItem(cells))
        End Sub

        Public Sub SortBy(ByVal column As Integer, Optional ByVal descending As Boolean = False, _
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
                    Case SortMethod.String : comparison = Function(x As String, y As String) String.Compare(x, y)
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

            Public Sub New(ByVal column As Integer, ByVal order As Boolean, ByVal comparison As Comparison(Of String))
                Me.Column = column
                Me.Comparison = comparison
                Me.Order = order
            End Sub

            Public Overloads Overrides Function Compare(ByVal x As ListViewItem, ByVal y As ListViewItem) As Integer
                Dim xValue As String = If(x.SubItems.Count > Column, x.SubItems(Column).Text, "")
                Dim yValue As String = If(y.SubItems.Count > Column, y.SubItems(Column).Text, "")

                If Order Then Return Comparison(yValue, xValue) Else Return Comparison(xValue, yValue)
            End Function

        End Class

    End Class

    Public Enum SortMethod As Integer
        : [Custom] : [String] : [Integer] : [Date]
    End Enum

End Namespace
