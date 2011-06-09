Imports System
Imports System.Collections.Generic
Imports System.IO

Namespace Huggle

    Friend Class MessageConfig : Inherits Config

        Private Language As Language

        Public Sub New(ByVal language As Language)
            Me.Language = language
        End Sub

        Protected Overrides Function Key() As String
            Return GetValidFileName(Language.Code)
        End Function

        Protected Overrides Function Location() As String
            Return "messages"
        End Function

        Public Overrides Sub Load(ByVal text As String)
            MyBase.Load(text)
            Language.IsLocalized = True
        End Sub

        Protected Overrides Sub ReadConfig(ByVal text As String)
            Language.Messages = ParseConfig("messages-" & Language.Code, Nothing, text)
            If Language.Messages.ContainsKey("name") Then Language.Name = Language.Messages("name")
        End Sub

        Public Overrides Function WriteConfig(ByVal target As ConfigTarget) As Dictionary(Of String, Object)
            Return Language.Messages.ToDictionary(Of String, Object)()
        End Function

        Public Shared Sub Initialize()
            'Load default languages and messages
            Dim en As Language = App.Languages("en")
            en.IsLocalized = True
            en.Name = "English"
            en.GetConfig.Load(Resources.en)

            App.Languages.Default = en
            App.Languages.Current = en

            'Load cached messages from config
            Try
                Dim languageLocation As String = PathCombine(Config.BaseLocation, "messages")

                If Directory.Exists(languageLocation) Then
                    For Each item As String In Directory.GetFiles(languageLocation)
                        Dim languageCode As String = Path.GetFileNameWithoutExtension(item)

#If DEBUG Then
                        'Ignore local en message file so that changes to internal resource file are not ignored
                        If languageCode = "en" Then Continue For
#End If

                        App.Languages(Path.GetFileNameWithoutExtension(item)).GetConfig.LoadLocal()
                    Next item
                End If

            Catch ex As SystemException
                Log.Write(Msg("language-loadfail"))
            End Try

            Log.Debug("Loaded messages [L]")
        End Sub

    End Class

End Namespace
