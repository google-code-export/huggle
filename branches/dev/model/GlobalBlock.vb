Namespace Huggle

    Public Class GlobalBlock : Inherits LogItem

        'Represents a global block made with MediaWiki's GlobalBlocking extension

        Private ReadOnly _AnonOnly As Boolean
        Private ReadOnly _Family As Family
        Private ReadOnly _Expires As Date
        Private ReadOnly _Id As Integer
        Private ReadOnly _Reason As String
        Private ReadOnly _Target As String
        Private ReadOnly _Time As Date
        Private ReadOnly _User As User

        Public Sub New(ByVal action As String, ByVal family As Family, ByVal id As Integer, ByVal rcid As Integer, _
            ByVal target As String, ByVal anonOnly As Boolean, ByVal user As User, ByVal time As Date, _
            ByVal expires As Date, ByVal reason As String)

            MyBase.New(user.Wiki, id, rcid)
            Me.Action = action
            _AnonOnly = anonOnly
            _Expires = expires
            _Family = family
            _Id = id
            _Reason = reason
            _Target = target
            _Time = time
            _User = user
        End Sub

        Public ReadOnly Property AnonOnly() As Boolean
            Get
                Return _AnonOnly
            End Get
        End Property

        Public ReadOnly Property Expires() As Date
            Get
                Return _Expires
            End Get
        End Property

        Public ReadOnly Property Family() As Family
            Get
                Return _Family
            End Get
        End Property

        Public ReadOnly Property Reason() As String
            Get
                Return _Reason
            End Get
        End Property

        Public Overrides ReadOnly Property Target() As String
            Get
                Return _Target
            End Get
        End Property

    End Class

End Namespace