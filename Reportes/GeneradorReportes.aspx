<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site.Master"
    CodeBehind="GeneradorReportes.aspx.vb" Inherits="FinanciaDin.GeneradorReportes" %>

<%@ Register Assembly="DevExpress.Web.v15.2, Version=15.2.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web" TagPrefix="dx" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
    <style type="text/css">
        .auto-style2 {
            margin-left: 0px;
        }
        .auto-style3 {
            height: 23px;
        }
        </style>
    </asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <br />
    <br />
    <center>
        <dx:ASPxRoundPanel ID="ASPxRoundPanel1" runat="server" HeaderText="Generador de Reportes"
            Theme="DevEx">
            <PanelCollection>
                <dx:PanelContent>
                    <table width="100%">
                        <tr>
                            <td colspan="3" align="center">
                                Seleccione un Reporte:</td>
                        </tr>
                        <tr>
                            <td colspan="3" align="center" class="auto-style3">
                                <asp:DropDownList ID="ddlReportes" runat="server" CssClass="auto-style2" Height="20px" Width="154px" AutoPostBack="True">
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="3"></td>
                        </tr>
                        <tr>
                            <td colspan="3">
                                <div id="divFechas" align="center" visible="false" runat="server">
                                    <table width="100%">
                                        <tr>
                                            <td align="center">
                                                &nbsp;Desde:&nbsp;&nbsp;&nbsp;&nbsp;
                                            </td>
                                            <td align="center">

                                                <dx:ASPxDateEdit ID="deDesde" CssClass="form-control" runat="server" >
                                                    <ClearButton DisplayMode="Always" ToolTip="Limpiar">
                                                    </ClearButton>
                                                </dx:ASPxDateEdit>

                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="center">
                                                Hasta:&nbsp;&nbsp;&nbsp;
                                            </td>
                                            <td align="center">
                                                
                                                <dx:ASPxDateEdit CssClass="form-control" ID="deHasta" runat="server" >
                                                    <ClearButton ToolTip="Limpiar" DisplayMode="Always">
                                                    </ClearButton>
                                                </dx:ASPxDateEdit>
                                            </td>
                                        </tr>
                                    </table>
                                </div>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="3">
                                <div id="divSuc"  align="center" visible="false" runat="server">
                                    <table width="100%">
                                        <tr>
                                            <td align="center">
                                                Sucursal:&nbsp;&nbsp;&nbsp;
                                            </td>
                                            <td align="center">
                                                <div class="form-group">
                                                <dx:ASPxComboBox ID="cbSuc" runat="server" CssClass="form-control" DataSourceID="SqlDataSource3" TextField="Descripcion" ValueField="Id" >
                                                </dx:ASPxComboBox>
                                                <asp:SqlDataSource ID="SqlDataSource3" runat="server"  ConnectionString="<%$ ConnectionStrings:FinanciaDinConnectionString %>" SelectCommand="SELECT [Id], [Descripcion]
FROM [CATSUCURSALES]
"></asp:SqlDataSource>
</div>
                                            </td>
                                        </tr>
                                    </table>
                                </div>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="3">
                                <div id="divLogin" align="center" visible="false" runat="server">
                                    <table width="100%">
                                        <tr>
                                            <%--<td align="center">
                                                Login:&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                            </td>--%>
                                            <td align="center" colspan="2">

                                                <dx:ASPxListBox ID="lbLogin" runat="server" Caption="Login" DataSourceID="SqlDataSource6" TextField="Nombre" ValueField="Nombre"
                                                    SelectionMode="CheckColumn" CaptionSettings-VerticalAlign="Middle" Width="225px" Height="100px" Theme="Office2010Silver">
                                                    <ItemStyle Font-Size="Smaller" />

<CaptionSettings VerticalAlign="Middle"></CaptionSettings>
                                                </dx:ASPxListBox>

                                                <asp:SqlDataSource ID="SqlDataSource6" runat="server" ConnectionString="<%$ ConnectionStrings:FinanciaDinConnectionString %>" SelectCommand="SELECT DISTINCT [USUARIOS].[Login], [USUARIOS].[Nombre] 
FROM [USUARIOS] INNER JOIN 
[PAGOS] ON [USUARIOS].[Login] = [PAGOS].[Login]
ORDER BY [USUARIOS].[NOMBRE]"></asp:SqlDataSource>

                                            </td>
                                        </tr>
                                    </table>
                                </div>
                            </td>
                        </tr>

                        <tr>
                            <td colspan="3">
                                <div id="divEstBases" align="center" visible="false" runat="server">
                                    <table width="100%">
                                        <tr>
                                            <%--<td align="center">
                                                Login:&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                            </td>--%>
                                            <td align="center" colspan="2">

                                                <asp:DropDownList ID="ddlEstatusBases" runat="server" CssClass="auto-style2" Height="20px" Width="154px" AutoPostBack="True">
                                </asp:DropDownList>


                                            </td>
                                        </tr>
                                    </table>
                                </div>
                            </td>
                        </tr>

                    </table>
                    <br />
                    <br />
                    <div align="center">
                        <dx:ASPxButton ID="btnGenRep" runat="server" Text="Generar Reporte">
                        </dx:ASPxButton>
                    </div>
                </dx:PanelContent>
            </PanelCollection>
        </dx:ASPxRoundPanel>
    </center>
    <script type="text/javascript">
        setInterval('MantenSesion()', <%= (Int(0.9 * (Session.Timeout * 60000)))%>);
    </script>
</asp:Content>
