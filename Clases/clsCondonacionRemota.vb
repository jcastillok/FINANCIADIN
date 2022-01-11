Imports System.Data.SqlClient
Imports FinanciaDin

Public Class clsCondonacionRemota
    Public Property Id As Integer
    Public Property Id_Credito As Integer
    Public Property Monto As Double
    Public Property Aplicado As Boolean
    Public Property Login As String
    Public Property FecCaptura As DateTime
    Public Property FecAplicacion As DateTime?
    Public Property Cancelado As Boolean
    Public Property LoginCancelado As String
    Public Property FecCancelado As DateTime?


    Public Shared Function obtener(Id_Credito As Integer) As clsCondonacionRemota

        Dim clCondonacionRemota As New clsCondonacionRemota
        Dim StrSelect As String = "SELECT *" _
        & " FROM CONDONACIONESREMOTAS" _
        & " WHERE Aplicado = 0 AND Cancelado = 0 AND Id_Credito = " & Id_Credito
        Dim conexion As String = clsConexion.ConnectionString
        Using Con As New SqlConnection(conexion)
            Con.Open()
            Using Com As New SqlCommand(StrSelect, Con)
                Using rst = Com.ExecuteReader()
                    If rst.HasRows Then
                        While rst.Read
                            clCondonacionRemota.Id = rst("Id")
                            clCondonacionRemota.Id_Credito = rst("Id_Credito")
                            clCondonacionRemota.Monto = rst("Monto")
                            clCondonacionRemota.Aplicado = rst("Aplicado")
                            clCondonacionRemota.Login = rst("Login")
                            clCondonacionRemota.FecCaptura = rst("FecCaptura")
                            clCondonacionRemota.FecAplicacion = If(IsDBNull(rst("FecAplicacion")), Nothing, rst("FecAplicacion"))
                            clCondonacionRemota.Cancelado = rst("Cancelado")
                            clCondonacionRemota.LoginCancelado = If(IsDBNull(rst("LoginCancelado")), Nothing, rst("LoginCancelado"))
                            clCondonacionRemota.FecCancelado = If(IsDBNull(rst("FecCancelado")), Nothing, rst("FecCancelado"))
                        End While
                    Else
                        Return Nothing
                    End If
                End Using
            End Using
        End Using
        Return clCondonacionRemota
    End Function


    Public Shared Function insertar(condonacionRemota As clsCondonacionRemota) As Boolean
        Dim strIns As String = "INSERT INTO CONDONACIONESREMOTAS (Id_Credito, Monto, Login, FecCaptura, Cancelado) " _
            & "VALUES(" & condonacionRemota.Id_Credito & "," & condonacionRemota.Monto & ", " _
            & "'" & condonacionRemota.Login & "',GETDATE(),0)"

        Return Utilerias.setUpdInsDel(strIns)
    End Function

    Friend Shared Sub actualizar(condonacionRemota As clsCondonacionRemota)
        Dim strUpdt As String = "UPDATE CONDONACIONESREMOTAS SET Id_Credito = " _
            & "" & condonacionRemota.Id_Credito & ", Monto = " & condonacionRemota.Monto & " " _
            & "WHERE Id = " & condonacionRemota.Id

        Utilerias.setUpdInsDel(strUpdt)
    End Sub

    Public Shared Function marcarUsado(Id As Integer) As Boolean
        Dim strUpdt = "UPDATE CONDONACIONESREMOTAS SET Aplicado = 1, FecAplicacion = GETDATE() WHERE Id = " & Id

        Return Utilerias.setUpdInsDel(strUpdt)
    End Function

    Public Shared Function cancelar(Id As Integer, Login As String) As Boolean
        Dim strUpdt = "UPDATE CONDONACIONESREMOTAS SET Cancelado = 1, LoginCancelado = '" & Login & "', FecCancelado = GETDATE() WHERE Id = " & Id

        Return Utilerias.setUpdInsDel(strUpdt)
    End Function

End Class
