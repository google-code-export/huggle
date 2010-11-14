Imports System
Imports System.Collections.Generic
Imports System.Text.RegularExpressions

Namespace Huggle

    'Represents protection/unprotection of a page

    Friend Class Protection : Inherits LogItem

        Private _Page As Page

        Public Sub New(id As Integer, wiki As Wiki)
            MyBase.New(id, wiki)
        End Sub

        Public Property IsCascading() As Boolean

        Public Property Create() As ProtectionPart

        Public Property Edit() As ProtectionPart

        Public Property Move() As ProtectionPart

        Public Overrides ReadOnly Property Icon() As Drawing.Image
            Get
                Return Resources.blob_log_protect
            End Get
        End Property

        Public Overrides ReadOnly Property Target() As String
            Get
                Return Page.Title
            End Get
        End Property

    End Class

    Friend Structure ProtectionPart

        Private _Expires As Date
        Private _Level As String

        Private Shared ReadOnly ProtectionLevelsMatch As New Regex(
            "\[(.+?)=(.+?)\] \((?:indefinite|expires (.+?) \(UTC\))\)", RegexOptions.Compiled)

        Public Shared ReadOnly None As New ProtectionPart(Date.MinValue, Nothing)

        Public Sub New(ByVal expires As Date, ByVal level As String)
            _Expires = expires
            _Level = level
        End Sub

        Public ReadOnly Property Expires() As Date
            Get
                Return _Expires
            End Get
        End Property

        Public ReadOnly Property Level() As String
            Get
                Return _Level
            End Get
        End Property

        Public Shared Function FromComment(ByVal comment As String, ByVal type As String) As ProtectionPart
            'Extract protection levels and expiry of each level from MediaWiki's internal representation
            If comment Is Nothing Then Return ProtectionPart.None

            For Each match As Match In ProtectionLevelsMatch.Matches(comment)
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