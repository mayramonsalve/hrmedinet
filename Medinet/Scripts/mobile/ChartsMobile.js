google.charts.load('current', { 'packages': ['corechart'] });

// Variable used in multiple reports
var screenWidth = $(window).width();
var test_id;
var compare_id;
var chartType;
var chartModel;
var demographic_global="";
var RegionTime = 0;
var AllTestsTime = 0;
var ClimateTime = 0;
var DepartmentTime = 0;
var AgeTime = 0;
var CountryTime = 0;
var InstructionTime = 0;
var LocationTime = 0;
var PositionLevelsTime = 0;
var PositionTime = 0;
var SeniorityTime = 0;
var GenderTime = 0;
var PerformanceTime = 0;
var CategoryTime = 0;
var FOTime ;
var elements_count = 0;
var textAnswersVectorCallback ;
var demographicTextAnswersCallback;
var initialize;
var demographicsHtml;
var company_id;

//Options for graphics
var colors = ["#00a0e3", "#ffce00", "#00b386", "#ff044d", "#664147", "#084c61", "#3c1642",
    "#1a181b", "#b4656f", "#f9b9f2", "#102542", "#2e0219", "#f8333c", "#a62639",
    "#065143", "#d9dbf1", "#af2bbf", "#841c26", "#aaaaaa", "#d30c7b", "#01295f",
    "#fd151b", "#FF69B4", "#00BFFF"];
var colors_gender = ["#FF69B4", "#00BFFF"];
var options_main = {
    title: "",
    titleTextStyle: { fontSize: 13 },
    legend: 'none',
    is3D: true,
    width: (screenWidth * 0.9),
    height: 300,
    top: 0,
    left: 0,
    chartArea: { 'width': '85%', 'height': '80%', 'top': '15%', 'left': '10%' },
    bar: { groupWidth: 30 },
    hAxis: { textPosition: 'none' },
    colors: colors,
    annotations: {
        alwaysOutside: false,
        textStyle: {
            fontSize: 8,
            bold: true,
            color: '#000',
            auraColor: 'none'
        }
    },
    animation: {
        duration: 1000,
        easing: 'out',
        startup: true
    }
};

//Variables used in ranking report
var InternalTime = 0;
var GeneralCountryTime = 0;
var CustomerTime = 0;
var questionnaire;
var company = null;

//Variables used in stadistics reports
var Result2Time = 0;
var Result3Time = 0;
var Result4Time = 0;
var Result5Time = 0;
var Result6Time = 0;
var Result7Time = 0;

function initializeVariables() {
    RegionTime = 0;
    AllTestsTime = 0;
    ClimateTime = 0;
    DepartmentTime = 0;
    AgeTime = 0;
    CountryTime = 0;
    InstructionTime = 0;
    LocationTime = 0;
    PositionLevelsTime = 0;
    PositionTime = 0;
    SeniorityTime = 0;
    GenderTime = 0;
    PerformanceTime = 0;
    CategoryTime = 0;
    elements_count = 0;

    //iables used in ranking report
    InternalTime = 0;
    GeneralCountryTime = 0;
    CustomerTime = 0;

    //iables used in stadistics reports
    Result2Time = 0;
    Result3Time = 0;
    Result4Time = 0;
    Result5Time = 0;
    Result6Time = 0;
    Result7Time = 0;
}


//Function called when orientation of the device its change
$(window).bind("orientationchange", function (evt) {
    if (isTablet() && evt.orientation == "landscape") {
        $(".principal").removeClass("ui-grid-solo");
        $(".principal").addClass("ui-grid-a");
        $(".chartDiv").removeClass("portrait");
        $(".chartDiv").addClass("landscape  ui-block-a");
        $(".generalDiv").removeClass("ui-block-a");
        $(".generalDiv").addClass("ui-block-b");
    }
    else {
        $(".principal").removeClass("ui-grid-a");
        $(".principal").addClass("ui-grid-solo");
        $(".chartDiv").removeClass("landscape");
        $(".chartDiv").addClass("portrait ui-block-a");
        $(".generalDiv").removeClass("ui-block-b");
        $(".generalDiv").addClass("ui-block-a");
    }
    handleDDLsWidth();
});

function handleDDLsWidth() {
    var tab = "#tab" + (demographic_global == "" ? "General" : demographic_global);
    if ($(tab + " .GroupByCategoriesDDL").length) {
        var width = screenWidth * 0.92;
        $(tab + ' .ui-field-contain .ui-select .ui-btn').css("max-width", width);
    }
}

//Function called on init of all reports to determine the orientation of the devices, and depending on the outcome, it will be necessary or not change the class
// Las clases de los div se cambian para que dependiendo si es tablet o no, se mostraran los elementos de una forma u otra.
function changeStyle() {
    if (isTablet() && isLandscape()) {
        $(".principal").removeClass("ui-grid-solo");
        $(".principal").addClass("ui-grid-a");
        $(".chartDiv").removeClass("portrait");
        $(".chartDiv").addClass("landscape  ui-block-a");
        $(".generalDiv").removeClass("ui-block-a");
        $(".generalDiv").addClass("ui-block-b");
    }
    else {
        $(".principal").removeClass("ui-grid-a");
        $(".principal").addClass("ui-grid-solo");
        $(".chartDiv").removeClass("landscape");
        $(".chartDiv").addClass("portrait ui-block-a");
        $(".generalDiv").removeClass("ui-block-b");
        $(".generalDiv").addClass("ui-block-a");
    }
    handleDDLsWidth();
}

function isTablet() {
    if (screenWidth > 500) {
        return true;
    }
    return false;
}

function isLandscape() {
    if (screenWidth > $(window).height()) {
        return true;
    }
    return false;
}

$(document).on('pageinit', '#mainChartStadistics', function () {
    test_id = $("#test_id").val();
    $("#tabResult2").hide();
    $("#tabResult3").hide();
    $("#tabResult4").hide();
    $("#tabResult5").hide();
    $("#tabResult6").hide();
    changeStyle();
});

$(document).on('pageinit', '#mainChartanalytical', function () {
    changeStyle();
    google.setOnLoadCallback(drawCharts);
});

$(document).on('pageinit', '#mainChart', function () {
    if (initialize != 0) {
        test_id = $("#test_id").val();
        compare_id = $("#compare_id").val(); if (compare_id == 0) compare_id = null;
        chartType = $("#chartType").val();
        chartModel = $("#chartModel").val();
        FOTime = createFOTime();
        textAnswersVectorCallback = new Array();
        demographicTextAnswersCallback;
        var initialTab = (chartType == "Comparative") ? "Population" : "General";
        demographic_global = initialTab;
        google.setOnLoadCallback(function () { GetChart(chartType, initialTab, test_id, null, null, null, null, null) });
        initialize++;
    }
    changeStyle();
    hideAll();
    $("#tabGeneral").show();
    $("#tabPopulation").show();
    $("#tabCategoryGeneral").show();
    handleDDLsWidth();
});

function createFOTime() {
    var FOT = new Array();
    if ($("#FOids").val() != "") {
        var FOids = $("#FOids").val().split("-");
        FOids.pop();
        for (var i = 0; i < FOids.length; i++) {
            FOT[i] = new Array(FOids[i], 0);
        }
    }
    return FOT;
}

function InitializeAccordion() {
}

function showLoading() {
    $.mobile.loading('show');
}

function hideLoading() {
    $('.ui-dialog').dialog('close');
    changeStyle();
    $('.table1').tablesorter();
}

$(document).ajaxStart(function () {
    $.mobile.loading('show');
});

$(document).ajaxComplete(function (event, request, settings) {
    $.mobile.loading('hide');
    $('.ui-dialog').dialog('close');
    changeStyle();
    $('.table1').tablesorter();
});

$(document).on('click', '.all', handle_tab_click_op);

$(document).on('click', '.allResult', handle_tab_click_result);

function handle_tab_click_result(event) {
    hideAllResult();
    var href = $(event.target).attr("href");
    var vec_result = $(event.target).attr("href").split('-');
    var questionnaire_id = vec_result[1];
    var result = $(event.target).attr("href").replace("#tab", "");
    result = result.split('-')[0];
    var result_id = $(event.target).attr("href").replace("#tabResult", "");
    result_id = result_id.split('-')[0];
    show(result);
    if (questionnaire_id) {
            var aux = $("#R" + result_id + "-" + questionnaire_id).length;
            if (aux == 0) {
                $.post("/ChartReports/LoadResult", { result: result_id, test_id: test_id, questionnaire_id: questionnaire_id }, function (htmlText) {
                    $(href).append(htmlText).trigger("create");
                    $(".table1").tablesorter();
                });
            }
            hideAllResultQ(result);
            show(result + "-" + questionnaire_id);
    }

    if (result == "Result2" && Result2Time == 0) {
        $.post("/ChartReports/LoadResult", { result: result_id, test_id: test_id, questionnaire_id: questionnaire_id }, function (htmlText) {
            $('#tabResult2').append(htmlText).trigger("create");
            Result2Time++;
            $(".table1").tablesorter();
        });
    }

    else if (result == "Result3" && Result3Time == 0) {
        $.post("/ChartReports/LoadResult", { result: result_id, test_id: test_id, questionnaire_id: questionnaire_id }, function (htmlText) {
            $('#tabResult3').append(htmlText).trigger("create");
            Result3Time++;
            InitializeAccordion();
            $(".table1").tablesorter();
        });
    }

    else if (result == "Result4" && Result4Time == 0) {
        $.post("/ChartReports/LoadResult", { result: result_id, test_id: test_id, questionnaire_id: questionnaire_id }, function (htmlText) {
            $('#tabResult4').append(htmlText).trigger("create");
            Result4Time++;
            InitializeAccordion();
            $(".table1").tablesorter();
        });
    }

    else if (result == "Result5" && Result5Time == 0) {
        $.post("/ChartReports/LoadResult", { result: result_id, test_id: test_id, questionnaire_id: questionnaire_id }, function (htmlText) {
            $('#tabResult5').append(htmlText).trigger("create");
            Result5Time++;
            $(".table1").tablesorter();
        });
    }

    else if (result == "Result6" && Result6Time == 0) {
        $.post("/ChartReports/LoadResult", { result: result_id, test_id: test_id, questionnaire_id: questionnaire_id }, function (htmlText) {
            $('#tabResult6').append(htmlText).trigger("create");
            Result6Time++;
            $(".table1").tablesorter();
        });
    }

    else if (result == "Result7" && Result7Time == 0) {
        $.post("/ChartReports/LoadResult", { result: result_id, test_id: test_id, questionnaire_id: questionnaire_id }, function (htmlText) {
            $('#tabResult7').append(htmlText).trigger("create");
            Result7Time++;
            $(".table1").tablesorter();
        });
    }

    else {
        hideLoading();
    }
}

