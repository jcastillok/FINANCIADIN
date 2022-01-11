<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site.Master"
    CodeBehind="CapturaSolicitudes.aspx.vb" Inherits="FinanciaDin.CapturaSolicitudes" %>

<%@ Register Assembly="DevExpress.Web.v15.2, Version=15.2.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web" TagPrefix="dx" %>
<%@ Register assembly="DevExpress.Web.ASPxPivotGrid.v15.2, Version=15.2.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.Web.ASPxPivotGrid" tagprefix="dx" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
    <style type="text/css">
        .auto-style3 {
            margin-bottom: 0px;
        }
        .auto-style4 {
            height: 17px;
        }
        .auto-style5 {
            height: 19px;
        }
        </style>
    </asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <br />
    <br />
    <center>
                <asp:ScriptManager ID="ScriptManager1" runat="server">
                </asp:ScriptManager>
                <dx:ASPxRoundPanel ID="ASPxRoundPanel1" runat="server" HeaderText="Captura de Solicitudes"
                    Theme="DevEx">
                    <PanelCollection>
                        <dx:PanelContent>

                            <table align="center">
                                <tbody>
                                    <tr>
                                        <td align="center" class="auto-style4">ID </td>
                                        <td align="center" class="auto-style4">Cliente</td>
                                        <td align="center" class="auto-style4">Fecha de Solicitud</td>
                                        <td align="center" class="auto-style4">Monto Solicitado</td>
                                        <td align="center" class="auto-style4">Frecuencia de Pagos</td>
                                    </tr>
                                    <tr>
                                        <td align="center">
                                            <asp:Label ID="lblId" runat="server">--</asp:Label>
                                        </td>
                                        <td align="center">
                                            <dx:ASPxComboBox ID="cbClientes" runat="server" DropDownStyle="DropDown" DataSourceID="SqlDataSource2" TextField="Nombre" ValueField="Id" AutoPostBack="True">
                                            </dx:ASPxComboBox>
                                            <asp:SqlDataSource ID="SqlDataSource2" runat="server" ConnectionString="<%$ ConnectionStrings:FinanciaDinConnectionString %>" SelectCommand="SELECT     Id, REPLACE(PrimNombre + ' ' + IsNull(SegNombre,'') + ' ' + PrimApellido + ' ' + IsNull(SegApellido,''), '  ', ' ') AS Nombre
FROM         CLIENTES
WHERE     (Id NOT IN
                          (SELECT     Id_Cliente
                            FROM          CREDITOS
                            WHERE      (Liquidado = 0) AND (estatusID = 1))) AND (Id NOT IN
                          (SELECT     Id_Cliente
                            FROM          SOLICITUDES
                            WHERE      (Autorizado IS NULL)))
