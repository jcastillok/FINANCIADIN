Public Class DetallePagos

    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then

        End If
    End Sub

    Private Sub limpiarCampos()
        cbClientes.SelectedIndex = -1
        lblAdeudo.Text = ""
        gvPagos.Visible = False
    End Sub

    Protected Sub gvPagos_HtmlRowPrepared(sender As Object, e As DevExpress.Web.ASPxGridViewTableRowEventArgs) Handles gvPagos.HtmlRowPrepared
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

        If IsNothing(gvPagos.DataSource) = False Then
            Dim historial As List(Of clsPago) = Session("Historial")
            Dim pago = historial.Find(Function(clsPago) clsPago.Id = e.GetValue("id"))

            If pago.Atrasado And (pago.Condonado = False Or IsNothing(pago.Condonado)) Then
                e.Row.BackColor = System.Drawing.Color.Gold
            ElseIf pago.Condonado Then
                e.Row.BackColor = System.Drawing.Color.Orange
            ElseIf pago.EsGarantia Or pago.EsAbonoCapital Then
                e.Row.BackColor = System.Drawing.Color.DodgerBlue
            ElseIf pago.EsAbonoCapital Then
                e.Row.BackColor = System.Drawing.Color.DodgerBlue
            Else
                e.Row.BackColor = System.Drawing.Color.LightGreen
            End If
        End If

    End Sub

    Protected Sub gvPagos_HtmlRowCreated(sender As Object, e As DevExpress.Web.ASPxGridViewTableRowEventArgs) Handles gvPagos.HtmlRowCreated
        If IsDBNull(e.GetValue("FechaPago")) Then
            e.Row.Visible = False
        End If
    End Sub

    Protected Sub gvPagos_DataBound(sender As Object, e As EventArgs) Handles gvPagos.DataBound

        gvPagos.Columns(0).CellStyle.HorizontalAlign = HorizontalAlign.Center
        gvPagos.Columns(1).CellStyle.HorizontalAlign = HorizontalAlign.Center
        gvPagos.Columns(2).CellStyle.HorizontalAlign = HorizontalAlign.Center
        TryCast(gvPagos.Columns(3), DevExpress.Web.GridViewDataTextColumn).PropertiesTextEdit.DisplayFormatString = "c"
        TryCast(gvPagos.Columns(4), DevExpress.Web.GridViewDataTextColumn).PropertiesTextEdit.DisplayFormatString = "c"
        TryCast(gvPagos.Columns(5), DevExpress.Web.GridViewDataTextColumn).PropertiesTextEdit.DisplayFormatString = "c"
        TryCast(gvPagos.Columns(6), DevExpress.Web.GridViewDataTextColumn).PropertiesTextEdit.DisplayFormatString = "c"
        TryCast(gvPagos.Columns(7), DevExpress.Web.GridViewDataTextColumn).PropertiesTextEdit.DisplayFormatString = "c"
        TryCast(gvPagos.Columns(8), DevExpress.Web.GridViewDataTextColumn).PropertiesTextEdit.DisplayFormatString = "c"
        TryCast(gvPagos.Columns(9), DevExpress.Web.GridViewDataTextColumn).PropertiesTextEdit.DisplayFormatString = "c"
        TryCast(gvPagos.Columns(10), DevExpress.Web.GridViewDataTextColumn).PropertiesTextEdit.DisplayFormatString = "c"

    End Sub

    Protected Sub gvPagos_SelectionChanged(sender As Object, e As EventArgs) Handles gvPagos.SelectionChanged
        If gvPagos.Selection.Count > 0 Then
            Dim id = gvPagos.GetSelectedFieldValues("id").Item(0)
            cargarGvPagos()
            Session("Pago") = clsPago.obtener(id)
        Else
            Session.Remove("Pago")
        End If
    End Sub

    Protected Sub ASPxButton1_Click(sender As Object, e As EventArgs) Handles ASPxButton1.Click
        limpiarCampos()
    End Sub

    Protected Sub grdCatalogo_RowDataBound(sender As Object, e As EventArgs) Handles gvPagos.DataBound
        gvPagos.Columns.Item(11).Visible = False
    End Sub

    Protected Sub cbCreditos_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cbCreditos.SelectedIndexChanged
        cbClientes.Value = cbCreditos.Value
        cargarDatos()
    End Sub

    Protected Sub cbClientes_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cbClientes.SelectedIndexChanged
        If cbClientes.SelectedIndex <> -1 Then cbCreditos.SelectedItem = cbCreditos.Items.FindByValue(cbClientes.Value)
        cargarDatos()
    End Sub

    Protected Sub btnReImpTckt_Click(sender As Object, e As EventArgs) Handles btnReImpTckt.Click
        If IsNothing(Session("Pago")) = False Then
            reImprimirTicket()
            'rlimpiarCampos(False)
            cargarGvPagos()
        Else
            Utilerias.MensajeAlerta("Debe elegir un pago para realizar la reimpresión!", Me, True)
        End If
    End Sub

    Private Sub cargarDatos()

        If cbClientes.SelectedIndex <> -1 And cbCreditos.SelectedIndex <> -1 Then

            Dim credito = clsCredito.obtener(cbClientes.SelectedItem.Value)
            Dim historial = clsPago.obtenerHistorial(credito.Id)
            If historial.Count = 0 Then Return
            Dim ultimoPago = clsPago.obtener(credito.Id, , True, True)
            Session("credito") = credito
            Session("Historial") = historial

            Dim tablaAmort = Utilerias.obtenerTablaPagados(credito, historial)

            gvPagos.DataSource = tablaAmort
            gvPagos.DataBind()
            gvPagos.Visible = True
            Session("TablaAmort") = tablaAmort
            Dim abonos = historial.Sum(Function(clsPago) clsPago.Monto)

            If credito.Liquidado Then
                lblAdeudo.Text = String.Format("{0:0.00}", 0)
            ElseIf IsNothing(ultimoPago) = False AndAlso System.DateTime.Now.Date > ultimoPago.FecEsperada Then
                'Dim TotalEsperado = Math.Ceiling((credito.Adeudo / credito.NumPagos) * ultimoPago.NumPago)
                'Dim diferencia = TotalEsperado - abonos
                'Dim Adeudo = (credito.Adeudo / credito.NumPagos) + diferencia + calcularRecargo(credito, System.DateTime.Now.Date, ultimoPago.FecProxPago)
                Dim recargo = calcularRecargo(credito, System.DateTime.Now.Date, ultimoPago.FecProxPago)
                Dim Adeudo = tablaAmort.Rows.Item(tablaAmort.Rows.Count - 1).Item(3) + recargo
                lblAdeudo.Text = String.Format("{0:0.00}", Adeudo)
                lblRecargo.Text = String.Format("{0:0.00}", recargo)
            ElseIf IsNothing(ultimoPago) = False Then
                Dim totalAbonadoLetra = Utilerias.getDataTable("SELECT SUM(monto) FROM PAGOS WHERE Id_CREDITO = " & ultimoPago.Id_Credito & " AND NumPago = " & ultimoPago.NumPago & " AND CANCELADO = 0 GROUP BY MONTO").Rows.Item(0).Item(0)
                lblAdeudo.Text = String.Format("{0:0.00}", If(abonos - totalAbonadoLetra < 0, 0, abonos - totalAbonadoLetra))
            End If

        End If

    End Sub

    Protected Sub cargarGvPagos()

        gvPagos.DataSource = Session("TablaAmort")
        gvPagos.DataBind()
        gvPagos.Visible = True

    End Sub

    Private Function calcularRecargo(credito As clsCredito, fechaPago As DateTime, fechaEsperada As DateTime) As Double

        Dim numPagosAtrasados As Integer
        If fechaPago > fechaEsperada Then
            While fechaPago > fechaEsperada
                numPagosAtrasados = numPagosAtrasados + 1
                fechaEsperada = fechaEsperada.AddDays(1)
            End While
            Dim recargo = (credito.MontoPrestado * (credito.TasaMoratoria / 100)) * numPagosAtrasados
            recargo = recargo + (recargo * 0.16)
            recargo = Math.Round(recargo, 2)
            Utilerias.MensajeAlerta(String.Format("Este pago tiene un atraso de {0} días y generá un interes adicional de ${1}", numPagosAtrasados, String.Format("{0:0.00}", recargo)), Me, True)
            Session("Recargo") = recargo
            Return recargo
        Else
            Session("Recargo") = 0
            Return 0
        End If

    End Function

    Private Sub reImprimirTicket()
        Try
            Dim pago As clsPago = Session("Pago")

            Dim movto = clsMovto.obtener(pago.Id_Mov)

            'If (tipoGar = 3 And pago.Monto > 0) Or (tipoGar <> 3) Then

            'Dim direccion = Utilerias.getDataTable("SELECT ('Calle ' + CLIENTES.Calle + ' #' +  CLIENTES.NumExt + ' ' + CLIENTES.Colonia + ', ' + CLIENTES.Localidad + ', ' + CLIENTES.Estado) AS Direccion FROM CLIENTES WHERE ID = " & pago.Id_Cliente).Rows.Item(0).Item(0)
            Dim telefono = Utilerias.getDataTable("SELECT celular FROM CLIENTES WHERE ID = " & pago.Id_Cliente).Rows.Item(0).Item(0)
            Dim cliente = Utilerias.getDataTable("SELECT (PrimNombre + ' ' + SegNombre + ' ' + PrimApellido + ' ' + SegApellido) AS cliente FROM CLIENTES WHERE ID = " & pago.Id_Cliente).Rows.Item(0).Item(0)
            Dim sucursal = Utilerias.getDataTable("SELECT Descripcion FROM CATSUCURSALES WHERE ID = " & pago.Id_Sucursal).Rows.Item(0).Item(0)
            Dim dirsuc = Utilerias.getDataTable("SELECT Direccion FROM CATSUCURSALES WHERE ID = " & pago.Id_Sucursal).Rows.Item(0).Item(0)

            Dim TipoTransac = If(pago.EsGarantia, "Pago de Garantía - Reimpresión", If(pago.EsAbonoCapital, "Pago a Capital - Reimpresión", "Pago - Reimpresión"))
            Dim crReporteDocumento As New crTicketCompra

            crReporteDocumento.SetParameterValue("folmov", movto.Id)
            crReporteDocumento.SetParameterValue("credito", movto.Id_Credito)
            crReporteDocumento.SetParameterValue("cliente", cliente)
            crReporteDocumento.SetParameterValue("telefono", telefono)
            crReporteDocumento.SetParameterValue("proxPago", If(movto.ProxPago = 0, 0, movto.ProxPago))
            crReporteDocumento.SetParameterValue("fecProxPago", If(IsNothing(movto.FecProxPago), "", movto.FecProxPago.Value.ToShortDateString))
            crReporteDocumento.SetParameterValue("tipoTicket", TipoTransac)
            crReporteDocumento.SetParameterValue("total", movto.Pago + movto.Moratorio)
            crReporteDocumento.SetParameterValue("sucursal", sucursal)
            crReporteDocumento.SetParameterValue("dirsuc", dirsuc)
            crReporteDocumento.SetParameterValue("usuario", movto.Login)
            crReporteDocumento.SetParameterValue("fechaPago", movto.FecMov)

            Dim fileName = "Comprobante" & movto.Id & ".PDF"
            Dim file As String = System.IO.Path.GetTempPath & fileName
            Session("filename") = file
            Dim diskOpts As New CrystalDecisions.Shared.DiskFileDestinationOptions
            diskOpts.DiskFileName = file
            crReporteDocumento.ExportOptions.DestinationOptions = diskOpts
            crReporteDocumento.ExportOptions.ExportFormatType = CrystalDecisions.Shared.ExportFormatType.PortableDocFormat
            crReporteDocumento.ExportOptions.ExportDestinationType = CrystalDecisions.Shared.ExportDestinationType.DiskFile
            crReporteDocumento.Export()
            crReporteDocumento.Close()
            crReporteDocumento.Dispose()
            GC.Collect()

            Context.Response.Write("<script language='javascript'>window.open('../Visor.aspx','_blank');</script>")

            Utilerias.MensajeConfirmacion("Se ha reimpreso el ticket!", Me, True)
        Catch ex As Exception
            Utilerias.MensajeAlerta("Hubo un poblema al reimprimir el ticket de pago!", Me, True)
        End Try
        cargarGvPagos()
    End Sub

    Protected Sub btnTablaAmort_Click(sender As Object, e As EventArgs) Handles btnTablaAmort.Click

        If cbCreditos.SelectedIndex = -1 Then Utilerias.MensajeAlerta("Primero debe seleccionar un crédito.",Me.Page, True) : Return

        Dim strSolicitud = String.Format("SELECT * FROM SOLICITUDES WHERE Id = {0}", Session("credito").Id_Sol)

        Dim strCliente = String.Format("SELECT Id, PrimNombre, SegNombre, PrimApellido, SegApellido, Login, Tipo, RFC, CURP, Calle, NumExt, 
                                        NumInt, Colonia, Localidad, Referencia, Pais, Estado, Municipio, CodPostal, FecNac, Telefono, EMail, 
                                        Estatus, FecRegistro, Celular, Nacionalidad, EstadoCivil, Ocupacion, IngresoMensual, EsAval, 
                                        (SELECT Id_Aval FROM SOLICITUDES WHERE Id ={0}) As Id_Aval, Identificacion, ComprobanteDom, SolBuro 
                                        FROM CLIENTES WHERE Id = (SELECT Id_Cliente FROM SOLICITUDES WHERE Id={0})", Session("credito").Id_Sol)

        Dim tasaRef = Utilerias.getDataTable("SELECT Descripcion FROM CATTASAINTERES WHERE Id = " & Session("credito").TasaReferencia)

        Dim datos As New DataSet
        Dim dt = Utilerias.getDataTable(strSolicitud)
        Dim dt2 = Utilerias.getDataTable(strCliente)
        Dim dt3 = Utilerias.crearTablaDePagos(Session("credito").FecInicio, Session("credito").MontoPrestado, Session("credito").Adeudo, Session("credito").numPagos, Session("credito").Plazo)

        datos.Tables.Add(dt)
        datos.Tables(0).TableName = "SOLICITUDES"
        datos.Merge(dt)
        datos.Tables.Add(dt2)
        datos.Tables(1).TableName = "CLIENTES"
        datos.Merge(dt2)
        datos.Tables.Add(dt3)
        datos.Tables(2).TableName = "TABLADEAMORTIZACION"
        datos.Merge(dt3)
        Session("Tabla") = datos

        Dim parametros As String = String.Format("{0}:={1}:={2}:={3}:={4}:={5}:={6}:={7}:={8}", Session("credito").Adeudo, " ", " ", Session("credito").FecUltPago, tasaRef.Rows.Item(0).Item(0), " ", " ", Session("credito").Sobretasa, Session("credito").Id)
        Session("parametros") = parametros

        If IsNothing(Session("Tabla")) = False And IsNothing(Session("parametros")) = False Then
                    Context.Response.Write("<script language='javascript'>window.open('../Prestamos/dspVisor/dspTablaAmort.aspx','_blank');</script>")
        End If

    End Sub

End Class