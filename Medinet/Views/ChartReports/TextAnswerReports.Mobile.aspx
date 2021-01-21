<%@ Page Title="" Debug="true" Language="C#" MasterPageFile="~/Views/Shared/Mobile/MobileMini.Master" Inherits="System.Web.Mvc.ViewPage<Medinet.Models.ViewModels.ChartReportViewModel>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    <%: ViewRes.Views.Shared.Shared.GenericTitle %> <%: ViewRes.Views.ChartReport.Graphics.TextAnswersTitle %>
</asp:Content>

<asp:Content ID="Content7" ContentPlaceHolderID="PageId" runat="server">
<div data-role="page" id="mainChart" class="basic">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
         <%: Html.Hidden("ButtonBack", ViewRes.Views.Evaluation.AnswerTest.ButtonBack, new { id = "ButtonBack" })%>
          <%: Html.Hidden("ButtonNext", ViewRes.Views.Evaluation.AnswerTest.ButtonNext, new { id = "ButtonNext" })%>
          <%: Html.Hidden("Select", ViewRes.Scripts.Shared.Select, new { id = "ViewRes" })%>
          <%: Html.Hidden("test_id",Model.test.Id)%>
          <%: Html.Hidden("compare_id",Model.testCompare)%>
          <%: Html.Hidden("General", 1)%>
          <%: Html.Hidden("AllTests", 1)%>
          <%: Html.Hidden("Location", Model.demographicsCount["Location"])%>
          <%: Html.Hidden("AgeRange", Model.demographicsCount["AgeRange"])%>
          <%: Html.Hidden("Country", Model.demographicsCount["Country"])%>
          <%: Html.Hidden("Region", Model.demographicsCount["Region"])%>
          <%: Html.Hidden("InstructionLevel", Model.demographicsCount["InstructionLevel"])%>
          <%: Html.Hidden("PositionLevel", Model.demographicsCount["PositionLevel"])%>
          <%: Html.Hidden("Seniority", Model.demographicsCount["Seniority"])%>
          <%: Html.Hidden("Gender", Model.demographicsCount["Gender"])%>
          <%: Html.Hidden("Performance", Model.demographicsCount["Performance"])%>
            <%: Html.Hidden("chartType", "TextAnswers")%>
            <% string FOids = ""; %>
          <%  foreach (var v in Model.FO)
              {%>
                <%: Html.Hidden("FunctionalOrganizationType"+v.Key, Model.GetFOCount(v.Key))%>
            <% }%>
      
            <div id="myTabs" class="box rounded tabs smallth">
                <div id="tabGeneral">
                    <h1><a href="#tabs" data-rel="dialog" data-role="button" data-iconpos="notext" data-theme="f" data-inline="true" data-icon="bars"></a><%: ViewRes.Views.ChartReport.Graphics.GeneralAnswers %></h1>
                    <%ViewData["option"] = "General"; %>
                    <%Html.RenderPartial("TextAnswersForm", Model); %>
                </div>
                <%Html.RenderPartial("TabDiv.Mobile"); %>
                <%Html.RenderPartial("Button.Mobile"); %>
            </div>
</asp:Content>

<asp:Content ID="Content6" ContentPlaceHolderID="PageContent" runat="server">
<%Html.RenderPartial("DialogDiv.Mobile"); %>
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>

<asp:Content ID="Content4" ContentPlaceHolderID="CssContent" runat="server">
</asp:Content>

<asp:Content ID="Content5" ContentPlaceHolderID="JsContent" runat="server">
    <script src="../../Scripts/jquery.pagination.js" type="text/javascript"></script>
    <script src="<%: Url.Content("~/Scripts/mobile/jquery.tablesorter.js") %>" type="text/javascript"></script>
    <script src="<%: Url.Content("~/Scripts/mobile/ChartsMobile.js?mm=0709") %>" type="text/javascript"></script>
</asp:Content>
