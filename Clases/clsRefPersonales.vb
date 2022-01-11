Imports System.Data.SqlClient

Public Class clsRefPersonales
    Public Property Id As Integer
    Public Property Id_Cliente As Integer
    Public Property Nombres1 As String
    Public Property ApellidoPaterno1 As String
    Public Property ApellidoMaterno1 As String
    Public Property Telefono1 As String
    Public Property TipoRef1 As String
    Public Property Direccion1 As String
    Public Property Nombres2 As String
    Public Property ApellidoPaterno2 As String
    Public Property ApellidoMaterno2 As String
    Public Property Telefono2 As String
    Public Property TipoRef2 As String
    Public Property Direccion2 As String
    Public Property Nombres3 As String
    Public Property ApellidoPaterno3 As String
    Public Property ApellidoMaterno3 As String
    Public Property Telefono3 As String
    Public Property TipoRef3 As String
    Public Property Direccion3 As String
    Public Property Nombres4 As String
    Public Property ApellidoPaterno4 As String
    Public Property ApellidoMaterno4 As String
    Public Property Telefono4 As String
    Public Property TipoRef4 As String
    Public Property Direccion4 As String



    Public Shared Function obtener(ID As Integer) As clsRefPersonales
        Dim StrSelect = "SELECT *" _
                & " FROM REFERENCIAS" _
                & " WHERE Id_Cliente = " & ID

        Dim refPersonales As New clsRefPersonales
        Dim conexion As String = clsConexion.ConnectionString
        Using Con As New SqlConnection(conexion)
            Con.Open()
            Using Com As New SqlCommand(StrSelect, Con)
                Using rst = Com.ExecuteReader()
                    If rst.HasRows Then
                        While rst.Read
                            refPersonales.Id = rst("Id")
                            refPersonales.Id_Cliente = rst("Id_Cliente")
                            refPersonales.Nombres1 = rst("Nombres1")
                            refPersonales.ApellidoPaterno1 = rst("ApellidoPaterno1")
                            refPersonales.ApellidoMaterno1 = rst("ApellidoMaterno1")
                            refPersonales.Telefono1 = rst("Telefono1")
                            refPersonales.TipoRef1 = rst("TipoRef1")
                            refPersonales.Direccion1 = rst("Direccion1")
                            refPersonales.Nombres2 = rst("Nombres2")
                            refPersonales.ApellidoPaterno2 = rst("ApellidoPaterno2")
                            refPersonales.ApellidoMaterno2 = rst("ApellidoMaterno2")
                            refPersonales.Telefono2 = rst("Telefono2")
                            refPersonales.TipoRef2 = rst("TipoRef2")
                            refPersonales.Direccion2 = rst("Direccion2")
                            refPersonales.Nombres3 = rst("Nombres3")
                            refPersonales.ApellidoPaterno3 = rst("ApellidoPaterno3")
                            refPersonales.ApellidoMaterno3 = rst("ApellidoMaterno3")
                            refPersonales.Telefono3 = rst("Telefono3")
                            refPersonales.TipoRef3 = rst("TipoRef3")
                            refPersonales.Direccion3 = rst("Direccion3")
                            refPersonales.Nombres4 = rst("Nombres4")
                            refPersonales.ApellidoPaterno4 = rst("ApellidoPaterno4")
                            refPersonales.ApellidoMaterno4 = rst("ApellidoMaterno4")
                            refPersonales.Telefono4 = rst("Telefono4")
                            refPersonales.TipoRef4 = rst("TipoRef4")
                            refPersonales.Direccion4 = rst("Direccion4")
                        End While
                    Else
                        Return Nothing
                    End If
                End Using
            End Using
        End Using
        Return refPersonales
    End Function

    Public Shared Function insertar(refPersonales As clsRefPersonales) As Boolean
        Dim strIns As String = "INSERT INTO REFERENCIAS (Id_Cliente, Nombres1, ApellidoPaterno1, ApellidoMaterno1, Telefono1, " _
            & "TipoRef1, Direccion1, Nombres2, ApellidoPaterno2, ApellidoMaterno2, Telefono2, TipoRef2, Direccion2, Nombres3, ApellidoPaterno3, " _
            & "ApellidoMaterno3, Telefono3, TipoRef3, Direccion3, Nombres4, ApellidoPaterno4, ApellidoMaterno4, Telefono4, TipoRef4, Direccion4) " _
            & "VALUES(" & refPersonales.Id_Cliente & ", '" & refPersonales.Nombres1 & "', '" & refPersonales.ApellidoPaterno1 & "', '" & refPersonales.ApellidoMaterno1 & "'," _
            & "'" & refPersonales.Telefono1 & "', '" & refPersonales.TipoRef1 & "','" & refPersonales.Direccion1 & "','" & refPersonales.Nombres2 & "', " _
            & "'" & refPersonales.ApellidoPaterno2 & "', '" & refPersonales.ApellidoMaterno2 & "','" & refPersonales.Telefono2 & "','" & refPersonales.TipoRef2 & "', " _
            & "'" & refPersonales.Direccion2 & "','" & refPersonales.Nombres3 & "','" & refPersonales.ApellidoPaterno3 & "','" & refPersonales.ApellidoMaterno3 & "', " _
            & "'" & refPersonales.Telefono3 & "','" & refPersonales.TipoRef3 & "','" & refPersonales.Direccion3 & "', '" & refPersonales.Nombres4 & "'," _
            & "'" & refPersonales.ApellidoPaterno4 & "','" & refPersonales.ApellidoMaterno4 & "','" & refPersonales.Telefono4 & "','" & refPersonales.TipoRef4 & "','" & refPersonales.Direccion4 & "')"

        Return Utilerias.setUpdInsDel(strIns)
    End Function

    Public Shared Function actualizar(refPersonales As clsRefPersonales) As Boolean
        Dim strUpdt As String = "UPDATE REFERENCIAS SET Nombres1='" & refPersonales.Nombres1 & "',ApellidoPaterno1='" & refPersonales.ApellidoPaterno1 & "'," _
            & "ApellidoMaterno1='" & refPersonales.ApellidoMaterno1 & "',Telefono1='" & refPersonales.Telefono1 & "',TipoRef1='" & refPersonales.TipoRef1 & "',Direccion1='" & refPersonales.Direccion1 & "'," _
            & "Nombres2='" & refPersonales.Nombres2 & "',ApellidoPaterno2='" & refPersonales.ApellidoPaterno2 & "',ApellidoMaterno2='" & refPersonales.ApellidoMaterno2 & "',Telefono2='" & refPersonales.Telefono2 & "',TipoRef2='" & refPersonales.TipoRef2 & "',Direccion2='" & refPersonales.Direccion2 & "'," _
            & "Nombres3='" & refPersonales.Nombres3 & "',ApellidoPaterno3='" & refPersonales.ApellidoPaterno3 & "',ApellidoMaterno3='" & refPersonales.ApellidoMaterno3 & "',Telefono3='" & refPersonales.Telefono3 & "',TipoRef3='" & refPersonales.TipoRef3 & "',Direccion3='" & refPersonales.Direccion3 & "'," _
            & "Nombres4='" & refPersonales.Nombres4 & "',ApellidoPaterno4='" & refPersonales.ApellidoPaterno4 & "',ApellidoMaterno4='" & refPersonales.ApellidoMaterno4 & "',Telefono4='" & refPersonales.Telefono4 & "',TipoRef4='" & refPersonales.TipoRef4 & "',Direccion4='" & refPersonales.Direccion4 & "' " _
            & "WHERE Id_Cliente = " & refPersonales.Id_Cliente
        Return Utilerias.setUpdInsDel(strUpdt)
    End Function

End Class
