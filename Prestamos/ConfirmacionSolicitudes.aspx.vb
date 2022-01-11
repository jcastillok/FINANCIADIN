Imports Ionic.Zip

Public Class ConfirmacionSolicitudes

    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            ddlRazon.Items.Add(New ListItem("RAPIPRES, S.A. DE C.V.", "RAPIPRES, S.A. DE C.V."))
        End If
    End Sub

    Protected Sub limpiarCampos() Handles btnLimpiar.Click
        lblId.Text = "--"
        deFecAut.Value = ""
        deFecDisp.Value = ""
        txtMontoAut.Value = ""
        cbPlazoAut.Value = ""
        seDurMeses.Value = ""
        sePagosAut.Value = ""
        seMontoDes.Value = ""
        ASPxGridView1.Selection.UnselectAll()
        llaveCredito.Value = ""
    End Sub

    Protected Sub Validar(sender As Object, e As EventArgs) Handles bttValidar.Click
        Dim llave As clsLlaveCredito = Nothing
        Dim llaveRequest As clsLlaveCredito = Nothing
        Dim mensaje As mensajeTransaccion = Nothing
        Dim tieneLlave As Boolean = False
        Dim cadenaLlave As String = ""
        Dim leyendaRetiro As String = ""

        Try
            If ASPxGridView1.Selection.Count > 0 Then

                Dim id = ASPxGridView1.GetSelectedFieldValues("Id")
                If clsCredito.tieneCredActivo(id.Item(0)) Then Utilerias.MensajeAlerta(String.Format("El cliente ya tiene un crédito activo."), Me, True) : Return
                Dim sobretasa = ASPxGridView1.GetSelectedFieldValues("Sobretasa").Item(0)
                Dim prestamo = ASPxGridView1.GetSelectedFieldValues("MontoAut").Item(0)
                Dim cliente = ASPxGridView1.GetSelectedFieldValues("Cliente")
                Dim tasaReferencia = ASPxGridView1.GetSelectedFieldValues("TasaRef").Item(0)
                Dim plazo = ASPxGridView1.GetSelectedFieldValues("Id_PlazoAut").Item(0)
                Dim numPagos = ASPxGridView1.GetSelectedFieldValues("NumPagosAut").Item(0)
                Dim autorizado = ASPxGridView1.GetSelectedFieldValues("Autorizado")
                Dim dineroEntregado = ASPxGridView1.GetSelectedFieldValues("DineroEntregado")
                Dim tasa = ASPxGridView1.GetSelectedFieldValues("Tasa").Item(0)
                'Dim incluyeIva = ASPxGridView1.GetSelectedFieldValues("IncluyeIVA")
                Dim tipoAmort = ASPxGridView1.GetSelectedFieldValues("Id_Amort").Item(0)

                'Dim duracionMeses = If(plazo.Item(0) = 1, numPagos.Item(0) / 4, If(plazo.Item(0) = 2, numPagos.Item(0), numPagos.Item(0) / 2))
                Dim duracionDias = If(plazo = 1, numPagos * 7, If(plazo = 3, numPagos * 30, numPagos * 15))

                tasa = (tasa * 12) / 365
                Dim deuda As Double

                If tipoAmort = 4 Then
                    deuda = Math.Abs(Pmt(tasa * duracionDias, numPagos, prestamo)) * numPagos
                Else
                    Dim interes = (prestamo * tasa) * (duracionDias)
                    deuda = Utilerias.Redondeo(prestamo + interes, 2)
                End If

                Dim finPrestamo = Utilerias.calcularFechaDePago(deFecDisp.Date, numPagos, plazo)
                Dim primPago = Utilerias.calcularFechaDePago(deFecDisp.Date, 1, plazo)
                Dim tasaRef = tasaReferencia.ToString().Substring(0, tasaReferencia.ToString().IndexOfAny("%") + 1)
                Dim numeros As String() = tasaRef.Replace("%", "").Split(".")
                Dim tasaRefLetras = If(numeros.Length > 1, Utilerias.Num2Text(numeros(0)) + " PUNTO " + Utilerias.Num2Text(numeros(1)), Utilerias.Num2Text(numeros(0))) + " PORCIENTO"
                Dim tasaAnual = (tasaRef.Substring(0, tasaRef.IndexOfAny("%")) * 12) & "%"

                llaveRequest = New clsLlaveCredito()
                llaveRequest.SolicitudID = id.Item(0)
                llave = clsLlaveCredito.LlaveCreditoCon(llaveRequest, 1)

                If Not IsNothing(llave) Then
                    tieneLlave = True
                End If


                Dim strUpdtSol = String.Format("UPDATE SOLICITUDES Set Autorizado = 1, LoginAut = '{0}' WHERE Id = {1}", Session("Usuario").Login, id.Item(0))

                Dim strSolicitud = String.Format("SELECT * FROM SOLICITUDES WHERE Id = {0}", id.Item(0))

                Dim strCliente = String.Format("SELECT Id, PrimNombre, SegNombre, PrimApellido, SegApellido, Login, Tipo, RFC, CURP, Calle, 
                                                       NumExt, NumInt, Colonia, LocalidadDom As Localidad, ReferenciaDom As Referencia, PaisDom As Pais, 
                                                       EstadoDom As Estado, MunicipioDom As Municipio, CodPostal, 
                                                       FecNac, TelefonoDom As Telefono, EMail, Estatus, FecRegistro, Celular, Nacionalidad, EstadoCivil, 
                                                       Ocupacion, IngresoMensual 
                                                       FROM CLIENTES WHERE Id = (SELECT Id_Cliente FROM SOLICITUDES WHERE Id={0})", id.Item(0))

                Dim strAval = String.Format("SELECT Id, PrimNombre, SegNombre, PrimApellido, SegApellido, Login, Tipo, RFC, CURP, Calle, 
                                                    NumExt, NumInt, Colonia, LocalidadDom As Localidad, ReferenciaDom As Referencia, PaisDom As Pais, 
                                                    EstadoDom As Estado, MunicipioDom As Municipio, CodPostal, 
                                                    FecNac, TelefonoDom As Telefono, EMail, Estatus, FecRegistro, Celular, Nacionalidad, EstadoCivil, 
                                                    Ocupacion, IngresoMensual 
                                                    FROM CLIENTES WHERE Id = (SELECT Id_Aval FROM SOLICITUDES WHERE Id={0})", id.Item(0))

                Dim strTalon = "SELECT REPLACE(CLIENTES.PrimNombre + ' ' + CLIENTES.SegNombre + ' ' + CLIENTES.PrimApellido + ' ' + CLIENTES.SegApellido, '  ', ' ') AS NombreCliente, 
                                CREDITOS.Id AS FolioCredito, CEILING(CREDITOS.Adeudo/CREDITOS.NumPagos) AS Pago, CATSUCURSALES.Descripcion AS Sucursal
                            FROM SOLICITUDES INNER JOIN
                                CREDITOS ON SOLICITUDES.Id = CREDITOS.Id_Sol LEFT OUTER JOIN
                                CLIENTES ON SOLICITUDES.Id_Cliente = CLIENTES.Id LEFT OUTER JOIN
                                CATSUCURSALES ON SOLICITUDES.Id_Sucursal = CATSUCURSALES.Id 
                            WHERE CREDITOS.estatusID= 1 AND SOLICITUDES.Id = " & id.Item(0)

                Dim datos As New DataSet
                Dim dt = Utilerias.getDataTable(strSolicitud)
                Dim dt2 = Utilerias.getDataTable(strCliente)
                Dim dt3 = Utilerias.getDataTable(strAval)
                Dim dt4 As New DataTable

                Dim directorio = System.IO.Path.GetTempPath & "Documentos" & cliente.Item(0).ToString.Replace(" ", "") & "\"
                Dim directorio2 = System.IO.Path.GetTempPath
                My.Computer.FileSystem.CreateDirectory(directorio)

                'If dineroEntregado.Item(0) = False Then
                '    dt4 = Utilerias.crearTablaDePagos(deFecDisp.Date, prestamo.Item(0), deuda, numPagos.Item(0), plazo(0))
                'Else
                '    dt4 = Utilerias.crearTablaDePagos(clsCredito.obtener(0, False, True, " Id = (SELECT ID FROM CREDITOS WHERE Id_Sol = " & id.Item(0) & ")"))
                'End If

                datos.Tables.Add(dt)
                datos.Tables(0).TableName = "SOLICITUDES"
                datos.Merge(dt)
                datos.Tables.Add(dt2)
                datos.Tables(1).TableName = "CLIENTES"
                datos.Merge(dt2)
                datos.Tables.Add(dt3)
                datos.Tables(2).TableName = "AVALES"
                datos.Merge(dt3)

                If IsDBNull(autorizado.Item(0)) Then

                    If clsCredito.insertar(id.Item(0), primPago, finPrestamo, deuda) Then
                        Utilerias.setUpdInsDel(strUpdtSol)
                    End If
                    'Else
                    '    Dim updt = "UPDATE CREDITOS SET FecInicio = convert(datetime,'" & deFecDisp.Date.ToShortDateString & "',103), FecPrimPago = convert(datetime,'" & primPago & "',103), FecUltPago = convert(datetime,'" & finPrestamo & "',103) WHERE Id_Sol = " & id.Item(0)
                    '    Utilerias.setUpdInsDel(updt)
                End If

                Dim credito = clsCredito.obtener(0, False, True, " C.Id = (SELECT ID FROM CREDITOS WHERE CREDITOS.estatusID= 1 AND Id_Sol = " & id.Item(0) & ")")
                Dim parametros As String = String.Format("{0}:={1}:={2}:={3}:={4}:={5}:={6}:={7}:={8}", deuda, Utilerias.EnLetras(deuda).ToUpper, cliente.Item(0).Trim, finPrestamo, tasaReferencia, tasaRefLetras, ddlRazon.SelectedItem.Text, tasaAnual, credito.Id)

                ' asociar el credito con la llave
                Dim clUsuario As clsUsuario
                clUsuario = DirectCast(Session("Usuario"), clsUsuario)
                If tieneLlave Then
                    llave.CreditoID = credito.Id
                    mensaje = clsLlaveCredito.LlaveCreditoACT(llave, clsLlaveCredito.Act_LlavesCredito.Asignar_Credito, clUsuario)
                    cadenaLlave = "Llave: " + llave.Llave
                    leyendaRetiro = "Recuerda que únicamente el titular del crédito podrá recibir el monto solicitado presentando una identificación oficial en ventanilla."
                End If


                Dim contrato = generarContrato(datos, parametros, cliente.Item(0).ToString.Replace(" ", ""))
                Dim pagare = generarPagare(datos, parametros, cliente.Item(0).ToString.Replace(" ", ""))

                dt4 = Utilerias.crearTablaDePagos(credito.FecInicio, prestamo, deuda, numPagos, plazo)
                datos.Tables.Add(dt4)
                datos.Tables(3).TableName = "TABLADEAMORTIZACION"
                datos.Merge(dt4)
                datos.Tables.RemoveAt(2)
                Dim tabla = generarTablaAmort(datos, parametros, cliente.Item(0).ToString.Replace(" ", ""))

                Dim datosTalon As New DataSet
                Dim dtTalon = Utilerias.getDataTable(strTalon)
                datosTalon.Tables.Add(dtTalon)
                datosTalon.Tables(0).TableName = "TALON"
                datosTalon.Merge(dtTalon)
                Dim talon = generarTalon(datosTalon, cliente.Item(0).ToString.Replace(" ", ""), cadenaLlave, leyendaRetiro)


                Dim zipFile = "Documentos " & cliente.Item(0).ToString.Replace(" ", "") & ".zip"
                Using zip As ZipFile = New _
                   ZipFile(directorio2)
                    ' Add the file to the Zip archive's root folder
                    zip.AddDirectory(directorio)

                    ' Save the Zip file.
                    zip.Save(directorio & zipFile)


                    Response.Clear()
                    Response.BufferOutput = False
                    Response.ContentType = "application/zip"
                    Response.AddHeader("Content-disposition", "attachment; filename=" & zipFile)
                    'Response.WriteFile(directorio & zipFile)
                    zip.Save(Response.OutputStream)
                    System.IO.Directory.Delete(directorio, True)
                    ' Response.Close()
                    Response.End()
                    'Response.Flush()
                    'Response.Close()
                End Using



                'If IsNothing(Session("ContratoDataSet")) = False Then
                '    Context.Response.Write("<script language='javascript'>window.open('dspVisor/dspPagare.aspx','_blank');</script>")
                '    Context.Response.Write("<script language='javascript'>window.open('dspVisor/dspContrato.aspx','_blank');</script>")
                '    Context.Response.Write("<script language='javascript'>window.open('dspVisor/dspTablaAmort.aspx','_blank');</script>")
                '    Context.Response.Write("<script language='javascript'>window.open('dspVisor/dspTalon.aspx','_blank');</script>")
                'End If

                Utilerias.MensajeConfirmacion("Se ha confirmado la solicitud y guardado el crédito!", Me, True)
                limpiarCampos()
                ASPxGridView1.DataBind()

            Else
                Utilerias.MensajeAlerta("Debe seleccionar una solicitud primero", Me, True)
            End If
        Catch ex As Exception
            Utilerias.MensajeAlerta("¡Algo salió mal! Detalles: " & ex.Message, Me, True)
            limpiarCampos()
            ASPxGridView1.DataBind()
        End Try


    End Sub

    Protected Sub seDurMeses_ValueChanged(sender As Object, e As EventArgs) Handles seDurMeses.ValueChanged
        If cbPlazoAut.Text = "" Then
            Utilerias.MensajeAlerta("Debe primero elegir un plazo", Me, True)
        ElseIf cbPlazoAut.Text = "Semanal" Then
            sePagosAut.Value = seDurMeses.Value * 4
        ElseIf cbPlazoAut.Text = "Quincenal" Then
            sePagosAut.Value = seDurMeses.Value * 2
        ElseIf cbPlazoAut.Text = "Mensual" Then
            sePagosAut.Value = seDurMeses.Value
        End If
    End Sub

    Protected Sub btnGuardar_Click(sender As Object, e As EventArgs) Handles btnGuardar.Click
        Dim mensaje As mensajeTransaccion = Nothing
        Dim llaveCreditoO As clsLlaveCredito = Nothing
        Dim usuario As clsUsuario = Nothing

        Dim camposVacios As String = ""

        If lblId.Text = "--" Then Utilerias.MensajeAlerta("Debe seleccionar una solicitud para poder guardar datos", Me, True) : Return
        If deFecAut.Text.Trim = "" Then camposVacios = camposVacios & "Fecha de Autorización, "
        If deFecDisp.Text.Trim = "" Then camposVacios = camposVacios & "Fecha de Disposición, "
        If txtMontoAut.Text.Trim = "" Then camposVacios = camposVacios & "Monto Autorizado, "
        If cbPlazoAut.Text.Trim = "" Then camposVacios = camposVacios & "Plazo Autorizado, "
        If seDurMeses.Text.Trim = "" Then camposVacios = camposVacios & "Duración en Meses, "
        If sePagosAut.Text.Trim = "" Then camposVacios = camposVacios & "Pagos Autorizados, "

        If camposVacios <> "" Then
            camposVacios = camposVacios.Remove(camposVacios.LastIndexOf(","), 2)
            If camposVacios.Contains(",") Then
                Utilerias.MensajeAlerta(String.Format("Los campos <b>{0}</b> no pueden estar vacios.", camposVacios), Me, True)
            Else
                Utilerias.MensajeAlerta(String.Format("El campo <b>{0}</b> no puede estar vacío.", camposVacios), Me, True)
            End If
            Return
        End If

        Try
            Dim strUpd = "UPDATE SOLICITUDES SET FecAutorizado = convert(datetime,'" & deFecAut.Date.ToShortDateString & "',103)," _
            & " FecDisposicion =  convert(datetime,'" & deFecDisp.Date.ToShortDateString & "',103)," _
            & " MontoAut = " & txtMontoAut.Text & ", PlazoAut = " & cbPlazoAut.Value & ", NumPagosAut = " & sePagosAut.Text & "" _
            & " WHERE Id = " & lblId.Text & ""

            Utilerias.setUpdInsDel(strUpd)

            ' RELACIONAR LA SOLICTUD CON LA LLAVE

            If Not IsNothing(llaveCredito.Value) Then

                llaveCreditoO = New clsLlaveCredito()
                llaveCreditoO.SolicitudID = lblId.Text
                llaveCreditoO.Llave = llaveCredito.Value
                usuario = Session("Usuario")

                mensaje = clsLlaveCredito.LlaveCreditoALT(llaveCreditoO, usuario)

                If Not mensaje.codigoRespuesta.Equals("000000") Then
                    Utilerias.MensajeAlerta("OCURRIO UN PROBLEMA! CODIGO: " + mensaje.codigoRespuesta + "\n MENSAJE RESPUESTA: " + mensaje.mensajeRespuesta, Me, True)

                End If
                Utilerias.MensajeConfirmacion("CODIGO: " + mensaje.codigoRespuesta + "\n MENSAJE RESPUESTA: " + mensaje.mensajeRespuesta, Me, True)


            End If



            ASPxGridView1.DataBind()
            limpiarCampos()
            Utilerias.MensajeConfirmacion("Se ha actualizado la información de la solicitud!", Me, True)
        Catch ex As Exception
            Utilerias.MensajeAlerta("¡Parece que ha ocurrido un problema! " & ex.Message, Me, True)
        End Try

    End Sub

    Protected Sub ASPxGridView1_SelectionChanged(sender As Object, e As EventArgs) Handles ASPxGridView1.SelectionChanged
        If ASPxGridView1.Selection.Count > 0 Then

            Dim seleccion = ASPxGridView1.GetSelectedFieldValues("Id", "FecAutorizado", "FecDisposicion", "MontoAut", "PlazoAut",
                                                                 "NumPagosAut", "MontoLiquid", "MontoDesembolsado",
                                                                 "LiquidaAnterior", "Autorizado", "Llave", "LlaveCreditoID")
            For Each item As Object() In seleccion

                lblId.Text = item(0).ToString

                If IsDBNull(item(9)) = False Then
                    If item(9) Then
                        deFecAut.Value = item(1)
                        deFecDisp.Value = item(2)
                    End If
                Else
                    deFecAut.Date = System.DateTime.Now.Date
                    deFecDisp.Value = System.DateTime.Now.Date
                End If

                txtMontoAut.Value = item(3)
                cbPlazoAut.Value = item(4)
                sePagosAut.Value = item(5)
                'seMontoDes.Value = item(7)

                If item(8) Then
                    seMontoDes.Value = item(3) - item(6)
                Else
                    seMontoDes.Value = item(3)
                End If

                If cbPlazoAut.Text = "Semanal" Then
                    seDurMeses.Value = seDurMeses.Value / 4
                ElseIf cbPlazoAut.Text = "Quincenal" Then
                    seDurMeses.Value = seDurMeses.Value / 2
                ElseIf cbPlazoAut.Text = "Mensual" Then
                    seDurMeses.Value = seDurMeses.Value
                End If
                llaveCredito.Value = item(10)
                txtLlaveCrediID.Value = item(11)
            Next item
        End If
    End Sub

    Protected Sub btnRechazar_Click(sender As Object, e As EventArgs) Handles btnRechazar.Click
        Dim mensaje As mensajeTransaccion = Nothing
        If lblId.Text = "--" Then Utilerias.MensajeAlerta("Debe seleccionar una solicitud primero!", Me, True) : Return
        Dim clUsuario As clsUsuario
        clUsuario = DirectCast(Session("Usuario"), clsUsuario)
        Try
            Dim autorizado = ASPxGridView1.GetSelectedFieldValues("Autorizado").Item(0)
            autorizado = If(IsDBNull(autorizado), False, autorizado)
            If autorizado Then
                Utilerias.MensajeAlerta("Esta solicitud ya fue marcada como autorizada, no puede rechazarse!", Me, True)
            Else
                Dim strUpd = "UPDATE SOLICITUDES SET Autorizado = 0" _
                        & " WHERE Id = " & lblId.Text & ""
                Dim llaveCredito As clsLlaveCredito = New clsLlaveCredito
                llaveCredito.LlaveCreditoID = txtLlaveCrediID.Value
                mensaje = clsLlaveCredito.LlaveCreditoACT(llaveCredito, clsLlaveCredito.Act_LlavesCredito.Baja_Llave_Financiera, clUsuario)


                Utilerias.setUpdInsDel(strUpd)
                ASPxGridView1.DataBind()
                limpiarCampos()
                Utilerias.MensajeConfirmacion("Se ha marcado la solicitud como rechazada!", Me, True)
            End If

        Catch ex As Exception
            Utilerias.MensajeAlerta("No se ha podido marcar la solicitud como rechazada!", Me, True)
        End Try
    End Sub

    Private Function generarContrato(datos As DataSet, parametros As String, cliente As String) As String
        Try

            Dim crReporteDocumento As New crContrato
            Dim variables As Array = Split(parametros, ":=")

            crReporteDocumento.SetDataSource(datos)

            crReporteDocumento.SetParameterValue("deuda", variables(0))
            crReporteDocumento.SetParameterValue("cliente", variables(2))
            crReporteDocumento.SetParameterValue("finPrestamo", variables(3))
            crReporteDocumento.SetParameterValue("tasaRef", variables(4))
            crReporteDocumento.SetParameterValue("tasaAnual", variables(7))
            'crReporteDocumento.SetParameterValue("credito", variables(8))


            Dim file As String = System.IO.Path.GetTempPath & "Documentos" & cliente & "\Contrato" & variables(2).ToString.Replace(" ", "") & ".PDF"
            Dim diskOpts As New CrystalDecisions.Shared.DiskFileDestinationOptions
            diskOpts.DiskFileName = file
            crReporteDocumento.ExportOptions.DestinationOptions = diskOpts
            crReporteDocumento.ExportOptions.ExportFormatType = CrystalDecisions.Shared.ExportFormatType.PortableDocFormat
            crReporteDocumento.ExportOptions.ExportDestinationType = CrystalDecisions.Shared.ExportDestinationType.DiskFile
            crReporteDocumento.Export()
            crReporteDocumento.Close()
            crReporteDocumento.Dispose()
            GC.Collect()

            Return file

            'Response.Clear()
            'Response.ContentType = "application/pdf"
            'Response.AddHeader("Content-disposition", "attachment; filename=Contrato" & variables(2).ToString.Replace(" ", "") & ".PDF")
            'Response.WriteFile(file)
            'Response.Flush()
            'Response.Close()

            'System.IO.File.Delete(file)

        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Private Function generarTablaAmort(datos As DataSet, parametros As String, cliente As String) As String
        Try

            Dim crReporteDocumento As New crTablaAmort
            Dim variables As Array = Split(parametros, ":=")

            crReporteDocumento.SetDataSource(datos)

            crReporteDocumento.SetParameterValue("deuda", variables(0)) '0
            crReporteDocumento.SetParameterValue("finPrestamo", variables(3)) '3
            crReporteDocumento.SetParameterValue("tasaRef", variables(4)) '4
            crReporteDocumento.SetParameterValue("tasaAnual", variables(7).ToString.Replace("%", "")) '7
            crReporteDocumento.SetParameterValue("credito", variables(8)) '8

            Dim file As String = System.IO.Path.GetTempPath & "Documentos" & cliente & "\TablaAmortizacion" & variables(2).ToString.Replace(" ", "") & ".PDF"
            Dim diskOpts As New CrystalDecisions.Shared.DiskFileDestinationOptions
            diskOpts.DiskFileName = file
            crReporteDocumento.ExportOptions.DestinationOptions = diskOpts
            crReporteDocumento.ExportOptions.ExportFormatType = CrystalDecisions.Shared.ExportFormatType.PortableDocFormat
            crReporteDocumento.ExportOptions.ExportDestinationType = CrystalDecisions.Shared.ExportDestinationType.DiskFile
            crReporteDocumento.Export()
            crReporteDocumento.Close()
            crReporteDocumento.Dispose()
            GC.Collect()

            Return file

            'Response.Clear()
            'Response.ContentType = "application/pdf"
            'Response.AddHeader("Content-disposition", "attachment; filename=Contrato" & variables(2).ToString.Replace(" ", "") & ".PDF")
            'Response.WriteFile(file)
            'Response.Flush()
            'Response.Close()

            'System.IO.File.Delete(file)

        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Private Function generarPagare(datos As DataSet, parametros As String, cliente As String) As String
        Try

            Dim crReporteDocumento As New crPagare
            Dim variables As Array = Split(parametros, ":=")

            crReporteDocumento.SetDataSource(datos)

            crReporteDocumento.SetParameterValue("deuda", variables(0))
            crReporteDocumento.SetParameterValue("deudaEnletras", variables(1))
            crReporteDocumento.SetParameterValue("cliente", variables(2))
            crReporteDocumento.SetParameterValue("finPrestamo", variables(3))
            crReporteDocumento.SetParameterValue("tasaRef", variables(4))
            crReporteDocumento.SetParameterValue("tasaRefLetras", variables(5))
            crReporteDocumento.SetParameterValue("razon", variables(6))

            Dim file As String = System.IO.Path.GetTempPath & "Documentos" & cliente & "\Pagare" & variables(2).ToString.Replace(" ", "") & ".PDF"
            Dim diskOpts As New CrystalDecisions.Shared.DiskFileDestinationOptions
            diskOpts.DiskFileName = file
            crReporteDocumento.ExportOptions.DestinationOptions = diskOpts
            crReporteDocumento.ExportOptions.ExportFormatType = CrystalDecisions.Shared.ExportFormatType.PortableDocFormat
            crReporteDocumento.ExportOptions.ExportDestinationType = CrystalDecisions.Shared.ExportDestinationType.DiskFile
            crReporteDocumento.Export()
            crReporteDocumento.Close()
            crReporteDocumento.Dispose()
            GC.Collect()

            Return file

            'Response.Clear()
            'Response.ContentType = "application/pdf"
            'Response.AddHeader("Content-disposition", "attachment; filename=Pagare" & variables(2).ToString.Replace(" ", "") & ".PDF")
            'Response.WriteFile(file)
            'Response.Flush()
            'Response.Close()

            'System.IO.File.Delete(file)


        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Private Function generarTalon(datos As DataSet, cliente As String, llave As String, leyendaRetiro As String) As String
        Try

            Dim crReporteDocumento As New crTalon
            crReporteDocumento.SetDataSource(DirectCast(datos, System.Data.DataSet))

            crReporteDocumento.SetParameterValue("Llave", llave)
            crReporteDocumento.SetParameterValue("LeyendaRetiro", leyendaRetiro)

            Dim file As String = System.IO.Path.GetTempPath & "Documentos" & cliente & "\Talon" & cliente & ".PDF"
            Dim diskOpts As New CrystalDecisions.Shared.DiskFileDestinationOptions
            diskOpts.DiskFileName = file
            crReporteDocumento.ExportOptions.DestinationOptions = diskOpts
            crReporteDocumento.ExportOptions.ExportFormatType = CrystalDecisions.Shared.ExportFormatType.PortableDocFormat
            crReporteDocumento.ExportOptions.ExportDestinationType = CrystalDecisions.Shared.ExportDestinationType.DiskFile
            crReporteDocumento.Export()
            crReporteDocumento.Close()
            crReporteDocumento.Dispose()
            GC.Collect()

            Return file

            'Response.Clear()
            'Response.ContentType = "application/pdf"
            'Response.AddHeader("Content-disposition", "attachment; filename=Talon.PDF")
            'Response.WriteFile(file)
            'Response.Flush()
            'Response.Close()

            'System.IO.File.Delete(file)

        Catch ex As Exception
            Throw ex
        End Try
    End Function

End Class