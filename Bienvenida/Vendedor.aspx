<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Vendedores.Master" CodeBehind="Vendedor.aspx.vb" Inherits="FinanciaDin.Vendedor" %>

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
        <dx:ASPxRoundPanel ID="ASPxRoundPanel1" runat="server" Theme="Office2010Silver" 
            HeaderText="Bienvenido">
            <panelcollection>
            <dx:PanelContent>
                <img runat="server" id="Img1" src="~/Images/Imagen1.jpg" height="300"></img>

            </img>

                </img>

            </img>

                </img>

            </dx:PanelContent>
        </panelcollection>
        </dx:ASPxRoundPanel>
    </center>
</asp:Content>
