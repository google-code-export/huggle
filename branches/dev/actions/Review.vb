Imports Huggle.Queries
Imports System.Collections.Generic
Imports System.Windows.Forms

Namespace Huggle.Actions

    Public Class Review : Inherits Query

        Private _Levels As Dictionary(Of ReviewFlag, Integer)
        Private _Rev As Revision
        Private _Summary As String
        Private _Watch As WatchAction

        Public Sub New(ByVal session As Session, ByVal rev As Revision)
            MyBase.New(session, Msg("review-desc"))
            _Rev = rev
        End Sub

        Public Property Levels() As Dictionary(Of ReviewFlag, Integer)
            Get
                Return _Levels
            End Get
            Set(ByVal value As Dictionary(Of ReviewFlag, Integer))
                _Levels = value
            End Set
        End Property

        Public ReadOnly Property Rev() As Revision
            Get
                Return _Rev
            End Get
        End Property

        Public Property Summary() As String
            Get
                Return _Summary
            End Get
            Set(ByVal value As String)
                _Summary = value
            End Set
        End Property

        Public Property Watch() As WatchAction
            Get
                Return _Watch
            End Get
            Set(ByVal value As WatchAction)
                _Watch = value
            End Set
        End Property

        Public Overrides Sub Start()
            If User.Can("review") Then
                If User.Can("quickreview") AndAlso Wiki.Config.QuickReview Then
                    If Levels Is Nothing Then Levels = Wiki.Config.QuickReviewLevels
                    If Summary Is Nothing AndAlso Wiki.Config.ReviewComments Then Summary = Wiki.Config.QuickReviewComment
                End If

                If Levels Is Nothing Then
                    If Interactive Then
                        Using reviewform As New ReviewForm(Rev)
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
                If Session.EditToken Is Nothing Then
                    Dim tokenQuery As New TokenQuery(Session)
                    tokenQuery.Start()
                    If tokenQuery.IsErrored Then OnFail(tokenQuery.Result) : Return
                End If

                'Create query string
                Dim query As New QueryString( _
                    "action", "review", _
                    "revid", Rev, _
                    "comment", Summary, _
                    "token", Session.EditToken)

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
                Dim req As New PatrolQuery(User, Rev.Page, User.Config.IsWatch("patrol"))
                req.Start()

            ElseIf User.Can("patrol") Then
                Dim req As New PatrolQuery(User, Rev, Watch:=User.Config.IsWatch("patrol"))
                req.Start()

            Else
                OnFail(Msg("review-unavailable"))
            End If
        End Sub

    End Class

End Namespace
