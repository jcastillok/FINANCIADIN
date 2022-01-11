Imports AjaxControlToolkit

Public Class CapturaPagoEmp
    Inherits System.Web.UI.Page

    Dim credito As clsCredito
    Dim sol As clsSolicitud

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

    End Sub

    Protected Sub cbClientes_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cbClientes.SelectedIndexChanged
        If cbClientes.SelectedIndex <> -1 Then cbIdCredito.SelectedItem = cbIdCredito.Items.FindByValue(cbClientes.Value)

        ResetCargaDatos()
        cargarDatos()
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

    'Protected Sub gvPagos_HtmlRowPrepared(sender As Object, e As DevExpress.Web.ASPxGridViewTableRowEventArgs) Handles gvPagos.HtmlRowPrepared
    '    'If cbIdCredito.Value <> "" Then
    '    If IsNothing(gvPagos.DataSource) = False Then
    '        Dim historial As List(Of clsPago) = Session("Historial")
    '        Dim pago = If(IsDBNull(e.GetValue("id")), Nothing, historial.Find(Function(clsPago) clsPago.Id = e.GetValue("id")))
    '        If IsNothing(pago) = False Then
    '            If pago.Atrasado And (pago.Condonado = False Or IsNothing(pago.Condonado)) Then
    '                e.Row.BackColor = System.Drawing.Color.Gold
    '            ElseIf pago.Condonado Then
    '                e.Row.BackColor = System.Drawing.Color.Orange
    '            ElseIf pago.EsGarantia Then
    '                e.Row.BackColor = System.Drawing.Color.DodgerBlue
    '            ElseIf pago.EsAbonoCapital Then
    '                e.Row.BackColor = System.Drawing.Color.DodgerBlue
    '            Else
    '                e.Row.BackColor = System.Drawing.Color.LightGreen
    '            End If
    '        End If
    '    End If

    'End Sub


    Private Sub cargarDatos()
        Dim tablaPagosAux As List(Of clsPago) = Nothing
        Dim ultimoPago As clsPago = Nothing
        Dim montoRecargo As Double = 0
        Dim montoDeudaActual As Double = 0
        Dim montoInteresActual As Double = 0
        Dim fechaPago As DateTime

        Dim interesPendiente As Double = 0
        Dim numPagoAnt As Integer = 0
        Dim sumIntePagAnte As Double = 0
        Dim sumIvaPagAnte As Double = 0
        Dim sumIntTotal As Double = 0
        Dim Cons_numPago As Integer = 0
        ' Obtener el usuario
        Dim usuario As clsUsuario = Nothing
        usuario = DirectCast(Session("Usuario"), clsUsuario)

        'se valida que en el control de clientes se haya selecionado a un cliente
        If cbClientes.SelectedIndex <> -1 Then

            credito = clsCredito.obtener(cbIdCredito.Text)
            sol = clsSolicitud.obtener(credito.Id_Sol)

            ' Obtener la lista de pagos 
            tablaPagosAux = clsPago.obtenerHistorial(cbIdCredito.Text)
            Session("Historial") = tablaPagosAux

            Dim tablaAmort = Utilerias.crearTablaDePagos(credito, Session("Historial"))

            deFecCap.Value = System.DateTime.Now
            ' preguntar si es el primer pago
            If tablaPagosAux.Count = 0 Then
                lblPago.Text = 1
                montoDeuda.Text = FormatCurrency(credito.Adeudo, , , TriState.True, TriState.True)
                txtMontiCapital.Text = credito.MontoPrestado


                fechaPago = credito.FecPrimPago
                ultimoPago = New clsPago()
                ultimoPago.FecProxPago = credito.FecPrimPago
                ultimoPago.Saldo = credito.Adeudo
                ultimoPago.SaldoCapital = credito.MontoPrestado
            Else
                ultimoPago = tablaPagosAux.Last()
                fechaPago = ultimoPago.FecProxPago
                numPagoAnt = ultimoPago.NumPago

                ' validar si en la letra anterior se pago el interés
                'sumIntePagAnte = tablaPagosAux.Where(Function(clsPago) clsPago.NumPago = numPagoAnt).Sum(Function(clsPago) clsPago.AbonoInteres)
                'sumIvaPagAnte = tablaPagosAux.Where(Function(clsPago) clsPago.NumPago = numPagoAnt).Sum(Function(clsPago) clsPago.AbonoIVA)
                sumIntePagAnte = tablaPagosAux.Where(Function(clsPago) clsPago.NumPago = numPagoAnt).Sum(Function(clsPago) clsPago.AbonoInteres)
                sumIvaPagAnte = tablaPagosAux.Where(Function(clsPago) clsPago.NumPago = numPagoAnt).Sum(Function(clsPago) clsPago.AbonoIVA)

                sumIntTotal = sumIntePagAnte + sumIvaPagAnte
                'If ultimoPago.AbonoCapital = 0 Then
                '    Dim interescubierto As Double = 0
                '    interescubierto = ultimoPago.Saldo - ultimoPago.SaldoCapital
                '    If interescubierto = sumIntTotal Then
                '        sumIntTotal = 0
                '    End If

                'End If

            End If
            montoInteresActual = Math.Round(ultimoPago.SaldoCapital * credito.Tasa, 2)
            interesPendiente = Math.Round(montoInteresActual - sumIntTotal, 2)
            montoDeuda.Text = FormatCurrency(ultimoPago.Saldo, , , TriState.True, TriState.True)
            monto.Value = Math.Round(montoInteresActual + montoRecargo, 2)
            interesACubrir.Value = montoInteresActual
            txtMontoInteres.Value = String.Format("{0:0.00}", montoInteresActual)
            txtMontiCapital.Text = String.Format("{0:0.00}", ultimoPago.SaldoCapital)
            If tablaPagosAux.Count > 0 Then
                If interesPendiente > 0 Then
                    lblPago.Text = ultimoPago.NumPago
                    monto.Value = interesPendiente
                    interesACubrir.Value = interesPendiente
                Else
                    lblPago.Text = ultimoPago.NumPago + 1
                    monto.Value = montoInteresActual
                End If


            End If
            Cons_numPago = lblPago.Text
            ' validar si hay recargos por atraso
            If fechaPago < System.DateTime.Now Then
                montoRecargo = CalcularMontoRecargo(credito, ultimoPago, Cons_numPago)
                panelRecargo.Visible = True
                TxtmontoRecargo.Value = montoRecargo
                monto.Value = monto.Value + montoRecargo
                PanelAbnCap.Visible = False
                esRecargo.Value = True
                condonacion.Visible = True
            Else
                PanelAbnCap.Visible = True
                TxtmontoRecargo.Value = String.Format("{0:0.00}", 0)
                esRecargo.Value = False
            End If


            gvPagos.DataSource = tablaAmort
            gvPagos.DataBind()
            gvPagos.Visible = True



        End If
        btnRegistrar.Enabled = True

    End Sub

    Private Function CalcularMontoRecargo(credito As clsCredito, ultimoPago As clsPago, Cons_numPago As Integer) As Double
        Dim montoRecargo As Double = 0
        Dim diasRetraso As Integer = 0
        Dim valorTasaMora As Double = 0
        Dim tasaMora As Double = 0

        Try
            ' identificar cuantos dias de retraso tiene el pago
            Dim fechapago As DateTime = DateTime.Parse(ultimoPago.FecProxPago)
            diasRetraso = DateDiff(DateInterval.Day, System.DateTime.Now, fechapago) * -1

            ' consultar la tasa del moratorio del credito
            tasaMora = clsCredito.ConsultarTasaMora(credito)

            ' calculr el monto del moratorio
            montoRecargo = (credito.MontoPrestado * (tasaMora / 100)) * diasRetraso
            If Cons_numPago > 1 Then
                ' calculr el monto del moratorio
                montoRecargo = (ultimoPago.SaldoCapital * (tasaMora / 100)) * diasRetraso
            End If


            ' montoRecargo = montoRecargo + (montoRecargo * 0.16)
            montoRecargo = Math.Round(montoRecargo, 2)

        Catch ex As Exception

        End Try

        Return montoRecargo
    End Function

    Private Function guardarPago(movto As clsMovto, numdias As Integer) As mensajeTransaccion
        Dim mensaje As mensajeTransaccion = Nothing

        Try
            mensaje = clsMovto.EltMovtoAlt(movto, numdias)
        Catch ex As Exception
            If IsNothing(mensaje) Then
                mensaje = New mensajeTransaccion
                mensaje.codigoRespuesta = "000002"
                mensaje.mensajeRespuesta = "OCURRIO UN PROBLEMA AL COMUNICARSE CON LA BASE DE DATOS" + "\n MENSAJE TÉCNICO: " + ex.Message
            End If
        End Try
        Return mensaje

    End Function

    Protected Sub btnRegistrar_Click(sender As Object, e As EventArgs) Handles btnRegistrar.Click
        Dim mensaje As mensajeTransaccion = Nothing
        Dim movto As clsMovto
        Dim interesPagar As Double = 0
        Dim tablaPagosAux As List(Of clsPago) = Nothing
        Dim saldoCap As Double = 0
        Dim abonoCapital As Double = 0
        Dim montoPago As Double = 0
        Dim proxPagoint As Double = 0
        Dim numPagoAct As Integer = 0
        Dim hayRecargo As Boolean = False ' bandera que indica que el cliente tiene mora acumulada
        Dim saldoInteres As Double = 0      ' es el monto del capital + interes que se guarda en la tabla
        Dim montoPAgpLetra As Double = 0 ' es el monto que pague del interes
        Dim interesCubierto As Double = 0 ' el interes que se pago en ese movimiento usado para el iva y guardar en tabla
        ' contantes
        Dim aportacionCapital = 0
        Dim cons_RecargosACubrir As Double = 0 'Monto del recargo a cubrir
        Dim Cons_RecarcoConDesc As Double = 0 ' Monto del recargo aplicado el descuento de la condonacion
        Dim Cons_pagoCliente As Double = 0 ' es el apgo del cliente el cual se mantendra fijo en el proceso
        Dim pagoClienteOperacion As Double = 0 ' es el pago del cliente que va disminuyendo conforme se v cubriendo deudas
        Dim Cons_PagoInteresAPagar As Double = 0  ' es el monto del interes a cubrir
        Dim Cons_porcentCondUsuario As Decimal = 0 'Porcernatje que el usuario puede condonar
        Dim Cons_MontoRecargoGen As Decimal = 0 ' MONTO DEL RECARGO SIN CONDONACION

        ' VARIABLES
        Dim var_SeCondonaMoratorio As Boolean = False  ' Bandera que indica si se le va a condonar el moratorio al cliente
        Dim var_RecargoPagado As Double = 0  ' monto del recargo que se cubrio con el pago
        Dim var_interesCubierto As Double = 0 ' monto del interes usado como auxiliar en operaciones
        Dim var_FecEsperada As DateTime
        Dim var_MontoCondonadoUsuario As Double =0 ' monto que el usuario puede condonar

        Try
            ' validar que los controles tengan valores
            Dim camposVacios As String = ""

            If cbClientes.Text.Trim = "" Then camposVacios = camposVacios & "Cliente, "
            If cbSuc.Text.Trim = "" Then camposVacios = camposVacios & "Sucursal, "
            If monto.Text.Trim = "" Then camposVacios = camposVacios & "Monto, "

            If Moracondonado.Checked AndAlso monto.Text.Trim = "" Then camposVacios = camposVacios & "Motivo de Condonación, "
            ' If cbCondonacion.Checked And txtMotCond.Value.Trim = "" Then camposVacios = camposVacios & "Motivo Condonación, "
            'If deFecDep.Text.Trim = "" Then camposVacios = camposVacios & "Motivo Condonación, "

            If camposVacios <> "" Then
                camposVacios = camposVacios.Remove(camposVacios.LastIndexOf(","), 2)
                If camposVacios.Contains(",") Then
                    Utilerias.MensajeAlerta(String.Format("Los campos <b>{0}</b> no pueden estar vacios.", camposVacios), Me, True)
                Else
                    Utilerias.MensajeAlerta(String.Format("El campo <b>{0}</b> no puede estar vacío.", camposVacios), Me, True)
                End If
                Session.Remove("ProcesandoPago")

                Return
            End If

            ' obtener valores necesarios para el proceso
            montoPago = monto.Value
            Cons_pagoCliente = monto.Value
            pagoClienteOperacion = Cons_pagoCliente
            cons_RecargosACubrir = TxtmontoRecargo.Value
            Cons_PagoInteresAPagar = interesACubrir.Value
            montoPAgpLetra = montoPago
            credito = clsCredito.obtener(cbIdCredito.Text)
            sol = clsSolicitud.obtener(credito.Id_Sol)
            tablaPagosAux = clsPago.obtenerHistorial(cbIdCredito.Text)

            'variables para el moratorio y  condonaciones
            hayRecargo = esRecargo.Value
            var_SeCondonaMoratorio = Moracondonado.Checked
            Cons_MontoRecargoGen = TxtmontoRecargo.Value

            ' crear el objeto para guardarlo en la bd
            movto = New clsMovto()
            movto.Id_Credito = credito.Id
            movto.FecMov = System.DateTime.Now
            movto.Pago = If(Moracondonado.Checked, montoPago, montoPago - TxtmontoRecargo.Value)
            movto.ProxPago = proxPagoint
            movto.Moratorio = cons_RecargosACubrir
            movto.Login = Session("Usuario").Login
            ' movto.Id_CondRem = If(IsNothing(condRem), Nothing, condRem.Id)
            'movto.MontoRealCond = If(IsNothing(condRem), Nothing, montoAcond)

            ' determinar los dias para el siguiente pago
            Dim numdias = 0
            If sol.PlazoAut = 2 Then
                numdias = 15
            End If
            movto.Id_CondRem = Nothing
            movto.MontoRealCond = Nothing
            movto.Id = clsMovto.folioSiguiente()
            'Dim transac As Boolean = clsMovto.insertar(movto)
            'If Not transac Then
            '    Utilerias.MensajeAlerta("Ocurrió un error al intentar registrar el pago!", Me, True)
            '    Return
            'End If

            ' crear el objeto pago para guardar en la tabla
            Dim pago As clsPago = New clsPago

            'Seccion calculo y validacion de recargos
            If hayRecargo Then

                pago.Atrasado = True
                pago.Condonado = False
                pago.Recargo = Cons_MontoRecargoGen
                'verificar si se condona
                If var_SeCondonaMoratorio Then
                    pago.Condonado = True
                    pago.MotivoCondonacion = txtMotCond.Text
                    pago.LoginCond = Session("UsuarioAutCond").Login

                End If

                'calcular y validar si el monto ingresado cumple con el monto de recargo y condonacion permitido

                Dim aux_interes = txtMontoInteres.Value
                Dim pagoRecargoCliente = Cons_pagoCliente - aux_interes
                Cons_RecarcoConDesc = Cons_MontoRecargoGen - TxtMontoRcrgGenerado.Value

                If Cons_RecarcoConDesc > pagoRecargoCliente Then
                    Utilerias.MensajeAlerta("El monto de la condonación es mayor a lo autorizado!", Me, True)
                    Return
                End If

                ' calcular cuanto del mora que se va a cubrir
                pagoClienteOperacion = pagoClienteOperacion - pagoRecargoCliente

                If pagoClienteOperacion >= 0 And Cons_RecarcoConDesc = 0 Then
                    pago.RecargoCobrado = 0
                ElseIf pagoClienteOperacion >= 0 Then
                    'var_RecargoPagado = pagoClienteOperacion * -1
                    pago.RecargoCobrado = pagoRecargoCliente 'cons_RecargosACubrir - var_RecargoPagado
                End If

            End If

                If tablaPagosAux.Count() = 0 Then
                saldoCap = credito.MontoPrestado
                saldoInteres = credito.Adeudo
                var_FecEsperada = credito.FecPrimPago
            ElseIf tablaPagosAux.Count() > 0 Then
                saldoCap = tablaPagosAux.Last().SaldoCapital
                saldoInteres = tablaPagosAux.Last().Saldo
                var_FecEsperada = tablaPagosAux.Last().FecEsperada
            End If

            ' Pagar intereses
            If Not cbAbonoCap.Checked And pagoClienteOperacion > 0 Then
                pagoClienteOperacion = pagoClienteOperacion - Cons_PagoInteresAPagar

                If pagoClienteOperacion > 0 Then
                    interesCubierto = Cons_PagoInteresAPagar

                    'si hay saldo este se abona al capital
                    abonoCapital = pagoClienteOperacion
                    montoPAgpLetra = Cons_PagoInteresAPagar
                    saldoCap = Math.Round(saldoCap - pagoClienteOperacion, 2)
                    saldoInteres = Math.Round(saldoCap * credito.Tasa, 2) + saldoCap

                Else
                    var_interesCubierto = pagoClienteOperacion * -1
                    interesCubierto = Cons_PagoInteresAPagar - var_interesCubierto

                    ' si es el primer pago, la fecha se obtiene de la tabla de credito
                    If tablaPagosAux.Count = 0 Then
                        var_FecEsperada = credito.FecPrimPago
                        'determinar los intereses a pagar para la siguiente letra
                        interesPagar = Math.Round(credito.MontoPrestado * credito.Tasa, 2)
                        saldoCap = credito.MontoPrestado
                        saldoInteres = interesPagar + credito.MontoPrestado
                    Else ' si no,la fecha se obtiene del ultimo registro de la lista de pagos
                        var_FecEsperada = tablaPagosAux.Last().FecProxPago
                        interesPagar = Math.Round(tablaPagosAux.Last().SaldoCapital * credito.Tasa, 2)
                        saldoInteres = interesPagar + tablaPagosAux.Last().SaldoCapital
                        saldoCap = tablaPagosAux.Last().SaldoCapital
                    End If
                End If

            Else
                interesCubierto = 0

            End If


            'validar si el pago afecta a capital
            If cbAbonoCap.Checked Then
                    abonoCapital = Cons_pagoCliente
                    montoPAgpLetra = Cons_pagoCliente
                saldoCap = saldoCap - Cons_pagoCliente
                proxPagoint = saldoCap * credito.Tasa
            End If

            Dim transac As Boolean = clsMovto.insertar(movto)
            If Not transac Then
                Utilerias.MensajeAlerta("Ocurrió un error al intentar registrar el pago!", Me, True)
                Return
            End If

            pago.Id_Credito = cbIdCredito.Text
            pago.Id_Cliente = cbClientes.SelectedItem.Value
            pago.Id_Sucursal = cbSuc.SelectedItem.Value
            pago.Id_Mov = movto.Id
            pago.NumPago = lblPago.Text
            pago.FecEsperada = var_FecEsperada
            pago.FecPago = System.DateTime.Now.Date
            pago.Login = Session("Usuario").Login
            pago.UltModif = System.DateTime.Now.Date
            pago.LoginModif = Session("Usuario").Login
            pago.FecProxPago = If(cbAbonoCap.Checked, var_FecEsperada, Utilerias.calcularFechaDePagoEmp(pago.FecEsperada, credito.Plazo))
            movto.FecProxPago = pago.FecProxPago
            movto.ProxPago = proxPagoint
            pago.Monto = montoPAgpLetra
            pago.AbonoIVA = Math.Round(interesCubierto * 0.16, 2)
            pago.AbonoInteres = Math.Round(interesCubierto - pago.AbonoIVA, 2)
            pago.AbonoCapital = abonoCapital
            pago.Saldo = If(cbAbonoCap.Checked, Utilerias.calcularNuevoSaldoEmp(credito, saldoCap), saldoInteres)
            pago.SaldoCapital = saldoCap

            pago.EsGarantia = False
            pago.EsAbonoCapital = cbAbonoCap.Checked


            transac = clsPago.insertar(pago)
            If Not transac Then
                Utilerias.MensajeAlerta("Ocurrió un error al intentar registrar el pago!", Me, True)
                Return
            End If

            generarTicket(pago, movto, False, credito)
            ResetControles()
            Session("Historial") = Nothing
        Catch ex As Exception

        End Try

    End Sub

    Private Sub generarTicket(pago As clsPago, movto As clsMovto, Liquidado As Boolean, credito As clsCredito)
        Try

            REM Validacion si es garantia liquida verificar por el pago correspodiente en pagos, si esta haz todo, si no, mensaje de que debe pagar

            'Dim tipoGar = clsSolicitud.obtener(credito.Id_Sol).TipoGarantia
            'Dim pago = clsPago.obtener(credito.Id, , True, True)
            'If ((tipoGar = 3 And pago.Monto > 0) Or (tipoGar = 5 And pago.Monto > 0)) Or (tipoGar <> 3 And tipoGar <> 5) Then

            Dim direccion = Utilerias.getDataTable("SELECT ('Calle ' + CLIENTES.Calle + ' #' +  CLIENTES.NumExt + ' ' + CLIENTES.Colonia + ', ' + CLIENTES.LocalidadDom + ', ' + CLIENTES.EstadoDom) AS Direccion FROM CLIENTES WHERE ID = " & credito.Id_Cliente).Rows.Item(0).Item(0)
            Dim telefono = Utilerias.getDataTable("SELECT celular FROM CLIENTES WHERE ID = " & credito.Id_Cliente).Rows.Item(0).Item(0)
            Dim cliente = Utilerias.getDataTable("SELECT (PrimNombre + ' ' + SegNombre + ' ' + PrimApellido + ' ' + SegApellido) AS cliente FROM CLIENTES WHERE ID = " & credito.Id_Cliente).Rows.Item(0).Item(0)
            Dim sucursal = Utilerias.getDataTable("SELECT Descripcion FROM CATSUCURSALES WHERE ID = " & pago.Id_Sucursal).Rows.Item(0).Item(0)
            Dim dirsuc = Utilerias.getDataTable("SELECT Direccion FROM CATSUCURSALES WHERE ID = " & pago.Id_Sucursal).Rows.Item(0).Item(0)

            'Dim tablaAmort = Utilerias.crearTablaDePagos(credito, Session("Historial"))
            'Dim j = 0
            'While j < tablaAmort.Rows.Count
            '    If IsDBNull(tablaAmort.Rows.Item(j).Item(2)) Then
            '        Exit While
            '    End If
            '    j += 1
            'End While

            'If IsNothing(Session("parametros")) = False Then
            '    Context.Response.Write("<script language='javascript'>window.open('dspVisor/dspTicketCompra.aspx','_blank');</script>")
            'End If

            If Moracondonado.Checked Then
                movto.Moratorio = 0.0
            End If

            Session("TipoTransaccion") = If(pago.EsAbonoCapital, "Pago a Capital", "Pago")
            clsMovto.actualizar(movto)

            Dim crReporteDocumento As New crTicketCompra

            crReporteDocumento.SetParameterValue("folmov", movto.Id)
            crReporteDocumento.SetParameterValue("credito", credito.Id)
            crReporteDocumento.SetParameterValue("cliente", cliente)
            crReporteDocumento.SetParameterValue("telefono", telefono)
            'crReporteDocumento.SetParameterValue("pago", abono)
            crReporteDocumento.SetParameterValue("proxPago", pago.FecProxPago)
            crReporteDocumento.SetParameterValue("fecProxPago", pago.FecProxPago)
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

    Private Sub ResetCargaDatos()
        TxtmontoRecargo.Value = ""
        esRecargo.Value = ""
        TxtMontoRcrgGenerado.Value = ""
        cbCondonacion.Checked = False
        interesACubrir.Value = ""
        cbClientes.DataBind()
        txtMotCond.Text = ""
        lblPago.Text = ""
        montoDeuda.Value = ""

        deFecCap.Value = ""

        cbSuc.SelectedIndex = -1
        cbAbonoCap.Checked = False
        cbAbonoCap.Visible = True

        panelRecargo.Visible = False
        PanelAbnCap.Visible = True
        Moracondonado.Checked = False
        Moracondonado.Visible = False
        condonacion.Visible = False
        monto.Value = ""
        btnRegistrar.Enabled = False
    End Sub

    Private Sub ResetControles()
        TxtmontoRecargo.Value = ""
        TxtMontoRcrgGenerado.Value = ""
        cbCondonacion.Checked = False
        esRecargo.Value = ""
        cbIdCredito.SelectedIndex = -1
        cbClientes.DataBind()
        cbClientes.SelectedIndex = -1
        lblPago.Text = ""
        montoDeuda.Value = ""
        txtMotCond.Text = ""
        deFecCap.Value = ""

        cbSuc.SelectedIndex = -1
        cbAbonoCap.Checked = False
        cbAbonoCap.Visible = True

        panelRecargo.Visible = False
        PanelAbnCap.Visible = True
        Moracondonado.Checked = False
        condonacion.Visible = False
        monto.Value = ""
        btnRegistrar.Enabled = False


    End Sub

    Protected Sub cbCondonacion_CheckedChanged(sender As Object, e As EventArgs) Handles cbCondonacion.CheckedChanged
        If cbCondonacion.Checked And esRecargo.Value Then
            ModalPopupExtender.Show()

        End If
    End Sub

    Protected Sub Cancelarcon_Click(sender As Object, e As EventArgs) Handles Cancelarcon.Click

    End Sub

    Protected Sub btnIngresar_Click(sender As Object, e As EventArgs) Handles btnIngresar.Click
        Dim conx = clsConexion.Open
        Dim cmd As New ADODB.Command
        Dim rst As New ADODB.Recordset
        cmd.ActiveConnection = conx
        Dim montoMoratActual As Double = 0
        Dim montoMoratdescuento As Double = 0

        Dim clUsuario As clsUsuario = clsUsuario.obtenerUsuarioCondEmp(txtLogin.Value)

        If IsNothing(clUsuario) = False AndAlso ValidaPassword(cmd, rst) AndAlso (clUsuario.PorcentajeCond > 0) Then
            REM validar si es de tipo administrador
            Session("UsuarioAutCond") = clUsuario
            'If IsNothing(Session("Recargo")) = False And Session("Recargo") > 0 Then
            montoMoratActual = TxtmontoRecargo.Value
            'TxtMontoRcrgGenerado.Value = montoMoratActual ' almacenara el recargo total

            ' recalcular el moratorio
            TxtMontoRcrgGenerado.Value = Math.Round(montoMoratActual * clUsuario.PorcentajeCond, 2)
            ' calcular el monto actual del moratorio


            'If montoMoratActual = 0 Then
            '    monto.Text = String.Format("{0:0.00}", 0)
            '    monto.Value = interesACubrir.Value
            'End If

            '' se asigna al control el nuevo valor del moratorio
            'TxtmontoRecargo.Value = String.Format("{0:0.00}", montoMoratActual)

            Moracondonado.Checked = True
            monto.Value = String.Format("{0:0.00}", interesACubrir.Value + TxtmontoRecargo.Value)
            txtMotCond.Enabled = True
            'End If
            txtLogin.Text = ""
            txtContra.Value = ""
            cbCondonacion.Checked = True
            ' panelRecargo.Visible = False
            Utilerias.MensajeConfirmacion("Se ha autorizado la condonación!", Me, True)
        Else
            cbCondonacion.Checked = False
            txtMotCond.Enabled = True
            txtMotCond.Text = ""
            txtLogin.Text = ""
            txtContra.Value = ""
            Utilerias.MensajeConfirmacion("Las credenciales que introdujo son incorrectas o no cuentan con los permisos para autorizar esta transacción!", Me, True)
            'cargarDatos()
        End If
        conx.Close()
    End Sub

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
End Class