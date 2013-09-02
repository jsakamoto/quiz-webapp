$(function () {
    $.ajaxSetup({ cache: false });
    var updateView = function () {
        $(".main-content").load("/Dashboard/LatestDashboard");
    };

    var conn = $.hubConnection();
    var contextHub = conn.createHubProxy("Context");
    contextHub.on("CurrentStateChanged", updateView);
    contextHub.on("PlayerSelectedOptionIndex", updateView);
    conn.start();
});