Public Class clsVistas

    Public Shared Sub getBuscarEnInventario(DESPROD1 As String, DESFAM As String, DESMAR As String, gv As DevExpress.Web.ASPxPivotGrid.ASPxPivotGrid)
        Dim StrSelect As String

        StrSelect = " SELECT     CVEPROD, DESBOD, DESPROD, DESFAM, DESMAR, LISPRE1, TieDia, 1 as Existe " _
        & " FROM         vwInvProductos " _
        & " WHERE     (EXISTE = 1) AND (DESBOD LIKE 'P%') " & DESPROD1 & DESFAM & DESMAR
        Try
            Utilerias.getDatareaderGvPv(StrSelect, gv)
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    'Public Shared Function getProducto(CVEPROD As Long) As CATPROD
    '    Using contex As New DataClsLinqDataContext
    '        Try
    '            Dim var = From productos In contex.CATPROD
    '            Where productos.CVEPROD = CVEPROD
    '            Select productos
    '            Return IIf(var.ToList.Count = 0, Nothing, var.First)
    '        Catch ex As Exception
    '            Return Nothing
    '        End Try
    '    End Using
    'End Function

    Public Shared Function getInventarioApartados(CveBodVta As String, tipoProducto As String, statusApartado As String) As DataTable
        Dim StrSelect As String

        StrSelect = " SELECT DISTINCT " _
            & "                       MA.BodVta, MVA.CveProd, MVA.DesProd, MVA.DESFAM, MVA.LISPRE6 AS Costo, MVA.LISPRE1 AS Precio1, MJ.Costo AS PreRetCap, MJ.ImpPrecio AS PreVitCap, MJ.GrsSinP, MJ.GrsConP, " _
            & "                       1 AS EXISTE, MA.FechaIniApar, MA.NumPagosTotal, MA.Status, MA.Pagos, MA.ImpTotPagado, MA.Saldo " _
            & " FROM         vwMovVentasApartados MVA INNER JOIN " _
            & "                       vwMovApartados MA ON MVA.CveBod = MA.CveBod AND MVA.CveMov = MA.CveMov AND MVA.SerMov = MA.SerMov AND MVA.FolMov = MA.FolMov LEFT OUTER JOIN " _
            & "                       vwMovJoyeria MJ ON MVA.CveProd = MJ.CveProd " _
            & " WHERE    (MA.BodVta in (" & CveBodVta & ")) " & tipoProducto & " " & statusApartado & " " _
            & " ORDER BY MVA.DESFAM, MVA.CveProd "
        Try
            Return Utilerias.getDataTable(StrSelect)
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Public Shared Function getVentaApartados(CveBodVta As String, tipoProducto As String, statusApartado As String) As DataTable
        Dim StrSelect As String
        StrSelect = " SELECT   1 AS Cantidad,   B.DESBOD AS BodVtaApa, MA.FechaIniApar, MA.FechaFinApar, case MA.Status when 'Cancelada' then 0 else MA.SaldoProxPag end as SaldoProxPag, " _
            & "  MA.ImpTotalApar, case MA.Status when 'Cancelada' then 0 else MA.Saldo end as Saldo, MA.ImpTotPagado, MA.Status, MA.Pagos, MVA.CveProd, MVA.DesProd as Producto, MVA.Login, " _
            & "                        MVA.NOMVEN as Vendedor, MVA.DESFAM as Familia, MVA.DESMAR as Marca, MVA.LISPRE1 as Precio,MVA.LISPRE6 as Costo, case MA.Status when 'Cancelada' then 0 else MVA.LISPRE1-MVA.LISPRE6 end as Utilidad, " _
            & "                        MVA.TieDia, MVA.TieMes, MVA.Ann, MVA.Mes, MVA.Dia, MVA.NOMCLI as Cliente, MA.LoginCancela " _
            & "  FROM         vwMovApartados MA INNER JOIN " _
            & "                        vwMovVentasApartados MVA ON MA.CveBod = MVA.CveBod AND MA.CveMov = MVA.CveMov AND MA.SerMov = MVA.SerMov AND MA.FolMov = MVA.FolMov INNER JOIN " _
            & "                        CATBOD B ON MA.BodVta = B.CVEBOD " _
                & " WHERE    (MA.BodVta in (" & CveBodVta & ")) " & tipoProducto & " " & statusApartado & " " _
            & "  ORDER BY MVA.CveProd "
        Try
            Return Utilerias.getDataTable(StrSelect)
        Catch ex As Exception
            Throw ex
        End Try
    End Function


End Class
