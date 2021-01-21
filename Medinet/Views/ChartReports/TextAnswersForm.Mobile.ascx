<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Medinet.Models.ViewModels.ChartReportViewModel>" %>
<%@ Import Namespace="MedinetClassLibrary.CustomClasses" %>

<% string demographic = ViewData["option"].ToString();
   string var_FO_id = "";
   int? FO_id= null;
   int? compare_id = null;
   string testCompareName = "";

       if (Model.testCompare != 0)
       {
           compare_id = Model.testCompare;
           testCompareName = new MedinetClassLibrary.Services.TestsServices().GetById(Model.testCompare).Name;
       }
       if (demographic.CompareTo("FunctionalOrganizationType") == 0)
       {
           FO_id = Int32.Parse(ViewData["FO_id"].ToString());
           var_FO_id = FO_id.ToString();
       }

       %> 
    <div>    
        <div>   
                <strong><%: ViewRes.Views.ChartReport.Graphics.Parametres%></strong>
                <fieldset data-role="fieldcontain">
                <% if (!Model.test.OneQuestionnaire)
                    { %>
                
                    <label for="<%: demographic %>GroupByQuestionnairesDDL<%: var_FO_id %>" ><%: ViewRes.Views.ChartReport.Graphics.Questionnaire%></label> 
                    <%: Html.DropDownList(demographic + "GroupByQuestionnairesDDL" + var_FO_id, Model.questionnaires, ViewRes.Scripts.Shared.ShowAll, new { @class = "GroupByQuestionnairesDDL form-short " })%>                      
                <%} %>
                    <label for="<%: demographic %>GroupByCategoriesDDL<%: var_FO_id %>" ><%: ViewRes.Views.ChartReport.Graphics.Category%></label>
                    <%: Html.DropDownList(demographic + "GroupByCategoriesDDL" + var_FO_id, Model.GroupByCategories, ViewRes.Scripts.Shared.ShowAll, new { @class = "GroupByCategoriesDDL form-short " })%>
                    <label for="<%: demographic %>GroupByQuestionsDDL<%: var_FO_id %>" ><%: ViewRes.Views.ChartReport.Graphics.Question%></label>
                    <%: Html.DropDownList(demographic + "GroupByQuestionsDDL" + var_FO_id, Model.question, ViewRes.Scripts.Shared.ShowAll, new { @class = "GroupByQuestionsDDL form-short" })%>
                </fieldset>     
        </div>
        <div class="textAnswers">
            <div id="DivSelect<%=demographic%><%=var_FO_id%>" class="alignCenter">
                    <h5><%: ViewRes.Views.ChartReport.Graphics.SelectQuestion%></h5>
            </div>
<%--            <div id="AnswersDiv<%=demographic%><%=var_FO_id%>" style = "display:none;">
                <div class="barraIzquierda" id="itemsDiv<%=demographic%><%=var_FO_id%>">
                </div>
                <div class="barraDerecha">
                    <div id="Pagination<%=demographic%><%=var_FO_id%>" class="pagination span-23 prepend-1 column last"></div>
                    <div id="Answers<%=demographic%><%=var_FO_id%>" class="span-23 column last AnswersResult"></div>
                </div>
            </div>--%>

            <div id="AnswersDiv<%=demographic%><%=var_FO_id%>" data-role="collapsible-set" data-theme="f" style = "display:none;">
                <% int k = 1; %>
                <% foreach (string title in Model.GetDemographics(demographic, FO_id))
                   { %>
                <div data-role="collapsible">
                    <h2><%: title%></h2>
                    <div id="Pagination<%=demographic%><%=var_FO_id%><%=k %>" class="pagination"></div>
                    <div id="Answers<%=demographic%><%=var_FO_id%><%=k %>" class="AnswersResult"></div>
                </div>
                <% k++; %>
                <% } %>

            </div>
        </div>



        <div>
            <% if (Model.testsToCompare.Count() > 0)
                { %>
                <div><%: ViewRes.Views.ChartReport.Graphics.Compare%></div>
                <div>
                    <%: Html.DropDownList(demographic + "CompareDDL" + var_FO_id, Model.testsToCompare, ViewRes.Views.ChartReport.Graphics.None, new { @class = "CompareDDL form-short" })%>
                </div>
            <%}
                else
                {%>
                <%: Html.Hidden(demographic + "CompareDDL" + var_FO_id, null, new { id = demographic + "CompareDDL" + var_FO_id })%>
                <%} %>
        </div>
   </div>

