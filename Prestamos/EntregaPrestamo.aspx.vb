Public Class EntregaPrestamo

    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then

        End If
    End Sub

    Protected Sub Validar(sender As Object, e As EventArgs) Handles bttValidar.Click

        Try
            If ASPxGridView1.Selection.Count > 0 Then
                REM Validacion si es garantia liquida verificar por el pago correspodiente en pagos, si esta haz todo, si no, mensaje de que debe pagar
                Dim credito = ASPxGridView1.GetSelectedFieldValues("Id_Credito")
                Dim tipoGar = ASPxGridView1.GetSelectedFieldValues("TipoGarantia")
                If ((tipoGar.Item(0) = 3 Or tipoGar.Item(0) = 5) And clsPago.pagoBase(credito.Item(0))) Or (tipoGar.Item(0) <> 3 And tipoGar.Item(0) <> 5) Then
                    Dim id = ASPxGridView1.GetSelectedFieldValues("Id")
                    Dim idCliente = ASPxGridView1.GetSelectedFieldValues("Id_Cliente")
                    Dim direccion = ASPxGridView1.GetSelectedFieldValues("Direccion")
                    Dim telefono = Utilerias.getDataTable("SELECT celular FROM CLIENTES WHERE ID = " & idCliente.Item(0)).Rows.Item(0).Item(0)
                    Dim prestamo = ASPxGridView1.GetSelectedFieldValues("MontoAut")
                    Dim cliente = ASPxGridView1.GetSelectedFieldValues("Cliente")
                    Dim fechaDisp = ASPxGridView1.GetSelectedFieldValues("FecDisposicion")
                    Dim plazo = ASPxGridView1.GetSelectedFieldValues("PlazoAut")
                    Dim numPagos = ASPxGridView1.GetSelectedFieldValues("NumPagosAut")
                    Dim sobretasa = ASPxGridView1.GetSelectedFieldValues("Sobretasa")
                    Dim tasaReferencia = ASPxGridView1.GetSelectedFieldValues("TasaRef")

                    REM Semanal = 1, Quincenal = 2, Mensual = 3
                    'Dim duracionMeses = If(plazo.Item(0) = 1, numPagos.Item(0) / 4, If(plazo.Item(0) = 2, numPagos.Item(0), numPagos.Item(0) / 2))
                    Dim duracionDias = If(plazo.Item(0) = 1, numPagos.Item(0) * 7, If(plazo.Item(0) = 2, numPagos.Item(0) * 30, numPagos.Item(0) * 15))
                    Dim deuda = Utilerias.Redondeo(prestamo.Item(0) + ((prestamo.Item(0) * ((sobretasa.Item(0) / 365) / 100)) * (duracionDias)), 2)
                    Dim finPrestamo = Utilerias.calcularFechaDePago(fechaDisp.Item(0), numPagos.Item(0), plazo.Item(0))
                    Dim tasaRef = tasaReferencia.Item(0).ToString().Substring(0, tasaReferencia.Item(0).ToString().IndexOfAny("%") + 1)
                    Dim plazoDes = String.Format("{0} {1}", numPagos.Item(0), If(plazo.Item(0) = 1, "Semanas", If(plazo.Item(0) = 3, "Meses", "Quincenas")))

                    Dim parametros As String = String.Format("{0}:={1}:={2}:={3}:={4}:={5}:={6}:={7}", cliente.Item(0), direccion.Item(0), telefono, prestamo.Item(0), plazoDes, deuda, finPrestamo, "Entrega de Efectivo")
                    Session("parametros") = parametros

                    'If IsNothing(Session("parametros")) = False Then
                    '    Context.Response.Write("<script language='javascript'>window.open('dspVisor/dspEntregaDinero.aspx','_blank');</script>")
                    'End If

                    generarTicket()

                    Dim strUpdt = String.Format("UPDATE SOLICITUDES SET DineroEntregado = 1 WHERE Id = {0}", id.Item(0))
                    Utilerias.setUpdInsDel(strUpdt)
                    ASPxGridView1.DataBind()


                    Utilerias.MensajeConfirmacion("Se ha marcado este prestamo como entregado", Me, True)
                Else
                    Utilerias.MensajeAlerta("Los prestamos con garantia deben hacer un poco anterior a recibir el prestamo!", Me, True)
                End If
            Else
                Utilerias.MensajeAlerta("Debe seleccionar una solicitud primero", Me, True)
            End If
        Catch ex As Exception

        End Try

    End Sub

    Private Sub generarTicket()

        Dim crReporteDocumento As New crEntregaDinero

        Dim variables As Array = Split(Session("parametros"), ":=")

        crReporteDocumento.SetParameterValue("cliente", variables(0))
        crReporteDocumento.SetParameterValue("direccion", variables(1))
        crReporteDocumento.SetParameterValue("telefono", variables(2))
        crReporteDocumento.SetParameterValue("prestamo", variables(3))
        crReporteDocumento.SetParameterValue("plazo", variables(4))
        crReporteDocumento.SetParameterValue("adeudo", variables(5))
        crReporteDocumento.SetParameterValue("fechaFinal", variables(6))
        crReporteDocumento.SetParameterValue("tipoTicket", variables(7))

        Dim fileName = "Comprobante" & variables(0).ToString.Replace(" ", "") & ".PDF"
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
        'Response.AddHeader("Content-disposition", "attachment; filename=Comprobante" & variables(0).ToString.Replace(" ", "") & ".PDF")
        'Response.WriteFile(file)
        'Response.Flush()
        'Response.Close()
        Context.Response.Write("<script language='javascript'>window.open('../Visor.aspx','_blank');</script>")

    End Sub

End Class