Imports System.Data.SqlClient

Public Class ClienteGarantiaModelView
    Public Property CredLiqID As Integer
    Public Property MontoGarCredLiq As Double

    Public Property FechaLiq As DateTime

    Public Property ClienteID As Integer

    Public Property GarantiaApl As Boolean

    Public Property CredNvoID As Integer
    Public Property MontoGarCredNvo As Double

    Public Property FechaIniCredNvo As DateTime

    Public Property MontoCred As Double
    Public Property SucursalID As Integer

    Public Property Sucursal As String

    Public Property FecPrimPago As DateTime

    Public Property Adeudo As Double

    Public Property CodigoRespuesta As String

    Public Property MensajeRespuesta As String

    Public Enum Con_ClienteGarantia
        Con_Credito_Liquidado = 1
        Con_Credito_Nuevo = 2
    End Enum

    Public Shared Function ClienteGarantiaCon(clienteID As Int32, numCon As Integer) As ClienteGarantiaModelView
        Dim ClienteGarantia As ClienteGarantiaModelView = Nothing
        Try
            Dim conexion As String = clsConexion.ConnectionString
            Using con As New SqlConnection(conexion.ToString())
                con.Open()

                ' se crea el objeto sqlcomand
                Dim command As New SqlCommand("CLIENTEGARANTIACON", con)
                command.CommandType = CommandType.StoredProcedure
                command.Parameters.AddWithValue("@Par_Clienteid", clienteID)
                command.Parameters.AddWithValue("@Par_NumCon", numCon)

                Dim dr As SqlDataReader = command.ExecuteReader()

                Do While dr.Read

                    ClienteGarantia = New ClienteGarantiaModelView()
                    ClienteGarantia.CredLiqID = dr("id").ToString()
                    ClienteGarantia.FechaLiq = dr("fecha_fin").ToString()
                    ClienteGarantia.MontoGarCredLiq = dr("MontoGar").ToString()
                    ClienteGarantia.GarantiaApl = Convert.ToBoolean(dr("GarantiaAplicada").ToString())
                    ClienteGarantia.ClienteID = dr("Id_Cliente").ToString()
                    ClienteGarantia.CodigoRespuesta = "000000"
                    ClienteGarantia.MensajeRespuesta = "Consulta Realizada con Exito"


                Loop
                dr.Close()

            End Using

            If IsNothing(ClienteGarantia) Then
                ClienteGarantia = New ClienteGarantiaModelView
                ClienteGarantia.CodigoRespuesta = "000000"
                ClienteGarantia.MensajeRespuesta = "Consulta Sin Datos"
            End If


        Catch ex As Exception
            If IsNothing(ClienteGarantia) Then

            End If

        End Try
        Return ClienteGarantia
    End Function


    Public Shared Function ClienteGarCredNvoCon(clienteID As Int32, numCon As Integer) As ClienteGarantiaModelView
        Dim ClienteGarantia As ClienteGarantiaModelView = Nothing
        Try
            Dim conexion As String = clsConexion.ConnectionString
            Using con As New SqlConnection(conexion.ToString())
                con.Open()

                ' se crea el objeto sqlcomand
                Dim command As New SqlCommand("CLIENTEGARANTIACON", con)
                command.CommandType = CommandType.StoredProcedure
                command.Parameters.AddWithValue("@Par_Clienteid", clienteID)
                command.Parameters.AddWithValue("@Par_NumCon", numCon)

                Dim dr As SqlDataReader = command.ExecuteReader()

                Do While dr.Read

                    ClienteGarantia = New ClienteGarantiaModelView()
                    ClienteGarantia.CredNvoID = dr("id").ToString()
                    ClienteGarantia.FechaIniCredNvo = dr("fecInicio").ToString()
                    ClienteGarantia.MontoCred = dr("MontoPrestado").ToString()
                    ClienteGarantia.MontoGarCredNvo = dr("MontoGar").ToString()
                    ClienteGarantia.ClienteID = dr("Id_Cliente").ToString()
                    ClienteGarantia.SucursalID = dr("Id_Sucursal").ToString()
                    ClienteGarantia.Sucursal = dr("Descripcion").ToString()
                    ClienteGarantia.FecPrimPago = dr("FecPrimPago").ToString()
                    ClienteGarantia.FecPrimPago = dr("Adeudo").ToString()

                    ClienteGarantia.CodigoRespuesta = "000000"
                    ClienteGarantia.MensajeRespuesta = "Consulta Realizada con Exito"

                Loop
                dr.Close()

            End Using
            If IsNothing(ClienteGarantia) Then
                ClienteGarantia = New ClienteGarantiaModelView
                ClienteGarantia.CodigoRespuesta = "000000"
                ClienteGarantia.MensajeRespuesta = "Consulta Sin Datos"

            End If
        Catch ex As Exception
            If IsNothing(ClienteGarantia) Then
                ClienteGarantia = New ClienteGarantiaModelView
                ClienteGarantia.CodigoRespuesta = "ERROR-200002"
                ClienteGarantia.MensajeRespuesta = "Ocurrio un error al consultar informacion 
                    del nuevo credito " + ex.Message

            End If

        End Try
        Return ClienteGarantia
    End Function


End Class
