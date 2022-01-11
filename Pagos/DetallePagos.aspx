<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site.Master"
    CodeBehind="DetallePagos.aspx.vb" Inherits="FinanciaDin.DetallePagos" %>

<%@ Register Assembly="DevExpress.Web.v15.2, Version=15.2.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web" TagPrefix="dx" %>
<%@ Register assembly="DevExpress.Web.ASPxPivotGrid.v15.2, Version=15.2.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.Web.ASPxPivotGrid" tagprefix="dx" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
    <style type="text/css">
        .auto-style3 {
            height: 45px;
        }
    </style>
    </asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <br />
    <br />
    <center>
        <dx:ASPxRoundPanel ID="ASPxRoundPanel1" runat="server" HeaderText="Detalle de Pagos"
            Theme="DevEx">
            <PanelCollection>
                <dx:PanelContent>
                    <table align="center">
                        <tbody>
                            <tr>
                                <td align="center" colspan="3">Crédito</td>
                                <td align="center" colspan="3">Cliente</td>
                            </tr>
                            <tr>
                                <td align="center" colspan="3">
                                    <dx:ASPxComboBox ID="cbCreditos" runat="server" AutoPostBack="True" DataSourceID="SqlDataSource1" DropDownStyle="DropDown" TextField="Id" ValueField="Id">
                                    </dx:ASPxComboBox>
                                    <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:FinanciaDinConnectionString %>" SelectCommand="SELECT [Id] FROM [CREDITOS]"></asp:SqlDataSource>
                                </td>
                                <td align="center" colspan="3">
                                    <dx:ASPxComboBox ID="cbClientes" runat="server" DropDownStyle="DropDown" AutoPostBack="True" DataSourceID="SqlDataSource2" TextField="Nombre" ValueField="Id">
                                    </dx:ASPxComboBox>
                                    <asp:SqlDataSource ID="SqlDataSource2" runat="server" ConnectionString="<%$ ConnectionStrings:FinanciaDinConnectionString %>" SelectCommand="SELECT REPLACE(CLI.PrimNombre + ' ' + IsNull(CLI.SegNombre,'') + ' ' + CLI.PrimApellido + ' ' + IsNull(CLI.SegApellido,''), '  ', ' ') AS Nombre,  C.Id
FROM CREDITOS AS C INNER JOIN 
CLIENTES AS CLI ON CLI.ID = C.ID_CLIENTE
ORDER BY Nombre 
"></asp:SqlDataSource>
                                </td>
                            </tr>
                            <tr>
                                <td  align="center" colspan="3" class="auto-style3"><font color="red">
                                    <dx:ASPxButton ID="ASPxButton1" runat="server" CausesValidation="False" Text="Limpiar" UseSubmitBehavior="False" ValidationGroup="Adeudo">
                                        <Paddings PaddingTop="5px" />
                                    </dx:ASPxButton>
                                    </font>
                                </td>
                                <td align="center" colspan="3" class="auto-style3"><font color="red">
                                    <dx:ASPxButton ID="ASPxButton4" runat="server" CausesValidation="False" Text="Mostrar Detalle" UseSubmitBehavior="False" ValidationGroup="Adeudo">
                                        <Paddings PaddingTop="5px" />
                                    </dx:ASPxButton>
                                    </font></td>
                            </tr>
                            <tr>
                                <td align="center" colspan="8">
                                    <br />
                                    <dx:ASPxLabel ID="ASPxLabel1" runat="server" Font-Bold="True" Font-Size="Medium" Text="Adeudo Total: $">
                                    </dx:ASPxLabel>
                                    <dx:ASPxLabel ID="lblAdeudo" runat="server" Font-Bold="True" Font-Size="Medium">
                                    </dx:ASPxLabel>
                                    <br />
                                    <br />
                                    <dx:ASPxLabel ID="ASPxLabel2" runat="server" ForeColor="#FF3300" Text="Recargo: $">
                                    </dx:ASPxLabel>
                                    <dx:ASPxLabel ID="lblRecargo" runat="server" ForeColor="#FF3300">
                                    </dx:ASPxLabel>
                                    <br />
                                    <br />
                                    <dx:ASPxGridView ID="gvPagos" runat="server" Visible="False" KeyFieldName="id">
                                        <SettingsPager Mode="ShowAllRecords">
                                        </SettingsPager>
                                        <SettingsBehavior AllowSelectByRowClick="True" AllowSelectSingleRowOnly="True" ProcessSelectionChangedOnServer="True" />
                                    </dx:ASPxGridView>
                                </td>
                            </tr>
                            <tr>
                                <td align="center" colspan="3">
                                    <br />
                                    <dx:ASPxButton ID="btnReImpTckt" runat="server" Text="Re-Imprimir Ticket"></dx:ASPxButton>
                                </td>
                                <td align="center" colspan="3">
                                    <br />
                                    <dx:ASPxButton ID="btnTablaAmort" runat="server" Text="Imprimir Plan de Pagos"></dx:ASPxButton>
                                </td>
                            </tr>
                        </tbody>
                    </table>
                </dx:PanelContent>
            </PanelCollection>
        </dx:ASPxRoundPanel>
    </center>
    <script type="text/javascript">
        setInterval('MantenSesion()', <%= (Int(0.9 * (Session.Timeout * 60000)))%>);
    </script>
</asp:Content>
