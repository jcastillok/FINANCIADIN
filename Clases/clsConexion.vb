Imports System.Data.SqlClient

Public Class clsConexion

    Public Shared Function Open() As ADODB.Connection
        Dim cn As New ADODB.Connection()
        Try

            Dim conexion As String = clsConexion.ConnectionString
            Dim arr1 As String() = Split(conexion, ";")
            REM no se puede cambiar el tipo de conexion porque utiliza un control que tine IRAK para descifrar el password
            conexion = "Provider=SQLNCLI10; Server=" & Trim(Split(arr1(0), "=")(1)) & "; Database=" & Trim(Split(arr1(1), "=")(1)) & "; " _
            & "Uid=" & Trim(Split(arr1(3), "=")(1)) & "; Password=" & Trim(Split(arr1(4), "=")(1)) & ";"
            cn.ConnectionString = conexion
            cn.Open()
        Catch ex As Exception

        End Try
        Return cn
    End Function

    Public Shared ReadOnly Property ConnectionString() As String
		Get
            Dim connectionName As String = "FinanciaDINConnectionString"
            If ConfigurationManager.ConnectionStrings.Count > 0 AndAlso Not ConfigurationManager.ConnectionStrings(connectionName).ToString().Equals([String].Empty) Then
				Return ConfigurationManager.ConnectionStrings(connectionName).ToString()
			Else
				Throw New Exception("No se pudo encontrar " & connectionName & " en ConnectionString en la seccion configuracion del Sistema ")
			End If
		End Get
	End Property

End Class
