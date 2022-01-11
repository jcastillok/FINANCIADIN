<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site.Master" CodeBehind="ConfirmCredCarteraemp.aspx.vb" Inherits="FinanciaDin.ConfirmCredCarteraemp" %>

<%@ Register Assembly="DevExpress.Web.ASPxHtmlEditor.v15.2, Version=15.2.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web.ASPxHtmlEditor" TagPrefix="dx" %>

<%@ Register Assembly="DevExpress.Web.v15.2, Version=15.2.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web" TagPrefix="dx" %>
<%@ Register assembly="DevExpress.Web.ASPxPivotGrid.v15.2, Version=15.2.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.Web.ASPxPivotGrid" tagprefix="dx" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
     <link href="../Style/Plantilla/ionicons.css" rel="stylesheet" />
    
    <link href="../Style/Plantilla/font-awesome/css/font-awesome.css" rel="stylesheet" />
    <link href="../Style/Plantilla/AdminLTE.css" rel="stylesheet" />
    <link href="../Style/Plantilla/_all-skins.css" rel="stylesheet" />

    <script src="../Scripts/jquery-3.4.1.js"></script>
    <script src="../Scripts/bootstrap.js"></script>
    <script src="../Scripts/adminlte.js"></script>
    <script src="../Scripts/demo.js"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
     <!-- Main content -->
    <section class="content">


        <div class="box box-default">
        <div class="box-header with-border">
          <h3 class="box-title">Créditos PreActivos</h3>

          <div class="box-tools pull-right">
            <button type="button" class="btn btn-box-tool" data-widget="collapse"><i class="fa fa-minus"></i></button>
            
          </div>
        </div>
        <!-- /.box-header -->
        <div class="box-body">
          <div class="row">

             <%--  <dx:ASPxGridView runat="server" KeyFieldName="Id" AutoGenerateColumns="False"  ID="gvCredPreActivos" EnableCallBacks="False">
            <SettingsBehavior AllowSelectByRowClick="True" AllowSelectSingleRowOnly="True" ProcessSelectionChangedOnServer="True" />
            <SettingsSearchPanel Visible="True" />
            <Columns>
                <dx:GridViewCommandColumn SelectAllCheckboxMode="Page" ShowSelectCheckbox="True" VisibleIndex="0">
                </dx:GridViewCommandColumn>
       

            </Columns>
            <Styles>
                <SearchPanel HorizontalAlign="Center" VerticalAlign="Middle">
                </SearchPanel>
            </Styles>
        </dx:ASPxGridView>--%>

            <div class="col-sm-12">
  
              <dx:ASPxGridView CssClass="table table-responsive"  EnableCallBacks="FALSE"  AutoGenerateColumns="False" KeyFieldName="SolID" ID="gvCredPreActivos" runat="server">
                    <SettingsPager NumericButtonCount="5">
                    </SettingsPager>
                   <SettingsBehavior AllowSelectByRowClick="True" AllowSelectSingleRowOnly="True" ProcessSelectionChangedOnServer="True" />
                    
                  <Columns>
             <%--   <dx:GridViewCommandColumn SelectAllCheckboxMode="Page" ShowSelectCheckbox="True" VisibleIndex="0">
                </dx:GridViewCommandColumn>--%>

                    <dx:GridViewDataTextColumn FieldName="CredID" VisibleIndex="1" ReadOnly="True">
                        <HeaderStyle Wrap="True" />
                    </dx:GridViewDataTextColumn>
       

                    <dx:GridViewDataTextColumn FieldName="SolID" VisibleIndex="1" Visible="false"  ReadOnly="True">
                        <HeaderStyle Wrap="True" />
                    </dx:GridViewDataTextColumn>

                    <dx:GridViewDataTextColumn FieldName="Cliente" VisibleIndex="1" ReadOnly="True">
                        <HeaderStyle Wrap="True" />
                    </dx:GridViewDataTextColumn>

                         <dx:GridViewDataTextColumn FieldName="FecAutorizado" VisibleIndex="1" ReadOnly="True">
                        <HeaderStyle Wrap="True" />
                             <EditFormSettings Visible="False"></EditFormSettings>
                    </dx:GridViewDataTextColumn>

                       <dx:GridViewDataTextColumn FieldName="MontoAut" VisibleIndex="1" ReadOnly="True">
                        <HeaderStyle Wrap="True" />
                    </dx:GridViewDataTextColumn>

                      <dx:GridViewDataTextColumn FieldName="Adeudo" VisibleIndex="1" ReadOnly="True">
                        <HeaderStyle Wrap="True" />
                    </dx:GridViewDataTextColumn>
                       
                      <dx:GridViewDataTextColumn FieldName="TasaRef" VisibleIndex="1" ReadOnly="True">
                        <HeaderStyle Wrap="True" />
                          <EditFormSettings Visible="False"></EditFormSettings>
                    </dx:GridViewDataTextColumn>

                      
                      <dx:GridViewDataTextColumn FieldName="PlazoAut" VisibleIndex="1" ReadOnly="True">
                        <HeaderStyle Wrap="True" />
                    </dx:GridViewDataTextColumn>

                      
                      <dx:GridViewDataTextColumn FieldName="Tasa" VisibleIndex="1" ReadOnly="True">
                        <HeaderStyle Wrap="True" />
                    </dx:GridViewDataTextColumn>

                      
                      <dx:GridViewDataTextColumn FieldName="TasaMoratoria" VisibleIndex="1" ReadOnly="True">
                        <HeaderStyle Wrap="True" />
                    </dx:GridViewDataTextColumn>

                      
                      <dx:GridViewDataTextColumn FieldName="Impuesto" VisibleIndex="1" ReadOnly="True">
                        <HeaderStyle Wrap="True" />
                    </dx:GridViewDataTextColumn>

                      <dx:GridViewDataTextColumn FieldName="Direccion" VisibleIndex="1" ReadOnly="True">
                        <HeaderStyle Wrap="True" />
                    </dx:GridViewDataTextColumn>

                     <dx:GridViewDataTextColumn FieldName="NombRZ" VisibleIndex="1" ReadOnly="True">
                        <HeaderStyle Wrap="True" />
                    </dx:GridViewDataTextColumn>

                    <dx:GridViewDataTextColumn FieldName="DirRZ" VisibleIndex="1" ReadOnly="True">
                        <HeaderStyle Wrap="True" />
                    </dx:GridViewDataTextColumn>

                            
            </Columns>

              </dx:ASPxGridView>
         

            </div>
          </div>
          <!-- /.row -->
        </div>
        <!-- /.box-body -->
        <div class="box-footer">
        
        </div>
      </div>
      <!-- /.box -->



         <!-- FORMULARIO PARA ACTIVAR CREDITO -->
      <div class="box box-default">
        <div class="box-header with-border">
          <h3 class="box-title">Activar Crédito</h3>

          <div class="box-tools pull-right">
            <button type="button" class="btn btn-box-tool" data-widget="collapse"><i class="fa fa-minus"></i></button>
            
          </div>
        </div>
        <!-- /.box-header -->
        <div class="box-body">
          <div class="row">
            <div class="col-md-6">
              <div class="form-group">
                <label>Cliente</label>

                <%-- <asp:TextBox ID="nmbCli" CssClass="form-control" runat="server"></asp:TextBox>--%>
                    <asp:TextBox ID="nmbCli" name="nmbCli" CssClass="form-control" runat="server"></asp:TextBox>
                
              </div>
              <!-- /.form-group -->
            </div>

               <div class="col-md-3">
              <div class="form-group">
                <label>Folio Crédito</label>
                <asp:TextBox ID="CredID"  CssClass="form-control" runat="server"></asp:TextBox>
              </div>
              <!-- /.form-group -->
            </div>
          

               <div class="col-md-3">
              <div class="form-group">
                <label>Monto</label>
                <asp:TextBox ID="monto" Enabled="false"  CssClass="form-control" runat="server"></asp:TextBox>
              </div>
              <!-- /.form-group -->
            </div>

               <div class="col-md-3">
              <div class="form-group">
                <label>Monto + Interés</label>
                <asp:TextBox ID="MontoDeuda" CssClass="form-control" runat="server"></asp:TextBox>
              </div>
              <!-- /.form-group -->
            </div>


               <div class="col-md-3">
              <div class="form-group">
                <label>Sucursal</label>
                <asp:TextBox ID="suc" CssClass="form-control" runat="server"></asp:TextBox>
              </div>
              <!-- /.form-group -->
            </div>

            
               <asp:TextBox ID="SolID" Visible="false"  CssClass="form-control" runat="server"></asp:TextBox>
              <asp:TextBox ID="Direccion" Visible="false"  CssClass="form-control" runat="server"></asp:TextBox>
              <asp:TextBox ID="NombRZ" Visible="false"  CssClass="form-control" runat="server"></asp:TextBox>
              <asp:TextBox ID="DirRZ" Visible="false"  CssClass="form-control" runat="server"></asp:TextBox>
               <asp:TextBox ID="tasaRefletra" Visible="false"  CssClass="form-control" runat="server"></asp:TextBox>


          </div>
          <!-- /.row -->
        </div>
        <!-- /.box-body -->
        <div class="box-footer">
                 <div class=" col-xs-4 col-sm-12 col-md-3">
                        <asp:Button ID="btnRegistrar" CssClass="btn btn-success" runat="server" Text="Activar"></asp:Button>
                 </div>

                 <div class=" col-xs-12 col-sm-3">
                        <asp:Button ID="btnCancelar" CssClass="btn btn-warning" runat="server" Text="Cancelar"></asp:Button>
                 </div>
        </div>
      </div>
      <!-- /.box -->
          </section>


</asp:Content>
