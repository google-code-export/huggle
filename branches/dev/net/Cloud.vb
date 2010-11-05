Imports System
Imports System.Collections.Generic
Imports System.IO
Imports System.IO.Compression
Imports System.Net
Imports System.Text
Imports System.Web.HttpUtility
Imports System.Xml

Namespace Huggle.Actions

    'When your data is in the Cloud(tm), you don't have to worry about anything!
    '(except for the free storage service that you used because you were too 
    'lazy/broke to host your own backend)

    Friend Class CloudQuery : Inherits Process

        Private Key As String
        Private _Value As String

        Friend Shared ReadOnly Property KeyPrefix As String
            Get
#If DEBUG Then
                'lets not stomp all over production
                Return "0639de1fd43fee3c9e21133743c57a10-" 'md5("huggledebug")
#Else
                Return "a16a3284b37ba641f8720fce6e42a02a-" 'md5("hugglerelease")
#End If
            End Get
        End Property

        Friend ReadOnly Property Value As String
            Get
                Return _Value
            End Get
        End Property

        Sub New(ByVal key As String)
            Me.Key = key
        End Sub

        Friend Overrides Sub Start()
            OnStarted()

            Dim req As New Request(Nothing)
            req.Url = New Uri(InternalConfig.CloudUrl.ToString & UrlEncode(KeyPrefix & Key))
            req.Start()

            'Key does not exist yet
            If req.IsErrored AndAlso req.Result.Code = "httperror-404" Then
                _Value = ""
                OnSuccess()
                Return
            End If

            'Some other error
            If req.IsFailed Then OnFail(req.Result.Wrap(Msg("config-cloudloaderror", Key))) : Return

            Dim failResult As New Result({Msg("config-cloudloaderror", Key), Msg("config-baddata")})

            'Uncompress data
            Dim data As Byte() = Uncompress(req.Response.ToArray)
            If data Is Nothing Then OnFail(failResult) : Return

            'Decode data
            Try
                _Value = Encoding.UTF8.GetString(data)

            Catch ex As ArgumentException
                _Value = Nothing
                OnFail(failResult) : Return
            End Try

            'Check for expected prefix
            If _Value.StartsWithI(KeyPrefix) Then
                _Value = _Value.Substring(KeyPrefix.Length)
            Else
                _Value = Nothing
                OnFail(failResult) : Return
            End If

            OnSuccess()
        End Sub

    End Class

    Friend Class CloudStore : Inherits Process

        Private Key As String
        Private Value As String

        Friend Sub New(ByVal key As String, ByVal value As String)
            Me.Key = key
            Me.Value = value
        End Sub

        Friend Overrides Sub Start()
            OnStarted()

            Dim req As New Request(Nothing)
            req.Url = New Uri(InternalConfig.CloudUrl.ToString)

            'Prefix data as a rudimentary guard against tampering
            Value = CloudQuery.KeyPrefix & Value

            Dim header As Byte() = Encoding.UTF8.GetBytes(UrlEncode(CloudQuery.KeyPrefix & Key) & "=")
            Dim uncompressedValue As Byte() = Encoding.UTF8.GetBytes(Value)
            Dim compressedValue As Byte() = UrlEncodeToBytes(Compress(Encoding.UTF8.GetBytes(Value)))
            Dim data(header.Length + compressedValue.Length) As Byte

            Array.Copy(header, 0, data, 0, header.Length)
            Array.Copy(compressedValue, 0, data, header.Length, compressedValue.Length)

            req.Data = data
            req.Start()
            If req.IsFailed Then OnFail(req.Result) : Return
            OnSuccess()
        End Sub

    End Class

End Namespace
