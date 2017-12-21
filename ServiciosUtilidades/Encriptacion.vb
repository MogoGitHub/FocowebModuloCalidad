Imports System.Security.Cryptography
Imports System.Text

Public Class Encriptacion

    Private vPalabra As String = "W0X.Y1Z*á2é-í3ó/ú4!+·5a$6~b%7c¬8d(9)e=A?f¿B¡g[C]h{D}yçE;j,F:k\GºlªH|m@IänëJïñöKüoÄLËpÏMÖqÜNÁrÉÑÍsÓOÚtPuQvRwSxTyUzV"

    Public Function EncryptText(ByVal strText As String, ByVal strPwd As String)
        Dim i As Integer, C As Integer
        Dim strBuff As String = Nothing

        strPwd = UCase$(strPwd)

        'Encrypt string
        If Len(strPwd) Then
            For i = 1 To Len(strText)
                C = Asc(Mid$(strText, i, 1))
                C = C + Asc(Mid$(strPwd, (i Mod Len(strPwd)) + 1, 1)) - 97
                strBuff &= vPalabra(C And &HFF)
            Next i
        Else
            strBuff = strText
        End If
        EncryptText = strBuff
    End Function

    'Decrypt text encrypted with EncryptText
    Public Function DecryptText(ByVal strText As String, ByVal strPwd As String)
        Dim i As Integer, C As Integer
        Dim strBuff As String = Nothing

        strPwd = UCase$(strPwd)

        'Decrypt string
        If Len(strPwd) Then
            For i = 1 To Len(strText)
                C = vPalabra.IndexOf(Mid$(strText, i, 1))
                C = C - Asc(Mid$(strPwd, (i Mod Len(strPwd)) + 1, 1)) + 97
                strBuff &= Chr(C And &HFF)
            Next i
        Else
            strBuff = strText
        End If
        DecryptText = strBuff
    End Function

    ''' <summary>
    ''' Encripta una cadena utilizando el algoritmo SHA256 (Secure Hash Algorithm)
    ''' </summary>
    ''' <param name="Data">Un string conteniendo los datos a encriptar.</param>
    ''' <returns>Un string conteniendo al string de entrada, encriptado con el algoritmo SHA256.</returns>
    ''' <remarks>Detalles: http://www.itl.nist.gov/fipspubs/fip180-1.htm</remarks>
    Public Function SHA256Hash(ByVal Data As String) As String
        Dim sha As SHA256 = New SHA256Managed()
        Dim hash As Byte() = sha.ComputeHash(Encoding.UTF8.GetBytes(Data))

        Dim stringBuilder As New StringBuilder()
        For Each b As Byte In hash
            stringBuilder.AppendFormat("{0:x2}", b)
        Next
        Return stringBuilder.ToString()
    End Function

    ''' <summary>
    ''' Encripta una cadena utilizando el algoritmo SHA256 (Secure Hash Algorithm)
    ''' </summary>
    ''' <param name="Data">Un string conteniendo los datos a encriptar.</param>
    ''' <returns>Un string conteniendo al string de entrada, encriptado con el algoritmo SHA384.</returns>
    ''' <remarks>Detalles: http://www.itl.nist.gov/fipspubs/fip180-1.htm</remarks>
    Public Function SHA384Hash(ByVal Data As String) As String
        Dim sha As SHA384 = New SHA384Managed()
        Dim hash As Byte() = sha.ComputeHash(Encoding.UTF8.GetBytes(Data))

        Dim stringBuilder As New StringBuilder()
        For Each b As Byte In hash
            stringBuilder.AppendFormat("{0:x2}", b)
        Next
        Return stringBuilder.ToString()
    End Function

    ''' <summary>
    ''' Encripta una cadena utilizando el algoritmo SHA256 (Secure Hash Algorithm)
    ''' </summary>
    ''' <param name="Data">Un string conteniendo los datos a encriptar.</param>
    ''' <returns>Un string conteniendo al string de entrada, encriptado con el algoritmo SHA512.</returns>
    ''' <remarks>Detalles: http://www.itl.nist.gov/fipspubs/fip180-1.htm</remarks>
    Public Function SHA512Hash(ByVal Data As String) As String
        Dim sha As SHA512 = New SHA512Managed()
        Dim hash As Byte() = sha.ComputeHash(Encoding.UTF8.GetBytes(Data))

        Dim stringBuilder As New StringBuilder()
        For Each b As Byte In hash
            stringBuilder.AppendFormat("{0:x2}", b)
        Next
        Return stringBuilder.ToString()
    End Function

    ''' <summary>
    ''' Encripta una cadena de texto usando el algoritmo de encriptacion de hash MD5.
    ''' el "Message Digest" es una encriptacion de 128-bit y es usado comunmente para
    ''' verificar datos chequeando el "Checksum MD5"
    ''' </summary>
    ''' <param name="Data">cadena conteniendo el string a hashear a MD5.</param>
    ''' <returns>Una cadena de texto conteniendo en forma encriptada la cadena ingresada.</returns>
    ''' <remarks>mas informacion en: http://www.faqs.org/rfcs/rfc1321.html</remarks>
    Public Function MD5Hash(ByVal Data As String) As String
        Dim md5 As MD5 = New MD5CryptoServiceProvider()
        Dim hash As Byte() = md5.ComputeHash(Encoding.UTF8.GetBytes(Data))
        Dim stringBuilder As New StringBuilder()

        For Each b As Byte In hash
            stringBuilder.AppendFormat("{0:x2}", b)
        Next
        Return stringBuilder.ToString()
    End Function

End Class
