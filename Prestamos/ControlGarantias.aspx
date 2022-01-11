<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site.Master" CodeBehind="ControlGarantias.aspx.vb" Inherits="FinanciaDin.ControlGarantias" %>
<%@ Register Assembly="DevExpress.Web.v15.2, Version=15.2.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.Web.ASPxPivotGrid.v15.2, Version=15.2.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web.ASPxPivotGrid" TagPrefix="dx" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

    <link rel="stylesheet" href="https://maxcdn.bootstrapcdn.com/bootstrap/3.3.7/css/bootstrap.min.css" integrity="sha384-BVYiiSIFeK1dGmJRAkycuHAHRg32OmUcww7on3RYdg4Va+PmSTsz/K68vbdEjh4u" crossorigin="anonymous"/>
    <link href="../Style/Plantilla/ionicons.css" rel="stylesheet" />
    <link href="../Style/Plantilla/font-awesome/css/font-awesome.css" rel="stylesheet" />
    <link href="../Style/Plantilla/AdminLTE.css" rel="stylesheet" />
    <link href="../Style/Plantilla/_all-skins.css" rel="stylesheet" />
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

                    <h3 class="box-title">Control de Garantías</h3>

                </div>

                <div class="box-body chat" id="chat-box">
                    <!-- chat item -->
                    <div class="item">


                        <div id="form_Baja_Credito" class="form-horizontal">

                            <div class="form-group" id="id">
                                <label for="IdRelacion" class="col-sm-2 control-label">Cliente:</label>

                                <div class="col-sm-6">
                                    <dx:ASPxComboBox ID="cbClientesGar" runat="server" CssClass="form-control" AutoPostBack="True" DropDownStyle="DropDown" TextField="Nombre" ValueField="Id_Cliente" DropDownRows="8">
                                        <ListBoxStyle Wrap="False">
                                        </ListBoxStyle>
                                    </dx:ASPxComboBox>
                                </div>

                            </div>

                            <div class="form-group">
                                <label for="deFecDep" class="col-sm-2 control-label">Fecha:</label>
                                <div class="col-sm-6">
                                    <dx:ASPxDateEdit ID="deFecDep" CssClass="form-control" runat="server" AutoPostBack="True">
                                    </dx:ASPxDateEdit>
                                </div>
                            </div>
                                                        
                            <div class="pad  no-print">
                                <div class="callout callout-warning" style="margin-bottom: 0!important;">
                                    <h4>Último Ciclo</h4>
                                   
                                </div>
                            </div>

                            <div class="form-group">
                                <label for="CreditoID" class="col-sm-2 control-label">Folio Credito</label>
                                <div class="col-sm-2">
                                    <asp:TextBox ID="CreditoId" name="CreditoID" CssClass="form-control" runat="server" TextMode="SingleLine"></asp:TextBox>
                                </div>
                            </div>

                            <div class="form-group">
                                <label for="MontoGarantia" class="col-sm-2 control-label">Monto Garantia</label>
                                <div class="col-sm-2">
                                    <asp:TextBox ID="MontoGarantia" name="MontoGarantia" CssClass="form-control" runat="server" TextMode="SingleLine"></asp:TextBox>
                                </div>
                            </div>

                            <div class="form-group">
                                <label for="FechaLiquidacion" class="col-sm-2 control-label">Fecha Liquidación Último Ciclo</label>
                                <div class="col-sm-3">
                                    <asp:TextBox ID="FechaLiquidacion" name="FechaLiquidacion" CssClass="form-control" runat="server" TextMode="SingleLine"></asp:TextBox>
                                </div>
                            </div>


                            <!-- En caso de tener un credito se habilitaran estos controles de abajo-->
                             <div class="pad  no-print" runat="server" id="panelInfoCicloNvo" >
                                <div class="callout callout-success" style="margin-bottom: 0!important;">
                                    <h4>Nuevo Ciclo</h4>
                                   
                                </div>
                            </div>

                            <div class="form-group" runat="server" id="panelFolioCredito">
                                <label for="CreditoIDNvo" class="col-sm-2 control-label">Folio Credito</label>
                                <div class="col-sm-2">
                                    <asp:TextBox ID="CreditoIDNvo" name="CreditoIDNvo" CssClass="form-control" runat="server" TextMode="SingleLine"></asp:TextBox>
                                </div>
                            </div>

                            <div class="form-group" runat="server" id="panelGarantiaNvo">
                                <label for="MontoGarNvo" class="col-sm-2 control-label">Monto Garantia Requerido</label>
                                <div class="col-sm-2">
                                    <asp:TextBox ID="MontoGarNvo" name="MontoGarNvo" CssClass="form-control" runat="server" TextMode="SingleLine"></asp:TextBox>
                                </div>
                            </div>

                            <div class="form-group" runat="server" id="panelSucursal">
                                <label for="Sucursal" class="col-sm-2 control-label">Sucursal</label>
                                <div class="col-sm-2">
                                    <asp:TextBox ID="txtSucursal" name="Sucursal" CssClass="form-control" runat="server" TextMode="SingleLine"></asp:TextBox>
                                </div>
                            </div>

                              <asp:TextBox ID="txtSucursalID" name="SucursalID" Visible="false"  CssClass="form-control" runat="server" TextMode="SingleLine"></asp:TextBox>
                               <asp:TextBox ID="txtFecPrimPago" name="FecPrimPago" Visible="false"  CssClass="form-control" runat="server" TextMode="SingleLine"></asp:TextBox>
                              <asp:TextBox ID="txtMontoCreditoNvo" name="txtMontoCreditoNvo" Visible="false"  CssClass="form-control" runat="server" TextMode="SingleLine"></asp:TextBox>
                             <asp:TextBox ID="txtSaldo" name="txtMontoCreditoNvo" Visible="false"  CssClass="form-control" runat="server" TextMode="SingleLine"></asp:TextBox>

                           
                        </div>
                    </div>
                    <!-- /.attachment -->
                </div>

                <div class="box-footer clearfix">

                    <div class=" col-xs-4 col-sm-12 col-md-3">
                        <asp:Button ID="btnDevolver" CssClass="btn btn-bitbucket " runat="server" Text="Retirar Garantía"></asp:Button>
                    </div>

                    <div class=" col-xs-4 col-sm-12 col-md-3">
                        <asp:Button ID="BtnNvoCiclo" CssClass="btn btn-github" runat="server" Text="Usar Nvo Ciclo"></asp:Button>
                    </div>

                    <div class=" col-xs-12 col-sm-3">
                        <asp:Button ID="btnCancelar" CssClass="btn btn-warning" runat="server" Text="Cancelar"></asp:Button>
                    </div>

                </div>

            </div>
        </div>

    </div>

</asp:Content>
