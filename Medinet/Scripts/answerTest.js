function InitializeSteps() {
    $('#idSelectorValue').val("0");
    var steps = $('.step').not('#f0');
    $.each(steps, function (index, value) {
        $("#fieldWrapper").append($(this));
    });
    prog = $("#ProgressB").val();
    var first = $("#fieldWrapper .step:first");
    first.css('display', 'inline-block');
    $("#MultiPageAnswers").formwizard("update_steps");
  // $("#MultiPageAnswers").formwizard("next");
}

$(document).ready(function () {
    var prog;
    var questionnaire_id;
    $("#MacAddress").val("MacAddress");
    var currentP = 0;
    $("#MultiPageAnswers").formwizard({
        validationEnabled: true,
        focusFirstInput: true,
        textSubmit: $("#ButtonSubmit").val(),
        textNext: $("#ButtonNext").val(),
        textBack: $("#ButtonBack").val()
    });
    InitializeSteps();
    prog = $("#ProgressB").val();
    $('.classSelector').change(function () {
        var name = $(this).attr("id").replace(/evaluation_/g, "");
        name = name.replace(/_Id/g, "");
        var demographic = $('#Demographic').val();
        if (demographic == name) {
            if ($(this).val()) {
                $('#idSelectorValue').val($(this).val());
                $.ajax({
                    async: false,
                    timeout: 3000,
                    type: "POST",
                    url: "/Evaluations/LoadDivQuestions", //buscar preguntas (cuestionario) correspondiente
                    data: { test_id: $('#test_id').val(), idSelectorValue: $(this).val() }
                }).done(function (htmlText) {
                    if (htmlText) {
                        $('#fieldWrapper .step').not('#f0').remove(); //eliminar todas las paginas menos la de demograficos
                        $('#AllQuestions').html(htmlText); //agregar el div de preguntas que se busco en LoadDivQuestions al div llamado AllQuestions
                        $('#fX').remove();
                        var steps = $('.step').not('#f0'); //para agregar todos los steps de AllQuestions menos el de demograficos q ya esta agregado
                        $.each(steps, function (index, value) {
                            $("#fieldWrapper").append($(this)); //agrego cada step al fieldwrapper
                        });
                        $("#MultiPageAnswers").formwizard("update_steps"); //actualizar fieldwrapper
                        questionnaire_id = $("#QuestionnaireId").val();
                        prog = $("#ProgressB-" + questionnaire_id).val();
                        return false;
                    }
                });
            }
            else {
                InitializeSteps();
            }
        }
    });

    jQuery("#f0").delegate(".FOT", "change", function () {
        //$('.FOT').change(function () {
        var FOT_Id = $(this).parent().parent().attr("id").replace(/Parent_/g, ""); //obtener id del tipo de estructura funcional
        $('.Child_' + FOT_Id).remove();
        if ($(this).val()) {
            //BUSCAR HIJOS DE FO_Id
            $.ajax({
                async: false,
                timeout: 3000,
                type: "POST",
                url: "/Evaluations/GetFOTChildren",
                data: { foParent_id: $(this).val() }
            }).done(function (data) {
                if (data) {
                    $.each(data, function (index, value) {
                        var childId = value.childId;
                        var childName = value.childName;

                        var idSelect = "evaluation_FunctionalOrganizationType_" + childName + "_Id";
                        var classSelect = "required form-short validateDDL FOT required form-short validateDDL FOT ui-wizard-content ui-helper-reset ui-state-default";
                        var html = "<div id='Div_Parent_FOT_" + childId + "' class='span-24 column last verticalPadding3 Child_" + FOT_Id + "'>" +
                                    "<div id='Parent_" + childId + "' class='column last'>" +
                                        "<div class='column'>" +
                                            "<h5>" + childName + ":&nbsp;&nbsp;&nbsp;</h5>" +
                                        "</div>" +
                                        "<div class='column last'>" +
                                            "<select id='" + idSelect + "' name='" + idSelect + "' class='" + classSelect + "'>" +
                                                "<option value=''>" + $('#ViewRes').val() + "</option>";
                        $.each(value.childDictionary, function (indexC, valueC) {
                            html = html + '<option value="' + indexC + '">' + valueC + '</option>';
                        });
                        html = html + "</select>" +
                                        "</div>" +
                                    "</div>" +
                                "</div>";
                        $('#Div_Parent_FOT_' + FOT_Id).append(html);
                    });
                }
            });
        }
        //        else {
        //            //ELIMINAR HIJOS DE FOT_Id
        //            $('.Child_' + FOT_Id).remove();
        //        }
    });

    $("#MultiPageAnswers").bind("step_shown", function (event, data) {
        var thisStep = "f1-Q" + questionnaire_id;
        if (data.currentStep == thisStep) {
            $("#DivInstructions").html($("#Instructions-" + questionnaire_id).val());
        }
        if (!data.isBackNavigation) {
            $("#progressbar").progressbar("option", "value", $("#progressbar").progressbar
("option", "value") + parseInt(prog));
        }
        else {
            $("#progressbar").progressbar("option", "value", $("#progressbar").progressbar
("option", "value") - parseInt(prog));
        }
        currentP = $("#progressbar").progressbar("option", "value");
        $("#amount").text(currentP + "%");
    });

    if ($.browser.msie && $.browser.version <= 8) {
        $('#MultiPageAnswers').submit(function () {
            if (Validate()) {
                //$(this).submit();
                return true;
            }
            else {
                alert("Debe seleccionar una opción por demográfico y una respuesta por pregunta.");
                //jQuery('#Dialog').open();
                return false;
            }
        });
    }
    else {
        $('#MultiPageAnswers').submit(function () {
            $("body").addClass("loading");
            return true;
            //            $(this).submit();
        });
    }

    $('#Dialog').dialog({
        autoOpen: false,
        show: "highlight",
        hide: "highlight",
        modal: true,
        buttons: {
            'Ok': function () {
                $(this).dialog('close');
            }
        }
    });

    $(".qTipo").change(function () {
        var questionText = $("input:text[id=q[10033190]]");

        if ($(this).val() == "76") {
            questionText.css('color', 'white');
            questionText.val('...');
        }
        else {
            questionText.val('');
            questionText.css('color', 'black');
        }
    });

    $("#progressbar").progressbar(
    {
        value: 0
    });

    function Validate() {
        var radiosTot = parseInt($("#QuestionsCount").val()) + 1;
        var valid = false;
        var Selects = $("select.validateDDL").find("option[value='']:selected").length;
        var Radios = $("input.validateRadio:checked").length;
        if (Radios == radiosTot && Selects == 0) {
            valid = true;
        }
        return valid;
    }

    //    $(".FOTypes").change(function () {
    //        if ($(this).val()) {
    //            loadChilDDL();
    //        }
    //        else {
    //            removeChildDDL();
    //        }
    //    });
});