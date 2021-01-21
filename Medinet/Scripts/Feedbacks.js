$(document).ready(function () {
    var dis;
    var spl;
    var wid;
    var optionStars20;
    var show = $('#ShowDialog').val();
    if (show == "true" || show == "True")
        show = true;
    else
        show = false;
    $.post("/Feedbacks/GetOptionsList", {}, function (data) {
        optionStars20 = data;
    });
    if ($("#Send").val() == "False")
        dis = true;
    else
        dis = false;
    if ($("#Split").val()) {
        spl = $("#Split").val();
        wid = 32;
    }
    else {
        spl = 0;
        wid = 16;
    }

    $(".rating").stars({
        inputType: "select",
        //        captionEl: $("#hover_ocv2"),
        disabled: dis,
        split: parseInt(spl),
        starWidth: parseInt(wid),
        callback: function (ui, type, value, event) {
            //            $("#" + this.name).val(value);
            $("#Add").val("True");
        },
        cancelClass: 'ui-crystal-cancel',
        starClass: 'ui-crystal-star',
        starOnClass: 'ui-crystal-star-on',
        starHoverClass: 'ui-crystal-star-hover',
        cancelHoverClass: 'ui-crystal-cancel-hover',
        cancelShow: false
    });

    $('#Types').change(function () {
        Updates($(this).val(), $('#Types option:selected').text());
    });

    function Show() {
        $.post("/Feedbacks/UpdateShow", { feedback_id: $('#FeedbackId').val() }, function (data) {
            $('#Show').text(data);
        });
    }

    function EmptyDiv(DivId) {
        $("#" + DivId).empty();
    }
    function FillDiv(DivId, Html) {
        $("#" + DivId).append(Html);
    }

    function Updates(type_id, type_name) {
        EmptyDiv("AddComments");
        EmptyDiv("Comments");
        EmptyDiv("StarsFeedbacks");
        if (type_id) {
            $("#textFeedbacks").text($("#TextAverage1").val() + type_name + $("#TextAverage2").val());
            $.post("/Feedbacks/UpdateStars", { type_id: type_id }, function (data) {
                if (data.length > 0) {
                    var html;
                    for (var i = 0; i < data.length; i++) { // $("#").addClass("");
                        html = "";
                        html += "<div id='" + data[i].featureId + "classRating'>";
                        html += "<div id='" + data[i].featureId + "span24-1'><h4>" + data[i].featureName + ": <span id='" + data[i].featureId + "Span'> " + data[i].featureAverage + "/5" + $("#OutOf").val() + data[i].featureCount + $("#Votes").val() + "</span></h4></div>";
                        html += "<div id='" + data[i].featureId + "span24-2'>";
                        html += "<form id='" + data[i].featureId + "Form' method='post' action='#'>";
                        html += "<div id='" + data[i].featureId + "'>";
                        html += "<select id='" + data[i].featureId + "Select'>";
                        var options = "";
                        var selected;
                        for (var j = 0; j < optionStars20.length; j++) {
                            if (data[i].featureScore == optionStars20[j].optionValue)
                                selected = " selected = 'selected' ";
                            else
                                selected = " ";
                            options += "<option" + selected + "value='" + optionStars20[j].optionValue + "'>" + optionStars20[j].optionDisplay + "</option>";
                        }
                        html += options;
                        html += "</select></div></form></div><br/>";
                        FillDiv("StarsFeedbacks", html);
                        $("#" + data[i].featureId + "classRating").addClass("rating span-23 append-1 column");
                        $("#" + data[i].featureId + "span24-1").addClass("span-24 last column");
                        $("#" + data[i].featureId + "Span").addClass("rating-cap");
                        $("#" + data[i].featureId + "span24-2").addClass("span-24 last column");
                        $("#" + data[i].featureId + "Select").addClass("input-background short");
                        $("#" + data[i].featureId + "classRating").stars({
                            inputType: "select",
                            //        captionEl: $("#hover_ocv2"),
                            disabled: dis,
                            split: parseInt(spl),
                            starWidth: parseInt(wid),
                            callback: function (ui, type, value, event) {
                                //            $("#" + this.name).val(value);
                                $("#Add").val("True");
                            },
                            cancelClass: 'ui-crystal-cancel',
                            starClass: 'ui-crystal-star',
                            starOnClass: 'ui-crystal-star-on',
                            starHoverClass: 'ui-crystal-star-hover',
                            cancelHoverClass: 'ui-crystal-cancel-hover',
                            cancelShow: false
                        });
                    }
//                    $(".rating").stars();
                }
                else {
                    FillDiv("StarsFeedbacks", $("#NoFeedbacks").val());
                }
            });

            $.post("/Feedbacks/UpdateDivs", { stringComment: "AddComments", type_id: type_id }, function (data) {
                if (data.length > 0) {
                    var html = "";
                    html = html + "<ul id='addC'>";
                    //                    $('#AddComments').css('height', '200px');
                    for (var i = 0; i < data.length; i++) {
                        html = html + "<li><p>" + data[i] + "</p></li>";
                    }
                    html = html + "</ul>"
                    FillDiv("AddComments", html);
                }
                else {
                    FillDiv("AddComments", $("#NoComments").val());
                }
            });

            $.post("/Feedbacks/UpdateDivs", { stringComment: "Comments", type_id: type_id }, function (data) {
                if (data.length > 0) {
                    var html = "";
                    html = html + "<ul id='C'>";
                    for (var i = 0; i < data.length; i++) {
                        html = html + "<li><p>" + data[i] + "</p></li>";
                    }
                    html = html + "</ul>"
                    FillDiv("Comments", html);
                }
                else {
                    FillDiv("Comments", $("#NoComments").val());
                }
            });
            $("#addC").addClass("listStyle");
            $("#C").addClass("listStyle");
        }
        else {
            $("#textFeedbacks").text($("#SelectType").val());
            FillDiv("AddComments", $("#SelectType").val());
            FillDiv("Comments", $("#SelectType").val());
        }
    }

    var lang = { GoHome: $("#GoHome").val() };
    var buttonsD = {};
    buttonsD[lang.GoHome] = function () {
        $(this).dialog('close');
        window.location.href = '/Home/Index';
    };

    $('#Dialog').dialog({
        autoOpen: false,
        show: "highlight",
        hide: "highlight",
        modal: true,
        buttons: buttonsD
    });

    if (show) {
        $('#Dialog').dialog("open");
    }

});
