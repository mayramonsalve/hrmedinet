
$(document).ready(function () {

//    var dotCounter = 0;
//    (function redirect() {
//        setTimeout(function () {
//            $('#Dialog').open();
//        }, 5000);
    //    })();


//    setTimeout(function () {
//       $('#Dialog').open();
//    }, 5000);

    var lang = { feedback: $("#Feedback").val(), home: $("#Home").val() };
    var buttonsD = {};

    buttonsD[lang.feedback] = function () {
        $(this).dialog('close');
        window.location.href = '/Feedbacks/SendFeedback';
    };
    buttonsD[lang.home] = function () {
        $(this).dialog('close');
        window.location.href = '/Home';
    };

    $('#Dialog').dialog({
        autoOpen: false,
        show: "highlight",
        hide: "highlight",
        modal: true,
        buttons: buttonsD
    });
});
