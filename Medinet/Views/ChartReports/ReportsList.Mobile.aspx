<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/mobile/MobileMini.Master" Inherits="System.Web.Mvc.ViewPage<Medinet.Models.ViewModels.ChartReportViewModel>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    <%: ViewRes.Views.Shared.Shared.GenericTitle %> <%:ViewRes.Views.Report.ReportsList.Title %>
</asp:Content>

<asp:Content ID="Content7" ContentPlaceHolderID="PageId" runat="server">
<div data-role="page" id="mainReportList" class="basic">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <script type="text/javascript">

            $.validator.messages.required = "<%: ViewRes.Views.Evaluation.AnswerTest.Required %>";

            $(document).on('pageinit', '#mainReportList', function () {
                $("#DialogSeveralQuestionnairesForm").validate();
                $("#DialogForm").validate();
                $("#DialogRankingForm").validate();

                if ($("#RepDemo").val() != "") {
                    var reports = $("#mainReportList ul li a");//not in
                    $.each(reports, function (index) {
                        var a_element = $(reports[index]);
                        if (a_element.attr("id") != "PopulationReports" && a_element.attr("id") != "FinalReport") {
                            a_element.attr("id", "");
                            a_element.attr("href", "#");
                        }
                    });
                }

            });

        var report = '';
        var final = false;

            $(document).on('click', '#PopulationReports', function () {
                report = '/ChartReports/PopulationGraphics?test_id=';
            });

            $(document).on('click', '#UnivariateReports', function () {
                report = '/ChartReports/UniVariateGraphics?test_id=';
            });

            $(document).on('click', '#BivariateReports', function () {
                report = '/ChartReports/BiVariateGraphics?test_id=';
            });

            $(document).on('click', '#RegressionReports', function () {
                report = '/ChartReports/Results?test_id=';
            });

            $(document).on('click', '#FinalReport', function () {
                report = '/ChartReports/AnalyticalReport?test_id=';
                final = true;
            });

            $(document).on('click', '#Ranking', function () {
                report = '/ChartReports/Ranking?questionnaire=';
            });

            $(document).on('click', '#TextReports', function () {
                report = '/ChartReports/TextAnswerReports?test_id=';
            });

            $(document).on('click', '#FrequencyReports', function () {
                report = '/ChartReports/FrequencyGraphics?test_id=';
            });

            $(document).on('click', '#CategoryReports', function () {
                report = '/ChartReports/CategoryGraphics?test_id=';
            });

            $(document).on('click', '#ComparativeReports', function () {
                report = '/ChartReports/ComparativeGraphics?test_id=';
            });

            $(document).on('click', '#SatisfactionReports', function () {
                report = '/ChartReports/SatisfactionTables?test_id=';
            });

            $(document).on('submit', '#DialogForm', handleForm);
            $(document).on('submit', '#DialogRankingForm', handleFormRanking);
            $(document).on('submit', '#DialogSeveralQuestionnairesForm', handleForm);

            function handleForm(e) {
                $.mobile.loading('show');
                var data = $(this).serializeArray();

                report = report + data[0].value; 
                if (report.indexOf("Uni") !== -1) {
                    if (data[1].value != "" && data[1].value != null) {
                        report = report + "&compare_id=" + data[1].value;
                    }
                }
                e.preventDefault();

                $.post("/ChartReports/GetTestWithNoEvaluations", { test_id: data[0].value, final: final, compare_id: data[1].value }, function (testN) {
                    if (testN == '0') {
                        
                        window.location.href = report;
                    }
                    else
                        window.location.href = '/ChartReports/NoEvaluations?id_test=' + testN;
                });
            };

            function handleFormRanking(e) {
                $.mobile.loading('show');
                var data = $(this).serializeArray();
                report = report + data[0].value;
                e.preventDefault();
                window.location.href = report;
            };


    function GetUrl(value, id, compare) {
        $.post("/ChartReports/GetTestWithNoEvaluations", { test_id: id, final: final, compare_id: compare }, function (testN) {
            if (testN == '0')
                window.location.href = value + id + compare;
            else
                window.location.href = '/ChartReports/NoEvaluations?id_test=' + testN;
        });
    }

    $(document).on('change', '#Tests', function () {
        if (report.indexOf("Uni") !== -1) {
            $.ajaxSetup({ async: false });
            $.mobile.loading('show');
            $.post("/ChartReports/GetTestsToCompare", { test_id: $("#Tests").val() }, function (j) {
                if (j.length > 0) {
                    var options = '<option value="">' + $('#ViewRes').val() + '</option>';
                    var k = j.length;
                    //                    for (var i = 0z; i < k; i++) {
                    //                        options += '<option value="' + j[i].optionValue + '">' + j[i].optionDisplay + '</option>';
                    //                    }
                    $.each(j, function (i, item) {
                        options += '<option value="' + j[i].optionValue + '">' + j[i].optionDisplay + '</option>'; 
                    });
                    $("#TestsToCompare").html(options);
                    showDivCompare();
                }
                else
                    hideDivCompare();
            });
            $.mobile.loading('hide');
        }
    });

    function showDivCompare() {
        $('#DivCompare').show();
    }

    function hideDivCompare() {
        $('#DivCompare').hide();
    }
    </script>
    <%: Html.Hidden("ViewRes", ViewRes.Scripts.Shared.Select, new { id = "ViewRes" })%> 
    <div class="box rounded">
        <div class="ui-grid-a">
            <div class="ui-block-a">
                <h1><%:ViewRes.Views.Report.ReportsList.Path %></h1>
            </div>
            <%--<div class="ui-block-b" style="width:30%">
                <a href="/Feedbacks/SendFeedback" id="Feedbacks"><span id=<%: ViewRes.Views.ChartReport.ReportsList.FeedbacksImage %>></span></a>
            </div>--%>
        </div>
        <br />
        <div data-demo-html="true">
            <% string roleClass = Roles.IsUserInRole("FreeReports") ? "rep_demo" : ""; %>
            <%: Html.Hidden("RepDemo", roleClass, new { id = "RepDemo" })%>
			<ul data-role="listview" data-inset="true" data-filter="true">
				<li><a href="#Dialog" data-rel="dialog" id="PopulationReports">
					<img src="/Content/mobile/Images/icon-report/population.png" />
					<h2><%: ViewRes.Views.ChartReport.ReportsList.Population %></h2>
					</a>
				</li>
				<li><a href="#Dialog" data-rel="dialog" id="UnivariateReports">
					<img src="/Content/mobile/Images/icon-report/univariate.png" />
					<h2><%: ViewRes.Views.ChartReport.ReportsList.Univariate%></h2>
					</a>
				</li>
				<li><a href="#Dialog" data-rel="dialog" id="CategoryReports">
					<img src="/Content/mobile/Images/icon-report/category.png" />
					<h2><%: ViewRes.Views.ChartReport.ReportsList.Category%></h2>
					</a>
				</li>
                <li><a href="#Dialog" data-rel="dialog" id="RegressionReports">
					<img src="/Content/mobile/Images/icon-report/result.png" />
					<h2><%: ViewRes.Views.ChartReport.ReportsList.Regression%></h2>
					</a>
				</li>
				<li><a href="#Dialog" data-rel="dialog" id="FinalReport">
					<img src="/Content/mobile/Images/icon-report/executive.png" />
					<h2><%: ViewRes.Views.ChartReport.ReportsList.Result%></h2>
					</a>
				</li>
				<li><a href="#Dialog" data-rel="dialog" id="TextReports">
					<img src="/Content/mobile/Images/icon-report/text.png" />
					<h2><%: ViewRes.Views.ChartReport.ReportsList.Text%></h2>
					</a>
				</li>
				<li><a href="#Dialog" data-rel="dialog" id="FrequencyReports">
					<img src="/Content/mobile/Images/icon-report/frecuency.png" />
					<h2><%: ViewRes.Views.ChartReport.ReportsList.Frequency%></h2>
					</a>
				</li>
				<li><a href="#Dialog" data-rel="dialog" id="BivariateReports">
					<img src="/Content/mobile/Images/icon-report/dual.png" />
					<h2><%: ViewRes.Views.ChartReport.ReportsList.Bivariate%></h2>
					</a>
				</li>
				<li><a href="#DialogRanking" data-rel="dialog" id="Ranking">
					<img src="/Content/mobile/Images/icon-report/ranking.png" />
					<h2><%: ViewRes.Views.ChartReport.ReportsList.Ranking%></h2>
					</a>
				</li>
                <%if (Model.testsSeveralQuestionnaires.Count() > 0)
                  { %>
				<li><a href="#DialogSeveralQuestionnaires" data-rel="dialog" id="SatisfactionReports">
					<img src="/Content/mobile/Images/icon-report/satisfaction.png" />
					<h2><%: ViewRes.Views.ChartReport.ReportsList.Satisfaction %></h2>
					</a>
				</li>
                <li><a href="#DialogSeveralQuestionnaires" data-rel="dialog" id="ComparativeReports">
					<img src="/Content/mobile/Images/icon-report/comparative.png" />
					<h2><%: ViewRes.Views.ChartReport.ReportsList.Comparative %></h2>
					</a>
				</li>
                <%
                    }
                %>
			</ul>
		</div>
    </div>

