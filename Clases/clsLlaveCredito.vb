Imports System.Data.SqlClient

Public Class clsLlaveCredito
    Public Property LlaveCreditoID As Integer
    Public Property CreditoID As Integer
    Public Property SolicitudID As Integer

    Public Property Llave As String
    Public Property estatus As Integer

    Public Enum Act_LlavesCredito
        Aplicar_Cred = 1
        Asignar_Credito = 2
        bAJA_lLAVE = 3
        Baja_Llave_Financiera = 4
    End Enum


    Public Shared Function LlaveCreditoALT(llaveCredito As clsLlaveCredito, usuario As clsUsuario) As mensajeTransaccion
        Dim mensaje As mensajeTransaccion = Nothing
        Try
            Dim conexion As String = clsConexion.ConnectionString
            Using con As New SqlConnection(conexion.ToString())
                con.Open()
                ' se crea el objeto transaction
                Dim myTrans As SqlTransaction = con.BeginTransaction()

                ' se crea el objeto sqlcomand
                Dim command As New SqlCommand("LLAVECREDITOALT", con)
                command.CommandType = CommandType.StoredProcedure
                command.Parameters.AddWithValue("@Par_SolicitudID", llaveCredito.SolicitudID)
                command.Parameters.AddWithValue("@Par_Llave", llaveCredito.Llave)

                command.Parameters.AddWithValue("Par_Salida", "S")
                command.Parameters.AddWithValue("Par_NumErr", "")
                command.Parameters.AddWithValue("Par_ErrMenDev", "")
                command.Parameters.AddWithValue("Par_Consecutivo", 0)
                command.Parameters.AddWithValue("@Par_UsuarioAlta", usuario.UsuarioID)


                ' se asigna la transaccion al comando
                command.Transaction = myTrans
                Try

                    Dim dr As SqlDataReader = command.ExecuteReader()

                    Do While dr.Read

                        mensaje = New mensajeTransaccion
                        mensaje.codigoRespuesta = dr("NumErr").ToString()
                        mensaje.mensajeRespuesta = dr("ErrMenDev").ToString()

                    Loop
                    dr.Close()
                    myTrans.Commit()

                Catch ex As Exception
                    myTrans.Rollback()
                End Try
            End Using

        Catch ex As Exception
            If IsNothing(mensaje) Then
                mensaje = New mensajeTransaccion
                mensaje.codigoRespuesta = "000001"
                mensaje.mensajeRespuesta = "OCURRIO UN PROBLEMA AL ACTIVAR EL CRÉDITO " + "\n Menaje tecnico: " + ex.Message
            End If

        End Try
        Return mensaje
    End Function


    Public Shared Function LlaveCreditoCon(llaveCredito As clsLlaveCredito, numCon As Integer) As clsLlaveCredito
        Dim llave As clsLlaveCredito = Nothing
        Try
            Dim conexion As String = clsConexion.ConnectionString
            Using con As New SqlConnection(conexion.ToString())
                con.Open()

                ' se crea el objeto sqlcomand
                Dim command As New SqlCommand("LLAVECREDITOFINCON", con)
                command.CommandType = CommandType.StoredProcedure
                command.Parameters.AddWithValue("@Par_SolicitudID", llaveCredito.SolicitudID)
                command.Parameters.AddWithValue("@Par_Llave", "")
                command.Parameters.AddWithValue("@Par_NumCon", numCon)


                Dim dr As SqlDataReader = command.ExecuteReader()

                    Do While dr.Read

                        llave = New clsLlaveCredito
                        llave.LlaveCreditoID = dr("FOLIO_LLAVE").ToString()
                        llave.Llave = dr("LLAVE").ToString()

                    Loop
                    dr.Close()

            End Using

        Catch ex As Exception
            If IsNothing(llave) Then

            End If

        End Try
        Return llave
    End Function



    Public Shared Function LlaveCreditoACT(llaveCredito As clsLlaveCredito, numAct As Integer, clUsuario As clsUsuario) As mensajeTransaccion
        Dim mensaje As mensajeTransaccion = Nothing
        Try
            Dim conexion As String = clsConexion.ConnectionString
            Using con As New SqlConnection(conexion.ToString())
                con.Open()
                ' se crea el objeto transaction
                Dim myTrans As SqlTransaction = con.BeginTransaction()

                ' se crea el objeto sqlcomand
                Dim command As New SqlCommand("LLAVECREDITOACT", con)
                command.CommandType = CommandType.StoredProcedure
                command.Parameters.AddWithValue("@Par_LlaveCreditoID", llaveCredito.LlaveCreditoID)
                command.Parameters.AddWithValue("@Par_CreditoID", llaveCredito.CreditoID)
                command.Parameters.AddWithValue("@Par_UsuarioFin", clUsuario.UsuarioID)
                command.Parameters.AddWithValue("@Par_UsuarioSaedi", clUsuario.Login)
                command.Parameters.AddWithValue("@Pa_Sucursal", "")

                command.Parameters.AddWithValue("@Par_NumAct", numAct)

                command.Parameters.AddWithValue("Par_Salida", "S")
                command.Parameters.AddWithValue("Par_NumErr", "")
                command.Parameters.AddWithValue("Par_ErrMenDev", "")
                command.Parameters.AddWithValue("Par_Consecutivo", 0)



                ' se asigna la transaccion al comando
                command.Transaction = myTrans
                Try

                    Dim dr As SqlDataReader = command.ExecuteReader()

                    Do While dr.Read

                        mensaje = New mensajeTransaccion
                        mensaje.codigoRespuesta = dr("NumErr").ToString()
                        mensaje.mensajeRespuesta = dr("ErrMenDev").ToString()

                    Loop
                    dr.Close()
                    myTrans.Commit()

                Catch ex As Exception
                    myTrans.Rollback()
                End Try
            End Using

        Catch ex As Exception
            If IsNothing(mensaje) Then
                mensaje = New mensajeTransaccion
                mensaje.codigoRespuesta = "000001"
                mensaje.mensajeRespuesta = "OCURRIO UN PROBLEMA AL ACTIVAR EL CRÉDITO " + "\n Menaje tecnico: " + ex.Message
            End If

        End Try
        Return mensaje
    End Function


End Class
