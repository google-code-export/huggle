Imports Huggle
Imports Huggle.Actions
Imports System
Imports System.Collections.Generic

Namespace Huggle.UI

    Public Class AccountPreferencesForm

        Private _User As User

        Public Sub New(ByVal user As User)
            InitializeComponent()
            _User = user
        End Sub

        Public ReadOnly Property User() As User
            Get
                Return _User
            End Get
        End Property

        Private Sub _Load() Handles Me.Load
            Try
                Icon = Resources.Icon
                Text = Msg("accountprefs-title", User.FullName)

                'Finish loading extra config
                If Not User.Wiki.Config.ExtraConfigLoaded Then
                    App.UserWaitForProcess(User.Wiki.Config.ExtraLoader)
                    If User.Wiki.Config.ExtraLoader.IsFailed _
                        Then App.ShowError(User.Wiki.Config.ExtraLoader.Result) : Close() : Return
                End If

                LoadData(User.Preferences)

            Catch ex As SystemException
                App.ShowError(Result.FromException(ex))
                Close()
            End Try
        End Sub

        Private Sub CancelBtn_Click() Handles CancelBtn.Click
            Close()
        End Sub

        Private Sub Defaults_Click() Handles Defaults.Click
            Dim reset As New ResetPreferences(App.Sessions(User))
            App.UserWaitForProcess(reset)
            If reset.IsErrored Then App.ShowError(reset.Result)
        End Sub

        Private Sub Save_Click() Handles Save.Click
            Dim setPrefs As New SetPreferences(App.Sessions(User), StoreData(User.Preferences))
            App.UserWaitForProcess(setPrefs)
            If setPrefs.IsErrored Then App.ShowError(setPrefs.Result)
            If setPrefs.IsComplete Then Close()
        End Sub

        Private Sub LoadData(ByVal prefs As Preferences)
            'General
            For Each language As Language In App.Languages.All
                InterfaceLanguage.Items.Add(language)
            Next language

            InterfaceLanguage.SelectedItem = App.Languages.Default

            Gender.Items.Add(Msg("a-notspecified"))
            Gender.Items.Add(Msg("a-male"))
            Gender.Items.Add(Msg("a-female"))
            Gender.SelectedIndex = 0

            If prefs.Language Is Nothing Then InterfaceLanguage.SelectedItem = User.Wiki.Language _
                Else InterfaceLanguage.SelectedItem = App.Languages(prefs.Language.ToLower)

            Select Case prefs.Gender
                Case "unknown" : Gender.SelectedIndex = 0
                Case "male" : Gender.SelectedIndex = 1
                Case "female" : Gender.SelectedIndex = 2
            End Select

            Signature.Text = prefs.Signature
            RawSignature.Checked = prefs.RawSignature
            EmailAddress.Text = prefs.EmailAddress

            'Appearence
            For Each skin As WikiSkin In User.Wiki.Skins.Values
                Skins.Items.Add(skin)
            Next skin

            If User.Wiki.Skins.ContainsKey(prefs.Skin) Then Skins.SelectedItem = User.Wiki.Skins(prefs.Skin)
            ImageSize.SelectedIndex = prefs.ImageSize
            ThumbnailSize.SelectedIndex = prefs.ThumbnailSize
            MathOption.SelectedIndex = prefs.MathOption
            UnderlineLinks.SelectedIndex = prefs.UnderlineLinks
            StubThresholdSelect.Checked = (prefs.StubThreshold > 0)
            StubThreshold.Value = prefs.StubThreshold
            AlternateLinks.Checked = prefs.AlternateLinks
            Toc.Checked = prefs.Toc
            DisableCaching.Checked = prefs.DisableCaching
            HiddenCategories.Checked = prefs.HiddenCategories
            JumpLinks.Checked = prefs.JumpLinks
            Justify.Checked = prefs.Justify
            NumberHeadings.Checked = prefs.NumberHeadings

            'Gadgets
            For Each gadget As Gadget In User.Wiki.Gadgets.All
                Dim prefKey As String = "gadget-" & gadget.Code

                Gadgets.Items.Add(gadget.Name)
                Gadgets.SetItemChecked(Gadgets.Items.Count - 1, _
                    (prefs.Other.ContainsKey(prefKey) AndAlso prefs.Other(prefKey) = "1"))
            Next gadget

            'Other
        End Sub

        Private Function StoreData(ByVal base As Preferences) As Preferences
            Dim prefs As Preferences = base.Clone

            'General
            prefs.Language = CType(InterfaceLanguage.SelectedItem, Language).Code

            Select Case Gender.SelectedIndex
                Case 0 : prefs.Gender = "unknown"
                Case 1 : prefs.Gender = "male"
                Case 2 : prefs.Gender = "female"
            End Select

            prefs.Signature = Signature.Text
            prefs.RawSignature = RawSignature.Checked
            prefs.EmailAddress = EmailAddress.Text

            'Appearence

            prefs.Skin = CType(Skins.SelectedItem, WikiSkin).Code
            prefs.ImageSize = ImageSize.SelectedIndex
            prefs.ThumbnailSize = ThumbnailSize.SelectedIndex
            prefs.MathOption = MathOption.SelectedIndex
            prefs.UnderlineLinks = UnderlineLinks.SelectedIndex
            prefs.StubThreshold = If(StubThresholdSelect.Checked, 0, CInt(StubThreshold.Value))
            prefs.AlternateLinks = AlternateLinks.Checked
            prefs.Toc = Toc.Checked
            prefs.DisableCaching = DisableCaching.Checked
            prefs.HiddenCategories = HiddenCategories.Checked
            prefs.JumpLinks = JumpLinks.Checked
            prefs.Justify = Justify.Checked
            prefs.NumberHeadings = NumberHeadings.Checked

            'Gadgets

            'Other

            Return prefs
        End Function

    End Class

End Namespace