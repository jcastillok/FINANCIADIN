<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site.Master" MaintainScrollPositionOnPostback="true" CodeBehind="CreditosCarteraVencida.aspx.vb" Inherits="FinanciaDin.CreditosCarteraVencida" %>

<%@ Register Assembly="DevExpress.Web.v15.2, Version=15.2.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web" TagPrefix="dx" %>
<%@ Register assembly="DevExpress.Web.ASPxPivotGrid.v15.2, Version=15.2.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.Web.ASPxPivotGrid" tagprefix="dx" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
            <link href="../Style/Plantilla/font-awesome/css/font-awesome.css" rel="stylesheet" />
    <link href="../Style/Plantilla/AdminLTE.css" rel="stylesheet" />
    <link href="../Style/Plantilla/_all-skins.css" rel="stylesheet" />

    <script src="../Scripts/jquery-3.4.1.js"></script>
    <script src="../Scripts/bootstrap.js"></script>
    <script src="../Scripts/adminlte.js"></script>
    <script src="../Scripts/demo.js"></script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    
        <div class="content">

          <asp:ScriptManager ID="ScriptManager1" runat="server">
                </asp:ScriptManager>
            <!-- Contenedor 1 del formulario-->

            <div class="box box-success">
                <!-- contenedor 2 --->
                <div class="box-header">
                    <i class="fa fa-money" aria-hidden="true"></i>

                    <h3 class="box-title">Cartera Vencida</h3>

                </div>

                <div class="box-body chat" id="chat-box">
                    <!-- chat item -->

                    <div class="col-md-3">
                        <div class="form-group" id="id">
                            <label for="lblSucursal" class="control-label">Secursal</label>
                            <dx:ASPxComboBox ID="cbSuc" runat="server" DataSourceID="SqlDataSource3" Enabled="false" DropDownStyle="DropDown" TextField="Descripcion" ValueField="Id">
                            </dx:ASPxComboBox>
                            <asp:SqlDataSource ID="SqlDataSource3" runat="server" ConnectionString="<%$ ConnectionStrings:FinanciaDinConnectionString %>" SelectCommand="SELECT [Id], [Descripcion] FROM [CATSUCURSALES] WHERE [Disponible] = 1"></asp:SqlDataSource>


                        </div>
                    </div>

       

                </div>
                    <!-- /.attachment -->
       



                <div class="box-footer clearfix">

                    <div class=" col-xs-4 col-sm-12 col-md-3">
                        <asp:Button ID="btnConsultar" CssClass="btn btn-success" runat="server" Text="Consultar"></asp:Button>
                    </div>

                    <div class=" col-xs-12 col-sm-3">
                        <asp:Button ID="btnCancelar" CssClass="btn btn-warning" runat="server" Text="Cancelar"></asp:Button>
                    </div>

                </div>

            </div>




          <!-- SELECT2 EXAMPLE -->
      <div class="box box-default">
        <div class="box-header with-border">
          <h3 class="box-title"></h3>

          <div class="box-tools pull-right">
            <button type="button" class="btn btn-box-tool" data-widget="collapse"><i class="fa fa-minus"></i></button>
            <button type="button" class="btn btn-box-tool" data-widget="remove"><i class="fa fa-remove"></i></button>
          </div>
        </div>
        <!-- /.box-header -->
        <div class="box-body">
          <div class="row">

              <asp:Panel ID="pnlGridCreditos" Visible="false"  class="col-sm-12" runat="server">

                   <dx:ASPxGridView CssClass="table table-responsive"  EnableCallBacks="False"  AutoGenerateColumns="False" KeyFieldName="Folio" ID="gvCarteraVencida" runat="server">
                     
                   <SettingsBehavior AllowSelectByRowClick="True" AllowSelectSingleRowOnly="True" ProcessSelectionChangedOnServer="True" AllowSort="False" />
                    
                  <Columns>
             <%--   <dx:GridViewCommandColumn SelectAllCheckboxMode="Page" ShowSelectCheckbox="True" VisibleIndex="0">
                </dx:GridViewCommandColumn>--%>

                      <dx:GridViewDataTextColumn FieldName="ClienteID" Caption="ClienteID" Visible="false" VisibleIndex="1" ReadOnly="True">
                          <HeaderStyle Wrap="True" />
                      </dx:GridViewDataTextColumn>

                      <dx:GridViewDataTextColumn FieldName="Nombre" Caption="Cliente" VisibleIndex="1" ReadOnly="True">
                        <HeaderStyle Wrap="True" />
                    </dx:GridViewDataTextColumn>
       

                    <dx:GridViewDataTextColumn FieldName="Direccion" VisibleIndex="2"  ReadOnly="True">
                        <HeaderStyle Wrap="True" />
                    </dx:GridViewDataTextColumn>

                    <dx:GridViewDataTextColumn FieldName="Celular" VisibleIndex="3" ReadOnly="True">
                        <HeaderStyle Wrap="True" />
                    </dx:GridViewDataTextColumn>
    
                    <dx:GridViewDataTextColumn FieldName="Folio" VisibleIndex="4" Visible="false"  Caption="Folio Crédito" ReadOnly="True">
                        <HeaderStyle Wrap="True" />
                    </dx:GridViewDataTextColumn>

                    <dx:GridViewDataTextColumn FieldName="Capital" VisibleIndex="5" ReadOnly="True">
                        <HeaderStyle Wrap="True" />
                             <PropertiesTextEdit DisplayFormatString="c2">
                        </PropertiesTextEdit>
                             <EditFormSettings Visible="False"></EditFormSettings>
                    </dx:GridViewDataTextColumn>

                      <dx:GridViewDataTextColumn FieldName="SaldoCapital" VisibleIndex="6" Visible="false"  Caption="Saldo Capital" ReadOnly="True">
                          <PropertiesTextEdit DisplayFormatString="c2">
                          </PropertiesTextEdit>
                        <HeaderStyle Wrap="True" />
                    </dx:GridViewDataTextColumn>
                       
                      <dx:GridViewDataTextColumn FieldName="SaldoVencido" VisibleIndex="7" Visible="false"  Caption="Saldo Vencido" ReadOnly="True">
                        <HeaderStyle Wrap="True" />
                          <PropertiesTextEdit DisplayFormatString="c2">
                          </PropertiesTextEdit>
                          <EditFormSettings Visible="False"></EditFormSettings>
                    </dx:GridViewDataTextColumn>

                      
                      <dx:GridViewDataTextColumn FieldName="InteresPendiente" VisibleIndex="8" Visible="false"  Caption="Interés Por Pagar" ReadOnly="True">
                          <PropertiesTextEdit DisplayFormatString="c2">
                          </PropertiesTextEdit>
                        <HeaderStyle Wrap="True" />
                    </dx:GridViewDataTextColumn>

                      
                      <dx:GridViewDataTextColumn FieldName="MoraGenerado" VisibleIndex="9" Visible="false"  Caption="MoraTorio" ReadOnly="True">
                          <PropertiesTextEdit DisplayFormatString="c2">
                          </PropertiesTextEdit>
                        <HeaderStyle Wrap="True" />
                    </dx:GridViewDataTextColumn>

                        
                      <dx:GridViewDataTextColumn FieldName="TotalDeuda" VisibleIndex="10" Caption="Total Deuda" ReadOnly="True">
                          <PropertiesTextEdit DisplayFormatString="c2">
                          </PropertiesTextEdit>
                        <HeaderStyle Wrap="True" />
                    </dx:GridViewDataTextColumn>

                       <dx:GridViewDataTextColumn FieldName="DiasDeAtraso" VisibleIndex="11" Caption="Días Atraso" ReadOnly="True">
                        <HeaderStyle Wrap="True" />
                    </dx:GridViewDataTextColumn>

                      <dx:GridViewDataTextColumn FieldName="FechaPagoVencido" VisibleIndex="12" Caption="Fecha Pago Vencido" ReadOnly="True">
                          <PropertiesTextEdit DisplayFormatString="dd/MM/yyyy">
                          </PropertiesTextEdit>
                        <HeaderStyle Wrap="True" />
                    </dx:GridViewDataTextColumn>

                      <dx:GridViewDataTextColumn FieldName="FechaFinCredito" VisibleIndex="13" Caption="Fecha Fin Crédito" ReadOnly="True">
                          <PropertiesTextEdit DisplayFormatString="dd/MM/yyyy">
                          </PropertiesTextEdit>
                        <HeaderStyle Wrap="True" />
                    </dx:GridViewDataTextColumn>

                      <dx:GridViewDataColumn Caption="Identificación Cliente" VisibleIndex="14">
                          <DataItemTemplate>
                              <asp:LinkButton ID="VerIdentificacion" runat="server" Text="Ver" OnClick="VerIdentificacion" CommandArgument='<%# Eval("ClienteID") %>'></asp:LinkButton>
                          </DataItemTemplate>

                          <HeaderStyle Wrap="True" />
                      </dx:GridViewDataColumn>

                      <dx:GridViewDataColumn Caption="Comprobante Dom. Cliente" VisibleIndex="15">
                          <DataItemTemplate>
                              <asp:LinkButton ID="VerComp" runat="server" Text="Ver" OnClick="VerComprobante" CommandArgument='<%# Eval("ClienteID") %>'></asp:LinkButton>
                          </DataItemTemplate>
                          <HeaderStyle Wrap="True" />
                      </dx:GridViewDataColumn>

                      <dx:GridViewDataTextColumn FieldName="AvalID" VisibleIndex="16" Visible="false" Caption="AvalDia" ReadOnly="True">
                        <HeaderStyle Wrap="True" />
                    </dx:GridViewDataTextColumn>

                      <dx:GridViewDataTextColumn FieldName="NombreAval" VisibleIndex="17" Caption="Nombre Aval" ReadOnly="True">
                        <HeaderStyle Wrap="True" />
                    </dx:GridViewDataTextColumn>

                      <dx:GridViewDataTextColumn FieldName="DireccionAval" VisibleIndex="18" Caption="Dirección Aval" ReadOnly="True">
                        <HeaderStyle Wrap="True" />
                    </dx:GridViewDataTextColumn>

                      <dx:GridViewDataTextColumn FieldName="CelularAval" VisibleIndex="19" Caption="Celular Aval" ReadOnly="True">
                        <HeaderStyle Wrap="True" />
                    </dx:GridViewDataTextColumn>

                      <dx:GridViewDataColumn Caption="Identificación Aval" VisibleIndex="20">
                          <DataItemTemplate>
                              <asp:LinkButton ID="VerIdentificacionAval" runat="server" Text="Ver" OnClick="VerIdentificacionAval" CommandArgument='<%# Eval("AvalID") %>'></asp:LinkButton>
                          </DataItemTemplate>

                          <HeaderStyle Wrap="True" />
                      </dx:GridViewDataColumn>

                      <dx:GridViewDataColumn Caption="Comprobante Dom. Aval" VisibleIndex="21">
                          <DataItemTemplate>
                              <asp:LinkButton ID="VerCompAval" runat="server" Text="Ver" OnClick="VerComprobante" CommandArgument='<%# Eval("AvalID") %>'></asp:LinkButton>
                          </DataItemTemplate>
                          <HeaderStyle Wrap="True" />
                      </dx:GridViewDataColumn>

                  </Columns>

              </dx:ASPxGridView>



                  <dx:ASPxGridViewExporter ID="ASPxGridViewExporter1" runat="server" GridViewID="gvCarteraVencida"></dx:ASPxGridViewExporter>

                   <asp:Button ID="btnExportar" CssClass="btn btn-success" runat="server" Text="Exportar"></asp:Button>


            </asp:Panel>

            
       
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
