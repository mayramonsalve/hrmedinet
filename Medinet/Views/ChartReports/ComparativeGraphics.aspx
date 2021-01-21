<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Reports.Master" Inherits="System.Web.Mvc.ViewPage<Medinet.Models.ViewModels.ChartReportViewModel>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    <%: ViewRes.Views.Shared.Shared.GenericTitle %> <%:ViewRes.Views.ChartReport.Graphics.ComparativeTitle %>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
        <%: Html.Hidden("FOids", "")%>
        <%: Html.Hidden("compare_id","")%>
        <%: Html.Hidden("chartModel", "Column")%>
        <%: Html.Hidden("test_id",Model.test.Id) %>
        <%: Html.Hidden("chartType", "Comparative")%> 
       <%: Html.Hidden("DownloadImage", ViewRes.Views.ChartReport.Graphics.DownloadImage)%>
        <%ViewData["print"] = "False"; %>
            <div id="myTabs">
            <h2 class="path"><%: ViewRes.Views.ChartReport.Graphics.ComparativeTitle%></h2>
                <ul>
                    <li><a href="#tabPopulation"><%: ViewRes.Views.ChartReport.Graphics.Population %></a></li>
                    <li><a class ="all" href="#tabClimate"><%: ViewRes.Views.ChartReport.Graphics.Climate%></a></li>
                </ul>
                <div id="tabPopulation">
                    <%ViewData["option"] = "Population"; %>
                    <%Html.RenderPartial("Comparative", Model); %>
                </div>
                <div id="tabClimate">
                </div>
        </div>
    <div class="span-12 column"> &nbsp;
<%--        <%: Html.ActionLink(ViewRes.Views.ChartReport.Graphics.PrintGraphics, "ImprimirPdf", new { report = "Population", test_id = Model.test.Id }, null)%>--%>
    </div>
    <div class="span-12 column last alignRight">
        <%: Html.ActionLink(ViewRes.Views.ChartReport.ReportsList.BackToList, "ReportsList", "ChartReports")%>
    </div>
    <div id="loading"></div>
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>

<asp:Content ID="Content4" ContentPlaceHolderID="CssContent" runat="server">
    <link href="../../Content/Css/demo_page.css" rel="stylesheet" type="text/css" media="screen, projection" />
    <link href="../../Content/Css/demo_table_jui.css" rel="stylesheet" type="text/css" media="screen, projection" />
    <link href="../../Content/Css/demo_table.css" rel="stylesheet" type="text/css" media="screen, projection" />
<%--    <link href="../../Content/Css/csskit.css" rel="stylesheet" type="text/css" />--%>
</asp:Content>

<asp:Content ID="Content5" ContentPlaceHolderID="JsContent" runat="server">
    <script src="<%: Url.Content("~/Scripts/jquery.dataTables.js") %>" type="text/javascript"></script>
    <script src="<%: Url.Content("~/Scripts/jquery.dataTables.min.js") %>" type="text/javascript"></script>
    <script src="../../Scripts/jquery.tinymce.js" type="text/javascript"></script>
</asp:Content>
