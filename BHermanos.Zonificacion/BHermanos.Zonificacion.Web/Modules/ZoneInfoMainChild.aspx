<%@ Page Title="" Language="C#" MasterPageFile="~/BHermanosSite.Master" AutoEventWireup="true" CodeBehind="ZoneInfoMainChild.aspx.cs" Inherits="BHermanos.Zonificacion.Web.Modules.ZoneInfoMainChild" %>

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
    <script src="../Scripts/jquery-1.4.1.js"></script>
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
						<h2>Visualización de Plazas</h2>
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
            <div id="MapMainDiv">
                <div class="featured-box featured-box-primary">    
                    <div class="scrolledGridView featured-box box-content">
                        <cc1:sfmap ID="sfmMainMap" runat="server" CacheOnClient="false" Width="450px" Height="200px"/> 
                    </div>
                </div>
            </div>
        </div>
        <div class="right-column" style="height:100%;">
            <div id="InfoDiv" class="featured-boxes">
                <div id="rightTopPane" class="featured-box featured-box-primary">    
                    <div class="scrolledGridView featured-box box-content">
					    <div>
                            <asp:GridView ID="dgZone" runat="server" CellPadding="4" ForeColor="#333333" GridLines="None" CssClass="Grid">
                                <AlternatingRowStyle BackColor="White" />
                                <EditRowStyle BackColor="#2461BF" />
                                <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                                <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" Font-Size="Small" />
                                <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                                <RowStyle BackColor="#EFF3FB" Font-Size="XX-Small" />
                                <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                                <SortedAscendingCellStyle BackColor="#F5F7FB" />
                                <SortedAscendingHeaderStyle BackColor="#6D95E1" />
                                <SortedDescendingCellStyle BackColor="#E9EBEF" />
                                <SortedDescendingHeaderStyle BackColor="#4870BE" />
                            </asp:GridView>
                        </div>                
					</div>
                </div>
                <div id="rightMiddlePane">
                    <table style="width:100%" >
                        <tr>
                            <td class="CellRight" style="vertical-align:top;">
                                <asp:Button ID="btnSelection" runat="server" Text="Regresar" CssClass="btn btn-primary btn-sm Invisible" OnClick="btn_Click" Width="50px" />
                                <asp:Button ID="btnRefreshMap" runat="server" Text="Regresar" CssClass="btn btn-primary btn-sm Invisible"  Width="50px" OnClick="btnRefreshMap_Click" ClientIDMode="Static" />
                                <asp:Button ID="btnCancel" runat="server" Text="Regresar" OnClick="btnCancel_Click" CssClass="btn btn-primary btn-sm" />
                            </td>
                        </tr>
                    </table>                
                </div>
                <div id="rightBottonPane" class="featured-box featured-box-primary">
                     <div class="scrolledGridView featured-box box-content">
					    <div>
                            <asp:GridView ID="dgvReportZonas" runat="server" CellPadding="4" ForeColor="#333333" GridLines="None" CssClass="Grid" OnRowDataBound="dgvReportZonas_RowDataBound">
                                <AlternatingRowStyle BackColor="White" />
                                <EditRowStyle BackColor="#2461BF" />
                                <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                                <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" Font-Size="Small" />
                                <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                                <RowStyle BackColor="#EFF3FB" Font-Size="XX-Small" />
                                <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                                <SortedAscendingCellStyle BackColor="#F5F7FB" />
                                <SortedAscendingHeaderStyle BackColor="#6D95E1" Font-Size="X-Small" />
                                <SortedDescendingCellStyle BackColor="#E9EBEF" />
                                <SortedDescendingHeaderStyle BackColor="#4870BE" />
                            </asp:GridView>
                        </div>
                    </div>
                </div>
                <div id="rightExtBottonPane">
                    <table style="width:100%" >
                        <tr>
                            <td class="CellRight" style="vertical-align:top;">
                                <asp:Button ID="btnAll" runat="server" Text="Ver todo" CssClass="btn btn-primary btn-sm" OnClick="btnAll_Click" />&nbsp;
                                <asp:Button ID="btnNse" runat="server" Text="NSE" CssClass="btn btn-primary btn-sm" OnClick="btnNse_Click" />
                            </td>
                        </tr>
                    </table>           
                </div>
            </div>       
        </div>   
    </div>    
    <!-- End Full page background technique -->        
    <div id="divBack" class="BackGroudPopUp" style="display:none; z-index:999;" />        
    <div id="divDetail" class="PopUpDetail" style="width:600px; height:250px; display:none; z-index:1000;">
        <table style="width:100%; height:100px; top:0px; left:0px; position:absolute;">
            <tr>
                <td class="CellCenter" style="background-color:White;">
                    <table style="border: 2px solid #C0C0C0; width:100%;">
                        <tr>
                            <td class="Title" colspan="2" style="text-align:left;">
                                <div>
                                    <span id="spanTitleDetail" runat="server" style="float:left">Eliminación de usuario</span>
                                    <div id="div1" style="float:right" class="CloseDiv">
                                        <img src="../Images/Close.png" width="16" class="Pointer" onclick="CloseDivDetail();" alt="Cerrar Ventana" />
                                    </div>  
                                </div>                                      
                            </td>
                        </tr>
                        <tr>
                            <td style="height:10px;"></td>
                        </tr>
                        <tr>
                            <td  class="CellCenter" style="padding:3px;">
                                <div style="overflow:auto; height:200px;">
                                    <asp:GridView ID="gvDetail" runat="server" CellPadding="4" ForeColor="#333333" GridLines="None" CssClass="Grid">
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
                            </td>
                        </tr>
                        <tr>
                            <td style="height:10px;"></td>
                        </tr>
                        <tr>
                            <td colspan="2" class="CellCenter">
                                <asp:Button ID="btnPdf" runat="server" Text="PDF" CssClass="btn btn-primary btn-sm" OnClick="btnPdf_Click" />
                                &nbsp;
                                <asp:Button ID="btnExcel" runat="server" Text="Excel" CssClass="btn btn-primary btn-sm" OnClick="btnExcel_Click" />
                                &nbsp;
                                <input type="button" value="Salir" onclick="CloseDivDetail();" class="btn btn-primary btn-sm" />
                            </td>
                        </tr>
                        <tr>
                            <td style="height:5px;"></td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>   
    </div>   
</asp:Content>
