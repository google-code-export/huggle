Imports System.Collections.Generic
Imports System.Web.HttpUtility

Namespace Huggle.Queries

    Class ExpandTemplatesQuery : Inherits Query

        Private Items As List(Of String), Page As Page

        Public Sub New(ByVal session As Session, ByVal items As List(Of String), Optional ByVal page As Page = Nothing)
            MyBase.New(session, Msg("expandtemplates-desc"))
            Me.Items = items
            Me.Page = page
        End Sub

        Public Overrides Sub Start()
            OnProgress(Msg("expandtemplates-progress"))
            OnStarted()

            Dim Request As New ApiRequest(Session, Description, New QueryString(
                "action", "expandtemplates",
                "text", Items.Join(Separator),
                "title", Page))

            Request.Start()
            If Request.Result.IsError Then OnFail(Request.Result) : Return
            OnSuccess()
        End Sub

        Public Shared ReadOnly Property Separator() As String
            Get
                'Item separator, to expand multiple snippets in one request
                Return "__FNORD__"
            End Get
        End Property

    End Class

End Namespace
