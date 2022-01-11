﻿Public Class dspContabilidad

    Inherits System.Web.UI.Page

    Public crReporteDocumento As New crContabilidad

    Private Sub dspContabilidad_Unload(sender As Object, e As System.EventArgs) Handles Me.Unload
        crReporteDocumento.Close()
        crReporteDocumento.Dispose()
        CrystalReportViewer1.Dispose()
        GC.Collect()
    End Sub

    Private Sub dspContabilidad_Init(sender As Object, e As System.EventArgs) Handles Me.Init
        Try

            If DirectCast(Session("Contabilidad"), System.Data.DataSet).Tables.Count <> 0 Then

                crReporteDocumento.SetDataSource(DirectCast(Session("Contabilidad"), System.Data.DataSet))

                CrystalReportViewer1.Visible = True
                CrystalReportViewer1.ReportSource = crReporteDocumento

                'Dim file As String = System.IO.Path.GetTempPath & "Contrato.PDF"
                'Dim diskOpts As New CrystalDecisions.Shared.DiskFileDestinationOptions
                'diskOpts.DiskFileName = file
                'crReporteDocumento.ExportOptions.DestinationOptions = diskOpts
                'crReporteDocumento.ExportOptions.ExportFormatType = CrystalDecisions.Shared.ExportFormatType.PortableDocFormat
                'crReporteDocumento.ExportOptions.ExportDestinationType = CrystalDecisions.Shared.ExportDestinationType.DiskFile
                'crReporteDocumento.Export()
                'crReporteDocumento.Close()
                'crReporteDocumento.Dispose()


                'Response.Clear()
                'Response.ContentType = "application/pdf"
                'Response.AddHeader("Content-disposition", "attachment; filename=Contrato" & variables(2).ToString.Replace(" ", "") & ".PDF")
                'Response.WriteFile(file)
                'Response.Flush()
                'Response.Close()

            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

End Class