Public Class ConfirmCredCarteraemp
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        If Not IsPostBack Then
            Dim usuario As clsUsuario = DirectCast(Session("Usuario"), clsUsuario)

            gvCredPreActivos.DataSource = clsCredito.getDataSource(usuario.IdSucursal)
            gvCredPreActivos.DataBind()
            btnRegistrar.Enabled = False

        End If



    End Sub

    Protected Sub gvCredPreActivos_SelectionChanged(sender As Object, e As EventArgs) Handles gvCredPreActivos.SelectionChanged
        Dim seleccion = gvCredPreActivos.GetSelectedFieldValues("SolID", "Cliente", "MontoAut", "Adeudo", "CredID", "Tasa", "Direccion", "NombRZ", "DirRZ")
        Dim usuario As clsUsuario = DirectCast(Session("Usuario"), clsUsuario)

        For Each item As Object() In seleccion

            SolID.Text = item(0).ToString
            nmbCli.Text = item(1).ToString
            monto.Text = item(2).ToString
            MontoDeuda.Text = item(3).ToString
            CredID.Text = item(4).ToString
            suc.Text = usuario.Sucursal
            tasaRefletra.Text = item(5).ToString
            Direccion.Text = item(6).ToString
            NombRZ.Text = item(7).ToString
            DirRZ.Text = item(8).ToString

            btnRegistrar.Enabled = True
        Next item

    End Sub

    Protected Sub btnRegistrar_Click(sender As Object, e As EventArgs) Handles btnRegistrar.Click
        Dim mensaje As mensajeTransaccion = Nothing
        Dim credito As clsCredito = Nothing
        Dim usuario As clsUsuario = DirectCast(Session("Usuario"), clsUsuario)
        Dim razon As clsRazonSocial = Nothing

        Dim sol As clsSolicitud = Nothing
        Try

            credito = New clsCredito()
            credito.Id = CredID.Text
            credito.Id_Sol = SolID.Text

            mensaje = clsCredito.CreditoACT(credito, "", usuario, clsCredito.Act_Credito.Act_Activar_Credito_Elite)
            If Not mensaje.codigoRespuesta.Equals("000000") Then
                Utilerias.MensajeAlerta("Codigo Respuesta: " + mensaje.codigoRespuesta + " Mensaje Respuesta: " + mensaje.mensajeRespuesta, Me, True)
                Return
            End If

            sol = clsSolicitud.obtener(credito.Id_Sol)
            ' consultar la razon social
            ' razon = clsRazonSocial.RazonSocialCon(sol.Id_Sucursal, clsRazonSocial.Con_RazonSoc.Con_Nombre_Razon_Soc)

            ' Generar Pagare
            Dim tasaReferencia = tasaRefletra.Text
            Dim tasaRef = tasaReferencia.ToString().Substring(0, tasaReferencia.ToString().IndexOfAny("%") + 1)
            Dim numeros As String() = tasaRef.Replace("%", "").Split(".")
            Dim tasaRefLetras = If(numeros.Length > 1, Utilerias.Num2Text(numeros(0)) + " PUNTO " + Utilerias.Num2Text(numeros(1)), Utilerias.Num2Text(numeros(0))) + " PORCIENTO"

            Dim crReporteDocumento As New crPagareCredEmp

            crReporteDocumento.SetParameterValue("deudaEnLetras", Utilerias.EnLetras(monto.Text).ToUpper)
            crReporteDocumento.SetParameterValue("finPrestamo", DateTime.Now.ToString("dd/MM/yyyy"))
            crReporteDocumento.SetParameterValue("deuda", monto.Text)
            crReporteDocumento.SetParameterValue("tasaRef", "4")

            crReporteDocumento.SetParameterValue("tasaRefLetras", tasaRefLetras)
            crReporteDocumento.SetParameterValue("cliente", nmbCli.Text)
            crReporteDocumento.SetParameterValue("razon", NombRZ.Text)
            crReporteDocumento.SetParameterValue("Calle", Direccion.Text)
            crReporteDocumento.SetParameterValue("DirRZ", DirRZ.Text)


            Dim fileName = "Pagare_" & credito.Id & ".PDF"
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

            'Utilerias.MensajeConfirmacion("Codigo Respuesta: " + mensaje.codigoRespuesta + " Mensaje Respuesta: " + mensaje.mensajeRespuesta, Me, True)
            VaciarCampos()
            gvCredPreActivos.DataSource = clsCredito.getDataSource(usuario.IdSucursal)
            gvCredPreActivos.DataBind()

            Context.Response.Write("<script language='javascript'>window.open('../Visor.aspx','_blank');</script>")

        Catch ex As Exception
            Utilerias.MensajeAlerta("Hubo un poblema al imprimir el ticket de pago! Error: " + ex.Message, Me, True)
        End Try
    End Sub


    Private Sub VaciarCampos()
        SolID.Text = ""
        nmbCli.Text = ""
        monto.Text = ""
        MontoDeuda.Text = ""
        CredID.Text = ""
        suc.Text = ""
        btnRegistrar.Enabled = False
        DirRZ.Text = ""
        Direccion.Text = ""
        NombRZ.Text = ""
    End Sub

End Class