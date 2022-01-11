Public Class AplicarLlaveCredito
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

    End Sub

    Protected Sub cbClientes_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cbClientes.SelectedIndexChanged
        If cbClientes.SelectedIndex <> -1 Then cbIdCredito.SelectedItem = cbIdCredito.Items.FindByValue(cbClientes.Value)
        cargarDatos()
    End Sub

    Private Sub cargarDatos()
        Dim credito As clsCredito = Nothing

        Try

            If cbClientes.SelectedIndex <> -1 And cbIdCredito.SelectedIndex <> -1 Then
                credito = clsCredito.obtener(cbIdCredito.Text)
                If IsNothing(credito) Then
                    Exit Sub
                End If

                montoDeuda.Text = credito.MontoPrestado

            End If

        Catch ex As Exception

        End Try
    End Sub

    Private Sub ResetCampos()
        montoDeuda.Value = ""
        llaveCredito.Value = ""
        cbClientes.SelectedIndex = -1
        cbIdCredito.SelectedIndex = -1

    End Sub

    Protected Sub btnRegistrar_Click(sender As Object, e As EventArgs) Handles btnRegistrar.Click
        Dim mensaje As mensajeTransaccion = Nothing
        Dim llaveCreditoO As clsLlaveCredito = Nothing
        Dim usuario As clsUsuario = Nothing
        Try
            llaveCreditoO = New clsLlaveCredito()
            llaveCreditoO.CreditoID = cbIdCredito.Text
            llaveCreditoO.Llave = llaveCredito.Value
            usuario = Session("Usuario")

            mensaje = clsLlaveCredito.LlaveCreditoALT(llaveCreditoO, usuario)

            If Not mensaje.codigoRespuesta.Equals("000000") Then
                Utilerias.MensajeAlerta("OCURRIO UN PROBLEMA! CODIGO: " + mensaje.codigoRespuesta + "\n MENSAJE RESPUESTA: " + mensaje.mensajeRespuesta, Me, True)

            End If
            Utilerias.MensajeAlerta("CODIGO: " + mensaje.codigoRespuesta + "\n MENSAJE RESPUESTA: " + mensaje.mensajeRespuesta, Me, True)
            ResetCampos()

        Catch ex As Exception

        End Try



    End Sub

    Protected Sub btnCancelar_Click(sender As Object, e As EventArgs) Handles btnCancelar.Click
        ResetCampos()
    End Sub
End Class