</asp:Content>

<asp:Content ID="Content6" ContentPlaceHolderID="PageContent" runat="server">

    <div data-role="page" id="DialogRanking" class="basic"  data-close-btn="right">
        <div data-role="header" data-theme="f">
		    <h1><%= ViewRes.Views.Report.ReportsList.ChooseQuestionnaire %></h1>
	    </div>
        <div data-role="content" class="content basic">
            <div class="box rounded" id="DialogRanking1">
                <p><%= ViewRes.Views.Report.ReportsList.SelectQuestionnaire %></p>
                <form action="/"  data-ajax="false" id="DialogRankingForm" name="DialogRankingName">
		            <div>
			            <%: Html.DropDownList("Questionnaires", Model.questionnaires, ViewRes.Scripts.Shared.Select, new { @class = "required" })%>
		            </div>
                    <div style="text-align:right">

                        <input type=submit value="Ok" data-inline="true" data-theme="f"/>
                        <a href="#main" data-rel="back" data-role="button" data-inline="true" data-icon="back" data-theme="f"><%:ViewRes.Views.Shared.Shared.CancelLabel %></a>
                        
                    </div>
                </form>
            </div>
        </div>
        <%--<% Html.RenderPartial("mobile/MobileFooter"); %> --%>
    </div>

    <div data-role="page" id="DialogSeveralQuestionnaires" class="basic"  data-close-btn="right">
        <div data-role="header" data-theme="f">
            <h1><%= ViewRes.Views.Report.ReportsList.ChooseTest %></h1>
	    </div>
        <div data-role="content" class="content basic">
            <div class="box rounded" id="DialogSeveralQuestionnaires1">
                <p><%= ViewRes.Views.Report.ReportsList.SelectTest %></p>
                <form action="/"  data-ajax="false" id="DialogSeveralQuestionnairesForm" name="DialogSeveralQuestionnairesName">
		            <div>
			            <%: Html.DropDownList("TestsSeveralQuestionnaires", Model.testsSeveralQuestionnaires, ViewRes.Scripts.Shared.Select, new { @class = "required" })%>
		            </div>
                    <div id="Div1" style="display:none;">
                        <div><%: ViewRes.Views.ChartReport.Graphics.Compare %></div>
                        <div>
                            <%: Html.DropDownList("TestsToCompare2", Model.testsToCompare, ViewRes.Scripts.Shared.Select, new { @class = "required" })%>
                        </div>
                    </div>
                    <div style="text-align:right">
                        <input type=submit value="Ok" data-inline="true" data-theme="f"/>
                        <a href="#main" data-rel="back" data-role="button" data-inline="true" data-icon="back" data-theme="f"><%:ViewRes.Views.Shared.Shared.CancelLabel %></a>
                    </div>
                </form>
            </div>
        </div>
        <%--<% Html.RenderPartial("mobile/MobileFooter"); %> --%>
    </div>

    <div data-role="page" id="Dialog" class="basic"  data-close-btn="right">
        <div data-role="header" data-theme="f">
            <h1><%= ViewRes.Views.Report.ReportsList.ChooseTest %></h1>
	    </div>
        <div data-role="content" class="content basic">
            <div class="box rounded" id="Dialog1">
                <p><%= ViewRes.Views.Report.ReportsList.SelectTest %></p>
                <form action="/" data-ajax="false" id="DialogForm" name="DialogName">
		            <div>
			            <%: Html.DropDownList("Tests", Model.tests, ViewRes.Scripts.Shared.Select, new { @class = "required" })%>
		            </div>
                    <div id="DivCompare" style="display:none;">
                        <div><%: ViewRes.Views.ChartReport.Graphics.Compare %></div>
                        <div>
                            <%: Html.DropDownList("TestsToCompare", Model.testsToCompare, ViewRes.Scripts.Shared.Select, new { @class = "required" })%>
                        </div>
                    </div>
                    <div style="text-align:right">
                        <input type=submit value="Ok" data-inline="true" data-theme="f"/>
                        <a href="#main" data-rel="back" data-role="button" data-inline="true" data-icon="back" data-theme="f"><%:ViewRes.Views.Shared.Shared.CancelLabel %></a>
                        
                    </div>
                </form>
            </div>
        </div>
        <%--<% Html.RenderPartial("mobile/MobileFooter"); %> --%>
    </div>
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>

<asp:Content ID="Content4" ContentPlaceHolderID="CssContent" runat="server">
</asp:Content>

<asp:Content ID="Content5" ContentPlaceHolderID="JsContent" runat="server">
</asp:Content>
