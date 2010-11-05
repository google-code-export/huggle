Imports System
Imports System.Collections.Generic
Imports System.Text.RegularExpressions

Namespace Huggle

    'Represents an automated or assisted editing tool

    <Diagnostics.DebuggerDisplay("{Name}")>
    Friend Class Tool

        Private Shared ReadOnly _All As New List(Of Tool)

        Private Sub New(ByVal Name As String)
            _Name = Name
            _All.Merge(Me)
        End Sub

        Friend Property Description() As String

        Friend Property Link() As String

        Friend Property Name() As String

        Friend Property Pattern() As Regex

        Friend Property Type() As String

        Public Overrides Function ToString() As String
            Return Name
        End Function

        Friend Shared ReadOnly Property All() As List(Of Tool)
            Get
                Return _All
            End Get
        End Property

        Friend Shared Function FromName(ByVal name As String) As Tool
            For Each tool As Tool In All
                If tool.Name = name Then Return tool
            Next tool

            Return New Tool(name)
        End Function

        Friend Shared Sub ResetState()
            _All.Clear()
        End Sub

    End Class

End Namespace
