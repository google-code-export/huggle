Imports System.Collections.Generic

Namespace Huggle

    Public Class MessageConfig : Inherits Config

        Private Language As Language

        Public Sub New(ByVal language As Language)
            Me.Language = language
        End Sub

        Protected Overrides ReadOnly Property Location() As String
            Get
                Return PathCombine("messages", GetValidFileName(Language.Code))
            End Get
        End Property

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

    End Class

End Namespace
