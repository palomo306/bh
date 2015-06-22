<%@ Page Title="" Language="C#" MasterPageFile="~/BHermanosSite.Master" AutoEventWireup="true" CodeBehind="ZoneInfoChild.aspx.cs" Inherits="BHermanos.Zonificacion.Web.Modules.ZoneInfoChild" %>

<%@ Register Assembly="EGIS.Web.Controls" Namespace="EGIS.Web.Controls" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="../Styles/Site.css" rel="stylesheet" /> 
    <link href="../Styles/jquery.alerts.css" rel="stylesheet" />
    <link href="../Styles/theme.css" rel="stylesheet" />
    <link href="../Styles/theme-elements.css" rel="stylesheet" />    
    <link href="../Vendor/bootstrap/css/bootstrap.css" rel="stylesheet" />
    <link href="../Styles/Splitter.css" rel="stylesheet" />
    <!-- Skin CSS -->
	<link rel="stylesheet" href="../Styles/skins/default.css" />
    <!--[if IE]>
			<link rel="stylesheet" href="css/ie.css">
	<![endif]-->
    <script src="../Vendor/jquery.js"></script>	
    <script src="../Vendor/jquery.stellar.js"></script>
    <script src="../Scripts/jquery.ui.draggable.js"></script>
    <script src="../Scripts/jquery.alerts.min.js"></script>
	<script src="../Vendor/bootstrap/js/bootstrap.js"></script>
    <script src="../Scripts/theme.js"></script>
    <script src="../Scripts/Generic.js"></script>    
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="contentHead" runat="server">
    <div role="main" class="main">
        <section class="page-top">
			<div class="container">
				<div class="row">
					<div class="col-md-12">
						<h2>
                            <asp:Label ID="lblHead" runat="server" Text="Label"></asp:Label>
						</h2>
					</div>
				</div>
			</div>
		</section>
        <div class="sort-source-wrapper">
			<div class="container">
                <ul class="nav nav-pills sort-source secundary pull-right" data-sort-id="portfolio" data-option-key="filter">
                    <li>
                        <table style="float:right;" border="0">
                            <tr>
                                <td>
                                    <h5>
                                        Plaza:
                                    </h5>                                    
                                </td>    
                                <td>
                                    <h5>
                                        Fecha inicial:
                                    </h5>                                    
                                </td>    
                                <td>
                                    <h5>
                                        Fecha final:
                                    </h5>                                    
                                </td>                                
                                <td id="cellZonaHead" runat="server">
                                    <h5>
                                        Zona:   
                                    </h5>
                                </td>
                                <td id="cellSubzonaHead" runat="server">
                                    <h5>
                                        Subzona:
                                    </h5>
                                </td>
                            </tr>
                            <tr>
                                <td class="SeparateCell">
                                    <input id="hdnZonaId" type="hidden" runat="server" />
                                    <input id="hdnSubzonaId" type="hidden" runat="server" />                                            
                                    <asp:HiddenField ID="hdnShowBackGroup" runat="server" ClientIDMode="Static" />
                                    <asp:DropDownList ID="ddlPlazas" runat="server" Width="250px" CssClass="DropDown" OnSelectedIndexChanged="ddlPlazas_SelectedIndexChanged" AutoPostBack="True"></asp:DropDownList>                                    
                                </td>
                                <td class="SeparateCell">
                                    <asp:DropDownList ID="ddlStartDate" runat="server" Width="150px" CssClass="DropDown" AutoPostBack="True" OnSelectedIndexChanged="ddlPlazas_SelectedIndexChanged"></asp:DropDownList>
                                </td>
                                <td class="SeparateCell">
                                    <asp:DropDownList ID="ddlEndDate" runat="server" Width="150px" CssClass="DropDown" AutoPostBack="True" OnSelectedIndexChanged="ddlPlazas_SelectedIndexChanged"></asp:DropDownList>
                                </td>
                                <td id="cellZonaName" runat="server" class="SeparateCell">  
                                    <asp:TextBox ID="txtCurrentZona" runat="server" Width="150px" CssClass="form-control"></asp:TextBox>
                                </td>
                                <td id="cellSubonaName" runat="server" class="SeparateCell">
                                    <asp:TextBox ID="txtCurrentSubzona" runat="server" Width="150px" CssClass="form-control"></asp:TextBox>                            
                                </td>
                            </tr>
                        </table>                                                    				
                    </li>
                </ul>
			</div>
		</div>
    </div>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="content" runat="server">
    <div id="ContentDiv">
        <div class="left-column">
            <div id="MapMainDiv2" class="row featured-boxes">
                <div class="featured-box featured-box-primary">    
                    <div class="scrolledGridView featured-box box-content">
                        <cc1:sfmap ID="sfmMainMap" runat="server" CacheOnClient="false" Width="450px" Height="200px" ProjectName="~/Maps/MapDinamic.egp" /> 
                    </div>
                </div>
            </div>
            <div id="TotalDiv">
                <div id="InfoDivTotales" class="row featured-boxes">
                    <div style="width:175px; float:left; margin:3px;">
                        <div class="featured-box featured-box-primary">    
                            <div class="scrolledGridView featured-box box-content">
					            <h4 class="small">
                                    <asp:Label ID="lblTotalDesc" runat="server" Text="Label"></asp:Label>
					            </h4>
					            <p id="lblTotalQty" runat="server"></p>
				            </div>
                        </div>
                    </div>
                    <div id="divUmbrales" runat="server">           
                        
                    </div>
                </div>
            </div>      
        </div>
        <div class="right-column">
            <div id="InfoDiv" class="featured-boxes">
                <div id="rightTopPane2" class="featured-box featured-box-primary">    
                    <div class="scrolledGridView featured-box box-content">
					    <div>
                            <asp:Label ID="lblNoDataMessage" runat="server"></asp:Label>
                            <asp:GridView ID="dgReportTab" runat="server" CellPadding="4" ForeColor="#333333" GridLines="None" CssClass="Grid" OnRowDataBound="dgReportTab_RowDataBound" Width="100%">
                                <AlternatingRowStyle BackColor="White" />
                                <EditRowStyle BackColor="#2461BF" CssClass="CellText" />
                                <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" CssClass="CellText" />
                                <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" Font-Size="Small"  CssClass="CellText"/>
                                <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" CssClass="CellText" />
                                <RowStyle BackColor="#EFF3FB" Font-Size="XX-Small" CssClass="CellText" />
                                <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" CssClass="CellText" />
                                <SortedAscendingCellStyle BackColor="#F5F7FB" CssClass="CellText" />
                                <SortedAscendingHeaderStyle BackColor="#6D95E1" CssClass="CellText" />
                                <SortedDescendingCellStyle BackColor="#E9EBEF" CssClass="CellText" />
                                <SortedDescendingHeaderStyle BackColor="#4870BE" CssClass="CellText" />
                            </asp:GridView>
                        </div>        
                               
					</div>
                </div>
                <div id="rightMiddlePane">
                    <table style="width:100%" >
                        <tr>
                            <td class="CellRight" style="vertical-align:top;">
                                <asp:Button ID="btnSelection" runat="server" Text="Regresar" CssClass="btn btn-primary btn-sm Invisible" OnClick="btn_Click" />
                                <asp:Button ID="btnRefreshMap" runat="server" Text="Regresar" CssClass="btn btn-primary btn-sm Invisible"  Width="50px" OnClick="btnRefreshMap_Click" />
                                <asp:Button ID="btnCancel" runat="server" Text="Regresar" OnClick="btnCancel_Click" CssClass="btn btn-primary btn-sm" />
                            </td>
                        </tr>
                    </table>                
                </div>
                <div id="rightBottonPane" class="featured-box featured-box-primary">
                     <div class="scrolledGridView featured-box box-content">
					    <div>
                            <asp:Label ID="lblColores" runat="server"></asp:Label>
                        </div>
                    </div>
                </div>
            </div>
        </div>    
    </div>   
    <div id="divBack" class="BackGroudPopUp" style="display:none; z-index:10000;" />        
</asp:Content>
