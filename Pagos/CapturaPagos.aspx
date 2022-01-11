<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site.Master"
    CodeBehind="CapturaPagos.aspx.vb" Inherits="FinanciaDin.CapturaPagos" %>

<%@ Register Assembly="DevExpress.Web.v15.2, Version=15.2.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.Web.ASPxPivotGrid.v15.2, Version=15.2.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web.ASPxPivotGrid" TagPrefix="dx" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
    <style type="text/css">
        .auto-style2 {
            height: 58px;
        }

        .auto-style4 {
            width: 239px;
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
                <dx:ASPxRoundPanel ID="ASPxRoundPanel1" runat="server" HeaderText="Captura de Pagos"
                    Theme="DevEx">
                    <PanelCollection>
                        <dx:PanelContent>
                            <table align="center">
                                <tbody>
                                    <tr>
                                        <td align="center" colspan="3"># Pago</td>
                                        <td align="center" colspan="3">ID Crédito</td>
                                        <td align="center" colspan="3">Fecha Deposito</td>
                                    </tr>
                                    <tr>
                                        <td align="center" colspan="3" class="auto-style2">
                                            <dx:ASPxLabel ID="lblPago" runat="server" Width="150px">
                                            </dx:ASPxLabel>
                                        </td>
                                        <td align="center" colspan="3" class="auto-style2">
                                            <dx:ASPxComboBox ID="cbIdCredito" runat="server" AutoPostBack="True" DataSourceID="SqlDataSource1" DropDownRows="8" DropDownStyle="DropDown" TextField="ID" ValueField="ID_CLIENTE">
                                                <ListBoxStyle Wrap="False">
                                                </ListBoxStyle>
                                            </dx:ASPxComboBox>
                                            <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:FinanciaDinConnectionString %>" SelectCommand="SELECT C.ID, C.ID_CLIENTE
FROM CREDITOS AS C 
WHERE C.Liquidado = 0 AND estatusID = 1"></asp:SqlDataSource>
                                        </td>
                                        <td align="center" colspan="3" class="auto-style2">
                                            <dx:ASPxDateEdit ID="deFecDep" runat="server" AutoPostBack="True">
                                            </dx:ASPxDateEdit>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="center" colspan="3">Sucursal</td>
                                        <td align="center" colspan="3">Cliente</td>
                                        <td align="center" colspan="3">Pago Realizado</td>
                                    </tr>
                                    <tr>
                                        <td align="center" colspan="3">

                                            <dx:ASPxComboBox ID="cbSuc" runat="server" DataSourceID="SqlDataSource3" DropDownStyle="DropDown" TextField="Descripcion" ValueField="Id">
                                            </dx:ASPxComboBox>
                                            <asp:SqlDataSource ID="SqlDataSource3" runat="server" ConnectionString="<%$ ConnectionStrings:FinanciaDinConnectionString %>" SelectCommand="SELECT [Id], [Descripcion] FROM [CATSUCURSALES] WHERE [Disponible] = 1"></asp:SqlDataSource>

                                        </td>
                                        <td align="center" colspan="3">

                                            <dx:ASPxComboBox ID="cbClientes" runat="server" AutoPostBack="True" DataSourceID="SqlDataSource2" DropDownStyle="DropDown" TextField="Nombre" ValueField="Id_Cliente" DropDownRows="8">
                                                <ListBoxStyle Wrap="False">
                                                </ListBoxStyle>
                                            </dx:ASPxComboBox>
                                            <asp:SqlDataSource ID="SqlDataSource2" runat="server" ConnectionString="<%$ ConnectionStrings:FinanciaDinConnectionString %>" SelectCommand="SELECT REPLACE(CLI.PrimNombre + ' ' + IsNull(CLI.SegNombre,'') + ' ' + CLI.PrimApellido + ' ' + IsNull(CLI.SegApellido,''), '  ', ' ') AS Nombre,  C.Id_Cliente
FROM CREDITOS AS C INNER JOIN 
CLIENTES AS CLI ON CLI.ID = C.ID_CLIENTE
WHERE C.Liquidado = 0 AND estatusID = 1
ORDER BY Nombre "></asp:SqlDataSource>

                                        </td>
                                        <td align="center" colspan="3" valign="middle">

                                            <dx:ASPxSpinEdit ID="txtMonto" runat="server" Number="0" DisplayFormatString="c" Height="20px" HorizontalAlign="Left" Width="120px" AutoPostBack="True" DecimalPlaces="2">
                                                <SpinButtons ClientVisible="False" Enabled="False">
                                                </SpinButtons>
                                            </dx:ASPxSpinEdit>

                                            <dx:ASPxLabel ID="lblMas" runat="server" Text="+" Visible="False">
                                            </dx:ASPxLabel>
                                            <br />
                                            <dx:ASPxLabel ID="lblTxtBase" runat="server" ForeColor="#33CC33" Text="Base: $" Visible="False">
                                            </dx:ASPxLabel>
                                            <dx:ASPxLabel ID="lblBase" runat="server" ForeColor="#33CC33" Visible="False">
                                            </dx:ASPxLabel>

                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="center" colspan="4" class="auto-style5"><font color="red">
                                            <dx:ASPxButton ID="btnLimpiar" runat="server" CausesValidation="False" Text="Limpiar" UseSubmitBehavior="False" ValidationGroup="Adeudo">
                                                <Paddings PaddingTop="5px" />
                                            </dx:ASPxButton>
                                        </font>
                                        </td>
                                        <td align="center" colspan="2" class="auto-style5"><font color="red"></font>
                                            <dx:ASPxCheckBox ID="cbAbonoCap" runat="server" AutoPostBack="True" CheckState="Unchecked" Text="Es abono capital?">
                                            </dx:ASPxCheckBox>
                                            <dx:ASPxCheckBox ID="cbLiquidaBase" runat="server" AutoPostBack="True" CheckState="Unchecked" Text="Desea liquidar con base?" Visible="False">
                                            </dx:ASPxCheckBox>
                                            <dx:ASPxCheckBox ID="cbLiquida" runat="server" AutoPostBack="True" CheckState="Unchecked" Text="Desea liquidar?" Visible="False">
                                            </dx:ASPxCheckBox>
                                        </td>
                                        <td align="center" colspan="4" class="auto-style5"><font color="red">
                                            <dx:ASPxButton ID="btnGuardar" runat="server" CausesValidation="False" Text="Cobrar" UseSubmitBehavior="False" ValidationGroup="Adeudo">
                                                <Paddings PaddingTop="5px" />
                                            </dx:ASPxButton>
                                        </font></td>
                                    </tr>
                                    <tr>
                                        <td align="center" colspan="5"></td>
                                        <td align="center" colspan="5">
                                            <dx:ASPxLabel ID="lblMotCond" runat="server" Text="Motivo Condonación" Visible="False">
                                            </dx:ASPxLabel>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="center" colspan="5">

                                            <dx:ASPxCheckBox ID="cbCondonacion" runat="server" AutoPostBack="True" CheckState="Unchecked" Text="Condonar Atraso" Visible="False">
                                            </dx:ASPxCheckBox>
                                            <dx:ASPxLabel ID="lblCondRem" runat="server" Font-Size="X-Small" ForeColor="Red" Theme="Default" Visible="False" Wrap="False">
                                            </dx:ASPxLabel>

                                            <ajaxToolkit:ModalPopupExtender ID="ModalPopupExtender" runat="server" TargetControlID="cbCondonacion"
                                                PopupControlID="Panel1" BackgroundCssClass="modalBackground"
                                                CancelControlID="btnCancelar" DropShadow="true" />

                                        </td>
                                        <td align="center" colspan="5">

                                            <textarea id="txtMotCond" name="S1" rows="2" runat="server" visible="false" class="auto-style4" disabled="disabled"></textarea>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="center" colspan="2">

                                            <br />
                                            <br />

                                            <dx:ASPxLabel ID="ASPxLabel1" runat="server" Font-Bold="False" Font-Size="Medium" Text="Abono Prestamo: ">
                                            </dx:ASPxLabel>

                                            <br />
                                            <br />

                                        </td>
                                        <td align="center" valign="bottom" colspan="2">

                                            <dx:ASPxLabel ID="lblMas2" runat="server" Text="+" Visible="False">
                                            </dx:ASPxLabel>

                                        </td>
                                        <td align="center" colspan="2">
                                            <dx:ASPxLabel ID="lblTxtRecargo" runat="server" Font-Size="Medium" ForeColor="Red" Text="Recargo: " Visible="False">
                                            </dx:ASPxLabel>
                                        </td>
                                        <td align="center" valign="bottom" colspan="2"><dx:ASPxLabel ID="lblIgual" runat="server" Text="=">
                                            </dx:ASPxLabel>
                                        </td>
                                        <td align="center" colspan="2">
                                            <dx:ASPxLabel ID="lblTotal" runat="server" Font-Bold="False" Font-Size="Medium" Text="Total: ">
                                            </dx:ASPxLabel>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="center" colspan="2">



                                            <dx:ASPxLabel ID="ASPxLabel2" runat="server" Font-Bold="True" Font-Size="Medium" Text="$ ">
                                            </dx:ASPxLabel>



                                            <dx:ASPxLabel ID="lblACredito" runat="server" Font-Bold="True" Font-Size="Medium" Text="">
                                            </dx:ASPxLabel>



                                        </td>
                                        <td align="center" colspan="2"></td>
                                        <td align="center" colspan="2">
                                            <dx:ASPxLabel ID="lblRecargoSig" runat="server" Font-Bold="True" Font-Size="Medium" ForeColor="Red" Text="$ " Visible="False">
                                            </dx:ASPxLabel>
                                            <dx:ASPxLabel ID="lblRecargo" runat="server" Font-Size="Medium" ForeColor="Red" Theme="Default" Visible="False">
                                            </dx:ASPxLabel>
                                        </td>
                                        <td align="center" colspan="2"></td>
                                        <td align="center" colspan="2">
                                            <dx:ASPxLabel ID="ASPxLabel3" runat="server" Font-Bold="True" Font-Size="Medium" Text="$ ">
                                            </dx:ASPxLabel>
                                            <dx:ASPxLabel ID="lblSuma" runat="server" Font-Bold="True" Font-Size="Medium">
                                            </dx:ASPxLabel>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="center" colspan="10">
                                            <br />
                                            <br />
                                            <dx:ASPxGridView KeyFieldName="id" ID="gvPagos" runat="server" Caption="Tabla de Amortización" EnableTheming="True" Theme="Default" Visible="False" EnableCallBacks="False">
                                                <SettingsPager AlwaysShowPager="True" Mode="ShowAllRecords">
                                                </SettingsPager>
                                                <SettingsBehavior AllowSelectByRowClick="True" AllowSelectSingleRowOnly="True" ProcessSelectionChangedOnServer="True" />
                                                <Styles>
                                                    <HeaderPanel BackColor="#666666">
                                                    </HeaderPanel>
                                                </Styles>
                                            </dx:ASPxGridView>
                                        </td>
                                    </tr>
                                    <tr align="center" valign="middle">
                                        <td align="center" colspan="10">
                                            <dx:ASPxButton ID="btnReImpTckt" runat="server" Text="Re-Imprimir Ticket"></dx:ASPxButton>
                                        </td>
                                    </tr>
                                </tbody>
                            </table>
                        </dx:PanelContent>
                    </PanelCollection>
                </dx:ASPxRoundPanel>
<%--            </ContentTemplate>
        </asp:UpdatePanel>--%>
    </center>
    <script type="text/javascript">
        setInterval('MantenSesion()', <%= (Int(0.9 * (Session.Timeout * 60000)))%>);
    </script>
</asp:Content>
