Imports SUL

Namespace Empresa

    Public Class Obra
        Public Shared Function ListarObrasUsuario(vUsuario As EL.Seguridad.Usuario) As DataTable
            Dim vCon As New Conexion(vUsuario.EmpresaSelected.DatosConexion.GenerarStringConexion)
            Dim vParam As New Dictionary(Of String, Object)

            vParam.Add("@ID_USU", vUsuario.ID_PARTICULAR)
            Try
                ListarObrasUsuario = vCon.ExecSP("WEB_SP_OBRAS_POR_USUARIO", vParam)
            Catch ex As Exception
                Throw New Exception("No se puede listar las obras de la empresa seleccionada.", ex)
            End Try
        End Function
    End Class
End Namespace