function handle_tab_click_op(event){
    event.preventDefault();
    handle_tab_click(event);
}

function hideAll(){
    $("#tabGeneral").hide();
    $("#tabCountry").hide();
    $("#tabRegion").hide();
    $("#tabAgeRange").hide();
    $("#tabInstructionLevel").hide();
    $("#tabLocation").hide();
    $("#tabPositionLevel").hide();
    $("#tabSeniority").hide();
    $("#tabGender").hide();
    $("#tabPerformance").hide();
    $("#tabClimate").hide();
    $("#tabPopulation").hide();

    $("#tabAllTests").hide();

    $("#tabCategoryGeneral").hide();
    $("#tabLocationGeneral").hide();
    $("#tabFunctionalOrganizationTypeGeneral").hide();

    var pos;
    for (pos = 0; pos < FOTime.length; pos++) {
        $('#tabFO-' + FOTime[pos]).hide();
        $('#tabCategory' + FOTime[pos]).hide();
    }
}

function hideAllResultQ(result) {
    $("#tab" + result + "-0").hide();
    $.post("/ChartReports/LoadQuestionnaires", { test_id: test_id }, function (questionnaires) {
        for (k = 0; k < questionnaires.length; k++) {
            $("#tab" + result + "-" + questionnaires[k]).hide();
        }
    });
}

//Hide div, used in stadistics reports 
function hideAllResult() {
    $("#tabResult1").hide();
    $("#tabResult2").hide();
    $("#tabResult3").hide();
    $("#tabResult4").hide();
    $("#tabResult5").hide();
    $("#tabResult6").hide();
}

function show(demographic){
    $("#tab"+demographic).show();
}

function handle_tab_click(event) {
    hideAll();
    var demographic = $(event.target).attr("href").replace("#tab", "");
    demographic_global = demographic;
    var selValId = "";
    show(demographic);
    if (chartType == "Satisfaction") {
        selValId = demographic.replace(/[A-Za-z]+/g, "");
        demographic = demographic.replace(/[0-9]+/g, "");
    }
    elements_count = $('#' + demographic + 'ElementsCount').val();

    if (demographic == "Region" && RegionTime == 0) {
        RegionTime++;
        $.post("/ChartReports/LoadTab", { demographic: "Region", type: chartType, test_id: test_id, FO_id: null, compare_id: compare_id }, function (htmlText) {
            $('#tabRegion').append(htmlText).trigger("create");
            google.setOnLoadCallback(function () { GetChart(chartType, demographic, test_id, null, null, null, null, null) });
        });
    }
    else if (demographic == "Country" && CountryTime == 0) {
        CountryTime++;
        $.post("/ChartReports/LoadTab", { demographic: "Country", type: chartType, test_id: test_id, FO_id: null, compare_id: compare_id }, function (htmlText) {
            $('#tabCountry').append(htmlText).trigger("create");
            google.setOnLoadCallback(function () { GetChart(chartType, demographic, test_id, null, null, null, null, null) });
        });
    }
    else if (demographic == "AgeRange" && AgeTime == 0) {
        AgeTime++;
        $.post("/ChartReports/LoadTab", { demographic: "AgeRange", type: chartType, test_id: test_id, FO_id: null, compare_id: compare_id }, function (htmlText) {
            $('#tabAgeRange').append(htmlText).trigger("create");
            google.setOnLoadCallback(function () { GetChart(chartType, demographic, test_id, null, null, null, null, null) });
        });
    }

    else if (demographic == "InstructionLevel" && InstructionTime == 0) {
        InstructionTime++;
        $.post("/ChartReports/LoadTab", { demographic: "InstructionLevel", type: chartType, test_id: test_id, FO_id: null, compare_id: compare_id }, function (htmlText) {
            $('#tabInstructionLevel').append(htmlText).trigger("create");
            google.setOnLoadCallback(function () { GetChart(chartType, demographic, test_id, null, null, null, null, null) });
        });
    }
    else if (demographic == "Location" && LocationTime == 0) {
        LocationTime++;
        $.post("/ChartReports/LoadTab", { demographic: "Location", type: chartType, test_id: test_id, FO_id: null, compare_id: compare_id }, function (htmlText) {
            $('#tabLocation').append(htmlText).trigger("create");
            google.setOnLoadCallback(function () { GetChart(chartType, demographic, test_id, null, null, null, null, null) });
        });
    }
    else if (demographic == "PositionLevel" && PositionLevelsTime == 0) {
        PositionLevelsTime++;
        $.post("/ChartReports/LoadTab", { demographic: "PositionLevel", type: chartType, test_id: test_id, FO_id: null, compare_id: compare_id }, function (htmlText) {
            $('#tabPositionLevel').append(htmlText).trigger("create");
            google.setOnLoadCallback(function () { GetChart(chartType, demographic, test_id, null, null, null, null, null) });
        });
    }
    else if (demographic == "Seniority" && SeniorityTime == 0) {
        SeniorityTime++;
        $.post("/ChartReports/LoadTab", { demographic: "Seniority", type: chartType, test_id: test_id, FO_id: null, compare_id: compare_id }, function (htmlText) {
            $('#tabSeniority').append(htmlText).trigger("create");
            google.setOnLoadCallback(function () { GetChart(chartType, demographic, test_id, null, null, null, null, null) });
        });
    }

    else if (demographic == "Gender" && GenderTime == 0) {
        GenderTime++;
        $.post("/ChartReports/LoadTab", { demographic: "Gender", type: chartType, test_id: test_id, FO_id: null, compare_id: compare_id }, function (htmlText) {
            $('#tabGender').append(htmlText).trigger("create");
            google.setOnLoadCallback(function () { GetChart(chartType, demographic, test_id, null, null, null, null, null) });
        });
    }

    else if (demographic == "Performance" && PerformanceTime == 0) {
        PerformanceTime++;
        $.post("/ChartReports/LoadTab", { demographic: "Performance", type: chartType, test_id: test_id, FO_id: null, compare_id: compare_id }, function (htmlText) {
            $('#tabPerformance').append(htmlText).trigger("create");
            google.setOnLoadCallback(function () { GetChart(chartType, demographic, test_id, null, null, null, null, null) });

        });
    }

    else if (demographic == "Category" && CategoryTime == 0 && chartType != "Satisfaction") {
        CategoryTime++;
        $.post("/ChartReports/LoadTab", { demographic: "Category", type: chartType, test_id: test_id, FO_id: null, compare_id: compare_id }, function (htmlText) {
            $('#tabCategory').append(htmlText).trigger("create");
            google.setOnLoadCallback(function () { GetChart(chartType, demographic, test_id, null, null, null, null, null) });
        });
    }

    else if (demographic == "AllTests" && AllTestsTime == 0) {
        AllTestsTime++;
        $.post("/ChartReports/LoadTab", { demographic: "AllTests", type: chartType, test_id: test_id, FO_id: null, compare_id: null }, function (htmlText) {
            $('#tabAllTests').append(htmlText).trigger("create");
            google.setOnLoadCallback(function () { GetChart(chartType, demographic, test_id, null, null, null, null, null) });
        });
    }

    else if (demographic == "Climate" && ClimateTime == 0) {
        ClimateTime++;
        $.post("/ChartReports/LoadTab", { demographic: "Climate", type: chartType, test_id: test_id, FO_id: null, compare_id: null }, function (htmlText) {
            $('#tabClimate').append(htmlText).trigger("create");
            google.setOnLoadCallback(function () { GetChart(chartType, demographic, test_id, null, null, null, null, null) });
        });
    }

    else if (chartType == "Satisfaction") {
        var div = $('#tab' + demographic + selValId + "1").html();
        if (div == "") {
            var auxDemographic = demographic.replace("General", "");
            $.post("/ChartReports/LoadTab", { demographic: auxDemographic, type: chartType, test_id: test_id, FO_id: selValId, compare_id: null }, function (htmlText) {
                $('#tab' + demographic + selValId + "1").append(htmlText).trigger("create");
                google.setOnLoadCallback(function () { GetChart(chartType, demographic, test_id, null, null, null, null, null) });
            });
        } else {
            hideLoading();
        }
    }

    else if (demographic.charAt(2) == '-') {
        var pos;
        var FO_id = $(event.target).attr("href").replace("#tabFO-", "");
        for (pos = 0; pos < FOTime.length; pos++) {
            if (FOTime[pos][0] == FO_id) {
                if (FOTime[pos][1] == 0) {
                    $.post("/ChartReports/LoadTab", { demographic: "FunctionalOrganizationType", type: chartType, test_id: test_id, FO_id: FO_id, compare_id: compare_id }, function (htmlText) {
                        $('#tabFO-' + FO_id).append(htmlText).trigger("create");
                        google.setOnLoadCallback(function () { GetChart(chartType, "FunctionalOrganizationType", test_id, FO_id, null, null, null, null) });
                    });
                    FOTime[pos][1]++;
                    elements_count = $('#' + demographic + 'ElementsCount' + FO_id).val();
                }
                break;
            }
        }
    }

    else {
        hideLoading();
    }
}
   
//Function called when pvalue its change in univariate graphics
function PValue(dem) {
    var demographic = dem.replace(/PValue/g, "");
    demographic = demographic.replace(/[0-9]+/g, "");
    var FO_id = "";
    if (demographic == "FunctionalOrganizationType") {
        FO_id = dem.replace(/[A-Za-z]+/g, "");
        elements_count = $('#' + demographic + 'ElementsCount' + FO_id).val();
    }
    else
        elements_count = $('#' + demographic + 'ElementsCount').val();
    var id_compare = $('#' + demographic + 'CompareDDL' + FO_id).val();
    var pVal = $('input[name=' + dem + ']:checked').val();
    var id_question = $('#' + demographic + 'GroupByQuestionsDDL').val();
    var id_category = $('#' + demographic + 'GroupByCategoriesDDL').val();
    var id_questionnaire = $('#' + demographic + 'GroupByQuestionnairesDDL').val();
    var MyCondition = "";

    if (elements_count <= 7) {
        MyCondition = false;
    } else {
        MyCondition = true;
    }
    UpdateTables(demographic, id_questionnaire, id_category, id_question, MyCondition, pVal, FO_id, id_compare);
}

