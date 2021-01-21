<%@ Page Title="" Debug="true" Language="C#" MasterPageFile="~/Views/Shared/Mobile/MobileMini.Master" Inherits="System.Web.Mvc.ViewPage<Medinet.Models.ViewModels.ChartReportViewModel>" %>
<%@ Import Namespace="MedinetClassLibrary.Services" %>
<%@ Import Namespace="MedinetClassLibrary.Models" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    <%: ViewRes.Views.Shared.Shared.GenericTitle %> <%: ViewRes.Views.ChartReport.Graphics.CategoryTitle %>
</asp:Content>

<asp:Content ID="Content7" ContentPlaceHolderID="PageId" runat="server">
    <div data-role="page" id="mainChart" class="basic">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
        <%: Html.Hidden("Select", ViewRes.Scripts.Shared.ShowAll, new { id = "ViewRes" })%>
        <%: Html.Hidden("test_id",Model.test.Id)%>
        <%: Html.Hidden("General", 1)%>
        <%: Html.Hidden("Location", Model.demographicsCount["Location"])%>
        <%: Html.Hidden("AgeRange", Model.demographicsCount["AgeRange"])%>
        <%: Html.Hidden("Country", Model.demographicsCount["Country"])%>
        <%: Html.Hidden("Region", Model.demographicsCount["Region"])%>
        <%: Html.Hidden("InstructionLevel", Model.demographicsCount["InstructionLevel"])%>
        <%: Html.Hidden("PositionLevel", Model.demographicsCount["PositionLevel"])%>
        <%: Html.Hidden("Seniority", Model.demographicsCount["Seniority"])%>
        <%: Html.Hidden("Gender", Model.demographicsCount["Gender"])%>
        <%: Html.Hidden("Performance", Model.demographicsCount["Performance"])%>
        <%: Html.Hidden("chartType", "Category")%>
        <%: Html.Hidden("chartModel", "Column")%>
        <% int numberOfCategories = Model.GetCategoriesCountByTest(Model.test.Id);
           
        %>
        <% string FOids = ""; %>
          <% int i = 0; 
              foreach (var v in Model.FO)
              {%>
                <%: Html.Hidden("FunctionalOrganizationType"+v.Key, Model.GetFOCount(v.Key))%>
            <% }%>
            <h1 class ="testname"><%= ViewRes.Views.ChartReport.Graphics.CategoryTitle%></h1>   
            <div id="myTabs" class="box rounded tabs">
                <div id="tabGeneral">
                    <h1><a href="#tabs" data-rel="dialog" data-role="button" data-iconpos="notext" data-theme="f" data-inline="true" data-icon="bars"></a><%: ViewRes.Views.ChartReport.Graphics.GeneralCategory%></h1>
                    <%ViewData["option"] = "General"; %>
                    <% if (numberOfCategories > 7){ %>
                        <%Html.RenderPartial("CategoryTablesForm", Model); %>
                    <%   } else { %>
                        <%Html.RenderPartial("CategoryChartsForm", Model); %>
                    <% } %>
                </div>
                <%Html.RenderPartial("TabDiv.Mobile"); %>
                <%Html.RenderPartial("Button.Mobile"); %>
            </div>
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>

<asp:Content ID="Content4" ContentPlaceHolderID="CssContent" runat="server">
</asp:Content>

<asp:Content ID="Content6" ContentPlaceHolderID="PageContent" runat="server">
    <%Html.RenderPartial("DialogDiv.Mobile"); %>
</asp:Content>

<asp:Content ID="Content5" ContentPlaceHolderID="JsContent" runat="server">
    <script src="<%: Url.Content("~/Scripts/mobile/jquery.tablesorter.js") %>" type="text/javascript"></script>
    <script src="<%: Url.Content("~/Scripts/mobile/ChartsMobile.js?mm=0709") %>" type="text/javascript"></script>
</asp:Content>
