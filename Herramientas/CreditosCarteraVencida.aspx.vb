Imports System.Data.SqlClient
Imports System.IO
Imports DevExpress.Web
Imports DevExpress.XtraPrinting

Public Class CreditosCarteraVencida
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            Dim usuario As clsUsuario = Nothing
            usuario = DirectCast(Session("Usuario"), clsUsuario)

            cbSuc.Text = usuario.IdSucursal

            'gvCarteraVencida.DataSource = Nothing
            'gvCarteraVencida.DataBind()
            'pnlGridCreditos.Visible = False
        End If


    End Sub

    Protected Sub btnConsultar_Click(sender As Object, e As EventArgs) Handles btnConsultar.Click

        If cbSuc.SelectedIndex <> -1 Then
            Dim sucursal = cbSuc.Text

            gvCarteraVencida.DataSource = clsReporteCarteraVencida.getDataSource(sucursal)
            gvCarteraVencida.DataBind()
            pnlGridCreditos.Visible = True

        End If

    End Sub

    Protected Sub gvCarteraVencida_PageIndexChanged(sender As Object, e As EventArgs) Handles gvCarteraVencida.PageIndexChanged
        Dim sucursal = cbSuc.Text
        gvCarteraVencida.DataSource = clsReporteCarteraVencida.getDataSource(sucursal)
        gvCarteraVencida.DataBind()

    End Sub

    ''' <summary>
    ''' Exportacion de la informacion del grid a excel
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Protected Sub btnExportar_Click(sender As Object, e As EventArgs) Handles btnExportar.Click

        Dim sucursal = cbSuc.Text
        gvCarteraVencida.DataSource = clsReporteCarteraVencida.getDataSource(sucursal)
        gvCarteraVencida.DataBind()

        Dim Options As XlsxExportOptionsEx = New XlsxExportOptionsEx()

        Options.ExportType = DevExpress.Export.ExportType.WYSIWYG

        ASPxGridViewExporter1.GridViewID = gvCarteraVencida.UniqueID
        ASPxGridViewExporter1.DataBind()
        ASPxGridViewExporter1.WriteXlsToResponse()
    End Sub

    Protected Sub ASPxGridViewExporter1_RenderBrick(sender As Object, e As DevExpress.Web.ASPxGridViewExportRenderingEventArgs) Handles ASPxGridViewExporter1.RenderBrick
        If (e.RowType = GridViewRowType.Data) Then
            e.Text = e.Value.ToString()
        End If
    End Sub

    Protected Sub VerIdentificacion(sender As Object, e As EventArgs)
        Dim ID As Integer = Integer.Parse(TryCast(sender, LinkButton).CommandArgument)
        Dim bytes As Byte()
        Dim fileName As String, contentType As String
        Dim fileNameTest As String
        Dim constr As String = clsConexion.ConnectionString
        Try
            Using con As New SqlConnection(constr)
                Using cmd As New SqlCommand()
                    cmd.CommandText = "select Identificacion, ('Identificacion' + PrimNombre + PrimApellido + '.pdf') AS Nombre, 'application/pdf' AS ContentType from clientes where Id=@Id"
                    cmd.Parameters.AddWithValue("@Id", ID)
                    cmd.Connection = con
                    con.Open()

                    Using sdr As SqlDataReader = cmd.ExecuteReader()
                        sdr.Read()
                        bytes = DirectCast(sdr("Identificacion"), Byte())
                        contentType = sdr("ContentType").ToString()
                        fileName = sdr("Nombre").ToString()
                    End Using
                    con.Close()
                    fileNameTest = System.IO.Path.GetTempPath & fileName

                End Using
            End Using
            'Response.Clear()
            'Response.Buffer = True
            'Response.Charset = ""
            'Response.Cache.SetCacheability(HttpCacheability.NoCache)
            'Response.ContentType = contentType
            ''Response.AppendHeader("Content-Disposition", "attachment; filename=" + fileName)
            'Response.AddHeader("content-length", bytes.Length.ToString())
            ''  Response.BinaryWrite(bytes)
            ''Response.AppendHeader("Content-Disposition", Convert.ToString("attachment; filename=") & fileName)
            '' File.WriteAllBytes(fileNameTest, bytes)
            '' Response.TransmitFile(fileNameTest)
            'Response.WriteFile(fileNameTest)
            'Response.Flush()
            'Response.End()
            ''Context.Response.Write("<script language='javascript'>window.open('CreditosCarteraVencida.aspx','_blank');</script>")
            'Context.Response.Write("<script language='javascript'>window.open('../Visor.aspx','_blank');</script>")

            Response.Clear()
            Response.Buffer = True
            Response.Charset = ""
            Response.Cache.SetCacheability(HttpCacheability.NoCache)
            Response.ContentType = contentType
            'Response.AppendHeader("Content-Disposition", "attachment; filename=" + fileName)
            Response.AddHeader("content-length", bytes.Length.ToString())
            Response.BinaryWrite(bytes)
            Response.Flush()
            Response.End()

        Catch ex As Exception
            '  Utilerias.MensajeAlerta("¡Este cliente no tiene una Identificación guardada!", Me, True)
        End Try
    End Sub

    Protected Sub VerComprobante(sender As Object, e As EventArgs)

        Dim ID As Integer = Integer.Parse(TryCast(sender, LinkButton).CommandArgument)
        Dim bytes As Byte()
        Dim fileName As String, contentType As String
        Dim constr As String = clsConexion.ConnectionString
        Try
            Using con As New SqlConnection(constr)
                Using cmd As New SqlCommand()
                    cmd.CommandText = "select ComprobanteDom, ('ComprobanteDomiciliario' + PrimNombre + PrimApellido + '.pdf') AS Nombre, 'application/pdf' AS ContentType from clientes where Id=@Id"
                    cmd.Parameters.AddWithValue("@Id", ID)
                    cmd.Connection = con
                    con.Open()
                    Using sdr As SqlDataReader = cmd.ExecuteReader()
                        sdr.Read()
                        bytes = DirectCast(sdr("ComprobanteDom"), Byte())
                        contentType = sdr("ContentType").ToString()
                        fileName = sdr("Nombre").ToString()
                    End Using
                    con.Close()
                End Using
            End Using
            Response.Clear()
            Response.Buffer = True
            Response.Charset = ""
            Response.Cache.SetCacheability(HttpCacheability.NoCache)
            Response.ContentType = contentType
            'Response.AppendHeader("Content-Disposition", "attachment; filename=" + fileName)
            Response.AddHeader("content-length", bytes.Length.ToString())
            Response.BinaryWrite(bytes)
            Response.Flush()
            Response.End()
        Catch ex As Exception
            '  Utilerias.MensajeAlerta("¡Este cliente no tiene un Comprobante Domiciliario guardado!", Me, True)
        End Try

    End Sub


    Protected Sub VerIdentificacionAval(sender As Object, e As EventArgs)
        Dim ID As Integer = Integer.Parse(TryCast(sender, LinkButton).CommandArgument)
        Dim bytes As Byte()
        Dim fileName As String, contentType As String
        Dim fileNameTest As String
        Dim constr As String = clsConexion.ConnectionString
        Try
            Using con As New SqlConnection(constr)
                Using cmd As New SqlCommand()
                    cmd.CommandText = "select Identificacion, ('Identificacion' + PrimNombre + PrimApellido + '.pdf') AS Nombre, 'application/pdf' AS ContentType from clientes where Id=@Id"
                    cmd.Parameters.AddWithValue("@Id", ID)
                    cmd.Connection = con
                    con.Open()

                    Using sdr As SqlDataReader = cmd.ExecuteReader()
                        sdr.Read()
                        bytes = DirectCast(sdr("Identificacion"), Byte())
                        contentType = sdr("ContentType").ToString()
                        fileName = sdr("Nombre").ToString()
                    End Using
                    con.Close()
                    fileNameTest = System.IO.Path.GetTempPath & fileName

                End Using
            End Using

            Response.Clear()
            Response.Buffer = True
            Response.Charset = ""
            Response.Cache.SetCacheability(HttpCacheability.NoCache)
            Response.ContentType = contentType

            Response.AddHeader("content-length", bytes.Length.ToString())
            Response.BinaryWrite(bytes)
            Response.Flush()
            Response.End()

        Catch ex As Exception
            '  Utilerias.MensajeAlerta("¡Este cliente no tiene una Identificación guardada!", Me, True)
        End Try
    End Sub



End Class