<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site.Master"
    CodeBehind="CondonacionesRemotas.aspx.vb" Inherits="FinanciaDin.CondonacionesRemotas" %>

<%@ Register Assembly="DevExpress.Web.v15.2, Version=15.2.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.Web.ASPxPivotGrid.v15.2, Version=15.2.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web.ASPxPivotGrid" TagPrefix="dx" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
    <style type="text/css">
        .auto-style2 {
            height: 58px;
        }

        .modalBackground {
            background-color: Gray;
            filter: alpha(opacity=70);
            opacity: 0.7;
        }

        .modalPopup {
            background-color: #ffffdd;
            border-width: 3px;
            border-style: solid;
            border-color: Gray;
            padding: 3px;
            width: 250px;
        }

        .popupControl {
            background-color: White;
            position: absolute;
            visibility: hidden;
        }

        .auto-style5 {
            height: 31px;
        }
        .auto-style6 {
            height: 50px;
        }
        </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <br />
    <br />
    <center>
        <%--<asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>--%>
                <asp:Panel ID="Panel1" runat="server" Style="display: none" CssClass="modalPopup">
                    <asp:Panel ID="Panel3" runat="server" Style="cursor: move; background-color: #DDDDDD; border: solid 1px Gray; color: Black">
                        <div>
                            <p>
                                Acceda con un usuario de administrador para continuar:
                            </p>
                        </div>
                    </asp:Panel>
                    <div>
                        <p>
                            Usuario:
                        </p>
                        <p>
                            <dx:ASPxTextBox ID="txtLogin" runat="server" Width="170px"></dx:ASPxTextBox>
                        </p>
                        <p>
                            Contraseña:
                        </p>
                        <p>
                            <input id="txtContra" type="password" runat="server" style="width: 170px; height: 18px" />
                        </p>
                        <p style="text-align: center;">
                            <table align="center">
                                <tr>
                                    <td>
                                        <dx:ASPxButton ID="btnCancelar" runat="server" CausesValidation="False" Text="Cancelar" UseSubmitBehavior="False" ValidationGroup="Adeudo">
                                            <Paddings PaddingTop="5px" />
                                        </dx:ASPxButton>
                                    </td>
                                    <td></td>
                                    <td></td>
                                    <td></td>
                                    <td>
                                        <dx:ASPxButton ID="btnIngresar" runat="server" CausesValidation="False" Text="Ingresar" UseSubmitBehavior="False" ValidationGroup="Adeudo">
                                            <Paddings PaddingTop="5px" />
                                        </dx:ASPxButton>
                                    </td>
                                </tr>
                            </table>
                        </p>
                    </div>
                </asp:Panel>

                <asp:ScriptManager ID="ScriptManager1" runat="server">
                </asp:ScriptManager>
                <dx:ASPxRoundPanel ID="ASPxRoundPanel1" runat="server" HeaderText="Condonaciones Remotas" HeaderStyle-HorizontalAlign="Center"
                    Theme="DevEx">
<HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                    <PanelCollection>
                        <dx:PanelContent>
                            <table align="center">
                                <tbody>
                                    <tr>
                                        <td align="center" colspan="5">Monto a Condonar</td>
                                        <td align="center" colspan="4">Cliente</td>
                                        <%--<td align="center" colspan="3">&nbsp;</td>--%>
                                    </tr>
                                    <tr>
                                        <td align="center" colspan="5" class="auto-style2">
                                            <dx:ASPxSpinEdit ID="txtMonto" runat="server" Height="20px" HorizontalAlign="Left" MaxValue="10000000" NumberType="Integer" Width="168px" MinValue="1">
                                                <SpinButtons ClientVisible="False" Enabled="False">
                                                </SpinButtons>
                                            </dx:ASPxSpinEdit>
                                        </td>
                                        <td align="center" colspan="4" class="auto-style2">
                                            <dx:ASPxComboBox ID="cbCreditos" runat="server" DataSourceID="SqlDataSource5" TextField="Cliente" ValueField="Id">
                                            </dx:ASPxComboBox>
                                            <asp:SqlDataSource ID="SqlDataSource5" runat="server" ConnectionString="<%$ ConnectionStrings:FinanciaDinConnectionString %>" SelectCommand="SELECT C.Id, REPLACE(CLI.PrimNombre + ' ' + CLI.SegNombre 
                                           + ' ' + CLI.PrimApellido + ' ' + CLI.SegApellido, '  ', ' ') AS Cliente 
                                           FROM CREDITOS AS C INNER JOIN 
                                                CLIENTES AS CLI ON C.Id_Cliente = CLI.Id 
                                                WHERE (C.Liquidado = 0) AND (C.Id NOT IN (SELECT Id_Credito FROM CONDONACIONESREMOTAS WHERE Aplicado = 0 AND Cancelado = 0))
