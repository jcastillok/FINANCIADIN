<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site.Master" CodeBehind="CapturaPagoEmp.aspx.vb" Inherits="FinanciaDin.CapturaPagoEmp" %>


<%@ Register Assembly="DevExpress.Web.ASPxHtmlEditor.v15.2, Version=15.2.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web.ASPxHtmlEditor" TagPrefix="dx" %>

<%@ Register Assembly="DevExpress.Web.v15.2, Version=15.2.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web" TagPrefix="dx" %>
<%@ Register assembly="DevExpress.Web.ASPxPivotGrid.v15.2, Version=15.2.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.Web.ASPxPivotGrid" tagprefix="dx" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
     <link href="../Style/Plantilla/ionicons.css" rel="stylesheet" />
    
    <link href="../Style/Plantilla/font-awesome/css/font-awesome.css" rel="stylesheet" />
    <link href="../Style/Plantilla/AdminLTE.css" rel="stylesheet" />
    <link href="../Style/Plantilla/_all-skins.css" rel="stylesheet" />

    <script src="../Scripts/jquery-3.4.1.js"></script>
    <script src="../Scripts/bootstrap.js"></script>
    <script src="../Scripts/adminlte.js"></script>
    <script src="../Scripts/demo.js"></script>


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

     <div class="content">


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
                                        <dx:ASPxButton ID="Cancelarcon" runat="server" CausesValidation="False" Text="Cancelar" UseSubmitBehavior="False" ValidationGroup="Adeudo">
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
            <!-- Contenedor 1 del formulario-->

            <div class="box box-success">
                <!-- contenedor 2 --->
                <div class="box-header">
                    <i class="fa fa-money" aria-hidden="true"></i>

                    <h3 class="box-title">CAPTURA  DE PAGOS</h3>

                </div>

                <div class="box-body chat" id="chat-box">
                    <!-- chat item -->

                    <div class="col-md-3">
                        <div class="form-group" id="id">
                            <label for="IdRelacion" class="control-label">Cliente</label>



                            <dx:ASPxComboBox ID="cbClientes" runat="server" CssClass="form-control " DropDownStyle="DropDown" DataSourceID="SqlDataSource2" TextField="Nombre" ValueField="Id_Cliente" AutoPostBack="True">
                            </dx:ASPxComboBox>
                            <asp:SqlDataSource ID="SqlDataSource2" runat="server" ConnectionString="<%$ ConnectionStrings:FinanciaDinConnectionString %>" SelectCommand="SELECT REPLACE(CLI.PrimNombre + ' ' + IsNull(CLI.SegNombre,'') + ' ' + CLI.PrimApellido + ' ' + IsNull(CLI.SegApellido,''), '  ', ' ') AS Nombre,  C.Id_Cliente
FROM CREDITOS AS C INNER JOIN 
CLIENTES AS CLI ON CLI.ID = C.ID_CLIENTE
INNER JOIN SOLICITUDES S ON C.Id_Sol = S.Id 
WHERE C.Liquidado = 0  AND C.estatusID = 1 AND 
S.TipoProducto = 'CREDITO ELITE'
ORDER BY Nombre "></asp:SqlDataSource>

                        </div>
                    </div>

                    <div class="col-md-3">
                        <div class="form-group">
                            <label>
                                Crédito
                            </label>
                            <dx:ASPxComboBox ID="cbIdCredito" runat="server" CssClass="form-control" Enabled="false" AutoPostBack="True" DataSourceID="SqlDataSource1" DropDownRows="8" DropDownStyle="DropDown" TextField="ID" ValueField="ID_CLIENTE">
                                <ListBoxStyle Wrap="False">
                                </ListBoxStyle>
                            </dx:ASPxComboBox>
                            <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:FinanciaDinConnectionString %>" SelectCommand="SELECT C.ID, C.ID_CLIENTE
