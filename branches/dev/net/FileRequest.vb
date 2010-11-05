Imports System
Imports System.IO
Imports System.Net
Imports System.Text
Imports System.Xml

Namespace Huggle

    'Represents a file request

    Class FileRequest : Inherits Request

        Friend Sub New(ByVal session As Session, ByVal url As Uri)
            MyBase.New(session)
            Me.Url = url
        End Sub

        Friend Overrides Sub Start()
            MyBase.Start()
            If Result.IsError Then OnFail(Result) Else OnSuccess()
        End Sub

        Friend ReadOnly Property File() As MemoryStream
            Get
                Return Response
            End Get
        End Property

    End Class

End Namespace
