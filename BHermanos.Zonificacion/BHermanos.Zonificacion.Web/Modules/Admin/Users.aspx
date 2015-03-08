<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Users.aspx.cs" Inherits="BHermanos.Zonificacion.Web.Modules.Admin.Users" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="../../Styles/Site.css" rel="stylesheet" />
    <link href="../../Styles/jquery.alerts.css" rel="stylesheet" />
    <script src="../../Scripts/jquery-1.4.1.js"></script>
    <script src="../../Scripts/jquery-ui.min.js"></script>
    <script src="../../Scripts/jquery.ui.draggable.js"></script>
    <script src="../../Scripts/jquery.alerts.min.js"></script>
    <script src="../../Scripts/Generic.js"></script>
</head>
<body>
    <form id="form1" runat="server">
        <asp:ScriptManager ID="ScriptManager1" runat="server">
        </asp:ScriptManager>    
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
                 <table id="tableContent" class="Content" border="0">
                     <tr>
                         <td>&nbsp;</td>
                     </tr>
                     <tr>
                         <td class="CellCenterTitlePage">
                             Administración de Usuarios
                         </td>
                     </tr>
                     <tr>
                         <td>&nbsp;</td>
                     </tr>
                     <tr>
                         <td class="CellCenter">
                             <asp:Button ID="btnNew" runat="server" Text="Nuevo Usuario" onclick="btnNew_Click" CssClass="BotonGde" />
                         </td>
                     </tr>
                     <tr>
                         <td>&nbsp;</td>
                     </tr>
                     <tr>
                         <td class="CellCenter">
                            <table id="tableNoData" runat="server">
                                <tr>
                                    <td class="CellCenterTitlePage">
                                        No hay datos para mostrar
                                    </td>
                                </tr>
                            </table>
                            <div style="margin-left: auto; margin-right: auto; width:375px;">
                                <asp:DataGrid ID="dgUsers" runat="server" AutoGenerateColumns="False" 
                                    CellPadding="2" ForeColor="#333333" AllowPaging="True" OnItemCommand="dgUsers_ItemCommand" OnPageIndexChanged="dgUsers_PageIndexChanged">
                                    <AlternatingItemStyle BackColor="White" />
                                    <Columns>
                                        <asp:BoundColumn HeaderText="Id" DataField="Usr" ItemStyle-HorizontalAlign="Left" ItemStyle-CssClass="PaddingCell"></asp:BoundColumn>
                                        <asp:BoundColumn HeaderText="Nombre" DataField="Nombre" ItemStyle-HorizontalAlign="Left" ItemStyle-CssClass="PaddingCell"></asp:BoundColumn>
                                        <asp:BoundColumn HeaderText="Correo" DataField="Mail" ItemStyle-HorizontalAlign="Left" ItemStyle-CssClass="PaddingCell">
                                            <HeaderStyle HorizontalAlign="Center" />
                                        </asp:BoundColumn>
                                        <asp:TemplateColumn HeaderStyle-HorizontalAlign="Center" ItemStyle-CssClass="PaddingCell">
                                            <ItemTemplate>
                                                <asp:ImageButton ID="imgEdit" runat="server" CommandName="Edit" 
                                                    ImageUrl="~/Images/Edit.png" Enabled='<%# Convert.ToString(Eval("Estatus")) == "0" ? false : true %>' />
                                            </ItemTemplate>
                                        </asp:TemplateColumn>
                                        <asp:TemplateColumn HeaderStyle-HorizontalAlign="Center" ItemStyle-CssClass="PaddingCell">
                                            <ItemTemplate>
                                                <asp:ImageButton ID="imgStatus" runat="server" CommandName="ChangeStatus" CommandArgument='<%# Convert.ToString(Eval("Estatus")) %>'
                                                     ImageUrl='<%# Convert.ToString(Eval("Estatus")) == "0" ? "~/Images/StatusDown.png" : "~/Images/StatusUp.png" %>' />
                                            </ItemTemplate>
                                        </asp:TemplateColumn>
                                        <asp:TemplateColumn HeaderStyle-HorizontalAlign="Center" ItemStyle-CssClass="PaddingCell">
                                            <ItemTemplate>
                                                <asp:ImageButton ID="imgPassword" runat="server" CommandName="ResetPassword" 
                                                    ImageUrl="~/Images/Password.png" Enabled='<%# Convert.ToString(Eval("Estatus")) == "0" ? false : true %>' />
                                            </ItemTemplate>
                                        </asp:TemplateColumn>
                                    </Columns>
                                    <EditItemStyle BackColor="#2461BF" />
                                    <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                                    <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" HorizontalAlign="Center" CssClass="HeaderText" />
                                    <ItemStyle BackColor="#EFF3FB" CssClass="NormalText" />
                                    <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" Mode="NumericPages" />
                                    <SelectedItemStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                                </asp:DataGrid>
                            </div>                        
                         </td>
                     </tr>
                     <tr>
                         <td>&nbsp;</td>
                     </tr>
                     <tr>
                         <td class="CellCenter">
                             <asp:Button ID="btnNew2" runat="server" Text="Nuevo Usuario" onclick="btnNew_Click" CssClass="BotonGde" />
                         </td>
                     </tr>
                     <tr>
                         <td>&nbsp;</td>
                     </tr>
                 </table>
                <div id="divBack" class="BackGroudPopUp" style="display:none; z-index:999;"></div>
                <div id="divNewEdit" class="PopNewUser" style="width:320px; height:100px; display:none; z-index:1000;">
                    <table style="width:100%; height:100px; top:0px; left:0px; position:absolute;">
                        <tr>
                            <td  style="background-color:White; text-align:center;">
                                <table style="border: 2px solid #C0C0C0; width:100%;">
                                    <tr>
                                        <td class="Title" colspan="2" style="text-align:center;">
                                            <div>
                                                <span id="spanTitle" runat="server" style="float:left">Eliminación de Grupo de Activos</span>
                                                <div id="div3" style="float:right" class="CloseDiv">
                                                    <img src="../../Images/Close.png" width="16" class="Pointer" onclick="CloseDivNewEdit();" alt="Cerrar Ventana" />
                                                </div>  
                                            </div>                                      
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="height:10px;"></td>
                                    </tr>
                                    <tr>
                                        <td class="CellCenter" style="padding:3px;">
                                            <table>
                                                <tr>
                                                    <td class="CellLeft">Id:</td>
                                                    <td class="CellLeft">
                                                        <asp:TextBox ID="txtId" runat="server" Width="100px" MaxLength ="8"></asp:TextBox>
                                                    </td>
                                                </tr>                    
                                                <tr>
                                                    <td class="Separator">&nbsp;</td>
                                                </tr>                                                                   
                                                <tr>
                                                    <td class="CellLeft">Nombre:</td>
                                                    <td class="CellLeft">
                                                        <asp:TextBox ID="txtNombre" runat="server" Width="200" MaxLength="50"></asp:TextBox>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td class="Separator">&nbsp;</td>
                                                </tr>        
                                                <tr>
                                                    <td class="CellLeft">Correo:</td>
                                                    <td class="CellLeft">
                                                        <asp:TextBox ID="txtCorreo" runat="server" Width="200" MaxLength="50"></asp:TextBox>
                                                    </td>
                                                </tr>    
                                                <tr>
                                                    <td class="Separator">&nbsp;</td>
                                                </tr>        
                                                <tr>
                                                    <td class="CellLeft">Roles:</td>                                                
                                                    <td>
                                                        <asp:CheckBoxList ID="cblRoles" runat="server" DataTextField="Nombre" DataValueField="Id" RepeatColumns="2"></asp:CheckBoxList>
                                                    </td>
                                                </tr>                                                                                                                                                              
                                            </table>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="height:10px;"></td>
                                    </tr>
                                    <tr>
                                        <td colspan="2" class="CellCenter">                                        
                                            <asp:Button ID="btnSave" runat="server" Text="Aceptar" CssClass="Boton" OnClick="btnSave_Click" />
                                            &nbsp;
                                            <input type="button" value="Cancelar" onclick="CloseDivNewEdit();" class="Boton" />                                        
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
                <div id="divDelete" class="PopUpDelete" style="width:320px; height:100px; display:none; z-index:1000;">
                    <table style="width:100%; height:100px; top:0px; left:0px; position:absolute;">
                        <tr>
                            <td class="CellCenter" style="background-color:White;">
                                <table style="border: 2px solid #C0C0C0; width:100%;">
                                    <tr>
                                        <td class="Title" colspan="2" style="text-align:left;">
                                            <div>
                                                <span id="spanDelete" runat="server" style="float:left">Eliminación de usuario</span>
                                                <div id="div1" style="float:right" class="CloseDiv">
                                                    <img src="../../Images/Close.png" width="16" class="Pointer" onclick="CloseDeleteWindow();" alt="Cerrar Ventana" />
                                                </div>  
                                            </div>                                      
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="height:10px;"></td>
                                    </tr>
                                    <tr>
                                        <td  class="CellCenter" style="padding:3px;">
                                            <asp:Label ID="lblDelete" runat="server" CssClass="Label"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="height:10px;"></td>
                                    </tr>
                                    <tr>
                                        <td colspan="2" class="CellCenter">
                                            <input id="hdnDelete" type="hidden" runat="server" />                                        
                                            <input id="hdnDeleteType" type="hidden" runat="server" />                                        
                                            <asp:Button ID="btnDelete" runat="server" Text="Aceptar" CssClass="Boton" OnClick="btnDelete_Click" />
                                            &nbsp;
                                            <input type="button" value="Cancelar" onclick="CloseDeleteWindow();" class="Boton" />
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
                <div id="divPassword" class="PopUpDelete" style="width:320px; height:100px; display:none; z-index:1000;">
                    <table style="width:100%; height:100px; top:0px; left:0px; position:absolute;">
                        <tr>
                            <td class="CellCenter" style="background-color:White;">
                                <table style="border: 2px solid #C0C0C0; width:100%;">
                                    <tr>
                                        <td class="Title" colspan="2" style="text-align:left;">
                                            <div>
                                                <span id="spanPassword" runat="server" style="float:left">Eliminación de usuario</span>
                                                <div id="divTitlePassword" style="float:right" class="CloseDiv">
                                                    <img src="../../Images/Close.png" width="16" class="Pointer" onclick="CloseDeleteWindow();" alt="Cerrar Ventana" />
                                                </div>  
                                            </div>                                      
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="height:10px;"></td>
                                    </tr>
                                    <tr>
                                        <td  class="CellCenter" style="padding:3px;">
                                            <asp:Label ID="lblPassword" runat="server" CssClass="Label"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="height:10px;"></td>
                                    </tr>
                                    <tr>
                                        <td colspan="2" class="CellCenter">
                                            <input id="hdnPassword" type="hidden" runat="server" />                                        
                                            <asp:Button ID="btnPassword" runat="server" Text="Aceptar" CssClass="Boton" OnClick="btnPassword_Click" />
                                            &nbsp;
                                            <input type="button" value="Cancelar" onclick="ClosePasswordWindow();" class="Boton" />
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
