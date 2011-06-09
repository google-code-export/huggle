Imports System

Namespace Huggle.Queries

    Friend Class ThreadQuery : Inherits ListQuery

        Public Sub New(ByVal session As Session)
            MyBase.New(session, "list", "threads", "th", New QueryString("thdir", "older",
                "thprop", "id|subject|page|parent|ancestor|created|modified|author|summaryid|rootid|type"), Msg("thread-desc"))
        End Sub

    End Class

End Namespace