//Function called when user update graphics detail
$(document).on('click', '.button', function (event) {
    var id = $(this).attr("id");
    var demographic = id.replace(/Button/g, "");
    demographic = demographic.replace(/[0-9]+/g, "");
    var FO_id = "";
    if (demographic == "FunctionalOrganizationType")
        FO_id = id.replace(/[A-Za-z]+/g, "");
    var id_compare = $("#" + demographic + "CompareDDL" + FO_id).val();
    var id_category = $("#" + demographic + "GroupByCategoriesDDL" + FO_id).val();
    var id_questionnaire = $("#" + demographic + "GroupByQuestionnairesDDL" + FO_id).val();
    var id_question = $("#" + demographic + "GroupByQuestionsDDL" + FO_id).val();
    var graphic_id = $("#" + demographic + "graphic_id" + FO_id).val();
    var title = $("#" + demographic + FO_id + "Title").val();
    var xaxis = $("#" + demographic + FO_id + "XAxis").val();
    var yaxis = $("#" + demographic + FO_id + "YAxis").val();
    var comments = $("#" + demographic + FO_id + "CommentsEditor").val();
    var elementsCount = $("#" + demographic + "ElementsCount" + FO_id).val();
    if (title != "" || xaxis != "" || yaxis != "" || comments != "") {
        $.post("/ChartReports/SaveComments", { test_id: test_id, graphic_id: graphic_id, title: title, xaxis: xaxis, yaxis: yaxis, comments: comments }, function (j) {
            if (j == 1) {
                if (elementsCount <= 7) {
                    ChangeChart(demographic, FO_id, test_id, id_questionnaire, id_category, id_question, id_compare);
                }
                else {
                    $("#h4Title" + demographic + FO_id).text(title);
                }
            }
        });
    }
});

function UpdatePrintLink(change, value, demographic) {
    if (demographic != "AllTests") {
        var currentValue = $('#' + demographic + 'PrintLink').attr('href');
        var newValue;
        $.post("/ChartReports/UpdateLink", { currentValue: currentValue, changeType: change, newValue: value }, function (newLink) {
            newValue = newLink;
            $('#' + demographic + 'PrintLink').attr('href', newValue);
        });
    }
}

function ChangeChart(demographic, FO_id, test_id, id_questionnaire, id_category, id_question, id_compare) {
    var graphic_id = $("#" + demographic + "graphic_id" + FO_id).val();
    if (chartType == "Univariate" || chartType == "Frequency" || chartType == "Category") {
        //var nullables = "";
        //if (chartType == "Category")
        //    nullables = "&questionnaire_id=" + id_questionnaire + "&id=" + id_category;
        //else
        //    nullables = "&questionnaire_id=" + id_questionnaire + "&category_id=" + id_category + "&question_id=" + id_question + "&compare=" + id_compare;
        //if (demographic == "FunctionalOrganizationType") {
        //    document.getElementById(demographic + "Chart" + FO_id).src = "/ChartReports/" + chartType + "ChartBy" + demographic + "?chartSize=Screen&chartType=" + chartModel + "&graphic_id=" + graphic_id + "&test_id=" + test_id + "&type_id=" + FO_id + nullables;
        //} else {
        //    var mapName = "";
        //    var auxc = "";
        //    if (demographic == "Country") {
        //        mapName = "&name=MyMap2";
        //        auxc = "Map";
        //    }
        //    if (demographic == "General")
        //        document.getElementById(demographic + "Chart").src = "/ChartReports/General" + chartType + "Chart?chartSize=Screen&chartType=" + chartModel + "&graphic_id=" + graphic_id + "&test_id=" + test_id + nullables;
        //    else if (demographic == "Category")
        //        document.getElementById(demographic + "Chart").src = "/ChartReports/CategoryUniVariateChart?chartSize=Screen&chartType=Column&graphic_id=" + graphic_id + "&test_id=" + test_id + "&compare=" + id_compare + "&questionnaire_id=" + id_questionnaire;
        //    else if (demographic == "AllTests")
        //        document.getElementById(demographic + "Chart").src = "/ChartReports/AllTestsUniVariateChart?chartSize=Screen&chartType=Line&graphic_id=" + graphic_id + "&test_id=" + test_id + "&questionnaire_id=" + id_questionnaire + "&category_id=" + id_category + "&question_id=" + id_question;
        //    else {
        //        if (demographic == "Country") {
        //            $.post("/ChartReports/" + chartType + "ChartMapByCountry", { chartSize: "Screen", chartType: chartModel,
        //                graphic_id: graphic_id, test_id: test_id, questionnaire_id: id_questionnaire, category_id: id_category, question_id: id_question, compare: id_compare,
        //                name: "MyMap2"
        //            }, function (j) {
        //                document.getElementById(demographic + "Chart").src = "/ChartReports/" + chartType + "ChartBy" + demographic + "?chartSize=Screen&chartType=" + chartModel + "&graphic_id=" + graphic_id + "&test_id=" + test_id + nullables + mapName;
        //            });
        //        }
        //        else
        //            document.getElementById(demographic + "Chart").src = "/ChartReports/" + chartType + "ChartBy" + demographic + "?chartSize=Screen&chartType=" + chartModel + "&graphic_id=" + graphic_id + "&test_id=" + test_id + nullables + mapName;
        //    }
        //}
        GetChart(chartType, demographic, test_id, FO_id, null, null, null, null, null);
    }
    else {
        if (demographic == "FunctionalOrganizationType") {
            $("#" + demographic + "Chart" + FO_id).attr("src", "/ChartReports/" + demographic + "PercentageChart?chartSize=Screen&chartType=Pie&test_id=" + test_id + "&graphic_id=" + graphic_id + "&type_id=" + FO_id);
        } else {
            var mapName = "";
            if (demographic == "Country")
                mapName = "&name=MyMap2";
            else {
                $("#" + demographic + "Chart").attr("src", "/ChartReports/" + demographic + "PercentageChart?chartSize=Screen&chartType=Pie&test_id=" + test_id + "&graphic_id=" + graphic_id + mapName);
            }
        }
    }
}

function GetChart(chartType, demographic, test_id, FO_id, questionnaire_id, category_id, question_id, compare) {
    if (chartType == "Population") {
        $.post("/ChartReports/GetPercentageChart", { test_id: test_id, demographic: demographic, FO_id: FO_id }, function (info) {
            var options = options_main;
            var gender_m_first = false;
            var data = new google.visualization.DataTable();
            $.each(info[0], function (key, value) {
                data.addColumn(value, key);
            });
            $.each(info[1], function (key, value) {
                data.addRow([key, value]);
                if (demographic == "Gender" && Object.keys(info[1]).length == 1) {
                    gender_m_first = key.startsWith("M");
                }
            });
            options['title'] = info[2];
            options['height'] = 250;
            options['chartArea']['height'] = '90%';
            options['colors'] = demographic == "Gender" ? (gender_m_first ? colors_gender_m : colors_gender) : colors;

            var chart_div_name = demographic + "ChartDiv" + (FO_id != null ? FO_id : "");
            var chart_div = document.getElementById(chart_div_name);
            var chart = new google.visualization.PieChart(chart_div);
            //google.visualization.events.addListener(chart, 'ready', function () {
            //    chart_div.innerHTML = '<img src="' + chart.getImageURI() + '">';
            //});
            chart.draw(data, options);
        });
    }
    else if (chartType == "Univariate") {
        $.post("/ChartReports/GetUnivariateChart", {
            test_id: test_id, demographic: demographic, FO_id: FO_id,
            questionnaire_id: questionnaire_id, category_id: category_id, question_id: question_id, compare: null
        }, function (info) {
            var options = options_main;
            var data = new google.visualization.DataTable();
            $.each(info[0], function (index) {
                data.addColumn(info[0][index][0], info[0][index][1]);
                if (index != 0) {
                    data.addColumn({ type: 'string', role: 'style' });
                    data.addColumn({ type: 'string', role: 'annotation' });
                    data.addColumn({ type: 'boolean', role: 'certainty' });
                }
            });
            data.addRows(info[1]);
            setWidths(info, options);
            options['title'] = info[2];
            options['vAxis'] = { minValue: 0, maxValue: info[3] };
            //if(data.getNumberOfRows() > 4)
            //    options['bar'] = { groupWidth: 30 };
            //if (FO_id != null || demographic == "AgeRange") {
            //    options['chartArea']['height'] = '60%';
            //    options['height'] = 450;
            //}
            var chart_div_name = demographic + "ChartDiv" + (FO_id != null ? FO_id : "");
            var chart_div = document.getElementById(chart_div_name);
            var chart = new google.visualization.ColumnChart(chart_div);
            //google.visualization.events.addListener(chart, 'ready', function () {
            //    chart_div.innerHTML = '<img src="' + chart.getImageURI() + '">';
            //});
            chart.draw(data, options);
        });
    }
    else if (chartType == "Category") {
        $.post("/ChartReports/GetCategoryChart", {
            test_id: test_id, demographic: demographic, FO_id: FO_id,
            questionnaire_id: questionnaire_id, id: category_id, compare: null
        }, function (info) {
            var options = options_main;
            var auxDemo = $('#' + demographic + 'GroupByDemographicDDL' + FO_id + '  option:selected').text();
            var data = new google.visualization.DataTable();
            $.each(info[0], function (index) {
                data.addColumn(info[0][index][0], info[0][index][1]);
                if (index != 0) {
                    data.addColumn({ type: 'string', role: 'style' });
                    data.addColumn({ type: 'string', role: 'annotation' });
                    data.addColumn({ type: 'boolean', role: 'certainty' });
                }
            });
            data.addRows(info[1]);
            setWidths(info, options);
            options['title'] = info[2].replace("()", "(" + auxDemo + ")");
            options['vAxis'] = { minValue: 0, maxValue: info[3] };
            var chart_div_name = demographic + "ChartDiv" + (FO_id != null ? FO_id : "");
            var chart_div = document.getElementById(chart_div_name);
            var chart = new google.visualization.ColumnChart(chart_div);
            //google.visualization.events.addListener(chart, 'ready', function () {
            //    chart_div.innerHTML = '<img src="' + chart.getImageURI() + '">';
            //});
            chart.draw(data, options);
        });
    }
    else if (chartType == "Comparative") {
        $.post("/ChartReports/GetComparativeChart", {
            test_id: test_id, demographic: demographic
        }, function (info) {
            var options = options_main;
            var data = new google.visualization.DataTable();
            $.each(info[0], function (index) {
                data.addColumn(info[0][index][0], info[0][index][1]);
                if (index != 0) {
                    data.addColumn({ type: 'string', role: 'style' });
                    data.addColumn({ type: 'string', role: 'annotation' });
                    data.addColumn({ type: 'boolean', role: 'certainty' });
                }
            });
            data.addRows(info[1]);
            setWidths(info, options);
            options['title'] = info[2];
            if (demographic == "Population") {
                options['chartArea']['left'] = '10%';
            }
            else {
                //options['legend'] = { position: 'top', alignment: 'center' };
                //options['chartArea']['height'] = '85%';
                //options['chartArea']['top'] = '10%';
                options['colors'] = [info[1][0][2]];
            }
            options['vAxis'] = { minValue: 0, maxValue: info[3] };

            var chart_div_name = demographic + "ChartDiv";
            var chart_div = document.getElementById(chart_div_name);
            var chart = new google.visualization.ColumnChart(chart_div);
            //google.visualization.events.addListener(chart, 'ready', function () {
            //    chart_div.innerHTML = '<img src="' + chart.getImageURI() + '">';
            //});
            chart.draw(data, options);
        });
    }
}

