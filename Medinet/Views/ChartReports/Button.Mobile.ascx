<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<dynamic>" %>

<div class="ui-grid-solo">
<h1></h1>
<%--    <div class="ui-block-a">
        <a data-ajax="false" data-role="button" data-theme="f" href="/ChartReports/ImprimirPdf?report=Population&test_id=<%:Model.test.Id %>">
    </div><%:ViewRes.Views.ChartReport.Graphics.PrintGraphics %></a>--%>
    <div class="ui-block-a">
        <a href="/ChartReports/ReportsList"  data-ajax="false" data-role="button" data-icon="back" data-theme="f"><%:ViewRes.Views.ChartReport.ReportsList.BackToList%></a>
    </div>
</div>