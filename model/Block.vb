Imports System
Imports System.Text.RegularExpressions

Namespace Huggle

    'Represents blocking/unblocking of a user

    Public Class Block : Inherits LogItem

        Private _AnonOnly As Boolean
        Private _AutoBlock As Boolean
        Private _BlockCreation As Boolean
        Private _BlockEmail As Boolean
        Private _BlockTalk As Boolean
        Private _Duration As String
        Private _Expires As Date
        Private _IsAutomatic As Boolean
        Private _IsRangeblock As Boolean
        Private _Target As String
        Private _TargetUser As User

        Private Shared ReadOnly RangeRegex As New Regex _
            ("\d{1,3}\.\d{1,3}\.\d{1,3}\.\d{1,3}/\d{2}", RegexOptions.Compiled)

        Public Sub New(ByVal time As Date, ByVal target As String, _
            ByVal user As User, ByVal action As String, ByVal comment As String, ByVal id As Integer, _
            ByVal rcid As Integer, ByVal automatic As Boolean, ByVal anonOnly As Boolean, _
            ByVal autoBlock As Boolean, ByVal blockCreation As Boolean, ByVal blockEmail As Boolean, _
            ByVal blockTalk As Boolean, ByVal duration As String, ByVal expires As Date)

            MyBase.New(user.Wiki, id, rcid)
            Me.Action = action
            Me.Comment = comment
            Me.Time = time
            Me.User = user

            _AnonOnly = anonOnly
            _AutoBlock = autoBlock
            _BlockCreation = blockCreation
            _BlockEmail = blockEmail
            _BlockTalk = blockTalk
            _Duration = Duration
            _Expires = Expires
            _Target = target

            _IsAutomatic = automatic

            If RangeRegex.IsMatch(target) Then
                _IsRangeblock = True
            Else
                _TargetUser = Wiki.Users(target)
                _TargetUser.Blocks.Add(Me)
            End If
        End Sub

        Public ReadOnly Property Duration() As String
            Get
                Return _Duration
            End Get
        End Property

        Public ReadOnly Property Expires() As Date
            Get
                Return _Expires
            End Get
        End Property

        Public Overrides ReadOnly Property Icon() As Drawing.Image
            Get
                Return Resources.blob_log_block
            End Get
        End Property

        Public ReadOnly Property IsAutomatic() As Boolean
            Get
                Return _IsAutomatic
            End Get
        End Property

        Public ReadOnly Property IsRangeblock() As Boolean
            Get
                Return _IsRangeblock
            End Get
        End Property

        Public Overrides ReadOnly Property Target() As String
            Get
                Return _Target
            End Get
        End Property

        Public ReadOnly Property TargetUser() As User
            Get
                Return _TargetUser
            End Get
        End Property

    End Class

End Namespace