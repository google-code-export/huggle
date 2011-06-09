Imports System

Namespace Huggle.Net

    'When your data is in the Cloud(tm), you don't have to worry about anything!
    '(except for the free storage service that you used because you were too 
    'lazy/broke to host your own backend)

    Friend Interface IKeyValueStore

        Function LoadData(ByVal key As String) As KeyValueRequest
        Function SaveData(ByVal key As String, ByVal value As String) As KeyValueRequest

    End Interface

    Friend MustInherit Class KeyValueRequest : Inherits Request

        Public MustOverride ReadOnly Property Value As String

    End Class

    Friend Class SharedStorage

        Private Shared Provider As IKeyValueStore

        Shared Sub New()
            Provider = New OpenKeyval(New Uri("http://api.openkeyval.org/"))
        End Sub

        Public Shared Function GetProvider() As IKeyValueStore
            Return Provider
        End Function

    End Class

End Namespace
