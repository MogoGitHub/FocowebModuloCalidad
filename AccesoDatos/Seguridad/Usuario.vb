Imports SUL
Imports System.Data.SqlClient
Imports EL
Imports EL.Seguridad
Imports EL.Sistema


Namespace Seguridad
    Public Class Usuario

        Public Shared Function Ingresar(vUsuario As String, vPassword As String, vStrConexion As String,
                                        ip As String, host As String) As UsuarioSistema
            Dim vCon As New Conexion(vStrConexion)
            Dim vParam As New Dictionary(Of String, Object)
            Dim vTablas As DataSet = Nothing
            Dim vEnc As New Encriptacion
            With vParam
                .Add("@USUARIO", vUsuario)
                .Add("@PASSWORD", vEnc.SHA256Hash(vPassword))
                .Add("@IP", ip)
                .Add("@HOST", host)
            End With
            Try
                vTablas = vCon.ExecSP_DS("SP_SEGURIDAD_INGRESAR", vParam)
            Catch ex As SqlException
                If ex.Number = 50000 And ex.Class = 16 Then
                    Throw ex
                    Return Nothing
                End If
            Catch ex As Exception
                Throw New Exception("No se ha podido validar el ingreso. Intente más tarde.", ex)
            End Try
            If vTablas IsNot Nothing AndAlso vTablas.Tables.Count = 2 Then
                Ingresar = New UsuarioSistema
                UsuarioSistema.StrCon = vStrConexion
                Ingresar.NickName = vTablas.Tables(0).Rows(0).Item("NOMBRE_USU")
                Ingresar.Estado = EL.Seguridad.EstadoUsuario.Activo
                Ingresar.ID_MAESTRO = vTablas.Tables(0).Rows(0).Item("ID_USU")

                Ingresar.ip = ip
                Ingresar.hostName = host
                Ingresar.UsuarioTest = vTablas.Tables(0).Rows(0).Item("USUARIO_TEST")
                If Not IsDBNull(vTablas.Tables(0).Rows(0).Item("ES_ADMIN_USU")) Then _
                    Ingresar.EsAdmin = vTablas.Tables(0).Rows(0).Item("ES_ADMIN_USU")
                Ingresar.Empresas = vTablas.Tables(1)
                If Ingresar.Empresas.Rows.Count = 1 Then
                    Ingresar.EmpresaSelected = New EL.Empresa.Empresa(Ingresar.Empresas.Rows(0))
                End If
            Else
                Ingresar = Nothing
            End If
        End Function

        Public Shared Sub CargarParticular(vUsuario As EL.Seguridad.Usuario)
            Dim vCon As New Conexion(vUsuario.EmpresaSelected.DatosConexion.GenerarStringConexion)
            Dim vParam As New Dictionary(Of String, Object)
            Dim vTablas As DataSet = Nothing
            With vParam
                .Add("@USU_NOMBRE", vUsuario.NickName)
            End With
            Try
                vTablas = vCon.ExecSP_DS("WEB_SP_SEGU_USUARIO_SELECT", vParam)
            Catch ex As Exception
                Throw New Exception("No se ha podido consultar los datos del usuario. Intente más tarde.", ex)
            End Try
            If vTablas IsNot Nothing AndAlso vTablas.Tables.Count > 1 AndAlso vTablas.Tables(0).Rows.Count = 1 Then
                vUsuario.NombreLargo = vTablas.Tables(0).Rows(0).Item("nomcom_Usu")
                vUsuario.IdPerfil = vTablas.Tables(0).Rows(0).Item("ID_PERF")
                ' vUsuario.Perfil = vTablas.Tables(0).Rows(0).Item("NOMBRE_PERF")
                vUsuario.ID_PARTICULAR = vTablas.Tables(0).Rows(0).Item("id_Usu")
                vUsuario.Opciones = vTablas.Tables(1)
                If vTablas.Tables(0).Rows(0).Item("PAGINA_INI_USU") <> "" Then
                    vUsuario.EmpresaSelected.paginaInicio = vTablas.Tables(0).Rows(0).Item("PAGINA_INI_USU")
                    vUsuario.PaginaInicio = vTablas.Tables(0).Rows(0).Item("PAGINA_INI_USU")
                Else
                    If IsNothing(vUsuario.EmpresaSelected.paginaInicio) = False Then
                        vUsuario.PaginaInicio = vUsuario.EmpresaSelected.paginaInicio
                    Else
                        vUsuario.EmpresaSelected.paginaInicio = vTablas.Tables(0).Rows(0).Item("PAGINA_INI")
                        vUsuario.PaginaInicio = vTablas.Tables(0).Rows(0).Item("PAGINA_INI")
                    End If
                End If
                vUsuario.EmpresaSelected.ObraIDSelected = vTablas.Tables(0).Rows(0).Item("ID_OBR_PRED")
            End If
        End Sub

        Public Shared Function CambiarPass(vUsuario As String,
                                           vPassword As String,
                                           newPass As String,
                                           ip As String,
                                           host As String,
                                           vStrConexion As String,
                                           idEmpresa As Integer) As Boolean
            Dim vCon As New Conexion(vStrConexion)
            Dim vParam As New Dictionary(Of String, Object)
            Dim vParamOut As New Dictionary(Of String, SqlDbType)
            Dim datos As SqlParameterCollection
            Dim vEnc As New Encriptacion
            With vParam
                .Add("@idUsr", vUsuario)
                .Add("@passAnt", vEnc.SHA256Hash(vPassword))
                .Add("@passNueva", vEnc.SHA256Hash(newPass))
                .Add("@ip", ip)
                .Add("@hostname", host)
                .Add("@idEmpresa", idEmpresa)

            End With
            With vParamOut
                .Add("@exito", SqlDbType.Int)
            End With

            Try
                datos = vCon.ExecSPOut("SP_WEB_CAMBIO_PASS", vParam, vParamOut, Nothing, 90)
            Catch ex As SqlClient.SqlException
                Throw ex
            Catch ex As Exception
                Throw New Exception("No se ha podido validar el ingreso. Intente más tarde.", ex)
            End Try

            If datos("@exito").Value = 1 Then Return True
            Return False
        End Function

        Public Shared Function Buscar(vNombre As String, vIDEmpresa As Long, vStrConexion As String) As DataTable
            Dim vCon As New Conexion(vStrConexion)
            Dim vParam As New Dictionary(Of String, Object)

            If Not String.IsNullOrWhiteSpace(vNombre) Then vParam.Add("@USU_NOMBRE", vNombre)
            If vIDEmpresa <> 0 Then vParam.Add("@EMP_ID", vIDEmpresa)
            Try
                Buscar = vCon.ExecSP("SP_USUARIO_SELECT", vParam)
            Catch ex As Exception
                Throw New Exception("No se pudo leer la lista de usuarios del sistema.", ex)
            End Try
        End Function

        Public Shared Sub AsociarEmpresa(vID_USU As Long, vID_EMP As Long, vStrConexion As String)
            Dim vCon As New Conexion(vStrConexion)
            Dim vParam As New Dictionary(Of String, Object)

            vParam.Add("@EMP_ID", vID_EMP)
            vParam.Add("@USU_ID", vID_USU)

            Try
                vCon.ExecSP("SP_EMPRESA_ASOC_USUARIO", vParam)
            Catch ex As Exception
                Throw New Exception("No se pudo asociar la empresa al usuario.", ex)
            End Try
        End Sub

        Public Shared Sub DesasociarEmpresa(vID_USU As Long, vID_EMP As Long, vStrConexion As String)
            Dim vCon As New Conexion(vStrConexion)
            Dim vParam As New Dictionary(Of String, Object)

            vParam.Add("@EMP_ID", vID_EMP)
            vParam.Add("@USU_ID", vID_USU)

            Try
                vCon.ExecSP("SP_EMPRESA_DESASOC_USUARIO", vParam)
            Catch ex As Exception
                Throw New Exception("No se pudo desvincular la empresa al usuario.", ex)
            End Try
        End Sub

        Public Shared Sub Eliminar(vID_USU As Long, vStrConexion As String)
            Dim vCon As New Conexion(vStrConexion)
            Dim vParam As New Dictionary(Of String, Object)

            vParam.Add("@USU_ID", vID_USU)

            Try
                vCon.ExecSP("SP_USUARIO_DELETE", vParam)
            Catch ex As Exception
                Throw New Exception("No se pudo eliminar el usuario.", ex)
            End Try
        End Sub

        Shared Sub Editar(vID As Long, vActivo As Boolean, vAdmin As Boolean, usuarioInterno As Boolean, vPassword As String, vStrConexion As String)
            Dim vCon As New Conexion(vStrConexion)
            Dim vParam As New Dictionary(Of String, Object)
            Dim vEnc As New Encriptacion
            vParam.Add("@USU_ID", vID)
            vParam.Add("@USU_ACTIVO", vActivo)
            vParam.Add("@USU_ADMIN", vAdmin)
            vParam.Add("@USU_INTERNO", usuarioInterno)
            If Not String.IsNullOrWhiteSpace(vPassword) Then vParam.Add("@USU_PASS", vEnc.SHA256Hash(vPassword))
            Try
                vCon.ExecSP("SP_USUARIO_UPDATE", vParam)
            Catch ex As Exception
                Throw New Exception("No se pudo editar el usuario.", ex)
            End Try
        End Sub

        Shared Sub Agregar(vNombre As String, vAdmin As Boolean, vPassword As String, usuarioInterno As Boolean, vStrConexion As String)
            Dim vCon As New Conexion(vStrConexion)
            Dim vParam As New Dictionary(Of String, Object)
            Dim vEnc As New Encriptacion
            vParam.Add("@USU_NOMBRE", vNombre)
            vParam.Add("@USU_ADMIN", vAdmin)
            vParam.Add("@USU_PASS", vEnc.SHA256Hash(vPassword))
            vParam.Add("@USU_INTERNO", usuarioInterno)
            Try
                vCon.ExecSP("SP_USUARIO_INSERT", vParam)
            Catch ex As Exception
                Throw New Exception("No se pudo agregar el usuario.", ex)
            End Try
        End Sub

        ''' <summary>
        ''' Busca en la base de datos identificada por el string de conexion vEn y si existe el usuario retorna el id del perfil que tiene 
        ''' asociado. si no tiene perfil asociado retorna -1.
        ''' </summary>
        ''' <param name="vUserNick">El usuario se buscará por el nombre.</param>
        ''' <param name="vEn">String de conexion de la base de datos a buscar.</param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Shared Function ObtenerIDPerf(vUserNick As String, vEn As String) As Long
            Dim vCon As New Conexion(vEn)
            Dim vParam As New Dictionary(Of String, Object)
            Dim vTabla As DataTable
            If Not String.IsNullOrWhiteSpace(vUserNick) Then vParam.Add("@USU_NOMBRE", vUserNick)
            vParam.Add("@SOLO_USUARIO", 2)
            Try
                vTabla = vCon.ExecSP("WEB_SP_SEGU_USUARIO_SELECT", vParam)
            Catch ex As Exception
                Throw New Exception("No se pudo leer el perfil del usuarios del sistema.", ex)
            End Try
            If vTabla IsNot Nothing AndAlso vTabla.Rows.Count = 1 Then
                ObtenerIDPerf = vTabla.Rows(0).Item("ID_PERF")
            Else
                ObtenerIDPerf = -1
            End If

        End Function

        Shared Sub AsociarPerfil(vNombreUsu As String, vIDPerf As Long, vEn As String)
            Dim vCon As New Conexion(vEn)
            Dim vParam As New Dictionary(Of String, Object)

            vParam.Add("@USU_NOMBRE", vNombreUsu)
            vParam.Add("@ID_PERF", vIDPerf)
            Try
                vCon.ExecSP("WEB_SP_SEGU_USUARIO_UPDATE", vParam)
            Catch ex As Exception
                Throw New Exception("No se pudo leer el perfil del usuarios del sistema.", ex)
            End Try
        End Sub

        Public Shared Sub RegistrarEventoHistorico(vUsuId As Long,
                                                   vCod As EL.Seguridad.CodigoHistorico,
                                                   vEvento As String,
                                                   vEn As String,
                                                   ip As String,
                                                   host As String,
                                                   idEmpresa As Integer,
                                                   nombreObra As String,
                                                   idObra As Integer)
            Dim vCon As New Conexion(vEn)
            Dim vParam As New Dictionary(Of String, Object)

            vParam.Add("@ID_USU", vUsuId)
            vParam.Add("@COD_HIST", vCod)
            vParam.Add("@EVENTO_HIST", vEvento)
            vParam.Add("@HOST", host)
            vParam.Add("@IP", ip)
            vParam.Add("@idEmpresa", idEmpresa)
            If nombreObra Is Nothing Then
                vParam.Add("@obra", "")

            Else
                vParam.Add("@obra", nombreObra)
            End If
            vParam.Add("@idObra", idObra)
            Try
                vCon.ExecSP("SP_HISTORICO_USUARIO_INSERT", vParam)
            Catch ex As Exception
                Throw New Exception("No se pudo cerrar adecuadamente la sesión.", ex)
            End Try
        End Sub

        Public Shared Sub RegistrarDescargaBalance(usuario As EL.Seguridad.Usuario)
            Dim vCon As New Conexion(usuario.EmpresaSelected.DatosConexion.GenerarStringConexion)
            Dim vParam As New Dictionary(Of String, Object)

            vParam.Add("@idObra", usuario.EmpresaSelected.ObraIDSelected)
            vParam.Add("@fechaBal", usuario.EmpresaSelected.FechaCierreBalance)
            vParam.Add("@idUsuario", usuario.ID_PARTICULAR)

            Try
                vCon.ExecSP("SP_GUARDA_LOG_DESCARGA_BALANCE", vParam)
            Catch ex As Exception
                Throw New Exception("No se pudo cerrar adecuadamente la sesión.", ex)
            End Try
        End Sub

        Public Shared Function Historial(vUsuId As Long, vCuanto As Integer, vEn As String) As DataTable
            Dim vCon As New Conexion(vEn)
            Dim vParam As New Dictionary(Of String, Object)

            vParam.Add("@ID_USU", vUsuId)
            vParam.Add("@CUANTO", vCuanto)
            Try
                Historial = vCon.ExecSP("SP_HISTORICO_USUARIO_SELECT", vParam)
            Catch ex As Exception
                Throw New Exception("No se pudo leer el historial del usuario.", ex)
            End Try
        End Function

        Public Shared Function ObtHistorial(conexion As String, idEmpresa As Integer, usuarioInterno As Integer) As DataTable
            Dim vCon As New Conexion(conexion)
            Dim vParam As New Dictionary(Of String, Object)
            Dim datos As New DataTable

            If idEmpresa = -1 Then
                vParam.Add("@idEmpresa", DBNull.Value)
            Else
                vParam.Add("@idEmpresa", idEmpresa)
            End If

            If usuarioInterno = -1 Then
                vParam.Add("@usrInterno", DBNull.Value)
            Else
                vParam.Add("@usrInterno", usuarioInterno)
            End If

            Try
                datos = vCon.ExecSP("SP_HISTORICO_USUARIO_SELECT_2", vParam)
            Catch ex As Exception
                Throw New Exception("No se pudo leer el historial del usuario.", ex)
            End Try
            Return datos
        End Function

        Public Shared Function ObtenerListadoUsuarioFoco(vUsuario As EL.Seguridad.Usuario) As List(Of UsuarioFoco)
            Dim vCon As New Conexion(vUsuario.EmpresaSelected.DatosConexion.GenerarStringConexion)
            Dim datos As DataTable
            Dim listaUsuario As New List(Of UsuarioFoco)
            Try
                datos = vCon.ExecSP("SP_SEG_OBT_USUARIOS")
            Catch ex As Exception
                Throw New Exception("No se ha podido consultar los datos del usuario. Intente más tarde.", ex)
            End Try

            If datos IsNot Nothing AndAlso datos.Rows.Count > 0 Then
                For Each r As DataRow In datos.Rows
                    Dim u As New UsuarioFoco With {.IdUsuario = r("ID_USU"),
                                                  .NombreUsu = r("NOMBRE_USU"),
                                                  .NombreCompleto = r("NOMCOM_USU"),
                                                  .PassWord = r("PASS_ENCRIPTADA"),
                                                  .UsrBasico = r("BASICO_USU"),
                                                  .EsAdmin = r("ADMIN_USU"),
                                                  .RestringueObra = r("OBRAS_USU"),
                                                   .UsuarioInterno = r("USUARIO_INTERNO"),
                                                  .Email = r("EMAIL_USU"),
                                                  .Estado = r("ESTADO_USU"),
                                                  .IdDepto = r("ID_DEP"),
                                                  .Departamento = r("NOMBRE_DEP"),
                                                   .ObraPredeterminada = r("ID_ObrPred"),
                                                   .UsuarioWeb = IIf(r("USR_WEB") = 1, True, False),
                                                   .UsuarioFoco = IIf(r("USR_FOCO") = 1, True, False),
                                                   .UsuarioAndroid = IIf(r("USR_FOCO_ANDROID") = 1, True, False)}
                    If IsDBNull(r("FIRMA_USU")) Then
                        u.Firma = Nothing
                    Else
                        u.Firma = DirectCast(r("FIRMA_USU"), Byte())
                    End If
                    listaUsuario.Add(u)
                Next

            End If
            Return listaUsuario
        End Function

        Public Shared Function ObtenerUsuarioFoco(vUsuario As EL.Seguridad.Usuario, idUsuario As Integer) As UsuarioFoco
            Dim vCon As New Conexion(vUsuario.EmpresaSelected.DatosConexion.GenerarStringConexion)
            Dim datos As DataTable
            Dim parametrosIn As New Dictionary(Of String, Object)
            parametrosIn.Add("@idUsuario", idUsuario)

            Try
                datos = vCon.ExecSP("SP_SEG_OBT_INFO_USUARIO", parametrosIn)
            Catch ex As Exception
                Throw New Exception("No se ha podido consultar los datos del usuario. Intente más tarde.", ex)
            End Try

            If datos IsNot Nothing AndAlso datos.Rows.Count > 0 Then
                For Each r As DataRow In datos.Rows
                    Dim usr As New UsuarioFoco With {.idUsuario = r("ID_USU"),
                                                  .NombreUsu = r("NOMBRE_USU"),
                                                  .NombreCompleto = r("NOMCOM_USU"),
                                                  .PassWord = r("PASS_ENCRIPTADA"),
                                                  .UsrBasico = r("BASICO_USU"),
                                                  .EsAdmin = r("ADMIN_USU"),
                                                  .RestringueObra = r("OBRAS_USU"),
                                                  .Email = r("EMAIL_USU"),
                                                  .Estado = r("ESTADO_USU"),
                                                  .IdDepto = r("ID_DEP"),
                                                  .Departamento = r("NOMBRE_DEP"),
                                                   .ObraPredeterminada = r("ID_ObrPred")}
                    If IsDBNull(r("FIRMA_USU")) Then
                        usr.Firma = Nothing
                    Else
                        usr.Firma = DirectCast(r("FIRMA_USU"), Byte())
                    End If
                    Return usr
                Next

            End If
            Return Nothing



        End Function


        Shared Function ExisteUsuario(vNombreUsu As String, usrInterno As Integer, vEn As String) As Boolean
            Dim vCon As New Conexion(vEn)
            Dim vParam As New Dictionary(Of String, Object)
            Dim paramOut As New Dictionary(Of String, SqlDbType)
            Dim parametros As SqlParameterCollection

            vParam.Add("@usuario", vNombreUsu)
            vParam.Add("@usuarioInterno", IIf(usrInterno, 1, 0))

            paramOut.Add("@existeUsr", SqlDbType.Int)
            Try
                parametros = vCon.ExecSPOut("SP_USUARIO_EXISTE_USUARIO", vParam, paramOut)

                Return IIf(parametros("@existeUsr").Value = 1, True, False)
            Catch ex As Exception
                Throw New Exception("No se pudo  verfiicar existencia del usuario.", ex)
            End Try

            Return False
        End Function


        Shared Function InsertaUsuarioFoco(usuario As EL.Seguridad.Usuario, newUser As UsuarioFoco,
                                           perfiles As DataTable,
                                           obras As DataTable,
                                           insumos As DataTable) As Integer
            Dim vCon As New Conexion(usuario.EmpresaSelected.DatosConexion.GenerarStringConexion)
            Dim vParam As New Dictionary(Of String, Object)

            Dim paramOut As New Dictionary(Of String, SqlDbType)
            Dim parametros As SqlParameterCollection
            Dim vEnc As New Encriptacion

            vParam.Add("@nombreUsuario", newUser.NombreUsu)
            vParam.Add("@nombreCompleto", newUser.NombreCompleto)
            vParam.Add("@pass", vEnc.SHA256Hash(newUser.PassWord))
            vParam.Add("@passNormal", newUser.PassWord)
            vParam.Add("@esAdmin", newUser.EsAdmin)
            vParam.Add("@restringueObra", newUser.RestringueObra)
            vParam.Add("@email", newUser.Email)
            vParam.Add("@estado", newUser.Estado)
            vParam.Add("@idDepto", newUser.IdDepto)
            vParam.Add("@idObraPreder", newUser.ObraPredeterminada)
            vParam.Add("@usrWeb", IIf(newUser.UsuarioWeb, 1, 0))
            vParam.Add("@usrFoco", IIf(newUser.UsuarioFoco, 1, 0))
            vParam.Add("@usrInterno", IIf(newUser.UsuarioInterno, 1, 0))
            vParam.Add("@usrAndroid", IIf(newUser.UsuarioAndroid, 1, 0))

            vParam.Add("@perfiles", perfiles)
            vParam.Add("@obras", obras)
            vParam.Add("@tipoInsumo", insumos)



            paramOut.Add("@idUsuarioOut", SqlDbType.Int)

            Try

                parametros = vCon.ExecSPOut("WEB_SP_SEGU_USUARIO_INSERT", vParam, paramOut)


                Return IIf(IsDBNull(parametros("@idUsuarioOut").Value), -1, parametros("@idUsuarioOut").Value)

            Catch ex As SqlException
                If ex.Number = 50000 And ex.Class = 16 Then
                    Throw ex
                End If
            Catch ex As Exception
                Throw New Exception("No se pudo insertar el usuario.", ex)
            End Try

            Return -1
        End Function


        Shared Function ModificarUsuarioFoco(usuario As EL.Seguridad.Usuario,
                                             newUser As UsuarioFoco,
                                              perfiles As DataTable,
                                           obras As DataTable,
                                           insumos As DataTable) As Boolean
            Dim vCon As New Conexion(usuario.EmpresaSelected.DatosConexion.GenerarStringConexion)
            Dim vParam As New Dictionary(Of String, Object)
            Dim paramOut As New Dictionary(Of String, SqlDbType)
            Dim parametros As SqlParameterCollection
            Dim vEnc As New Encriptacion

            vParam.Add("@idUsuario", newUser.IdUsuario)
            vParam.Add("@nombreUsuario", newUser.NombreUsu)
            vParam.Add("@nombreCompleto", newUser.NombreCompleto)
            If newUser.PassWord = Nothing Then
                vParam.Add("@pass", DBNull.Value)
                vParam.Add("@passNormal", DBNull.Value)
            Else
                vParam.Add("@pass", vEnc.SHA256Hash(newUser.PassWord))
                vParam.Add("@passNormal", newUser.PassWord)
            End If

            vParam.Add("@esAdmin", newUser.EsAdmin)
            vParam.Add("@restringueObra", newUser.RestringueObra)
            vParam.Add("@email", newUser.Email)
            vParam.Add("@estado", newUser.Estado)
            vParam.Add("@idDepto", newUser.IdDepto)
            vParam.Add("@idObraPreder", newUser.ObraPredeterminada)
            vParam.Add("@usrWeb", IIf(newUser.UsuarioWeb, 1, 0))
            vParam.Add("@usrFoco", IIf(newUser.UsuarioFoco, 1, 0))
            vParam.Add("@usrInterno", IIf(newUser.UsuarioInterno, 1, 0))
            vParam.Add("@usrAndroid", IIf(newUser.UsuarioAndroid, 1, 0))

            vParam.Add("@perfiles", perfiles)
            vParam.Add("@obras", obras)
            vParam.Add("@tipoInsumo", insumos)

            Try
                parametros = vCon.ExecSPOut("WEB_SP_SEGU_USUARIO_UPDATE_2", vParam)
                Return True

            Catch ex As Exception
                Throw New Exception("No se pudo insertar el usuario.", ex)
            End Try

            Return False
        End Function

        Shared Function EliminarUsuarioFoco(usuario As EL.Seguridad.Usuario, idUsuario As Integer) As Boolean
            Dim vCon As New Conexion(usuario.EmpresaSelected.DatosConexion.GenerarStringConexion)
            Dim vParam As New Dictionary(Of String, Object)
            Dim paramOut As New Dictionary(Of String, SqlDbType)
            vParam.Add("@idUsuario", idUsuario)
            Try
                vCon.Exec("WEB_SP_SEGU_USUARIO_DELETE", vParam)
                Return True

            Catch ex As Exception
                Throw New Exception("No se pudo insertar el usuario.", ex)
            End Try

            Return False
        End Function

        Shared Function ObtUsuarioPerfil(usuario As EL.Seguridad.Usuario, idPerfil As Integer) As List(Of UsuarioFoco)
            Dim vCon As New Conexion(usuario.EmpresaSelected.DatosConexion.GenerarStringConexion)
            Dim vParam As New Dictionary(Of String, Object)
            Dim listaUsuario As New List(Of UsuarioFoco)
            Dim datos As DataTable
            If idPerfil = -1 Then
                vParam.Add("@idPerfil", DBNull.Value)
            Else
                vParam.Add("@idPerfil", idPerfil)
            End If

            vParam.Add("@internos", IIf(Not usuario.usuarioInterno, 0, DBNull.Value)) ''externos no ven a personal interno




            Try
                datos = vCon.ExecSP("SEG_OBT_USUARIOS_PERFIL", vParam)

                If datos.Rows.Count > 0 Then
                    For Each r As DataRow In datos.Rows
                        Dim usr As New UsuarioFoco With {.IdUsuario = r("id_Usu"),
                                                         .NombreUsu = r("nombre_Usu"),
                                                         .NombreCompleto = r("nomcom_Usu")}
                        listaUsuario.Add(usr)
                    Next
                End If

                Return listaUsuario

            Catch ex As Exception
                Throw New Exception("No se pudo obtener la lista de usuarios.", ex)
            End Try

            Return Nothing
        End Function


        Shared Function ObtEstadoBloqueoUsuario(vStrConexion As String, nombreUsr As String) As Integer
            Dim vCon As New Conexion(vStrConexion)
            Dim vParam As New Dictionary(Of String, Object)
            Dim vParamOut As New Dictionary(Of String, SqlDbType)
            Dim salida As SqlParameterCollection

            vParam.Add("@usuario", nombreUsr)
            vParamOut.Add("@estado", SqlDbType.Int)

            Try
                salida = vCon.ExecSPOut("SP_OBT_BLOQUEO_USUARIO", vParam, vParamOut)
                If IsDBNull(salida("@estado").Value) Then Return 0

                Return IIf(salida("@estado").Value = 1, True, False)
            Catch ex As Exception
                Throw New Exception("No se pudo obtener el estado del usuario.", ex)
            End Try

            Return False
        End Function

        Shared Function ActualizaEstadoBloqueoUsuario(vStrConexion As String, nombreUsr As String, usrBloqueado As Boolean) As Integer
            Dim vCon As New Conexion(vStrConexion)
            Dim vParam As New Dictionary(Of String, Object)

            vParam.Add("@usuario", nombreUsr)
            vParam.Add("@usrBloqueado", usrBloqueado)

            Try
                vCon.ExecSP("SP_ACTUALIZA_BLOQUEO_USUARIO", vParam)
                Return True
            Catch ex As Exception
                Throw New Exception("No se pudo obtener el estado del usuario.", ex)
            End Try

            Return False
        End Function



        ''' <summary>
        ''' Obtiene tipos de insumos
        ''' </summary>
        ''' <param name="vDebugStr"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>


        Shared Function InsertarInsumoUsuario(usuario As EL.Seguridad.Usuario, idPerfil As Integer) As List(Of UsuarioFoco)
            Dim vCon As New Conexion(usuario.EmpresaSelected.DatosConexion.GenerarStringConexion)
            Dim vParam As New Dictionary(Of String, Object)
            Dim listaUsuario As New List(Of UsuarioFoco)
            Dim datos As DataTable
            If idPerfil = -1 Then
                vParam.Add("@idPerfil", DBNull.Value)
            Else
                vParam.Add("@idPerfil", idPerfil)
            End If




            Try
                datos = vCon.ExecSP("SEG_OBT_USUARIOS_PERFIL", vParam)

                If datos.Rows.Count > 0 Then
                    For Each r As DataRow In datos.Rows
                        Dim usr As New UsuarioFoco With {.IdUsuario = r("id_Usu"),
                                                         .NombreUsu = r("nombre_Usu"),
                                                         .NombreCompleto = r("nomcom_Usu")}
                        listaUsuario.Add(usr)
                    Next
                End If

                Return listaUsuario

            Catch ex As Exception
                Throw New Exception("No se pudo obtener la lista de usuarios.", ex)
            End Try

            Return Nothing
        End Function
    End Class
End Namespace