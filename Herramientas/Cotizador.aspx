<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site.Master"
    CodeBehind="Cotizador.aspx.vb" Inherits="FinanciaDin.Cotizador" %>

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
                <dx:ASPxRoundPanel ID="ASPxRoundPanel1" runat="server" HeaderText="Cotizador" HeaderStyle-HorizontalAlign="Center"
                    Theme="DevEx">
<HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                    <PanelCollection>
                        <dx:PanelContent>
                            <table align="center">
                                <tbody>
                                    <tr>
                                        <td align="center" colspan="3">Tipo de Amortización</td>
                                        <td align="center" colspan="3">Monto a Prestar</td>
                                        <td align="center" colspan="3">Fecha de Inicio</td>    
                                    </tr>
                                    <tr>
                                        <td align="center" colspan="3" class="auto-style2">
                                            <dx:ASPxComboBox ID="cbAmort" runat="server" DataSourceID="SqlDataSource4" DropDownStyle="DropDown" TextField="Nombre" ValueField="Id" DropDownRows="8" Width="125px" Height="21px">
                                                <ListBoxStyle Wrap="False">
                                                </ListBoxStyle>
                                            </dx:ASPxComboBox>
                                            <asp:SqlDataSource ID="SqlDataSource4" runat="server" ConnectionString="<%$ ConnectionStrings:FinanciaDinConnectionString %>" SelectCommand="SELECT [Id], [Nombre] FROM [CATTIPAMORT] ORDER BY [Nombre]"></asp:SqlDataSource>
                                        </td>
                                        <td align="center" colspan="3" class="auto-style2">
                                            <dx:ASPxSpinEdit ID="txtMonto" runat="server" Height="20px" HorizontalAlign="Left" MaxValue="10000000" NumberType="Integer" Width="120px" MinValue="1">
                                                <SpinButtons ClientVisible="False" Enabled="False">
                                                </SpinButtons>
                                            </dx:ASPxSpinEdit>
                                        </td>
                                        <td align="center" colspan="3" class="auto-style2">
                                            <dx:ASPxDateEdit ID="deFecha" runat="server">
                                            </dx:ASPxDateEdit>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="center" colspan="3">Tasa de Interés</td>
                                        <td align="center" colspan="3">Plazo</td>
                                        <td align="center" colspan="3">Número de Pagos</td>
                                    </tr>
                                    <tr>
                                        <td align="center" colspan="3">

                                            <dx:ASPxComboBox ID="cbTasa" runat="server" DataSourceID="SqlDataSource3" DropDownStyle="DropDown" TextField="Descripcion" ValueField="Valor" Width="150px">
                                            </dx:ASPxComboBox>
                                            <asp:SqlDataSource ID="SqlDataSource3" runat="server" ConnectionString="<%$ ConnectionStrings:FinanciaDinConnectionString %>" SelectCommand="SELECT [Descripcion], [Valor] FROM [CATTASAINTERES] ORDER BY [Valor]"></asp:SqlDataSource>

                                        </td>
                                        <td align="center" colspan="3">

                                            <dx:ASPxComboBox ID="cbPlazo" runat="server" DataSourceID="SqlDataSource2" DropDownStyle="DropDown" TextField="Descripcion" ValueField="Id" DropDownRows="8" Width="125px" Height="21px">
                                                <ListBoxStyle Wrap="False">
                                                </ListBoxStyle>
                                            </dx:ASPxComboBox>
                                            <asp:SqlDataSource ID="SqlDataSource2" runat="server" ConnectionString="<%$ ConnectionStrings:FinanciaDinConnectionString %>" SelectCommand="SELECT [Descripcion], [Id] FROM [CATPLAZOS]"></asp:SqlDataSource>

                                        </td>
                                        <td align="center" colspan="3" valign="middle">

                                            <dx:ASPxSpinEdit ID="txtNumPagos" runat="server" Height="20px" HorizontalAlign="Left" Width="120px" MaxValue="200" NumberType="Integer" MinValue="1">
                                                <SpinButtons ClientVisible="False" Enabled="False">
                                                </SpinButtons>
                                            </dx:ASPxSpinEdit>

                                            <br />

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
                                        </td>
                                        <td align="center" colspan="4" class="auto-style5"><font color="red">
                                            <dx:ASPxButton ID="btnCalcular" runat="server" CausesValidation="False" Text="Calcular" UseSubmitBehavior="False" ValidationGroup="Adeudo">
                                                <Paddings PaddingTop="5px" />
                                            </dx:ASPxButton>
                                        </font></td>
                                    </tr>
                                    <tr>
                                        <td align="center" colspan="5">
                                            <br />
                                            <dx:ASPxLabel ID="ASPxLabel1" runat="server" Font-Bold="False" Font-Size="Medium" Text="Prestamo: ">
                                            </dx:ASPxLabel>
                                        </td>
                                        <td align="center" colspan="5">
                                            <br />
                                            <dx:ASPxLabel ID="lblTotal" runat="server" Font-Bold="False" Font-Size="Medium" Text="Adeudo: ">
                                            </dx:ASPxLabel>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="center" colspan="5">



                                            <dx:ASPxLabel ID="ASPxLabel2" runat="server" Font-Bold="True" Font-Size="Medium" Text="$ ">
                                            </dx:ASPxLabel>



                                            <dx:ASPxLabel ID="lblPrestamo" runat="server" Font-Bold="True" Font-Size="Medium" Text="">
                                            </dx:ASPxLabel>



                                        </td>
                                        <td align="center" colspan="5">
                                            <dx:ASPxLabel ID="ASPxLabel3" runat="server" Font-Bold="True" Font-Size="Medium" Text="$ ">
                                            </dx:ASPxLabel>
                                            <dx:ASPxLabel ID="lblAdeudo" runat="server" Font-Bold="True" Font-Size="Medium">
                                            </dx:ASPxLabel>
                                        </td>
                                    </tr>
                                    <tr>

                                        <td align="center" colspan="10">
                                            <div id="divTabla" runat="server">
                                                <br />
                                                <br />
                                                <dx:ASPxGridView KeyFieldName="id" ID="gvPagos" runat="server" Caption="Tabla de Amortización" EnableTheming="True" Theme="Default" EnableCallBacks="False">
                                                    <SettingsPager AlwaysShowPager="True" Mode="ShowAllRecords">
                                                    </SettingsPager>
                                                    <SettingsBehavior AllowSelectByRowClick="True" AllowSelectSingleRowOnly="True" ProcessSelectionChangedOnServer="True" />
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
