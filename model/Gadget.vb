Imports System
Imports System.Collections.Generic

Namespace Huggle

    <Diagnostics.DebuggerDisplay("{Name}")> _
    Friend Class Gadget

        Private ReadOnly _Code As String
        Private ReadOnly _Wiki As Wiki

        Friend Sub New(ByVal wiki As Wiki, ByVal code As String)
            _Code = code
            _Name = code
            _Wiki = wiki
        End Sub

        Friend ReadOnly Property Code() As String
            Get
                Return _Code
            End Get
        End Property

        Friend Property Description() As String
        Friend Property Name() As String
        Friend Property Pages() As List(Of Page)
        Friend Property Type() As String
        Friend Property TypeDesc() As String

        Friend ReadOnly Property Wiki() As Wiki
            Get
                Return _Wiki
            End Get
        End Property

        Public Overrides Function ToString() As String
            Return Name
        End Function

    End Class

    Friend Class GadgetCollection

        Private ReadOnly _All As New Dictionary(Of String, Gadget)
        Private Wiki As Wiki

        Friend Sub New(ByVal wiki As Wiki)
            Me.Wiki = wiki
        End Sub

        Friend ReadOnly Property All() As IList(Of Gadget)
            Get
                Return _All.Values.ToList.AsReadOnly
            End Get
        End Property

        Default Friend ReadOnly Property Item(ByVal code As String) As Gadget
            Get
                If Not _All.ContainsKey(code) Then _All.Add(code, New Gadget(Wiki, code))
                Return _All(code)
            End Get
        End Property

        Friend Function FromName(ByVal name As String) As Gadget
            For Each item As Gadget In All
                If item.Code = name OrElse item.Name = name Then Return item
            Next item

            Return Nothing
        End Function

        Friend Sub Clear()
            _All.Clear()
        End Sub

    End Class

End Namespace