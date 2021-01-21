var matchingTable;
var date_format = "";
$(document).ready(function () {
    date_format = $("#culture").val() == "es" ? "dd/mm/yy" : "mm/dd/yy";

    matchingTable = $('#Matching').dataTable({
        "bPaginate": false,
        "bFilter": false,
        "bInfo": false,
        "bJQueryUI": true,
        "bRetrieve": true,
        "bSort": false,
        "bAutoWidth": false,
        "oLanguage": {
            "sEmptyTable": $('#NoData').val()
        }
    });

    $("#StartDate").datepicker({
        minDate: new Date(),
        dateFormat: date_format,
        changeMonth: true,
        changeYear: true,
         onClose: function( selectedDate )
        {
            $("#EndDate").datepicker("option", "minDate", selectedDate);
        }
    }).datepicker("setDate", $('#inicial').val());

    $("#EndDate").datepicker({
        minDate: new Date(),
        dateFormat: date_format,
        changeMonth: true,
        changeYear: true,
        onClose: function (selectedDate) {
            $("#StartDate").datepicker("option", "maxDate", selectedDate);
        }
    }).datepicker("setDate", $('#final').val());

    if ($('#weighted').is(':checked'))
        loadDivCategories();

    if ($('#group').is(':checked'))
        hideDivDisordered();
    else
        showDivDisordered();

    if ($('#one').is(':checked')) {
        hideDivButtonSeveralQuestionnaires()
        showDivOneQuestionnaire();
    }
    else {
        hideDivOneQuestionnaire();
        showDivButtonSeveralQuestionnaires();
    }

    $('#group').change(function () {
        if ($(this).is(':checked'))
            hideDivDisordered();
        else
            showDivDisordered();
    });

    $('#one').change(function () {
        if ($(this).is(':checked')) {
            hideDivButtonSeveralQuestionnaires()
            showDivOneQuestionnaire();
        }
        else {
            hideDivOneQuestionnaire();
            showDivButtonSeveralQuestionnaires();
        }
    });

    $('#weighted').change(function () {
        if ($(this).is(':checked'))
            showDivCategories();
        else
            hideDivCategories();
    });

    $('#test_Name').focus();

    $('#test_Company_Id').change(function () {
        showDivDemographicsInTest();
        showDivOneQuestionnaireCheckBox();
        if ($(this).val()) {
            updateQuestionnairesList();
            updateNotStartedTestsList();
            updateDemographicsList();
        }
        else {
            $("#test_Questionnaire_Id").html(null);
            $("#test_PreviousTest_Id").html(null);
            $("#Demographics").html(null);
            $("#QuestionnairesInTest").html(null);
            $("#DemographicSelector").html(null);
            $('#weighted').attr("false");
            hideDivCategories();
        }
    });

    $('#test_PreviousTest_Id').change(function () {
        if ($(this).val()) {
            hideDivDemographicsInTest();
            hideDivOneQuestionnaireCheckBox();
        }
        else {
            showDivDemographicsInTest();
            showDivOneQuestionnaireCheckBox();
        }
    });

    $('#Demographics').change(function () {
        if ($(this).val()) {
            updateDemographicsSelectorList();
        }
        else {
            $("#DemographicSelector").html(null);
        }
    });

    $('#DemographicSelector').change(function () {
        if ($(this).val()) {
            $("#QuestionnairesInTest").multiselect("enable");
            updateTable("Demographic");
        }
        else {
            $("#QuestionnairesInTest").multiselect("disable");
            matchingTable.fnClearTable();
        }
    });

    $('#QuestionnairesInTest').change(function () {
        if ($(this).val()) {
            updateTable("Questionnaire");
        }
        else {
            matchingTable.fnClearTable();
        }
    });

    $("#Calculate").click(function () {
        $("#Dialog").dialog("open");
        return false;
    });

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
    var confidenceLevel = 1.9600;
    $('#CLDD').change(function () {
        $.post("/Tests/GetConfidenceLevelValueById", { id: $(this).val() }, function (j) {
            confidenceLevel = j;
        });
    });
    var standardError = 0.06;
    $('#SEDD').change(function () {
        $.post("/Tests/GetStandardErrorValueById", { id: $(this).val() }, function (j) {
            standardError = j;
        });
    });

    $('#ButtonSeveralQuestionnaires').click(function () {
        $("#DialogQ").dialog("open");
        return false;
    });

    var lang = { ok: "Ok", cancel: $("#Cancel").val() };
    var buttonsD = {};
    buttonsD["Ok"] = function () {
        var CL = $("#CLDD option:selected").val();
        var SE = $("#SEDD option:selected").val();
        var P = $("#P").val();
        var valid = checkRegexp($("#P"), /^[1-9][0-9]*$/, $("#ValidateInt").val());
        if (valid && confidenceLevel != 0 && standardError != 0) {
            var EN = (confidenceLevel * confidenceLevel * P * 0.5 * (1 - 0.5)) / (((P - 1) * (standardError * standardError)) + ((confidenceLevel * confidenceLevel * 0.5 * (1 - 0.5))));
            EN = Math.round(EN);
            $("#test_EvaluationNumber").val(EN);
            $("#test_ConfidenceLevel_Id").val(CL);
            $("#test_StandardError_Id").val(SE);
            $("#test_NumberOfEmployees").val(P);
            $('#Dialog').parent().appendTo(jQuery('form:first'));
            $(this).dialog("close");
        }
    };
    buttonsD[$("#Cancel").val()] = function () {
        $(this).dialog('close');
    };

    var buttonsQ = {};
    buttonsQ["Ok"] = function () {
        if (true) {
            $('#DialogQ').parent().appendTo(jQuery('form:first'));
            $(this).dialog("close");
        }
    };
    buttonsQ[$("#Cancel").val()] = function () {
        $(this).dialog('close');
    };

    $('#Dialog').dialog({
        autoOpen: false,
        show: "highlight",
        hide: "highlight",
        modal: true,
        buttons: buttonsD
    });

    $('#DialogQ').dialog({
        autoOpen: false,
        show: "highlight",
        hide: "highlight",
        modal: true,
        width: "500px",
        buttons: buttonsQ,
        open: function (data) {
            matchingTable.fnSetColumnVis(0, false);
        },
        close: function () {

        }
    });

    $(".multiselect").multiselect({
        //        selectedText: "# de # seleccionado",
        selectedList: 10
    });

});

