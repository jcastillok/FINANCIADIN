Imports Ionic.Zip

Public Class Contratos

    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then

        End If
    End Sub

    Protected Sub Validar(sender As Object, e As EventArgs) Handles bttValidar.Click
        Dim cadenaLlave As String = ""
        Dim leyendaRetiro As String = ""
        Try
            If ASPxGridView1.Selection.Count > 0 Then

                Dim folio = ASPxGridView1.GetSelectedFieldValues("Folio").Item(0)
                Dim id = ASPxGridView1.GetSelectedFieldValues("Id_Sol").Item(0)
                Dim cliente = ASPxGridView1.GetSelectedFieldValues("Cliente").Item(0)
                Dim deuda = ASPxGridView1.GetSelectedFieldValues("Adeudo").Item(0)
                Dim prestamo = ASPxGridView1.GetSelectedFieldValues("Prestamo").Item(0)
                Dim numPagos = ASPxGridView1.GetSelectedFieldValues("#Pagos").Item(0)
                Dim tasaReferencia = ASPxGridView1.GetSelectedFieldValues("TasaRef").Item(0)

                Dim tasaRef = tasaReferencia.ToString().Substring(0, tasaReferencia.ToString().IndexOfAny("%") + 1)

                Dim numeros As String() = tasaRef.Replace("%", "").Split(".")
                Dim tasaRefLetras = If(numeros.Length > 1, Utilerias.Num2Text(numeros(0)) + " PUNTO " + Utilerias.Num2Text(numeros(1)), Utilerias.Num2Text(numeros(0))) + " PORCIENTO"

                Dim strSolicitud = String.Format("SELECT * FROM SOLICITUDES WHERE Id = {0}", id)

                Dim strCliente = String.Format("SELECT Id, PrimNombre, SegNombre, PrimApellido, SegApellido, Login, Tipo, RFC, CURP, Calle, 
                                                       NumExt, NumInt, Colonia, LocalidadDom As Localidad, ReferenciaDom As Referencia, PaisDom As Pais, 
                                                       EstadoDom As Estado, MunicipioDom As Municipio, CodPostal, 
                                                       FecNac, TelefonoDom As Telefono, EMail, Estatus, FecRegistro, Celular, Nacionalidad, EstadoCivil, 
                                                       Ocupacion, IngresoMensual 
                                                       FROM CLIENTES WHERE Id = (SELECT Id_Cliente FROM SOLICITUDES WHERE Id={0})", id)

                Dim strAval = String.Format("SELECT Id, PrimNombre, SegNombre, PrimApellido, SegApellido, Login, Tipo, RFC, CURP, Calle, 
                                                    NumExt, NumInt, Colonia, LocalidadDom As Localidad, ReferenciaDom As Referencia, PaisDom As Pais, 
                                                    EstadoDom As Estado, MunicipioDom As Municipio, CodPostal, 
                                                    FecNac, TelefonoDom As Telefono, EMail, Estatus, FecRegistro, Celular, Nacionalidad, EstadoCivil, 
                                                    Ocupacion, IngresoMensual 
                                                    FROM CLIENTES WHERE Id = (SELECT Id_Aval FROM SOLICITUDES WHERE Id={0})", id)

                Dim strTalon = "SELECT REPLACE(CLIENTES.PrimNombre + ' ' + CLIENTES.SegNombre + ' ' + CLIENTES.PrimApellido + ' ' + CLIENTES.SegApellido, '  ', ' ') AS NombreCliente, 
                                CREDITOS.Id AS FolioCredito, CEILING(CREDITOS.Adeudo/CREDITOS.NumPagos) AS Pago, CATSUCURSALES.Descripcion AS Sucursal
                            FROM SOLICITUDES INNER JOIN
                                CREDITOS ON SOLICITUDES.Id = CREDITOS.Id_Sol LEFT OUTER JOIN
                                CLIENTES ON SOLICITUDES.Id_Cliente = CLIENTES.Id LEFT OUTER JOIN
                                CATSUCURSALES ON SOLICITUDES.Id_Sucursal = CATSUCURSALES.Id 
                            WHERE  CREDITOS.EstatusID = 1 AND SOLICITUDES.Id = " & id

                Dim datos As New DataSet
                Dim dt = Utilerias.getDataTable(strSolicitud)
                Dim dt2 = Utilerias.getDataTable(strCliente)
                Dim dt3 = Utilerias.getDataTable(strAval)
                Dim dt4 As New DataTable

                ''Dim directorio = System.IO.Path.GetTempPath & "Documentos" & cliente.ToString.Replace(" ", "") & "\"
                Dim directorio = Server.MapPath("~/TempFile/Documentos" & cliente.ToString.Replace(" ", "") & "\")
                ' Dim directorio2 = System.IO.Path.GetTempPath
                Dim directorio2 = Server.MapPath("~/TempFile")
                My.Computer.FileSystem.CreateDirectory(directorio)

                datos.Tables.Add(dt)
                datos.Tables(0).TableName = "SOLICITUDES"
                datos.Merge(dt)
                datos.Tables.Add(dt2)
                datos.Tables(1).TableName = "CLIENTES"
                datos.Merge(dt2)
                datos.Tables.Add(dt3)
                datos.Tables(2).TableName = "AVALES"
                datos.Merge(dt3)

                Dim credito = clsCredito.obtener(0, False, True, " C.Id = (SELECT ID FROM CREDITOS WHERE EstatusID = 1 AND Id_Sol = " & id & ")")
                Dim parametros As String = String.Format("{0}:={1}:={2}:={3}:={4}:={5}:={6}:={7}:={8}", deuda, Utilerias.EnLetras(deuda).ToUpper, cliente.Trim, credito.FecUltPago.ToShortDateString(), tasaReferencia, tasaRefLetras, "RAPIPRES, S.A. DE C.V.", String.Concat(credito.Sobretasa, "%"), credito.Id)

                Dim contrato = generarContrato(datos, parametros, cliente.ToString.Replace(" ", ""))
                Dim pagare = generarPagare(datos, parametros, cliente.ToString.Replace(" ", ""))

                dt4 = Utilerias.crearTablaDePagos(credito.FecInicio, prestamo, deuda, credito.NumPagos, credito.Plazo)
                datos.Tables.Add(dt4)
                datos.Tables(3).TableName = "TABLADEAMORTIZACION"
                datos.Merge(dt4)
                datos.Tables.RemoveAt(2)
                Dim tabla = generarTablaAmort(datos, parametros, cliente.ToString.Replace(" ", ""))

                Dim datosTalon As New DataSet
                Dim dtTalon = Utilerias.getDataTable(strTalon)
                datosTalon.Tables.Add(dtTalon)
                datosTalon.Tables(0).TableName = "TALON"
                datosTalon.Merge(dtTalon)
                Dim talon = generarTalon(datosTalon, cliente.ToString.Replace(" ", ""), cadenaLlave, leyendaRetiro)


                Dim zipFile = "Documentos" & cliente.ToString.Replace(" ", "") & ".zip"
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
                    zip.Save(Response.OutputStream)
                    System.IO.Directory.Delete(directorio, True)
                    Response.End()

                End Using



                'If IsNothing(Session("ContratoDataSet")) = False Then
                '    Context.Response.Write("<script language='javascript'>window.open('dspVisor/dspPagare.aspx','_blank');</script>")
                '    Context.Response.Write("<script language='javascript'>window.open('dspVisor/dspContrato.aspx','_blank');</script>")
                '    Context.Response.Write("<script language='javascript'>window.open('dspVisor/dspTablaAmort.aspx','_blank');</script>")
                '    Context.Response.Write("<script language='javascript'>window.open('dspVisor/dspTalon.aspx','_blank');</script>")
                'End If

                Utilerias.MensajeConfirmacion("Se ha confirmado la solicitud y guardado el crédito!", Me, True)
                ASPxGridView1.DataBind()

            Else
                Utilerias.MensajeAlerta("Debe seleccionar una solicitud primero", Me, True)
            End If
        Catch ex As Exception
            Utilerias.MensajeAlerta("¡Algo salió mal! Detalles: " & ex.Message, Me, True)
            ASPxGridView1.DataBind()
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
            Dim directorio = Server.MapPath("~/TempFile/Documentos" & cliente.ToString.Replace(" ", "") & "\")


            Dim file As String = directorio & "\Contrato" & variables(2).ToString.Replace(" ", "") & ".PDF"
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
            Dim directorio = Server.MapPath("~/TempFile/Documentos" & cliente.ToString.Replace(" ", "") & "\")

            Dim file As String = directorio & "\TablaAmortizacion" & variables(2).ToString.Replace(" ", "") & ".PDF"

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
            Dim directorio = Server.MapPath("~/TempFile/Documentos" & cliente.ToString.Replace(" ", "") & "\")


            'System.IO.Path.GetTempPath
            Dim file As String = directorio & "\Pagare" & variables(2).ToString.Replace(" ", "") & ".PDF"
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
            Dim directorio = Server.MapPath("~/TempFile/Documentos" & cliente.ToString.Replace(" ", "") & "\")



            Dim file As String = directorio & "\Talon" & cliente & ".PDF"
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