Imports System
Imports System.Reflection
Imports System.Resources
Imports System.Runtime.InteropServices
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

    Public Module Main

        Private _App As Application

        Public ReadOnly Property App As Application
            Get
                Return _App
            End Get
        End Property

        Public Property Handle As Form

        Public Sub Main()
            Try
                AddHandler AppDomain.CurrentDomain.UnhandledException, AddressOf UnhandledException
                AddHandler Windows.Forms.Application.ThreadException, AddressOf ThreadException

                Thread.CurrentThread.Name = "Main"
                Log.Reset()
                Log.Debug("Session started")

                _App = New Application
                App.Initialize()
                App.Run()

                Config.Local.Save()
                Log.Debug("Session ended")

#If Not DEBUG Then
            Catch ex As SystemException
                HandleException(ex)
#End If
            Finally
                Handle.Dispose()
                End
            End Try
        End Sub

        Private Sub ThreadException(ByVal sender As Object, ByVal e As ThreadExceptionEventArgs)
            HandleException(e.Exception)
        End Sub

        Private Sub UnhandledException(ByVal sender As Object, ByVal e As UnhandledExceptionEventArgs)
            HandleException(CType(e.ExceptionObject, Exception))
        End Sub

        Private Sub HandleException(ByVal ex As Exception)
            Dim result As Result = result.FromException(ex)
            If App IsNot Nothing Then App.ShowError(result)
            Log.Write(result.LogMessage)
        End Sub

        Public Sub CallOnMainThread(ByVal method As Action)
            If Handle Is Nothing OrElse Not Handle.InvokeRequired Then method.Invoke() Else Handle.BeginInvoke(method)
        End Sub

        Public Sub CallOnMainThread(Of T)(ByVal method As Action(Of T), ByVal param As T)
            If Handle Is Nothing OrElse Not Handle.InvokeRequired _
                Then method.Invoke(param) Else Handle.BeginInvoke(method, param)
        End Sub

        Public Sub CreateThread(ByVal method As Action, Optional ByVal callback As Action = Nothing)
            If SynchronizationContext.Current Is Nothing _
                Then SynchronizationContext.SetSynchronizationContext(New SynchronizationContext)

            Dim state As New InvokeState(SynchronizationContext.Current, method, callback)
            ThreadPool.QueueUserWorkItem(AddressOf InvokeMethod, state)
        End Sub

        Private Sub InvokeCallback(ByVal stateObject As Object)
            Dim state As InvokeState = CType(stateObject, InvokeState)
            state.Callback.Invoke()
        End Sub

        Private Sub InvokeMethod(ByVal stateObject As Object)
            Thread.CurrentThread.Name = "ThreadPool thread"

            Dim state As InvokeState = CType(stateObject, InvokeState)
            state.Method.Invoke()
            If state.Context IsNot Nothing AndAlso state.Callback IsNot Nothing _
                Then state.Context.Post(New SendOrPostCallback(AddressOf InvokeCallback), state)
        End Sub

        Private Structure InvokeState

            Public Sub New(ByVal context As SynchronizationContext, ByVal method As Action, ByVal callback As Action)
                Me.Callback = callback
                Me.Context = context
                Me.Method = method
            End Sub

            Public ReadOnly Callback As Action
            Public ReadOnly Context As SynchronizationContext
            Public ReadOnly Method As Action

        End Structure

    End Module

End Namespace