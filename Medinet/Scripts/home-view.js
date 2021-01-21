$(document).ready(function () {
    //start slider
//    if ($.cookie("TestCookie")=="")
//        $.cookie("TestCookie", "", { path: '/' });

//    $('#slider1').anythingSlider({
//        theme: 'metallic',
//        buildNavigation: false
//    });

    /*function resizeSlider() {
        //get the screen resolution
        var width = $(window).width();

        //for 1024x768 and lower
        if (parseInt(width) < 1200) {
        $(".anythingSlider").width(480);
        }
        else if (parseInt(width) < 1300) {
        $(".anythingSlider").width(630);
        }
        else
        $(".anythingSlider").width(665);
        $(".anythingSlider").width("95%");
    }

    resizeSlider();

    $(window).resize(function () {
        resizeSlider();
    });*/

    $("label").inFieldLabels();

    /*********/
    var tips = $(".validateTips");
    function updateTips(t) {
        tips
				.text(t)
				.addClass("ui-state-highlight");
        setTimeout(function () {
            tips.removeClass("ui-state-highlight", 1500);
        }, 500);
    }

    function checkRegexp(o, regexp, n) {
        if (!(regexp.test(o.val()))) {
            o.addClass("ui-state-error");
            updateTips(n);
            return false;
        } else {
            return true;
        }
    }
    var lang = { ok: "Ok", cancel: $("#Cancel").val() };
    var buttonsD = {};
    //buttonsD[lang.ok] = function () {
    //    var id = $("#Companies option:selected");
    //    if (checkValue(id)) {
    //        $(this).dialog('close');
    //        var href = '/Home/ClimateMap?company=' + id.val();
    //        redirectToMap(href, id.val());
    //    }
    //};
    buttonsD[lang.ok] = function () {
        var id = $("#Tests option:selected");
        if (checkValue(id)) {
            $(this).dialog('close');
            var href = '/Home/ClimateMap?test_id=' + id.val();
            redirectToMap(href, id.val());
        }
    };
    buttonsD[lang.cancel] = function () {
        $(this).dialog('close');
    };

    $('#Dialog').dialog({
        autoOpen: false,
        show: "highlight",
        hide: "highlight",
        modal: true,
        width: "auto",
        buttons: buttonsD
    });

    function checkValue(id) {
        if (id.val() != null && id.val() != "") {
            return true;
        } else {
            id.addClass("ui-state-error");
            updateTips($("#Validate").val());
            return false;
        }
    }

    $("#Climate").click(function (e) {
        e.preventDefault();
        //if ($("#Rol").val() != "HRAdministrator") {
        //    redirectToMap('/Home/ClimateMap', 0);
        //}
        //else
            //getCompaniesList();
        $.post("/Tests/GetTestsForGlobalClimate", {}, function (j) {
            var options = '<select id="Tests"><option value="">' + $('#ViewRes').val() + '</option>';
            for (var i = 0; i < j.length; i++) {
                options += '<option value="' + j[i].optionValue + '">' + j[i].optionDisplay + '</option>';
            }
            options += '</select>';
            $("#DivDDL").html(options);
            $("#Tests").addClass("input-background");
            $("#Tests").addClass("short");
        });
        $("#Dialog").dialog("open");
    });

    function getCompaniesList() {
        $.post("/Tests/GetCompaniesForHRAdministrator", {}, function (j) {
            var options = '<select id="Companies"><option value="">' + $('#ViewRes').val() + '</option>';
            for (var i = 0; i < j.length; i++) {
                options += '<option value="' + j[i].optionValue + '">' + j[i].optionDisplay + '</option>';
            }
            options += '</select>';
            $("#DivDDL").html(options);
            $("#Companies").addClass("input-background");
            $("#Companies").addClass("short");
        });
        $("#Dialog").dialog("open");
    }
    function redirectToMap(href, test) {
        $.post("/Home/GetHRef", { test: test }, function (newHref) {
            var finalHref = (newHref == "-") ? href : newHref;
            window.location.href = finalHref;
        });
    }
});