Public Class dspTalon

    Inherits System.Web.UI.Page

    Public crReporteDocumento As New crTalon

    Private Sub dspTalon_Unload(sender As Object, e As System.EventArgs) Handles Me.Unload
        crReporteDocumento.Close()
        crReporteDocumento.Dispose()
        'CrystalReportViewer1.Dispose()
    End Sub

    Private Sub dspTalon_Init(sender As Object, e As System.EventArgs) Handles Me.Init
        Try

            If DirectCast(Session("TalonDataSet"), System.Data.DataSet).Tables.Count <> 0 Then

                crReporteDocumento.SetDataSource(DirectCast(Session("TalonDataSet"), System.Data.DataSet))

                CrystalReportViewer1.Visible = True
                CrystalReportViewer1.ReportSource = crReporteDocumento

                Dim file As String = System.IO.Path.GetTempPath & "Talon.PDF"
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
                Response.AddHeader("Content-disposition", "attachment; filename=Talon.PDF")
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