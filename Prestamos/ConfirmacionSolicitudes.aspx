<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site.Master"
    CodeBehind="ConfirmacionSolicitudes.aspx.vb" Inherits="FinanciaDin.ConfirmacionSolicitudes" %>

<%@ Register Assembly="DevExpress.Web.v15.2, Version=15.2.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web" TagPrefix="dx" %>
<%@ Register assembly="DevExpress.Web.ASPxPivotGrid.v15.2, Version=15.2.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.Web.ASPxPivotGrid" tagprefix="dx" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
    <style type="text/css">
        .auto-style2 {
            height: 25px;
        }
        .auto-style3 {
            height: 17px;
        }
    </style>
    </asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <br />
    <br />
    <center>
        <asp:ScriptManager runat="server" ID="ScriptManager1"></asp:ScriptManager>

        <dx:ASPxRoundpanel id="ASPxRoundPanel1" runat="server" headertext="Confirmación de Solicitudes"
            theme="DevEx">
            <PanelCollection>
                <dx:PanelContent>
                    <table align="center">
                                <tbody>
                                    <tr>
                                        <td align="center">ID </td>
                                        <td align="center">Fecha Autorización</td>
                                        <td align="center">Fecha de Disposición</td>
                                        <td align="center">Monto Autorizado</td>
                                        <td align="center">Frecuencia de Pagos</td>
                                    </tr>
                                    <tr>
                                        <td align="center" class="auto-style2">
                                            <asp:Label ID="lblId" runat="server">--</asp:Label>
                                        </td>
                                        <td align="center" class="auto-style2">
                                            <dx:ASPxDateEdit ID="deFecAut" runat="server">
                                            </dx:ASPxDateEdit>
                                        </td>
                                        <td align="center" class="auto-style2">
                                            <dx:ASPxDateEdit ID="deFecDisp" runat="server">
                                            </dx:ASPxDateEdit>
                                        </td>
                                        <td align="center" class="auto-style2">
                                            <font color="red">
                                            <dx:ASPxSpinEdit ID="txtMontoAut" runat="server" DecimalPlaces="2" MaxValue="2147483647" MinValue="1" NumberType="Integer">
                                                <spinbuttons clientvisible="False" enabled="False">
                                                </spinbuttons>
                                            </dx:ASPxSpinEdit>
                                            </font>
                                        </td>
                                        <td align="center" class="auto-style2">
                                            <dx:ASPxComboBox ID="cbPlazoAut" runat="server" DataSourceID="SqlDataSource2" DropDownStyle="DropDown" TextField="Descripcion" ValueField="Id">
                                            </dx:ASPxComboBox>
                                            <asp:SqlDataSource ID="SqlDataSource2" runat="server" ConnectionString="<%$ ConnectionStrings:FinanciaDinConnectionString %>" SelectCommand="SELECT [Id], [Descripcion] FROM [CATPLAZOS]"></asp:SqlDataSource>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="center" class="auto-style3">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; </td>
                                        <td align="center" class="auto-style3">Duración en Meses</td>
                                        <td align="center" class="auto-style3">Pagos Autorizados</td>
                                        <td align="center" class="auto-style3">Monto Desembolsado</td>
                                        <td align="center" class="auto-style3">Llave Retiro</td>
                                        <td align="center" class="auto-style3">&nbsp;</td>
                                    </tr>
                                    <tr>
                                        <td align="center" valign="top">
                                            &nbsp;</td>
                                        <td align="center" style="margin-left: 40px" valign="top">
                                            <font color="red">
                                            <dx:ASPxSpinEdit ID="seDurMeses" runat="server" AutoPostBack="True" MaxValue="24" MinValue="1" NumberType="Integer">
                                            </dx:ASPxSpinEdit>
                                            </font>
                                        </td>
                                        <td align="center" valign="top">
                                            <font color="red">
                                            <dx:ASPxSpinEdit ID="sePagosAut" runat="server" Enabled="False" MaxValue="24" MinValue="1" NumberType="Integer">
                                                <SpinButtons ClientVisible="False" Enabled="False">
                                                </SpinButtons>
                                            </dx:ASPxSpinEdit>
                                            </font></td>
                                        <td align="center" valign="top">
                                            <font color="red">
                                            <dx:ASPxSpinEdit ID="seMontoDes" runat="server" Enabled="False" MaxValue="24" MinValue="1" NumberType="Integer">
                                                <SpinButtons ClientVisible="False" Enabled="False">
                                                </SpinButtons>
                                            </dx:ASPxSpinEdit>
                                            </font></td>
                                        <td align="center" valign="top">
                                            <dx:ASPxTextBox ID="llaveCredito"   runat="server"></dx:ASPxTextBox>
                                             <dx:ASPxTextBox ID="txtLlaveCrediID"  visible="false" runat="server"></dx:ASPxTextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td></td>
                                        <td></td>
                                        <td></td>
                                        <td>
                                            <br />
                                        </td>
                                        <td></td>
                                    </tr>
                                    <tr>
                                        <td></td>
                                        <td align="center"><font color="red">
                                            <dx:ASPxButton ID="btnLimpiar" runat="server" CausesValidation="False" Text="Limpiar" UseSubmitBehavior="False">
                                                <Paddings PaddingTop="5px" />
                                            </dx:ASPxButton>
                                        </font>
                                        </td>
                                        <td align="center"><font color="red">
                                            <dx:ASPxButton ID="btnRechazar" runat="server" CausesValidation="False" Text="Rechazar" Theme="RedWine" UseSubmitBehavior="False">
                                                <Paddings PaddingTop="5px" />
                                                <CheckedStyle BackColor="#FF3300">
                                                </CheckedStyle>
                                            </dx:ASPxButton>
                                            </font></td>
                                        <td align="center"><font color="red">
                                            <dx:ASPxButton ID="btnGuardar" runat="server" CausesValidation="False" Text="Guardar" UseSubmitBehavior="False">
                                                <Paddings PaddingTop="5px" />
                                            </dx:ASPxButton>
                                        </font>
                                        </td>
                                        <td></td>
                                    </tr>
                                    <tr>
                                        <td align="center" colspan="5">
                                            <br />
                                        </td>
                                    </tr>
                                </tbody>
                    </table>
                </dx:PanelContent>
            </PanelCollection>
        </dx:ASPxRoundpanel>
        <br />
        <dx:ASPxGridView runat="server" KeyFieldName="Id" AutoGenerateColumns="False" DataSourceID="SqlDataSource1" ID="ASPxGridView1" EnableCallBacks="False">
            <SettingsBehavior AllowSelectByRowClick="True" AllowSelectSingleRowOnly="True" ProcessSelectionChangedOnServer="True" />
            <SettingsSearchPanel Visible="True" />
            <Columns>
                <dx:GridViewCommandColumn SelectAllCheckboxMode="Page" ShowSelectCheckbox="True" VisibleIndex="0">
                </dx:GridViewCommandColumn>
                <dx:GridViewDataTextColumn FieldName="Id" VisibleIndex="1" ReadOnly="True">
                    <HeaderStyle Wrap="True" />
                </dx:GridViewDataTextColumn>
                <dx:GridViewDataTextColumn FieldName="Cliente" ReadOnly="True" VisibleIndex="2">
                    <EditFormSettings Visible="False"></EditFormSettings>

                    <HeaderStyle Wrap="True" />
                </dx:GridViewDataTextColumn>
                <dx:GridViewDataDateColumn FieldName="FecSolicitud" VisibleIndex="4">
                    <HeaderStyle Wrap="True" />
                </dx:GridViewDataDateColumn>
                <dx:GridViewDataDateColumn FieldName="FecAutorizado" VisibleIndex="5">
                    <HeaderStyle Wrap="True" />
                </dx:GridViewDataDateColumn>
                <dx:GridViewDataDateColumn FieldName="FecDisposicion" VisibleIndex="6">
                    <HeaderStyle Wrap="True" />
                </dx:GridViewDataDateColumn>
                <dx:GridViewDataTextColumn FieldName="MontoSolicitud" VisibleIndex="7">
                    <HeaderStyle Wrap="True" />
                </dx:GridViewDataTextColumn>
                <dx:GridViewDataTextColumn FieldName="MontoAut" VisibleIndex="8">
                    <HeaderStyle Wrap="True" />
                </dx:GridViewDataTextColumn>
                <dx:GridViewDataTextColumn FieldName="TasaRef" VisibleIndex="9" ReadOnly="True">
                    <HeaderStyle Wrap="True" />
                </dx:GridViewDataTextColumn>
                <dx:GridViewDataTextColumn FieldName="MontoLiquid" VisibleIndex="10">
                    <HeaderStyle Wrap="True" />
                </dx:GridViewDataTextColumn>
                <dx:GridViewDataTextColumn FieldName="Id_A_Liquidar" Caption="Id A Liquidar" VisibleIndex="11">
                    <HeaderStyle Wrap="True" />
                </dx:GridViewDataTextColumn>
                <dx:GridViewDataCheckColumn FieldName="LiquidaAnterior" VisibleIndex="12">
                    <HeaderStyle Wrap="True" />
                </dx:GridViewDataCheckColumn>
                <dx:GridViewDataTextColumn FieldName="NumPagosAut" VisibleIndex="13">
                    <HeaderStyle Wrap="True" />
                </dx:GridViewDataTextColumn>
                <dx:GridViewDataTextColumn FieldName="NumPagosSol" VisibleIndex="14">
                    <HeaderStyle Wrap="True" />
                </dx:GridViewDataTextColumn>
                <dx:GridViewDataTextColumn FieldName="PlazoAut" VisibleIndex="15" ReadOnly="True">
                    <HeaderStyle Wrap="True" />
                </dx:GridViewDataTextColumn>
                <dx:GridViewDataTextColumn FieldName="PlazoSol" VisibleIndex="16" ReadOnly="True">
                    <HeaderStyle Wrap="True" />
                </dx:GridViewDataTextColumn>
                <dx:GridViewDataTextColumn FieldName="TipoAmort" VisibleIndex="17" Caption="Amortización">
                    <HeaderStyle Wrap="True" />
                </dx:GridViewDataTextColumn>
                <dx:GridViewDataTextColumn FieldName="Sobretasa" VisibleIndex="18">
                    <HeaderStyle Wrap="True" />
                </dx:GridViewDataTextColumn>
                <dx:GridViewDataTextColumn FieldName="TasaMoratoria" Visible="false" VisibleIndex="19">
                    <HeaderStyle Wrap="True" />
                </dx:GridViewDataTextColumn>
                <dx:GridViewDataTextColumn FieldName="Impuesto" Visible="false" VisibleIndex="20">
                    <HeaderStyle Wrap="True" />
                </dx:GridViewDataTextColumn>
                <dx:GridViewDataTextColumn FieldName="TipoGarantia" VisibleIndex="21">
                    <HeaderStyle Wrap="True" />
                </dx:GridViewDataTextColumn>
                <dx:GridViewDataTextColumn FieldName="ValorGarantia" VisibleIndex="22">
                    <HeaderStyle Wrap="True" />
                </dx:GridViewDataTextColumn>
                <dx:GridViewDataTextColumn FieldName="Login" Visible="false" VisibleIndex="23">
                    <HeaderStyle Wrap="True" />
                </dx:GridViewDataTextColumn>
                <dx:GridViewDataDateColumn FieldName="FecCaptura" VisibleIndex="24">
                    <HeaderStyle Wrap="True" />
                </dx:GridViewDataDateColumn>
                <dx:GridViewDataTextColumn FieldName="MontoDesembolsado" Visible="false" VisibleIndex="25">
                    <HeaderStyle Wrap="True" />
                </dx:GridViewDataTextColumn>
                <dx:GridViewDataTextColumn FieldName="IngresoCliente" VisibleIndex="26">
                    <HeaderStyle Wrap="True" />
                </dx:GridViewDataTextColumn>
                <dx:GridViewDataTextColumn FieldName="Id_PlazoAut" Visible="False" VisibleIndex="27">
                </dx:GridViewDataTextColumn>
                <dx:GridViewDataTextColumn FieldName="Autorizado" Visible="False" VisibleIndex="28">
                </dx:GridViewDataTextColumn>
                <dx:GridViewDataTextColumn FieldName="DineroEntregado" Visible="False" VisibleIndex="29">
                </dx:GridViewDataTextColumn>
                <dx:GridViewDataTextColumn FieldName="Aval" VisibleIndex="3">
                    <HeaderStyle Wrap="True" />
                </dx:GridViewDataTextColumn>
                <dx:GridViewDataTextColumn FieldName="Tasa" Visible="False" VisibleIndex="30">
                </dx:GridViewDataTextColumn>
                <dx:GridViewDataTextColumn FieldName="IncluyeIVA" Visible="False" VisibleIndex="31">
                </dx:GridViewDataTextColumn>
                <dx:GridViewDataTextColumn FieldName="Id_Amort"  Visible="False" VisibleIndex="32">
                </dx:GridViewDataTextColumn>

                 <dx:GridViewDataTextColumn FieldName="Llave" Visible="true" VisibleIndex="33">
                </dx:GridViewDataTextColumn>
                 <dx:GridViewDataTextColumn FieldName="LlaveCreditoID" Visible="false" VisibleIndex="34">
                </dx:GridViewDataTextColumn>

            </Columns>
            <Styles>
                <SearchPanel HorizontalAlign="Center" VerticalAlign="Middle">
                </SearchPanel>
            </Styles>
        </dx:ASPxGridView>

        <asp:SqlDataSource runat="server" ConnectionString="<%$ ConnectionStrings:FinanciaDinConnectionString %>" SelectCommand="SELECT     S.Id, REPLACE(C1.PrimNombre + ' ' + C1.SegNombre + ' ' + C1.PrimApellido + ' ' + C1.SegApellido,'  ', ' ') AS Cliente,
           REPLACE(C2.PrimNombre + ' ' + C2.SegNombre + ' ' + C2.PrimApellido + ' ' + C2.SegApellido,'  ', ' ') AS Aval, S.FecSolicitud,  
           S.FecAutorizado, S.FecDisposicion, S.MontoSolicitud, S.MontoAut, CTI.Descripcion AS TasaRef, CTI.Valor AS Tasa, CTI.IncluyeIVA, S.MontoLiquid, S.Id_A_Liquidar, 
           S.LiquidaAnterior, S.NumPagosAut, S.NumPagosSol, CP1.Descripcion AS PlazoAut, S.PlazoAut AS Id_PlazoAut, CP2.Descripcion AS PlazoSol,  
           S.TipoAmort As Id_Amort, CTA.Nombre As TipoAmort, S.Sobretasa, S.TasaMoratoria, S.Impuesto, S.TipoGarantia, S.ValorGarantia, S.Login, S.FecCaptura, S.MontoDesembolsado, 
           S.IngresoCliente, S.Autorizado, S.DineroEntregado, LV.Llave , LV.LlaveCreditoID
