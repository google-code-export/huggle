Imports System
Imports System.Collections.Generic
Imports System.IO
Imports System.Net.Sockets
Imports System.Text.RegularExpressions
Imports System.Threading

Namespace Huggle

    <Diagnostics.DebuggerDisplay("{Server}")> _
    Public Class Feed : Implements IDisposable

        Public Shared ReadOnly BasePattern As String = _
            ":[^ ]+ PRIVMSG (\#[^ ]+) :\cC14\[\[\cC07([^\cC]+)\cC14\]\]\cC4 {0}\cC10 \cC02{1}" & _
            "\cC \cC5\*\cC \cC03([^\cC]+)\cC \cC5\*\cC {2} \cc10{3}(?:: ([^\cC]+))?\cC?"

        Private Shared ReadOnly AllMatch As New Regex _
            (BasePattern.FormatWith("([^\cC]+)", ".*?", ".*?", ".*?"), RegexOptions.Compiled)

        Private _ConnectionAttempted As Boolean
        Private _Family As Family
        Private _Port As Integer = 6667
        Private _Server As String
        Private _Wikis As New List(Of Wiki)

        Private LastMessageTime As Date
        Private Messages As New Queue(Of String)
        Private Reader As StreamReader
        Private State As FeedState
        Private Stream As NetworkStream
        Private Thread As Thread
        Private Username As String
        Private Writer As StreamWriter
        Private WikiCodes As New Dictionary(Of String, Wiki)

        Private ProcessTimer As Timer

        Public Event Action As EventHandler(Of QueueItem)

        Public Sub New(ByVal family As Family, ByVal server As String, ByVal port As Integer)
            _Family = family
            _Port = port
            _Server = server
        End Sub

        Public ReadOnly Property Available() As Boolean
            Get
                Return (State = FeedState.Connected AndAlso Wikis.Count > 0 _
                    AndAlso LastMessageTime.AddSeconds(30) > Date.UtcNow)
            End Get
        End Property

        Public Property ConnectionAttempted() As Boolean
            Get
                Return _ConnectionAttempted
            End Get
            Private Set(ByVal value As Boolean)
                _ConnectionAttempted = value
            End Set
        End Property

        Public ReadOnly Property Family() As Family
            Get
                Return _Family
            End Get
        End Property

        Public ReadOnly Property IsDisconnected() As Boolean
            Get
                Return (State = FeedState.Disconnected OrElse State = FeedState.Disconnecting)
            End Get
        End Property

        Public ReadOnly Property Port() As Integer
            Get
                Return _Port
            End Get
        End Property

        Public ReadOnly Property Server() As String
            Get
                Return _Server
            End Get
        End Property

        Public ReadOnly Property Wikis() As List(Of Wiki)
            Get
                Return _Wikis
            End Get
        End Property

        Public Sub AddWiki(ByVal wiki As Wiki)
            Wikis.Merge(wiki)

            If wiki.Channel IsNot Nothing Then
                WikiCodes.Merge(wiki.Channel, wiki)

                If Not IsDisconnected AndAlso Writer IsNot Nothing Then
                    Writer.WriteLine("JOIN " & wiki.Channel)
                    Writer.Flush()
                End If
            End If
        End Sub

        Public Sub RemoveWiki(ByVal wiki As Wiki)
            Wikis.Unmerge(wiki)

            If wiki.Channel IsNot Nothing Then
                WikiCodes.Unmerge(wiki.Channel)

                If Not IsDisconnected AndAlso Writer IsNot Nothing Then
                    Writer.WriteLine("PART " & wiki.Channel)
                    Writer.Flush()
                End If
            End If
        End Sub

        Public Sub Connect()
            If Server Is Nothing Then Return

            Log.Debug("Connecting to recent changes feed")
            ConnectionAttempted = True
            If State <> FeedState.Disconnected Then Disconnect()
            If ProcessTimer IsNot Nothing Then ProcessTimer.Dispose()
            ProcessTimer = New Timer(AddressOf ProcessCallback, Nothing, 0, 100)
            Thread = New Thread(AddressOf Process)
            Thread.Name = "Feed"
            Thread.Start()
        End Sub

        Public Sub Disconnect()
            State = FeedState.Disconnecting
        End Sub

        Public Sub Reconnect()
            If State = FeedState.Connecting OrElse State = FeedState.Connected _
                Then State = FeedState.Reconnecting Else Connect()
        End Sub

        Public Sub ResetState()
            Disconnect()
        End Sub

        Private Sub Process()
            State = FeedState.Connecting

            'Username in RC feed channels is "h_" followed by random 6-digit number
            Username = "h_" & (App.Randomness.Next Mod 1000000).ToString

            Try
                Stream = New TcpClient(Server, Port).GetStream
                Reader = New StreamReader(Stream, Text.Encoding.UTF8)
                Writer = New StreamWriter(Stream)

                Writer.WriteLine("USER " & Username & " 8 * :" & Username)
                Writer.WriteLine("NICK " & Username)
                Writer.Flush()

                For Each wiki As Wiki In Wikis
                    AddWiki(wiki)
                Next wiki

                Dim message As String = ""

                While True
                    While Not Reader.EndOfStream
                        message = Reader.ReadLine
                        LastMessageTime = Date.UtcNow

                        If State = FeedState.Disconnecting Then
                            Reader.Close()
                            Writer.Close()
                            Stream.Close()
                            State = FeedState.Disconnected
                            Exit Try

                        ElseIf State = FeedState.Reconnecting Then
                            Reader.Close()
                            Writer.Close()
                            Stream.Close()
                            CallOnMainThread(AddressOf Connect)
                            Exit Try

                        ElseIf message.StartsWith("ERROR ") Then
                            If State = FeedState.Connected Then Log.Write(Msg("feed-disconnected"))
                            State = FeedState.Reconnecting

                        ElseIf message.StartsWith(":" & Server & " 001") AndAlso State = FeedState.Connecting Then
                            State = FeedState.Connected
                            Log.Write(Msg("feed-connected"))

                        ElseIf message.StartsWith(":" & Server & " 403") Then
                            Log.Write(Msg("feed-nochannel"))
                            State = FeedState.Disconnected

                        ElseIf message.StartsWith("PING ") Then
                            Writer.WriteLine("PONG " & message.Substring(5))
                            Writer.Flush()

                        ElseIf message.StartsWith(":rc!~rc@") Then
                            Messages.Enqueue(message)
                        End If
                    End While

                    Thread.Sleep(50)
                End While

            Catch ex As SocketException
                'Server didn't like the connection; give up
                Dim failMsg As String = Msg("feed-error", Family)

                Select Case ex.SocketErrorCode
                    Case SocketError.HostNotFound
                        Log.Write(New Result({failMsg, Msg("feed-badserver", Server)}).LogMessage)

                    Case SocketError.TimedOut
                        Log.Write(New Result({failMsg, Msg("feed-timeout")}).LogMessage)

                    Case Else
                        Log.Write(New Result({failMsg, Msg("feed-socketerror", CInt(ex.SocketErrorCode), _
                            ex.SocketErrorCode.ToString)}).LogMessage)
                End Select

                Log.Write(Msg("feed-blocked"))
                State = FeedState.Disconnected
                Config.Local.Feed = False

            Catch ex As IOException
                'Feed was disconnected; retry
                Log.Write(Msg("feed-disconnected"))
                CallOnMainThread(AddressOf Connect)
            End Try
        End Sub

        Private Sub ProcessCallback(ByVal state As Object)
            While Messages.Count > 0
                Dim message As String = Messages.Dequeue
                Dim result As New List(Of QueueItem)
                If Not AllMatch.IsMatch(message) Then Continue While

                Dim wiki As Wiki = WikiCodes(AllMatch.Match(message).Groups(1).Value)
                If wiki.FeedPatterns.Count = 0 Then Continue While

                If wiki.FeedPatterns("edit").IsMatch(message) Then
                    Dim groups As GroupCollection = wiki.FeedPatterns("edit").Match(message).Groups
                    Dim rev As Revision = wiki.Revisions(CInt(groups(6).Value))

                    rev.Page = wiki.Pages(groups(2).Value)
                    rev.IsReviewed = String.IsNullOrEmpty(groups(3).Value)
                    rev.IsMinor = Not String.IsNullOrEmpty(groups(4).Value)
                    rev.IsBot = Not String.IsNullOrEmpty(groups(5).Value)
                    rev.Prev = wiki.Revisions(CInt(groups(7).Value))
                    If Not String.IsNullOrEmpty(groups(8).Value) Then rev.Rcid = CInt(groups(8).Value)
                    rev.User = wiki.Users(groups(9).Value)
                    rev.Change = CInt(groups(10).Value)
                    rev.Summary = groups(11).Value
                    rev.ApproxTime = wiki.ServerTime
                    rev.Exists = TS.True
                    rev.DetailsKnown = True
                    rev.Page.Process()
                    rev.User.Process()
                    rev.Process()
                    rev.ProcessNew()
                    result.Add(rev)

                ElseIf wiki.FeedPatterns("new").IsMatch(message) Then
                    Dim groups As GroupCollection = wiki.FeedPatterns("new").Match(message).Groups
                    Dim rev As Revision = wiki.Revisions(CInt(groups(6).Value))

                    rev.Page = wiki.Pages(groups(2).Value)
                    rev.IsReviewed = String.IsNullOrEmpty(groups(3).Value)
                    rev.IsMinor = Not String.IsNullOrEmpty(groups(4).Value)
                    rev.IsBot = Not String.IsNullOrEmpty(groups(5).Value)
                    If Not String.IsNullOrEmpty(groups(6).Value) Then rev.Rcid = CInt(groups(7).Value)
                    rev.User = wiki.Users(groups(8).Value)
                    rev.Change = CInt(groups(9).Value)
                    rev.Summary = groups(10).Value
                    rev.ApproxTime = wiki.ServerTime
                    rev.Prev = Revision.Null
                    rev.Exists = TS.True
                    rev.DetailsKnown = True
                    rev.Page.Process()
                    rev.User.Process()
                    rev.Process()
                    rev.ProcessNew()
                    result.Add(rev)

                    If rev.IsReviewed Then
                        Dim review As New Review( _
                            Auto:=True, _
                            Comment:=Nothing, _
                            id:=0, _
                            rcid:=0, _
                            Revision:=rev, _
                            Time:=rev.ApproxTime, _
                            Type:="newpage-patrol", _
                            User:=rev.User)

                        rev.Review = review
                        result.Add(review)
                    End If

                Else
                    Dim action As String = AllMatch.Match(message).Groups(3).Value

                    If Not wiki.FeedPatterns.ContainsKey(action) Then
                        Log.Write(Msg("feed-badaction", action))
                        Continue While
                    End If

                    Dim match As Match = wiki.FeedPatterns(action).Match(message)

                    If Not match.Success Then
                        Log.Write(Msg("feed-badpattern", action))
                        Continue While
                    End If

                    Dim groups As GroupCollection = wiki.FeedPatterns(action).Match(message).Groups

                    Select Case action
                        Case "delete", "restore"
                            result.Add(New Deletion( _
                                action:=action, _
                                Comment:=groups(5).Value, _
                                id:=0, _
                                rcid:=0, _
                                Page:=wiki.Pages(groups(4).Value), _
                                Time:=wiki.ServerTime, _
                                User:=wiki.Users(groups(3).Value)))

                        Case "block"
                            'Ignore autoblocks
                            If Not groups(3).Value.StartsWith("#") Then result.Add(New Block( _
                                action:=action, _
                                anonOnly:=groups(4).Value.Contains("anon. only"), _
                                autoBlock:=Not groups(4).Value.Contains("autoblock disabled"), _
                                automatic:=MessageMatch(wiki.Message("autoblocker"), groups(6).Value).Success, _
                                blockCreation:=groups(4).Value.Contains("account creation disabled"), _
                                blockEmail:=Not groups(4).Value.Contains("e-mail blocked"), _
                                blockTalk:=groups(4).Value.Contains("cannot edit own talk page"), _
                                Comment:=groups(6).Value, _
                                duration:=groups(5).Value, _
                                expires:=Date.MinValue, _
                                id:=0, _
                                rcid:=0, _
                                target:=groups(3).Value, _
                                time:=wiki.ServerTime, _
                                User:=wiki.Users(groups(2).Value)))

                        Case "reblock"
                            result.Add(New Block( _
                                action:=action, _
                                automatic:=MessageMatch(wiki.Message("autoblocker"), groups(7).Value).Success, _
                                anonOnly:=groups(6).Value.Contains("anon. only"), _
                                autoBlock:=Not groups(6).Value.Contains("autoblock disabled"), _
                                blockCreation:=groups(6).Value.Contains("account creation disabled"), _
                                blockEmail:=Not groups(6).Value.Contains("e-mail blocked"), _
                                blockTalk:=groups(6).Value.Contains("cannot edit own talk page"), _
                                Comment:=groups(7).Value, _
                                duration:="", _
                                expires:=If(groups(5).Value = "indefinite", Date.MaxValue, CDate(groups(5).Value)), _
                                id:=0, _
                                rcid:=0, _
                                time:=wiki.ServerTime, _
                                target:=groups(4).Value, _
                                User:=wiki.Users(groups(3).Value)))

                        Case "move"
                            Dim move As New Move( _
                                Comment:=groups(6).Value, _
                                Destination:=groups(4).Value, _
                                id:=0, _
                                rcid:=0, _
                                Source:=groups(3).Value, _
                                Time:=wiki.ServerTime, _
                                User:=wiki.Users(groups(2).Value))

                            wiki.Pages(move.Source).MovedTo(move.Destination)
                            result.Add(move)

                        Case "unblock"
                            result.Add(New Block( _
                                action:=action, _
                                anonOnly:=False, _
                                autoBlock:=False, _
                                automatic:=False, _
                                blockCreation:=False, _
                                blockEmail:=False, _
                                blockTalk:=False, _
                                Comment:=groups(6).Value, _
                                duration:=Nothing, _
                                expires:=Date.MinValue, _
                                id:=0, _
                                rcid:=0, _
                                Target:=groups(3).Value, _
                                Time:=wiki.ServerTime, _
                                User:=wiki.Users(groups(2).Value)))

                        Case "upload", "overwrite"
                            result.Add(New Upload( _
                                action:=action, _
                                Comment:=groups(5).Value, _
                                File:=groups(4).Value, _
                                id:=0, _
                                rcid:=0, _
                                Time:=wiki.ServerTime, _
                                User:=wiki.Users(groups(3).Value)))

                        Case "protect", "modify"
                            result.Add(New Protection( _
                                action:=action, _
                                Cascade:=False, _
                                Comment:=groups(11).Value, _
                                Hidden:=False, _
                                id:=0, _
                                rcid:=0, _
                                Page:=wiki.Pages(groups(4).Value), _
                                Time:=wiki.ServerTime, _
                                Levels:=groups(5).Value, _
                                User:=wiki.Users(groups(3).Value)))

                        Case "unprotect"
                            result.Add(New Protection( _
                                action:=action, _
                                Cascade:=False, _
                                Comment:=groups(4).Value, _
                                Hidden:=False, _
                                id:=0, _
                                rcid:=0, _
                                Page:=wiki.Pages(groups(3).Value), _
                                Time:=wiki.ServerTime, _
                                Levels:=Nothing, _
                                User:=wiki.Users(groups(2).Value)))

                        Case "create", "autocreate"
                            wiki.Users.NewUsers.Add(wiki.Users(groups(2).Value))

                            result.Add(New UserCreation( _
                                Auto:=(action = "autocreate"), _
                                id:=0, _
                                rcid:=0, _
                                Time:=wiki.ServerTime, _
                                User:=wiki.Users(groups(2).Value)))

                        Case "create2"
                            wiki.Users.NewUsers.Add(wiki.Users(groups(3).Value))

                            result.Add(New UserCreation( _
                                Auto:=False, _
                                id:=0, _
                                rcid:=0, _
                                Time:=wiki.ServerTime, _
                                User:=wiki.Users(groups(2).Value)))

                        Case "renameuser"
                            Dim rename As New UserRename( _
                                Comment:=groups(6).Value, _
                                Destination:=groups(4).Value, _
                                id:=0, _
                                rcid:=0, _
                                Source:=groups(3).Value, _
                                Time:=wiki.ServerTime, _
                                User:=wiki.Users(groups(2).Value))

                            wiki.Users(rename.Source).Rename(rename.Destination)
                            result.Add(rename)

                        Case "rights"
                            result.Add(New RightsChange( _
                                Comment:=groups(6).Value, _
                                id:=0, _
                                rcid:=0, _
                                Rights:=groups(5).Value.ToList.Trim, _
                                TargetUser:=wiki.Users(groups(3).Value), _
                                Time:=wiki.ServerTime, _
                                User:=wiki.Users(groups(2).Value)))

                        Case "patrol"
                            If groups(4).Value = "" Then
                                'New page patrol
                                Dim page As Page = wiki.Pages(groups(5).Value)
                                page.IsPatrolled = True

                                result.Add(New Review( _
                                    Auto:=False, _
                                    Comment:="", _
                                    id:=0, _
                                    rcid:=0, _
                                    Revision:=page.FirstRev, _
                                    Time:=wiki.ServerTime, _
                                    Type:="newpage-patrol", _
                                    User:=wiki.Users(groups(3).Value)))

                            Else
                                'Revision patrol
                                Dim rev As Revision = wiki.Revisions(CInt(groups(2).Value))
                                rev.Page = wiki.Pages(groups(2).Value)

                                result.Add(New Review( _
                                    Auto:=False, _
                                    Comment:="", _
                                    id:=0, _
                                    rcid:=0, _
                                    Revision:=rev, _
                                    Time:=wiki.ServerTime, _
                                    Type:="patrol", _
                                    User:=wiki.Users(groups(3).Value)))
                            End If

                        Case "review"
                            Dim rev As Revision = wiki.Revisions(CInt(groups(4).Value))

                            rev.Page = wiki.Pages(groups(5).Value)

                            result.Add(New Review( _
                                Auto:=False, _
                                Comment:=groups(6).Value, _
                                id:=0, _
                                rcid:=0, _
                                Revision:=rev, _
                                Time:=wiki.ServerTime, _
                                Type:=groups(2).Value, _
                                User:=wiki.Users(groups(3).Value)))

                        Case Else
                            Log.Write(Msg("feed-badaction", action))
                    End Select
                End If

                For Each item As QueueItem In result
                    RaiseEvent Action(Me, item)
                Next item
            End While
        End Sub

        Public Overrides Function ToString() As String
            Return _Server
        End Function

        Private Function ParseExpiry(ByVal str As String) As Date
            If String.IsNullOrEmpty(str) Then Return Date.MinValue
            If str = "indefinite" Then Return Date.MaxValue
            Return CDate(str.FromFirst(" ").Remove(" (UTC)"))
        End Function

        Private Enum FeedState As Integer
            : Disconnected : Connecting : Connected : Disconnecting : Reconnecting
        End Enum

        Protected Overridable Sub Dispose(ByVal disposing As Boolean)
            Static disposed As Boolean

            If Not disposed Then
                If disposing Then
                    Reader.Dispose()
                    Writer.Dispose()
                    Stream.Dispose()
                    ProcessTimer.Dispose()
                End If
            End If

            disposed = True
        End Sub

        Public Sub Dispose() Implements IDisposable.Dispose
            Dispose(True)
            GC.SuppressFinalize(Me)
        End Sub

    End Class

End Namespace