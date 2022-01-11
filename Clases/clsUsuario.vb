Imports System.Data.SqlClient

Public Class clsUsuario

    Public Property Login As String
    Public Property Password As String
    Public Property Nombre As String
    Public Property IdSucursal As Integer
    Public Property Sucursal As String
    Public Property Tipo As Integer
    Public Property DescTipo As String
    Public Property Estado As Boolean
    Public Property AutCondonaciones As Boolean
    Public Property UsuarioID As Integer
    Public Property PorcentajeCond As Decimal
    Public Property PorcentajeText As String


    Public Sub establecerPass(pass As String)
        Me.Password = Utilerias.Encriptar(pass)
    End Sub

    Public Shared Function obtenerUsuario(LOGIN As String) As clsUsuario
        Dim clUsuario As New clsUsuario
        Dim StrSelect As String = " SELECT U.Login, U.Pass, U.Nombre, U.Sucursal AS IdSucursal," _
        & " CS.Descripcion AS Sucursal, U.Tipo, CTU.Descripcion, U.Estado, U.AutCondonaciones, U.UsuarioID" _
        & " FROM USUARIOS U INNER JOIN" _
        & " CATTIPOUSUARIOS CTU ON U.Tipo = CTU.Id JOIN" _
        & " CATSUCURSALES CS ON U.Sucursal = CS.Id" _
        & " WHERE (U.Login = '" & LOGIN & "') "
        Dim conexion As String = clsConexion.ConnectionString
        Using Con As New SqlConnection(conexion)
            Con.Open()
            Using Com As New SqlCommand(StrSelect, Con)
                Using rst = Com.ExecuteReader()
                    If rst.HasRows Then
                        While rst.Read
                            clUsuario.Login = rst("Login")
                            clUsuario.Password = rst("Pass")
                            clUsuario.Nombre = rst("Nombre")
                            clUsuario.IdSucursal = rst("IdSucursal")
                            clUsuario.Sucursal = rst("Sucursal")
                            clUsuario.Tipo = rst("Tipo")
                            clUsuario.DescTipo = rst("Descripcion")
                            clUsuario.Estado = rst("Estado")
                            clUsuario.AutCondonaciones = rst("AutCondonaciones")
                            clUsuario.UsuarioID = rst("UsuarioID")
                        End While
                    Else
                        Return Nothing
                    End If
                End Using
            End Using
        End Using
        Return clUsuario
    End Function



    ''' <summary>
    ''' obtiene el usuario y los montos permitidos para condonar
    ''' </summary>
    ''' <param name="LOGIN"></param>
    ''' <returns></returns>
    Public Shared Function obtenerUsuarioCondEmp(LOGIN As String) As clsUsuario
        Dim clUsuario As New clsUsuario
        Dim StrSelect As String = "SELECT U.Login, U.Pass, U.Nombre, U.Sucursal AS IdSucursal," _
        & "CS.Descripcion AS Sucursal, U.Tipo, CTU.Descripcion, U.Estado, U.AutCondonaciones, U.UsuarioID, " _
        & "COND.PorcentajeTxt, COND.PorcentajeCond " _
        & "FROM USUARIOS U INNER JOIN " _
        & "CATTIPOUSUARIOS CTU ON U.Tipo = CTU.Id JOIN " _
        & "CATSUCURSALES CS ON U.Sucursal = CS.Id " _
        & "INNER JOIN PERFILCONDONACIONESEMP COND ON CTU.ID = COND.TipoUsuarioID  " _
        & "WHERE (U.Login = '" & LOGIN & "')"
        Dim conexion As String = clsConexion.ConnectionString
        Using Con As New SqlConnection(conexion)
            Con.Open()
            Using Com As New SqlCommand(StrSelect, Con)
                Using rst = Com.ExecuteReader()
                    If rst.HasRows Then
                        While rst.Read
                            clUsuario.Login = rst("Login")
                            clUsuario.Password = rst("Pass")
                            clUsuario.Nombre = rst("Nombre")
                            clUsuario.IdSucursal = rst("IdSucursal")
                            clUsuario.Sucursal = rst("Sucursal")
                            clUsuario.Tipo = rst("Tipo")
                            clUsuario.DescTipo = rst("Descripcion")
                            clUsuario.Estado = rst("Estado")
                            clUsuario.AutCondonaciones = rst("AutCondonaciones")
                            clUsuario.UsuarioID = rst("UsuarioID")
                            clUsuario.PorcentajeText = rst("PorcentajeTxt")
                            clUsuario.PorcentajeCond = rst("PorcentajeCond")
                        End While
                    Else
                        Return Nothing
                    End If
                End Using
            End Using
        End Using
        Return clUsuario
    End Function


    Public Shared Function actualizarUsuario(usuario As clsUsuario) As Boolean

        Dim strUpdt As String
        If usuario.Password <> "" Then
            strUpdt = " UPDATE USUARIOS" _
            & " SET Login = '" & usuario.Login & "', Pass = '" & usuario.Password & "', Nombre = '" & usuario.Nombre & "'," _
            & " Sucursal = " & usuario.IdSucursal & ", Tipo = " & usuario.Tipo & ", Estado = '" & usuario.Estado & "'" _
            & " WHERE (Login = '" & usuario.Login & "') "
        Else
            strUpdt = " UPDATE USUARIOS" _
            & " SET Login = '" & usuario.Login & "', Nombre = '" & usuario.Nombre & "'," _
            & " Sucursal = " & usuario.IdSucursal & ", Tipo = " & usuario.Tipo & ", Estado = '" & usuario.Estado & "'" _
            & " WHERE (Login = '" & usuario.Login & "') "
        End If

        Utilerias.setUpdInsDel(StrUpdt)
    End Function

    Public Shared Function listarUsuarios() As List(Of clsUsuario)
        Dim clUsuario As New List(Of clsUsuario)
        Dim StrSelect As String = " SELECT U.Login, U.Pass, U.Nombre U.Sucursal AS IdSucursal, " _
            & " CS.Descripcion AS Sucursal, U.Tipo, CTU.Descripcion, U.Estado, U.AutCondonaciones" _
            & " FROM USUARIOS U INNER JOIN " _
            & " CATTIPOUSUARIOS CTU ON U.Tipo = CTU.Id JOIN" _
            & " CATSUCURSALES CS ON U.Sucursal = CS.Id"
        Dim conexion As String = clsConexion.ConnectionString
        Using Con As New SqlConnection(conexion)
            Con.Open()
            Using Com As New SqlCommand(StrSelect, Con)
                Using rst = Com.ExecuteReader()
                    If rst.HasRows Then
                        Dim cUsuario As clsUsuario = New clsUsuario()
                        While rst.Read
                            cUsuario.Login = rst("Login")
                            cUsuario.Password = rst("Pass")
                            cUsuario.Nombre = rst("Nombre")
                            cUsuario.IdSucursal = rst("IdSucursal")
                            cUsuario.Sucursal = rst("Sucursal")
                            cUsuario.Tipo = rst("Tipo")
                            cUsuario.DescTipo = rst("Descripcion")
                            cUsuario.Estado = rst("Estado")
                            cUsuario.AutCondonaciones = rst("AutCondonaciones")
                            clUsuario.Add(cUsuario)
                        End While
                    End If
                End Using
            End Using
        End Using
        Return clUsuario
    End Function

    Public Shared Function existeUsuario(userLogin As String) As Boolean
        Dim existe = False
        Dim StrSelect As String = " SELECT * FROM USUARIOS" _
        & " WHERE (Login = '" & userLogin & "')"
        Dim conexion As String = clsConexion.ConnectionString
        Using Con As New SqlConnection(conexion)
            Con.Open()
            Using Com As New SqlCommand(StrSelect, Con)
                Using rst = Com.ExecuteReader()
                    If rst.HasRows Then
                        existe = True
                    End If
                End Using
            End Using
        End Using
        Return existe
    End Function

End Class
