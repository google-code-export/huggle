Imports System
Imports System.Collections.Generic

Namespace Huggle

    <Diagnostics.DebuggerDisplay("{Name}")>
    Friend Class UserGroup

        'Represents a MediaWiki user group

        Private _Name As String
        Private _Rights As New List(Of String)
        Private _Wiki As Wiki

        Friend Sub New(ByVal wiki As Wiki, ByVal name As String)
            _Name = name
            _Wiki = wiki
        End Sub

        Friend Property Count() As Integer = -1
        Friend Property IsImplicit As Boolean

        Friend ReadOnly Property Description As String
            Get
                If Name = "*" Then Return Msg("view-usergroup-all")
                If Wiki.Messages.ContainsKey("group-" & Name) Then Return Wiki.Message("group-" & Name)
                Return Name
            End Get
        End Property

        Friend ReadOnly Property Name() As String
            Get
                Return _Name
            End Get
        End Property

        Friend ReadOnly Property Rights() As List(Of String)
            Get
                Return _Rights
            End Get
        End Property

        Friend ReadOnly Property Wiki() As Wiki
            Get
                Return _Wiki
            End Get
        End Property

        Public Overrides Function ToString() As String
            Return Name
        End Function

    End Class

    Friend Class UserGroupCollection

        Private Wiki As Wiki

        Private ReadOnly _All As New Dictionary(Of String, UserGroup)

        Friend Sub New(ByVal wiki As Wiki)
            Me.Wiki = wiki
            Reset()
        End Sub

        Friend ReadOnly Property All() As IList(Of UserGroup)
            Get
                Return _All.Values.ToList.AsReadOnly
            End Get
        End Property

        Default Friend ReadOnly Property Item(ByVal name As String) As UserGroup
            Get
                If Not _All.ContainsKey(name) Then _All.Add(name, New UserGroup(Wiki, name))
                Return _All(name)
            End Get
        End Property

        Friend Sub Reset()
            _All.Clear()

            Item("*").IsImplicit = True
            Item("user").IsImplicit = True
        End Sub

    End Class

    Friend Class UserGroupChange

        Private _Group As UserGroup
        Private User As User

        Friend Sub New(ByVal user As User, ByVal group As UserGroup)
            _Group = group
            Me.User = user
        End Sub

        Friend Property CanAdd As Boolean
        Friend Property CanAddSelf As Boolean
        Friend Property CanRemove As Boolean
        Friend Property CanRemoveSelf As Boolean

        Friend ReadOnly Property Group() As UserGroup
            Get
                Return _Group
            End Get
        End Property

        Public Overrides Function ToString() As String
            Return User.FullName & ":" & Group.Name
        End Function

    End Class

    Friend Class UserGroupChangeCollection

        Private _All As New Dictionary(Of UserGroup, UserGroupChange)

        Private User As User

        Friend Sub New(ByVal user As User)
            Me.User = user
        End Sub

        Friend ReadOnly Property All As IList(Of UserGroupChange)
            Get
                Return _All.Values.ToList.AsReadOnly
            End Get
        End Property

        Default Friend ReadOnly Property Item(ByVal group As UserGroup) As UserGroupChange
            Get
                If Not _All.ContainsKey(group) Then _All.Add(group, New UserGroupChange(User, group))
                Return _All(group)
            End Get
        End Property

        Friend Sub Reset()
            For Each group As UserGroupChange In All
                group.CanAdd = False
                group.CanAddSelf = False
                group.CanRemove = False
                group.CanRemoveSelf = False
            Next group
        End Sub

    End Class

End Namespace