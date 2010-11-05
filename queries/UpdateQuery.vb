'Imports System
'Imports System.IO
'Imports System.Net

'Namespace Huggle.Queries

'    Class UpdateRequest : Inherits OldQuery

'        'Download latest version of the application

'        Private Filename As String
'        Private Complete, Total As Integer

'        Friend Event UpdateProgress As EventHandler(Of UpdateRequest, UpdateProgressEventArgs)

'        Friend Sub New(ByVal Account As User, ByVal Filename As String)
'            MyBase.New(Account)
'            Me.Filename = Filename
'        End Sub

'        Protected Overrides Function Process() As Result

'            'Download into memory and then write to file
'            Try
'                Dim Request As HttpWebRequest = CType(WebRequest.Create _
'                    (Config.Global.DownloadLocation.FormatWith(Config.Global.LatestVersion.ToString)), HttpWebRequest)
'                Request.Proxy = HttpWebRequest.GetSystemWebProxy
'                Request.UserAgent = Config.Internal.UserAgent

'                Dim Response As WebResponse = Request.GetResponse
'                Dim ResponseStream As Stream = Response.GetResponseStream
'                Total = CInt(Response.ContentLength)
'                Dim MemoryStream As New MemoryStream(Total)

'                Dim Buffer(255) As Byte, S As Integer

'                Do
'                    S = ResponseStream.Read(Buffer, 0, Buffer.Length)
'                    MemoryStream.Write(Buffer, 0, S)
'                    Complete += S
'                    CreateThread(AddressOf OnUpdateProgress)
'                Loop While S > 0 AndAlso State <> QueryState.Cancelled

'                File.WriteAllBytes(Filename, MemoryStream.ToArray)

'            Catch ex As SystemException
'                If State <> QueryState.Cancelled Then Return Result.Fail(Msg("update-error"), ex.Message)
'            End Try

'            If State = QueryState.Cancelled Then
'                IfIO.File.Exists(Filename) Then File.Delete(Filename)
'                Return Result.Fail("", "")
'            End If

'            Return Result.Success
'        End Function

'        Private Sub OnUpdateProgress()
'            RaiseEvent UpdateProgress(Me, New UpdateProgressEventArgs(Complete, Total))
'        End Sub

'    End Class

'    Class UpdateProgressEventArgs : Inherits EventArgs

'        Private _Complete, _Total As Integer

'        Friend Sub New(ByVal Complete As Integer, ByVal Total As Integer)
'            _Complete = Complete
'            _Total = Total
'        End Sub

'        Friend ReadOnly Property Complete() As Integer
'            Get
'                Return _Complete
'            End Get
'        End Property

'        Friend ReadOnly Property Total() As Integer
'            Get
'                Return _Total
'            End Get
'        End Property

'    End Class

'End Namespace