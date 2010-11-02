Namespace Huggle.Actions

    Public Class Edit : Inherits Query

        Private _ConflictRetry As Boolean
        Private _Page As Page
        Private _Text As String

        Private Retrying As Boolean

        Public Sub New(ByVal session As Session, ByVal page As Page, ByVal text As String, ByVal summary As String)
            MyBase.New(session, Msg("edit-desc", page))
            _Page = page
            _Summary = summary
            _Text = text
        End Sub

        Public Property AllowCreate() As Boolean

        Public Property AllowRecreate() As Boolean

        Public Property Bot() As Boolean

        Public Property Conflict() As ConflictAction

        Public ReadOnly Property ConflictRetry() As Boolean
            Get
                Return _ConflictRetry
            End Get
        End Property

        Public Property CreateOnly() As Boolean

        Public Property Minor() As Boolean

        Public Property NewSection() As Boolean
            Get
                Return (Section = "new")
            End Get
            Set(ByVal value As Boolean)
                If value Then Section = "new" Else Section = Nothing
            End Set
        End Property

        Public ReadOnly Property Page() As Page
            Get
                Return _Page
            End Get
        End Property

        Public Property Section() As String

        Public Property Summary() As String

        Public ReadOnly Property Text() As String
            Get
                Return _Text
            End Get
        End Property

        Public Property UseSummaryTag() As Boolean

        Public Property Watch() As WatchAction

        Public Overrides Sub Start()
            'Abort if blocked
            If User.IsBlocked Then OnFail(Msg("error-blocked", User.CurrentBlock.Comment)) : Return

            _ConflictRetry = False
            If Page.LastRev IsNot Nothing AndAlso Page.LastRev.Text = Text Then OnFail(Msg("edit-nochange")) : Return

            OnStarted()

            'Get token
            If Session.EditToken Is Nothing Then
                Dim tokenQuery As New TokenQuery(Session)
                tokenQuery.Start()
                If tokenQuery.IsErrored Then OnFail(tokenQuery.Result) : Return
            End If

            Dim startTime As Date

            'Get last revision timestamp if needed
            If Conflict <> ConflictAction.Ignore AndAlso Page.Exists AndAlso _
                (Page.LastRev Is Nothing OrElse Page.LastRev.StartTime = Date.MinValue) Then

                Dim timeQuery As New PageDetailQuery(Session, Page)
                timeQuery.Start()
                If timeQuery.Result.IsError Then OnFail(timeQuery.Result.Message) : Return

                If Page.Exists AndAlso Page.LastRev.StartTime = Date.MinValue Then Page.LastRev.StartTime = Wiki.ServerTime
            End If

            If UseSummaryTag AndAlso Wiki.Config.SummaryTag IsNot Nothing Then Summary &= " " & Wiki.Config.SummaryTag

            'Create query string
            Dim query As New QueryString( _
                "action", "edit", _
                "title", Page, _
                "text", _Text, _
                "summary", Summary, _
                "token", Session.EditToken, _
                "section", Section, _
                "watchlist", Watch.ToString.ToLowerI)

            If Bot Then query.Add("bot")
            If Minor Then query.Add("minor") Else query.Add("notminor")
            If Not AllowCreate Then query.Add("nocreate")
            If AllowRecreate Then query.Add("recreate")
            If CreateOnly Then query.Add("createonly")

            If Conflict <> ConflictAction.Ignore Then
                If Page.Exists AndAlso Page.LastRev IsNot Nothing Then query.Add("basetimestamp", WikiTimestamp(Page.LastRev.Time))
                query.Add("starttimestamp", WikiTimestamp(startTime))
            End If

            'Edit the page
            Dim req As New ApiRequest(Session, Description, query)
            req.Start()

            'If edit token is bad, clear it and retry
            If req.Result.Code = "badtoken" AndAlso Not Retrying Then
                Log.Debug("Retrying action due to bad token")
                Session.EditToken = Nothing
                Retrying = True
                Start()
                Return
            End If

            'Handle edit conflicts
            If req.Result.Code = "editconflict" Then
                Select Case Conflict
                    Case ConflictAction.Abort
                        OnFail(Msg("edit-conflict")) : Return

                    Case ConflictAction.Prompt
                        If Not Interactive Then OnFail(Msg("edit-conflict")) : Return

                        Select Case App.ShowPrompt(Msg("edit-conflict"), Msg("edit-conflictdetail", Page), _
                            Nothing, 1, Msg("edit-retry"), Msg("edit-ignore"), Msg("cancel"))

                            Case 1
                                _ConflictRetry = True
                                OnFail(Msg("edit-conflict-retry")) : Return

                            Case 2
                                Conflict = ConflictAction.Ignore
                                Retrying = True
                                Start()
                                Return

                            Case 3
                                OnFail(Msg("error-cancelled")) : Return
                        End Select

                    Case ConflictAction.Retry
                        _ConflictRetry = True
                        OnFail(Msg("edit-conflict-retry")) : Return
                End Select
            End If

            If req.Result.IsError Then OnFail(req.Result.Message) : Return
            If Watch = WatchAction.Watch Then User.Watchlist.Merge(Page.Title)

            OnSuccess()
        End Sub

    End Class

End Namespace
