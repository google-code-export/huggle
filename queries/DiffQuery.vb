﻿Imports Huggle.Net
Imports System.Web.HttpUtility

Namespace Huggle.Queries

    'Retrieve a diff

    Class DiffQuery : Inherits Query

        Private _Diff As Diff

        Public Sub New(ByVal session As Session, ByVal diff As Diff)
            MyBase.New(session, Msg("diff-desc"))
            _Diff = diff
        End Sub

        Public ReadOnly Property Diff() As Diff
            Get
                Return _Diff
            End Get
        End Property

        Public Overrides Sub Start()
            OnProgress(Msg("diff-progress", Diff.Page))
            OnStarted()

            Dim query As New QueryString(
                "action", "render",
                "title", Diff.Page,
                "diff", Diff.NewId,
                "oldid", Diff.OldId,
                "diffonly", True)

            Dim req As New UIRequest(Session, Description, query, Nothing)
            req.Start()

            If req.IsFailed Then
                Diff.CacheState = CacheState.NotCached
                OnFail(req.Result)
                Return
            End If

            OnSuccess()
        End Sub

    End Class

End Namespace