ORDER BY CLIENTE"></asp:SqlDataSource>
                                        </td>
                                        <%--<td align="center" colspan="3" class="auto-style2">
                                            &nbsp;</td>--%>
                                    </tr>                                 
                                    <tr>
                                        <td align="center" colspan="4" class="auto-style5"><font color="red">
                                            <dx:ASPxButton ID="btnLimpiar" runat="server" CausesValidation="False" Text="Limpiar" UseSubmitBehavior="False" ValidationGroup="Adeudo">
                                                <Paddings PaddingTop="5px" />
                                            </dx:ASPxButton>
                                        </font>
                                        </td>
                                        <td align="center" colspan="2" class="auto-style5"><font color="red"></font>
                                        </td>
                                        <td align="center" colspan="4" class="auto-style5"><font color="red">
                                            <dx:ASPxButton ID="btnGuardar" runat="server" CausesValidation="False" Text="Guardar" UseSubmitBehavior="False" ValidationGroup="Adeudo">
                                                <Paddings PaddingTop="5px" />
                                            </dx:ASPxButton>
                                        </font></td>
                                    </tr>
                                    <tr>

                                        <td align="center" colspan="10">
                                            <div id="divTabla" runat="server">
                                                <br />
                                                <asp:SqlDataSource ID="SqlDataSource4" runat="server" ConnectionString="<%$ ConnectionStrings:FinanciaDinConnectionString %>" SelectCommand="SELECT CR.Id, CR.Id_Credito, REPLACE(CLI.PrimNombre + ' ' + CLI.SegNombre + ' ' + CLI.PrimApellido + ' ' + CLI.SegApellido, '  ', ' ') AS Cliente, CR.Monto, CR.Login, CR.FecCaptura FROM CONDONACIONESREMOTAS AS CR INNER JOIN CREDITOS AS C ON CR.Id_Credito = C.Id INNER JOIN CLIENTES AS CLI ON C.Id_Cliente = CLI.Id WHERE (CR.Cancelado = 0) AND (CR.Aplicado = 0)"></asp:SqlDataSource>
                                                <asp:SqlDataSource ID="SqlDataSource6" runat="server" ConnectionString="<%$ ConnectionStrings:FinanciaDinConnectionString %>" SelectCommand="SELECT C.Id AS Id_Credito, REPLACE(CLI.PrimNombre + ' ' + CLI.SegNombre 
                                           + ' ' + CLI.PrimApellido + ' ' + CLI.SegApellido, '  ', ' ') AS Cliente 
                                           FROM CREDITOS AS C INNER JOIN 
                                                CLIENTES AS CLI ON C.Id_Cliente = CLI.Id 
                                                WHERE (C.Liquidado = 0) AND (C.Id NOT IN (SELECT Id_Credito FROM CONDONACIONESREMOTAS  WHERE Aplicado = 0 AND Cancelado = 0))
ORDER BY CLIENTE"></asp:SqlDataSource>
                                                <br />
                                                <dx:ASPxGridView ID="gvCondRem" runat="server" Caption="Condonaciones Remotas" EnableTheming="True" Theme="Default" EnableCallBacks="False" AutoGenerateColumns="False" DataSourceID="SqlDataSource4" KeyFieldName="Id" DeleteText="Cancelar">
                                                    <SettingsPager AlwaysShowPager="True" Mode="ShowAllRecords">
                                                    </SettingsPager>
                                                    <SettingsBehavior AllowSelectByRowClick="True" AllowSelectSingleRowOnly="True" ProcessSelectionChangedOnServer="True" ConfirmDelete="True" />
                                                    <Columns>
                                                        <dx:GridViewCommandColumn ShowDeleteButton="True" ShowEditButton="True" ShowInCustomizationForm="True" VisibleIndex="0">
                                                        </dx:GridViewCommandColumn>
                                                        <dx:GridViewDataTextColumn FieldName="Id" ShowInCustomizationForm="False" VisibleIndex="1" ReadOnly="True" Visible="False">
                                                            <EditFormSettings Visible="False" />
                                                        </dx:GridViewDataTextColumn>
                                                        <dx:GridViewDataTextColumn FieldName="Id_Credito" ShowInCustomizationForm="True" VisibleIndex="2">
                                                            <EditItemTemplate>
                                                                <dx:ASPxComboBox ID="cbCredito" runat="server" DataSourceID="SqlDataSource6" TextField="Cliente" ValueField="Id_Credito"/>
                                                            </EditItemTemplate>
                                                        </dx:GridViewDataTextColumn>
                                                        <dx:GridViewDataTextColumn FieldName="Cliente" ShowInCustomizationForm="False" VisibleIndex="3" ReadOnly="True">
                                                            <EditFormSettings Visible="False" />
                                                        </dx:GridViewDataTextColumn>
                                                        <dx:GridViewDataTextColumn FieldName="Monto" ShowInCustomizationForm="True" VisibleIndex="4">
                                                        </dx:GridViewDataTextColumn>
                                                        <dx:GridViewDataTextColumn FieldName="Login" ShowInCustomizationForm="False" VisibleIndex="5">
                                                            <EditFormSettings Visible="False" />
                                                        </dx:GridViewDataTextColumn>
                                                        <dx:GridViewDataDateColumn FieldName="FecCaptura" ShowInCustomizationForm="False" VisibleIndex="6" Caption="Fecha Captura">
                                                            <EditFormSettings Visible="False" />
                                                        </dx:GridViewDataDateColumn>
                                                    </Columns>
                                                    <Styles>
                                                        <HeaderPanel BackColor="#666666">
                                                        </HeaderPanel>
                                                    </Styles>
                                                </dx:ASPxGridView>

                                            </div>
                                        </td>

                                    </tr>
                                    <tr align="center" valign="middle">
                                        <td align="center" colspan="5" class="auto-style6">
                                            &nbsp;</td>
                                        <td colspan="5">
                                            &nbsp;</td>
                                    </tr>
                                </tbody>
                            </table>
                        </dx:PanelContent>
                    </PanelCollection>
                </dx:ASPxRoundPanel>
        <%--<td align="center" colspan="3">&nbsp;</td>--%>
    </center>
    <script type="text/javascript">
        setInterval('MantenSesion()', <%= (Int(0.9 * (Session.Timeout * 600000)))%>);
    </script>
</asp:Content>
