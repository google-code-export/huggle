Namespace Huggle

    Friend Class GlobalUserStatusChange : Inherits LogItem

        'Represents a change in the status of a global user account created by MediaWiki's CentralAuth extension

        Private _Target As GlobalUser

        Friend Sub New(ByVal wiki As Wiki, ByVal comment As String, ByVal id As Integer, ByVal rcid As Integer,
            ByVal target As GlobalUser, ByVal time As Date, ByVal user As User)

            MyBase.New(wiki, id, rcid)
            Me.Action = "globalauth/setstatus"
            Me.Comment = comment
            Me.Time = time
            Me.User = user

            _Target = target
        End Sub

        Friend Overrides ReadOnly Property Target As String
            Get
                Return _Target.Name
            End Get
        End Property

    End Class

End Namespace