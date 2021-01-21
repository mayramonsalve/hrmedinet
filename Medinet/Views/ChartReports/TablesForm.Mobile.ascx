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

   <%
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
        <!--Seccion izquierda-->
       <%: Html.Hidden("Average", ViewRes.Views.ChartReport.Graphics.Average, "Average")%>
       <%: Html.Hidden("Median", ViewRes.Views.ChartReport.Graphics.Median, "Median")%>
       <%: Html.Hidden("Satisfied", ViewRes.Views.ChartReport.Graphics.Satisfied, "Satisfied")%>
       <%: Html.Hidden("NoSatisfied", ViewRes.Views.ChartReport.Graphics.NoSatisfied, "NoSatisfied")%>
       <%: Html.Hidden("ChiSquareValue", ViewRes.Views.ChartReport.Graphics.ChiSquareValue, "ChiSquareValue")%>
       <%: Html.Hidden("OurChiSquare", ViewRes.Views.ChartReport.Graphics.OurChiSquare, "OurChiSquare")%>
       <%: Html.Hidden("Conclusion", ViewRes.Views.ChartReport.Graphics.Conclusion, "Conclusion")%>

       <% string divClass = "span-14 column";
           if (compare_id.HasValue)
          {
              divClass = "span-23 append-1 column last";
          } %>

        <div id="divTable<%:demographic %><%:var_FO_id%>" class="<%: divClass %>">
            <br />
            <table data-role="table" data-column-btn-text="<%: ViewRes.Views.ChartReport.Graphics.Columns %>" data-mode="columntoggle" data-column-btn-theme="f" id="<%=demographic %><%:var_FO_id%>Table" class="table1" >
                <thead>
                <% if (compare_id.HasValue)
                    { %>
                    <tr>
                    <th rowspan="2"></th>
                    <td colspan="4"><%: Model.test.Name %></td>
                    <td colspan="4"><%: testCompareName %></td>
                    </tr>
                    <%} %>
                    <tr>
                    <% if (!compare_id.HasValue)
                        { %>
                        <th data-priority="1" class="ui-table-cell-visible" style="visibility:hidden"><%--<%: ViewRes.Views.ChartReport.Graphics.Name%>--%><%:demographic %></th>
                        <%} %>
                        <th data-priority="1" class="ui-table-cell-visible"><%: ViewRes.Views.ChartReport.Graphics.Average %></th>
                        <th data-priority="1"><%: ViewRes.Views.ChartReport.Graphics.Median %></th>
                        <th data-priority="1"><%: ViewRes.Views.ChartReport.Graphics.Satisfied %></th>
                        <th data-priority="1"><%: ViewRes.Views.ChartReport.Graphics.NoSatisfied %></th>
                        <% if (compare_id.HasValue)
                            { %>
                        <th data-priority="1" class="ui-table-cell-visible"><%: ViewRes.Views.ChartReport.Graphics.Average %></th>
                        <th data-priority="1"><%: ViewRes.Views.ChartReport.Graphics.Median %></th>
                        <th data-priority="1"><%: ViewRes.Views.ChartReport.Graphics.Satisfied %></th>
                        <th data-priority="1"><%: ViewRes.Views.ChartReport.Graphics.NoSatisfied %></th>
                            <%} %>
                    </tr>
                </thead>
                <tbody>
                    <%foreach (var objeList in totalList ) {%>
                    <% var average = (Math.Round(objeList.Average*100)/100).ToString();
                        var averageCompare = (Math.Round(objeList.AverageCompare * 100) / 100).ToString(); %>
                    <tr>
                        <th class="ui-table-cell-visible"><%:Html.Label(objeList.Label)%></th> 
                        <td class="ui-table-cell-visible"><%=average%></td> 
                        <td><%=objeList.Median.ToString()%></td> 
                        <td><%=objeList.Satisfied.ToString()%></td> 
                        <td><%=objeList.NotSatisfied.ToString()%></td> 
                        <% if (compare_id.HasValue)
                            {%>
                        <td class="ui-table-cell-visible"><%=averageCompare%></td> 
                        <td><%=objeList.MedianCompare.ToString()%></td> 
                        <td><%=objeList.SatisfiedCompare.ToString()%></td> 
                        <td><%=objeList.NotSatisfiedCompare.ToString()%></td>
                            <%} %>
                    </tr>
                    <%} %>
                </tbody>          
            </table>
        </div>
    <!--Seccion tablas-->
