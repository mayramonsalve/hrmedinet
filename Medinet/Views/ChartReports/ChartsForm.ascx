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
       if (Model.graphics[i].Demographic.CompareTo(demographic) == 0 && Model.graphics[i].Type.CompareTo(type) == 0)//id del gráfico y toda su información
       {
           id = Model.graphics[i].Id;
           order = Model.graphics[i].Order;
           source = Model.graphics[i].Source.ToString();
           i = Model.graphics.Length;
       }
   } 
   %> 

   <div class="span-24 column last">
   <%  if (Model.testCompare != 0)
       {
           compare_id = Model.testCompare;
           testCompareName = new MedinetClassLibrary.Services.TestsServices().GetById(Model.testCompare).Name;
       }
        if (demographic.CompareTo("FunctionalOrganizationType") == 0)
        {
            FO_id = Int32.Parse(ViewData["FO_id"].ToString());
            totalList = Model.NameAverageMedianSastNoSast(questionnaire, category, question, pValue, test, demographic, condition, FO_id, null);//esta función dependiendo de la cantidad de elementos trae satisfechos y no satisfechos o media,mediana,satisfechos y no satisfechos
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
        {//totalList
            totalList = Model.NameAverageMedianSastNoSast(questionnaire, category, question, pValue, test, demographic, condition, 0, compare_id);//totalList=lista de summarytable. condition trae si es mayor o menor a 7.pValue se usa para mandar a buscar el chicuadrado en la tabla.totalList contiene satisfechos y no satisfechos
            keys = totalList.Select(l => l.Label).ToList();
            elements_count = totalList.Count;//cantidad de demográficos
            if (elements_count >= 2)
            {
                chiSquare = Model.getChiSquare(test, demographic, questionnaire, category, question, country, null, 0.05);//chiSquare para el test actual
                if (compare_id.HasValue)
                    chiSquareCompare = Model.getChiSquare(compare_id.Value, demographic, questionnaire, category, question, country, null, 0.05);//pValue 0.05
            }
        }
       %> 
       <%: Html.Hidden("DownloadImage", ViewRes.Views.ChartReport.Graphics.DownloadImage)%>
           <%: Html.Hidden(demographic + "graphic_id" + var_FO_id, id)%>
<%--          <%: Html.Hidden(demographic + "TestName" + var_FO_id, Model.test.Name, new { id = demographic + "TestName" + var_FO_id })%>  
          <%: Html.Hidden(demographic + "TestCompareName" + var_FO_id, testCompareName, new { id = demographic + "TestCompareName" + var_FO_id })%>  --%>
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
        <div class="span-24 column last">
        <!--Seccion izquierda-->
                <% //if (demographic.CompareTo("Country") == 0)
                   //{ %>
<%--                    <img id="<%:demographic%>Chart" name="<%:demographic%>Chart" src="<%= Url.Action("UniVariateChartByCountry", new {chartSize= "Screen", chartType="Column"}) %>" usemap="#MyMap2" alt="Graph" style="width: 600px;"/>
                    <% Html.RenderAction("UniVariateChartMapByCountry", new { chartSize = "Screen", chartType = "Column", graphic_id = Model.graphics[17].Id, name = "MyMap2", test_id = Model.test.Id, compare = compare_id }); %>--%>
                <%  /*}
                   else if (demographic.CompareTo("FunctionalOrganizationType") == 0)
                   {*/ %>
                         <%--<img id="<%:demographic%>Chart<%:FO_id%>" name="<%:demographic%>Chart" src="<%:source%>&graphic_id=<%:id%>&test_id=<%:Model.test.Id%>&type_id=<%:FO_id%>&compare=<%:compare_id%>" alt="Grafico" style="width: 600px;" />--%>
                <%  /* }
                   else
                   {*/ %>
                        <div id="<%:demographic%>ChartDiv<%:var_FO_id%>" class="column span-12 google_chart"></div>
                        <div class="clear"></div>
                        <div id="<%:demographic%>Img<%:var_FO_id%>" class="span-24 google_img"></div>
                    <%--<img id="<%:demographic%>Chart<%:var_FO_id%>" name="<%:demographic%>Chart<%:var_FO_id%>" src="<%:source%>&graphic_id=<%:id%>&test_id=<%:Model.test.Id%>&compare=<%:compare_id%>" alt="Grafico" style="width: 600px;" />--%>
                <%//} %>

        </div>
        <!--Seccion derecha-->
        <%--<div class="span-9 column last">--%>
<%--            <%    List<GraphicDetail> details = Model.graphics[order].GraphicDetails.ToList();                   
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
    <!--Seccion tablas-->
    <div class="span-24 column last">
          <%string styleCompare = "span-24 column last";  %>
    <%if (elements_count >= 2)
      {
          styleCompare = "span-12 column last alignRight";
          string significanceTip = ViewRes.Views.ChartReport.Graphics.SignificanceTip;
          string styleSignificance = "span-24 column last";
          if (Model.testsToCompare.Count() > 0) styleSignificance = "span-12 column";
          %>
        <div class="<%:styleSignificance %>">  
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
        </div>
    <%}%>
    <% if (Model.testsToCompare.Count() > 0)
        { %>
            <div class="<%: styleCompare %>">
                <div class="span-24 last"><%: ViewRes.Views.ChartReport.Graphics.Compare%></div>
                <div>
                    <%: Html.DropDownList(demographic + "CompareDDL" + var_FO_id, Model.testsToCompare, ViewRes.Views.ChartReport.Graphics.None, new { @class = "CompareDDL form-short" })%>
                </div>
            </div>
    <%}
        else
        {%>
        <%: Html.Hidden(demographic + "CompareDDL" + var_FO_id, null, new { id = demographic + "CompareDDL" + var_FO_id })%>
        <%} %>        

        <div class="clear">&nbsp;</div>         
          
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
        <div id="<%:demographic%><%:var_FO_id%>DivSat" class="<%: divClassSatNoSat %>">
        <fieldset >
            <% if (demographic.CompareTo("FunctionalOrganizationType") == 0)
               { %>
            <table id="<%:demographic%><%:var_FO_id%>Table" class="display tabla">
            <%}
               else
               { %>
            <table id="<%:demographic%>Table"  class="display tabla">
            <%} %>     
                    <thead>
                    <% if (compare_id.HasValue)
                       { %>
                      <tr>
                        <th rowspan="2"></th>
                        <th  colspan="2"><%: Model.test.Name%></th>
                        <th  colspan="2"><%: testCompareName%></th>
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
                              <td style="color:Black;"><%:table.Label%></td>
                              <td style="color:Green; text-align: center;"><%:table.Satisfied%></td>     
                              <td style="color:Red; text-align: center;"><%:table.NotSatisfied%></td> 
                              <% if (compare_id.HasValue)
                                 {%>
                              <td style="color:Green; text-align: center;"><%:table.SatisfiedCompare%></td>     
                              <td style="color:Red; text-align: center;"><%:table.NotSatisfiedCompare%></td> 
                                 <%} %>
                            </tr>
                        <%} %>
                    </tbody>
                </table>
            </fieldset>
        </div>
        <%if (compare_id.HasValue)
          { %>
          <div><br /></div>
          <%} %>
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
                               <td><%: Model.test.Name%></td>
                               <%} %>
                            <td style="text-align: center;"><%=(Math.Round(chiSquare.ChiSquareValue * 100) / 100).ToString()%></td>
                            <td style="text-align: center;"><%=(Math.Round(chiSquare.OurChiSquare * 100) / 100).ToString()%></td>
                            <td style="text-align: left;"><%= chiSquare.Conclusion%></td>
                        </tr>
                        <% if (compare_id.HasValue)
                           { %>
                        <tr>
                            <td><%: testCompareName%></td>
                            <td style="text-align: center;"><%=(Math.Round(chiSquareCompare.ChiSquareValue * 100) / 100).ToString()%></td>
                            <td style="text-align: center;"><%=(Math.Round(chiSquareCompare.OurChiSquare * 100) / 100).ToString()%></td>
                            <td style="text-align: left;"><%= chiSquareCompare.Conclusion%></td>
                        </tr>
                        <%} %>
                </table>
            </fieldset >
        <%} %>
          </div>
    </div>
    <script type="text/javascript">
    InitializeDataTable();
    </script>
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
