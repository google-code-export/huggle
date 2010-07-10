Imports System.Collections.Generic
Imports System.ComponentModel

Namespace System.Windows.Forms

    Public Class ListViewEx : Inherits ListView

        Private LastSortColumn As Integer
        Private LastSortOrder As Boolean = True

        Private _FlexibleColumn As Integer = -1
        Private _LinkColumn As Boolean
        Private _SortMethods As New Dictionary(Of Integer, String)
        Private _SortOnColumnClick As Boolean

        Public Sub New()
            Me.DoubleBuffered = True
            Me.FullRowSelect = True
            Me.GridLines = True
            Me.ShowGroups = False
            Me.View = Forms.View.Details
        End Sub

        <DefaultValue(-1)> _
        Public Property FlexibleColumn() As Integer
            Get
                Return _FlexibleColumn
            End Get
            Set(ByVal value As Integer)
                _FlexibleColumn = value
            End Set
        End Property

        <Browsable(False)> _
        Public ReadOnly Property SortMethods() As Dictionary(Of Integer, String)
            Get
                Return _SortMethods
            End Get
        End Property

        <DefaultValue(False)> _
        Public Property SortOnColumnClick() As Boolean
            Get
                Return _SortOnColumnClick
            End Get
            Set(ByVal value As Boolean)
                _SortOnColumnClick = value
            End Set
        End Property

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
                    Case "integer" : comparison = Function(x As String, y As String) CInt(x) - CInt(y)
                    Case "date" : comparison = Function(x As String, y As String) Date.Compare(CDate(x), CDate(y))
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
                Dim columnWidths As Integer = 22

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

End Namespace
