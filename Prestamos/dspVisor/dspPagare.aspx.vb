Public Class dspPagare

    Inherits System.Web.UI.Page

    Public crReporteDocumento As New crPagare

    Private Sub dspPagare_Unload(sender As Object, e As System.EventArgs) Handles Me.Unload
        crReporteDocumento.Close()
        crReporteDocumento.Dispose()
        'CrystalReportViewer1.Dispose()
    End Sub

    Private Sub dspPagare_Init(sender As Object, e As System.EventArgs) Handles Me.Init
        Try

            If DirectCast(Session("ContratoDataSet"), System.Data.DataSet).Tables.Count <> 0 Then

                Dim variables As Array = Split(Session("parametros"), ":=")

                crReporteDocumento.SetDataSource(DirectCast(Session("ContratoDataSet"), System.Data.DataSet))

                crReporteDocumento.SetParameterValue("deuda", variables(0))
                crReporteDocumento.SetParameterValue("deudaEnletras", variables(1))
                crReporteDocumento.SetParameterValue("cliente", variables(2))
                crReporteDocumento.SetParameterValue("finPrestamo", variables(3))
                crReporteDocumento.SetParameterValue("tasaRef", variables(4))
                crReporteDocumento.SetParameterValue("tasaRefLetras", variables(5))
                crReporteDocumento.SetParameterValue("razon", variables(6))

                CrystalReportViewer1.Visible = True
                CrystalReportViewer1.ReportSource = crReporteDocumento

                Dim file As String = System.IO.Path.GetTempPath & "Pagare" & variables(2).ToString.Replace(" ", "") & ".PDF"
                Dim diskOpts As New CrystalDecisions.Shared.DiskFileDestinationOptions
                diskOpts.DiskFileName = file
                crReporteDocumento.ExportOptions.DestinationOptions = diskOpts
                crReporteDocumento.ExportOptions.ExportFormatType = CrystalDecisions.Shared.ExportFormatType.PortableDocFormat
                crReporteDocumento.ExportOptions.ExportDestinationType = CrystalDecisions.Shared.ExportDestinationType.DiskFile
                crReporteDocumento.Export()
                CrystalReportViewer1.Dispose()
                CrystalReportViewer1 = Nothing
                crReporteDocumento.Close()
                crReporteDocumento.Dispose()

                Response.Clear()
                Response.ContentType = "application/pdf"
                Response.AddHeader("Content-disposition", "attachment; filename=Pagare" & variables(2).ToString.Replace(" ", "") & ".PDF")
                Response.WriteFile(file)
                Response.Flush()
                Response.Close()

                System.IO.File.Delete(file)

            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub



End Class