function setWidths(info, options) {//, long_legends) {
    //options['width'] = info[1].length <= 4 ? 600 : (info[1].length <= 7 ? 800 : 1000);
    //if (long_legends) {
    //    options['height'] = 450;
    //    options['chartArea']['height'] = '75%';
    //}
    var barWidth = info[1].length <= 4 ? 30 : 20;
    var chartAreaWidth = info[1].length * barWidth;
    //var chartWidth = chartAreaWidth;
    options['bar']['groupWidth'] = barWidth;
    //options['width'] = chartWidth > options['width'] ? chartWidth : options['width'];
    options['chartArea']['width'] = chartAreaWidth > (options['width']*0.9) ? chartAreaWidth : (options['width']*0.9);
    options['chartArea']['left'] = '10%';
}

google.setOnLoadCallback(drawCharts);

function drawCharts() {
    var test_id = $("#test").val();
    $.post("/ChartReports/GetPercentageChart", { test_id: test_id, demographic: "Gender", FO_id: null }, function (info) {
        var options = options_main;
        var gender_m_first = false;
        var data = new google.visualization.DataTable();
        $.each(info[0], function (key, value) {
            data.addColumn(value, key);
        });
        $.each(info[1], function (key, value) {
            data.addRow([key, value]);
            if (Object.keys(info[1]).length == 1) {
                gender_m_first = key.startsWith("M");
            }
        });
        options['title'] = info[2];
        options['legend'] = 'right';
        options['chartArea'] = { 'width': '100%', 'height': '100%' };
        options['colors'] = gender_m_first ? ["#00BFFF", "#FF69B4"] : ["#FF69B4", "#00BFFF"];
        options['is3D'] = true;
        var chart_div = document.getElementById('GenderChart');
        var chart = new google.visualization.PieChart(chart_div);
        chart.draw(data, options);
    });
    $.post("/ChartReports/GetUnivariateChart", {
        test_id: test_id, demographic: "General", FO_id: null,
        questionnaire_id: null, category_id: null, question_id: null, compare: null
    }, function (info) {
        var options = options_main;
        var data = new google.visualization.DataTable();
        $.each(info[0], function (index) {
            data.addColumn(info[0][index][0], info[0][index][1]);
            if (index != 0) {
                data.addColumn({ type: 'string', role: 'style' });
                data.addColumn({ type: 'string', role: 'annotation' });
                data.addColumn({ type: 'boolean', role: 'certainty' });
            }
        });
        data.addRows(info[1]);
        setWidths(info, options);
        options['title'] = info[2];
        options['vAxis'] = { minValue: 0, maxValue: info[3] };
        var chart_div = document.getElementById("GeneralChart");
        var chart = new google.visualization.ColumnChart(chart_div);
        chart.draw(data, options);
    });
    $.post("/ChartReports/GetUnivariateChart", {
        test_id: test_id, demographic: "Location", FO_id: null,
        questionnaire_id: null, category_id: null, question_id: null, compare: null
    }, function (info) {
        var options = options_main;
        var data = new google.visualization.DataTable();
        $.each(info[0], function (index) {
            data.addColumn(info[0][index][0], info[0][index][1]);
            if (index != 0) {
                data.addColumn({ type: 'string', role: 'style' });
                data.addColumn({ type: 'string', role: 'annotation' });
                data.addColumn({ type: 'boolean', role: 'certainty' });
            }
        });
        data.addRows(info[1]);
        setWidths(info, options);
        options['title'] = info[2];
        options['vAxis'] = { minValue: 0, maxValue: info[3] };
        var chart_div = document.getElementById("LocationChart");
        var chart = new google.visualization.ColumnChart(chart_div);
        chart.draw(data, options);
    });
    $.post("/ChartReports/GetUnivariateChart", {
        test_id: test_id, demographic: "FunctionalOrganizationType", FO_id: -1,
        questionnaire_id: null, category_id: null, question_id: null, compare: null
    }, function (info) {
        var options = options_main;
        var data = new google.visualization.DataTable();
        $.each(info[0], function (index) {
            data.addColumn(info[0][index][0], info[0][index][1]);
            if (index != 0) {
                data.addColumn({ type: 'string', role: 'style' });
                data.addColumn({ type: 'string', role: 'annotation' });
                data.addColumn({ type: 'boolean', role: 'certainty' });
            }
        });
        data.addRows(info[1]);
        setWidths(info, options);
        options['title'] = info[2];
        options['vAxis'] = { minValue: 0, maxValue: info[3] };
        var chart_div = document.getElementById("FOTypeChart");
        var chart = new google.visualization.ColumnChart(chart_div);
        chart.draw(data, options);
    });
    $.post("/ChartReports/GetUnivariateChart", {
        test_id: test_id, demographic: "AgeRange", FO_id: null,
        questionnaire_id: null, category_id: null, question_id: null, compare: null
    }, function (info) {
        var options = options_main;
        var data = new google.visualization.DataTable();
        $.each(info[0], function (index) {
            data.addColumn(info[0][index][0], info[0][index][1]);
            if (index != 0) {
                data.addColumn({ type: 'string', role: 'style' });
                data.addColumn({ type: 'string', role: 'annotation' });
                data.addColumn({ type: 'boolean', role: 'certainty' });
            }
        });
        data.addRows(info[1]);
        setWidths(info, options);
        options['title'] = info[2];
        options['vAxis'] = { minValue: 0, maxValue: info[3] };
        var chart_div = document.getElementById("AgeRangeChart");
        var chart = new google.visualization.ColumnChart(chart_div);
        chart.draw(data, options);
    });
    $.post("/ChartReports/GetCategoryChart", {
        test_id: test_id, demographic: "General", FO_id: null,
        questionnaire_id: null, id: null, compare: null
    }, function (info) {
        var options = options_main;
        var data = new google.visualization.DataTable();
        $.each(info[0], function (index) {
            data.addColumn(info[0][index][0], info[0][index][1]);
            if (index != 0) {
                data.addColumn({ type: 'string', role: 'style' });
                data.addColumn({ type: 'string', role: 'annotation' });
                data.addColumn({ type: 'boolean', role: 'certainty' });
            }
        });
        data.addRows(info[1]);
        setWidths(info, options);
        options['title'] = info[2];
        options['vAxis'] = { minValue: 0, maxValue: info[3] };
        var chart_div = document.getElementById("CategoryChart");
        var chart = new google.visualization.ColumnChart(chart_div);
        chart.draw(data, options);
    });
}

$(document).on('change', '.GroupByQuestionnairesDDL', function () {
    Updates(this, "questionnaire_id");
});

$(document).on('change', '.GroupByCategoriesDDL', function () {
    Updates(this, "category_id");
});

$(document).on('change', '.GroupByQuestionsDDL', function () {
    Updates(this, "question_id");
});

$(document).on('change', '.CompareDDL', function () {
    Updates(this, "compare_id");
});

