function DistributeMainTable() {
    var Width = document.body.scrollWidth + "px";
    var Height = document.body.clientHeight;
    var HeightDoc = document.documentElement.clientHeight;
    var WidthDoc = document.documentElement.clientWidth;
    if (HeightDoc > Height)
        Height = HeightDoc;
    if (WidthDoc > Width)
        Width = WidthDoc;
    document.getElementById("mainTable").style.width = Width;
    document.getElementById("mainTable").style.height = Height + "px";
    document.getElementById("rowLogo").style.height = 85 + "px";
    document.getElementById("rowMenu").style.height = 40 + "px";
    document.getElementById("rowContent").style.height = (Height - 140) + "px";
    document.getElementById("ContentDiv").style.height = (Height - 132) + "px";
    document.getElementById("frmContent").style.height = (Height - 135) + "px";
    document.getElementById("frmContent").frameBorder = 0;
}

function PrepareMenu() {
    DistributeMainTable();
    var example = $('#MainMenu').superfish({
        //add options here if required
    });
}

function OpenMenu(url) {
    document.getElementById("frmContent").src = url;
}

function CloseMainWindow() {
    FullScreenBackGround("divBack");
    document.getElementById("divBack").style.display = "inline";
    jConfirm("Está seguro que desea salir del Sistema?", "Salir de Sistema", function (r) {
        if (r == true) {
            //Se abre la ventana de espera
            document.getElementById("divBack").style.display = "inline";
            document.getElementById("divUSerInfoExit").style.display = "inline";
            PageMethods.SignOff(OnCloseSession);
        }
        else {
            document.getElementById("divBack").style.display = "none";
        }
    });
}

function OnCloseSession() {
    window.open("Login/LogIn.aspx", "_top");
}

function OpenHomeScreen()
{
    OpenMenu("../Modules/Home.aspx");
}