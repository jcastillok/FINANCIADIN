Public Class Catalogos

    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            ddlCatalogo.Items.Add(New ListItem("-- Seleccione --", ""))
            ddlCatalogo.Items.Add(New ListItem("Tipos de Usuario", "CATTIPOUSUARIOS"))
            ddlCatalogo.Items.Add(New ListItem("Tipos de Cliente", "CATTIPOCLIENTES"))
            ddlCatalogo.Items.Add(New ListItem("Sucursales", "CATSUCURSALES"))
            ddlCatalogo.Items.Add(New ListItem("Tipos de Amortización", "CATTIPAMORT"))
            ddlCatalogo.Items.Add(New ListItem("Tasas de Interés", "CATTASAINTERES"))
            ddlCatalogo.Items.Add(New ListItem("Tipos de Garantía", "CATTIPGARANTIAS"))
            ddlCatalogo.Items.Add(New ListItem("Días Inhábiles", "CATDIASINHAB"))
            'ddlCatalogo.Items.Add(New ListItem("Empresas", "CATEMPRESAS"))
        End If
    End Sub

    Private Sub limpiarCampos()
        lblID.Text = ""
        txtDescripcion.Text = ""
        txtNombre.Text = ""
        txtValor.Text = ""
        deFechaInhab.Text = ""
        txtDir.Text = ""
        seTel.Text = ""
        'ddlCatalogo.SelectedIndex = -1
        grdCatalogo.SelectedIndex = -1
        lblAccion.Text = "Guardar"
        If grdCatalogo.Rows.Count > 0 Then lblID.Text = Convert.ToInt32(grdCatalogo.Rows.Item(grdCatalogo.Rows.Count - 1).Cells.Item(1).Text) + 1
    End Sub

    Protected Sub ddlCatalogo_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlCatalogo.SelectedIndexChanged
        cargarCatalogo()
    End Sub

    Protected Sub grdCatalogo_SelectedIndexChanged(sender As Object, e As EventArgs) Handles grdCatalogo.SelectedIndexChanged

        lblID.Text = grdCatalogo.SelectedRow.Cells.Item(1).Text
        If ddlCatalogo.SelectedItem.Text <> "Empresas" Then txtDescripcion.Text = grdCatalogo.SelectedRow.Cells.Item(2).Text
        If ddlCatalogo.SelectedItem.Text = "Tipos de Amortización" Then txtNombre.Text = grdCatalogo.SelectedRow.Cells.Item(3).Text
        If ddlCatalogo.SelectedItem.Text = "Tipos de Garantía" Then txtValor.Text = grdCatalogo.SelectedRow.Cells.Item(4).Text
        If ddlCatalogo.SelectedItem.Text = "Días Inhábiles" Then deFechaInhab.Value = grdCatalogo.SelectedRow.Cells.Item(2).Text : txtDescripcion.Text = grdCatalogo.SelectedRow.Cells.Item(3).Text
        If ddlCatalogo.SelectedItem.Text = "Empresas" Then txtNombre.Text = grdCatalogo.SelectedRow.Cells.Item(2).Text : txtDir.Text = grdCatalogo.SelectedRow.Cells.Item(5).Text : seTel.Text = grdCatalogo.SelectedRow.Cells.Item(6).Text
        lblAccion.Text = "Actualizar"

    End Sub

    Protected Sub imgLimpiar_Click(sender As Object, e As System.Web.UI.ImageClickEventArgs) Handles imgLimpiar.Click
        limpiarCampos()
    End Sub

    Protected Sub imgGuardar_Click(sender As Object, e As ImageClickEventArgs) Handles imgGuardar.Click

        Dim camposVacios = validarCampos()

        If camposVacios <> "" Then
            camposVacios = camposVacios.Remove(camposVacios.LastIndexOf(","), 2)
            If camposVacios.Contains(",") Then
                Utilerias.MensajeAlerta(String.Format("Los campos <b>{0}</b> no pueden estar vacios.", camposVacios), Me, True)
            Else
                Utilerias.MensajeAlerta(String.Format("El campo <b>{0}</b> no puede estar vacío.", camposVacios), Me, True)
            End If
            Return
        End If

        Dim query As String = ""
        Try
            Select Case ddlCatalogo.SelectedItem.Value
                Case "CATTIPOUSUARIOS"
                    If grdCatalogo.SelectedIndex = -1 Then
                        query = "INSERT INTO CATTIPOUSUARIOS (Descripcion, Login) " _
                            & "VALUES ('" & txtDescripcion.Text & "','" & Session("Usuario").Login & "')"
                    Else
                        query = String.Format("UPDATE CATTIPOUSUARIOS SET Descripcion = '{0}', Login = '{1}' WHERE Id = {2}", txtDescripcion.Text, Session("Usuario").Login, lblID.Text)
                    End If
                Case "CATTIPOCLIENTES"
                    If grdCatalogo.SelectedIndex = -1 Then
                        query = "INSERT INTO CATTIPOCLIENTES (Descripcion, Login) " _
                            & "VALUES ('" & txtDescripcion.Text & "','" & Session("Usuario").Login & "')"
                    Else
                        query = String.Format("UPDATE CATTIPOCLIENTES SET Descripcion = '{0}', Login = '{1}' WHERE Id = {2}", txtDescripcion.Text, Session("Usuario").Login, lblID.Text)
                    End If
                Case "CATDIASINHAB"
                    If grdCatalogo.SelectedIndex = -1 Then
                        query = "INSERT INTO CATDIASINHAB (Fecha, Descripcion, Login, FecCap) " _
                            & "VALUES ('" & deFechaInhab.Date & "', '" & txtDescripcion.Text & "','" & Session("Usuario").Login & "',convert(datetime,'" & System.DateTime.Now.ToShortDateString & "',103))"
                    Else
                        query = String.Format("UPDATE CATDIASINHAB SET Descripcion = '{0}', Login = '{1}', Fecha = convert(datetime,'{2}',101) WHERE Id = {3}", txtDescripcion.Text, Session("Usuario").Login, deFechaInhab.Date.ToShortDateString, lblID.Text)
                    End If
                Case "CATSUCURSALES"
                    If grdCatalogo.SelectedIndex = -1 Then
                        query = "INSERT INTO CATSUCURSALES (Descripcion, Login) " _
                            & "VALUES ('" & txtDescripcion.Text & "','" & Session("Usuario").Login & "')"
                    Else
                        query = String.Format("UPDATE CATSUCURSALES SET Descripcion = '{0}', Login = '{1}' WHERE Id = {2}", txtDescripcion.Text, Session("Usuario").Login, lblID.Text)
                    End If
                Case "CATTIPAMORT"
                    If grdCatalogo.SelectedIndex = -1 Then
                        query = "INSERT INTO CATTIPAMORT (Nombre,Descripcion,Login) " _
                            & "VALUES ('" & txtNombre.Text & "', '" & txtDescripcion.Text & "','" & Session("Usuario").Login & "')"
                    Else
                        query = String.Format("UPDATE CATTIPAMORT SET Nombre = '{0}',Descripcion = '{1}',Login = '{2}' WHERE Id = {3}", txtNombre.Text, txtDescripcion.Text, Session("Usuario").Login, lblID.Text)
                    End If
                Case "CATTASAINTERES"
                    If grdCatalogo.SelectedIndex = -1 Then
                        query = "INSERT INTO CATTASAINTERES (Descripcion,Login) " _
                            & "VALUES ('" & txtDescripcion.Text & "','" & Session("Usuario").Login & "')"
                    Else
                        query = String.Format("UPDATE CATTASAINTERES SET Descripcion = '{0}', Login = '{1}' WHERE Id = {2}", txtDescripcion.Text, Session("Usuario").Login, lblID.Text)
                    End If
                Case "CATTIPGARANTIAS"
                    If grdCatalogo.SelectedIndex = -1 Then
                        query = "INSERT INTO CATTIPGARANTIAS (Nombre,Valor,Descripcion,Login) " _
                            & "VALUES ('" & txtNombre.Text & "','" & txtValor.Text & "','" & txtDescripcion.Text & "','" & Session("Usuario").Login & "')"
                    Else
                        query = String.Format("UPDATE CATTIPGARANTIAS SET Descripcion = '{0}', Login = '{1}' WHERE Id = {2}", txtDescripcion.Text, Session("Usuario").Login, lblID.Text)
                    End If
                Case "CATEMPRESAS"
                    If grdCatalogo.SelectedIndex = -1 Then
                        query = "INSERT INTO CATEMPRESAS (Nombre,Direccion,Telefono,Login) " _
                            & "VALUES ('" & txtNombre.Text & "','" & txtDir.Text & "'," & seTel.Value & ",'" & Session("Usuario").Login & "')"
                    Else
                        query = "UPDATE CATEMPRESAS SET Nombre = " & txtNombre.Text & ", Direccion = " & txtDir.Text & ", " _
                                & "Telefono = " & seTel.Value & ", Login = " & Session("Usuario").Login & " WHERE Id = '" & lblID.Text & "'"
                    End If
                Case Else
                    Utilerias.MensajeAlerta("Debe seleccionar un catálogo", Me, True)
            End Select
            If query <> "" Then
                Utilerias.setUpdInsDel(query)
                limpiarCampos()
                cargarCatalogo()
                Utilerias.MensajeConfirmacion("Se han guardado los cambios con éxito!", Me, True)
            End If
        Catch
            Utilerias.MensajeAlerta("Ha ocurrido un problema durante la captura!", Me, True)
        End Try

    End Sub

    Protected Sub imgEliminar_Click(sender As Object, e As ImageClickEventArgs) Handles imgEliminar.Click

    End Sub

    Private Function validarCampos() As String

        Dim camposVacios As String = ""
        If txtDescripcion.Text.Trim = "" And ddlCatalogo.SelectedItem.Value <> "CATEMPRESAS" Then camposVacios = camposVacios & "Descripcion, "

        If ddlCatalogo.SelectedItem.Value = "CATTIPAMORT" Then
            If txtDescripcion.Text.Trim = "" Then camposVacios = camposVacios & "Descripcion, "
            If txtNombre.Text.Trim = "" Then camposVacios = camposVacios & "Nombre, "
        ElseIf ddlCatalogo.SelectedItem.Value = "CATDIASINHAB" Then
            If deFechaInhab.Text.Trim = "" Then camposVacios = camposVacios & "Fecha Inhabil, "
        ElseIf ddlCatalogo.SelectedItem.Value = "CATTIPGARANTIAS" Then
            If txtNombre.Text.Trim = "" Then camposVacios = camposVacios & "Nombre, "
            If txtValor.Text.Trim = "" Then camposVacios = camposVacios & "Valor, "
        ElseIf ddlCatalogo.SelectedItem.Value = "CATEMPRESAS" Then
            If txtNombre.Text.Trim = "" Then camposVacios = camposVacios & "Nombre, "
            If txtDir.Text.Trim = "" Then camposVacios = camposVacios & "Direccion, "
            If seTel.Text.Trim = "" Then camposVacios = camposVacios & "Telefono, "
        End If

        Return camposVacios
    End Function

    Private Sub cargarCatalogo()
        Dim campos = "Id"

        If ddlCatalogo.SelectedItem.Text = "Tipos de Amortización" Then
            ASPxFormLayout1.Items.Item(1).ClientVisible = True
            ASPxFormLayout1.Items.Item(2).ClientVisible = True
            ASPxFormLayout1.Items.Item(3).ClientVisible = False
            ASPxFormLayout1.Items.Item(4).ClientVisible = False
            ASPxFormLayout1.Items.Item(5).ClientVisible = False
            ASPxFormLayout1.Items.Item(6).ClientVisible = False
            campos = campos + ", Descripcion, Nombre"
        ElseIf ddlCatalogo.SelectedItem.Text = "Tipos de Garantía" Then
            ASPxFormLayout1.Items.Item(1).ClientVisible = True
            ASPxFormLayout1.Items.Item(2).ClientVisible = True
            ASPxFormLayout1.Items.Item(3).ClientVisible = True
            ASPxFormLayout1.Items.Item(4).ClientVisible = False
            ASPxFormLayout1.Items.Item(5).ClientVisible = False
            ASPxFormLayout1.Items.Item(6).ClientVisible = False
            campos = campos + ", Descripcion, Nombre, Valor"
        ElseIf ddlCatalogo.SelectedItem.Text = "Días Inhábiles" Then
            ASPxFormLayout1.Items.Item(1).ClientVisible = True
            ASPxFormLayout1.Items.Item(2).ClientVisible = False
            ASPxFormLayout1.Items.Item(3).ClientVisible = False
            ASPxFormLayout1.Items.Item(4).ClientVisible = True
            ASPxFormLayout1.Items.Item(5).ClientVisible = False
            ASPxFormLayout1.Items.Item(6).ClientVisible = False
            campos = "Id, Convert(varchar(10), Fecha, 101) AS Fecha, Descripcion"
        ElseIf ddlCatalogo.SelectedItem.Text = "Empresas" Then
            ASPxFormLayout1.Items.Item(1).ClientVisible = False
            ASPxFormLayout1.Items.Item(2).ClientVisible = True
            ASPxFormLayout1.Items.Item(3).ClientVisible = False
            ASPxFormLayout1.Items.Item(4).ClientVisible = False
            ASPxFormLayout1.Items.Item(5).ClientVisible = True
            ASPxFormLayout1.Items.Item(6).ClientVisible = True
            campos = campos + ", Nombre, Direccion, Telefono"
        Else
            ASPxFormLayout1.Items.Item(1).ClientVisible = True
            ASPxFormLayout1.Items.Item(2).ClientVisible = False
            ASPxFormLayout1.Items.Item(3).ClientVisible = False
            ASPxFormLayout1.Items.Item(4).ClientVisible = False
            ASPxFormLayout1.Items.Item(5).ClientVisible = False
            ASPxFormLayout1.Items.Item(6).ClientVisible = False
            campos = campos + ", Descripcion"
        End If

        If ddlCatalogo.SelectedValue <> "" Then
            Dim StrSelect = String.Format("SELECT {0} FROM {1}", campos, ddlCatalogo.SelectedValue)
            grdCatalogo.DataSource = Utilerias.getDataTable(StrSelect)
            grdCatalogo.DataBind()
        End If

        If grdCatalogo.Rows.Count > 0 Then
            lblID.Text = Convert.ToInt32(grdCatalogo.Rows.Item(grdCatalogo.Rows.Count - 1).Cells.Item(1).Text) + 1
        Else
            lblID.Text = "1"
        End If
    End Sub

    Protected Sub grdCatalogo_RowDataBound(sender As Object, e As GridViewRowEventArgs) Handles grdCatalogo.RowDataBound
        'If grdCatalogo.Rows.Count > 0 Then e.Row.Cells(1).Visible = False
    End Sub

End Class