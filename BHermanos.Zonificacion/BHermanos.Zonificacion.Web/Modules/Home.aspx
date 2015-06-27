<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Home.aspx.cs" Inherits="BHermanos.Zonificacion.Web.Modules.Home" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %> 

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="../Styles/Site.css" rel="stylesheet" />
    <link href="../Styles/jquery.alerts.css" rel="stylesheet" />
    <script src="../Scripts/jquery-1.4.1.js"></script>
    <script src="../Scripts/jquery-ui.min.js"></script>
    <script src="../Scripts/jquery.ui.draggable.js"></script>
    <script src="../Scripts/jquery.alerts.min.js"></script>
    <script src="../Scripts/Generic.js"></script>
</head>
<body>
    <form id="form1" runat="server">
        <ajaxToolkit:ToolkitScriptManager ID="ScriptManager1" runat="server"></ajaxToolkit:ToolkitScriptManager>    
        <ajaxToolkit:TabContainer runat="server" ID="tabMainContainer" Width="100%" Height="100%" OnClientActiveTabChanged="ActiveTabChanged">
            <ajaxToolkit:TabPanel runat="server" ID="tabMain" HeaderText="Principal" CssClass="TabContent" >
                <ContentTemplate>  
                    <iframe id="tab0" seamless="seamless" src="MainZoneInfo.aspx" style="width:100%; height:100%;"></iframe>
                </ContentTemplate>  
            </ajaxToolkit:TabPanel>
        </ajaxToolkit:TabContainer>                
    </form>
</body>
</html>
