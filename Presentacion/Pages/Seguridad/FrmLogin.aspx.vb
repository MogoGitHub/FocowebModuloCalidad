Imports Presentacion.CSS
Imports Presentacion.Maestro
Imports System.Net
Imports System.Data.SqlClient

Public Class FrmLoginV2
    Inherits System.Web.UI.Page

    Dim ip As String = ""
    Dim hostName As String = ""

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim vRazon As String = Request.QueryString("Raz")
        If Not String.IsNullOrWhiteSpace(vRazon) Then
            Select Case vRazon
                Case "1" 'La session caducó o el intento de ingreso no es válido.
                    Alerta("La sesión ha caducado. Ingrese nuevamente.")
                Case "2" 'El usuario no tiene empresas.
                    Alerta("La cuenta del usuario no tiene empresas asociadas.")
                Case "3"
                    Mensaje("Su sesión se ha cerrado correctamente.")
            End Select
        End If
    End Sub

    Private Function Validar() As Boolean
        'Validar = True
        'Dim vStr As String = String.Empty

        'litError.Text = String.Empty
        'If String.IsNullOrWhiteSpace(txtUser.Text) Then
        '    txtUser.CssClass = ControlError
        '    vStr = "El campo Usuario no puede estar vacío.<br/>"
        '    Validar = False
        'Else
        '    txtUser.CssClass = ControlNormal
        'End If

        'If String.IsNullOrWhiteSpace(txtPass.Text) Then
        '    txtPass.CssClass = ControlError
        '    vStr &= "El campo Contraseña no puede estar vacío.<br/>"
        '    Validar = False
        'Else
        '    txtPass.CssClass = ControlNormal
        'End If

        'If Not Validar Then
        '    Alerta(vStr)
        'End If
    End Function

    Private Sub Ingresar()
        Dim vUser As DAL.Seguridad.UsuarioSistema = Nothing
        ObtenerHost()
        Try
            vUser = DAL.Seguridad.Usuario.Ingresar(fname.Value, fpass.Value, Maestro.CadenaConexion, ip, hostName)
        Catch sqlEx As SqlException
            Alerta(sqlEx.Message)
            Exit Sub
        Catch ex As Exception
            'MostrarError(ex.Message, , ex)
            Exit Sub
        End Try
        If vUser IsNot Nothing Then
            If vUser.Empresas.Rows.Count = 0 Then
                Alerta("La cuenta del usuario no tiene empresas asociadas.")
            Else
                If vUser.Empresas.Rows.Count = 1 Then
                    'Solo tiene una empresa
                    DAL.Seguridad.Usuario.CargarParticular(vUser)
                End If
                'Podemos redireccionar
                Session(SSN_USUARIO) = vUser
                vUser.LoggedIn = True
                'Response.Redirect(ConfigurationManager.AppSettings("PaginaInicio"))
                If IsNothing(vUser.EmpresaSelected) = False Then
                    If IsNothing(vUser.EmpresaSelected.paginaInicio) = True Then
                        Alerta("La cuenta del usuario no tiene empresas asociadas.")
                    Else
                        Response.Redirect(vUser.EmpresaSelected.paginaInicio, False)
                    End If
                Else
                    Response.Redirect(ConfigurationManager.AppSettings("PaginaInicio"))
                End If
                End If
        Else
            Alerta("El nombre de usuario o contraseña no es válido.")
        End If
    End Sub

    Private Sub Alerta(vMensaje As String)
        'litError.Text = vMensaje
        'pError.CssClass = CajaError
        'pError.Visible = True
        div_msj.Attributes.Add("style", "visibility:visible")
        msg.InnerText = vMensaje
    End Sub

    Private Sub Mensaje(vMensaje As String)
        'litError.Text = vMensaje
        'pError.CssClass = CajaInfo
        'pError.Visible = True
        div_msj.Attributes.Add("style", "visibility:visible")
        icoMsj.Attributes.Add("class", "fa fa-info-circle fa-2x")
        msg.InnerText = vMensaje
    End Sub


    Private Sub ObtenerHost()
        Dim strHostName As String = Dns.GetHostName()
        Dim ipEntry As IPHostEntry = Dns.GetHostEntry(strHostName)

        ip = Convert.ToString(ipEntry.AddressList(ipEntry.AddressList.Length - 1))
        hostName = Convert.ToString(ipEntry.HostName)

        'IP detras de Proxy O Client Machine en ASP.NET  
        Dim IPAdd As String = String.Empty
        IPAdd = Request.ServerVariables("HTTP_X_FORWARDED_FOR")

        If String.IsNullOrEmpty(IPAdd) Then
            IPAdd = Request.ServerVariables("REMOTE_ADDR")
            ip = IPAdd
        End If
        ' strHostName = System.Net.Dns.GetHostEntry(ip).HostName
    End Sub

    'Private Sub btnIngresar_Click(sender As Object, e As System.EventArgs) Handles btnIngresar.Click
    '    Ingresar()
    'End Sub

    Sub Ingresar_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        Ingresar()
    End Sub
End Class