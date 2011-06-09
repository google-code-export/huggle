Imports System
Imports System.Text
Imports System.Web.HttpUtility

Namespace Huggle.Net

    Friend Class OpenKeyval : Implements IKeyValueStore

        Private Uri As Uri

        Public Sub New(ByVal url As Uri)
            Me.Uri = Uri
        End Sub


        Public Shared ReadOnly Property Prefix() As String
            Get
#If DEBUG Then
                'Data prefix as a rudimentary guard against tampering
                'lets not stomp all over production
                Return "0639de1fd43fee3c9e21133743c57a10-" 'md5("huggledebug")
#Else
                Return "a16a3284b37ba641f8720fce6e42a02a-" 'md5("hugglerelease")
#End If
            End Get
        End Property

        Private Function GetValidKey(ByVal key As String) As String
            Const validKeyChars As String = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-_"

            For i As Integer = 0 To key.Length - 1
                If Not validKeyChars.Contains(key(i)) Then key = key.Replace(key(i), "_")
            Next i

            Return key
        End Function

        Public Function LoadData(ByVal key As String) As KeyValueRequest Implements IKeyValueStore.LoadData
            Return New OpenKeyvalQuery(Uri, GetValidKey(key))
        End Function

        Public Function SaveData(ByVal key As String, ByVal value As String) As KeyValueRequest _
            Implements IKeyValueStore.SaveData

            Return New OpenKeyvalStore(Uri, GetValidKey(key), value)
        End Function

        Friend Class OpenKeyvalQuery : Inherits KeyValueRequest

            Private Key As String
            Private _Value As String

            Sub New(ByVal uri As Uri, ByVal key As String)
                Me.Key = key
                Me.Url = uri
            End Sub

            Public Overrides Sub Start()
                OnStarted()

                Url = New Uri(Url.ToString & UrlEncode(Key))
                MyBase.Start()

                If Result.IsError Then
                    If Result.Code = "httperror-404" Then
                        'Key does not exist yet
                        _Value = Nothing
                        OnSuccess()
                        Return
                    Else
                        'Some other error
                        OnFail(Result.Wrap(Msg("config-cloudloaderror", Key))) : Return
                    End If
                End If

                Dim failResult As New Result({Msg("config-cloudloaderror", Key), Msg("config-baddata")})

                'Uncompress data
                Dim data As Byte() = Uncompress(MyBase.Response)
                MyBase.Response.Dispose()
                If data Is Nothing Then OnFail(failResult) : Return

                Dim dataString As String

                'Decode data
                Try
                    dataString = Encoding.UTF8.GetString(data)
                Catch ex As ArgumentException
                    OnFail(failResult) : Return
                End Try

                'Check for expected prefix
                If Not dataString.StartsWithI(OpenKeyval.Prefix) Then OnFail(failResult) : Return

                _Value = dataString.Substring(OpenKeyval.Prefix.Length)
                OnSuccess()
            End Sub

            Public Overrides ReadOnly Property Value As String
                Get
                    Return _Value
                End Get
            End Property

        End Class

        Friend Class OpenKeyvalStore : Inherits KeyValueRequest

            Private Key As String
            Private _Value As String

            Public Sub New(ByVal url As Uri, ByVal key As String, ByVal value As String)
                Me.Key = key
                _Value = value
                Me.Url = url
            End Sub

            Public Overrides Sub Start()
                Dim header As Byte() = Encoding.UTF8.GetBytes(UrlEncode(Key) & "=")

                'Compress data
                Dim compressedValue As Byte() = UrlEncodeToBytes(Compress(Encoding.UTF8.GetBytes(Value)))
                Dim data(header.Length + compressedValue.Length) As Byte

                Array.Copy(header, 0, data, 0, header.Length)
                Array.Copy(compressedValue, 0, data, header.Length, compressedValue.Length)

                MyBase.Data = data
                MyBase.Start()

                If Result.IsError Then OnFail(Result) : Return
                OnSuccess()
            End Sub

            Public Overrides ReadOnly Property Value As String
                Get
                    Return _Value
                End Get
            End Property

        End Class

    End Class

End Namespace