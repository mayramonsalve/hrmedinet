<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Medinet.Models.ViewModels.ChartReportViewModel>" %>
<%@ Import Namespace="MedinetClassLibrary.CustomClasses" %>
<%@ Import Namespace="MedinetClassLibrary.Models" %>

<% List<SummaryTable> totalList;
   List<SummaryTable> totalListCompare = new List<SummaryTable>();

   int test = Model.test.Id;
   int? compare_id = null; if (Model.testCompare != 0) compare_id = Model.testCompare;
   int elements_count = Model.test.OneQuestionnaire ? Model.GetCategoriesCount(Model.test.Questionnaire_Id.Value) : 1;
   string testCompareName = "";
   int id = Model.GetByDemographicAndType("Category", "Univariate").Id;
   int order = Model.GetByDemographicAndType("Category", "Univariate").Order;
   string source = Model.GetByDemographicAndType("Category", "Univariate").Source;
   string colorStyle;
   List<GraphicDetail> details;
   %> 
   <%: Html.Hidden("Categorygraphic_id", id)%>
   <div class="span-24 columns last">
   <%  if (Model.testCompare != 0)
       {
           compare_id = Model.testCompare;
           testCompareName = new MedinetClassLibrary.Services.TestsServices().GetById(Model.testCompare).Name;
       }
       bool graphic = elements_count < 10;
       totalList = Model.NameAverageSastNoSastForCategories(test, !graphic, compare_id, null);
   %> 
          <%: Html.Hidden("CategoryElementsCount", elements_count, new { id = "CategoryElementsCount" })%>
        <div class="span-24 column last">
            <fieldset >
                <legend><%: ViewRes.Views.ChartReport.Graphics.Parametres%></legend>
                <% if (!Model.test.OneQuestionnaire)
                   { %>
                <div class="span-24 last"><%: ViewRes.Views.ChartReport.Graphics.Questionnaire%>
                    <%: Html.DropDownList("CategoryGroupByQuestionnairesDDL", Model.questionnaires, ViewRes.Scripts.Shared.ShowAll, new { @class = "GroupByQuestionnairesDDL form-short " })%>
                </div>                               
                <%} %>
            </fieldset>
        </div>
        <div class="clear">&nbsp;</div>
          <%if (graphic)
            { %>
        <div class="span-15 column">
            <img id="CategoryChart" name="CategoryChart" src="<%:source%>&graphic_id=<%:id%>&test_id=<%:test%>&compare=<%:compare_id%>" alt="Grafico"  />
        </div>
        <!--Seccion derecha-->
        <div class="span-9 column last">
            <%    details = Model.graphics[order].GraphicDetails.ToList();                   
                   if (Model.UserLogged.Role.Name == "HRAdministrator")
                   {
                       %>
                       <div id="CategoryCommentsDiv">
                            <div class="span-24 column last button-padding-top">
                                <div class="span-3 column"><%: ViewRes.Views.ChartReport.Graphics.TableTitle%></div>
                                <div class="span-21 column last"><%: Html.TextBox("CategoryTitle", Model.details[order].Title, new { @class = "input-background short" })%></div>
                            </div>
                            <div class="span-24 column last button-padding-top">
                                <div class="span-3 column"><%: ViewRes.Views.ChartReport.Graphics.AxisX%></div>
                                <div class="span-21 column last"><%: Html.TextBox("CategoryXAxis", Model.details[order].AxisXName, new { @class = "input-background short" })%></div>
                            </div>
                            <div class="span-24 column last button-padding-top">
                                <div class="span-3 column"><%: ViewRes.Views.ChartReport.Graphics.AxisY%></div>
                                <div class="span-21 column last"><%: Html.TextBox("CategoryYAxis", Model.details[order].AxisYName, new { @class = "input-background short" })%></div>
                            </div>
                            <div class="span-24 column last button-padding-top">
                                <div class="span-24 column last"><%: ViewRes.Views.ChartReport.Graphics.Comments%></div>
                                <div class="span-24 column last"><%:Html.TextArea("CategoryCommentsEditor", Model.details[order].Text, new { @class = "input-background textEditor" })%></div>
                            </div>
                        </div>
                        <div id="login-submit" class="div-button column last button-padding-top">
	                        <input id="CategoryButton" type="submit" value="<%: ViewRes.Views.Account.LogOn.LogOnButton %>" class="button" />
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
        <%}%>
        <div class="span-8 column last">
            <% if (Model.testsToCompare.Count() > 0)
               { %>
                <div class="span-24 last"><%: ViewRes.Views.ChartReport.Graphics.Compare %></div>
                <div>
                    <%: Html.DropDownList("CategoryCompareDDL", Model.testsToCompare, ViewRes.Views.ChartReport.Graphics.None, new { @class = "CompareDDL form-short" })%>
                </div>
            <%} %>
        </div>
        <div class="clear">&nbsp;</div>
    <div class="span-24 column last">
    <%  string divClassSatNoSat = graphic ? "span-12 column last" : "span-15 column";
        if (compare_id.HasValue)
        {
            divClassSatNoSat = "span-24 column last";
        }
       %>
        <div id="CategoryDivSat" class="<%: divClassSatNoSat %>">
            <% string colspan = graphic ? "2" : "4"; %>
            <fieldset >
            <table id="CategoryTable"  class="display tabla">  
                    <thead>
                    <% if (compare_id.HasValue)
                       { %>
                      <tr>
                        <th rowspan="2" style="vertical-align:middle;"><%: ViewRes.Views.ChartReport.Graphics.CategoryTab %></th>
                        <% if (!graphic)
                         { %>
                        <th rowspan="2"></th>
                        <%} %>
                        <th colspan="<%:colspan %>"><%: Model.test.Name %></th>
                        <th colspan="<%:colspan %>"><%: testCompareName %></th>
                      <% if (!graphic)
                         { %>
                        <th rowspan="2"></th>
                        <%} %>
                      </tr>
                      <%} %>
                      <tr>
                      <% if (!compare_id.HasValue)
                         { %>
                            <th><%: ViewRes.Views.ChartReport.Graphics.CategoryTab %></th>
                         <%}
                         if (!graphic)
                         {%>
                            <th></th>
                            <th align="right"><%: ViewRes.Views.ChartReport.Graphics.Average%></th>
                            <th align="right"><%: ViewRes.Views.ChartReport.Graphics.Median%></th>
                        <%} %>
                        <th><%: ViewRes.Views.ChartReport.Graphics.Satisfied%></th>
                        <th><%: ViewRes.Views.ChartReport.Graphics.NoSatisfied%></th>
                        <%if (compare_id.HasValue)
                          {
                            if (!graphic)
                            {%>
                                <th><%: ViewRes.Views.ChartReport.Graphics.Average%></th>
                                <th><%: ViewRes.Views.ChartReport.Graphics.Median%></th>
                            <%} %>
                        <th><%: ViewRes.Views.ChartReport.Graphics.Satisfied%></th>
                        <th><%: ViewRes.Views.ChartReport.Graphics.NoSatisfied%></th>
                        <% if (graphic)
                        {  %>
                        <th></th>
                        <%} %>
                        <%} %>
                      </tr>
                    </thead>
                    <tbody>
                        <%foreach (SummaryTable table in totalList)
                          { %>
                           <tr>                   
                              <td style="color:Black; " ><%:table.Label%></td>
                              <%if (!graphic)
                                {
                                 colorStyle = "background-color:" + Model.GetColourByClimate(table.Average); %>
                                <td><span style=<%:colorStyle %>>&nbsp;&nbsp;&nbsp;</span></td>  
                                <td style="color:Black; text-align: right;"><%:(Math.Round(table.Average * 100) / 100).ToString()%></td> 
                                <td style="color:Black; text-align: right;"><%:(Math.Round(table.Median * 100) / 100).ToString()%></td> 
                              <%} %>
                              <td style="color:Green; text-align: right;"><%:table.Satisfied%></td>     
                              <td style="color:Red; text-align: right;"><%:table.NotSatisfied%></td> 
                              <% if (compare_id.HasValue)
                                 {
                                   if (!graphic)
                                   {%>
                                      <td style="color:Black; text-align: right;"><%:(Math.Round(table.AverageCompare * 100) / 100).ToString()%></td> 
                                      <td style="color:Black; text-align: right;"><%:(Math.Round(table.MedianCompare * 100) / 100).ToString()%></td> 
                                  <%} %>
                              <td style="color:Green; text-align: right;"><%:table.SatisfiedCompare%></td>     
                              <td style="color:Red; text-align: right;"><%:table.NotSatisfiedCompare%></td> 
                                    <% if(!graphic)
                                      {
                                         colorStyle = "background-color:" + Model.GetColourByClimate(table.AverageCompare); %>
                                        <td><span style=<%:colorStyle %>>&nbsp;&nbsp;&nbsp;</span></td>  
                                      <%} %>    
                                 <%} %>
                            </tr>
                        <%} %>
                    </tbody>
                </table>
            </fieldset>
        </div>
        <% if (!graphic)
           { %>
        <div class="span-9 column last">
            <%    details = Model.graphics[order].GraphicDetails.ToList();
                  if (Model.UserLogged.Role.Name == "HRAdministrator")
                  {
                       %>
                       <div id="CategoryCommentsDiv">
                            <div class="span-24 column last button-padding-top">
                                <div class="span-3 column"><%: ViewRes.Views.ChartReport.Graphics.TableTitle%></div>
                                <div class="span-21 column last"><%: Html.TextBox("CategoryTitle", Model.details[order].Title, new { @class = "input-background short" })%></div>
                            </div>
                            <div class="span-24 column last button-padding-top">
                                <div class="span-3 column"><%: ViewRes.Views.ChartReport.Graphics.AxisX%></div>
                                <div class="span-21 column last"><%: Html.TextBox("CategoryXAxis", Model.details[order].AxisXName, new { @class = "input-background short" })%></div>
                            </div>
                            <div class="span-24 column last button-padding-top">
                                <div class="span-3 column"><%: ViewRes.Views.ChartReport.Graphics.AxisY%></div>
                                <div class="span-21 column last"><%: Html.TextBox("CategoryYAxis", Model.details[order].AxisYName, new { @class = "input-background short" })%></div>
                            </div>
                            <div class="span-24 column last button-padding-top">
                                <div class="span-24 column last"><%: ViewRes.Views.ChartReport.Graphics.Comments%></div>
                                <div class="span-24 column last"><%:Html.TextArea("CategoryCommentsEditor", Model.details[order].Text, new { @class = "input-background textEditor" })%></div>
                            </div>
                        </div>
                        <div id="login-submit" class="div-button column last button-padding-top">
	                        <input id="CategoryButton" type="submit" value="<%: ViewRes.Views.Account.LogOn.LogOnButton %>" class="button" />
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
        <%} %>
<%--        <%if(!graphic)
        { %>
            <div class="span-10 column last">
                <% if (Model.testCompare != 0)
                    { %>
                    <%: ViewRes.Views.ChartReport.Graphics.Compare%><%: Model.testCompareName%>
                <%} %>
            </div>
        <%} %>--%>
    </div>
   </div>
   <div class="clear">&nbsp;</div>
   <div class="prepend-19 span-5 column last">
<%--        <%: Html.ActionLink(ViewRes.Views.ChartReport.Graphics.PrintGraphics + ViewRes.Views.ChartReport.Graphics.AndResult, "PdfUnivariate", new
        {
            test_id = Model.test.Id, graphic_id = id, elementsCount = elements_count,
            category_id=0, question_id=0, pValue=0,
            FO_id = 0, country_id = 0, compare_id = Model.testCompare
         }, new { id = "CategoryPrintLink" })%>--%>
   </div>