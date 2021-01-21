
$(document).ready(function () {
    $('#question_Text').focus();

    $('#Questionnaires').change(function () {
        if ($(this).val()) {
            updateCategoriesList();
            var questionnaire_id = $(this).val();
            if ($("#question_SortOrder").val() == 0) {
                $.post("/Questions/GetNextSortOrderByQuestionnaire", { id: questionnaire_id }, function (j) {
                    $("#question_SortOrder").val(j);
                });
            }
        }
        else {
            $("#question_Category_Id").html(null);
        }
    });

});

    function updateCategoriesList() {
        $.post("/Categories/GetCategoriesByQuestionnaire", { questionnaire_id: $("#Questionnaires").val() }, function (j) {
            var options = '<option value="">' + $('#ViewRes').val() + '</option>';
            for (var i = 0; i < j.length; i++) {
                options += '<option value="' + j[i].optionValue + '">' + j[i].optionDisplay + '</option>';
            }
            $("#question_Category_Id").html(options);
        });
    }