Imports System.Collections.Generic
Imports System.Web.HttpUtility

Namespace Huggle

    'Represents anything that reviews a revision (patrolling, 'flagged revisions', etc.)

    Friend Class Review : Inherits LogItem

        Public Sub New(ByVal id As Integer, ByVal wiki As Wiki)
            MyBase.New(id, wiki)
        End Sub

        Public Property IsAutomatic() As Boolean

        Public Property Revision() As Revision

        Public Property Levels() As Dictionary(Of ReviewFlag, Integer)

        Public Property Type() As String

        Public Overrides ReadOnly Property Target() As String
            Get
                If Revision Is Nothing Then Return Nothing Else Return Revision.Page.Name
            End Get
        End Property

        Private Function GetLevels(ByVal str As String) As Dictionary(Of ReviewFlag, Integer)
            Dim result As New Dictionary(Of ReviewFlag, Integer)
            If str Is Nothing Then Return result

            If str.Contains("[") AndAlso str.Contains("]") Then
                For Each item As String In str.FromLast("[").ToLast("]").Split(",")
                    If item.Contains(":") Then result.Merge(
                        Wiki.Config.ReviewFlags(item.ToFirst(":").Trim), CInt(item.FromFirst(":").Trim))
                Next item
            End If

            Return result
        End Function

    End Class

End Namespace
