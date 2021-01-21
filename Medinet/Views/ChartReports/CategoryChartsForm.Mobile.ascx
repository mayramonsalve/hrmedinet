<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Medinet.Models.ViewModels.ChartReportViewModel>" %>
<%@ Import Namespace="MedinetClassLibrary.CustomClasses" %>
<%@ Import Namespace="MedinetClassLibrary.Models" %>

<% int test = Model.test.Id;
   bool elements_count = Model.test.CurrentEvaluations >= Model.test.MinimumPeople;//Model.GetCategoriesCount(Model.test.Questionnaire_Id);
   string demographic = ViewData["option"].ToString();
   Graphic graphic = Model.GetByDemographicAndType(demographic, "Category");
   int id = graphic.Id;
   int order = graphic.Order;
   string source = graphic.Source;
   string var_FO_id = (demographic.CompareTo("FunctionalOrganizationType") == 0) ? ViewData["FO_id"].ToString() : "" ;
   List<GraphicDetail> details;
%> 
   <%: Html.Hidden(demographic + "graphic_id" + var_FO_id, id)%>
       <% if (elements_count)
       {
           if (demographic != "General" || !Model.test.OneQuestionnaire)
           {%>
            <strong><%: ViewRes.Views.ChartReport.Graphics.Parametres%></strong>    
            <fieldset data-role="fieldcontain">    
                <% if (!Model.test.OneQuestionnaire)
                    { %>
                    <label for="<%:demographic %>GroupByCategoryQuestionnairesDDL<%:var_FO_id%>"><%: ViewRes.Views.ChartReport.Graphics.Questionnaire%></label>
                    <%: Html.DropDownList(demographic + "GroupByCategoryQuestionnairesDDL" + var_FO_id, Model.questionnaires, ViewRes.Scripts.Shared.ShowAll, new { @class = "GroupByCategoryQuestionnairesDDL form-short " })%>
                              
                <%} %>   
                <% if (demographic != "General")
                    { %>                             
                    <label for="<%:demographic %>GroupByDemographicDDL<%:var_FO_id%>"><%: Model.GetParameterNameByDemographic(demographic, var_FO_id)%></label>
                    <%: Html.DropDownList(demographic + "GroupByDemographicDDL" + var_FO_id, Model.GroupByCategories, ViewRes.Scripts.Shared.ShowAll, new { @class = "GroupByDemographicDDL form-short " })%>

                <%} %>   
            </fieldset>
        <%} %>
        <%: Html.Hidden(demographic + "ElementsCount" + var_FO_id, elements_count, new { id = demographic + "ElementsCount" + var_FO_id })%>
        
        <div class="principal">
            <div class="chartDiv portrait">
<%--                    <% if (demographic.CompareTo("Country") == 0)
                       { %>
                        <img id="<%:demographic%>Chart" name="<%:demographic%>Chart" src="<%= Url.Action("CategoryChartByCountry", new {chartSize= "Screen", chartType="Column"}) %>" usemap="#MyMap2" alt="Graph" />
                        <% Html.RenderAction("CategoryChartMapByCountry", new { chartSize = "Screen", chartType = "Column", graphic_id = id, name = "MyMap2", test_id = Model.test.Id }); %>
                    <%  }
                       else if (demographic.CompareTo("FunctionalOrganizationType") == 0)
                       { %>
                             <img id="<%:demographic%>Chart<%:var_FO_id%>" name="<%:demographic%>Chart" src="<%:source%>&graphic_id=<%:id%>&test_id=<%:Model.test.Id%>&type_id=<%:var_FO_id%>" alt="Grafico"  />
                    <%   }
                       else
                       { %>
                        <img id="<%:demographic%>Chart" name="<%:demographic%>Chart" src="<%:source%>&graphic_id=<%:id%>&test_id=<%:Model.test.Id%>" alt="Grafico"  />
                    <%} %>--%>
                <div id="<%:demographic%>ChartDiv<%:var_FO_id%>" class="column span-24 google_chart"></div>
            </div>
        <!--Seccion derecha-->
<%--            <div class="generalDiv">
                <%    details = Model.graphics[order].GraphicDetails.ToList();                   
                       if (Model.UserLogged.Role.Name == "HRAdministrator")
                       {
                           %>
                           <div id="<%: demographic %><%: var_FO_id %>CommentsDiv">
                                <div data-role="fieldcontain">
                                    <label for="<%:demographic %><%:var_FO_id %>Title"><%: ViewRes.Views.ChartReport.Graphics.TableTitle%></label>
                                    <%: Html.TextBox(demographic + var_FO_id + "Title", Model.details[order].Title, new { @class = "input-background short" })%>
                                    <label for="<%:demographic %><%:var_FO_id %>XAxis"><%: ViewRes.Views.ChartReport.Graphics.AxisX%></label>
                                    <%: Html.TextBox(demographic + var_FO_id + "XAxis", Model.details[order].AxisXName, new { @class = "input-background short" })%>
                                    <label for="<%:demographic %><%:var_FO_id %>YAxis"><%: ViewRes.Views.ChartReport.Graphics.AxisY%></label>
                                    <%: Html.TextBox(demographic + var_FO_id + "YAxis", Model.details[order].AxisYName, new { @class = "input-background short" })%>
                                    <label for="<%:demographic %><%:var_FO_id %>CommentsEditor"><%: ViewRes.Views.ChartReport.Graphics.Comments%></label>
                                    <%:Html.TextArea(demographic + var_FO_id + "CommentsEditor", Model.details[order].Text, new { @class = "input-background textEditor" })%>
                                </div>
                                <input data-role="button" data-theme="f" id="<%: demographic %><%: var_FO_id %>Button" type="submit" value="<%: ViewRes.Views.Account.LogOn.LogOnButton %>" class="button" />
                            </div>
                           
                       <%}
                       else
                       {
                       //MOSTRAR COMENTARIOS
                           if (details.Count() > 0)
                           {%>
                            <div><strong><<%: ViewRes.Views.ChartReport.Graphics.Comments%></strong></div>
                            <div><%:Model.details[order].Text%></div>
                         <%}
                       }%>
               </div>--%>
        </div>
      <%}
       else
       {%>
            <%Html.RenderPartial("NoMinimum"); %>
       <%}%>
 