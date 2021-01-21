<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>


<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    <%: ViewRes.Views.Shared.Shared.GenericTitle %> - Charts Test
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="JsContent" runat="server">
    <%--<script type="text/javascript" src="https://www.gstatic.com/charts/loader.js"></script>--%>
    <script type="text/javascript">

        //// Load the Visualization API and the corechart package.
        //google.charts.load('current', { 'packages': ['corechart'] });

        //// Set a callback to run when the Google Visualization API is loaded.
        //google.charts.setOnLoadCallback(drawCharts);

        //// Callback that creates and populates a data table,
        //// instantiates the pie chart, passes in the data and
        //// draws it.
        //function drawCharts() {
        //    //$.post("/Test/GetPieInfo", { test_id: 120, demographic: "AgeRange" }, function (info) {
        //    //$.post("/ChartReports/GetPercentageChart", { test_id: 120, demographic: "AgeRange" }, function (info) {
        //    //    var data = new google.visualization.DataTable();
        //    //    $.each(info[0], function (key, value) {
        //    //        data.addColumn(value, key);
        //    //    });
        //    //    $.each(info[1], function (key, value) {
        //    //        data.addRow([key, value]);
        //    //    });
        //    //    var options = info[2];
        //    //    var pie_chart = new google.visualization.PieChart(document.getElementById('pie_chart_div'));
        //    //    pie_chart.draw(data, options);
        //    //});

        //    $.post("/ChartReports/GetUnivariateChart", {
        //        test_id: 120, demographic: "General", FO_id: null,
        //        questionnaire_id: null, category_id: null, question_id: null, compare: null
        //    }, function (info) {
        //        var data = new google.visualization.DataTable();
        //        $.each(info[0], function (index) {
        //            data.addColumn(info[0][index][0], info[0][index][1]);
        //            if (index != 0) {
        //                data.addColumn({ type: 'string', role: 'style' });
        //                data.addColumn({ type: 'string', role: 'annotation' });
        //                data.addColumn({ type: 'boolean', role: 'certainty' });
        //            }
        //        });
        //        data.addRows(info[1]);
        //        options['title'] = info[2];
        //        options['vAxis'] = { minValue: 0, maxValue: info[3] };
        //        var chart_div = demographic + "ChartDiv" + (FO_id != null ? FO_id : "");
        //        var chart = new google.visualization.ColumnChart(document.getElementById(chart_div));
        //        chart.draw(data, options);
        //    });

        //    var options = {
        //        backgroundColor: '#eeeeee',
        //        is3D: true,
        //        width: 700,
        //        height: 400,
        //        vAxis: { minValue: 0, maxValue: 5 },
        //        colors: ["#00a0e3", "#ffce00", "#00b386", "#ff044d", "#664147", "#084c61", "#3c1642",
        //            "#1a181b", "#b4656f", "#f9b9f2", "#102542", "#2e0219", "#f8333c", "#a62639",
        //            "#065143", "#d9dbf1", "#af2bbf", "#841c26", "#aaaaaa", "#d30c7b", "#01295f",
        //            "#fd151b", "#FF69B4", "#00BFFF"],
        //        animation: {
        //            duration: 1000,
        //            easing: 'out',
        //            startup: true
        //        }
        //    };

        //    $.post("/ChartReports/GetComparativeChart", {
        //        test_id: 153, demographic: "Climate"
        //    }, function (info) {
        //        var data = new google.visualization.DataTable();
        //        $.each(info[0], function (index) {
        //            var key = info[0][index][0];
        //            var value = info[0][index][1];
        //            data.addColumn(info[0][index][0], info[0][index][1]);
        //        });
        //        data.addRows(info[1]);
        //        options['title'] = info[2];
        //        options['vAxis'] = { minValue: 0, maxValue: 5 };
        //        var bar_chart = new google.visualization.ColumnChart(document.getElementById('bar_chart_div'));
        //        bar_chart.draw(data, options);
        //    });
        //}

    </script>
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="MainContent" runat="server">
<%--    <div id="pie_chart_div" class="span-24"></div>
    <div id="bar_chart_div" class="column span-12"></div>--%>
    <div class="column prepend-2 span-10">
        <h1 class="rosado">POPULATION REPORTS</h1>
        <h1 class="azul">UNIVARIATE REPORTS</h1>
        <h1 class="azul">BIVARIATE REPORTS</h1>
        <h1 class="morado-azul">TEXT ANSWERS</h1>
        <h1 class="morado">FREQUENCY REPORTS</h1>
        <h1 class="morado">STATISTIC REPORTS</h1>
        <h1 class="morado-azul">CATEGORY REPORTS</h1>
        <h1 class="azul">EXECUTIVE REPORT</h1>
        <h1 class="rosado">RANKING</h1>
        <h1 class="rosado">COMPARATIVE REPORTS</h1>
        <h1 class="morado-azul">SATISFACTION TABLES</h1>
    </div>
    <div class="column span-10 append-2 last">
        <h1 class="rosado">REPORTES DE MUESTREO</h1>
        <h1 class="azul">REPORTES UNIVARIABLES</h1>
        <h1 class="azul">REPORTES DUALES</h1>
        <h1 class="morado-azul">RESPUESTAS DE TEXTO</h1>
        <h1 class="morado">REPORTES DE FRECUENCIA</h1>
        <h1 class="morado">REPORTES ESTADÍSTICOS</h1>
        <h1 class="morado-azul">REPORTES DE CATEGORÍA</h1>
        <h1 class="azul">INFORME EJECUTIVO</h1>
        <h1 class="rosado">CLASIFICACIÓN</h1>
        <h1 class="rosado">REPORTES COMPARATIVOS</h1>
        <h1 class="morado-azul">TABLAS DE SATISFACCIÓN</h1>
    </div>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="CssContent" runat="server">
    <style>
        h1 {
              font-size: 17.5px;
              font-weight: bold;
              padding: 0;
              letter-spacing: -1px;
              background-repeat: repeat;
              -webkit-background-clip: text;
              -webkit-text-fill-color: transparent; 
              -moz-background-clip: text;
              -moz-text-fill-color: transparent;
        }
        h1.azul {
              background-color: #01A0E4;
              background-image: linear-gradient(rgb(0, 114, 255), rgb(0, 198, 255));
        }
        h1.morado-azul {
              background-color: #01A0E4;
              background-image: linear-gradient(rgb(26, 109, 255), rgb(200, 34, 255));
        }
        h1.morado {
              background-color: #01A0E4;
              background-image: linear-gradient(rgb(99, 49, 218), rgb(121, 33, 221));
        }
        h1.rosado {
              background-color: #01A0E4;
              background-image: linear-gradient(rgb(234, 33, 106), rgb(255, 129, 119));
        }
    </style>
</asp:Content>
