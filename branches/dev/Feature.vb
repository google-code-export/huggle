Imports System
Imports System.Collections.Generic

Namespace Huggle

    <Diagnostics.DebuggerDisplay("{Name}")> _
    Public Class Feature

        Private _Name, _Comment, _Extension As String
        Private _Enabled As Boolean
        Private _Rights As New List(Of String), _Groups As New List(Of String), _Users As New List(Of String)
        Private _AccountAge As TimeSpan, _AccountEdits As Integer, _Version As Integer

        Private Shared ReadOnly _All As New Dictionary(Of String, Feature)

        Private Sub New(ByVal Name As String)
            _Name = Name
            _Enabled = True
            All.Merge(_Name, Me)
        End Sub

        Public Property AccountAge() As TimeSpan
            Get
                Return _AccountAge
            End Get
            Set(ByVal value As TimeSpan)
                _AccountAge = value
            End Set
        End Property

        Public Property AccountEdits() As Integer
            Get
                Return _AccountEdits
            End Get
            Set(ByVal value As Integer)
                _AccountEdits = value
            End Set
        End Property

        Public ReadOnly Property AvailableTo(ByVal Account As User) As Boolean
            Get
                Return (DisabledReasonFor(Account) Is Nothing)
            End Get
        End Property

        Public Property Comment() As String
            Get
                Return _Comment
            End Get
            Set(ByVal value As String)
                _Comment = value
            End Set
        End Property

        Public ReadOnly Property Description() As String
            Get
                Return Msg("feature-" & Name)
            End Get
        End Property

        Public ReadOnly Property DisabledReasonFor(ByVal Account As User) As String
            Get
                If Version > Account.Wiki.Config.EngineRevision _
                    Then Return Msg("access-version", Version)

                If Extension IsNot Nothing AndAlso Not Account.Wiki.Extensions.Contains(Extension) _
                    Then Return Msg("access-extension", Extension)

                'Users on approved users list have access regardless of other criteria
                If Account IsNot Nothing AndAlso Users.Contains(Account.Name) Then Return Nothing

                Dim UnavailableRights As New List(Of String)

                For Each Item As String In Rights
                    If Not Account.Rights.Contains(Item) Then UnavailableRights.Add(Item)
                Next Item

                If UnavailableRights.Count > 0 Then Return Msg("access-right", _
                    String.Join(", ", UnavailableRights.ToArray))

                If Account IsNot Nothing Then
                    If Groups.Count > 0 Then
                        Dim GroupsOK As Boolean

                        For Each item As String In Groups
                            If Account.IsInGroup(Account.Wiki.UserGroups(item)) Then GroupsOK = True
                        Next item

                        If Not GroupsOK Then Return Msg("access-group", String.Join(", ", Groups.ToArray))
                    End If

                    If Account.Contributions < AccountEdits Then Return Msg("access-edits", CStr(AccountEdits))

                    If Account.Created.Add(AccountAge) > Account.Wiki.ServerTime _
                        Then Return Msg("access-age", CStr(CInt(AccountAge.TotalDays)))
                End If

                If Not Enabled Then Return Msg("access-notavailable")

                If Account IsNot Nothing AndAlso Users.Count > 0 AndAlso Not Users.Contains(Account.Name) _
                    Then Return Msg("access-userlist")

                Return Nothing
            End Get
        End Property

        Public ReadOnly Property Enabled() As Boolean
            Get
                Return _Enabled
            End Get
        End Property

        Public Property Extension() As String
            Get
                Return _Extension
            End Get
            Set(ByVal value As String)
                _Extension = value
            End Set
        End Property

        Public Property Groups() As List(Of String)
            Get
                Return _Groups
            End Get
            Set(ByVal value As List(Of String))
                _Groups = value
            End Set
        End Property

        Public ReadOnly Property Name() As String
            Get
                Return _Name
            End Get
        End Property

        Public Property Rights() As List(Of String)
            Get
                Return _Rights
            End Get
            Set(ByVal value As List(Of String))
                _Rights = value
            End Set
        End Property

        Public Property Users() As List(Of String)
            Get
                Return _Users
            End Get
            Set(ByVal value As List(Of String))
                _Users = value
            End Set
        End Property

        Public Property Version() As Integer
            Get
                Return _Version
            End Get
            Set(ByVal value As Integer)
                _Version = value
            End Set
        End Property

        Public Shared ReadOnly Property All() As Dictionary(Of String, Feature)
            Get
                Return _All
            End Get
        End Property

        Public Shared Function FromName(ByVal Name As String) As Feature
            If All.ContainsKey(Name) Then Return All(Name) Else Return New Feature(Name)
        End Function

        Public Shared Sub ResetState()
            All.Clear()
        End Sub

    End Class

End Namespace
