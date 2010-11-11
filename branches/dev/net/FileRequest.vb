Imports System
Imports System.IO
Imports System.Net
Imports System.Text
Imports System.Xml

Namespace Huggle

    'Represents a file request

    Class FileRequest : Inherits Request

        Public Sub New(ByVal url As Uri)
            Me.Url = url
        End Sub

        Public Overrides Sub Start()
            MyBase.Start()
            If Result.IsError Then OnFail(Result) Else OnSuccess()
        End Sub

        Public Shadows ReadOnly Property Response() As MemoryStream
            Get
                Return MyBase.Response
            End Get
        End Property

    End Class

End Namespace
