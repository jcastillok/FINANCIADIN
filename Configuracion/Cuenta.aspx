<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site.Master"
    CodeBehind="Cuenta.aspx.vb" Inherits="FinanciaDin.Cuenta" %>

<%@ Register Assembly="DevExpress.Web.v15.2, Version=15.2.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web" TagPrefix="dx" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
    </asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <br />
    <br />
    <center>
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
                <asp:ScriptManager ID="ScriptManager1" runat="server">
                </asp:ScriptManager>
                <dx:ASPxRoundPanel ID="ASPxRoundPanel1" runat="server" HeaderText="Configuración de Cuenta"
                    Theme="DevEx">
                    <PanelCollection>
                        <dx:PanelContent>
                            <table width="100%">
                                <tr>
                                    <td align="center">Informació de Usuario</td>
                                </tr>
                                <tr>
                                    <td align="center">&nbsp;</td>
                                </tr>
                                <tr>
                                    <td align="center">
                                        <dx:ASPxFormLayout ID="ASPxFormLayout1" runat="server">
                                            <Items>
                                                <dx:LayoutItem Caption="Login:" Name="Login">
                                                    <LayoutItemNestedControlCollection>
                                                        <dx:LayoutItemNestedControlContainer runat="server" SupportsDisabledAttribute="True">
                                                            <dx:ASPxTextBox ID="txtLogin" runat="server" Enabled="False" ReadOnly="True">
                                                            </dx:ASPxTextBox>
                                                        </dx:LayoutItemNestedControlContainer>
                                                    </LayoutItemNestedControlCollection>
                                                </dx:LayoutItem>
                                                <dx:LayoutItem Caption="Nombre" Name="Nombre">
                                                    <LayoutItemNestedControlCollection>
                                                        <dx:LayoutItemNestedControlContainer runat="server">
                                                            <dx:ASPxTextBox ID="txtNombre" runat="server" Enabled="False" ReadOnly="True">
                                                            </dx:ASPxTextBox>
                                                        </dx:LayoutItemNestedControlContainer>
                                                    </LayoutItemNestedControlCollection>
                                                </dx:LayoutItem>
                                                <dx:LayoutItem Caption="Contraseña:" Name="Contrasenia">
                                                    <LayoutItemNestedControlCollection>
                                                        <dx:LayoutItemNestedControlContainer runat="server" SupportsDisabledAttribute="True">
                                                            <dx:ASPxTextBox ID="txtPass" runat="server" Width="170px" Password="True">
                                                            </dx:ASPxTextBox>
                                                        </dx:LayoutItemNestedControlContainer>
                                                    </LayoutItemNestedControlCollection>
                                                </dx:LayoutItem>
                                                <dx:LayoutItem Caption="Nueva:" Name="NuevaPass">
                                                    <LayoutItemNestedControlCollection>
                                                        <dx:LayoutItemNestedControlContainer runat="server">
                                                            <dx:ASPxTextBox ID="txtPassNueva" runat="server" Password="True" Width="170px">
                                                            </dx:ASPxTextBox>
                                                        </dx:LayoutItemNestedControlContainer>
                                                    </LayoutItemNestedControlCollection>
                                                </dx:LayoutItem>
                                                <dx:LayoutItem Caption="Confirmación:" Name="txtPassConf">
                                                    <LayoutItemNestedControlCollection>
                                                        <dx:LayoutItemNestedControlContainer runat="server">
                                                            <dx:ASPxTextBox ID="txtPassConf" runat="server" Password="True">
                                                            </dx:ASPxTextBox>
                                                        </dx:LayoutItemNestedControlContainer>
                                                    </LayoutItemNestedControlCollection>
                                                </dx:LayoutItem>
                                                <dx:LayoutItem Caption="Sucursal" Name="Sucursal">
                                                    <LayoutItemNestedControlCollection>
                                                        <dx:LayoutItemNestedControlContainer runat="server">
                                                            <dx:ASPxComboBox ID="cbSuc" runat="server" TextField="Descripcion" ValueField="Id" ValueType="System.Int32">
                                                            </dx:ASPxComboBox>
                                                        </dx:LayoutItemNestedControlContainer>
                                                    </LayoutItemNestedControlCollection>
                                                </dx:LayoutItem>
                                            </Items>
                                            <SettingsItemCaptions Location="Left" />
                                            <SettingsItems HorizontalAlign="Right" />
                                        </dx:ASPxFormLayout>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="center">
                                        <asp:ImageButton ID="imgGuardar" runat="server" BorderStyle="None" Height="30px"
                                            ImageUrl="~/Images/add.ico" Width="30px" />
                                    </td>
                                </tr>
                                <tr>
                                    <td align="center">
                                        <dx:ASPxLabel ID="lblAccion" runat="server" Text="Guardar">
                                        </dx:ASPxLabel>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="center" colspan="3">
                                        <font color="red">
                                            <asp:Literal ID="Literal1" runat="server"></asp:Literal>
                                        </font>
                                    </td>
                                </tr>
                            </table>
                            <br />
                            <br />
                            <div align="center">
                            </div>
                        </dx:PanelContent>
                    </PanelCollection>
                </dx:ASPxRoundPanel>
            </ContentTemplate>
        </asp:UpdatePanel>
    </center>
    <script type="text/javascript">
        setInterval('MantenSesion()', <%= (Int(0.9 * (Session.Timeout * 60000)))%>);
    </script>
</asp:Content>
