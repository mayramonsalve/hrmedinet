<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Medinet.Models.ViewModels.ChartReportViewModel>" %>
<%@ Import Namespace="MedinetClassLibrary.CustomClasses" %>
<%@ Import Namespace="MedinetClassLibrary.Models" %>

<% string demographic = ViewData["option"].ToString();
   string source = "", type = Model.chartType;
   int id = 1, order = 1;

   string var_FO_id = "";
   int FO_id=0;
   int test = Model.test.Id;
   int elements_count = 1;
   for (int i = 0; i < Model.graphics.Length; i++)
   {
       if (Model.graphics[i].Demographic.CompareTo(demographic) == 0 && Model.graphics[i].Type.CompareTo(type) == 0)
       {
           id = Model.graphics[i].Id;
           order = Model.graphics[i].Order;
           source = Model.graphics[i].Source.ToString();
           i = Model.graphics.Length;
       }
   }
   if (demographic.CompareTo("FunctionalOrganizationType") == 0)
   {
       FO_id = Int32.Parse(ViewData["FO_id"].ToString());
       var_FO_id = FO_id.ToString();
   } 
   %> 

   <div class="span-24 column last">
           <%: Html.Hidden(demographic + "graphic_id" + var_FO_id, id)%>
           <%: Html.Hidden(demographic + "ElementsCount" + var_FO_id, elements_count, new { id = demographic + "ElementsCount" + var_FO_id })%>
    <% if (elements_count > 0)
       {%>
        <div class="span-24 column last">
            <fieldset >
                <legend><b><%: ViewRes.Views.ChartReport.Graphics.Parametres%></b></legend>   
                <% if (!Model.test.OneQuestionnaire)
                   { %>                      
                <div class="span-24 last"><%: ViewRes.Views.ChartReport.Graphics.Questionnaire%>
                    <%: Html.DropDownList(demographic + "GroupByQuestionnairesDDL" + var_FO_id, Model.questionnaires, ViewRes.Scripts.Shared.ShowAll, new { @class = "GroupByQuestionnairesDDL form-short " })%>
                </div> 
                <%} %>                       
                <div class="span-24 last"><%: ViewRes.Views.ChartReport.Graphics.Category%>
                    <%: Html.DropDownList(demographic + "GroupByCategoriesDDL" + var_FO_id, Model.GroupByCategories, ViewRes.Scripts.Shared.ShowAll, new { @class = "GroupByCategoriesDDL form-short " })%>
                </div>
                <div class="span-24 last"><%: ViewRes.Views.ChartReport.Graphics.Question%>
                    <%: Html.DropDownList(demographic + "GroupByQuestionsDDL" + var_FO_id, Model.question, ViewRes.Scripts.Shared.ShowAll, new { @class = "GroupByQuestionsDDL form-short" })%>
                </div>
            </fieldset>
        </div>
        <div class="clear">&nbsp;</div>
        <div class="span-15 column">
        <!--Seccion izquierda-->
                <% if (demographic.CompareTo("Country") == 0)
                   { %>
                    <img id="<%:demographic%>Chart" name="<%:demographic%>Chart" src="<%= Url.Action("FrequencyChartByCountry", new {chartSize= "Screen", chartType="Bar"}) %>" usemap="#MyMap2" alt="Graph" />
                    <% Html.RenderAction("FrequencyChartMapByCountry", new { chartSize = "Screen", chartType = "Bar", graphic_id = id, name = "MyMap2", test_id = Model.test.Id }); %>
                <%  }
                   else if (demographic.CompareTo("FunctionalOrganizationType") == 0)
                   { %>
                         <img id="<%:demographic%>Chart<%:FO_id%>" name="<%:demographic%>Chart" src="<%:source%>&graphic_id=<%:id%>&test_id=<%:Model.test.Id%>&type_id=<%:FO_id%>" alt="Grafico"  />
                <%   }
                   else
                   { %>
                    <img id="<%:demographic%>Chart" name="<%:demographic%>Chart" src="<%:source%>&graphic_id=<%:id%>&test_id=<%:Model.test.Id%>" alt="Grafico"  />
                <%} %>

        </div>
        <!--Seccion derecha-->
        <div class="span-9 column last">
            <%    List<GraphicDetail> details = Model.graphics[order].GraphicDetails.ToList();                   
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
                   }%>
        </div>
   <%}
       else
       {%>
            <%Html.RenderPartial("NoMinimum"); %>
       <%}%>
   </div>
   <% if (elements_count > 0)
      {%>
<%--      <div class="clear">&nbsp;</div>
   <div class="prepend-19 span-5 column last">
        <%: Html.ActionLink(ViewRes.Views.ChartReport.Graphics.PrintGraphics + ViewRes.Views.ChartReport.Graphics.AndResult, "PdfUnivariate", new
        {
            test_id = Model.test.Id,
            graphic_id = id,
            elementsCount = elements_count,
            category_id = 0,
            question_id = 0,
            pValue = 0.05,
            FO_id = FO_id,
            country_id = 0,
            compare_id = Model.testCompare
        }, new { id = demographic + var_FO_id + "PrintLink" })%>
   </div>--%>
   <%} %>