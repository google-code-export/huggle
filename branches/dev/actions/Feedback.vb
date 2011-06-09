Imports Huggle.Net
Imports System.Collections.Generic

Namespace Huggle.Queries

    Friend Class Feedback : Inherits Query

        Private _Page As Page
        Private Ratings As List(Of String)

        Public Sub New(ByVal session As Session, ByVal page As Page, ByVal ratings As List(Of String))
            MyBase.New(session, Msg("feedback-desc", page))

            ThrowNull(page, "page")
            _Page = page

            Me.Ratings = ratings
        End Sub

        Public ReadOnly Property Page As Page
            Get
                Return _Page
            End Get
        End Property

        Public Overrides Sub Start()
            If Page.Id = 0 OrElse Page.LastRev Is Nothing OrElse Not Page.ExistsKnown Then
                Dim infoQuery As New PageDetailQuery(Session, Page)
                infoQuery.Content = False
                infoQuery.Start()
                If infoQuery.IsFailed Then OnFail(infoQuery.Result) : Return
            End If

            If Not Page.Exists Then OnFail(Msg("feedback-notexist", Page)) : Return

            Dim query As New QueryString(
                "action", "articlefeedback",
                "pageid", Page.Id,
                "revid", Page.LastRev.Id)

            For i As Integer = 0 To Ratings.Count - 1
                query.Add("r" & (i + 1).ToStringI, Ratings(i))
            Next i

            Dim req As New ApiRequest(Session, Description, query)
            req.Start()
            If req.IsFailed Then OnFail(req.Result) : Return

            OnSuccess()
        End Sub

    End Class

End Namespace
