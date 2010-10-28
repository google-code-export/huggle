Imports Huggle.Actions
Imports Huggle.UI
Imports System
Imports System.Collections.Generic
Imports System.Drawing
Imports System.IO
Imports System.Net
Imports System.Text.RegularExpressions
Imports System.Windows.Forms

Namespace Huggle

    Public Class Application

        Private _Families As FamilyCollection
        Private _Languages As LanguageCollection
        Private _Randomness As New Random
        Private _Sessions As SessionCollection
        Private _Wikis As WikiCollection

        Public Sub Run()
            Windows.Forms.Application.EnableVisualStyles()
            Windows.Forms.Application.SetCompatibleTextRenderingDefault(False)

            'Create a dummy form we can call Invoke on from other threads to manipulate the UI
            'Access window handle to force creation without actually displaying the form
            Handle = New Form
            Dim ptr As IntPtr = Handle.Handle

            ServicePointManager.Expect100Continue = False
            ServicePointManager.DefaultConnectionLimit = 4
            HttpWebRequest.DefaultWebProxy = Nothing

            Config.Global.DownloadLocation = Config.Internal.DownloadUrl.ToString
            Config.Global.IsDefault = True
            Config.Global.LatestVersion = Version
            Config.Global.WikiConfigPageTitle = "Project:Huggle/Config"

            'This information needs to be hardcoded so that the global configuration can be located
            Dim metaWiki As Wiki = Wikis("meta")
            metaWiki.Family = Families.Wikimedia
            metaWiki.Url = New Uri("http://meta.wikimedia.org/w/")
            Families.Wikimedia.CentralWiki = metaWiki
            Families.Wikimedia.Name = "Wikimedia"
            Wikis.Global = metaWiki

            'Load configuration
            Config.Local.LoadBeforeGlobal()
            Config.Messages.LoadLocal()
            Config.Global.LoadLocal()

            'Show first-time preferences form
            If Config.Local.IsFirstRun Then
                Log.Debug("Doing first run")

                Using form As New FirstRunForm
                    If form.ShowDialog <> DialogResult.OK Then Return
                End Using

                Config.Local.IsFirstRun = False
            End If

            'Detect proxy settings if necessary
            If Config.Local.DetectProxySettings Then UserWaitForProcess _
                (New GeneralProcess(Msg("config-proxy"), AddressOf GetProxy))

            'Load global config if out of date
            If Config.Global.NeedsUpdate Then
                UserWaitForProcess(Config.Global.Loader, Nothing, True)
                If Config.Global.Loader.IsFailed Then Return
            End If

            Config.Local.LoadAfterGlobal()

            'As connection to IRC feed can take several seconds, anticipate the user selecting a
            'Wikimedia wiki, and try to make the feed available as soon as possible
            If Config.Local.RcFeeds AndAlso Families.Wikimedia.Feed IsNot Nothing Then Families.Wikimedia.Feed.Connect()

            Dim mainSession As Session = Nothing

            'Login automatically if configured to do so
            If Config.Local.AutoLogin AndAlso Config.Local.LastLogin IsNot Nothing Then
                Dim user As User = Config.Local.LastLogin
                user.Config.LoadLocal()

                If user.IsAnonymous OrElse user.Password IsNot Nothing Then
                    mainSession = App.Sessions(user)
                    Dim login As New Login(mainSession, "Automatic login")
                    UserWaitForProcess(login, Msg("login-error-auto"))

                    If login.IsFailed Then
                        mainSession.Discard()
                        mainSession = Nothing
                    End If
                End If
            End If

            While True
                'Show login form
                If mainSession Is Nothing Then
                    Using loginForm As New LoginForm
                        If loginForm.ShowDialog <> DialogResult.OK Then Return
                        mainSession = loginForm.Session
                    End Using
                End If

                'Show main form
                Using mainForm As New MainForm(mainSession)
                    If mainForm.ShowDialog() <> DialogResult.OK Then Return
                End Using

                mainSession = Nothing
            End While
        End Sub

        Public ReadOnly Property Families() As FamilyCollection
            Get
                If _Families Is Nothing Then _Families = New FamilyCollection
                Return _Families
            End Get
        End Property

        Public ReadOnly Property IsMono() As Boolean
            Get
                Return Type.GetType("Mono.Runtime", False) IsNot Nothing
            End Get
        End Property

        Public ReadOnly Property Languages() As LanguageCollection
            Get
                If _Languages Is Nothing Then _Languages = New LanguageCollection
                Return _Languages
            End Get
        End Property

        Public ReadOnly Property Name As String
            Get
                Return Windows.Forms.Application.ProductName
            End Get
        End Property

        Public ReadOnly Property Randomness As Random
            Get
                Return _Randomness
            End Get
        End Property

        Public ReadOnly Property Sessions() As SessionCollection
            Get
                If _Sessions Is Nothing Then _Sessions = New SessionCollection
                Return _Sessions
            End Get
        End Property

        Public ReadOnly Property Version As Version
            Get
                Return Reflection.Assembly.GetExecutingAssembly.GetName.Version
            End Get
        End Property

        Public ReadOnly Property VersionString As String
            Get
                Return Windows.Forms.Application.ProductVersion
            End Get
        End Property

        Public ReadOnly Property Wikis() As WikiCollection
            Get
                If _Wikis Is Nothing Then _Wikis = New WikiCollection
                Return _Wikis
            End Get
        End Property

        Public Function ShowError(ByVal result As Result, Optional ByVal showRetry As Boolean = False) As DialogResult
            Using form As New ErrorForm(result.ErrorMessage, showRetry)
                Return form.ShowDialog()
            End Using
        End Function

        Public Function ShowPrompt _
            (ByVal title As String, ByVal largeText As String, ByVal smallText As String, _
            ByVal defaultButton As Integer, ByVal ParamArray buttons As String()) As Integer

            Return Prompt.Show(title, largeText, smallText, defaultButton, buttons)
        End Function

        Public Sub UserWaitForProcess(ByVal process As Process,
            Optional ByVal errorMessage As String = Nothing, Optional ByVal retryable As Boolean = False)

            'Display cancellable progress dialog while executing an action on another thread
            'If action fails, display error message

            Do
                Using waitForm As New WaitForm(Msg("wait-generic"))
                    If process.IsComplete Then Return

                    AddHandler process.Progress, AddressOf waitForm.UpdateByProcess
                    AddHandler process.Complete, AddressOf waitForm.CloseByProcess

                    If Not process.IsRunning Then CreateThread(AddressOf process.Start)
                    waitForm.ShowDialog()
                    If waitForm.Cancelled Then process.Cancel()

                    RemoveHandler process.Progress, AddressOf waitForm.UpdateByProcess
                    RemoveHandler process.Complete, AddressOf waitForm.CloseByProcess
                End Using

                Dim fullMessage As Result = If(errorMessage Is Nothing, process.Result, process.Result.Wrap(errorMessage))

                If process.IsErrored AndAlso App.ShowError(fullMessage, retryable) = DialogResult.Retry Then
                    process.Reset()
                    Continue Do
                Else
                    Exit Do
                End If
            Loop
        End Sub

        Public Sub WaitFor(ByVal condition As Expression)
            While Not condition()
                Threading.Thread.Sleep(200)
            End While
        End Sub

        Private Sub GetProxy()
            If Not IsMono AndAlso HttpWebRequest.DefaultWebProxy Is Nothing _
                Then HttpWebRequest.DefaultWebProxy = HttpWebRequest.GetSystemWebProxy
        End Sub

    End Class

    Public NotInheritable Class Icons

        Private Sub New()
        End Sub

        Friend Shared ReadOnly Anon As Image = Resources.blob_anon
        Friend Shared ReadOnly AnonFilter As Image = Resources.blob_anon_filter
        Friend Shared ReadOnly Blanked As Image = Resources.blob_blanked
        Friend Shared ReadOnly Blocked As Image = Resources.blob_blocked
        Friend Shared ReadOnly BlockNote As Image = Resources.blob_blocknote
        Friend Shared ReadOnly Bot As Image = Resources.blob_bot
        Friend Shared ReadOnly Filter As Image = Resources.blob_filter
        Friend Shared ReadOnly Ignored As Image = Resources.blob_ignored
        Friend Shared ReadOnly IgnoredAssisted As Image = Resources.blob_ignored_assisted
        Friend Shared ReadOnly [Me] As Image = Resources.blob_me
        Friend Shared ReadOnly Message As Image = Resources.blob_message
        Friend Shared ReadOnly [New] As Image = Resources.blob_new
        Friend Shared ReadOnly None As Image = Resources.blob_none
        Friend Shared ReadOnly NoneAssisted As Image = Resources.blob_none_assisted
        Friend Shared ReadOnly NoneFilter As Image = Resources.blob_none_filter
        Friend Shared ReadOnly Redirect As Image = Resources.blob_redirect
        Friend Shared ReadOnly Replaced As Image = Resources.blob_replaced
        Friend Shared ReadOnly Report As Image = Resources.blob_report
        Friend Shared ReadOnly Reported As Image = Resources.blob_reported
        Friend Shared ReadOnly Revert As Image = Resources.blob_revert
        Friend Shared ReadOnly Reverted As Image = Resources.blob_reverted
        Friend Shared ReadOnly Tag As Image = Resources.blob_tag
        Friend Shared ReadOnly Warned1 As Image = Resources.blob_warn_1
        Friend Shared ReadOnly Warned2 As Image = Resources.blob_warn_2
        Friend Shared ReadOnly Warned3 As Image = Resources.blob_warn_3
        Friend Shared ReadOnly Warned4 As Image = Resources.blob_warn_4
        Friend Shared ReadOnly Warning1 As Image = Resources.blob_warning_1
        Friend Shared ReadOnly Warning2 As Image = Resources.blob_warning_2
        Friend Shared ReadOnly Warning3 As Image = Resources.blob_warning_3
        Friend Shared ReadOnly Warning4 As Image = Resources.blob_warning_4

    End Class

End Namespace