ORDER BY Nombre"></asp:SqlDataSource>
                                        </td>
                                        <td align="center">
                                            <dx:ASPxDateEdit ID="deFecSol" runat="server">
                                            </dx:ASPxDateEdit>
                                        </td>
                                        <td align="center">
                                            <font color="red">
                                            <dx:ASPxSpinEdit ID="txtMontoSol" runat="server" DecimalPlaces="2" MaxValue="2147483647" MinValue="1" AutoPostBack="True">
                                                <SpinButtons ClientVisible="False" Enabled="False">
                                                </SpinButtons>
                                            </dx:ASPxSpinEdit>
                                            </font>
                                        </td>
                                        <td align="center">
                                            <dx:ASPxComboBox ID="cbPlazoSol" runat="server" DataSourceID="SqlDataSource3" DropDownStyle="DropDown" TextField="Descripcion" ValueField="Id">
                                            </dx:ASPxComboBox>
                                            <asp:SqlDataSource ID="SqlDataSource3" runat="server" ConnectionString="<%$ ConnectionStrings:FinanciaDinConnectionString %>" SelectCommand="SELECT * FROM [CATPLAZOS]"></asp:SqlDataSource>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="center" class="auto-style4">Pagos Solicitados </td>
                                        <td align="center" class="auto-style4">Tasa de Referencia</td>
                                        <td align="center" class="auto-style4">Tipo de Amortización</td>
                                        <td align="center" class="auto-style4">Tasa Anual</td>
                                        <td align="center" class="auto-style4">Interés Moratorio</td>
                                    </tr>
                                    <tr>
                                        <td align="center" valign="middle">
                                            <font color="red">
                                            <dx:ASPxSpinEdit ID="sePagosSol" runat="server" AutoPostBack="True" MaxValue="104" MinValue="1" NumberType="Integer" ShowOutOfRangeWarning="False">
                                                <ValidationSettings ValidateOnLeave="False">
                                                </ValidationSettings>
                                            </dx:ASPxSpinEdit>
                                            </font>
                                        </td>
                                        <td align="center" style="margin-left: 40px" valign="top">
                                            <dx:ASPxComboBox ID="cbTasaRef" runat="server" AutoPostBack="True" DataSourceID="SqlDataSource7" DropDownStyle="DropDown" TextField="Descripcion" ValueField="Id" ValueType="System.Int32">
                                            </dx:ASPxComboBox>
                                            <asp:SqlDataSource ID="SqlDataSource7" runat="server" ConnectionString="<%$ ConnectionStrings:FinanciaDinConnectionString %>" SelectCommand="SELECT [Id], [Descripcion] FROM [CATTASAINTERES] ORDER BY [Descripcion]"></asp:SqlDataSource>
                                        </td>
                                        <td align="center" valign="top">
                                            <dx:ASPxComboBox ID="cbTipAmort" runat="server" DataSourceID="SqlDataSource6" DropDownStyle="DropDown" TextField="Nombre" ValueField="Id">
                                            </dx:ASPxComboBox>
                                            <asp:SqlDataSource ID="SqlDataSource6" runat="server" ConnectionString="<%$ ConnectionStrings:FinanciaDinConnectionString %>" SelectCommand="SELECT [Id], [Nombre] FROM [CATTIPAMORT]"></asp:SqlDataSource>
                                        </td>
                                        <td align="center" valign="middle">
                                            <font color="red">
                                                <dx:ASPxSpinEdit ID="txtSobretasa" runat="server" DecimalPlaces="4" MaxValue="100" DisplayFormatString="p3" Increment="0.01" LargeIncrement="0.1">
                                                    <SpinButtons ClientVisible="False" Enabled="False">
                                                    </SpinButtons>
                                                </dx:ASPxSpinEdit>
                                            </font>
                                        </td>
                                        <td align="center">
                                            <dx:ASPxComboBox ID="cbTasaMora" runat="server" DataSourceID="SqlDataSource5" DropDownStyle="DropDown" TextField="Tasa" ValueField="Id">
                                            </dx:ASPxComboBox>
                                            <asp:SqlDataSource ID="SqlDataSource5" runat="server" ConnectionString="<%$ ConnectionStrings:FinanciaDinConnectionString %>" SelectCommand="SELECT [Id], [Tasa] FROM [CATTASAMORA]"></asp:SqlDataSource>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="center">Impuesto (IVA)</td>
                                        <td align="center">Tipo de Garantía</td>
                                        <td align="center">Valor de Garantía</td>
                                        <td align="center">Ingreso Mensual del Cliente</td>
                                        <td align="center">Sucursal</td>
                                    </tr>
                                    <tr>
                                        <td align="center" valign="middle">
                                            <font color="red">
                                            <dx:ASPxSpinEdit ID="txtImpuesto" runat="server" DecimalPlaces="2" MaxValue="100" MinValue="1" Number="16">
                                                <SpinButtons ClientVisible="False" Enabled="False">
                                                </SpinButtons>
                                            </dx:ASPxSpinEdit>
                                            </font>
                                        </td>
                                        <td align="center" valign="middle">
                                            <dx:ASPxComboBox ID="cbTipGar" runat="server" DataSourceID="SqlDataSource8" DropDownStyle="DropDown" TextField="Nombre" ValueField="Id" AutoPostBack="True">
                                            </dx:ASPxComboBox>
                                            <asp:SqlDataSource ID="SqlDataSource8" runat="server" ConnectionString="<%$ ConnectionStrings:FinanciaDinConnectionString %>" SelectCommand="SELECT [Id], [Nombre] FROM [CATTIPGARANTIAS]"></asp:SqlDataSource>
                                        </td>
                                        <td align="center" valign="middle">
                                            <dx:ASPxTextBox ID="txtValorGar" runat="server" Width="170px" Enabled="False" ReadOnly="True">
                                            </dx:ASPxTextBox>
                                        </td>
                                        <td align="center" valign="middle">
                                            <font color="red">
                                            <dx:ASPxSpinEdit ID="txtIngCli" runat="server" DecimalPlaces="2" MaxValue="2147483647" MinValue="1" NumberType="Integer">
                                                <SpinButtons ClientVisible="False" Enabled="False">
                                                </SpinButtons>
                                            </dx:ASPxSpinEdit>
                                            </font></td>
                                        <td align="center" valign="middle">
                                            <dx:ASPxComboBox ID="cbSuc" runat="server" DataSourceID="SqlDataSource9" DropDownStyle="DropDown" TextField="Descripcion" ValueField="Id">
                                            </dx:ASPxComboBox>
                                            <asp:SqlDataSource ID="SqlDataSource9" runat="server" ConnectionString="<%$ ConnectionStrings:FinanciaDinConnectionString %>" SelectCommand="SELECT [Id], [Descripcion] 
