Public Class Asignacion2
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        grdDetalle.DataBind()
        Aspxgridview1.DataBind()
        popUpTable.DataBind()
        grdDetalle4.DataBind()

    End Sub

    Public Property UseFixedTableLayout As Boolean

    Function GetTable() As DataTable
        ' Create new DataTable instance.

        Dim table As New DataTable
        Dim i As Double

        ' Create four typed columns in the DataTable.
        'table.Columns.Add("id", GetType(Integer))
        table.Columns.Add("chkbox", GetType(Integer))
        table.Columns.Add("nombre", GetType(String))
        table.Columns.Add("rut", GetType(String))
        table.Columns.Add("estado", GetType(String))
        table.Columns.Add("especialidad", GetType(String))
        table.Columns.Add("cargo", GetType(String))
        table.Columns.Add("supervisor", GetType(String))
        For i = 1 To 100
            ' Add five rows with those columns filled in the DataTable.
            table.Rows.Add(0, "Juan Pérez" & Str(i), "15.311.232-1", "No Asignado", "Gasfiter", "Maestro", "José González")
        Next i
        Return table

    End Function

    Function GetTable2() As DataTable


        Dim table2 As New DataTable
        Dim j As Double

        ' Create four typed columns in the DataTable.
        table2.Columns.Add("id", GetType(Integer))

        table2.Columns.Add("cod", GetType(String))
        table2.Columns.Add("partida", GetType(String))
        table2.Columns.Add("um", GetType(String))
        table2.Columns.Add("qavance", GetType(String))

        table2.Columns.Add("chkbox1", GetType(Integer))
        table2.Columns.Add("chkbox2", GetType(Integer))
        table2.Columns.Add("chkbox3", GetType(Integer))
        table2.Columns.Add("chkbox4", GetType(Integer))
        table2.Columns.Add("chkbox5", GetType(Integer))
        table2.Columns.Add("chkbox6", GetType(Integer))
        table2.Columns.Add("chkbox7", GetType(Integer))

        table2.Columns.Add("chkbox8", GetType(Integer))
        table2.Columns.Add("chkbox9", GetType(Integer))
        table2.Columns.Add("chkbox10", GetType(Integer))
        table2.Columns.Add("chkbox11", GetType(Integer))
        table2.Columns.Add("chkbox12", GetType(Integer))
        table2.Columns.Add("chkbox13", GetType(Integer))
        table2.Columns.Add("chkbox14", GetType(Integer))


        table2.Columns.Add("totalavance", GetType(Integer))
        For j = 1 To 100
            ' Add five rows with those columns filled in the DataTable.
            table2.Rows.Add(j, "001", "Texto de Prueba", "m3", "99999", 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 9999)
        Next j
        Return table2



    End Function


    Function GetTable3() As DataTable


        Dim table3 As New DataTable
        Dim k As Double

        ' Create four typed columns in the DataTable.
        'table.Columns.Add("id", GetType(Integer))
        table3.Columns.Add("chk2", GetType(Integer))
        table3.Columns.Add("cod2", GetType(String))
        table3.Columns.Add("partida2", GetType(String))
        table3.Columns.Add("um2", GetType(String))
        table3.Columns.Add("qavance2", GetType(String))


        For k = 1 To 100
            ' Add five rows with those columns filled in the DataTable.
            table3.Rows.Add(0, "001", "Texto de Prueba2", "m3", "99999")
        Next k
        Return table3



    End Function

    Function GetTable4() As DataTable
        ' Create new DataTable instance.

        Dim table4 As New DataTable
        Dim v As Double

        ' Create four typed columns in the DataTable.
        'table.Columns.Add("id", GetType(Integer))
        table4.Columns.Add("chkbox4", GetType(Integer))
        table4.Columns.Add("nombre4", GetType(String))
        table4.Columns.Add("rut4", GetType(String))
        table4.Columns.Add("estado4", GetType(String))
        table4.Columns.Add("especialidad4", GetType(String))
        table4.Columns.Add("cargo4", GetType(String))
        table4.Columns.Add("supervisor4", GetType(String))
        For v = 1 To 100
            ' Add five rows with those columns filled in the DataTable.
            table4.Rows.Add(0, "Juan Pérez" & Str(v), "15.311.232-1", "No Asignado", "Gasfiter", "Maestro", "José González")
        Next v
        Return table4

    End Function

    Private Sub grdAvance_DataBinding(sender As Object, e As System.EventArgs) Handles grdDetalle.DataBinding
        grdDetalle.DataSource = GetTable()
        Aspxgridview1.DataSource = GetTable2()
        popUpTable.DataSource = GetTable3()
        grdDetalle4.DataSource = GetTable4()
    End Sub

    Public Property PageSize As Integer

    'Protected Sub ASPxGridView1_SelectionChanged(ByVal sender As Object, ByVal e As EventArgs)
    '    'this updates the custom summary item
    '    grdDetalle.DataBind()
    'End Sub
End Class