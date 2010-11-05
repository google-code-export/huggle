Imports System
Imports System.Collections.Generic
Imports System.Text.RegularExpressions

Namespace Huggle

    'Represents protection/unprotection of a page

    Friend Class Protection : Inherits LogItem

        Private _Cascade As Boolean
        Private _Create As ProtectionPart
        Private _Edit As ProtectionPart
        Private _Move As ProtectionPart
        Private _Page As Page

        Friend Sub New(ByVal time As Date, ByVal page As Page, ByVal user As User, _
            ByVal action As String, ByVal comment As String, ByVal cascade As Boolean, _
            ByVal levels As String, ByVal hidden As Boolean, ByVal id As Integer, ByVal rcid As Integer)

            MyBase.New(user.Wiki, Id, rcid)
            Me.Action = action
            Me.Comment = comment
            Me.Time = time
            Me.User = user
            Me.IsHidden = Hidden

            _Cascade = cascade
            _Page = page

            _Edit = ProtectionPart.FromString(levels, "edit")
            _Move = ProtectionPart.FromString(levels, "move")
            _Create = ProtectionPart.FromString(levels, "create")

            If _Page IsNot Nothing Then _Page.Logs.Add(Me)
        End Sub

        Friend ReadOnly Property Cascade() As Boolean
            Get
                Return _Cascade
            End Get
        End Property

        Friend ReadOnly Property Create() As ProtectionPart
            Get
                Return _Create
            End Get
        End Property

        Friend ReadOnly Property Edit() As ProtectionPart
            Get
                Return _Edit
            End Get
        End Property

        Friend ReadOnly Property Move() As ProtectionPart
            Get
                Return _Move
            End Get
        End Property

        Friend Overrides ReadOnly Property Icon() As Drawing.Image
            Get
                Return Resources.blob_log_protect
            End Get
        End Property

        Friend ReadOnly Property Page() As Page
            Get
                Return _Page
            End Get
        End Property

        Friend Overrides ReadOnly Property Target() As String
            Get
                Return _Page.Name
            End Get
        End Property

    End Class

    Friend Structure ProtectionPart

        Private _Expires As Date, _Level As String

        Private Shared ReadOnly ProtectionLevelsMatch As New Regex _
            ("\[(.+?)=(.+?)\] \((?:indefinite|expires (.+?) \(UTC\))\)", RegexOptions.Compiled)

        Friend Shared ReadOnly None As New ProtectionPart(Date.MinValue, Nothing)

        Friend Sub New(ByVal expires As Date, ByVal level As String)
            _Expires = expires
            _Level = level
        End Sub

        Friend ReadOnly Property Expires() As Date
            Get
                Return _Expires
            End Get
        End Property

        Friend ReadOnly Property Level() As String
            Get
                Return _Level
            End Get
        End Property

        Friend Shared Function FromString(ByVal str As String, ByVal type As String) As ProtectionPart

            'Extract protection levels and expiry of each level from MediaWiki's internal representation
            If str Is Nothing Then Return ProtectionPart.None

            For Each match As Match In ProtectionLevelsMatch.Matches(str)
                If match.Groups(1).Value = type Then
                    Dim expiry As Date
                    Date.TryParse(match.Groups(3).Value, expiry)
                    If expiry = Date.MinValue Then expiry = Date.MaxValue
                    Return New ProtectionPart(expiry, match.Groups(2).Value)
                End If
            Next match

            Return ProtectionPart.None
        End Function

        Public Shared Operator =(ByVal x As ProtectionPart, ByVal y As ProtectionPart) As Boolean
            Return (x.Expires = y.Expires AndAlso x.Level = y.Level)
        End Operator

        Public Shared Operator <>(ByVal x As ProtectionPart, ByVal y As ProtectionPart) As Boolean
            Return Not (x = y)
        End Operator

        Public Overrides Function Equals(ByVal obj As Object) As Boolean
            Return (TypeOf obj Is ProtectionPart AndAlso CType(obj, ProtectionPart) = Me)
        End Function

        Public Overrides Function GetHashCode() As Integer
            Return Expires.GetHashCode Xor Level.GetHashCode
        End Function

    End Structure

End Namespace