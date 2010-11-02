Imports System
Imports System.Collections.Generic
Imports System.IO
Imports System.Text

Imports KVP = System.Collections.Generic.KeyValuePair(Of String, String)

Namespace Huggle

    Public Class FamilyConfig : Inherits Config

        Private Family As Family

        Public Property Logo As String

        Public Sub New(ByVal family As Family)
            Me.Family = family
        End Sub

        Protected Overrides ReadOnly Property Location() As String
            Get
                Return PathCombine("family", GetValidFileName(Family.Code))
            End Get
        End Property

        Protected Overrides Sub ReadConfig(ByVal text As String)
            For Each item As KVP In Config.ParseConfig("family", Nothing, text)
                Dim key As String = item.Key
                Dim value As String = item.Value

                Try
                    Select Case key
                        Case "central-wiki"
                            Family.CentralWiki = App.Wikis(value)
                            App.Wikis(value).Family = Family

                        Case "default-globaluser-config" : Family.GlobalUsers.Default.Config.Load(value)
                        Case "default-user-config" : Family.Wikis.Default.Users.Default.Config.Load(value)
                        Case "default-wiki-config" : Family.Wikis.Default.Config.Load(value)
                        Case "feed" : Family.Feed = New Feed(Family, value, 6667)

                        Case "file-wiki"
                            Family.FileWiki = App.Wikis(value)
                            App.Wikis(value).Family = Family

                        Case "global-groups"
                            For Each groupItem As KVP In Config.ParseConfig("family", key, value)
                                Dim group As GlobalGroup = Family.GlobalGroups(groupItem.Key)

                                For Each groupProp As KVP In Config.ParseConfig("family", key & ":" & groupItem.Key, groupItem.Value)
                                    Select Case groupProp.Key
                                        Case "display-name" : group.DisplayName = groupProp.Value
                                    End Select
                                Next groupProp
                            Next groupItem

                        Case "logo" : Logo = value
                        Case "name" : Family.Name = value
                        Case "spam-list" : ReadSpamLists(Family.GlobalSpamLists, "family", value)

                        Case "title-blacklist"
                            Family.GlobalTitleBlacklist = New TitleList(Family.CentralWiki.Pages.FromString(value))

                        Case "title-blacklist-entries"
                            Dim entries As New List(Of String)

                            For Each entry As KeyValuePair(Of String, String) In Config.ParseConfig("family", key, value)
                                entries.Add(entry.Value)
                            Next entry

                            'Family.GlobalTitleBlacklist.Location.Text = entries.Join(LF)
                    End Select

                Catch ex As SystemException
                    Log.Write(Msg("error-configvalue", item.Key, "family"))
                End Try
            Next item
        End Sub

        Public Overrides Function WriteConfig(ByVal target As ConfigTarget) As Dictionary(Of String, Object)
            Dim items As New Dictionary(Of String, Object)

            If Family.CentralWiki IsNot Nothing Then items.Add("central-wiki", Family.CentralWiki.Code)
            If Family.Feed IsNot Nothing Then items.Add("feed", Family.Feed.Server)
            If Family.FileWiki IsNot Nothing Then items.Add("file-wiki", Family.FileWiki.Code)
            If Logo IsNot Nothing Then items.Add("logo", Logo)
            If Family.Name <> Family.Code Then items.Add("name", Family.Name)

            If Family.GlobalGroups.All.Count > 0 Then
                Dim groupItems As New Dictionary(Of String, Object)

                For Each group As GlobalGroup In Family.GlobalGroups.All
                    Dim groupObject As New Dictionary(Of String, Object)
                    If group.DisplayName IsNot Nothing Then groupObject.Add("display-name", group.DisplayName)
                    If group.Rights.Count > 0 Then groupObject.Add("rights", group.Rights.Join(", "))

                    groupItems.Add(group.Name, groupObject)
                Next group

                items.Add("global-groups", groupItems)
            End If

            If Family.GlobalTitleBlacklist IsNot Nothing Then
                items.Add("title-blacklist", Family.GlobalTitleBlacklist.Location.Title)

                If Family.GlobalTitleBlacklist.IsLoaded Then
                    Dim entries As New Dictionary(Of String, Object)

                    For i As Integer = 0 To Family.GlobalTitleBlacklist.Entries.Count - 1
                        entries.Add(i.ToStringI.PadLeft(4, "0"c), Family.GlobalTitleBlacklist.Entries(i))
                    Next i

                    items.Add("title-blacklist-entries", entries)
                End If
            End If

            If target = ConfigTarget.Local Then
                items.Add("default-globaluser-config", Family.GlobalUsers.Default.Config.WriteConfig(target))
                items.Add("default-user-config", Family.Wikis.Default.Users.Default.Config.WriteConfig(target))
                items.Add("default-wiki-config", Family.Wikis.Default.Config.WriteConfig(target))
            End If

            Return items
        End Function

        Public Function Copy(ByVal family As Family) As FamilyConfig
            Dim result As New FamilyConfig(family)
            result.Load(Config.MakeConfig(WriteConfig(ConfigTarget.Local)))
            Return result
        End Function

    End Class

End Namespace