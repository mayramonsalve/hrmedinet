//$(document).ready(function () {
//    $(this).removeClass("loading");
//    $("body").on({
//        ajaxStart: function () {
//            $(this).addClass("loading");
//        },
//        ajaxStop: function () {
//            $(this).removeClass("loading");
//        }
//    });
//});
$(document).ready(function () {
    $("html").removeClass("loading");
    $(document)
    .ajaxStart(function () {
        $("html").addClass('loading');
    })
    .ajaxStop(function () {
        $("html").removeClass('loading');
    });
});