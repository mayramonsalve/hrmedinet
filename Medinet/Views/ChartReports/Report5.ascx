<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Medinet.Models.ViewModels.ResultViewModel>" %>

    <div class="prepend-1 span-22 append-1" style="page-break-inside:avoid">
        <h4><%: ViewRes.Views.ChartReport.Graphics.GeneralResult%> <%: Model.test.Name %></h4>
        <h4><%: ViewRes.Views.ChartReport.Graphics.Period %><%: Model.test.StartDate.ToString(ViewRes.Views.Shared.Shared.Date)%> - <%: Model.test.EndDate.ToString(ViewRes.Views.Shared.Shared.Date)%></h4><br/><br/>
       <% if (!Model.test.OneQuestionnaire)
          { %>
            <div id="myTabs-Result5">
                <ul>
                    <li><a href="#tabResult5-">General</a></li>
                <% foreach (MedinetClassLibrary.Models.Questionnaire questionnaire in Model.questionnairesInTest)
                   { %>
                    <li><a class="all" href="#tabResult5-<%: questionnaire.Id %>"><%: questionnaire.Name %></a></li>
                    <%} %>
                </ul>
                <div id="tabResult5-">
                        <%Html.RenderPartial("Report5Form", Model); %>
                </div>
                <% foreach (MedinetClassLibrary.Models.Questionnaire questionnaire in Model.questionnairesInTest)
                   { %>
                        <div id="tabResult5-<%: questionnaire.Id %>">
                        </div>
                    <%} %>
            </div>
        <%}
          else
          { %>
          <%Html.RenderPartial("Report5Form", Model); %>
        <%} %>
    </div>
    <br/>
        <% if (!Model.test.OneQuestionnaire)
       { %>
        <script type="text/javascript">
            $(function () {
                $("#myTabs-Result5").tabs();
                $("#myTabs-Result5 .all").bind("click", handle_tab_click);
            });        
        </script>  
     <%} %>
<%if (!Model.print)
  { %>
<%--<div class="prepend-20 span-4 column last">
    <%: Html.ActionLink(ViewRes.Views.ChartReport.Graphics.PrintSection, "PdfResults", new { test_id = Model.test.Id, report = 5 }, null)%>
</div>
<div class="clear"></div>--%>
<%} %>