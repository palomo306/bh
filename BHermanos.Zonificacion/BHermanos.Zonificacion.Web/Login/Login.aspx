<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="BHermanos.Zonificacion.Web.Login.Login" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="../Styles/Site.css" rel="stylesheet" />
    <link href="../Styles/Login.css" rel="stylesheet" />
    <link href="../Styles/jquery.alerts.css" rel="stylesheet" />
    <script src="../Scripts/jquery.js"></script>
    <script src="../Scripts/jquery.ui.draggable.js"></script>
    <script src="../Scripts/jquery.alerts.min.js"></script>
    <script src="../Scripts/Generic.js"></script>    
    <script src="../Scripts/Login.js"></script>
        <script type="text/javascript">
        if (window.top != window.self) {
            window.open("Login.aspx", "_top");
        }
    </script>
</head>
<body>
    <form id="signin" runat="server"> 
        <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>        
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
                <div id="cont">
                    <div id="lockImage" class="box lock"> </div>
                    <div id="loginform" class="box form">
                        <h2>Autentificacion Requerida <a href="#" class="close">Cerrar</a></h2>
                        <div class="formcont">
                            <fieldset id="signin_menu">
                                <table border="0">
                                    <tr>
                                        <td  style="vertical-align:middle">
                                            <img src="../Images/LoginBig.png" />
                                        </td>
                                        <td>
                                            <span class="message">Por favor verifique sus datos antes de continuar</span>                                
                                            <table>
                                                <tr>
                                                    <td>
                                                        <label for="username">Usuario:</label>
                                                        <input id="username" name="username" value="" title="username" class="required" tabindex="4" type="text" runat="server" />                            
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <label for="password">Contraseña:&nbsp;</label>
                                                        <input id="password" name="password" value="" title="password" class="required" tabindex="5" type="password" runat="server" />
                                                    </td>                                            
                                                </tr>
                                                <tr>
                                                    <td colspan="2">
                                                        <p class="remember">
                                                            <asp:Button ID="signin_submit" runat="server" Text="Aceptar" TabIndex="6" OnClick="signin_submit_Click" />                                                    
                                                            <input id="cancel_submit" value="Cancelar" tabindex="7" type="button" />
                                                        </p>
                                                    </td>
                                                </tr>
                                            </table>                                    
                                            <p class="clear"></p>                                                                    
                                        </td>
                                    </tr>
                                </table>                    
                            </fieldset>
                        </div>
                        <div class="formfooter"></div>
                    </div>
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>
    </form>    
    <!-- Begin Full page background technique -->
    <div id="bg">
        <div>
            <table style="border-collapse:collapse; border-spacing:0px;">
                <tr>
                    <td><img src="../images/bg.png" alt=""/> </td>
                </tr>
            </table>
        </div>
    </div>
    <!-- End Full page background technique -->
    <div id="divBack" class="BackGroudPopUp" style="display:none; z-index:999;"></div>
</body>
</html>