function Updates(Field, Change) {
    var testCompareName = "";
    if (Change == "questionnaire_id") {
        var demographic = Field.name.replace(/GroupByQuestionnairesDDL/g, "");
    }
    else {
        if (Change == "category_id")
            var demographic = Field.name.replace(/GroupByCategoriesDDL/g, "");
        else {
            if (Change == "question_id")
                var demographic = Field.name.replace(/GroupByQuestionsDDL/g, "");
            else
                var demographic = Field.name.replace(/CompareDDL/g, "");
        }
    }
    demographic = demographic.replace(/[0-9]+/g, "");
    var FO_id = "";
    if (demographic == "FunctionalOrganizationType") {
        FO_id = Field.name.replace(/[A-Za-z]+/g, "");
    }
    var pVal;
    if (FO_id != "") {
        pVal = $('input[name=' + FO_id + demographic + 'PValue]:checked').val();
        elements_count = $('#' + demographic + 'ElementsCount' + FO_id).val();
    } else {
        pVal = $('input[name=' + demographic + 'PValue]:checked').val();
        elements_count = $('#' + demographic + 'ElementsCount').val();
    }
    var id_compare;
    if (demographic != "AllTests")
        id_compare = $('#' + demographic + 'CompareDDL' + FO_id).val();
    else
        id_compare = "";
    var id_questionnaire = $('#' + demographic + 'GroupByQuestionnairesDDL' + FO_id).val();
    var id_category;
    var id_question;
    if (Change == "questionnaire_id") {
        id_category = null;
        id_question = null;
        if (id_questionnaire == "") {
            var options = '<option value=""> ' + $('#ViewRes').val() + ' </option>';
            $("#" + demographic + "GroupByQuestionsDDL" + FO_id).html(options);
            $.ajax({
                async: false,
                timeout: 3000,
                type: "POST",
                url: "/Categories/GetCategoriesByCompanyTest",
                data: { test_id: test_id }
            }).done(function (j) {
                if (j) {
                    options = '<option value="">' + $('#ViewRes').val() + '</option>';
                    for (var i = 0; i < j.length; i++) {
                        options += '<option value="' + j[i].optionValue + '">' + j[i].optionDisplay + '</option>';
                    }
                    $("#" + demographic + "GroupByCategoriesDDL" + FO_id).html(options);
                }
            });
            hideDivAnswers(demographic + FO_id);
            showDivSelect(demographic + FO_id);
        } else {
            var options = '<option value=""> ' + $('#ViewRes').val() + ' </option>';
            $("#" + demographic + "GroupByQuestionsDDL" + FO_id).html(options);
            $.ajax({
                async: false,
                timeout: 3000,
                type: "POST",
                url: "/Categories/GetCategoriesByQuestionnaire",
                data: { questionnaire_id: id_questionnaire }
            }).done(function (j) {
                if (j) {
                    options = '<option value="">' + $('#ViewRes').val() + '</option>';
                    for (var i = 0; i < j.length; i++) {
                        options += '<option value="' + j[i].optionValue + '">' + j[i].optionDisplay + '</option>';
                    }
                    $("#" + demographic + "GroupByCategoriesDDL" + FO_id).html(options);
                }
            });
        }
    }
    else {
        id_category = $('#' + demographic + 'GroupByCategoriesDDL' + FO_id).val();
        if (Change == "category_id") {
            id_question = null;
            if (id_category == "") {
                var options = '<option value=""> ' + $('#ViewRes').val() + ' </option>';
                $("#" + demographic + "GroupByQuestionsDDL" + FO_id).html(options);
                hideDivAnswers(demographic + FO_id);
                showDivSelect(demographic + FO_id);
            } else {
                var questionType;
                if ($("#chartType").val() == "TextAnswers")
                    questionType = 2;
                else
                    questionType = 1;
                $.post("/Questions/GetQuestionsByCategoryAndType", { category_id: id_category, type_id: questionType }, function (j) {
                    var options = '<option value="">' + $('#ViewRes').val() + '</option>';
                    for (var i = 0; i < j.length; i++) {
                        options += '<option value="' + j[i].optionValue + '">' + j[i].optionDisplay + '</option>';
                    }
                    $("#" + demographic + "GroupByQuestionsDDL" + FO_id).html(options);
                });
            }
        }
        else
            id_question = $('#' + demographic + 'GroupByQuestionsDDL' + FO_id).val();
    }
    var clavo = !(test_id == 54 && FO_id == 48);
    if (elements_count <= 7 && $("#chartType").val() != "Frequency" && $("#chartType").val() != "Category") {
        MyCondition = false;
        GetChart(chartType, demographic, test_id, FO_id, id_questionnaire, id_category, id_question, id_compare);
        //ChangeChart(demographic, FO_id, test_id, id_questionnaire, id_category, id_question, id_compare);
    } else {
        MyCondition = true;
    }
    if ($("#chartType").val() == "TextAnswers") {
        if (id_question != null) {
            UpdateTableForTextAnswers(demographic, id_questionnaire, id_category, id_question, FO_id, id_compare, "");
        }
    }
    else if ($("#chartType").val() == "Frequency" || $("#chartType").val() == "Category")
        UpdateTablesFC(demographic, id_questionnaire, id_category, id_question, FO_id, null);
    else
        UpdateTables(demographic, id_questionnaire, id_category, id_question, MyCondition, pVal, FO_id, id_compare);

}

function showDivSelect(demographic) {
    $('#DivSelect' + demographic).show();
}

function hideDivSelect(demographic) {
    $('#DivSelect' + demographic).hide();
}

function showDivAnswers(demographic) {
    $('#AnswersDiv' + demographic).show();
}

function hideDivAnswers(demographic) {
    $('#AnswersDiv' + demographic).hide();
}

function UpdateTableForTextAnswers(demographic, id_quetionnaire, id_category, id_question, FO_id, id_compare, item) {
    $.post("/ChartReports/UpdateTableForTextAnswers", { questionnaire_id: id_quetionnaire, category_id: id_category, question_id: id_question, test_id: test_id, demographic: demographic, FO_id: FO_id, compare_id: id_compare }, function (data) {
        var FO_id_var = "";
        if (FO_id != "") {
            FO_id_var = FO_id;
        }
        textAnswersVectorCallback = new Array();
        demographicTextAnswersCallback = "Answers" + demographic + FO_id_var;
        if (item != "") {
            textAnswersVectorCallback = data[item];
        }
        else {
            var k = 1;
            $.each(data, function (label, answers) {
                textAnswersVectorCallback = data[label];
                makePaginate(k, demographicTextAnswersCallback, textAnswersVectorCallback, demographic, FO_id_var);
                k++;
            });
        }
        hideDivSelect(demographic + FO_id_var);
        showDivAnswers(demographic + FO_id_var);
    });
}

function makePaginate(label, demographicTextAnswersCallback, textAnswersVectorCallback, demographic, FO_id_var) {
    var opt = { callback: pageselectCallback,
        items_per_page: 10,
        num_display_entries: 5,
        current_page: 0,
        num_edge_entries: 5,
        prev_text: $("#ButtonBack").val(),
        next_text: $("#ButtonNext").val(),
        div_aux: demographicTextAnswersCallback + label,
        text: textAnswersVectorCallback
    };
    $("#Pagination" + demographic + FO_id_var + label).pagination(textAnswersVectorCallback.length, opt).trigger('create'); ;
}

function pageselectCallback(page_index, jq, div, text) {
    var items_per_page = 10; // Number of elements per pagionation page
    var max_elem = Math.min((page_index + 1) * items_per_page, text.length);
    var newcontent = '';
    //newcontent += '<dt>' + text[0].Label + '</dt>';
    for (var i = page_index * items_per_page; i < max_elem; i++) {
        if (i % 2 == 0)
            newcontent += '<dd class="state par">' + text[i] + '</dd>';
        else
            newcontent += '<dd class="state inpar">' + text[i] + '</dd>';
    }
    $('#' + div).html(newcontent);
    return false;
}

function UpdateTablesFC(demographic, id_questionnaire, id_category, id_question, FO_id, id_compare) {
    var head = $("#" + demographic + FO_id + "Table > thead > tr > th");
    var headerA = "";
    $.each(head, function () {
        headerA = headerA + '-' + this.textContent;
    });
    $.post("/ChartReports/UpdateTableFC", { questionnaire_id: id_questionnaire, category_id: id_category, question_id: id_question, test_id: test_id, demographic: demographic, FO_id: FO_id, type: chartType, header: headerA }, function (data) {
        //$("#" + demographic + FO_id + "Table").css("width","auto");
        $("#" + demographic + FO_id + "Table > tbody").empty();
        var dh = data[0];
        var trclass = "";
        var tdclass = "";
        for (var i = 1; i < data.length - 1; i++) {
            if (i % 2 == 0)
                trclass = "even";
            else
                trclass = "odd";
            var html = "";
            var row = data[i];
            html = html + "<tr class='" + trclass + "'>";
            tdclass = "";
            for (var j = 0; j < row.length; j++) {
                if (j == 0)
                    tdclass = "sorting_1";
                var c = j > 0 ? 'alignCenter' : '';
                html = html + "<td class=" + c + " " + tdclass + ">" + row[j] + "</td>";
            };
            html = html + "</tr>";
            if (i == 1)
                $("#" + demographic + FO_id + "Table >tbody").append(html);
            else
                $("#" + demographic + FO_id + "Table >tbody tr:last").after(html);
            $(".table1").trigger("update");

        }
    });
}