FROM CREDITOS AS C 
INNER JOIN SOLICITUDES S ON C.Id_Sol = S.Id 
WHERE S.TipoProducto = 'CREDITO ELITE' AND  C.Liquidado = 0  AND C.estatusID = 1"></asp:SqlDataSource>

                        </div>
                    </div>

                    <div class="col-md-3">
                        <div class="form-group">
                            <label>
                                Fecha Captura
                            </label>


                            <dx:ASPxDateEdit CssClass="form-control" ID="deFecCap" runat="server">
                            </dx:ASPxDateEdit>

                        </div>
                    </div>



                    <div class="col-md-3">
                        <div class="form-group">
                            <label>
                                Sucursal
                            </label>
                            <dx:ASPxComboBox ID="cbSuc" CssClass="form-control" runat="server" DataSourceID="SqlDataSource3" DropDownStyle="DropDown" TextField="Descripcion" ValueField="Id">
                            </dx:ASPxComboBox>
                            <asp:SqlDataSource ID="SqlDataSource3" runat="server" ConnectionString="<%$ ConnectionStrings:FinanciaDinConnectionString %>" SelectCommand="SELECT [Id], [Descripcion] FROM [CATSUCURSALES]" CacheDuration="10000"></asp:SqlDataSource>


                        </div>
                    </div>

                   
                    <div class="box-footer clearfix">
                    </div>


                    <div class="col-md-11">

                        <div class="col-md-2">
                            <div class="form-group">
                                <label>
                                    # Pago
                                </label>
                                
                                 <dx:ASPxTextBox ID="lblPago" CssClass ="form-control" Border-BorderColor="White" Enabled="false"  runat="server"></dx:ASPxTextBox>


                            </div>
                        </div>

                        <div class="col-md-2">
                            <div class="form-group">
                                <label>
                                    Monto Deuda $
                                </label>
                               

                                <dx:ASPxTextBox ID="montoDeuda" CssClass ="form-control" Border-BorderColor="White" Enabled="false"  runat="server"></dx:ASPxTextBox>

                            </div>
                        </div>

                        <div class="col-md-2">
                            <div class="form-group">
                                <label>
                                    Monto Capital $
                                </label>


                                <dx:ASPxTextBox ID="txtMontiCapital" CssClass="form-control" Border-BorderColor="White" Enabled="false" runat="server"></dx:ASPxTextBox>

                            </div>
                        </div>

                          <div class="col-md-2">
                            <div class="form-group">
                                <label>
                                    Monto Interés $
                                </label>


                                <dx:ASPxTextBox ID="txtMontoInteres" CssClass="form-control" Border-BorderColor="White" Enabled="false" runat="server"></dx:ASPxTextBox>

                            </div>
                        </div>

                        <asp:Panel CssClass="col-md-2" ID="panelRecargo" Visible="false" runat="server">
                            <div class="form-group">
                                <label>
                                    Monto Recargo $
                                </label>
                               

                                <dx:ASPxTextBox ID="TxtmontoRecargo" CssClass ="form-control" Border-BorderColor="White"  Enabled="false"  runat="server"></dx:ASPxTextBox>
                                  <dx:ASPxTextBox ID="TxtMontoRcrgGenerado" CssClass ="form-control" Border-BorderColor="White" Visible="false"  Enabled="false"  runat="server"></dx:ASPxTextBox>
                                 <dx:ASPxTextBox ID="esRecargo" CssClass ="form-control" Border-BorderColor="White"  Enabled="false" Visible="false" runat="server"></dx:ASPxTextBox>
                                <dx:ASPxTextBox ID="interesACubrir" CssClass ="form-control" Border-BorderColor="White"  Enabled="false" Visible="false" runat="server"></dx:ASPxTextBox>
                               
                               <dx:ASPxCheckBox ID="Moracondonado" runat="server" visivle="false" CheckState="Unchecked" Text="">
                                            </dx:ASPxCheckBox>

                            </div>
                        </asp:Panel>

                       <%-- <div class="col-md-3">
                            <div class="form-group">
                                <label>
                                    Monto Recargo $
                                </label>
                               

                                <dx:ASPxTextBox ID="TxtmontoRecargo" CssClass ="form-control" Border-BorderColor="White" Visible="false" Enabled="false"  runat="server"></dx:ASPxTextBox>

                            </div>
                        </div>--%>

                        <asp:Panel ID="PanelAbnCap" CssClass="col-md-2" runat="server">
                            <div class="form-group">
                                <label>
                                    Es abono capital?
                                </label>
                               <dx:ASPxCheckBox ID="cbAbonoCap" runat="server" AutoPostBack="True" CheckState="Unchecked" Text="">
                                            </dx:ASPxCheckBox>

                            </div>

                        </asp:Panel>


                        <div class="col-md-2">
                            <div class="form-group">
                                <label>
                                    Monto del Pago $
                                </label>
                                <dx:ASPxTextBox ID="monto" runat="server" AutoCompleteType="Disabled" CssClass="form-control">
                                </dx:ASPxTextBox>

                            </div>
                        </div>

                        <asp:Panel ID="condonacion" Visible="false" CssClass="col-md-12" runat="server">

                            <div class="form-group">
                                <label>
                                   Condonar Atraso
                                </label>
                               <dx:ASPxCheckBox ID="cbCondonacion"  runat="server" AutoPostBack="True" CheckState="Unchecked" Text="" >
                                            </dx:ASPxCheckBox>

                                   <ajaxToolkit:ModalPopupExtender ID="ModalPopupExtender" runat="server" TargetControlID="cbCondonacion"
                                                PopupControlID="Panel1" BackgroundCssClass="modalBackground"
                                                CancelControlID="btnCancelar" DropShadow="true" />

                            </div>
                              <asp:TextBox ID="txtMotCond" CssClass ="form-control" name="S1"  TextMode="MultiLine" enabled="false" runat="server"></asp:TextBox>

                           
                        </asp:Panel>

                         
                      
                    </div>


                </div>
                    <!-- /.attachment -->
       



                <div class="box-footer clearfix">

                    <div class=" col-xs-4 col-sm-12 col-md-3">
                        <asp:Button ID="btnRegistrar" CssClass="btn btn-success" runat="server" Text="Cobrar"></asp:Button>
                    </div>

                    <div class=" col-xs-12 col-sm-3">
                        <asp:Button ID="btnCancelar" CssClass="btn btn-warning" runat="server" Text="Cancelar"></asp:Button>
                    </div>

                </div>

            </div>




          <!-- SELECT2 EXAMPLE -->
      <div class="box box-default">
        <div class="box-header with-border">
          <h3 class="box-title">Select2</h3>

          <div class="box-tools pull-right">
            <button type="button" class="btn btn-box-tool" data-widget="collapse"><i class="fa fa-minus"></i></button>
            <button type="button" class="btn btn-box-tool" data-widget="remove"><i class="fa fa-remove"></i></button>
          </div>
        </div>
        <!-- /.box-header -->
        <div class="box-body">
          <div class="row">

              <div class="table">
                  <dx:ASPxGridView KeyFieldName="id" CssClass="table table-responsive" ID="gvPagos" runat="server" Caption="Tabla de Amortización" EnableTheming="True" Theme="Default" Visible="False" EnableCallBacks="False">
                  <SettingsPager AlwaysShowPager="True" Mode="ShowAllRecords">
                  </SettingsPager>
                  <SettingsBehavior AllowSelectByRowClick="True" AllowSelectSingleRowOnly="True" ProcessSelectionChangedOnServer="True" />
                  <Styles>
                      <HeaderPanel BackColor="#666666">
                      </HeaderPanel>
                  </Styles>
              </dx:ASPxGridView>
              </div>

            
       
            <!-- /.col -->
          </div>
          <!-- /.row -->
        </div>
        <!-- /.box-body -->
        <div class="box-footer">
         
        </div>
      </div>
      <!-- /.box -->


    </div>


</asp:Content>
