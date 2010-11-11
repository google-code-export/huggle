Imports System
Imports System.Collections.Generic
Imports System.IO
Imports System.Net
Imports System.Text

Namespace Huggle.Actions

    Friend Class LoadGlobalConfig : Inherits Query

        Public Sub New()
            MyBase.New(App.Sessions(App.Wikis.Global.Users.Anonymous), Msg("config-desc"))
            Interactive = True
        End Sub

        Public Overrides Sub Start()
            OnStarted()
            OnProgress(Msg("config-progress"))

            'Get cached config from the cloud
            If InternalConfig.UseCloud Then
                Config.Global.LoadCloud()

                If Config.Global.IsCurrent Then
                    Config.Global.SaveLocal()
                    OnSuccess()
                    Return
                Else
                    Log.Debug("Outdated in cloud: global")
                End If
            End If

            Dim configPage As Page = Wiki.Pages(GlobalConfig.PageTitle)
            Dim languagePage As Page = Wiki.Pages(GlobalConfig.LocalizationPageName(App.Languages.Current.Code))

            'Parts to global configuration:
            '* the configuration page itself
            '* action=sitematrix through the API
            '* closed.dblist because sitematrix extension is broken and doesn't indicate closed wikis
            '* the global title blacklist
            '* global groups from Toolserver because there is no API

            Dim configRequest As New PageInfoQuery(Session, New Page() {configPage, languagePage}.ToList, Content:=True)

            If Wiki.Family.GlobalTitleBlacklist IsNot Nothing _
                Then configRequest.Pages.Merge(Wiki.Family.GlobalTitleBlacklist.Location)

            Dim wikiRequest As New ApiRequest(App.Sessions(App.Wikis.Global.Users.Anonymous),
                Description, New QueryString("action", "sitematrix"))

            Dim groupsRequest As New TextRequest(InternalConfig.WMGlobalGroupsUrl)
            Dim closedRequest As New TextRequest(InternalConfig.WMClosedWikisUrl)

            App.DoParallel({configRequest, closedRequest, wikiRequest, groupsRequest})

            If configRequest.Result.IsError Then OnFail(configRequest.Result) : Return
            If wikiRequest.Result.IsError Then OnFail(wikiRequest.Result) : Return

            If Not configPage.Exists OrElse String.IsNullOrEmpty(configPage.Text) _
                Then OnFail(Msg("config-globalnotfound")) : Return

            Try
                Config.Global.Load(configPage.Text)
                Log.Debug("Load from wiki: global")

            Catch ex As ConfigException
                OnFail(Result.FromException(ex)) : Return
            End Try

            If languagePage.Exists Then
                Try
                    App.Languages.Current.GetConfig.Load(languagePage.Text)
                    Log.Debug("Loaded messages for '{0}' from wiki".FormatI(App.Languages.Current.Name))

                Catch ex As ConfigException
                    Log.Write(Msg("language-loadfail", App.Languages.Current.Name,
                        Msg("error-config", Nothing, "messages-" & App.Languages.Current.Code)))
                End Try
            End If

            'Scrape HTML of toolserver global groups list
            If groupsRequest.IsSuccess Then
                Dim html As String = groupsRequest.Response
                html = html.FromFirst("<div id=""toc"">").FromFirst("</div>").ToFirst("<!-- begin generated footer -->")

                For Each groupItem As String In html.Split("<h2")
                    If Not groupItem.Contains("<table") Then Continue For

                    Dim groupName As String = groupItem.FromFirst(">").ToFirst("<").Trim
                    Dim group As GlobalGroup = App.Families.Wikimedia.GlobalGroups(groupName)
                    Dim rightsTable As String = groupItem.FromFirst("<table").ToFirst("</table>")
                    Dim rights As New List(Of String)

                    For Each rightItem As String In rightsTable.Split("<tr>")
                        If Not rightItem.Contains("<td>") Then Continue For

                        Dim right As String = rightItem.FromFirst("<td>").ToFirst("</td>").Trim
                        rights.Merge(right)
                    Next rightItem

                    group.Rights = rights

                    If groupItem.FromFirst("</table>").Contains("<table") Then
                        Dim wikisTable As String = groupItem.FromFirst("</table>").FromFirst("<table").ToFirst("</table>")
                        Dim header As String = wikisTable.FromFirst("<th>").ToFirst("</th>")

                        wikisTable = wikisTable.FromFirst("<tr").FromFirst("<tr").FromFirst("<td>").ToFirst("</td>")
                        Dim wikis As New List(Of Wiki)

                        For Each wikiItem As String In wikisTable.Split("<br />")
                            Dim wikiCode As String = wikiItem.Trim

                            If App.Wikis.Contains(wikiCode) Then
                                Dim wiki As Wiki = App.Wikis(wikiCode)

                                Dim a As String = App.Wikis.All.Count.ToStringI
                                If wiki IsNot Nothing AndAlso App.Families.Wikimedia.Wikis.All.Contains(wiki) _
                                    Then wikis.Merge(wiki)
                            Else
                                Log.Debug("Could not locate wiki '{0}' referenced in global groups list".FormatI(wikiCode))
                            End If
                        Next wikiItem

                        group.Wikis = wikis

                        If header.Contains("the following") Then
                            group.Applicability = GlobalGroupApplicability.Inclusive
                        ElseIf header.Contains("all wikis except") Then
                            group.Applicability = GlobalGroupApplicability.Exclusive
                        Else
                            group.Wikis = Nothing
                            group.Applicability = GlobalGroupApplicability.All
                        End If

                    Else
                        group.Wikis = Nothing
                        group.Applicability = GlobalGroupApplicability.All
                    End If
                Next groupItem
            Else
                Log.Write("Error loading global groups list: {0}".FormatI(groupsRequest.Result.LogMessage))
            End If

            'Closed wikis
            If closedRequest.IsSuccess Then
                For Each code As String In groupsRequest.Response.Split(LF)
                    If App.Wikis.Contains(code) Then
                        Dim wiki As Wiki = App.Wikis(code)

                        wiki.IsPublicEditable = False
                        wiki.IsPublicReadable = False
                    End If
                Next code
            Else
                Log.Write("Error loading closed wiki list: {0}".FormatI(closedRequest.Result.LogMessage))
            End If

            'Save configuration
            Config.Global.Updated = Date.UtcNow
            Config.Global.SaveLocal()

            For Each lang As Language In App.Languages.All
                If lang.IsLocalized Then lang.GetConfig.SaveLocal()
            Next lang

            'Store config to the cloud
            If InternalConfig.UseCloud Then CreateThread(AddressOf Config.Global.SaveCloud)

            OnSuccess()
        End Sub

    End Class

End Namespace
