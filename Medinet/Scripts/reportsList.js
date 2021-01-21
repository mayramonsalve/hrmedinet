$(document).ready(function () {
    var value;
    var compare = '';
    var univariate = false;
    var final = false;
    var tips = $(".validateTips");
    function updateTips(t) {
        tips
				.text(t)
				.addClass("ui-state-highlight");
        setTimeout(function () {
            tips.removeClass("ui-state-highlight", 1500);
        }, 500);
    }

    if ($("#RepDemo").val() != "") {
        var reports = $("#contenido-small-list div a");
        $.each(reports, function (index) {
            var a_element = $(reports[index]);
            if (a_element.attr("id") != "PopulationReports" && a_element.attr("id") != "FinalReport")
                a_element.attr("id", "");
        });
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

    function GetUrl(value, id, compare, final) {
        $.post("/ChartReports/GetTestWithNoEvaluations", { test_id: id, final: final, compare_id: compare }, function (testN) {
            if (testN == '0')
                window.location.href = value + id + compare;
            else
                window.location.href = '/ChartReports/NoEvaluations?id_test=' + testN;
        });
    }

    var lang = { ok: "Ok", cancel: $("#Cancel").val() };
    var buttonsD = {};
    buttonsD[lang.ok] = function () {
        var id = $("#Tests option:selected");
        if (checkValue(id)) {
            $(this).dialog('close');
            GetUrl(value, id.val(), compare, final);
            //window.location.href = GetUrl(value, id.val(), compare);
        }
    };
    buttonsD[lang.cancel] = function () {
        univariate = false;
        hideDivCompare();
        $("#Tests").val("");
        $(this).dialog('close');
    };

    $('#Dialog').dialog({
        autoOpen: false,
        show: "highlight",
        hide: "highlight",
        width: "auto",
        modal: true,
        buttons: buttonsD
    });

    var buttonsSQ = {};
    buttonsSQ[lang.ok] = function () {
        var id = $("#TestsSeveralQuestionnaires option:selected");
        if (checkValue(id)) {
            $(this).dialog('close');
            GetUrl(value, id.val(), compare, false);
            //window.location.href = GetUrl(value, id.val(), compare);
        }
    };
    buttonsSQ[lang.cancel] = function () {
        univariate = false;
        hideDivCompare();
        $("#TestsSeveralQuestionnaires").val("");
        $(this).dialog('close');
    };

    $('#DialogSeveralQuestionnaires').dialog({
        autoOpen: false,
        show: "highlight",
        hide: "highlight",
        modal: true,
        width: "auto",
        buttons: buttonsSQ
    });

    var buttonsR = {};
    buttonsR[lang.ok] = function () {
        var id = $("#Questionnaires option:selected");
        if (checkValue(id)) {
            $(this).dialog('close');
            window.location.href = '/ChartReports/Ranking?questionnaire=' + id.val();
        }
    };
    buttonsR[lang.cancel] = function () {
        $("#Questionnaires").val("");
        $(this).dialog('close');
    };

    $('#DialogRanking').dialog({
        autoOpen: false,
        show: "highlight",
        hide: "highlight",
        modal: true,
        width: "auto",
        buttons: buttonsR
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

    $("#Tests").change(function () {
        $.cookie("TestCookie", $("#Tests").val(), { path: '/' });
        if (univariate) {
            $.post("/ChartReports/GetTestsToCompare", { test_id: $("#Tests").val() }, function (j) {
                if (j.length > 0) {
                    var options = '<option value="">' + $('#ViewRes').val() + '</option>';
                    for (var i = 0; i < j.length; i++) {
                        options += '<option value="' + j[i].optionValue + '">' + j[i].optionDisplay + '</option>';
                    }
                    $("#TestsToCompare").html(options);
                    showDivCompare();
                }
                else
                    hideDivCompare();
            });
        }
    });

    $("#TestsSeveralQuestionnaires").change(function () {
        $.cookie("TestSQCookie", $("#TestsSeveralQuestionnaires").val(), { path: '/' });
    });

    function showDivCompare() {
        $('#DivCompare').show();
    }

    function hideDivCompare() {
        $('#DivCompare').hide();
    }

    $("#TestsToCompare").change(function () {
        if ($("#TestsToCompare").val())
            compare = '&compare_id=' + $("#TestsToCompare").val();
        else
            compare = '';
    });

    function ChangeTestDDLValue() {
        $("#Tests").val($.cookie("TestCookie"));
    }

    function ChangeTestSQDDLValue() {
        $("#TestsSeveralQuestionnaires").val($.cookie("TestSQCookie"));
    }

    $("#PopulationReports").click(function () {
        ChangeTestDDLValue();
        univariate = false;
        value = '/ChartReports/PopulationGraphics?test_id=';
        $("#Dialog").dialog("open");
        return false;
    });
    $("#UnivariateReports").click(function () {
        ChangeTestDDLValue();        
        univariate = true;
        value = '/ChartReports/UniVariateGraphics?test_id=';
        $("#Dialog").dialog("open");
        return false;
    });
    $("#BivariateReports").click(function () {
        ChangeTestDDLValue();
        value = '/ChartReports/BiVariateGraphics?test_id=';
        $("#Dialog").dialog("open");
        return false;
    });
    $("#RegressionReports").click(function () {
        ChangeTestDDLValue();
        univariate = false;
        value = '/ChartReports/Results?test_id=';
        $("#Dialog").dialog("open");
        return false;
    });
    $("#FinalReport").click(function () {
        ChangeTestDDLValue();
        univariate = false;
        final = true;
        value = '/ChartReports/AnalyticalReport?test_id=';
        $("#Dialog").dialog("open");
        return false;
    });
    $("#TextReports").click(function () {
        ChangeTestDDLValue();
        univariate = true;
        value = '/ChartReports/TextAnswerReports?test_id=';
        $("#Dialog").dialog("open");
        return false;
    });
    $("#FrequencyReports").click(function () {
        ChangeTestDDLValue();
        univariate = false;
        value = '/ChartReports/FrequencyGraphics?test_id=';
        $("#Dialog").dialog("open");
        return false;
    });
    $("#CategoryReports").click(function () {
        ChangeTestDDLValue();
        univariate = false;
        value = '/ChartReports/CategoryGraphics?test_id=';
        $("#Dialog").dialog("open");
        return false;
    });
    $("#ComparativeReports").click(function () {
        ChangeTestSQDDLValue();
        univariate = false;
        value = '/ChartReports/ComparativeGraphics?test_id=';
        $("#DialogSeveralQuestionnaires").dialog("open");
        return false;
    });
    $("#SatisfactionReports").click(function () {
        ChangeTestSQDDLValue();
        univariate = false;
        value = '/ChartReports/SatisfactionTables?test_id=';
        $("#DialogSeveralQuestionnaires").dialog("open");
        return false;
    });
    $("#Ranking").click(function () {
        $("#DialogRanking").dialog("open");
        return false;
    });

    $("#Feedbacks").click(function () {
        window.location.href = '/Feedbacks/SendFeedback';
    });
});