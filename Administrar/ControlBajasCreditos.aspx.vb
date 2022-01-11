Imports System.Data.SqlClient

Public Class ControlBajasCreditos
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            btnBaja.Enabled = False
            movID.Visible = False
            panelMensaje.Visible = False
        End If

        movID.Visible = False



    End Sub

    Protected Sub cbIdCredito_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cbIdCredito.SelectedIndexChanged
        If cbIdCredito.Text.Equals("") Then
            limpiarCampos()
            Exit Sub
        End If

        If cbIdCredito.SelectedIndex <> -1 Then cbIdCredito.SelectedItem = cbIdCredito.Items.FindByValue(cbIdCredito.Value)
        Dim credito As clsCredito
        credito = clsCredito.obtener(cbIdCredito.Text)
        If IsNothing(credito) Then
            Utilerias.MensajeAlerta("EL FOLIO DEL CRÉDITO INGRESADO NO SE ENCUENTRA REGISTRADO", Me, True)
            Exit Sub
        End If

        SolicitudID.Text = credito.Id_Sol
        creditoID.Text = credito.Id
        cargarDatosCredito(credito)
        Dim pagos = Convert.ToInt32(pagosRealizados.Text)
        If pagos > 0 Then
            Utilerias.MensajeAlerta("NO SE PUEDE DAR DE BAJA EL CREDITO PORQUE TIENE CUBIERTO " + pagosRealizados.Text +
                                    " LETRA(S)!!!!", Me, True)
            btnBaja.Enabled = False
            movID.Visible = False
            bajaMov.Checked = False
            Exit Sub

        End If
        btnBaja.Enabled = True

    End Sub


    Private Function cargarDatosCredito(credito As clsCredito) As clsCredito
        Dim dt As DataTable
        Dim CreditoCon As clsCreditoViewModel = Nothing
        Try
            Dim conexion As String = clsConexion.ConnectionString
            dt = clsCreditoViewModel.consultaDatosCredito(credito)

            ASPxGridView1.DataSource = dt
            ASPxGridView1.DataBind()
            garantia.Text = dt.Rows(0).Item(3)
            pagosRealizados.Text = dt.Rows(0).Item(2)
            TxtLlaveCredID.Text = dt.Rows(0).Item(4)
        Catch ex As Exception

        End Try
        Return Nothing
    End Function

    Protected Sub btnBaja_Click(sender As Object, e As EventArgs) Handles btnBaja.Click
        Dim mensaje As mensajeTransaccion = Nothing
        Dim credito As clsCredito = New clsCredito()
        Dim movto As clsMovto = Nothing
        Dim clUsuario As clsUsuario

        If cbIdCredito.Text.Equals("") Then
            Utilerias.MensajeAlerta("INGRESAR EL FOLIO DEL CRÉDITO", Me, True)
            limpiarCampos()
            Exit Sub
        End If


        credito = clsCredito.obtener(cbIdCredito.Text)
        If IsNothing(credito) Then
            Utilerias.MensajeAlerta("EL FOLIO DEL CRÉDITO INGRESADO NO SE ENCUENTRA REGISTRADO", Me, True)
            limpiarCampos()
            Exit Sub
        End If

        clUsuario = DirectCast(Session("Usuario"), clsUsuario)

        credito.Id = creditoID.Text
        credito.Login = clUsuario.Login
        credito.Id_Sol = SolicitudID.Text

        'validar si es un movimiento que se quiere dar de baja
        If bajaMov.Checked Then
            movto = New clsMovto

            If movID.Text.Equals("") Then
                Utilerias.MensajeAlerta("INGRESAR EL FOLIO DEL MOVIMIENTO DE PAGO ", Me, True)
                bajaMov.Checked = False
                Exit Sub
            End If

            ' se crea el objeto para manejar en la cancelacion de movimiento
            movto.Id = movID.Text
            movto.Id_Credito = creditoID.Text
            movto.Login = clUsuario.Login
            mensaje = clsMovto.cancelarMovimiento(movto, comentario.Text)

            If Not mensaje.codigoRespuesta.Equals("000000") Then
                Utilerias.MensajeAlerta("NO SE PUDO DAR DE BAJA EL PAGO, MENSAJE:  " + mensaje.mensajeRespuesta, Me, True)
            Else
                Utilerias.MensajeConfirmacion("LA TRANSACCIÓN FUE REALIZADA CON ÉXITO", Me, True)
            End If
            limpiarCampos()
            Exit Sub
        End If

        'validar si se tiene garantia
        Dim Esgarantia = Convert.ToBoolean(garantia.Text)

        mensaje = clsCredito.cancelarCredito(credito, clUsuario.UsuarioID, comentario.Text, Esgarantia)

        If Not mensaje.codigoRespuesta.Equals("000000") Then
            Utilerias.MensajeAlerta("ERROR!, NO SE PUEDE DAR DE BAJA EL CREDITO " + mensaje.mensajeRespuesta, Me, True)
            limpiarCampos()
            Exit Sub
        End If

        '' se valida si tiene llave de retiro, para cancelarlo.
        'Dim llaveCredito As clsLlaveCredito = New clsLlaveCredito
        'llaveCredito.LlaveCreditoID = TxtLlaveCredID.Text
        'mensaje = clsLlaveCredito.LlaveCreditoACT(llaveCredito, clsLlaveCredito.Act_LlavesCredito.Baja_Llave_Financiera, clUsuario)


        limpiarCampos()
        Utilerias.MensajeConfirmacion(mensaje.mensajeRespuesta, Me, True)

    End Sub

    Protected Sub bajaMov_CheckedChanged(sender As Object, e As EventArgs) Handles bajaMov.CheckedChanged
        If bajaMov.Checked Then
            movID.Visible = True
            btnBaja.Enabled = True
            panelMensaje.Visible = True
        Else
            movID.Visible = False
            btnBaja.Enabled = False
            panelMensaje.Visible = False
        End If

    End Sub

    Private Sub limpiarCampos()
        cbIdCredito.Text = ""
        bajaMov.Checked = False
        movID.Text = ""
        comentario.Text = ""
        SolicitudID.Text = ""
        creditoID.Text = ""
        garantia.Text = ""
        pagosRealizados.Text = ""
        btnBaja.Enabled = False
        Dim Tabla As New DataTable
        ASPxGridView1.DataSource = Tabla
        ASPxGridView1.DataBind()
        panelMensaje.Visible = False
    End Sub

    Protected Sub btnCancelar_Click(sender As Object, e As EventArgs) Handles btnCancelar.Click
        limpiarCampos()
    End Sub
End Class