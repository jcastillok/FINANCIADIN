Imports Ionic.Zip

Public Class CapturaSolicitudes

    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
        End If
    End Sub

    Protected Sub limpiarCampos() Handles btnLimpiar.Click
        lblId.Text = "--"
        cbClientes.Value = ""
        deFecSol.Value = ""
        txtMontoSol.Value = ""
        cbPlazoSol.Value = ""
        sePagosSol.Value = ""
        cbALiquidar.Text = ""
        txtMontoALiquidar.Value = ""
        cbTasaRef.Value = ""
        cbTipAmort.Text = ""
        txtSobretasa.Value = ""
        cbTasaMora.Text = ""
        txtImpuesto.Text = ""
        cbTipGar.Value = ""
        txtValorGar.Value = ""
        txtIngCli.Value = ""
        txtMontoDes.Value = ""
        cbSuc.Value = ""
        cbAval.Value = ""
        cbAsesores.Value = ""
        Session.Remove("Id_Cliente")
        Session.Remove("Id_Aval")
        Session.Remove("FecAutorizado")
        Session.Remove("FecDisposicion")
        gvSolicitudes.Selection.UnselectAll()
    End Sub

    Protected Sub btnGuardar_Click(sender As Object, e As EventArgs) Handles btnGuardar.Click

        Dim camposVacios As String = ""

        If cbClientes.Text.Trim = "" And cbALiquidar.Text.Trim = "" Then camposVacios = camposVacios & "Cliente, "
        If deFecSol.Text.Trim = "" Then camposVacios = camposVacios & "Fecha de Solicitud, "
        If txtMontoSol.Text.Trim = "" Then camposVacios = camposVacios & "Monto Solicitado, "
        If cbPlazoSol.Text.Trim = "" Then camposVacios = camposVacios & "Frecuencia de Pagos, "
        If sePagosSol.Text.Trim = "" Then camposVacios = camposVacios & "Pagos Solicitados, "
        If cbTasaRef.Text.Trim = "" Then camposVacios = camposVacios & "Tasa de Referencia, "
        If cbTipAmort.Text.Trim = "" Then camposVacios = camposVacios & "Tipo de Amortización, "
        If txtSobretasa.Text.Trim = "" Then camposVacios = camposVacios & "Sobretasa, "
        If cbTasaMora.Text.Trim = "" Then camposVacios = camposVacios & "Tasa Moratoria, "
        If txtImpuesto.Text.Trim = "" Then camposVacios = camposVacios & "Impuesto, "
        If cbTipGar.Text.Trim = "" Then camposVacios = camposVacios & "Tipo de Garantía, "
        If txtValorGar.Text.Trim = "" Then camposVacios = camposVacios & "Valor de Garantía, "
        If cbSuc.Text.Trim = "" Then camposVacios = camposVacios & "Sucursal, "
        If cbAval.Text.Trim = "" And IsNothing(Session("Id_Aval")) And cbTipAmort.Value <> 4 Then camposVacios = camposVacios & "Aval, "
        If cbAsesores.Text.Trim = "" Then camposVacios = camposVacios & "Asesor, "
        If txtTipProd.Text.Trim = "" Then camposVacios = camposVacios & "Tipo de Producto, "
        If txtValorGar.Text.Trim = "" Then camposVacios = camposVacios & "Valor de Garantía, "



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
            Dim aval = Utilerias.getDataTable("SELECT Id_Aval FROM CLIENTES WHERE Id = " & If(IsNothing(Session("Id_Cliente")) = False, Session("Id_Cliente"), cbClientes.Value))

            If clsSolicitud.tieneSolActiva(If(IsNothing(Session("Id_Cliente")) = False, Session("Id_Cliente"), cbClientes.Value)) Then Utilerias.MensajeAlerta(String.Format("El cliente ya tiene una solicitud activa."), Me, True) : Return

            Dim solicitud As New clsSolicitud()
            solicitud.Id_Cliente = If(IsNothing(Session("Id_Cliente")) = False, Session("Id_Cliente"), cbClientes.Value)
            solicitud.Id_Sucursal = cbSuc.Value
            solicitud.Login_Asesor = cbAsesores.Value
            solicitud.Id_Aval = If(cbAval.SelectedIndex = -1, Session("Id_Aval"), cbAval.Value)
            solicitud.FecSol = deFecSol.Value
            solicitud.MontoSol = txtMontoSol.Value
            solicitud.PlazoSol = cbPlazoSol.Value
            solicitud.NumPagosSol = sePagosSol.Value
            solicitud.TasaReferencia = cbTasaRef.Value
            solicitud.TipoAmort = cbTipAmort.Value
            solicitud.Sobretasa = (Utilerias.getDataTable("SELECT Valor FROM CATTASAINTERES WHERE Id = " & cbTasaRef.Value).Rows.Item(0).Item(0) * 100) * 12
            solicitud.TasaMoratoria = cbTasaMora.Text
            solicitud.Impuesto = txtImpuesto.Text
            solicitud.TipoGarantia = cbTipGar.Value
            solicitud.ValorGarantia = txtValorGar.Text
            solicitud.IngresoCliente = txtIngCli.Value
            solicitud.TipoProducto = txtTipProd.Text
            solicitud.Destino = txtDestino.Text
            solicitud.LiquidaAnterior = If(cbALiquidar.Text <> "" And txtMontoALiquidar.Text <> "", 1, 0)
            solicitud.Id_A_Liquidar = If(cbALiquidar.Value = 0, 0, cbALiquidar.Value)
            solicitud.MontoALiquidar = If(txtMontoALiquidar.Text = "", 0, txtMontoALiquidar.Value)
            solicitud.MontoDesembolsado = If(txtMontoDes.Text = "", 0, txtMontoDes.Value)
            solicitud.Login = Session("Usuario").Login


            'Dim solicitud As New clsSolicitud() With {
            '.Id_Cliente = If(IsNothing(Session("Id_Cliente")) = False, Session("Id_Cliente"), cbClientes.Value),
            '.Id_Sucursal = cbSuc.Value,
            '.Login_Asesor = cbAsesores.Value,
            '.Id_Aval = cliente.Id_Aval,
            '.FecSol = deFecSol.Value,
            '.MontoSol = txtMontoSol.Value,
            '.PlazoSol = cbPlazoSol.Value,
            '.NumPagosSol = sePagosSol.Value,
            '.TasaReferencia = cbTasaRef.Value,
            '.TipoAmort = cbTipAmort.Value,
            '.Sobretasa = txtSobretasa.Value * 100,
            '.TasaMoratoria = cbTasaMora.Text,
            '.Impuesto = txtImpuesto.Text,
            '.TipoGarantia = cbTipGar.Value,
            '.ValorGarantia = txtValorGar.Text,
            '.IngresoCliente = txtIngCli.Text,
            '.LiquidaAnterior = If(cbALiquidar.Text <> "" And txtMontoALiquidar.Text <> "", 1, 0),
            '.Id_A_Liquidar = If(cbALiquidar.Value = 0, Nothing, cbALiquidar.Value),
            '.MontoALiquidar = txtMontoALiquidar.Value,
            '.MontoDesembolsado = txtMontoDes.Value,
            '.Login = Session("Usuario").Login
            '}

            If lblId.Text <> "--" Then
                solicitud.Id = lblId.Text
                solicitud.FecAut = Session("FecAutorizado")
                solicitud.FecDisp = Session("FecDisposicion")
                If clsSolicitud.actualizar(solicitud) Then
                    generarSolicitud(cbClientes.Text.Replace(" ", ""), solicitud.Id_Cliente, solicitud.Id_Aval)
                    gvSolicitudes.DataBind()
                    limpiarCampos()
                    Utilerias.MensajeConfirmacion("Se ha actualizado la solicitud exitosamente!", Me, True)
                Else
                    Utilerias.MensajeAlerta("No se ha podido actualizar la solicitud!", Me, True)
                End If
            Else
                If cbALiquidar.Text.Trim <> "" Then
                    Dim strUpdt = "UPDATE CREDITOS SET Liquidado = 1 AND estatusID <> 1 WHERE Id = " & cbALiquidar.Value
                    If Utilerias.setUpdInsDel(strUpdt) AndAlso clsSolicitud.insertar(solicitud, cbALiquidar.Value) Then
                        generarSolicitud(cbALiquidar.Text.Replace(" ", ""), Session("Id_Cliente"), Session("Id_Aval"))
                        gvSolicitudes.DataBind()
                        limpiarCampos()
                        Utilerias.MensajeConfirmacion("Se ha guardado la solicitud exitosamente!", Me, True)
                    Else
                        Utilerias.MensajeAlerta("No se ha podido guardar la solicitud!", Me, True)
                    End If
                Else
                    If clsSolicitud.insertar(solicitud) Then
                        generarSolicitud(cbClientes.Text, solicitud.Id_Cliente, solicitud.Id_Aval)
                        gvSolicitudes.DataBind()

                        Utilerias.MensajeConfirmacion("Se ha guardado la solicitud exitosamente!", Me, True)
                    Else
                        Utilerias.MensajeAlerta("No se ha podido guardar la solicitud!", Me, True)
                    End If

                End If
                limpiarCampos()
            End If

        Catch ex As Exception
            Utilerias.MensajeAlerta("¡Parece que ha ocurrido un problema! " & ex.Message, Me, True)
        End Try

    End Sub

    Protected Sub gvSolicitudes_SelectionChanged(sender As Object, e As EventArgs) Handles gvSolicitudes.SelectionChanged

        If gvSolicitudes.Selection.Count > 0 Then

            Dim seleccion = gvSolicitudes.GetSelectedFieldValues("Id", "Cliente", "FecSolicitud", "MontoSolicitud", "PlazoSol", "NumPagosSol",
                                                                 "Id_A_Liquidar", "MontoLiquid", "TasaRef", "TipoAmort", "Sobretasa",
                                                                 "TasaMoratoria", "Impuesto", "TipoGarantia", "ValorGarantia", "IngresoCliente",
                                                                 "MontoDesembolsado", "Sucursal", "Login_Asesor", "Id_Cliente", "FecAutorizado",
                                                                 "FecDisposicion", "Id_Aval", "Aval")
            For Each item As Object() In seleccion
                lblId.Text = item(0).ToString
                cbClientes.Text = item(1)
                deFecSol.Value = item(2)
                txtMontoSol.Value = item(3)
                cbPlazoSol.Text = item(4)
                sePagosSol.Value = item(5)
                cbALiquidar.Text = item(6).ToString()
                txtMontoALiquidar.Value = item(7).ToString()
                cbTasaRef.Text = item(8)
                cbTipAmort.Text = item(9)
                txtSobretasa.Value = item(10) / 100
                cbTasaMora.Text = item(11)
                txtImpuesto.Text = item(12)
                cbTipGar.Text = item(13)
                txtValorGar.Value = item(14)
                txtIngCli.Value = item(15).ToString()
                txtMontoDes.Value = item(16)
                cbSuc.Text = item(17)
                cbAsesores.SelectedItem = cbAsesores.Items.FindByValue(item(18))
                Session("Id_Cliente") = item(19)
                Session("FecAutorizado") = item(20)
                Session("FecDisposicion") = item(21)
                Session("Id_Aval") = item(22)
                cbAval.Text = item(23)
            Next item
        End If

    End Sub

    Protected Sub cbClientes_TextChanged(sender As Object, e As EventArgs) Handles cbClientes.TextChanged
        If cbClientes.SelectedIndex = -1 Then
            Utilerias.MensajeAlerta("Porfavor seleccione un cliente registrado", Me, True)
            sender.Focus()
        End If
    End Sub

    Protected Sub cbTasaRef_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cbTasaRef.SelectedIndexChanged
        If cbTasaRef.SelectedIndex <> -1 Then
            txtSobretasa.Value = Utilerias.getDataTable("SELECT Valor FROM CATTASAINTERES WHERE Id = " & cbTasaRef.Value).Rows.Item(0).Item(0) * 12
        End If
    End Sub

    Protected Sub cbTipGar_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cbTipGar.SelectedIndexChanged
        If cbTipGar.SelectedItem.Value = 3 Or cbTipGar.SelectedItem.Value = 5 Then
            Dim strSlc = "SELECT Valor FROM CATTIPGARANTIAS WHERE Id=" & cbTipGar.SelectedItem.Value
            Dim valor = Utilerias.getDataTable(strSlc)
            txtValorGar.Text = valor.Rows.Item(0).Item(0)

            If cbPlazoSol.Text <> "" AndAlso cbPlazoSol.SelectedItem.Equals(cbPlazoSol.Items.FindByText("Semanal")) Then

            Else
                cbPlazoSol.SelectedIndex = -1
                Utilerias.MensajeAlerta("Los prestamos con Garantía Líquida solo pueden ser a plazos semanales", Me, True)
                sePagosSol.Text = ""
            End If
        Else
                txtValorGar.Text = "0"
        End If
    End Sub

    Protected Sub sePagosSol_NumberChanged(sender As Object, e As EventArgs) Handles sePagosSol.NumberChanged
        If cbTipAmort.Value <> 4 Then
            If cbPlazoSol.Value = 1 Then
                If (sePagosSol.Value <> 16) And (sePagosSol.Value <> 20) Then
                    sePagosSol.Text = ""
                    Utilerias.MensajeAlerta("Los únicos plazos semanales válidos son 16 y 20 semanas", Me, True)
                End If
            ElseIf cbPlazoSol.Value = 2 Then
                If sePagosSol.Value > 36 Then
                    sePagosSol.Text = ""
                    Utilerias.MensajeAlerta("Los plazos quincenales válidos son de 1 a 36 quincenas", Me, True)
                End If
            Else
                If sePagosSol.Value > 18 Then
                    sePagosSol.Text = ""
                    Utilerias.MensajeAlerta("Los plazos mensuales válidos son de 1 a 18 meses", Me, True)
                End If
            End If
        Else
            If cbPlazoSol.Value = 1 Then
                If sePagosSol.Value > 104 Then
                    sePagosSol.Text = ""
                    Utilerias.MensajeAlerta("Los plazos semanales no pueden exceder 104 semanas", Me, True)
                End If
            ElseIf cbPlazoSol.Value = 2 Then
                If sePagosSol.Value > 52 Then
                    sePagosSol.Text = ""
                    Utilerias.MensajeAlerta("Los plazos quincenales no pueden exceder 52 quincenas", Me, True)
                End If
            Else
                If sePagosSol.Value > 24 Then
                    sePagosSol.Text = ""
                    Utilerias.MensajeAlerta("Los plazos mensuales no pueden exceder 24 meses", Me, True)
                End If
            End If
        End If
    End Sub

    Protected Sub cbClientes_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cbClientes.SelectedIndexChanged
        Dim strSlc = "SELECT IngresoMensual FROM CLIENTES WHERE Id=" & cbClientes.SelectedItem.Value
        Dim valor = Utilerias.getDataTable(strSlc)
        txtIngCli.Text = valor.Rows.Item(0).Item(0)
        If cbClientes.SelectedIndex <> -1 Then
            cbALiquidar.SelectedIndex = -1
            txtMontoALiquidar.Text = ""
            txtMontoDes.Text = ""
        End If
    End Sub

    Private Sub generarSolicitud(cliente As String, Id_Cliente As Integer, Id_Aval As Integer)
        Try

            Dim directorio = System.IO.Path.GetTempPath & "Documentos" & cliente.Replace(" ", "") & "\"
            Dim directorio2 = System.IO.Path.GetTempPath
            My.Computer.FileSystem.CreateDirectory(directorio)

            Dim slctCliente = "SELECT Id, PrimNombre, SegNombre, PrimApellido, SegApellido, Tipo, RFC, CURP, EMail, FecNac, DATEDIFF(dd, FecNac, GETDATE()) / 365 AS Edad, 
                                      Pais, Localidad, Nacionalidad, Sexo, PuestoLaboral, Ocupacion, Celular, EstadoCivil, NombresConyuge, PrimApellidoConyuge, 
                                      SegApellidoConyuge, FecNacConyuge, Id_Aval, EsAval, IngresoMensual, Calle, NumExt, NumInt, Colonia, CodPostal, LocalidadDom, PaisDom, 
                                      EstadoDom, MunicipioDom, AntigEnDom, TelefonoDom, ReferenciaDom, TipoContratoLaboral, AntigEmpleo, AntigEmpAnt
                               FROM CLIENTES
                               WHERE Id = " & Id_Cliente

            Dim slctAval = "SELECT Id, PrimNombre, SegNombre, PrimApellido, SegApellido, Tipo, RFC, CURP, EMail, FecNac, DATEDIFF(dd, FecNac, GETDATE()) / 365 AS Edad, 
                                      Pais, Localidad, Nacionalidad, Sexo, PuestoLaboral, Ocupacion, Celular, EstadoCivil, NombresConyuge, PrimApellidoConyuge, 
                                      SegApellidoConyuge, FecNacConyuge, Id_Aval, EsAval, IngresoMensual, Calle, NumExt, NumInt, Colonia, CodPostal, LocalidadDom, PaisDom, 
                                      EstadoDom, MunicipioDom, AntigEnDom, TelefonoDom, ReferenciaDom, TipoContratoLaboral, AntigEmpleo, AntigEmpAnt
                               FROM CLIENTES
                               WHERE Id = " & Id_Aval

            Dim slctSol = "SELECT TOP 1 S.Id, S.Id_Cliente, S.Id_Aval, S.Id_Sucursal, S.TipoProducto, S.Destino, S.MontoSolicitud,  
                                  CP.Descripcion AS PlazoSol, S.NumPagosSol, CTI.Descripcion As TasaInteres
                           FROM SOLICITUDES AS S INNER JOIN
                                CATTASAINTERES AS CTI ON S.TasaRef = CTI.Id INNER JOIN
                                CATPLAZOS AS CP ON S.PlazoSol = CP.Id
                                WHERE Id_Cliente = " & Id_Cliente & "
                                ORDER BY S.Id DESC"


            Dim dtCliente = Utilerias.getDataTable(slctCliente)
            Dim dtAval = Utilerias.getDataTable(slctAval)
            Dim dtSolicitud = Utilerias.getDataTable(slctSol)
            Dim dtEmpCli = Utilerias.getDataTable("SELECT * FROM EMPRESAS WHERE Id_Cliente = " & Id_Cliente)
            Dim dtEmpAval = Utilerias.getDataTable("SELECT * FROM EMPRESAS WHERE Id_Cliente = " & Id_Aval)
            Dim dtNegCli = Utilerias.getDataTable("SELECT * FROM NEGOCIOS WHERE Id_Cliente = " & Id_Cliente)
            Dim dtNegAval = Utilerias.getDataTable("SELECT * FROM NEGOCIOS WHERE Id_Cliente = " & Id_Aval)
            Dim dtInfoEcoCli = Utilerias.getDataTable("SELECT * FROM INFORMACIONECONOMICA WHERE Id_Cliente = " & Id_Cliente)
            Dim dtInfoEcoAval = Utilerias.getDataTable("SELECT * FROM INFORMACIONECONOMICA WHERE Id_Cliente = " & Id_Aval)
            Dim dtReferencias = Utilerias.getDataTable("SELECT * FROM REFERENCIAS WHERE Id_Cliente = " & Id_Cliente)

            Dim datos As New DataSet
            datos.Tables.Add(dtCliente)
            datos.Tables(0).TableName = "CLIENTES"
            datos.Merge(dtCliente)
            datos.Tables.Add(dtSolicitud)
            datos.Tables(1).TableName = "SOLICITUDES"
            datos.Merge(dtSolicitud)
            datos.Tables.Add(dtEmpCli)
            datos.Tables(2).TableName = "EMPRESAS"
            datos.Merge(dtEmpCli)
            datos.Tables.Add(dtNegCli)
            datos.Tables(3).TableName = "NEGOCIOS"
            datos.Merge(dtNegCli)
            datos.Tables.Add(dtInfoEcoCli)
            datos.Tables(4).TableName = "INFORMACIONECONOMICA"
            datos.Merge(dtInfoEcoCli)
            datos.Tables.Add(dtReferencias)
            datos.Tables(5).TableName = "REFERENCIAS"
            datos.Merge(dtReferencias)
            Dim solicitudCliente = generarSolicitud_Cliente(datos, cliente)

            datos.Reset()
            dtSolicitud = Utilerias.getDataTable(slctSol)
            datos.Tables.Add(dtAval)
            datos.Tables(0).TableName = "CLIENTES"
            datos.Merge(dtAval)
            datos.Tables.Add(dtSolicitud)
            datos.Tables(1).TableName = "SOLICITUDES"
            datos.Merge(dtSolicitud)
            datos.Tables.Add(dtEmpAval)
            datos.Tables(2).TableName = "EMPRESAS"
            datos.Merge(dtEmpAval)
            datos.Tables.Add(dtNegAval)
            datos.Tables(3).TableName = "NEGOCIOS"
            datos.Merge(dtNegAval)
            datos.Tables.Add(dtInfoEcoAval)
            datos.Tables(4).TableName = "INFORMACIONECONOMICA"
            datos.Merge(dtInfoEcoAval)


            Dim solicitudAval = generarSolicitud_Aval(datos, cliente)

            Dim zipFile = "Documentos" & cliente.Replace(" ", "_") & ".zip"
            Using zip As ZipFile = New _
                ZipFile(directorio2)
                ' Add the file to the Zip archive's root folder
                zip.AddDirectory(directorio)

                ' Save the Zip file.
                zip.Save(directorio & zipFile)


                Response.Clear()
                Response.BufferOutput = False      'n
                Response.ContentType = "application/zip"
                Response.AddHeader("Content-disposition", "attachment; filename=" & zipFile)
                'Response.WriteFile(directorio & zipFile)
                zip.Save(Response.OutputStream)
                System.IO.Directory.Delete(directorio, True)
                Response.End()
            End Using
            'End Using
            'System.IO.Directory.Delete(directorio, True)
            '    Try
            '        Response.End()
            '    Catch ex As Exception
            '        Response.Close()
            '        limpiarCampos()
            '    End Try






        Catch ex As Exception
            limpiarCampos()
            Throw ex
        End Try
    End Sub

    Private Function generarSolicitud_Cliente(datos As DataSet, cliente As String) As String
        Try

            Dim crReporteDocumento As New crSolicitud

            crReporteDocumento.SetDataSource(datos)


            Dim file As String = System.IO.Path.GetTempPath & "Documentos" & cliente.Replace(" ", "") & "\SolicitudCliente_" & cliente.Replace(" ", "") & ".PDF"
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

    Private Function generarSolicitud_Aval(datos As DataSet, cliente As String) As String
        Try

            Dim crReporteDocumento As New crSolicitudAval

            crReporteDocumento.SetDataSource(datos)

            crReporteDocumento.SetParameterValue("cliente", cliente)

            Dim file As String = System.IO.Path.GetTempPath & "Documentos" & cliente.Replace(" ", "") & "\SolicitudAval_" & cliente.Replace(" ", "") & ".PDF"
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

    Protected Sub cbALiquidar_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cbALiquidar.SelectedIndexChanged
        If cbALiquidar.SelectedIndex <> -1 Then
            cbClientes.SelectedIndex = -1
            Dim UltPago = clsPago.obtener(cbALiquidar.Value,, True, True)
            txtMontoALiquidar.Value = Math.Ceiling(UltPago.Saldo)
            If txtMontoSol.Text <> "" Then
                txtMontoDes.Value = Math.Floor(txtMontoSol.Value - txtMontoALiquidar.Value)
                If txtMontoDes.Value < 0 Then
                    txtMontoDes.Text = ""
                    Utilerias.MensajeAlerta("El monto a solicitar debe ser superior al adeudo del crédito anterior.", Me.Page, True)
                End If
            End If
            Session("Id_Cliente") = UltPago.Id_Cliente
            Session("Id_Aval") = clsSolicitud.obtener(clsCredito.obtener(UltPago.Id_Credito).Id_Sol).Id_Aval
        End If
    End Sub

    Protected Sub txtMontoSol_NumberChanged(sender As Object, e As EventArgs) Handles txtMontoSol.NumberChanged

        If txtMontoALiquidar.Text <> "" Then
            txtMontoDes.Value = Math.Floor(txtMontoSol.Value - txtMontoALiquidar.Value)
            If txtMontoDes.Value < 0 Then
                txtMontoDes.Text = ""
                Utilerias.MensajeAlerta("El monto a solicitar debe ser superior al adeudo del crédito anterior.", Me.Page, True)
            End If
        End If

    End Sub

End Class