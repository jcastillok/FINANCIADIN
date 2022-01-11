Imports System.Data.SqlClient

Public Class CapturaCreditoCarteraEmp
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

    End Sub

    Protected Sub btnRegistrar_Click(sender As Object, e As EventArgs) Handles btnRegistrar.Click
        Dim solicitudRequest As clsSolicitud = Nothing
        Dim mensajeResp As mensajeTransaccion = Nothing
        Dim deudaTotal As Double = 0.0
        Try
            ' Recuperar el usuario para determinar el tipo
            Dim clUsuario As clsUsuario
            clUsuario = DirectCast(Session("Usuario"), clsUsuario)

            ' armar el objeto para guardar en la bd
            solicitudRequest = New clsSolicitud()

            solicitudRequest.Id_Cliente = cbClientes.Value
            solicitudRequest.FecSol = deFecCap.Value
            solicitudRequest.MontoSol = monto.Value
            solicitudRequest.PlazoSol = cbPlazoSol.Value
            solicitudRequest.TasaReferencia = cbTasaRef.Value
            solicitudRequest.TipoAmort = cbTipAmort.Value
            solicitudRequest.TasaMoratoria = cbTasaMora.Value
            solicitudRequest.Id_Sucursal = cbSuc.Value
            solicitudRequest.Login = clUsuario.Login

            ' Registrar la solicitud del cliente
            mensajeResp = clsSolicitud.SolicitudAlta(solicitudRequest)

            If Not mensajeResp.codigoRespuesta.Equals("000000") Then
                Utilerias.MensajeAlerta("Codigo Respuesta: " + mensajeResp.codigoRespuesta + " Mensaje Respuesta: " + mensajeResp.mensajeRespuesta, Me, True)
                Return
            End If

            ' registrar el crédito
            solicitudRequest.Id = mensajeResp.consecutivo
            ' calcular deuda total
            deudaTotal = solicitudRequest.MontoSol + (solicitudRequest.MontoSol * Convert.ToDouble(ValTasa.Text.ToString()))
            mensajeResp = clsCredito.CreditoALT(solicitudRequest, deudaTotal)
            If Not mensajeResp.codigoRespuesta.Equals("000000") Then
                Utilerias.MensajeAlerta("Codigo Respuesta: " + mensajeResp.codigoRespuesta + " Mensaje Respuesta: " + mensajeResp.mensajeRespuesta, Me, True)
                Return
            End If

            cbClientes.DataSource = CargaComboClintes()
            cbClientes.DataBind()
            Utilerias.MensajeConfirmacion("Codigo Respuesta: " + mensajeResp.codigoRespuesta + " Mensaje Respuesta: " + mensajeResp.mensajeRespuesta, Me, True)
            ReinicioValores()
        Catch ex As Exception
            If IsNothing(mensajeResp) Then
                mensajeResp = New mensajeTransaccion
                mensajeResp.codigoRespuesta = "000002"
                mensajeResp.mensajeRespuesta = "OCURRIO UN PROBLEMA AL REGISTRAR LA SOLICITUD DEL CRÉDITO " + "\n Menaje tecnico: " + ex.Message
            End If

        End Try

    End Sub

    Protected Sub cbTasaRef_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cbTasaRef.SelectedIndexChanged
        ' Bucar el valor de la tasa
        Dim idtasa = cbTasaRef.Value
        Dim valorTasa As Double = 0.0
        Dim StrSelect As String
        StrSelect = "SELECT Valor FROM CATTASAINTERES where id=" & idtasa

        Dim pago As New clsPago
        Dim conexion As String = clsConexion.ConnectionString
        Using Con As New SqlConnection(conexion)
            Con.Open()
            Using Com As New SqlCommand(StrSelect, Con)
                Using rst = Com.ExecuteReader()
                    If rst.HasRows Then
                        While rst.Read
                            valorTasa = rst("Valor")

                        End While

                    End If
                End Using
            End Using
        End Using
        ValTasa.Text = valorTasa
    End Sub

    Private Function CargaComboClintes() As DataTable
        Dim dt As DataTable
        Dim da As SqlDataAdapter
        Dim query As String = "SELECT     Id, REPLACE(PrimNombre + ' ' + IsNull(SegNombre,'') + ' ' + PrimApellido + ' ' + IsNull(SegApellido,''), '  ', ' ') AS Nombre
FROM         CLIENTES
WHERE     (Id NOT IN
                          (SELECT     Id_Cliente 
                            FROM        CREDITOS
                            WHERE       (estatusID  in (1,3)) and Liquidado = 0 ))
							AND TIPO = 4
ORDER BY Nombre"

        Dim conexion As String = clsConexion.ConnectionString
        Using Con As New SqlConnection(conexion)
            Con.Open()

            Dim command As New SqlCommand(query, Con)
            command.CommandType = CommandType.Text
            da = New SqlDataAdapter(command)
            dt = New DataTable()
            da.Fill(dt)
        End Using
    End Function


    Private Sub ReinicioValores()
        cbClientes.SelectedIndex = -1
        deFecCap.Value = ""
        monto.Text = ""
        cbPlazoSol.SelectedIndex = -1
        cbTasaRef.SelectedIndex = -1
        cbTipAmort.SelectedIndex = -1
        cbTasaMora.SelectedIndex = -1
        cbSuc.SelectedIndex = -1

    End Sub

End Class