Imports System.Collections.Generic

Namespace Huggle.Actions

    'Parse wikitext of a specified page or revision

    Class ParseRevisionQuery : Inherits Query

        Private _Page As Page
        Private _Rev As Revision

        Public Sub New(ByVal session As Session, ByVal page As Page)
            MyBase.New(session, Msg("parse-desc"))
            _Page = page
        End Sub

        Public Sub New(ByVal session As Session, ByVal rev As Revision)
            MyBase.New(session, Msg("parse-desc"))
            _Rev = rev
        End Sub

        Public ReadOnly Property Page() As Page
            Get
                Return _Page
            End Get
        End Property

        Public ReadOnly Property Rev() As Revision
            Get
                Return _Rev
            End Get
        End Property

        Public Overrides Sub Start()
            OnProgress(Msg("parse-progress"))
            OnStarted()

            Dim req As New ApiRequest(Session, Description, New QueryString( _
                "action", "parse", _
                "page", Page, _
                "oldid", Rev))

            req.Start()
            If req.IsFailed Then OnFail(req.Result) : Return

            OnSuccess()
        End Sub

    End Class

End Namespace
