Imports Huggle.Actions
Imports System
Imports System.Collections.Generic
Imports System.IO
Imports System.Text
Imports System.Text.RegularExpressions

Namespace Huggle

    Public Class GlobalUserConfig

        Private GlobalUser As GlobalUser
        Private LocalPath As String

        Public Property AutoUnifiedLogin As Boolean
        Public Property Extra As Boolean
        Public Property ExtraLoader As ExtraGlobaluserConfig

        Public Sub New(ByVal globalUser As GlobalUser)
            Me.GlobalUser = globalUser
            IsDefault = True
            LocalPath = Config.Local.ConfigPath & "globaluser" & Slash & GetValidFileName(globalUser.FullName) & ".txt"
        End Sub

        Public ReadOnly Property CacheTime() As TimeSpan
            Get
                Return TimeSpan.FromHours(12)
            End Get
        End Property

        Public Sub LoadLocal()
            Try
                If File.Exists(LocalPath) Then
                    Load(File.ReadAllText(LocalPath, Encoding.UTF8))
                    Log.Debug("Loaded global user config for {0} [L]".FormatWith(GlobalUser.FullName))
                Else
                    If IsDefault Then Config.Global.NeedsUpdate = True
                End If

            Catch ex As ConfigException
                Log.Write(Result.FromException(ex).LogMessage)

            Catch ex As SystemException
                Log.Write(Result.FromException(ex).LogMessage)
            End Try
        End Sub

        Public Property IsDefault() As Boolean

        Public Property IsLocalCopy() As Boolean

        Public Sub Load(ByVal text As String)
            For Each mainProp As KeyValuePair(Of String, String) In Config.ParseConfig("globaluser", Nothing, text)
                Dim key As String = mainProp.Key
                Dim value As String = mainProp.Value

                Try
                    Select Case key
                        Case "auto-unified-login" : AutoUnifiedLogin = value.ToBoolean
                        Case "created" : GlobalUser.Created = value.ToDate
                        Case "extra" : Extra = value.ToBoolean

                        Case "groups"
                            GlobalUser.GlobalGroups.Clear()

                            For Each group As String In value.ToList.Trim
                                GlobalUser.GlobalGroups.Add(GlobalUser.Family.GlobalGroups(group))
                            Next group

                        Case "home" : GlobalUser.PrimaryUser = App.Wikis(value).Users(GlobalUser.Name)
                        Case "id" : GlobalUser.Id = value.ToInteger

                        Case "rights"
                            GlobalUser.Rights.Clear()
                            GlobalUser.Rights.AddRange(value.ToList.Trim)

                        Case "users"
                            GlobalUser.Users.Clear()
                            GlobalUser.Wikis.Clear()

                            For Each item As KeyValuePair(Of String, String) In
                                Config.ParseConfig("globaluser", key, value)

                                Dim user As User = App.Wikis(item.Key).Users(GlobalUser.Name)
                                user.Contributions = 0
                                user.GlobalUser = GlobalUser
                                user.UnificationMethod = "login"

                                For Each prop As KeyValuePair(Of String, String) In
                                    Config.ParseConfig("globaluser", key & ":" & item.Key, item.Value)

                                    Select Case prop.Key
                                        Case "contribs" : user.Contributions = CInt(prop.Value)
                                        Case "merged" : user.UnificationDate = prop.Value.ToDate
                                        Case "method" : user.UnificationMethod = prop.Value
                                    End Select
                                Next prop

                                GlobalUser.Users.Add(user)
                                GlobalUser.Wikis.Add(user.Wiki)
                            Next item
                    End Select

                Catch ex As SystemException
                    Log.Write(Msg("error-configvalue", mainProp.Key, "globaluser"))
                End Try
            Next mainProp

            If Not GlobalUser.IsDefault Then IsDefault = False
        End Sub

        Public ReadOnly Property NeedsUpdate() As Boolean
            Get
                Try
                    Return (File.GetLastWriteTime(LocalPath).Add(CacheTime) < Date.Now)
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
                Log.Debug("Saved global user config for {0} [L]".FormatWith(GlobalUser.FullName))

            Catch ex As IOException
                Log.Write(Msg("globalconfig-savefail", ex.Message))
            End Try
        End Sub

        Public Function WriteConfig(ByVal local As Boolean) As Dictionary(Of String, Object)
            Dim items As New Dictionary(Of String, Object)

            items.Add("auto-unified-login", AutoUnifiedLogin)

            If local Then
                If GlobalUser.Created > Date.MinValue Then items.Add("created", WikiTimestamp(GlobalUser.Created))
                If Extra Then items.Add("extra", True)
                If GlobalUser.GlobalGroups.Count > 0 Then items.Add("groups", GlobalUser.GlobalGroups.Join(","))
                If GlobalUser.Home IsNot Nothing Then items.Add("home", GlobalUser.Home.Code)
                If GlobalUser.Id > 0 Then items.Add("id", GlobalUser.Id)
                If GlobalUser.Rights.Count > 0 Then items.Add("rights", GlobalUser.Rights.Join(","))

                If GlobalUser.Users.Count > 0 Then
                    Dim userItems As New Dictionary(Of String, Object)

                    For Each user As User In GlobalUser.Users
                        Dim item As New Dictionary(Of String, Object)

                        If user.Contributions > 0 Then item.Add("contribs", user.Contributions)
                        If user.UnificationDate > Date.MinValue Then item.Add("merged", WikiTimestamp(user.UnificationDate))
                        If user.UnificationMethod <> "login" Then item.Add("method", user.UnificationMethod)

                        userItems.Add(user.Wiki.Code, item)
                    Next user

                    items.Add("users", userItems)
                End If
            End If

            Return items
        End Function

        Public Function Copy(ByVal globalUser As GlobalUser) As GlobalUserConfig
            Dim result As New GlobalUserConfig(globalUser)
            result.Load(Config.MakeConfig(WriteConfig(True)))
            result.IsDefault = IsDefault
            Return result
        End Function

    End Class

End Namespace
