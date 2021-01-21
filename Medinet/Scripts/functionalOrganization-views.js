
$(document).ready(function () {
    $('#functionalOrganization_Name').focus();
    $('#Parent_Type_Id').change(function () {
        updateTypesChildrenList($(this).val());
        if ($(this).val()) {
            updateParentsFOList($(this).val());
        }
        else {
            $("#functionalOrganization_FOParent_Id").html(null);
        }
    });
});

function updateParentsFOList(type_id) {
    $.post("/FunctionalOrganizations/GetFunctionalOrganizationsByType", { type_id: type_id }, function (j) {
        var options = '<option value="">' + $('#ViewRes').val() + '</option>';
        for (var i = 0; i < j.length; i++) {
            options += '<option value="' + j[i].optionValue + '">' + j[i].optionDisplay + '</option>';
        }
        $("#functionalOrganization_FOParent_Id").html(options);
    });
}

function updateTypesChildrenList(type_id) {
    $.post("/FunctionalOrganizationTypes/GetFunctionalOrganizationTypesChildrenByType", { type_id: type_id }, function (j) {
        var options = '<option value="">' + $('#ViewRes').val() + '</option>';
        for (var i = 0; i < j.length; i++) {
            options += '<option value="' + j[i].optionValue + '">' + j[i].optionDisplay + '</option>';
        }
        $("#functionalOrganization_Type_Id").html(options);
    });
}