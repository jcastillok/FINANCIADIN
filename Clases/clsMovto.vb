Imports System.Data.SqlClient

Public Class clsMovto
    Public Property Id As Integer
    Public Property Id_Credito As Integer
    Public Property FecMov As DateTime
    Public Property Pago As Double
    Public Property Moratorio As Double
    Public Property ProxPago As Double
    Public Property FecProxPago As DateTime?
    Public Property Login As String
    Public Property Cancelado As Boolean
    Public Property LoginCancelado As String
    Public Property Id_CondRem As Integer?
    Public Property MontoRealCond As Double?

    Public Shared Function obtener(ID As Integer) As clsMovto
        Dim StrSelect = "SELECT *" _
                & " FROM MOVTOS" _
                & " WHERE Id = " & ID

        Dim movto As New clsMovto
        Dim conexion As String = clsConexion.ConnectionString
        Using Con As New SqlConnection(conexion)
            Con.Open()
            Using Com As New SqlCommand(StrSelect, Con)
                Using rst = Com.ExecuteReader()
                    If rst.HasRows Then
                        While rst.Read
                            movto.Id = rst("Id")
                            movto.Id_Credito = rst("Id_Credito")
                            movto.FecMov = rst("FecMov")
                            movto.Pago = rst("Pago")
                            movto.Moratorio = rst("Moratorio")
                            movto.ProxPago = If(IsDBNull(rst("ProxPago")), Nothing, rst("ProxPago"))
                            movto.FecProxPago = If(IsDBNull(rst("FecProxPago")), Nothing, rst("FecProxPago"))
                            movto.Login = rst("Login")
                            movto.Cancelado = rst("Cancelado")
                            movto.LoginCancelado = If(IsDBNull(rst("LoginCancelado")), Nothing, rst("LoginCancelado"))
                            movto.Id_CondRem = If(IsDBNull(rst("Id_CondRem")), Nothing, rst("Id_CondRem"))
                            movto.MontoRealCond = if(isdbnull(rst("MontoRealCond")), Nothing, rst("MontoRealCond"))
                        End While
                    Else
                        Return Nothing
                    End If
                End Using
            End Using
        End Using
        Return movto
    End Function

    Public Shared Function insertar(movto As clsMovto) As Boolean
        Dim strIns As String = "INSERT INTO MOVTOS (Id, Id_Credito, FecMov, Pago, Moratorio, Login, Id_CondRem, MontoRealCond) " _
            & "VALUES(" & movto.Id & ", " & movto.Id_Credito & ", convert(datetime,'" & movto.FecMov.ToShortDateString & "',103), " _
            & "" & movto.Pago & ", " & movto.Moratorio & ", '" & movto.Login & "'," & If(IsNothing(movto.Id_CondRem) Or movto.Id_CondRem = 0, "NULL", movto.Id_CondRem) & ", " _
            & "" & If(IsNothing(movto.MontoRealCond), "NULL", movto.MontoRealCond) & ")"

        Return Utilerias.setUpdInsDel(strIns)
    End Function

    Public Shared Function actualizar(movto As clsMovto) As Boolean
        Dim strUpdt As String = "UPDATE MOVTOS SET ProxPago = " & If(IsNothing(movto.ProxPago) Or movto.ProxPago = Double.NaN, "Null", movto.ProxPago) & ", " _
           & "FecProxPago = " & If(IsNothing(movto.FecProxPago), "NULL", "convert(datetime,'" & movto.FecProxPago & "',103) ") & " " _
           & "WHERE Id = " & movto.Id
        Return Utilerias.setUpdInsDel(strUpdt)
    End Function

    Public Shared Function eliminar(movto As clsMovto) As Boolean
        Dim strDlt = "DELETE FROM MOVTOS WHERE Id = " & movto.Id
        Return Utilerias.setUpdInsDel(strDlt)
    End Function

    Public Shared Function folioSiguiente() As Integer
        Dim strSlct = "SELECT TOP 1 Id FROM MOVTOS ORDER BY Id DESC"
        Dim folSig = Utilerias.getDataTable(strSlct).Rows
        Return If(folSig.Count > 0, folSig.Item(0).Item(0) + 1, 1)
    End Function


    Public Shared Function cancelarMovimiento(movto As clsMovto, ccomentario As String) As mensajeTransaccion
        Dim mensaje As mensajeTransaccion = Nothing
        Try
            Dim conexion As String = clsConexion.ConnectionString
            Using con As New SqlConnection(conexion.ToString())
                con.Open()
                ' se crea el objeto transaction
                Dim myTrans As SqlTransaction = con.BeginTransaction()

                ' se crea el objeto sqlcomand
                Dim command As New SqlCommand("BAJAMOVTOACT", con)
                command.CommandType = CommandType.StoredProcedure
                command.Parameters.AddWithValue("@Par_CreditoID", movto.Id_Credito)
                command.Parameters.AddWithValue("@Par_UsuarioLogin", movto.Login)
                command.Parameters.AddWithValue("@Par_MovtoID", movto.Id)
                command.Parameters.AddWithValue("@Par_Comentario", ccomentario)


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
                mensaje.mensajeRespuesta = "OCURRIO UN PROBLEMA AL CANCELAR EL MOVIMIENTO " + "\n Menaje tecnico: " + ex.Message
            End If

        End Try
        Return mensaje
    End Function

    ''' <summary>
    ''' metodo para dar de alta un nuevo movimiento
    ''' </summary>
    ''' <param name="movto"></param>
    ''' <param name="numdias"></param>
    ''' <returns></returns>
    Public Shared Function EltMovtoAlt(movto As clsMovto, numdias As Integer) As mensajeTransaccion
        Dim mensaje As mensajeTransaccion = Nothing
        Try
            Dim conexion As String = clsConexion.ConnectionString
            Using con As New SqlConnection(conexion.ToString())
                con.Open()
                ' se crea el objeto transaction
                Dim myTrans As SqlTransaction = con.BeginTransaction()

                ' se crea el objeto sqlcomand
                Dim command As New SqlCommand("ELTMOVTOALT", con)
                command.CommandType = CommandType.StoredProcedure
                command.Parameters.AddWithValue("@Par_Id_Credito", movto.Id_Credito)
                command.Parameters.AddWithValue("@Par_FecMov", movto.FecMov)
                command.Parameters.AddWithValue("@Par_Pago", movto.Pago)
                command.Parameters.AddWithValue("@Par_Moratorio", movto.Moratorio)
                command.Parameters.AddWithValue("@Par_ProxPago", movto.ProxPago)
                command.Parameters.AddWithValue("@Par_FecProxPago", numdias)
                command.Parameters.AddWithValue("@Par_Id_CondRem", movto.Id_CondRem)
                command.Parameters.AddWithValue("@Par_MontoRealCond", movto.MontoRealCond)


                command.Parameters.AddWithValue("Par_Salida", "S")
                command.Parameters.AddWithValue("Par_NumErr", "")
                command.Parameters.AddWithValue("Par_ErrMenDev", "")
                command.Parameters.AddWithValue("Par_Consecutivo", 0)
                command.Parameters.AddWithValue("@Par_UsuarioAlta", movto.Login)

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
                mensaje.mensajeRespuesta = "OCURRIO UN PROBLEMA AL REGISTRAR EL MOVIMIENTO " + "\n Menaje tecnico: " + ex.Message
            End If

        End Try
        Return mensaje
    End Function

End Class
