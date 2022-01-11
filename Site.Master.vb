Public Class Site1
    Inherits System.Web.UI.MasterPage

    Private Sub Page_Init(sender As Object, e As System.EventArgs) Handles Me.Init
        Dim usuario As clsUsuario = Nothing

        Try
            If IsNothing(DirectCast(Session("Usuario"), clsUsuario)) Then
                FormsAuthentication.SignOut()
                Session.Contents.RemoveAll()
                Me.Dispose()
                Session.Abandon()
                Response.Redirect("Default.aspx", False)

            Else
                usuario = DirectCast(Session("Usuario"), clsUsuario)
                LblNombre.Text = DirectCast(Session("Usuario"), clsUsuario).Nombre
                LblSuc.Text = Session("SucActualUsr")
                LblAcceso.Text = Session("TipoUsuario")

                'ID  Descripcion
                '1   Administrador
                '2   Analista
                '3   Cobrador
                '4   Sin Permisos
                '5   Asesor
                '6   Contabilidad
                '7   Coordinador
                '9   Gerente Tienda
                '10  Analista Administracion


                If usuario.Tipo = 1 Then
                    'ASPxMenu1.Items.Item(0).Items.Item(2).Visible = False
                    'ASPxMenu1.Items.Item(1).Items.Item(0).Visible = False
                    ' usuario mildred admon
                    ASPxMenu1.Items.Item(5).Visible = False

                ElseIf usuario.Tipo = 2 Or usuario.Tipo = 10 Then
                    'ASPxMenu1.Items.Item(0).Items.Item(2).Visible = False
                    'ASPxMenu1.Items.Item(1).Items.Item(0).Visible = False
                    ASPxMenu1.Items.Item(0).Items.Item(5).Visible = False

                    ASPxMenu1.Items.Item(1).Items.Item(2).Visible = False
                    ASPxMenu1.Items.Item(3).Items.Item(0).Visible = False
                    ASPxMenu1.Items.Item(3).Items.Item(1).Visible = False
                    ASPxMenu1.Items.Item(4).Items.Item(1).Visible = False
                ElseIf DirectCast(Session("Usuario"), clsUsuario).Tipo = 3 Or DirectCast(Session("Usuario"), clsUsuario).Tipo = 9 Then
                    ASPxMenu1.Items.Item(0).Items.Item(0).Visible = False
                    ASPxMenu1.Items.Item(0).Items.Item(1).Visible = False
                    ASPxMenu1.Items.Item(0).Items.Item(3).Visible = False
                    ASPxMenu1.Items.Item(0).Items.Item(2).Visible = False

                    ASPxMenu1.Items.Item(1).Items.Item(3).Visible = False
                    ASPxMenu1.Items.Item(3).Visible = False
                ElseIf DirectCast(Session("Usuario"), clsUsuario).Tipo = 6 Then
                    ASPxMenu1.Items.Item(0).Visible = False
                    ASPxMenu1.Items.Item(1).Items.Item(0).Visible = False
                    ASPxMenu1.Items.Item(1).Items.Item(2).Visible = False
                    ASPxMenu1.Items.Item(1).Items.Item(3).Visible = False
                    ASPxMenu1.Items.Item(2).Visible = False
                    ASPxMenu1.Items.Item(3).Visible = False
                ElseIf DirectCast(Session("Usuario"), clsUsuario).Tipo = 7 Then
                    ASPxMenu1.Items.Item(0).Visible = False
                    ASPxMenu1.Items.Item(1).Items.Item(0).Visible = False
                    ASPxMenu1.Items.Item(1).Items.Item(2).Visible = False
                    ASPxMenu1.Items.Item(3).Visible = False
                    ' permisos para cancelar 
                ElseIf DirectCast(Session("Usuario"), clsUsuario).Tipo = 11 Then
                    ASPxMenu1.Items.Item(0).Visible = False
                    ASPxMenu1.Items.Item(1).Visible = False
                    ASPxMenu1.Items.Item(2).Visible = False
                    ASPxMenu1.Items.Item(3).Items.Item(0).Visible = False
                    ASPxMenu1.Items.Item(3).Items.Item(1).Visible = False
                    ASPxMenu1.Items.Item(4).Visible = False
                    ASPxMenu1.Items.Item(5).Visible = False
                    ASPxMenu1.Items.Item(1).Items.Item(3).Visible = False
                    'ElseIf DirectCast(Session("Usuario"), clsUsuario).Tipo = 11 Then
                    '    ASPxMenu1.Items.Item(1).Items.Item(3).Visible = False
                ElseIf DirectCast(Session("Usuario"), clsUsuario).DescTipo.Equals("Cobranza") Then
                    ASPxMenu1.Items.Item(0).Visible = False
                    ASPxMenu1.Items.Item(1).Visible = False
                    ASPxMenu1.Items.Item(2).Visible = False
                    ASPxMenu1.Items.Item(3).Visible = False
                    ASPxMenu1.Items.Item(4).Items.Item(0).Visible = False
                    ASPxMenu1.Items.Item(4).Items.Item(1).Visible = True
                    ASPxMenu1.Items.Item(5).Visible = False
                    ASPxMenu1.Items.Item(6).Visible = False
                Else
                    ASPxMenu1.Visible = False
                End If

                If Not DirectCast(Session("Usuario"), clsUsuario).AutCondonaciones Then
                    ASPxMenu1.Items.Item(5).Items.Item(1).Visible = False
                End If

            End If
        Catch ex As Exception
            FormsAuthentication.SignOut()
            Session.Contents.RemoveAll()
            Me.Dispose()
            Session.Abandon()
            Response.Redirect("Default.aspx", False)
        End Try
    End Sub

    Private Sub Page_Disposed(sender As Object, e As System.EventArgs) Handles Me.Disposed
        FormsAuthentication.SignOut()
        Response.Redirect(Request.UrlReferrer.ToString())
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

    End Sub

    Protected Sub ASPxMenu1_ItemClick(source As Object, e As DevExpress.Web.MenuItemEventArgs) Handles ASPxMenu1.ItemClick
        If e.Item.Text = "Salir" Then
			FormsAuthentication.SignOut()
			Session.Contents.RemoveAll()
			Me.Dispose()
			Session.Abandon()
			Response.Redirect("~/Default.aspx")
		End If
    End Sub
End Class