//function updateTable(field) {
//    if (field == "Demographic") {
//        reloadFromDemographic();
//    }
//    else {
//        reloadFromQuestionnaire();
//    }
//}

//function reloadFromDemographic() {

//}

function updateTable(field) {
    matchingTable.fnClearTable();
    var valor = $("#QuestionnairesInTest option:selected");
    $("#QuestionnairesInTest option:selected").each(function () {
        var ddl_id = $(this).val();
        var text = $(this).text();
        var demographic_id = $("#DemographicSelector").val();
        var code = "<select id='Selector_" + ddl_id + "' multiple='multiple' class='required input-background short multiselectNew' name='Selector_" + ddl_id + "'>";
        var options = '';// '<option value="">' + $('#ViewRes').val() + '</option>';
        $.ajax({
            async: false,
            timeout: 3000,
            type: "POST",
            url: "/Tests/GetSelectorsByDemographicAndCompany",
            data: { demographic_id: demographic_id, company_id: $("#test_Company_Id").val() }
        }).done(function (data) {
            if (data) {
                $.each(data, function (index, value) {
                    options += '<option value="' + value.optionValue + '">' + value.optionDisplay + '</option>';
                });
                code = code + options + "</select>";
                matchingTable.fnAddData([ddl_id, text, code]);
                $(".multiselectNew").multiselect({
//                    selectedText: "# de # seleccionado",
                    selectedList: 10
                });
                $(".multiselectNew").multiselect("uncheckAll");
                $(".multiselectNew").multiselect('refresh');
            }
        });
    });

}

function updateNotStartedTestsList() {
    $.post("/Tests/GetNotStartedTestsByCompany", { company_id: $("#test_Company_Id").val(), test_id: $("#Test").val() }, function (j) {
        var options = '<option value="">' + $('#ViewRes').val() + '</option>';
        for (var i = 0; i < j.length; i++) {
            options += '<option value="' + j[i].optionValue + '">' + j[i].optionDisplay + '</option>';
        }
        $("#test_PreviousTest_Id").html(options);
    });
}

