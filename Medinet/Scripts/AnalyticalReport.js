google.charts.load('current', { 'packages': ['corechart'] });

$(document).ajaxStart(function () {
    $("#loading").show();
});

$(document).ajaxComplete(function () {
    $("#loading").hide();
});

var print;
var perc_size;

$(document).ready(function () {
    print = $("#Print").val() == "1";
    perc_size = print ? 0.8 : 1;
    $('.tabla').dataTable({
        "bPaginate": false,
        "bFilter": false,
        "bInfo": false,
        "bJQueryUI": true,
        "bRetrieve": true,
        "bSort": false
    });
    google.setOnLoadCallback(drawCharts);

    function drawCharts() {
        var test_id = $("#test").val();
        var options = {
            title: "",
            legend: 'none',
            is3D: true,
            width: 600 * perc_size,
            height: 400 * perc_size,
            top: 0,
            bar: { groupWidth: 75 * perc_size },
            hAxis: { textStyle: { fontSize: 14 * perc_size } },
            chartArea: { 'width': '100%', 'height': '85%', 'top': '5%' },
            annotations: {
                alwaysOutside: false,
                textStyle: {
                    fontSize: 10,
                    bold: true,
                    color: '#fff',
                    auraColor: 'none'
                }
            }
        };
        $.post("/ChartReports/GetPercentageChart", { test_id: test_id, demographic: "Gender", FO_id: null }, function (info) {
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
            options['chartArea'] = { 'width': '90%', 'height': '90%' };
            options['colors'] = gender_m_first ? ["#00BFFF", "#FF69B4"] : ["#FF69B4", "#00BFFF"];
            options['is3D'] = true;
            var chart_div = document.getElementById("GenderChart");
            var chart = new google.visualization.PieChart(chart_div);
            google.visualization.events.addListener(chart, 'ready', function () {
                chart_div.innerHTML = '<img src="' + chart.getImageURI() + '">';
            });
            chart.draw(data, options);
        });
        $.post("/ChartReports/GetUnivariateChart", {
            test_id: test_id, demographic: "General", FO_id: null,
            questionnaire_id: null, category_id: null, question_id: null, compare: null
        }, function (info) {
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
            options['legend'] = 'none';
            options['title'] = info[2];
            options['vAxis'] = { minValue: 0, maxValue: info[3] };
            var chart_div = document.getElementById("GeneralChart");
            var chart = new google.visualization.ColumnChart(chart_div);
            google.visualization.events.addListener(chart, 'ready', function () {
                chart_div.innerHTML = '<img src="' + chart.getImageURI() + '">';
            });
            chart.draw(data, options);
        });
        $.post("/ChartReports/GetUnivariateChart", {
            test_id: test_id, demographic: "Location", FO_id: null,
            questionnaire_id: null, category_id: null, question_id: null, compare: null
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
                setWidths(info, options, true);
                options['legend'] = 'none';
                options['title'] = info[2];
                options['vAxis'] = { minValue: 0, maxValue: info[3] };
                var chart_div = document.getElementById("LocationChart");
                var chart = new google.visualization.ColumnChart(chart_div);
                google.visualization.events.addListener(chart, 'ready', function () {
                    chart_div.innerHTML = '<img src="' + chart.getImageURI() + '">';
                });
                chart.draw(data, options);
            }
        });
        $.post("/ChartReports/GetUnivariateChart", {
            test_id: test_id, demographic: "FunctionalOrganizationType", FO_id: -1,
            questionnaire_id: null, category_id: null, question_id: null, compare: null
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
                setWidths(info, options, true);
                options['legend'] = 'none';
                options['title'] = info[2];
                options['vAxis'] = { minValue: 0, maxValue: info[3] };
                var chart_div = document.getElementById("FOTypeChart");
                var chart = new google.visualization.ColumnChart(chart_div);
                google.visualization.events.addListener(chart, 'ready', function () {
                    chart_div.innerHTML = '<img src="' + chart.getImageURI() + '">';
                });
                chart.draw(data, options);
            }
        });
        $.post("/ChartReports/GetUnivariateChart", {
            test_id: test_id, demographic: "AgeRange", FO_id: null,
            questionnaire_id: null, category_id: null, question_id: null, compare: null
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
                options['legend'] = 'none';
                options['title'] = info[2];
                options['vAxis'] = { minValue: 0, maxValue: info[3] };
                var chart_div = document.getElementById("AgeRangeChart");
                var chart = new google.visualization.ColumnChart(chart_div);
                google.visualization.events.addListener(chart, 'ready', function () {
                    chart_div.innerHTML = '<img src="' + chart.getImageURI() + '">';
                });
                chart.draw(data, options);
            }
        });
        $.post("/ChartReports/GetCategoryChart", {
            test_id: test_id, demographic: "General", FO_id: null,
            questionnaire_id: null, id: null, compare: null
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
                setWidths(info, options, true);
                options['legend'] = 'none';
                options['title'] = info[2];
                options['vAxis'] = { minValue: 0, maxValue: info[3] };
                var chart_div = document.getElementById("CategoryChart");
                var chart = new google.visualization.ColumnChart(chart_div);
                google.visualization.events.addListener(chart, 'ready', function () {
                    chart_div.innerHTML = '<img src="' + chart.getImageURI() + '">';
                });
                chart.draw(data, options);
            }
        });

        function setWidths(info, options, long_legends) {
            options['width'] = (info[1].length <= 4 ? 600 : (info[1].length <= 7 ? 800 : 1000)) * perc_size;
            if (long_legends) {
                options['height'] = 450 * perc_size;
                options['chartArea']['height'] = '70%';
            }
        }
    }

    var specialElementHandlers = {
        '#editor': function (element, renderer) {
            return true;
        }
    };

    function getPDF(pdfFileName, div) {

        var HTML_Width = div.width();
        var HTML_Height = div.height();
        var top_left_margin = 15;
        var PDF_Width = HTML_Width + (top_left_margin * 2);
        var PDF_Height = (PDF_Width * 1.5) + (top_left_margin * 2);
        var canvas_image_width = HTML_Width;
        var canvas_image_height = HTML_Height;

        var totalPDFPages = Math.ceil(HTML_Height / PDF_Height) - 1;


        html2canvas(div[0], { allowTaint: true }).then(function (canvas) {
            canvas.getContext('2d');

            console.log(canvas.height + "  " + canvas.width);


            var imgData = canvas.toDataURL("image/jpeg", 1.0);
            var pdf = new jsPDF('p', 'pt', [PDF_Width, PDF_Height]);
            pdf.addImage(imgData, 'JPG', top_left_margin, top_left_margin, canvas_image_width, canvas_image_height);


            for (var i = 1; i <= totalPDFPages; i++) {
                pdf.addPage(PDF_Width, PDF_Height);
                pdf.addImage(imgData, 'JPG', top_left_margin, -(PDF_Height * i) + (top_left_margin * 4), canvas_image_width, canvas_image_height);
            }

            pdf.save(pdfFileName+".pdf");
        });
    };

    function generatePDF() {
        pdf = "";
        //$("#downloadbtn").hide();
        //$("#genmsg").show();
        html2canvas($(".print-wrap:eq(0)")[0], { allowTaint: true }).then(function (canvas) {

            calculatePDF_height_width(".print-wrap", 0);
            var imgData = canvas.toDataURL("image/png", 1.0);
            pdf = new jsPDF('p', 'pt', [PDF_Width, PDF_Height]);
            pdf.addImage(imgData, 'JPG', top_left_margin, top_left_margin, HTML_Width, HTML_Height);

        });

        html2canvas($(".print-wrap:eq(1)")[0], { allowTaint: true }).then(function (canvas) {

            calculatePDF_height_width(".print-wrap", 1);

            var imgData = canvas.toDataURL("image/png", 1.0);
            pdf.addPage(PDF_Width, PDF_Height);
            pdf.addImage(imgData, 'JPG', top_left_margin, top_left_margin, HTML_Width, HTML_Height);

        });

        html2canvas($(".print-wrap:eq(2)")[0], { allowTaint: true }).then(function (canvas) {

            calculatePDF_height_width(".print-wrap", 2);

            var imgData = canvas.toDataURL("image/png", 1.0);
            pdf.addPage(PDF_Width, PDF_Height);
            pdf.addImage(imgData, 'JPG', top_left_margin, top_left_margin, HTML_Width, HTML_Height);



            //console.log((page_section.length-1)+"==="+index);
            setTimeout(function () {

                //Save PDF Doc	
                pdf.save("HTML-Document.pdf");

                //Generate BLOB object
                var blob = pdf.output("blob");

                //Getting URL of blob object
                var blobURL = URL.createObjectURL(blob);

                //Showing PDF generated in iFrame element
                var iframe = document.getElementById('sample-pdf');
                iframe.src = blobURL;

                //Setting download link
                var downloadLink = document.getElementById('pdf-download-link');
                downloadLink.href = blobURL;

                $("#sample-pdf").slideDown();


                //$("#downloadbtn").show();
                //$("#genmsg").hide();
            }, 0);
        });
    };

    function calculatePDF_height_width(selector, index) {
        page_section = $(selector).eq(index);
        HTML_Width = page_section.width();
        HTML_Height = page_section.height();
        top_left_margin = 15;
        PDF_Width = HTML_Width + (top_left_margin * 2);
        PDF_Height = (PDF_Width * 1.2) + (top_left_margin * 2);
        canvas_image_width = HTML_Width;
        canvas_image_height = HTML_Height;
    }

    $('#PrintPdf').click(function (e) {
        e.preventDefault();
        var pdfFileName = $('#PdfName').val();
        var div = $('#contenido-sistema');



        //------------------------------------------------------------//

        //getPDF(pdfFileName, div);

        //------------------------------------------------------------//

        //const options = {
        //     'jsPDF': {
        //         'orientation': 'p',
        //         'unit': 'mm',
        //         'format': 'letter',
        //     }
        // };
        //const jsPdfWrapper = new JsPdfWrapper(options);
        //const targetElement = $('#contenido-sistema')[0];
        //jsPdfWrapper.DownloadFromHTML(targetElement, pdfFileName);

        //-----------------------------------------------------------//

        //var pdf = new jsPDF('p', 'pt', 'letter');
        //pdf.fromHTML($('#contenido-sistema').html(), 15, 15, {
        //    'width': 170,
        //    'elementHandlers': specialElementHandlers
        //});
        //pdf.html(document.body, {
        //    callback: function (pdf) {
        //        window.open(output('bloburl'));
        //    }
        //});
        //pdf.addHTML($('#contenido-sistema')[0], function () {
        //    pdf.save(pdf_name);
        //});
        //pdf.fromHTML($('#contenido-sistema').html(), 15, 15, {
        //    'width': 800,
        //    'elementHandlers': specialElementHandlers
        //});
        //pdf.save(pdf_name);
        return false;
    });
});