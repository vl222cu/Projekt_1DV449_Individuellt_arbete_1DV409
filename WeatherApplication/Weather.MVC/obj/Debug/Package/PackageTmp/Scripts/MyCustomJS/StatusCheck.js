$(document).ready(function () {
    var onlineStatus = navigator.onLine;
    if (onlineStatus == false) {
        $("#dialog").dialog({
            autoOpen: true,
            height: 50,
            width: 350
        });
        $("#myDialogText").text("You are currently offline");
    }
});