<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site.Master"
    CodeBehind="Catalogos.aspx.vb" Inherits="FinanciaDin.Catalogos" %>

<%@ Register Assembly="DevExpress.Web.v15.2, Version=15.2.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web" TagPrefix="dx" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
    <style type="text/css">
        .auto-style2 {
            margin-left: 0px;
        }
        </style>
    </asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <br />
    <br />
    <center>
        <dx:ASPxRoundPanel ID="ASPxRoundPanel1" runat="server" HeaderText="Mantenimiento de Catálogos"
            Theme="DevEx">
            <PanelCollection>
                <dx:PanelContent>
                    <table width="100%">
                        <tr>
                            <td colspan="3" align="center">
                                Seleccione un Catálogo:</td>
                        </tr>
                        <tr>
                            <td colspan="3" align="center">
                                <asp:DropDownList ID="ddlCatalogo" runat="server" CssClass="auto-style2" Height="20px" Width="154px" AutoPostBack="True">
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="3" align="center">
                                <dx:ASPxFormLayout ID="ASPxFormLayout1" runat="server">
                                    <Items>
                                        <dx:LayoutItem Caption="ID:" Name="lblId">
                                            <LayoutItemNestedControlCollection>
                                                <dx:LayoutItemNestedControlContainer runat="server" SupportsDisabledAttribute="True">
                                                    <dx:ASPxLabel ID="lblID" runat="server" EnableTheming="True" Theme="SoftOrange">
                                                    </dx:ASPxLabel>
                                                </dx:LayoutItemNestedControlContainer>
                                            </LayoutItemNestedControlCollection>
                                        </dx:LayoutItem>
                                        <dx:LayoutItem Caption="Descripcion:" Name="txtDescrip">
                                            <LayoutItemNestedControlCollection>
                                                <dx:LayoutItemNestedControlContainer runat="server" SupportsDisabledAttribute="True">
                                                    <dx:ASPxTextBox ID="txtDescripcion" runat="server" Width="170px">
                                                    </dx:ASPxTextBox>
                                                </dx:LayoutItemNestedControlContainer>
                                            </LayoutItemNestedControlCollection>
                                        </dx:LayoutItem>
                                        <dx:LayoutItem Caption="Nombre:" Name="txtNombre" ClientVisible="False">
                                            <LayoutItemNestedControlCollection>
                                                <dx:LayoutItemNestedControlContainer runat="server">
                                                    <dx:ASPxTextBox ID="txtNombre" runat="server">
                                                    </dx:ASPxTextBox>
                                                </dx:LayoutItemNestedControlContainer>
                                            </LayoutItemNestedControlCollection>
                                        </dx:LayoutItem>
                                        <dx:LayoutItem Caption="Valor:" ClientVisible="False" Name="txtValor">
                                            <LayoutItemNestedControlCollection>
                                                <dx:LayoutItemNestedControlContainer runat="server">
                                                    <dx:ASPxTextBox ID="txtValor" runat="server">
                                                    </dx:ASPxTextBox>
                                                </dx:LayoutItemNestedControlContainer>
                                            </LayoutItemNestedControlCollection>
                                        </dx:LayoutItem>
                                        <dx:LayoutItem Caption="Fecha Inhábil" ClientVisible="False" Name="deFechaInhab">
                                            <LayoutItemNestedControlCollection>
                                                <dx:LayoutItemNestedControlContainer runat="server">
                                                    <dx:ASPxDateEdit ID="deFechaInhab" runat="server">
                                                    </dx:ASPxDateEdit>
                                                </dx:LayoutItemNestedControlContainer>
                                            </LayoutItemNestedControlCollection>
                                        </dx:LayoutItem>
                                        <dx:LayoutItem Caption="Dirección" ClientVisible="False" Name="txtDir">
                                            <LayoutItemNestedControlCollection>
                                                <dx:LayoutItemNestedControlContainer runat="server">
                                                    <dx:ASPxTextBox ID="txtDir" runat="server">
                                                    </dx:ASPxTextBox>
                                                </dx:LayoutItemNestedControlContainer>
                                            </LayoutItemNestedControlCollection>
                                        </dx:LayoutItem>
                                        <dx:LayoutItem Caption="Telefono" ClientVisible="False" Name="seTel">
                                            <LayoutItemNestedControlCollection>
                                                <dx:LayoutItemNestedControlContainer runat="server">
                                                    <dx:ASPxSpinEdit ID="seTel" runat="server" NumberType="Integer">
                                                        <SpinButtons ClientVisible="False" Enabled="False">
                                                        </SpinButtons>
                                                    </dx:ASPxSpinEdit>
                                                </dx:LayoutItemNestedControlContainer>
                                            </LayoutItemNestedControlCollection>
                                        </dx:LayoutItem>
                                    </Items>
                                </dx:ASPxFormLayout>
                            </td>
                        </tr>
                        <tr>
                            <td align="center">
                                <asp:ImageButton ID="imgLimpiar" runat="server" BorderStyle="None" CausesValidation="False"
                                    Height="30px" ImageUrl="~/Images/new_file.ico" Width="30px" />
                            </td>
                            <td align="center">
                                <asp:ImageButton ID="imgGuardar" runat="server" BorderStyle="None" Height="30px"
                                    ImageUrl="~/Images/add.ico" Width="30px" />
                            </td>
                            <td align="center">
                                <asp:ImageButton ID="imgEliminar" runat="server" BorderStyle="None" CausesValidation="False"
                                    Height="30px" ImageUrl="~/Images/Stop 2.ico" OnClientClick="return confirm(&quot;Favor de Aceptar si realmente desea eliminar el registro?&quot;);"
                                    Width="30px" />
                            </td>
                        </tr>
                        <tr>
                            <td align="center">Limpiar
                            </td>
                            <td align="center">
                                <dx:ASPxLabel ID="lblAccion" runat="server" Text="Guardar">
                                </dx:ASPxLabel>
                            </td>
                            <td align="center">Eliminar
                            </td>
                        </tr>
                        <tr>
                            <td align="center" colspan="3">
                                <font color="red">
                                    <asp:Literal ID="Literal1" runat="server"></asp:Literal>
                                </font>
                            </td>
                        </tr>
                    </table>
                    <br />
                    <br />
                    <div align="center">
                        <asp:GridView ID="grdCatalogo" runat="server" Caption="Catalogo" CellPadding="4" EmptyDataText="No hay registros para mostrar" ForeColor="Black" AutoGenerateColumns="True" AutoGenerateSelectButton="True" BackColor="#CCCCCC" BorderColor="#999999" BorderStyle="Solid" BorderWidth="3px" CellSpacing="2">
                            <FooterStyle BackColor="#CCCCCC" />
                            <HeaderStyle BackColor="Black" Font-Bold="True" ForeColor="White" />
                            <PagerStyle BackColor="#CCCCCC" ForeColor="Black" HorizontalAlign="Left" />
                            <RowStyle BackColor="White" Wrap="True" />
                            <SelectedRowStyle BackColor="#000099" Font-Bold="True" ForeColor="White" />
                            <SortedAscendingCellStyle BackColor="#F1F1F1" />
                            <SortedAscendingHeaderStyle BackColor="Gray" />
                            <SortedDescendingCellStyle BackColor="#CAC9C9" />
                            <SortedDescendingHeaderStyle BackColor="#383838" />
                            <SortedAscendingCellStyle BackColor="#F8FAFA"></SortedAscendingCellStyle>
                            <SortedAscendingHeaderStyle BackColor="#246B61"></SortedAscendingHeaderStyle>
                            <SortedDescendingCellStyle BackColor="#D4DFE1"></SortedDescendingCellStyle>
                            <SortedDescendingHeaderStyle BackColor="#15524A"></SortedDescendingHeaderStyle>
                        </asp:GridView>
                    </div>
                </dx:PanelContent>
            </PanelCollection>
        </dx:ASPxRoundPanel>
    </center>
    <script type="text/javascript">
        setInterval('MantenSesion()', <%= (Int(0.9 * (Session.Timeout * 60000)))%>);
    </script>
</asp:Content>
