<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ZoneInfo.aspx.cs" Inherits="BHermanos.Zonificacion.Web.Modules.ZoneInfo" %>

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
        function RefreshMap()
        {
            document.getElementById("btnRefreshMap").click();
        }
    </script>
</head>
<body>
        <form id="form1" runat="server">
        <asp:ScriptManager ID="ScriptManager1" runat="server">
        </asp:ScriptManager>    
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
                        <div id="rightTopPane3">
                            <div id="scrolledGridView" class="scrolledGridView">
                                <asp:GridView ID="dgReportTab" runat="server" CellPadding="4" ForeColor="#333333" GridLines="None" CssClass="Grid" OnRowDataBound="dgReportTab_RowDataBound" Width="100%">
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
                                        <asp:Button ID="btnSelection" runat="server" Text="Regresar" CssClass="BotonMediano Invisible" OnClick="btn_Click" />
                                        <asp:Button ID="btnRefreshMap" runat="server" Text="Regresar" CssClass="BotonMediano Invisible"  Width="50px" OnClick="btnRefreshMap_Click" />
                                        <asp:Button ID="btnCancel" runat="server" Text="Regresar" OnClick="btnCancel_Click" CssClass="BotonMediano" />
                                    </td>
                                </tr>
                            </table>                            
                        </div>
                        <div id="rightBottonPane2">
                            <div id="scrolledGridView2" class="scrolledGridView">
                                <asp:Label ID="lblColores" runat="server"></asp:Label>
                            </div>
	                    </div>                      
	                </div>
                </div>
                <div id="divBack" class="BackGroudPopUp" style="display:none; z-index:999;"></div>
            </ContentTemplate>
        </asp:UpdatePanel>
    </form>

</body>
</html>
