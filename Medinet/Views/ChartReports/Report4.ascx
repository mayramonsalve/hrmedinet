<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Medinet.Models.ViewModels.ResultViewModel>" %>
<div class="prepend-1 span-22 apend-1">
        <h4><%: ViewRes.Views.ChartReport.Graphics.GeneralResult%> <%: Model.test.Name %></h4>
        <h4><%: ViewRes.Views.ChartReport.Graphics.Period %><%: Model.test.StartDate.ToString(ViewRes.Views.Shared.Shared.Date)%> - <%: Model.test.EndDate.ToString(ViewRes.Views.Shared.Shared.Date)%></h4><br/><br/>
       <% if (!Model.test.OneQuestionnaire)
          { %>
            <div id="myTabs-Result4">
                <ul>
                    <%--<li><a href="#tabResult4-">General</a></li>--%>
                <% foreach (MedinetClassLibrary.Models.Questionnaire questionnaire in Model.questionnairesInTest)
                   {
                       string classA = questionnaire == Model.questionnairesInTest.First() ? "" : "all";
                       %>
                    <li><a class="<%: classA %>" href="#tabResult4-<%: questionnaire.Id %>"><%: questionnaire.Name %></a></li>
                    <%} %>
                </ul>
<%--                <div id="tabResult4-">
                        <%Html.RenderPartial("Report4Form", Model); %>
                </div>--%>
                <% foreach (MedinetClassLibrary.Models.Questionnaire questionnaire in Model.questionnairesInTest)
                   { %>
                        <div id="tabResult4-<%: questionnaire.Id %>">
                            <%if (questionnaire == Model.questionnairesInTest.First())
                              {%>
                                <%Html.RenderPartial("Report4Form", Model); %>
                            <%} %>
                        </div>
                    <%} %>
            </div>
        <%}
          else
          { %>
          <%Html.RenderPartial("Report4Form", Model); %>
        <%} %>
</div>
<br/>
        <% if (!Model.test.OneQuestionnaire)
       { %>
        <script type="text/javascript">
            $(function () {
                $("#myTabs-Result4").tabs();
                $("#myTabs-Result4 .all").bind("click", handle_tab_click);
            });        
        </script>  
     <%} %>
<%if (!Model.print)
  { %>
<%--<div class="prepend-20 span-4 column last">
    <%: Html.ActionLink(ViewRes.Views.ChartReport.Graphics.PrintSection, "PdfResults", new { test_id = Model.test.Id, report = 4 }, null)%>
</div>
<div class="clear"></div>--%>
<%} %>