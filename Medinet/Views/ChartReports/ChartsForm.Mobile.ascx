<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Medinet.Models.ViewModels.ChartReportViewModel>" %>
<%@ Import Namespace="MedinetClassLibrary.CustomClasses" %>
<%@ Import Namespace="MedinetClassLibrary.Models" %>

<% string demographic = ViewData["option"].ToString();
   string source = "", type = Model.chartType;
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
   double? pValue = null;
   int test = Model.test.Id;
   int? compare_id = null; if (Model.testCompare != 0) compare_id = Model.testCompare;
   bool condition = false;
   int elements_count = 0;
   string testCompareName = "";
   List<string> keys;
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

    <%  if (Model.testCompare != 0)
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

    <div class="principal">

        <div class="chartDiv portrait">
        <!--Seccion izquierda-->
        <%--        <% if (demographic.CompareTo("Country") == 0)
                   { %>
                    <img id="<%:demographic%>Chart" name="<%:demographic%>Chart" src="<%= Url.Action("UniVariateChartByCountry", new {chartSize= "Screen", chartType="Column"}) %>" usemap="#MyMap2" alt="Graph" />
                    <% Html.RenderAction("UniVariateChartMapByCountry", new { chartSize = "Screen", chartType = "Column", graphic_id = Model.graphics[17].Id, name = "MyMap2", test_id = Model.test.Id, compare = compare_id }); %>
                <%  }
                   else if (demographic.CompareTo("FunctionalOrganizationType") == 0)
                   { %>
                         <img id="<%:demographic%>Chart<%:FO_id%>" name="<%:demographic%>Chart" src="<%:source%>&graphic_id=<%:id%>&test_id=<%:Model.test.Id%>&type_id=<%:FO_id%>&compare=<%:compare_id%>" alt="Grafico"  />
                <%   }
                   else
                   { %>
                    <img id="<%:demographic%>Chart" name="<%:demographic%>Chart" src="<%:source%>&graphic_id=<%:id%>&test_id=<%:Model.test.Id%>&compare=<%:compare_id%>" alt="Grafico"  />
                <%} %>--%>
            <div id="<%:demographic%>ChartDiv<%:var_FO_id%>" class="column span-24 google_chart"></div>
        </div>

        <!--Seccion derecha-->
