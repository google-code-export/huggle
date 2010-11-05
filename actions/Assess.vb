Imports System.Collections.Generic

Namespace Huggle.Actions

    Friend Class Assess : Inherits Query

        Private Page As Page
        Private Ratings As List(Of String)

        Friend Sub New(ByVal session As Session, ByVal page As Page, ByVal ratings As List(Of String))
            MyBase.New(session, Msg("assess-desc", page))
            Me.Page = page
            Me.Ratings = ratings
        End Sub

        Friend Overrides Sub Start()
            If Page.Id = 0 OrElse Page.LastRev Is Nothing OrElse Not Page.ExistsKnown Then
                Dim infoQuery As New PageDetailQuery(Session, Page, Content:=False)
                infoQuery.Start()
                If infoQuery.IsFailed Then OnFail(infoQuery.Result) : Return
            End If

            If Not Page.Exists Then OnFail(Msg("assess-notexist", Page)) : Return

            Dim query As New QueryString(
                "action", "articleassessment",
                "pageid", Page.Id,
                "revid", Page.LastRev.Id)

            For i As Integer = 0 To Ratings.Count - 1
                query.Add("r" & (i + 1).ToStringI, Ratings(i))
            Next i

            Dim assessReq As New ApiRequest(Session, Description, query)

            assessReq.Start()
            If assessReq.IsFailed Then OnFail(assessReq.Result) : Return

            OnSuccess()
        End Sub

    End Class

End Namespace
