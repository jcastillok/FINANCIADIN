Imports System.Data.SqlClient

Public Class clsRazonSocial

    Public Property RazonSocialID As Integer
    Public Property Nombre As String
    Public Property CP As Integer
    Public Property Direccion As String
    Public Property RFC As String

    Public Enum Con_RazonSoc
        Con_Nombre_Razon_Soc = 1

    End Enum

    Public Shared Function RazonSocialCon(SucursalID As Integer, numCon As Integer) As clsRazonSocial
        Dim rz As clsRazonSocial = Nothing
        Try
            Dim conexion As String = clsConexion.ConnectionString
            Using con As New SqlConnection(conexion.ToString())
                con.Open()
                ' se crea el objeto transaction
                Dim myTrans As SqlTransaction = con.BeginTransaction()

                ' se crea el objeto sqlcomand
                Dim command As New SqlCommand("RAZONSOCSUCCON", con)
                command.CommandType = CommandType.StoredProcedure
                command.Parameters.AddWithValue("@Par_SucursalID", SucursalID)
                command.Parameters.AddWithValue("@Par_Numcon", numCon)



                Dim dr As SqlDataReader = command.ExecuteReader()

                    Do While dr.Read
                        rz = New clsRazonSocial()

                        rz.Nombre = dr("Nombre").ToString()
                        rz.CP = dr("CP").ToString()
                        rz.Direccion = dr("Direccion").ToString()
                        rz.RFC = dr("RFC").ToString()

                    Loop
                dr.Close()
            End Using

        Catch ex As Exception

        End Try

        Return rz
    End Function


End Class
