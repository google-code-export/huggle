Imports System.Collections.Generic

Namespace Huggle.Queries

    Class RevisionInfoQuery : Inherits Query

        Private Revisions As List(Of Revision)

        Public Sub New(ByVal session As Session, ByVal ParamArray revisions() As Revision)
            MyBase.New(session, Msg("revisioninfo-desc"))

            Me.Revisions = revisions.ToList
        End Sub

        Public Overrides Sub Start()
            OnStarted()

            Dim revIds As String = ""

            For Each rev As Revision In Revisions
                revIds &= rev.Id.ToStringI & "|"
            Next rev

            revIds = revIds.Substring(0, revIds.Length - 1)

            Dim req As New ApiRequest(Session, Description, New QueryString(
                "action", "query",
                "prop", "info|revisions",
                "revids", revIds,
                "rvprop", "ids|flags|timestamp|user|size|comment"))

            req.Start()
            If req.Result.IsError Then OnFail(req.Result) : Return

            OnSuccess()
        End Sub

    End Class

End Namespace
