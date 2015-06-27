<%@ Page Title="" Language="C#" AutoEventWireup="true" CodeBehind="MainForm.aspx.cs" Inherits="BHermanos.Zonificacion.Web.MainForm" %>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="Styles/jquery.alerts.css" rel="stylesheet" />
    <link href="Styles/Site.css" rel="stylesheet" />
    <link href="Styles/Menu.css" rel="stylesheet" />
    <script src="Scripts/jquery-1.4.1.js"></script>
    <script src="Scripts/jquery.ui.draggable.js"></script>
    <script src="Scripts/jquery.alerts.min.js"></script>
    <script src="Scripts/jquery.min.js"></script>
    <script src="Scripts/Hover.js"></script>
    <script src="Scripts/MenuMake.js"></script>
    <script src="Scripts/Generic.js"></script>
    <script src="Scripts/Menu.js"></script>    
</head>
<body>
    <form id="form1" runat="server" style="left:0px; top:0px; margin:0px;">
        <asp:ScriptManager ID="ScriptManager1" runat="server" EnablePageMethods="true"></asp:ScriptManager>        
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
                <table id="mainTable" class="Content" border="0">
                    <tr id="rowLogo">
                        <td style="border:solid 1px black; background-color:#fcfcfc;">
                            <img src="Images/BHermanosLogo.png" width="500" />
                            <div id="divTopMenu" style="float:right;">
                                <img src="Images/TopMenu/Home.png" width="24" class="Pointer" style="margin-right:5px;" onclick="OpenHomeScreen();" alt="Cerrar Aplicación" />
                                <img src="Images/TopMenu/Exit.png" width="24" class="Pointer" style="margin-right:5px;" onclick="CloseMainWindow();" alt="Cerrar Aplicación" />
                            </div>   
                        </td>                
                    </tr>
                    <tr id="rowMenu">
                        <td style="background-color:#BDD2FF;">
                            <ul runat="server" id="MainMenu" class="sf-menu">
			
		                    </ul>
                        </td>
                    </tr>
                    <tr id="rowContent">
                        <td>
                            <div id="ContentDiv" class="ContentDiv">
                                <iframe id="frmContent" seamless="seamless" src="Modules/Home.aspx" style="width:100%;"></iframe>
                            </div>                    
                        </td>
                    </tr>
                </table>           
            </ContentTemplate>
        </asp:UpdatePanel>
    </form>
    <div id="bg">
        <div>
            <table style="border-collapse:collapse; border-spacing:0px;">
                <tr>
                    <td><img src="images/bg.png" alt=""/> </td>
                </tr>
            </table>
        </div>
    </div>
    <div id="divBack" class="BackGroudPopUp" style="display:none; z-index:999;"></div>
    <div id="divUSerInfoExit" class="PopUp3" style="width:450px; height:75; display:none;">
        <table style="width:100%; height:75px; top:0px; left:0px; position:absolute;">
                <tr>
                <td class="CellCenter" style="background-color:White;">
                    <table style="border: 2px solid #C0C0C0; width:100%;">
                        <tr>
                            <td class="Title" colspan="2">
                                Fin de Sesión
                            </td>
                        </tr>
                        <tr>
                            <td style="height:2px;"></td>
                        </tr>
                        <tr>
                            <td class="LetraHeadUserInfo" style="border-top-style: solid; border-bottom-style: solid; border-top-width: 1px; border-bottom-width: 1px; border-top-color: Black; border-bottom-color: Black">
                                <img src="Images/AjaxIndicator.gif" />
                            </td>
                            <td class="LetraHeadUserInfo CellCenter" style="border-top-style: solid; border-bottom-style: solid; border-top-width: 1px; border-bottom-width: 1px; border-top-color: Black; border-bottom-color: Black">                                        
                                Por favor espere un momento mientras se cierra su sesión
                            </td>
                        </tr>
                        <tr>
                            <td style="height:2px;"></td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>   
    </div>
</body>
</html>
