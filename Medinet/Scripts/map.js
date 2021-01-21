$(document).ready(function () {
    var c_id = null;
    var GlobalTest = null;
    if ($('#country').val() != "0")
        c_id = $('#country').val();
    loadMap("World", $('#UrlMap').val(), c_id, $('#test').val());
    $.ajax({
        beforeSend: function (data) {
            $("#click").hide();
            $("body").css("cursor", "progress");
        },
        success: function (html) {
            $("#click").show();
            $("body").css("cursor", "auto");
        },
        type: 'GET'
    });
});
function loadMap(country_name, urlMap, country_id, test_id) {
    var tests = new Array();
    var countries = new Array();
    var codes = new Array();
    $.post("/Home/GetInfoForMap", { test_id: test_id, country_id: country_id }, function (info) {
        for (var i = 0; i < info.length; i++) {
            tests[i] = info[i].test;
            codes[i] = info[i].code;
            countries[i] = new Array(info[i].code, info[i].name, info[i].climate, info[i].color, info[i].map, info[i].id);
        }
        getMap(country_name, urlMap, country_id, test_id, countries, codes, tests);
    });
}

function getMap(country_name, urlMap, country_id, test_id, countries, codes, tests) {
    $('#map').vectorMap({
        map: urlMap,
        onLabelShow: function (event, label, code) {
            var pos = jQuery.inArray(code, codes);
            if (pos != -1) {
                label.text(countries[pos][1] + ": " + countries[pos][2]);
            }
        },
        onRegionClick: function (event, code) {
            var pos = jQuery.inArray(code, codes);
            if (pos != -1) {
                loadDialog(countries[pos][1], countries[pos][1] + $("#DialogText").val() + countries[pos][2] + ". ", countries[pos][4], countries[pos][5], test_id, tests[pos]);
            }
        },
        color: '#cccccc',
        backgroundColor: false,
        hoverOpacity: 0.7,
        hoverColor: false
    });
    if (codes[0] != '-') {
        $('#map').vectorMap('set', 'colors', changeColors(codes, countries));
    }
}

function loadDialog(country_name, text, map, country_id, test_id, test) {
    if (map != "-") {
        var t = text;
        t += $("#DialogQuestion").val();
        $("#Text").html(t);
        getDialog(country_name, map, country_id, test_id, test);
        $("#Dialog").dialog("open");
    }
}

function getDialog(country_name, urlMap, country_id, test_id, test) {
    var rol = $("#Rol").val();
    var lang = { createTest: $("#CreateTest").val(), showMap: $("#ShowMap").val(), viewReport: $("#ViewReport").val(), close: $("#Close").val() };
    var buttonsD = {};
    if (rol != "CompanyManager") {
        var aux;
        if (rol == "HRAdministrator")
            aux = "?test_id=" + $("#test").val();
        else
            aux = "";
        buttonsD[lang.createTest] = function () {
            $(this).dialog('close');
            window.location.href = '/Tests/Create' + aux;
        };
    }
    if (urlMap!="-") {
        buttonsD[lang.showMap] = function () {
            $(this).dialog('close');
            loadMap(country_name, urlMap, country_id, test_id);
            window.location.href = '/Home/ClimateStateMap?country_id=' + country_id + "&test_id=" + $("#test").val();
        };
    }
//    buttonsD[lang.viewReport] = function () {
//        var auxCountry = "";
//        var auxState = "";
//        if ($('#country').val() == "0") {
//            auxCountry = "&country_id=" + country_id;
//        }
//        else {
//            auxCountry = "&country_id=" + $('#country').val();
//            auxState = "&state_id=" + country_id;
//        }
//        $(this).dialog('close');
//        window.location.href = '/ChartReports/AnalyticalReport?test_id=' + test + auxCountry + auxState;
//    };
    buttonsD[lang.close] = function () {
        $(this).dialog('close');
    };

    $('#Dialog').dialog({
        autoOpen: false,
        show: "highlight",
        hide: "highlight",
        modal: true,
        buttons: buttonsD
    });

    $("#Report").click(function () {
        var hr = $("#Rol").val();
        if ($("#Rol").val() != "HRAdministrator")
            window.location.href = '/Home/AnalyticalReport?test_id=';
        else
            window.location.href = '/ChartReports/AnalyticalReport?test_id=';
    });
}
function changeColors(codes, countries) {
    var newColors = {};
    var count = codes.length;
    for (var pos = 0; pos < count; pos++) {
        var code = codes[pos];
        var country_color = countries[pos][3];
        newColors[code] = country_color;
    }
    return newColors;
}
