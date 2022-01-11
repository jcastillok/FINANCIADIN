Imports System.Data.SqlClient

Public Class clsNegocio
    Public Property Id As Integer
    Public Property Id_Cliente As Integer
    Public Property Nombre As String
    Public Property Antiguedad As String
    Public Property Telefono As String
    Public Property Giro As String
    Public Property Calle As String
    Public Property NumExt As String
    Public Property NumInt As String
    Public Property Colonia As String
    Public Property CodigoPostal As String
    Public Property Municipio As String
    Public Property Ciudad As String
    Public Property Estado As String

    Public Shared Function obtener(ID As Integer) As clsNegocio
        Dim StrSelect = "SELECT *" _
                & " FROM NEGOCIOS" _
                & " WHERE Id_Cliente = " & ID

        Dim negocio As New clsNegocio
        Dim conexion As String = clsConexion.ConnectionString
        Using Con As New SqlConnection(conexion)
            Con.Open()
            Using Com As New SqlCommand(StrSelect, Con)
                Using rst = Com.ExecuteReader()
                    If rst.HasRows Then
                        While rst.Read
                            negocio.Id = rst("Id")
                            negocio.Id_Cliente = rst("Id_Cliente")
                            negocio.Nombre = rst("Nombre")
                            negocio.Antiguedad = rst("Antiguedad")
                            negocio.Telefono = rst("Telefono")
                            negocio.Giro = rst("Giro")
                            negocio.Calle = rst("Calle")
                            negocio.NumExt = rst("NumExt")
                            negocio.NumInt = If(IsDBNull(rst("NumInt")), Nothing, rst("NumInt"))
                            negocio.Colonia = rst("Colonia")
                            negocio.CodigoPostal = rst("CodigoPostal")
                            negocio.Municipio = rst("Municipio")
                            negocio.Ciudad = rst("Ciudad")
                            negocio.Estado = rst("Estado")
                        End While
                    Else
                        Return Nothing
                    End If
                End Using
            End Using
        End Using
        Return negocio
    End Function

    Public Shared Function insertar(negocio As clsNegocio) As Boolean
        Dim strIns As String = "INSERT INTO NEGOCIOS (Id_Cliente, Nombre, Giro, Antiguedad, Telefono, Calle, NumExt, NumInt, " _
            & "Colonia, CodigoPostal, Municipio, Ciudad, Estado) " _
            & "VALUES(" & negocio.Id_Cliente & ", '" & negocio.Nombre & "', '" & negocio.Giro & "', '" & negocio.Antiguedad & "'," _
            & "'" & negocio.Telefono & "', '" & negocio.Calle & "', '" & negocio.NumExt & "'," & If(IsNothing(negocio.NumInt), "NULL", "'" & negocio.NumInt & "'") & ", " _
            & "'" & negocio.Colonia & "', '" & negocio.CodigoPostal & "', '" & negocio.Municipio & "', '" & negocio.Ciudad & "', '" & negocio.Estado & "')"

        Return Utilerias.setUpdInsDel(strIns)
    End Function

    Public Shared Function actualizar(negocio As clsNegocio) As Boolean
        Dim strUpdt As String = "UPDATE NEGOCIOS SET Nombre='" & negocio.Nombre & "',Giro='" & negocio.Giro & "',Antiguedad=" & negocio.Antiguedad & "," _
            & "Telefono='" & negocio.Telefono & "',Calle='" & negocio.Calle & "',NumExt='" & negocio.NumExt & "'," _
            & "NumInt=" & If(IsNothing(negocio.NumInt), "NULL", "'" & negocio.NumInt & "'") & ",Colonia='" & negocio.Colonia & "',CodigoPostal='" & negocio.CodigoPostal & "'," _
            & "Municipio='" & negocio.Municipio & "', Ciudad='" & negocio.Ciudad & "',Estado='" & negocio.Estado & "' " _
            & "WHERE Id_Cliente = " & negocio.Id_Cliente
        Return Utilerias.setUpdInsDel(strUpdt)
    End Function

End Class
