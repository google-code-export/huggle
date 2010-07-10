Imports System
Imports System.Collections.Generic
Imports System.Text.RegularExpressions

Namespace Huggle

    'Represents an automated or assisted editing tool

    <Diagnostics.DebuggerDisplay("{Name}")> _
    Public Class Tool

        Private _Description, _Link, _Name, _Type As String, _Pattern As Regex

        Private Shared ReadOnly _All As New List(Of Tool)

        Private Sub New(ByVal Name As String)
            _Name = Name
            _All.Merge(Me)
        End Sub

        Public Property Description() As String
            Get
                Return _Description
            End Get
            Set(ByVal value As String)
                _Description = value
            End Set
        End Property

        Public Property Link() As String
            Get
                Return _Link
            End Get
            Set(ByVal value As String)
                _Link = value
            End Set
        End Property

        Public Property Name() As String
            Get
                Return _Name
            End Get
            Set(ByVal value As String)
                _Name = value
            End Set
        End Property

        Public Property Pattern() As Regex
            Get
                Return _Pattern
            End Get
            Set(ByVal value As Regex)
                _Pattern = value
            End Set
        End Property

        Public Property Type() As String
            Get
                Return _Type
            End Get
            Set(ByVal value As String)
                _Type = value
            End Set
        End Property

        Public Overrides Function ToString() As String
            Return Name
        End Function

        Public Shared ReadOnly Property All() As List(Of Tool)
            Get
                Return _All
            End Get
        End Property

        Public Shared Function FromName(ByVal Name As String) As Tool
            For Each Item As Tool In All
                If Item.Name = Name Then Return Item
            Next Item

            Return New Tool(Name)
        End Function

        Public Shared Sub ResetState()
            _All.Clear()
        End Sub

    End Class

End Namespace
