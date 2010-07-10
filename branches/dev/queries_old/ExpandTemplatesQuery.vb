Imports System.Collections.Generic
Imports System.Web.HttpUtility

Namespace Huggle.Actions

    Class ExpandTemplatesQuery : Inherits Query

        Private Items As List(Of String), Page As Page

        Public Sub New(ByVal session As Session, ByVal Items As List(Of String), Optional ByVal Page As Page = Nothing)
            MyBase.New(session, Msg("expandtemplates-desc"))
            Me.Items = Items
            Me.Page = Page
        End Sub

        Public Overrides Sub Start()
            OnProgress(Msg("expandtemplates-progress"))
            OnStarted()

            Dim Request As New ApiRequest(Session, Description, New QueryString( _
                "action", "expandtemplates", _
                "text", Items.Join(Separator), _
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