FROM [CATSUCURSALES]
WHERE [Disponible] = 1"></asp:SqlDataSource>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="center" class="auto-style5">Aval</td>
                                        <td align="center" class="auto-style5">Asesor</td>
                                        <td align="center" class="auto-style5">Tipo de Producto</td>
                                        <td align="center" class="auto-style5">Destino</td>
                                        <td align="center" class="auto-style5"></td>
                                    </tr>
                                    <tr>
                                        <td align="center" valign="middle">
                                            <dx:ASPxComboBox ID="cbAval" runat="server" DataSourceID="SqlDataSource11" TextField="Nombre" ValueField="Id">
                                            </dx:ASPxComboBox>
                                            <asp:SqlDataSource ID="SqlDataSource11" runat="server" ConnectionString="<%$ ConnectionStrings:FinanciaDinConnectionString %>" SelectCommand="SELECT     REPLACE(PrimNombre + ' ' + SegNombre + ' ' + PrimApellido + ' ' + SegApellido, '  ', ' ') AS Nombre, Id
FROM         CLIENTES
WHERE     (EsAval = 1) AND (Id NOT IN
                          (SELECT DISTINCT Id_Aval
                            FROM          SOLICITUDES
                            WHERE      (Autorizado IS NULL) AND (Id_Aval IS NOT NULL))) AND (Id NOT IN
                          (SELECT DISTINCT Id_Aval
                            FROM          CREDITOS
                            WHERE      (Liquidado = 0) AND (estatusID = 1) AND (Id_Aval IS NOT NULL)))"></asp:SqlDataSource>
                                        </td>
                                        <td align="center" valign="middle">
                                           
                                            <dx:ASPxComboBox ID="cbAsesores" runat="server" AutoPostBack="True" DataSourceID="SqlDataSource10" DropDownStyle="DropDown" TextField="Nombre" ValueField="Login">
                                            </dx:ASPxComboBox>
                                            <asp:SqlDataSource ID="SqlDataSource10" runat="server" ConnectionString="<%$ ConnectionStrings:FinanciaDinConnectionString %>" SelectCommand="SELECT Login, Nombre FROM USUARIOS WHERE (Tipo = 5)"></asp:SqlDataSource>
                                           
                                        </td>
                                        <td align="center" valign="middle">
                                            <dx:ASPxTextBox ID="txtTipProd" runat="server" Width="170px">
                                            </dx:ASPxTextBox>
                                        </td>
                                        <td align="center" valign="middle">
                                            <dx:ASPxTextBox ID="txtDestino" runat="server" Width="170px">
                                            </dx:ASPxTextBox>
                                        </td>
                                        <td align="center" valign="middle">
                                            
                                        </td>
                                    </tr>
                                    <tr>
                                        <td></td>
                                        <td></td>
                                        <td>
                                            <br></br</td>
                                        <td></td>
                                        <td></td>
                                    </tr>
                                    <tr>
                                        <td></td>
                                        <td></td>
                                        <td></td>
                                        <td></td>
                                        <td></td>
                                    </tr>
                                    <tr valign="middle">
                                        <td align="center" valign="middle"></td>
                                        <td colspan="3" align="center" valign="middle">
                                            <table style="border-style: solid">
                                                <tr>
                                                    <td align="center" valign="middle">
                                                        Crédito a Liquidar</td>
                                                    <td align="center" valign="middle">
                                                        Monto Liquidado Crédito Anterior
                                                    </td>
                                                    <td align="center" valign="middle">
                                                        Monto Desembolsado
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td align="center" valign="middle">
                                                        <dx:ASPxComboBox ID="cbALiquidar" runat="server" DataSourceID="SqlDataSource4" DropDownStyle="DropDown" TextField="Nombre" ValueField="Id"  AutoPostBack="True">
                                                        </dx:ASPxComboBox>
                                                        <asp:SqlDataSource ID="SqlDataSource4" runat="server" ConnectionString="<%$ ConnectionStrings:FinanciaDinConnectionString %>" SelectCommand="SELECT C.Id, REPLACE(PrimNombre + ' ' + IsNull(SegNombre,'') + ' ' + PrimApellido + ' ' + IsNull(SegApellido,''), '  ', ' ') AS Nombre
FROM [CREDITOS] AS C INNER JOIN
	 CLIENTES AS CLI ON CLI.Id = C.Id_Cliente