<%--        <div class="generalDiv">
            <%    List<GraphicDetail> details = Model.graphics[order].GraphicDetails.ToList(); 
                              
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
                        <div><strong><%: ViewRes.Views.ChartReport.Graphics.Comments%></strong></div>
                        <div><%:Model.details[order].Text%></div>
                     <%}
                   }%>

        </div>--%>


    <!--Seccion tablas-->
    <% string styleCompare = "span-24 column last";  %>
    <% if (elements_count >= 2)
    {
        styleCompare = "span-12 column last alignRight";
        string significanceTip = ViewRes.Views.ChartReport.Graphics.SignificanceTip;
        string styleSignificance = "span-24 column last";
        if (Model.testsToCompare.Count() > 0) styleSignificance = "span-12 column";
        %>
        <div class="chartDiv portrait"> 
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
        </div>
    <%}%>
    <% if (Model.testsToCompare.Count() > 0)
        { %>
            <div class="<%: styleCompare %> generalDiv">
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
      
          
      <% string divClassSatNoSat = "span-12 column";
      string divClassChiSquare = "span-12 column last";
      if (compare_id.HasValue)
      {
          divClassSatNoSat = "span-24";
          divClassChiSquare = "span-24";
      }
       %>
       <%: Html.Hidden("Satisfied", ViewRes.Views.ChartReport.Graphics.Satisfied, "Satisfied")%>
       <%: Html.Hidden("NoSatisfied", ViewRes.Views.ChartReport.Graphics.NoSatisfied, "NoSatisfied")%>
       <%: Html.Hidden("ChiSquareValue", ViewRes.Views.ChartReport.Graphics.ChiSquareValue, "ChiSquareValue")%>
       <%: Html.Hidden("OurChiSquare", ViewRes.Views.ChartReport.Graphics.OurChiSquare, "OurChiSquare")%>
       <%: Html.Hidden("Conclusion", ViewRes.Views.ChartReport.Graphics.Conclusion, "Conclusion")%>
       <%: Html.Hidden("Average", ViewRes.Views.ChartReport.Graphics.Average, "Average")%>
       <%: Html.Hidden("Median", ViewRes.Views.ChartReport.Graphics.Median, "Median")%>
        <div id="<%:demographic%><%:var_FO_id%>DivSat" class="<%: divClassSatNoSat %> chartDiv">
            <% if (demographic.CompareTo("FunctionalOrganizationType") == 0)
               { %>
            <table id="<%:demographic%><%:var_FO_id%>Table" class="table1">
            <%}
               else
               { %>
            <table id="<%:demographic%>Table"  class="table1 satnotsat">
            <%} %>     
                    <thead>
                    <% if (compare_id.HasValue)
                       { %>
                      <tr>
                        <th rowspan="2"></th>
                        <td colspan="2"><%: Model.test.Name%></th>
                        <td colspan="2"><%: testCompareName%></th>
                      </tr>
                      <%} %>
                      <tr>
                      <% if (!compare_id.HasValue)
                         { %>
                         <th></th>
                         <%} %>
                        <th><%: ViewRes.Views.ChartReport.Graphics.Satisfied%></th>
                        <th><%: ViewRes.Views.ChartReport.Graphics.NoSatisfied%></th>
                        <%if (compare_id.HasValue)
                          { %>
                        <th><%: ViewRes.Views.ChartReport.Graphics.Satisfied%></th>
                        <th><%: ViewRes.Views.ChartReport.Graphics.NoSatisfied%></th>
                        <%} %>
                      </tr>
                    </thead>
                    <tbody>
                        <%foreach (SummaryTable table in totalList)
                          {%>
                           <tr>                        
                              <td><%:table.Label%></td>
                              <td><%:table.Satisfied%></td>     
                              <td><%:table.NotSatisfied%></td> 
                              <% if (compare_id.HasValue)
                                 {%>
                              <td><%:table.SatisfiedCompare%></td>     
                              <td><%:table.NotSatisfiedCompare%></td> 
                                 <%} %>
                            </tr>
                        <%} %>
                    </tbody>
                </table>
        </div>
        <%if (compare_id.HasValue)
          { %>
          <div><br /></div>
          <%} %>
        <div id="<%:demographic%><%:var_FO_id%>DivChi" class="<%: divClassChiSquare %> generalDiv">
        <%if (elements_count >= 2)
          { %>
        <fieldset >
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
                     <tbody>
                        <tr>
                            <% if (compare_id.HasValue)
                               { %>
                               <td><%: Model.test.Name%></td>
                               <%} %>
                            <td><%=(Math.Round(chiSquare.ChiSquareValue * 100) / 100).ToString()%></td>
                            <td><%=(Math.Round(chiSquare.OurChiSquare * 100) / 100).ToString()%></td>
                            <td><%= chiSquare.Conclusion%></td>
                        </tr>
                        <% if (compare_id.HasValue)
                           { %>
                        <tr>
                            <td><%: testCompareName%></td>
                            <td><%=(Math.Round(chiSquareCompare.ChiSquareValue * 100) / 100).ToString()%></td>
                            <td><%=(Math.Round(chiSquareCompare.OurChiSquare * 100) / 100).ToString()%></td>
                            <td><%= chiSquareCompare.Conclusion%></td>
                        </tr>
                        <%} %>
                    </tbody>
                </table>
            </fieldset >
        <%} %>
          </div>
    </div>
    <script type="text/javascript">
        $(".table1").tablesorter(); 
    </script>
   <%}
    else
       {%>
            <%Html.RenderPartial("NoMinimum"); %>
       <%}%>
