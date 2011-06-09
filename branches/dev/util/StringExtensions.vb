Imports System
Imports System.Globalization
Imports System.Runtime.CompilerServices

Namespace Huggle

    Public Module StringExtensions

        'Basic string manipulating functions using invariant casing rules
        'because typing out System.Globalization.CultureInfo.InvariantCulture every time is a pain

        <Extension()>
        Public Function EndsWithI(ByVal this As String, ByVal value As String) As Boolean
            If this Is Nothing Then Throw New ArgumentNullException("this")
            Return this.EndsWith(value, StringComparison.Ordinal)
        End Function

        <Extension()>
        Public Function EqualsI(ByVal this As String, ByVal value As String) As Boolean
            If this Is Nothing Then Throw New ArgumentNullException("this")
            Return this.Equals(value, StringComparison.Ordinal)
        End Function

        <Extension()>
        Public Function EqualsIgnoreCase(ByVal this As String, ByVal value As String) As Boolean
            If this Is Nothing Then Throw New ArgumentNullException("this")
            Return this.Equals(value, StringComparison.OrdinalIgnoreCase)
        End Function

        <Extension()>
        Public Function FormatForUser(ByVal format As String, ByVal ParamArray args As Object()) As String
            Try
                Return String.Format(CultureInfo.CurrentCulture, format, args)
            Catch ex As FormatException
                Return format
            End Try
        End Function

        <Extension()>
        Public Function FormatI(ByVal format As String, ByVal ParamArray args As Object()) As String
            Return String.Format(CultureInfo.InvariantCulture, format, args)
        End Function

        <Extension()>
        Public Function IndexOfI(ByVal this As String, ByVal value As String) As Integer
            If this Is Nothing Then Throw New ArgumentNullException("this")
            Return this.IndexOf(value, StringComparison.Ordinal)
        End Function

        <Extension()>
        Public Function IndexOfI(ByVal this As String, ByVal value As String, ByVal startIndex As Integer) As Integer
            If this Is Nothing Then Throw New ArgumentNullException("this")
            Return this.IndexOf(value, startIndex, StringComparison.Ordinal)
        End Function

        <Extension()>
        Public Function IndexOfIgnoreCase(ByVal this As String, ByVal value As String) As Integer
            If this Is Nothing Then Throw New ArgumentNullException("this")
            Return this.IndexOf(value, StringComparison.OrdinalIgnoreCase)
        End Function

        <Extension()>
        Public Function StartsWithI(ByVal this As String, ByVal value As String) As Boolean
            If this Is Nothing Then Throw New ArgumentNullException("this")
            Return this.StartsWith(value, StringComparison.Ordinal)
        End Function

        <Extension()>
        Public Function StartsWithIgnoreCase(ByVal this As String, ByVal value As String) As Boolean
            If this Is Nothing Then Throw New ArgumentNullException("this")
            Return this.StartsWith(value, StringComparison.OrdinalIgnoreCase)
        End Function

        <Extension()>
        Public Function LastIndexOfI(ByVal this As String, ByVal value As String) As Integer
            If this Is Nothing Then Throw New ArgumentNullException("this")
            Return this.LastIndexOf(value, StringComparison.Ordinal)
        End Function

        <Extension()>
        Public Function ToLowerFirstI(ByVal this As String) As String
            If this Is Nothing Then Throw New ArgumentNullException("this")
            If this.Length <= 1 Then Return this.ToLowerI
            Return this.Substring(0, 1).ToLowerI & this.Substring(1)
        End Function

        <Extension()>
        Public Function ToLowerI(ByVal this As String) As String
            If this Is Nothing Then Throw New ArgumentNullException("this")
            Return this.ToLower(CultureInfo.InvariantCulture)
        End Function

        <Extension()>
        Public Function ToUpperFirstI(ByVal this As String) As String
            If this Is Nothing Then Throw New ArgumentNullException("this")
            If this.Length <= 1 Then Return this.ToUpperI
            Return this.Substring(0, 1).ToUpperI & this.Substring(1)
        End Function

        <Extension()>
        Public Function ToUpperI(ByVal this As String) As String
            If this Is Nothing Then Throw New ArgumentNullException("this")
            Return this.ToUpper(CultureInfo.InvariantCulture)
        End Function

        <Extension()>
        Public Function ToStringI(ByVal this As IFormattable) As String
            If this Is Nothing Then Throw New ArgumentNullException("this")
            Return this.ToString(Nothing, CultureInfo.InvariantCulture)
        End Function

        <Extension()>
        Public Function ToStringForUser(ByVal this As IFormattable) As String
            If this Is Nothing Then Throw New ArgumentNullException("this")
            Return this.ToString(Nothing, CultureInfo.CurrentCulture)
        End Function

    End Module

End Namespace