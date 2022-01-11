Public Class Visor
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            REM busco o creo un archivo pdf en forma dinamica y como ya se su ruta y nombre entonces lo declaro
            Dim pdfUrl = Session("filename")

            Response.Clear()
            Response.ContentType = "application/pdf"
            Response.AddHeader("Content-disposition", "inline; filename=" & pdfUrl)
            Response.WriteFile(pdfUrl)
            Response.Flush()
            Response.Close()

            'Response.Redirect.ActionLink("view pdf", "getpdf", "somecontroller", null, New { target = "_blank" })
            'pnlPdfHrBook.Controls.Add(New LiteralControl("<Center><object id='pdfFrame' style='display:block; border:none; margin-top:10px; height:65vh; width:60vw;' data='" + pdfUrl + "'> </object></Center>"))
        Catch ex As Exception
        End Try
    End Sub

    Protected Sub Page_Unload(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Unload
        Try
            System.IO.File.Delete(Session("filename"))
            Session.Remove("filename")
        Catch ex As Exception
        End Try
    End Sub

End Class