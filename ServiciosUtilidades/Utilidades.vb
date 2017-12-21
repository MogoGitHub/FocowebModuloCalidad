Namespace Utilidades

    Public Class Utilidades
        Public Shared _ANIO As String = "Anio"
        Public Shared _MES As String = "Mes"
        Public Shared _NOMBRE_CORTO As String = "NombreCorto"
        Public Shared _NOMBRE_PERIODO As String = "NombrePeriodo"
        Public Shared _PERIODO As String = "Periodo"
        Public Shared _INICIO_PERIODO As String = "inicioPeriodo"
        Public Shared _FIN_PERIODO As String = "finPeriodo"

        Public Shared Function obtNombreMesCorto(mes As Integer) As String
            Select Case mes
                Case 1
                    Return "Ene"
                Case 2
                    Return "Feb"
                Case 3
                    Return "Mar"
                Case 4
                    Return "Abr"
                Case 5
                    Return "May"
                Case 6
                    Return "Jun"
                Case 7
                    Return "Jul"
                Case 8
                    Return "Ago"
                Case 9
                    Return "Sep"
                Case 10
                    Return "Oct"
                Case 11
                    Return "Nov"
                Case 12
                    Return "Dic"
            End Select
        End Function

        Public Shared Function cantPeriodo(fechaIni As Date, fechaFin As Date) As Integer
            Dim anioIni As Integer = Year(fechaIni)
            Dim mesIni As Integer = Month(fechaIni)
            Dim anioFin As Integer = Year(fechaFin)
            Dim mesFin As Integer = Month(fechaFin)
            Dim periodos As Integer = 0


            Dim mesInicia As Integer = mesIni
            For a As Integer = anioIni To anioFin
                For m As Integer = mesInicia To 12
                    periodos += 1

                    If m = mesFin And a = anioFin Then
                        Return periodos
                    End If
                Next
                mesInicia = 1
            Next
            Return periodos
        End Function

     

        Public Shared Function MesesEntreFecha(fechaIni As Date, fechaFin As Date) As DataTable


            Dim anioIni As Integer = Year(fechaIni)
            Dim mesIni As Integer = Month(fechaIni)
            Dim anioFin As Integer = Year(fechaFin)
            Dim mesFin As Integer = Month(fechaFin)

            Dim meses As New DataTable
            meses.Columns.Add("Mes")
            meses.Columns.Add("Anio")
            meses.Columns.Add("NombreCorto")
            meses.Columns.Add("NombrePeriodo")
            meses.Columns.Add("Periodo")
            meses.Columns.Add("inicioPeriodo")
            meses.Columns.Add("finPeriodo")
            Dim mesInicia As Integer = mesIni

            For a As Integer = anioIni To anioFin
                For m As Integer = mesInicia To 12
                    Dim r As DataRow = meses.NewRow

                    r("Mes") = m
                    r("Anio") = a
                    r("Periodo") = m & "-" & a
                    r("inicioPeriodo") = New Date(a, m, 1)

                    Select Case m
                        Case 1
                            r("NombreCorto") = "Ene"
                            r("NombrePeriodo") = "Ene " & a
                            r("finPeriodo") = New Date(a, m, 31)
                        Case 2
                            r("NombreCorto") = "Feb"
                            r("NombrePeriodo") = "Feb " & a
                            r("finPeriodo") = New Date(a, m, 28)
                        Case 3
                            r("NombreCorto") = "Mar"
                            r("NombrePeriodo") = "Mar " & a
                            r("finPeriodo") = New Date(a, m, 31)
                        Case 4
                            r("NombreCorto") = "Abr"
                            r("NombrePeriodo") = "Abr " & a
                            r("finPeriodo") = New Date(a, m, 30)
                        Case 5
                            r("NombreCorto") = "May"
                            r("NombrePeriodo") = "May " & a
                            r("finPeriodo") = New Date(a, m, 31)
                        Case 6
                            r("NombreCorto") = "Jun"
                            r("NombrePeriodo") = "Jun " & a
                            r("finPeriodo") = New Date(a, m, 30)
                        Case 7
                            r("NombreCorto") = "Jul"
                            r("NombrePeriodo") = "Jul " & a
                            r("finPeriodo") = New Date(a, m, 31)
                        Case 8
                            r("NombreCorto") = "Ago"
                            r("NombrePeriodo") = "Ago " & a
                            r("finPeriodo") = New Date(a, m, 31)
                        Case 9
                            r("NombreCorto") = "Sep"
                            r("NombrePeriodo") = "Sep " & a
                            r("finPeriodo") = New Date(a, m, 30)
                        Case 10
                            r("NombreCorto") = "Oct"
                            r("NombrePeriodo") = "Oct " & a
                            r("finPeriodo") = New Date(a, m, 31)
                        Case 11
                            r("NombreCorto") = "Nov"
                            r("NombrePeriodo") = "Nov " & a
                            r("finPeriodo") = New Date(a, m, 30)
                        Case 12
                            r("NombreCorto") = "Dic"
                            r("NombrePeriodo") = "Dic " & a
                            r("finPeriodo") = New Date(a, m, 31)
                    End Select
                    meses.Rows.Add(r)
                    If m = mesFin And a = anioFin Then
                        Return meses
                    End If
                Next
                mesInicia = 1
            Next

            Return meses

        End Function

        Public Shared Function ObtenerSemana(fecha As Date) As String
            Dim semana As String = ""

            If fecha.DayOfWeek = DayOfWeek.Friday Then
                Return "Al" & fecha
            Else
                Dim fechaTmp As Date = DateAdd(DateInterval.Day, FirstDayOfWeek.Friday - fecha.DayOfWeek - 1, fecha)
                Return "Al" & fechaTmp
                
            End If

            Return ""
        End Function

        Public Shared Function EsFinMes(ByVal myDate As Date) As Boolean
            Return myDate.Day = Date.DaysInMonth(myDate.Year, myDate.Month)
        End Function

        Public Shared Function ConvertToLetter(iCol As Integer) As String
            Dim iAlpha As Integer
            Dim iRemainder As Integer
            iAlpha = Int(iCol / 27)
            iRemainder = iCol - (iAlpha * 26)
            If iAlpha > 0 Then
                ConvertToLetter = Chr(iAlpha + 64)
            End If
            If iRemainder > 0 Then
                ConvertToLetter = ConvertToLetter & Chr(iRemainder + 64)
            End If
        End Function

    End Class
End Namespace