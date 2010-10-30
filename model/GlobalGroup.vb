Imports System
Imports System.Collections.Generic

Namespace Huggle

    <Diagnostics.DebuggerDisplay("{Name}")> _
    Public Class GlobalGroup

        'Represents a global group created by MediaWiki's CentralAuth extension

        Private ReadOnly _Family As Family
        Private ReadOnly _Name As String
        Private ReadOnly _Rights As New List(Of String)

        Public Sub New(ByVal family As Family, ByVal name As String)
            _Family = family
            _Name = name
        End Sub

        Public Property DisplayName As String
            
        Public ReadOnly Property Family() As Family
            Get
                Return _Family
            End Get
        End Property

        Public ReadOnly Property Name() As String
            Get
                Return _Name
            End Get
        End Property

        Public ReadOnly Property Rights() As List(Of String)
            Get
                Return _Rights
            End Get
        End Property

        Public Overrides Function ToString() As String
            Return _Name
        End Function

    End Class

    Public Class GlobalGroupCollection

        Private ReadOnly _All As New Dictionary(Of String, GlobalGroup)

        Private Family As Family

        Public Sub New(ByVal family As Family)
            Me.Family = family
        End Sub

        Public ReadOnly Property All() As IList(Of GlobalGroup)
            Get
                Return _All.Values.ToList.AsReadOnly
            End Get
        End Property

        Default Public ReadOnly Property Item(ByVal code As String) As GlobalGroup
            Get
                If Not _All.ContainsKey(code) Then _All.Add(code, New GlobalGroup(Family, code))
                Return _All(code)
            End Get
        End Property

        Public Sub Clear()
            _All.Clear()
        End Sub

    End Class

End Namespace