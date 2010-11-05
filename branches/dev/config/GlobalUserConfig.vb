Imports Huggle.Actions
Imports System
Imports System.Collections.Generic
Imports System.IO
Imports System.Text
Imports System.Text.RegularExpressions

Imports KVP = System.Collections.Generic.KeyValuePair(Of String, String)

Namespace Huggle

    Friend Class GlobalUserConfig : Inherits Config

        Private GlobalUser As GlobalUser

        Friend Property AutoUnifiedLogin As Boolean
        Friend Property Extra As Boolean
        Friend Property ExtraLoader As ExtraGlobaluserConfig

        Friend Sub New(ByVal globalUser As GlobalUser)
            Me.GlobalUser = globalUser
        End Sub

        Protected Overrides ReadOnly Property Location() As String
            Get
                Return PathCombine("globaluser", GetValidFileName(GlobalUser.FullName))
            End Get
        End Property

        Protected Overrides Sub ReadConfig(ByVal text As String)
            For Each item As KVP In Config.ParseConfig("globaluser", Nothing, text)
                Dim key As String = item.Key
                Dim value As String = item.Value

                Try
                    Select Case key
                        Case "auto-unified-login" : AutoUnifiedLogin = value.ToBoolean
                        Case "created" : GlobalUser.Created = value.ToDate
                        Case "extra" : Extra = value.ToBoolean

                        Case "groups"
                            Dim groups As New List(Of GlobalGroup)

                            For Each group As String In value.ToList.Trim
                                groups.Merge(GlobalUser.Family.GlobalGroups(group))
                            Next group

                            GlobalUser.GlobalGroups = groups

                        Case "home" : GlobalUser.PrimaryUser = App.Wikis(value).Users(GlobalUser.Name)
                        Case "id" : GlobalUser.Id = CInt(value)
                        Case "rights" : GlobalUser.Rights = New List(Of String)(value.ToList.Trim)
                        Case "updated" : Updated = value.ToDate

                        Case "users"
                            GlobalUser.Users.Clear()
                            GlobalUser.Wikis.Clear()

                            For Each userItem As KVP In Config.ParseConfig("globaluser", key, value)
                                Dim user As User = App.Wikis(userItem.Key).Users(GlobalUser.Name)
                                user.Contributions = 0
                                user.GlobalUser = GlobalUser
                                user.UnificationMethod = "login"

                                For Each prop As KVP In Config.ParseConfig("globaluser", key & ":" & userItem.Key, userItem.Value)
                                    Select Case prop.Key
                                        Case "contribs" : user.Contributions = CInt(prop.Value)
                                        Case "merged" : user.UnificationDate = prop.Value.ToDate
                                        Case "method" : user.UnificationMethod = prop.Value
                                    End Select
                                Next prop

                                GlobalUser.Users.Add(user)
                                GlobalUser.Wikis.Add(user.Wiki)
                            Next userItem
                    End Select

                Catch ex As SystemException
                    Log.Write(Msg("error-configvalue", item.Key, "globaluser"))
                End Try
            Next item
        End Sub

        Friend Overrides Function WriteConfig(ByVal target As ConfigTarget) As Dictionary(Of String, Object)
            Dim items As New Dictionary(Of String, Object)

            If target = ConfigTarget.Local Then
                items.Add("auto-unified-login", AutoUnifiedLogin)
                If Extra Then items.Add("extra", True)
            End If

            If GlobalUser.Created > Date.MinValue Then items.Add("created", WikiTimestamp(GlobalUser.Created))
            If GlobalUser.GlobalGroups IsNot Nothing Then items.Add("groups", GlobalUser.GlobalGroups.Join(","))
            If GlobalUser.Home IsNot Nothing Then items.Add("home", GlobalUser.Home.Code)
            If GlobalUser.Id > 0 Then items.Add("id", GlobalUser.Id)
            If GlobalUser.Rights IsNot Nothing Then items.Add("rights", GlobalUser.Rights.Join(","))
            If Updated > Date.MinValue Then items.Add("updated", Updated)

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

            Return items
        End Function

        Friend Function Copy(ByVal globalUser As GlobalUser) As GlobalUserConfig
            Dim result As New GlobalUserConfig(globalUser)
            result.ReadConfig(Config.MakeConfig(WriteConfig(ConfigTarget.Local)))
            Return result
        End Function

    End Class

End Namespace
