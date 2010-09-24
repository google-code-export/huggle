Imports System
Imports System.Collections.Generic
Imports System.Drawing
Imports System.IO
Imports System.Text
Imports System.Windows.Forms

Namespace Huggle

    Public Class LocalConfig

        Public Property AutoLogin As Boolean
        Public Property DebugEnabled As Boolean = True
        Public Property DebugVisible As Boolean
        Public Property DetectProxySettings As Boolean
        Public Property Feed As Boolean = True
        Public Property IsFirstRun As Boolean
        Public Property LastLogin As User
        Public Property LoginSecure As Boolean
        Public Property LogToFile As Boolean = True
        Public Property RcFeeds As Boolean = True
        Public Property SavePasswords As Boolean = True
        Public Property Uid As String
        Public Property UpdateState As Integer
        Public Property WindowLocation As Point
        Public Property WindowMaximized As Boolean = True
        Public Property WindowSize As Size

        Public Sub Load()
            Dim configFile As String = ConfigPath & "config.txt"

            If Not IO.File.Exists(configFile) Then
                Config.Local.IsFirstRun = True
            Else
                Dim Text As String = ""

                Try
                    If IO.File.Exists(configFile) Then Text = IO.File.ReadAllText(configFile, Encoding.UTF8)
                Catch ex As IOException
                    Log.Write(Msg("config-loadfail", ex.Message))
                End Try

                Dim Items As Dictionary(Of String, String) = Config.ParseConfig("local", Nothing, Text)

                For Each Item As KeyValuePair(Of String, String) In Items
                    Dim Name As String = Item.Key
                    Dim Value As String = Item.Value

                    Try
                        Select Case Name
                            Case "accounts"
                                For Each wikiCode As KeyValuePair(Of String, String) In Config.ParseConfig("local", "accounts", Value)
                                    For Each userCode As String In Value.ToList
                                        Dim user As User = App.Wikis(wikiCode.Key).Users.FromString(userCode)
                                        If user IsNot Nothing Then user.IsUsed = True
                                    Next userCode
                                Next wikiCode

                            Case "auto-login" : AutoLogin = Value.ToBoolean
                            Case "debug-enabled" : DebugEnabled = Value.ToBoolean
                            Case "debug-visible" : DebugVisible = Value.ToBoolean
                            Case "detect-proxy" : DetectProxySettings = Value.ToBoolean
                            Case "first-run" : IsFirstRun = Value.ToBoolean

                            Case "last-login"
                                Dim wiki As Wiki = App.Wikis(Value.FromLast("@"))
                                Dim user As String = Value.ToLast("@")
                                If user = "[anonymous]" Then LastLogin = wiki.Users.Anonymous _
                                    Else LastLogin = wiki.Users.FromString(user)

                            Case "log-to-file" : LogToFile = Value.ToBoolean
                            Case "login-secure" : LoginSecure = Value.ToBoolean
                            Case "save-passwords" : SavePasswords = Value.ToBoolean
                            Case "uid" : Uid = Value
                            Case "window-location" : WindowLocation = New Point(CInt(Value.ToFirst(",")), CInt(Value.FromFirst(",")))
                            Case "window-maximized" : WindowMaximized = Value.ToBoolean
                            Case "window-size" : WindowSize = New Size(CInt(Value.ToFirst(",")), CInt(Value.FromFirst(",")))
                        End Select

                    Catch ex As SystemException
                        Log.Write(Msg("error-configvalue", Name, "local") & ": " & ex.GetType.Name)
                    End Try
                Next Item
            End If

            If Uid Is Nothing Then Uid = Guid.NewGuid.ToString

            Log.Debug("Loaded local config")
        End Sub

        Public Sub Save()
            Dim items As New Dictionary(Of String, Object)

            items.Add("auto-login", AutoLogin)
            items.Add("debug-enabled", DebugEnabled)
            items.Add("debug-visible", DebugVisible)
            items.Add("detect-proxy", DetectProxySettings)
            items.Add("first-run", IsFirstRun)
            items.Add("login-secure", LoginSecure)
            If LastLogin IsNot Nothing Then items.Add("last-login", LastLogin.FullName)
            items.Add("log-file", LogToFile)
            items.Add("save-passwords", SavePasswords)
            items.Add("uid", Uid)
            items.Add("window-location", WindowLocation.X.ToString & "," & WindowLocation.Y.ToString)
            items.Add("window-maximized", WindowMaximized)
            items.Add("window-size", WindowSize.Width.ToString & "," & WindowSize.Width.ToString)

            Dim accounts As New Dictionary(Of String, Object)

            For Each wiki As Wiki In App.Wikis.All
                Dim wikiUsers As New List(Of String)

                For Each user As User In wiki.Users.All
                    If Not user.IsAnonymous AndAlso user.IsUsed Then wikiUsers.Add(user.Name)
                Next user

                If wikiUsers.Count > 0 Then accounts.Add(wiki.Code, wikiUsers.Join(","))
            Next wiki

            If accounts.Count > 0 Then items.Add("accounts", accounts)

            Dim Text As String = Config.MakeConfig(items)

            Try
                IO.File.WriteAllText(ConfigPath & "config.txt", Text, Encoding.UTF8)
                Log.Debug("Saved local config")

            Catch ex As UnauthorizedAccessException
                Log.Write(Msg("config-saveerror", ex.Message))
            End Try
        End Sub

        Public ReadOnly Property ConfigPath() As String
            Get
                Dim path As String = Environment.CurrentDirectory & Slash & "config" & Slash

                If Not Directory.Exists(path) Then
                    Try
                        Directory.CreateDirectory(path)

                    Catch ex As SystemException
                        App.ShowError(New Result(Msg("config-saveerror", ex.Message)))
                    End Try
                End If

                Return path
            End Get
        End Property

    End Class

End Namespace
