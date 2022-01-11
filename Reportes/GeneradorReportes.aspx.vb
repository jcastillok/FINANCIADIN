Public Class GeneradorReportes

    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            ddlReportes.Items.Add(New ListItem("-- Seleccione --", ""))
            ddlReportes.Items.Add(New ListItem("Estado de Cartera", "dspCartera.aspx"))
            ddlReportes.Items.Add(New ListItem("Estado de Cartera San Luis", "dspCarteraSL.aspx"))
            ddlReportes.Items.Add(New ListItem("Resumen de Créditos", "dspContabilidad.aspx"))
            ddlReportes.Items.Add(New ListItem("Detallado de Pagos", "dspDetallePagos.aspx"))
            ddlReportes.Items.Add(New ListItem("Historial de Movimientos", "dspMovtos.aspx"))
            ddlReportes.Items.Add(New ListItem("Créditos Otorgados", "dspCreditos.aspx"))
            ddlReportes.Items.Add(New ListItem("Cartera Vencida", "dspCreditosVencidos.aspx"))
            ddlReportes.Items.Add(New ListItem("Entradas Esperadas", "dspEntradasEsperadas.aspx"))
            ddlReportes.Items.Add(New ListItem("Estado de Bases", "dspBases.aspx"))

            ddlEstatusBases.Items.Add(New ListItem("Seleccione", ""))
            ddlEstatusBases.Items.Add(New ListItem("Bases Aplicadas", "APLICADO"))
            ddlEstatusBases.Items.Add(New ListItem("Bases Vigentes", "VIGENTE"))


        End If
    End Sub

    Private Sub limpiarCampos()
        ddlReportes.SelectedIndex = -1
    End Sub

    Private Sub cargarReporte()

        Dim strSolicitud As String
        Dim strFiltro As String = ""

        Select Case ddlReportes.Text
            Case "dspCartera.aspx"
                strSolicitud = "SET LANGUAGE ESPAÑOL " _
                    & "SELECT Sucursal, Asesor, Nombre, Id_Cli, NoCredito, Folio, Capital, SaldoCapital, SaldoVencido, InteresPendiente, " _
                    & "MoraGenerado, FechaUltimoPago, DiasDeAtraso, FechaInicio, FechaFinCredito, DiaDePago, CuotaDePago, PagosCubiertos, " _
                    & "PagosConAtraso, MoraGenerado + InteresPendiente + SaldoVencido AS MontoPendiente, MontoUltimoPago, FrecuenciaDePago FROM ReporteCartera "

                Dim datos As New DataSet
                Dim dt = Utilerias.getDataTable(strSolicitud)
                datos.Tables.Add(dt)
                datos.Tables(0).TableName = "ReporteCartera"
                datos.Merge(dt)

                Session("Cartera") = datos

            Case "dspCreditosVencidos.aspx"
                strSolicitud = "SET LANGUAGE ESPAÑOL " _
                    & "
                   SELECT Folio
                      ,Cliente
                      ,Sucursal
                      ,FechaUltPagoEsperado
                      ,MontoPrestado
                      ,InteresAPagar
                      ,IvaAPagar
                      ,TotalAPagar
                      ,Capital_Cobrado
                      ,Interes_Cobrado
                      ,IVA_Cobrado
                      ,Recargo_Cobrado
                      ,Ultimo_Pago_Cobrado
                      ,Monto_Ultimo_Pago_Cobrado
                  FROM ReporteCarteraVencida "

                Dim filtros As String = ""


                If deHasta.Text <> "" And deDesde.Text <> "" Then
                    strFiltro = " WHERE  c.Liquidado = 0 AND convert(datetime,'" & deDesde.Date & "',103) > FechaUltPagoEsperado  AND  convert(datetime,'" & deHasta.Date & "',103) >  FechaUltPagoEsperado "
                    filtros = "Pagos desde " & deDesde.Text & " hasta " & deHasta.Text
                ElseIf deHasta.Text = "" And deDesde.Text <> "" Then
                    strFiltro = " WHERE  convert(datetime,'" & deDesde.Date & "',103) >  FechaUltPagoEsperado  "
                    filtros = "Pagos hasta " & deHasta.Text
                ElseIf deHasta.Text <> "" And deDesde.Text = "" Then
                    strFiltro = " WHERE  convert(datetime,'" & deHasta.Date & "',103) > FechaUltPagoEsperado"
                    filtros = "Pagos desde " & deDesde.Text
                End If

                If cbSuc.SelectedIndex <> -1 And filtros = "" Then
                    strFiltro = " WHERE Sucursal = '" & cbSuc.SelectedItem.Text & "'"
                    filtros = "Sucursal = " & cbSuc.SelectedItem.Text
                ElseIf cbSuc.SelectedIndex <> -1 And filtros <> "" Then
                    strFiltro = strFiltro & " AND Sucursal = '" & cbSuc.SelectedItem.Text & "'"
                    filtros = filtros & ", Sucursal = " & cbSuc.SelectedItem.Text
                End If

                strSolicitud = strSolicitud & strFiltro

                Dim datos As New DataSet
                Dim dt = Utilerias.getDataTable(strSolicitud)
                datos.Tables.Add(dt)
                datos.Tables(0).TableName = "ReporteCarteraVencida"
                datos.Merge(dt)

                Session("CarteraVencida") = datos
                Session("parametros") = filtros


            Case "dspCarteraSL.aspx"
                strSolicitud = "SET LANGUAGE ESPAÑOL " _
                    & "SELECT Sucursal, Nombre, Id_Cli, Folio, Capital, SaldoCapital, SaldoVencido, InteresPendiente, " _
                    & "MoraGenerado, FechaUltimoPago, DiasDeAtraso, FechaInicio, FechaFinCredito, DiaDePago, CuotaDePago, PagosCubiertos, " _
                    & "PagosConAtraso, MoraGenerado + InteresPendiente + SaldoVencido AS MontoPendiente, MontoUltimoPago, FrecuenciaDePago FROM ReporteCartera_SLP_G "

                Dim datos As New DataSet
                Dim dt = Utilerias.getDataTable(strSolicitud)
                datos.Tables.Add(dt)
                datos.Tables(0).TableName = "ReporteCartera"
                datos.Merge(dt)

                Session("CarteraSL") = datos

            Case "dspContabilidad.aspx"
                strSolicitud = "SELECT * FROM ReporteContabilidad"

                Dim datos As New DataSet
                Dim dt = Utilerias.getDataTable(strSolicitud)
                datos.Tables.Add(dt)
                datos.Tables(0).TableName = "ReporteContabilidad"
                datos.Merge(dt)

                Session("Contabilidad") = datos


            Case "dspDetallePagos.aspx"
                Dim filtros = ""
                strSolicitud = "SELECT * FROM ReporteDetPagos"

                If deHasta.Text <> "" And deDesde.Text <> "" Then
                    strFiltro = " WHERE FecPago >= convert(datetime,'" & deDesde.Date & "',103) AND FecPago <= convert(datetime,'" & deHasta.Date & "',103)"
                    filtros = "Pagos desde " & deDesde.Text & " hasta " & deHasta.Text
                ElseIf deHasta.Text = "" And deDesde.Text <> "" Then
                    strFiltro = " WHERE FecPago >= convert(datetime,'" & deDesde.Date & "',103)"
                    filtros = "Pagos hasta " & deHasta.Text
                ElseIf deHasta.Text <> "" And deDesde.Text = "" Then
                    strFiltro = " WHERE FecPago <= convert(datetime,'" & deHasta.Date & "',103)"
                    filtros = "Pagos desde " & deDesde.Text
                End If

                If cbSuc.SelectedIndex <> -1 And filtros = "" Then
                    strFiltro = " WHERE Sucursal = '" & cbSuc.SelectedItem.Text & "'"
                    filtros = "Sucursal = " & cbSuc.SelectedItem.Text
                ElseIf cbSuc.SelectedIndex <> -1 And filtros <> "" Then
                    strFiltro = strFiltro & " AND Sucursal = '" & cbSuc.SelectedItem.Text & "'"
                    filtros = filtros & ", Sucursal = " & cbSuc.SelectedItem.Text
                End If

                If lbLogin.SelectedItems.Count > 0 And filtros = "" Then
                    strFiltro = " WHERE Login IN ('" & String.Join("','", lbLogin.SelectedValues.OfType(Of String).ToList()) & "')"
                    filtros = "Login IN ('" & String.Join("','", lbLogin.SelectedValues.OfType(Of String).ToList()) & "')"
                ElseIf lbLogin.SelectedItems.Count > 0 And filtros <> "" Then
                    strFiltro = strFiltro & " AND Login IN ('" & String.Join("','", lbLogin.SelectedValues.Cast(Of String).ToList()) & "')"
                    filtros = filtros & ", Login = " & String.Join("','", lbLogin.SelectedValues.Cast(Of String).ToList()) & ""
                End If

                strSolicitud = strSolicitud & strFiltro

                Dim datos As New DataSet
                Dim dt = Utilerias.getDataTable(strSolicitud)
                datos.Tables.Add(dt)
                datos.Tables(0).TableName = "ReporteDetPagos"
                datos.Merge(dt)

                Session("DetallePagos") = datos
                Session("parametros") = filtros


            Case "dspMovtos.aspx"
                Dim filtros = ""
                strSolicitud = "SELECT * FROM ReporteMOVTOS"

                If deHasta.Text <> "" And deDesde.Text <> "" Then
                    strFiltro = " WHERE FecPago >= convert(datetime,'" & deDesde.Date & "',103) AND FecPago <= convert(datetime,'" & deHasta.Date & "',103)"
                    filtros = "Pagos desde " & deDesde.Text & " hasta " & deHasta.Text
                ElseIf deHasta.Text = "" And deDesde.Text <> "" Then
                    strFiltro = " WHERE FecPago >= convert(datetime,'" & deDesde.Date & "',103)"
                    filtros = "Pagos hasta " & deHasta.Text
                ElseIf deHasta.Text <> "" And deDesde.Text = "" Then
                    strFiltro = " WHERE FecPago <= convert(datetime,'" & deHasta.Date & "',103)"
                    filtros = "Pagos desde " & deDesde.Text
                End If

                If cbSuc.SelectedIndex <> -1 And filtros = "" Then
                    strFiltro = " WHERE Sucursal = '" & cbSuc.SelectedItem.Text & "'"
                    filtros = "Sucursal = " & cbSuc.SelectedItem.Text
                ElseIf cbSuc.SelectedIndex <> -1 And filtros <> "" Then
                    strFiltro = strFiltro & " AND Sucursal = '" & cbSuc.SelectedItem.Text & "'"
                    filtros = filtros & ", Sucursal = " & cbSuc.SelectedItem.Text
                End If

                If lbLogin.SelectedItems.Count > 0 And filtros = "" Then
                    strFiltro = " WHERE NombreLogin IN ('" & String.Join("','", lbLogin.SelectedValues.OfType(Of String).ToList()) & "')"
                    filtros = "Login IN ('" & String.Join("','", lbLogin.SelectedValues.OfType(Of String).ToList()) & "')"
                ElseIf lbLogin.SelectedItems.Count > 0 And filtros <> "" Then
                    strFiltro = strFiltro & " AND NombreLogin IN ('" & String.Join("','", lbLogin.SelectedValues.Cast(Of String).ToList()) & "')"
                    filtros = filtros & ", Login = " & String.Join("','", lbLogin.SelectedValues.Cast(Of String).ToList()) & ""
                End If

                strSolicitud = strSolicitud & strFiltro

                Dim datos As New DataSet
                Dim dt = Utilerias.getDataTable(strSolicitud)
                datos.Tables.Add(dt)
                datos.Tables(0).TableName = "ReporteMovtos"
                datos.Merge(dt)

                Session("Movtos") = datos
                Session("parametros") = filtros


            Case "dspCreditos.aspx"
                Dim filtros = ""
                strSolicitud = "SELECT * FROM ReporteCreditos"

                If deHasta.Text <> "" And deDesde.Text <> "" Then
                    strFiltro = " WHERE Inicio >= convert(datetime,'" & deDesde.Date & "',103) AND Inicio <= convert(datetime,'" & deHasta.Date & "',103)"
                    filtros = "Créditos Otorgados desde " & deDesde.Text & " hasta " & deHasta.Text
                ElseIf deHasta.Text = "" And deDesde.Text <> "" Then
                    strFiltro = " WHERE Inicio >= convert(datetime,'" & deDesde.Date & "',103)"
                    filtros = "Créditos Otorgados hasta " & deHasta.Text
                ElseIf deHasta.Text <> "" And deDesde.Text = "" Then
                    strFiltro = " WHERE Inicio <= convert(datetime,'" & deHasta.Date & "',103)"
                    filtros = "Créditos Otorgados desde " & deDesde.Text
                End If


                If cbSuc.SelectedIndex <> -1 And filtros = "" Then
                    strFiltro = " WHERE Sucursal = '" & cbSuc.SelectedItem.Text & "'"
                    filtros = "Sucursal = " & cbSuc.SelectedItem.Text
                ElseIf cbSuc.SelectedIndex <> -1 And filtros <> "" Then
                    strFiltro = strFiltro & " AND Sucursal = '" & cbSuc.SelectedItem.Text & "'"
                    filtros = filtros & ", Sucursal = " & cbSuc.SelectedItem.Text
                End If

                If lbLogin.SelectedItems.Count > 0 And filtros = "" Then
                    strFiltro = " WHERE Asesor IN ('" & String.Join("','", lbLogin.SelectedValues.OfType(Of String).ToList()) & "')"
                    filtros = "Asesor IN ('" & String.Join("','", lbLogin.SelectedValues.OfType(Of String).ToList()) & "')"
                ElseIf lbLogin.SelectedItems.Count > 0 And filtros <> "" Then
                    strFiltro = strFiltro & " AND Asesor IN ('" & String.Join("','", lbLogin.SelectedValues.Cast(Of String).ToList()) & "')"
                    filtros = filtros & ", Asesor = " & String.Join("','", lbLogin.SelectedValues.Cast(Of String).ToList()) & ""
                End If

                strSolicitud = strSolicitud & strFiltro

                Dim datos As New DataSet
                Dim dt = Utilerias.getDataTable(strSolicitud)
                datos.Tables.Add(dt)
                datos.Tables(0).TableName = "ReporteCreditos"
                datos.Merge(dt)

                Session("Creditos") = datos
                Session("parametros") = filtros


            Case "dspEntradasEsperadas.aspx"
                Dim filtros = ""
                strSolicitud = "SELECT Id_Cliente AS Id_Cliente, Nombre AS Nombre, Asesor AS Asesor, Sucursal AS Sucursal, 
			                    Id_Credito, Monto AS Monto, Capital AS Capital, Interes AS Interes, IVA AS IVA, 
                                Saldo AS Saldo, SaldoCapital AS SaldoCapital, Liquidado, FechaPago, NumPago
                                FROM ReporteEntradasEsperadas"

                If deHasta.Text <> "" And deDesde.Text <> "" Then
                    strFiltro = " WHERE FechaPago BETWEEN convert(datetime,'" & deDesde.Date & "',103) AND convert(datetime,'" & deHasta.Date & "',103)"
                    filtros = "Entradas desde " & deDesde.Text & " hasta " & deHasta.Text
                ElseIf deHasta.Text = "" And deDesde.Text <> "" Then
                    strFiltro = " WHERE FechaPago >= convert(datetime,'" & deDesde.Date & "',103)"
                    filtros = "Entradas hasta " & deHasta.Text
                ElseIf deHasta.Text <> "" And deDesde.Text = "" Then
                    strFiltro = " WHERE FechaPago <= convert(datetime,'" & deHasta.Date & "',103)"
                    filtros = "Entradas desde " & deDesde.Text
                End If

                If cbSuc.SelectedIndex <> -1 And filtros = "" Then
                    strFiltro = " WHERE Sucursal = '" & cbSuc.SelectedItem.Text & "'"
                    filtros = "Sucursal = " & cbSuc.SelectedItem.Text
                ElseIf cbSuc.SelectedIndex <> -1 And filtros <> "" Then
                    strFiltro = strFiltro & " AND Sucursal = '" & cbSuc.SelectedItem.Text & "'"
                    filtros = filtros & ", Sucursal = " & cbSuc.SelectedItem.Text
                End If

                strSolicitud = strSolicitud & strFiltro & " ORDER BY Nombre, NumPago"

                Dim datos As New DataSet
                Dim dt = Utilerias.getDataTable(strSolicitud)
                datos.Tables.Add(dt)
                datos.Tables(0).TableName = "ReporteEntradasEsperadas"
                datos.Merge(dt)

                Session("Entradas") = datos
                Session("parametros") = filtros

            Case "dspBases.aspx"
                Dim filtros = ""
                strSolicitud = "SELECT * FROM ReporteBases"


                ' Se obtiene el estatus de las bases
                Dim estatusBase As String = ddlEstatusBases.Text

                filtros = " WHERE"

                If deHasta.Text <> "" And deDesde.Text <> "" Then

                    strFiltro = " WHERE (FecPago >= convert(datetime,'" & deDesde.Date & "',103) AND FecPago <= convert(datetime,'" & deHasta.Date & "',103))"
                    filtros = "Bases capturadas o aplicadas desde " & deDesde.Text & " hasta " & deHasta.Text
                ElseIf deHasta.Text = "" And deDesde.Text <> "" Then

                    strFiltro = " WHERE (FecPago >= convert(datetime,'" & deDesde.Date & "',103))"
                    filtros = "Bases capturadas o aplicadas hasta " & deHasta.Text
                ElseIf deHasta.Text <> "" And deDesde.Text = "" Then

                    strFiltro = " WHERE (FecPago <= convert(datetime,'" & deHasta.Date & "',103))"
                    filtros = "Bases capturadas o aplicadas desde " & deDesde.Text
                End If

                If cbSuc.SelectedIndex <> -1 And filtros = "" Then
                    strFiltro = " WHERE Sucursal = '" & cbSuc.SelectedItem.Text & "'"
                    filtros = "Sucursal = " & cbSuc.SelectedItem.Text
                ElseIf cbSuc.SelectedIndex <> -1 And filtros <> "" Then
                    strFiltro = strFiltro & " AND Sucursal = '" & cbSuc.SelectedItem.Text & "'"
                    filtros = filtros & ", Sucursal = " & cbSuc.SelectedItem.Text
                End If



                If estatusBase <> "" Then

                    If estatusBase = "APLICADO" Then
                        strFiltro = " WHERE EstadoGarantia = '" & estatusBase & "' and (FechaAplicacion >= convert(datetime,'" & deDesde.Date & "',103) AND FechaAplicacion <= convert(datetime,'" & deHasta.Date & "',103))"

                        If cbSuc.SelectedIndex <> -1 Then

                            strFiltro = strFiltro & " AND Sucursal = '" & cbSuc.SelectedItem.Text & "' ORDER BY Sucursal ASC, FechaAplicacion ASC"
                        Else
                            strFiltro = " WHERE EstadoGarantia = '" & estatusBase & "' and (FechaAplicacion >= convert(datetime,'" & deDesde.Date & "',103) AND FechaAplicacion <= convert(datetime,'" & deHasta.Date & "',103)) ORDER BY Sucursal ASC, FechaAplicacion ASC"

                        End If

                    End If

                    If estatusBase = "VIGENTE" And strFiltro = "" Then
                        strFiltro = " WHERE EstadoGarantia = '" & estatusBase & "' ORDER BY Sucursal ASC, FecPago ASC"
                        filtros = filtros & ", Sucursal = " & cbSuc.SelectedItem.Text
                    ElseIf estatusBase = "VIGENTE" And strFiltro <> "" Then

                        strFiltro = strFiltro & " and EstadoGarantia = '" & estatusBase & "' ORDER BY Sucursal ASC, FecPago ASC"
                    End If

                Else
                    If strFiltro = "" Then
                        strFiltro = " WHERE EstadoGarantia IN ('APLICADO', 'VIGENTE') ORDER BY Sucursal ASC, FecPago ASC"

                    Else
                        strFiltro = strFiltro & "  or (FechaAplicacion >= convert(datetime,'" & deDesde.Date & "',103) AND FechaAplicacion <= convert(datetime,'" & deHasta.Date & "',103)) AND EstadoGarantia IN ('APLICADO', 'VIGENTE') Sucursal ASC, FecPago ASC"
                    End If


                End If

                strSolicitud = strSolicitud & strFiltro

                Dim datos As New DataSet
                Dim dt = Utilerias.getDataTable(strSolicitud)
                datos.Tables.Add(dt)
                datos.Tables(0).TableName = "ReporteBases"
                datos.Merge(dt)

                Session("Bases") = datos
                Session("parametros") = filtros

        End Select

        If IsNothing(Session("Cartera")) = False Or
            IsNothing(Session("CarteraSL")) = False Or
            IsNothing(Session("Contabilidad")) = False Or
            IsNothing(Session("DetallePagos")) = False Or
            IsNothing(Session("Movtos")) = False Or
            IsNothing(Session("Creditos")) = False Or
            IsNothing(Session("Entradas")) = False Or
             IsNothing(Session("CarteraVencida")) = False Or
            IsNothing(Session("Bases")) = False Then
            Context.Response.Write("<script language='javascript'>window.open('../Reportes/dspVisor/" + ddlReportes.SelectedValue + "','_blank');</script>")
        End If

    End Sub

    Protected Sub btnGenRep_Click(sender As Object, e As EventArgs) Handles btnGenRep.Click
        If ddlReportes.SelectedIndex = -1 Then Utilerias.MensajeAlerta("Debe seleccionar un reporte", Me.Page, True) : Return
        cargarReporte()
    End Sub

    Protected Sub ddlReportes_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlReportes.SelectedIndexChanged

        If ddlReportes.SelectedIndex = -1 Then divFechas.Visible = False : Return

        Select Case ddlReportes.Text
            Case "dspCartera.aspx"
                divFechas.Visible = False
                divSuc.Visible = False
                divLogin.Visible = False
            Case "dspCarteraSL.aspx"
                divFechas.Visible = False
                divSuc.Visible = False
                divLogin.Visible = False
            Case "dspContabilidad.aspx"
                divFechas.Visible = False
                divSuc.Visible = False
                divLogin.Visible = False
            Case "dspDetallePagos.aspx", "dspMovtos.aspx"
                divFechas.Visible = True
                divSuc.Visible = True
                divLogin.Visible = True
            Case "dspCreditos.aspx"
                divFechas.Visible = True
                divSuc.Visible = True
                divLogin.Visible = True
            Case "dspCreditosVencidos.aspx"
                divFechas.Visible = True
                divSuc.Visible = True
                divLogin.Visible = False
            Case "dspEntradasEsperadas.aspx"
                divFechas.Visible = True
                divSuc.Visible = True
                divLogin.Visible = False
            Case "dspBases.aspx"
                divFechas.Visible = True
                divSuc.Visible = True
                divLogin.Visible = False
                divEstBases.Visible = True


        End Select

    End Sub

End Class