$(document).ready(function () {
    var InternalTime = 0;
    var GeneralCountryTime = 0;
    var RegionTime = 0;
    var AgeTime = 0;
    var CountryTime = 0;
    var InstructionTime = 0;
    var LocationTime = 0;
    var PositionLevelsTime = 0;
    var SeniorityTime = 0;
    var GenderTime = 0;
    var PerformanceTime = 0;
    var CustomerTime = 0;
    var FOTime;
    var questionnaire = $("#questionnaire").val();
    var company = null;

    function InitializeDataTable() {
        $('.tabla').dataTable({
            "bPaginate": false,
            "bFilter": false,
            "bInfo": false,
            "bJQueryUI": true,
            "bRetrieve": true,
            "bSort": false,
            "oLanguage": {
                "sZeroRecords": " "
            }
        });
    }

    function createFOTime() {
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

    $(function () {
        $(".myTabs").tabs();
        InitializeDataTable();
        $("RankingGeneral").hide();
    });

    $(".all").bind("click", handle_tab_click);
    function handle_tab_click(event) {
        //        $.ajax({
        //            beforeSend: function (data) {
        //                $("body").css("cursor", "progress");
        //            },
        //            success: function (html) {
        //                $("body").css("cursor", "auto");
        //            },
        //            type: 'POST'
        //        });
        var demographic = $(event.target).attr("href").replace("#tab", "");
        if (demographic == "Internal" && InternalTime == 0) {
            $.post("/ChartReports/LoadRankingTab", { questionnaire_id: questionnaire, demographic: "Internal", FO_id: null, company_id: company }, function (htmlText) {
                $('#tabInternal').html(htmlText);
                InitializeDataTable();
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
        }
        if (demographic == "Region" && RegionTime == 0) {
            $.post("/ChartReports/LoadRankingTab", { questionnaire_id: questionnaire, demographic: "Region", FO_id: null, company_id: company }, function (htmlText) {
                $('#tabRegion').html(htmlText);
                InitializeDataTable();
                RegionTime++;
            });
        }
        else if (demographic == "Country" && CountryTime == 0) {
            $.post("/ChartReports/LoadRankingTab", { questionnaire_id: questionnaire, demographic: "Country", FO_id: null, company_id: company }, function (htmlText) {
                $('#tabCountry').html(htmlText);
                InitializeDataTable();
                CountryTime++;
            });
        }
        else if (demographic == "AgeRange" && AgeTime == 0) {
            $.post("/ChartReports/LoadRankingTab", { questionnaire_id: questionnaire, demographic: "AgeRange", FO_id: null, company_id: company }, function (htmlText) {
                $('#tabAgeRange').html(htmlText);
                InitializeDataTable();
                AgeTime++;
            });
        }

        else if (demographic == "InstructionLevel" && InstructionTime == 0) {
            $.post("/ChartReports/LoadRankingTab", { questionnaire_id: questionnaire, demographic: "InstructionLevel", FO_id: null, company_id: company }, function (htmlText) {
                $('#tabInstructionLevel').html(htmlText);
                InitializeDataTable();
                InstructionTime++;
            });
        }
        else if (demographic == "Location" && LocationTime == 0) {
            $.post("/ChartReports/LoadRankingTab", { questionnaire_id: questionnaire, demographic: "Location", FO_id: null, company_id: company }, function (htmlText) {
                $('#tabLocation').html(htmlText);
                InitializeDataTable();
                LocationTime++;
            });
        }
        else if (demographic == "PositionLevel" && PositionLevelsTime == 0) {
            $.post("/ChartReports/LoadRankingTab", { questionnaire_id: questionnaire, demographic: "PositionLevel", FO_id: null, company_id: company }, function (htmlText) {
                $('#tabPositionLevel').html(htmlText);
                InitializeDataTable();
                PositionLevelsTime++;
            });
        }
        else if (demographic == "Seniority" && SeniorityTime == 0) {
            $.post("/ChartReports/LoadRankingTab", { questionnaire_id: questionnaire, demographic: "Seniority", FO_id: null, company_id: company }, function (htmlText) {
                $('#tabSeniority').html(htmlText);
                InitializeDataTable();
                SeniorityTime++;
            });
        }

        else if (demographic == "Performance" && PerformanceTime == 0) {
            $.post("/ChartReports/LoadRankingTab", { questionnaire_id: questionnaire, demographic: "Performance", FO_id: null, company_id: company }, function (htmlText) {
                $('#tabPerformance').html(htmlText);
                InitializeDataTable();
                PerformanceTime++;
            });
        }

        else if (demographic == "Customer" && CustomerTime == 0) {
            $.post("/ChartReports/LoadRankingTab", { questionnaire_id: questionnaire, demographic: "Customer", FO_id: null, company_id: company }, function (htmlText) {
                $('#tabCustomer').html(htmlText);
                InitializeDataTable();
                CustomerTime++;
                company = null;
            });
        }

        else if (demographic == "GeneralCountry" && GeneralCountryTime == 0) {
            $.post("/ChartReports/LoadRankingTab", { questionnaire_id: questionnaire, demographic: "GeneralCountry", FO_id: null, company_id: company }, function (htmlText) {
                $('#tabGeneralCountry').html(htmlText);
                InitializeDataTable();
                GeneralCountryTime++;
                company = null;
            });
        }

        else if (demographic.charAt(2) == '-') {
            var pos;
            var FO_id = $(event.target).attr("href").replace("#tabFO-", "");
            for (pos = 0; pos < FOTime.length; pos++) {
                if (FOTime[pos][0] == FO_id) {
                    if (FOTime[pos][1] == 0) {
                        $.post("/ChartReports/LoadRankingTab", { questionnaire_id: questionnaire, demographic: "FunctionalOrganizationType", FO_id: FO_id, company_id: company }, function (htmlText) {
                            $('#tabFO-' + FO_id).html(htmlText);
                            InitializeDataTable();
                        });
                        FOTime[pos][1]++;
                    }
                    break;
                }
            }
        }

    }

    function InitializeCompanyDDL() {
        jQuery("#tabInternal").delegate(".Companies", "change", function () {
            var company_id = $(this).val();
            if (company_id) {
                company = company_id;
                hideDivSelect("Internal");
                LoadTabs(company_id);
                //UpdatePrintLink("company_id", company_id, "Internal");
            }
            else {
                company = null;
                $('#DemographicsDiv').html("");
                hideFieldSet("Internal");
                showDivSelect("Internal");
                //UpdatePrintLink("company_id", 0, "Internal");
            }
        });
    }

    function InitializeTabs() {
        $("#myInternalTabs").tabs();
        InitializeDataTable();
        $(".all").bind("click", handle_tab_click);
        FOTime = createFOTime();
        showFieldset("Internal");
    }

    function DeleteRows(tableId) {
        $("#" + tableId).find("tr:gt(0)").remove(); //.fnClearTable(); //.find("tr:gt(0)").remove();
    }

    function AddRows(tableId, html) {
        $("#" + tableId + " tr:last").after(html);
    }

    function LoadTabs(company_id) {
        $.post("/ChartReports/LoadCompanyTabs", { questionnaire: questionnaire, company: company_id }, function (htmlText) {
            $('#DemographicsDiv').html(htmlText);
            InitializeTabs();
        });
    }

    function UpdateTable(sector_id, country_id, company_id, fot, demographic) {
        var pos = 0;
        $.post("/ChartReports/UpdateRanking", { questionnaire_id: questionnaire, sector_id: sector_id, country_id: country_id, company_id: company_id, fot: fot, demographic: demographic }, function (companyDouble) {
            if (companyDouble.length > 0) {
                DeleteRows("Ranking" + demographic + fot);
                for (var i = 0; i < companyDouble.length; i++) {
                    var companyShow = "";
                    if (companyDouble[i].show)
                        companyShow = companyDouble[i].companyName;
                    pos++;
                    var html = "<tr><td align='left'>" + companyShow + "</td><td style='text-align: center;'>" + pos + "</td><td style='text-align: center;'>" + companyDouble[i].companyClimate + "%</td></tr>";
                    AddRows("Ranking" + demographic + fot, html); //[companyShow, pos, companyDouble[i].companyClimate + "%"]);
                }
            }
            if (pos > 0) {
                hideDiv(demographic + fot);
                showTable(demographic + fot);
                showFieldset(demographic + fot);
            }
            else {
                hideTable(demographic + fot);
                showDiv(demographic + fot);
                hideFieldSet(demographic + fot);
            }
        });
    }

    //-----------------------------------------------------------------//

    function UpdatePrintLink(change, value, demographic) {
        var currentValue = $('#PrintLink' + demographic).attr('href');
        var newValue;
        $.post("/ChartReports/UpdateRankingLink", { currentValue: currentValue, changeType: change, newValue: value }, function (newLink) {
            newValue = newLink;
            $('#PrintLink' + demographic).attr('href', newValue);
        });
    }

    $("#tabExternal").delegate(".PrintPdf", "click", function () {
        PrintPdfFunction(this);
    });

    function PrintPdfFunction(field) {
        var demographic = field.id.replace(/PrintPdf/g, "");
        var fot = demographic.replace(/[A-Za-z]+/g, "");
        var demographic = demographic.replace(/[0-9]+/g, "");
        var rol = $('#Rol').val();
        var sector;
        if (rol == "HRAdministrator" && (demographic == "General" || demographic == "Customer" || demographic == "GeneralCountry"))
            sector = $('#Sectors' + demographic).val();
        else
            sector = 1;
        var country;
        if (demographic == "GeneralCountry" || demographic == "External")
            country = $('#CountriesGeneralCountry').val();
        else
            country = 1;
        var company;
        if (rol == "HRAdministrator" && demographic == "Internal")
            company = $('#CompaniesGeneral').val();
        else
            company = 1;
        if (ValidateValuesForPrint(sector, country, company, demographic))
            window.location.href = '/ChartReports/PdfRanking?questionnaire_id=' + questionnaire + '&sector_id=' + $('#Sectors' + demographic).val()
            + '&country_id=' + $('#CountriesGeneralCountry').val() + '&company_id=' + $('#CompaniesGeneral').val()
            + '&demographic=' + demographic + '&fot=' + fot;
        else
            $("#Dialog").dialog("open");
    };

    function ValidateValuesForPrint(sector, country, company, demographic) {
        if (demographic == "External") {
            if (country != 0 && country != "" && $("#SectorsGeneral").val() != 0 && $("#SectorsGeneral").val() != ""
            && $("#SectorsCountryGeneral").val() != 0 && $("#SectorsCountryGeneral").val() != "") {
                if ($("#Rol").val() == "HRAdministrator" && $("#SectorsCustomer").val() != 0 && $("#SectorsCustomer").val() != "")
                    return true;
            }
            else {
                $("#DialogText").html($("#SelectSectorAndCountry").val());
                return false;
            }
        } else if (demographic == "Internal") {
            if (company != 0 && company != "")
                return true;
            else {
                $("#DialogText").html($("#SelectCountry").val());
                return false;
            }
        } else if (demographic == "General" || demographic == "Customer") {
            if (sector != 0 && sector != "")
                return true;
            else {
                $("#DialogText").html($("#SelectSector").val());
                return false;
            }
        } else if (demographic == "GeneralCountry") {
            if (sector != 0 && sector != "" && country != 0 && country != "")
                return true;
            else {
                $("#DialogText").html($("#SelectSectorAndCountry").val());
                return false;
            }
        } else {
            return true;
        }
    }

    jQuery("#myExternalTabs").delegate(".Sectors", "change", function () {
        var demographic = this.name.replace(/Sectors/g, "");
        var cou;
        if (demographic == "GeneralCountry")
            cou = $('#Countries' + demographic).val();
        else
            cou = null;
        if (validate($(this).val(), cou, demographic)) {
            var sector_id = $(this).val();
            var country_id = cou;
            hideDivSelect(demographic);
            UpdateTable(sector_id, country_id, null, "", demographic);
            //            UpdatePrintLink("sector_id", sector_id, demographic);
        }
        else {
            DivSelect($(this).val(), cou, null, demographic);
            //            UpdatePrintLink("sector_id", 0, demographic);
        }
    });

    jQuery("#myExternalTabs").delegate(".Countries", "change", function () {
        var demographic = this.name.replace(/Countries/g, "");
        // UpdatePrintLink("questionnaire_id", $(this).val());
        var sec;
        if ($('#Rol').val() == "HRAdministrator")
            sec = $('#Sectors' + demographic).val();
        else
            sec = 1;
        if (validate(sec, $(this).val(), demographic)) {
            var country_id = $(this).val();
            var sector_id = sec;
            hideDivSelect(demographic);
            UpdateTable(sector_id, country_id, null, "", demographic);
            //            UpdatePrintLink("country_id", country_id, demographic);
        }
        else {
            DivSelect(sec, $(this).val(), null, demographic);
            //            UpdatePrintLink("country_id", 0, demographic);
        }

    });


    function DivSelect(sector, country, company, demographic) {
        if (!ValidateValuesForRanking(sector, country, company, demographic)) {
            hideTable(demographic);
            hideDiv(demographic);
            showDivSelect(demographic);
        }
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

    function showTable(demographic) {
        $('#Ranking' + demographic).show();
        InitializeDataTable();
    }

    function hideTable(demographic) {
        $('#Ranking' + demographic).hide();
    }

    function showDiv(demographic) {
        $('#NoCompanies' + demographic).show();
    }

    function hideDiv(demographic) {
        $('#NoCompanies' + demographic).hide();
    }

    function showDivSelect(demographic) {
        $('#DivSelect' + demographic).show();
    }

    function hideDivSelect(demographic) {
        $('#DivSelect' + demographic).hide();
    }

    function showFieldset(demographic) {
        $('#Fieldset' + demographic).show();
    }

    function hideFieldSet(demographic) {
        $('#Fieldset' + demographic).hide();
    }

    $('#Dialog').dialog({
        autoOpen: false,
        show: "highlight",
        hide: "highlight",
        modal: true,
        buttons: {
            Ok: function () {
                $(this).dialog("close");
            }
        }
    });

});