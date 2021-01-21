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
   <div class="span-24 columns last">
       <% if (elements_count)
       {
           if (demographic != "General" || !Model.test.OneQuestionnaire)
           {%>
            <div class="span-24 column last">
                <fieldset >
                    <legend><b><%: ViewRes.Views.ChartReport.Graphics.Parametres%></b></legend>     
                    <% if (!Model.test.OneQuestionnaire)
                       { %>
                    <div class="span-24 last"><%: ViewRes.Views.ChartReport.Graphics.Questionnaire%>
                        <%: Html.DropDownList(demographic + "GroupByCategoryQuestionnairesDDL" + var_FO_id, Model.questionnaires, ViewRes.Scripts.Shared.ShowAll, new { @class = "GroupByCategoryQuestionnairesDDL form-short " })%>
                    </div>                               
                    <%} %>   
                    <% if (demographic != "General")
                       { %>                             
                    <div class="span-24 last"><%: Model.GetParameterNameByDemographic(demographic, var_FO_id)%>
                        <%: Html.DropDownList(demographic + "GroupByDemographicDDL" + var_FO_id, Model.GroupByCategories, ViewRes.Scripts.Shared.ShowAll, new { @class = "GroupByDemographicDDL form-short " })%>
                    </div>
                    <%} %>   
                </fieldset>
            </div>
        <%} %>
        <div class="clear">&nbsp;</div>
       <%: Html.Hidden("DownloadImage", ViewRes.Views.ChartReport.Graphics.DownloadImage)%>
          <%: Html.Hidden(demographic + "ElementsCount" + var_FO_id, elements_count, new { id = demographic + "ElementsCount" + var_FO_id })%>
        <div class="span-24 column last">
<%--                <% if (demographic.CompareTo("Country") == 0)
                   { %>
                    <img id="<%:demographic%>Chart" name="<%:demographic%>Chart" src="<%= Url.Action("CategoryChartByCountry", new {chartSize= "Screen", chartType="Column"}) %>" usemap="#MyMap2" alt="Graph" style="width: 600px;" />
                    <% Html.RenderAction("CategoryChartMapByCountry", new { chartSize = "Screen", chartType = "Column", graphic_id = id, name = "MyMap2", test_id = Model.test.Id }); %>
                <%  }
                   else if (demographic.CompareTo("FunctionalOrganizationType") == 0)
                   { %>
                         <img id="<%:demographic%>Chart<%:var_FO_id%>" name="<%:demographic%>Chart" src="<%:source%>&graphic_id=<%:id%>&test_id=<%:Model.test.Id%>&type_id=<%:var_FO_id%>" alt="Grafico" style="width: 600px;" />
                <%   }
                   else
                   { %>
                    <img id="<%:demographic%>Chart" name="<%:demographic%>Chart" src="<%:source%>&graphic_id=<%:id%>&test_id=<%:Model.test.Id%>" alt="Grafico" style="width: 600px;" />
                <%} %>--%>
                <div id="<%:demographic%>ChartDiv<%:var_FO_id%>" class="column span-12 google_chart"></div>
                <div class="clear"></div>
                <div id="<%:demographic%>Img<%:var_FO_id%>" class="span-24 google_img"></div>
        </div>
        <!--Seccion derecha-->
        <%--<div class="span-9 column last">--%>
<%--            <%    details = Model.graphics[order].GraphicDetails.ToList();                   
                   if (Model.UserLogged.Role.Name == "HRAdministrator")
                   {
                       %>
                       <div id="<%: demographic %><%: var_FO_id %>CommentsDiv">
                            <div class="span-24 column last button-padding-top">
                                <div class="span-3 column"><%: ViewRes.Views.ChartReport.Graphics.TableTitle%></div>
                                <div class="span-21 column last"><%: Html.TextBox(demographic + var_FO_id + "Title", Model.details[order].Title, new { @class = "input-background short" })%></div>
                            </div>
                            <div class="span-24 column last button-padding-top">
                                <div class="span-3 column"><%: ViewRes.Views.ChartReport.Graphics.AxisX%></div>
                                <div class="span-21 column last"><%: Html.TextBox(demographic + var_FO_id + "XAxis", Model.details[order].AxisXName, new { @class = "input-background short" })%></div>
                            </div>
                            <div class="span-24 column last button-padding-top">
                                <div class="span-3 column"><%: ViewRes.Views.ChartReport.Graphics.AxisY%></div>
                                <div class="span-21 column last"><%: Html.TextBox(demographic + var_FO_id + "YAxis", Model.details[order].AxisYName, new { @class = "input-background short" })%></div>
                            </div>
                            <div class="span-24 column last button-padding-top">
                                <div class="span-24 column last"><%: ViewRes.Views.ChartReport.Graphics.Comments%></div>
                                <div class="span-24 column last"><%:Html.TextArea(demographic + var_FO_id + "CommentsEditor", Model.details[order].Text, new { @class = "input-background textEditor" })%></div>
                            </div>
                        </div>
                        <div id="login-submit" class="div-button column last button-padding-top">
	                        <input id="<%: demographic %><%: var_FO_id %>Button" type="submit" value="<%: ViewRes.Views.Account.LogOn.LogOnButton %>" class="button" />
                        </div>
                   <%}
                   else
                   {
                   //MOSTRAR COMENTARIOS
                       if (details.Count() > 0)
                       {%>
                        <div><%: ViewRes.Views.ChartReport.Graphics.Comments%></div>
                        <div><%:Model.details[order].Text%></div>
                     <%}
                   }%>--%>
           <%--</div>--%>
      <%}
       else
       {%>
            <%Html.RenderPartial("NoMinimum"); %>
       <%}%>
    </div>
<%--   <div class="clear">&nbsp;</div>
   <div class="prepend-19 span-5 column last">--%>
<%--        <%: Html.ActionLink(ViewRes.Views.ChartReport.Graphics.PrintGraphics + ViewRes.Views.ChartReport.Graphics.AndResult, "PdfUnivariate", new
        {
            test_id = Model.test.Id, graphic_id = id, elementsCount = elements_count,
            category_id=0, question_id=0, pValue=0,
            FO_id = 0, country_id = 0, compare_id = Model.testCompare
         }, new { id = "CategoryPrintLink" })%>--%>
   <%--</div>--%>