<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site.Master"
    CodeBehind="Contratos.aspx.vb" Inherits="FinanciaDin.Contratos" %>

<%@ Register Assembly="DevExpress.Web.v15.2, Version=15.2.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web" TagPrefix="dx" %>
<%@ Register assembly="DevExpress.Web.ASPxPivotGrid.v15.2, Version=15.2.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.Web.ASPxPivotGrid" tagprefix="dx" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
    <style type="text/css">
        .auto-style2 {
            width: 78px;
        }
    </style>
    </asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <br />
    <br />
    <center>
        <br />
        <dx:ASPxGridView runat="server" AutoGenerateColumns="False" DataSourceID="SqlDataSource1" ID="ASPxGridView1" EnableCallBacks="False" KeyFieldName="Folio">
            <SettingsBehavior AllowSelectByRowClick="True" AllowSelectSingleRowOnly="True" ProcessSelectionChangedOnServer="True" />
            <SettingsSearchPanel Visible="True" />
            <Columns>
                <dx:GridViewDataTextColumn FieldName="Folio" VisibleIndex="0" ReadOnly="True">
                    <EditFormSettings Visible="False" />
                </dx:GridViewDataTextColumn>
                <dx:GridViewDataTextColumn FieldName="Id_Sol" VisibleIndex="1" ReadOnly="True" Visible="False">
                </dx:GridViewDataTextColumn>
                <dx:GridViewDataTextColumn FieldName="Cliente" ReadOnly="True" VisibleIndex="2">
                    <EditFormSettings Visible="False"></EditFormSettings>

                </dx:GridViewDataTextColumn>
                <dx:GridViewDataTextColumn FieldName="Aval" VisibleIndex="3" ReadOnly="True">
                </dx:GridViewDataTextColumn>
                <dx:GridViewDataDateColumn FieldName="FecSolicitud" VisibleIndex="4">
                </dx:GridViewDataDateColumn>
                <dx:GridViewDataDateColumn FieldName="FecAutorizado" VisibleIndex="5">
                </dx:GridViewDataDateColumn>
                <dx:GridViewDataDateColumn FieldName="FecDisposicion" VisibleIndex="6">
                </dx:GridViewDataDateColumn>
                <dx:GridViewDataTextColumn FieldName="Prestamo" VisibleIndex="7">
                </dx:GridViewDataTextColumn>
                <dx:GridViewDataTextColumn FieldName="Adeudo" VisibleIndex="8">
                </dx:GridViewDataTextColumn>
                <dx:GridViewDataTextColumn FieldName="#Pagos" VisibleIndex="9">
                </dx:GridViewDataTextColumn>
                <dx:GridViewDataTextColumn FieldName="Plazo" VisibleIndex="10">
                </dx:GridViewDataTextColumn>
                <dx:GridViewDataTextColumn FieldName="Login" VisibleIndex="11">
                </dx:GridViewDataTextColumn>
                <dx:GridViewDataDateColumn FieldName="FecCaptura" VisibleIndex="12">
                </dx:GridViewDataDateColumn>
                <dx:GridViewDataTextColumn FieldName="TasaRef" VisibleIndex="13">
                </dx:GridViewDataTextColumn>
            </Columns>
            <Styles>
                <SearchPanel HorizontalAlign="Center" VerticalAlign="Middle">
                </SearchPanel>
            </Styles>
        </dx:ASPxGridView>

        <asp:SqlDataSource runat="server" ConnectionString="<%$ ConnectionStrings:FinanciaDinConnectionString %>" SelectCommand="SELECT     C.Id AS Folio, S.Id As Id_Sol, REPLACE(C1.PrimNombre + ' ' + IsNull(C1.SegNombre,'') + ' ' + C1.PrimApellido + ' ' + IsNull(C1.SegApellido,''),'  ', ' ') AS Cliente,
           REPLACE(C2.PrimNombre + ' ' + IsNull(C2.SegNombre,'') + ' ' + C2.PrimApellido + ' ' + IsNull(C2.SegApellido,''),'  ', ' ') AS Aval, S.FecSolicitud,  
           S.FecAutorizado, S.FecDisposicion, S.MontoAut AS Prestamo, C.Adeudo, S.NumPagosAut AS #Pagos, CP1.Descripcion AS Plazo, S.Login, S.FecCaptura, CTI.Descripcion AS TasaRef
FROM         SOLICITUDES AS S INNER JOIN
			 CREDITOS AS C ON S.Id = C.Id_Sol LEFT OUTER JOIN
             CLIENTES AS C1 ON C1.Id = S.Id_Cliente LEFT OUTER JOIN 
             CLIENTES AS C2 ON C2.Id = S.Id_Aval LEFT OUTER JOIN
             CATPLAZOS AS CP1 ON CP1.Id = S.PlazoAut LEFT OUTER JOIN
             CATTASAINTERES AS CTI ON CTI.Id = S.TasaRef
WHERE     (S.Autorizado = 1) AND (C.Liquidado = 0) AND (C.estatusID = 1)
ORDER BY Cliente" ID="SqlDataSource1"></asp:SqlDataSource>
        <table>
            <tbody>
                <tr>
                    <td class="auto-style2">

                        &nbsp;</td>
                    <td>
                        
                    </td>
                </tr>
                <tr>
                    <td class="auto-style2">

                        &nbsp;</td>
                    <td>
                        <dx:ASPxButton runat="server" Text="Generar Documentación" ID="bttValidar" UseSubmitBehavior="False"></dx:ASPxButton>
                    </td>
                    <td class="auto-style2">
                        &nbsp;</td>
                </tr>
            </tbody>
        </table>
        

        <br />

    </center>
    <script type="text/javascript">
        setInterval('MantenSesion()', <%= (Int(0.9 * (Session.Timeout * 60000)))%>);
    </script>
</asp:Content>
