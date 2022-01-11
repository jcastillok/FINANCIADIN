<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site.Master"
    CodeBehind="EntregaPrestamo.aspx.vb" Inherits="FinanciaDin.EntregaPrestamo" %>

<%@ Register Assembly="DevExpress.Web.v15.2, Version=15.2.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web" TagPrefix="dx" %>
<%@ Register assembly="DevExpress.Web.ASPxPivotGrid.v15.2, Version=15.2.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.Web.ASPxPivotGrid" tagprefix="dx" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
    </asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <br />
    <br />
    <center>
        <dx:ASPxRoundPanel ID="ASPxRoundPanel1" runat="server" HeaderText="Confirmación de Solicitudes"
            Theme="DevEx">
            <PanelCollection>
                <dx:PanelContent>
                    <table align="center">
                        <tbody>
                            <tr>
                                <td align="center" colspan="8">
                                    <br />
                                    <asp:ScriptManager ID="ScriptManager1" runat="server">
                                    </asp:ScriptManager>
                                    <br />
                                    <br />
                                    <dx:ASPxGridView ID="ASPxGridView1" runat="server" AutoGenerateColumns="False" DataSourceID="SqlDataSource1" KeyFieldName="Id" EnableCallBacks="False">
                                        <SettingsBehavior AllowSelectByRowClick="True" AllowSelectSingleRowOnly="True" ProcessSelectionChangedOnServer="True" />
                                        <SettingsSearchPanel Visible="True" />
                                        <Columns>
                                            <dx:GridViewCommandColumn ShowSelectCheckbox="True" SelectAllCheckboxMode="Page" ShowInCustomizationForm="True" VisibleIndex="0"></dx:GridViewCommandColumn>
                                                <dx:GridViewDataTextColumn FieldName="Id" ReadOnly="True" ShowInCustomizationForm="True" VisibleIndex="1">
                                                    <EditFormSettings Visible="False" />
                                                </dx:GridViewDataTextColumn>
                                                <dx:GridViewDataTextColumn FieldName="Cliente" ShowInCustomizationForm="True" VisibleIndex="2" ReadOnly="True">
                                                </dx:GridViewDataTextColumn>
                                                <dx:GridViewDataDateColumn FieldName="FecAutorizado" ShowInCustomizationForm="True" VisibleIndex="3" Caption="Fecha Autorizado">
                                                    <HeaderStyle Wrap="True" />
                                                </dx:GridViewDataDateColumn>
                                                <dx:GridViewDataTextColumn FieldName="Frecuencia" ShowInCustomizationForm="True" VisibleIndex="4" Caption="Frecuencia">
                                                </dx:GridViewDataTextColumn>
                                                <dx:GridViewDataDateColumn FieldName="FecDisposicion" ShowInCustomizationForm="True" VisibleIndex="5" Caption="Fecha Disposición">
                                                    <HeaderStyle Wrap="True" />
                                                </dx:GridViewDataDateColumn>
                                                <dx:GridViewDataTextColumn FieldName="MontoAut" ShowInCustomizationForm="True" VisibleIndex="6" Caption="Monto">
                                                </dx:GridViewDataTextColumn>
                                                <dx:GridViewDataTextColumn FieldName="NumPagosAut" ShowInCustomizationForm="True" VisibleIndex="7" Caption="# Pagos">
                                                </dx:GridViewDataTextColumn>
                                                <dx:GridViewDataTextColumn FieldName="MontoDesembolsado" ShowInCustomizationForm="True" VisibleIndex="10">
                                                    <HeaderStyle Wrap="True" />
                                                </dx:GridViewDataTextColumn>
                                            <dx:GridViewDataTextColumn Caption="Tasa de Interes" FieldName="TasaRef" ShowInCustomizationForm="True" VisibleIndex="8">
                                            </dx:GridViewDataTextColumn>
                                            <dx:GridViewDataSpinEditColumn FieldName="Sobretasa" ShowInCustomizationForm="True" VisibleIndex="9">
                                                <PropertiesSpinEdit DisplayFormatString="{0}%" NumberFormat="Percent">
                                                </PropertiesSpinEdit>
                                            </dx:GridViewDataSpinEditColumn>
                                            <dx:GridViewDataTextColumn FieldName="Id_Cliente" ShowInCustomizationForm="True" Visible="False" VisibleIndex="11">
                                            </dx:GridViewDataTextColumn>
                                            <dx:GridViewDataTextColumn FieldName="Direccion" ShowInCustomizationForm="True" Visible="False" VisibleIndex="12">
                                            </dx:GridViewDataTextColumn>
                                            <dx:GridViewDataTextColumn FieldName="TipoGarantia" ShowInCustomizationForm="True" Visible="False" VisibleIndex="13">
                                            </dx:GridViewDataTextColumn>
                                            <dx:GridViewDataTextColumn FieldName="PlazoAut" ShowInCustomizationForm="True" Visible="False" VisibleIndex="14">
                                            </dx:GridViewDataTextColumn>
                                            <dx:GridViewDataTextColumn FieldName="Id_Credito" ShowInCustomizationForm="True" Visible="False" VisibleIndex="15">
                                            </dx:GridViewDataTextColumn>
                                        </Columns>
                                        <Styles>
                                            <SearchPanel HorizontalAlign="Center" VerticalAlign="Middle">
                                            </SearchPanel>
                                        </Styles>
                                    </dx:ASPxGridView>
                                    <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:FinanciaDinConnectionString %>" SelectCommand="SELECT SOLICITUDES.Id, CREDITOS.ID AS Id_Credito, (PrimNombre + ' ' +  SegNombre + ' ' +PrimApellido + ' ' + SegApellido) AS Cliente, ('Calle ' + CLIENTES.Calle + ' #' +  CLIENTES.NumExt + ' ' + CLIENTES.Colonia + ', ' + CLIENTES.Localidad + ', ' + CLIENTES.EstadoDom) AS Direccion, CREDITOS.Id_Cliente, FecAutorizado, CATPLAZOS.Descripcion AS Frecuencia, PlazoAut, FecDisposicion, MontoAut, NumPagosAut, MontoDesembolsado, CREDITOS.Sobretasa, CATTASAINTERES.Descripcion As TasaRef, SOLICITUDES.TipoGarantia
FROM SOLICITUDES JOIN 
CLIENTES ON SOLICITUDES.Id_Cliente = Clientes.Id JOIN
CATPLAZOS ON CATPLAZOS.ID = SOLICITUDES.PlazoAut JOIN
CATTASAINTERES ON CATTASAINTERES.ID = TasaRef JOIN
CREDITOS ON CREDITOS.ID_SOL = SOLICITUDES.ID
WHERE (Autorizado = 1) AND (DineroEntregado = 0)"></asp:SqlDataSource>
                                    <br />
                                    <br />
                                    <dx:ASPxButton ID="bttValidar" runat="server" Text="Marcar Dinero Entregado">
                                    </dx:ASPxButton>
                                    <br />
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
