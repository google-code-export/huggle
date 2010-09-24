Imports System
Imports System.Collections.Generic
Imports System.IO
Imports System.Text

Namespace Huggle

    Public Class FamilyConfig

        Private Family As Family
        Private LocalPath As String

        Private Shared ReadOnly Path As String = Config.Local.ConfigPath & "family"

        Public Logo As String = "Wikimedia.png"

        Public Sub New(ByVal family As Family)
            Me.Family = family
            LocalPath = Path & Slash & GetValidFileName(family.Code) & ".txt"
        End Sub

        Public ReadOnly Property CacheTime() As TimeSpan
            Get
                Return TimeSpan.FromHours(12)
            End Get
        End Property

        Public Shared Sub LoadAll()
            For Each family As Family In App.Families.All
                family.Config.LoadLocal()
            Next family
        End Sub

        Public Shared Sub SaveAll()
            For Each family As Family In App.Families.All
                family.Config.SaveLocal()
            Next family
        End Sub

        Public Sub LoadLocal()
            Try
                If IO.File.Exists(LocalPath) Then
                    Load(IO.File.ReadAllText(LocalPath, Encoding.UTF8))
                    Log.Debug("Loaded family config for {0} [L]".FormatWith(Family.Name))
                Else
                    Config.Global.NeedsUpdate = True
                End If

            Catch ex As ConfigException
                Log.Write(Result.FromException(ex).LogMessage)

            Catch ex As SystemException
                Log.Write(Result.FromException(ex).LogMessage)
            End Try
        End Sub

        Public Sub Load(ByVal text As String)
            For Each item As KeyValuePair(Of String, String) In Config.ParseConfig("family", Nothing, text)
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

                        Case "logo" : Logo = value
                        Case "name" : Family.Name = value
                        Case "spam-list" : ConfigRead.ReadSpamLists(Family.GlobalSpamLists, "family", value)

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

        Public ReadOnly Property NeedsUpdate() As Boolean
            Get
                Try
                    Return IO.File.GetLastWriteTime(LocalPath).Add(CacheTime) < Date.Now
                Catch ex As IOException
                    Return True
                End Try
            End Get
        End Property

        Public Sub SaveLocal()
            Try
                Dim path As String = IO.Path.GetDirectoryName(LocalPath)
                If Not Directory.Exists(path) Then Directory.CreateDirectory(path)
                IO.File.WriteAllText(LocalPath, Config.MakeConfig(WriteConfig(True)), Encoding.UTF8)
                Log.Debug("Saved family config for {0} [L]".FormatWith(Family.Name))

            Catch ex As IOException
                Log.Write(Msg("globalconfig-savefail", ex.Message))
            End Try
        End Sub

        Public Function WriteConfig(ByVal local As Boolean) As Dictionary(Of String, Object)
            Dim items As New Dictionary(Of String, Object)

            If Family.CentralWiki IsNot Nothing Then items.Add("central-wiki", Family.CentralWiki.Code)
            If Family.Feed IsNot Nothing Then items.Add("feed", Family.Feed.Server)
            If Family.FileWiki IsNot Nothing Then items.Add("file-wiki", Family.FileWiki.Code)
            If Logo IsNot Nothing Then items.Add("logo", Logo)
            If Family.Name <> Family.Code Then items.Add("name", Family.Name)

            If Family.GlobalTitleBlacklist IsNot Nothing Then
                items.Add("title-blacklist", Family.GlobalTitleBlacklist.Location.Title)

                If Family.GlobalTitleBlacklist.IsLoaded Then
                    Dim entries As New Dictionary(Of String, Object)

                    For i As Integer = 0 To Family.GlobalTitleBlacklist.Entries.Count - 1
                        entries.Add(i.ToString.PadLeft(4, "0"c), Family.GlobalTitleBlacklist.Entries(i))
                    Next i

                    items.Add("title-blacklist-entries", entries)
                End If
            End If

            If local Then
                items.Add("default-globaluser-config", Family.GlobalUsers.Default.Config.WriteConfig(True))
                items.Add("default-user-config", Family.Wikis.Default.Users.Default.Config.WriteConfig(True))
                items.Add("default-wiki-config", Family.Wikis.Default.Config.WriteConfig(True))
            End If

            Return items
        End Function

        Public Function Copy(ByVal family As Family) As FamilyConfig
            Dim result As New FamilyConfig(family)
            result.Load(Config.MakeConfig(WriteConfig(True)))
            Return result
        End Function

    End Class

End Namespace