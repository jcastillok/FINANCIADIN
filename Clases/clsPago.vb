Imports System.Data.SqlClient

Public Class clsPago
    Public Property Id As Integer
    Public Property Id_Mov As Integer
    Public Property Id_Credito As Integer
    Public Property Id_Cliente As Integer
    Public Property Id_Sucursal As Integer
    Public Property NumPago As Integer
    Public Property FecEsperada As DateTime
    Public Property FecPago As DateTime
    Public Property FecProxPago As DateTime?
    Public Property Monto As Double
    Public Property AbonoCapital As Double
    Public Property AbonoInteres As Double
    Public Property AbonoIVA As Double
    Public Property Saldo As Double
    Public Property SaldoCapital As Double
    Public Property Login As String
    Public Property Atrasado As Boolean
    Public Property Recargo As Double
    Public Property RecargoCobrado As Double
    Public Property EsGarantia As Boolean
    Public Property GarantiaAplicada As Boolean?
    Public Property FecAplicGarantia As DateTime?
    Public Property EsAbonoCapital As Boolean
    Public Property Condonado As Boolean?
    Public Property MotivoCondonacion As String
    Public Property LoginCond As String
    Public Property UltModif As DateTime
    Public Property LoginModif As String
    Public Property Cancelado As Boolean = False
    Public Property FecCancelado As DateTime
    Public Property LoginCancelado As String

    Public Shared Function obtener(ID As Integer, Optional numPago As Integer = 0, Optional obtenerUltimo As Boolean = False, Optional incluirPagoCero As Boolean = False, Optional obtenerCancelado As Boolean = False) As clsPago
        Dim StrSelect As String
        If obtenerUltimo Then
            If incluirPagoCero Then
                StrSelect = "SELECT TOP 1 *" _
                & " FROM PAGOS" _
                & " WHERE Id_Credito = " & ID & " AND Cancelado = 0 ORDER BY Saldo ASC"
            Else
                StrSelect = "SELECT TOP 1 *" _
                & " FROM PAGOS" _
                & " WHERE Id_Credito = " & ID & " AND Cancelado = 0 AND NumPago <> 0 ORDER BY Saldo ASC"
            End If
        ElseIf numPago <> 0 Then
            If obtenerCancelado Then
                StrSelect = "SELECT *" _
                & " FROM PAGOS" _
                & " WHERE Id_Credito = " & ID & " AND Cancelado = 1 AND NumPago = " & numPago
            Else
                StrSelect = "SELECT *" _
                & " FROM PAGOS" _
                & " WHERE Id_Credito = " & ID & " AND Cancelado = 0 AND NumPago = " & numPago
            End If
        ElseIf numPago = 0 Then
            If obtenerCancelado Then
                StrSelect = "SELECT TOP 1 *" _
                & " FROM PAGOS" _
                & " WHERE Id_Credito = " & ID & " AND Cancelado = 1 AND NumPago = 0"
                '    Else
                '        StrSelect = "SELECT *" _
                '        & " FROM PAGOS" _
                '        & " WHERE Id = " & ID & " AND Cancelado <> 1 ORDER BY Id, NumPago ASC"
            Else
                StrSelect = "SELECT *" _
                & " FROM PAGOS" _
                & " WHERE Id = " & ID
            End If
        End If

        Dim pago As New clsPago
        Dim conexion As String = clsConexion.ConnectionString
        Using Con As New SqlConnection(conexion)
            Con.Open()
            Using Com As New SqlCommand(StrSelect, Con)
                Using rst = Com.ExecuteReader()
                    If rst.HasRows Then
                        While rst.Read
                            pago.Id = rst("Id")
                            pago.Id_Mov = rst("Id_Mov")
                            pago.Id_Credito = rst("Id_Credito")
                            pago.Id_Cliente = rst("Id_Cliente")
                            pago.Id_Sucursal = rst("Id_Sucursal")
                            pago.NumPago = rst("NumPago")
                            pago.FecEsperada = rst("FecEsperada")
                            pago.FecPago = rst("FecPago")
                            pago.FecProxPago = If(IsDBNull(rst("FecProxPago")), Nothing, rst("FecProxPago"))
                            pago.Monto = rst("Monto")
                            pago.AbonoCapital = rst("AbonoCapital")
                            pago.AbonoInteres = rst("AbonoInteres")
                            pago.AbonoIVA = rst("AbonoIVA")
                            pago.Saldo = rst("Saldo")
                            pago.SaldoCapital = rst("SaldoCapital")
                            pago.Login = rst("Login")
                            pago.Atrasado = rst("Atrasado")
                            pago.Recargo = rst("Recargo")
                            pago.RecargoCobrado = rst("RecargoCobrado")
                            pago.EsGarantia = rst("EsGarantia")
                            pago.GarantiaAplicada = If(IsDBNull(rst("GarantiaAplicada")), Nothing, rst("GarantiaAplicada"))
                            pago.FecAplicGarantia = If(IsDBNull(rst("FecAplicGarantia")), Nothing, rst("FecAplicGarantia"))
                            pago.EsAbonoCapital = rst("EsAbonoCapital")
                            pago.Condonado = If(IsDBNull(rst("Condonado")), Nothing, rst("Condonado"))
                            pago.MotivoCondonacion = If(IsDBNull(rst("MotivoCondonacion")), Nothing, rst("MotivoCondonacion"))
                            pago.LoginCond = If(IsDBNull(rst("LoginCond")), Nothing, rst("LoginCond"))
                            pago.UltModif = rst("UltModif")
                            pago.LoginModif = rst("LoginModif")
                        End While
                    Else
                        Return Nothing
                    End If
                End Using
            End Using
        End Using
        Return pago
    End Function

    Public Shared Function obtenerBase(ID As Integer) As clsPago
        Dim StrSelect As String

        StrSelect = "SELECT *" _
                & " FROM PAGOS" _
                & " WHERE Id_Credito = " & ID & " AND EsGarantia = 1"

        Dim pago As New clsPago
        Dim conexion As String = clsConexion.ConnectionString
        Using Con As New SqlConnection(conexion)
            Con.Open()
            Using Com As New SqlCommand(StrSelect, Con)
                Using rst = Com.ExecuteReader()
                    If rst.HasRows Then
                        While rst.Read
                            pago.Id = rst("Id")
                            pago.Id_Mov = rst("Id_Mov")
                            pago.Id_Credito = rst("Id_Credito")
                            pago.Id_Cliente = rst("Id_Cliente")
                            pago.Id_Sucursal = rst("Id_Sucursal")
                            pago.NumPago = rst("NumPago")
                            pago.FecEsperada = rst("FecEsperada")
                            pago.FecPago = rst("FecPago")
                            pago.FecProxPago = If(IsDBNull(rst("FecProxPago")), Nothing, rst("FecProxPago"))
                            pago.Monto = rst("Monto")
                            pago.AbonoCapital = rst("AbonoCapital")
                            pago.AbonoInteres = rst("AbonoInteres")
                            pago.AbonoIVA = rst("AbonoIVA")
                            pago.Saldo = rst("Saldo")
                            pago.SaldoCapital = rst("SaldoCapital")
                            pago.Login = rst("Login")
                            pago.Atrasado = rst("Atrasado")
                            pago.Recargo = rst("Recargo")
                            pago.RecargoCobrado = rst("RecargoCobrado")
                            pago.EsGarantia = rst("EsGarantia")
                            pago.GarantiaAplicada = If(IsDBNull(rst("GarantiaAplicada")), Nothing, rst("GarantiaAplicada"))
                            pago.FecAplicGarantia = If(IsDBNull(rst("FecAplicGarantia")), Nothing, rst("FecAplicGarantia"))
                            pago.EsAbonoCapital = rst("EsAbonoCapital")
                            pago.Condonado = If(IsDBNull(rst("Condonado")), Nothing, rst("Condonado"))
                            pago.MotivoCondonacion = If(IsDBNull(rst("MotivoCondonacion")), Nothing, rst("MotivoCondonacion"))
                            pago.LoginCond = If(IsDBNull(rst("LoginCond")), Nothing, rst("LoginCond"))
                            pago.UltModif = rst("UltModif")
                            pago.LoginModif = rst("LoginModif")
                        End While
                    Else
                        Return Nothing
                    End If
                End Using
            End Using
        End Using
        Return pago
    End Function
    Public Shared Function obtenerHistorial(ID_Credito As Integer) As List(Of clsPago)

        Dim historial As New List(Of clsPago)
        'Dim StrSelect As String = "SELECT * FROM PAGOS " _
        '& " WHERE Id_Credito = " & ID_Credito & " AND Cancelado = 0 AND EsGarantia = 1 AND (GarantiaAplicada IS NULL OR GarantiaAplicada = 0) " _
        '& " UNION Select * FROM PAGOS" _
        '& " WHERE Id_Credito = " & ID_Credito & " And Cancelado = 0 And EsGarantia = 0" _
        '& " UNION Select * FROM PAGOS " _
        '& " WHERE Id_Credito = " & ID_Credito & " And Cancelado = 0 And EsGarantia = 1 And GarantiaAplicada = 1" _
        '& " ORDER BY Id, FecPago, NumPago"
        Dim StrSelect As String = "SELECT * FROM PAGOS " _
        & " WHERE Id_Credito = " & ID_Credito & " AND Cancelado = 0" _
        & " ORDER BY Saldo DESC, SaldoCapital DESC"
        Dim conexion As String = clsConexion.ConnectionString
        Using Con As New SqlConnection(conexion)
            Con.Open()
            Using Com As New SqlCommand(StrSelect, Con)
                Using rst = Com.ExecuteReader()
                    If rst.HasRows Then
                        While rst.Read
                            Dim pago As New clsPago
                            pago.Id = rst("Id")
                            pago.Id_Mov = rst("Id_Mov")
                            pago.Id_Credito = rst("Id_Credito")
                            pago.Id_Cliente = rst("Id_Cliente")
                            pago.Id_Sucursal = rst("Id_Sucursal")
                            pago.NumPago = rst("NumPago")
                            pago.FecEsperada = rst("FecEsperada")
                            pago.FecPago = rst("FecPago")
                            pago.FecProxPago = If(IsDBNull(rst("FecProxPago")), Nothing, rst("FecProxPago"))
                            pago.Monto = rst("Monto")
                            pago.AbonoCapital = rst("AbonoCapital")
                            pago.AbonoInteres = rst("AbonoInteres")
                            pago.AbonoIVA = rst("AbonoIVA")
                            pago.Saldo = rst("Saldo")
                            pago.SaldoCapital = rst("SaldoCapital")
                            pago.Login = rst("Login")
                            pago.Atrasado = rst("Atrasado")
                            pago.Recargo = rst("Recargo")
                            pago.RecargoCobrado = rst("RecargoCobrado")
                            pago.EsGarantia = rst("EsGarantia")
                            pago.GarantiaAplicada = If(IsDBNull(rst("GarantiaAplicada")), Nothing, rst("GarantiaAplicada"))
                            pago.FecAplicGarantia = If(IsDBNull(rst("FecAplicGarantia")), Nothing, rst("FecAplicGarantia"))
                            pago.EsAbonoCapital = rst("EsAbonoCapital")
                            pago.Condonado = If(IsDBNull(rst("Condonado")), Nothing, rst("Condonado"))
                            pago.MotivoCondonacion = If(IsDBNull(rst("MotivoCondonacion")), Nothing, rst("MotivoCondonacion"))
                            pago.LoginCond = If(IsDBNull(rst("LoginCond")), Nothing, rst("LoginCond"))
                            pago.UltModif = rst("UltModif")
                            pago.LoginModif = rst("LoginModif")
                            historial.Add(pago)
                        End While
                    End If
                End Using
            End Using
        End Using
        Return historial
    End Function

    Public Shared Function insertar(pago As clsPago) As Boolean
        Dim strIns As String = "INSERT INTO PAGOS (Id_Credito, Id_Cliente, Id_Sucursal, NumPago, FecEsperada, " _
            & "FecPago, FecProxPago, Monto, AbonoCapital, AbonoInteres, AbonoIVA, Saldo, SaldoCapital, Login, Atrasado, Recargo, " _
            & "RecargoCobrado, EsGarantia, EsAbonoCapital, Condonado, MotivoCondonacion, LoginCond, UltModif, LoginModif, Id_Mov) " _
            & "VALUES(" & pago.Id_Credito & "," & pago.Id_Cliente & "," & pago.Id_Sucursal & "," & pago.NumPago & "," _
            & "convert(datetime,'" & pago.FecEsperada & "',103),convert(datetime,'" & pago.FecPago & "',103)," & If(IsNothing(pago.FecProxPago), "NULL", "convert(datetime,'" & pago.FecProxPago & "',103)") & "," _
            & "" & pago.Monto & "," & pago.AbonoCapital & "," & pago.AbonoInteres & "," & pago.AbonoIVA & "," & pago.Saldo & "," & pago.SaldoCapital & ",'" & pago.Login & "','" & pago.Atrasado & "'," _
            & "" & pago.Recargo & "," & pago.RecargoCobrado & ",'" & pago.EsGarantia & "','" & pago.EsAbonoCapital & "'," & If(IsNothing(pago.Condonado), "NULL", "'" & pago.Condonado & "'") & "," _
            & "" & If(IsNothing(pago.MotivoCondonacion), "NULL", "'" & pago.MotivoCondonacion & "'") & "," & If(IsNothing(pago.LoginCond), "NULL", "'" & pago.LoginCond & "'") & "," _
            & "GETDATE(),'" & pago.LoginModif & "', " & pago.Id_Mov & ")"

        Return Utilerias.setUpdInsDel(strIns)
    End Function

    Public Shared Function actualizar(pago As clsPago) As Boolean

        'Try
        '    Dim strUpdt As String = "UPDATE PAGOS SET FecEsperada=@FecEsperada, FecPago=@FecPago, FecProxPago=@FecProxPago, " _
        '        & "Monto=@Monto, Saldo=@Saldo, SaldoCapital=@SaldoCapital, Login=@Login, Atrasado=@Atrasado, Recargo=@Recargo, " _
        '        & "EsGarantia=@EsGarantia, EsAbonoCapital=@EsAbonoCapital, Condonado=@Condonado, " _
        '        & "MotivoCondonacion=@MotivoCondonacion, UltModif=@UltModif, LoginModif=@LoginModif WHERE Id = @Id"
        '    Dim conexion As String = clsConexion.ConnectionString
        '    Using Con As New SqlConnection(conexion)
        '        Using Com As New SqlCommand(strUpdt, Con)
        '            Com.Parameters.AddWithValue("@FecEsperada", pago.FecEsperada)
        '            Com.Parameters.AddWithValue("@FecPago", pago.FecPago)
        '            Com.Parameters.AddWithValue("@FecProxPago", pago.FecProxPago)
        '            Com.Parameters.AddWithValue("@Monto", pago.Monto)
        '            Com.Parameters.AddWithValue("@Saldo", pago.Saldo)
        '            Com.Parameters.AddWithValue("@SaldoCapital", pago.SaldoCapital)
        '            Com.Parameters.AddWithValue("@Login", pago.Login)
        '            Com.Parameters.AddWithValue("@Atrasado", pago.Atrasado)
        '            Com.Parameters.AddWithValue("@Recargo", pago.Recargo)
        '            Com.Parameters.AddWithValue("@EsGarantia", pago.EsGarantia)
        '            Com.Parameters.AddWithValue("@EsAbonoCapital", pago.EsAbonoCapital)
        '            Com.Parameters.AddWithValue("@Condonado", If(IsNothing(pago.Condonado), DBNull.Value, pago.Condonado))
        '            Com.Parameters.AddWithValue("@MotivoCondonacion", If(IsNothing(pago.MotivoCondonacion), DBNull.Value, pago.MotivoCondonacion))
        '            Com.Parameters.AddWithValue("@UltModif", pago.UltModif)
        '            Com.Parameters.AddWithValue("@LoginModif", pago.LoginModif)

        '            Com.Parameters.AddWithValue("@Id", pago.Id)
        '            Con.Open()
        '            Com.ExecuteNonQuery()
        '        End Using
        '    End Using
        '    Return True
        'Catch ex As Exception
        '    Return False
        'End Try
        Dim strUpdt As String = "UPDATE PAGOS SET Monto=" & pago.Monto & ", AbonoCapital=" & pago.AbonoCapital & ", AbonoInteres=" & pago.AbonoInteres & ", " _
           & "AbonoIVA=" & pago.AbonoIVA & ", Saldo=" & pago.Saldo & ", SaldoCapital=" & pago.SaldoCapital & ", Atrasado='" & pago.Atrasado & "', Recargo=" & pago.Recargo & ", " _
           & "RecargoCobrado=" & pago.RecargoCobrado & ",EsAbonoCapital='" & pago.EsAbonoCapital & "', Condonado=" & If(IsNothing(pago.Condonado), "NULL", "'" & pago.Condonado & "'") & ", " _
           & "MotivoCondonacion=" & If(IsNothing(pago.MotivoCondonacion), "NULL", "'" & pago.MotivoCondonacion & "'") & ", Id_Sucursal = " & pago.Id_Sucursal & ", " _
           & "UltModif=GETDATE(), LoginModif='" & pago.LoginModif & "', FecPago  =  convert(datetime,'" & pago.FecPago & "',103), LoginCond=" & If(IsNothing(pago.LoginCond), "NULL", "'" & pago.LoginCond & "'") & " " _
           & "WHERE Id = " & pago.Id
        'Dim strCancel As String = "UPDATE PAGOS SET Cancelado = 1, UltModif = convert(datetime,'" & pago.UltModif & "',103), LoginModif = '" & pago.LoginModif & "'" _
        '& "WHERE Id_Credito = " & pago.Id_Credito & " And (FecPago > convert(datetime,'" & pago.FecPago & "',103) OR NumPago > " & pago.NumPago & ")"
        Return Utilerias.setUpdInsDel(strUpdt) 'And Utilerias.setUpdInsDel(strCancel)
    End Function

    Public Shared Function pagoBase(IdCredito As Integer) As Boolean
        Dim StrSelect = "SELECT *" _
                & " FROM PAGOS" _
                & " WHERE Id_Credito = " & IdCredito & " AND EsGarantia = 1"

        Dim pago As New clsPago
        Dim conexion As String = clsConexion.ConnectionString
        Using Con As New SqlConnection(conexion)
            Con.Open()
            Using Com As New SqlCommand(StrSelect, Con)
                Using rst = Com.ExecuteReader()
                    If rst.HasRows Then
                        Return True
                    Else
                        Return False
                    End If
                End Using
            End Using
        End Using
    End Function
    Public Shared Function liquidarConBase(pago As clsPago) As Boolean
        Dim strUpdt = "UPDATE PAGOS SET AbonoCapital = " & pago.AbonoCapital & ", Saldo = " & pago.Saldo & ", " _
            & "SaldoCapital = " & pago.SaldoCapital & ", GarantiaAplicada = '" & pago.GarantiaAplicada & "', " _
            & "FecAplicGarantia = convert(datetime,'" & pago.FecAplicGarantia & "',103), LoginModif = '" & pago.Login & "', UltModif = GETDATE() " _
            & "WHERE Id = " & pago.Id
        Return Utilerias.setUpdInsDel(strUpdt)
    End Function

    Public Shared Function ultimaFechaRecargo(Id_Credito As Integer) As DateTime
        Dim strSlct = "SELECT TOP 1 FecPago FROM PAGOS " _
                    & "WHERE Id_Credito = " & Id_Credito & " AND Atrasado = 1 AND Cancelado = 0 ORDER BY FecPago DESC"
        Dim fecha = Utilerias.getDataTable(strSlct)
        Return If(fecha.Rows.Count > 0, fecha.Rows.Item(0).Item(0), Nothing)
    End Function

End Class
