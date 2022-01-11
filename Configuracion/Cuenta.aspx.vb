Public Class Cuenta

    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            Dim clUsuario As clsUsuario = Session("Usuario")
            txtLogin.Text = clUsuario.Login
            txtNombre.Text = clUsuario.Nombre

            Dim StrSuc = "SELECT Id, Descripcion FROM CATSUCURSALES"
            cbSuc.DataSource = Utilerias.getDataTable(StrSuc)
            cbSuc.ValueField = "Id"
            cbSuc.ValueType = GetType(Int32)
            cbSuc.TextField = "Descripcion"
            cbSuc.DataBind()
            cbSuc.SelectedItem = cbSuc.Items.FindByText(Session("SucActualUsr"))
        End If
    End Sub

    Private Sub limpiarCampos()
        txtPass.Text = ""
        txtPassNueva.Text = ""
        txtPassConf.Text = ""
        cbSuc.SelectedItem = cbSuc.Items.FindByText(Session("SucActualUsr"))
    End Sub

    Protected Sub imgGuardar_Click(sender As Object, e As ImageClickEventArgs) Handles imgGuardar.Click

        If (txtPass.Text.Trim = "" Or txtPassNueva.Text.Trim = "" Or txtPassConf.Text.Trim = "") And (cbSuc.SelectedItem.Text = Session("SucActualUsr")) Then Utilerias.MensajeAlerta("Debe realizar cambio de contraseña o sucursal.", Me, True) : Return

        Dim mensaje As String = ""

        If txtPass.Text.Trim <> "" And txtPassNueva.Text.Trim <> "" And txtPassConf.Text.Trim <> "" Then
            If Utilerias.Encriptar(txtPass.Text) = Session("Usuario").Password Then
                If txtPassNueva.Text = txtPassConf.Text Then
                    Dim usuario As clsUsuario = Session("Usuario")
                    usuario.Password = Utilerias.Encriptar(txtPassNueva.Text)
                    clsUsuario.actualizarUsuario(usuario)
                    mensaje = "<b>cambio de contraseña</b>"
                Else
                    Utilerias.MensajeAlerta("Los campos <b>Nueva Contraseña</b> y <b>Confirmación</b> deben ser iguales", Me, True)
                    Return
                End If
            Else
                Utilerias.MensajeAlerta("La contraseña introducida es incorrecta", Me, True)
                Return
            End If
        End If

        If cbSuc.SelectedItem.Text <> Session("SucActualUsr") Then
            Session("SucActualUsr") = cbSuc.SelectedItem.Text
            mensaje = mensaje & (If(mensaje <> "", " y <b>cambio de sucursal de la sesion</b>", "<b>cambio de sucursal de la sesion</b>"))
        End If

        Utilerias.MensajeConfirmacion(String.Format("¡El {0} se realizó exitosamente!", mensaje), Me, True)
        limpiarCampos()
    End Sub

End Class