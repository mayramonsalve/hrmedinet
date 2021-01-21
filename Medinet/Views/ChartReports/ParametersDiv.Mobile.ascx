<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Medinet.Models.ViewModels.ChartReportViewModel>" %>
<%
    string demographic = ViewData["option"].ToString();
 %>
    <div>
            
            <h4><%: ViewRes.Views.ChartReport.Graphics.Parametres%></h4>
            <% if (!Model.test.OneQuestionnaire)
                { %>
            <fieldset data-role="fieldcontain">
                <label for="<%: demographic %>GroupByQuestionnairesDDL<%: var_FO_id %>" ><%: ViewRes.Views.ChartReport.Graphics.Questionnaire%></label> 
                <%: Html.DropDownList(demographic + "GroupByQuestionnairesDDL" + var_FO_id, Model.questionnaires, ViewRes.Scripts.Shared.ShowAll, new { @class = "GroupByQuestionnairesDDL form-short " })%>
            </fieldset>                             
            <%} %>
            <fieldset data-role="fieldcontain">
                <label for="<%: demographic %>GroupByCategoriesDDL<%: var_FO_id %>" ><%: ViewRes.Views.ChartReport.Graphics.Category%></label>
                <%: Html.DropDownList(demographic + "GroupByCategoriesDDL" + var_FO_id, Model.GroupByCategories, ViewRes.Scripts.Shared.ShowAll, new { @class = "GroupByCategoriesDDL form-short " })%>
                <label for="<%: demographic %>GroupByQuestionsDDL<%: var_FO_id %>" ><%: ViewRes.Views.ChartReport.Graphics.Question%></label>
                <%: Html.DropDownList(demographic + "GroupByQuestionsDDL" + var_FO_id, Model.question, ViewRes.Scripts.Shared.ShowAll, new { @class = "GroupByQuestionsDDL form-short" })%>
            </fieldset>     
            
    </div>