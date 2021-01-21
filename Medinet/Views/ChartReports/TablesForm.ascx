<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Medinet.Models.ViewModels.ChartReportViewModel>" %>
<%@ Import Namespace="MedinetClassLibrary.CustomClasses" %>
<%@ Import Namespace="MedinetClassLibrary.Models" %>
<% string demographic = ViewData["option"].ToString();
    string source = "", type = "";
   int id = 1, order = 1;
   List<SummaryTable> totalList;
   List<SummaryTable> totalListCompare = new List<SummaryTable>();
   MedinetClassLibrary.Models.ChiSquare chiSquare = new MedinetClassLibrary.Models.ChiSquare();
   MedinetClassLibrary.Models.ChiSquare chiSquareCompare = new MedinetClassLibrary.Models.ChiSquare();

   string var_FO_id = "";
   int FO_id=0;
   int? questionnaire = null;
   int? category = null;
   int? question = null;
   int? country = null;
   double pValue = 0;
   int test = Model.test.Id;
   int? compare_id = null; if (Model.testCompare != 0) compare_id = Model.testCompare;
   bool condition = true;
   int elements_count = 0;
   string testCompareName = "";
   List<string> keys;
       type = Model.chartType;
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
   %> 

   <div class="span-24 columns last"><%
   if (Model.chartType.CompareTo("Univariate") == 0 || Model.chartType.CompareTo("EditUnivariate") == 0)
   {
       if (Model.testCompare != 0)
       {
           compare_id = Model.testCompare;
           testCompareName = new MedinetClassLibrary.Services.TestsServices().GetById(Model.testCompare).Name;
       }
           if (demographic.CompareTo("FunctionalOrganizationType") == 0)
           {
               FO_id = Int32.Parse(ViewData["FO_id"].ToString());
               totalList = Model.NameAverageMedianSastNoSast(questionnaire, category, question, pValue, test, demographic, condition, FO_id, null);
               keys = totalList.Select(l => l.Label).ToList();
               if (compare_id.HasValue)
               {
                   totalListCompare = Model.NameAverageMedianSastNoSast(questionnaire, category, question, pValue, compare_id.Value, demographic, condition, FO_id, null);
                   keys = Model.GetKeysWhenCompare(totalList.Select(l => l.Label).ToList(), totalListCompare.Select(l => l.Label).ToList());
                   elements_count = totalList.Count > totalListCompare.Count ? totalListCompare.Count : totalList.Count;
               }
               else
                   elements_count = totalList.Count;
               if (elements_count >= 2)
               {
                   chiSquare = Model.getChiSquare(test, demographic, questionnaire, category, question, country, FO_id, 0.05);
                   if (compare_id.HasValue)
                       chiSquareCompare = Model.getChiSquare(compare_id.Value, demographic, questionnaire, category, question, country, FO_id, 0.05);
               }
               var_FO_id = FO_id.ToString();
           }
           else
           {
               totalList = Model.NameAverageMedianSastNoSast(questionnaire, category, question, pValue, test, demographic, condition, 0, compare_id);
               keys = totalList.Select(l => l.Label).ToList();
                   elements_count = totalList.Count;             
               if (elements_count >= 2)
               {
                   chiSquare = Model.getChiSquare(test, demographic, questionnaire, category, question, country, null, 0.05);
                   if (compare_id.HasValue)
                       chiSquareCompare = Model.getChiSquare(compare_id.Value, demographic, questionnaire, category, question, country, null, 0.05);
               }
           }
       %> 
       <%: Html.Hidden(demographic + "graphic_id" + var_FO_id, id)%>
          <%: Html.Hidden(demographic + "ElementsCount" + var_FO_id, elements_count, new { id = demographic + "ElementsCount" + var_FO_id })%>
    <% if (elements_count > 0)
       {%>
        <div class="span-24 column last">
            <fieldset >
                <legend><%: ViewRes.Views.ChartReport.Graphics.Parametres %></legend>
                <% if (!Model.test.OneQuestionnaire)
                   { %>
                <div class="span-24 last"><%: ViewRes.Views.ChartReport.Graphics.Questionnaire %>
                    <%: Html.DropDownList(demographic + "GroupByQuestionnairesDDL" + var_FO_id, Model.questionnaires, ViewRes.Scripts.Shared.ShowAll, new { @class = "GroupByQuestionnairesDDL form-short " })%>
                </div>
                <%} %>
                <div class="span-24 last"><%: ViewRes.Views.ChartReport.Graphics.Category %>
                    <%: Html.DropDownList(demographic + "GroupByCategoriesDDL" + var_FO_id, Model.GroupByCategories, ViewRes.Scripts.Shared.ShowAll, new { @class = "GroupByCategoriesDDL form-short " })%>
                </div>
                <div class="span-24 last"><%: ViewRes.Views.ChartReport.Graphics.Question %>
                    <%: Html.DropDownList(demographic + "GroupByQuestionsDDL" + var_FO_id, Model.question, ViewRes.Scripts.Shared.ShowAll, new { @class = "GroupByQuestionsDDL form-short" })%>
                </div>
            </fieldset>
        </div>
        <div class="clear">&nbsp;</div>
        <!--Seccion izquierda-->
       <%: Html.Hidden("Average", ViewRes.Views.ChartReport.Graphics.Average, "Average")%>
       <%: Html.Hidden("Median", ViewRes.Views.ChartReport.Graphics.Median, "Median")%>
       <%: Html.Hidden("Satisfied", ViewRes.Views.ChartReport.Graphics.Satisfied, "Satisfied")%>
       <%: Html.Hidden("NoSatisfied", ViewRes.Views.ChartReport.Graphics.NoSatisfied, "NoSatisfied")%>
       <%: Html.Hidden("ChiSquareValue", ViewRes.Views.ChartReport.Graphics.ChiSquareValue, "ChiSquareValue")%>
       <%: Html.Hidden("OurChiSquare", ViewRes.Views.ChartReport.Graphics.OurChiSquare, "OurChiSquare")%>
       <%: Html.Hidden("Conclusion", ViewRes.Views.ChartReport.Graphics.Conclusion, "Conclusion")%>
       <%: Html.Hidden("countOptions",Model.optionsCount)%>

       <% string divClass = "span-14 column";
           if (compare_id.HasValue)
          {
              divClass = "span-23 append-1 column last";
          }
          
           %>
         
        <div id="divTable<%:demographic %><%:var_FO_id%>" class="<%: divClass %>">
           <%if (Model.details != null && Model.details[order] != null)
             { %>
           <h4 id="h4Title<%:demographic %><%:var_FO_id%>"><%:Model.details[order].Title%></h4>
           <br />
           <%} %>
            <div style="border: 1px solid Black;">
                    <table id="<%=demographic %><%:var_FO_id%>Table" class="display tabla" >
                        <thead>
                        <% if (compare_id.HasValue)
                           { %>
                          <tr>
                            <th rowspan="2"></th>
                            <th colspan="4"><%: Model.test.Name %></th>
                            <th colspan="4"><%: testCompareName %></th>
                          </tr>
                          <%} %>
                            <tr>
                            <% if (!compare_id.HasValue)
                               { %>
                                <th><%--<%: ViewRes.Views.ChartReport.Graphics.Name%>--%></th>
                                <%} %>
                                <th align="right"><%: ViewRes.Views.ChartReport.Graphics.Average %></th>
                                <th align="right"><%: ViewRes.Views.ChartReport.Graphics.Median %></th>
                                <th align="right"><%: ViewRes.Views.ChartReport.Graphics.Satisfied %></th>
                                <th align="right"><%: ViewRes.Views.ChartReport.Graphics.NoSatisfied %></th>
                                <% if (compare_id.HasValue)
                                   { %>
                                <th align="right"><%: ViewRes.Views.ChartReport.Graphics.Average %></th>
                                <th align="right"><%: ViewRes.Views.ChartReport.Graphics.Median %></th>
                                <th align="right"><%: ViewRes.Views.ChartReport.Graphics.Satisfied %></th>
                                <th align="right"><%: ViewRes.Views.ChartReport.Graphics.NoSatisfied %></th>
                                 <%} %>
                            </tr>
                        </thead>
                        <tbody>
                         
                           <%foreach (var objeList in totalList ) {%>
                            <% var average = (Math.Round(objeList.Average * 100) / Model.optionsCount).ToString();
                               var averageCompare = (Math.Round(objeList.AverageCompare * 100) / Model.optionsCount).ToString(); %>
                            <tr>
                                <td style="color:Black;"><%:Html.Label(objeList.Label)%></td> 
                                <td style="color:Black; text-align: center;"><%=average%></td> 
                                <td style="color:Black; text-align: center;"><%=objeList.Median.ToString()%></td> 
                                <td style="color:Green; text-align: center;"><%=objeList.Satisfied.ToString()%></td> 
                                <td style="color:Red; text-align: center;"><%=objeList.NotSatisfied.ToString()%></td> 
                                <% if (compare_id.HasValue)
                                 {%>
                                <td style="color:Black; text-align: center;"><%=averageCompare%></td> 
                                <td style="color:Black; text-align: center;"><%=objeList.MedianCompare.ToString()%></td> 
                                <td style="color:Green; text-align: center;"><%=objeList.SatisfiedCompare.ToString()%></td> 
                                <td style="color:Red; text-align: center;"><%=objeList.NotSatisfiedCompare.ToString()%></td>
                                 <%} %>
                           </tr>
                            <%} %>
                        </tbody>          
                      </table>
            </div>
        </div>
    <!--Seccion tablas-->
    <div class="clear">&nbsp;</div>
    <div class="span-24 column last">
        <div class="span-7 column">
        <%if (elements_count >= 2)
          {
              string significanceTip = ViewRes.Views.ChartReport.Graphics.SignificanceTip; %>
             <span class=".tool" title="<%: significanceTip %>"><%: ViewRes.Views.ChartReport.Graphics.SignificanceLevel%></span>
             <% if (demographic.CompareTo("FunctionalOrganizationType") == 0)
                { %>
                <span class=".tool" title="<%: significanceTip %>"><%=Html.RadioButton(FO_id + demographic + "PValue", "0.01", false, new { @id = demographic + "0.01", @class = "PValue" })%> 0.01</span>
                <span class=".tool" title="<%: significanceTip %>"><%=Html.RadioButton(FO_id + demographic + "PValue", "0.05", true, new { @id = demographic + "0.05", @class = "PValue" })%> 0.05</span>
                <span class=".tool" title="<%: significanceTip %>"><%=Html.RadioButton(FO_id + demographic + "PValue", "0.1", false, new { @id = demographic + "0.1", @class = "PValue" })%> 0.1</span>
            <%}
                else
                { %>
                <span class=".tool" title="<%: significanceTip %>"><%=Html.RadioButton(demographic + "PValue", "0.01", false, new { @id = demographic + "0.01", @class = "PValue" })%> 0.01</span>
                <span class=".tool" title="<%: significanceTip %>"><%=Html.RadioButton(demographic + "PValue", "0.05", true, new { @id = demographic + "0.05", @class = "PValue" })%> 0.05</span>
                <span class=".tool" title="<%: significanceTip %>"><%=Html.RadioButton(demographic + "PValue", "0.1", false, new { @id = demographic + "0.1", @class = "PValue" })%> 0.1</span>
            <%} %>   
        <%}
        else
        {%>
        <br />
        <%} %>
        </div>  
        <%if (!compare_id.HasValue)
          {%>
        <div class="span-7 column append-10 last">
        <%}
          else
          { %>
                <div class="span-17 column last">
          <%} %>
            <div class="alignRight">
                <% if (Model.testsToCompare.Count() > 0)
                   { %>
                    <div class="span-24 last"><%: ViewRes.Views.ChartReport.Graphics.Compare%>
                        <%: Html.DropDownList(demographic + "CompareDDL" + var_FO_id, Model.testsToCompare, ViewRes.Views.ChartReport.Graphics.None, new { @class = "CompareDDL form-short" })%>
                    </div>
                <%}
                else
                {%>
                    <%: Html.Hidden(demographic + "CompareDDL" + var_FO_id, null, new { id = demographic + "CompareDDL" + var_FO_id })%>
                <%} %>
            </div>
        </div>
    </div>        
    <div class="clear">&nbsp;</div>    
       <%
        string divClassChiSquare = "span-12 column last";
        if (compare_id.HasValue)
            divClassChiSquare = "span-24 column last";
       %>
        <div id="<%:demographic%><%:var_FO_id%>DivChi" class="<%: divClassChiSquare %>">
        <%if (elements_count >= 2)
          { %>
        <fieldset >
            <% if (demographic.CompareTo("FunctionalOrganizationType") == 0)
               { %>
                <table id="<%:demographic%><%:var_FO_id%>TableChiSquare" class="display tabla">
                <%}
               else
               { %>
                <table id="<%:demographic%>TableChiSquare"  class="display tabla">
                <%} %>
                     <thead>
                        <tr>
                            <% if (compare_id.HasValue)
                               { %>
                               <th></th>
                               <%} %>
                             <th><%: ViewRes.Views.ChartReport.Graphics.ChiSquareValue%></th>
                             <th><%: ViewRes.Views.ChartReport.Graphics.OurChiSquare%></th>
                             <th><%: ViewRes.Views.ChartReport.Graphics.Conclusion%></th>
                        </tr>
                     </thead>  
                        <tr>
                            <% if (compare_id.HasValue)
                               { %>
                               <td><%: Model.test.Name %></td>
                               <%} %>
                            <td style="text-align: center;"><%=(Math.Round(chiSquare.ChiSquareValue * 100) / 100).ToString()%></td>
                            <td style="text-align: center;"><%=(Math.Round(chiSquare.OurChiSquare * 100) / 100).ToString()%></td>
                            <td style="text-align: left;"><%= chiSquare.Conclusion%></td>
                        </tr>
                        <% if (compare_id.HasValue)
                          { %>
                        <tr>
                            <td><%: testCompareName %></td>
                            <td style="text-align: center;"><%=(Math.Round(chiSquareCompare.ChiSquareValue * 100) / 100).ToString()%></td>
                            <td style="text-align: center;"><%=(Math.Round(chiSquareCompare.OurChiSquare * 100) / 100).ToString()%></td>
                            <td style="text-align: left;"><%= chiSquareCompare.Conclusion%></td>
                        </tr>
                        <%} %>
                </table>
            </fieldset >
        <%} %>
        </div>
<%--        <div>
        <%
           List<GraphicDetail> details = Model.graphics[order].GraphicDetails.ToList();
            if (Model.UserLogged.Role.Name == "HRAdministrator")
            { %>
                <div id="<%: demographic %><%: var_FO_id %>CommentsDiv">
                    <div class="span-24 column last button-padding-top">
                        <div class="span-3 column"><%: ViewRes.Views.ChartReport.Graphics.TableTitle%></div>
                        <div class="span-21 column last"><%: Html.TextBox(demographic + var_FO_id + "Title", Model.details[order].Title, new { @class = "input-background short" })%></div>
                    </div>
                    <div class="span-24 column last button-padding-top">
                        <div class="span-24 column last"><%: ViewRes.Views.ChartReport.Graphics.Comments%></div>
                        <div class="span-24 column last"><%:Html.TextArea(demographic + var_FO_id + "CommentsEditor", Model.details[order].Text, new { @class = "input-background textEditor" })%></div>
                    </div>
                </div>
                <div id="<%: demographic %><%: var_FO_id %>Button" class="div-button column last button-padding-top">
	                <input id="Submit1" type="submit" value="<%: ViewRes.Views.Account.LogOn.LogOnButton %>" class="button" />
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
      </div>--%>
   <%}
       else
       {%>
            <%Html.RenderPartial("NoMinimum"); %>
       <%}
   }%>
   </div>
      <% if (elements_count > 0)
      {%>
<%--      <div class="clear">&nbsp;</div>
   <div class="prepend-19 span-5 column last">
        <%: Html.ActionLink(ViewRes.Views.ChartReport.Graphics.PrintGraphics + ViewRes.Views.ChartReport.Graphics.AndResult, "PdfUnivariate", new
        {
            test_id = Model.test.Id, graphic_id = id, elementsCount = elements_count,
            category_id=0, question_id=0, pValue=0.05,
            FO_id = FO_id, country_id = 0, compare_id = Model.testCompare
         }, new { id = demographic+var_FO_id+"PrintLink" })%>
   </div>--%>
    <%} %>

    <script type="text/javascript">
        InitializeDataTable();
    </script>