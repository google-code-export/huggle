Imports System
Imports System.Collections.Generic

Namespace Huggle

    <Diagnostics.DebuggerDisplay("{Name}")> _
    Friend Class ChangeTag

        'Represents a MediaWiki change tag; currently these are only created by abuse filters

        Private ReadOnly _Name As String
        Private ReadOnly _Wiki As Wiki

        Public Sub New(ByVal wiki As Wiki, ByVal name As String)
            _DisplayName = name
            _Name = name
            _Wiki = wiki
        End Sub

        Public ReadOnly Property Name() As String
            Get
                Return _Name
            End Get
        End Property

        Public Property Description() As String

        Public Property DisplayName() As String

        Public Property Hits() As Integer

        Public ReadOnly Property Wiki() As Wiki
            Get
                Return _Wiki
            End Get
        End Property

    End Class

    Friend Class ChangeTagCollection

        Private Wiki As Wiki

        Private ReadOnly _All As New Dictionary(Of String, ChangeTag)

        Public Sub New(ByVal wiki As Wiki)
            Me.Wiki = wiki
        End Sub

        Public ReadOnly Property All() As IList(Of ChangeTag)
            Get
                Return _All.Values.ToList.AsReadOnly
            End Get
        End Property

        Public Sub Clear()
            _All.Clear()
        End Sub

        Default Public ReadOnly Property Item(ByVal name As String) As ChangeTag
            Get
                If Not _All.ContainsKey(name) Then _All.Add(name, New ChangeTag(Wiki, name))
                Return _All(name)
            End Get
        End Property

    End Class

End Namespace