function updateQuestionnairesList() {
    $.post("/Questionnaires/GetQuestionnairesByCompany", { company_id: $("#test_Company_Id").val() }, function (j) {
        var options = '<option value="">' + $('#ViewRes').val() + '</option>';
        for (var i = 0; i < j.length; i++) {
            options += '<option value="' + j[i].optionValue + '">' + j[i].optionDisplay + '</option>';
        }
        $("#QuestionnairesInTest").html(null);
        $("#test_Questionnaire_Id").html(null);
        $("#test_Questionnaire_Id").html(options);
        $("#QuestionnairesInTest").html(options);
    });
}

function updateDemographicsSelectorList() {
    var options = '<option value="">' + $('#ViewRes').val() + '</option>';
    $("#Demographics option:selected").each(function () {
        options += '<option value="' + $(this).val() + '">' + $(this).text() + '</option>';
    });
    $("#DemographicSelector").html(options);
}

function updateDemographicsList() {
    $.post("/Tests/GetDemographicsByCompany", { company_id: $("#test_Company_Id").val() }, function (j) {
        $("#Demographics").empty();
        var options = '<option value="">' + $('#ViewRes').val() + '</option>';
        for (var i = 0; i < j.length; i++) {
            options += '<option value="' + j[i].optionValue + '">' + j[i].optionDisplay + '</option>';
            $("#Demographics").append($('<option></option>').attr('value', j[i].optionValue).text(j[i].optionDisplay));
        }
        $("#Demographics").multiselect("refresh");
        $("#DemographicSelector").html(options);
    });
}

function showDivDisordered() {
    $('#disordered').show();
}

function hideDivDisordered() {
    $('#disordered').hide();
}

function showDivOneQuestionnaire() {
    $('#DivOneQuestionnaire').show();
}

function hideDivOneQuestionnaire() {
    $('#DivOneQuestionnaire').hide();
}

function showDivOneQuestionnaireCheckBox() {
    $('#DivOneQuestionnaireCheckBox').show();
}

function hideDivOneQuestionnaireCheckBox() {
    $('#DivOneQuestionnaireCheckBox').hide();
}

function showDivDemographicsInTest() {
    $('#DivDemographicsInTest').show();
}

function hideDivDemographicsInTest() {
    $('#DivDemographicsInTest').hide();
}

function showDivButtonSeveralQuestionnaires() {
    $('#DivButtonSeveralQuestionnaires').show();
}

function hideDivButtonSeveralQuestionnaires() {
    $('#DivButtonSeveralQuestionnaires').hide();
}

function showDivCategories() {
    if ($("#test_Questionnaire_Id").val()) {
        $.post("/Categories/GetCategoriesByQuestionnaire", { questionnaire_id: $("#test_Questionnaire_Id").val() }, function (j) {
            var categoryHtml = '';
            for (var i = 0; i < j.length; i++) {
                categoryHtml += '<div id="W'+ i + '" class="column span-24 last">';
                categoryHtml += '<div class="column span-24 last"><h4>' + j[i].optionDisplay + '</h4></div>';
                categoryHtml += '<div class="column span-24 last"><input type="text" id="weighing.' + j[i].optionValue + '" name = "weighing.' + j[i].optionValue + '" class = "input-background tiny"/>%</div>';
                categoryHtml += '</div>';
            }
            $("#categories").html(categoryHtml);
        });
        $('#categories').show();
    }
}

function hideDivCategories() {
    var i = 0;
    while ($('#W' + i).length > 0) {
        $('#W' + i).remove();
        i++;
    }
    $('#categories').hide();
}

function loadDivCategories() {
    if ($("#test_Questionnaire_Id").val() && $('#one').is(':checked')) {
        $.post("/Tests/GetWeighingsByTest", { test_id: $("#Test").val() }, function (j) {
            var weighingsHtml = '';
            for (var i = 0; i < j.length; i++) {
                weighingsHtml += '<div id="W' + i + '" class="span-24 last">';
                weighingsHtml += '<div class="span-24 last"><h4>' + j[i].weighingCategory + '</h4></div>';
                weighingsHtml += '<div class="span-24 last"><input type="text" id="weighing.' + j[i].categoryId + '" name = "weighing.' + j[i].categoryId + '" value = "' + j[i].weighingValue + '" class = "required input-background tiny"/>%</div>';
                weighingsHtml += '</div>';
            }
            $("#categories").html(weighingsHtml);
        });
        $('#categories').show();
    }
}