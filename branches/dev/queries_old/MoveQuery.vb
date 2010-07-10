Namespace Huggle.Queries.Actions

    Class MoveQuery : Inherits OldQuery

        'Move a page

        Private Comment, Source, Destination As String, MoveTalk, Redirect, Watch As Boolean

        Public Sub New(ByVal Account As User, ByVal Page As Page, ByVal Destination As String, ByVal Comment As String, _
             ByVal MoveTalk As Boolean, ByVal Redirect As Boolean, ByVal Watch As Boolean)

            MyBase.New(Account)
            Me.Comment = Comment
            Me.Destination = Destination
            Me.MoveTalk = MoveTalk
            Me.Source = Page.Title
            Me.Redirect = Redirect
            Me.Watch = Watch
        End Sub

        Protected Overrides Function Process() As Result
            Dim FailMsg As String = Msg("move-fail", Source, Destination)

            DoProgress(Msg("move-progress", Source, Destination), "move-" & Source)

            'Get token
            If User.Token Is Nothing Then
                Result = (New TokenQuery(User)).Do
                If Result.IsError Then Return Result.FailWith(FailMsg)
            End If

            'Create query string
            Dim Qs As New QueryString( _
                "action", "move", _
                "from", Source, _
                "to", Destination, _
                "reason", Comment, _
                "token", User.Token)

            If MoveTalk Then Qs.Add("movetalk")
            If Not Redirect Then Qs.Add("noredirect")

            'Move the page
            Result = DoApiRequest(Qs)
            If Result.IsError Then Return Result.FailWith(FailMsg)

            Return Result.Success
        End Function

    End Class

End Namespace
