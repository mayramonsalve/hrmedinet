var test_id;
var Result2Time = 0;
var Result3Time = 0;
var Result4Time = 0;
var Result5Time = 0;
var Result6Time = 0;
var Result7Time = 0;

$(document).ready(function () {
    test_id = $("#test_id").val();
    $(function () {
        $("#myTabs").tabs();
    });

    //    $(function () {
    //        $(".myaccordion").accordion({
    //            collapsible: true
    //        });
    //    });

    InitializeDataTable('#tabResult1');

    $(".all").bind("click", handle_tab_click);


});

function InitializeDataTable4(tab) {
    var tabla = tab + ' .tabla'
    $(tabla).dataTable({
        "bPaginate": false,
        "bFilter": false,
        "bInfo": false,
        "bJQueryUI": true,
        "bRetrieve": true,
        "bSort": true,
        "aoColumns": [
              { "bSortable": true, "asSorting": ["asc", "desc"], "sType": "string"  },
              { "bSortable": true, "asSorting": ["asc", "desc"], "sType": "numeric-comma" },
              { "bSortable": true, "asSorting": ["asc", "desc"], "sType": "numeric-comma" },
              { "bSortable": true, "asSorting": ["asc", "desc"], "sType": "numeric-comma" }
              ]
    });
}

function InitializeDataTable(tab) {
    var tabla = tab + ' .tabla'
    $(tabla).dataTable({
        "bPaginate": false,
        "bFilter": false,
        "bInfo": false,
        "bJQueryUI": true,
        "bRetrieve": true,
        "aoColumnDefs": [
                          { "bSortable": false, "aTargets": [0] }
                        ]
    });
}

function InitializeAccordion() {
    $(".myaccordion").accordion({
        collapsible: true,
        heightStyle: "content",
        autoHeight: false
    });
}

function handle_tab_click(event) {
    var href = $(event.target).attr("href");
    var vec_result = $(event.target).attr("href").split('-');
    var questionnaire_id = vec_result[1];
    var result = $(event.target).attr("href").replace("#tab", "");
    result = result.split('-')[0];
    var result_id = $(event.target).attr("href").replace("#tabResult", "");
    result_id = result_id.split('-')[0];

    if (questionnaire_id) {
        var aux = $("#R" + result_id + "-" + questionnaire_id).length;
        if (aux == 0) {
            $.post("/ChartReports/LoadResult", { result: result_id, test_id: test_id, questionnaire_id: questionnaire_id }, function (htmlText) {
                $(href).html(htmlText);
                InitializeDataTable(href);
            });
        }
    }

    if (result == "Result2" && Result2Time == 0) {
        $.post("/ChartReports/LoadResult", { result: result_id, test_id: test_id, questionnaire_id: questionnaire_id }, function (htmlText) {
            $('#tabResult2').html(htmlText);
            Result2Time++;
            InitializeDataTable('#tabResult2');
        });
    }

    if (result == "Result3" && Result3Time == 0) {
        $.post("/ChartReports/LoadResult", { result: result_id, test_id: test_id, questionnaire_id: questionnaire_id }, function (htmlText) {
            $('#tabResult3').html(htmlText);
            Result3Time++;
            InitializeAccordion();
            InitializeDataTable('#tabResult3');
        });
    }

    if (result == "Result4" && Result4Time == 0) {
        $.post("/ChartReports/LoadResult", { result: result_id, test_id: test_id, questionnaire_id: questionnaire_id }, function (htmlText) {
            $('#tabResult4').html(htmlText);
            Result4Time++;
            InitializeAccordion();
            InitializeDataTable4('#tabResult4');
        });
    }

    if (result == "Result5" && Result5Time == 0) {
        $.post("/ChartReports/LoadResult", { result: result_id, test_id: test_id, questionnaire_id: questionnaire_id }, function (htmlText) {
            $('#tabResult5').html(htmlText);
            Result5Time++;
            InitializeDataTable('#tabResult5');
        });
    }

    if (result == "Result6" && Result6Time == 0) {
        $.post("/ChartReports/LoadResult", { result: result_id, test_id: test_id, questionnaire_id: questionnaire_id }, function (htmlText) {
            $('#tabResult6').html(htmlText);
            Result6Time++;
            InitializeAccordion();
            InitializeDataTable('#tabResult6');
        });
    }

    if (result == "Result7" && Result7Time == 0) {
        $.post("/ChartReports/LoadResult", { result: result_id, test_id: test_id, questionnaire_id: questionnaire_id }, function (htmlText) {
            $('#tabResult7').html(htmlText);
            Result7Time++;
            InitializeDataTable('#tabResult7');
        });
    }
}
