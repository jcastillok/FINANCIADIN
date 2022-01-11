Imports System.Data.SqlClient

Public Class clsReporteCarteraVencida

    Public Property Nombre As String
    Public Property Direccion As String
    Public Property Celular As String
    Public Property Capital As Double
    Public Property Folio As String
    Public Property SaldoCapital As Double
    Public Property SaldoVencido As Double
    Public Property InteresPendiente As Double
    Public Property MoraGenerado As Double
    Public Property FechaUltimoPago As DateTime
    Public Property DiasAtraso As Integer
    Public Property FechaFinCredito As DateTime

    Public Property TotalDeuda As Decimal




    Shared Function getDataSource(sucID As String) As DataTable
        Dim dt As DataTable
        Dim da As SqlDataAdapter

        Dim conexion As String = clsConexion.ConnectionString
        Using con As New SqlConnection(conexion.ToString())
            con.Open()

            Dim command As New SqlCommand("REPORTECARTERALIS", con)
            command.CommandType = CommandType.StoredProcedure
            command.Parameters.AddWithValue("@Par_Sucursal", sucID)
            command.Parameters.AddWithValue("@Par_NumLis", 1)

            da = New SqlDataAdapter(command)
            dt = New DataTable()
            da.Fill(dt)

        End Using

        Return dt

    End Function


End Class
