
$(document).ready(function () {
    $('#user_Company_Id').change(function () {
        var company_id  = $(this).val();
        $.post("/Companies/GetTypeById", { id: company_id }, function (j) {
            if (j == "CUSTOMER") {
                var l = updateLocationsList();
                if(l>0)
                    showDivCustomer();
            }
            else
                hideDivCustomer();
        });
    });
    $('#user_Role_Id').change(function () {
        if ($(this).val()) {
            updateCompaniesList();
        }
    });
    $('#user_FirstName').focus();
    $('#user_UserName').focus();
});

    function showDivCustomer() {
        $('#customer').show();
    }

    function hideDivCustomer() {
        $('#customer').hide();
    }

    function updateCompaniesList() {
        
        $.post("/Users/GetCompaniesByRole", { role_id: $("#user_Role_Id").val() }, function (j) {
            var options = '<option value="">' + $('#ViewRes').val() + '</option>';
            for (var i = 0; i < j.length; i++) {
                options += '<option value="' + j[i].optionValue + '">' + j[i].optionDisplay + '</option>';
            }
            $("#user_Company_Id").html(options);
        });
    }

    function updateLocationsList() {
        var count;
        $.post("/Locations/GetLocationsByCompany", { company_id: $("#user_Company_Id").val() }, function (j) {
            count = j.length;
            var options = '<option value="">' + $('#ViewRes').val() + '</option>';
            for (var i = 0; i < j.length; i++) {
                options += '<option value="' + j[i].optionValue + '">' + j[i].optionDisplay + '</option>';
            }
            $("#user_Location_Id").html(options);
        });
        return count;
    }


