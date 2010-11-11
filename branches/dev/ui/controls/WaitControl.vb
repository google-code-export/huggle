Imports Huggle.Actions
Imports System
Imports System.Collections.Generic
Imports System.ComponentModel
Imports System.Drawing
Imports System.Windows.Forms

Namespace Huggle.UI

    Friend Class WaitControl : Inherits Control

        Private _TextPosition As WaitTextPosition

        Private WithEvents Timer As New Timer

        Private Const Frames As Integer = 8
        Private Const AnimationSize As Double = 6.25

        Private Brushes As New List(Of Brush)(Frames)
        Private CanRender As Boolean
        Private Frame As Integer
        Private Gfx As BufferedGraphics

        Private WithEvents AttachedProcess As Process
        Private Callback As SimpleEventHandler(Of Process)

        Public Sub New()
            TabStop = False
        End Sub

        Public ReadOnly Property IsRunning As Boolean
            Get
                Return (Timer.Enabled)
            End Get
        End Property

        <DefaultValue(WaitTextPosition.None)>
        Public Property TextPosition As WaitTextPosition
            Get
                Return _TextPosition
            End Get
            Set(ByVal value As WaitTextPosition)
                _TextPosition = value
                _TextChanged()
            End Set
        End Property

        Private Sub _HandleCreated() Handles Me.HandleCreated
            CanRender = True
            Timer.Interval = 80

            For i As Integer = 0 To Frames - 1
                Brushes.Add(New SolidBrush(Color.FromArgb(Math.Max(80, 255 - CInt(255 * i / Frames)), 0, 0, 0)))
            Next i
        End Sub

        Private Sub _HandleDestroyed() Handles Me.HandleDestroyed
            CanRender = False
        End Sub

        Private Sub _Paint(ByVal s As Object, ByVal e As PaintEventArgs) Handles Me.Paint
            If Gfx Is Nothing Then Return

            Gfx.Graphics.Clear(BackColor)
            If BackgroundImage IsNot Nothing Then Gfx.Graphics.DrawImageUnscaled(BackgroundImage, 0, 0)
            If CanRender Then Gfx.Render()
        End Sub

        Private Sub _Resize() Handles Me.Resize
            If DisplayRectangle.Height > 0 AndAlso DisplayRectangle.Width > 0 Then
                Gfx = BufferedGraphicsManager.Current.Allocate(CreateGraphics, DisplayRectangle)
                Gfx.Graphics.SmoothingMode = Drawing2D.SmoothingMode.HighQuality
            End If
        End Sub

        Private Sub _TextChanged() Handles Me.TextChanged
            Select Case TextPosition
                Case WaitTextPosition.None
                    Width = 16
                    Height = 16

                Case WaitTextPosition.Horizontal
                    Width = 24 + TextRenderer.MeasureText(Text, Font).Width
                    Height = 16

                Case WaitTextPosition.Vertical
                    Dim measurement As Size = TextRenderer.MeasureText(Text, Font, Size.Empty, TextFormatFlags.VerticalCenter)
                    Width = Size.Width
                    Height = Size.Height + 20
            End Select
        End Sub

        Private Sub Timer_Tick() Handles Timer.Tick
            If Gfx Is Nothing Then Return

            Gfx.Graphics.Clear(BackColor)
            Frame = (Frame + 1) Mod Frames

            Select Case TextPosition
                Case WaitTextPosition.None
                    DrawSpinner(8, 8)

                Case WaitTextPosition.Horizontal
                    DrawSpinner(8, 8)
                    TextRenderer.DrawText(Gfx.Graphics, Text, Font, New Point(20, 1), ForeColor, BackColor)

                Case WaitTextPosition.Vertical
                    DrawSpinner(Width \ 2, 8)
                    TextRenderer.DrawText(Gfx.Graphics, Text, Font, New Rectangle(0, 21, Width, Height - 20),
                        ForeColor, BackColor, TextFormatFlags.VerticalCenter)
            End Select

            If CanRender Then Gfx.Render()
        End Sub

        Private Sub DrawSpinner(ByVal x As Integer, ByVal y As Integer)
            For i As Integer = 0 To Frames
                Dim r As Double = (i / Frames) * 2 * Math.PI
                Dim ax As Single = x - 8 + CSng(AnimationSize * (1 + Math.Sin(r)))
                Dim ay As Single = y - 8 + CSng(AnimationSize * (1 + Math.Cos(r)))

                Gfx.Graphics.FillEllipse(Brushes((i + Frame) Mod Frames), ax, ay, 3.5F, 3.5F)
            Next i
        End Sub

        Public Sub Start()
            Timer.Start()
        End Sub

        Public Sub [Stop]()
            Timer.Stop()

            If CanRender Then
                Gfx.Graphics.Clear(BackColor)
                Gfx.Render()
            End If
        End Sub

        Public Sub WaitOn(ByVal process As Process, ByVal callback As SimpleEventHandler(Of Process))
            If process.IsComplete Then
                [Stop]()
                callback(Me, New EventArgs(Of Process)(process))
            Else
                If Not process.IsRunning Then CreateThread(AddressOf process.Start)
                Me.Callback = callback
                AttachedProcess = process
                Start()
            End If
        End Sub

        Private Sub AttachedProcess_Complete() Handles AttachedProcess.Complete
            [Stop]()
            Callback(Me, New EventArgs(Of Process)(AttachedProcess))
        End Sub

        Public Enum WaitTextPosition As Integer
            : None : Horizontal : Vertical
        End Enum

    End Class

End Namespace