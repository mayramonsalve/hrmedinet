<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Reports.Master" Inherits="System.Web.Mvc.ViewPage<Medinet.Models.ViewModels.ChartReportViewModel>" %>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <h2><%:ViewRes.Views.ChartReport.Graphics.SatisfactionTitle %></h2>

        <%: Html.Hidden("FOids", "")%>
        <%: Html.Hidden("compare_id","")%>
        <%: Html.Hidden("chartModel", "Table")%>
        <%: Html.Hidden("test_id",Model.test.Id) %>
        <%: Html.Hidden("chartType", "Satisfaction")%> 
        <%ViewData["print"] = "False"; %>
            <div id="myTabs">
                <ul>
                    <li><a href="#tabCategoryGeneral"> General </a></li>
                    <% foreach (KeyValuePair<int, string> kvp in Model.tabs)
                       { %>
                        <li><a class ="all" href="#tabCategory<%: kvp.Key %>"><%: kvp.Value %></a></li>
                    <%} %>
                    <li><a class ="all" href="#tabLocationGeneral"> <%: ViewRes.Views.ChartReport.Graphics.LocationTab %> </a></li>
                    <li><a class ="all" href="#tabFunctionalOrganizationTypeGeneral"> <%: ViewRes.Views.ChartReport.Graphics.FOTypeTab %> </a></li>
                </ul>
                <div id="tabCategoryGeneral">
                    <%ViewData["option"] = "Category"; %>
                    <%Html.RenderPartial("SatisfactionTable", Model); %>
                </div>
                <% foreach (KeyValuePair<int, string> kvp in Model.tabs)
                   { %>
                <div id="tabCategory<%: kvp.Key %>"></div>
                <%} %>
                <div id="tabLocationGeneral"></div>
                <div id="tabFunctionalOrganizationTypeGeneral"></div>
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
