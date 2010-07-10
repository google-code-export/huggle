﻿Imports System
Imports System.Collections.Generic
Imports System.Drawing
Imports System.Windows.Forms

Namespace Huggle.Controls

    Class ActivityIndicator : Inherits UserControl

        Private WithEvents Timer As New Timer

        Private Const Frames As Integer = 8
        Private Const AnimationSize As Double = 6.25

        Private Brushes As New List(Of Brush)(Frames)
        Private CanRender As Boolean
        Private Frame As Integer
        Private Gfx As BufferedGraphics

        Public Sub New()
            InitializeComponent()
            MinimumSize = New Size(16, 16)
            MaximumSize = New Size(16, 16)
            TabStop = False
        End Sub

        Private Sub _HandleCreated() Handles Me.HandleCreated
            CanRender = True
            Timer.Interval = 80

            For i As Integer = 0 To Frames - 1
                Dim color As Color = color.FromArgb(Math.Max(80, 255 - CInt(255 * i / Frames)), 0, 0, 0)
                Brushes.Add(New Pen(color).Brush)
            Next i

            Gfx = BufferedGraphicsManager.Current.Allocate(CreateGraphics, DisplayRectangle)
            Gfx.Graphics.SmoothingMode = Drawing2D.SmoothingMode.HighQuality
        End Sub

        Private Sub _HandleDestroyed() Handles Me.HandleDestroyed
            CanRender = False
        End Sub

        Private Sub _Paint(ByVal s As Object, ByVal e As PaintEventArgs) Handles Me.Paint
            Gfx.Graphics.Clear(BackColor)
            If BackgroundImage IsNot Nothing Then Gfx.Graphics.DrawImageUnscaled(BackgroundImage, 0, 0)
            If CanRender Then Gfx.Render()
        End Sub

        Private Sub InitializeComponent()
            Me.SuspendLayout()
            Me.Name = "ActivityIndicator"
            Me.ResumeLayout(False)
        End Sub

        Private Sub Timer_Tick() Handles Timer.Tick
            Const Frames As Integer = 8

            Gfx.Graphics.Clear(BackColor)

            For i As Integer = 0 To Frames
                Dim r As Double = (i / Frames) * 2 * Math.PI
                Dim x As Single = CSng(AnimationSize * (1 + Math.Sin(r)))
                Dim y As Single = CSng(AnimationSize * (1 + Math.Cos(r)))

                Gfx.Graphics.FillEllipse(Brushes((i + Frame) Mod Frames), x, y, 3.5, 3.5)
            Next i

            Frame = (Frame + 1) Mod Frames

            If CanRender Then Gfx.Render()
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

    End Class

End Namespace