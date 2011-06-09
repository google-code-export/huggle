Imports Huggle.UI
Imports System.Collections.Generic
Imports System.Windows.Forms

Namespace Huggle.Queries

    Friend Class ReviewRevision : Inherits Query

        Private _Rev As Revision

        Public Sub New(ByVal session As Session, ByVal rev As Revision)
            MyBase.New(session, Msg("review-desc"))
            _Rev = rev
        End Sub

        Public Property Levels() As Dictionary(Of ReviewFlag, Integer)

        Public ReadOnly Property Rev() As Revision
            Get
                Return _Rev
            End Get
        End Property

        Public Property Summary() As String

        Public Property Watch() As WatchAction

        Public Overrides Sub Start()
            If User.Can("review") Then
                If User.Can("quickreview") AndAlso Wiki.Config.QuickReview Then
                    If Levels Is Nothing Then Levels = Wiki.Config.QuickReviewLevels
                    If Summary Is Nothing AndAlso Wiki.Config.ReviewComments Then Summary = Wiki.Config.QuickReviewComment
                End If

                If Levels Is Nothing Then
                    If Interactive Then
                        Using reviewform As New ReviewForm(Session, Rev)
                            If reviewform.ShowDialog = DialogResult.Cancel Then OnFail(Msg("error-cancelled")) : Return
                            Levels = reviewform.Levels
                            Summary = reviewform.Comment
                        End Using
                    Else
                        OnFail(Msg("review-quickunavailable")) : Return
                    End If
                End If

                'Submit the review
                OnProgress(Msg("review-progress", Rev.Page))

                'Get token
                If Not Session.HasTokens Then
                    Dim tokenQuery As New TokenQuery(Session)
                    tokenQuery.Start()
                    If tokenQuery.IsErrored Then OnFail(tokenQuery.Result) : Return
                End If

                'Create query string
                Dim query As New QueryString(
                    "action", "review",
                    "revid", Rev,
                    "comment", Summary,
                    "token", Session.Tokens("review"))

                For Each level As KeyValuePair(Of ReviewFlag, Integer) In Levels
                    query.Add("flag_" & level.Key.Name, level.Value)
                Next level

                'Submit the review
                Dim req As New ApiRequest(Session, Description, query)
                req.Start()
                If req.Result.IsError Then OnFail(req.Result.Message) : Return

                'Watch the page
                Dim w As New Watch(Session, Rev.Page, Watch)
                w.Start()

                OnSuccess()
                Return

            ElseIf User.Can("patrolnew") Then
                Dim req As New Patrol(Session, Rev.Page)
                req.Watch = Watch
                req.Start()

            ElseIf User.Can("patrol") Then
                Dim req As New Patrol(Session, Rev)
                req.Watch = Watch
                req.Start()

            Else
                OnFail(Msg("review-unavailable"))
            End If
        End Sub

    End Class

End Namespace
