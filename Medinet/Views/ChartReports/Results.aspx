<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<Medinet.Models.ViewModels.ResultViewModel>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    <%: ViewRes.Views.Shared.Shared.GenericTitle %><%: ViewRes.Views.ChartReport.Graphics.StatisticalReports %>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

<h2><%: ViewRes.Views.ChartReport.Graphics.StatisticalReports %></h2>
    <%: Html.Hidden("test_id",Model.test.Id) %> 
    <% bool sevT = Model.test.OneQuestionnaire ? (Model.test.Questionnaire_Id == 7 ? true : false) : false;%><%-- Model.test.OneQuestionnaire ? Model.test.Questionnaire.Tests.Where(t => t.Company_Id != Model.test.Company_Id).Count() > 0 : false; %>--%>
    <div id="myTabs">
        <ul>
            <li><a href="#tabResult1"><%: ViewRes.Views.ChartReport.Graphics.PositiveByCategory %></a></li>
            <li><a class ="all" href="#tabResult2"><%: ViewRes.Views.ChartReport.Graphics.ByCategory %></a></li>
            <li><a class ="all" href="#tabResult3"><%: ViewRes.Views.ChartReport.Graphics.PositiveByQuestion %></a></li>
            <li><a class ="all" href="#tabResult4"><%: ViewRes.Views.ChartReport.Graphics.ByQuestion %></a></li>
            <% if (sevT)
               { %>
            <li><a class ="all" href="#tabResult5"><%: ViewRes.Views.ChartReport.Graphics.ComparativeByCategory%></a></li>
            <li><a class ="all" href="#tabResult6"><%: ViewRes.Views.ChartReport.Graphics.ComparativeByQuestion%></a></li>
            <%} %>
        </ul>
        <div id="tabResult1">
            <%Html.RenderPartial("Report1",Model); %>
        </div>
        <div id="tabResult2"></div>
        <div id="tabResult3"></div>
        <div id="tabResult4"></div>
        <% if (sevT)
           { %>
        <div id="tabResult5"></div>
        <div id="tabResult6"></div>
        <%} %>
    </div>
<%--    <div class="span-12 column">
        <%: Html.ActionLink(ViewRes.Views.ChartReport.Graphics.PrintResults, "PdfResults", new { test_id = Model.test.Id }, null)%>
    </div>--%>
    <div class="span-12 column last alignLeft">
        <%: Html.ActionLink(ViewRes.Views.ChartReport.ReportsList.BackToList, "ReportsList", "ChartReports")%>
    </div>
    <div id="loading"></div>

</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>

<asp:Content ID="Content4" ContentPlaceHolderID="CssContent" runat="server">
    <%--<link href="../../Content/Css/demo_page.css" rel="stylesheet" type="text/css" media="screen, projection" />--%>
    <link href="../../Content/Css/demo_table_jui.css" rel="stylesheet" type="text/css" media="screen, projection" />
    <link href="../../Content/Css/demo_table.css" rel="stylesheet" type="text/css" media="screen, projection" />
</asp:Content>

<asp:Content ID="Content5" ContentPlaceHolderID="JsContent" runat="server"> 

    <%--<script src="<%: Url.Content("~/Scripts/jquery.dataTables.js") %>" type="text/javascript"></script>--%>
    <script src="<%: Url.Content("~/Scripts/jquery.dataTables.min.js") %>" type="text/javascript"></script>
    <script type="text/javascript">
        jQuery.extend(jQuery.fn.dataTableExt.oSort, {

            "numeric-comma-pre": function (a) {
                var x = (a == "-") ? 0 : a.replace(/,/, "."); 
                return parseFloat(x);
            },
            "numeric-comma-asc": function (a, b) {
                var x = a.replace("%", "");
                var y = b.replace("%", "");
                x = parseFloat(x);
                y = parseFloat(y);
                return ((x < y) ? -1 : ((x > y) ? 1 : 0));
            },
            "numeric-comma-desc": function (a, b) {
                var x = a.replace("%", "");
                var y = b.replace("%", "");
                x = parseFloat(x);
                y = parseFloat(y);
                return ((x < y) ? 1 : ((x > y) ? -1 : 0));
            }
         });
</script>
    <script src="<%: Url.Content("~/Scripts/Results.js?mm=0709") %>" type="text/javascript"></script>
</asp:Content>
