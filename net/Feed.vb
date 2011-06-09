Imports System
Imports System.Collections.Generic
Imports System.IO
Imports System.Net.Sockets
Imports System.Text.RegularExpressions
Imports System.Threading

Namespace Huggle.Net

    <Diagnostics.DebuggerDisplay("{Server}")>
    Friend Class Feed : Implements IDisposable

        Public Const BasePattern As String =
            ":[^ ]+ PRIVMSG (\#[^ ]+) :\cC14\[\[\cC07([^\cC]+)\cC14\]\]\cC4 {0}\cC10 \cC02{1}" &
            "\cC \cC5\*\cC \cC03([^\cC]+)\cC \cC5\*\cC {2} \cc10{3}(?:: ([^\cC]+))?\cC?"

        Private Shared ReadOnly AllMatch As New Regex(
            BasePattern.FormatI("([^\cC]+)", ".*?", ".*?", ".*?"), RegexOptions.Compiled)

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

        Public Event Action As SimpleEventHandler(Of QueueItem)

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

        Public ReadOnly Property ConnectionAttempted() As Boolean
            Get
                Return _ConnectionAttempted
            End Get
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
            _ConnectionAttempted = True
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
            Username = "h_" & (App.Randomness.Next Mod 1000000).ToStringI

            Try
                Using client As New TcpClient(Server, Port)
                    Dim stream As NetworkStream = client.GetStream
                    Reader = New StreamReader(stream, Text.Encoding.UTF8)
                    Writer = New StreamWriter(stream)

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
                                stream.Close()
                                State = FeedState.Disconnected
                                Exit Try

                            ElseIf State = FeedState.Reconnecting Then
                                Reader.Close()
                                Writer.Close()
                                stream.Close()
                                CallOnMainThread(AddressOf Connect)
                                Exit Try

                            ElseIf message.StartsWithI("ERROR ") Then
                                If State = FeedState.Connected Then Log.Write(Msg("feed-disconnected"))
                                State = FeedState.Reconnecting

                            ElseIf message.StartsWithI(":" & Server & " 001") AndAlso State = FeedState.Connecting Then
                                State = FeedState.Connected
                                Log.Write(Msg("feed-connected"))

                            ElseIf message.StartsWithI(":" & Server & " 403") Then
                                Log.Write(Msg("feed-nochannel"))
                                State = FeedState.Disconnected

                            ElseIf message.StartsWithI("PING ") Then
                                Writer.WriteLine("PONG " & message.Substring(5))
                                Writer.Flush()

                            ElseIf message.StartsWithI(":rc!~rc@") Then
                                Messages.Enqueue(message)
                            End If
                        End While

                        Thread.Sleep(50)
                    End While
                End Using

            Catch ex As SocketException
                'Server didn't like the connection; give up
                Dim failMsg As String = Msg("feed-error", Family)

                Select Case ex.SocketErrorCode
                    Case SocketError.HostNotFound
                        Log.Write(New Result({failMsg, Msg("feed-badserver", Server)}).LogMessage)

                    Case SocketError.TimedOut
                        Log.Write(New Result({failMsg, Msg("feed-timeout")}).LogMessage)

                    Case Else
                        Log.Write(New Result({failMsg, Msg("feed-socketerror", CInt(ex.SocketErrorCode),
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

                If Not ProcessRevision(wiki, message, result) Then
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

                    If Not ProcessLogItem(action, wiki, message, result) Then Log.Write(Msg("feed-badaction", action))
                End If

                For Each item As QueueItem In result
                    RaiseEvent Action(Me, New EventArgs(Of QueueItem)(item))
                Next item
            End While
        End Sub

        Private Function ProcessRevision(
            ByVal wiki As Wiki, ByVal message As String, ByVal result As List(Of QueueItem)) As Boolean

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
                rev.Exists = True
                rev.DetailsKnown = True
                rev.Page.Process()
                rev.User.Process()
                rev.Process()
                rev.ProcessNew()
                result.Add(rev)
                Return True
            End If

            If wiki.FeedPatterns("new").IsMatch(message) Then

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
                rev.Exists = True
                rev.DetailsKnown = True
                rev.Page.Process()
                rev.User.Process()
                rev.Process()
                rev.ProcessNew()
                result.Add(rev)

                If rev.IsReviewed Then
                    Dim review As New Review(0, wiki)
                    review.IsAutomatic = True
                    review.Comment = Nothing
                    review.Revision = rev
                    review.Time = wiki.ServerTime
                    review.Type = "newpage-patrol"
                    review.User = rev.User

                    rev.Review = review
                    result.Add(review)
                End If

                Return True
            End If

            Return False
        End Function

        Private Function ProcessLogItem(ByVal action As String, ByVal wiki As Wiki,
            ByVal message As String, ByVal result As List(Of QueueItem)) As Boolean

            Dim groups As GroupCollection = wiki.FeedPatterns(action).Match(message).Groups
            Dim logItem As LogItem = Nothing

            Select Case action
                Case "delete", "restore"
                    Dim deletion As New Deletion(0, wiki)
                    logItem = deletion

                    deletion.Comment = groups(5).Value
                    deletion.Page = wiki.Pages(groups(4).Value)
                    deletion.User = wiki.Users(groups(3).Value)


                Case "block"
                    'Ignore autoblocks
                    If groups(3).Value.StartsWithI("#") Then Exit Select

                    Dim block As New Block(0, wiki)
                    logItem = block

                    block.Comment = groups(6).Value
                    block.Duration = groups(5).Value
                    block.Expires = Date.MinValue
                    block.IsAccountCreationBlocked = groups(4).Value.Contains("account creation disabled")
                    block.IsAnonymousOnly = groups(4).Value.Contains("anon. only")
                    block.IsAutoblockEnabled = Not groups(4).Value.Contains("autoblock disabled")
                    block.IsAutomatic = MwMessageIsMatch(wiki, "autoblocker", groups(6).Value)
                    block.IsEmailBlocked = Not groups(4).Value.Contains("e-mail blocked")
                    block.IsTalkBlocked = groups(4).Value.Contains("cannot edit own talk page")
                    block.Page = wiki.Pages.FromNsAndName(wiki.Spaces.User, groups(3).Value)
                    block.User = wiki.Users.FromName(groups(2).Value)


                Case "reblock"
                    Dim block As New Block(0, wiki)
                    logItem = block

                    block.IsAccountCreationBlocked = groups(6).Value.Contains("account creation disabled")
                    block.IsAnonymousOnly = groups(6).Value.Contains("anon. only")
                    block.IsAutoblockEnabled = Not groups(6).Value.Contains("autoblock disabled")
                    block.IsAutomatic = MwMessageIsMatch(wiki, "autoblocker", groups(6).Value)
                    block.IsEmailBlocked = Not groups(6).Value.Contains("e-mail blocked")
                    block.IsTalkBlocked = groups(6).Value.Contains("cannot edit own talk page")
                    block.Comment = groups(7).Value
                    block.Expires = If(groups(5).Value = "indefinite", Date.MaxValue, CDate(groups(5).Value))
                    block.Page = wiki.Pages.FromNsAndName(wiki.Spaces.User, groups(4).Value)
                    block.User = wiki.Users.FromName(groups(3).Value)


                Case "move"
                    Dim move As New Move(0, wiki)
                    logItem = move

                    move.Comment = groups(6).Value
                    move.DestinationTitle = groups(4).Value
                    move.SourceTitle = groups(3).Value
                    move.User = wiki.Users.FromName(groups(2).Value)


                Case "unblock"
                    Dim block As New Block(0, wiki)
                    logItem = block

                    block.Comment = groups(6).Value
                    block.Page = wiki.Pages.FromNsAndName(wiki.Spaces.User, groups(3).Value)
                    block.User = wiki.Users.FromName(groups(2).Value)


                Case "upload", "overwrite"
                    Dim upload As New Upload(0, wiki)
                    logItem = upload

                    upload.Comment = groups(5).Value
                    upload.Page = wiki.Pages.FromTitle(groups(4).Value)
                    upload.User = wiki.Users(groups(3).Value)


                Case "protect", "modify"
                    Dim protection As New Protection(0, wiki)
                    logItem = protection

                    protection.Comment = groups(11).Value
                    protection.Create = ProtectionPart.FromComment(groups(5).Value, "create")
                    protection.Edit = ProtectionPart.FromComment(groups(5).Value, "edit")
                    protection.IsCascading = (groups(5).Value.Contains("[cascading]"))
                    protection.Move = ProtectionPart.FromComment(groups(5).Value, "move")
                    protection.Page = wiki.Pages.FromTitle(groups(4).Value)
                    protection.User = wiki.Users.FromName(groups(3).Value)


                Case "unprotect"
                    Dim protection As New Protection(0, wiki)
                    logItem = protection

                    protection.Comment = groups(4).Value
                    protection.Page = wiki.Pages.FromTitle(groups(3).Value)
                    protection.User = wiki.Users.FromName(groups(2).Value)


                Case "create", "autocreate"
                    Dim userCreation As New UserCreation(0, wiki)
                    logItem = userCreation

                    userCreation.IsAutomatic = (action = "autocreate")
                    userCreation.TargetUser = wiki.Users.FromName(groups(2).Value)
                    userCreation.User = wiki.Users.FromName(groups(2).Value)


                Case "create2"
                    Dim userCreation As New UserCreation(0, wiki)
                    logItem = userCreation

                    userCreation.TargetUser = wiki.Users.FromName(groups(3).Value)
                    userCreation.User = wiki.Users(groups(2).Value)


                Case "renameuser"
                    Dim userRename As New UserRename(0, wiki)
                    logItem = userRename

                    userRename.Comment = groups(6).Value
                    userRename.Page = wiki.Pages.FromNsAndName(wiki.Spaces.User, groups(3).Value)
                    userRename.TargetUser = wiki.Users.FromName(groups(4).Value)
                    userRename.User = wiki.Users.FromName(groups(2).Value)


                Case "rights"
                    Dim rightsChange As New RightsChange(0, wiki)
                    logItem = rightsChange

                    rightsChange.Comment = groups(6).Value
                    rightsChange.Rights = groups(5).Value.ToList.Trim
                    rightsChange.TargetUser = wiki.Users.FromName(groups(3).Value)
                    rightsChange.User = wiki.Users.FromName(groups(2).Value)


                Case "patrol"
                    If groups(4).Value = "" Then
                        'New page patrol
                        Dim review As New Review(0, wiki)
                        logItem = review

                        review.Comment = ""
                        review.Page = wiki.Pages.FromTitle(groups(5).Value)
                        review.Revision = review.Page.FirstRev
                        review.Type = "newpage-patrol"
                        review.User = wiki.Users.FromName(groups(3).Value)

                    Else
                        'Revision patrol
                        Dim review As New Review(0, wiki)
                        logItem = review

                        review.Comment = ""
                        review.Page = wiki.Pages.FromTitle(groups(2).Value)
                        review.Revision = wiki.Revisions.FromID(CInt(groups(2).Value))
                        review.Type = "patrol"
                        review.User = wiki.Users.FromName(groups(3).Value)

                        review.Revision.Page = review.Page
                    End If


                Case "review"
                    Dim review As New Review(0, wiki)
                    logItem = review

                    review.IsAutomatic = (action.EndsWithI("a"))
                    review.Comment = groups(6).Value
                    review.Page = wiki.Pages.FromTitle(groups(5).Value)
                    review.Revision = wiki.Revisions(CInt(groups(4).Value))
                    review.Type = groups(2).Value
                    review.User = wiki.Users.FromName(groups(3).Value)

                    review.Revision.Page = review.Page

                Case Else
                    Return False
            End Select

            logItem.Action = action
            logItem.Time = wiki.ServerTime
            result.Add(logItem)
            Return True
        End Function

        Public Overrides Function ToString() As String
            Return _Server
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