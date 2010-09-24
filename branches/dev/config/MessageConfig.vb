Imports System
Imports System.Collections.Generic
Imports System.IO

Namespace Huggle

    Public Class MessageConfig

        Private ReadOnly Property LocalPath() As String
            Get
                Return Config.Local.ConfigPath & "messages" & Slash
            End Get
        End Property

        Public Sub LoadLocal()
            'Load default languages and messages
            Dim en As Language = App.Languages("en")
            en.IsLocalized = True
            en.Messages = Config.ParseConfig("messages-en", Nothing, Resources.en)
            en.Name = "English"

            App.Languages.Default = en
            App.Languages.Current = en

            If Directory.Exists(LocalPath) Then
                'Load cached messages from config
                For Each item As String In Directory.GetFiles(LocalPath)
                    Try
                        Dim code As String = Path.GetFileNameWithoutExtension(item)
                        Dim lang As Language = App.Languages(code)
                        lang.IsLocalized = True

                        For Each msg As KeyValuePair(Of String, String) In _
                            Config.ParseConfig("messages-" & code, Nothing, IO.File.ReadAllText(item))

                            lang.Messages.Merge(msg.Key, msg.Value)
                        Next msg

                        If lang.Messages.ContainsKey("name") Then lang.Name = lang.Message("name")

                    Catch ex As AccessViolationException
                        Log.Write(Msg("language-loadfail", Path.GetFileNameWithoutExtension(item)))

                    Catch ex As ConfigException
                        Log.Write(Msg("language-loadfail", Path.GetFileNameWithoutExtension(item)))
                    End Try
                Next item
            Else
                SaveLocal()
                Config.Global.NeedsUpdate = True
            End If

            Log.Debug("Loaded messages [L]")
        End Sub

        Public Sub SaveLocal()
            Try
                If Not Directory.Exists(LocalPath) Then Directory.CreateDirectory(LocalPath)

                For Each language As Language In App.Languages.All
                    Dim path As String = LocalPath & GetValidFileName(language.Code) & ".txt"

                    If language.Messages.Count > 0 Then IO.File.WriteAllText(path, _
                        Config.MakeConfig(language.Messages.ToDictionary(Of String, Object)))
                Next language

                Log.Debug("Saved messages")

            Catch ex As UnauthorizedAccessException
                Log.Debug("Failed to save messages: " & ex.Message)
            End Try
        End Sub

    End Class

End Namespace
