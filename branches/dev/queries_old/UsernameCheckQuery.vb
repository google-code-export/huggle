Imports System.Xml

Namespace Huggle.Actions

    Public Class UsernameCheckQuery : Inherits Query

        Private _Name As String
        Private _Status As CheckStatus

        Public Sub New(ByVal session As Session, ByVal name As String)
            MyBase.New(session, Msg("usernamecheck-desc", name))
            _Name = name
        End Sub

        Public ReadOnly Property Name() As String
            Get
                Return _Name
            End Get
        End Property

        Public ReadOnly Property Status() As CheckStatus
            Get
                Return _Status
            End Get
        End Property

        Public Overrides Sub Start()

            Dim req As New ApiRequest(Session, Description, New QueryString( _
                "action", "query", _
                "list", "users", _
                "ususers", Name))

            req.Start()

            If req.Result.IsError Then
                _Status = CheckStatus.Error
                OnFail(req.Result) : Return
            End If

            Dim node As XmlNodeList = req.Response.GetElementsByTagName("user")

            If node.Count = 0 OrElse node(0).HasAttribute("invalid") Then
                _Status = CheckStatus.Invalid
            ElseIf node(0).HasAttribute("missing") Then
                _Status = CheckStatus.OK
            Else
                _Status = CheckStatus.Used
            End If

            OnSuccess()
        End Sub

    End Class

    Public Enum CheckStatus As Integer
        : None : Checking : Invalid : Used : OK : [Error]
    End Enum

End Namespace
