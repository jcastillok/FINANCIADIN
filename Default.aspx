<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="Default.aspx.vb" Inherits="FinanciaDin._Default" %>

<%--<%@ Register Assembly="DevExpress.Web.v15.2, Version=15.2.5.0, Culture=neutral, PublicKeyToken=B88D1754D700E49A"
    Namespace="DevExpress.Web" TagPrefix="dx" %>--%>

<%@ Register Assembly="DevExpress.Web.v15.2, Version=15.2.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web" TagPrefix="dx" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="Style/estilo.css" rel="stylesheet" type="text/css" />
    <style type="text/css">
        BODY
        {
            
            font-size: 8.5pt;
        }
        TD
        {
            font-size: 8.5pt;
        }
        TH
        {
            font-size: 8.5pt;
        }
        BODY
        {
            margin: 0;
            padding: 0;
            text-align: center;
        }
        .centered
        {
            margin: 0 auto;
            text-align: left;
            width: 800px;
        }
        .style1
        {
            font-size: 18pt;
            color: #009933;
            font-family: Arial, Verdana, Helvetica, sans-serif;
            font-weight: bold;
            margin: 0px;
            text-decoration: none;
            font-style: normal;
            text-align: left;
            letter-spacing: 1px;
        }
        .style2
        {
            width: 176px;
        }
        .auto-style1 {
            width: 381px;
            height: 135px;
        }
        .auto-style2 {
            width: 323px;
            height: 112px;
        }
        .checkbox {
            margin-right: 0px;
            margin: 0px;
        }
    </style>
</head>
<body style="background-color: #3C4456">
    <form id="form1" runat="server">
    <div class="centered">
    <br />
    <br />
        <dx:ASPxRoundPanel ID="ASPxRoundPanel2" runat="server" Theme="SoftOrange" 
            HeaderText="Acceso de Usuario">
            <PanelCollection>
                <dx:PanelContent>
                    <table width="800" border="0" align="center" cellpadding="0" cellspacing="0" bgcolor="#ffffff">
                        <tr>
                            <td colspan="3" align="center">
                                &nbsp;<img alt="" class="auto-style2" src="Images/din sa.png" /><br /><br />
                            </td>
                        </tr>
                        <tr>
                            <td colspan="3" style="text-align:center" class="title" >
                                FINANCIA DIN <br />
                            </td>
                        </tr>
                        <tr>
                            <td width="10" height="350" valign="top">
                                &nbsp;
                            </td>
                            <td>
                                <table border="0" cellspacing="0" cellpadding="0" align="center">
                                    <tr>
                                        <td>
                                            <table width="100%">
                                                <tr>
                                                    <td class="style2">
                                                        &nbsp;<br />
                                                        <br />
                                                    </td>
                                                    <td>
                                                    </td>
                                                    <td>
                                                        &nbsp;
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td align="left" valign="top" class="style2">
                                                        <img src="Images/candado.jpg" style="height: 159px; width: 152px" />
                                                    </td>
                                                    <td width="490" align="left" valign="top">
                                                        <dx:ASPxRoundPanel ID="ASPxRoundPanel1" runat="server" Width="346px" EnableTheming="True"
                                                            HeaderText="" Theme="SoftOrange">
                                                            <PanelCollection>
                                                                <dx:PanelContent ID="PanelContent1" runat="server" SupportsDisabledAttribute="True">
                                                                    <asp:Login ID="Login1" runat="server" BackColor="#F7F7DE" BorderColor="#CCCC99" BorderStyle="Solid" BorderWidth="1px" Font-Names="Verdana" Font-Size="10pt" Width="314px">
                                                                        <CheckBoxStyle Wrap="True"/>
                                                                        <TitleTextStyle BackColor="#6B696B" Font-Bold="True" ForeColor="White" />
                                                                    </asp:Login>
                                                                </dx:PanelContent>
                                                            </PanelCollection>
                                                        </dx:ASPxRoundPanel>
                                                    </td>
                                                    <td width="25" align="left" valign="top">
                                                        &nbsp;
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td height="60" align="left" valign="top" class="style2">
                                                        &nbsp;
                                                    </td>
                                                    <td width="490" height="60" align="left" valign="top">
                                                        &nbsp;
                                                    </td>
                                                    <td width="25" height="25" align="left" valign="top">
                                                        &nbsp;
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                            <td width="20" valign="top">
                                &nbsp;
                            </td>
                        </tr>
                        <tr>
                            <td width="10" bgcolor="#d2d2c6">
                                &nbsp;
                            </td>
                            <td width="770" bgcolor="#d2d2c6">
                                &nbsp;
                            </td>
                            <td width="20" bgcolor="#d2d2c6">
                                &nbsp;
                            </td>
                        </tr>
                    </table>
                </dx:PanelContent>
            </PanelCollection>
        </dx:ASPxRoundPanel>
    </div>
    </form>
</body>
</html>
