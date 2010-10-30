Imports System.Text.RegularExpressions
Imports System.Web.HttpUtility

Namespace Huggle.Actions

    'Load information about global groups

    Public Class GlobalGroupsQuery : Inherits Query

        Private Family As Family

        Public Sub New(ByVal family As Family)
            MyBase.New(App.Sessions(family), Msg("globalgroups-desc", family.Name))
            Me.Family = family
        End Sub

        Public Overrides Sub Start()
            OnStarted()
            OnProgress(Msg("globalgroups-progress", Family.Name))

            'This should be in the API but isn't
            Dim request As New UIRequest(Session, Description,
                New QueryString("title", "Special:GlobalGroupPermissions"), Nothing)

            request.Start()
            If request.IsFailed Then OnFail(request.Result) : Return

            'Parse list of global groups
            Dim globalGroupSectionPattern As New Regex("<fieldset>(.*)</fieldset>",
                RegexOptions.Compiled Or RegexOptions.Singleline Or RegexOptions.IgnoreCase)
            Dim globalGroupItemPattern As New Regex("<li>.*?([^(]+) *\(.*?title="".*?/(.*?)"".*?</li>",
                RegexOptions.Compiled Or RegexOptions.Singleline Or RegexOptions.IgnoreCase)

            Dim responseMatch As Match = globalGroupSectionPattern.Match(request.Response)
            If Not responseMatch.Success Then OnFail(Msg("error-scrape")) : Return

            Family.GlobalGroups.Clear()

            For Each line As Match In globalGroupItemPattern.Matches(responseMatch.Groups(1).Value)
                Dim lineMatch As Match = globalGroupItemPattern.Match(line.Value)

                Dim group As GlobalGroup = Family.GlobalGroups(HtmlDecode(lineMatch.Groups(2).Value.Trim))
                group.DisplayName = HtmlDecode(lineMatch.Groups(1).Value.Trim.Replace("_", " "))
            Next line

            Family.Config.SaveLocal()
            OnSuccess()
        End Sub

    End Class

End Namespace
