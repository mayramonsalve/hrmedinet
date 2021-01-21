
$(document).ready(function () {
    if ($('#agrupacion').is(':checked')) {
        hideDivCategories();
        hideDivQuestionnaires();
        showDivCompanies();
    }
    else {
        hideDivCompanies();
        showDivQuestionnaires();
        showDivCategories();
    }
    $('#agrupacion').change(function () {
        if ($(this).is(':checked')) {
            hideDivCategories();
            hideDivQuestionnaires();
            showDivCompanies();
        }
        else {
            hideDivCompanies();
            showDivQuestionnaires();
            showDivCategories();
        }
    });
    $('#category_Company_Id').change(function () {
        var company_id = $(this).val();
        var options = '<option value="">' + $('#ViewRes').val() + '</option>';
        if (company_id) {
            $.post("/Categories/GetEmptyCategoriesByCompanyId", { company_id: company_id }, function (j) {
                for (var i = 0; i < j.length; i++) {
                    options += '<option value="' + j[i].optionValue + '">' + j[i].optionDisplay + '</option>';
                }
                $("#category_CategoryGroup_Id").html(options);
            });
        }
        else
            $("#category_CategoryGroup_Id").html(options);
    });
    $('#category_Questionnaire_Id').change(function () {
        var questionnaire_id = $(this).val();
        var options = '<option value="">' + $('#ViewRes').val() + '</option>';
        if (questionnaire_id) {
            $.post("/Categories/GetEmptyCategoriesByCompanyByQuestionnaireId", { questionnaire_id: questionnaire_id }, function (j) {
                for (var i = 0; i < j.length; i++) {
                    options += '<option value="' + j[i].optionValue + '">' + j[i].optionDisplay + '</option>';
                }
                $("#category_CategoryGroup_Id").html(options);
            });
        }
        else
            $("#category_CategoryGroup_Id").html(options);
    });
    $('#category_Name').focus();
});

function showDivCategories() {
    $('#DivCategories').show();
}
function hideDivCategories() {
    $('#DivCategories').hide();
}
function showDivQuestionnaires() {
    $('#DivQuestionnaires').show();
}
function hideDivQuestionnaires() {
    $('#DivQuestionnaires').hide();
}
function showDivCompanies() {
    if ($('#Role').val() == "HRAdministrator")
        $('#DivCompanies').show();
}
function hideDivCompanies() {
    if ($('#Role').val() == "HRAdministrator")
        $('#DivCompanies').hide();
}
