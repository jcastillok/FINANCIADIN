Imports System.Data.SqlClient

Public Class clsInfoEconomica
    Public Property Id As Integer
    Public Property Id_Cliente As Double
    Public Property IngEmpleo As Double
    Public Property IngNegocio As Double
    Public Property IngConyuge As Double
    Public Property IngApoyos As Double
    Public Property IngOtros As Double
    Public Property EgrRenta As Double
    Public Property EgrServicios As Double
    Public Property EgrGastosFam As Double
    Public Property EgrCreditos As Double
    Public Property EgrOtros As Double
    Public Property DepEconParent1 As String
    Public Property DepEconEdad1 As String
    Public Property DepEconOcup1 As String
    Public Property DepEconParent2 As String
    Public Property DepEconEdad2 As String
    Public Property DepEconOcup2 As String
    Public Property DepEconParent3 As String
    Public Property DepEconEdad3 As String
    Public Property DepEconOcup3 As String



    Public Shared Function obtener(ID As Integer) As clsInfoEconomica
        Dim StrSelect = "SELECT *" _
                & " FROM INFORMACIONECONOMICA" _
                & " WHERE Id_Cliente = " & ID

        Dim infoEconomica As New clsInfoEconomica
        Dim conexion As String = clsConexion.ConnectionString
        Using Con As New SqlConnection(conexion)
            Con.Open()
            Using Com As New SqlCommand(StrSelect, Con)
                Using rst = Com.ExecuteReader()
                    If rst.HasRows Then
                        While rst.Read
                            infoEconomica.Id = rst("Id")
                            infoEconomica.Id_Cliente = rst("Id_Cliente")
                            infoEconomica.IngEmpleo = rst("IngEmpleo")
                            infoEconomica.IngNegocio = rst("IngNegocio")
                            infoEconomica.IngConyuge = rst("IngConyuge")
                            infoEconomica.IngApoyos = rst("IngApoyos")
                            infoEconomica.IngOtros = rst("IngOtros")
                            infoEconomica.EgrRenta = rst("EgrRenta")
                            infoEconomica.EgrServicios = rst("EgrServicios")
                            infoEconomica.EgrGastosFam = rst("EgrGastosFam")
                            infoEconomica.EgrCreditos = rst("EgrCreditos")
                            infoEconomica.EgrOtros = rst("EgrOtros")
                            infoEconomica.DepEconParent1 = rst("DepEconParent1")
                            infoEconomica.DepEconEdad1 = rst("DepEconEdad1")
                            infoEconomica.DepEconOcup1 = rst("DepEconOcup1")
                            infoEconomica.DepEconParent2 = rst("DepEconParent2")
                            infoEconomica.DepEconEdad2 = rst("DepEconEdad2")
                            infoEconomica.DepEconOcup2 = rst("DepEconOcup2")
                            infoEconomica.DepEconParent3 = rst("DepEconParent3")
                            infoEconomica.DepEconEdad3 = rst("DepEconEdad3")
                            infoEconomica.DepEconOcup3 = rst("DepEconOcup3")
                        End While
                    Else
                        Return Nothing
                    End If
                End Using
            End Using
        End Using
        Return infoEconomica
    End Function

    Public Shared Function insertar(infoEconomica As clsInfoEconomica) As Boolean
        Dim strIns As String = "INSERT INTO INFORMACIONECONOMICA (Id_Cliente, IngEmpleo, IngNegocio, IngConyuge, IngApoyos, " _
            & "IngOtros, EgrRenta, EgrServicios, EgrGastosFam, EgrCreditos, EgrOtros, DepEconParent1, DepEconEdad1, DepEconOcup1, " _
            & "DepEconParent2, DepEconEdad2, DepEconOcup2, DepEconParent3, DepEconEdad3, DepEconOcup3) " _
            & "VALUES(" & infoEconomica.Id_Cliente & ", " & infoEconomica.IngEmpleo & ", " & infoEconomica.IngNegocio & "," _
            & "" & infoEconomica.IngConyuge & ", " & infoEconomica.IngApoyos & ", " & infoEconomica.IngOtros & "," & infoEconomica.EgrRenta & ", " _
            & "" & infoEconomica.EgrServicios & ", " & infoEconomica.EgrGastosFam & ", " & infoEconomica.EgrCreditos & ", " & infoEconomica.EgrOtros & ", " _
            & "'" & infoEconomica.DepEconParent1 & "', '" & infoEconomica.DepEconEdad1 & "','" & infoEconomica.DepEconOcup1 & "', " _
            & "'" & infoEconomica.DepEconParent2 & "', '" & infoEconomica.DepEconEdad2 & "','" & infoEconomica.DepEconOcup2 & "', " _
            & "'" & infoEconomica.DepEconParent3 & "', '" & infoEconomica.DepEconEdad3 & "','" & infoEconomica.DepEconOcup3 & "')"

        Return Utilerias.setUpdInsDel(strIns)
    End Function

    Public Shared Function actualizar(infoEconomica As clsInfoEconomica) As Boolean
        Dim strUpdt As String = "UPDATE INFORMACIONECONOMICA SET IngEmpleo=" & infoEconomica.IngEmpleo & ",IngNegocio=" & infoEconomica.IngNegocio & "," _
            & "IngConyuge=" & infoEconomica.IngConyuge & ",IngApoyos=" & infoEconomica.IngApoyos & ",IngOtros=" & infoEconomica.IngOtros & ",EgrRenta=" & infoEconomica.EgrRenta & "," _
            & "EgrServicios=" & infoEconomica.EgrServicios & ",EgrGastosFam=" & infoEconomica.EgrGastosFam & ",EgrCreditos=" & infoEconomica.EgrCreditos & ",EgrOtros=" & infoEconomica.EgrOtros & "," _
            & "DepEconParent1='" & infoEconomica.DepEconParent1 & "', DepEconEdad1='" & infoEconomica.DepEconEdad1 & "',DepEconOcup1='" & infoEconomica.DepEconOcup1 & "'," _
            & "DepEconParent2='" & infoEconomica.DepEconParent2 & "', DepEconEdad2='" & infoEconomica.DepEconEdad2 & "',DepEconOcup2='" & infoEconomica.DepEconOcup2 & "'," _
            & "DepEconParent3='" & infoEconomica.DepEconParent3 & "', DepEconEdad3='" & infoEconomica.DepEconEdad3 & "',DepEconOcup3='" & infoEconomica.DepEconOcup3 & "' " _
            & "WHERE Id_Cliente=" & infoEconomica.Id_Cliente
        Return Utilerias.setUpdInsDel(strUpdt)
    End Function

End Class
