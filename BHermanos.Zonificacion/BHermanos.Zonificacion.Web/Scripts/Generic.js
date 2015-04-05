function FullScreenBackGround(Div) {
    var Width = document.body.scrollWidth + "px";
    var Height = document.body.clientHeight;
    var HeightDoc = document.documentElement.clientHeight;
    var WidthDoc = document.documentElement.clientWidth;
    if (HeightDoc > Height)
        Height = HeightDoc;
    if (WidthDoc > Width)
        Width = WidthDoc;
    document.getElementById(Div).style.width = Width;
    document.getElementById(Div).style.height = Height + "px";
}

function ShowMessage(title, text) {
    FullScreenBackGround("divBack");
    document.getElementById("divBack").style.display = "inline";
    jAlert(text, title, function () {
        document.getElementById("divBack").style.display = "none";
    });
}

function OpenNewWindow() {
    FullScreenBackGround("divBack");
    document.getElementById("divBack").style.display = "inline";
    $('#divNewEdit').show('slow');
}

function OpenDetailWindow() {
    FullScreenBackGround("divBack");
    document.getElementById("divBack").style.display = "inline";
    $('#divDetail').show('slow');
}


function CloseDivDetail() {
    $('#divDetail').hide('slow', function () {
        document.getElementById("divBack").style.display = "none";
    });
}

function CloseDivNewEdit() {
    $('#divNewEdit').hide('slow', function () {
        document.getElementById("divBack").style.display = "none";
    });
}

function OpenDeleteWindow() {
    FullScreenBackGround("divBack");
    document.getElementById("divBack").style.display = "inline";
    $('#divDelete').draggable().show('slow');
}

function CloseDeleteWindow() {
    $('#divDelete').hide('slow', function () {
        document.getElementById("divBack").style.display = "none";
    });
}

function OpenPasswordWindow() {
    FullScreenBackGround("divBack");
    document.getElementById("divBack").style.display = "inline";
    $('#divPassword').draggable().show('slow');
}

function ClosePasswordWindow() {
    $('#divPassword').hide('slow', function () {
        document.getElementById("divBack").style.display = "none";
    });
}
   

function SetHeightIframes() {    
    var i, frames;
    var Height = document.body.clientHeight;
    frames = document.getElementsByTagName("iframe");
    for (i = 0; i < frames.length; ++i) {
        frames[i].style.height = (Height - 35) + "px";
        frames[i].frameBorder = 0;
    }
}

/*Funciones para el manejo de los mapas*/
function SetSplitter() {
    $("#splitterContainer").splitter({ splitVertical: true, A: $('#leftPane'), B: $('#rightPane'), closeableto: 0 });
    ResizeMap();
}

function ResizeMap()
{
    var Width = $("#MapMainDiv").width() + "px";
    var Height = document.body.clientHeight - 60;
    document.getElementById("content_sfmMainMap").style.position = "relative";
    document.getElementById("content_sfmMainMap").style.height = (Height) + "px";
    document.getElementById("content_sfmMainMap").style.width = Width;
    document.getElementById("content_sfmMainMap").style.left = "0px";
    document.getElementById("content_sfmMainMap").style.top = "0px";
    document.getElementById("content_sfmMainMap").children[0].style.height = (Height) + "px";
    document.getElementById("content_sfmMainMap").children[0].style.width = Width;
    document.getElementById("content_sfmMainMap").children[0].style.top = "0px";
    document.getElementById("content_sfmMainMap").children[0].style.left = "0px";
    document.getElementById("content_sfmMainMap_gisImage").style.height = (Height) + "px";
    document.getElementById("content_sfmMainMap_gisImage").style.width = Width;
    document.getElementById("content_sfmMainMap_gisep").style.height = (Height) + "px";
    document.getElementById("content_sfmMainMap_gisep").style.width = Width;
    try {
        document.getElementById("tabRightPane_body").style.height = (Height - 30) + "px";
    }
    catch (e) {

    }    
}

function ResizeMap2()
{
    var Width = ($("#MapMainDiv2").width() - 14) + "px";
    var Height = $("#MapMainDiv2").height();
    document.getElementById("content_sfmMainMap").style.position = "relative";
    document.getElementById("content_sfmMainMap").style.height = (Height) + "px";
    document.getElementById("content_sfmMainMap").style.width = Width;
    document.getElementById("content_sfmMainMap").style.left = "0px";
    document.getElementById("content_sfmMainMap").style.top = "0px";
    document.getElementById("content_sfmMainMap").children[0].style.height = (Height) + "px";
    document.getElementById("content_sfmMainMap").children[0].style.width = Width;
    document.getElementById("content_sfmMainMap").children[0].style.top = "0px";
    document.getElementById("content_sfmMainMap").children[0].style.left = "0px";
    document.getElementById("content_sfmMainMap_gisImage").style.height = (Height) + "px";
    document.getElementById("content_sfmMainMap_gisImage").style.width = Width;
    document.getElementById("content_sfmMainMap_gisep").style.height = (Height) + "px";
    document.getElementById("content_sfmMainMap_gisep").style.width = Width;
    try {
        document.getElementById("tabRightPane_body").style.height = (Height - 30) + "px";
    }
    catch (e) {

    }    
}

function ActiveTabChanged(sender, e) {
    var index = sender.get_activeTabIndex();
    var iframe = document.getElementById("tab" + index);
    iframe.contentWindow.RefreshMap();
}

function SelectZone(zoneId, level, zoneName, subZoneName, btnAction) {
    if (level == "0")
        document.getElementById(zoneName).value = zoneId;
    else
        document.getElementById(subZoneName).value = zoneId;
    document.getElementById(btnAction).click();
}

function OpenMenu(url) {
    window.open(url,"_self");
}

/*---------------------------------------------------------------------------*/