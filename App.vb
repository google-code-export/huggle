Imports Huggle.Actions
Imports System
Imports System.Collections.Generic
Imports System.Drawing
Imports System.IO
Imports System.Net
Imports System.Reflection
Imports System.Resources
Imports System.Runtime.InteropServices
Imports System.Text.RegularExpressions
Imports System.Threading
Imports System.Windows.Forms

<Assembly: AssemblyTitle("Huggle")> 
<Assembly: AssemblyDescription("")> 
<Assembly: AssemblyCompany("")> 
<Assembly: AssemblyFileVersion("1.0.0.0")> 
<Assembly: AssemblyProduct("Huggle")> 
<Assembly: AssemblyCopyright("")> 
<Assembly: AssemblyTrademark("")> 
<Assembly: AssemblyVersion("1.0.0.0")> 
<Assembly: CLSCompliant(True)> 
<Assembly: ComVisible(False)> 
<Assembly: Guid("abf5d615-ff1e-477c-99ab-99a64b10db73")> 
<Assembly: NeutralResourcesLanguage("en")> 

Namespace Huggle

    Public Class App

        Private Shared _Families As FamilyCollection
        Private Shared _Languages As LanguageCollection
        Private Shared _Sessions As SessionCollection
        Private Shared _Wikis As WikiCollection

        Public Shared Property Context() As SynchronizationContext

        Public Shared ReadOnly Property Families() As FamilyCollection
            Get
                If _Families Is Nothing Then _Families = New FamilyCollection
                Return _Families
            End Get
        End Property

        Public Shared Property Handle() As Control

        Public Shared ReadOnly Property IsMono() As Boolean
            Get
                Return Type.GetType("Mono.Runtime", False) IsNot Nothing
            End Get
        End Property

        Public Shared ReadOnly Property Languages() As LanguageCollection
            Get
                If _Languages Is Nothing Then _Languages = New LanguageCollection
                Return _Languages
            End Get
        End Property

        Public Shared ReadOnly Property Sessions() As SessionCollection
            Get
                If _Sessions Is Nothing Then _Sessions = New SessionCollection
                Return _Sessions
            End Get
        End Property

        Public Shared ReadOnly Property Wikis() As WikiCollection
            Get
                If _Wikis Is Nothing Then _Wikis = New WikiCollection
                Return _Wikis
            End Get
        End Property

        Public Shared Sub Main()
            Try
                AddHandler AppDomain.CurrentDomain.UnhandledException, AddressOf UnhandledException
                AddHandler Application.ThreadException, AddressOf ThreadException

                Log.Reset()
                Log.Debug("Session started")

                Initialize()

                Config.Local.Load()
                Config.Messages.LoadLocal()
                Config.Global.LoadLocal()

                If Config.Local.IsFirstRun Then FirstRun()

                'Detect proxy settings if necessary
                If Config.Local.DetectProxySettings Then UserWaitForProcess _
                    (New GeneralProcess(Msg("config-proxy"), AddressOf GetProxy))

                'Load global config if out of date
                If Config.Global.NeedsUpdate Then
                    UserWaitForProcess(Config.Global.Loader)
                    If Config.Global.Loader.IsFailed Then ShowError(Config.Global.Loader.Result) : Shutdown()
                End If

                'As connection to IRC feed can take several seconds, anticipate the user selecting a
                'Wikimedia wiki, and try to make the feed available as soon as possible
                If Config.Local.RcFeeds AndAlso Families.Wikimedia.Feed IsNot Nothing Then Families.Wikimedia.Feed.Connect()

                'Login automatically if configured to do so
                If Config.Local.AutoLogin AndAlso Config.Local.LastLogin IsNot Nothing Then
                    Config.Local.LastLogin.Config.LoadLocal()

                    Dim login As New Login(Config.Local.LastLogin.Session, "Automatic login")
                    UserWaitForProcess(login)
                    If login.IsFailed Then ShowError(login.Result.Wrap(Msg("login-error-auto")))
                End If

                Dim session As Session = Nothing
                If Config.Local.LastLogin IsNot Nothing Then session = Config.Local.LastLogin.Session

                While True
                    'Show login form
                    If session Is Nothing OrElse Not session.IsActive Then
                        Dim loginForm As New LoginForm
                        If loginForm.ShowDialog <> DialogResult.OK Then Exit While
                        session = loginForm.Session
                    End If

                    'Show main form
                    Dim mainForm As New MainForm(session)
                    If mainForm.ShowDialog() <> DialogResult.OK Then Exit While
                End While

            Catch ex As SystemException
                ShowError(Result.FromException(ex).Wrap("Error loading {0}".FormatWith(Application.ProductName)))

            Finally
                Shutdown()
            End Try
        End Sub

        Public Shared Sub Initialize()
            SynchronizationContext.SetSynchronizationContext(New SynchronizationContext)
            Context = SynchronizationContext.Current
            Thread.CurrentThread.Name = "Main"

            Application.EnableVisualStyles()
            Application.SetCompatibleTextRenderingDefault(False)

            'Create a dummy form we can call Invoke on from other threads to manipulate the UI
            'Access window handle to force it to be created without actually displaying the form
            Handle = New Form
            Dim ptr As IntPtr = Handle.Handle

            ServicePointManager.Expect100Continue = False
            ServicePointManager.DefaultConnectionLimit = 4
            HttpWebRequest.DefaultWebProxy = Nothing

            Config.Global.DownloadLocation = "http://code.google.com/p/huggle"
            Config.Global.IsDefault = True
            Config.Global.LatestVersion = Assembly.GetExecutingAssembly.GetName.Version
            Config.Global.WikiConfigPageName = "Project:Huggle/Config"

            Dim metaWiki As Wiki = Wikis("meta")
            metaWiki.Family = App.Families.Wikimedia
            metaWiki.Url = New Uri("http://meta.wikimedia.org/w")

            Dim commonsWiki As Wiki = Wikis("commons")
            commonsWiki.Family = App.Families.Wikimedia
            commonsWiki.Url = New Uri("http://commons.wikimedia.org/w")

            App.Families.Wikimedia.CentralWiki = metaWiki
            App.Families.Wikimedia.Feed = New Feed(Families.Wikimedia, "irc.wikimedia.org")
            App.Families.Wikimedia.FileWiki = commonsWiki
            App.Families.Wikimedia.Name = "Wikimedia"

            App.Wikis.Global = metaWiki
        End Sub

        Private Shared Sub GetProxy()
            If Not IsMono AndAlso HttpWebRequest.DefaultWebProxy Is Nothing _
                Then HttpWebRequest.DefaultWebProxy = HttpWebRequest.GetSystemWebProxy
        End Sub

        Private Shared Sub ThreadException(ByVal sender As Object, ByVal e As ThreadExceptionEventArgs)
            HandleException(e.Exception)
        End Sub

        Private Shared Sub UnhandledException(ByVal sender As Object, ByVal e As UnhandledExceptionEventArgs)
            HandleException(CType(e.ExceptionObject, Exception))
        End Sub

        Private Shared Sub HandleException(ByVal ex As Exception)
            Dim result As Result = result.FromException(ex)
            Log.Write(result.LogMessage)
            App.ShowError(result)
        End Sub

        Public Shared Sub Invoke(ByVal method As Action)
            Handle.BeginInvoke(method)
        End Sub

        Public Shared Sub Post(ByVal method As SendOrPostCallback, ByVal state As Object)
            App.Context.Post(method, state)
        End Sub

        Public Shared Sub ShowError(ByVal result As Result)
            Dim form As New ErrorForm(result.ErrorMessage)
            form.ShowDialog()
        End Sub

        Public Shared Sub ShowError(ByVal ex As Exception)
            ShowError(Result.FromException(ex))
        End Sub

        Public Shared Sub ShowError(ByVal message As String)
            ShowError(New Result(message))
        End Sub
        Public Shared Sub Start(ByVal method As Action, Optional ByVal callback As Action = Nothing)
            If SynchronizationContext.Current Is Nothing _
                Then SynchronizationContext.SetSynchronizationContext(New SynchronizationContext)

            Dim state As New InvokeState(SynchronizationContext.Current, method, callback)
            ThreadPool.QueueUserWorkItem(AddressOf InvokeMethod, state)
        End Sub

        'Display a cancellable progress dialog while executing an action on another thread
        Public Shared Sub UserWaitForProcess(ByVal process As Process)
            Dim waitForm As New WaitForm(Msg("wait-generic"))
            AddHandler process.Progress, AddressOf waitForm.UpdateByProcess
            AddHandler process.Complete, AddressOf waitForm.CloseByProcess
            If process.IsComplete Then Return
            If Not process.IsRunning Then Start(AddressOf process.Start)
            waitForm.ShowDialog()
            If waitForm.Cancelled Then process.Cancel()
        End Sub

        Public Shared Sub WaitFor(ByVal condition As Expression)
            While Not condition()
                Threading.Thread.Sleep(1000)
            End While
        End Sub

        Private Shared Sub InvokeCallback(ByVal stateObject As Object)
            Dim state As InvokeState = CType(stateObject, InvokeState)
            state.Callback.Invoke()
        End Sub

        Private Shared Sub InvokeMethod(ByVal stateObject As Object)
            Dim state As InvokeState = CType(stateObject, InvokeState)
            state.Method.Invoke()
            If state.Context IsNot Nothing AndAlso state.Callback IsNot Nothing _
                Then state.Context.Post(New SendOrPostCallback(AddressOf InvokeCallback), state)
        End Sub

        Public Shared Sub FirstRun()
            Log.Debug("Doing first run")
            Dim form As New FirstRunForm
            If form.ShowDialog <> DialogResult.OK Then App.Shutdown()
            Config.Local.IsFirstRun = False
        End Sub

        ''' <summary>Terminate the application</summary>
        Public Shared Sub Shutdown()
            Config.Local.Save()
            Log.Debug("Session ended")
            End
        End Sub

        Private Structure InvokeState

            Private _Callback As Action
            Private _Context As SynchronizationContext
            Private _Method As Action

            Public Sub New(ByVal Context As SynchronizationContext, ByVal Method As Action, ByVal Callback As Action)
                _Callback = Callback
                _Context = Context
                _Method = Method
            End Sub

            Public ReadOnly Property Callback() As Action
                Get
                    Return _Callback
                End Get
            End Property

            Public ReadOnly Property Context() As SynchronizationContext
                Get
                    Return _Context
                End Get
            End Property

            Public ReadOnly Property Method() As Action
                Get
                    Return _Method
                End Get
            End Property

        End Structure

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