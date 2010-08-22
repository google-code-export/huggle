Imports Huggle.Actions
Imports System
Imports System.Collections.Generic
Imports System.Net

Namespace Huggle

    'Represents a login session

    <Diagnostics.DebuggerDisplay("{Description}")> _
    Public Class Session

        Private _RightsTokens As New Dictionary(Of User, String)
        Private _RollbackTokens As New Dictionary(Of Revision, String)
        Private _User As User

        Public Sub New(ByVal user As User)
            _User = user
        End Sub

        Public Property Cookies() As New CookieContainer

        Public ReadOnly Property Description() As String
            Get
                Return User.FullName & If(IsSecure, " (Secure)", "")
            End Get
        End Property

        Public Property EditToken() As String

        Public Property IsActive() As Boolean

        Public Property IsAutoconfirmed As Boolean

        Public Property IsSecure() As Boolean

        Public ReadOnly Property RightsTokens() As Dictionary(Of User, String)
            Get
                Return _RightsTokens
            End Get
        End Property

        Public ReadOnly Property RollbackTokens() As Dictionary(Of Revision, String)
            Get
                Return _RollbackTokens
            End Get
        End Property

        Public ReadOnly Property User() As User
            Get
                Return _User
            End Get
        End Property

        Public ReadOnly Property Wiki() As Wiki
            Get
                Return _User.Wiki
            End Get
        End Property

    End Class

    Public Class SessionCollection

        Private _All As New Dictionary(Of User, Session)

        Public ReadOnly Property All() As IList(Of Session)
            Get
                Return _All.Values.ToList.AsReadOnly
            End Get
        End Property

        Public Function Active() As IList(Of Session)
            Dim result As New List(Of Session)

            For Each session As Session In All
                If session.IsActive AndAlso Not session.User.IsAnonymous Then result.Add(session)
            Next session

            Return result
        End Function

        Public Function ActiveForWiki(ByVal wiki As Wiki) As IList(Of Session)
            Dim result As New List(Of Session)

            For Each session As Session In All
                If session.User.Wiki Is wiki AndAlso session.IsActive Then result.Add(session)
            Next session

            Return result
        End Function

        Public Function GetUserWithRight(ByVal wiki As Wiki, ByVal right As String) As Session
            For Each session As Session In All
                If session.User.Wiki Is wiki AndAlso session.User.HasRight(right) _
                    AndAlso Not session.User.IsAnonymous Then Return session
            Next session

            If wiki.Users.Anonymous.HasRight(right) Then Return Item(wiki.Users.Anonymous)
            Return Nothing
        End Function

        Default Public ReadOnly Property Item(ByVal globalUser As GlobalUser) As Session
            Get
                For Each session As Session In All
                    If session.User.GlobalUser Is globalUser AndAlso session.IsActive Then Return session
                Next session

                Return Nothing
            End Get
        End Property

        Default Public ReadOnly Property Item(ByVal user As User) As Session
            Get
                If Not _All.ContainsKey(user) Then _All.Add(user, New Session(user))
                Return _All(user)
            End Get
        End Property

        Default Public ReadOnly Property Item(ByVal wiki As Wiki) As Session
            Get
                For Each session As Session In All
                    If session.User.Wiki Is wiki AndAlso session.IsActive _
                        AndAlso Not session.User.IsAnonymous Then Return session
                Next session

                Return Item(wiki.Users.Anonymous)
            End Get
        End Property

        Default Public ReadOnly Property Item(ByVal family As Family) As Session
            Get
                For Each session As Session In All
                    If session.User.Wiki.Family Is family AndAlso session.IsActive _
                        AndAlso Not session.User.IsAnonymous Then Return session
                Next session

                Return family.CentralWiki.Users.Anonymous.Session
            End Get
        End Property

    End Class

End Namespace
