
$(document).ready(function () {
    var companyId;
    $('#company_CompanyType_Id').change(function () {
        if ($(this).val() == 3) {
            showDivSector();
        }
        else
            hideDivSector();
    });

    $('#company_Name').focus();

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

    function checkValue(id) {
        if (id.val() != null && id.val() != "") {
            return true;
        } else {
            id.addClass("ui-state-error");
            updateTips($("#Validate").val());
            return false;
        }
    }

    var lang = { ok: "Ok", cancel: $("#Cancel").val() };
    var buttonsD = {};
    buttonsD[lang.ok] = function () {
        var countryId = $("#Countries option:selected");
        var languageId = $("#Languages option:selected");
        if (checkValue(countryId)) {
            if (checkValue(languageId)) {
                CreateDemographics(countryId.val(), languageId.val());
            }
        }
    };
    buttonsD[lang.cancel] = function () {
        $("#Countries").val("");
        $("#Languages").val("");
        $(this).dialog('close');
    };

    $('#Dialog').dialog({
        autoOpen: false,
        show: "highlight",
        hide: "highlight",
        modal: true,
        buttons: buttonsD
    });
    $('#DialogClose').dialog({
        autoOpen: false,
        show: "highlight",
        hide: "highlight",
        modal: true,
        buttons:
        {
            Ok: function() {
					$( this ).dialog( "close" );
				}
        }
    });
});

function LoadDialog(company) {
    companyId = company;
    $("#Dialog").dialog("open");
}

//function closeDialog() {
//    $("#Dialog").dialog("close");
//    $("#DialogClose").dialog("open");
//    var dotCounter = 0;
//    (function redirect() {
//        setTimeout(function () {
//            $("#DialogClose").dialog('close');
//        }, 3000);
//    })();
//}

function CreateDemographics(countryId, languageId) {
    $.post("/Demos/CreateDemographics", { companyId: companyId, countryId: countryId, languageId: languageId }, function (j) {
        if (j == 1) {
            $("#Dialog").dialog("close");
            $("#DialogClose").dialog("open");
        }
    });
}

function showDivSector() {
    $('#sector').show();
}

function hideDivSector() {
    $('#sector').hide();
}