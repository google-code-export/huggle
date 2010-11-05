Imports System

Namespace System.Collections.Generic

    Friend Class SortedList(Of T) : Inherits List(Of T)

        'Implements a generic sorted list
        'Differs from System.Collections.Generic.SortedList(Of TKey, TValue)
        'in that it stores a list of objects, not key-value pairs
        'and tolerates changes to items' sort orders

        Private _Comparer As Comparison(Of T)

        Friend Sub New(ByVal comparer As Comparison(Of T))
            _Comparer = comparer
        End Sub

        Friend Overloads Sub Add(ByVal item As T)
            If Not Contains(item) Then

                'Binary insertion sort
                Dim a As Integer = 0, b As Integer = Count, n As Integer

                While a <> b
                    n = CInt(Math.Ceiling((b + a) \ 2))
                    If _Comparer(item, Me(n)) > 0 Then a = n + 1 Else b = n
                End While

                Insert(a, item)
            End If
        End Sub

        Friend Overloads Sub Sort()
            MyBase.Sort(_Comparer)
        End Sub

        Friend Property Comparer() As Comparison(Of T)
            Get
                Return _Comparer
            End Get
            Set(ByVal value As Comparison(Of T))
                _Comparer = value
                Sort()
            End Set
        End Property

    End Class

End Namespace