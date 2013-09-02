$(function () {
    $.ajaxSetup({ cache: false });

    var conn = $.hubConnection();
    var contextHub = conn.createHubProxy("Context");

    $(".options input:radio").live("click", function () {
        contextHub.invoke("PlayerSelectedOptionIndex", $(this).val());
    });

    contextHub.on("CurrentStateChanged", function (newState) {
        $(".main-content").load("/Player/PlayerMainContent");
    });
    conn.start();
});