<div class="principal">
    <div class="chartDiv portrait">
        <%if (elements_count >= 2)
          {
              string significanceTip = ViewRes.Views.ChartReport.Graphics.SignificanceTip; %>
                <div><strong><span class=".tool" title="<%: significanceTip %>"><%: ViewRes.Views.ChartReport.Graphics.SignificanceLevel%>&nbsp</span></strong></div>
                <div>
                    <fieldset data-role="controlgroup" data-type="horizontal" style="float:left;">
                            <% if (demographic.CompareTo("FunctionalOrganizationType") == 0)
                            { %>
                            <label for="<%:demographic %>0.01">0.01</label>
                            <%=Html.RadioButton(FO_id + demographic + "PValue", "0.01", false, new { @id = demographic + "0.01", @class = "PValue", onclick = "PValue('"+ FO_id + demographic + "PValue');" })%>
                            <label for="<%:demographic %>0.05">0.05</label>
                            <%=Html.RadioButton(FO_id + demographic + "PValue", "0.05", true, new { @id = demographic + "0.05", @class = "PValue", onclick = "PValue('" + FO_id + demographic + "PValue');" })%>
                            <label for="<%:demographic %>0.1">0.1</label>
                            <%=Html.RadioButton(FO_id + demographic + "PValue", "0.1", false, new { @id = demographic + "0.1", @class = "PValue", onclick = "PValue('" + FO_id + demographic + "PValue');" })%>
                        <%}
                            else
                            { %>
                            <label for="<%:demographic %>0.01">0.01</label>
                            <%=Html.RadioButton(demographic + "PValue", "0.01", false, new { @id = demographic + "0.01", @class = "PValue", onclick = "PValue('" + demographic + "PValue');" })%>
                            <label for="<%:demographic %>0.05">0.05</label>
                            <%=Html.RadioButton(demographic + "PValue", "0.05", true, new { @id = demographic + "0.05", @class = "PValue", onclick = "PValue('" + demographic + "PValue');" })%>
                            <label for="<%:demographic %>0.1">0.1</label> 
                            <%=Html.RadioButton(demographic + "PValue", "0.1", false, new { @id = demographic + "0.1", @class = "PValue", onclick = "PValue('" + demographic + "PValue');" })%>
                        <%} %>  
                    </fieldset>
                </div> 
        <%} %>
    </div>  
        <% if (Model.testsToCompare.Count() > 0)
        { %>
    <div class="generalDiv">
        <div><strong><%: ViewRes.Views.ChartReport.Graphics.Compare%></strong></div>
        <div>
            <%: Html.DropDownList(demographic + "CompareDDL" + var_FO_id, Model.testsToCompare, ViewRes.Views.ChartReport.Graphics.None, new { @class = "CompareDDL form-short" })%>
        </div>
    </div>
        <%}
        else
        {%>
        <%: Html.Hidden(demographic + "CompareDDL" + var_FO_id, null, new { id = demographic + "CompareDDL" + var_FO_id })%>
        <%} %>        
    <%
        string divClassChiSquare = "span-12 column last";
        if (compare_id.HasValue)
            divClassChiSquare = "span-24 column last";
    %>
    <div id="<%:demographic%><%:var_FO_id%>DivChi" class="<%: divClassChiSquare %> chartDiv portrait">
        <%if (elements_count >= 2)
          { %>
            <% if (demographic.CompareTo("FunctionalOrganizationType") == 0)
               { %>
                <table id="<%:demographic%><%:var_FO_id%>TableChiSquare" class="table1">
                <%}
               else
               { %>
                <table id="<%:demographic%>TableChiSquare"  class="table1">
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
                            <td style="text-align: right;"><%=(Math.Round(chiSquare.ChiSquareValue * 100) / 100).ToString()%></td>
                            <td style="text-align: right;"><%=(Math.Round(chiSquare.OurChiSquare * 100) / 100).ToString()%></td>
                            <td style="text-align: left;"><%= chiSquare.Conclusion%></td>
                        </tr>
                        <% if (compare_id.HasValue)
                          { %>
                        <tr>
                            <td><%: testCompareName %></td>
                            <td style="text-align: right;"><%=(Math.Round(chiSquareCompare.ChiSquareValue * 100) / 100).ToString()%></td>
                            <td style="text-align: right;"><%=(Math.Round(chiSquareCompare.OurChiSquare * 100) / 100).ToString()%></td>
                            <td style="text-align: left;"><%= chiSquareCompare.Conclusion%></td>
                        </tr>
                        <%} %>
                </table>
        <%} %>
    </div>
    <div class="generalDiv">
        <%
           List<GraphicDetail> details = Model.graphics[order].GraphicDetails.ToList();
            if (Model.UserLogged.Role.Name == "HRAdministrator")
            { %>
                <div id="<%: demographic %><%: var_FO_id %>CommentsDiv">
                    <fieldset data-role="fieldcontain">
                        <legend></legend>
                        <label for="<%:demographic %><%:var_FO_id %>Title"><%: ViewRes.Views.ChartReport.Graphics.TableTitle%></label>
                        <%: Html.TextBox(demographic + var_FO_id + "Title", Model.details[order].Title)%>
                        <label for="<%:demographic %><%:var_FO_id %>CommentsEditor"><%: ViewRes.Views.ChartReport.Graphics.Comments%></label>
                        <%:Html.TextArea(demographic + var_FO_id + "CommentsEditor", Model.details[order].Text)%>
                    </fieldset>
                </div>
	            <input data-role="button" data-theme="f"  id="<%: demographic %><%: var_FO_id %>Button" type="submit" value="<%: ViewRes.Views.Account.LogOn.LogOnButton %>" class="button"/>
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
      
   <%}
       else
       {%>
            <%Html.RenderPartial("NoMinimum"); %>
       <%}
   }%>
    </div>
   </div>

    <script type="text/javascript">
        $(".table1").tablesorter(); 
    </script>