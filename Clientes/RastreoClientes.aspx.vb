Public Class RastreoClientes

    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        If Not Page.IsPostBack Then

        End If

        'If Not IsNothing(Session("Historial")) Then
        '    gvHistorial.DataSource = Session("Historial")
        '    gvHistorial.DataBind()
        'End If

    End Sub

    Private Sub limpiarCampos()
        cbClientes.SelectedIndex = -1
        gvHistorial.Visible = False
        Session.Remove("Historial")
    End Sub

    Protected Sub gvPagos_HtmlRowPrepared(sender As Object, e As DevExpress.Web.ASPxGridViewTableRowEventArgs) Handles gvHistorial.HtmlRowPrepared
        'Dim historial = clsPago.obtenerHistorial(Session("credito").Id)

        'If IsNothing(historial.Find(Function(clsPago) clsPago.NumPago = e.GetValue("#Pago"))) = False Then
        '    If historial.Find(Function(clsPago) clsPago.NumPago = e.GetValue("#Pago")).Atrasado = True Then
        '        e.Row.BackColor = System.Drawing.Color.Gold
        '    ElseIf historial.Find(Function(clsPago) clsPago.NumPago = e.GetValue("#Pago")).EsGarantia = True Then
        '        e.Row.BackColor = System.Drawing.Color.DodgerBlue
        '    ElseIf historial.Find(Function(clsPago) clsPago.NumPago = e.GetValue("#Pago")).EsAbonoCapital = True Then
        '        e.Row.BackColor = System.Drawing.Color.DodgerBlue
        '    Else
        '        e.Row.BackColor = System.Drawing.Color.OliveDrab
        '    End If
        'End If

        If IsNothing(gvHistorial.DataSource) = False Then
            Dim Liquidado = e.GetValue("Liquidado")

            If Liquidado Then
                e.Row.BackColor = System.Drawing.Color.LightGreen
            ElseIf Not Liquidado Then
                e.Row.BackColor = System.Drawing.Color.Red
            End If
        End If

    End Sub

    Protected Sub gvPagos_DataBound(sender As Object, e As EventArgs) Handles gvHistorial.DataBound

        gvHistorial.Columns(0).CellStyle.HorizontalAlign = HorizontalAlign.Center
        gvHistorial.Columns(1).CellStyle.HorizontalAlign = HorizontalAlign.Center
        gvHistorial.Columns(2).CellStyle.HorizontalAlign = HorizontalAlign.Center
        gvHistorial.Columns(3).CellStyle.HorizontalAlign = HorizontalAlign.Center
        TryCast(gvHistorial.Columns(3), DevExpress.Web.GridViewDataTextColumn).PropertiesTextEdit.DisplayFormatString = "c"
        gvHistorial.Columns(4).CellStyle.HorizontalAlign = HorizontalAlign.Center
        TryCast(gvHistorial.Columns(4), DevExpress.Web.GridViewDataTextColumn).PropertiesTextEdit.DisplayFormatString = "c"
        gvHistorial.Columns(5).CellStyle.HorizontalAlign = HorizontalAlign.Center
        TryCast(gvHistorial.Columns(5), DevExpress.Web.GridViewDataTextColumn).PropertiesTextEdit.DisplayFormatString = "c"
        gvHistorial.Columns(6).CellStyle.HorizontalAlign = HorizontalAlign.Center
        TryCast(gvHistorial.Columns(6), DevExpress.Web.GridViewDataTextColumn).PropertiesTextEdit.DisplayFormatString = "c"
        gvHistorial.Columns(7).CellStyle.HorizontalAlign = HorizontalAlign.Center
        TryCast(gvHistorial.Columns(7), DevExpress.Web.GridViewDataTextColumn).PropertiesTextEdit.DisplayFormatString = "c"
        gvHistorial.Columns(8).CellStyle.HorizontalAlign = HorizontalAlign.Center
        'TryCast(gvHistorial.Columns(8), DevExpress.Web.GridViewDataTextColumn).PropertiesTextEdit.DisplayFormatString = "c"
        gvHistorial.Columns(9).CellStyle.HorizontalAlign = HorizontalAlign.Center
        TryCast(gvHistorial.Columns(9), DevExpress.Web.GridViewDataTextColumn).PropertiesTextEdit.DisplayFormatString = "c"

    End Sub

    Protected Sub ASPxButton1_Click(sender As Object, e As EventArgs) Handles ASPxButton1.Click
        limpiarCampos()
    End Sub

    Protected Sub grdHistorial_RowDataBound(sender As Object, e As EventArgs) Handles gvHistorial.DataBound
        gvHistorial.Columns.Item(10).Visible = False
    End Sub

    Protected Sub cbClientes_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cbClientes.SelectedIndexChanged

        If cbClientes.SelectedIndex <> -1 Then
            Dim slctCli = "SELECT   C.Id, REPLACE(CLI.PrimNombre + ' ' + IsNull(CLI.SegNombre,'') + ' ' + CLI.PrimApellido + ' ' + IsNull(CLI.SegApellido,''), '  ', ' ') AS Cliente, 
		                        REPLACE(CLI2.PrimNombre + ' ' + CLI2.SegNombre + ' ' + CLI2.PrimApellido + ' ' + CLI2.SegApellido, '  ', ' ') AS Aval, convert(varchar,C.FecInicio,103) As Inicio, 
		                        convert(varchar,C.FecPrimPago,103) AS PrimerPago, convert(varchar,C.FecUltPago,103) AS UltimoPago, C.MontoPrestado, C.Adeudo, C.NumPagos, CP.Descripcion, C.Liquidado
                       FROM     CREDITOS AS C INNER JOIN
                                CLIENTES AS CLI ON C.Id_Cliente = CLI.Id INNER JOIN
                                CLIENTES AS CLI2 ON C.Id_Aval = CLI2.Id INNER JOIN
                                CATPLAZOS AS CP ON C.Plazo = CP.Id
                       WHERE    C.estatusID = 1 AND C.Id_Cliente = " & cbClientes.Value & " OR C.Id_Aval = " & cbClientes.Value & "
                       ORDER BY C.Id, C.FecInicio"

            Session("Historial") = Utilerias.getDataTable(slctCli)
            cargarGvPagos()
        End If

    End Sub

    Protected Sub cargarGvPagos()

        gvHistorial.DataSource = Session("Historial")
        gvHistorial.DataBind()
        gvHistorial.Visible = True

    End Sub

End Class