function UpdateTables(demographic, id_questionnaire, id_category, id_question, MyCondition, pVal, FO_id, id_compare) {
    // Update SatisfiedAndNoSatisfied table
    if (FO_id != "") {
        DeleteRows(demographic + FO_id + "Table");
    } else {
        DeleteRows(demographic + "Table");
    }

    $.post("/ChartReports/UpdateTable", { questionnaire_id: id_questionnaire, category_id: id_category, question_id: id_question, test_id: test_id, condition: MyCondition, demographic: demographic, pValue: pVal, FO_id: FO_id, compare_id: id_compare }, function (data) {
        var header;
        var currentClass, newClass;
        for (var i = 0; i < data.length; i++) {
            if (MyCondition == true) { //TableForm
                if (id_compare == "") {
                    if (i == 0) {
                        //                            currentClass = "span-24";
                        //                            newClass = "span-12 column";
                        header = "<thead><tr><th class='ui-table-cell-visible' data-priority='1'></th><th class='ui-table-cell-visible' data-priority='1' align='center'>" + $("#Average").val() + "</th><th class='ui-table-cell-visible' data-priority='1' align='center'>" + $("#Median").val() + "</th>"
                        + "<th class='ui-table-cell-visible' data-priority='1' align='center'>" + $("#Satisfied").val() + "</th><th class='ui-table-cell-visible' data-priority='1' align='center'>" + $("#NoSatisfied").val() + "</th></tr></thead>";
                    }
                    var html = "<tr><th class='ui-table-cell-visible'>" + data[i].Label + "</th><td class='ui-table-cell-visible' style='text-align: right;'>" + Math.round(data[i].Average * 100) / 100 + "</td><td class='ui-table-cell-visible' style='text-align: right;'>" + data[i].Median + "</td><td class='ui-table-cell-visible' style='color:Green; text-align: right;'  >" + data[i].Satisfied + "</td><td class='ui-table-cell-visible' style='color:Red; text-align: right;' >" + data[i].NotSatisfied + "</td></tr>";
                }
                else {
                    if (i == 0) {
                        //                            currentClass = "span-12 column";
                        //                            newClass = "span-24";
                        header = "<thead><tr><th rowspan='2'></th><td  align='center' colspan='4'>" + data[i].TestName + "</td><td  align='center' colspan='4'>" + data[i].TestCompareName + "</td></tr>"
                        + "<tr><th class='ui-table-cell-visible' data-priority='1' align='center'>" + $("#Average").val() + "</th><th class='ui-table-cell-visible' data-priority='1' align='center'>" + $("#Median").val() + "</th><th class='ui-table-cell-visible' data-priority='1' align='center'>" + $("#Satisfied").val() + "</th><th class='ui-table-cell-visible' data-priority='1' align='center'>" + $("#NoSatisfied").val() + "</th>"
                        + "<th class='ui-table-cell-visible' data-priority='1' align='center'>" + $("#Average").val() + "</th><th class='ui-table-cell-visible' data-priority='1' align='center'>" + $("#Median").val() + "</th><th class='ui-table-cell-visible' data-priority='1' align='center'>" + $("#Satisfied").val() + "</th><th class='ui-table-cell-visible' data-priority='1' align='center'>" + $("#NoSatisfied").val() + "</th></tr></thead>";
                    }
                    var html = "<tr><th class='ui-table-cell-visible'>" + data[i].Label + "</th><td class='ui-table-cell-visible' style='text-align: right;'>" + Math.round(data[i].Average * 100) / 100 + "</td><td class='ui-table-cell-visible' style='text-align: right;'>" + data[i].Median + "</td><td class='ui-table-cell-visible' style='color:Green; text-align: right;'  >" + data[i].Satisfied + "</td><td class='ui-table-cell-visible' style='color:Red; text-align: right;' >" + data[i].NotSatisfied + "</td>"
                    + "<td class='ui-table-cell-visible' style='text-align: right;'>" + Math.round(data[i].AverageCompare * 100) / 100 + "</td><td class='ui-table-cell-visible' style='text-align: right;'>" + data[i].MedianCompare + "</td><td class='ui-table-cell-visible' style='color:Green; text-align: right;'  >" + data[i].SatisfiedCompare + "</td><td class='ui-table-cell-visible' style='color:Red; text-align: right;' >" + data[i].NotSatisfiedCompare + "</td></tr>";
                }
            } else {
                if (id_compare == "") {
                    if (i == 0) {
                        currentClass = "span-24 column last";
                        newClass = "span-12 column";
                        header = "<thead><tr><th></th><th align='center'>" + $("#Satisfied").val() + "</th><th align='center'>" + $("#NoSatisfied").val() + "</th></tr>";
                    }
                    var html = "<tr><th>" + data[i].Label + "</th><td style='color:Green; text-align: right;' >" + data[i].Satisfied + "</td><td style='color:Red; text-align: right;'>" + data[i].NotSatisfied + "</td></tr></thead>";
                }
                else {
                    if (i == 0) {
                        currentClass = "span-12 column";
                        newClass = "span-24 column last";
                        header = "<thead><tr><th rowspan='2'></th><td align='center' colspan='2'>" + data[i].TestName + "</td><td align='center' colspan='2'>" + data[i].TestCompareName + "</td></tr>"
                        + "<tr><th align='center'>" + $("#Satisfied").val() + "</th><th align='center'>" + $("#NoSatisfied").val() + "</th><th align='center'>" + $("#Satisfied").val() + "</th><th align='center'>" + $("#NoSatisfied").val() + "</th></tr></thead>";
                    }
                    var html = "<tr><th>" + data[i].Label + "</th><td style='color:Green; text-align: right;' >" + data[i].Satisfied + "</td><td style='color:Red; text-align: right;'>" + data[i].NotSatisfied + "</td><td style='color:Green; text-align: right;' >" + data[i].SatisfiedCompare + "</td><td style='color:Red; text-align: right;'>" + data[i].NotSatisfiedCompare + "</td></tr>";
                }
            }
            if (FO_id != "") {
                if (i == 0) {
                    if (MyCondition != true)
                        ChangeDivClass(demographic + FO_id + "DivSat", currentClass, newClass);
                    ChangeTableHeader(demographic + FO_id + "Table", header);
                    AddBody(demographic + FO_id + "Table");
                }
                AddRows(demographic + FO_id + "Table", html);
            } else {
                if (i == 0) {
                    if (MyCondition != true)
                        ChangeDivClass(demographic + "DivSat", currentClass, newClass);
                    ChangeTableHeader(demographic + "Table", header);
                    AddBody(demographic + FO_id + "Table");
                }
                AddRows(demographic + "Table", html);
            }
        }
    });
    // Update chiSquare Table
    if (elements_count >= 2 /*demographic == "General"*/) {
        if (FO_id != "") {
            DeleteRows(demographic + FO_id + "TableChiSquare");
        } else {
            DeleteRows(demographic + "TableChiSquare");
        }
        $.post("/ChartReports/UpdateChiSquare", { questionnaire_id: id_questionnaire, category_id: id_category, question_id: id_question, country_id: null, pValue: pVal, test_id: test_id, demographic: demographic, FO_id: FO_id, compare_id: id_compare }, function (cs) {
            var header;
            var currentClass, newClass;
            var columnLast = " column last";
            for (var i = 0; i < cs.length; i++) {
                var ChiSquareValue = cs[i].chiSquareValue;
                var OurChiSquare = Math.round(cs[i].ourChiSquare * 100) / 100;
                var Conclusion = cs[i].conclusion;
                if (id_compare == "") {
                    if (i == 0) {
                        currentClass = "span-24";
                        newClass = "span-12 column last";
                        header = "<thead><tr><th align='center'>" + $("#ChiSquareValue").val() + "</th><th align='center'>" + $("#OurChiSquare").val() + "</th><th align='center'>" + $("#Conclusion").val() + "</th></tr></thead>";
                    }
                    var html = "<tr><td style='text-align: right;'>" + ChiSquareValue + "</td><td style='text-align: right;'>" + OurChiSquare + "</td><td style='text-align: left;'>" + Conclusion + "</td></tr>";
                }
                else {
                    if (i == 0) {
                        currentClass = "span-12 column last";
                        newClass = "span-24 column last";
                        header = "<thead><tr><th></th><th align='center'>" + $("#ChiSquareValue").val() + "</th><th align='center'>" + $("#OurChiSquare").val() + "</th><th align='center'>" + $("#Conclusion").val() + "</th></tr></thead>";
                    }
                    var html = "<tr><td style='text-align: left;'>" + cs[i].testName + "</td><td style='text-align: right;'>" + ChiSquareValue + "</td><td style='text-align: right;'>" + OurChiSquare + "</td><td style='text-align: left;'>" + Conclusion + "</td></tr>";
                }
                if (FO_id != "") {
                    if (i == 0) {
                        ChangeDivClass(demographic + FO_id + "DivChi", currentClass, newClass);
                        ChangeTableHeader(demographic + FO_id + "TableChiSquare", header);
                        AddBody(demographic + FO_id + "TableChiSquare");
                    }
                    AddRows(demographic + FO_id + "TableChiSquare", html);
                } else {
                    if (i == 0) {
                        ChangeDivClass(demographic + "DivChi", currentClass, newClass);
                        ChangeTableHeader(demographic + "TableChiSquare", header);
                        AddBody(demographic + "TableChiSquare");
                    }
                    AddRows(demographic + "TableChiSquare", html);
                }
            }
            if (FO_id != "") {
                $("#" + demographic + FO_id + "Table").trigger("update");
            } else {
                $("#" + demographic + "Table").trigger("update");
            }
        });
    }
}

function DeleteRows(tableId) {
    $("#" + tableId).empty();
}

function AddRows(tableId, html) {
    $("#" + tableId + ">tbody").append(html).trigger("create");
}

function ChangeDivClass(divId, currentClass, newClass) {
    $("#" + divId).removeClass(currentClass);
    $("#" + divId).addClass(newClass);
}

function ChangeTableHeader(tableId, header) {
    $("#" + tableId).append(header).trigger("create") ;
}

function AddBody(tableId) {
    $("#" + tableId).append("<tbody></tbody>");
}


//For Bivariate Graphics
//El bivariate se hizo de esta manera ya que jquery mobile para la version actual (1.3.0) no provee un metodo
//para que dinamicamente se actualize el column toggle.. en la documentacion de dicha version indica que el metodo
//esta definido pero no se encuentra cuando se hace llamado a el

$(document).on('pageinit', '#mainChartBivariate', function () {
    $("#Table").attr("cellpadding", 10);
    demographicsHtml = $("#Demographics2").html();
    company_id = $("#company_id").val();
    test_id = $("#test_id").val();
    changeStyle();
});

$(document).on('change', '#Demographics1', function () {
    if ($(this).val()) {
        if ($("#Demographics2").val()) {
            GetTableOrImage();
        }
        else {
            updateDemograhicsList2();
            hideChart();
            hideTable();
        }
    }
    else {
        if ($("#Demographics2").val()) {
        }
        else {
            $("#Demographics1").html(demographicsHtml);
            $("#Demographics2").html(demographicsHtml);
        }
        hideChart();
        hideTable();
    }
});

$(document).on('change', '#Demographics2', function () {
    if ($(this).val()) {
        if ($("#Demographics1").val()) {
            GetTableOrImage();
        }
        else {
            updateDemograhicsList1();
            hideChart();
            hideTable();
        }
    }
    else {
        if ($("#Demographics1").val()) {
        }
        else {
            $("#Demographics2").html(demographicsHtml);
            $("#Demographics1").html(demographicsHtml);
        }
        hideChart();
        hideTable();
    }
});

function updateDemograhicsList2() {
    $.post("/ChartReports/UpdateDropDownListBivariateChart", { demographic1_value: $("#Demographics1 option:selected").val(), test_id: test_id, demographic1_key: $("#Demographics1").val() }, function (j) {
        var options = '<option value="">' + $("#ViewRes").val() + '</option>';
        for (var i = 0; i < j.length; i++) {
            options += '<option value="' + j[i].optionValue + '">' + j[i].optionDisplay + '</option>';
        }
        $("#Demographics2").html(options);
    });
}

function updateDemograhicsList1() {
    $.post("/ChartReports/UpdateDropDownListBivariateChart", { demographic1_value: $("#Demographics2 option:selected").val(), test_id: test_id, demographic1_key: $("#Demographics2").val() }, function (j) {
        var options = '<option value="">' + $("#ViewRes").val() + '</option>';
        for (var i = 0; i < j.length; i++) {
            options += '<option value="' + j[i].optionValue + '">' + j[i].optionDisplay + '</option>';
        }
        $("#Demographics1").html(options);
    });
}

function GetTableOrImage() {
    var test_id = $("#test_id").val();
    var demographic1 = $("#Demographics1").val();
    var demographic2 = $("#Demographics2").val();
    if (demographic1 != demographic2) {
        $.post("/ChartReports/GetIsTable", { test_id: test_id, demo1: demographic1, demo2: demographic2 },
        function (t) {
            if (t == "True") {
                updateDataTable(test_id, demographic1, demographic2);
            }
            else {
                updateSourceImage(test_id, demographic1, demographic2);
            }
        });
    }
    else {
        alert($("#demographicsEquals").val());
    }
}

