<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Medinet.Models.ViewModels.ResultViewModel>" %>
    <div class="prepend-2 span-20 apend-2" style="page-break-inside:avoid">
        <strong><%: ViewRes.Views.ChartReport.Graphics.GeneralResult%> <%: Model.test.Name %></strong><br />
        <strong><%: ViewRes.Views.ChartReport.Graphics.Period %><%: Model.test.StartDate.ToString(ViewRes.Views.Shared.Shared.Date)%> - <%: Model.test.EndDate.ToString(ViewRes.Views.Shared.Shared.Date)%></strong>
       <% if (!Model.test.OneQuestionnaire)
          { %>
            <div id="myTabs-Result2">
                <div id="tabResult2-0">
                        <h1 style="padding-left:2%;">
                            <a href="#questionnairesTabs2" data-rel="dialog" data-role="button" data-iconpos="notext" data-theme="f" data-inline="true" data-icon="bars"></a>
                            <%: ViewRes.Views.ChartReport.Graphics.General%>
                        </h1>
                        <%Html.RenderPartial("Report2Form", Model); %>
                </div>
                <% foreach (MedinetClassLibrary.Models.Questionnaire questionnaire in Model.questionnairesInTest)
                   { %>
                        <div id="tabResult2-<%: questionnaire.Id %>" style="display:none;">
                            <h1 style="padding-left:2%;">
                                <a href="#questionnairesTabs2" data-rel="dialog" data-role="button" data-iconpos="notext" data-theme="f" data-inline="true" data-icon="bars"></a>
                                <%:questionnaire.Name%>
                            </h1>
                        </div>
                    <%} %>
            </div>
        <%}
          else
          { %>
          <%Html.RenderPartial("Report2Form", Model); %>
        <%} %>
    </div>
<%if (!Model.print)
  { %>
<%--<div class="prepend-20 span-4 column last">
    <%: Html.ActionLink(ViewRes.Views.ChartReport.Graphics.PrintSection, "PdfResults", new { test_id = Model.test.Id, report = 2 }, null)%>
</div>
<div class="clear"></div>--%>
<%} %>