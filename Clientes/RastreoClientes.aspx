<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site.Master"
    CodeBehind="RastreoClientes.aspx.vb" Inherits="FinanciaDin.RastreoClientes" %>

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
        <dx:ASPxRoundPanel ID="ASPxRoundPanel1" runat="server" HeaderText="Rastreo de Clientes"
            Theme="DevEx">
            <PanelCollection>
                <dx:PanelContent>
                    <table align="center">
                        <tbody>
                            <tr>
                                <td align="center" colspan="6">Cliente</td>
                            </tr>
                            <tr>
                                <td align="center" colspan="6">
                                    <dx:ASPxComboBox ID="cbClientes" runat="server" AutoPostBack="True" DataSourceID="SqlDataSource1" DropDownStyle="DropDown" TextField="Cliente" ValueField="Id">
                                    </dx:ASPxComboBox>
                                    <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:FinanciaDinConnectionString %>" SelectCommand="SELECT [Id], REPLACE(PrimNombre + ' ' + IsNull(SegNombre,'') + ' ' + PrimApellido + ' ' + IsNull(SegApellido,''), '  ', ' ') AS Cliente FROM [CLIENTES]"></asp:SqlDataSource>
                                </td>
                            </tr>
                            <tr>
                                <td  align="center" colspan="6" class="auto-style3"><font color="red">
                                    <dx:ASPxButton ID="ASPxButton1" runat="server" CausesValidation="False" Text="Limpiar" UseSubmitBehavior="False" ValidationGroup="Adeudo">
                                        <Paddings PaddingTop="5px" />
                                    </dx:ASPxButton>
                                    </font>
                                </td>
                            </tr>
                            <tr>
                                <td align="center" colspan="8">
                                    <br />
                                    <br />
                                    <dx:ASPxGridView ID="gvHistorial" runat="server" Visible="False" KeyFieldName="id">
                                        <SettingsPager Mode="ShowAllRecords">
                                        </SettingsPager>
                                        <SettingsBehavior AllowSelectByRowClick="True" AllowSelectSingleRowOnly="True" ProcessSelectionChangedOnServer="True" />
                                    </dx:ASPxGridView>
                                </td>
                            </tr>
                            <tr>
                                <td align="center" colspan="3">
                                    <br />
                                </td>
                                <td align="center" colspan="3">
                                    <br />
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