WHERE Liquidado = 0 AND (estatusID = 1)
ORDER BY Nombre"></asp:SqlDataSource>
                                                    </td>
                                                    <td align="center" valign="middle">
                                                        <dx:ASPxSpinEdit ID="txtMontoALiquidar" runat="server" DecimalPlaces="2" MaxValue="2147483647" MinValue="1" CssClass="auto-style3" DisplayFormatString="C" Enabled="False" ReadOnly="True">
                                                            <SpinButtons ClientVisible="False" Enabled="False">
                                                            </SpinButtons>
                                                        </dx:ASPxSpinEdit>
                                                    </td>
                                                    <td align="center" valign="middle">
                                                        <font color="red">
                                                            <dx:ASPxSpinEdit ID="txtMontoDes" runat="server" DecimalPlaces="2" MaxValue="2147483647" MinValue="1" NumberType="Integer" DisplayFormatString="C" Enabled="False" ReadOnly="True">
                                                                <SpinButtons ClientVisible="False" Enabled="False">
                                                                </SpinButtons>
                                                            </dx:ASPxSpinEdit>
                                                        </font></td>
                                                </tr>
                                            </table>
                                        </td>
                                        <td align="center" valign="middle">
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="center" valign="middle"></td>
                                        <td colspan="3">
                                        </td>
                                        <td align="center" valign="middle">
                                            </td>
                                    </tr>
                                    <tr>
                                        <td></td>
                                        <td></td>
                                        <td><br /></td>
                                        <td></td>
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
                                        <td></td>
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
                                            <br />
                                            <br />
                                        </td>
                                    </tr>
                                </tbody>
                            </table>

                        </dx:PanelContent>
                    </PanelCollection>
                </dx:ASPxRoundPanel>
                <br />
                <br />
                <br />
                <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:FinanciaDinConnectionString %>" SelectCommand="SELECT SOLICITUDES.Id, SOLICITUDES.Id_Cliente, CLIENTES_1.PrimNombre + ' ' + CLIENTES_1.SegNombre + ' ' + CLIENTES_1.PrimApellido + ' ' + CLIENTES_1.SegApellido AS Cliente, (SELECT PrimNombre + ' ' + SegNombre + ' ' + PrimApellido + ' ' + SegApellido AS Aval FROM CLIENTES WHERE (Id = SOLICITUDES.Id_Aval)) AS Aval, CATSUCURSALES.Id AS Id_Sucursal, USUARIOS.Nombre AS Asesor, CATSUCURSALES.Descripcion AS Sucursal, SOLICITUDES.FecSolicitud, SOLICITUDES.MontoSolicitud, SOLICITUDES.MontoAut, (SELECT Descripcion FROM CATPLAZOS WHERE (Id = SOLICITUDES.PlazoSol)) AS PlazoSol, SOLICITUDES.NumPagosSol, SOLICITUDES.LiquidaAnterior, SOLICITUDES.Id_A_Liquidar, SOLICITUDES.MontoLiquid, CATTASAINTERES.Descripcion AS TasaRef, CATTIPAMORT.Nombre AS TipoAmort, SOLICITUDES.Sobretasa, SOLICITUDES.TasaMoratoria, SOLICITUDES.Impuesto, CATTIPGARANTIAS.Nombre AS TipoGarantia, SOLICITUDES.ValorGarantia, SOLICITUDES.IngresoCliente, SOLICITUDES.MontoDesembolsado, SOLICITUDES.FecDisposicion, SOLICITUDES.FecAutorizado, SOLICITUDES.Id_Aval, SOLICITUDES.Login_Asesor FROM SOLICITUDES INNER JOIN CLIENTES AS CLIENTES_1 ON SOLICITUDES.Id_Cliente = CLIENTES_1.Id INNER JOIN CATTIPGARANTIAS ON CATTIPGARANTIAS.Id = SOLICITUDES.TipoGarantia INNER JOIN CATTASAINTERES ON CATTASAINTERES.Id = SOLICITUDES.TasaRef INNER JOIN CATTIPAMORT ON CATTIPAMORT.Id = SOLICITUDES.TipoAmort INNER JOIN CATSUCURSALES ON CATSUCURSALES.Id = SOLICITUDES.Id_Sucursal LEFT OUTER JOIN USUARIOS ON SOLICITUDES.Login_Asesor = USUARIOS.Login WHERE (SOLICITUDES.Autorizado IS NULL)"></asp:SqlDataSource>
                <dx:ASPxGridView ID="gvSolicitudes" runat="server" AutoGenerateColumns="False" DataSourceID="SqlDataSource1" KeyFieldName="Id" EnableCallBacks="False">
                    <SettingsBehavior AllowSelectByRowClick="True" AllowSelectSingleRowOnly="True" ProcessSelectionChangedOnServer="True" />
                    <SettingsSearchPanel Visible="True" />
                    <Columns>
                        <dx:GridViewCommandColumn SelectAllCheckboxMode="Page" ShowSelectCheckbox="True" VisibleIndex="0">
                        </dx:GridViewCommandColumn>
                        <dx:GridViewDataTextColumn FieldName="Id" ReadOnly="True" VisibleIndex="1">
                            <EditFormSettings Visible="False" />
                            <HeaderStyle Wrap="True" />
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn FieldName="Cliente" VisibleIndex="3" ReadOnly="True">
                            <HeaderStyle Wrap="True" />
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataDateColumn FieldName="FecSolicitud" VisibleIndex="8" Caption="Fecha Solicitud">
                            <HeaderStyle Wrap="True" />
                        </dx:GridViewDataDateColumn>
                        <dx:GridViewDataTextColumn FieldName="MontoSolicitud" VisibleIndex="9">
                            <HeaderStyle Wrap="True" />
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn FieldName="PlazoSol" VisibleIndex="10" ReadOnly="True" Caption="Frecuencia Pagos">
                            <HeaderStyle Wrap="True" />
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn FieldName="NumPagosSol" VisibleIndex="11" Caption="Num Pagos">
                            <HeaderStyle Wrap="True" />
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn FieldName="Id_A_Liquidar" VisibleIndex="13" Caption="Id A Liquidar" Visible="False">
                            <HeaderStyle Wrap="True" />
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn FieldName="MontoLiquid" VisibleIndex="14" Visible="False">
                            <HeaderStyle Wrap="True" />
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn FieldName="TasaRef" VisibleIndex="15" Caption="Tasa Referencia">
                            <HeaderStyle Wrap="True" />
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn FieldName="TipoAmort" VisibleIndex="16">
                            <HeaderStyle Wrap="True" />
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn FieldName="Sobretasa" VisibleIndex="17">
                            <HeaderStyle Wrap="True" />
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn FieldName="TasaMoratoria" VisibleIndex="18">
                            <HeaderStyle Wrap="True" />
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn FieldName="Impuesto" VisibleIndex="19">
                            <HeaderStyle Wrap="True" />
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn FieldName="TipoGarantia" VisibleIndex="20">
                            <HeaderStyle Wrap="True" />
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn FieldName="ValorGarantia" VisibleIndex="21">
                            <HeaderStyle Wrap="True" />
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn FieldName="IngresoCliente" VisibleIndex="22">
                            <HeaderStyle Wrap="True" />
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn FieldName="MontoDesembolsado" VisibleIndex="23">
                            <HeaderStyle Wrap="True" />
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn FieldName="Sucursal" VisibleIndex="7">
                            <HeaderStyle Wrap="True" />
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn FieldName="Id_Sucursal" Visible="False" VisibleIndex="5">
                            <HeaderStyle Wrap="True" />
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataCheckColumn FieldName="LiquidaAnterior" VisibleIndex="12">
                            <HeaderStyle Wrap="True" />
                        </dx:GridViewDataCheckColumn>
                        <dx:GridViewDataTextColumn FieldName="Asesor" VisibleIndex="6">
                            <HeaderStyle Wrap="True" />
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn FieldName="Id_Cliente" Visible="False" VisibleIndex="2">
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn FieldName="FecAutorizado" Visible="False" VisibleIndex="24">
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn FieldName="FecDisposicion" ShowInCustomizationForm="False" Visible="False" VisibleIndex="25">
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn Caption="Id_Aval" FieldName="Id_Aval" Visible="False" VisibleIndex="26">
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn Caption="Aval" FieldName="Aval" VisibleIndex="4">
                            <HeaderStyle Wrap="True" />
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn FieldName="Login_Asesor" Visible="False" VisibleIndex="27">
                        </dx:GridViewDataTextColumn>
                    </Columns>
                    <Styles>
                        <FilterBar HorizontalAlign="Center" VerticalAlign="Middle">
                        </FilterBar>
                        <HeaderFilterItem HorizontalAlign="Center" VerticalAlign="Middle">
                        </HeaderFilterItem>
                        <SearchPanel HorizontalAlign="Center" VerticalAlign="Middle">
                        </SearchPanel>
                    </Styles>
                </dx:ASPxGridView>
    </center>
    <script type="text/javascript">
        setInterval('MantenSesion()', <%= (Int(0.9 * (Session.Timeout * 60000)))%>);
    </script>
</asp:Content>
