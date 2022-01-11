Imports System.Data.SqlClient
Imports ADODB

Public Class _Default
    Inherits System.Web.UI.Page

    Dim CnnDin As New SqlConnection

    Protected Sub Login1_Authenticate(sender As Object, e As System.Web.UI.WebControls.AuthenticateEventArgs) Handles Login1.Authenticate

        'Dim conx = clsConexion.Open
        ''Dim connstr = clsConexion.ConnectionString
        ''Dim Cone = New SqlConnection(connstr)
        'Dim cmd As New ADODB.Command
        'Dim rst As New ADODB.Recordset
        'cmd.ActiveConnection = conx

        If ValidaPassword() Then
            REM validar si es de tipo administrador
            Dim clUsuario As clsUsuario = clsUsuario.obtenerUsuario(Login1.UserName)
            Session("Usuario") = clUsuario
            Session("SucActualUsr") = clUsuario.Sucursal
            Session("TipoUsuario") = Utilerias.getDataTable("SELECT Descripcion FROM CATTIPOUSUARIOS WHERE Id = " + Session("Usuario").Tipo.ToString).Rows.Item(0).Item(0).ToString.ToUpper

            REM Se verifica si el usuario es ADMINISTRADOR, ANALISTA O COBRADOR
            If clUsuario.Tipo = 1 Or
                clUsuario.Tipo = 2 Or
                clUsuario.Tipo = 3 Or
                clUsuario.Tipo = 6 Or
                clUsuario.Tipo = 7 Or
                clUsuario.Tipo = 9 Or
                clUsuario.Tipo = 10 Or
                 clUsuario.Tipo = 12 Or
                clUsuario.Tipo = 11 Then


                FormsAuthentication.RedirectFromLoginPage(Login1.UserName, Login1.RememberMeSet)
                Response.Redirect("Bienvenida\General.aspx")
            End If
        End If
        'conx.Close()

        'If autentificado = True Then
        '    If TipoUsuario = 4 Then
        '        Exit Sub
        '    Else
        '        FormsAuthentication.RedirectFromLoginPage(Login1.UserName, Login1.RememberMeSet)
        '        Response.Redirect("Bienvenida\General.aspx")
        '    End If
        'End If

    End Sub

    Private Function ValidaPassword()
        Dim login As String = ""
        Dim pass As String = ""


        Dim conexion As ConnectionStringSettings = ConfigurationManager.ConnectionStrings("FinanciaDinConnectionString")

        Using con As New SqlConnection(conexion.ToString())
            con.Open()
            Dim comando = String.Format("Select Login, Pass FROM USUARIOS WHERE (Login = '{0}')", Login1.UserName)
            Dim command As New SqlCommand(comando, con)
            command.CommandType = CommandType.Text

            Dim dr As SqlDataReader = command.ExecuteReader()
            Do While dr.Read

                login = dr("Login").ToString()
                pass = dr("Pass").ToString()
            Loop
        End Using


        Dim encriptPass = Utilerias.Encriptar(Login1.Password)
        'cmd.CommandText = String.Format("SELECT Login, Pass FROM USUARIOS WHERE (Login = '{0}')", Login1.UserName)
        'rst = cmd.Execute
        Try
            If login.ToString.ToLower = Login1.UserName.ToLower And pass = encriptPass Then
                Return True
            End If
        Catch ex As Exception

        End Try
        Return False
    End Function

End Class