Imports System.Data.SqlClient

Public Class clsEmpresa
    Public Property Id As Integer
    Public Property Id_Cliente As Integer
    Public Property Nombre As String
    Public Property Giro As String
    Public Property Sector As String
    Public Property Telefono As String
    Public Property Calle As String
    Public Property NumExt As String
    Public Property NumInt As String
    Public Property Colonia As String
    Public Property CodigoPostal As String
    Public Property Municipio As String
    Public Property Ciudad As String
    Public Property Estado As String

    Public Shared Function obtener(ID As Integer) As clsEmpresa
        Dim StrSelect = "SELECT *" _
                & " FROM EMPRESAS" _
                & " WHERE Id_Cliente = " & ID

        Dim empresa As New clsEmpresa
        Dim conexion As String = clsConexion.ConnectionString
        Using Con As New SqlConnection(conexion)
            Con.Open()
            Using Com As New SqlCommand(StrSelect, Con)
                Using rst = Com.ExecuteReader()
                    If rst.HasRows Then
                        While rst.Read
                            empresa.Id = rst("Id")
                            empresa.Id_Cliente = rst("Id_Cliente")
                            empresa.Nombre = rst("Nombre")
                            empresa.Giro = rst("Giro")
                            empresa.Sector = rst("Sector")
                            empresa.Telefono = rst("Telefono")
                            empresa.Calle = rst("Calle")
                            empresa.NumExt = rst("NumExt")
                            empresa.NumInt = If(IsDBNull(rst("NumInt")), Nothing, rst("NumInt"))
                            empresa.Colonia = rst("Colonia")
                            empresa.CodigoPostal = rst("CodigoPostal")
                            empresa.Municipio = rst("Municipio")
                            empresa.Ciudad = rst("Ciudad")
                            empresa.Estado = rst("Estado")
                        End While
                    Else
                        Return Nothing
                    End If
                End Using
            End Using
        End Using
        Return empresa
    End Function

    Public Shared Function insertar(empresa As clsEmpresa) As Boolean
        Dim strIns As String = "INSERT INTO EMPRESAS (Id_Cliente, Nombre, Giro, Sector, Telefono, Calle, NumExt, NumInt, " _
            & "Colonia, CodigoPostal, Municipio, Ciudad, Estado) " _
            & "VALUES(" & empresa.Id_Cliente & ", '" & empresa.Nombre & "', '" & empresa.Giro & "', '" & empresa.Sector & "'," _
            & "'" & empresa.Telefono & "', '" & empresa.Calle & "', '" & empresa.NumExt & "'," & If(IsNothing(empresa.NumInt), "NULL", "'" & empresa.NumInt & "'") & ", " _
            & "'" & empresa.Colonia & "', '" & empresa.CodigoPostal & "', '" & empresa.Municipio & "', '" & empresa.Ciudad & "', '" & empresa.Estado & "')"

        Return Utilerias.setUpdInsDel(strIns)
    End Function

    Public Shared Function actualizar(empresa As clsEmpresa) As Boolean
        Dim strUpdt As String = "UPDATE EMPRESAS SET Nombre='" & empresa.Nombre & "',Giro='" & empresa.Giro & "',Sector='" & empresa.Sector & "'," _
            & "Telefono='" & empresa.Telefono & "',Calle='" & empresa.Calle & "',NumExt='" & empresa.NumExt & "'," _
            & "NumInt=" & If(IsNothing(empresa.NumInt), "NULL", "'" & empresa.NumInt & "'") & ",Colonia='" & empresa.Colonia & "',CodigoPostal='" & empresa.CodigoPostal & "'," _
            & "Municipio='" & empresa.Municipio & "', Ciudad='" & empresa.Ciudad & "',Estado='" & empresa.Estado & "' " _
            & "WHERE Id_Cliente = " & empresa.Id_Cliente
        Return Utilerias.setUpdInsDel(strUpdt)
    End Function

End Class
