Imports Huggle.Wikitext
Imports System.Collections.Generic
Imports System.Globalization

Namespace Huggle.Actions

    Friend Class Warning : Inherits Query

        'Handles issuing a warning for a specified revision

        Private Shared All As New List(Of Warning)

        Private _Rev As Revision
        Private _Type As SanctionType

        Friend Sub New(ByVal session As Session, ByVal rev As Revision, ByVal type As SanctionType)
            MyBase.New(session, Msg("warn-desc"))

            _Rev = rev
            _Type = type
        End Sub

        Friend ReadOnly Property Rev() As Revision
            Get
                Return _Rev
            End Get
        End Property

        Friend Property Type() As SanctionType
            Get
                Return _Type
            End Get
            Private Set(ByVal value As SanctionType)
                _Type = value
            End Set
        End Property

        Friend Overrides Sub Start()
            'Check user's status
            If Rev.User.IsBlocked Then OnFail(Msg("warn-alreadyblocked")) : Return

            If Rev.User.IsReported Then
                For Each sanction As Sanction In Rev.User.Sanctions
                    If sanction.Type.Name = "report" AndAlso sanction.Type.Subtype = "vandal" Then
                        OnFail(Msg("warn-alreadyreported"))

                        If User.Can("block") AndAlso Interactive Then
                            'Blocking a reported user
                            If User.Config.ConfirmBlockReported Then
                                If App.ShowPrompt(Msg("warn-prompt", Rev.User), Msg("warn-reported", Rev.User), _
                                    Nothing, 1, Msg("warn-block"), Msg("cancel")) = 2 Then Return
                            End If

                            Dim block As New Block(Session, Rev.User, Rev.Wiki.Config.BlockSummary)
                            block.Start()
                        End If

                        Return
                    End If
                Next sanction
            End If

            If Rev.IsWarnedFor Then OnFail(Msg("warn-alreadywarned")) : Return
            If Rev.User.IsIgnored Then OnFail(Msg("warn-ignored")) : Return

            If Rev.User.Sanction.Level = Rev.Wiki.Config.SanctionLevels Then
                OnFail(Msg("warn-reported"))

                If Interactive Then
                    'Reporting / blocking a user with a final warning
                    Dim answer As String

                    If User.Can("block") AndAlso Not User.Config.ConfirmBlock Then
                        answer = Msg("warn-block")
                    ElseIf Not User.Can("block") AndAlso User.Can("report") AndAlso Not User.Config.ConfirmReport Then
                        answer = Msg("warn-report")
                    Else
                        Dim options As New List(Of String)
                        If User.Can("report") Then options.Add(Msg("warn-report"))
                        If User.Can("block") Then options.Add(Msg("warn-block"))
                        If options.Count = 0 Then Return
                        options.Add(Msg("cancel"))

                        answer = options(App.ShowPrompt(Msg("warn-prompt"), _
                            Msg("warn-alreadywarned", Rev.User), Nothing, 1, options.ToArray) - 1)
                    End If

                    Select Case answer
                        Case Msg("warn-report")
                            'Dim report As New VandalReport(Session, Rev.User, "vandal")
                            'report.Start()

                        Case Msg("warn-block")
                            Dim block As New Block(Session, Rev.User)
                            block.Start()
                    End Select
                End If

                Return
            End If

        End Sub

        Private Sub DoWarning()
            'Retrieve text of user's talk page
            If Rev.User.Talkpage.Text Is Nothing OrElse Not Rev.User.Userpage.LogsKnown Then
                'Dim DetailQuery As New UserDetailQuery(User, Rev.User)
                'DetailQuery.Start()

                'If DetailQuery.Result.IsError Then
                '    OnFail(DetailQuery.Result.Message)
                '    Return
                'End If
            End If

            'Don't warn for old edits
            If Rev.User.LastEdit IsNot Nothing AndAlso Rev.User.LastSanctionTime > Rev.User.LastEdit.Time Then
                OnFail(Msg("warn-oldedit"))
                Return
            End If

            If Type.Level = 0 Then
                If Rev.User.Sanction.Level < Rev.Wiki.Config.WarningLevels Then
                    Type = New SanctionType(Type.Name, Type.Subtype, Rev.User.Sanction.Level + 1)
                Else
                    If Rev.User.IsBlocked Then OnFail(Msg("warn-alreadyblocked"))
                    If Rev.User.IsReported Then OnFail(Msg("warn-alreadyreported"))
                End If
            End If

            'Check warning exists in wiki's config
            If Not Rev.Wiki.Config.WarningMessages.ContainsKey(Type) Then
                OnFail(Msg("warn-unknown", Type))
                Return
            End If

            Dim i As Integer = 0

            While i < All.Count - 1
                If All(i).Rev.Page Is Rev.Page Then All.RemoveAt(i) Else i += 1
            End While

            All.Add(Me)

            Dim monthName As String = DateTimeFormatInfo.InvariantInfo.MonthNames(Wiki.ServerTime.Month - 1)
            Dim summary As String = Rev.Wiki.Config.WarningSummaries(Type.Level).FormatForUser(Rev.Page)
            Dim warning As String = Rev.Wiki.Config.WarningMessages(Type)

            Dim document As New Document(Rev.User.Talkpage, Rev.User.Talkpage.Text)
            If Not document.Sections.Contains(monthName) Then document.Sections.Append(monthName, Nothing)

            document.Sections(monthName).Append(LF & warning.FormatForUser(Rev.Page, _
                "{{" & Rev.Wiki.MagicWords("fullurle") & Rev.Page.Name & "|diff=" & CStr(Rev.Id) & "}}"))

            OnStarted()

            Dim edit As New Edit(Session, Rev.User.Talkpage, document.Text, summary.FormatForUser(Rev.Page))
            edit.Conflict = ConflictAction.Retry
            edit.Minor = Wiki.Config.IsMinor("warning")
            edit.Watch = If(User.Config.IsWatch("warning"), WatchAction.Watch, WatchAction.NoChange)

            Do
                edit.Start()
            Loop Until Not edit.ConflictRetry

            If edit.IsFailed Then OnFail(edit.Result) : Return

            If All.Contains(Me) Then All.Remove(Me)
            OnSuccess()
        End Sub

    End Class

End Namespace
