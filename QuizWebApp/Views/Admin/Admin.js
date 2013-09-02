$(function () {
    $.ajaxSetup({ cache: false });

    var updateViewState = function () {
        var currentStateVal = $("#currentState").val();
        if (currentStateVal != 'PleaseWait')
            $("#currentQuestion").attr("disabled", "disabled");
        else
            $("#currentQuestion").removeAttr("disabled");
    };

    updateViewState();

    var conn = $.hubConnection();
    var contextHub = conn.createHubProxy("Context");
    $("#currentState").live("change", function () {
        contextHub.invoke("UpdateCurrentState", $(this).val());
        updateViewState();
    });

    $("#currentQuestion").live("change", function () {
        $.post(
            "/Admin/CurrentQuestion",
            { "questionID": $(this).val() },
            function () {
                $(".question-body").load("/Admin/QuestionBody");
            });
    });

    conn.start();
});