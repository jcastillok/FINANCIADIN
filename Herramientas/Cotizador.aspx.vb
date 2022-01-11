Public Class Cotizador

    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Me.IsPostBack = False Then
            divTabla.Visible = False
        End If
    End Sub

    Protected Sub gvPagos_DataBound(sender As Object, e As EventArgs) Handles gvPagos.DataBound
        If gvPagos.VisibleRowCount > 0 Then
            gvPagos.Columns(0).CellStyle.HorizontalAlign = HorizontalAlign.Center
            gvPagos.Columns(1).CellStyle.HorizontalAlign = HorizontalAlign.Center
            TryCast(gvPagos.Columns(2), DevExpress.Web.GridViewDataTextColumn).PropertiesTextEdit.DisplayFormatString = "c"
            TryCast(gvPagos.Columns(3), DevExpress.Web.GridViewDataTextColumn).PropertiesTextEdit.DisplayFormatString = "c"
            TryCast(gvPagos.Columns(4), DevExpress.Web.GridViewDataTextColumn).PropertiesTextEdit.DisplayFormatString = "c"
            TryCast(gvPagos.Columns(5), DevExpress.Web.GridViewDataTextColumn).PropertiesTextEdit.DisplayFormatString = "c"
            TryCast(gvPagos.Columns(6), DevExpress.Web.GridViewDataTextColumn).PropertiesTextEdit.DisplayFormatString = "c"
            TryCast(gvPagos.Columns(7), DevExpress.Web.GridViewDataTextColumn).PropertiesTextEdit.DisplayFormatString = "c"
        End If

    End Sub

    Protected Sub btnCalcular_Click(sender As Object, e As EventArgs) Handles btnCalcular.Click

        Dim camposVacios As String = ""

        If cbAmort.SelectedIndex = -1 Then camposVacios = camposVacios & "Tipo de Amortización, "
        If txtMonto.Text = "" Then camposVacios = camposVacios & "Monto a Prestar, "
        If deFecha.Date = Nothing Then camposVacios = camposVacios & "Fecha de Inicio, "
        If cbTasa.SelectedIndex = -1 Then camposVacios = camposVacios & "Tasa de Interés, "
        If cbPlazo.SelectedIndex = -1 Then camposVacios = camposVacios & "Plazo, "
        If txtNumPagos.Text = "" Then camposVacios = camposVacios & "Numero de Pagos, "

        If camposVacios <> "" Then
            camposVacios = camposVacios.Remove(camposVacios.LastIndexOf(","), 2)
            If camposVacios.Contains(",") Then
                Utilerias.MensajeAlerta(String.Format("Los campos <b>{0}</b> no pueden estar vacios.", camposVacios), Me, True)
            Else
                Utilerias.MensajeAlerta(String.Format("El campo <b>{0}</b> no puede estar vacío.", camposVacios), Me, True)
            End If
            Return
        End If

        Dim sobretasa
        Dim duracionDias
        Dim interes
        Dim deuda

        If cbAmort.SelectedItem.Value <> 4 Then
            sobretasa = (cbTasa.Value * 12) / 365
            duracionDias = If(cbPlazo.Value = 1, txtNumPagos.Value * 7, If(cbPlazo.Value = 3, txtNumPagos.Value * 30, txtNumPagos.Value * 15))
            interes = ((txtMonto.Value * sobretasa) * (duracionDias))
            'If Not cbTasa.Text.Contains("incluido") Then interes = interes * 1.16
            deuda = Utilerias.Redondeo(txtMonto.Value + interes, 2)
            gvPagos.DataSource = Utilerias.crearTablaDePagos(deFecha.Date, txtMonto.Value, deuda, txtNumPagos.Value, cbPlazo.Value)
        Else
            sobretasa = (cbTasa.Value * 12) / 365
            sobretasa = If(cbPlazo.Value = 1, sobretasa * 7, If(cbPlazo.Value = 3, sobretasa * 30, sobretasa * 15))

            'If Not cbTasa.Text.Contains("incluido") Then
            '    sobretasa = sobretasa * 1.16
            'End If

            deuda = Math.Abs(Pmt(sobretasa, txtNumPagos.Value, txtMonto.Value)) * txtNumPagos.Value
            gvPagos.DataSource = Utilerias.crearTablaDePagosProgresiva(deFecha.Date, txtMonto.Value, deuda, sobretasa, txtNumPagos.Value, cbPlazo.Value)
        End If

        lblAdeudo.Text = String.Format("{0:0.00}", deuda)
        lblPrestamo.Text = String.Format("{0:0.00}", txtMonto.Value)
        divTabla.Visible = True
        'gvPagos.DataSource = Utilerias.crearTablaDePagos(deFecha.Date,txtMonto.Value,deuda,txtNumPagos.Value,cbPlazo.Value)
        gvPagos.DataBind()
        
    
    End Sub

    Protected Sub btnLimpiar_Click(sender As Object, e As EventArgs) Handles btnLimpiar.Click
        lblAdeudo.Text = ""
        lblPrestamo.Text = ""
        gvPagos.DataSource= Nothing
        gvPagos.DataBind()
        divTabla.Visible = False
    End Sub

End Class