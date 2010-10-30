Imports System
Imports System.Collections.Generic
Imports System.Drawing
Imports System.IO
Imports System.Net
Imports System.Text
Imports System.Windows.Forms

Imports KVP = System.Collections.Generic.KeyValuePair(Of String, String)

Namespace Huggle

    Public Class LocalConfig : Inherits Config

        Public Property AutoLogin As Boolean
        Public Property DebugEnabled As Boolean = True
        Public Property DebugVisible As Boolean
        Public Property DetectProxySettings As Boolean
        Public Property Feed As Boolean = True
        Public Property IsFirstRun As Boolean = True
        Public Property LastLogin As User
        Public Property LoginSecure As Boolean
        Public Property LogToFile As Boolean = True
        Public Property ManualProxySettings As Boolean
        Public Property Proxy As IWebProxy
        Public Property ProxyHost As String
        Public Property ProxyPort As Integer
        Public Property RcFeeds As Boolean = True
        Public Property SavePasswords As Boolean = True
        Public Property Uid As String
        Public Property UpdateState As Integer
        Public Property WindowLocation As Point
        Public Property WindowMaximized As Boolean = True
        Public Property WindowSize As Size

        Protected Overrides Function Location() As String
            Return "config"
        End Function

        Protected Overrides Sub ReadConfig(ByVal text As String)
            If Config.Global.IsLoaded Then LoadAfterGlobal(text) Else LoadBeforeGlobal(text)
        End Sub

        Private Sub LoadBeforeGlobal(ByVal text As String)
            For Each item As KVP In Config.ParseConfig("local", Nothing, text)
                Dim name As String = item.Key
                Dim value As String = item.Value

                Try
                    Select Case name
                        Case "debug-enabled" : DebugEnabled = value.ToBoolean
                        Case "debug-visible" : DebugVisible = value.ToBoolean
                        Case "detect-proxy" : DetectProxySettings = value.ToBoolean
                        Case "first-run" : IsFirstRun = value.ToBoolean
                        Case "language" : App.Languages.Current = App.Languages(value)
                        Case "log-to-file" : LogToFile = value.ToBoolean
                        Case "manual-proxy" : ManualProxySettings = value.ToBoolean
                        Case "uid" : Uid = value
                    End Select

                Catch ex As SystemException
                    Log.Write(Msg("error-configvalue", name, "local") & ": " & ex.GetType.Name)
                End Try
            Next item

            If Uid Is Nothing Then Uid = Guid.NewGuid.ToString
        End Sub

        Private Sub LoadAfterGlobal(ByVal text As String)
            For Each item As KVP In Config.ParseConfig("local", Nothing, text)
                Dim name As String = item.Key
                Dim value As String = item.Value

                Try
                    Select Case name
                        Case "accounts"
                            For Each wikiCode As KVP In Config.ParseConfig("local", "accounts", value)
                                For Each userCode As String In wikiCode.Value.ToList("|")
                                    If App.Wikis.Contains(wikiCode.Key) Then
                                        Dim user As User = App.Wikis(wikiCode.Key).Users.FromString(userCode)
                                        If user IsNot Nothing Then user.IsUsed = True
                                    End If
                                Next userCode
                            Next wikiCode

                        Case "auto-login" : AutoLogin = value.ToBoolean

                        Case "last-login"
                            Dim user As String = value.ToLast("@")
                            Dim wikiCode As String = value.FromLast("@")

                            If App.Wikis.Contains(wikiCode) Then
                                If user = "[anonymous]" Then LastLogin = App.Wikis(wikiCode).Users.Anonymous _
                                    Else LastLogin = App.Wikis(wikiCode).Users.FromString(user)
                            End If

                        Case "login-secure" : LoginSecure = value.ToBoolean
                        Case "save-passwords" : SavePasswords = value.ToBoolean
                        Case "window-location" : WindowLocation = New Point(CInt(value.ToFirst(",")), CInt(value.FromFirst(",")))
                        Case "window-maximized" : WindowMaximized = value.ToBoolean
                        Case "window-size" : WindowSize = New Size(CInt(value.ToFirst(",")), CInt(value.FromFirst(",")))
                    End Select

                Catch ex As SystemException
                    Log.Write(Msg("error-configvalue", name, "local") & ": " & ex.GetType.Name)
                End Try
            Next item
        End Sub

        Public Overrides Function WriteConfig(ByVal target As ConfigTarget) As Dictionary(Of String, Object)
            Dim items As New Dictionary(Of String, Object)

            items.Add("auto-login", AutoLogin)
            items.Add("debug-enabled", DebugEnabled)
            items.Add("debug-visible", DebugVisible)
            items.Add("detect-proxy", DetectProxySettings)
            items.Add("first-run", IsFirstRun)
            items.Add("language", App.Languages.Current.Code)
            items.Add("login-secure", LoginSecure)
            If LastLogin IsNot Nothing Then items.Add("last-login", LastLogin.FullName)
            items.Add("log-file", LogToFile)
            items.Add("manual-proxy", ManualProxySettings)
            items.Add("save-passwords", SavePasswords)
            items.Add("uid", Uid)
            items.Add("window-location", WindowLocation.X.ToString & "," & WindowLocation.Y.ToString)
            items.Add("window-maximized", WindowMaximized)
            items.Add("window-size", WindowSize.Width.ToString & "," & WindowSize.Width.ToString)

            Dim accounts As New Dictionary(Of String, Object)

            For Each wiki As Wiki In App.Wikis.All
                Dim wikiAccounts As New List(Of String)

                For Each user As User In wiki.Users.All
                    If Not user.IsAnonymous AndAlso user.IsUsed Then wikiAccounts.Add(user.Name)
                Next user

                If wikiAccounts.Count > 0 Then accounts.Add(wiki.Code, wikiAccounts.Join("|"))
            Next wiki

            If accounts.Count > 0 Then items.Add("accounts", accounts)

            Return items
        End Function

    End Class

End Namespace
