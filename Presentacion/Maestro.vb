Imports System.Reflection

Public Class Maestro

    ''' <summary>
    ''' Obtiene el string de conexion a la base de datos Maestro de Foco en Obra.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared ReadOnly Property CadenaConexion As String
        Get
            CadenaConexion = ConfigurationManager.ConnectionStrings("Maestro").ConnectionString
        End Get
    End Property

    ''' <summary>
    ''' Constante de nombre de Session donde se almacenará el usuario.
    ''' </summary>
    ''' <remarks></remarks>
    Public Const SSN_USUARIO As String = "SSN_USUARIO"

    ''' <summary>
    ''' Constante de nombre de session donde se almacena la bandera de debug
    ''' </summary>
    ''' <remarks></remarks>
    Public Const SSN_DEBUG As String = "SSN_DEBUG"

    ''' <summary>
    ''' constante con la ruta del directorio temporal de archivos
    ''' </summary>
    ''' <remarks></remarks>
    Public Const DIR_TEMP As String = "~/tmp/"

    Public Shared Sub GuardarLogError(vDetalle As String, vEn As String)
        If Not IO.Directory.Exists(vEn & "\LOG") Then
            IO.Directory.CreateDirectory(vEn & "\LOG")
        End If
        vEn &= "\LOG\" & Format(Now, "yyyyMMddHHmmss") & ".log"
        Try
            IO.File.WriteAllText(vEn, vDetalle)
        Catch ex As Exception
            Throw New Exception(ex.Message & ". A causa de: " & vDetalle)
        End Try
    End Sub


    ''' <summary>
    ''' verifica la visibilidad para una lista de controles en un mismo permiso
    ''' </summary>
    ''' <param name="vListaControl"></param>
    ''' <param name="vUsuario"></param>
    ''' <param name="vPagina"></param>
    ''' <remarks></remarks>
    Public Shared Sub ValidaPermisosGrupo(vListaControl As List(Of ControlAcceso), vUsuario As EL.Seguridad.Usuario, vPagina As String)
        Dim vPermisos As String = String.Empty
        Dim vRow As DataRow = Nothing
        If vUsuario.Opciones IsNot Nothing AndAlso vUsuario.Opciones.Rows.Count > 0 Then
            For Each vFila In vUsuario.Opciones.Rows
                If vFila("URL_OPCI") = vPagina Then
                    vPermisos = vFila("FUNCIONES_OPPE")
                    vRow = vFila
                    Exit For
                End If
            Next

            If vRow IsNot Nothing Then

                If vListaControl IsNot Nothing Then
                    For Each c As ControlAcceso In vListaControl
                        ''permiso del control
                        'En este caso revisamos los botones

                        For Each p As String In c.Permiso
                            '  permisoGral.Insert(p.IndexOf("1"), 1)

                            ''el usuario tiene en sus permisos un 1 donde corresponde?
                            If vPermisos(p.IndexOf("1")) = "1" Then
                                c.Control.Visible = True
                                Exit For
                            Else
                                c.Control.Visible = False
                            End If

                        Next

                        'Dim permiso As String = "" 'c.Permiso

                        'Dim index As Integer = permiso.IndexOf("1")
                        'If vPermisos(index) = "1" Then
                        '    c.Control.Visible = True
                        'Else
                        '    c.Control.Visible = False
                        'End If

                    Next
                End If
            Else
                Throw New Exception("El usuario no tiene permisos para visualizar esta página.")
            End If
        Else
            Throw New Exception("El perfil asociado al usuario no tiene permisos asignados.")
        End If
    End Sub

    ''' <summary>
    ''' verifica la visibilidad de un control de acuerdo a los permisos
    ''' </summary>
    ''' <param name="vListaBotones"></param>
    ''' <param name="vUsuario"></param>
    ''' <param name="vPagina"></param>
    ''' <remarks>en el string de permisos, cada 1 significa la visibilidad de un control.</remarks>
    Public Shared Sub ValidaPermisos(vListaBotones As List(Of Control), vUsuario As EL.Seguridad.Usuario, vPagina As String)
        Dim vPermisos As String = String.Empty
        Dim vRow As DataRow = Nothing
        If vUsuario.Opciones IsNot Nothing AndAlso vUsuario.Opciones.Rows.Count > 0 Then
            For Each vFila In vUsuario.Opciones.Rows
                If vFila("URL_OPCI") = vPagina Then
                    vPermisos = vFila("FUNCIONES_OPPE")
                    vRow = vFila
                    Exit For
                End If
            Next


            If vRow IsNot Nothing Then
                'En este caso revisamos los botones
                Dim vI As Integer = 0
                If vListaBotones IsNot Nothing Then
                    For Each vBoton As Control In vListaBotones
                        'If vPermisos.Length < vI Then
                        If vPermisos(vI) = "1" Then
                            vBoton.Visible = True
                        Else : vBoton.Visible = False
                        End If
                        vI += 1
                        '  End If
                    Next
                End If
            Else
                Throw New Exception("El usuario no tiene permisos para visualizar esta página.")
            End If
        Else
            Throw New Exception("El perfil asociado al usuario no tiene permisos asignados.")
        End If
    End Sub

    Public Shared Function RecuperarPermisos(vUsuario As EL.Seguridad.Usuario, vPagina As String) As String
        If vUsuario.Opciones IsNot Nothing AndAlso vUsuario.Opciones.Rows.Count > 0 Then
            For Each vFila In vUsuario.Opciones.Rows
                If vFila("URL_OPCI") = vPagina Then
                    RecuperarPermisos = vFila("FUNCIONES_OPPE")
                    Exit For
                End If
            Next
        Else
            RecuperarPermisos = "000000000"
        End If
    End Function

    Public Shared ReadOnly Property Version As String
        Get
            Dim web As Assembly = Assembly.GetExecutingAssembly() 'Assembly.Load("App_Code")
            Dim webName As AssemblyName = web.GetName()

            Dim myVersion As String = webName.Version.ToString()

            'Version = "[versión: " & ConfigurationManager.AppSettings("version") & "] <br/>" & myVersion
            Version = "[versión: " & myVersion & ConfigurationManager.AppSettings("version") & "]"
        End Get
    End Property

End Class


