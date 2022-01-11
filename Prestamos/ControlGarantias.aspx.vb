Public Class ControlGarantias
    Inherits System.Web.UI.Page
    Dim clienteGarantiaDAO As ClienteGarantiaModelView = Nothing

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        If Not Page.IsPostBack Then
            cbClientesGar.DataSource = clsGarantia.getDataSource(clsGarantia.Garantia_Lista.Lista_gral)
            cbClientesGar.DataBind()

            panelInfoCicloNvo.Visible = False
            panelFolioCredito.Visible = False
            panelGarantiaNvo.Visible = False
            panelSucursal.Visible = False

        End If


    End Sub

    ''' <summary>
    ''' Consultar el nvo y antiguo credito del cliente
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Protected Sub cbClientesGar_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cbClientesGar.SelectedIndexChanged
        Dim clienteSeleccionado As Int32
        Dim clienteGarResponse As ClienteGarantiaModelView = Nothing
        Try
            clienteSeleccionado = Convert.ToInt32(cbClientesGar.SelectedItem.Value)
            clienteGarResponse = CargarDatosCred(clienteSeleccionado, 1)

            CreditoId.Text = clienteGarResponse.CredLiqID
            MontoGarantia.Text = clienteGarResponse.MontoGarCredLiq
            FechaLiquidacion.Text = clienteGarResponse.FechaLiq
            If clienteGarResponse.GarantiaApl = True Then
                btnDevolver.Enabled = False
                BtnNvoCiclo.Enabled = False
            End If

            clienteGarResponse = CargarDatosCred(clienteSeleccionado, 2)
            ' si el cliente tiene  un nuevo credito se habilitan los campos
            If clienteGarResponse.CredNvoID > 0 Then

                panelInfoCicloNvo.Visible = True
                panelFolioCredito.Visible = True
                panelGarantiaNvo.Visible = True
                panelSucursal.Visible = True

                CreditoIDNvo.Text = clienteGarResponse.CredNvoID
                MontoGarNvo.Text = clienteGarResponse.MontoGarCredNvo
                txtSucursalID.Text = clienteGarResponse.SucursalID
                txtSucursal.Text = clienteGarResponse.Sucursal
                txtFecPrimPago.Text = clienteGarResponse.FecPrimPago
                txtMontoCreditoNvo.Text = clienteGarResponse.MontoCred
                txtSaldo.Text = clienteGarResponse.Adeudo

                BtnNvoCiclo.Enabled = True

            End If


        Catch ex As Exception

        End Try

    End Sub

    Private Function CargarDatosCred(clienteSeleccionado As Int32, numCon As Int32) As ClienteGarantiaModelView
        Dim clienteGarResponse As ClienteGarantiaModelView = Nothing
        Try
            Select Case numCon
                Case 1
                    clienteGarResponse = ClienteGarantiaModelView.ClienteGarantiaCon(clienteSeleccionado, ClienteGarantiaModelView.Con_ClienteGarantia.Con_Credito_Liquidado)
                    Return clienteGarResponse
                Case 2
                    clienteGarResponse = ClienteGarantiaModelView.ClienteGarCredNvoCon(clienteSeleccionado, ClienteGarantiaModelView.Con_ClienteGarantia.Con_Credito_Nuevo)

            End Select


        Catch ex As Exception
            If IsNothing(clienteGarResponse) Then
                clienteGarResponse = New ClienteGarantiaModelView
                clienteGarResponse.CodigoRespuesta = "ERROR-100001"
                clienteGarResponse.MensajeRespuesta = "Problema en la consulta de garantia " + ex.Message
            End If

        End Try
        Return clienteGarResponse

    End Function

    Private Sub GuardarPagoGarantia()

        Try
            Dim pago As clsPago = New clsPago
            Dim movto As clsMovto = New clsMovto

            pago.Id_Credito = CreditoIDNvo.Text
            pago.Id_Cliente = cbClientesGar.SelectedItem.Value
            pago.Id_Sucursal = txtSucursalID.Text
            pago.NumPago = 0
            pago.FecEsperada = deFecDep.Value.Date
            'If IsNothing(pagoCancelado) Then
            pago.FecPago = deFecDep.Value.Date
            pago.Atrasado = False
            pago.Login = Session("Usuario").Login
            'Else
            '    pago.FecPago = pagoCancelado.FecPago
            '    pago.Atrasado = pagoCancelado.Atrasado
            '    pago.Login = pagoCancelado.Login
            'End If
            pago.UltModif = deFecDep.Value
            pago.LoginModif = Session("Usuario").Login
            pago.FecProxPago = txtFecPrimPago.Text
            pago.Monto = MontoGarantia.Text
            pago.AbonoInteres = 0.0

            pago.AbonoIVA = 0.0

            pago.AbonoCapital = 0
            pago.Saldo = txtMontoCreditoNvo.Text
            pago.SaldoCapital = txtSaldo.Text
            pago.Recargo = 0
            pago.RecargoCobrado = 0
            pago.EsGarantia = True
            pago.EsAbonoCapital = False
            pago.Condonado = Nothing

            pago.MotivoCondonacion = Nothing
            pago.LoginCond = Nothing

            movto.Id = clsMovto.folioSiguiente()
            movto.Id_Credito = CreditoIDNvo.Text
            movto.FecMov = deFecDep.Value.Date
            movto.Pago = MontoGarantia.Text
            movto.Moratorio = 0
            movto.Login = Session("Usuario").Login
            movto.Id_CondRem = Nothing
            movto.MontoRealCond = Nothing
            movto.ProxPago = clsCredito.ConsultarPrimPago(CreditoIDNvo.Text)
            movto.FecProxPago = clsCredito.ConsultarFechaPrimPago(CreditoIDNvo.Text)

            If Not clsMovto.insertar(movto) Then

                Utilerias.MensajeAlerta("Error al dar de alta el pago de la garantía", Me, True)
                Exit Sub
            End If
            pago.Id_Mov = movto.Id
            clsPago.insertar(pago)



        Catch ex As Exception

        End Try

    End Sub

    Protected Sub BtnNvoCiclo_Click(sender As Object, e As EventArgs) Handles BtnNvoCiclo.Click
        Dim usuario As clsUsuario = Nothing
        usuario = DirectCast(Session("Usuario"), clsUsuario)

        Try

            If deFecDep.Text = "" Then
                Utilerias.MensajeAlerta("Debe de seleciconar una fecha", Me, True)
            End If


            GuardarPagoGarantia()
        Catch ex As Exception
            Utilerias.MensajeAlerta("Ocurrió un poblema al guardar el pago! Error: " + ex.Message, Me, True)
        End Try

    End Sub

    Protected Sub btnDevolver_Click(sender As Object, e As EventArgs) Handles btnDevolver.Click

    End Sub

    Protected Sub btnCancelar_Click(sender As Object, e As EventArgs) Handles btnCancelar.Click

    End Sub
End Class