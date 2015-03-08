<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="MainZoneInfo.aspx.cs" Inherits="BHermanos.Zonificacion.Web.Modules.MainZoneInfo" %>

<%@ Register Assembly="EGIS.Web.Controls" Namespace="EGIS.Web.Controls" TagPrefix="cc1" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %> 

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="../Styles/Site.css" rel="stylesheet" />
    <link href="../Styles/Splitter.css" rel="stylesheet" />
    <link href="../Styles/jquery.alerts.css" rel="stylesheet" />
    <script src="../Scripts/jquery-1.4.1.js"></script>
    <script src="../Scripts/Splitter.js"></script>
    <script src="../Scripts/jquery-ui.min.js"></script>
    <script src="../Scripts/jquery.ui.draggable.js"></script>
    <script src="../Scripts/jquery.alerts.min.js"></script>
    <script src="../Scripts/Generic.js"></script>
    <script type="text/javascript">
        function RefreshMap() {
            document.getElementById("btnRefreshMap").click();
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
        <ajaxToolkit:ToolkitScriptManager ID="ScriptManager1" runat="server"></ajaxToolkit:ToolkitScriptManager>    
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
                <div id="headContainer">
                    <table style="width:100%">
                        <tr>
                            <td class="CellRight">
                                <table style="float:right;" border="0">
                                    <tr>
                                        <td class="CellLeft">
                                            Estado:
                                        </td>
                                        <td class="CellLeft">
                                            Municipio:
                                        </td>
                                        <td id="cellZonaHead" class="CellLeft" runat="server">
                                            Zona:
                                        </td>
                                        <td id="cellSubzonaHead"  class="CellLeft" runat="server">
                                            Subzona:
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="CellLeft">
                                            <asp:DropDownList ID="cmbEstado" runat="server" Width="200px" OnSelectedIndexChanged="cmbEstado_SelectedIndexChanged" AutoPostBack="True"></asp:DropDownList>
                                        </td>
                                        <td class="CellLeft">
                                            <input id="hdnZonaId" type="hidden" runat="server" />
                                            <input id="hdnSubzonaId" type="hidden" runat="server" />                                            
                                            <asp:DropDownList ID="cmbMunicipio" runat="server" Width="200px" OnSelectedIndexChanged="cmbMunicipio_SelectedIndexChanged" AutoPostBack="True"></asp:DropDownList>
                                        </td>
                                        <td id="cellZonaName" class="CellLeft" runat="server">                                            
                                            <asp:TextBox ID="txtCurrentZona" runat="server" Width="150px"></asp:TextBox>
                                        </td>
                                        <td id="cellSubonaName" class="CellLeft" runat="server">                                            
                                            <asp:TextBox ID="txtCurrentSubzona" runat="server" Width="150px"></asp:TextBox>                            
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                    </table>                    
                </div>
                <div id="splitterContainer">
	                <div id="leftPane" onresize="ResizeMap()">
                        <cc1:SFMap ID="sfmMainMap" runat="server" CacheOnClient="false"/> 
	                </div>
	                <div id="rightPane">
                        <ajaxToolkit:TabContainer ID="tabRightPane" runat="server" Width="100%" ActiveTabIndex="0">
                            <ajaxToolkit:TabPanel runat="server" ID="tabMain" HeaderText="Visualización" CssClass="TabContent" Height="100%">
                                <ContentTemplate>                                    
                                    <div id="rightTopPane">
                                        <div id="scrolledGridView" class="scrolledGridView">
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
                                    <div id="rightMiddlePane">
                                        <table style="width:100%" >
                                            <tr>
                                                <td class="CellRight" style="vertical-align:top;">
                                                    <asp:Button ID="btnSelection" runat="server" Text="Regresar" CssClass="BotonMediano Invisible" OnClick="btn_Click" Width="50px" />
                                                    <asp:Button ID="btnRefreshMap" runat="server" Text="Regresar" CssClass="BotonMediano Invisible"  Width="50px" OnClick="btnRefreshMap_Click" ClientIDMode="Static" />
                                                    <asp:Button ID="btnCancel" runat="server" Text="Regresar" OnClick="btnCancel_Click" CssClass="BotonMediano" />
                                                </td>
                                            </tr>
                                        </table>                            
                                    </div>
                                    <div id="rightBottonPane">
                                        <div id="scrolledGridView2" class="scrolledGridView">
                                            <asp:GridView ID="dgvReportZonas" runat="server" CellPadding="4" ForeColor="#333333" GridLines="None" CssClass="Grid" OnRowDataBound="dgvReportZonas_RowDataBound">
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
                                    <div id="rightExtBottonPane">
                                        <table style="width:100%" >
                                            <tr>
                                                <td class="CellRight" style="vertical-align:top;">
                                                    <asp:Button ID="btnAll" runat="server" Text="Ver todo" CssClass="BotonMediano" OnClick="btnAll_Click" />&nbsp;
                                                    <asp:Button ID="btnNse" runat="server" Text="NSE" CssClass="BotonMediano" OnClick="btnNse_Click" />
                                                </td>
                                            </tr>
                                        </table>                            
                                    </div>
                                </ContentTemplate>  
                            </ajaxToolkit:TabPanel>
                            <ajaxToolkit:TabPanel runat="server" ID="tabInfoZona" HeaderText="Por zona" CssClass="TabContent" Height="100%" >
                                <ContentTemplate>
                                    <div id="rightTopPane2">
                                        <div id="scrolledGridView3" class="scrolledGridView">
                                            <asp:GridView ID="dgListZones" runat="server" CellPadding="4" ForeColor="#333333" GridLines="None" CssClass="Grid" AutoGenerateColumns="False" OnRowCommand="dgListZones_RowCommand">
                                                <AlternatingRowStyle BackColor="White" />
                                                <Columns>
                                                    <asp:BoundField DataField="Zona" HeaderText="Zona" />
                                                    <asp:TemplateField>
                                                        <ItemTemplate>
                                                            <asp:Button ID="btnSelecZone" runat="server" Text="Ver" CssClass="BotonMediano" CommandArgument='<%# Eval("ZoneId") %>' />
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:BoundField DataField="ZoneId" Visible="False" />
                                                </Columns>
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
                                    <div id="rightMiddlePane2">
                                        <table style="width:100%" >
                                            <tr>
                                                <td class="CellCenter HeaderText" style="vertical-align:top;">
                                                    <asp:Label ID="lblCurrentZonaName" runat="server" Text="Label"></asp:Label>
                                                </td>
                                            </tr>
                                        </table>                            
                                    </div>
                                    <div id="rightBottonPane2">
                                        <div id="scrolledGridView4" class="scrolledGridView">
                                            <asp:GridView ID="sgReportZoneSingle" runat="server" CellPadding="4" ForeColor="#333333" GridLines="None" CssClass="Grid">
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
                                </ContentTemplate>
                            </ajaxToolkit:TabPanel>
                        </ajaxToolkit:TabContainer>
	                </div>
                </div>
                <div id="divBack" class="BackGroudPopUp" style="display:none; z-index:999;"></div>
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
                                            <asp:Button ID="btnPdf" runat="server" Text="PDF" CssClass="Boton" OnClick="btnPdf_Click" />
                                            &nbsp;
                                            <asp:Button ID="btnExcel" runat="server" Text="Excel" CssClass="Boton" OnClick="btnExcel_Click" />
                                            &nbsp;
                                            <input type="button" value="Salir" onclick="CloseDivDetail();" class="Boton" />
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
            </ContentTemplate>
        </asp:UpdatePanel>
    </form>
</body>
</html>
