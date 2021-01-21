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
       //List<SummaryTable> textAnswers = Model.GetTextAnswersList(Model.test.Id, demographic, null, null, FO_id, compare_id);
       //elements_count = textAnswers.Count;
       %> 
    <div class="span-24 column last">    
<%--    <% if (elements_count > 0)
       {%>--%>
        <div class="span-24 column last">
            <fieldset >
                <legend><b><%: ViewRes.Views.ChartReport.Graphics.Parametres %></b></legend>
<%--                <% if (Model.details.Count() > 0 && Model.details[order] != null) { %>
                    <h4><%:Model.details[order].Title%></h4>
                <% }%>             --%>     
                <% if (!Model.test.OneQuestionnaire)
                   { %>
                <div class="span-24 last"><%: ViewRes.Views.ChartReport.Graphics.Questionnaire%>
                    <%: Html.DropDownList(demographic + "GroupByQuestionnairesDDL" + var_FO_id, Model.questionnaires, ViewRes.Scripts.Shared.Select, new { @class = "GroupByQuestionnairesDDL form-short " })%>
                </div>
                <%} %>                  
                <div class="span-24 last"><%: ViewRes.Views.ChartReport.Graphics.Category %>
                    <%: Html.DropDownList(demographic + "GroupByCategoriesDDL" + var_FO_id, Model.GroupByCategories, ViewRes.Scripts.Shared.Select, new { @class = "GroupByCategoriesDDL form-short " })%>
                </div>
                <div class="span-24 last"><%: ViewRes.Views.ChartReport.Graphics.Question %>
                    <%: Html.DropDownList(demographic + "GroupByQuestionsDDL" + var_FO_id, Model.question, ViewRes.Scripts.Shared.Select, new { @class = "GroupByQuestionsDDL form-short" })%>
                </div>
            </fieldset>
        </div>
        <br />
        <div class="span-24 column last textAnswers">
            <fieldset>
            <div id="DivSelect<%=demographic%><%=var_FO_id%>" class="alignCenter">
                    <h5><%: ViewRes.Views.ChartReport.Graphics.SelectQuestion%></h5>
            </div>
            <div id="AnswersDiv<%=demographic%><%=var_FO_id%>" class="span-23 append-1 column last" style = "display:none;">
                <div class="span-4 column barraIzquierda" id="itemsDiv<%=demographic%><%=var_FO_id%>">
                </div>
                <div class="span-20 column last barraDerecha">
                    <div id="Pagination<%=demographic%><%=var_FO_id%>" class="pagination span-23 prepend-1 column last"></div>
                    <div id="Answers<%=demographic%><%=var_FO_id%>" class="span-23 column last AnswersResult">
                </div>
            </div>
            </div>
            </fieldset>
        </div>
        <div class="span-8 column last">
            <% if (Model.testsToCompare.Count() > 0)
                { %>
                <div class="span-24 last"><%: ViewRes.Views.ChartReport.Graphics.Compare%></div>
                <div>
                    <%: Html.DropDownList(demographic + "CompareDDL" + var_FO_id, Model.testsToCompare, ViewRes.Views.ChartReport.Graphics.None, new { @class = "CompareDDL form-short" })%>
                </div>
            <%}
                else
                {%>
                <%: Html.Hidden(demographic + "CompareDDL" + var_FO_id, null, new { id = demographic + "CompareDDL" + var_FO_id })%>
                <%} %>
        </div>
<%--   <%}
       else
       {%>
            <%Html.RenderPartial("NoMinimum"); %>
       <%}%>--%>
   </div>
<%--      <% if (elements_count > 0)
      {%>--%>
<%--      <div class="clear">&nbsp;</div>
   <div class="prepend-19 span-5 column last">
        <%: Html.ActionLink(ViewRes.Views.ChartReport.Graphics.PrintGraphics + ViewRes.Views.ChartReport.Graphics.AndResult, "PdfUnivariate", new
        {
            test_id = Model.test.Id, graphic_id = id, elementsCount = elements_count,
            category_id=0, question_id=0, pValue=0.05,
            FO_id = FO_id, country_id = 0, compare_id = Model.testCompare
         }, new { id = demographic+var_FO_id+"PrintLink" })%>
   </div>--%>
<%--    <%} %>--%>

