<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site.Master"
    CodeBehind="AltaUsuarios.aspx.vb" Inherits="FinanciaDin.AltaUsuarios" %>

<%@ Register Assembly="DevExpress.Web.v15.2, Version=15.2.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web" TagPrefix="dx" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
    <style type="text/css">
        .auto-style4 {
            width: 984px;
        }
        .auto-style35 {
            height: 18px;
            width: 169px;
        }
        .auto-style37 {
            height: 18px;
            width: 99px;
        .auto-style43 {
            width: 99px;
        }
    }
        .auto-style38 {
            width: 99px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <br />
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <br />
    <center>
        <dx:ASPxRoundPanel ID="ASPxRoundPanel1" runat="server" HeaderText="Alta y Modificación de Usuarios"
            Theme="SoftOrange">
            <PanelCollection>
                <dx:PanelContent>
                    <table>
                        <tr>
                            <td class="auto-style4">
                            </td>
                        </tr>
                        <tr>
                            <td class="auto-style4">
                                <table align="left" class="auto-style4">
                                    <tr>
                                        <td class="auto-style38"></td>
                                        <td></td>
                                        <td rowspan="13" align="center">
                                            <asp:GridView ID="grdUsrs" runat="server" AutoGenerateColumns="False" Caption="Lista de Usuarios" CellPadding="4" DataKeyNames="Login" DataSourceID="SqlDataSource1" EmptyDataText="No hay registros para mostrar" ForeColor="Black" BackColor="#CCCCCC" BorderColor="#999999" BorderStyle="Solid" BorderWidth="3px" CellSpacing="2" AllowPaging="True" PageSize="7">
                                                <Columns>
                                                    <asp:CommandField ShowSelectButton="True" />
                                                    <asp:BoundField DataField="Login" HeaderText="Login" ReadOnly="True" SortExpression="Login" />
                                                    <asp:BoundField DataField="Nombre" HeaderText="Nombre" SortExpression="Nombre" />
                                                    <asp:BoundField DataField="Sucursal" HeaderText="Sucursal" SortExpression="Sucursal" />
                                                    <asp:BoundField DataField="DESCRIPCION" HeaderText="Acceso" SortExpression="DESCRIPCION" />
                                                    <asp:CheckBoxField DataField="Estado" HeaderText="Estado" SortExpression="Estado" />
                                                </Columns>
                                                <FooterStyle BackColor="#CCCCCC" />
                                                <HeaderStyle BackColor="Black" Font-Bold="True" ForeColor="White" />
                                                <PagerStyle BackColor="#CCCCCC" ForeColor="Black" HorizontalAlign="Left" />
                                                <RowStyle BackColor="White" />
                                                <SelectedRowStyle BackColor="#000099" Font-Bold="True" ForeColor="White" />
                                                <SortedAscendingCellStyle BackColor="#F1F1F1" />
                                                <SortedAscendingHeaderStyle BackColor="Gray" />
                                                <SortedDescendingCellStyle BackColor="#CAC9C9" />
                                                <SortedDescendingHeaderStyle BackColor="#383838" />
                                            </asp:GridView>
                                            <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:FinanciaDinConnectionString %>" SelectCommand="SELECT [U].[Login], [U].[Nombre], [CS].[Descripcion] AS Sucursal, [CTP].[DESCRIPCION], [U].[Estado] FROM [USUARIOS] [U] JOIN [CATTIPOUSUARIOS] [CTP] ON [U].[Tipo] = [CTP].[Id] JOIN [CATSUCURSALES] [CS] ON [U].[Sucursal] = [CS].[Id]"></asp:SqlDataSource>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="2">&nbsp;</td>
                                    </tr>
                                    <tr>
                                        <td class="auto-style38"></td>
                                        <td></td>
                                    </tr>
                                    <tr>
                                        <td align="right" class="auto-style38">
                                            Login:
                                        </td>
                                        <td align="left" class="auto-style42">
                                            <dx:ASPxTextBox ID="txtLogin" runat="server" Theme="SoftOrange" Width="170px">
                                            </dx:ASPxTextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="right" class="auto-style38" >
                                            Contraseña</td>
                                        <td align="left" class="auto-style42" valign="middle">
                                            <dx:ASPxTextBox ID="txtPass" runat="server" Password="True" style="margin-right: 1px" Theme="SoftOrange" Width="170px" ToolTip="Al actualizar un cliente existente, el contenido de este campo se ignorara">
                                            </dx:ASPxTextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="right" class="auto-style38">
                                            Confirmar Contraseña:</td>
                                        <td align="left" class="auto-style42"  valign="middle">
                                            <dx:ASPxTextBox ID="txtConfPass" runat="server" Password="True" Theme="SoftOrange" Width="170px" ToolTip="Al actualizar un cliente existente, el contenido de este campo se ignorara">
                                            </dx:ASPxTextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="right" class="auto-style38" >
                                            Nombre:</td>
                                        <td align="left" class="auto-style42">
                                            <dx:ASPxTextBox ID="txtNombre" runat="server" Theme="SoftOrange" Width="170px">
                                            </dx:ASPxTextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="right" class="auto-style38">
                                            Sucursal:</td>
                                        <td align="left" class="auto-style42">
                                            <asp:DropDownList ID="ddlSuc" runat="server" DataSourceID="SqlDataSource3" DataTextField="Sucursal" DataValueField="Id" Height="21px" Width="177px">
                                            </asp:DropDownList>
                                            <asp:SqlDataSource ID="SqlDataSource3" runat="server" ConnectionString="<%$ ConnectionStrings:FinanciaDinConnectionString %>" SelectCommand="SELECT Id, Descripcion AS Sucursal FROM CATSUCURSALES UNION ALL SELECT 0 AS Expr1, '-- Seleccione --' AS Expr2 ORDER BY Id"></asp:SqlDataSource>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="right" class="auto-style38">
                                            Tipo de Acceso:</td>
                                        <td align="left" class="auto-style42">
                                            <asp:DropDownList ID="ddlTipo" runat="server" DataSourceID="SqlDataSource2" DataTextField="Descripcion" DataValueField="id" Height="21px" Width="177px">
                                            </asp:DropDownList>
                                            <asp:SqlDataSource ID="SqlDataSource2" runat="server" ConnectionString="<%$ ConnectionStrings:FinanciaDinConnectionString %>" SelectCommand="SELECT Id, Descripcion FROM [CATTIPOUSUARIOS] UNION ALL SELECT 0, '-- Seleccione --' ORDER BY ID"></asp:SqlDataSource>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="auto-style38" align="right">Estado:</td>
                                        <td align="LEFT">
                                            <asp:CheckBox ID="chkEstado" runat="server" ToolTip="Si el usuario esta o no activo en el sistema" Width="58px" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="auto-style38"></td>
                                        <td class="auto-style42" align="center">
                                            <dx:ASPxButton ID="btnGuardar" runat="server" Text="Guardar" Theme="SoftOrange" AutoPostBack="False">
                                            </dx:ASPxButton>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="auto-style37"></td>
                                        <td class="auto-style35">&nbsp;</td>
                                    </tr>
                                    <tr>
                                        <td class="auto-style38"></td>
                                        <td class="auto-style42"></td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                    </table>
                </dx:PanelContent>
            </PanelCollection>
        </dx:ASPxRoundPanel>
    </center>
    <script type="text/javascript">
        setInterval('MantenSesion()', <%= (Int(0.9 * (Session.Timeout * 60000)))%>);
    </script>
</asp:Content>
