Imports System
Imports System.Text

Namespace Huggle

    'Represents a request for a text document

    Friend Class TextRequest : Inherits Request

        Private _Response As String

        Public Sub New(ByVal url As Uri)
            Me.Url = url
        End Sub

        Public Overrides Sub Start()
            MyBase.Start()
            If Result.IsError Then OnFail(Result) : Return

            _Response = Encoding.UTF8.GetString(MyBase.Response.ToArray)
            MyBase.Response.Dispose()

            OnSuccess()
        End Sub

        Public Shadows ReadOnly Property Response As String
            Get
                Return _Response
            End Get
        End Property

    End Class

End Namespace
