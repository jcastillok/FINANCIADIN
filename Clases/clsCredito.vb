Imports System.Data.SqlClient

Public Class clsCredito
    Public Property Id As Integer
    Public Property Id_Sol As Integer
    Public Property Id_Cliente As Integer
    Public Property FecInicio As DateTime
    Public Property FecPrimPago As DateTime
    Public Property FecUltPago As DateTime
    Public Property MontoPrestado As Double
    Public Property Adeudo As Double
    Public Property Plazo As Integer
    Public Property NumPagos As Integer
    Public Property TasaReferencia As Integer
    Public Property TipoAmort As Integer
    Public Property Sobretasa As Double
    Public Property Tasa As Double
    Public Property IncluyeIVA As Boolean
    Public Property TasaMoratoria As Double
    Public Property TipoGarantia As Integer
    Public Property Impuesto As Double
    Public Property Login As String
    Public Property FecCaptura As DateTime
    Public Property Liquidado As Boolean


    Public Enum Act_Credito
        Act_Dar_Baja_Credito = 1
        Act_Activar_Credito_Elite = 2
    End Enum

    Public Shared Function cancelarBase(credito As clsCredito, usuarioID As Integer) As mensajeTransaccion
        Dim mensaje As mensajeTransaccion = Nothing
        Try
            Dim conexion As String = clsConexion.ConnectionString
            Using con As New SqlConnection(conexion.ToString())
                con.Open()
                ' se crea el objeto transaction
                Dim myTrans As SqlTransaction = con.BeginTransaction()

                ' se crea el objeto sqlcomand
                Dim command As New SqlCommand("ADMBAJACREDPRO", con)
                command.CommandType = CommandType.StoredProcedure
                command.Parameters.AddWithValue("@Par_CreditoID", credito.Id)
                command.Parameters.AddWithValue("@Par_UsuarioLogin", credito.Login)
                command.Parameters.AddWithValue("@Par_UsuarioID", usuarioID)

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
                mensaje.mensajeRespuesta = "OCURRIO UN PROBLEMA AL CANCELAR UNA BASE " + "\n Menaje tecnico: " + ex.Message
            End If

        End Try
        Return mensaje
    End Function

    ''' <summary>
    ''' cancelacion del credito afectando la tabla de creditos
    ''' </summary>
    ''' <param name="credito"></param>
    ''' <param name="usuarioID"></param>
    ''' <returns></returns>
    Public Shared Function cancelarCredito(credito As clsCredito, usuarioID As Integer, comentario As String, esgarantia As Boolean) As mensajeTransaccion
        Dim mensaje As mensajeTransaccion = Nothing
        Try
            Dim conexion As String = clsConexion.ConnectionString
            Using con As New SqlConnection(conexion.ToString())
                con.Open()
                ' se crea el objeto transaction
                Dim myTrans As SqlTransaction = con.BeginTransaction()

                If esgarantia Then
                    ' se crea el objeto sqlcomand
                    Dim command1 As New SqlCommand("ADMBAJACREDPRO", con)
                    command1.CommandType = CommandType.StoredProcedure
                    command1.Parameters.AddWithValue("@Par_CreditoID", credito.Id)
                    command1.Parameters.AddWithValue("@Par_UsuarioLogin", credito.Login)
                    command1.Parameters.AddWithValue("@Par_UsuarioID", usuarioID)

                    command1.Parameters.AddWithValue("Par_Salida", "S")
                    command1.Parameters.AddWithValue("Par_NumErr", "")
                    command1.Parameters.AddWithValue("Par_ErrMenDev", "")
                    command1.Parameters.AddWithValue("Par_Consecutivo", 0)
                    ' se asigna la transaccion al comando
                    command1.Transaction = myTrans
                    Try
                        'command.ExecuteNonQuery()
                        'myTrans.Commit()
                        Dim dr As SqlDataReader = command1.ExecuteReader()

                        Do While dr.Read

                            mensaje = New mensajeTransaccion
                            mensaje.codigoRespuesta = dr("NumErr").ToString()
                            mensaje.mensajeRespuesta = dr("ErrMenDev").ToString()

                        Loop
                        dr.Close()
                        If Not mensaje.codigoRespuesta.Equals("000000") Then
                            Throw New Exception
                        End If

                    Catch ex As Exception
                        myTrans.Rollback()
                        Return mensaje
                    End Try

                End If

                mensaje = Nothing
                ' se crea el objeto sqlcomand
                Dim command As New SqlCommand("ADMCREDITOSACT", con)
                command.CommandType = CommandType.StoredProcedure
                command.Parameters.AddWithValue("@Par_CreditoID", credito.Id)
                command.Parameters.AddWithValue("@Par_UsuarioBaja", usuarioID)
                command.Parameters.AddWithValue("@Par_Comentario", comentario)
                command.Parameters.AddWithValue("@Par_SolicitudID", credito.Id_Sol)
                command.Parameters.AddWithValue("@Par_NumAct", 1)

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
                    If Not mensaje.codigoRespuesta.Equals("000000") Then
                        Throw New Exception
                    End If
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
                mensaje.mensajeRespuesta = "OCURRIO UN PROBLEMA AL CANCELAR EL CREDITO " + "\n Menaje tecnico: " + ex.Message
            End If

        End Try
        Return mensaje
    End Function


    Public Shared Function obtener(ID As Integer, Optional EsIdCliente As Boolean = False, Optional EsCondicionPersonalizada As Boolean = False, Optional Condicion As String = "") As clsCredito

        If EsIdCliente Then
            Condicion = String.Format("C.Id_Cliente = {0} AND C.Liquidado = 0 AND EstatusID = 1", ID)
        ElseIf EsCondicionPersonalizada = False Then
            Condicion = "EstatusID = 1 AND C.Id = " & ID
        End If

        Dim clCredito As New clsCredito
        Dim StrSelect As String = "SELECT C.Id, C.Id_Sol, C.Id_Cliente, C.Id_Aval, C.FecInicio, C.FecPrimPago, C.FecUltPago, C.MontoPrestado, C.Adeudo, C.Plazo, C.NumPagos, 
                                          C.TasaRef, C.TipoAmort, C.Sobretasa, C.TasaMoratoria, C.TipoGarantia, Impuesto, C.Login, C.FecCaptura, C.Liquidado, CTI.IncluyeIVA, CTI.Valor AS Tasa
                                   FROM CREDITOS C INNER JOIN
	                                    CATTASAINTERES CTI ON C.TasaRef = CTI.Id" _
                               & " WHERE " & Condicion
        Dim conexion As String = clsConexion.ConnectionString
        Using Con As New SqlConnection(conexion)
            Con.Open()
            Using Com As New SqlCommand(StrSelect, Con)
                Using rst = Com.ExecuteReader()
                    If rst.HasRows Then
                        While rst.Read
                            clCredito.Id = rst("Id")
                            clCredito.Id_Sol = rst("Id_Sol")
                            clCredito.Id_Cliente = rst("Id_Cliente")
                            clCredito.FecInicio = rst("FecInicio")
                            clCredito.FecPrimPago = rst("FecPrimPago")
                            clCredito.FecUltPago = rst("FecUltPago")
                            clCredito.MontoPrestado = rst("MontoPrestado")
                            clCredito.Adeudo = rst("Adeudo")
                            clCredito.Plazo = rst("Plazo")
                            clCredito.NumPagos = rst("NumPagos")
                            clCredito.TasaReferencia = rst("TasaRef")
                            clCredito.TipoAmort = rst("TipoAmort")
                            clCredito.Sobretasa = rst("Sobretasa")
                            clCredito.TasaMoratoria = rst("TasaMoratoria")
                            clCredito.TipoGarantia = rst("TipoGarantia")
                            clCredito.Impuesto = rst("Impuesto")
                            clCredito.Login = rst("Login")
                            clCredito.FecCaptura = rst("FecCaptura")
                            clCredito.Liquidado = rst("Liquidado")
                            clCredito.IncluyeIVA = rst("IncluyeIVA")
                            clCredito.Tasa = rst("Tasa")
                        End While
                    Else
                        Return Nothing
                    End If
                End Using
            End Using
        End Using
        Return clCredito
    End Function

    Public Shared Function ConsultarTasaMora(credito As clsCredito) As Double
        Dim valorTasaMora As Double = 0
        Try
            Dim StrSelect As String = "SELECT  Tasa	FROM CATTASAMORA WHERE ID =  " & credito.TasaMoratoria
            Dim conexion As String = clsConexion.ConnectionString
            Using Con As New SqlConnection(conexion)
                Con.Open()
                Using Com As New SqlCommand(StrSelect, Con)
                    Using rst = Com.ExecuteReader()
                        If rst.HasRows Then
                            While rst.Read
                                valorTasaMora = rst("Tasa")

                            End While
                        Else
                            Return Nothing
                        End If
                    End Using
                End Using
            End Using
        Catch ex As Exception

        End Try
        Return valorTasaMora
    End Function


    Public Shared Function insertar(Id_Sol As Integer, FecPrimPago As DateTime, FecUltPago As DateTime, Adeudo As Double) As Boolean
        'Dim strIns As String = "INSERT INTO CREDITOS (Id_Sol,Id_Cliente,FecInicio,FecPrimPago,FecUltPago,MontoPrestado,Adeudo,Plazo," _
        ' & "NumPagos,TasaRef,TipoAmort,Sobretasa,TasaMoratoria,Impuesto,Login,FecCaptura)" _
        ' & "VALUES (" & credito.Id_Sol & "," & credito.Id_Cliente & ",convert(datetime,'" & credito.FecInicio & "',103),convert(datetime,'" & credito.FecPrimPago & "',103)," _
        ' & "convert(datetime,'" & credito.FecUltPago & "',103)," & credito.MontoPrestado & "," & credito.Adeudo & "," & credito.Plazo & "," _
        ' & "" & credito.NumPagos & "," & credito.TasaReferencia & "," & credito.TipoAmort & "," _
        ' & "" & credito.Sobretasa & "," & credito.TasaMoratoria & "," & credito.Impuesto & ",'" & credito.Login & "',convert(datetime,'" & credito.FecCaptura & "',103))"
        Dim strIns As String = "INSERT INTO CREDITOS (Id_Sol, Id_Cliente, Id_Aval, FecInicio, FecPrimPago, FecUltPago, TipoGarantia, MontoPrestado, Adeudo, " _
            & "Plazo, NumPagos, TasaRef, TipoAmort, Sobretasa, TasaMoratoria, Impuesto, Login, FecCaptura) " _
            & "SELECT Id, Id_Cliente, Id_Aval, FecDisposicion, convert(datetime,'" & FecPrimPago & "',103), convert(datetime,'" & FecUltPago & "',103), TipoGarantia, " _
            & "MontoAut, " & Adeudo & ", PlazoAut, NumPagosAut,TasaRef, TipoAmort, Sobretasa, TasaMoratoria, Impuesto, Login, convert(datetime,'" & System.DateTime.Now.ToShortDateString & "',103)" _
            & "FROM SOLICITUDES WHERE SOLICITUDES.ID = " & Id_Sol

        Return Utilerias.setUpdInsDel(strIns)
    End Function

    'Public Shared Function actualizar(solicitud As clsSolicitud) As Boolean
    'Dim strUpdt As String
    'strUpdt = " UPDATE CREDITOS" _
    '    & " SET Id_Cliente = " & solicitud.Id_Cliente & ", FecSolicitud = '" & solicitud.FecSol & "', FecAutorizado = '" & solicitud.FecAut & "'," _
    '    & " FecDisposicion = '" & solicitud.FecDisp & "', MontoSolicitud = " & solicitud.MontoSol & ", MontoAut = '" & solicitud.MontoAut & "'," _
    '    & " PlazoSol = " & solicitud.PlazoSol & ", PlazoAut = " & solicitud.PlazoAut & ", NumPagosSol = " & solicitud.NumPagosSol & "," _
    '    & " NumPagosAut = " & solicitud.NumPagosAut & ", TasaRef = " & solicitud.TasaReferencia & ", TipoAmort = " & solicitud.TipoAmort & "," _
    '    & " Sobretasa = " & solicitud.Sobretasa & ", TasaMoratoria = " & solicitud.TasaMoratoria & ", Impuesto = " & solicitud.Impuesto & "," _
    '    & " TipoGarantia = '" & solicitud.TipoGarantia & "', ClaseGarantia = '" & solicitud.ClaseGarantia & "', ValorGarantia = " & solicitud.ValorGarantia & "," _
    '    & " DescripGarantia = '" & solicitud.DescripGarantia & "', IngresoCliente = " & solicitud.IngresoCliente & ", TipoPago = " & solicitud.TipoPago & "," _
    '    & " LiquidaAnterior = " & solicitud.LiquidaAnterior & ", Id_A_Liquidar = " & solicitud.Id_A_Liquidar & ", MontoLiquid = " & solicitud.MontoALiquidar & "," _
    '    & " Pago = " & solicitud.Pago & ", Login = '" & solicitud.Login & "', FecUltMod = '" & System.DateTime.Now.Date & "'" _
    '    & " WHERE (ID = " & solicitud.Id & ") "
    'Utilerias.setUpdInsDel(strUpdt)
    'End Function

    Public Shared Function tieneCredActivo(Id_Sol As Integer) As Boolean
        Dim clSolicitud As New clsSolicitud
        Dim StrSelect As String = "SELECT Id_Sol" _
        & " FROM [CREDITOS]" _
        & " WHERE Id_Cliente = (SELECT Id_Cliente FROM CREDITOS WHERE Id_Sol = " & Id_Sol & ") AND Liquidado = 0 AND estatusID <>2"
        Dim conexion As String = clsConexion.ConnectionString
        Using Con As New SqlConnection(conexion)
            Con.Open()
            Using Com As New SqlCommand(StrSelect, Con)
                Using rst = Com.ExecuteReader()
                    If rst.HasRows Then
                        Return True
                    End If
                End Using
            End Using
        End Using
        Return False
    End Function


    Public Shared Function CreditoACT(credito As clsCredito, comentario As String, usuario As clsUsuario, numAct As Integer) As mensajeTransaccion
        Dim mensaje As mensajeTransaccion = Nothing
        Try
            Dim conexion As String = clsConexion.ConnectionString
            Using con As New SqlConnection(conexion.ToString())
                con.Open()
                ' se crea el objeto transaction
                Dim myTrans As SqlTransaction = con.BeginTransaction()

                ' se crea el objeto sqlcomand
                Dim command As New SqlCommand("ADMCREDITOSACT", con)
                command.CommandType = CommandType.StoredProcedure
                command.Parameters.AddWithValue("@Par_CreditoID", credito.Id)
                command.Parameters.AddWithValue("@Par_UsuarioBaja", usuario.UsuarioID)
                command.Parameters.AddWithValue("@Par_Comentario", comentario)
                command.Parameters.AddWithValue("@Par_SolicitudID", credito.Id_Sol)
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

    Public Shared Function CreditoALT(solicitudRequest As clsSolicitud, montoDeuda As Double) As mensajeTransaccion
        Dim mensaje As mensajeTransaccion = Nothing
        Try
            Dim conexion As String = clsConexion.ConnectionString
            Using con As New SqlConnection(conexion.ToString())
                con.Open()
                ' se crea el objeto transaction
                Dim myTrans As SqlTransaction = con.BeginTransaction()

                ' se crea el objeto sqlcomand
                Dim command As New SqlCommand("CREDITOSALT", con)
                command.CommandType = CommandType.StoredProcedure
                command.Parameters.AddWithValue("@Par_id_Sol", solicitudRequest.Id)
                command.Parameters.AddWithValue("@Par_Id_Cliente", solicitudRequest.Id_Cliente)
                command.Parameters.AddWithValue("@Par_Id_Sucursal", solicitudRequest.Id_Sucursal)
                command.Parameters.AddWithValue("@Par_FecSolicitud", solicitudRequest.FecSol)
                command.Parameters.AddWithValue("@Par_MontoSolicitud", solicitudRequest.MontoSol)
                command.Parameters.AddWithValue("@Par_MontoDeuda", montoDeuda)
                command.Parameters.AddWithValue("@Par_PlazoSol", solicitudRequest.PlazoSol)
                command.Parameters.AddWithValue("@Par_TasaRef", solicitudRequest.TasaReferencia)
                command.Parameters.AddWithValue("@Par_TipoAmort", solicitudRequest.TipoAmort)
                command.Parameters.AddWithValue("@Par_TasaMoratoria", solicitudRequest.TasaMoratoria)

                command.Parameters.AddWithValue("Par_Salida", "S")
                command.Parameters.AddWithValue("Par_NumErr", "")
                command.Parameters.AddWithValue("Par_ErrMenDev", "")
                command.Parameters.AddWithValue("Par_Consecutivo", 0)
                command.Parameters.AddWithValue("@Par_UsuarioAlta", solicitudRequest.Login)

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
                mensaje.mensajeRespuesta = "OCURRIO UN PROBLEMA AL PREACTIVAR EL CRÉDITO " + "\n Menaje tecnico: " + ex.Message
            End If

        End Try
        Return mensaje
    End Function


    Shared Function getDataSource(sucID As Integer) As DataTable
        Dim dt As DataTable
        Dim da As SqlDataAdapter

        Dim conexion As String = clsConexion.ConnectionString
        Using con As New SqlConnection(conexion.ToString())
            con.Open()

            Dim command As New SqlCommand("CREDITOCARTEMPLIS", con)
            command.CommandType = CommandType.StoredProcedure

            command.Parameters.AddWithValue("Par_NumLis", 1)
            command.Parameters.AddWithValue("@Par_SucID", sucID)

            da = New SqlDataAdapter(command)
            dt = New DataTable()
            da.Fill(dt)

        End Using

        Return dt

    End Function




    Public Shared Function ConsultarPrimPago(creditoID As Integer) As Double
        Dim valorTasaMora As Double = 0
        Try
            Dim StrSelect As String = "  Select Monto from PLANESDEPAGO where NumPago = 1 AND  Id_Credito = " & creditoID
            Dim conexion As String = clsConexion.ConnectionString
            Using Con As New SqlConnection(conexion)
                Con.Open()
                Using Com As New SqlCommand(StrSelect, Con)
                    Using rst = Com.ExecuteReader()
                        If rst.HasRows Then
                            While rst.Read
                                valorTasaMora = rst("Monto")

                            End While
                        Else
                            Return Nothing
                        End If
                    End Using
                End Using
            End Using
        Catch ex As Exception

        End Try
        Return valorTasaMora
    End Function

    Public Shared Function ConsultarFechaPrimPago(creditoID As Integer) As DateTime
        Dim FechaPrimPago As DateTime = Nothing
        Try
            Dim StrSelect As String = "  Select FechaPago from PLANESDEPAGO where NumPago = 1 AND  Id_Credito = " & creditoID
            Dim conexion As String = clsConexion.ConnectionString
            Using Con As New SqlConnection(conexion)
                Con.Open()
                Using Com As New SqlCommand(StrSelect, Con)
                    Using rst = Com.ExecuteReader()
                        If rst.HasRows Then
                            While rst.Read
                                FechaPrimPago = rst("FechaPago")

                            End While
                        Else
                            Return Nothing
                        End If
                    End Using
                End Using
            End Using
        Catch ex As Exception

        End Try
        Return FechaPrimPago
    End Function




End Class
