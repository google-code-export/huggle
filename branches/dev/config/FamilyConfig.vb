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
            IsDefault = True
            LocalPath = Path & Slash & GetValidFileName(family.Code) & ".txt"
        End Sub

        Public ReadOnly Property CacheTime() As TimeSpan
            Get
                Return TimeSpan.FromHours(12)
            End Get
        End Property

        Public Shared Sub LoadAll()
            If Directory.Exists(Path) Then
                For Each familyPath As String In Directory.GetFiles(Path)
                    Dim family As Family = App.Families(IO.Path.GetFileNameWithoutExtension(familyPath))
                    family.Config.LoadLocal()
                Next familyPath
            End If
        End Sub

        Public Shared Sub SaveAll()
            For Each family As Family In App.Families.All
                family.Config.SaveLocal()
            Next family
        End Sub

        Public Sub LoadLocal()
            Try
                If File.Exists(LocalPath) Then
                    Load(File.ReadAllText(LocalPath, Encoding.UTF8))
                    Log.Debug("Loaded family config for {0} [L]".FormatWith(Family.Name))
                Else
                    If IsDefault Then Config.Global.NeedsUpdate = True
                End If

            Catch ex As ConfigException
                Log.Write(Result.FromException(ex).LogMessage)

            Catch ex As SystemException
                Log.Write(Result.FromException(ex).LogMessage)
            End Try
        End Sub

        Public Property IsDefault As Boolean

        Public Sub Load(ByVal text As String)
            For Each item As KeyValuePair(Of String, String) In Config.ParseConfig("globaluser", Nothing, text)
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
                        Case "feed" : Family.Feed = New Feed(Family, value)

                        Case "file-wiki"
                            Family.FileWiki = App.Wikis(value)
                            App.Wikis(value).Family = Family

                        Case "logo" : Logo = value
                        Case "name" : Family.Name = value

                        Case "wikis"
                            For Each code As String In value.ToList.Trim
                                App.Wikis(code).Family = Family
                            Next code
                    End Select

                Catch ex As SystemException
                    Log.Write(Msg("error-configvalue", item.Key, "globaluser"))
                End Try
            Next item

            IsDefault = False
        End Sub

        Public ReadOnly Property NeedsUpdate() As Boolean
            Get
                Try
                    Return File.GetLastWriteTime(LocalPath).Add(CacheTime) < Date.Now
                Catch ex As IOException
                    Return True
                End Try
            End Get
        End Property

        Public Sub SaveLocal()
            Try
                Dim path As String = IO.Path.GetDirectoryName(LocalPath)
                If Not Directory.Exists(path) Then Directory.CreateDirectory(path)
                File.WriteAllText(LocalPath, Config.MakeConfig(WriteConfig(True)), Encoding.UTF8)

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

            If local Then
                items.Add("default-globaluser-config", Family.GlobalUsers.Default.Config.WriteConfig(True))
                items.Add("default-user-config", Family.Wikis.Default.Users.Default.Config.WriteConfig(True))
                items.Add("default-wiki-config", Family.Wikis.Default.Config.WriteConfig(True))

                If Family.Wikis.All.Count > 0 Then
                    Dim wikiCodes As New List(Of String)

                    For Each wiki As Wiki In Family.Wikis.All
                        wikiCodes.Add(wiki.Code)
                    Next wiki

                    wikiCodes.Sort()
                    items.Add("wikis", wikiCodes)
                End If
            End If

            Return items
        End Function

        Public Function Copy(ByVal family As Family) As FamilyConfig
            Dim result As New FamilyConfig(family)
            result.Load(Config.MakeConfig(WriteConfig(True)))
            result.IsDefault = IsDefault
            Return result
        End Function

    End Class

End Namespace