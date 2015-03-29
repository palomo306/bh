<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ResetPassword.aspx.cs" Inherits="BHermanos.Zonificacion.Web.Login.ResetPassword" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="../Styles/Site.css" rel="stylesheet" />
    <link href="../Styles/Login.css" rel="stylesheet" />
    <link href="../Styles/jquery.alerts.css" rel="stylesheet" />
    <link href="../Styles/skins/default.css" rel="stylesheet" />
    <script src="../Scripts/jquery.js"></script>
    <script src="../Scripts/jquery.ui.draggable.js"></script>
    <script src="../Scripts/jquery.alerts.min.js"></script>
    <script src="../Scripts/Generic.js"></script>    
    <script src="../Scripts/Login.js"></script>
    <script type="text/javascript">
        if (window.top != window.self) {
            window.open("Login.aspx", "_top");
        }

        function pageLoad() {
            var manager = Sys.WebForms.PageRequestManager.getInstance();
            manager.add_endRequest(endRequest);
            manager.add_beginRequest(OnBeginRequest);
        }

        function OnBeginRequest(sender, args) {
            FullScreenBackGround("divBack");
            document.getElementById("divBack").style.display = "inline";
        }

        function endRequest(sender, args) {
            document.getElementById("divBack").style.display = "none";
        }
    </script>
</head>
<body>
    <form id="signin" runat="server"> 
        <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
        <asp:UpdateProgress runat="server" id="PageUpdateProgress" AssociatedUpdatePanelID="UpdatePanel1" DisplayAfter="100" style="z-index:1000; position: absolute;left: 49%;top: 49%;visibility:visible;vertical-align:middle;">
            <ProgressTemplate>
                <img src="../Images/AjaxIndicator.gif" />
            </ProgressTemplate>
        </asp:UpdateProgress>        
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
                <div id="cont">
                    <div id="loginform" class="box form">
                        <h2>Cambio de Password <a href="#" class="close">Cerrar</a></h2>
                        <div class="formcont">
                            <fieldset id="signin_menu">
                                <table border="0">
                                    <tr>
                                        <td  style="vertical-align:middle">
                                            <img src="../Images/PasswordReset.png" width="120" />
                                        </td>
                                        <td>
                                            <span class="message">Estimado <asp:Label ID="lblNombre" runat="server" Text="Label"></asp:Label>, por favor escriba su contraseña y confírmelo para restablecerla</span>                                
                                            <table>
                                                <tr>
                                                    <td>
                                                        <label for="username">Contraseña:&nbsp;</label>
                                                        <input id="password" name="password" value="" title="password" class="required" tabindex="5" type="password" runat="server" />
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <label for="password">Confirmación:&nbsp;</label>
                                                        <input id="password1" name="password" value="" title="password" class="required" tabindex="5" type="password" runat="server" />
                                                    </td>                                            
                                                </tr>
                                                <tr>
                                                    <td colspan="2">
                                                        <p class="remember">
                                                            <asp:Button ID="signin_submit" runat="server" Text="Aceptar" TabIndex="6" OnClick="signin_submit_Click" CssClass="btn btn-primary btn-sm" />                                                    
                                                            <input id="cancel_submit" value="Cancelar" tabindex="7" type="button" class="btn btn-primary btn-sm" />
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
