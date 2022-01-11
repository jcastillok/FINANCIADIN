Imports System.Data.SqlClient

Public Class clsDiaInhabil
    Public Property Id As Integer
    Public Property Fecha As DateTime
    Public Property Descripcion As String
    Public Property Login As String
    Public Property FecCap As DateTime

    Public Shared Function obtener(Fecha As Date, Optional ID As Integer = 0) As clsDiaInhabil

        Dim condicion As String = ""

        If ID <> 0 Then
            condicion = "Id = " & ID
        Else
            condicion = String.Format("Fecha = convert(datetime,'{0}',103)", Fecha)
        End If

        Dim clDiaInhabil As New clsDiaInhabil
        Dim StrSelect As String = "SELECT *" _
        & " FROM CATDIASINHAB" _
        & " WHERE " & condicion
        Dim conexion As String = clsConexion.ConnectionString
        Using Con As New SqlConnection(conexion)
            Con.Open()
            Using Com As New SqlCommand(StrSelect, Con)
                Using rst = Com.ExecuteReader()
                    If rst.HasRows Then
                        While rst.Read
                            clDiaInhabil.Id = rst("Id")
                            clDiaInhabil.Fecha = rst("Fecha")
                            clDiaInhabil.Descripcion = rst("Descripcion")
                            clDiaInhabil.Login = rst("Login")
                            clDiaInhabil.FecCap = rst("FecCap")
                        End While
                    End If
                End Using
            End Using
        End Using
        Return clDiaInhabil
    End Function

    Public Shared Function insertar(fechaInhabil As clsDiaInhabil) As Boolean
        Dim strIns As String = "INSERT INTO CATDIASINHAB (Fecha, Descripcion, Login, FecCap) " _
            & "VALUES(convert(datetime,'" & fechaInhabil.Fecha & "',103),'" & fechaInhabil.Descripcion & "'," _
            & "'" & fechaInhabil.Login & "',convert(datetime,'" & fechaInhabil.FecCap & "',103))"

        Utilerias.setUpdInsDel(strIns)
    End Function

    Public Shared Function esInhabil(fecha As DateTime) As Boolean
        Dim clDiaInhabil As New clsDiaInhabil
        Dim StrSelect As String = "SELECT *" _
        & " FROM CATDIASINHAB" _
        & " WHERE Fecha = convert(datetime,'" & fecha.Date & "',101)"
        Dim conexion As String = clsConexion.ConnectionString
        Using Con As New SqlConnection(conexion)
            Con.Open()
            Using Com As New SqlCommand(StrSelect, Con)
                Using rst = Com.ExecuteReader()
                    If rst.HasRows Then
                        Return True
                    End If
                End Using
            End Using
        End Using
        Return False
    End Function

    Public Shared Function obtenerLista(desde As Date, Optional hasta As Date = Nothing) As List(Of clsDiaInhabil)
        
        Dim DiasInhabiles As New List (Of clsDiaInhabil)
        Dim StrSelect As String = "SELECT *" _
        & " FROM CATDIASINHAB" _
        & " WHERE Fecha >= convert(datetime,'" & desde & "',103)" & If(IsNothing(hasta),""," AND Fecha <= convert(datetime,'" & hasta & "',103)")
        Dim conexion As String = clsConexion.ConnectionString
        Using Con As New SqlConnection(conexion)
            Con.Open()
            Using Com As New SqlCommand(StrSelect, Con)
                Using rst = Com.ExecuteReader()
                    Dim clDiaInhabil As New clsDiaInhabil
                    If rst.HasRows Then
                        While rst.Read            
                            clDiaInhabil.Id = rst("Id")
                            clDiaInhabil.Fecha = rst("Fecha")
                            clDiaInhabil.Descripcion = rst("Descripcion")
                            clDiaInhabil.Login = rst("Login")
                            clDiaInhabil.FecCap = rst("FecCap")
                            DiasInhabiles.Add(clDiaInhabil)
                        End While
                    End If
                End Using
            End Using
        End Using
        Return DiasInhabiles
    End Function

End Class
