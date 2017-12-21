Imports SUL

Namespace Empresa

    Public Class Empresa

        Public Shared Property ClaveEncripta As String

        Public Sub New(Optional vFila As DataRow = Nothing)
            If vFila IsNot Nothing Then
                Try
                    ID = vFila("ID_EMP")
                    Nombre = vFila("NOMBRE_EMP")
                    BaseDatos = vFila("BASE_EMP")
                    If Not IsDBNull(vFila("BASE_EMP_SERVER")) Then BDServer = vFila("BASE_EMP_SERVER")
                    If Not IsDBNull(vFila("BASE_EMP_PASS")) Then BDPassword = vFila("BASE_EMP_PASS")
                    If Not IsDBNull(vFila("BASE_EMP_USER")) Then BDUsuario = vFila("BASE_EMP_USER")
                    Estado = vFila("ESTADO_EMP")
                    tipoMando = vFila("TIPO_MANDO")
                    cargoGerente = vFila("CARGO_GERENTE_WEB")
                    muestraDescargaInf = IIf(vFila("MUESTRA_DESCARGA_INF") = 1, True, False)
                Catch ex As Exception
                    Throw New Exception("No se pudo convertir el registro de Empresa.", ex)
                End Try
            End If
        End Sub

        Public Property ID As Long
        Public Property Nombre As String
        Public Property tipoMando As Integer
        Public Property muestraDescargaInf As Boolean

        Public Property BaseDatos As String
            Get
                BaseDatos = DatosConexion.Catalogo
            End Get
            Set(value As String)
                If DatosConexion Is Nothing Then
                    DatosConexion = New DatosConexion
                    DatosConexion.Servidor = "KR4\EXPRESS"
                    DatosConexion.UsarWindowsAutentification = True
                End If
                DatosConexion.Catalogo = value
            End Set
        End Property
        Public Property BDServer As String
            Get
                BDServer = DatosConexion.Servidor
            End Get
            Set(value As String)
                If DatosConexion Is Nothing Then
                    DatosConexion = New DatosConexion
                End If
                DatosConexion.Servidor = value
            End Set
        End Property
        Public Property BDUsuario As String
            Get
                BDUsuario = DatosConexion.Usuario
            End Get
            Set(value As String)
                If DatosConexion Is Nothing Then
                    DatosConexion = New DatosConexion
                End If
                DatosConexion.Usuario = value
                DatosConexion.UsarWindowsAutentification = String.IsNullOrEmpty(value)
            End Set
        End Property

        ''' <summary>
        ''' Este password siempre debe venir encriptado y se entrega desencriptado. es decir, al usar el setter debe venir la cadena encriptada.
        ''' al usar el getter se obtendra la contraseña desencriptada.
        ''' </summary>
        ''' <value></value>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Property BDPassword As String
            Get
                BDPassword = DatosConexion.Clave
            End Get
            Set(value As String)
                If DatosConexion Is Nothing Then
                    DatosConexion = New DatosConexion
                End If
                Dim vEn As New Encriptacion
                DatosConexion.Clave = vEn.DecryptText(value, ClaveEncripta)
            End Set
        End Property

        Public Property Estado As EstadoEmpresa
        Public Property DatosConexion As DatosConexion

        Public Property ObraIDSelected As Long
        Public Property ObraNombreSelected As String
        Public Property ObraCodigoSelected As String
        Public Property GanttIDSelected As Long
        Public Property GanttNombreSelected As String
        Public Property FechaEvalSelected As Date
        Public Property FechaAvaIDSelected As Long
        Public Property PorcAvanceSelected As Decimal
        Public Property PorcDesvSelected As Decimal
        Public Property DesvProyDias As Integer
        Public Property FechaTermProy As Date
        Public Property utilizaFoco As Integer
        Public Property cierreManual As Integer
        Public Property FechaCierreBalance As Nullable(Of Date)
        Public Property FechaAvaCierreBalance As Nullable(Of Date)
        Public Property ganttCierreBalance As String
        Public Property idGanttCierreBalance As Nullable(Of Integer)
        Public Property vistoBueno As Boolean
        Public Property estadoFlujo As Boolean
        Public Property cargoGerente As String
        Public Property resultadoActualFoco As Nullable(Of Double)
        Public Property calendario As String
        Public Property usaInformeCC As Integer
        Public Property fechaCierreMO As Nullable(Of Date)
        Public Property fechaCierreSC As Nullable(Of Date)
        Public Property porcRealBal As Double
        Public Property paginaInicio As String
    End Class

End Namespace
