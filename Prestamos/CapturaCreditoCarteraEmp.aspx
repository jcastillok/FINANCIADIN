<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site.Master" CodeBehind="CapturaCreditoCarteraEmp.aspx.vb" Inherits="FinanciaDin.CapturaCreditoCarteraEmp" %>

<%@ Register Assembly="DevExpress.Web.v15.2, Version=15.2.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web" TagPrefix="dx" %>
<%@ Register assembly="DevExpress.Web.ASPxPivotGrid.v15.2, Version=15.2.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.Web.ASPxPivotGrid" tagprefix="dx" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
        <link href="../Style/Plantilla/ionicons.css" rel="stylesheet" />
    
    <link href="../Style/Plantilla/font-awesome/css/font-awesome.css" rel="stylesheet" />
    <link href="../Style/Plantilla/AdminLTE.css" rel="stylesheet" />


    <link href="../Style/Plantilla/_all-skins.css" rel="stylesheet" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">


     <div class="container">

        <div class="col-sm-12 col-md-10  col-lg-9">
            <!-- Contenedor 1 del formulario-->

            <div class="box box-success">
                <!-- contenedor 2 --->
                <div class="box-header">
                    <i class="fa fa-money" aria-hidden="true"></i>

                    <h3 class="box-title">CAPTURA CRÉDITOS CARTERA DE EMPEÑOS</h3>

                </div>

                <div class="box-body chat" id="chat-box">
                    <!-- chat item -->
                    <div class="item">


                        <div id="form_Baja_Credito" class="form-inline">
                            <div class="col-md-6">
                                <div class="form-group" id="id">
                                    <label for="IdRelacion" class="control-label">Cliente</label>



                                    <dx:ASPxComboBox ID="cbClientes" runat="server" CssClass="form-control " DropDownStyle="DropDown" DataSourceID="SqlDataSource2" TextField="Nombre" ValueField="Id" AutoPostBack="True">
                                    </dx:ASPxComboBox>
                                    <asp:SqlDataSource ID="SqlDataSource2" runat="server" ConnectionString="<%$ ConnectionStrings:FinanciaDinConnectionString %>" SelectCommand="SELECT     Id, REPLACE(PrimNombre + ' ' + IsNull(SegNombre,'') + ' ' + PrimApellido + ' ' + IsNull(SegApellido,''), '  ', ' ') AS Nombre
FROM         CLIENTES
WHERE     (Id NOT IN
                          (SELECT     Id_Cliente 
                            FROM        CREDITOS
                            WHERE       (estatusID  in (1,3)) and Liquidado = 0 ))
							AND TIPO = 4
