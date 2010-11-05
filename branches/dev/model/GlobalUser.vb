Imports System
Imports System.Collections.Generic
Imports System.Net

Namespace Huggle

    'Represents a global user account created by MediaWiki's CentralAuth extension

    <Diagnostics.DebuggerDisplay("{FullName}")> _
    Friend Class GlobalUser

        Private ReadOnly _Family As Family
        Private ReadOnly _Name As String

        Private _Blocks As List(Of GlobalBlock)
        Private _Config As GlobalUserConfig
        Private _Cookies As CookieCollection
        Private _Users As List(Of User)
        Private _Wikis As List(Of Wiki)

        Friend Sub New(ByVal family As Family, ByVal name As String)
            _Family = family
            _Name = name
        End Sub

        Friend ReadOnly Property Blocks() As List(Of GlobalBlock)
            Get
                If _Blocks Is Nothing Then _Blocks = New List(Of GlobalBlock)
                Return _Blocks
            End Get
        End Property

        Friend ReadOnly Property Config() As GlobalUserConfig
            Get
                If _Config Is Nothing Then
                    If IsDefault Then
                        _Config = New GlobalUserConfig(Me)
                    Else
                        _Config = Family.GlobalUsers.Default.Config.Copy(Me)
                    End If
                End If

                Return _Config
            End Get
        End Property

        Friend ReadOnly Property Cookies() As CookieCollection
            Get
                If _Cookies Is Nothing Then _Cookies = New CookieCollection
                Return _Cookies
            End Get
        End Property

        Friend Property Created() As Date

        Friend ReadOnly Property Family() As Family
            Get
                Return _Family
            End Get
        End Property

        Friend ReadOnly Property FullName() As String
            Get
                Return Name & "@" & Family.Code
            End Get
        End Property

        Friend Property GlobalGroups() As List(Of GlobalGroup)

        Friend ReadOnly Property HasAccountOn(ByVal wiki As Wiki) As Boolean
            Get
                Return Wikis.Contains(wiki)
            End Get
        End Property

        Friend ReadOnly Property Home() As Wiki
            Get
                If PrimaryUser Is Nothing Then Return Nothing Else Return PrimaryUser.Wiki
            End Get
        End Property

        Friend Property Id() As Integer

        Friend Property IsActive() As Boolean

        Friend ReadOnly Property IsDefault() As Boolean
            Get
                Return (Name = "[default]")
            End Get
        End Property

        Friend Property IsHidden() As Boolean

        Friend Property IsLocked() As Boolean

        Friend ReadOnly Property Name() As String
            Get
                Return _Name
            End Get
        End Property

        Friend Property PrimaryUser() As User

        Friend Property Rights() As List(Of String)

        Friend ReadOnly Property UserOn(ByVal wiki As Wiki) As User
            Get
                If Wikis.Contains(wiki) Then Return wiki.Users(Name)
                Return Nothing
            End Get
        End Property

        Friend ReadOnly Property Users() As List(Of User)
            Get
                If _Users Is Nothing Then _Users = New List(Of User)
                Return _Users
            End Get
        End Property

        Friend ReadOnly Property Wikis() As List(Of Wiki)
            Get
                If _Wikis Is Nothing Then _Wikis = New List(Of Wiki)
                Return _Wikis
            End Get
        End Property

        Public Overrides Function ToString() As String
            Return _Name
        End Function

    End Class

    Friend Class GlobalUserCollection

        Private Family As Family

        Private ReadOnly _All As New Dictionary(Of String, GlobalUser)
        Private ReadOnly _Default As GlobalUser

        Friend Sub New(ByVal family As Family)
            Me.Family = family
            _Default = New GlobalUser(family, "[default]")
        End Sub

        Friend ReadOnly Property All() As IEnumerable(Of GlobalUser)
            Get
                Return _All.Values
            End Get
        End Property

        Friend ReadOnly Property [Default]() As GlobalUser
            Get
                Return _Default
            End Get
        End Property

        Default Friend ReadOnly Property FromName(ByVal name As String) As GlobalUser
            Get
                If Not _All.ContainsKey(name) Then _All.Add(name, New GlobalUser(Family, name))
                Return _All(name)
            End Get
        End Property

    End Class

End Namespace
