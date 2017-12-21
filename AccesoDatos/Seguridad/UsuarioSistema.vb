Imports System.Net

Namespace Seguridad
    Public Class UsuarioSistema
        Inherits EL.Seguridad.Usuario

        Shared Property StrCon As String

        Public Property LastPage As String

        Protected Overrides Sub Finalize()
            'Aqui tenemos que ver como 

            If LoggedIn Then
                Dim idEmpresa As Integer
                If Me.EmpresaSelected Is Nothing Then
                    idEmpresa = 0
                Else
                    idEmpresa = Me.EmpresaSelected.ID
                End If

                Usuario.RegistrarEventoHistorico(ID_MAESTRO, EL.Seguridad.CodigoHistorico.SesionCaducada, "La sesión expiró. El sistema regitra cierre.", StrCon, ip, hostName, Me.EmpresaSelected.ID, "", -1)
            End If
            MyBase.Finalize()
        End Sub

        Public Overrides Property EmpresaSelected As EL.Empresa.Empresa
            Get
                EmpresaSelected = MyBase.EmpresaSelected
            End Get
            Set(value As EL.Empresa.Empresa)
                MyBase.EmpresaSelected = value

                Dim idEmpresa As Integer
                If Me.EmpresaSelected Is Nothing Then
                    idEmpresa = 0
                Else
                    idEmpresa = Me.EmpresaSelected.ID
                End If

                'como cambio la empresa entonces tenemos que registrarlo.
                DAL.Seguridad.Usuario.RegistrarEventoHistorico(ID_MAESTRO, EL.Seguridad.CodigoHistorico.SeleccionEmpresa, "Ha seleccionado la Empresa " & value.Nombre, StrCon, ip, hostName, idEmpresa, "", -1)
            End Set
        End Property



    End Class
End Namespace
