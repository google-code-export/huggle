'Imports System.Collections.Generic
'Imports System.Web.HttpUtility

'Namespace Huggle.Queries

'    'Retrieve one or more revisions

'    Class RevisionDetailQuery : Inherits OldQuery

'        Private _Revision As Integer, _Section As String
'        Private Revisions As New List(Of Revision)
'        Private Pages As List(Of Page), _Results As New Dictionary(Of Page, Revision)
'        Private _Content As Boolean

'        Public Sub New(ByVal user As User, ByVal Page As Page, Optional ByVal Revision As Integer = 0, _
'            Optional ByVal Section As String = Nothing, Optional ByVal Content As Boolean = True)

'            MyBase.New(user)
'            Pages.Add(Page)
'            _Revision = Revision
'            _Section = Section
'            _Content = Content
'        End Sub

'        Public Sub New(ByVal user As User, ByVal Revision As Revision, Optional ByVal Section As String = Nothing)
'            MyBase.New(user)
'            Pages.Add(Revision.Page)
'            _Revision = Revision.Id
'            _Section = Section
'            _Content = True
'        End Sub

'        Public Sub New(ByVal user As User, ByVal Revisions As List(Of Revision), Optional ByVal Content As Boolean = True)
'            MyBase.New(user)
'            Me.Revisions = Revisions
'            _Content = Content
'        End Sub

'        Public Sub New(ByVal user As User, ByVal Pages As List(Of Page), Optional ByVal Content As Boolean = True)
'            MyBase.New(user)
'            Me.Pages = Pages
'            _Content = Content
'        End Sub

'        Public Sub New(ByVal user As User, ByVal ParamArray Pages() As Page)
'            MyBase.New(user)
'            Me.Pages = Pages.ToList
'            _Content = True
'        End Sub

'        Public ReadOnly Property Results() As Dictionary(Of Page, Revision)
'            Get
'                Return _Results
'            End Get
'        End Property

'        Protected Overrides Function Process() As Result

'            Dim query As New QueryString( _
'                "action", "query", _
'                "prop", "info|revisions", _
'                "rvsection", _Section, _
'                "rvprop", "flags|ids|timestamp|user|size|comment")

'            If Pages IsNot Nothing Then
'                Dim titles As New List(Of String)

'                For Each page As Page In Pages
'                    titles.Add(page.Title)
'                Next page

'                query.Add("titles", titles.Join("|"))
'                query.Add("rvlimit", 1)
'            End If

'            If Revisions IsNot Nothing Then
'                Dim ids As New List(Of String)

'                For Each rev As Revision In Revisions
'                    ids.Add(CStr(rev.Id))
'                Next rev

'                query.Add("revids", ids.Join("|"))
'            End If

'            If _Content Then query.Add("rvprop", "flags|ids|timestamp|user|size|comment|content")

'            Dim request As New ApiRequest(User, query)
'            request.Start()

'            If request.Result.IsError Then Return request.Result

'            Return Result.Success
'        End Function

'    End Class

'End Namespace