Public Class CapturaPagosSuc

    Inherits System.Web.UI.Page

    Private Property credito As clsCredito
    Private Property historial As List(Of clsPago)

#Region "CONTROLES Y EVENTOS"
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Me.IsPostBack = False Then
            divTabla.Visible = False
            btnGuardar.Attributes.Add("onclick", "this.disabled=true;")

            ' Recuperar el usuario para determinar el tipo
            Dim clUsuario As clsUsuario
            clUsuario = DirectCast(Session("Usuario"), clsUsuario)
            btnGuardar.Enabled = True
            ' si es tipo 2 analista se le bloquea el cobro
            If clUsuario.Tipo = 2 Then
                btnGuardar.Enabled = False
            End If

        End If
    End Sub

    Protected Sub gvPagos_DataBound(sender As Object, e As EventArgs) Handles gvPagos.DataBound
        If gvPagos.VisibleRowCount > 0 Then
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
            TryCast(gvPagos.Columns(12), DevExpress.Web.GridViewDataTextColumn).CellStyle.HorizontalAlign = HorizontalAlign.Center
        End If

    End Sub

    Protected Sub gvPagos_RowDataBound(sender As Object, e As EventArgs) Handles gvPagos.DataBound
        If IsNothing(gvPagos.DataSource) = False Then gvPagos.Columns.Item(11).Visible = False
    End Sub

    Protected Sub gvPagos_HtmlRowPrepared(sender As Object, e As DevExpress.Web.ASPxGridViewTableRowEventArgs) Handles gvPagos.HtmlRowPrepared
        'If cbIdCredito.Value <> "" Then
        If IsNothing(gvPagos.DataSource) = False Then
            Dim historial As List(Of clsPago) = Session("Historial")
            Dim pago = If(IsDBNull(e.GetValue("id")), Nothing, historial.Find(Function(clsPago) clsPago.Id = e.GetValue("id")))
            If IsNothing(pago) = False Then
                If pago.Atrasado And (pago.Condonado = False Or IsNothing(pago.Condonado)) Then
                    e.Row.BackColor = System.Drawing.Color.Gold
                ElseIf pago.Condonado Then
                    e.Row.BackColor = System.Drawing.Color.Orange
                ElseIf pago.EsGarantia Then
                    e.Row.BackColor = System.Drawing.Color.DodgerBlue
                ElseIf pago.EsAbonoCapital Then
                    e.Row.BackColor = System.Drawing.Color.DodgerBlue
                Else
                    e.Row.BackColor = System.Drawing.Color.LightGreen
                End If
            End If
        End If

    End Sub

    Protected Sub gvPagos_SelectionChanged(sender As Object, e As EventArgs) Handles gvPagos.SelectionChanged
        If gvPagos.Selection.Count > 0 Then
            Dim id = gvPagos.GetSelectedFieldValues("id").Item(0)
            'If clsPago.obtener(cbIdCredito.Text,, True, True, False).Id = id Then
            '    Dim numPago = gvPagos.GetSelectedFieldValues("#Pago").Item(0)
            '    Dim monto = gvPagos.GetSelectedFieldValues("TotalPago").Item(0)
            '    Dim pago = clsPago.obtener(id)

            '    Session("Pago") = pago
            '    cbAbonoCap.Checked = pago.EsAbonoCapital
            '    cbCondonacion.Checked = If(IsNothing(pago.Condonado), False, pago.Condonado)
            '    txtMotCond.Value = pago.MotivoCondonacion
            '    lblPago.Text = numPago
            '    txtMonto.Value = monto
            '    Session("Recargo") = pago.Recargo
            '    lblRecargo.Text = pago.Recargo
            '    lblACredito.Text = pago.Monto - pago.Recargo
            '    'deFecDep.Date = pago.FecPago
            '    'deFecDep.ReadOnly = True
            '    cbSuc.SelectedItem = cbSuc.Items.FindByValue(pago.Id_Sucursal)

            '    If pago.Atrasado Then
            '        cbCondonacion.Visible = True
            '        cbCondonacion.Checked = False
            '        lblMotCond.Visible = True
            '        txtMotCond.Visible = True
            '    Else
            '        cbCondonacion.Visible = False
            '        cbCondonacion.Checked = False
            '        lblMotCond.Visible = False
            '        txtMotCond.Visible = False
            '    End If
            '    cargarGvPagos()
            'Else
            'Utilerias.MensajeAlerta("Solo puedo editar el último pago realizado!", Me, True)
            cargarGvPagos()
            Session("Pago") = clsPago.obtener(id)
            'End If
        Else
            Session.Remove("Pago")
            cargarDatos(cbLiquidaBase.Checked, cbLiquida.Checked)
        End If
    End Sub

    Protected Sub cbClientes_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cbClientes.SelectedIndexChanged
        If cbClientes.SelectedIndex <> -1 Then cbIdCredito.SelectedItem = cbIdCredito.Items.FindByValue(cbClientes.Value)
        cargarDatos(cbLiquidaBase.Checked, cbLiquida.Checked, True)
    End Sub

    Protected Sub cbIdCredito_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cbIdCredito.SelectedIndexChanged
        If cbIdCredito.SelectedIndex <> -1 Then cbClientes.SelectedItem = cbClientes.Items.FindByValue(cbIdCredito.Value)
        cargarDatos(cbLiquidaBase.Checked, cbLiquida.Checked, True)
    End Sub

    Protected Sub cbAbonoCap_CheckedChanged(sender As Object, e As EventArgs) Handles cbAbonoCap.CheckedChanged

        If cbAbonoCap.Checked Then
            lblPago.Text = 0
            cbLiquidaBase.Visible = False
            cbLiquidaBase.Checked = False
            cbLiquida.Visible = False
            cbLiquida.Checked = False
        Else
            If cbClientes.SelectedIndex <> -1 Then
                lblPago.Text = Session("PagoActual")
            Else
                lblPago.Text = ""
            End If
            If IsNothing(Session("Faltante")) = False And Session("Recargo") = 0 Then
                cbLiquidaBase.Visible = True
                cbLiquida.Visible = True
            End If
        End If
        cargarGvPagos()

    End Sub

    Protected Sub cbCondonacion_CheckedChanged(sender As Object, e As EventArgs) Handles cbCondonacion.CheckedChanged

        If cbCondonacion.Checked Then
            ModalPopupExtender.Show()
        ElseIf IsNothing(Session("Recargo")) = False And Session("Recargo") > 0 And cbCondonacion.Checked = False Then
            lblRecargo.Text = String.Format("{0:0.00}", Session("Recargo"))
            lblACredito.Text = String.Format("{0:0.00}", txtMonto.Value - Session("Recargo"))
            lblSuma.Text = String.Format("{0:0.00}", txtMonto.Value)
            txtMotCond.Disabled = True
        End If
        cargarGvPagos()

    End Sub

    Protected Sub cbLiquida_CheckedChanged(sender As Object, e As EventArgs) Handles cbLiquida.CheckedChanged

        If cbLiquida.Checked Then
            txtMonto.Value = Session("Faltante")
            Dim credito As clsCredito = Session("credito")
            Dim j = 0
            Dim tablaAmort = Session("TablaAmort")
            Dim pagosLiquid As New List(Of Double)
            Dim aCapital As Double
            While j < tablaAmort.Rows.Count
                If IsDBNull(tablaAmort.Rows.Item(j).Item(2)) And (tablaAmort.Rows.Item(j).Item(0) <= (credito.NumPagos - 3) Or credito.Plazo = 3) Then
                    pagosLiquid.Add(tablaAmort.Rows.Item(j).Item(3))
                ElseIf IsDBNull(tablaAmort.Rows.Item(j).Item(2)) And (tablaAmort.Rows.Item(j).Item(0) > (credito.NumPagos - 3) And credito.Plazo <> 3) Then
                    aCapital = aCapital + tablaAmort.Rows.Item(j).Item(6)
                End If
                j += 1
            End While
            pagosLiquid.Add(aCapital)
            Session("PagosLiquidacion") = pagosLiquid
            cbAbonoCap.Visible = False
            cbAbonoCap.Checked = False
            cbLiquidaBase.Visible = False
            cbLiquidaBase.Checked = False
        Else
            cbAbonoCap.Visible = True
            lblBase.Visible = False
            lblMas.Visible = False
            cbLiquidaBase.Visible = True
            cbLiquidaBase.Checked = False
        End If
        cargarDatos(cbLiquidaBase.Checked, cbLiquida.Checked)

    End Sub

    Protected Sub cbLiquidaBase_CheckedChanged(sender As Object, e As EventArgs) Handles cbLiquidaBase.CheckedChanged

        If cbLiquidaBase.Checked Then
            txtMonto.Value = Session("Faltante")
            Dim credito As clsCredito = Session("credito")
            Dim j = 0
            Dim tablaAmort = Session("TablaAmort")
            Dim pagosLiquid As New List(Of Double)
            Dim aCapital As Double
            While j < tablaAmort.Rows.Count
                If IsDBNull(tablaAmort.Rows.Item(j).Item(2)) And (tablaAmort.Rows.Item(j).Item(0) <= (credito.NumPagos - 3) Or credito.Plazo = 3) Then
                    pagosLiquid.Add(tablaAmort.Rows.Item(j).Item(3))
                ElseIf IsDBNull(tablaAmort.Rows.Item(j).Item(2)) And (tablaAmort.Rows.Item(j).Item(0) > (credito.NumPagos - 3) And credito.Plazo <> 3) Then
                    aCapital = aCapital + tablaAmort.Rows.Item(j).Item(6)
                End If
                j += 1
            End While
            pagosLiquid.Add(aCapital)
            Session("PagosLiquidacion") = pagosLiquid
            cbAbonoCap.Visible = False
            cbAbonoCap.Checked = False
            cbLiquida.Visible = False
            cbLiquida.Checked = False
        Else
            cbAbonoCap.Visible = True
            lblBase.Visible = False
            lblMas.Visible = False
            cbLiquida.Visible = True
            cbLiquida.Checked = False
        End If
        cargarDatos(cbLiquidaBase.Checked)

    End Sub

    Protected Sub txtMonto_NumberChanged(sender As Object, e As EventArgs) Handles txtMonto.NumberChanged
        Dim recargo = If(IsNothing(Session("condonacionRemota")), If(cbCondonacion.Checked, 0, Session("Recargo")), If(Session("Recargo") < Session("condonacionRemota").Monto, 0, Session("Recargo") - Session("condonacionRemota").Monto))
        If txtMonto.Value < Session("Recargo") And Not cbCondonacion.Checked Then Utilerias.MensajeAlerta("Cuando hay recargo no puede depositarse una cifra inferior al monto del mismo.", Me, True) : txtMonto.Value = Math.Ceiling(Session("Recargo"))
        If txtMonto.Value < recargo And cbCondonacion.Checked Then Utilerias.MensajeAlerta("Cuando hay recargo no puede depositarse una cifra inferior al monto del mismo.", Me, True) : txtMonto.Value = recargo
        If Session("EsPagoGarantia") And txtMonto.Value <> lblACredito.Value Then Utilerias.MensajeAlerta("El pago de garantía debe ser el 10% del monto prestado. Monto Prestado = $" + String.Format("{0:0.00}", lblACredito.Value * 10) + ", Pago de Garantía = $" + String.Format("{0:0.00}", lblACredito.Value) + ".", Me, True) : txtMonto.Value = lblACredito.Value
        lblACredito.Text = String.Format("{0:0.00}", If(Session("EsPagoGarantia"), txtMonto.Value, If(cbLiquidaBase.Checked, txtMonto.Value + lblBase.Text, txtMonto.Value - If(Session("credito").TipoAmort <> 4, lblRecargo.Text, 0))))
        lblSuma.Text = String.Format("{0:0.00}", If(cbLiquidaBase.Checked, txtMonto.Value + lblBase.Text, txtMonto.Value))
        If Not Session("EsPagoGarantia") Then cargarGvPagos()
    End Sub

    Protected Sub btnGuardar_Click(sender As Object, e As EventArgs) Handles btnGuardar.Click

        If IsNothing(Session("ProcesandoPago")) Then

            Session("ProcesandoPago") = True

            Dim camposVacios As String = ""

            If cbClientes.Text.Trim = "" Then camposVacios = camposVacios & "Cliente, "
            If cbSuc.Text.Trim = "" Then camposVacios = camposVacios & "Sucursal, "
            If txtMonto.Text.Trim = "" Then camposVacios = camposVacios & "Monto, "
            If cbCondonacion.Checked And txtMotCond.Value.Trim = "" Then camposVacios = camposVacios & "Motivo Condonación, "
            'If deFecDep.Text.Trim = "" Then camposVacios = camposVacios & "Motivo Condonación, "

            If camposVacios <> "" Then
                camposVacios = camposVacios.Remove(camposVacios.LastIndexOf(","), 2)
                If camposVacios.Contains(",") Then
                    Utilerias.MensajeAlerta(String.Format("Los campos <b>{0}</b> no pueden estar vacios.", camposVacios), Me, True)
                Else
                    Utilerias.MensajeAlerta(String.Format("El campo <b>{0}</b> no puede estar vacío.", camposVacios), Me, True)
                End If
                Return
            End If

            If Session("credito").TipoAmort = 4 Then
                guardarPagoProgresivo()
            Else
                guardarPago()
            End If

        End If

    End Sub

    Protected Sub btnLimpiar_Click(sender As Object, e As EventArgs) Handles btnLimpiar.Click
        limpiarCampos()
    End Sub

    Protected Sub btnCancelar_Click(sender As Object, e As EventArgs) Handles btnCancelar.Click
        cbCondonacion.Checked = False
        txtMotCond.Disabled = True
        txtLogin.Text = ""
        txtContra.Value = ""
        cargarGvPagos()
    End Sub

    Protected Sub btnIngresar_Click(sender As Object, e As EventArgs) Handles btnIngresar.Click
        Dim conx = clsConexion.Open
        Dim cmd As New ADODB.Command
        Dim rst As New ADODB.Recordset
        cmd.ActiveConnection = conx
        Dim clUsuario As clsUsuario = clsUsuario.obtenerUsuario(txtLogin.Value)

        If IsNothing(clUsuario) = False AndAlso ValidaPassword(cmd, rst) AndAlso (clUsuario.Tipo = 1 Or clUsuario.Tipo = 9) Then
            REM validar si es de tipo administrador
            Session("UsuarioAutCond") = clUsuario
            'If IsNothing(Session("Recargo")) = False And Session("Recargo") > 0 Then
            lblRecargo.Text = String.Format("{0:0.00}", 0)
            lblACredito.Text = String.Format("{0:0.00}", txtMonto.Value)
            lblSuma.Text = String.Format("{0:0.00}", txtMonto.Value)
            txtMotCond.Disabled = False
            'End If
            txtLogin.Text = ""
            txtContra.Value = ""
            cbCondonacion.Checked = True
            Utilerias.MensajeConfirmacion("Se ha autorizado la condonación!", Me, True)
        Else
            cbCondonacion.Checked = False
            txtMotCond.Disabled = True
            txtMotCond.InnerText = ""
            txtLogin.Text = ""
            txtContra.Value = ""
            Utilerias.MensajeConfirmacion("Las credenciales que introdujo son incorrectas o no cuentan con los permisos para autorizar esta transacción!", Me, True)
            'cargarDatos()
        End If
        conx.Close()
        cargarGvPagos()
        'If autentificado = True Then
        '    If TipoUsuario = 4 Then
        '        Exit Sub
        '    Else
        '        FormsAuthentication.RedirectFromLoginPage(Login1.UserName, Login1.RememberMeSet)
        '        Response.Redirect("Bienvenida\General.aspx")
        '    End If
        'End If
    End Sub

    Protected Sub btnReImpTckt_Click(sender As Object, e As EventArgs) Handles btnReImpTckt.Click
        If IsNothing(Session("Pago")) = False Then
            reImprimirTicket()
            limpiarCampos(False)
            cargarGvPagos()
        Else
            Utilerias.MensajeAlerta("Debe elegir un pago para realizar la reimpresión!", Me, True)
        End If
    End Sub

    Protected Sub btnTabla_Click(sender As Object, e As EventArgs) Handles btnTabla.Click
        If divTabla.Visible Then
            divTabla.Visible = False
        Else
            divTabla.Visible = True
        End If
        cargarGvPagos()
    End Sub
