Imports System.Data.SqlClient

Public Class clsSolicitud
    Public Property Id As Integer
    Public Property Id_Cliente As Integer
    Public Property Id_Sucursal As Integer
    Public Property Login_Asesor As String
    Public Property Id_Aval As Integer
    Public Property FecSol As DateTime
    Public Property FecAut As DateTime?
    Public Property FecDisp As DateTime?
    Public Property MontoSol As Double
    Public Property MontoAut As Double
    Public Property PlazoSol As Integer
    Public Property PlazoAut As Integer
    Public Property NumPagosSol As Integer
    Public Property NumPagosAut As Integer
    Public Property TipoProducto As String
    Public Property Destino As String
    Public Property LiquidaAnterior As Integer
    Public Property Id_A_Liquidar As Integer
    Public Property MontoALiquidar As Double
    Public Property TasaReferencia As Integer
    Public Property TipoAmort As Integer
    Public Property Sobretasa As Double
    Public Property TasaMoratoria As Double
    Public Property Impuesto As Double
    Public Property TipoGarantia As Integer
    Public Property ValorGarantia As String
    Public Property IngresoCliente As Integer
    Public Property MontoDesembolsado As Double
    Public Property Login As String
    Public Property Autorizado As Integer
    Public Property LoginAut As String
    Public Property DineroEntregado As Boolean

    Public Shared Function obtener(ID As Integer) As clsSolicitud
        Dim clSolicitud As New clsSolicitud
        Dim StrSelect As String = "SELECT Id, Id_Cliente, Id_Sucursal, Login_Asesor, Id_Aval, FecSolicitud, FecAutorizado," _
        & " FecDisposicion, MontoSolicitud, MontoAut, PlazoSol, PlazoAut, NumPagosSol, NumPagosAut," _
        & " LiquidaAnterior, Id_A_Liquidar, MontoLiquid, TasaRef, TipoAmort, Sobretasa, TasaMoratoria," _
        & " Impuesto, TipoGarantia, ValorGarantia, IngresoCliente, MontoDesembolsado," _
        & " Login, Autorizado, LoginAut, DineroEntregado, TipoProducto, Destino" _
        & " FROM [SOLICITUDES]" _
        & " WHERE Id = " & ID & ""
        Dim conexion As String = clsConexion.ConnectionString
        Using Con As New SqlConnection(conexion)
            Con.Open()
            Using Com As New SqlCommand(StrSelect, Con)
                Using rst = Com.ExecuteReader()
                    If rst.HasRows Then
                        While rst.Read
                            clSolicitud.Id = rst("Id")
                            clSolicitud.Id_Cliente = rst("Id_Cliente")
                            clSolicitud.Id_Sucursal = rst("Id_Sucursal")
                            clSolicitud.Login_Asesor = If(IsDBNull(rst("Login_Asesor")), "", rst("Login_Asesor"))
                            clSolicitud.Id_Aval = If(IsDBNull(rst("Id_Aval")), 0, rst("Id_Aval"))
                            clSolicitud.FecSol = rst("FecSolicitud")
                            clSolicitud.FecAut = rst("FecAutorizado")
                            clSolicitud.FecDisp = rst("FecDisposicion")
                            clSolicitud.MontoSol = rst("MontoSolicitud")
                            clSolicitud.MontoAut = rst("MontoAut")
                            clSolicitud.PlazoSol = rst("PlazoSol")
                            clSolicitud.PlazoAut = rst("PlazoAut")
                            clSolicitud.NumPagosSol = rst("NumPagosSol")
                            clSolicitud.NumPagosAut = rst("NumPagosAut")
                            clSolicitud.TipoProducto = rst("TipoProducto")
                            clSolicitud.Destino = rst("Destino")
                            clSolicitud.LiquidaAnterior = rst("LiquidaAnterior")
                            clSolicitud.Id_A_Liquidar = If(IsDBNull(rst("Id_A_Liquidar")), 0, rst("Id_A_Liquidar"))
                            clSolicitud.MontoALiquidar = If(IsDBNull(rst("MontoLiquid")), 0, rst("MontoLiquid"))
                            clSolicitud.TasaReferencia = rst("TasaRef")
                            clSolicitud.TipoAmort = rst("TipoAmort")
                            clSolicitud.Sobretasa = rst("Sobretasa")
                            clSolicitud.TasaMoratoria = rst("TasaMoratoria")
                            clSolicitud.Impuesto = rst("Impuesto")
                            clSolicitud.TipoGarantia = rst("TipoGarantia")
                            clSolicitud.Destino = rst("Destino")
                            clSolicitud.ValorGarantia = rst("ValorGarantia")
                            'clSolicitud.IngresoCliente = If(IsDBNull(rst("IngresoCliente")), Nothing, rst("IngresoCliente"))
                            clSolicitud.MontoDesembolsado = If(IsDBNull(rst("MontoDesembolsado")), 0, rst("MontoDesembolsado"))
                            clSolicitud.Login = rst("Login")
                            clSolicitud.Autorizado = rst("Autorizado")
                            clSolicitud.LoginAut = rst("LoginAut")
                            clSolicitud.DineroEntregado = rst("DineroEntregado")
                        End While
                    End If
                End Using
            End Using
        End Using
        Return clSolicitud
    End Function

    Public Shared Function insertar(solicitud As clsSolicitud, Optional credAReestruc As Integer = 0) As Boolean

        Dim strIns As String

        If credAReestruc = 0 Then
            strIns = "INSERT INTO SOLICITUDES (Id_Cliente,Id_Sucursal,Login_Asesor,Id_Aval,FecSolicitud,FecAutorizado,FecDisposicion,MontoSolicitud,MontoAut,PlazoSol,PlazoAut,NumPagosSol," _
                 & " NumPagosAut,LiquidaAnterior,Id_A_Liquidar,MontoLiquid,TasaRef,TipoAmort,Sobretasa,TasaMoratoria,Impuesto,TipoGarantia,ValorGarantia," _
                 & " IngresoCliente,MontoDesembolsado,FecCaptura,FecUltMod,Login,Autorizado,TipoProducto,Destino)" _
                 & " VALUES (" & solicitud.Id_Cliente & "," & solicitud.Id_Sucursal & ",'" & solicitud.Login_Asesor & "'," & If(IsNothing(solicitud.Id_Aval), "NULL", solicitud.Id_Aval) & ",convert(datetime,'" & solicitud.FecSol.ToShortDateString() & "',103),convert(datetime,'" _
                 & " " & If(solicitud.FecAut Is Nothing, "", solicitud.FecAut) & "',103),convert(datetime,'" & If(solicitud.FecDisp Is Nothing, "", solicitud.FecDisp) & "',103)," _
                 & " " & solicitud.MontoSol & "," & If(solicitud.MontoAut = 0, "NULL", solicitud.MontoAut) & "," & solicitud.PlazoSol & "," _
                 & " " & If(solicitud.PlazoAut = 0, "NULL", solicitud.PlazoAut) & "," & solicitud.NumPagosSol & "," & If(solicitud.NumPagosAut = 0, "NULL", solicitud.NumPagosAut) & "," _
                 & " " & solicitud.LiquidaAnterior & "," & If(solicitud.Id_A_Liquidar = 0, "NULL", solicitud.Id_A_Liquidar) & "," _
                 & " " & If(solicitud.MontoALiquidar = 0, "NULL", solicitud.MontoALiquidar) & "," & solicitud.TasaReferencia & "," & solicitud.TipoAmort & "," _
                 & " " & solicitud.Sobretasa & "," & solicitud.TasaMoratoria & "," & solicitud.Impuesto & "," & solicitud.TipoGarantia & "," _
                 & " " & solicitud.ValorGarantia & "," & If(solicitud.IngresoCliente = 0, "NULL", solicitud.IngresoCliente) & "," & solicitud.MontoDesembolsado & "," _
                 & " convert(datetime,'" & System.DateTime.Now.Date & "',103),convert(datetime,'" & System.DateTime.Now.Date & "',103),'" & solicitud.Login & "',NULL," _
                 & " '" & solicitud.TipoProducto & "','" & solicitud.Destino & "')"
        Else
            strIns = "INSERT INTO SOLICITUDES (Id_Cliente,Id_Sucursal,Login_Asesor,Id_Aval,FecSolicitud,FecAutorizado,FecDisposicion,MontoSolicitud,MontoAut,PlazoSol,PlazoAut,NumPagosSol," _
                 & " NumPagosAut,LiquidaAnterior,Id_A_Liquidar,MontoLiquid,TasaRef,TipoAmort,Sobretasa,TasaMoratoria,Impuesto,TipoGarantia,ValorGarantia," _
                 & " IngresoCliente,MontoDesembolsado,FecCaptura,FecUltMod,Login,Autorizado,TipoProducto,Destino)" _
                 & " SELECT Id_Cliente,Id_Sucursal,'" & solicitud.Login_Asesor & "'," & If(IsNothing(solicitud.Id_Aval), "Id_Aval", solicitud.Id_Aval) & ",convert(datetime,'" & solicitud.FecSol.ToShortDateString() & "',103),convert(datetime,'" _
                 & " " & If(solicitud.FecAut Is Nothing, "", solicitud.FecAut) & "',103),convert(datetime,'" & If(solicitud.FecDisp Is Nothing, "", solicitud.FecDisp) & "',103)," _
                 & " " & solicitud.MontoSol & "," & If(solicitud.MontoAut = 0, "NULL", solicitud.MontoAut) & "," & solicitud.PlazoSol & "," _
                 & " " & If(solicitud.PlazoAut = 0, "NULL", solicitud.PlazoAut) & "," & solicitud.NumPagosSol & "," & If(solicitud.NumPagosAut = 0, "NULL", solicitud.NumPagosAut) & "," _
                 & " " & solicitud.LiquidaAnterior & "," & solicitud.Id_A_Liquidar & "," & solicitud.MontoALiquidar & "," & solicitud.TasaReferencia & "," & solicitud.TipoAmort & "," _
                 & " " & solicitud.Sobretasa & "," & solicitud.TasaMoratoria & "," & solicitud.Impuesto & "," & solicitud.TipoGarantia & "," _
                 & " " & solicitud.ValorGarantia & "," & If(solicitud.IngresoCliente = 0, "NULL", solicitud.IngresoCliente) & "," & solicitud.MontoDesembolsado & "," _
                 & " convert(datetime,'" & System.DateTime.Now.Date & "',103),convert(datetime,'" & System.DateTime.Now.Date & "',103),'" & solicitud.Login & "',NULL," _
                 & " '" & solicitud.TipoProducto & "','" & solicitud.Destino & "'" _
                 & " FROM SOLICITUDES WHERE Id = (SELECT Id_Sol FROM CREDITOS WHERE Id = " & credAReestruc & ")"

        End If

        Return Utilerias.setUpdInsDel(strIns)

    End Function

    Public Shared Function actualizar(solicitud As clsSolicitud) As Boolean
        Dim strUpdt As String
        strUpdt = " UPDATE SOLICITUDES" _
            & " Set Id_Cliente = " & solicitud.Id_Cliente & ",Id_Sucursal = " & solicitud.Id_Sucursal & ", FecSolicitud = convert(datetime,'" & solicitud.FecSol.ToShortDateString() & "',103), FecAutorizado = convert(datetime,'" & If(solicitud.FecAut Is Nothing, "NULL", solicitud.FecAut) & "',103)," _
            & " FecDisposicion = convert(datetime,'" & If(solicitud.FecDisp Is Nothing, "NULL", solicitud.FecDisp) & "',103), MontoSolicitud = " & solicitud.MontoSol & ", MontoAut = " & If(solicitud.MontoAut = 0, "NULL", solicitud.MontoAut) & "," _
            & " PlazoSol = " & solicitud.PlazoSol & ", PlazoAut = " & If(solicitud.PlazoAut = 0, "NULL", solicitud.PlazoAut) & ", NumPagosSol = " & solicitud.NumPagosSol & "," _
            & " NumPagosAut = " & If(solicitud.NumPagosAut = 0, "NULL", solicitud.NumPagosAut) & ", TasaRef = " & solicitud.TasaReferencia & ", TipoAmort = " & solicitud.TipoAmort & "," _
            & " Sobretasa = " & solicitud.Sobretasa & ", TasaMoratoria = " & solicitud.TasaMoratoria & ", Impuesto = " & solicitud.Impuesto & "," _
            & " TipoGarantia = '" & solicitud.TipoGarantia & "', ValorGarantia = " & solicitud.ValorGarantia & ", Id_Aval = " & If(IsNothing(solicitud.Id_Aval), "NULL", solicitud.Id_Aval) & "," _
            & " IngresoCliente = " & If(solicitud.IngresoCliente = 0, "NULL", solicitud.IngresoCliente) & ", Login_Asesor = '" & solicitud.Login_Asesor & "'," _
            & " LiquidaAnterior = " & solicitud.LiquidaAnterior & ", Id_A_Liquidar = " & If(solicitud.Id_A_Liquidar = 0, "NULL", solicitud.Id_A_Liquidar) & ", MontoLiquid = " & If(solicitud.MontoALiquidar = 0, "NULL", solicitud.MontoALiquidar) & "," _
            & " MontoDesembolsado = " & If(solicitud.MontoDesembolsado = 0, "NULL", solicitud.MontoDesembolsado) & ", Login = '" & solicitud.Login & "', FecUltMod = convert(datetime,'" & System.DateTime.Now.Date & "',103), " _
            & " TipoProducto = '" & solicitud.TipoProducto & "', Destino = '" & solicitud.Destino & "' " _
            & " WHERE (ID = " & solicitud.Id & ") "
        Return Utilerias.setUpdInsDel(strUpdt)
    End Function

    Public Shared Function esGarantiaLiquida(Id As Integer) As Boolean
        Dim clSolicitud As New clsSolicitud
        Dim StrSelect As String = "SELECT TipoGarantia" _
        & " FROM [SOLICITUDES]" _
        & " WHERE Id = " & Id & ""
        Dim conexion As String = clsConexion.ConnectionString
        Using Con As New SqlConnection(conexion)
            Con.Open()
            Using Com As New SqlCommand(StrSelect, Con)
                Using rst = Com.ExecuteReader()
                    If rst.HasRows Then
                        While rst.Read
                            If rst("TipoGarantia") = 3 Or rst("TipoGarantia") = 5 Then
                                Return True
                            End If
                        End While
                    End If
                End Using
            End Using
        End Using
        Return False
    End Function

    Public Shared Function tieneSolActiva(Id_Cliente As Integer) As Boolean
        Dim clSolicitud As New clsSolicitud
        Dim StrSelect As String = "SELECT Id_Cliente" _
        & " FROM [SOLICITUDES]" _
        & " WHERE Id_Cliente = " & Id_Cliente & " AND Autorizado IS NULL"
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


    Public Shared Function SolicitudAlta(solicitudRequest As clsSolicitud) As mensajeTransaccion
        Dim menssajeTran As mensajeTransaccion = Nothing

        Try
            Dim conexion As String = clsConexion.ConnectionString
            Using con As New SqlConnection(conexion.ToString())
                con.Open()
                ' se crea el objeto transaction
                Dim myTrans As SqlTransaction = con.BeginTransaction()

                ' se crea el objeto sqlcomand
                Dim command As New SqlCommand("SOLICITUDESALT", con)
                command.CommandType = CommandType.StoredProcedure
                command.Parameters.AddWithValue("@Par_Id_Cliente", solicitudRequest.Id_Cliente)
                command.Parameters.AddWithValue("@Par_Id_Sucursal", solicitudRequest.Id_Sucursal)
                command.Parameters.AddWithValue("@Par_FecSolicitud", solicitudRequest.FecSol)
                command.Parameters.AddWithValue("@Par_MontoSolicitud", solicitudRequest.MontoSol)
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

                        menssajeTran = New mensajeTransaccion
                        menssajeTran.codigoRespuesta = dr("NumErr").ToString()
                        menssajeTran.mensajeRespuesta = dr("ErrMenDev").ToString()
                        menssajeTran.consecutivo = dr("Consecutivo").ToString()

                    Loop
                    dr.Close()
                    myTrans.Commit()

                Catch ex As Exception
                    myTrans.Rollback()
                End Try
            End Using

        Catch ex As Exception
            If IsNothing(menssajeTran) Then
                menssajeTran = New mensajeTransaccion
                menssajeTran.codigoRespuesta = "000001"
                menssajeTran.mensajeRespuesta = "OCURRIO UN PROBLEMA AL REGISTRAR LA SOLICITUD DEL CRÉDITO " + "\n Menaje tecnico: " + ex.Message
            End If

        End Try

        Return menssajeTran
    End Function


End Class
