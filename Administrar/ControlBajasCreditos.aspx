<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site.Master" CodeBehind="ControlBajasCreditos.aspx.vb" Inherits="FinanciaDin.ControlBajasCreditos" %>

<%@ Register Assembly="DevExpress.Web.v15.2, Version=15.2.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.Web.ASPxPivotGrid.v15.2, Version=15.2.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web.ASPxPivotGrid" TagPrefix="dx" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <!-- Latest compiled and minified CSS -->
<link rel="stylesheet" href="https://maxcdn.bootstrapcdn.com/bootstrap/3.3.7/css/bootstrap.min.css" integrity="sha384-BVYiiSIFeK1dGmJRAkycuHAHRg32OmUcww7on3RYdg4Va+PmSTsz/K68vbdEjh4u" crossorigin="anonymous"/>

    <link href="../Style/Plantilla/ionicons.css" rel="stylesheet" />
    
    <link href="../Style/Plantilla/font-awesome/css/font-awesome.css" rel="stylesheet" />
    <link href="../Style/Plantilla/AdminLTE.css" rel="stylesheet" />


    <link href="../Style/Plantilla/_all-skins.css" rel="stylesheet" />

<!-- Optional theme -->
<link rel="stylesheet" href="https://maxcdn.bootstrapcdn.com/bootstrap/3.3.7/css/bootstrap-theme.min.css" integrity="sha384-rHyoN1iRsVXV4nD0JutlnGaslCJuC7uwjduW9SVrLvRYooPp2bWYgmgJQIXwl/Sp" crossorigin="anonymous"/>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <div class="container">

        <div class="col-sm-12 col-md-10  col-lg-9">
            <!-- Contenedor 1 del formulario-->

            <div class="box box-success">
                <!-- contenedor 2 --->
                <div class="box-header">
                    <i class="fa fa-money" aria-hidden="true"></i>

                    <h3 class="box-title">Control Baja de Créditos</h3>

                </div>

                <div class="box-body chat" id="chat-box">
                    <!-- chat item -->
                    <div class="item">


                        <div id="form_Baja_Credito" class="form-horizontal">

                            <div class="form-group" id="id">
                                <label for="IdRelacion" class="col-sm-2 control-label">FOLIO CREDITO</label>

                                <div class="col-sm-4">

                                    <dx:ASPxComboBox ID="cbIdCredito" runat="server" AutoPostBack="True" DataSourceID="SqlDataSource1" DropDownRows="8" DropDownStyle="DropDown" CssClass="form-control" TextField="ID" ValueField="ID_CLIENTE">
                                        <ListBoxStyle Wrap="False">
                                        </ListBoxStyle>
                                    </dx:ASPxComboBox>
                                    <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:FinanciaDinConnectionString %>" SelectCommand="SELECT C.ID, C.ID_CLIENTE
FROM CREDITOS AS C 
WHERE C.Liquidado = 0 and EstatusID = 1    "></asp:SqlDataSource>

                                </div>

                            </div>

                          
                            <div class="form-group">
                                <div class="col-sm-offset-2 col-sm-10">
                                    <div class="checkbox">
                                        <label>
                                            <asp:CheckBox ID="bajaMov" runat="server" OnCheckedChanged="bajaMov_CheckedChanged" AutoPostBack="true" />
                                            <%--<asp:CheckBox ID="generarVencimiento" runat="server" />--%>
                                        Baja de Movimiento
                                        </label>

                                       <asp:TextBox ID="movID" name="movID" AutoCompleteType="Disabled" CssClass="form-control" runat="server" TextMode="SingleLine"></asp:TextBox>

                                    </div>
                                    <asp:Panel ID="panelMensaje" CssClass="col-sm-7" runat="server">
                                        <p class="bg-danger">Solo se le podrá dar de baja al último pago</p>
                                    </asp:Panel>
                                    <br />
                                </div>
                            </div>


                            <div class="form-group">
                                <label for="comentario" class="col-sm-2 control-label">COMENTARIO</label>
                                <div class="col-sm-10">
                                    <asp:TextBox ID="comentario" name="comentario" CssClass="form-control" runat="server" TextMode="MultiLine"></asp:TextBox>
                                </div>
                            </div>


                            <asp:TextBox ID="SolicitudID" name="SolicitudID" CssClass="form-control" runat="server" Enabled="False" Visible="false" ></asp:TextBox>
                            <asp:TextBox ID="creditoID" name="creditoID" CssClass="form-control" runat="server" Enabled="False" Visible="false" ></asp:TextBox>
                            <asp:TextBox ID="garantia" name="creditoID" CssClass="form-control" runat="server" Enabled="False" Visible="false" ></asp:TextBox>
                            <asp:TextBox ID="pagosRealizados" name="creditoID" CssClass="form-control" runat="server" Enabled="False" Visible="false" ></asp:TextBox>
                             <asp:TextBox ID="TxtLlaveCredID" name="LlaveCredID" CssClass="form-control" runat="server" Enabled="False" Visible="false" ></asp:TextBox>
                             
                        </div>
                    </div>
                    <!-- /.attachment -->
                </div>



                <div class="box-footer clearfix">

                    <div class=" col-xs-4 col-sm-12 col-md-3">
                        <asp:Button ID="btnBaja" CssClass="btn btn-danger" runat="server" Text="Baja"></asp:Button>
                    </div>

                    <div class=" col-xs-12 col-sm-3">
                        <asp:Button ID="btnCancelar" CssClass="btn btn-warning" runat="server" Text="Cancelar"></asp:Button>
                    </div>

                </div>

            </div>
        </div>

    </div>


      <!-- TO DO List -->
          <div class="box box-primary">
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

                            <dx:GridViewDataTextColumn FieldName="LlaveCreditoID" Visible="true" VisibleIndex="4">
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






</asp:Content>