#End Region

#Region "METODOS DE LA CLASE"
    Protected Sub limpiarCampos(Optional esTotal As Boolean = True)
        If esTotal Then
            cbIdCredito.SelectedIndex = -1
            cbClientes.DataBind()
            cbClientes.SelectedIndex = -1
        End If
        REM Controles base
        lblPago.Text = ""
        gvPagos.Visible = False
        gvPagos.Selection.UnselectAll()
        txtMonto.Text = ""
        cbSuc.SelectedIndex = -1
        cbAbonoCap.Checked = False
        cbAbonoCap.Visible = True
        lblACredito.Text = ""
        lblCuota.Text = "0.00"
        REM Condonacion
        cbCondonacion.Visible = False
        cbCondonacion.Checked = False
        lblMotCond.Visible = False
        txtMotCond.Visible = False
        txtMotCond.Value = ""
        txtMotCond.Disabled = True
        REM Recargo
        lblMas2.Visible = False
        lblRecargo.Visible = False
        lblRecargoSig.Visible = False
        lblTxtRecargo.Visible = False
        'lblRecargo.Text = "--"
        REM Base
        lblMas.Visible = False
        lblBase.Visible = False
        lblTxtBase.Visible = False
        cbLiquidaBase.Visible = False
        cbLiquidaBase.Checked = False
        cbLiquida.Visible = False
        cbLiquida.Checked = False
        REM Otros
        lblSuma.Value = ""
        Session.Remove("Pago")
    End Sub

    Protected Sub cargarGvPagos()

        gvPagos.DataSource = Session("TablaAmort")
        gvPagos.DataBind()
        gvPagos.Visible = True

    End Sub

    Private Sub cargarDatos(Optional LiqBase As Boolean = False, Optional Liq As Boolean = False, Optional EsTotal As Boolean = False)
        Dim usuario As clsUsuario = Nothing
        usuario = DirectCast(Session("Usuario"), clsUsuario)

        If cbClientes.SelectedIndex <> -1 And cbIdCredito.SelectedIndex <> -1 Then

            Session.Remove("PagosAtrasados")
            'Session.Remove("#PagosAtrasados")
            'credito = clsCredito.obtener(cbClientes.Value, True)

            Dim sol As clsSolicitud = Nothing
            Dim tablaAmort
            If EsTotal Then
                credito = clsCredito.obtener(cbIdCredito.Text)
                sol = clsSolicitud.obtener(credito.Id_Sol)
                Session("credito") = credito
                If credito.TipoAmort = 4 Then cargarDatosProgresivos(Liq, EsTotal) : Return
                If sol.TipoProducto.Equals("CREDITO ELITE") Then cargarDatosCredEmp(Liq, EsTotal) : Return

                lblCuota.Text = String.Format("{0:0.00}", Math.Ceiling(credito.Adeudo / credito.NumPagos))
                    Session("Historial") = clsPago.obtenerHistorial(cbIdCredito.Text)
                    tablaAmort = Utilerias.crearTablaDePagos(credito, Session("Historial"))
                    Session("TablaAmort") = tablaAmort

                    Session("Solicitud") = sol
                Else
                    credito = Session("credito")
                tablaAmort = Session("TablaAmort")
                sol = Session("Solicitud")
            End If

            Dim ultimoPago = clsPago.obtener(credito.Id, , True)

            cbSuc.SelectedItem = cbSuc.Items.FindByValue(sol.Id_Sucursal.ToString)

            If credito.TipoAmort <> 4 AndAlso clsSolicitud.esGarantiaLiquida(credito.Id_Sol) And Session("Historial").Count = 0 Then
                limpiarCampos(False)
                txtMonto.Value = credito.MontoPrestado * (sol.ValorGarantia / 100)
                lblACredito.Value = txtMonto.Value
                lblSuma.Text = txtMonto.Value
                Session("EsPagoGarantia") = True
                gvPagos.DataSource = Nothing
                gvPagos.DataBind()
                gvPagos.Visible = False
                lblPago.Text = 0


                If IsNothing(ultimoPago) = True And usuario.Tipo <> 10 And usuario.Tipo <> 1 Then
                    Utilerias.MensajeConfirmacion("El crédito actual no cuenta con garantía, contactar a los analistas administrativos", Me, True, True)
                    btnGuardar.ClientEnabled = False
                    Session("PagoActual") = lblPago.Text
                End If
            Else
                btnGuardar.ClientEnabled = True

                Dim historial As List(Of clsPago) = Session("Historial")
                If historial.FindAll(Function(clsPago) clsPago.EsAbonoCapital = True).Count > 0 Then
                    Dim ultPagoCap = historial.OrderBy(Function(clsPago) clsPago.Saldo).ThenBy(Function(clsPago) clsPago.Id_Mov).ToList().FindAll(Function(clsPago) clsPago.EsAbonoCapital = True).FirstOrDefault
                    Dim pagoAntesAbonoCapital = historial.OrderBy(Function(clsPago) clsPago.SaldoCapital).ThenBy(Function(clsPago) clsPago.Id_Mov).ToList().FindAll(Function(clsPago) clsPago.EsGarantia = False And clsPago.EsAbonoCapital = False And clsPago.Saldo >= ultPagoCap.Saldo).FirstOrDefault
                    Dim pagosRestantes = credito.NumPagos - If(IsNothing(pagoAntesAbonoCapital), 0, pagoAntesAbonoCapital.NumPago)
                    Session("PagoEsperado") = Math.Ceiling(ultPagoCap.Saldo / pagosRestantes)
                    Session("InteresActual") = Math.Round(((ultPagoCap.Saldo - ultPagoCap.SaldoCapital) / pagosRestantes) * (0.8621), 2)
                    Session("IvaActual") = Math.Round(((ultPagoCap.Saldo - ultPagoCap.SaldoCapital) / pagosRestantes) * (0.1379), 2)
                    lblCuota.Text = String.Format("{0:0.00}", Session("PagoEsperado"))
                Else
                    Session("PagoEsperado") = Math.Ceiling(credito.Adeudo / credito.NumPagos)
                    Session("InteresActual") = Math.Round(((credito.Adeudo - credito.MontoPrestado) / credito.NumPagos) * (0.8621), 2)
                    Session("IvaActual") = Math.Round(((credito.Adeudo - credito.MontoPrestado) / credito.NumPagos) * (0.1379), 2)
                End If

                Dim totalAbonado As Double
                If IsNothing(ultimoPago) = False Then
                    btnGuardar.ClientEnabled = True
                    totalAbonado = Utilerias.getDataTable(String.Format("SELECT SUM(monto) FROM PAGOS WHERE Id_CREDITO = {0} AND NumPago = {1} AND CANCELADO = 0", ultimoPago.Id_Credito, ultimoPago.NumPago)).Rows.Item(0).Item(0)
                    Session("TotalAbonado") = totalAbonado
                    Session("InteresAbonado") = Utilerias.getDataTable(String.Format("SELECT SUM(AbonoInteres) FROM PAGOS WHERE Id_CREDITO = {0} AND NumPago = {1} AND CANCELADO = 0", ultimoPago.Id_Credito, ultimoPago.NumPago)).Rows.Item(0).Item(0)
                    Session("IVAAbonado") = Utilerias.getDataTable(String.Format("SELECT SUM(AbonoIVA) FROM PAGOS WHERE Id_CREDITO = {0} AND NumPago = {1} AND CANCELADO = 0", ultimoPago.Id_Credito, ultimoPago.NumPago)).Rows.Item(0).Item(0)

                    If Session("TotalAbonado") >= Session("PagoEsperado") Then
                        lblPago.Text = ultimoPago.NumPago + 1
                        Session("TotalAbonado") = 0
                        Session("InteresAbonado") = 0
                        Session("IVAAbonado") = 0
                    Else
                        lblPago.Text = ultimoPago.NumPago
                        Session("UltLetraIncompleta") = True
                    End If

                    Session("PagoActual") = lblPago.Text

                Else
                    Session("TotalAbonado") = 0
                    Session("InteresAbonado") = 0
                    Session("IVAAbonado") = 0
                    lblPago.Text = 1
                End If

                Dim pagoCancelado = clsPago.obtener(credito.Id, lblPago.Text,,, True)
                Dim fechaEsperada = Utilerias.calcularFechaDePago(credito.FecInicio, lblPago.Text, credito.Plazo)

                Dim pagosAtrasados As New List(Of Double)
                Dim j = 0
                Dim esperadoGuardado = False
                Dim esperado As Double

                While j < tablaAmort.Rows.Count
                    If IsDBNull(tablaAmort.Rows.Item(j).Item(2)) And Not esperadoGuardado Then
                        esperado = (tablaAmort.Rows.Item(j).Item(3))
                        esperadoGuardado = True
                    Else
                        If IsDBNull(tablaAmort.Rows.Item(j).Item(2)) And tablaAmort.Rows.Item(j).Item(1) <= System.DateTime.Now.Date And esperadoGuardado Then
                            pagosAtrasados.Add(tablaAmort.Rows.Item(j).Item(3))
                        End If
                    End If
                    j += 1
                End While

                Session("PagosAtrasados") = pagosAtrasados
                Session("PagoEsperado") = esperado
                Session("Recargo") = calcularRecargo(credito, System.DateTime.Now.Date, fechaEsperada)

                Dim condonacionRemota = If(Session("Recargo") > 0, clsCondonacionRemota.obtener(credito.Id), Nothing)
                Session("condonacionRemota") = condonacionRemota
                Dim montoACondonar = If(IsNothing(condonacionRemota) = False, If(condonacionRemota.Monto < Session("Recargo"), condonacionRemota.Monto, Session("Recargo")), 0)
                Dim recargo = Session("Recargo") - montoACondonar
                Dim aCredito = Session("PagoEsperado") + pagosAtrasados.Sum()
                Dim total = Math.Ceiling(aCredito + Session("Recargo"))
                Dim redondeo = total - aCredito - recargo

                If lblPago.Text <> 0 Then
                    lblRecargo.Value = String.Format("{0:0.00}", recargo)
                    lblACredito.Text = String.Format("{0:0.00}", aCredito + redondeo)
                    lblSuma.Value = String.Format("{0:0.00}", total)
                End If

                txtMonto.Value = total

                'If lblPago.Text <> 0 Then
                '    lblRecargo.Value = String.Format("{0:0.00}", Session("Recargo") - montoACondonar)
                '    lblACredito.Text = String.Format("{0:0.00}", Session("PagoEsperado") + pagosAtrasados.Sum())
                '    lblSuma.Value = String.Format("{0:0.00}", Session("PagoEsperado") + Session("Recargo") + pagosAtrasados.Sum() - montoACondonar)
                'End If


                'txtMonto.Value = Session("PagoEsperado") + (Session("Recargo") - montoACondonar) + pagosAtrasados.Sum()

                If Session("Recargo") = 0 Then
                    lblRecargo.Visible = False
                    lblRecargoSig.Visible = False
                    lblTxtRecargo.Visible = False
                    lblCondRem.Text = ""
                    lblCondRem.Visible = False
                    cbCondonacion.Checked = False
                    cbCondonacion.Enabled = True
                Else
                    lblRecargo.Visible = True
                    lblRecargoSig.Visible = True
                    lblTxtRecargo.Visible = True
                    cbAbonoCap.Visible = False
                    cbAbonoCap.Checked = False
                    If IsNothing(condonacionRemota) = False Then
                        lblCondRem.Text = "Condonación Remota por $" & String.Format("{0:0.00}", condonacionRemota.Monto) & " autorizada por " & condonacionRemota.Login
                        lblCondRem.Visible = True
                        cbCondonacion.Checked = True
                        cbCondonacion.Enabled = False
                        txtMotCond.InnerText = "CONDONACIÓN REMOTA ID: " & condonacionRemota.Id
                    Else
                        lblCondRem.Text = ""
                        lblCondRem.Visible = False
                        cbCondonacion.Checked = False
                        cbCondonacion.Enabled = True
                    End If
                End If

                gvPagos.DataSource = tablaAmort
                gvPagos.DataBind()
                gvPagos.Visible = True
                Session("TablaAmort") = tablaAmort
                Session("EsPagoGarantia") = False

            End If

            If Session("Recargo") = 0 AndAlso
                (credito.TipoGarantia = 3 OrElse credito.TipoGarantia = 5) AndAlso
                LiqBase Then
                Dim base = credito.MontoPrestado * (sol.ValorGarantia / 100)
                Dim faltante = calcularFaltanteLiquidacion(lblPago.Text) - base
                faltante = If(faltante < 0, 0, faltante)
                Session("Faltante") = Math.Ceiling(faltante)
                Dim extra = If(faltante = 0, "", " más un pago de $" & Session("Faltante"))
                txtMonto.Value = Session("Faltante")
                cbAbonoCap.Visible = False
                cbLiquidaBase.Visible = True
                cbLiquidaBase.Checked = True
                lblBase.Visible = True
                lblTxtBase.Visible = True
                lblBase.Text = String.Format("{0:0.00}", base)
                lblACredito.Text = String.Format("{0:0.00}", base + Session("Faltante"))
                lblSuma.Value = lblACredito.Value
                lblPago.Text = 0
                Utilerias.MensajeConfirmacion("Puede liquidar el crédito usando su base de $" & base & extra, Me, True, True)
            ElseIf Session("Recargo") = 0 AndAlso
                (credito.TipoGarantia = 3 OrElse credito.TipoGarantia = 5) AndAlso
                Liq Then
                Dim faltante = calcularFaltanteLiquidacion(lblPago.Text)
                faltante = If(faltante < 0, 0, faltante)
                Session("Faltante") = Math.Ceiling(faltante)
                txtMonto.Value = Session("Faltante")
                cbAbonoCap.Visible = False
                cbLiquida.Visible = True
                cbLiquida.Checked = True
                lblBase.Visible = False
                lblTxtBase.Visible = False
                'lblBase.Text = String.Format("{0:0.00}", base)
                lblACredito.Text = String.Format("{0:0.00}", Session("Faltante"))
                lblSuma.Value = lblACredito.Value
                lblPago.Text = 0
                Utilerias.MensajeConfirmacion("Puede liquidar el crédito con un pago de $" & Session("Faltante"), Me, True, True)
            ElseIf Session("Recargo") = 0 AndAlso
                (credito.TipoGarantia = 3 OrElse credito.TipoGarantia = 5) AndAlso
                Not LiqBase AndAlso Not Liq Then
                cbLiquidaBase.Visible = True
                cbLiquida.Visible = True
                lblBase.Visible = False
                lblTxtBase.Visible = False
            Else
                cbLiquidaBase.Visible = False
                cbLiquidaBase.Checked = False
                cbLiquida.Visible = False
                cbLiquida.Checked = False
                lblBase.Visible = False
                lblTxtBase.Visible = False
            End If

        Else
            If cbClientes.SelectedIndex = -1 And cbIdCredito.SelectedIndex <> -1 Then
                cbClientes.SelectedItem = cbClientes.Items.FindByValue(cbIdCredito.Value)
            ElseIf cbIdCredito.SelectedIndex = -1 And cbClientes.SelectedIndex <> -1 Then
                cbIdCredito.SelectedItem = cbIdCredito.Items.FindByValue(cbClientes.Value)
            End If
        End If

        If lblBase.Visible = False Then
            lblMas.Visible = False
        Else
            lblMas.Visible = True
        End If

        If lblRecargo.Visible = False Then
            lblMas2.Visible = False
        Else
            lblMas2.Visible = True
        End If



    End Sub

    Private Sub cargarDatosProgresivos(Optional Liq As Boolean = False, Optional EsTotal As Boolean = False)

        Session.Remove("PagosAtrasados")
        'Session.Remove("#PagosAtrasados")
        'credito = clsCredito.obtener(cbClientes.Value, True)

        Dim sol
        Dim tablaAmort
        If EsTotal Then
            credito = clsCredito.obtener(cbIdCredito.Text)
            Session("credito") = credito
            Session("Historial") = clsPago.obtenerHistorial(cbIdCredito.Text)
            tablaAmort = Utilerias.crearTablaDePagosProgresiva(credito, Session("Historial"))
            Session("TablaAmort") = tablaAmort
            sol = clsSolicitud.obtener(credito.Id_Sol)
            Session("Solicitud") = sol
        Else
            credito = Session("credito")
            tablaAmort = Session("TablaAmort")
            sol = Session("Solicitud")
        End If

        Dim ultimoPago = clsPago.obtener(credito.Id, , True)

        cbSuc.SelectedItem = cbSuc.Items.FindByValue(sol.Id_Sucursal.ToString)
        Dim sobretasa = (credito.Sobretasa / 365) / 100
        sobretasa = Math.Round(If(credito.Plazo = 1, sobretasa * 7, If(credito.Plazo = 3, sobretasa * 30, sobretasa * 15)), 3)
        Dim historial As List(Of clsPago) = Session("Historial")
        If historial.FindAll(Function(clsPago) clsPago.EsAbonoCapital = True).Count > 0 Then
            Dim ultPagoCap = historial.OrderBy(Function(clsPago) clsPago.Saldo).ThenBy(Function(clsPago) clsPago.Id_Mov).ToList().FindAll(Function(clsPago) clsPago.EsAbonoCapital = True).FirstOrDefault
            Dim pagoAntesAbonoCapital = historial.OrderBy(Function(clsPago) clsPago.SaldoCapital).ThenBy(Function(clsPago) clsPago.Id_Mov).ToList().FindAll(Function(clsPago) clsPago.EsAbonoCapital = False And clsPago.Saldo >= ultPagoCap.Saldo).FirstOrDefault
            Dim pagosRestantes = credito.NumPagos - If(IsNothing(pagoAntesAbonoCapital), 0, pagoAntesAbonoCapital.NumPago)
            Session("PagoEsperado") = Math.Abs(Pmt(sobretasa, credito.NumPagos, credito.MontoPrestado))
            If Not IsNothing(ultPagoCap) Then
                Session("PagoEsperado") = Math.Abs(Pmt(sobretasa, credito.NumPagos - pagoAntesAbonoCapital.NumPago, ultPagoCap.SaldoCapital))
            End If
        Else
            Session("PagoEsperado") = Math.Abs(Pmt(sobretasa, credito.NumPagos, credito.MontoPrestado))
        End If

        lblCuota.Text = String.Format("{0:0.00}", Math.Ceiling(Session("PagoEsperado")))

        Dim totalAbonado As Double
        If IsNothing(ultimoPago) = False Then

            totalAbonado = Utilerias.getDataTable(String.Format("SELECT SUM(monto) FROM PAGOS WHERE Id_CREDITO = {0} AND NumPago = {1} AND CANCELADO = 0", ultimoPago.Id_Credito, ultimoPago.NumPago)).Rows.Item(0).Item(0)
            Session("TotalAbonado") = totalAbonado
            Session("InteresAbonado") = Utilerias.getDataTable(String.Format("SELECT SUM(AbonoInteres) FROM PAGOS WHERE Id_CREDITO = {0} AND NumPago = {1} AND CANCELADO = 0", ultimoPago.Id_Credito, ultimoPago.NumPago)).Rows.Item(0).Item(0)
            Session("IVAAbonado") = Utilerias.getDataTable(String.Format("SELECT SUM(AbonoIVA) FROM PAGOS WHERE Id_CREDITO = {0} AND NumPago = {1} AND CANCELADO = 0", ultimoPago.Id_Credito, ultimoPago.NumPago)).Rows.Item(0).Item(0)

            If Session("TotalAbonado") >= Session("PagoEsperado") Then
                lblPago.Text = ultimoPago.NumPago + 1
                Session("TotalAbonado") = 0
                Session("InteresAbonado") = 0
                Session("IVAAbonado") = 0
            Else
                lblPago.Text = If(IsNothing(ultimoPago), 1, ultimoPago.NumPago)
            End If

        Else
            Session("TotalAbonado") = 0
            Session("InteresAbonado") = 0
            Session("IVAAbonado") = 0
            lblPago.Text = 1
        End If

        Session("PagoActual") = lblPago.Text

        Dim fechaEsperada = Utilerias.calcularFechaDePago(credito.FecInicio, lblPago.Text, credito.Plazo)

        Dim pagosAtrasados As New List(Of Double)
        Dim j = 0
        Dim esperadoGuardado = False
        Dim esperado As Double

        While j < tablaAmort.Rows.Count
            If IsDBNull(tablaAmort.Rows.Item(j).Item(2)) And Not esperadoGuardado Then
                esperado = (tablaAmort.Rows.Item(j).Item(3))
                esperadoGuardado = True
            Else
                If IsDBNull(tablaAmort.Rows.Item(j).Item(2)) And tablaAmort.Rows.Item(j).Item(1) <= System.DateTime.Now.Date And esperadoGuardado Then
                    pagosAtrasados.Add(tablaAmort.Rows.Item(j).Item(3))
                End If
            End If
            j += 1
        End While

        Session("Recargo") = 0
        Session("PagosAtrasados") = pagosAtrasados
        Session("PagoEsperado") = esperado

        Dim aCredito = Session("PagoEsperado") + pagosAtrasados.Sum()
        Dim total = Math.Ceiling(aCredito)
        Dim redondeo = total - aCredito

        If lblPago.Text <> 0 Then
            'lblRecargo.Value = String.Format("{0:0.00}", recargo)
            lblACredito.Text = String.Format("{0:0.00}", aCredito + redondeo)
            lblSuma.Value = String.Format("{0:0.00}", total)
        End If

        txtMonto.Value = total

        'If lblPago.Text <> 0 Then
        '    lblRecargo.Value = String.Format("{0:0.00}", Session("Recargo") - montoACondonar)
        '    lblACredito.Text = String.Format("{0:0.00}", Session("PagoEsperado") + pagosAtrasados.Sum())
        '    lblSuma.Value = String.Format("{0:0.00}", Session("PagoEsperado") + Session("Recargo") + pagosAtrasados.Sum() - montoACondonar)
        'End If


        'txtMonto.Value = Session("PagoEsperado") + (Session("Recargo") - montoACondonar) + pagosAtrasados.Sum()
        lblRecargo.Visible = False
        lblRecargoSig.Visible = False
        lblTxtRecargo.Visible = False
        lblCondRem.Text = ""
        lblCondRem.Visible = False
        cbCondonacion.Checked = False
        cbCondonacion.Enabled = True


        gvPagos.DataSource = tablaAmort
        gvPagos.DataBind()
        gvPagos.Visible = True
        Session("TablaAmort") = tablaAmort

        If Liq Then
            Dim faltante = calcularFaltanteLiquidacion(lblPago.Text)
            faltante = If(faltante < 0, 0, faltante)
            Session("Faltante") = faltante
            txtMonto.Value = faltante
            cbAbonoCap.Visible = False
            cbLiquida.Visible = True
            cbLiquida.Checked = True
            lblBase.Visible = False
            lblTxtBase.Visible = False
            lblACredito.Text = String.Format("{0:0.00}", faltante)
            lblSuma.Value = lblACredito.Value
            lblPago.Text = 0
            Utilerias.MensajeConfirmacion("Puede liquidar el crédito con un pago de $" & faltante, Me, True, True)
        Else
            cbLiquida.Visible = False
            cbLiquida.Checked = False
        End If

        cbLiquidaBase.Visible = False
        cbLiquidaBase.Checked = False
        lblBase.Visible = False
        lblTxtBase.Visible = False
        lblRecargo.Visible = False

        If lblBase.Visible = False Then
            lblMas.Visible = False
        Else
            lblMas.Visible = True
        End If

        If lblRecargo.Visible = False Then
            lblMas2.Visible = False
        Else
            lblMas2.Visible = True
        End If

    End Sub


    Private Sub cargarDatosCredEmp(Optional Liq As Boolean = False, Optional EsTotal As Boolean = False)
        Dim usuario As clsUsuario = Nothing
        usuario = DirectCast(Session("Usuario"), clsUsuario)

        If cbClientes.SelectedIndex <> -1 And cbIdCredito.SelectedIndex <> -1 Then

            Session.Remove("PagosAtrasados")
            'Session.Remove("#PagosAtrasados")
            'credito = clsCredito.obtener(cbClientes.Value, True)

            Dim sol As clsSolicitud = Nothing
            Dim tablaPagosAux As List(Of clsPago) = Nothing
            Dim tablaAmort
            If EsTotal Then
                credito = clsCredito.obtener(cbIdCredito.Text)
                sol = clsSolicitud.obtener(credito.Id_Sol)
                Session("credito") = credito



                lblCuota.Text = String.Format("{0:0.00}", Math.Ceiling(credito.Adeudo / credito.NumPagos))
                ' Session("Historial") = clsPago.obtenerHistorial(cbIdCredito.Text)
                tablaPagosAux = clsPago.obtenerHistorial(cbIdCredito.Text)
                Session("Historial") = tablaPagosAux



                tablaAmort = Utilerias.crearTablaDePagos(credito, Session("Historial"))
                Session("TablaAmort") = tablaAmort

                Session("Solicitud") = sol


            Else
                credito = Session("credito")
                tablaAmort = Session("TablaAmort")
                sol = Session("Solicitud")
            End If

            Dim ultimoPago = clsPago.obtener(credito.Id, , True)

            cbSuc.SelectedItem = cbSuc.Items.FindByValue(sol.Id_Sucursal.ToString)

            If credito.TipoAmort <> 4 AndAlso clsSolicitud.esGarantiaLiquida(credito.Id_Sol) And Session("Historial").Count = 0 Then

            Else
                btnGuardar.ClientEnabled = True

                Dim historial As List(Of clsPago) = Session("Historial")
                If historial.FindAll(Function(clsPago) clsPago.EsAbonoCapital = True).Count > 0 Then
                    Dim ultPagoCap = historial.OrderBy(Function(clsPago) clsPago.Saldo).ThenBy(Function(clsPago) clsPago.Id_Mov).ToList().FindAll(Function(clsPago) clsPago.EsAbonoCapital = True).FirstOrDefault
                    Dim pagoAntesAbonoCapital = historial.OrderBy(Function(clsPago) clsPago.SaldoCapital).ThenBy(Function(clsPago) clsPago.Id_Mov).ToList().FindAll(Function(clsPago) clsPago.EsGarantia = False And clsPago.EsAbonoCapital = False And clsPago.Saldo >= ultPagoCap.Saldo).FirstOrDefault
                    Dim pagosRestantes = credito.NumPagos - If(IsNothing(pagoAntesAbonoCapital), 0, pagoAntesAbonoCapital.NumPago)
                    Session("PagoEsperado") = Math.Ceiling(ultPagoCap.Saldo / pagosRestantes)
                    Session("InteresActual") = Math.Round(((ultPagoCap.Saldo - ultPagoCap.SaldoCapital) / pagosRestantes) * (0.8621), 2)
                    Session("IvaActual") = Math.Round(((ultPagoCap.Saldo - ultPagoCap.SaldoCapital) / pagosRestantes) * (0.1379), 2)
                    lblCuota.Text = String.Format("{0:0.00}", Session("PagoEsperado"))
                Else
                    Session("PagoEsperado") = Math.Ceiling(credito.Adeudo / credito.NumPagos)
                    Session("InteresActual") = Math.Round(((credito.Adeudo - credito.MontoPrestado) / credito.NumPagos) * (0.8621), 2)
                    Session("IvaActual") = Math.Round(((credito.Adeudo - credito.MontoPrestado) / credito.NumPagos) * (0.1379), 2)
                End If

                Dim totalAbonado As Double
                If IsNothing(ultimoPago) = False Then
                    btnGuardar.ClientEnabled = True
                    totalAbonado = Utilerias.getDataTable(String.Format("SELECT SUM(monto) FROM PAGOS WHERE Id_CREDITO = {0} AND NumPago = {1} AND CANCELADO = 0", ultimoPago.Id_Credito, ultimoPago.NumPago)).Rows.Item(0).Item(0)
                    Session("TotalAbonado") = totalAbonado
                    Session("InteresAbonado") = Utilerias.getDataTable(String.Format("SELECT SUM(AbonoInteres) FROM PAGOS WHERE Id_CREDITO = {0} AND NumPago = {1} AND CANCELADO = 0", ultimoPago.Id_Credito, ultimoPago.NumPago)).Rows.Item(0).Item(0)
                    Session("IVAAbonado") = Utilerias.getDataTable(String.Format("SELECT SUM(AbonoIVA) FROM PAGOS WHERE Id_CREDITO = {0} AND NumPago = {1} AND CANCELADO = 0", ultimoPago.Id_Credito, ultimoPago.NumPago)).Rows.Item(0).Item(0)

                    If Session("TotalAbonado") >= Session("PagoEsperado") Then
                        lblPago.Text = ultimoPago.NumPago + 1
                        Session("TotalAbonado") = 0
                        Session("InteresAbonado") = 0
                        Session("IVAAbonado") = 0
                    Else
                        lblPago.Text = ultimoPago.NumPago
                        Session("UltLetraIncompleta") = True
                    End If

                    Session("PagoActual") = lblPago.Text

                Else
                    Session("TotalAbonado") = 0
                    Session("InteresAbonado") = 0
                    Session("IVAAbonado") = 0
                    lblPago.Text = 1
                End If

                Dim pagoCancelado = clsPago.obtener(credito.Id, lblPago.Text,,, True)
                Dim fechaEsperada = Utilerias.calcularFechaDePago(credito.FecInicio, lblPago.Text, credito.Plazo)

                Dim pagosAtrasados As New List(Of Double)
                Dim j = 0
                Dim esperadoGuardado = False
                Dim esperado As Double

                While j < tablaAmort.Rows.Count
                    If IsDBNull(tablaAmort.Rows.Item(j).Item(2)) And Not esperadoGuardado Then
                        esperado = (tablaAmort.Rows.Item(j).Item(3))
                        esperadoGuardado = True
                    Else
                        If IsDBNull(tablaAmort.Rows.Item(j).Item(2)) And tablaAmort.Rows.Item(j).Item(1) <= System.DateTime.Now.Date And esperadoGuardado Then
                            pagosAtrasados.Add(tablaAmort.Rows.Item(j).Item(3))
                        End If
                    End If
                    j += 1
                End While

                Session("PagosAtrasados") = pagosAtrasados
                Session("PagoEsperado") = esperado
                Session("Recargo") = calcularRecargo(credito, System.DateTime.Now.Date, fechaEsperada)

                Dim condonacionRemota = If(Session("Recargo") > 0, clsCondonacionRemota.obtener(credito.Id), Nothing)
                Session("condonacionRemota") = condonacionRemota
                Dim montoACondonar = If(IsNothing(condonacionRemota) = False, If(condonacionRemota.Monto < Session("Recargo"), condonacionRemota.Monto, Session("Recargo")), 0)
                Dim recargo = Session("Recargo") - montoACondonar
                Dim aCredito = Session("PagoEsperado") + pagosAtrasados.Sum()
                Dim total = Math.Ceiling(aCredito + Session("Recargo"))
                Dim redondeo = total - aCredito - recargo

                'validar si es el primer pago
                If tablaPagosAux.Count = 0 Then
                    lblACredito.Text = credito.Adeudo
                    lblSuma.Text = credito.Adeudo
                    lblCuota.Text = (credito.Adeudo - credito.MontoPrestado)
                    Return


                ElseIf lblPago.Text <> 0 Then
                    lblRecargo.Value = String.Format("{0:0.00}", recargo)
                    lblACredito.Text = String.Format("{0:0.00}", aCredito + redondeo)
                    lblSuma.Value = String.Format("{0:0.00}", total)
                End If

                txtMonto.Value = total

                'If lblPago.Text <> 0 Then
                '    lblRecargo.Value = String.Format("{0:0.00}", Session("Recargo") - montoACondonar)
                '    lblACredito.Text = String.Format("{0:0.00}", Session("PagoEsperado") + pagosAtrasados.Sum())
                '    lblSuma.Value = String.Format("{0:0.00}", Session("PagoEsperado") + Session("Recargo") + pagosAtrasados.Sum() - montoACondonar)
                'End If


                'txtMonto.Value = Session("PagoEsperado") + (Session("Recargo") - montoACondonar) + pagosAtrasados.Sum()

                If Session("Recargo") = 0 Then
                    lblRecargo.Visible = False
                    lblRecargoSig.Visible = False
                    lblTxtRecargo.Visible = False
                    lblCondRem.Text = ""
                    lblCondRem.Visible = False
                    cbCondonacion.Checked = False
                    cbCondonacion.Enabled = True
                Else
                    lblRecargo.Visible = True
                    lblRecargoSig.Visible = True
                    lblTxtRecargo.Visible = True
                    cbAbonoCap.Visible = False
                    cbAbonoCap.Checked = False
                    If IsNothing(condonacionRemota) = False Then
                        lblCondRem.Text = "Condonación Remota por $" & String.Format("{0:0.00}", condonacionRemota.Monto) & " autorizada por " & condonacionRemota.Login
                        lblCondRem.Visible = True
                        cbCondonacion.Checked = True
                        cbCondonacion.Enabled = False
                        txtMotCond.InnerText = "CONDONACIÓN REMOTA ID: " & condonacionRemota.Id
                    Else
                        lblCondRem.Text = ""
                        lblCondRem.Visible = False
                        cbCondonacion.Checked = False
                        cbCondonacion.Enabled = True
                    End If
                End If

                gvPagos.DataSource = tablaAmort
                gvPagos.DataBind()
                gvPagos.Visible = True
                Session("TablaAmort") = tablaAmort
                Session("EsPagoGarantia") = False

            End If

            If Session("Recargo") = 0 AndAlso
                (credito.TipoGarantia = 3 OrElse credito.TipoGarantia = 5) AndAlso
                Liq Then
                Dim faltante = calcularFaltanteLiquidacion(lblPago.Text)
                faltante = If(faltante < 0, 0, faltante)
                Session("Faltante") = Math.Ceiling(faltante)
                txtMonto.Value = Session("Faltante")
                cbAbonoCap.Visible = False
                cbLiquida.Visible = True
                cbLiquida.Checked = True
                lblBase.Visible = False
                lblTxtBase.Visible = False
                'lblBase.Text = String.Format("{0:0.00}", base)
                lblACredito.Text = String.Format("{0:0.00}", Session("Faltante"))
                lblSuma.Value = lblACredito.Value
                lblPago.Text = 0
                Utilerias.MensajeConfirmacion("Puede liquidar el crédito con un pago de $" & Session("Faltante"), Me, True, True)

            Else
                cbLiquidaBase.Visible = False
                cbLiquidaBase.Checked = False
                cbLiquida.Visible = False
                cbLiquida.Checked = False
                lblBase.Visible = False
                lblTxtBase.Visible = False
            End If

        Else
            If cbClientes.SelectedIndex = -1 And cbIdCredito.SelectedIndex <> -1 Then
                cbClientes.SelectedItem = cbClientes.Items.FindByValue(cbIdCredito.Value)
            ElseIf cbIdCredito.SelectedIndex = -1 And cbClientes.SelectedIndex <> -1 Then
                cbIdCredito.SelectedItem = cbIdCredito.Items.FindByValue(cbClientes.Value)
            End If
        End If

        If lblBase.Visible = False Then
            lblMas.Visible = False
        Else
            lblMas.Visible = True
        End If

        If lblRecargo.Visible = False Then
            lblMas2.Visible = False
        Else
            lblMas2.Visible = True
        End If



    End Sub


    Private Sub guardarPago()

        Dim condRem As clsCondonacionRemota = Session("condonacionRemota")
        Dim montoAcond = If(IsNothing(condRem) = False, If(condRem.Monto < Session("Recargo"), condRem.Monto, Session("Recargo")), 0)
        Dim recargo As Double = If(cbCondonacion.Checked, If(IsNothing(condRem), 0, Session("Recargo") - montoAcond), Session("Recargo"))

        If txtMonto.Value < 1 And Not (cbLiquidaBase.Checked AndAlso (txtMonto.Value <= Session("Faltante"))) Then Utilerias.MensajeAlerta("El monto abonado no puede ser negativo ni 0.", Me, True) : Return
        If txtMonto.Value < Session("Recargo") And Not cbCondonacion.Checked Then Utilerias.MensajeAlerta("El abono no puede ser menor al recargo.", Me, True) : Return
        If txtMonto.Value < recargo And cbCondonacion.Checked Then Utilerias.MensajeAlerta("El abono no puede ser menor al recargo.", Me, True) : Return
        If cbLiquidaBase.Checked AndAlso (txtMonto.Value < Session("Faltante")) Then Utilerias.MensajeAlerta("El monto a pagar debe cubrir el sobrante de la aplicación de base. Sobrante = $" + String.Format("{0:0.00}", Session("Faltante")), Me, True) : Return
        If cbLiquida.Checked AndAlso (txtMonto.Value < Session("Faltante")) Then Utilerias.MensajeAlerta("El monto a pagar debe cubrir el monto especificado de $" + String.Format("{0:0.00}", Session("Faltante")), Me, True) : Return

        Try

            credito = Session("credito")
            Dim ultimoPago = clsPago.obtener(credito.Id, , True, True)
            Dim ultPagoObligatorio = clsPago.obtener(credito.Id, , True)
            Dim LiquidacionDirecta = If(IsNothing(ultimoPago) = False AndAlso lblACredito.Text >= ultimoPago.Saldo, True, False)
            Dim saldoCap As Double
            Dim interesPorPago = Session("IvaActual") + Session("InteresActual")
            Dim interes = Session("InteresActual")
            Dim iva = Session("IvaActual")
            'Dim monto = If(Session("PagoEsperado") > lblACredito.Value, lblACredito.Value, Session("PagoEsperado"))
            Dim monto = If(IsNothing(Session("Pago")) = False OrElse (IsNothing(Session("PagosAtrasados")) OrElse Session("PagosAtrasados").Count = 0), If(cbLiquidaBase.Checked, txtMonto.Value, lblACredito.Value), If(Session("PagoEsperado") > lblACredito.Value, lblACredito.Value, Session("PagoEsperado")))
            Dim pagosAdicionales As New List(Of Double)
            Dim adicional As Double = 0

            If (monto - Session("PagoEsperado") > 0) AndAlso Not (cbLiquidaBase.Checked Or cbLiquida.Checked Or cbAbonoCap.Checked Or Session("EsPagoGarantia")) AndAlso (Session("PagoActual") <> credito.NumPagos) Then
                adicional = monto - Session("PagoEsperado")
                Dim tablaAmort = Session("TablaAmort")
                Dim j = 0
                While j < tablaAmort.Rows.Count
                    If IsDBNull(tablaAmort.Rows.Item(j).Item(2)) = False Then
                        j += 1
                    Else
                        pagosAdicionales.Add(tablaAmort.Rows.Item(j).Item(3))
                        j += 1
                    End If
                End While
                pagosAdicionales.RemoveAt(0)
                If adicional > pagosAdicionales.Sum() Then
                    pagosAdicionales.Item(pagosAdicionales.Count - 1) = pagosAdicionales.Item(pagosAdicionales.Count - 1) + (adicional - pagosAdicionales.Sum())
                End If
                monto = monto - adicional
            End If

            Dim movto As New clsMovto

            'If IsNothing(Session("Pago")) = False Then
            '    Dim pago As clsPago = Session("Pago")
            '    ultimoPago = If(pago.NumPago > 1, clsPago.obtener(credito.Id, pago.NumPago - 1), Nothing)
            '    ultPagoObligatorio = If(IsNothing(ultimoPago) = False AndAlso IsNothing(ultPagoObligatorio) = False AndAlso ultPagoObligatorio.Id = ultimoPago.Id, clsPago.obtener(credito.Id, pago.NumPago - 1), Nothing)
            '    'recargo = Session("Recargo")
            '    Dim montoAnterior = monto - pago.Monto
            '    movto = clsMovto.obtener(pago.Id_Mov)
            '    movto.Pago = pago - (montoAnterior - monto)

            '    If cbAbonoCap.Checked And IsNothing(ultimoPago) Then
            '        saldoCap = credito.MontoPrestado - monto
            '    ElseIf cbAbonoCap.Checked And IsNothing(ultimoPago) = False Then
            '        saldoCap = ultimoPago.SaldoCapital - monto
            '    ElseIf lblPago.Text > 1 And IsNothing(ultimoPago) = False Then
            '        saldoCap = ultimoPago.SaldoCapital - (monto - (If(Session("TotalAbonado") >= interesPorPago, 0, interesPorPago - Session("TotalAbonado"))))
            '    ElseIf lblPago.Text = 1 Then
            '        saldoCap = credito.MontoPrestado - (monto - (If(Session("TotalAbonado") >= interesPorPago, 0, interesPorPago - Session("TotalAbonado"))))
            '    Else
            '        saldoCap = credito.MontoPrestado
            '    End If

            '    pago.Monto = monto
            '    pago.AbonoInteres = If(pago.EsGarantia, 0, If(IsNothing(Session("InteresAbonado")), If(monto >= interes, interes, monto), If(monto >= interes - Session("InteresAbonado"), interes - Session("InteresAbonado"), monto)))
            '    monto = monto - pago.AbonoInteres
            '    pago.AbonoIVA = If(pago.EsGarantia, 0, If(IsNothing(Session("IVAAbonado")), If(monto >= iva, iva, monto), If(monto >= iva - Session("IVAAbonado"), iva - Session("IVAAbonado"), monto)))
            '    monto = monto - pago.AbonoIVA
            '    pago.AbonoCapital = If(pago.EsGarantia, 0, monto)
            '    pago.FecPago = System.DateTime.Now.Date
            '    pago.Saldo = If(pago.EsGarantia, credito.Adeudo, If((cbLiquidaBase.Checked Or cbAbonoCap.Checked), Utilerias.calcularNuevoSaldo(credito, If(IsNothing(ultPagoObligatorio), 0, ultPagoObligatorio.NumPago), saldoCap), If(lblPago.Text = 1, credito.Adeudo - txtMonto.Value, ultimoPago.Saldo - pago.Monto)))
            '    pago.SaldoCapital = saldoCap
            '    pago.EsAbonoCapital = cbAbonoCap.Checked
            '    pago.Id_Sucursal = cbSuc.SelectedItem.Value
            '    pago.Condonado = If(pago.Atrasado, pago.Condonado = cbCondonacion.Checked, Nothing)
            '    pago.MotivoCondonacion = If(IsNothing(pago.Condonado) = False AndAlso pago.Condonado, txtMotCond.Value, Nothing)
            '    pago.UltModif = System.DateTime.Now.Date
            '    pago.LoginModif = Session("Usuario").Login
            '    pago.LoginCond = If(pago.Condonado, Session("UsuarioAutCond").Login, Nothing)

            '    If clsPago.actualizar(pago) AndAlso clsMovto.actualizar(movto) Then
            '        Session.Remove("Pago")
            '        limpiarCampos(False)
            '        cargarDatos(cbLiquidaBase.Checked, , True)
            '        'deFecDep.ReadOnly = False
            '        Utilerias.MensajeConfirmacion("El pago se ha actualizado exitosamente!", Me, True)
            '    Else
            '        Utilerias.MensajeAlerta("Ocurrió un error mientras se actulizaba la información del pago", Me, True)
            '    End If
            'Else
            'Dim pagoCancelado = clsPago.obtener(credito.Id, lblPago.Text, , , True)
            'recargo = Session("Recargo")
            'If cbCondonacion.Checked Then recargo = 0

            'If IsNothing(pagoCancelado) AndAlso IsNothing(ultPagoObligatorio) = False AndAlso System.DateTime.Now.Date > ultPagoObligatorio.FecProxPago And cbAbonoCap.Checked Then Utilerias.MensajeAlerta(String.Format("No puedo hacer un abono a capital con un saldo vencido!", camposVacios), Me, True) : Return
            Dim historial As List(Of clsPago) = Session("Historial")

            If (cbLiquidaBase.Checked Or cbAbonoCap.Checked) And IsNothing(ultimoPago) Then
                saldoCap = credito.MontoPrestado - monto
            ElseIf (cbLiquidaBase.Checked Or cbAbonoCap.Checked) And IsNothing(ultimoPago) = False Then
                saldoCap = ultimoPago.SaldoCapital - monto
            ElseIf IsNothing(ultimoPago) = False Then
                saldoCap = ultimoPago.SaldoCapital - (monto - (If(Session("TotalAbonado") >= interesPorPago, 0, If(monto >= interesPorPago - Session("TotalAbonado"), interesPorPago - Session("TotalAbonado"), monto))))
            ElseIf IsNothing(ultimoPago) And Not Session("EsPagoGarantia") Then
                saldoCap = credito.MontoPrestado - (monto - (If(Session("TotalAbonado") >= interesPorPago, 0, If(monto >= interesPorPago - Session("TotalAbonado"), interesPorPago - Session("TotalAbonado"), monto))))
            Else
                saldoCap = credito.MontoPrestado
            End If

            'Dim pago As clsPago = New clsPago With {
            '.Id_Credito = txtIdCredito.Text,
            '.Id_Cliente = cbClientes.SelectedItem.Value,
            '.Id_Sucursal = cbSuc.SelectedItem.Value,
            '.NumPago = lblPago.Text,
            '.FecEsperada = If(Session("EsPagoGarantia"), System.DateTime.Now.Date, Utilerias.calcularFechaDePago(credito.FecInicio, lblPago.Text, credito.Plazo)),
            '.FecPago = System.DateTime.Now.Date,
            '.FecProxPago = Utilerias.calcularFechaDePago(credito.FecInicio, lblPago.Text + 1, credito.Plazo),
            '.Monto = txtMonto.Value,
            '.Saldo = If(Session("EsPagoGarantia"), credito.Adeudo, clsPago.calcularSaldo(credito, cbAbonoCap.Checked)),
            '.SaldoCapital = saldoCap,
            '.Login = Session("Usuario").Login,
            '.Atrasado = If(System.DateTime.Now.Date <= Utilerias.calcularFechaDePago(credito.FecInicio, lblPago.Text, credito.Plazo), False, True),
            '.EsGarantia = Session("EsPagoGarantia"),
            '.EsAbonoCapital = cbAbonoCap.Checked
            '}

            Dim pago As clsPago = New clsPago
            pago.Id_Credito = cbIdCredito.Text
            pago.Id_Cliente = cbClientes.SelectedItem.Value
            pago.Id_Sucursal = cbSuc.SelectedItem.Value
            pago.NumPago = If(cbLiquidaBase.Checked And pagosAdicionales.Count > 3, Session("PagoActual"), lblPago.Text)
            pago.FecEsperada = If(Session("EsPagoGarantia") Or cbLiquidaBase.Checked Or cbAbonoCap.Checked, System.DateTime.Now.Date, Utilerias.calcularFechaDePago(credito.FecInicio, lblPago.Text, credito.Plazo))
            'If IsNothing(pagoCancelado) Then
            pago.FecPago = System.DateTime.Now.Date
            pago.Atrasado = If(cbLiquidaBase.Checked Or cbAbonoCap.Checked Or Session("EsPagoGarantia") = True, False, If(System.DateTime.Now.Date <= Utilerias.calcularFechaDePago(credito.FecInicio, lblPago.Text, credito.Plazo), False, True))
            pago.Login = Session("Usuario").Login
            'Else
            '    pago.FecPago = pagoCancelado.FecPago
            '    pago.Atrasado = pagoCancelado.Atrasado
            '    pago.Login = pagoCancelado.Login
            'End If
            pago.UltModif = System.DateTime.Now.Date
            pago.LoginModif = Session("Usuario").Login
            pago.FecProxPago = If(cbLiquidaBase.Checked Or saldoCap <= 0, CType(Nothing, DateTime?), If(cbAbonoCap.Checked, System.DateTime.Now.Date, Utilerias.calcularFechaDePago(credito.FecInicio, lblPago.Text + 1, credito.Plazo)))
            pago.Monto = monto
            pago.AbonoInteres = If(lblPago.Text = 0 Or (cbLiquidaBase.Checked And pagosAdicionales.Count <= 3) Or cbAbonoCap.Checked, 0, If(IsNothing(Session("InteresAbonado")), If(monto >= interes, interes, monto), If(monto >= interes - Session("InteresAbonado"), interes - Session("InteresAbonado"), monto)))
            monto = monto - pago.AbonoInteres
            pago.AbonoIVA = If(lblPago.Text = 0 Or (cbLiquidaBase.Checked And pagosAdicionales.Count <= 3) Or cbAbonoCap.Checked, 0, If(IsNothing(Session("IVAAbonado")), If(monto >= iva, iva, monto), If(monto >= iva - Session("IVAAbonado"), iva - Session("IVAAbonado"), monto)))
            monto = monto - pago.AbonoIVA
            pago.AbonoCapital = If(Session("EsPagoGarantia"), 0, monto)
            pago.Saldo = If(Session("EsPagoGarantia"), credito.Adeudo, If(((cbLiquidaBase.Checked And pagosAdicionales.Count <= 3) Or cbAbonoCap.Checked), Utilerias.calcularNuevoSaldo(credito, If(IsNothing(ultPagoObligatorio), 0, ultPagoObligatorio.NumPago), saldoCap), If(IsNothing(ultimoPago), credito.Adeudo - pago.Monto, ultimoPago.Saldo - pago.Monto)))
            pago.SaldoCapital = saldoCap
            pago.Recargo = Session("Recargo")
            pago.RecargoCobrado = recargo
            pago.EsGarantia = Session("EsPagoGarantia")
            pago.EsAbonoCapital = (cbLiquidaBase.Checked And pagosAdicionales.Count <= 3) Or cbAbonoCap.Checked
            If pago.Atrasado Then
                pago.Condonado = cbCondonacion.Checked
            Else
                pago.Condonado = Nothing
            End If
            pago.MotivoCondonacion = If(IsNothing(pago.Condonado) = False AndAlso pago.Condonado, txtMotCond.Value, Nothing)
            pago.LoginCond = If(pago.Condonado, If(IsNothing(condRem), Session("UsuarioAutCond").Login, condRem.Login), Nothing)

            movto.Id = clsMovto.folioSiguiente()
            movto.Id_Credito = credito.Id
            movto.FecMov = System.DateTime.Now
            movto.Pago = If(cbLiquidaBase.Checked, txtMonto.Value, lblACredito.Value)
            movto.Moratorio = pago.RecargoCobrado
            movto.Login = Session("Usuario").Login
            movto.Id_CondRem = If(IsNothing(condRem), Nothing, condRem.Id)
            movto.MontoRealCond = If(IsNothing(condRem), Nothing, montoAcond)

            If clsMovto.insertar(movto) Then
                If IsNothing(condRem) = False Then clsCondonacionRemota.marcarUsado(condRem.Id)
                pago.Id_Mov = movto.Id
                If cbLiquidaBase.Checked OrElse cbLiquida.Checked OrElse clsPago.insertar(pago) Then

                    Session("TipoTransaccion") = If(pago.EsGarantia, "Pago de Garantía", If(pago.EsAbonoCapital, "Pago a Capital", "Pago"))

                    'If Not Session("EsPagoGarantia") Then
                    '    Session("TablaAmort") = Utilerias.crearTablaDePagos(credito)
                    '    gvPagos.DataSource = Session("TablaAmort")
                    '    gvPagos.DataBind()
                    'End If

                    If IsNothing(Session("PagosAtrasados")) = False AndAlso Session("PagosAtrasados").Count > 0 AndAlso Not cbLiquidaBase.Checked AndAlso Not cbLiquida.Checked Then
                        guardarPagosAtrasados(pago, lblACredito.Value - If(Session("PagoEsperado") > lblACredito.Value, lblACredito.Value, Session("PagoEsperado")))
                    End If

                    If pagosAdicionales.Count > 0 AndAlso Not cbLiquidaBase.Checked Then
                        guardarPagosAdelantados(pago, adicional, pagosAdicionales)
                    End If

                    Dim LiquidadoXBase = False
                    If IsNothing(Session("Faltante")) = False And cbLiquidaBase.Checked Then
                        ultimoPago = clsPago.obtener(credito.Id, , True, True)
                        LiquidadoXBase = LiquidarConBase(credito, ultimoPago, txtMonto.Value, movto.Id)
                        Session.Remove("Faltante")
                    End If

                    Dim Liquidado = False
                    If IsNothing(Session("Faltante")) = False And cbLiquida.Checked Then
                        ultimoPago = clsPago.obtener(credito.Id, , True, True)
                        Liquidado = LiquidarSinBase(credito, ultimoPago, txtMonto.Value, movto.Id)
                        Session.Remove("Faltante")
                    End If

                    Session.Remove("EsPagoGarantia")
                    Session.Remove("Recargo")
                    Session.Remove("PagoEsperado")
                    Session.Remove("TotalAbonado")
                    Session.Remove("InteresAbonado")
                    Session.Remove("IVAAbonado")
                    Session.Remove("ProcesandoPago")
                    Session.Remove("condonacionRemota")
                    Session.Remove("UltLetraIncompleta")
                    Session.Remove("InteresActual")
                    Session.Remove("IvaActual")

                    'monto = txtMonto.Value - If(pago.Condonado = True, 0, pago.Recargo)
                    monto = lblACredito.Value
                    'If pago.SaldoCapital <= 0 Or ((IsNothing(pagoAdicional) = False AndAlso pagoAdicional.SaldoCapital >= 0)) Then
                    If (pago.SaldoCapital <= 0) OrElse (LiquidadoXBase Or Liquidado) OrElse (LiquidacionDirecta) OrElse Session("Liquidado") Then
                        If IsNothing(Session("Liquidado")) Then
                            Dim strUpt = "UPDATE CREDITOS SET Liquidado = 1 WHERE Id =" & credito.Id
                            Utilerias.setUpdInsDel(strUpt)
                        End If
                        limpiarCampos()
                        Session.Remove("Liquidado")
                        Utilerias.MensajeConfirmacion("Este ha sido el último pago de este crédito!", Me, True)
                    Else
                        Dim slcCli = cbClientes.SelectedItem.Value
                        limpiarCampos(False)
                        cbClientes.Value = slcCli
                        cargarDatos(cbLiquidaBase.Checked, , True)
                        Utilerias.MensajeConfirmacion("Se ha registrado el pago exitosamente!", Me, True)
                    End If
                    generarTicket(pago, movto, (LiquidadoXBase Or Liquidado))
                Else
                    Utilerias.MensajeAlerta("Ocurrió un error al intentar registrar el pago!", Me, True)
                    clsMovto.eliminar(movto)
                End If
            Else
                Utilerias.MensajeAlerta("Ocurrió un error al intentar registrar el pago!", Me, True)
            End If
            'End If

        Catch ex As Exception
            Utilerias.MensajeAlerta("¡Parece que ha ocurrido un problema! " & ex.Message, Me, True)
            Session.Remove("ProcesandoPago")
            btnGuardar.Enabled = True
        Finally
            btnGuardar.Enabled = True
            Session.Remove("ProcesandoPago")
        End Try

    End Sub

    Private Sub guardarPagoProgresivo()

        If txtMonto.Value < 0 Then Utilerias.MensajeAlerta("El monto abonado no puede ser negativo.", Me, True) : Return
        If cbLiquida.Checked AndAlso (txtMonto.Value < Session("Faltante")) Then Utilerias.MensajeAlerta("El monto a pagar debe cubrir el monto especificado de $" + String.Format("{0:0.00}", Session("Faltante")), Me, True) : Return

        Try

            credito = Session("credito")
            Dim sobretasa = (credito.Sobretasa / 365) / 100
            sobretasa = Math.Round(If(credito.Plazo = 1, sobretasa * 7, If(credito.Plazo = 3, sobretasa * 30, sobretasa * 15)), 3)
            Dim historial As List(Of clsPago) = Session("Historial")

            Dim ultPagoCap = historial.FindAll(Function(clsPago) clsPago.EsAbonoCapital = True).OrderBy(Function(clsPago) clsPago.Saldo).ThenByDescending(Function(clsPago) clsPago.Id_Mov).FirstOrDefault
            Dim ultPagFijAntCap = If(IsNothing(ultPagoCap), Nothing, historial.FindAll(Function(clsPago) clsPago.EsAbonoCapital = False And clsPago.Id_Mov < ultPagoCap.Id_Mov).OrderBy(Function(clsPago) clsPago.Saldo).ThenByDescending(Function(clsPago) clsPago.Id_Mov).FirstOrDefault)

            Dim ultimoPago = historial.OrderBy(Function(clsPago) clsPago.Saldo).ThenByDescending(Function(clsPago) clsPago.Id_Mov).FirstOrDefault
            Dim ultPagoObligatorio = historial.FindAll(Function(clsPago) clsPago.EsAbonoCapital = False).OrderBy(Function(clsPago) clsPago.Saldo).ThenByDescending(Function(clsPago) clsPago.Id_Mov).FirstOrDefault
            Dim LiquidacionDirecta = If(IsNothing(ultimoPago) = False AndAlso lblACredito.Text >= ultimoPago.Saldo, True, False)
            Dim saldoCap As Double
            'Dim interesPorPago = Utilerias.calcularInteresPorPago(credito)

            Dim pagoIncompleto = False
            If IsNothing(ultPagFijAntCap) = False Then
                Dim ultLetra = historial.FindAll(Function(clsPago) clsPago.NumPago = ultPagFijAntCap.NumPago).Sum(Function(clsPago) clsPago.Monto)
                Dim pagCap = historial.FindAll(Function(clsPago) clsPago.EsAbonoCapital = True And clsPago.Id_Mov < ultPagFijAntCap.Id_Mov).OrderBy(Function(clsPago) clsPago.Saldo).ThenByDescending(Function(clsPago) clsPago.Id_Mov).FirstOrDefault
                Dim pagFijAntCap = If(IsNothing(pagCap), Nothing, historial.FindAll(Function(clsPago) clsPago.EsAbonoCapital = False And clsPago.Id_Mov < pagCap.Id_Mov).OrderBy(Function(clsPago) clsPago.Saldo).ThenByDescending(Function(clsPago) clsPago.Id_Mov).FirstOrDefault)
                Dim letra

                If Not IsNothing(pagCap) Then
                    letra = Math.Abs(Pmt(sobretasa, credito.NumPagos - If(IsNothing(pagFijAntCap), 0, pagFijAntCap.NumPago), pagCap.SaldoCapital))
                Else
                    letra = Math.Abs(Pmt(sobretasa, credito.NumPagos, credito.MontoPrestado))
                End If

                If ultLetra < letra Then pagoIncompleto = True
            End If

            Dim interes = Math.Abs(If(IsNothing(ultPagoCap), IPmt(sobretasa, Session("PagoActual"), credito.NumPagos, credito.MontoPrestado), IPmt(sobretasa, Session("PagoActual") - (ultPagFijAntCap.NumPago - If(pagoIncompleto, 1, 0)), credito.NumPagos - (ultPagFijAntCap.NumPago - If(pagoIncompleto, 1, 0)), ultPagoCap.SaldoCapital))) * (0.8621)
            Dim iva = Math.Abs(If(IsNothing(ultPagoCap), IPmt(sobretasa, Session("PagoActual"), credito.NumPagos, credito.MontoPrestado), IPmt(sobretasa, Session("PagoActual") - (ultPagFijAntCap.NumPago - If(pagoIncompleto, 1, 0)), credito.NumPagos - (ultPagFijAntCap.NumPago - If(pagoIncompleto, 1, 0)), ultPagoCap.SaldoCapital))) * (0.1379)
            'Dim monto = If(Session("PagoEsperado") > lblACredito.Value, lblACredito.Value, Session("PagoEsperado"))
            Dim monto = If(IsNothing(Session("Pago")) = False OrElse (IsNothing(Session("PagosAtrasados")) OrElse Session("PagosAtrasados").Count = 0), If(cbLiquidaBase.Checked, txtMonto.Value, lblACredito.Value), If(Session("PagoEsperado") > lblACredito.Value, lblACredito.Value, Session("PagoEsperado")))
            Dim pagosAdicionales As New List(Of Double)
            Dim adicional As Double = 0

            If (monto - Session("PagoEsperado") > 0) AndAlso Not (cbLiquidaBase.Checked Or cbLiquida.Checked Or cbAbonoCap.Checked Or Session("EsPagoGarantia")) AndAlso (Session("PagoActual") <> credito.NumPagos) Then
                adicional = monto - Session("PagoEsperado")
                Dim tablaAmort = Session("TablaAmort")
                Dim j = 0
                While j < tablaAmort.Rows.Count
                    If IsDBNull(tablaAmort.Rows.Item(j).Item(2)) = False Then
                        j += 1
                    Else
                        pagosAdicionales.Add(tablaAmort.Rows.Item(j).Item(3))
                        j += 1
                    End If
                End While
                pagosAdicionales.RemoveAt(0)
                If adicional > pagosAdicionales.Sum() Then
                    pagosAdicionales.Item(pagosAdicionales.Count - 1) = pagosAdicionales.Item(pagosAdicionales.Count - 1) + (adicional - pagosAdicionales.Sum())
                End If
                monto = monto - adicional
            End If

            Dim movto As New clsMovto

            'If (cbLiquidaBase.Checked Or cbAbonoCap.Checked) And IsNothing(ultimoPago) Then
            '    saldoCap = credito.MontoPrestado - monto
            'ElseIf (cbLiquidaBase.Checked Or cbAbonoCap.Checked) And IsNothing(ultimoPago) = False Then
            '    saldoCap = ultimoPago.SaldoCapital - monto
            'ElseIf lblPago.Text > 1 And IsNothing(ultimoPago) = False Then
            '    saldoCap = ultimoPago.SaldoCapital - (monto - (If(Session("TotalAbonado") >= interesPorPago, 0, interesPorPago - Session("TotalAbonado"))))
            'ElseIf lblPago.Text = 1 Then
            '    saldoCap = credito.MontoPrestado - (monto - (If(Session("TotalAbonado") >= interesPorPago, 0, interesPorPago - Session("TotalAbonado"))))
            'Else
            '    saldoCap = credito.MontoPrestado
            'End If
            If cbAbonoCap.Checked Then
                saldoCap = If(IsNothing(ultimoPago), credito.MontoPrestado - monto, ultimoPago.SaldoCapital - monto)
            Else
                saldoCap = If(IsNothing(ultimoPago), credito.MontoPrestado - (If(monto < interes + iva, 0, monto - interes - iva)), ultimoPago.SaldoCapital - (monto - (If(Session("TotalAbonado") >= interes + iva, 0, If(interes + iva - Session("TotalAbonado") > monto, monto, interes + iva - Session("TotalAbonado"))))))
            End If

            Dim pago As clsPago = New clsPago
            pago.Id_Credito = cbIdCredito.Text
            pago.Id_Cliente = cbClientes.SelectedItem.Value
            pago.Id_Sucursal = cbSuc.SelectedItem.Value
            pago.NumPago = If(cbLiquidaBase.Checked And pagosAdicionales.Count > 3, Session("PagoActual"), lblPago.Text)
            pago.FecEsperada = If(cbLiquidaBase.Checked Or cbAbonoCap.Checked, System.DateTime.Now.Date, Utilerias.calcularFechaDePago(credito.FecInicio, lblPago.Text, credito.Plazo))
            pago.FecPago = System.DateTime.Now.Date
            pago.Atrasado = False
            pago.Login = Session("Usuario").Login
            pago.UltModif = System.DateTime.Now.Date
            pago.LoginModif = Session("Usuario").Login
            pago.FecProxPago = If(cbLiquidaBase.Checked Or saldoCap <= 0, CType(Nothing, DateTime?), If(cbAbonoCap.Checked, System.DateTime.Now.Date, Utilerias.calcularFechaDePago(credito.FecInicio, lblPago.Text + 1, credito.Plazo)))
            pago.Monto = monto
            pago.AbonoInteres = If(lblPago.Text = 0 Or (cbLiquidaBase.Checked And pagosAdicionales.Count <= 3) Or cbAbonoCap.Checked, 0, If(IsNothing(Session("InteresAbonado")), If(monto >= interes, interes, monto), If(monto >= interes - Session("InteresAbonado"), interes - Session("InteresAbonado"), monto)))
            monto = monto - pago.AbonoInteres
            pago.AbonoIVA = If(lblPago.Text = 0 Or (cbLiquidaBase.Checked And pagosAdicionales.Count <= 3) Or cbAbonoCap.Checked, 0, If(IsNothing(Session("IVAAbonado")), If(monto >= iva, iva, monto), If(monto >= iva - Session("IVAAbonado"), iva - Session("IVAAbonado"), monto)))
            monto = monto - pago.AbonoIVA
            pago.AbonoCapital = monto
            'pago.Saldo = If(((cbLiquidaBase.Checked And pagosAdicionales.Count <= 3) Or cbAbonoCap.Checked), Utilerias.calcularNuevoSaldo(credito, If(IsNothing(ultPagoObligatorio), 0, ultPagoObligatorio.NumPago), saldoCap), If(lblPago.Text = 1, credito.Adeudo - txtMonto.Value, ultimoPago.Saldo - pago.Monto))
            pago.Saldo = If(cbAbonoCap.Checked, Math.Abs(Pmt(sobretasa, credito.NumPagos - (Session("PagoActual") - 1), saldoCap)) * (credito.NumPagos - (Session("PagoActual") - 1)), If(IsNothing(ultimoPago), credito.Adeudo - pago.Monto, ultimoPago.Saldo - pago.Monto))
            pago.SaldoCapital = saldoCap
            pago.Recargo = 0
            pago.RecargoCobrado = 0
            pago.EsGarantia = False
            pago.EsAbonoCapital = (cbLiquidaBase.Checked And pagosAdicionales.Count <= 3) Or cbAbonoCap.Checked
            pago.Condonado = Nothing
            pago.MotivoCondonacion = Nothing
            pago.LoginCond = Nothing

            movto.Id = clsMovto.folioSiguiente()
            movto.Id_Credito = credito.Id
            movto.FecMov = System.DateTime.Now
            movto.Pago = If(cbLiquidaBase.Checked, txtMonto.Value, lblACredito.Value)
            movto.Moratorio = pago.RecargoCobrado
            movto.Login = Session("Usuario").Login
            movto.Id_CondRem = Nothing
            movto.MontoRealCond = Nothing

            If clsMovto.insertar(movto) Then
                pago.Id_Mov = movto.Id
                If cbLiquidaBase.Checked OrElse cbLiquida.Checked OrElse clsPago.insertar(pago) Then

                    Session("TipoTransaccion") = If(pago.EsGarantia, "Pago de Garantía", If(pago.EsAbonoCapital, "Pago a Capital", "Pago"))

                    'If Not Session("EsPagoGarantia") Then
                    '    Session("TablaAmort") = Utilerias.crearTablaDePagos(credito)
                    '    gvPagos.DataSource = Session("TablaAmort")
                    '    gvPagos.DataBind()
                    'End If

                    If IsNothing(Session("PagosAtrasados")) = False AndAlso Session("PagosAtrasados").Count > 0 AndAlso Not cbLiquidaBase.Checked AndAlso Not cbLiquida.Checked Then
                        guardarPagosAtrasados(pago, lblACredito.Value - If(Session("PagoEsperado") > lblACredito.Value, lblACredito.Value, Session("PagoEsperado")))
                    End If

                    If pagosAdicionales.Count > 0 AndAlso Not cbLiquidaBase.Checked Then
                        guardarPagosAdelantados(pago, adicional, pagosAdicionales)
                    End If

                    Dim LiquidadoXBase = False
                    If IsNothing(Session("Faltante")) = False And cbLiquidaBase.Checked Then
                        ultimoPago = clsPago.obtener(credito.Id, , True, True)
                        LiquidadoXBase = LiquidarConBase(credito, ultimoPago, txtMonto.Value, movto.Id)
                        Session.Remove("Faltante")
                    End If

                    Dim Liquidado = False
                    If IsNothing(Session("Faltante")) = False And cbLiquida.Checked Then
                        ultimoPago = clsPago.obtener(credito.Id, , True, True)
                        Liquidado = LiquidarSinBase(credito, ultimoPago, txtMonto.Value, movto.Id)
                        Session.Remove("Faltante")
                    End If

                    Session.Remove("EsPagoGarantia")
                    Session.Remove("Recargo")
                    Session.Remove("PagoEsperado")
                    Session.Remove("TotalAbonado")
                    Session.Remove("InteresAbonado")
                    Session.Remove("IVAAbonado")
                    Session.Remove("ProcesandoPago")
                    Session.Remove("condonacionRemota")

                    'monto = txtMonto.Value - If(pago.Condonado = True, 0, pago.Recargo)
                    monto = lblACredito.Value
                    'If pago.SaldoCapital <= 0 Or ((IsNothing(pagoAdicional) = False AndAlso pagoAdicional.SaldoCapital >= 0)) Then
                    If (pago.SaldoCapital <= 0) OrElse (LiquidadoXBase Or Liquidado) OrElse (LiquidacionDirecta) Then
                        Dim strUpt = "UPDATE CREDITOS SET Liquidado = 1 WHERE Id =" & credito.Id
                        Utilerias.setUpdInsDel(strUpt)
                        limpiarCampos()
                        Utilerias.MensajeConfirmacion("Este ha sido el último pago de este crédito!", Me, True)
                    Else
                        Dim slcCli = cbClientes.SelectedItem.Value
                        limpiarCampos(False)
                        cbClientes.Value = slcCli
                        cargarDatosProgresivos(, True)
                        Utilerias.MensajeConfirmacion("Se ha registrado el pago exitosamente!", Me, True)
                    End If
                    generarTicket(pago, movto, (LiquidadoXBase Or Liquidado))
                Else
                    Utilerias.MensajeAlerta("Ocurrió un error al intentar registrar el pago!", Me, True)
                    clsMovto.eliminar(movto)
                End If
            Else
                Utilerias.MensajeAlerta("Ocurrió un error al intentar registrar el pago!", Me, True)
            End If
            'End If

        Catch ex As Exception
            Utilerias.MensajeAlerta("¡Parece que ha ocurrido un problema! " & ex.Message, Me, True)
            Session.Remove("ProcesandoPago")
            btnGuardar.Enabled = True
        Finally
            btnGuardar.Enabled = True
            Session.Remove("ProcesandoPago")
        End Try

    End Sub

    Private Function calcularRecargo(credito As clsCredito, fechaPago As DateTime, fechaEsperada As DateTime, Optional conMensaje As Boolean = True) As Double

        'Dim pagoCancelado = clsPago.obtener(credito.Id, lblPago.Text, , , True)

        'If IsNothing(pagoCancelado) = False Then fechaPago = pagoCancelado.FecPago : txtMotCond.Value = pagoCancelado.MotivoCondonacion : cbCondonacion.Checked = If(IsNothing(pagoCancelado.Condonado), False, pagoCancelado.Condonado)
        Dim ultimaFecConRecargo = clsPago.ultimaFechaRecargo(credito.Id)
        fechaEsperada = If(ultimaFecConRecargo > fechaEsperada, ultimaFecConRecargo, fechaEsperada)
        Dim numPagosAtrasados As Integer
        If credito.TipoAmort <> 3 And fechaPago > Utilerias.agregarDiasHabiles(fechaEsperada, 2) Then
            While fechaPago > fechaEsperada
                numPagosAtrasados = numPagosAtrasados + 1
                'fechaEsperada = Utilerias.calcularFechaDePago(fechaEsperada, 1, credito.Plazo)
                fechaEsperada = fechaEsperada.AddDays(1)
            End While
            Dim recargo = (credito.MontoPrestado * (credito.TasaMoratoria / 100)) * numPagosAtrasados
            recargo = recargo + (recargo * 0.16)
            recargo = Math.Round(recargo, 2)
            If IsNothing(Session("Faltante")) And conMensaje Then Utilerias.MensajeAlerta(String.Format("Este pago tiene un atraso de {0} días y generá un interes adicional de ${1}", numPagosAtrasados, String.Format("{0:0.00}", recargo)), Me, True)
            Session("Recargo") = recargo
            cbCondonacion.Visible = True
            lblMotCond.Visible = True
            txtMotCond.Visible = True
            Return recargo
        Else
            cbCondonacion.Visible = False
            cbCondonacion.Checked = False
            lblMotCond.Visible = False
            txtMotCond.Visible = False
            txtMotCond.Value = ""
            Session("Recargo") = 0
            Return 0
        End If

    End Function

    Private Sub generarTicket(pago As clsPago, movto As clsMovto, Liquidado As Boolean)
        Try

            REM Validacion si es garantia liquida verificar por el pago correspodiente en pagos, si esta haz todo, si no, mensaje de que debe pagar
            Dim credito As clsCredito = Session("credito")
            'Dim tipoGar = clsSolicitud.obtener(credito.Id_Sol).TipoGarantia
            'Dim pago = clsPago.obtener(credito.Id, , True, True)
            'If ((tipoGar = 3 And pago.Monto > 0) Or (tipoGar = 5 And pago.Monto > 0)) Or (tipoGar <> 3 And tipoGar <> 5) Then

            Dim direccion = Utilerias.getDataTable("SELECT ('Calle ' + CLIENTES.Calle + ' #' +  CLIENTES.NumExt + ' ' + CLIENTES.Colonia + ', ' + CLIENTES.LocalidadDom + ', ' + CLIENTES.EstadoDom) AS Direccion FROM CLIENTES WHERE ID = " & credito.Id_Cliente).Rows.Item(0).Item(0)
            Dim telefono = Utilerias.getDataTable("SELECT celular FROM CLIENTES WHERE ID = " & credito.Id_Cliente).Rows.Item(0).Item(0)
            Dim cliente = Utilerias.getDataTable("SELECT (PrimNombre + ' ' + SegNombre + ' ' + PrimApellido + ' ' + SegApellido) AS cliente FROM CLIENTES WHERE ID = " & credito.Id_Cliente).Rows.Item(0).Item(0)
            Dim sucursal = Utilerias.getDataTable("SELECT Descripcion FROM CATSUCURSALES WHERE ID = " & pago.Id_Sucursal).Rows.Item(0).Item(0)
            Dim dirsuc = Utilerias.getDataTable("SELECT Direccion FROM CATSUCURSALES WHERE ID = " & pago.Id_Sucursal).Rows.Item(0).Item(0)

            Dim tablaAmort = Utilerias.crearTablaDePagos(Session("credito"), Session("Historial"))
            Dim j = 0
            While j < tablaAmort.Rows.Count
                If IsDBNull(tablaAmort.Rows.Item(j).Item(2)) Then
                    Exit While
                End If
                j += 1
            End While

            'If IsNothing(Session("parametros")) = False Then
            '    Context.Response.Write("<script language='javascript'>window.open('dspVisor/dspTicketCompra.aspx','_blank');</script>")
            'End If

            movto.FecProxPago = If(Liquidado, Nothing, If(j + 1 > tablaAmort.Rows.Count, Nothing, tablaAmort.Rows.Item(j).Item(1)))
            movto.ProxPago = If(Liquidado, Nothing, If(j + 1 > tablaAmort.Rows.Count, 0, tablaAmort.Rows.Item(j).Item(3)))
            clsMovto.actualizar(movto)

            Dim crReporteDocumento As New crTicketCompra

            crReporteDocumento.SetParameterValue("folmov", movto.Id)
            crReporteDocumento.SetParameterValue("credito", credito.Id)
            crReporteDocumento.SetParameterValue("cliente", cliente)
            crReporteDocumento.SetParameterValue("telefono", telefono)
            'crReporteDocumento.SetParameterValue("pago", abono)
            crReporteDocumento.SetParameterValue("proxPago", If(pago.Saldo - (movto.Pago - pago.Monto) <= 0 Or Liquidado, 0, tablaAmort.Rows.Item(j).Item(3)))
            crReporteDocumento.SetParameterValue("fecProxPago", If(pago.Saldo - (movto.Pago - pago.Monto) <= 0 Or Liquidado, "Liquidado", tablaAmort.Rows.Item(j).Item(1).ToShortDateString))
            crReporteDocumento.SetParameterValue("tipoTicket", Session("TipoTransaccion"))
            'crReporteDocumento.SetParameterValue("moratorio", If(pago.Condonado = True, 0, pago.Recargo))
            crReporteDocumento.SetParameterValue("total", movto.Pago + movto.Moratorio)
            crReporteDocumento.SetParameterValue("sucursal", sucursal)
            crReporteDocumento.SetParameterValue("dirsuc", dirsuc)
            crReporteDocumento.SetParameterValue("usuario", pago.Login)
            crReporteDocumento.SetParameterValue("fechaPago", pago.FecPago)

            Dim fileName = "Comprobante" & credito.Id & ".PDF"
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

            'Response.Clear()
            'Response.ContentType = "application/pdf"
            'Response.AddHeader("Content-disposition", "inline; filename=" & fileName)
            'Response.WriteFile(file)
            'Response.Flush()
            'Response.Close()

            Context.Response.Write("<script language='javascript'>window.open('../Visor.aspx','_blank');</script>")

            Utilerias.MensajeConfirmacion("Gracias por su pago!", Me, True)
            'Else
            'Utilerias.MensajeAlerta("Hubo un poblema al imprimir el ticket de pago!", Me, True)
            'End If
        Catch ex As Exception
            Utilerias.MensajeAlerta("Hubo un poblema al imprimir el ticket de pago! Error: " + ex.Message, Me, True)
        End Try
    End Sub

    Private Sub reImprimirTicket()
        Try
            Dim pago As clsPago = Session("Pago")

            'Dim credito As clsCredito = Session("credito")
            'Dim tipoGar = clsSolicitud.obtener(credito.Id_Sol).TipoGarantia

            Dim movto = clsMovto.obtener(pago.Id_Mov)

            'If (tipoGar = 3 And pago.Monto > 0) Or (tipoGar <> 3) Then

            Dim direccion = Utilerias.getDataTable("SELECT ('Calle ' + CLIENTES.Calle + ' #' +  CLIENTES.NumExt + ' ' + CLIENTES.Colonia + ', ' + CLIENTES.LocalidadDom + ', ' + CLIENTES.EstadoDom) AS Direccion FROM CLIENTES WHERE ID = " & pago.Id_Cliente).Rows.Item(0).Item(0)
            Dim telefono = Utilerias.getDataTable("SELECT celular FROM CLIENTES WHERE ID = " & pago.Id_Cliente).Rows.Item(0).Item(0)
            Dim cliente = Utilerias.getDataTable("SELECT (PrimNombre + ' ' + SegNombre + ' ' + PrimApellido + ' ' + SegApellido) AS cliente FROM CLIENTES WHERE ID = " & pago.Id_Cliente).Rows.Item(0).Item(0)
            Dim sucursal = Utilerias.getDataTable("SELECT Descripcion FROM CATSUCURSALES WHERE ID = " & pago.Id_Sucursal).Rows.Item(0).Item(0)
            Dim dirsuc = Utilerias.getDataTable("SELECT Direccion FROM CATSUCURSALES WHERE ID = " & pago.Id_Sucursal).Rows.Item(0).Item(0)

            'Dim tablaAmort = Session("TablaAmort")
            'Dim j = 0
            'While j < tablaAmort.Rows.Count
            '    If tablaAmort.Rows.Item(j).Item(11) = pago.Id Then
            '        j += 1
            '        Exit While
            '    End If
            '    j += 1
            'End While

            'If IsNothing(Session("parametros")) = False Then
            '    Context.Response.Write("<script language='javascript'>window.open('dspVisor/dspTicketCompra.aspx','_blank');</script>")
            'End If
            Dim TipoTransac = If(pago.EsGarantia, "Pago de Garantía - Reimpresión", If(pago.EsAbonoCapital, "Pago a Capital - Reimpresión", "Pago - Reimpresión"))
            Dim crReporteDocumento As New crTicketCompra

            'crReporteDocumento.SetParameterValue("credito", credito.Id)
            'crReporteDocumento.SetParameterValue("cliente", cliente)
            'crReporteDocumento.SetParameterValue("telefono", telefono)
            ''crReporteDocumento.SetParameterValue("pago", pago.Monto)
            'crReporteDocumento.SetParameterValue("proxPago", If(j + 1 > tablaAmort.Rows.Count, 0, tablaAmort.Rows.Item(j).Item(3)))
            'crReporteDocumento.SetParameterValue("fecProxPago", If(j + 1 > tablaAmort.Rows.Count, "Liquidado", pago.FecProxPago.Value.ToShortDateString))
            'crReporteDocumento.SetParameterValue("tipoTicket", TipoTransac)
            ''crReporteDocumento.SetParameterValue("moratorio", If(pago.Condonado = True, 0, pago.Recargo))
            'crReporteDocumento.SetParameterValue("total", pago.Monto + If(pago.Condonado = True, 0, pago.Recargo))
            'crReporteDocumento.SetParameterValue("sucursal", sucursal)
            'crReporteDocumento.SetParameterValue("dirsuc", dirsuc)
            'crReporteDocumento.SetParameterValue("usuario", pago.Login)
            'crReporteDocumento.SetParameterValue("fechaPago", pago.FecPago)

            crReporteDocumento.SetParameterValue("folmov", movto.Id)
            crReporteDocumento.SetParameterValue("credito", movto.Id_Credito)
            crReporteDocumento.SetParameterValue("cliente", cliente)
            crReporteDocumento.SetParameterValue("telefono", telefono)
            crReporteDocumento.SetParameterValue("proxPago", movto.ProxPago)
            crReporteDocumento.SetParameterValue("fecProxPago", If(IsNothing(movto.FecProxPago), "Liquidado", movto.FecProxPago.Value.ToShortDateString))
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

            'Response.Clear()
            'Response.ContentType = "application/pdf"
            'Response.AddHeader("Content-disposition", "inline; filename=" & fileName)
            'Response.WriteFile(file)
            'Response.Flush()
            'Response.Close()

            Context.Response.Write("<script language='javascript'>window.open('../Visor.aspx','_blank');</script>")

            Utilerias.MensajeConfirmacion("Se ha reimpreso el ticket!", Me, True)
            'Else
            '    Utilerias.MensajeAlerta("Hubo un poblema al imprimir el ticket de pago!", Me, True)
            'End If
        Catch ex As Exception
            Utilerias.MensajeAlerta("Hubo un poblema al reimprimir el ticket de pago!", Me, True)
        End Try
        cargarGvPagos()
    End Sub

    Private Function CrearPagoConSaldo(credito As clsCredito, numPago As Integer, monto As Double, id_movto As Integer, Optional esAbCap As Boolean = False) As clsPago

        'pagInteres = Math.Round((interes / (credito.NumPagos - (pagoActual))) * (0.8621), 1)
        'pagIva = Math.Round((interes / (credito.NumPagos - (pagoActual))) * (0.1379), 1)
        Dim historial As List(Of clsPago) = Session("Historial")
        Dim fecEsperada = Utilerias.calcularFechaDePago(credito.FecInicio, numPago, credito.Plazo)
        Dim ultimoPago = clsPago.obtener(credito.Id, , True, True)
        Dim ultPagoObligatorio = clsPago.obtener(credito.Id, , True)
        Dim ultPagoCap = historial.FindAll(Function(clsPago) clsPago.EsAbonoCapital = True).OrderBy(Function(clsPago) clsPago.Saldo).ThenByDescending(Function(clsPago) clsPago.Id_Mov).FirstOrDefault
        Dim ultPagFijAntCap = If(IsNothing(ultPagoCap), Nothing, historial.FindAll(Function(clsPago) clsPago.EsAbonoCapital = False And clsPago.Id_Mov < ultPagoCap.Id_Mov).OrderBy(Function(clsPago) clsPago.Saldo).ThenByDescending(Function(clsPago) clsPago.Id_Mov).FirstOrDefault)
        Dim interes
        Dim iva

        If credito.TipoAmort = 4 Then
            Dim sobretasa = (credito.Sobretasa / 365) / 100
            sobretasa = Math.Round(If(credito.Plazo = 1, sobretasa * 7, If(credito.Plazo = 3, sobretasa * 30, sobretasa * 15)), 3)
            interes = Math.Abs(If(IsNothing(ultPagoCap), IPmt(sobretasa, numPago, credito.NumPagos, credito.MontoPrestado), IPmt(sobretasa, numPago - ultPagFijAntCap.NumPago, credito.NumPagos - ultPagFijAntCap.NumPago, ultPagoCap.SaldoCapital))) * (0.8621)
            iva = Math.Abs(If(IsNothing(ultPagoCap), IPmt(sobretasa, numPago, credito.NumPagos, credito.MontoPrestado), IPmt(sobretasa, numPago - ultPagFijAntCap.NumPago, credito.NumPagos - ultPagFijAntCap.NumPago, ultPagoCap.SaldoCapital))) * (0.1379)
        Else
            interes = Session("InteresActual")
            iva = Session("IvaActual")
        End If



        'Dim saldoCap = ultimoPago.SaldoCapital - (monto - interesPorPago)
        Dim saldo = ultimoPago.Saldo - monto

        Dim pago As clsPago = New clsPago
        pago.Id_Mov = id_movto
        pago.Id_Credito = cbIdCredito.Text
        pago.Id_Cliente = cbClientes.SelectedItem.Value
        pago.Id_Sucursal = cbSuc.SelectedItem.Value
        pago.NumPago = If(esAbCap, 0, numPago)
        pago.FecEsperada = fecEsperada
        pago.FecPago = System.DateTime.Now.Date
        pago.Atrasado = If(esAbCap, False, If(System.DateTime.Now.Date <= fecEsperada, False, True))
        pago.Login = Session("Usuario").Login
        pago.UltModif = System.DateTime.Now
        pago.LoginModif = Session("Usuario").Login
        pago.FecProxPago = If(esAbCap, System.DateTime.Now.Date, Utilerias.calcularFechaDePago(credito.FecInicio, numPago + 1, credito.Plazo))
        pago.Monto = monto
        pago.AbonoInteres = If(esAbCap, 0, If(Session("InteresAbonado") <> 0 And Session("PagoActual") = pago.NumPago, If(monto >= interes - Session("InteresAbonado"), interes - Session("InteresAbonado"), monto), If(monto >= interes, interes, monto)))
        monto = monto - pago.AbonoInteres
        pago.AbonoIVA = If(esAbCap, 0, If(Session("IVAAbonado") <> 0 And Session("PagoActual") = pago.NumPago, If(monto >= iva - Session("IVAAbonado"), iva - Session("IVAAbonado"), monto), If(monto >= iva, iva, monto)))
        monto = monto - pago.AbonoIVA
        pago.AbonoCapital = monto
        pago.Saldo = saldo
        pago.SaldoCapital = ultimoPago.SaldoCapital - monto
        pago.Recargo = 0
        pago.RecargoCobrado = 0
        pago.EsGarantia = False
        pago.EsAbonoCapital = esAbCap
        If pago.Atrasado Then
            pago.Condonado = True
            pago.MotivoCondonacion = "CONDONACIÓN POR SISTEMA - PAGO ADELANTADO"
        Else
            pago.Condonado = Nothing
        End If
        'pago.LoginCond = If(pago.Condonado, Session("UsuarioAutCond").Login, Nothing)

        If (pago.SaldoCapital <= 0) Then
            Dim strUpt = "UPDATE CREDITOS SET Liquidado = 1 WHERE Id =" & credito.Id
            Utilerias.setUpdInsDel(strUpt)
            Session("Liquidado") = True
        End If

        Return pago
    End Function

    Private Function ValidaPassword(cmd As ADODB.Command, rst As ADODB.Recordset)
        Dim encriptPass = Utilerias.Encriptar(txtContra.Value)
        cmd.CommandText = String.Format("SELECT Login, Pass FROM USUARIOS WHERE (Login = '{0}')", txtLogin.Value)
        rst = cmd.Execute
        Try
            If rst.Fields(0).Value.ToString.ToLower = txtLogin.Value.ToLower And rst.Fields(1).Value = encriptPass Then
                Return True
            End If
        Catch ex As Exception

        End Try
        Return False
    End Function

    Private Function calcularFaltanteLiquidacion(numPago As Integer)

        Dim faltante As Double
        Dim tablaAmort As DataTable = Session("TablaAmort")

        Dim j = 0
        While j < tablaAmort.Rows.Count
            If IsDBNull(tablaAmort.Rows.Item(j).Item(2)) Then

                If tablaAmort.Rows.Item(j).Item(0) < 14 Then
                    faltante = faltante + tablaAmort.Rows.Item(j).Item(3)
                Else
                    faltante = faltante + tablaAmort.Rows.Item(j).Item(6)
                End If

            End If
            j += 1
        End While

        Return faltante

    End Function

    Private Function LiquidarConBase(credito As clsCredito, ultimoPago As clsPago, monto As Double, movto As Integer) As Boolean

        Dim pagosLiquid As List(Of Double) = Session("PagosLiquidacion")
        Dim pagoActual = Session("PagoActual") - 1
        Dim pagoGenerado As New clsPago
        Dim j = 1
        While j < pagosLiquid.Count
            Dim aporte = If(monto >= pagosLiquid.Item(j - 1), pagosLiquid.Item(j - 1), monto)
            pagoGenerado = CrearPagoConSaldo(Session("credito"), pagoActual + j, aporte, movto)
            'If (pagoGenerado.Saldo <= 0 Or pagoGenerado.SaldoCapital <= 0) And (aporte < monto) Then
            '    aporte = monto
            '    pagoGenerado = CrearPagoConSaldo(Session("credito"), ultimoPago.NumPago + (j + 1), aporte, ultimoPago.Id_Mov)
            'End If
            clsPago.insertar(pagoGenerado)
            monto = monto - aporte
            j += 1
        End While

        pagoGenerado = CrearPagoConSaldo(Session("credito"), 0, monto, movto, True)
        clsPago.insertar(pagoGenerado)

        Dim base = clsPago.obtenerBase(credito.Id)
        base.AbonoCapital = base.Monto
        base.Saldo = pagoGenerado.Saldo - base.Monto
        base.SaldoCapital = pagoGenerado.SaldoCapital - base.Monto
        base.GarantiaAplicada = True
        base.FecAplicGarantia = pagoGenerado.FecPago
        base.LoginModif = Session("Usuario").Login

        Return clsPago.liquidarConBase(base)

    End Function

    Private Function LiquidarSinBase(credito As clsCredito, ultimoPago As clsPago, monto As Double, movto As Integer) As Boolean

        Dim pagosLiquid As List(Of Double) = Session("PagosLiquidacion")
        Dim pagoActual = Session("PagoActual") - 1
        Dim pagoGenerado As New clsPago
        Dim j = 1
        While j < pagosLiquid.Count
            Dim aporte = If(monto >= pagosLiquid.Item(j - 1), pagosLiquid.Item(j - 1), monto)
            pagoGenerado = CrearPagoConSaldo(Session("credito"), pagoActual + j, aporte, movto)
            'If (pagoGenerado.Saldo <= 0 Or pagoGenerado.SaldoCapital <= 0) And (aporte < monto) Then
            '    aporte = monto
            '    pagoGenerado = CrearPagoConSaldo(Session("credito"), ultimoPago.NumPago + (j + 1), aporte, ultimoPago.Id_Mov)
            'End If
            clsPago.insertar(pagoGenerado)
            monto = monto - aporte
            j += 1
        End While

        pagoGenerado = CrearPagoConSaldo(Session("credito"), 0, monto, movto, True)
        clsPago.insertar(pagoGenerado)

        Dim base = clsPago.obtenerBase(credito.Id)
        base.GarantiaAplicada = False
        base.LoginModif = Session("Usuario").Login

        Return clsPago.liquidarConBase(base)

    End Function

    Private Sub guardarPagosAtrasados(pago As clsPago, sobrante As Double)

        Dim pagosAtrasados As List(Of Double) = Session("PagosAtrasados")
        Dim pagoGenerado As New clsPago
        Dim j = 1
        While j <= pagosAtrasados.Count And sobrante > 0
            Dim aporte = If(sobrante >= pagosAtrasados.Item(j - 1), pagosAtrasados.Item(j - 1), sobrante)
            pagoGenerado = CrearPagoConSaldo(Session("credito"), pago.NumPago + j, aporte, pago.Id_Mov)
            If (pagoGenerado.Saldo <= 0 Or pagoGenerado.SaldoCapital <= 0) And (aporte < sobrante) Then
                aporte = sobrante
                pagoGenerado = CrearPagoConSaldo(Session("credito"), pago.NumPago + (j + 1), aporte, pago.Id_Mov)
            End If
            clsPago.insertar(pagoGenerado)
            sobrante = sobrante - aporte
            j += 1
        End While

        'If sobrante > 0 Then
        '    Dim tablaAmort As DataTable = Session("TablaAmort")
        '    Dim indice = tablaAmort.AsEnumerable().Where(Function(f) f.Field(Of String)("#Pago") = pagoGenerado.NumPago + 1)
        '    Dim pagosAdicionales As New List(Of Double)
        '    j = pagoGenerado.NumPago + 1
        '    While j < tablaAmort.Rows.Count
        '        If tablaAmort.Rows.Item(j).Item(0) >= pagoGenerado.NumPago + 1 Then
        '            pagosAdicionales.Add(tablaAmort.Rows.Item(j).Item(3))
        '            j += 1
        '        End If
        '        j += 1
        '    End While
        '    guardarPagosAdelantados(pagoGenerado, sobrante, pagosAdicionales)
        'End If

        If sobrante > 0 Then
            Dim tablaAmort = Utilerias.crearTablaDePagos(Session("credito"), Session("Historial"))
            Dim pagosAdicionales As New List(Of Double)

            While j < tablaAmort.Rows.Count
                If IsDBNull(tablaAmort.Rows.Item(j).Item(2)) = False Then
                    j += 1
                Else
                    pagosAdicionales.Add(tablaAmort.Rows.Item(j).Item(3))
                    j += 1
                End If
            End While
            guardarPagosAdelantados(pagoGenerado, sobrante, pagosAdicionales)
        End If

    End Sub

    Private Sub guardarPagosAdelantados(pago As clsPago, sobrante As Double, pagosAdicionales As List(Of Double))

        Dim j = 0
        While sobrante > 0
            Dim aporte = If(sobrante >= pagosAdicionales.Item(j), pagosAdicionales.Item(j), sobrante)
            Dim pagoGenerado = CrearPagoConSaldo(Session("credito"), pago.NumPago + (j + 1), aporte, pago.Id_Mov)
            If (pagoGenerado.Saldo <= 0 Or pagoGenerado.SaldoCapital <= 0) And (aporte < sobrante) Then
                aporte = sobrante
                pagoGenerado = CrearPagoConSaldo(Session("credito"), pago.NumPago + (j + 1), aporte, pago.Id_Mov)
            End If
            clsPago.insertar(pagoGenerado)
            sobrante = sobrante - aporte
            j += 1
        End While

    End Sub
#End Region

End Class