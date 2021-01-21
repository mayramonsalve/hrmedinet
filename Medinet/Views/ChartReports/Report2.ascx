<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Medinet.Models.ViewModels.ResultViewModel>" %>
    <div class="prepend-2 span-20 apend-2" style="page-break-inside:avoid">
        <h4><%: ViewRes.Views.ChartReport.Graphics.GeneralResult%> <%: Model.test.Name %></h4>
        <h4><%: ViewRes.Views.ChartReport.Graphics.Period %><%: Model.test.StartDate.ToString(ViewRes.Views.Shared.Shared.Date)%> - <%: Model.test.EndDate.ToString(ViewRes.Views.Shared.Shared.Date)%></h4><br/><br/>
       <% if (!Model.test.OneQuestionnaire)
          { %>
            <div id="myTabs-Result2">
                <ul>
                    <li><a href="#tabResult2-">General</a></li>
                <% foreach (MedinetClassLibrary.Models.Questionnaire questionnaire in Model.questionnairesInTest)
                   { %>
                    <li><a class="all" href="#tabResult2-<%: questionnaire.Id %>"><%: questionnaire.Name %></a></li>
                    <%} %>
                </ul>
                <div id="tabResult2-">
                        <%Html.RenderPartial("Report2Form", Model); %>
                </div>
                <% foreach (MedinetClassLibrary.Models.Questionnaire questionnaire in Model.questionnairesInTest)
                   { %>
                        <div id="tabResult2-<%: questionnaire.Id %>">
                        </div>
                    <%} %>
            </div>
        <%}
          else
          { %>
          <%Html.RenderPartial("Report2Form", Model); %>
        <%} %>
    </div>
    <br/>
        <% if (!Model.test.OneQuestionnaire)
       { %>
        <script type="text/javascript">
            $(function () {
                $("#myTabs-Result2").tabs();
                $("#myTabs-Result2 .all").bind("click", handle_tab_click);
            });        
        </script>  
     <%} %>
<%if (!Model.print)
  { %>
<%--<div class="prepend-20 span-4 column last">
    <%: Html.ActionLink(ViewRes.Views.ChartReport.Graphics.PrintSection, "PdfResults", new { test_id = Model.test.Id, report = 2 }, null)%>
</div>
<div class="clear"></div>--%>
<%} %>