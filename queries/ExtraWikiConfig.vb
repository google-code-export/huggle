Imports Huggle.Wikitext
Imports System
Imports System.Collections.Generic
Imports System.Text.RegularExpressions
Imports System.Web.HttpUtility

Namespace Huggle.Actions

    'Load non-essential wiki configuration

    Public Class ExtraWikiConfig : Inherits Query

        Public Sub New(ByVal wiki As Wiki)
            MyBase.New(App.Sessions(wiki), Msg("extraconfig-desc"))
        End Sub

        Public Overrides Sub Start()
            OnStarted()
            OnProgress(Msg("extraconfig-progress"))

            Dim reqs As New List(Of Process)

            If Wiki.Extensions.Contains("Flagged Revisions") _
                Then reqs.Add(New ApiRequest(Session, Description, New QueryString("action", "flagconfig")))

            If Wiki.Extensions.Contains("Gadgets") Then reqs.Add(New ApiRequest(Session, Description, New QueryString( _
                "action", "query", _
                "titles", "MediaWiki:Gadgets-definition", _
                "prop", "revisions", _
                "rvprop", "ids|content")))

            Dim pageReq As New PageInfoQuery(Session)
            pageReq.Content = True

            If Wiki.Extensions.Contains(Extension.SpamList) Then
                Dim list As SpamList = Wiki.SpamLists.FromPage(Wiki.Pages("MediaWiki:Spam-blacklist"))
                If Not list.IsLoaded Then pageReq.Pages.Add(list.Page)
            End If

            If Wiki.Extensions.Contains(Extension.TitleList) Then
                If Wiki.TitleBlacklist Is Nothing _
                    Then Wiki.TitleBlacklist = New TitleList(Wiki.Pages("MediaWiki:Titleblacklist"))
                If Not Wiki.TitleBlacklist.IsLoaded Then pageReq.Pages.Add(Wiki.TitleBlacklist.Location)
            End If

            If Wiki.Config.Logo IsNot Nothing Then reqs.Add(New MediaQuery(Session, Wiki.Files.FromString(Wiki.Config.Logo), 128))

            Dim prefsReq As New UIRequest(Session, Description, New QueryString("title", "Special:Preferences"), Nothing)
            reqs.Add(prefsReq)

            App.DoParallel(reqs)

            If Not Session.User.IsAnonymous Then
                'Time zone options
                If Config.Global.TimeZones IsNot Nothing Then
                    Dim zoneTable As String = Nothing

                    If prefsReq.Response.Contains("id=""mw-input-timecorrection""") Then
                        zoneTable = prefsReq.Response.FromFirst("id=""mw-input-timecorrection""")

                    ElseIf prefsReq.Response.Contains("id=""wpTimeZone""") Then
                        zoneTable = prefsReq.Response.FromFirst("id=""wpTimeZone""")

                    Else
                        Log.Debug("Unable to load timezones from user preferences page")
                    End If

                    If zoneTable IsNot Nothing Then
                        Dim zones As New Dictionary(Of String, Integer)
                        zoneTable = zoneTable.FromFirst(">").ToFirst("</select>")

                        For Each item As String In zoneTable.Split("<option ")
                            If item.Contains("value=""") Then
                                item = item.FromFirst("value=""").ToFirst("""")

                                If item.StartsWithI("ZoneInfo|") Then
                                    item = item.FromFirst("|")
                                    zones.Merge(item.FromFirst("|"), CInt(item.ToFirst("|")))
                                End If
                            End If
                        Next item

                        Config.Global.TimeZones = zones
                    End If
                End If

                'Skins
                If Wiki.Skins.Count = 0 Then
                    If Not (prefsReq.Response.Contains("id=""mw-htmlform-skin""") _
                        AndAlso prefsReq.Response.Contains("</table>")) Then

                        Log.Debug("Unable to load skins from user preferences page")
                    Else
                        Dim skinsTable As String = prefsReq.Response.FromFirst("id=""mw-htmlform-skin""") _
                            .FromFirst(">").ToFirst("</table>")

                        For Each item As String In skinsTable.Split("<input ")
                            If item.Contains("value=""") Then
                                Dim code As String = item.FromFirst("value=""").ToFirst("""")
                                Dim name As String = item.FromFirst("for=""mw-input-skin-").FromFirst(">").ToFirst(" (")
                                Wiki.Skins.Add(code, New WikiSkin(Wiki, code, name))
                                If item.Contains(" (default") Then Wiki.Config.DefaultSkin = code
                            End If
                        Next item
                    End If
                End If
            End If

            'Gadgets
            If Wiki.Extensions.Contains("Gadgets") Then
                Dim descPages As New List(Of Page)
                Dim doc As New Document(Wiki.Pages("MediaWiki:Gadgets-definition"))

                For Each section As Section In doc.Sections.All
                    If section.Title IsNot Nothing Then
                        Dim type As String = section.Title
                        descPages.Merge(Wiki.Pages("MediaWiki:Gadget-section-" & type))

                        For Each line As String In section.Text.Split(LF)
                            If line.StartsWithI("*") Then
                                Dim gadget As Gadget = Wiki.Gadgets(line.FromFirst("*").ToFirst("|").Trim)
                                gadget.Type = type

                                Dim pages As New List(Of String)(line.FromFirst("|").Split("|"))
                                gadget.Pages = New List(Of Page)

                                For Each pageName As String In pages
                                    gadget.Pages.Merge(Wiki.Pages("MediaWiki:Gadget-" & pageName))
                                Next pageName

                                descPages.Merge(Wiki.Pages.FromString("MediaWiki:Gadget-" & gadget.Code))
                            End If
                        Next line
                    End If
                Next section

                Dim query As New PageInfoQuery(Session, descPages, Content:=True)
                query.Start()

                If Not query.IsFailed Then
                    For Each gadget As Gadget In Wiki.Gadgets.All
                        gadget.Description = HtmlDecode(Wiki.Pages.FromString("MediaWiki:Gadget-" & gadget.Code).Text)
                        gadget.TypeDesc = HtmlDecode(Wiki.Pages("MediaWiki:Gadget-section-" & gadget.Type).Text)

                        Dim name As String = gadget.Code
                        Dim match As Match = Wiki.Config.GadgetIdentifierPattern.Match(gadget.Description)

                        If match.Success Then
                            name = match.Groups(1).Value
                            gadget.Description = gadget.Description.Substring(match.Groups(0).Length)
                        End If

                        gadget.Name = UcFirst(name).Replace("_", " ").Replace("-", " ")
                    Next gadget
                End If
            End If

            For Each req As Process In reqs
                If req.IsFailed Then OnFail(req.Result) : Return
            Next req

            If Wiki.Config.PriorityQuery IsNot Nothing AndAlso Wiki.Config.PriorityNeedsUpdate Then
                OnProgress("extraconfig-priority")

                Dim eval As New Scripting.Evaluator(Session, "Priority", Wiki.Config.PriorityQuery)
                eval.Start()
                If eval.IsFailed Then Log.Write(eval.Result.LogMessage)

                Wiki.Pages.Priority.Clear()

                For Each item As Object In eval.Value.AsList
                    Wiki.Pages.Priority.Add(item.ToString)
                Next item
            End If

            Wiki.Config.ExtraConfigLoaded = True
            Wiki.Config.Updated = Date.UtcNow

            User.Config.SaveLocal()
            Wiki.Config.SaveLocal()
            Config.Global.SaveLocal()
            If User.IsUnified Then User.GlobalUser.Config.SaveLocal()

            If InternalConfig.UseCloud Then CreateThread(AddressOf Wiki.Config.SaveCloud)

            OnSuccess()
        End Sub

    End Class

End Namespace
