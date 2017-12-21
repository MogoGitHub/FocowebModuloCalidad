Imports System.Web

Namespace Utilidades
    Public Class SecureURL
        'instanciamos el objeto y le pasamos como argumento el mismo array 'de bits mas el parámetro data, que viene de la llamada de la 'pagina default.aspx que contiene todo el queryString
        Shared keyString As Byte() = {0, 1, 2, 3, 4, 5, 8, 8, 8, 9, 1, 2, 3, 1, 1, 8}

        Public Shared Function Encriptar(parametros As Dictionary(Of String, Object)) As String
            Dim querystringSeguro As TSHAK.Components.SecureQueryString
            querystringSeguro = New TSHAK.Components.SecureQueryString(keyString)
            For Each p In parametros
                querystringSeguro(p.Key) = p.Value
            Next
            Return HttpUtility.UrlEncode(querystringSeguro.ToString())

        End Function


        Public Shared Function Desencriptar(secureUrl As String) As TSHAK.Components.SecureQueryString
            Return New TSHAK.Components.SecureQueryString(keyString, secureUrl)
        End Function

    End Class

End Namespace
