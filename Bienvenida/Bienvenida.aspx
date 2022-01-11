<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Principal.Master"
    CodeBehind="Bienvenida.aspx.vb" Inherits="FinanciaDin.Formulario_web1" %>

<%@ Register Assembly="DevExpress.Web.v15.2, Version=15.2.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dx" %>


<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <br />
    <br />
    <center>
    <br />
    <br />
    <dx:ASPxRoundPanel ID="ASPxRoundPanel1" runat="server" Theme="Youthful" HeaderText="Bienvenido">
        <PanelCollection>
            <dx:PanelContent>
                <img runat="server" id="Img1" src="~/Images/Imagen1.jpg" height="300"></img>

            </img>

                </img>

            </dx:PanelContent>
        </PanelCollection>
    </dx:ASPxRoundPanel>
            </center>
</asp:Content>
