$(function () {
    $('#signout').click(function () {
        if (confirm('サインアウトしますか?')) {
            $.post($(this).attr('href'))
                .done(function (data) {
                    location.href = data.url;
                });
        }
        return false;
    });

})