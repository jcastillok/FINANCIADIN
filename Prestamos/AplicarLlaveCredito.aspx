<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site.Master" CodeBehind="AplicarLlaveCredito.aspx.vb" Inherits="FinanciaDin.AplicarLlaveCredito" %>

<%@ Register Assembly="DevExpress.Web.ASPxHtmlEditor.v15.2, Version=15.2.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web.ASPxHtmlEditor" TagPrefix="dx" %>

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
left join pagos p on c.id = p.Id_Credito 
WHERE C.Liquidado = 0  AND C.estatusID = 1 and c.id not in(select distinct Id_Credito
from pagos 
inner join creditos c on pagos.Id_Credito = c.id
where Cancelado = 0 and Liquidado = 0 and estatusID =1) and c.id not in (select distinct CreditoID
from LLAVECREDITO lv 
inner join creditos c on lv.CreditoID = c.id
where Estatus = 'ACT' and C.Liquidado = 0 and C.estatusID =1)
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
inner join CLIENTES AS CLI ON CLI.ID = C.ID_CLIENTE
left join pagos p on c.id = p.Id_Credito 
WHERE C.Liquidado = 0  AND C.estatusID = 1 and c.id not in(select distinct Id_Credito
from pagos 
inner join creditos c on pagos.Id_Credito = c.id
where Cancelado = 0 and Liquidado = 0 and estatusID =1) and c.id not in (select distinct CreditoID
from LLAVECREDITO lv 
inner join creditos c on lv.CreditoID = c.id
where Estatus = 'ACT' and C.Liquidado = 0 and C.estatusID =1)"></asp:SqlDataSource>

                        </div>
                    </div>

                    <div class="col-md-3">
                            <div class="form-group">
                                <label>
                                    Monto Deuda $
                                </label>
                                <dx:ASPxTextBox ID="montoDeuda" CssClass ="form-control"  Enabled="false"  runat="server"></dx:ASPxTextBox>

                            </div>
                        </div>

                   
                    <div class="box-footer clearfix">
                    </div>


                    <div class="col-md-8">

                         <div class="col-md-8">
                            <div class="form-group">
                                <label>
                                    Ingresar Llave
                                </label>
                               

                                <dx:ASPxTextBox ID="llaveCredito" CssClass ="form-control"   runat="server"></dx:ASPxTextBox>

                            </div>
                        </div>


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




<%--          <!-- SELECT2 EXAMPLE -->
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

               

              </div>

            
       
            <!-- /.col -->
          </div>
          <!-- /.row -->
        </div>
        <!-- /.box-body -->
        <div class="box-footer">
         
        </div>
      </div>
      <!-- /.box -->--%>


    </div>



</asp:Content>
