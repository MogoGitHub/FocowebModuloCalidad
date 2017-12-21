Imports System.Data.SqlClient

Public Class Conexion

    Private vStrCon As String

    Private vCon As SqlConnection
    Private vTra As IDbTransaction
    Private Const TimeoutConexion As Integer = 90

    Public Property CadenaConexion() As String
        Get
            CadenaConexion = vStrCon
        End Get
        Set(ByVal value As String)
            If String.IsNullOrEmpty(value) Then
                Throw New Exception("No se puede establecer la cadena de conexion a una cadena vacía o nula.")
            End If
            vStrCon = value
        End Set
    End Property

    ''' <summary>
    ''' Constructor de la conexion.
    ''' </summary>
    ''' <param name="vStringConexion">Puede proveer un string de conexion util solo para éste objeto de conexion.</param>
    ''' <remarks></remarks>
    Public Sub New(Optional ByVal vStringConexion As String = "")
        If String.IsNullOrEmpty(vStringConexion) Then
            Throw New Exception("No se puede crear la conexión porque no se ha provisto una cadena de conexión válida.")
        Else
            vStrCon = vStringConexion
        End If
        vCon = New SqlConnection(vStringConexion)
    End Sub

    Public Function ExecSP(ByVal vProcAlmacenado As String, _
                           Optional ByVal vParaMetros As Dictionary(Of String, Object) = Nothing, _
                           Optional ByVal vTimeoutConexion As Integer = TimeoutConexion) As DataTable
        Dim cmdTmp As SqlCommand
        Dim dt As New DataTable

        cmdTmp = New SqlClient.SqlCommand(vProcAlmacenado)
        cmdTmp.CommandType = CommandType.StoredProcedure
        cmdTmp.Connection = vCon
        cmdTmp.CommandTimeout = vTimeoutConexion
        If vTra IsNot Nothing And vCon.State = ConnectionState.Open Then
            cmdTmp.Transaction = vTra
        End If
        If vParaMetros IsNot Nothing Then
            'tenemos argumentos que preparar para el sp.
            For Each vClave As String In vParaMetros.Keys
                cmdTmp.Parameters.AddWithValue(vClave.ToUpper(), vParaMetros(vClave))
            Next
        End If
        Try
            Dim dtd As SqlDataAdapter = New SqlDataAdapter(cmdTmp)
            'solo para debugear habilitar las dos siguientes lineas
            'Dim vDS As New DataSet
            'dtd.Fill(vDS)
            dtd.Fill(dt)
            Return dt
        Catch ex As SqlException
            If ex.Number = 1205 Or ex.Class = 16 Then
                Throw ex
            ElseIf ex.Number = 51000 Then
                Throw ex
            Else
                Throw New ProcedimientoException("No se pudo realizar el proceso. Ha fallado la ejecución del procedimiento almacenado.", ex, vProcAlmacenado, vParaMetros, vStrCon)
            End If
        Catch ex As Exception
            Throw New ProcedimientoException("No se pudo realizar el proceso. Ha fallado la ejecución del procedimiento almacenado.", ex, vProcAlmacenado, vParaMetros, vStrCon)
        End Try
    End Function

    Public Function ExecSP_DS(ByVal vProcAlmacenado As String, _
                           Optional ByVal vParaMetros As Dictionary(Of String, Object) = Nothing, _
                           Optional ByVal vTimeoutConexion As Integer = TimeoutConexion) As DataSet
        Dim cmdTmp As SqlCommand
        ExecSP_DS = New DataSet

        cmdTmp = New SqlClient.SqlCommand(vProcAlmacenado)
        cmdTmp.CommandType = CommandType.StoredProcedure
        cmdTmp.Connection = vCon
        cmdTmp.CommandTimeout = vTimeoutConexion
        If vTra IsNot Nothing And vCon.State = ConnectionState.Open Then
            cmdTmp.Transaction = vTra
        End If
        If vParaMetros IsNot Nothing Then
            'tenemos argumentos que preparar para el sp.
            For Each vClave As String In vParaMetros.Keys
                cmdTmp.Parameters.AddWithValue(vClave.ToUpper(), vParaMetros(vClave))
            Next
        End If
        Try
            Dim dtd As SqlDataAdapter = New SqlDataAdapter(cmdTmp)
            dtd.Fill(ExecSP_DS)
        Catch sqlEx As SqlException
            If sqlEx.Number = 50000 And sqlEx.Class = 16 Then
                Throw sqlEx
            Else
                Throw New ProcedimientoException("No se pudo realizar el proceso. Ha fallado la ejecución del procedimiento almacenado.", sqlEx, vProcAlmacenado, , vStrCon)
            End If
        Catch ex As Exception
            Throw New ProcedimientoException("No se pudo realizar el proceso. Ha fallado la ejecución del procedimiento almacenado.", ex, vProcAlmacenado, vParaMetros, vStrCon)
        End Try
    End Function


    ''' <summary>
    ''' Ejecuta un sp 
    ''' </summary>
    ''' <param name="vProcAlmacenado"></param>
    ''' <param name="vParaMetros"></param>
    ''' <param name="vTimeoutConexion"></param>
    ''' <remarks></remarks>
    Public Sub Exec(ByVal vProcAlmacenado As String, _
                           Optional ByVal vParaMetros As Dictionary(Of String, Object) = Nothing, _
                           Optional ByVal vTimeoutConexion As Integer = TimeoutConexion)
        Dim cmdTmp As SqlCommand


        cmdTmp = New SqlClient.SqlCommand(vProcAlmacenado)
        cmdTmp.CommandType = CommandType.StoredProcedure
        cmdTmp.Connection = vCon
        cmdTmp.CommandTimeout = vTimeoutConexion
        If vTra IsNot Nothing And vCon.State = ConnectionState.Open Then
            cmdTmp.Transaction = vTra
        End If
        If vParaMetros IsNot Nothing Then
            'tenemos argumentos que preparar para el sp.
            For Each vClave As String In vParaMetros.Keys
                cmdTmp.Parameters.AddWithValue(vClave, vParaMetros(vClave))
            Next
        End If
        Try
            If vCon.State <> ConnectionState.Open Then
                cmdTmp.Connection.Open()
            End If
            cmdTmp.ExecuteNonQuery()
        Catch ex As SqlException
            If ex.Number = 50000 And ex.Class = 16 Then
                Throw ex
            Else
                Throw New ProcedimientoException("No se pudo realizar el proceso. Ha fallado la ejecución del procedimiento almacenado.", ex, vProcAlmacenado, , vStrCon)
            End If
        Catch ex As Exception
            Throw New ProcedimientoException("No se pudo realizar el proceso. Ha fallado la ejecución del procedimiento almacenado.", ex, vProcAlmacenado, vParaMetros, vStrCon)
        Finally
            If cmdTmp.Transaction Is Nothing Then
                cmdTmp.Connection.Close()
                cmdTmp.Connection.Dispose()
            End If
        End Try
    End Sub

    ''' <summary>
    ''' Ejecuta un sp que contiene parametros OUTPUT
    ''' </summary>
    ''' <param name="vProcAlmacenado">nombre sp</param>
    ''' <param name="vParaMetrosIn">parametros de entrada</param>
    ''' <param name="vParaMetrosOut">parametros de salida</param>
    ''' <param name="result">datatable donde se almacenaran los resultados de tabla</param>
    ''' <param name="vTimeoutConexion">tiempo maximo de ejecucion</param>
    ''' <returns>coleccion de parametros</returns>
    ''' <remarks></remarks>
    Public Function ExecSPOut(ByVal vProcAlmacenado As String, _
                         Optional ByVal vParaMetrosIn As Dictionary(Of String, Object) = Nothing, _
                         Optional ByVal vParaMetrosOut As Dictionary(Of String, SqlDbType) = Nothing, _
                         Optional result As DataTable = Nothing, _
                         Optional ByVal vTimeoutConexion As Integer = TimeoutConexion) As SqlParameterCollection
        Dim cmdTmp As SqlCommand


        cmdTmp = New SqlClient.SqlCommand(vProcAlmacenado)
        cmdTmp.CommandType = CommandType.StoredProcedure
        cmdTmp.Connection = vCon
        cmdTmp.CommandTimeout = vTimeoutConexion
        If vTra IsNot Nothing And vCon.State = ConnectionState.Open Then
            cmdTmp.Transaction = vTra
        End If
        If vParaMetrosIn IsNot Nothing Then
            'tenemos argumentos que preparar para el sp.
            For Each vClave As String In vParaMetrosIn.Keys
                cmdTmp.Parameters.AddWithValue(vClave.ToUpper(), vParaMetrosIn(vClave))
            Next
        End If

        If vParaMetrosOut IsNot Nothing Then
            ''parametros de salida?
            For Each vClave As String In vParaMetrosOut.Keys
                If vParaMetrosOut(vClave) = SqlDbType.VarChar Then
                    cmdTmp.Parameters.Add(vClave, vParaMetrosOut(vClave), 8000).Direction = ParameterDirection.Output
                Else
                    cmdTmp.Parameters.Add(vClave, vParaMetrosOut(vClave)).Direction = ParameterDirection.Output
                End If

            Next

        End If
        Try
            If result Is Nothing Then
                If vCon.State <> ConnectionState.Open Then
                    cmdTmp.Connection.Open()
                End If
                cmdTmp.ExecuteNonQuery()
            Else
                Dim dtd As SqlDataAdapter = New SqlDataAdapter(cmdTmp)
                dtd.Fill(result)
            End If


            Return cmdTmp.Parameters
        Catch ex As SqlException
            If ex.Number = 1205 Then
                Throw ex
            ElseIf ex.Number = 50000 And ex.Class = 16 Then
                Throw ex
            Else
                Throw New ProcedimientoException("No se pudo realizar el proceso. Ha fallado la ejecución del procedimiento almacenado.", ex, vProcAlmacenado, vParaMetrosIn, vStrCon)
            End If
        Catch ex As Exception
            Throw New ProcedimientoException("No se pudo realizar el proceso. Ha fallado la ejecución del procedimiento almacenado.", ex, vProcAlmacenado, vParaMetrosIn, vStrCon)
        Finally
            If cmdTmp.Transaction Is Nothing Then
                cmdTmp.Connection.Close()
                cmdTmp.Connection.Dispose()
            End If
        End Try
    End Function

    Public Sub Confirmar()
        If vCon.State = ConnectionState.Closed Then
            Throw New Exception("La conexión está cerrada, No se puede confirmar la transacción.")
        Else
            vTra.Commit()
            vCon.Close()
        End If
    End Sub

    Public Sub Deshacer()
        If vCon.State = ConnectionState.Closed Then
            Throw New Exception("La conexión está cerrada, No se puede deshacer la transacción.")
        Else
            vTra.Rollback()
            vCon.Close()
        End If
    End Sub

    Public Sub IniciarTransaccion()
        If vCon.State = ConnectionState.Open And vTra IsNot Nothing Then
            Throw New Exception("La conexión ya esta establecida y la transaccion inicada. Cierre la transaccion actual con el metodo Deshacer o con el metodo Confirmar.")
        Else
            vCon.Open()
            vTra = vCon.BeginTransaction()
        End If
    End Sub

    Protected Overloads Overrides Sub Finalize()
        If vCon Is Nothing Then Return
        If vCon.State = ConnectionState.Open Then
            If vTra IsNot Nothing Then
                Try
                    vTra.Rollback()
                    vTra.Dispose()
                Catch ex As Exception

                End Try
            End If
            Try
                vCon.Close()
                vCon.Dispose()
            Catch ex As Exception

            End Try
        End If
    End Sub

    Public ReadOnly Property Estado() As Data.ConnectionState
        Get
            Estado = vCon.State
        End Get
    End Property

End Class

