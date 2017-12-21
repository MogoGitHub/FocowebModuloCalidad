Namespace Utilidades
    Public Class Operaciones

        ''' <summary>
        ''' Simplifica un valor según el coeficiente indicado
        ''' </summary>
        ''' <param name="valor">valor a dividir</param>
        ''' <param name="coeficiente">coeficiente por el que se dividira.</param>
        ''' <returns>Valor simplificado</returns>
        ''' <remarks></remarks>
        Public Shared Function Simplificar(valor As Nullable(Of Double), coeficiente As Integer) As Nullable(Of Double)
            If valor Is Nothing Then Return Nothing
            If IsDBNull(valor) Then Return Nothing

            Return valor / coeficiente

        End Function

        ''' <summary>
        ''' Simplifica un valor según el coeficiente indicado y lo devuelve segun formato
        ''' </summary>
        ''' <param name="valor">valor a dividir</param>
        ''' <param name="coeficiente">coeficiente por el que se dividira.</param>
        ''' <param name="formato"> formato para string.format.</param>
        ''' <returns>Valor simplificado</returns>
        ''' <remarks></remarks>
        Public Shared Function Simplificar(valor As Object, coeficiente As Integer, formato As String, Optional muestraCero As Boolean = True, Optional canDecimales As Integer = 2) As String
            Select Case muestraCero
                Case True
                    If valor Is Nothing Then Return 0
                    If IsDBNull(valor) Then Return 0
                    If TypeOf valor Is Integer AndAlso valor = 0 Then
                        Return 0
                    End If

                    If TypeOf valor Is Double AndAlso valor = 0.0 Then
                        Return 0
                    End If

                Case False
                    If valor Is Nothing Then Return Nothing
                    If IsDBNull(valor) Then Return Nothing
                    If TypeOf valor Is Integer AndAlso valor = 0 Then
                        Return Nothing
                    End If

                    If TypeOf valor Is Double AndAlso valor = 0.0 Then
                        Return Nothing
                    End If

            End Select

            'If valor Is Nothing Then Return Nothing
            'If IsDBNull(valor) Then Return Nothing

            ' Return String.Format(formato, Math.Truncate(CDbl(valor) / coeficiente))

            Dim mult As Integer
            If canDecimales = 0 Then
                mult = 1
            Else
                mult = Math.Pow(10, canDecimales)
            End If

            Select Case True
                Case TypeOf valor Is Double
                    Return String.Format(formato, Math.Truncate(CDbl(valor) / coeficiente * mult) / mult)
                Case TypeOf valor Is Decimal
                    Return String.Format(formato, Math.Truncate(CDec(valor) / coeficiente * mult) / mult)
                Case TypeOf valor Is Integer
                    Return String.Format(formato, Math.Truncate(CInt(valor) / coeficiente * mult) / mult)
            End Select

            Return valor

        End Function

        ''' <summary>
        ''' Simplifica un valor según el coeficiente indicado y lo devuelve segun formato
        ''' </summary>
        ''' <param name="valor">valor a dividir</param>
        ''' <param name="coeficiente">coeficiente por el que se dividira.</param>
        ''' <param name="formato"> formato para string.format.</param>
        ''' <returns>Valor simplificado</returns>
        ''' <remarks></remarks>
        Public Shared Function SimplificarSinTruncar(valor As Object, coeficiente As Integer, formato As String, Optional muestraCero As Boolean = True) As String
            Select Case muestraCero
                Case True
                    If valor Is Nothing Then Return 0
                    If IsDBNull(valor) Then Return 0
                    If TypeOf valor Is Integer AndAlso valor = 0 Then
                        Return 0
                    End If

                    If TypeOf valor Is Double AndAlso valor = 0.0 Then
                        Return 0
                    End If

                Case False
                    If valor Is Nothing Then Return Nothing
                    If IsDBNull(valor) Then Return Nothing
                    If TypeOf valor Is Integer AndAlso valor = 0 Then
                        Return Nothing
                    End If

                    If TypeOf valor Is Double AndAlso valor = 0.0 Then
                        Return Nothing
                    End If

            End Select

            'If valor Is Nothing Then Return Nothing
            'If IsDBNull(valor) Then Return Nothing

            ' Return String.Format(formato, Math.Truncate(CDbl(valor) / coeficiente))


            Select Case True
                Case TypeOf valor Is Double
                    Return String.Format(formato, CDbl(valor) / coeficiente)
                Case TypeOf valor Is Decimal
                    Return String.Format(formato, CDec(valor) / coeficiente)
                Case TypeOf valor Is Integer
                    Return String.Format(formato, CInt(valor) / coeficiente)
            End Select

            Return valor

        End Function


        ''' <summary>
        ''' Simplifica un valor dividiendolo por 1000 devolviendolo con separador de miles
        ''' </summary>
        ''' <param name="valor">valor a dividir</param>       
        ''' <returns>Valor simplificado</returns>
        ''' <remarks></remarks>
        Public Shared Function SimplificarMil(valor As Object, Optional muestraCero As Boolean = True) As String
            Select Case muestraCero
                Case True
                    If valor Is Nothing Then Return 0
                    If IsDBNull(valor) Then Return 0
                    If TypeOf valor Is Integer AndAlso valor = 0 Then
                        Return 0
                    End If

                    If TypeOf valor Is Double AndAlso valor = 0.0 Then
                        Return 0
                    End If

                Case False
                    If valor Is Nothing Then Return Nothing
                    If IsDBNull(valor) Then Return Nothing
                    If TypeOf valor Is Integer AndAlso valor = 0 Then
                        Return Nothing
                    End If

                    If TypeOf valor Is Double AndAlso valor = 0.0 Then
                        Return Nothing
                    End If

            End Select

            Select Case True
                Case TypeOf valor Is Double
                    Return String.Format("{0:###,###,##0}", Math.Truncate(CDbl(valor) / 1000))
                Case TypeOf valor Is Decimal
                    Return String.Format("{0:###,###,##0}", Math.Truncate(CDec(valor) / 1000))
                Case TypeOf valor Is Integer
                    Return String.Format("{0:###,###,##0}", Math.Truncate(CInt(valor) / 1000))
            End Select

            Return valor


        End Function


        ''' <summary>
        ''' obtiene el valor con separador de miles
        ''' </summary>
        ''' <param name="valor"></param>       
        ''' <returns>Valor con  separador de miles</returns>
        ''' <remarks></remarks>
        Public Shared Function FormatoMoneda(valor As Object, Optional muestraCero As Boolean = True) As String
            Try
                Select Case muestraCero
                    Case True
                        If valor Is Nothing Then Return 0
                        If IsDBNull(valor) Then Return 0
                        If TypeOf valor Is Integer AndAlso valor = 0 Then
                            Return 0
                        End If

                        If TypeOf valor Is Double AndAlso valor = 0.0 Then
                            Return 0
                        End If

                    Case False
                        If valor Is Nothing Then Return Nothing
                        If IsDBNull(valor) Then Return Nothing
                        If TypeOf valor Is Integer AndAlso valor = 0 Then
                            Return Nothing
                        End If

                        If TypeOf valor Is Double AndAlso valor = 0.0 Then
                            Return Nothing
                        End If

                End Select

                Return String.Format("{0:###,###,##0}", valor)

            Catch
                Return ""
            End Try

        End Function


        ''' <summary>
        ''' Simplifica un valor dividiendolo por 1000000 devolviendolo con separador de miles
        ''' </summary>
        ''' <param name="valor">valor a dividir</param>       
        ''' <returns>Valor simplificado</returns>
        ''' <remarks></remarks>
        Public Shared Function SimplificarMillon(valor As Object, Optional muestraCero As Boolean = True) As String
            Select Case muestraCero
                Case True
                    If valor Is Nothing Then Return 0
                    If IsDBNull(valor) Then Return 0
                    If TypeOf valor Is Integer AndAlso valor = 0 Then
                        Return 0
                    End If

                    If TypeOf valor Is Double AndAlso valor = 0.0 Then
                        Return 0
                    End If

                Case False
                    If valor Is Nothing Then Return Nothing
                    If IsDBNull(valor) Then Return Nothing
                    If TypeOf valor Is Integer AndAlso valor = 0 Then
                        Return Nothing
                    End If

                    If TypeOf valor Is Double AndAlso valor = 0.0 Then
                        Return Nothing
                    End If

            End Select

            Select Case True
                Case TypeOf valor Is Double
                    Return String.Format("{0:###,###,##0}", Math.Truncate(CDbl(valor) / 1000000))
                Case TypeOf valor Is Decimal
                    Return String.Format("{0:###,###,##0}", Math.Truncate(CDec(valor) / 1000000))
                Case TypeOf valor Is Integer
                    Return String.Format("{0:###,###,##0}", Math.Truncate(CInt(valor) / 1000000))
            End Select

            Return valor


        End Function

        ''' <summary>
        ''' Simplifica un valor dividiendolo por 1000000 devolviendolo con separador de miles y con 1 decimal
        ''' </summary>
        ''' <param name="valor">valor a dividir</param>       
        ''' <returns>Valor simplificado</returns>
        ''' <remarks></remarks>
        Public Shared Function SimplificarMillonUnDecimal(valor As Object, Optional muestraCero As Boolean = True) As String
            Select Case muestraCero
                Case True
                    If valor Is Nothing Then Return 0
                    If IsDBNull(valor) Then Return 0
                    If TypeOf valor Is Integer AndAlso valor = 0 Then
                        Return 0
                    End If

                    If TypeOf valor Is Double AndAlso valor = 0.0 Then
                        Return 0
                    End If

                Case False
                    If valor Is Nothing Then Return Nothing
                    If IsDBNull(valor) Then Return Nothing
                    If TypeOf valor Is Integer AndAlso valor = 0 Then
                        Return Nothing
                    End If

                    If TypeOf valor Is Double AndAlso valor = 0.0 Then
                        Return Nothing
                    End If

            End Select

            Select Case True
                Case TypeOf valor Is Double
                    Return String.Format("{0:N1}", CDbl(valor) / 1000000)
                Case TypeOf valor Is Decimal
                    Return String.Format("{0:N1}", CDec(valor) / 1000000)
                Case TypeOf valor Is Integer
                    Return String.Format("{0:N1}", CInt(valor) / 1000000)
            End Select

            Return valor


        End Function

        ''' <summary>
        ''' Simplifica un valor dividiendolo por 1000 devolviendolo con separador de miles y parentesis si es negativo
        ''' </summary>
        ''' <param name="valor">valor a dividir</param>       
        ''' <returns>Valor simplificado</returns>
        ''' <remarks></remarks>
        Public Shared Function SimplificarMilContable(valor As Object, Optional muestraCero As Boolean = True) As String
            Select Case muestraCero
                Case True
                    If valor Is Nothing Then Return 0
                    If IsDBNull(valor) Then Return 0
                    If valor = 0.0 Then Return 0
                Case Else
                    If valor Is Nothing Then Return Nothing
                    If IsDBNull(valor) Then Return Nothing
                    If valor = 0.0 Then Return Nothing
            End Select


            

            If valor < 0 Then
                Return String.Format("({0:###,###,##0})", Math.Truncate(CDbl(valor) / 1000)).Replace("-", "")
            Else
                Return String.Format("{0:###,###,##0}", Math.Truncate(CDbl(valor) / 1000))
            End If


        End Function

        ''' <summary>
        ''' Formatea un numero y lo trunca.
        ''' </summary>
        ''' <param name="Valor"></param>
        ''' <param name="formato"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Shared Function FormateaNumero(Valor As Object, formato As String, Optional muestraCero As Boolean = False) As String
            If EsNothingOrDbNUll(Valor) Then
                Return IIf(muestraCero, "0", "")
            End If
            If Valor = 0.0 Then Return IIf(muestraCero, "0", "")


            Select Case True
                Case TypeOf Valor Is Integer
                    Return String.Format(formato, Valor)
                Case TypeOf Valor Is Double
                    Return String.Format(formato, Math.Truncate(CDbl(Valor)))
                Case TypeOf Valor Is Decimal
                    Return String.Format(formato, Math.Truncate(CDec(Valor)))
            End Select
            Return ""
        End Function


        ''' <summary>
        ''' Formatea un numero 
        ''' </summary>
        ''' <param name="Valor"></param>
        ''' <param name="formato"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Shared Function FormateaNumeroSinTruncar(Valor As Object, formato As String, Optional muestraCero As Boolean = False) As String
            If EsNothingOrDbNUll(Valor) Then
                Return IIf(muestraCero, "0", "")
            End If

            If Valor = 0.0 Then Return IIf(muestraCero, "0", "")

            Select Case True
                Case TypeOf Valor Is Integer
                    Return String.Format(formato, Valor)
                Case TypeOf Valor Is Double
                    Return String.Format(formato, CDbl(Valor))
                Case TypeOf Valor Is Decimal
                    Return String.Format(formato, CDec(Valor))
            End Select
            Return ""
        End Function



        ''' <summary>
        ''' Devuelve la clase css azul o rojo segun el valor ingresado
        ''' </summary>
        ''' <param name="valor">valor a dividir</param>       
        ''' <returns>Valor simplificado</returns>
        ''' <remarks></remarks>
        Public Shared Function CssSegunValor(valor As Object) As String

            If valor Is Nothing Then Return ""
            If IsDBNull(valor) Then Return ""



            If valor < 0.0 Then
                Return "FilaTextoRojo"
            Else
                Return "FilaTextoAzul"
            End If
            Return ""
        End Function

        ''' <summary>
        ''' si es negativo obtiene la clase CSS de texto rojo
        ''' </summary>
        ''' <param name="valor">valor a dividir</param>       
        ''' <returns>Valor simplificado</returns>
        ''' <remarks></remarks>
        Public Shared Function CssNegativo(valor As Object) As String
            If EsNothingOrDbNUll(valor) Then
                Return ""
            End If

            Select Case True
                Case TypeOf valor Is Integer
                    Return IIf(valor < 0, "TextoRojo", "")
                Case TypeOf valor Is Double
                    Return IIf(valor < 0.0, "TextoRojo", "")
                Case TypeOf valor Is Decimal
                    Return IIf(valor < 0.0, "TextoRojo", "")
            End Select
            Return ""


        End Function



        ''' <summary>
        ''' Obtiene valor double
        ''' </summary>
        ''' <param name="i"></param>
        ''' <param name="retornaCero">Si es verdadero,retorna un cero cuando en nothing o dbnull</param>
        ''' <returns>nulo si es nothing o isdbnull</returns>
        ''' <remarks></remarks>
        Public Shared Function ObtValorDbl(i As Object, Optional retornaCero As Boolean = False) As Nullable(Of Double)
            Try
                If retornaCero Then
                    If IsDBNull(i) Then Return 0.0
                    If i Is Nothing Then Return 0.0
                Else
                    If IsDBNull(i) Then Return Nothing
                    If i Is Nothing Then Return Nothing
                    'If i = 0 Then Return Nothing
                End If

                Return CDbl(i)
            Catch ex As Exception
                Throw ex
            End Try
        End Function

        

        ''' <summary>
        ''' Identifica si un valor es DBNull, nothing 
        ''' </summary>
        ''' <param name="valor"></param>
        ''' <returns>Verdadero si es DbNUll o nothing</returns>
        ''' <remarks></remarks>
        Private Shared Function EsNothingOrDbNUll(valor As Object) As Boolean


            If valor Is Nothing Then Return True
            If IsDBNull(valor) Then Return True

            Return False

        End Function


        Public Shared Function extensionValidas(nombreArchivo As String) As Boolean
            Dim extensiones() As String = {".JPG", ".JPEG", ".PNG", ".TIF", ".RAW", ".GIF", ".BMP", ".PDF", ".DOC", ".DOCX", ".XLS", ".XLSX", ".DWG", ".PRESTOOBRA"}

            For i As Integer = 0 To extensiones.Length - 1
                If nombreArchivo.ToUpper.LastIndexOf(extensiones(i)) <> -1 Then
                    Return True
                End If
            Next
            Return False

        End Function


        Public Shared Function SelectDataTable(ByVal dt As DataTable, ByVal filter As String, ByVal sort As String) As DataTable

            Dim rows As DataRow()

            Dim dtNew As DataTable

            ' copy table structure
            dtNew = dt.Clone()

            ' sort and filter data
            rows = dt.Select(filter, sort)

            ' fill dtNew with selected rows

            For Each dr As DataRow In rows
                dtNew.ImportRow(dr)

            Next

            ' return filtered dt
            Return dtNew

        End Function
    End Class
End Namespace