function updateSourceImage(test_id, demographic1, demographic2) {
    //document.getElementById("Chart").src = "/ChartReports/SelectBivariateGraphic?test_id=" + test_id + "&demographic_1=" + demographic1 + "&demographic_2=" + demographic2;
    google.setOnLoadCallback(function () { GetBivariateChart(test_id, demographic1, demographic2) });
    hideTable();
    showChart();
}

function GetBivariateChart(test_id, demographic1, demographic2) {
    var options = {
        title: "",
        titleTextStyle: { fontSize: 15 },
        is3D: true,
        width: screenWidth,
        height: 400,
        colors: colors,
        chartArea: {
            left: 30,
            width: '70%',
            height: '70%'
        },
        bar: { groupWidth: "90%" },
        explorer: { axis: 'horizontal', keepInBounds: true },
        legend: {
            textStyle: {
                fontSize: 13
            }
        },
        annotations: {
            alwaysOutside: false,
            textStyle: {
                fontSize: 10,
                bold: true,
                color: '#000',
                auraColor: 'none'
            }
        },
        animation: {
            duration: 1000,
            easing: 'out',
            startup: true
        }
    };
    $.post("/ChartReports/GetBivariateChart", {
        test_id: test_id, demographic_1: demographic1, demographic_2: demographic2
    }, function (info) {
            if (info[1].length > 0) {
                $("#noMinimum").hide();
                var data = new google.visualization.DataTable();
                $.each(info[0], function (index) {
                    data.addColumn(info[0][index][0], info[0][index][1]);
                    if (index != 0) {
                        data.addColumn({ type: 'string', role: 'style' });
                        data.addColumn({ type: 'string', role: 'annotation' });
                        data.addColumn({ type: 'boolean', role: 'certainty' });
                    }
                });
                data.addRows(info[1]);
                var barGroupWidth = ((info[1][0].length - 1) / 4) * 30;
                var chartAreaWidth = info[1].length * barGroupWidth;
                var chartWidth = chartAreaWidth + 250;
                options['width'] = chartWidth > screenWidth ? chartWidth : screenWidth;
                options['title'] = info[2];
                options['hAxis'] = {
                    title: info[4],
                    titleTextStyle: {
                        bold: true,
                        fontSize: 12
                    }
                };
                options['vAxis'] = {
                    title: info[3],
                    minValue: 0, maxValue: info[5],
                    titleTextStyle: {
                        bold: true,
                        fontSize: 12
                    }
                };
                var chart = new google.visualization.ColumnChart(document.getElementById("Chart"));
                chart.draw(data, options);
            }
            else {
                $("#noMinimum").show();
            }
    });
}

function updateDataTable(test_id, demographic1, demographic2) {
    DeleteRowsBivariate();
    $.post("/ChartReports/SelectBivariateTable", { test_id: test_id, demographic_1: demographic1, demographic_2: demographic2 }, function (table) {
        $("#Title").val(table[6]);
        var demoName1 = table[0]; //.name;
        var demoCount1 = table[1]; //.count;
        var demoName2 = table[2]; //.name;
        var demoCount2 = table[3]; //.count;
        var optionsCount = table[5];
        var stringObject = new Array();
        stringObject = table[4];
        var first = true;
        var cab = new Array(); var c = 0;
        $.each(stringObject, function (demo1, trObject) {
            var stringDemo1 = demo1;
            var tdObject = new Array();
            tdObject = trObject; //stringObject[demo1];
            //cargar segunda linea de la cabecera
            html = "";
            if (first) {
                html = html + "<th rowspan='1' colspan='1' padding='10px'></th>";
                $.each(tdObject, function (names2, value) {
                    html = html + "<th class='ui-table-cell-visible' data-priority='1' padding='10px' style='border-style:solid; border-width:1px; text-align:right'>" + names2 + "</th>";
                    cab[c] = names2;
                    c++;
                });
                AddRowsHead(html, true);
            }

            //cargar th de Demographics1
            html = "";
            //cargar demo de la izkierda
            html = html + "<th padding='10px' style='border-style:solid; border-width:1px'>" + stringDemo1 + "</th>";
            //cargar row con promedios
            //-------REVISAR AQUI-------//
            //$.each(tdObject, function (key, value) {
            $.each(cab, function () {
                var color = ''; //= GetColor(optionsCount, value);
                //var pct = value * 100 / optionsCount;
                var pct = tdObject[this] ? tdObject[this].toFixed(2) : 0;

                //if (pct > 80) color = 'green';
                //else if (pct > 60) color = '#FF9900';
                //else if (pct > 0) color = 'red';
                //else color = 'black';
                html = html + "<td class='ui-table-cell-visible' padding='10px' style='border-style:solid; border-width:1px;'><span style='color:" + color + ";'>" + pct + "</span></td>"
            });
            AddRows2(html, false);
            if (first) {
                first = false;
            }
        });
        hideChart();
        showTable();
        $("#Table").trigger("update");
        $("#Table").table("refresh");
    });
}

function DeleteRowsBivariate() {
    $("#Table thead").empty();
    $("#Table tbody").empty();
}

function AddRowsHead(html, first) {
    var tr = "<tr>" + html + "</tr>";
    $("#Table thead").append(tr).trigger("create"); ;
}

function AddRows2(html, first) {
    var tr = "<tr>" + html + "</tr>";
    $("#Table tbody").append(tr).trigger("create"); ;
}

function showChart() {
    $('#DivChart').show();
}

function hideChart() {
    $('#DivChart').hide();
    document.getElementById("Chart").src = "";
}

function showTable() {
    $('#FieldsetTable').show();
}

function hideTable() {
    $('#FieldsetTable').hide();
}

//RANKING

$(document).on('pageinit', '#mainChartRanking', function () {
    questionnaire = $("#questionnaire").val();
    company = null;
    FOTime = {};
    changeStyle();
});

function createFOTime2() {
    if ($("#FOids").val() != "") {
        var FOids = $("#FOids").val().split("-");
        FOids.pop();
        var FOT = new Array();
        for (var i = 0; i < FOids.length; i++) {
            FOT[i] = new Array(FOids[i], 0);
        }
        return FOT;
    }
    else
        return null;
}

 $(document).on('click', '.allRanking', handle_tab_clickRanking);

function handle_tab_clickRanking(event) {
    event.preventDefault();
    var demographic = $(event.target).attr("href")
    handle_tab_click2(demographic);
}

$(document).on('change', '.demTabs', handle_tab_clickRanking2);

function handle_tab_clickRanking2(event) {
    event.preventDefault();
    var demographic = $('#selectDemographic option:selected').val();
    handle_tab_click2(demographic);
}

function handle_tab_click2(text) {
    var demographic = text.replace("#tab", "");
    hideAllExternal(demographic);
    hideAll();
    $('#tabSelect').hide();
    if (demographic == "GeneralCountry" && GeneralCountryTime == 0) {
        $.post("/ChartReports/LoadRankingTab", { questionnaire_id: questionnaire, demographic: "GeneralCountry", FO_id: null, company_id: company }, function (htmlText) {
            $('#tabGeneralCountry').html(htmlText).trigger("create");
            GeneralCountryTime++;
            company = null;
        });
    } else if (demographic == "Internal" && InternalTime == 0) {
        $.post("/ChartReports/LoadRankingTab", { questionnaire_id: questionnaire, demographic: "Internal", FO_id: null, company_id: company }, function (htmlText) {
            $('#tabInternal').append(htmlText).trigger("create");
            InternalTime++;
            company = null;
            $("#tabInternal").delegate(".PrintPdf", "click", function () {
                PrintPdfFunction(this);
            });
            if ($('#Rol').val() == "HRAdministrator")
                InitializeCompanyDDL();
            else
                InitializeTabs();
        });
    } else if (demographic == "Customer" && CustomerTime == 0) {
        $.post("/ChartReports/LoadRankingTab", { questionnaire_id: questionnaire, demographic: "Customer", FO_id: null, company_id: company }, function (htmlText) {
            $('#tabCustomer').html(htmlText).trigger("create");
            CustomerTime++;
            company = null;
        });

    } else if (demographic == "Region" && RegionTime == 0) {
        $.post("/ChartReports/LoadRankingTab", { questionnaire_id: questionnaire, demographic: "Region", FO_id: null, company_id: company }, function (htmlText) {
            $('#tabRegion').html(htmlText).trigger("create");
            RegionTime++;
        });
    }
    else if (demographic == "Country" && CountryTime == 0) {
        $.post("/ChartReports/LoadRankingTab", { questionnaire_id: questionnaire, demographic: "Country", FO_id: null, company_id: company }, function (htmlText) {
            $('#tabCountry').html(htmlText).trigger("create");
            CountryTime++;
        });
    }
    else if (demographic == "AgeRange" && AgeTime == 0) {
        $.post("/ChartReports/LoadRankingTab", { questionnaire_id: questionnaire, demographic: "AgeRange", FO_id: null, company_id: company }, function (htmlText) {
            $('#tabAgeRange').html(htmlText).trigger("create");
            AgeTime++;
        });
    }

    else if (demographic == "InstructionLevel" && InstructionTime == 0) {
        $.post("/ChartReports/LoadRankingTab", { questionnaire_id: questionnaire, demographic: "InstructionLevel", FO_id: null, company_id: company }, function (htmlText) {
            $('#tabInstructionLevel').html(htmlText).trigger("create");
            InstructionTime++;
        });
    }
    else if (demographic == "Gender" && GenderTime == 0) {
        $.post("/ChartReports/LoadRankingTab", { questionnaire_id: questionnaire, demographic: "Gender", FO_id: null, company_id: company }, function (htmlText) {
            $('#tabGender').html(htmlText).trigger("create");
            GenderTime++;
        });
    }
    else if (demographic == "Location" && LocationTime == 0) {
        $.post("/ChartReports/LoadRankingTab", { questionnaire_id: questionnaire, demographic: "Location", FO_id: null, company_id: company }, function (htmlText) {
            $('#tabLocation').html(htmlText).trigger("create");
            LocationTime++;
        });
    }
    else if (demographic == "PositionLevel" && PositionLevelsTime == 0) {
        $.post("/ChartReports/LoadRankingTab", { questionnaire_id: questionnaire, demographic: "PositionLevel", FO_id: null, company_id: company }, function (htmlText) {
            $('#tabPositionLevel').html(htmlText).trigger("create");
            PositionLevelsTime++;
        });
    }
    else if (demographic == "Seniority" && SeniorityTime == 0) {
        $.post("/ChartReports/LoadRankingTab", { questionnaire_id: questionnaire, demographic: "Seniority", FO_id: null, company_id: company }, function (htmlText) {
            $('#tabSeniority').html(htmlText).trigger("create");
            SeniorityTime++;
        });
    }

    else if (demographic == "Performance" && PerformanceTime == 0) {
        $.post("/ChartReports/LoadRankingTab", { questionnaire_id: questionnaire, demographic: "Performance", FO_id: null, company_id: company }, function (htmlText) {
            $('#tabPerformance').html(htmlText).trigger("create");
            PerformanceTime++;
        });
    }

    else if (demographic.charAt(2) == '-') {
        var pos;
        var FO_id = demographic.replace("FO-", "");
        for (pos = 0; pos < FOTime.length; pos++) {
            if (FOTime[pos][0] == FO_id) {
                if (FOTime[pos][1] == 0) {
                    $.post("/ChartReports/LoadRankingTab", { questionnaire_id: questionnaire, demographic: "FunctionalOrganizationType", FO_id: FO_id, company_id: company }, function (htmlText) {
                        $('#tabFO-' + FO_id).html(htmlText).trigger("create");
                    });
                    FOTime[pos][1]++;
                }
                break;
            }
        }
    }
    else {
        hideLoading();
    }
    showExternal(demographic);
}

