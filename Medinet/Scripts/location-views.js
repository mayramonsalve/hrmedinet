
        $(document).ready(function () {
            $('#location_Name').focus();

            $('#Country_Id').change(function () {
                if ($(this).val()) {
                    updateStatesList();
                }
                else {
                    $("#location_State_Id").html(null);
                }
            });

        });

        function updateStatesList() {
            $.post("/States/GetStatesByCountry", { country_id: $("#Country_Id").val() }, function (j) {
                var options = '<option value="">' + $('#ViewRes').val() + '</option>';
                for (var i = 0; i < j.length; i++) {
                    options += '<option value="' + j[i].optionValue + '">' + j[i].optionDisplay + '</option>';
                }
                $("#location_State_Id").html(options);
            });
        }