Imports System.IO
Imports System.Net.Sockets
Imports System.Text.RegularExpressions
Imports System.Threading

Module Irc

    Private Reconnecting, Disconnecting As Boolean

    Public Sub IrcConnect()
        Dim IrcThread As New Thread(AddressOf IrcProcess)
        IrcThread.IsBackground = True
        IrcThread.Start()
    End Sub

    Dim EditMatch As New Regex(":rc!~rc@localhost PRIVMSG #[^:]*:14\[\[07([^]*)14\]\]4 B?(M?)B?10 02.*di" & _
    "ff=([^&]*)&oldid=([^]*) 5\* 03([^]*) 5\* \(?([^]*)?\) 10([^]*)", RegexOptions.Compiled)

    Dim NewPageMatch As New Regex(":rc!~rc@localhost PRIVMSG #[^:]*:14\[\[07([^]*)14\]\]4 (M?)N" & _
        "10 02[^ ]* 5\* 03([^]*) 5\* \(([^\)]*)\) 10([^]*)", RegexOptions.Compiled)

    Dim BlockMatch As New Regex(":rc!~rc@localhost PRIVMSG #[^:]*:14\[\[07Special:Log/block" & _
        "14\]\]4 block10 02 5\* 03([^]*?) 5\*  10blocked ""02User:([^]*?)10"" \([^\)]*\) " & _
        "with an expiry time of ([^:]*?): ([^]*?)", RegexOptions.Compiled)

    Dim UnblockMatch As New Regex(":rc!~rc@localhost PRIVMSG #[^:]*:14\[\[07Special:Log/block" & _
        "14\]\]4 unblock10 02 5\* 03([^]*) 5\*  10unblocked 02([^]*)10: ([^]*)?", _
        RegexOptions.Compiled)

    Dim DeleteMatch As New Regex(":rc!~rc@localhost PRIVMSG #[^:]*:14\[\[07Special:Log/del" & _
        "ete14\]\]4 delete10 02 5\* 03([^]*?) 5\*  10deleted ""\[\[02([^]*?)10\]" & _
        "\]"": ([^]*)", RegexOptions.Compiled)

    Dim RestoreMatch As New Regex(":rc!~rc@localhost PRIVMSG #[^:]*:14\[\[07Special:Log/delete" & _
        "14\]\]4 restore10 02 5\* 03([^]*) 5\*  10restored ""\[\[02([^]*)10\]\]""" & _
        ": ([^]*)?", RegexOptions.Compiled)

    Dim MoveMatch As New Regex(":rc!~rc@localhost PRIVMSG #[^:]*:14\[\[07Special:Log/move" & _
        "14\]\]4 move(?:_redir)?10 02 5\* 03([^]*?) 5\*  10moved \[\[02([^]*?)10\]\] to " & _
        "\[\[([^\]]*)\]\](?: over redirect)?(: ([^]*))?", RegexOptions.Compiled)

    Dim NewUserMatch As New Regex(":rc!~rc@localhost PRIVMSG #[^:]*:14\[\[07Special:Log/newusers" & _
        "14\]\]4 create10 02 5\* 03([^]*?) 5\*  10New user account", RegexOptions.Compiled)

    Dim CreateUserMatch As New Regex(":rc!~rc@localhost PRIVMSG #[^:]*:14\[\[07Special:Log/newusers" & _
        "14\]\]4 create210 02 5\* 03([^]*?) 5\*  10created new account 02User:([^]*)10", _
        RegexOptions.Compiled)

    Dim UploadMatch As New Regex(":rc!~rc@localhost PRIVMSG #[^:]*:14\[\[07Special:Log/upload" & _
       "14\]\]4 upload10 02 5\* 03([^]*) 5\*  10uploaded ""\[\[02([^]*?)10\]\]""" & _
       "(: ([^]*))??", RegexOptions.Compiled)

    Dim OverwriteMatch As New Regex(":rc!~rc@localhost PRIVMSG #[^:]*:14\[\[07Special:Log/upload" & _
        "14\]\]4 overwrite10 02 5\* 03([^]*) 5\*  10uploaded a new version of """ & _
        "\[\[02([^]*)10\]\]""(?:: )?([^]*)?", RegexOptions.Compiled)

    Dim ProtectMatch As New Regex(":rc!~rc@localhost PRIVMSG #[^:]*:14\[\[07Special:Log/protect" & _
        "14\]\]4 protect10 02 5\* 03([^]*) 5\*  10protected 02([^]*)10:([^\[]*)?(?:" & _
        "\[edit=([a-z]*):move=([a-z]*)\] )?(?:\(expires ([^\(]*) \(UTC\)\))??", RegexOptions.Compiled)

    Dim ModifyMatch As New Regex(":rc!~rc@localhost PRIVMSG #[^:]*:14\[\[07Special:Log/protect" & _
        "14\]\]4 modify10 02 5\* 03([^]*) 5\*  10changed protection level for ""\[\[02([^]*)" & _
        "10\]\]"":([^\[]*)?(?:\[edit=([a-z]*):move=([a-z]*)\])? \(expires ([^\(]*) \(UTC\)\)", _
        RegexOptions.Compiled)

    Dim UnprotectMatch As New Regex(":rc!~rc@localhost PRIVMSG #[^:]*:14\[\[07Special:Log/protect" & _
        "14\]\]4 unprotect10 02 5\* 03([^]*) 5\*  10unprotected 02([^]*)10: ([^]*)?", _
        RegexOptions.Compiled)

    Private Sub IrcProcess()
        Disconnecting = False

        'Username in RC feed IRC channels is "h_" followed by random 6-digit number
        Config.IrcUsername = "h_" & New Random(Date.UtcNow.Millisecond).NextDouble.ToString.Substring(2, 6)

        Try
            Dim Stream As NetworkStream = New TcpClient(Config.IrcServer, Config.IrcPort).GetStream
            Dim Reader As New StreamReader(Stream, System.Text.Encoding.UTF8)
            Dim Writer As New StreamWriter(Stream)

            Writer.WriteLine("USER " & Config.IrcUsername & " 8 * :" & Config.IrcUsername)
            Writer.WriteLine("NICK " & Config.IrcUsername)
            Writer.WriteLine("JOIN " & Config.IrcChannel)
            Writer.Flush()

            Dim Message As String = ""

            While True
                While Not Reader.EndOfStream
                    Message = Reader.ReadLine

                    If Message = "" Then

                    ElseIf Message.StartsWith(":" & Config.IrcServer & " 403") _
                        AndAlso Message.EndsWith("No such channel") Then

                        IrcMode = False
                        Exit Sub

                    ElseIf Message.StartsWith("PING ") Then
                        Writer.WriteLine("PONG " & Message.Substring(5))
                        Writer.Flush()

                    ElseIf EditMatch.IsMatch(Message) Then
                        Dim NewEdit As New Edit
                        Dim Match As Match = EditMatch.Match(Message)

                        NewEdit.Page = GetPage(Match.Groups(1).Value)
                        NewEdit.User = GetUser(Match.Groups(5).Value)
                        NewEdit.Id = Match.Groups(3).Value
                        NewEdit.Oldid = Match.Groups(4).Value
                        NewEdit.Size = CInt(Match.Groups(6).Value)
                        NewEdit.Summary = Match.Groups(7).Value

                        Callback(AddressOf ProcessIrcEdit, CObj(NewEdit))

                    ElseIf NewPageMatch.Match(Message).Success Then
                        Dim NewEdit As New Edit
                        Dim Match As Match = NewPageMatch.Match(Message)

                        NewEdit.Page = GetPage(Match.Groups(1).Value)
                        NewEdit.User = GetUser(Match.Groups(3).Value)
                        NewEdit.Id = "cur"
                        NewEdit.Oldid = "-1"
                        NewEdit.Size = CInt(Match.Groups(4).Value)
                        NewEdit.Summary = Match.Groups(5).Value
                        NewEdit.Prev = NullEdit
                        NewEdit.NewPage = True

                        Callback(AddressOf ProcessIrcEdit, CObj(NewEdit))

                    ElseIf NewUserMatch.IsMatch(Message) Then
                        'Dim Match As Match = NewUserMatch.Match(Message)

                    ElseIf DeleteMatch.IsMatch(Message) Then
                        Dim NewDelete As New Delete
                        Dim Match As Match = DeleteMatch.Match(Message)

                        NewDelete.Time = Date.UtcNow
                        NewDelete.Admin = GetUser(Match.Groups(1).Value)
                        NewDelete.Page = GetPage(Match.Groups(2).Value)
                        NewDelete.Action = "delete"
                        NewDelete.Comment = Match.Groups(3).Value

                        Callback(AddressOf ProcessDelete, CObj(NewDelete))

                    ElseIf BlockMatch.IsMatch(Message) Then
                        Dim NewBlock As New Block
                        Dim Match As Match = BlockMatch.Match(Message)

                        NewBlock.Time = Date.UtcNow
                        NewBlock.Admin = GetUser(Match.Groups(1).Value)
                        NewBlock.User = GetUser(Match.Groups(2).Value)
                        NewBlock.Options = Match.Groups(3).Value
                        NewBlock.Duration = Match.Groups(4).Value
                        NewBlock.Action = "block"
                        NewBlock.Comment = Match.Groups(5).Value
                        Callback(AddressOf ProcessBlock, CObj(NewBlock))

                    ElseIf MoveMatch.IsMatch(Message) Then
                        Dim NewPageMove As New PageMove
                        Dim Match As Match = MoveMatch.Match(Message)

                        NewPageMove.Time = Date.UtcNow
                        NewPageMove.User = GetUser(Match.Groups(1).Value)
                        NewPageMove.Source = Match.Groups(2).Value
                        NewPageMove.Destination = Match.Groups(3).Value
                        NewPageMove.Summary = Match.Groups(5).Value

                        Callback(AddressOf ProcessPageMove, CObj(NewPageMove))

                    ElseIf RestoreMatch.IsMatch(Message) Then
                        Dim NewRestore As New Delete
                        Dim Match As Match = NewPageMatch.Match(Message)

                        NewRestore.Time = Date.UtcNow
                        NewRestore.Admin = GetUser(Match.Groups(1).Value)
                        NewRestore.Page = GetPage(Match.Groups(2).Value)
                        NewRestore.Action = "restore"
                        NewRestore.Comment = Match.Groups(3).Value

                        Callback(AddressOf ProcessRestore, CObj(NewRestore))

                    ElseIf UnblockMatch.IsMatch(Message) Then
                        Dim NewUnblock As New Block
                        Dim Match As Match = UnblockMatch.Match(Message)

                        NewUnblock.Time = Date.UtcNow
                        NewUnblock.Admin = GetUser(Match.Groups(1).Value)
                        NewUnblock.User = GetUser(Match.Groups(2).Value)
                        NewUnblock.Options = Match.Groups(3).Value
                        NewUnblock.Duration = Match.Groups(4).Value
                        NewUnblock.Action = "unblock"
                        NewUnblock.Comment = Match.Groups(5).Value
                        Callback(AddressOf ProcessBlock, CObj(NewUnblock))

                    ElseIf UploadMatch.IsMatch(Message) Then
                        Dim NewUpload As New Upload
                        Dim Match As Match = UploadMatch.Match(Message)

                        NewUpload.Time = Date.UtcNow
                        NewUpload.User = GetUser(Match.Groups(1).Value)
                        NewUpload.File = GetPage(Match.Groups(2).Value)
                        NewUpload.Summary = Match.Groups(3).Value

                        Callback(AddressOf ProcessUpload, CObj(NewUpload))

                    ElseIf ProtectMatch.IsMatch(Message) Then
                        Dim NewProtection As New Protection
                        Dim Match As Match = ProtectMatch.Match(Message)

                        NewProtection.Admin = GetUser(Match.Groups(1).Value)
                        NewProtection.Page = GetPage(Match.Groups(2).Value)
                        NewProtection.Summary = Match.Groups(3).Value
                        NewProtection.EditLevel = Match.Groups(4).Value
                        NewProtection.MoveLevel = Match.Groups(5).Value
                        'NewProtection.Expiry = CDate(IrcProtectMatch.Groups(6).Value)

                        Callback(AddressOf ProcessProtection, CObj(NewProtection))

                    ElseIf ModifyMatch.IsMatch(Message) Then
                        Dim NewProtection As New Protection
                        Dim Match As Match = ModifyMatch.Match(Message)

                        NewProtection.Admin = GetUser(Match.Groups(1).Value)
                        NewProtection.Page = GetPage(Match.Groups(2).Value)
                        NewProtection.Summary = Match.Groups(3).Value
                        NewProtection.EditLevel = Match.Groups(4).Value
                        NewProtection.MoveLevel = Match.Groups(5).Value
                        'NewProtection.Expiry = CDate(IrcProtectMatch.Groups(6).Value)

                        Callback(AddressOf ProcessProtection, CObj(NewProtection))

                    ElseIf UnprotectMatch.IsMatch(Message) Then
                        Dim NewProtection As New Protection
                        Dim Match As Match = ProtectMatch.Match(Message)

                        NewProtection.Admin = GetUser(Match.Groups(1).Value)
                        NewProtection.Page = GetPage(Match.Groups(2).Value)
                        NewProtection.Summary = Match.Groups(3).Value

                        Callback(AddressOf ProcessProtection, CObj(NewProtection))

                    ElseIf OverwriteMatch.IsMatch(Message) Then
                        Dim NewUpload As New Upload
                        Dim Match As Match = OverwriteMatch.Match(Message)

                        NewUpload.Time = Date.UtcNow
                        NewUpload.User = GetUser(Match.Groups(1).Value)
                        NewUpload.File = GetPage(Match.Groups(2).Value)
                        NewUpload.Summary = Match.Groups(3).Value

                        Callback(AddressOf ProcessUpload, CObj(NewUpload))

                    ElseIf CreateUserMatch.IsMatch(Message) Then
                        'Dim Match As Match = CreateUserMatch.Match(Message)

                    End If

                    If Disconnecting Then
                        Reader.Close()
                        Writer.Close()
                        Stream.Close()
                        Exit Sub
                    End If
                End While

                If Reconnecting Then
                    Reconnecting = False
                    Reader.Close()
                    Writer.Close()
                    Stream.Close()
                    Callback(AddressOf IrcConnect)
                    Exit Sub
                End If

                Thread.Sleep(50)
            End While

        Catch ex As SocketException
            'Server didn't like the connection; give up and fall back to API queries
            IrcMode = False
        End Try
    End Sub

    <DebuggerStepThrough()> _
    Private Sub ProcessIrcEdit(ByVal EditObject As Object)
        Dim Edit As Edit = CType(EditObject, Edit)
        ProcessEdit(Edit)
        ProcessNewEdit(Edit)
    End Sub

    Private Sub LogIrc(ByVal MessageObject As Object)
        Log(CStr(MessageObject))
    End Sub

    Public Sub Disconnect()
        Disconnecting = True
    End Sub

    Public Sub Reconnect()
        Disconnecting = True
    End Sub

End Module