ORDER BY Nombre"></asp:SqlDataSource>

                                </div>
                            </div>

                            <div class="col-md-6">
                                <div class="form-group">
                                    <label>
                                        Fecha Captura
                                    </label>

                                    <dx:ASPxDateEdit CssClass="form-control" ID="deFecCap" runat="server">
                                    </dx:ASPxDateEdit>

                                </div>
                            </div> 

                            <div class="col-md-6">
                                <div class="form-group">
                                    <label>
                                        Monto
                                    </label>
                                     <dx:ASPxTextBox ID="monto" runat="server" CssClass="form-control">
                                            </dx:ASPxTextBox>
                                    
                                </div>
                            </div>

                             <div class="col-md-6">
                                <div class="form-group">
                                    <label>
                                        Plazo
                                    </label>

                                    <dx:ASPxComboBox ID="cbPlazoSol" CssClass="form-control" runat="server" DataSourceID="SqlDataSource3" DropDownStyle="DropDown" TextField="Descripcion" ValueField="Id">
                                            </dx:ASPxComboBox>
                                            <asp:SqlDataSource ID="SqlDataSource3" runat="server" ConnectionString="<%$ ConnectionStrings:FinanciaDinConnectionString %>" SelectCommand="SELECT * FROM [CATPLAZOS] WHERE ID = 2 "></asp:SqlDataSource>

                                </div>
                            </div> 


                            <div class="col-md-6">
                                <div class="form-group">
                                    <label>
                                        Tasa Interés
                                    </label>

                                    <dx:ASPxComboBox ID="cbTasaRef" CssClass="form-control" runat="server" AutoPostBack="True" DataSourceID="SqlDataSource7" DropDownStyle="DropDown" TextField="Descripcion" ValueField="Id" ValueType="System.Int32">
                                            </dx:ASPxComboBox>
                                            <asp:SqlDataSource ID="SqlDataSource7" runat="server" ConnectionString="<%$ ConnectionStrings:FinanciaDinConnectionString %>" SelectCommand="SELECT [Id], [Descripcion] FROM [CATTASAINTERES] WHERE ID = 17 ORDER BY [Descripcion]"></asp:SqlDataSource>
                                    <asp:TextBox ID="ValTasa" runat="server" Visible="false" ></asp:TextBox>
                                    
                                     </div>
                            </div> 


                              <div class="col-md-6">
                                <div class="form-group">
                                    <label>
                                        Tipo Amortización
                                    </label>

                                    <dx:ASPxComboBox ID="cbTipAmort" runat="server" CssClass="form-control" DataSourceID="SqlDataSource6" DropDownStyle="DropDown" TextField="Nombre" ValueField="Id">
                                            </dx:ASPxComboBox>
                                            <asp:SqlDataSource ID="SqlDataSource6" runat="server" ConnectionString="<%$ ConnectionStrings:FinanciaDinConnectionString %>" SelectCommand="SELECT [Id], [Nombre] FROM [CATTIPAMORT] WHERE ID = 2"></asp:SqlDataSource>
                                        
                                   
                                </div>
                            </div> 

                                <div class="col-md-6">
                                <div class="form-group">
                                    <label>
                                        Interés Moratorio
                                    </label>

                                  <dx:ASPxComboBox ID="cbTasaMora" runat="server" CssClass="form-control" DataSourceID="SqlDataSource5" DropDownStyle="DropDown" TextField="Tasa" ValueField="Id">
                                            </dx:ASPxComboBox>
                                            <asp:SqlDataSource ID="SqlDataSource5" runat="server" ConnectionString="<%$ ConnectionStrings:FinanciaDinConnectionString %>" SelectCommand="SELECT [Id], [Tasa] FROM [CATTASAMORA] WHERE ID = 3"></asp:SqlDataSource>
                                   
                                </div>
                            </div> 


                             <div class="col-md-6">
                                <div class="form-group">
                                    <label>
                                        Sucursal
                                    </label>

                                  <dx:ASPxComboBox ID="cbSuc" runat="server" DataSourceID="SqlDataSource9" CssClass="form-control" DropDownStyle="DropDown" TextField="Descripcion" ValueField="Id">
                                            </dx:ASPxComboBox>
                                            <asp:SqlDataSource ID="SqlDataSource9" runat="server" ConnectionString="<%$ ConnectionStrings:FinanciaDinConnectionString %>" SelectCommand="SELECT [Id], [Descripcion] 
FROM [CATSUCURSALES]"></asp:SqlDataSource>
                                   
                                </div>
                            </div> 


                         

                        </div>
                    </div>
                    <!-- /.attachment -->
                </div>



                <div class="box-footer clearfix">

                    <div class=" col-xs-4 col-sm-12 col-md-3">
                        <asp:Button ID="btnRegistrar" CssClass="btn btn-success" runat="server" Text="Registrar"></asp:Button>
                    </div>

                    <div class=" col-xs-12 col-sm-3">
                        <asp:Button ID="btnCancelar" CssClass="btn btn-warning" runat="server" Text="Cancelar"></asp:Button>
                    </div>

                </div>

            </div>
        </div>

    </div>

    <!-- liosta de los creditos registrados-->

          <!-- TO DO List -->
<%--          <div class="box box-primary">
            <div class="box-header">
              <h3 class="box-title">INFORMACIÓN DETALLADA</h3>
              <div class="box-tools pull-right">
              </div>
         
            </div>
            <!-- /.box-header -->
            <div class="box-body">
              <!-- See dist/js/pages/dashboard.js to activate the todoList plugin -->

               <div class="table-responsive">

                   <dx:ASPxGridView ID="ASPxGridView1" CssClass="table"  runat="server" AutoGenerateColumns="False"  EnableCallBacks="False" KeyFieldName="FOLIO_CREDITO" EnableTheming="True" Theme="PlasticBlue">
           
                                 <SettingsPager NumericButtonCount="5">
                                 </SettingsPager>
           
                               
                       <Columns>
                           <dx:GridViewCommandColumn VisibleIndex="0">
                           </dx:GridViewCommandColumn>
                           <dx:GridViewDataTextColumn FieldName="FOLIO_CREDITO" ReadOnly="True" VisibleIndex="1" Caption="ID">
                           </dx:GridViewDataTextColumn>
                           <dx:GridViewDataTextColumn FieldName="Nombre" VisibleIndex="2">
                           </dx:GridViewDataTextColumn>
                           <dx:GridViewDataTextColumn FieldName="PagosRealizados" VisibleIndex="3">
                           </dx:GridViewDataTextColumn>
                           <dx:GridViewDataTextColumn FieldName="Garantia" VisibleIndex="4">
                           </dx:GridViewDataTextColumn>

                       </Columns>
                   </dx:ASPxGridView>


               </div>

            </div>


            <!-- /.box-body -->
            <div class="box-footer clearfix no-border">
            </div>
          </div>
          <!-- /.box -->
--%>


</asp:Content>
