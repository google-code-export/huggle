Imports System

Namespace Huggle

    <Diagnostics.DebuggerDisplay("{ToString()}")>
    Public Structure Ternary

        Private State As States

        Private Shared ReadOnly _False As New Ternary With {.State = States.FALSE}
        Private Shared ReadOnly _True As New Ternary With {.State = States.TRUE}
        Private Shared ReadOnly _Undefined As New Ternary With {.State = States.UNDEFINED}

        Public Shared ReadOnly Property [False]() As Ternary
            Get
                Return _False
            End Get
        End Property

        Public Shared ReadOnly Property [True]() As Ternary
            Get
                Return _True
            End Get
        End Property

        Public Shared ReadOnly Property Undefined() As Ternary
            Get
                Return _Undefined
            End Get
        End Property

        Public Shared Operator =(ByVal first As Ternary, ByVal second As Ternary) As Boolean
            Return first.State = second.State
        End Operator

        Public Shared Operator <>(ByVal first As Ternary, ByVal second As Ternary) As Boolean
            Return first.State <> second.State
        End Operator

        Public Shared Operator IsFalse(ByVal value As Ternary) As Boolean
            Return value.State = States.FALSE
        End Operator

        Public Shared Operator IsTrue(ByVal value As Ternary) As Boolean
            Return value.State = States.TRUE
        End Operator

        Public Shared Operator And(ByVal first As Ternary, ByVal second As Ternary) As Ternary
            If first.IsUndefined OrElse second.IsUndefined Then Return Ternary.Undefined
            Return first.IsTrue AndAlso second.IsTrue
        End Operator

        Public Shared Operator Or(ByVal first As Ternary, ByVal second As Ternary) As Ternary
            If first.IsUndefined OrElse second.IsUndefined Then Return Ternary.Undefined
            Return first.IsTrue OrElse second.IsTrue
        End Operator

        Public Shared Operator Xor(ByVal first As Ternary, ByVal second As Ternary) As Ternary
            If first.IsUndefined OrElse second.IsUndefined Then Return Ternary.Undefined
            Return first.IsTrue Xor second.IsTrue
        End Operator

        Public Shared Operator Not(ByVal value As Ternary) As Boolean
            If value.IsTrue Then Return False
            If value.IsFalse Then Return True
            Return False
        End Operator

        Public Shared Function [And](ByVal first As Ternary, ByVal second As Ternary) As Ternary
            Return first And second
        End Function

        Public Shared Function [Or](ByVal first As Ternary, ByVal second As Ternary) As Ternary
            Return first Or second
        End Function

        Public Shared Function [Xor](ByVal first As Ternary, ByVal second As Ternary) As Ternary
            Return first Xor second
        End Function

        Public Shared Function [Not](ByVal value As Ternary) As Boolean
            Return Not value
        End Function

        Public ReadOnly Property IsFalse() As Boolean
            Get
                Return State = States.FALSE
            End Get
        End Property

        Public ReadOnly Property IsTrue() As Boolean
            Get
                Return State = 2
            End Get
        End Property

        Public ReadOnly Property IsUndefined() As Boolean
            Get
                Return State = 0
            End Get
        End Property

        Public Overrides Function Equals(ByVal obj As Object) As Boolean
            Return (TypeOf obj Is Ternary AndAlso CType(obj, Ternary).State = State)
        End Function

        Public Overrides Function GetHashCode() As Integer
            Return State.GetHashCode
        End Function

        Public Overrides Function ToString() As String
            Select Case State
                Case States.FALSE : Return Boolean.FalseString
                Case States.TRUE : Return Boolean.TrueString
                Case Else : Return "Undefined"
            End Select
        End Function

        Public Shared Widening Operator CType(ByVal value As Boolean) As Ternary
            Return If(value, Ternary.True, Ternary.False)
        End Operator

        Public Shared Widening Operator CType(ByVal value As Boolean?) As Ternary
            If value Is Nothing Then Return Ternary.Undefined
            Return If(value.Value, Ternary.True, Ternary.False)
        End Operator

        Public Shared Widening Operator CType(ByVal value As Ternary) As Boolean
            If value.IsTrue Then Return True
            If value.IsFalse Then Return False
            Return False
        End Operator

        Public Shared Widening Operator CType(ByVal value As Ternary) As Boolean?
            If value.IsTrue Then Return True
            If value.IsFalse Then Return False
            Return Nothing
        End Operator

        Private Enum States As Integer
            UNDEFINED = 0
            [TRUE] = 1
            [FALSE] = 2
        End Enum

    End Structure

    Public Module TernaryFunctions

        Public ReadOnly Property Undefined As Ternary
            Get
                Return Ternary.Undefined
            End Get
        End Property

    End Module

End Namespace