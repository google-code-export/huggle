Imports System.Collections.Generic

Namespace Huggle

    Friend Class RightsChange : Inherits LogItem

        'Represents a user rights change

        Public Sub New(ByVal id As Integer, ByVal wiki As Wiki)
            MyBase.New(id, wiki)
        End Sub

        Public Property PrevRights As List(Of String)

        Public Property Rights() As List(Of String)

        Public Overrides ReadOnly Property Target() As String
            Get
                Return TargetUser.Name
            End Get
        End Property

    End Class

End Namespace