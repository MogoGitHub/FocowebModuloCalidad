Public Class Main
    Inherits System.Web.UI.MasterPage

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        Page.ClientScript.GetPostBackEventReference(Me, "")
        If Not Page.IsPostBack Then
            CargarObras()

            'lnk_CM.Attributes.Add("onclick", Page.ClientScript.GetPostBackEventReference(Me, lnk_CM.ID.ToString))
            'lnk_CM.HRef = "~/foco/proyectosEnGrupo.aspx"

        End If
    End Sub

    Private Sub CargarObras()
        Try
            rpt1.DataSource = DAL.Empresa.Obra.ListarObrasUsuario(Session("SSN_USUARIO"))
            rpt1.DataBind()

        Catch ex As Exception
        End Try
    End Sub

    Private Sub lnk_CM_ServerClick(sender As Object, e As System.EventArgs) Handles lnk_CM.ServerClick
        Response.Redirect("~/Sistema/FrmInicio.aspx", False)
    End Sub

    Private Sub lnk_exit_ServerClick(sender As Object, e As System.EventArgs) Handles lnk_exit.ServerClick
        Response.Redirect("../Seguridad/FrmLogin.aspx?Raz=3")
    End Sub

End Class