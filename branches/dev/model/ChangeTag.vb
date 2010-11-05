Imports System
Imports System.Collections.Generic

Namespace Huggle

    <Diagnostics.DebuggerDisplay("{Name}")> _
    Friend Class ChangeTag

        'Represents a MediaWiki change tag; currently these are only created by abuse filters

        Private ReadOnly _Name As String
        Private ReadOnly _Wiki As Wiki

        Friend Sub New(ByVal wiki As Wiki, ByVal name As String)
            _DisplayName = name
            _Name = name
            _Wiki = wiki
        End Sub

        Friend ReadOnly Property Name() As String
            Get
                Return _Name
            End Get
        End Property

        Friend Property Description() As String

        Friend Property DisplayName() As String

        Friend Property Hits() As Integer

        Friend ReadOnly Property Wiki() As Wiki
            Get
                Return _Wiki
            End Get
        End Property

    End Class

    Friend Class ChangeTagCollection

        Private Wiki As Wiki

        Private ReadOnly _All As New Dictionary(Of String, ChangeTag)

        Friend Sub New(ByVal wiki As Wiki)
            Me.Wiki = wiki
        End Sub

        Friend ReadOnly Property All() As IList(Of ChangeTag)
            Get
                Return _All.Values.ToList.AsReadOnly
            End Get
        End Property

        Friend Sub Clear()
            _All.Clear()
        End Sub

        Default Friend ReadOnly Property Item(ByVal name As String) As ChangeTag
            Get
                If Not _All.ContainsKey(name) Then _All.Add(name, New ChangeTag(Wiki, name))
                Return _All(name)
            End Get
        End Property

    End Class

End Namespace
