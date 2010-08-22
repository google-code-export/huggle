Imports System
Imports System.Collections.Generic
Imports System.IO
Imports System.Net
Imports System.Text

Namespace Huggle

    Public Class UserConfig

        Private _User As User

        Private LocalPath As String

        Public Sub New(ByVal user As User)
            _IsDefault = True
            _User = user
            LocalPath = Config.Local.ConfigPath & "user" & Slash & GetValidFileName(user.FullName) & ".txt"
        End Sub

        Public Property AutoIgnoreAfter As Integer
        Public Property AutoIgnoreLocalCondition As String
        Public Property AutoIgnoreListCondition As String
        Public Property ConfirmBlock As Boolean
        Public Property ConfirmBlocked As Boolean
        Public Property ConfirmBlockIgnored As Boolean
        Public Property ConfirmBlockPrivileged As Boolean
        Public Property ConfirmBlockReported As Boolean
        Public Property ConfirmBlockScary As Boolean
        Public Property ConfirmBlockWarned As Boolean
        Public Property ConfirmIgnoredUser As Boolean
        Public Property ConfirmIgnoredPage As Boolean
        Public Property ConfirmMultiple As Boolean
        Public Property ConfirmPartialSeries As Boolean
        Public Property ConfirmRange As Boolean
        Public Property ConfirmReport As Boolean
        Public Property ConfirmReportIgnored As Boolean
        Public Property ConfirmReportUnwarned As Boolean
        Public Property ConfirmRevertedRev As Boolean
        Public Property ConfirmRevertedUser As Boolean
        Public Property ConfirmRollback As Boolean
        Public Property ConfirmSameUser As Boolean
        Public Property ConfirmSelf As Boolean
        Public Property ConfirmSemiIgnored As Boolean
        Public Property ConfirmUseful As Boolean
        Public Property ConfirmWarnedRev As Boolean
        Public Property ConfirmWarnedUser As Boolean
        Public Property EmailAuthenticated As Date
        Public Property Minor As New Dictionary(Of String, Boolean)
        Public Property OldSession As Boolean
        Public Property RememberPassword As Boolean
        Public Property RevertAlwaysBlank As Boolean
        Public Property RevertCheckRollbackTarget As Boolean
        Public Property RevertDelete As Boolean
        Public Property RevertRollback As Boolean
        Public Property RevertSelfSummary As String
        Public Property RevertSpeedy As Boolean
        Public Property SemiIgnoreAfter As Integer
        Public Property Watch As New List(Of String)

        Public Shared ReadOnly Property CacheTime() As TimeSpan
            Get
                Return TimeSpan.FromHours(12)
            End Get
        End Property

        Public Property IsDefault() As Boolean
        Public Property IsLocalCopy() As Boolean

        Public ReadOnly Property IsWatch(ByVal key As String) As Boolean
            Get
                Return (Watch.Contains(key))
            End Get
        End Property

        Public ReadOnly Property NeedsUpdate() As Boolean
            Get
                If IsDefault Then Return True

                Try
                    If File.Exists(LocalPath) Then Return File.GetLastWriteTime(LocalPath).Add(CacheTime) < Date.Now
                Catch ex As IOException
                End Try

                Return True
            End Get
        End Property

        Public ReadOnly Property User() As User
            Get
                Return _User
            End Get
        End Property

        Public Sub Load(ByVal source As String)
            ReadConfig(source)
        End Sub

        Public Sub LoadLocal()
            Try
                If File.Exists(LocalPath) Then
                    Dim text As String = File.ReadAllText(LocalPath, Encoding.UTF8)
                    Load(text)
                    _IsDefault = False
                    _IsLocalCopy = True
                    Log.Debug("Loaded user config for {0} [L]".FormatWith(User.FullName))
                Else
                    If IsDefault Then Config.Global.NeedsUpdate = True
                End If

            Catch ex As IOException
                Log.Write(Msg("accountconfig-loadfail", ex.Message))
            End Try
        End Sub

        Public Sub SaveLocal()
            Try
                Dim path As String = IO.Path.GetDirectoryName(LocalPath)
                If Not Directory.Exists(path) Then Directory.CreateDirectory(path)
                File.WriteAllText(LocalPath, Config.MakeConfig(WriteConfig(True)), Encoding.UTF8)
                Log.Debug("Saved user config for {0} [L]".FormatWith(User.FullName))

            Catch ex As IOException
                Log.Write(Msg("accountconfig-savefail", ex.Message))
            End Try
        End Sub

        Private Sub ReadConfig(ByVal text As String)

            For Each item As KeyValuePair(Of String, String) In Config.ParseConfig("account", Nothing, text)
                Dim key As String = item.Key
                Dim value As String = item.Value

                Try
                    Select Case key
                        Case "autoignore-after" : AutoIgnoreAfter = CInt(value)
                        Case "autoignore-condition-list" : AutoIgnoreListCondition = value
                        Case "autoignore-condition-local" : AutoIgnoreLocalCondition = value
                        Case "confirm-block" : ConfirmBlock = value.ToBoolean
                        Case "confirm-blocked" : ConfirmBlocked = value.ToBoolean
                        Case "confirm-blockignored" : ConfirmBlockIgnored = value.ToBoolean
                        Case "confirm-blockreported" : ConfirmBlockReported = value.ToBoolean
                        Case "confirm-blockscary" : ConfirmBlockScary = value.ToBoolean
                        Case "confirm-blockwarned" : ConfirmBlockWarned = value.ToBoolean
                        Case "confirm-ignoredpage" : ConfirmIgnoredPage = value.ToBoolean
                        Case "confirm-ignoreduser" : ConfirmIgnoredUser = value.ToBoolean
                        Case "confirm-multiple" : ConfirmMultiple = value.ToBoolean
                        Case "confirm-partialseries" : ConfirmPartialSeries = value.ToBoolean
                        Case "confirm-range" : ConfirmRange = value.ToBoolean
                        Case "confirm-report" : ConfirmReport = value.ToBoolean
                        Case "confirm-reportignored" : ConfirmReportIgnored = value.ToBoolean
                        Case "confirm-reportunwarned" : ConfirmReportUnwarned = value.ToBoolean
                        Case "confirm-revertedrev" : ConfirmRevertedRev = value.ToBoolean
                        Case "confirm-reverteduser" : ConfirmRevertedUser = value.ToBoolean
                        Case "confirm-rollback" : ConfirmRollback = value.ToBoolean
                        Case "confirm-sameuser" : ConfirmSameUser = value.ToBoolean
                        Case "confirm-self" : ConfirmSelf = value.ToBoolean
                        Case "confirm-semiignored" : ConfirmSemiIgnored = value.ToBoolean
                        Case "confirm-useful" : ConfirmUseful = value.ToBoolean
                        Case "confirm-warnedrev" : ConfirmWarnedRev = value.ToBoolean
                        Case "confirm-warneduser" : ConfirmWarnedUser = value.ToBoolean
                        Case "contributions" : User.Contributions = CInt(value)
                        Case "created" : User.Created = value.ToDate
                        Case "display-name" : User.DisplayName = value
                        Case "email-authenticated" : EmailAuthenticated = value.ToDate

                        Case "groups"
                            User.Groups.Clear()

                            For Each group As String In value.ToList
                                User.Groups.Merge(User.Wiki.UserGroups(group))
                            Next group

                        Case "groups-add"
                            For Each group As String In value.ToList
                                User.GroupChanges(User.Wiki.UserGroups(group)).CanAdd = True
                            Next group

                        Case "groups-add-self"
                            For Each group As String In value.ToList
                                User.GroupChanges(User.Wiki.UserGroups(group)).CanAddSelf = True
                            Next group

                        Case "groups-remove"
                            For Each group As String In value.ToList
                                User.GroupChanges(User.Wiki.UserGroups(group)).CanRemove = True
                            Next group

                        Case "groups-remove-self"
                            For Each group As String In value.ToList
                                User.GroupChanges(User.Wiki.UserGroups(group)).CanRemoveSelf = True
                            Next group

                        Case "id" : User.Id = value.ToInteger
                        Case "minor"
                        Case "password"
                            If Config.Local.SavePasswords AndAlso Not User.IsAnonymous Then
                                Try
                                    User.Password = Convert.FromBase64String(value)
                                Catch ex As FormatException
                                    Log.Debug("Ignored saved password for {0}, bad format".FormatWith(User))
                                End Try
                            End If

                        Case "preferences"
                            User.Preferences.LoadFromMwFormat(Config.ParseConfig("user", key, value))
                            
                        Case "revert-alwaysblank" : RevertAlwaysBlank = value.ToBoolean
                        Case "revert-checkrollbacktarget" : RevertCheckRollbackTarget = value.ToBoolean
                        Case "revert-delete" : RevertDelete = value.ToBoolean
                        Case "revert-rollback" : RevertRollback = value.ToBoolean
                        Case "revert-speedy" : RevertSpeedy = value.ToBoolean
                        Case "semi-ignore-after" : SemiIgnoreAfter = CInt(value)
                        Case "watch"
                    End Select

                Catch ex As SystemException
                    Log.Write(Msg("error-configvalue", item.Key, "account:" & User.FullName))
                End Try
            Next item
        End Sub

        Public Function WriteConfig(ByVal local As Boolean) As Dictionary(Of String, Object)
            Dim def As UserConfig = User.Wiki.Users.Default.Config

            Dim items As New Dictionary(Of String, Object)

            If ConfirmBlock <> def.ConfirmBlock Then items.Add("confirm-block", ConfirmBlock)
            If ConfirmBlocked <> def.ConfirmBlocked Then items.Add("confirm-blocked", ConfirmBlocked)
            If ConfirmBlockIgnored <> def.ConfirmBlockIgnored Then items.Add("confirm-blockignored", ConfirmBlockIgnored)
            If ConfirmBlockReported <> def.ConfirmBlockReported Then items.Add("confirm-blockreported", ConfirmBlockReported)
            If ConfirmBlockScary <> def.ConfirmBlockScary Then items.Add("confirm-blockscary", ConfirmBlockScary)
            If ConfirmBlockWarned <> def.ConfirmBlockWarned Then items.Add("confirm-blockwarned", ConfirmBlockWarned)
            If ConfirmIgnoredPage <> def.ConfirmIgnoredPage Then items.Add("confirm-ignoredpage", ConfirmIgnoredPage)
            If ConfirmIgnoredUser <> def.ConfirmIgnoredUser Then items.Add("confirm-ignoreduser", ConfirmIgnoredUser)
            If ConfirmMultiple <> def.ConfirmMultiple Then items.Add("confirm-multiple", ConfirmMultiple)
            If ConfirmPartialSeries <> def.ConfirmPartialSeries Then items.Add("confirm-partialseries", ConfirmPartialSeries)
            If ConfirmRange <> def.ConfirmRange Then items.Add("confirm-range", ConfirmRange)
            If ConfirmReport <> def.ConfirmReport Then items.Add("confirm-report", ConfirmReport)
            If ConfirmReportIgnored <> def.ConfirmReportIgnored Then items.Add("confirm-reportignored", ConfirmReportIgnored)
            If ConfirmReportUnwarned <> def.ConfirmReportUnwarned Then items.Add("confirm-reportignored", ConfirmReportUnwarned)
            If ConfirmRevertedRev <> def.ConfirmRevertedRev Then items.Add("confirm-reportignored", ConfirmRevertedRev)
            If ConfirmRevertedUser <> def.ConfirmRevertedUser Then items.Add("confirm-reportignored", ConfirmRevertedUser)
            If ConfirmSameUser <> def.ConfirmSameUser Then items.Add("confirm-reportignored", ConfirmSameUser)
            If ConfirmSelf <> def.ConfirmSelf Then items.Add("confirm-reportignored", ConfirmSelf)
            If ConfirmSemiIgnored <> def.ConfirmSemiIgnored Then items.Add("confirm-semiignored", ConfirmSemiIgnored)
            If ConfirmUseful <> def.ConfirmUseful Then items.Add("confirm-reportignored", ConfirmUseful)
            If ConfirmWarnedRev <> def.ConfirmWarnedRev Then items.Add("confirm-reportignored", ConfirmWarnedRev)
            If ConfirmWarnedUser <> def.ConfirmWarnedUser Then items.Add("confirm-reportignored", ConfirmWarnedUser)
            If User.Contributions > -1 Then items.Add("contributions", User.Contributions)
            If User.Created > Date.MinValue Then items.Add("created", User.Created)
            If EmailAuthenticated > Date.MinValue Then items.Add("email-authenticated", EmailAuthenticated)
            If AutoIgnoreAfter <> def.AutoIgnoreAfter Then items.Add("ignore-autoafter", AutoIgnoreAfter)
            If RevertAlwaysBlank <> def.RevertAlwaysBlank Then items.Add("revert-blank", RevertAlwaysBlank)
            If RevertCheckRollbackTarget <> def.RevertCheckRollbackTarget Then items.Add("revert-checkrollbacktarget", RevertCheckRollbackTarget)
            If RevertDelete <> def.RevertDelete Then items.Add("revert-delete", RevertDelete)
            If RevertRollback <> def.RevertRollback Then items.Add("revert-rollback", RevertRollback)
            If RevertSpeedy <> def.RevertSpeedy Then items.Add("revert-speedy", RevertSpeedy)
            If SemiIgnoreAfter <> def.SemiIgnoreAfter Then items.Add("semi-ignore-after", SemiIgnoreAfter)

            If local AndAlso Not User.IsDefault Then
                If User.DisplayName <> User.Name Then items.Add("display-name", User.DisplayName)

                If Config.Local.SavePasswords AndAlso Not User.IsAnonymous AndAlso User.Password IsNot Nothing _
                    Then items.Add("password", Convert.ToBase64String(User.Password.ToArray))

                If User.Groups.Count > 0 Then items.Add("groups", User.Groups.Join(","))

                Dim canAdd As New List(Of UserGroup)
                Dim canAddSelf As New List(Of UserGroup)
                Dim canRemove As New List(Of UserGroup)
                Dim canRemoveSelf As New List(Of UserGroup)

                For Each groupChange As UserGroupChange In User.GroupChanges.All
                    If groupChange.CanAdd Then canAdd.Add(groupChange.Group)
                    If groupChange.CanAddSelf Then canAddSelf.Add(groupChange.Group)
                    If groupChange.CanRemove Then canRemove.Add(groupChange.Group)
                    If groupChange.CanRemoveSelf Then canRemoveSelf.Add(groupChange.Group)
                Next groupChange

                If canAdd.Count > 0 Then items.Add("groups-add", canAdd.Join(","))
                If canAddSelf.Count > 0 Then items.Add("groups-add-self", canAddSelf.Join(","))
                If canRemove.Count > 0 Then items.Add("groups-remove", canRemove.Join(","))
                If canRemoveSelf.Count > 0 Then items.Add("groups-remove-self", canRemoveSelf.Join(","))

                items.Add("preferences", User.Preferences.ToMwFormat.ToDictionary(Of String, Object))

                If User.UnificationDate > Date.MinValue Then items.Add("unification-date", User.UnificationDate)
                If User.UnificationMethod IsNot Nothing Then items.Add("unification-method", User.UnificationMethod)
            End If

            Return items
        End Function

        Public Function Copy(ByVal user As User) As UserConfig
            Dim result As New UserConfig(user)
            result.ReadConfig(Config.MakeConfig(WriteConfig(True)))
            result.IsDefault = IsDefault
            Return result
        End Function

    End Class

End Namespace
