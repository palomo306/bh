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
								<asp:GridView ID="dgZone" runat="server" CellPadding="4" ForeColor="#333333" GridLines="None" CssClass="Grid">
                                    <AlternatingRowStyle BackColor="White" />
                                    <EditRowStyle BackColor="#2461BF" />
                                    <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                                    <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                                    <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                                    <RowStyle BackColor="#EFF3FB" />
                                    <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                                    <SortedAscendingCellStyle BackColor="#F5F7FB" />
                                    <SortedAscendingHeaderStyle BackColor="#6D95E1" />
                                    <SortedDescendingCellStyle BackColor="#E9EBEF" />
                                    <SortedDescendingHeaderStyle BackColor="#4870BE" />
                                </asp:GridView>
							</div>
						</div>
					</div>
                </div>
            </div>
        </div>
        <div class="right-column" style="height:100%;">
            <div id="InfoDiv">Right Side Row 1</div>      
        </div>
    </div>    
</asp:Content>
