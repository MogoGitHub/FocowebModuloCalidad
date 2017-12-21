Imports System.IO
Imports System.Runtime.Serialization.Formatters.Binary

<Serializable()> _
Public Class DatosConexion

    Private vServidor As String
    Private vBaseDatos As String
    Private vUsuario As String
    Private vClave As String
    Private vUsarWindowsAutent As Boolean

    Private vRespaldo As Dictionary(Of String, Object)

    Public Property Servidor As String
        Get
            Servidor = vServidor
        End Get
        Set(ByVal value As String)
            vServidor = value
        End Set
    End Property

    Public Property Catalogo As String
        Get
            Catalogo = vBaseDatos
        End Get
        Set(ByVal value As String)
            vBaseDatos = value
        End Set
    End Property

    Public Property Usuario As String
        Get
            Usuario = vUsuario
        End Get
        Set(ByVal value As String)
            vUsuario = value
        End Set
    End Property

    Public Property Clave As String
        Get
            Clave = vClave
        End Get
        Set(ByVal value As String)
            vClave = value
        End Set
    End Property

    Public Property UsarWindowsAutentification As Boolean
        Get
            UsarWindowsAutentification = vUsarWindowsAutent
        End Get
        Set(ByVal value As Boolean)
            vUsarWindowsAutent = value
        End Set
    End Property

    Public Function GenerarStringConexion() As String
        If Not vUsarWindowsAutent Then
            GenerarStringConexion = "Initial Catalog=[CATALOGO];Data Source=[SERVIDOR];Integrated Security=false;USER ID=[USUARIO];password=[CLAVE]"
            GenerarStringConexion = GenerarStringConexion _
                                    .Replace("[CATALOGO]", Catalogo) _
                                    .Replace("[SERVIDOR]", Servidor) _
                                    .Replace("[USUARIO]", Usuario) _
                                    .Replace("[CLAVE]", Clave)
        Else
            GenerarStringConexion = "Data Source=[SERVIDOR]; Initial Catalog=[CATALOGO]; Integrated Security=True; Connect Timeout=200;"
            GenerarStringConexion = GenerarStringConexion _
                                    .Replace("[CATALOGO]", Catalogo) _
                                    .Replace("[SERVIDOR]", Servidor)
        End If
    End Function

    Public Function Clone() As DatosConexion
        Dim vMS As New MemoryStream()
        Dim vBF As New BinaryFormatter()
        vBF.Serialize(vMS, Me)
        vMS.Position = 0
        Clone = vBF.Deserialize(vMS)
        vMS.Close()
    End Function

End Class

