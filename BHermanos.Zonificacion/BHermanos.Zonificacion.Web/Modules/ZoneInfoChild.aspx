<%@ Page Title="" Language="C#" MasterPageFile="~/BHermanosSite.Master" AutoEventWireup="true" CodeBehind="ZoneInfoChild.aspx.cs" Inherits="BHermanos.Zonificacion.Web.Modules.ZoneInfoChild" %>
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
            
        </div>    
        <div class="left-column">
            <div id="MapDiv">Left Side Row 1</div>
            <div id="TotalDiv">
                <div class="row featured-boxes">
					<div class="col-md-3">
						<div class="featured-box featured-box-primary">
							<div class="box-content">
								<h4>Loved by Customers</h4>
								<p>Donec id elit non mi porta gravida at eget metus. Fusce dapibus.</p>
							</div>
						</div>
					</div>                </div>
            </div>
        </div>
        <div class="right-column" style="height:100%;">
            <div id="InfoDiv">Right Side Row 1</div>      
        </div>
    </div>    
</asp:Content>
