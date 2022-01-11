Imports System.Data.SqlClient

Public Class clsCreditoViewModel
    Public Property Id As Integer
    Public Property nombCliente As String
    Public Property numPagos As Integer
    Public Property esGarantia As Boolean

    Public Property codigo As String

    Public Property mensaje As String

    Public Shared Function consultaDatosCredito(credito As clsCredito) As DataTable
        Dim CreditoCon As clsCreditoViewModel = Nothing
        Dim dt As DataTable = Nothing
        Dim da As SqlDataAdapter
        Try
            Dim conexion As String = clsConexion.ConnectionString

            Using con As New SqlConnection(conexion.ToString())
                con.Open()

                Dim command As New SqlCommand("ADMCREDITOSCON", con)
                command.CommandType = CommandType.StoredProcedure
                command.Parameters.AddWithValue("@Par_creditoID", credito.Id)


                'Dim dr As SqlDataReader = command.ExecuteReader()
                'Do While dr.Read

                '    CreditoCon = New clsCreditoViewModel
                '    CreditoCon.Id = dr("FOLIO_CREDITO").ToString()
                '    CreditoCon.nombCliente = dr("Nombre").ToString()
                '    CreditoCon.numPagos = dr("PagosRealizados").ToString()
                '    CreditoCon.numPagos = dr("Garantia").ToString()

                'Loop
                da = New SqlDataAdapter(command)
                dt = New DataTable()
                da.Fill(dt)
            End Using

        Catch ex As Exception
            If IsNothing(CreditoCon) Then
                CreditoCon.codigo = "000001"
                CreditoCon.mensaje = "Ocurrio un error en la ejecucion del proceso ADMCREDITOSCON " +
                    "\n mensaje tecnico: " + ex.Message
            End If

        End Try
        Return dt
    End Function


End Class
