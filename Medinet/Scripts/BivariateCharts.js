google.charts.load('current', { 'packages': ['corechart'] });
var downloadImg = "";

$(document).ready(function () {
    downloadImg = $('#DownloadImage').val();
    $("#Table").attr("cellpadding", 10);
    $("#SmallTable").attr("cellpadding", 10);
    var dem1 = 0;
    var demographicsHtml = $("#Demographics2").html();
    var company_id = $("#company_id").val(); //estos son campos ocultos que se crearon en la vista
    var test_id = $("#test_id").val(); //estos son campos ocultos que se crearon en la vista
    var demographic1, demographic2;
    $('#Demographics1').change(function () {
        dem1 = $(this).val();
        if (dem1) {
            updateDemograhicsList2();
            hideChart();
            hideTable("SmallTable");
            hideTable("Table");
        }
        else {
            $("#Demographics2").html(demographicsHtml);
            hideChart();
            hideTable("SmallTable");
            hideTable("Table");
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

    $('#Demographics2').change(function () {
        var val = $(this).val();
        if (val) {
            GetTableOrImage();
            $("#Demographics1").html(demographicsHtml);
            $("#Demographics1").val(dem1);
            $("#Demographics1 option[value='" + val + "']").remove();
        }
        else {
            hideChart();
            hideTable("SmallTable");
            hideTable("Table");
        }
    });

    function GetTableOrImage() {
        var test_id = $("#test_id").val();
        var demographic1 = $("#Demographics1").val();
        var demographic2 = $("#Demographics2").val();
        $.post("/ChartReports/GetIsTable", { test_id: test_id, demo1: demographic1, demo2: demographic2 },
        function (t) {
            if (t == "True") {
                updateDataTable(test_id, demographic1, demographic2, "Table"); //busca la tabla
            }
            else {
                updateSourceImage(test_id, demographic1, demographic2); //busca la imagen
                updateDataTable(test_id, demographic1, demographic2, "SmallTable");
            }
        });
    }

    function InitializeDataTable() {
        $('.tabla').dataTable({
            "bPaginate": false,
            "bFilter": false,
            "bInfo": false,
            "bJQueryUI": true,
            "bRetrieve": true,
            "bSort": true
        });
    }

    function updateSourceImage(test_id, demographic1, demographic2) {
        $("body").addClass("loading2");
        fn1(function () {
            fn2();
        });

    }

    function fn1(callback) {
        var test_id = $("#test_id").val();
        var demographic1 = $("#Demographics1").val();
        var demographic2 = $("#Demographics2").val();
        //document.getElementById("Chart").src = "/ChartReports/SelectBivariateGraphic?test_id=" + test_id + "&demographic_1=" + demographic1 + "&demographic_2=" + demographic2;

        google.setOnLoadCallback(function () { GetChart(test_id, demographic1, demographic2, callback) });
    }

    function GetChart(test_id, demographic1, demographic2, callback) {
        var colors = ["#00a0e3", "#ffce00", "#00b386", "#ff044d", "#664147", "#084c61", "#3c1642",
            "#1a181b", "#b4656f", "#f9b9f2", "#102542", "#2e0219", "#f8333c", "#a62639",
            "#065143", "#d9dbf1", "#af2bbf", "#841c26", "#aaaaaa", "#d30c7b", "#01295f",
            "#fd151b", "#FF69B4", "#00BFFF"];
        var options = {
            title: "",
            is3D: true,
            height: 450,
            colors: colors,
            chartArea: {
                left: 30,
                width: '70%',
                height: '80%'
            },
            bar: { groupWidth: "85%" },
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

                    var barGroupWidth = ((info[1][0].length - 1) / 4) * 35;
                    var chartAreaWidth = info[1].length * barGroupWidth;
                    var chartWidth = chartAreaWidth + 350;
                    options['width'] = chartWidth > 1200 ? chartWidth : 1200;

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
                    //var chart = new google.visualization.ColumnChart(document.getElementById("Chart"));
                    //chart.draw(data, options);

                    var chart_div = document.getElementById('Chart');
                    var chart = new google.visualization.ColumnChart(chart_div);
                    var img = document.getElementById("Img");
                    google.visualization.events.addListener(chart, 'ready', function () {
                        img.innerHTML = '<a href="' + chart.getImageURI() + '" download>' + downloadImg + '</a>';
                    });
                    chart.draw(data, options);

                    //$("#Chart").on('load', function () {
                    //    callback();
                    //    return;
                    //});
                    fn2();
                }
                else {
                    $("#noMinimum").show();
                    $("body").removeClass("loading2");
                }
        });
    }

    function fn2() {
        hideTable("Table");
        showChart();
        $("body").removeClass("loading2");
    }

    function updateDataTable(test_id, demographic1, demographic2, table_id) {
        DeleteRows(table_id);
        $.post("/ChartReports/SelectBivariateTable", { test_id: test_id, demographic_1: demographic1, demographic_2: demographic2 }, function (table) {
            if (table[3] > 0) {
                $("#Title").val(table[6]); //este es el titulo
                var demoName1 = table[0]; //.name;nombre de mi primer demográfico
                var demoCount1 = table[1]; //.count;cantidad de ese primer demográfico
                var demoName2 = table[2]; //.name;nombre de mi segundo demográfico
                var demoCount2 = table[3]; //.count;cantidad de ese segundo demográfico
                var optionsCount = table[5]; //cantidad de opciones
                var is100Base = table[8]; //cantidad de opciones
                var stringObject = new Array();
                stringObject = table[4]; //table[4] es el diccionario
                var demos2List = new Array();
                demos2List = table[9];
                var first = true;
                //cargar primera linea del encabezado
                var html = "<th rowspan='2' colspan='2' padding='10px'></th><th padding='10px' colspan='" + demoCount2 + "' style='border-style:solid; border-width:2px'>" + demoName2 + "</th>";
                AddRows(html, true, table_id);
                html = ""; //vacia el html
                $.each(demos2List, function () {//key=names2,value=value del tdObject que contiene ejemplo,key=femenino,value=4,5
                    html = html + "<th padding='10px' style='border-style:solid; border-width:1px; text-align:right'>" + this + "</th>";
                });
                AddRows(html, false, table_id); //añade filas y dice false porque ya no estoy en la primera fila
                var cab = new Array(); var c = 0;
                $.each(stringObject, function (demo1, trObject) {//key=demo1(variable temporal),value=trObject(variable temporal)
                    var stringDemo1 = demo1;
                    var tdObject = new Array();
                    tdObject = trObject; //stringObject[demo1];
                    //cargar segunda linea de la cabecera
                    //html = ""; //vacia el html
                    //                if (first) {//quiere decir que la cabecera aun esta en true
                    //                    $.each(tdObject, function (names2, value) {//key=names2,value=value del tdObject que contiene ejemplo,key=femenino,value=4,5
                    //                        html = html + "<th padding='10px' style='border-style:solid; border-width:1px; text-align:right'>" + names2 + "</th>";
                    //                        cab[c] = names2;
                    //                        c++;
                    //                    });
                    //                }
                    //AddRows(html, false, table_id); //añade filas y dice false porque ya no estoy en la primera fila
                    //cargar th de Demographics1
                    html = "";
                    if (first)//si sigo en la cabecera
                        html = "<th padding='10px' rowspan='" + demoCount1 + "' style='border-style:solid; border-width:2px; vertical-align:middle;'>" + demoName1 + "</th>";
                    //cargar demo de la izquierda
                    html = html + "<th padding='10px' style='border-style:solid; border-width:1px'>" + stringDemo1 + "</th>"; //aqui añade el primer pais,solo el nombre
                    //cargar row con promedios
                    //-------REVISAR AQUI-------//
                    //$.each(tdObject, function (key, value) {
                    $.each(demos2List, function () {//registros de la cabecera(cab) de mis demográfico 2, recorro la cabecera para CADA pais
                        var color = ''; //= GetColor(optionsCount, value);
                        //var pct = value * 100 / optionsCount;
                        var valLabel = tdObject[this] ? tdObject[this].toFixed(2) : 0;
                        var pct = '';
                        if (is100Base == "1")
                            pct = (Number(valLabel) * 100 / optionsCount).toFixed(2);
                        else
                            pct = valLabel;
                        //var pct = tdObject[this] ? tdObject[this].toFixed(2) : 0; //si tiene algo coloca eso mismo llevado a 2 decimales (tdObject[this].toFixed(2)

                        //if (pct > 80) color = 'green';
                        //else if (pct > 60) color = '#FF9900';
                        //else if (pct > 0) color = 'red';
                        //else color = 'black';
                        html = html + "<td padding='10px' style='text-align:right; border-style:solid; border-width:1px;'><span style='color:" + color + ";'>" + pct + "</span></td>"
                    });
                    AddRows(html, false, table_id);
                    if (first) {
                        first = false;
                    }
                });
                if (table_id == "Table")
                    hideChart();
                showTable(table_id);
            }
        });
    }

    function DeleteRows(table_id) {
        $("#" + table_id).empty();
    }

    function AddRows(html, first, table_id) {//si es first o el primero es porque es la cabecera
        var tr = "<tr>" + html + "</tr>";
        if (html != "") {
            if (first)
                $("#" + table_id).append(tr); //append busca la tabla que tengo vacia y le coloca ese tr que se le esta enviando, al comienzo
            else
                $("#" + table_id + " tr:last").after(tr); //despues del ultimo agrega el tr
        }
    }

    function showChart() {
        $('#DivChart').show();
    }

    function hideChart() {
        $('#DivChart').hide();
        document.getElementById("Chart").src = "";
    }

    function showTable(table_id) {
        $("#Fieldset" + table_id).show();
    }

    function hideTable(table_id) {
        $("#Fieldset" + table_id).hide();
    }

});
