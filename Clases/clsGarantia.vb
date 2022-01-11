Imports System.Data.SqlClient
Imports Microsoft.VisualBasic
Public Class clsGarantia

    Public Enum Garantia_Lista
        Lista_gral = 1

    End Enum


    ''' <summary>
    '''  Metodo para listar los clientes que han tenido créditos con garantias
    ''' </summary>
    ''' 
    ''' <returns></returns>
    Public Shared Function getDataSource(numlis As Integer) As DataTable
        Dim dt As DataTable
        Dim da As SqlDataAdapter

        Dim conexion As String = clsConexion.ConnectionString
        Using con As New SqlConnection(conexion.ToString())
            con.Open()

            Dim command As New SqlCommand("CLIENTEGARANTIALIS", con)
            command.CommandType = CommandType.StoredProcedure
            command.Parameters.AddWithValue("Par_NumLis", numlis)


            ' da = New SqlDataAdapter(command)
            dt = New DataTable()
            dt.Load(command.ExecuteReader())
            ' da.Fill(dt)

        End Using

        Return dt

    End Function


End Class
