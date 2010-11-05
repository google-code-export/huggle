Imports System.Collections.Generic
Imports System.Web.HttpUtility

Namespace Huggle

    'Represents anything that reviews a revision (patrolling, 'flagged revisions', etc.)

    Friend Class Review : Inherits LogItem

        Private _Auto As Boolean
        Private _Levels As Dictionary(Of ReviewFlag, Integer)
        Private _Revision As Revision
        Private _Type As String

        Friend Sub New(ByVal time As Date, ByVal revision As Revision, ByVal user As User, _
            ByVal type As String, ByVal auto As Boolean, ByVal comment As String, ByVal id As Integer, ByVal rcid As Integer)

            MyBase.New(user.Wiki, Id, rcid)
            Me.Action = "review"
            Me.Comment = Comment
            Me.Time = time
            Me.User = user

            _Auto = Auto
            _Revision = revision
            _Levels = GetLevels(HtmlDecode(Comment))
            _Type = type

            If Comment IsNot Nothing AndAlso Comment.Contains("[") Then Comment = Comment.ToFirst("[").Trim

            If revision IsNot Nothing Then
                _Revision.Review = Me
                _Revision.IsReviewed = True
            End If
        End Sub

        Friend ReadOnly Property Auto() As Boolean
            Get
                Return _Auto
            End Get
        End Property

        Friend ReadOnly Property Revision() As Revision
            Get
                Return _Revision
            End Get
        End Property

        Friend ReadOnly Property Levels() As Dictionary(Of ReviewFlag, Integer)
            Get
                Return _Levels
            End Get
        End Property

        Friend ReadOnly Property Type() As String
            Get
                Return _Type
            End Get
        End Property

        Friend Overrides ReadOnly Property Target() As String
            Get
                If Revision Is Nothing Then Return Nothing Else Return Revision.Page.Name
            End Get
        End Property

        Private Function GetLevels(ByVal str As String) As Dictionary(Of ReviewFlag, Integer)
            Dim result As New Dictionary(Of ReviewFlag, Integer)
            If str Is Nothing Then Return result

            If str.Contains("[") AndAlso str.Contains("]") Then
                For Each item As String In str.FromLast("[").ToLast("]").Split(",")
                    If item.Contains(":") Then result.Merge(Wiki.Config.ReviewFlags(item.ToFirst(":").Trim), CInt(item.FromFirst(":").Trim))
                Next item
            End If

            Return result
        End Function

    End Class

End Namespace