function hideAllExternal(demographic) {
    $('#tabCustomer').hide();
    $('#tabGeneral').hide();
    $("#tabGeneralCountry").hide();
    if (demographic == "Internal") {
        $('#tabExternal').hide();
    }
    if (demographic == "External") {
        $('#tabInternal').hide();
    }
}

function showExternal(text) {
    $("#tab" + text).show();
    if (text == "External") {
        $('#tabGeneral').show();
    }
}

function InitializeCompanyDDL() {
    jQuery("#tabInternal").delegate(".Companies", "change", function () {
        var company_id = $(this).val();
        if (company_id) {
            company = company_id;
            hideDivSelect2("Internal");
            LoadTabs(company_id);
            initializeVariables();
        }
        else {
            company = null;
            $('#DemographicsDiv').html("");
            hideFieldSet2("Internal");
            showDivSelect2("Internal");
        }
    });
}

function LoadTabs(company_id) {
    $.post("/ChartReports/LoadCompanyTabs", { questionnaire: questionnaire, company: company_id }, function (htmlText) {
        $('#DemographicsDiv').html(htmlText).trigger('create');
        InitializeTabs();
    });
}

function InitializeTabs() {
    //$("#myInternalTabs").tabs();
    //$(".all").bind("click", handle_tab_click);
    FOTime = createFOTime2();
    showFieldset2("Internal");
}

$(document).on('change', '.Sectors', function () {
    var demographic = this.name.replace(/Sectors/g, "");
    var cou;
    if (demographic == "GeneralCountry")
        cou = $('#Countries' + demographic).val();
    else
        cou = null;
    if (validate($(this).val(), cou, demographic)) {
        var sector_id = $(this).val();
        var country_id = cou;
        hideDivSelect2(demographic);
        UpdateTable2(sector_id, country_id, null, "", demographic);
    }
    else {
        DivSelect2($(this).val(), cou, null, demographic);
    }
});

$(document).on('change', '.Countries', function () {
    var demographic = this.name.replace(/Countries/g, "");
    var sec;
    if ($('#Rol').val() == "HRAdministrator")
        sec = $('#Sectors' + demographic).val();
    else
        sec = 1;
    if (validate(sec, $(this).val(), demographic)) {
        var country_id = $(this).val();
        var sector_id = sec;
        hideDivSelect2(demographic);
        UpdateTable2(sector_id, country_id, null, "", demographic);
    }
    else {
        DivSelect2(sec, $(this).val(), null, demographic);
    }
});

    function DivSelect2(sector, country, company, demographic) {
        if (!ValidateValuesForRanking(sector, country, company, demographic)) {
            hideTable2(demographic);
            hideDiv2(demographic);
            showDivSelect2(demographic);
        }
    }

    function UpdateTable2(sector_id, country_id, company_id, fot, demographic) {
        var pos = 0;
        $.post("/ChartReports/UpdateRanking", { questionnaire_id: questionnaire, sector_id: sector_id, country_id: country_id, company_id: company_id, fot: fot, demographic: demographic }, function (companyDouble) {
            if (companyDouble.length > 0) {
                DeleteRows2("Ranking" + demographic + fot);
                for (var i = 0; i < companyDouble.length; i++) {
                    var companyShow = "";
                    if (companyDouble[i].show)
                        companyShow = companyDouble[i].companyName;
                    pos++;
                    var html = "<tr><td align='left'>" + companyShow + "</td><td style='text-align: center;'>" + pos + "</td><td style='text-align: center;'>" + companyDouble[i].companyClimate + "%</td></tr>";
                    AddRowsRanking("Ranking" + demographic + fot, html); //[companyShow, pos, companyDouble[i].companyClimate + "%"]);
                }
            }
            if (pos > 0) {
                hideDiv2(demographic + fot);
                showTable2(demographic + fot);
                showFieldset2(demographic + fot);
            }
            else {
                hideTable2(demographic + fot);
                showDiv2(demographic + fot);
                hideFieldSet2(demographic + fot);
            }
            $('.table1').trigger("update");
        });
    }

    function validate(sector, country, demo) {
        var secBool = false;
        var couBool = false;
        if ($('#Rol').val() == "HRAdministrator") {
            if (sector != 0 && sector != "")
                secBool = true;
        }
        else
            secBool = true;
        if (demo == "GeneralCountry") {
            if (country != 0 && country != "")
                couBool = true;
        }
        else
            couBool = true;
        return (secBool && couBool);
    }

    function DeleteRows2(tableId) {
        $("#" + tableId).find("tr:gt(0)").remove(); //.fnClearTable(); //.find("tr:gt(0)").remove();
    }

    function AddRowsRanking(tableId, html) {
        $("#" + tableId + " tbody").append(html);
    }

    function showTable2(demographic) {
        $('#Ranking' + demographic).show();
        $(".table1").tablesorter();
    }

    function hideTable2(demographic) {
        $('#Ranking' + demographic).hide();
    }

    function showDiv2(demographic) {
        $('#NoCompanies' + demographic).show();
    }

    function hideDiv2(demographic) {
        $('#NoCompanies' + demographic).hide();
    }

    function showFieldset2(demographic) {
        $('#Fieldset' + demographic).show();
    }

    function hideFieldSet2(demographic) {
        $('#Fieldset' + demographic).hide();
    }

    function showDivSelect2(demographic) {
        $('#DivSelect' + demographic).show();
    }

    function hideDivSelect2(demographic) {
        $('#DivSelect' + demographic).hide();
    }

    function ValidateValuesForRanking(sector, country, company, demographic) {
        if (demographic == "General" || demographic == "Customer" || demographic == "GeneralCountry") {
            if (sector) {
                if (demographic == "GeneralCountry" && !country)
                    return false;
            }
            else
                return false;
        }
        else {
            if (demographic == "Internal") {
                if (!company)
                    return false;
            }
        }
    }


    //---------------categories report--------------------------------------------------//

    $(document).on('change', '.GroupByDemographicDDL', function () {
        UpdatesCategory(this, "demographic_id");
    });

    $(document).on('change', '.GroupByCategoryQuestionnairesDDL', function () {
        UpdatesCategory(this, "questionnaire_id");
    });

    function UpdatesCategory(Field, Change) {
        if (Change == "questionnaire_id")
            var demographic = Field.name.replace(/GroupByCategoryQuestionnairesDDL/g, "");
        else
            var demographic = Field.name.replace(/GroupByDemographicDDL/g, "");
        demographic = demographic.replace(/[0-9]+/g, "");
        var FO_id = "";
        if (demographic == "FunctionalOrganizationType") {
            FO_id = Field.name.replace(/[A-Za-z]+/g, "");
        }
        var id_questionnaire = $('#' + demographic + 'GroupByCategoryQuestionnairesDDL' + FO_id).val();
        var id_demographic = $('#' + demographic + 'GroupByDemographicDDL' + FO_id).val();
        if (Change == "questionnaire_id") {
            id_demographic = null;
            $.ajax({
                type: "POST",
                url: "/ChartReports/GetDemographicsByQuestionnaire",
                data: { test_id: test_id, demographic: demographic, c_fo: FO_id, questionnaire_id: id_questionnaire }
            }).done(function (j) {
                if (j) {
                    options = '<option value="">' + $('#ViewRes').val() + '</option>';
                    for (var i = 0; i < j.length; i++) {
                        options += '<option value="' + j[i].optionValue + '">' + j[i].optionDisplay + '</option>';
                    }
                    $("#" + demographic + "GroupByDemographicDDL" + FO_id).html(options);
                }
            });
        }
        if ($("#" + demographic + "ElementsCount" + FO_id).val() > 5) {
            $.post("/ChartReports/LoadTableCategory", { demographic: demographic, test_id: test_id, FO_id: FO_id, id_questionnaire: id_questionnaire, id_demographic: id_demographic }, function (htmlText) {
                UpdateTableCategory(demographic, FO_id , htmlText);
            });
        } 
        else {
            google.setOnLoadCallback(function () { GetChart(chartType, "FunctionalOrganizationType", test_id, FO_id, null, null, null, null) });
            //ChangeChart(demographic, FO_id, test_id, id_questionnaire, id_demographic, null, null);
            //        ChangeChart(demographic, FO_id, test_id, id_questionnaire, id_category, id_question, id_compare);
        }
    }

    function UpdateTableCategory(demographic, FO_id,htmlText) {
        $("#" + demographic + FO_id + "Table tbody").empty();
        for (var k = 0; k < htmlText.length; k++) {
            var j = htmlText[k];
            $("#" + demographic + FO_id + "Table tbody").append("<tr><td>" + j.Key + "</td><td>" + j.Value + "</td></tr>");
        }
        $("#" + demographic + FO_id + "Table").trigger("update");
        //$(".table1").tablesorter();
    }
    //-----------------------------------------------------------------//