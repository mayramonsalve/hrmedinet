google.charts.load('current', { 'packages': ['corechart'] });
var downloadImg = "";

//google.charts.setOnLoadCallback(drawCharts);

$(document).ajaxStart(function () {
    $("#loading").show();
});

$(document).ajaxComplete(function () {
    $("#loading").hide();
});

function InitializeDataTable() {
    var sort = true;
    if ($("#chartType").val() == "Satisfaction") {
        sort = false;
    }
    $('.tabla').dataTable({
        "bPaginate": false,
        "bFilter": false,
        "bInfo": false,
        "bJQueryUI": true,
        "bRetrieve": true,
        "bSort": sort,
        "bAutoWidth": false
    });
}

function InitializeDataTableById(demo) {
    var sort = true;
    if ($("#chartType").val() == "Satisfaction") {
        sort = false;
    }
    $('#' + demo).dataTable({
        "bPaginate": false,
        "bFilter": false,
        "bInfo": false,
        "bJQueryUI": true,
        "bRetrieve": true,
        "bSort": sort,
        "bAutoWidth": false
    });
}

$(document).ready(function () {
    downloadImg = $('#DownloadImage').val();
    var optionsInQuestionnaire = $("#OptionsInQuestionnaire").val();
    var test_id = $("#test_id").val();
    var compare_id = $("#compare_id").val(); if (compare_id == 0) compare_id = null;
    var chartType = $("#chartType").val();
    var chartModel = $("#chartModel").val();
    var RegionTime = 0;
    var AllTestsTime = 0;
    var ClimateTime = 0;
    var AgeTime = 0;
    var CountryTime = 0;
    var InstructionTime = 0;
    var LocationTime = 0;
    var PositionLevelsTime = 0;
    var SeniorityTime = 0;
    var GenderTime = 0;
    var PerformanceTime = 0;
    var CategoryTime = 0;
    var FOTime = createFOTime();
    var elements_count = 0;
    var textAnswersVectorCallback = new Array();
    var demographicTextAnswersCallback;

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

    $(function () {
        $("#myTabs").tabs();
        //SetTextEditorPlugin();
        //if (chartType == "Population")
        //    $("#GeneralCommentsEditor").css("height", 130);
    });

    var initialTab = (chartType == "Comparative") ? "Population" : "General";
    google.setOnLoadCallback(function () { GetChart(chartType, initialTab, test_id, null, null, null, null, null) });

    function SetTextEditorPlugin() {
        if (chartType != "TextAnswers") {
            $(".textEditor").tinymce({
                script_url: '../../Scripts/tiny_mce.js',
                theme: "advanced",
                theme_advanced_buttons1: "bold,italic,underline,strikethrough,|,justifyleft,justifycenter,justifyright,justifyfull,|,fontselect,fontsizeselect",
                theme_advanced_buttons2: "bullist,numlist,|,outdent,indent,blockquote,|,undo,redo,|,forecolor,backcolor",
                theme_advanced_toolbar_location: "top",
                theme_advanced_toolbar_align: "left",
                theme_advanced_statusbar_location: "none",
                theme_advanced_resizing: false
            });
        }
    }

    $(".all").bind("click", handle_tab_click);
    function handle_tab_click(event) {
        var demographic = $(event.target).attr("href").replace("#tab", "");
        var selValId = "";
        if (chartType == "Satisfaction") {
            selValId = demographic.replace(/[A-Za-z]+/g, "");
            demographic = demographic.replace(/[0-9]+/g, "");
        }
        elements_count = $('#' + demographic + 'ElementsCount').val();
        if (demographic == "Region" && RegionTime == 0) {
            $.post("/ChartReports/LoadTab", { demographic: "Region", type: chartType, test_id: test_id, FO_id: null, compare_id: compare_id }, function (htmlText) {
                $('#tabRegion').html(htmlText);
                google.setOnLoadCallback(function () { GetChart(chartType, demographic, test_id, null, null, null, null, null) } );
                SetTextEditorPlugin();
                RegionTime++;
            });
        }
        else if (demographic == "Country" && CountryTime == 0) {
            $.post("/ChartReports/LoadTab", { demographic: "Country", type: chartType, test_id: test_id, FO_id: null, compare_id: compare_id }, function (htmlText) {
                $('#tabCountry').html(htmlText);
                google.setOnLoadCallback(function () { GetChart(chartType, demographic, test_id, null, null, null, null, null) } );
                SetTextEditorPlugin();
                CountryTime++;
            });
        }
        else if (demographic == "AgeRange" && AgeTime == 0) {
            $.post("/ChartReports/LoadTab", { demographic: "AgeRange", type: chartType, test_id: test_id, FO_id: null, compare_id: compare_id }, function (htmlText) {
                $('#tabAgeRange').html(htmlText);
                google.setOnLoadCallback(function () { GetChart(chartType, demographic, test_id, null, null, null, null, null) } );
                SetTextEditorPlugin();
                AgeTime++;
            });
        }
        else if (demographic == "InstructionLevel" && InstructionTime == 0) {
            $.post("/ChartReports/LoadTab", { demographic: "InstructionLevel", type: chartType, test_id: test_id, FO_id: null, compare_id: compare_id }, function (htmlText) {
                $('#tabInstructionLevel').html(htmlText);
                google.setOnLoadCallback(function () { GetChart(chartType, demographic, test_id, null, null, null, null, null) } );
                SetTextEditorPlugin();
                InstructionTime++;
            });
        }
        else if (demographic == "Location" && LocationTime == 0) {
            $.post("/ChartReports/LoadTab", { demographic: "Location", type: chartType, test_id: test_id, FO_id: null, compare_id: compare_id }, function (htmlText) {
                $('#tabLocation').html(htmlText);
                google.setOnLoadCallback(function () { GetChart(chartType, demographic, test_id, null, null, null, null, null) } );
                SetTextEditorPlugin();
                LocationTime++;
            });
        }
        else if (demographic == "PositionLevel" && PositionLevelsTime == 0) {
            $.post("/ChartReports/LoadTab", { demographic: "PositionLevel", type: chartType, test_id: test_id, FO_id: null, compare_id: compare_id }, function (htmlText) {
                $('#tabPositionLevel').html(htmlText);
                google.setOnLoadCallback(function () { GetChart(chartType, demographic, test_id, null, null, null, null, null) } );
                SetTextEditorPlugin();
                PositionLevelsTime++;
            });
        }
        else if (demographic == "Seniority" && SeniorityTime == 0) {
            $.post("/ChartReports/LoadTab", { demographic: "Seniority", type: chartType, test_id: test_id, FO_id: null, compare_id: compare_id }, function (htmlText) {
                $('#tabSeniority').html(htmlText);
                google.setOnLoadCallback(function () { GetChart(chartType, demographic, test_id, null, null, null, null, null) } );
                SetTextEditorPlugin();
                SeniorityTime++;
            });
        }
        else if (demographic == "Gender" && GenderTime == 0) {
            $.post("/ChartReports/LoadTab", { demographic: "Gender", type: chartType, test_id: test_id, FO_id: null, compare_id: compare_id }, function (htmlText) {
                $('#tabGender').html(htmlText);
                google.setOnLoadCallback(function () { GetChart(chartType, demographic, test_id, null, null, null, null, null) } );
                SetTextEditorPlugin();
                GenderTime++;
            });
        }
        else if (demographic == "Performance" && PerformanceTime == 0) {
            $.post("/ChartReports/LoadTab", { demographic: "Performance", type: chartType, test_id: test_id, FO_id: null, compare_id: compare_id }, function (htmlText) {
                $('#tabPerformance').html(htmlText);
                google.setOnLoadCallback(function () { GetChart(chartType, demographic, test_id, null, null, null, null, null) } );
                SetTextEditorPlugin();
                PerformanceTime++;
            });
        }
        else if (demographic == "Category" && CategoryTime == 0 && chartType != "Satisfaction") {
            $.post("/ChartReports/LoadTab", { demographic: "Category", type: chartType, test_id: test_id, FO_id: null, compare_id: compare_id }, function (htmlText) {
                $('#tabCategory').html(htmlText);
                google.setOnLoadCallback(function () { GetChart(chartType, demographic, test_id, null, null, null, null, null) } );
                SetTextEditorPlugin();
                CategoryTime++;
            });
        }
        else if (demographic == "AllTests" && AllTestsTime == 0) {
            $.post("/ChartReports/LoadTab", { demographic: "AllTests", type: chartType, test_id: test_id, FO_id: null, compare_id: null }, function (htmlText) {
                $('#tabAllTests').html(htmlText);
                google.setOnLoadCallback(function () { GetChart(chartType, demographic, test_id, null, null, null, null, null) } );
                SetTextEditorPlugin();
                AllTestsTime++;
            });
        }
        else if (demographic == "Climate" && ClimateTime == 0) {
            $.post("/ChartReports/LoadTab", { demographic: "Climate", type: chartType, test_id: test_id, FO_id: null, compare_id: null }, function (htmlText) {
                $('#tabClimate').html(htmlText);
                google.setOnLoadCallback(function () { GetChart(chartType, demographic, test_id, null, null, null, null, null) } );
                SetTextEditorPlugin();
                ClimateTime++;
            });
        }
        else if (chartType == "Satisfaction") {
            var div = $('#tab' + demographic + selValId).html();
            if (div == "") {
                var auxDemographic = demographic.replace("General", "");
                $.post("/ChartReports/LoadTab", { demographic: auxDemographic, type: chartType, test_id: test_id, FO_id: selValId, compare_id: null }, function (htmlText) {
                    var id = '#tab' + demographic + selValId;
                    $(id).html(htmlText);
                    //SetTextEditorPlugin();
                });
            }
        }
        else if (demographic.charAt(2) == '-') {
            var pos;
            var FO_id = $(event.target).attr("href").replace("#tabFO-", "");
            for (pos = 0; pos < FOTime.length; pos++) {
                if (FOTime[pos][0] == FO_id) {
                    if (FOTime[pos][1] == 0) {
                        $.post("/ChartReports/LoadTab", { demographic: "FunctionalOrganizationType", type: chartType, test_id: test_id, FO_id: FO_id, compare_id: compare_id }, function (htmlText) {
                            $('#tabFO-' + FO_id).html(htmlText);
                            google.setOnLoadCallback(function () { GetChart(chartType, "FunctionalOrganizationType", test_id, FO_id, null, null, null, null) } );
                            SetTextEditorPlugin();
                        });
                        FOTime[pos][1]++;
                        elements_count = $('#' + demographic + 'ElementsCount' + FO_id).val();
                    }
                    break;
                }
            }

        }

    }


    function GetChart(chartType, demographic, test_id, FO_id, questionnaire_id, category_id, question_id, compare) {
        var colors = ["#00a0e3", "#ffce00", "#00b386", "#ff044d", "#664147", "#084c61", "#3c1642",
                        "#1a181b", "#b4656f", "#f9b9f2", "#102542", "#2e0219", "#f8333c", "#a62639",
                        "#065143", "#d9dbf1", "#af2bbf", "#841c26", "#aaaaaa", "#d30c7b", "#01295f",
                        "#fd151b", "#FF69B4", "#00BFFF"];
        var colors_gender = ["#FF69B4", "#00BFFF"];
        var colors_gender_m = ["#00BFFF", "#FF69B4"];
        var options = {
            title: "",
            legend: 'none',
            is3D: true,
            width: 600,
            height: 400,
            top: 0,
            bar: { groupWidth: 75 },
            chartArea: { 'width': '90%', 'height': '85%', 'top': '5%' },
            colors: colors,
            annotations: {
                alwaysOutside: false,
                textStyle: {
                    fontSize: 10,
                    bold: true,
                    color: '#fff',
                    auraColor: 'none'
                }
            },
            animation: {
                duration: 1000,
                easing: 'out',
                startup: true
            }
        };
        if (chartType == "Population") {
            var chart_div_name = demographic + "ChartDiv" + (FO_id != null ? FO_id : "");
            var chart_div = document.getElementById(chart_div_name);
            if ($(chart_div).length) {
                $.post("/ChartReports/GetPercentageChart", { test_id: test_id, demographic: demographic, FO_id: FO_id }, function (info) {
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
                    options['legend'] = 'right';
                    options['chartArea'] = { 'width': '90%', 'height': '90%' };
                    options['colors'] = demographic == "Gender" ? (gender_m_first ? colors_gender_m : colors_gender) : colors;
                    var chart = new google.visualization.PieChart(chart_div);
                    var chart_img_name = chart_div_name.replace("ChartDiv", "Img");
                    var img = document.getElementById(chart_img_name);
                    google.visualization.events.addListener(chart, 'ready', function () {
                        img.innerHTML = '<a href="' + chart.getImageURI() + '" download>' + downloadImg + '</a>';
                    });
                    chart.draw(data, options);
                });
            }
        }
        else if (chartType == "Univariate") {
            var chart_div_name = demographic + "ChartDiv" + (FO_id != null ? FO_id : "");
            var chart_div = document.getElementById(chart_div_name);
            if ($(chart_div).length) {
                $.post("/ChartReports/GetUnivariateChart", {
                    test_id: test_id, demographic: demographic, FO_id: FO_id,
                    questionnaire_id: questionnaire_id, category_id: category_id, question_id: question_id, compare: null
                }, function (info) {
                    if (info[1].length <= 10) {
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
                        var long_legends = FO_id != null || demographic == "AgeRange";
                        setWidths(info, options, long_legends);
                        options['title'] = info[2];
                        options['vAxis'] = { minValue: 0, maxValue: info[3] };
                        var chart = new google.visualization.ColumnChart(chart_div);
                        var chart_img_name = chart_div_name.replace("ChartDiv", "Img");
                        var img = document.getElementById(chart_img_name);
                        google.visualization.events.addListener(chart, 'ready', function () {
                            img.innerHTML = '<a href="' + chart.getImageURI() + '" download>' + downloadImg + '</a>';
                        });
                        chart.draw(data, options);
                    }
                });
            }
        }
        else if (chartType == "Category") {
            var chart_div_name = demographic + "ChartDiv" + (FO_id != null ? FO_id : "");
            var chart_div = document.getElementById(chart_div_name);
            if ($(chart_div).length) {
                $.post("/ChartReports/GetCategoryChart", {
                    test_id: test_id, demographic: demographic, FO_id: FO_id,
                    questionnaire_id: questionnaire_id, id: category_id, compare: null
                }, function (info) {
                    if (info[1].length <= 10) {
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
                        setWidths(info, options, false);
                        options['title'] = info[2].replace("()", "(" + auxDemo + ")");
                        options['vAxis'] = { minValue: 0, maxValue: info[3] };
                        var chart = new google.visualization.ColumnChart(chart_div);
                        var chart_img_name = chart_div_name.replace("ChartDiv", "Img");
                        var img = document.getElementById(chart_img_name);
                        google.visualization.events.addListener(chart, 'ready', function () {
                            img.innerHTML = '<a href="' + chart.getImageURI() + '" download>' + downloadImg + '</a>';
                        });
                        chart.draw(data, options);
                    }
                });
            }
        }
        else if (chartType == "Comparative") {
            var chart_div_name = demographic + "ChartDiv";
            var chart_div = document.getElementById(chart_div_name);
            if ($(chart_div).length) {
                $.post("/ChartReports/GetComparativeChart", {
                    test_id: test_id, demographic: demographic
                }, function (info) {
                    if (info[1].length <= 10) {
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
                        setWidths(info, options, false);
                        options['title'] = info[2];
                        if (demographic == "Population") {
                            options['chartArea']['left'] = '10%';
                        }
                        else {
                            options['legend'] = { position: 'top', alignment: 'center' };
                            options['chartArea']['height'] = '85%';
                            options['chartArea']['top'] = '10%';
                            options['colors'] = [info[1][0][2]];
                        }
                        options['vAxis'] = { minValue: 0, maxValue: info[3] };
                        var chart = new google.visualization.ColumnChart(chart_div);
                        var chart_img_name = chart_div_name.replace("ChartDiv", "Img");
                        var img = document.getElementById(chart_img_name);
                        google.visualization.events.addListener(chart, 'ready', function () {
                            img.innerHTML = '<a href="' + chart.getImageURI() + '" download>' + downloadImg + '</a>';
                        });
                        chart.draw(data, options);
                    }
                });
            }
        }

        function setWidths(info, options, long_legends) {
            options['width'] = info[1].length <= 4 ? 600 : (info[1].length <= 7 ? 800 : 1000);
            if (long_legends) {
                options['height'] = 450;
                options['chartArea']['height'] = '75%';
            }
            //var paddingWidth = 20;
            //var barWidth = info[1].length <= 5 ? 75 : 50;
            //var chartAreaWidth = info[1].length * barWidth;
            //var chartWidth = chartAreaWidth + paddingWidth;
            //options['bar']['groupWidth'] = barWidth;
            //options['width'] = chartWidth > options['width'] ? chartWidth : options['width'];
            //options['chartArea']['width'] = chartAreaWidth > options['width'] * 0.9 ? chartAreaWidth : options['width'] * 0.9;
        }
    }

    $(".tool").tooltip({
        position: "center right",
        offset: [-2, 10],
        effect: "fade"
    });

    function DeleteRows(tableId) {
        $("#" + tableId).empty();
    }

    function AddRows(tableId, html) {
        $("#" + tableId + " tr:last").after(html);
    }

    function ChangeDivClass(divId, currentClass, newClass) {
        $("#" + divId).removeClass(currentClass);
        $("#" + divId).addClass(newClass);
    }

    function ChangeTableHeader(tableId, header) {
        $("#" + tableId).append(header);
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
            var countOptions = $("#countOptions").val();
            var trClass = "odd";
            for (var i = 0; i < data.length; i++) {
                if (MyCondition == true) { //TableForm
                    if (id_compare == "") {
                        if (i == 0) {
                            //                            currentClass = "span-24";
                            //                            newClass = "span-12 column";
                            header = "<thead><tr><th></th><th align='center'>" + $("#Average").val() + "</th><th align='center'>" + $("#Median").val() + "</th>"
                            + "<th align='center'>" + $("#Satisfied").val() + "</th><th align='center'>" + $("#NoSatisfied").val() + "</th></tr>";
                        }
                        var html = "<tr class='" + trClass + "'><td>" + data[i].Label + "</td><td style='text-align: right;'>" + Math.round(data[i].Average * 100) / countOptions + "</td><td style='text-align: right;'>" + data[i].Median + "</td><td style='color:Green; text-align: right;'  >" + data[i].Satisfied + "</td><td style='color:Red; text-align: right;' >" + data[i].NotSatisfied + "</td></tr>";
                    }
                    else {
                        if (i == 0) {
                            //                            currentClass = "span-12 column";
                            //                            newClass = "span-24";
                            header = "<thead><tr><th rowspan='2'></th><th  align='center' colspan='4'>" + data[i].TestName + "</th><th  align='center' colspan='4'>" + data[i].TestCompareName + "</th></tr>"
                            + "<th align='center'>" + $("#Average").val() + "</th><th align='center'>" + $("#Median").val() + "</th><th align='center'>" + $("#Satisfied").val() + "</th><th align='center'>" + $("#NoSatisfied").val() + "</th>"
                            + "<th align='center'>" + $("#Average").val() + "</th><th align='center'>" + $("#Median").val() + "</th><th align='center'>" + $("#Satisfied").val() + "</th><th align='center'>" + $("#NoSatisfied").val() + "</th></tr></thead>";
                        }
                        var html = "<tr class='" + trClass + "'><td>" + data[i].Label + "</td><td style='text-align: right;'>" + Math.round(data[i].Average * 100) / countOptions + "</td><td style='text-align: right;'>" + data[i].Median + "</td><td style='color:Green; text-align: right;'  >" + data[i].Satisfied + "</td><td style='color:Red; text-align: right;' >" + data[i].NotSatisfied + "</td>"
                        + "<td style='text-align: right;'>" + Math.round(data[i].AverageCompare * 100) / countOptions + "</td><td style='text-align: right;'>" + data[i].MedianCompare + "</td><td style='color:Green; text-align: right;'  >" + data[i].SatisfiedCompare + "</td><td style='color:Red; text-align: right;' >" + data[i].NotSatisfiedCompare + "</td></tr>";
                    }
                } else {
                    if (id_compare == "") {
                        if (i == 0) {
                            currentClass = "span-24 column last";
                            newClass = "span-12 column";
                            header = "<thead><tr><th></th><th align='center'>" + $("#Satisfied").val() + "</th><th align='center'>" + $("#NoSatisfied").val() + "</th></tr>";
                        }
                        var html = "<tr><td>" + data[i].Label + "</td><td style='color:Green; text-align: right;' >" + data[i].Satisfied + "</td><td style='color:Red; text-align: right;'>" + data[i].NotSatisfied + "</td></tr></thead>";
                    }
                    else {
                        if (i == 0) {
                            currentClass = "span-12 column";
                            newClass = "span-24 column last";
                            header = "<thead><tr><th rowspan='2'></th><th align='center' colspan='2'>" + data[i].TestName + "</th><th align='center' colspan='2'>" + data[i].TestCompareName + "</th></tr>"
                            + "<tr><th align='center'>" + $("#Satisfied").val() + "</th><th align='center'>" + $("#NoSatisfied").val() + "</th><th align='center'>" + $("#Satisfied").val() + "</th><th align='center'>" + $("#NoSatisfied").val() + "</th></tr></thead>";
                        }
                        var html = "<tr><td>" + data[i].Label + "</td><td style='color:Green; text-align: right;' >" + data[i].Satisfied + "</td><td style='color:Red; text-align: right;'>" + data[i].NotSatisfied + "</td><td style='color:Green; text-align: right;' >" + data[i].SatisfiedCompare + "</td><td style='color:Red; text-align: right;'>" + data[i].NotSatisfiedCompare + "</td></tr>";
                    }
                }

                trClass = trClass == "odd" ? "even" : "odd";

                if (FO_id != "") {
                    if (i == 0) {
                        if (MyCondition != true)
                            ChangeDivClass(demographic + FO_id + "DivSat", currentClass, newClass);
                        ChangeTableHeader(demographic + FO_id + "Table", header);
                    }
                    AddRows(demographic + FO_id + "Table", html);
                } else {
                    if (i == 0) {
                        if (MyCondition != true)
                            ChangeDivClass(demographic + "DivSat", currentClass, newClass);
                        ChangeTableHeader(demographic + "Table", header);
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
                        }
                        AddRows(demographic + FO_id + "TableChiSquare", html);
                    } else {
                        if (i == 0) {
                            ChangeDivClass(demographic + "DivChi", currentClass, newClass);
                            ChangeTableHeader(demographic + "TableChiSquare", header);
                        }
                        AddRows(demographic + "TableChiSquare", html);
                    }
                }
            });
        }
    }

    function UpdateTablesFC(demographic, id_questionnaire, id_category, id_question, FO_id, id_compare) {
        var head = $("#" + demographic + FO_id + "Table > thead > tr > th");
        var headerA = "";
        $.each(head, function () {
            headerA = headerA + '-' + this.textContent;
        });
        $.post("/ChartReports/UpdateTableFC", { questionnaire_id: id_questionnaire, category_id: id_category, question_id: id_question, test_id: test_id, demographic: demographic, FO_id: FO_id, type: chartType, header: headerA }, function (data) {
            $("#" + demographic + FO_id + "Table >tbody").empty();
            //            var header = "<thead><tr><th></th>";
            var dh = data[0];
            if (optionsInQuestionnaire == 0) {
                $("#" + demographic + FO_id + "Table >thead").empty();
                var headerNew = data[data.length - 1];
                var htmlHeaderNew = "<tr class=''>";
                var c = 'alignCenter ui-state-default';
                htmlHeaderNew = htmlHeaderNew + "<th class='ui-state-default' rowspan='1' colspan='1'><div class='DataTables_sort_wrapper'>"
                            + "<span class='DataTables_sort_icon css_right ui-icon ui-icon-triangle-1-n'></span></div></th>";
                for (var j = 0; j < headerNew.length; j++) {

                    htmlHeaderNew = htmlHeaderNew + "<th class='alignCenter ui-state-default' rowspan='1' colspan='1'>" +
                                    "<div class='DataTables_sort_wrapper'>" + headerNew[j] +
                                    "<span class='DataTables_sort_icon css_right ui-icon ui-icon-carat-2-n-s'></span></div></th>";

                    //htmlHeaderNew = htmlHeaderNew + "<th class=" + c + " " + ">" + headerNew[j] + "</th>";
                };
                htmlHeaderNew = htmlHeaderNew + "</tr>";
                $("#" + demographic + FO_id + "Table >thead").append(htmlHeaderNew);
            }

            var trclass = "";
            var tdclass = "";

            var table = $("#" + demographic + FO_id + "Table").dataTable();
            table.fnClearTable();
            for (var k = 1; k < data.length - 1; k++) {
                var row = data[k];
                table.dataTable().fnAddData(row);
            }
            /*for (var i = 1; i < data.length - 1; i++) {
            if (i % 2 == 0)
            trclass = "even";
            else
            trclass = "odd";
            var html = "";
            var row = data[i];
            //                if (i = 1)
            //                    html = "<tbody>";
            html = html + "<tr class='" + trclass + "'>";
            tdclass = "";
            for (var j = 0; j < row.length; j++) {
            if (j == 0)
            tdclass = "sorting_1";
            var c = j > 0 ? 'alignCenter' : '';
            html = html + "<td class=" + c + " " + tdclass + ">" + row[j] + "</td>";
            };
            html = html + "</tr>";
            //                if (i = data.length - 1)
            //                    html = html + "</tbody>";
            if (i == 1)
            $("#" + demographic + FO_id + "Table >tbody").append(html);
            else
            $("#" + demographic + FO_id + "Table >tbody tr:last").after(html);
            //                AddRows(demographic + FO_id + "Table", html);
            }
            //            if (FO_id != "") {
            //                AddRows(demographic + FO_id + "Table", html);
            //            } else {
            //                AddRows(demographic + "Table", html);
            //            

            //            InitializeDataTable();
            $('#' + demographic + FO_id + 'Table').dataTable({
            "bPaginate": false,
            "bFilter": false,
            "bInfo": false,
            "bJQueryUI": true,
            "bRetrieve": true,
            "bSort": true,
            "bAutoWidth": false
            });*/
        });
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

    jQuery("#myTabs").delegate(".item", "click", function () {
        //pair.Value-demographicvar_FO_id
        if (!($(this).hasClass("currentItem"))) {
            var id = $(this).attr("id");
            var aux = (id.split('-'))[1];
            var FO_id = "";
            var demographic = aux.replace(/[0-9]+/g, "");
            if (demographic == "FunctionalOrganizationType")
                FO_id = aux.replace(/[A-Za-z]+/g, "");
            $("#itemsDiv" + demographic + FO_id).find(".item").each(function () {
                $(this).removeClass("currentItem");
            });
            $(this).addClass("currentItem");
            var item = (id.split('-'))[0];
            var id_compare = "";
            var id_questionnaire = $("#" + demographic + "GroupByQuestionnairesDDL" + FO_id).val();
            var id_category = $("#" + demographic + "GroupByCategoriesDDL" + FO_id).val();
            var id_question = $("#" + demographic + "GroupByQuestionsDDL" + FO_id).val();
            UpdateTableForTextAnswers(demographic, id_questionnaire, id_category, id_question, FO_id, id_compare, item);
        }
        return false;
    });

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
                //                for (var i = 0; i < data.length; i++) { //label, textAnswers, textAnswersCompare
                //                    textAnswersVectorCallback = data[i].TextAnswers;
                //                    demographicTextAnswersCallback = "Answers" + demographic + FO_id_var;
                //                }
            }
            else {
                var divItems;
                divItems = "<div class='contenedor'>";
                var first = true;
                var currentClass = "currentItem";
                $.each(data, function (label, answers) {
                    divItems = divItems + "<div class='item " + currentClass + "' id='" + label + "-" + demographic + FO_id_var + "'>" + label + "</div>";
                    if (first) {
                        currentClass = "";
                        textAnswersVectorCallback = data[label];
                        first = false;
                    }
                });
                divItems = divItems + "</div>";
            }
            hideDivSelect(demographic + FO_id_var);
            var opt = { callback: pageselectCallback,
                items_per_page: 10,
                num_display_entries: 5,
                current_page: 0,
                num_edge_entries: 5,
                prev_text: $("#ButtonBack").val(),
                next_text: $("#ButtonNext").val(),
                div_aux: demographicTextAnswersCallback,
                text: textAnswersVectorCallback
            };
            $("#itemsDiv" + demographic + FO_id_var).html(divItems);
            $("#Pagination" + demographic + FO_id_var).pagination(textAnswersVectorCallback.length, opt);
            showDivAnswers(demographic + FO_id_var);
        });
    }

    //-----------------------------------------------------------------//

    jQuery("#myTabs").delegate(".GroupByDemographicDDL", "change", function () {
        UpdatesCategory(this, "demographic_id");
        //        var demographic = this.name.replace(/GroupByDemographicDDL/g, "");
        //        demographic = demographic.replace(/[0-9]+/g, "");
        //        var FO_id = "";
        //        if (demographic == "FunctionalOrganizationType") {
        //            FO_id = this.name.replace(/[A-Za-z]+/g, "");
        //        }
        //        var id_demographic = $('#' + demographic + 'GroupByDemographicDDL' + FO_id).val();
        //        ChangeChart(demographic, FO_id, test_id, id_demographic, null, null);
    });

    jQuery("#myTabs").delegate(".GroupByCategoryQuestionnairesDDL", "change", function () {
        UpdatesCategory(this, "questionnaire_id");
        //        var demographic = this.name.replace(/GroupByCategoryQuestionnairesDDL/g, "");
        //        demographic = demographic.replace(/[0-9]+/g, "");
        //        var FO_id = "";
        //        if (demographic == "FunctionalOrganizationType") {
        //            FO_id = this.name.replace(/[A-Za-z]+/g, "");
        //        }
        //        var id_demographic = $('#' + demographic + 'GroupByCategoryQuestionnairesDDL' + FO_id).val();
        //        ChangeChart(demographic, FO_id, test_id, id_demographic, null, null);
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
                async: false,
                timeout: 3000,
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
                UpdateTableCategory(demographic, FO_id, htmlText);
            });
        }
        else {
            ChangeChart(demographic, FO_id, test_id, id_questionnaire, id_demographic, null, null);
            //        ChangeChart(demographic, FO_id, test_id, id_questionnaire, id_category, id_question, id_compare);
        }
    }
    function UpdateTableCategory(demographic, FO_id, htmlText) {

        /* $("#" + demographic + FO_id + "Table tbody").empty();
        for (var k = 0; k < htmlText.length; k++) {
        var j = htmlText[k];
        $("#" + demographic + FO_id + "Table tbody").append("<tr><td>" + j.Key + "</td><td>" + j.Value + "</td></tr>");
        }
        InitializeDataTableById(demographic + FO_id + "Table");
        */

        var table = $("#" + demographic + FO_id + "Table").dataTable();
        table.fnClearTable();
        $.post("/ChartReports/GetOptionsByTest", { test_id: test_id }, function (text) {

            for (var k = 0; k < htmlText.length; k++) {
                var j = htmlText[k];
                table.dataTable().fnAddData([j.Key, (parseFloat((j.Value * 100) / text).toFixed(2))]);
            }
        });

    }

    jQuery("#myTabs").delegate(".GroupByQuestionnairesDDL", "change", function () {
        Updates(this, "questionnaire_id");
    });

    jQuery("#myTabs").delegate(".GroupByCategoriesDDL", "change", function () {
        Updates(this, "category_id");
    });

    jQuery("#myTabs").delegate(".GroupByQuestionsDDL", "change", function () {
        Updates(this, "question_id");
    });

    jQuery("#myTabs").delegate(".CompareDDL", "change", function () {
        Updates(this, "compare_id");
    });

    //-----------------------------------------------------------------//

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
        if (elements_count <= 10 && $("#chartType").val() != "Frequency" && $("#chartType").val() != "Category") {
            MyCondition = false;
            ChangeChart(demographic, FO_id, test_id, id_questionnaire, id_category, id_question, id_compare);
        } else {
            MyCondition = true;
        }
        if ($("#chartType").val() == "TextAnswers") {
            if (id_question != null) {
                UpdateTableForTextAnswers(demographic, id_questionnaire, id_category, id_question, FO_id, id_compare, "");
            }
        }
        else if ($("#chartType").val() == "Frequency" || $("#chartType").val() == "Category") {
            if ((optionsInQuestionnaire > 0 || (optionsInQuestionnaire == 0 && id_category != null && id_category != "" && id_question != null && id_question != "")))
                UpdateTablesFC(demographic, id_questionnaire, id_category, id_question, FO_id, null);
        }
        else
            UpdateTables(demographic, id_questionnaire, id_category, id_question, MyCondition, pVal, FO_id, id_compare);

        //        if (Change == "category_id")
        //            UpdatePrintLink("category_id", id_category, demographic + FO_id);
        //        else if (Change == "question_id")
        //            UpdatePrintLink("question_id", id_question, demographic + FO_id);
        //        else if (Change == "compare_id")
        //            UpdatePrintLink("compare_id", id_compare, demographic + FO_id);
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

            GetChart(chartType, demographic, test_id, FO_id, id_questionnaire, id_category, id_question, id_compare);
        }
        else {
            if (demographic == "FunctionalOrganizationType") {
                $("#" + demographic + "Chart" + FO_id).attr("src", "/ChartReports/" + demographic + "PercentageChart?chartSize=Screen&chartType=Pie&test_id=" + test_id + "&graphic_id=" + graphic_id + "&type_id=" + FO_id);
            } else {
                var mapName = "";
                if (demographic == "Country")
                    mapName = "&name=MyMap2";
                else
                    $("#" + demographic + "Chart").attr("src", "/ChartReports/" + demographic + "PercentageChart?chartSize=Screen&chartType=Pie&test_id=" + test_id + "&graphic_id=" + graphic_id + mapName);
            }
        }
    }

    //-----------------------------------------------------------------//

    var time = 0;
    jQuery("#myTabs").delegate("input:radio[class=PValue]", "click", function () {
        var demographic = this.name.replace(/PValue/g, "");
        demographic = demographic.replace(/[0-9]+/g, "");
        var FO_id = "";
        if (demographic == "FunctionalOrganizationType") {
            FO_id = this.name.replace(/[A-Za-z]+/g, "");
            elements_count = $('#' + demographic + 'ElementsCount' + FO_id).val();
        }
        else
            elements_count = $('#' + demographic + 'ElementsCount').val();
        var id_compare = $('#' + demographic + 'CompareDDL' + FO_id).val();
        var pVal = $('input[name=' + this.name + ']:checked').val();
        var id_question = $('#' + demographic + 'GroupByQuestionsDDL').val();
        var id_category = $('#' + demographic + 'GroupByCategoriesDDL').val();
        var id_questionnaire = $('#' + demographic + 'GroupByQuestionnairesDDL').val();
        //elements_count = $('#' + demographic).val();
        var MyCondition = "";

        if (elements_count <= 10) {
            MyCondition = false;
        } else {
            MyCondition = true;
        }

        UpdateTables(demographic, id_questionnaire, id_category, id_question, MyCondition, pVal, FO_id, id_compare);
        //        UpdatePrintLink("pValue", pVal, demographic + FO_id);
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

    jQuery("#myTabs").delegate(".button", "click", function () {
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
                    if (elementsCount <= 10) {
                        ChangeChart(demographic, FO_id, test_id, id_questionnaire, id_category, id_question, id_compare);
                    }
                    else {
                        $("#h4Title" + demographic + FO_id).text(title);
                    }
                }
            });
        }
    });

});

