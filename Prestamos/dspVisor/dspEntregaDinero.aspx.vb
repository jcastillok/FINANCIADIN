Public Class dspEntregaDinero

    Inherits System.Web.UI.Page

    Public crReporteDocumento As New crEntregaDinero

    Private Sub dspEntregaDinero_Unload(sender As Object, e As System.EventArgs) Handles Me.Unload
        crReporteDocumento.Close()
        crReporteDocumento.Dispose()
        CrystalReportViewer1.Dispose()
    End Sub

    Private Sub dspEntregaDinero_Init(sender As Object, e As System.EventArgs) Handles Me.Init
        Try

            Dim variables As Array = Split(Session("parametros"), ":=")

            crReporteDocumento.SetParameterValue("cliente", variables(0))
            crReporteDocumento.SetParameterValue("direccion", variables(1))
            crReporteDocumento.SetParameterValue("telefono", variables(2))
            crReporteDocumento.SetParameterValue("prestamo", variables(3))
            crReporteDocumento.SetParameterValue("plazo", variables(4))
            crReporteDocumento.SetParameterValue("adeudo", variables(5))
            crReporteDocumento.SetParameterValue("fechaFinal", variables(6))
            crReporteDocumento.SetParameterValue("tipoTicket", variables(7))

            CrystalReportViewer1.Visible = True
            CrystalReportViewer1.ReportSource = crReporteDocumento

            'Dim file As String = System.IO.Path.GetTempPath & "Comprobante.PDF"
            'Dim diskOpts As New CrystalDecisions.Shared.DiskFileDestinationOptions
            'diskOpts.DiskFileName = file
            'crReporteDocumento.ExportOptions.DestinationOptions = diskOpts
            'crReporteDocumento.ExportOptions.ExportFormatType = CrystalDecisions.Shared.ExportFormatType.PortableDocFormat
            'crReporteDocumento.ExportOptions.ExportDestinationType = CrystalDecisions.Shared.ExportDestinationType.DiskFile
            'crReporteDocumento.Export()
            'crReporteDocumento.Close()

            'Response.Clear()
            'Response.ContentType = "application/pdf"
            'Response.AddHeader("Content-disposition", "attachment; filename=Comprobante" & variables(0).ToString.Replace(" ", "") & ".PDF")
            'Response.WriteFile(file)
            'Response.Flush()
            'Response.Close()

        Catch ex As Exception
            Throw ex
        End Try
    End Sub

End Class