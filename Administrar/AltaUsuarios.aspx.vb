Public Class AltaUsuarios

    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then

        End If
    End Sub

    Private Sub limpiarCampos()
        txtLogin.Text = ""
        txtPass.Text = ""
        txtConfPass.Text = ""
        txtNombre.Text = ""
        ddlSuc.SelectedValue = 0
        ddlTipo.SelectedValue = 0
        chkEstado.Checked = True
        txtPass.Enabled = True
        txtConfPass.Enabled = True
    End Sub

    Protected Sub grdUsrs_SelectedIndexChanged(sender As Object, e As EventArgs) Handles grdUsrs.SelectedIndexChanged
        txtLogin.Text = grdUsrs.SelectedRow.Cells.Item(1).Text
        txtNombre.Text = grdUsrs.SelectedRow.Cells.Item(2).Text
        ddlSuc.ClearSelection()
        ddlSuc.Items.FindByText(grdUsrs.SelectedRow.Cells.Item(3).Text).Selected = True
        ddlTipo.ClearSelection()
        ddlTipo.Items.FindByText(grdUsrs.SelectedRow.Cells.Item(4).Text).Selected = True
        Dim checkBox As CheckBox = grdUsrs.SelectedRow.Cells.Item(5).Controls(0)
        chkEstado.Checked = checkBox.Checked
        txtLogin.Enabled = False
        txtPass.Enabled = False
        txtConfPass.Enabled = False
    End Sub

    Protected Sub btnGuardar_Click(sender As Object, e As EventArgs) Handles btnGuardar.Click

        Dim camposVacios As String = ""

        If txtLogin.Text.Trim = "" Then camposVacios = camposVacios & "Login, "
        If Not clsUsuario.existeUsuario(txtLogin.Text) Then
            If txtPass.Text.Trim = "" And txtConfPass.Text.Trim = "" Then camposVacios = camposVacios & "Contraseña, Confirmación, "
        End If
        If txtNombre.Text.Trim = "" Then camposVacios = camposVacios & "Nombre, "
        If ddlSuc.SelectedValue = 0 Then camposVacios = camposVacios & "Sucursal, "
        If ddlTipo.SelectedValue = 0 Then camposVacios = camposVacios & "Tipo de Acceso, "

        If camposVacios <> "" Then
            camposVacios = camposVacios.Remove(camposVacios.LastIndexOf(","), 2)
            If camposVacios.Contains(",") Then
                Utilerias.MensajeAlerta(String.Format("Los campos <b>{0}</b> no pueden estar vacios.", camposVacios), Me, True)
            Else
                Utilerias.MensajeAlerta(String.Format("El campo <b>{0}</b> no puede estar vacío.", camposVacios), Me, True)
            End If
            Return
        End If

        Dim usuario As New clsUsuario() With {
            .Login = txtLogin.Text,
            .Nombre = txtNombre.Text,
            .IdSucursal = ddlSuc.SelectedValue,
            .Sucursal = ddlSuc.SelectedItem.Text,
            .Tipo = ddlTipo.SelectedValue,
            .DescTipo = ddlTipo.SelectedItem.Text,
            .Estado = chkEstado.Checked}

        'Si el usuario ya existia, actualiza la información, si no lo crea
        Try
            If clsUsuario.existeUsuario(usuario.Login) Then
                clsUsuario.actualizarUsuario(usuario)
                Utilerias.MensajeConfirmacion("Se ha actualizado correctamente el usuario", Me, True)
            Else
                If txtPass.Text.Trim <> "" And txtConfPass.Text.Trim <> "" Then
                    If txtPass.Text = txtConfPass.Text Then
                        usuario.establecerPass(txtPass.Text)
                        Dim StrInst = "INSERT INTO USUARIOS (Login, Pass, Nombre, Sucursal, Tipo, Estado)" _
                        & " VALUES ('" & usuario.Login & "','" & usuario.Password & "','" & usuario.Nombre & "'," _
                        & "" & usuario.IdSucursal & "," & usuario.Tipo & ",'" & usuario.Estado & "')"
                        Utilerias.setUpdInsDel(StrInst)
                        Utilerias.MensajeConfirmacion("Se ha guardado correctamente el usuario", Me, True)
                    Else
                        Utilerias.MensajeAlerta("No coinciden las contraseñas", Me, True)
                        Return
                    End If
                End If
            End If
            grdUsrs.SelectedIndex = -1
            grdUsrs.DataBind()
            limpiarCampos()
        Catch ex As Exception
            Utilerias.MensajeAlerta("¡Algo salió mal! Información Adicional: " & ex.Message, Me, True)
        End Try

    End Sub

End Class