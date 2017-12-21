Namespace Seguridad

    Public Enum CodigoHistorico
        IngresoCorrecto = 1
        UsuarioNoExiste = 2
        IngresoFallifo = 3
        SalidaDelUsuario = 4
        SesionCaducada = 5
        cambioPass = 6
        usuarioBloqueado = 7

        SeleccionEmpresa = 10
        NavegacionPagina = 11
    End Enum

    Public Class Usuario

        'EN MAESTRO
        Public Property ID_MAESTRO As Long
        Public Property NickName As String
        Public Property Estado As EstadoUsuario
        Public Property EsAdmin As Boolean
        Public Property Empresas As DataTable
        Public Overridable Property EmpresaSelected As Empresa.Empresa

        'EN PARTICULAR
        ''' <summary>
        ''' Id usuario en base de las inmobiliarias
        ''' </summary>
        ''' <value></value>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Property ID_PARTICULAR As Long
        Public Property NombreLargo As String
        Public Property IdPerfil As Long
        Public Property Perfil As String
        Public Property perfilInmobiliario As Boolean
        Public Property perfilOperacional As Boolean
        Public Property vePrebalance As Boolean
        Public Property Opciones As DataTable
        Public Property ip As String
        Public Property hostName As String
        Public Property LoggedIn As Boolean
        Public Property UsuarioTest As Integer
        Public Property usuarioInterno As Boolean
        Public Property vePreAvance As Boolean
        Public Property veEfectoAuditoria As Boolean
        Public Property PaginaInicio As String
        Public Property Block_MO_GG As Integer

    End Class

End Namespace

