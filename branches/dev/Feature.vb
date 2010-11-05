Imports System
Imports System.Collections.Generic

Namespace Huggle

    <Diagnostics.DebuggerDisplay("{Name}")>
    Friend Class Feature

        Private _Enabled As Boolean
        Private _Name As String

        Private Shared ReadOnly _All As New Dictionary(Of String, Feature)

        Private Sub New(ByVal Name As String)
            _Name = Name
            _Enabled = True
            All.Merge(_Name, Me)
        End Sub

        Friend Property AccountAge() As TimeSpan

        Friend Property AccountEdits() As Integer

        Friend ReadOnly Property AvailableTo(ByVal account As User) As Boolean
            Get
                Return (DisabledReasonFor(account) Is Nothing)
            End Get
        End Property

        Friend Property Comment() As String

        Friend ReadOnly Property Description() As String
            Get
                Return Msg("feature-" & Name)
            End Get
        End Property

        Friend ReadOnly Property DisabledReasonFor(ByVal account As User) As String
            Get
                If Version > account.Wiki.Config.EngineRevision _
                    Then Return Msg("access-version", Version)

                If Extension IsNot Nothing AndAlso Not account.Wiki.Extensions.Contains(Extension) _
                    Then Return Msg("access-extension", Extension)

                'Users on approved users list have access regardless of other criteria
                If account IsNot Nothing AndAlso Users.Contains(account.Name) Then Return Nothing

                Dim UnavailableRights As New List(Of String)

                For Each right As String In Rights
                    If Not account.Rights.Contains(right) Then UnavailableRights.Add(right)
                Next right

                If UnavailableRights.Count > 0 Then Return Msg("access-right", _
                    String.Join(", ", UnavailableRights.ToArray))

                If account IsNot Nothing Then
                    If Groups.Count > 0 Then
                        Dim GroupsOK As Boolean

                        For Each item As String In Groups
                            If account.IsInGroup(account.Wiki.UserGroups(item)) Then GroupsOK = True
                        Next item

                        If Not GroupsOK Then Return Msg("access-group", String.Join(", ", Groups.ToArray))
                    End If

                    If account.Contributions < AccountEdits Then Return Msg("access-edits", CStr(AccountEdits))

                    If account.Created.Add(AccountAge) > account.Wiki.ServerTime _
                        Then Return Msg("access-age", CStr(CInt(AccountAge.TotalDays)))
                End If

                If Not Enabled Then Return Msg("access-notavailable")

                If account IsNot Nothing AndAlso Users.Count > 0 AndAlso Not Users.Contains(account.Name) _
                    Then Return Msg("access-userlist")

                Return Nothing
            End Get
        End Property

        Friend ReadOnly Property Enabled() As Boolean
            Get
                Return _Enabled
            End Get
        End Property

        Friend Property Extension() As String

        Friend Property Groups() As List(Of String)

        Friend ReadOnly Property Name() As String
            Get
                Return _Name
            End Get
        End Property

        Friend Property Rights() As List(Of String)

        Friend Property Users() As List(Of String)

        Friend Property Version() As Integer

        Friend Shared ReadOnly Property All() As Dictionary(Of String, Feature)
            Get
                Return _All
            End Get
        End Property

        Friend Shared Function FromName(ByVal name As String) As Feature
            If All.ContainsKey(name) Then Return All(name) Else Return New Feature(name)
        End Function

        Friend Shared Sub ResetState()
            All.Clear()
        End Sub

    End Class

End Namespace
