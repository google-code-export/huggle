Imports System
Imports System.Collections.Generic

Namespace Huggle

    <Diagnostics.DebuggerDisplay("{Name}")>
    Friend Class UserGroup

        'Represents a MediaWiki user group

        Private _Name As String
        Private _Rights As New List(Of String)
        Private _Wiki As Wiki

        Public Sub New(ByVal wiki As Wiki, ByVal name As String)
            _Name = name
            _Wiki = wiki
        End Sub

        Public Property Count() As Integer = -1
        Public Property IsImplicit As Boolean

        Public ReadOnly Property Description As String
            Get
                If Name = "*" Then Return Msg("view-usergroup-all")
                If Wiki.Messages.ContainsKey("group-" & Name) Then Return Wiki.Message("group-" & Name)
                Return Name
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

        Public ReadOnly Property Wiki() As Wiki
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

        Public Sub New(ByVal wiki As Wiki)
            Me.Wiki = wiki
            Reset()
        End Sub

        Public ReadOnly Property All() As IList(Of UserGroup)
            Get
                Return _All.Values.ToList.AsReadOnly
            End Get
        End Property

        Default Public ReadOnly Property Item(ByVal name As String) As UserGroup
            Get
                If Not _All.ContainsKey(name) Then _All.Add(name, New UserGroup(Wiki, name))
                Return _All(name)
            End Get
        End Property

        Public Sub Reset()
            _All.Clear()

            Item("*").IsImplicit = True
            Item("user").IsImplicit = True
        End Sub

    End Class

    Friend Class UserGroupChange

        Private _Group As UserGroup
        Private User As User

        Public Sub New(ByVal user As User, ByVal group As UserGroup)
            _Group = group
            Me.User = user
        End Sub

        Public Property CanAdd As Boolean

        Public Property CanAddSelf As Boolean

        Public Property CanRemove As Boolean

        Public Property CanRemoveSelf As Boolean

        Public ReadOnly Property Group() As UserGroup
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

        Public Sub New(ByVal user As User)
            Me.User = user
        End Sub

        Public ReadOnly Property All As IList(Of UserGroupChange)
            Get
                Return _All.Values.ToList.AsReadOnly
            End Get
        End Property

        Default Public ReadOnly Property Item(ByVal group As UserGroup) As UserGroupChange
            Get
                If Not _All.ContainsKey(group) Then _All.Add(group, New UserGroupChange(User, group))
                Return _All(group)
            End Get
        End Property

        Public Sub Reset()
            For Each group As UserGroupChange In All
                group.CanAdd = False
                group.CanAddSelf = False
                group.CanRemove = False
                group.CanRemoveSelf = False
            Next group
        End Sub

    End Class

End Namespace