FROM         SOLICITUDES AS S LEFT OUTER JOIN
             CLIENTES AS C1 ON C1.Id = S.Id_Cliente LEFT OUTER JOIN 
             CLIENTES AS C2 ON C2.Id = S.Id_Aval LEFT OUTER JOIN
             CATTASAINTERES AS CTI ON CTI.Id = S.TasaRef LEFT OUTER JOIN
             CATPLAZOS AS CP1 ON CP1.Id = S.PlazoAut LEFT OUTER JOIN
             CATPLAZOS AS CP2 ON CP2.Id = S.PlazoSol LEFT OUTER JOIN 
             CATTIPAMORT AS CTA ON CTA.Id = S.TipoAmort 
			 LEFT OUTER JOIN LLAVECREDITO LV ON S.ID = LV.SolicitudID 
WHERE     (S.Autorizado IS NULL)" ID="SqlDataSource1"></asp:SqlDataSource>
        <table>
            <tbody>
                <tr>
                    <td>

                        Razón Social:

                    </td>
                    <td>
                        
                    </td>
                </tr>
                <tr>
                    <td>

                        <asp:DropDownList ID="ddlRazon" runat="server">
                        </asp:DropDownList>

                    </td>
                    <td>
                        <dx:ASPxButton runat="server" Text="Validar" ID="bttValidar" UseSubmitBehavior="False"></dx:ASPxButton>
                    </td>
                    <td>
                        <dx:ASPxButton runat="server" Text="Generar Reporte de Cartera" ID="bttRepCartera" UseSubmitBehavior="False"></dx:ASPxButton>
                    </td>
                </tr>
            </tbody>
        </table>
        

        <br />

    </center>
    <script type="text/javascript">
        setInterval('MantenSesion()', <%= (Int(0.9 * (Session.Timeout * 60000)))%>);
    </script>
</asp:Content>
