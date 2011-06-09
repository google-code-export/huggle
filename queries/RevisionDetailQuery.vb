Imports System.Collections.Generic
Imports System.Web.HttpUtility

Namespace Huggle.Queries

    'Retrieve one or more revisions

    Class RevisionDetailQuery : Inherits Query

        Private _Content As Boolean
        Private _Results As New Dictionary(Of Page, Revision)
        Private _Revision As Integer
        Private _Section As String

        Private Pages As List(Of Page)
        Private Revisions As New List(Of Revision)

        Public Sub New(ByVal session As Session, ByVal Page As Page, Optional ByVal Revision As Integer = 0,
            Optional ByVal Section As String = Nothing, Optional ByVal Content As Boolean = True)

            MyBase.New(session, Msg("revisiondetail-desc"))
            Pages.Add(Page)
            _Revision = Revision
            _Section = Section
            _Content = Content
        End Sub

        Public Sub New(ByVal session As Session, ByVal Revision As Revision, Optional ByVal Section As String = Nothing)
            MyBase.New(session, Msg("revisiondetail-desc"))
            Pages.Add(Revision.Page)
            _Revision = Revision.Id
            _Section = Section
            _Content = True
        End Sub

        Public Sub New(ByVal session As Session, ByVal Revisions As List(Of Revision), Optional ByVal Content As Boolean = True)
            MyBase.New(session, Msg("revisiondetail-desc"))
            Me.Revisions = Revisions
            _Content = Content
        End Sub

        Public Sub New(ByVal session As Session, ByVal Pages As List(Of Page), Optional ByVal Content As Boolean = True)
            MyBase.New(session, Msg("revisiondetail-desc"))
            Me.Pages = Pages
            _Content = Content
        End Sub

        Public Sub New(ByVal session As Session, ByVal ParamArray Pages() As Page)
            MyBase.New(session, Msg("revisiondetail-desc"))
            Me.Pages = Pages.ToList
            _Content = True
        End Sub

        Public ReadOnly Property Results() As Dictionary(Of Page, Revision)
            Get
                Return _Results
            End Get
        End Property

        Public Overrides Sub Start()
            OnStarted()

            Dim query As New QueryString(
                "action", "query",
                "prop", "info|revisions",
                "rvsection", _Section,
                "rvprop", "flags|ids|timestamp|user|size|comment")

            If Pages IsNot Nothing Then
                Dim titles As New List(Of String)

                For Each page As Page In Pages
                    titles.Add(page.Title)
                Next page

                query.Add("titles", titles.Join("|"))
                query.Add("rvlimit", 1)
            End If

            If Revisions IsNot Nothing Then
                Dim ids As New List(Of String)

                For Each rev As Revision In Revisions
                    ids.Add(CStr(rev.Id))
                Next rev

                query.Add("revids", ids.Join("|"))
            End If

            If _Content Then query.Add("rvprop", "flags|ids|timestamp|user|size|comment|content")

            Dim req As New ApiRequest(Session, Description, query)
            req.Start()
            If req.IsErrored Then OnFail(req.Result) : Return

            OnSuccess()
        End Sub

    End Class

End Namespace