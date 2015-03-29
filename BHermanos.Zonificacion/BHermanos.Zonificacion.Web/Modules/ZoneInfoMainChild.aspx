<%@ Page Title="" Language="C#" MasterPageFile="~/BHermanosSite.Master" AutoEventWireup="true" CodeBehind="ZoneInfoMainChild.aspx.cs" Inherits="BHermanos.Zonificacion.Web.Modules.ZoneInfoMainChild" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="../Styles/Site.css" rel="stylesheet" /> 
    <link href="../Styles/theme.css" rel="stylesheet" />
    <link href="../Styles/theme-elements.css" rel="stylesheet" />    
    <link href="../Vendor/bootstrap/css/bootstrap.css" rel="stylesheet" />
    <!-- Skin CSS -->
	<link rel="stylesheet" href="../Styles/skins/default.css" />
    <!--[if IE]>
			<link rel="stylesheet" href="css/ie.css">
	<![endif]-->
    <script src="../Vendor/jquery.js"></script>	
	<script src="../Vendor/bootstrap/js/bootstrap.js"></script>
    <script src="../Scripts/theme.js"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="content" runat="server">
    <div id="ContentDiv">
        <div id="HeaderDiv">
            <div id="Content" class="ContentRight">
                <asp:DropDownList ID="ddlPlazas" runat="server" Width="250px"></asp:DropDownList>                
            </div>
        </div>    
        <div class="left-column">
            <div id="MapMainDiv">Left Side Row 1</div>
        </div>
        <div class="right-column" style="height:100%;">
            <div id="InfoDiv">Right Side Row 1</div>      
        </div>
    </div>    
</asp:Content>
