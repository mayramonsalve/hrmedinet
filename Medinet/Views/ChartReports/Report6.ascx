<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Medinet.Models.ViewModels.ResultViewModel>" %>

<div class="prepend-1 span-22 apend-1">
        <h4><%: ViewRes.Views.ChartReport.Graphics.GeneralResult%> <%: Model.test.Name %></h4>
        <h4><%: ViewRes.Views.ChartReport.Graphics.Period %><%: Model.test.StartDate.ToString(ViewRes.Views.Shared.Shared.Date)%> - <%: Model.test.EndDate.ToString(ViewRes.Views.Shared.Shared.Date)%></h4><br/><br/>
       <% if (!Model.test.OneQuestionnaire)
          { %>
            <div id="myTabs-Result6">
                <ul>
                    <li><a href="#tabResult6-">General</a></li>
                <% foreach (MedinetClassLibrary.Models.Questionnaire questionnaire in Model.questionnairesInTest)
                   { %>
                    <li><a class="all" href="#tabResult6-<%: questionnaire.Id %>"><%: questionnaire.Name %></a></li>
                    <%} %>
                </ul>
                <div id="tabResult6-">
                        <%Html.RenderPartial("Report6Form", Model); %>
                </div>
                <% foreach (MedinetClassLibrary.Models.Questionnaire questionnaire in Model.questionnairesInTest)
                   { %>
                        <div id="tabResult6-<%: questionnaire.Id %>">
                        </div>
                    <%} %>
            </div>
        <%}
          else
          { %>
          <%Html.RenderPartial("Report6Form", Model); %>
        <%} %>
</div>
<br/>
        <% if (!Model.test.OneQuestionnaire)
       { %>
        <script type="text/javascript">
            $(function () {
                $("#myTabs-Result6").tabs();
                $("#myTabs-Result6 .all").bind("click", handle_tab_click);
            });        
        </script>  
     <%} %>
<%if (!Model.print)
  { %>
<%--<div class="prepend-20 span-4 column last">
    <%: Html.ActionLink(ViewRes.Views.ChartReport.Graphics.PrintSection, "PdfResults", new { test_id = Model.test.Id, report = 6 }, null)%>
</div>
<div class="clear"></div>--%>
<%}%>
<%--<script type="text/javascript">
    $(function () {
        $(".myaccordion").accordion({
            collapsible: true
        });
    });
</script>
--%>