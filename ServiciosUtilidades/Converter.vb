Public Class Converter

    ''' <summary>
    ''' Converts Unix's epoch time to VB DateTime value
    ''' </summary>
    ''' <param name="EpochValue">Epoch time (seconds)</param>
    ''' <returns>VB Date</returns>
    ''' <remarks>http://windevblog.blogspot.com/2008/09/epoch-time-in-vbnet.html</remarks>
    Public Shared Function EpochToDateTime(ByVal EpochValue As Integer) As Date
        If EpochValue >= 0 Then
            Return CDate("1.1.1970 00:00:00").AddSeconds(EpochValue)
        Else
            Return CDate("1.1.1970 00:00:00")
        End If
    End Function

    ''' <summary>
    ''' Converts VB DateTime value to Unix's epoch time
    ''' </summary>
    ''' <param name="DateTimeValue">DateTime to convert</param>
    ''' <returns>Epoch time (seconds)</returns>
    ''' <remarks>http://windevblog.blogspot.com/2008/09/epoch-time-in-vbnet.html</remarks>
    Public Shared Function DateTimeToEpoch(ByVal DateTimeValue As Date) As Integer
        Try
            Return CInt(DateTimeValue.Subtract(CDate("1.1.1970 00:00:00")).TotalSeconds)
        Catch ex As System.OverflowException
            Return -1
        End Try
    End Function



End Class
