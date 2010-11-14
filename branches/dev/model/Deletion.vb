Imports System

Namespace Huggle

    'Represents deletion/restoration of a page

    Friend Class Deletion : Inherits LogItem

        Public Sub New(ByVal id As Integer, ByVal wiki As Wiki)
            MyBase.New(id, wiki)
        End Sub

        Public Overrides ReadOnly Property Icon() As Drawing.Image
            Get
                Return Resources.blob_log_delete
            End Get
        End Property

        Public Overrides ReadOnly Property Target() As String
            Get
                Return Page.Name
            End Get
        End Property

        Protected Overrides Sub OnSetPage()
            Select Case Action
                Case "delete/delete"
                    Page.Exists = False
                    Page.DeletedEditsKnown = False
                    Page.HasDeletedEdits = True

                Case "delete/restore"
                    Page.Exists = True
            End Select
        End Sub

    End Class

End Namespace