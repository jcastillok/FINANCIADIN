<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site.Master"
    CodeBehind="AltaClientes.aspx.vb" Inherits="FinanciaDin.AltaClientes" %>

<%@ Register Assembly="DevExpress.Web.v15.2, Version=15.2.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.Web.ASPxPivotGrid.v15.2, Version=15.2.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web.ASPxPivotGrid" TagPrefix="dx" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
    <style type="text/css">
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <br />
    <br />
    <center>
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
                <asp:ScriptManager ID="ScriptManager1" runat="server">
                </asp:ScriptManager>
                <dx:ASPxRoundPanel ID="ASPxRoundPanel1" runat="server" HeaderText="Captura de Clientes"
                    Theme="DevEx">
                    <PanelCollection>
                        <dx:PanelContent>
                            <table align="center">
                                <tbody>
                                    <tr>
                                        <td colspan="8">
                                            <dx:ASPxPageControl ID="ASPxPageControl1" runat="server" ActiveTabIndex="0">
                                                <TabPages>
                                                    <dx:TabPage Name="Cliente" Text="Cliente">
                                                        <ContentCollection>
                                                            <dx:ContentControl runat="server">
                                                                <table>
                                                                    <tbody>
                                                                        <tr>
                                                                            <td align="center">ID</td>
                                                                            <td align="center">Primer Nombre</td>
                                                                            <td align="center">Segundo Nombre</td>
                                                                            <td align="center">Apellido Paterno</td>
                                                                            <td align="center" class="auto-style2">Apellido Materno</td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td align="center">
                                                                                <asp:Label ID="lblId" runat="server">--</asp:Label>
                                                                            </td>
                                                                            <td align="center">
                                                                                <dx:ASPxTextBox ID="txtPrimNom" runat="server" Width="170px" AutoPostBack="True">
                                                                                </dx:ASPxTextBox>
                                                                            </td>
                                                                            <td align="center">
                                                                                <dx:ASPxTextBox ID="txtSegNom" runat="server" Width="170px" AutoPostBack="True">
                                                                                </dx:ASPxTextBox>
                                                                            </td>
                                                                            <td align="center">
                                                                                <dx:ASPxTextBox ID="txtPrimApe" runat="server" Width="170px" AutoPostBack="True">
                                                                                </dx:ASPxTextBox>
                                                                            </td>
                                                                            <td align="center" class="auto-style2">
                                                                                <dx:ASPxTextBox ID="txtSegApe" runat="server" Width="170px" AutoPostBack="True">
                                                                                </dx:ASPxTextBox>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td align="center">Tipo Cliente</td>
                                                                            <td align="center">CURP</td>
                                                                            <td align="center">RFC</td>
                                                                            <td align="center">Email</td>
                                                                            <td align="center" class="auto-style2">Fecha de Nacimiento </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td align="center" valign="top">
                                                                                <dx:ASPxComboBox ID="cbTipCli" runat="server" DropDownStyle="DropDown" DataSourceID="SqlDataSource6" TextField="Descripcion" ValueField="Id">
                                                                                </dx:ASPxComboBox>
                                                                                <asp:SqlDataSource ID="SqlDataSource6" runat="server" ConnectionString="<%$ ConnectionStrings:FinanciaDinConnectionString %>" SelectCommand="SELECT [Id], [Descripcion] FROM [CATTIPOCLIENTES]"></asp:SqlDataSource>
                                                                            </td>
                                                                            <td align="center" style="margin-left: 40px" valign="top">
                                                                                <dx:ASPxTextBox ID="txtCurp" runat="server" Width="170px">
                                                                                </dx:ASPxTextBox>
                                                                            </td>
                                                                            <td align="center" valign="top">
                                                                                <dx:ASPxTextBox ID="txtRFC" runat="server" Width="170px">
                                                                                </dx:ASPxTextBox>
                                                                            </td>
                                                                            <td align="center" valign="top">
                                                                                <dx:ASPxTextBox ID="txtEmail" runat="server" AutoPostBack="True" Width="170px">
                                                                                </dx:ASPxTextBox>
                                                                            </td>
                                                                            <td align="center" valign="top" class="auto-style2">
                                                                                <dx:ASPxDateEdit ID="deFecNac" runat="server">
                                                                                </dx:ASPxDateEdit>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td align="center">País de Nacimiento</td>
                                                                            <td align="center">Entidad de Nacimiento</td>
                                                                            <td align="center">Nacionalidad</td>
                                                                            <td align="center">Sexo</td>
                                                                            <td align="center">Ocupación</td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td align="center" valign="top">

                                                                                <dx:ASPxComboBox ID="cbPais" runat="server">
                                                                                </dx:ASPxComboBox>

                                                                            </td>
                                                                            <td align="center" valign="top">

                                                                                <dx:ASPxComboBox ID="cbEntidadNac" runat="server" DataSourceID="SqlDataSource2" DropDownStyle="DropDown" TextField="Localidad" ValueField="Localidad">
                                                                                </dx:ASPxComboBox>

                                                                            </td>
                                                                            <td align="center" valign="top">
                                                                                <dx:ASPxComboBox ID="cbNacionalidad" runat="server" DataSourceID="SqlDataSource7" TextField="Nacionalidad" ValueField="Nacionalidad" DropDownStyle="DropDown">
                                                                                </dx:ASPxComboBox>
                                                                                <asp:SqlDataSource ID="SqlDataSource7" runat="server" ConnectionString="<%$ ConnectionStrings:FinanciaDinConnectionString %>" SelectCommand="SELECT DISTINCT [Nacionalidad] FROM [CLIENTES] ORDER BY [Nacionalidad]"></asp:SqlDataSource>
                                                                            </td>
                                                                            <td align="center" valign="top">

                                                                                <dx:ASPxComboBox ID="cbSexo" runat="server">
                                                                                </dx:ASPxComboBox>

                                                                            </td>
                                                                            <td align="center" valign="top">
                                                                                <dx:ASPxTextBox ID="txtOcupacion" runat="server" AutoPostBack="True" Width="170px">
                                                                                </dx:ASPxTextBox>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td align="center">Celular</td>
                                                                            <td align="center">Estado Civil</td>
                                                                            <td align="center">Nombre(s) Conyuge</td>
                                                                            <td align="center">Apellido Paterno Conyuge</td>
                                                                            <td align="center">Apellido Materno Conyuge</td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td align="center" valign="middle">
                                                                                <font color="red">
                                                                                    <dx:ASPxSpinEdit ID="txtCel" runat="server" DecimalPlaces="2" MaxValue="21474836470" MinValue="1" NumberType="Integer">
                                                                                        <SpinButtons ClientVisible="False" Enabled="False">
                                                                                        </SpinButtons>
                                                                                    </dx:ASPxSpinEdit>
                                                                                </font>
                                                                            </td>
                                                                            <td align="center" valign="middle">
                                                                                <dx:ASPxTextBox ID="txtEstCivil" runat="server" Width="170px">
                                                                                </dx:ASPxTextBox>
                                                                            </td>
                                                                            <td align="center" valign="middle">
                                                                                <dx:ASPxTextBox ID="txtPrimNomCon" runat="server" Width="170px">
                                                                                </dx:ASPxTextBox>
                                                                            </td>
                                                                            <td align="center" valign="middle">
                                                                                <dx:ASPxTextBox ID="txtApellPatCon" runat="server" Width="170px">
                                                                                </dx:ASPxTextBox>
                                                                            </td>
                                                                            <td align="center" valign="middle">
                                                                                <dx:ASPxTextBox ID="txtApellMatCon" runat="server" Width="170px">
                                                                                </dx:ASPxTextBox>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td align="center">Fecha de Nacimiento Conyuge</td>
                                                                            <td align="center">INE / IFE</td>
                                                                            <td align="center">Comprobante de Domicilio</td>
                                                                            <td align="center">Solicitud de Consulta a Buro</td>
                                                                            <td align="center"></td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td align="center" valign="middle">
                                                                                <dx:ASPxDateEdit ID="deFecNacCon" runat="server">
                                                                                </dx:ASPxDateEdit>
                                                                            </td>
                                                                            <td align="center" valign="middle">
                                                                                <ajaxToolkit:AsyncFileUpload ID="fuIdentificacion" runat="server" FailedValidation="False" Width="200px" PersistFile="True" />
                                                                            </td>
                                                                            <td align="center" valign="middle">
                                                                                <ajaxToolkit:AsyncFileUpload ID="fuCompDom" runat="server" FailedValidation="False" Width="200px" />
                                                                            </td>
                                                                            <td align="center" valign="middle">
                                                                                <ajaxToolkit:AsyncFileUpload ID="fuSolBuro" runat="server" FailedValidation="False" Width="200px" />
                                                                            </td>
                                                                            <td align="center" valign="middle">
                                                                                <asp:CheckBox ID="checkAval" runat="server" AutoPostBack="True" Text="Es Aval?" />
                                                                            </td>
                                                                        </tr>
                                                                    </tbody>
                                                                </table>
                                                            </dx:ContentControl>
                                                        </ContentCollection>
                                                    </dx:TabPage>
                                                    <dx:TabPage Name="Domicilio" Text="Domicilio">
                                                        <ContentCollection>
                                                            <dx:ContentControl runat="server">
                                                                <table>
                                                                    <tbody>
                                                                        <tr>
                                                                            <td align="center">Calle</td>
                                                                            <td align="center">Número Exterior</td>
                                                                            <td align="center">Numero Interior</td>
                                                                            <td align="center">Colonia</td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td align="center" valign="middle">
                                                                                <dx:ASPxTextBox ID="txtCalle" runat="server" Width="170px">
                                                                                </dx:ASPxTextBox>
                                                                            </td>
                                                                            <td align="center" class="auto-style2">
                                                                                <dx:ASPxTextBox ID="txtNumExt" runat="server" Width="170px">
                                                                                </dx:ASPxTextBox>
                                                                            </td>
                                                                            <td align="center" valign="middle">
                                                                                <dx:ASPxTextBox ID="txtNumInt" runat="server" Width="170px">
                                                                                </dx:ASPxTextBox>
                                                                            </td>
                                                                            <td align="center" valign="middle">
                                                                                <dx:ASPxTextBox ID="txtColonia" runat="server" Width="170px">
                                                                                </dx:ASPxTextBox>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td align="center">Codigo Postal</td>
                                                                            <td align="center">Localidad</td>
                                                                            <td align="center">Pais Domicilio</td>
                                                                            <td align="center">Estado Domicilio</td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td align="center" valign="top">
                                                                                <font color="red">
                                                                                    <dx:ASPxSpinEdit ID="txtCP" runat="server" DecimalPlaces="2" MaxValue="2147483647" MinValue="1" NumberType="Integer">
                                                                                        <SpinButtons ClientVisible="False" Enabled="False">
                                                                                        </SpinButtons>
                                                                                    </dx:ASPxSpinEdit>
                                                                                </font>
                                                                            </td>
                                                                            <td align="center" valign="top">
                                                                                <dx:ASPxComboBox ID="cbLocalidad" runat="server" DropDownStyle="DropDown" DataSourceID="SqlDataSource2" TextField="Localidad" ValueField="Localidad">
                                                                                </dx:ASPxComboBox>
                                                                            </td>
                                                                            <td align="center" valign="top">
                                                                                <dx:ASPxComboBox runat="server" ID="cbPaisDom">
                                                                                </dx:ASPxComboBox>
                                                                            </td>
                                                                            <td align="center" valign="top">
                                                                                <dx:ASPxComboBox ID="cbEstado" runat="server" DropDownStyle="DropDown" DataSourceID="SqlDataSource4" TextField="Estado" ValueField="Estado">
                                                                                </dx:ASPxComboBox>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td align="center">Municipio Domicilio</td>
                                                                            <td align="center">Antigüedad Viviendo en Domicilio</td>
                                                                            <td align="center">Teléfono</td>
                                                                            <td align="center">Referencia de Domicilio</td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td align="center" valign="top">
                                                                                <dx:ASPxComboBox ID="cbMunicipio" runat="server" DropDownStyle="DropDown" DataSourceID="SqlDataSource5" TextField="Municipio" ValueField="Municipio">
                                                                                </dx:ASPxComboBox>
                                                                            </td>
                                                                            <td align="center" valign="top">

                                                                                <font color="red">
                                                                                    <dx:ASPxSpinEdit ID="txtAntDom" runat="server" DecimalPlaces="2" MaxValue="2147483647" MinValue="1" NumberType="Integer">
                                                                                        <SpinButtons ClientVisible="False" Enabled="False">
                                                                                        </SpinButtons>
                                                                                    </dx:ASPxSpinEdit>
                                                                                </font>

                                                                            </td>
                                                                            <td align="center" valign="top">
                                                                                <font color="red">
                                                                                    <dx:ASPxSpinEdit ID="txtTel" runat="server" DecimalPlaces="2" MaxValue="21474836470" MinValue="1" NumberType="Integer">
                                                                                        <SpinButtons ClientVisible="False" Enabled="False">
                                                                                        </SpinButtons>
                                                                                    </dx:ASPxSpinEdit>
                                                                                </font>
                                                                            </td>
                                                                            <td align="center" valign="top">
                                                                                <asp:TextBox ID="txtRef" runat="server" onkeypress="textBoxOnKeyPress(event, 'ContentPlaceHolder1_ASPxRoundPanel1_txtPrestamo4');" Rows="2" TextMode="MultiLine" Width="152px"></asp:TextBox>
                                                                            </td>
                                                                        </tr>
                                                                    </tbody>
                                                                </table>
                                                            </dx:ContentControl>
                                                        </ContentCollection>
                                                    </dx:TabPage>
                                                    <dx:TabPage Name="Empleo" Text="Empleo">
                                                        <ContentCollection>
                                                            <dx:ContentControl runat="server">
                                                                <table>
                                                                    <tbody>
                                                                        <tr>
                                                                            <td align="center">Nombre de la Empresa</td>
                                                                            <td align="center">Puesto</td>
                                                                            <td align="center">Giro/Actividad</td>
                                                                            <td align="center">Sector</td>
                                                                            
                                                                        </tr>
                                                                        <tr>
                                                                            <td align="center" valign="top">
                                                                                <dx:ASPxTextBox ID="txtNomEmp" runat="server" Width="170px">
                                                                                </dx:ASPxTextBox>
                                                                            </td>
                                                                            <td align="center" valign="top">
                                                                                <dx:ASPxTextBox ID="txtPuesto" runat="server" Width="170px">
                                                                                </dx:ASPxTextBox>
                                                                            </td>
                                                                            <td align="center" valign="top">
                                                                                <dx:ASPxTextBox ID="txtGiroEmp" runat="server" Width="170px">
                                                                                </dx:ASPxTextBox>
                                                                            </td>
                                                                            <td align="center" valign="top">
                                                                                <dx:ASPxTextBox ID="txtSector" runat="server" Width="170px">
                                                                                </dx:ASPxTextBox>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td align="center">Tipo Contrato</td>
                                                                            <td align="center">Antigüedad (Años)</td>
                                                                            <td align="center">Antigüedad en Empleo Anterior (Años)</td>
                                                                            <td align="center">Teléfono</td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td align="center" valign="top">
                                                                                <dx:ASPxTextBox ID="txtTipoCont" runat="server" Width="170px">
                                                                                </dx:ASPxTextBox>
                                                                            </td>
                                                                            <td align="center" valign="top">
                                                                                <font color="red">
                                                                                    <dx:ASPxSpinEdit ID="seAntigEmp" runat="server" DecimalPlaces="2" MaxValue="2147483647" MinValue="1" NumberType="Integer">
                                                                                        <SpinButtons ClientVisible="False" Enabled="False">
                                                                                        </SpinButtons>
                                                                                    </dx:ASPxSpinEdit>
                                                                                </font>
                                                                            </td>
                                                                            <td align="center" valign="top">
                                                                                <font color="red">
                                                                                    <dx:ASPxSpinEdit ID="seAntigEmpAnt" runat="server" DecimalPlaces="2" MaxValue="2147483647" MinValue="1" NumberType="Integer">
                                                                                        <SpinButtons ClientVisible="False" Enabled="False">
                                                                                        </SpinButtons>
                                                                                    </dx:ASPxSpinEdit>
                                                                                </font>
                                                                            </td>
                                                                            <td align="center" valign="top">
                                                                                <font color="red">
                                                                                    <dx:ASPxSpinEdit ID="txtTelEmp" runat="server" DecimalPlaces="2" MaxValue="21474836470" MinValue="1" NumberType="Integer">
                                                                                        <SpinButtons ClientVisible="False" Enabled="False">
                                                                                        </SpinButtons>
                                                                                    </dx:ASPxSpinEdit>
                                                                                </font>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td align="center">Calle</td>
                                                                            <td align="center">Número Exterior</td>
                                                                            <td align="center">Número Interior</td>
                                                                            <td align="center">Colonia</td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td align="center" valign="top">
                                                                                
                                                                                <dx:ASPxTextBox ID="txtCalleEmp" runat="server" Width="170px">
                                                                                </dx:ASPxTextBox>
                                                                                
                                                                            </td>
                                                                            <td align="center" valign="top">
                                                                                
                                                                                <dx:ASPxTextBox ID="txtNumExtEmp" runat="server" Width="170px">
                                                                                </dx:ASPxTextBox>
                                                                                
                                                                            </td>
                                                                            <td align="center" valign="top">
                                                                                
                                                                                <dx:ASPxTextBox ID="txtNumIntEmp" runat="server" Width="170px">
                                                                                </dx:ASPxTextBox>
                                                                                
                                                                            </td>
                                                                            <td align="center" valign="top">
                                                                                
                                                                                <dx:ASPxTextBox ID="txtColoniaEmp" runat="server" Width="170px">
                                                                                </dx:ASPxTextBox>

                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td align="center">Código Postal</td>
                                                                            <td align="center">Delegación o Municipio</td>
                                                                            <td align="center">Ciudad</td>
                                                                            <td align="center">Estado</td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td align="center" valign="top">
                                                                                <font color="red">
                                                                                    <dx:ASPxSpinEdit ID="txtCPEmp" runat="server" DecimalPlaces="2" MaxValue="2147483647" MinValue="1" NumberType="Integer">
                                                                                        <SpinButtons ClientVisible="False" Enabled="False">
                                                                                        </SpinButtons>
                                                                                    </dx:ASPxSpinEdit>
                                                                                </font>
                                                                            </td>
                                                                            <td align="center" valign="top">

                                                                                <dx:ASPxComboBox ID="cbMunicipioEmp" runat="server" DataSourceID="SqlDataSource5" DropDownStyle="DropDown" TextField="Municipio" ValueField="Municipio">
                                                                                </dx:ASPxComboBox>
                                                                                
                                                                            </td>
                                                                            <td align="center" valign="top">
                                                                                
                                                                                <dx:ASPxComboBox ID="cbLocalidadEmp" runat="server" DataSourceID="SqlDataSource2" DropDownStyle="DropDown" TextField="Localidad" ValueField="Localidad">
                                                                                </dx:ASPxComboBox>
                                                                                
                                                                            </td>
                                                                            <td align="left" valign="top">

                                                                                <dx:ASPxComboBox ID="cbEstadoEmp" runat="server" DataSourceID="SqlDataSource4" DropDownStyle="DropDown" TextField="Estado" ValueField="Estado">
                                                                                </dx:ASPxComboBox>

                                                                            </td>
                                                                        </tr>
                                                                    </tbody>
                                                                </table>
                                                            </dx:ContentControl>
                                                        </ContentCollection>
                                                    </dx:TabPage>
                                                    <dx:TabPage Name="Negocio" Text="Negocio">
                                                        <ContentCollection>
                                                            <dx:ContentControl runat="server">
                                                                <table>
                                                                    <tbody>
                                                                        <tr>
                                                                            <td align="center">Nombre del Negocio</td>
                                                                            <td align="center">Antigüedad</td>
                                                                            <td align="center">Teléfono</td>
                                                                            <td align="center">Giro</td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td align="center" valign="top">
                                                                                <dx:ASPxTextBox ID="txtNomNeg" runat="server" Width="170px">
                                                                                </dx:ASPxTextBox>
                                                                            </td>
                                                                            <td align="center" valign="top">
                                                                                <font color="red">
                                                                                    <dx:ASPxSpinEdit ID="seAntNeg" runat="server" DecimalPlaces="2" MaxValue="2147483647" MinValue="1" NumberType="Integer" NullText="0">
                                                                                        <SpinButtons ClientVisible="False" Enabled="False">
                                                                                        </SpinButtons>
                                                                                    </dx:ASPxSpinEdit>
                                                                                </font>
                                                                            </td>
                                                                            <td align="center" valign="top">
                                                                                <font color="red">
                                                                                    <dx:ASPxSpinEdit ID="txtTelNeg" runat="server" DecimalPlaces="2" MaxValue="21474836470" MinValue="1" NumberType="Integer">
                                                                                        <SpinButtons ClientVisible="False" Enabled="False">
                                                                                        </SpinButtons>
                                                                                    </dx:ASPxSpinEdit>
                                                                                </font>
                                                                            </td>
                                                                            <td align="center" valign="top">
                                                                                <dx:ASPxTextBox ID="txtGiroNeg" runat="server" Width="170px">
                                                                                </dx:ASPxTextBox>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td align="center">Calle</td>
                                                                            <td align="center">Número Exterior</td>
                                                                            <td align="center">Número Interior</td>
                                                                            <td align="center">Colonia</td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td align="center" valign="top">
                                                                                
                                                                                <dx:ASPxTextBox ID="txtCalleNeg" runat="server" Width="170px">
                                                                                </dx:ASPxTextBox>
                                                                                
                                                                            </td>
                                                                            <td align="center" valign="top">
                                                                                
                                                                                <dx:ASPxTextBox ID="txtNumExtNeg" runat="server" Width="170px">
                                                                                </dx:ASPxTextBox>
                                                                                
                                                                            </td>
                                                                            <td align="center" valign="top">
                                                                                
                                                                                <dx:ASPxTextBox ID="txtNumIntNeg" runat="server" Width="170px">
                                                                                </dx:ASPxTextBox>
                                                                                
                                                                            </td>
                                                                            <td align="center" valign="top">
                                                                                
                                                                                <dx:ASPxTextBox ID="txtColoniaNeg" runat="server" Width="170px">
                                                                                </dx:ASPxTextBox>
                                                                                
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td align="center">Código Postal</td>
                                                                            <td align="center">Delegación o Municipio</td>
                                                                            <td align="center">Ciudad</td>
                                                                            <td align="center">Estado</td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td align="center" valign="top">
                                                                                
                                                                                <font color="red">
                                                                                <dx:ASPxSpinEdit ID="txtCPNeg" runat="server" DecimalPlaces="2" MaxValue="2147483647" MinValue="1" NumberType="Integer">
                                                                                    <SpinButtons ClientVisible="False" Enabled="False">
                                                                                    </SpinButtons>
                                                                                </dx:ASPxSpinEdit>
                                                                                </font>
                                                                                
                                                                            </td>
                                                                            <td align="center" valign="top">

                                                                                <dx:ASPxComboBox ID="cbMunicipioNeg" runat="server" DataSourceID="SqlDataSource5" DropDownStyle="DropDown" TextField="Municipio" ValueField="Municipio">
                                                                                </dx:ASPxComboBox>

                                                                            </td>
                                                                            <td align="center" valign="top">
                                                                                
                                                                                <dx:ASPxComboBox ID="cbLocalidadNeg" runat="server" DataSourceID="SqlDataSource2" DropDownStyle="DropDown" TextField="Localidad" ValueField="Localidad">
                                                                                </dx:ASPxComboBox>
                                                                                
                                                                            </td>
                                                                            <td align="center" valign="top">
                                                                                
                                                                                <dx:ASPxComboBox ID="cbEstadoNeg" runat="server" DataSourceID="SqlDataSource4" DropDownStyle="DropDown" TextField="Estado" ValueField="Estado">
                                                                                </dx:ASPxComboBox>
                                                                                
                                                                            </td>
                                                                        </tr>
                                                                    </tbody>
                                                                </table>
                                                            </dx:ContentControl>
                                                        </ContentCollection>
                                                    </dx:TabPage>
                                                    <dx:TabPage Name="Economica" Text="Económica">
                                                        <ContentCollection>
                                                            <dx:ContentControl runat="server">
                                                                <table>
                                                                    <tbody>
                                                                        <tr>
                                                                            <td colspan="5" align="center">Ingresos</td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td align="center">Empleo</td>
                                                                            <td align="center">Negocio</td>
                                                                            <td align="center">Cónyuge</td>
                                                                            <td align="center">Apoyos</td>
                                                                            <td align="center">Otros</td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td>

                                                                                <dx:ASPxSpinEdit ID="seEmpleo" runat="server" AllowNull="False" DecimalPlaces="2" DisplayFormatString="{0:$00.00}" Number="0">
                                                                                    <SpinButtons ClientVisible="False" Enabled="False">
                                                                                    </SpinButtons>
                                                                                </dx:ASPxSpinEdit>

                                                                            </td>
                                                                            <td>

                                                                                <dx:ASPxSpinEdit ID="seNegocio" runat="server" DecimalPlaces="2" DisplayFormatString="{0:$00.00}" Number="0">
                                                                                    <SpinButtons ClientVisible="False" Enabled="False">
                                                                                    </SpinButtons>
                                                                                </dx:ASPxSpinEdit>

                                                                            </td>
                                                                            <td>

                                                                                <dx:ASPxSpinEdit ID="seConyuge" runat="server" DecimalPlaces="2" DisplayFormatString="{0:$00.00}" Number="0">
                                                                                    <SpinButtons ClientVisible="False" Enabled="False">
                                                                                    </SpinButtons>
                                                                                </dx:ASPxSpinEdit>

                                                                            </td>
                                                                            <td>

                                                                                <dx:ASPxSpinEdit ID="seApoyos" runat="server" DecimalPlaces="2" DisplayFormatString="{0:$00.00}" Number="0">
                                                                                    <SpinButtons ClientVisible="False" Enabled="False">
                                                                                    </SpinButtons>
                                                                                </dx:ASPxSpinEdit>

                                                                            </td>
                                                                            <td>

                                                                                <dx:ASPxSpinEdit ID="seIngrOtros" runat="server" DecimalPlaces="2" DisplayFormatString="{0:$00.00}" Number="0">
                                                                                    <SpinButtons ClientVisible="False" Enabled="False">
                                                                                    </SpinButtons>
                                                                                </dx:ASPxSpinEdit>

                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td colspan="5" align="center">Egresos</td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td align="center">Renta</td>
                                                                            <td align="center">Servicios</td>
                                                                            <td align="center">Gastos Familiares</td>
                                                                            <td align="center">Créditos</td>
                                                                            <td align="center">Otros</td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td>

                                                                                <dx:ASPxSpinEdit ID="seRenta" runat="server" AllowNull="False" DecimalPlaces="2" DisplayFormatString="{0:$00.00}" Number="0">
                                                                                    <SpinButtons ClientVisible="False" Enabled="False">
                                                                                    </SpinButtons>
                                                                                </dx:ASPxSpinEdit>

                                                                            </td>
                                                                            <td>

                                                                                <dx:ASPxSpinEdit ID="seServicios" runat="server" AllowNull="False" DecimalPlaces="2" DisplayFormatString="{0:$00.00}" Number="0">
                                                                                    <SpinButtons ClientVisible="False" Enabled="False">
                                                                                    </SpinButtons>
                                                                                </dx:ASPxSpinEdit>

                                                                            </td>
                                                                            <td>

                                                                                <dx:ASPxSpinEdit ID="seFamiliares" runat="server" AllowNull="False" DecimalPlaces="2" DisplayFormatString="{0:$00.00}" Number="0">
                                                                                    <SpinButtons ClientVisible="False" Enabled="False">
                                                                                    </SpinButtons>
                                                                                </dx:ASPxSpinEdit>

                                                                            </td>
                                                                            <td>

                                                                                <dx:ASPxSpinEdit ID="seCredito" runat="server" AllowNull="False" DecimalPlaces="2" DisplayFormatString="{0:$00.00}" Number="0">
                                                                                    <SpinButtons ClientVisible="False" Enabled="False">
                                                                                    </SpinButtons>
                                                                                </dx:ASPxSpinEdit>

                                                                            </td>
                                                                            <td>

                                                                                <dx:ASPxSpinEdit ID="seEgrOtros" runat="server" AllowNull="False" DecimalPlaces="2" DisplayFormatString="{0:$00.00}" Number="0">
                                                                                    <SpinButtons ClientVisible="False" Enabled="False">
                                                                                    </SpinButtons>
                                                                                </dx:ASPxSpinEdit>

                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td colspan="5" align="center">Dependientes Económicos</td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td align="center"></td>
                                                                            <td align="center">Parentesco</td>
                                                                            <td align="center">Edad</td>
                                                                            <td align="center">Ocupación</td>
                                                                            <td align="center"></td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td>

                                                                            </td>
                                                                            <td>

                                                                                <dx:ASPxTextBox ID="txtDepEcoParent1" runat="server" Width="170px">
                                                                                </dx:ASPxTextBox>

                                                                            </td>
                                                                            <td>

                                                                                <dx:ASPxSpinEdit ID="seDepEcoEdad1" runat="server" AllowNull="False" DecimalPlaces="0" NumberType="Integer">
                                                                                    <SpinButtons ClientVisible="False" Enabled="False">
                                                                                    </SpinButtons>
                                                                                </dx:ASPxSpinEdit>

                                                                            </td>
                                                                            <td>

                                                                                <dx:ASPxTextBox ID="txtDepEcoOcup1" runat="server" Width="170px">
                                                                                </dx:ASPxTextBox>

                                                                            </td>
                                                                            <td>

                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td align="center"></td>
                                                                            <td align="center">Parentesco</td>
                                                                            <td align="center">Edad</td>
                                                                            <td align="center">Ocupación</td>
                                                                            <td align="center"></td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td>

                                                                            </td>
                                                                            <td>

                                                                                <dx:ASPxTextBox ID="txtDepEcoParent2" runat="server" Width="170px">
                                                                                </dx:ASPxTextBox>

                                                                            </td>
                                                                            <td>

                                                                                <dx:ASPxSpinEdit ID="seDepEcoEdad2" runat="server" AllowNull="False" DecimalPlaces="0" NumberType="Integer">
                                                                                    <SpinButtons ClientVisible="False" Enabled="False">
                                                                                    </SpinButtons>
                                                                                </dx:ASPxSpinEdit>

                                                                            </td>
                                                                            <td>

                                                                                <dx:ASPxTextBox ID="txtDepEcoOcup2" runat="server" Width="170px">
                                                                                </dx:ASPxTextBox>

                                                                            </td>
                                                                            <td>

                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td align="center"></td>
                                                                            <td align="center">Parentesco</td>
                                                                            <td align="center">Edad</td>
                                                                            <td align="center">Ocupación</td>
                                                                            <td align="center"></td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td>

                                                                            </td>
                                                                            <td>

                                                                                <dx:ASPxTextBox ID="txtDepEcoParent3" runat="server" Width="170px">
                                                                                </dx:ASPxTextBox>

                                                                            </td>
                                                                            <td>

                                                                                <dx:ASPxSpinEdit ID="seDepEcoEdad3" runat="server" AllowNull="False" DecimalPlaces="0" NumberType="Integer">
                                                                                    <SpinButtons ClientVisible="False" Enabled="False">
                                                                                    </SpinButtons>
                                                                                </dx:ASPxSpinEdit>

                                                                            </td>
                                                                            <td>

                                                                                <dx:ASPxTextBox ID="txtDepEcoOcup3" runat="server" Width="170px">
                                                                                </dx:ASPxTextBox>

                                                                            </td>
                                                                            <td>

                                                                            </td>
                                                                        </tr>
                                                                    </tbody>
                                                                </table>
                                                            </dx:ContentControl>
                                                        </ContentCollection>
                                                    </dx:TabPage>
                                                    <dx:TabPage Name="Referencias" Text="Referencias Personales">
                                                        <ContentCollection>
                                                            <dx:ContentControl runat="server">
                                                                <table>
                                                                    <tbody>
                                                                        <tr>
                                                                            <td align="center">Nombre(s)</td>
                                                                            <td align="center">Apellido Paterno</td>
                                                                            <td align="center">Apellido Materno</td>
                                                                            <td align="center">Télefono</td>
                                                                            <td align="center">Tipo de Referencia</td>
                                                                            <td align="center">Dirección</td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td>
                                                                                <dx:ASPxTextBox ID="txtNomRef1" runat="server" Width="170px" AutoPostBack="True">
                                                                                </dx:ASPxTextBox>
                                                                            </td>
                                                                            <td>
                                                                                <dx:ASPxTextBox ID="txtPrimApeRef1" runat="server" Width="170px" AutoPostBack="True">
                                                                                </dx:ASPxTextBox>
                                                                            </td>
                                                                            <td>
                                                                                <dx:ASPxTextBox ID="txtSegApeRef1" runat="server" Width="170px" AutoPostBack="True">
                                                                                </dx:ASPxTextBox>
                                                                            </td>
                                                                            <td>
                                                                                <dx:ASPxTextBox ID="txtTelRef1" runat="server" Width="170px" AutoPostBack="True">
                                                                                </dx:ASPxTextBox>
                                                                            </td>
                                                                            <td>
                                                                                <dx:ASPxTextBox ID="txtTipRef1" runat="server" Width="170px" AutoPostBack="True">
                                                                                </dx:ASPxTextBox>
                                                                            </td>
                                                                            <td>
                                                                                <asp:TextBox ID="txtDirRef1" runat="server" Rows="2" TextMode="MultiLine" Width="152px"></asp:TextBox>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td align="center">Nombre(s)</td>
                                                                            <td align="center">Apellido Paterno</td>
                                                                            <td align="center">Apellido Materno</td>
                                                                            <td align="center">Télefono</td>
                                                                            <td align="center">Tipo de Referencia</td>
                                                                            <td align="center">Dirección</td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td>
                                                                                <dx:ASPxTextBox ID="txtNomRef2" runat="server" Width="170px" AutoPostBack="True">
                                                                                </dx:ASPxTextBox>
                                                                            </td>
                                                                            <td>
                                                                                <dx:ASPxTextBox ID="txtPrimApeRef2" runat="server" Width="170px" AutoPostBack="True">
                                                                                </dx:ASPxTextBox>
                                                                            </td>
                                                                            <td>
                                                                                <dx:ASPxTextBox ID="txtSegApeRef2" runat="server" Width="170px" AutoPostBack="True">
                                                                                </dx:ASPxTextBox>
                                                                            </td>
                                                                            <td>
                                                                                <dx:ASPxTextBox ID="txtTelRef2" runat="server" Width="170px" AutoPostBack="True">
                                                                                </dx:ASPxTextBox>
                                                                            </td>
                                                                            <td>
                                                                                <dx:ASPxTextBox ID="txtTipRef2" runat="server" Width="170px" AutoPostBack="True">
                                                                                </dx:ASPxTextBox>
                                                                            </td>
                                                                            <td>
                                                                                <asp:TextBox ID="txtDirRef2" runat="server" Rows="2" TextMode="MultiLine" Width="152px"></asp:TextBox>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td align="center">Nombre(s)</td>
                                                                            <td align="center">Apellido Paterno</td>
                                                                            <td align="center">Apellido Materno</td>
                                                                            <td align="center">Télefono</td>
                                                                            <td align="center">Tipo de Referencia</td>
                                                                            <td align="center">Dirección</td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td>
                                                                                <dx:ASPxTextBox ID="txtNomRef3" runat="server" Width="170px" AutoPostBack="True">
                                                                                </dx:ASPxTextBox>
                                                                            </td>
                                                                            <td>
                                                                                <dx:ASPxTextBox ID="txtPrimApeRef3" runat="server" Width="170px" AutoPostBack="True">
                                                                                </dx:ASPxTextBox>
                                                                            </td>
                                                                            <td>
                                                                                <dx:ASPxTextBox ID="txtSegApeRef3" runat="server" Width="170px" AutoPostBack="True">
                                                                                </dx:ASPxTextBox>
                                                                            </td>
                                                                            <td>
                                                                                <dx:ASPxTextBox ID="txtTelRef3" runat="server" Width="170px" AutoPostBack="True">
                                                                                </dx:ASPxTextBox>
                                                                            </td>
                                                                            <td>
                                                                                <dx:ASPxTextBox ID="txtTipRef3" runat="server" Width="170px" AutoPostBack="True">
                                                                                </dx:ASPxTextBox>
                                                                            </td>
                                                                            <td>
                                                                                <asp:TextBox ID="txtDirRef3" runat="server" Rows="2" TextMode="MultiLine" Width="152px"></asp:TextBox>
                                                                            </td>
                                                                            <tr>
                                                                            <td align="center">Nombre(s)</td>
                                                                            <td align="center">Apellido Paterno</td>
                                                                            <td align="center">Apellido Materno</td>
                                                                            <td align="center">Télefono</td>
                                                                            <td align="center">Tipo de Referencia</td>
                                                                            <td align="center">Dirección</td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td>
                                                                                <dx:ASPxTextBox ID="txtNomRef4" runat="server" Width="170px" AutoPostBack="True">
                                                                                </dx:ASPxTextBox>
                                                                            </td>
                                                                            <td>
                                                                                <dx:ASPxTextBox ID="txtPrimApeRef4" runat="server" Width="170px" AutoPostBack="True">
                                                                                </dx:ASPxTextBox>
                                                                            </td>
                                                                            <td>
                                                                                <dx:ASPxTextBox ID="txtSegApeRef4" runat="server" Width="170px" AutoPostBack="True">
                                                                                </dx:ASPxTextBox>
                                                                            </td>
                                                                            <td>
                                                                                <dx:ASPxTextBox ID="txtTelRef4" runat="server" Width="170px" AutoPostBack="True">
                                                                                </dx:ASPxTextBox>
                                                                            </td>
                                                                            <td>
                                                                                <dx:ASPxTextBox ID="txtTipRef4" runat="server" Width="170px" AutoPostBack="True">
                                                                                </dx:ASPxTextBox>
                                                                            </td>
                                                                            <td>
                                                                                <asp:TextBox ID="txtDirRef4" runat="server" Rows="2" TextMode="MultiLine" Width="152px"></asp:TextBox>
                                                                            </td>
                                                                        </tr>
                                                                    </tbody>
                                                                </table>
                                                            </dx:ContentControl>
                                                        </ContentCollection>
                                                    </dx:TabPage>
                                                </TabPages>
                                                <TabStyle HorizontalAlign="Center">
                                                </TabStyle>
                                            </dx:ASPxPageControl>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="8">
                                            <asp:SqlDataSource ID="SqlDataSource2" runat="server" ConnectionString="<%$ ConnectionStrings:FinanciaDinConnectionString %>" SelectCommand="SELECT DISTINCT [Localidad] FROM [CLIENTES]"></asp:SqlDataSource>
                                            <asp:SqlDataSource ID="SqlDataSource4" runat="server" ConnectionString="<%$ ConnectionStrings:FinanciaDinConnectionString %>" SelectCommand="SELECT DISTINCT [EstadoDom] As Estado FROM [CLIENTES]"></asp:SqlDataSource>
                                            <asp:SqlDataSource ID="SqlDataSource5" runat="server" ConnectionString="<%$ ConnectionStrings:FinanciaDinConnectionString %>" SelectCommand="SELECT DISTINCT [MunicipioDom] As Municipio FROM [CLIENTES]"></asp:SqlDataSource>
                                            <br />
                                            <br />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="center" colspan="5"><font color="red">
                                            <dx:ASPxButton ID="btnLimpiar" runat="server" CausesValidation="False" Text="Limpiar" UseSubmitBehavior="False">
                                                <Paddings PaddingTop="5px" />
                                            </dx:ASPxButton>
                                        </font></td>
                                        <td align="center" colspan="4"><font color="red">
                                            <dx:ASPxButton ID="btnGuardar" runat="server" CausesValidation="False" Text="Guardar" UseSubmitBehavior="False">
                                                <Paddings PaddingTop="5px" />
                                            </dx:ASPxButton>
                                        </font></td>
                                    </tr>
                                    <tr>
                                        <td align="center" colspan="8">
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
            </ContentTemplate>
        </asp:UpdatePanel>
        <br />
        <dx:ASPxGridView ID="gvClientes" runat="server" AutoGenerateColumns="False" DataSourceID="SqlDataSource1" KeyFieldName="Id" EnableCallBacks="False">
            <SettingsBehavior AllowSelectByRowClick="True" AllowSelectSingleRowOnly="True" ProcessSelectionChangedOnServer="True" />
            <SettingsSearchPanel AllowTextInputTimer="False" Delay="1000" Visible="True" />
            <Columns>
                <dx:GridViewCommandColumn SelectAllCheckboxMode="Page" ShowSelectCheckbox="True" VisibleIndex="0">
                    <HeaderStyle Wrap="True" />
                </dx:GridViewCommandColumn>
                <dx:GridViewDataTextColumn FieldName="Id" VisibleIndex="1" ReadOnly="True" Caption="Id" Visible="False">
                    <HeaderStyle Wrap="True" HorizontalAlign="Center" />
                    <CellStyle HorizontalAlign="Center">
                    </CellStyle>
                </dx:GridViewDataTextColumn>
                <dx:GridViewDataTextColumn FieldName="Tipo" VisibleIndex="8">
                    <HeaderStyle Wrap="True" HorizontalAlign="Center" />
                    <CellStyle HorizontalAlign="Center">
                    </CellStyle>
                </dx:GridViewDataTextColumn>
                <dx:GridViewDataTextColumn FieldName="RFC" VisibleIndex="9">
                    <HeaderStyle Wrap="True" HorizontalAlign="Center" />
                    <CellStyle HorizontalAlign="Center">
                    </CellStyle>
                </dx:GridViewDataTextColumn>
                <dx:GridViewDataTextColumn FieldName="CURP" VisibleIndex="10">
                    <HeaderStyle Wrap="True" HorizontalAlign="Center" />
                    <CellStyle HorizontalAlign="Center">
                    </CellStyle>
                </dx:GridViewDataTextColumn>
                <dx:GridViewDataTextColumn FieldName="Referencia" VisibleIndex="17">
                    <HeaderStyle Wrap="True" HorizontalAlign="Center" />
                    <CellStyle HorizontalAlign="Center">
                    </CellStyle>
                </dx:GridViewDataTextColumn>
                <dx:GridViewDataDateColumn FieldName="FecNac" VisibleIndex="22" Caption="Fecha Nacimiento">
                    <HeaderStyle Wrap="True" HorizontalAlign="Center" />
                    <CellStyle HorizontalAlign="Center">
                    </CellStyle>
                </dx:GridViewDataDateColumn>
                <dx:GridViewDataTextColumn FieldName="EMail" VisibleIndex="25" Caption="Email">
                    <HeaderStyle Wrap="True" HorizontalAlign="Center" />
                    <CellStyle HorizontalAlign="Center">
                    </CellStyle>
                </dx:GridViewDataTextColumn>
                <dx:GridViewDataTextColumn FieldName="Celular" VisibleIndex="23">
                    <HeaderStyle Wrap="True" HorizontalAlign="Center" />
                    <CellStyle HorizontalAlign="Center">
                    </CellStyle>
                </dx:GridViewDataTextColumn>
                <dx:GridViewDataTextColumn FieldName="Nacionalidad" VisibleIndex="26" ReadOnly="True">
                    <HeaderStyle Wrap="True" HorizontalAlign="Center" />
                    <CellStyle HorizontalAlign="Center">
                    </CellStyle>
                </dx:GridViewDataTextColumn>
                <dx:GridViewDataTextColumn FieldName="Ocupacion" VisibleIndex="28" ReadOnly="True" Caption="Ocupación">
                    <HeaderStyle Wrap="True" HorizontalAlign="Center" />
                    <CellStyle HorizontalAlign="Center">
                    </CellStyle>
                </dx:GridViewDataTextColumn>
                <dx:GridViewDataCheckColumn FieldName="EsAval" VisibleIndex="31" Caption="Es Aval?">
                    <HeaderStyle Wrap="True" HorizontalAlign="Center" />
                    <CellStyle HorizontalAlign="Center">
                    </CellStyle>
                </dx:GridViewDataCheckColumn>
                <dx:GridViewDataTextColumn FieldName="Nombre" VisibleIndex="2">
                    <FilterCellStyle Wrap="False">
                    </FilterCellStyle>
                    <HeaderStyle HorizontalAlign="Center" Wrap="True" />
                    <CellStyle HorizontalAlign="Center" Wrap="False">
                    </CellStyle>
                </dx:GridViewDataTextColumn>
                <dx:GridViewDataColumn Caption="Identificación" VisibleIndex="34">
                    <DataItemTemplate>
                        <asp:LinkButton ID="descargarId" runat="server" Text="Descargar" OnClick="DescargarIdentificacion" CommandArgument='<%# Eval("Id") %>'></asp:LinkButton>
                    </DataItemTemplate>
                    <HeaderStyle Wrap="True" />
                </dx:GridViewDataColumn>
                <dx:GridViewDataColumn Caption="Comprobante Domicilio" VisibleIndex="35">
                    <DataItemTemplate>
                        <asp:LinkButton ID="descargarComp" runat="server" Text="Descargar" OnClick="DescargarComprobante" CommandArgument='<%# Eval("Id") %>'></asp:LinkButton>
                    </DataItemTemplate>
                    <HeaderStyle Wrap="True" />
                </dx:GridViewDataColumn>
                <dx:GridViewDataColumn Caption="Solicitud Buro" VisibleIndex="36">
                    <DataItemTemplate>
                        <asp:LinkButton ID="descargarSol" runat="server" Text="Descargar" OnClick="DescargarSolBuro" CommandArgument='<%# Eval("Id") %>'></asp:LinkButton>
                    </DataItemTemplate>
                    <HeaderStyle Wrap="True" />
                </dx:GridViewDataColumn>
                <dx:GridViewDataTextColumn FieldName="IngresoMensual" VisibleIndex="29">
                    <HeaderStyle Wrap="True" />
                </dx:GridViewDataTextColumn>
            </Columns>
            <SettingsPager PageSize="15">
            </SettingsPager>
            <Styles>
                <FilterBar HorizontalAlign="Center" VerticalAlign="Middle">
                </FilterBar>
                <HeaderFilterItem HorizontalAlign="Center" VerticalAlign="Middle">
                </HeaderFilterItem>
                <SearchPanel HorizontalAlign="Center" VerticalAlign="Middle">
                </SearchPanel>
            </Styles>
        </dx:ASPxGridView>
        <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:FinanciaDinConnectionString %>" SelectCommand="SELECT     C.Id, C.PrimNombre + ' ' + C.SegNombre + ' ' + C.PrimApellido + ' ' + C.SegApellido AS Nombre, 
                  CATTIPOCLIENTES.Descripcion AS Tipo, C.RFC, C.CURP, C.ReferenciaDom As Referencia, C.FecNac, C.EMail, C.Celular,                            ISNULL(C.Nacionalidad, '') AS Nacionalidad,  ISNULL(C.Ocupacion, '') AS Ocupacion, C.EsAval,                                      C.IngresoMensual
FROM         CLIENTES AS C INNER JOIN
                      CATTIPOCLIENTES ON CATTIPOCLIENTES.Id = C.Tipo"></asp:SqlDataSource>
        <br />
        <br />
    </center>
    <script type="text/javascript">
        setInterval('MantenSesion()', <%= (Int(0.9 * (Session.Timeout * 60000)))%>);
    </script>
</asp:Content>
