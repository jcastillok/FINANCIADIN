Public Class CondonacionesRemotas

    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Me.IsPostBack = False Then

        End If
    End Sub

    Private Sub limpiarCampos()
        txtMonto.Value = ""
        cbCreditos.SelectedIndex = -1
        'gvCondRem.DataSource = Nothing
        gvCondRem.DataBind()
    End Sub

    Protected Sub gvPagos_DataBound(sender As Object, e As EventArgs) Handles gvCondRem.DataBound
        If gvCondRem.VisibleRowCount > 0 Then
            gvCondRem.Columns(1).CellStyle.HorizontalAlign = HorizontalAlign.Center
            gvCondRem.Columns(2).CellStyle.HorizontalAlign = HorizontalAlign.Center
            TryCast(gvCondRem.Columns(3), DevExpress.Web.GridViewDataTextColumn).PropertiesTextEdit.DisplayFormatString = "c"
            gvCondRem.Columns(4).CellStyle.HorizontalAlign = HorizontalAlign.Center
            gvCondRem.Columns(5).CellStyle.HorizontalAlign = HorizontalAlign.Center
        End If

    End Sub

    Protected Sub btnGuardar_Click(sender As Object, e As EventArgs) Handles btnGuardar.Click
        Try

            Dim camposVacios As String = ""

            If txtMonto.Text = "" Then camposVacios = camposVacios & "Monto, "
            If cbCreditos.SelectedIndex = -1 Then camposVacios = camposVacios & "Cliente, "


            If camposVacios <> "" Then
                camposVacios = camposVacios.Remove(camposVacios.LastIndexOf(","), 2)
                If camposVacios.Contains(",") Then
                    Utilerias.MensajeAlerta(String.Format("Los campos <b>{0}</b> no pueden estar vacios.", camposVacios), Me, True)
                Else
                    Utilerias.MensajeAlerta(String.Format("El campo <b>{0}</b> no puede estar vacío.", camposVacios), Me, True)
                End If
                Return
            End If

            Dim condRem As New clsCondonacionRemota
            condRem.Id_Credito = cbCreditos.Value
            condRem.Monto = txtMonto.Value
            condRem.Login = Session("Usuario").Login

            If clsCondonacionRemota.insertar(condRem) Then
                gvCondRem.DataBind()
                cbCreditos.DataBind()
                limpiarCampos()
            End If

        Catch ex As Exception
            Utilerias.MensajeAlerta("Ha surgido un problema al capturar la condonación remota!", Me.Page, True)
        End Try

    End Sub

    Protected Sub btnLimpiar_Click(sender As Object, e As EventArgs) Handles btnLimpiar.Click
        limpiarCampos()
    End Sub

    Protected Sub gvCondRem_StartRowEditing(sender As Object, e As DevExpress.Web.Data.ASPxStartRowEditingEventArgs) Handles gvCondRem.StartRowEditing
        Dim id = e.EditingKeyValue.ToString()

        Dim datasource = "SELECT C.Id AS Id_Credito, REPLACE(CLI.PrimNombre + ' ' + CLI.SegNombre 
                                           + ' ' + CLI.PrimApellido + ' ' + CLI.SegApellido, '  ', ' ') AS Cliente 
                                           FROM CREDITOS AS C INNER JOIN 
                                                CLIENTES AS CLI ON C.Id_Cliente = CLI.Id 
                                                WHERE (C.Liquidado = 0) AND (C.Id NOT IN (SELECT Id_Credito FROM CONDONACIONESREMOTAS WHERE Aplicado = 0 AND Cancelado = 0)) UNION
                                           SELECT C.Id AS Id_Credito, REPLACE(CLI.PrimNombre + ' ' + CLI.SegNombre 
                                           + ' ' + CLI.PrimApellido + ' ' + CLI.SegApellido, '  ', ' ') AS Cliente 
                                           FROM CREDITOS AS C INNER JOIN 
                                                CLIENTES AS CLI ON C.Id_Cliente = CLI.Id INNER JOIN 
                                                CONDONACIONESREMOTAS AS CR ON C.Id = CR.Id_Credito  
                                                WHERE CR.Id = " & id & " ORDER BY Cliente"

        SqlDataSource6.SelectCommand = datasource
        SqlDataSource6.DataBind()

        'Dim id_Credito = gvCondRem.GetRowValues(gvCondRem.EditingRowVisibleIndex, "Id_Credito")
        'cb.SelectedItem = cb.Items.FindByValue(id_Credito)
    End Sub

    Protected Sub gvCondRem_RowUpdating(sender As Object, e As DevExpress.Web.Data.ASPxDataUpdatingEventArgs) Handles gvCondRem.RowUpdating
        Dim cb As DevExpress.Web.ASPxComboBox = gvCondRem.FindEditRowCellTemplateControl(gvCondRem.Columns("Id_Credito"), "cbCredito")
        Dim condRem = clsCondonacionRemota.obtener(gvCondRem.GetRowValues(gvCondRem.EditingRowVisibleIndex, "Id_Credito"))
        condRem.Monto = e.NewValues("Monto")
        condRem.Id_Credito = cb.Value
        clsCondonacionRemota.actualizar(condRem)
        gvCondRem.CancelEdit()
        gvCondRem.DataBind()
        cbCreditos.DataBind()
        e.Cancel = True
    End Sub

    Protected Sub gvCondRem_RowDeleting(sender As Object, e As DevExpress.Web.Data.ASPxDataDeletingEventArgs) Handles gvCondRem.RowDeleting
        clsCondonacionRemota.cancelar(e.Keys.Item(0), Session("Usuario").Login)
        gvCondRem.DataBind()
        cbCreditos.DataBind()
        e.Cancel = True
    End Sub
End Class