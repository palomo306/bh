function PrepareDocument()
{
    $("a.close").click(function (e) {
        e.preventDefault();
        $("#loginform").hide();
        $(".lock").fadeIn();
    });

    $("#lockImage").click(function (e) {
        e.preventDefault();
        $("#loginform").toggle();
        $(".lock").fadeOut();
    });

    $("input#cancel_submit").click(function (e) {
        e.preventDefault();
        $("#loginform").hide();
        $(".lock").fadeIn();
    });
}