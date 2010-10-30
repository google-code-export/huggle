Imports System
Imports System.Collections.Generic
Imports System.Net
Imports System.Text
Imports System.Web.HttpUtility
Imports System.Xml

Namespace Huggle.Actions

    'When your data is in the Cloud(tm), you don't have to worry about anything!
    '(except for the free storage service that you used because you were too 
    'lazy/broke to host your own backend)

    Public Class CloudQuery : Inherits Query

        Private Key As String

        Private _Value As String

        Public ReadOnly Property Value As String
            Get
                Return _Value
            End Get
        End Property

        Sub New(ByVal key As String)
            MyBase.New(Nothing, Msg("cloud-desc"))
            Me.Key = key
        End Sub

        Public Overrides Sub Start()
            OnStarted()

            Dim req As New Request(Session)
            req.Url = New Uri(InternalConfig.CloudUrl.ToString & UrlEncode(InternalConfig.CloudKey & "-" & Key))
            req.Start()

            If req.IsErrored AndAlso req.Result.Code = "httperror" Then
                _Value = ""
                OnSuccess()
            ElseIf req.IsFailed Then
                OnFail(req.Result)
            Else
                _Value = Encoding.UTF8.GetString(req.Response.ToArray)
                OnSuccess()
            End If
        End Sub

    End Class

    Public Class CloudStore : Inherits Query

        Private Key As String
        Private Value As String

        Public Sub New(ByVal key As String, ByVal value As String)
            MyBase.New(Nothing, Msg("cloud-desc"))
            Me.Key = key
            Me.Value = value
        End Sub

        Public Overrides Sub Start()
            OnStarted()

            Dim req As New Request(Session)
            req.Url = New Uri(InternalConfig.CloudUrl.ToString)
            req.Data = Encoding.UTF8.GetBytes(InternalConfig.CloudKey & "-" & Key & "=" & Value)
            req.Start()
            If req.IsFailed Then OnFail(req.Result) : Return
            OnSuccess()
        End Sub

    End Class

End Namespace
