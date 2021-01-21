<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Medinet.Models.ViewModels.ChartReportViewModel>" %>
<%@ Import Namespace="MedinetClassLibrary.CustomClasses" %>


<% string demographic = Model.graphicPrint.Demographic;
   string type = Model.chartType;
   string source = Model.graphicPrint.Source;
   int id = Model.graphicPrint.Id;
   List<SummaryTable> totalList = Model.listSummary;
   MedinetClassLibrary.Models.ChiSquare[] chiSquare = Model.chiSquarePrint;
   int? FO_id=Model.FO_id;
   int? category_id = Model.category_id;
   int? question_id = Model.question_id;
   string category_name = Model.category_name;
   string question_name = Model.question_name;
   int? country_id = Model.country_id;
   double pValue = Model.pValue;
   int test = Model.test_id;
   int elements_count = Model.elementsCount;
   string title;
   string c_fo = "";
   if (demographic == "FunctionalOrganizationType")
   {
       c_fo = Model.GetFOTNameById(Int32.Parse(ViewData["FO_id"].ToString()));
   }
   else if (demographic == "State")
   {
           c_fo = Model.GetCountryNameById(Int32.Parse(ViewData["country_id"].ToString()));
   }
   title = Model.GetTitle(demographic, "Univariate", c_fo); 
   %> 

   <div class="span-24 columns last" style="page-break-inside:avoid">   
        <h2><%:title%></h2>
        <br/>
        <% if (demographic.CompareTo("Category") != 0)
           { %>
            <div class="span-24 column last">
                <legend><%: ViewRes.Views.ChartReport.Graphics.Parametres%></legend>        
                <div class="span-24 last"><%: ViewRes.Views.ChartReport.Graphics.Category%> <%: category_name%> </div>
                <div class="span-24 last"><%: ViewRes.Views.ChartReport.Graphics.Question%> <%: question_name%> </div>
            </div>
        <%} %>
        <br/>
        <div class="span-24 column">
            <% if (elements_count < 7)
               { %>
            <div class="span-18 column">
            <!--Seccion izquierda-->
               <div class="span-24 column last">
                <% int? tc_id = null;
                   if (Model.testCompare != 0)
                       tc_id = Model.testCompare;
                   if (demographic.CompareTo("State") == 0)
                   { %>
                    <img id="Img1" src="<%:source%>&graphic_id=<%:id%>&country_id=<%:country_id%>&category_id=<%:category_id%>&question_id=<%:question_id%>&test_id=<%:Model.test.Id%>&compare=<%:tc_id%>" alt="Grafico"  />
                <%  }
                   else if (demographic.CompareTo("Country") == 0)
                   { %>
                    <img id="Chart1" src="<%= Url.Action("UniVariateChartByCountry", new {chartSize= "Screen", chartType="Column"}) %>" usemap="#MyMap2" alt="Graph" />
                    <% Html.RenderAction("UniVariateChartMapByCountry", new { chartSize = "Screen", chartType = "Column", name = "MyMap2", graphic_id = id, test_id = Model.test_id, compare = Model.testCompare }); %>
                <%  }
                   else if (demographic.CompareTo("FunctionalOrganizationType") == 0)
                   { %>
                         <img id="Img2" src="<%:source%>&graphic_id=<%:id%>&test_id=<%:Model.test_id%>&category_id=<%:category_id%>&question_id=<%:question_id%>&type_id=<%:FO_id%>&compare=<%:tc_id%>" alt="Grafico"  />
                <%   }
                   else if (demographic.CompareTo("Category") == 0)
                   { %>
                    <img id="Chart2" src="<%:source%>&graphic_id=<%:id%>&test_id=<%:Model.test_id%>&compare=<%:tc_id%>" alt="Grafico"  />
                <%}
                   else
                   { %>
                    <img id="Img3" src="<%:source%>&graphic_id=<%:id%>&test_id=<%:Model.test_id%>&category_id=<%:category_id%>&question_id=<%:question_id%>&compare=<%:tc_id%>" alt="Grafico"  />
                <%} %>
            </div>
            </div>
            <div class="span-6 column last">
                        <% if (Model.testCompare != 0)
                           { %>
                            <div class="span-24 last"><%: ViewRes.Views.ChartReport.Graphics.Compare %></div>
                            <div>
                                <%: Model.testCompareName%>
                            </div>
                        <%} %>
            </div>
            <%}
               else
               { %>
                    <table id="<%=demographic %>Table" class="display" >
                        <thead>
                        <% if (Model.testCompare!=0)
                           { %>
                          <tr>
                            <th rowspan="2"></th>
                            <th  align="center" colspan="4"><%: Model.testName %></th>
                            <th  align="center" colspan="4"><%: Model.testCompareName %></th>
                          </tr>
                          <%} %>
                            <tr>
                            <% if (Model.testCompare == 0)
                               { %>
                                <th><%--<%: ViewRes.Views.ChartReport.Graphics.Name%>--%></th>
                                <%} %>
                                <th align="right"><%: ViewRes.Views.ChartReport.Graphics.Average %></th>
                                <th align="right"><%: ViewRes.Views.ChartReport.Graphics.Median %></th>
                                <th align="right"><%: ViewRes.Views.ChartReport.Graphics.Satisfied %></th>
                                <th align="right"><%: ViewRes.Views.ChartReport.Graphics.NoSatisfied %></th>
                                <% if (Model.testCompare != 0)
                                   { %>
                                <th align="right"><%: ViewRes.Views.ChartReport.Graphics.Average %></th>
                                <th align="right"><%: ViewRes.Views.ChartReport.Graphics.Median %></th>
                                <th align="right"><%: ViewRes.Views.ChartReport.Graphics.Satisfied %></th>
                                <th align="right"><%: ViewRes.Views.ChartReport.Graphics.NoSatisfied %></th>
                                 <%} %>
                            </tr>
                        </thead>
                        <tbody>
                           <%foreach ( var objeList in totalList ) {%>
                            <% var average = (Math.Round(objeList.Average*100)/100).ToString();
                               var averageCompare = (Math.Round(objeList.Average * 100) / 100).ToString(); %>
                            <tr>
                                <td style="color:Black;"><%:Html.Label(objeList.Label)%></td> 
                                <td style="color:Black; text-align: right;"><%=average%></td> 
                                <td style="color:Black; text-align: right;"><%=objeList.Median.ToString()%></td> 
                                <td style="color:Green; text-align: right;"><%=objeList.Satisfied.ToString()%></td> 
                                <td style="color:Red; text-align: right;"><%=objeList.NotSatisfied.ToString()%></td> 
                                <% if (Model.testCompare != 0)
                                 {%>
                                <td style="color:Black; text-align: right;"><%=averageCompare%></td> 
                                <td style="color:Black; text-align: right;"><%=objeList.MedianCompare.ToString()%></td> 
                                <td style="color:Green; text-align: right;"><%=objeList.SatisfiedCompare.ToString()%></td> 
                                <td style="color:Red; text-align: right;"><%=objeList.NotSatisfiedCompare.ToString()%></td>
                                 <%} %>
                           </tr>
                            <%} %>
                        </tbody>          
                      </table>
             <%} %>
        </div>
    <!--Seccion tablas-->
    <br/>
    <div class="span-24 column last">
    <%if (elements_count>=2)
      { 
          string significanceClass = "span-24 column last";
          if(!(Model.elementsCount<7))
              significanceClass = "span-14 column";
          %>
        <div class="<%: significanceClass %>">   
         <%: ViewRes.Views.ChartReport.Graphics.SignificanceLevel%> <%: pValue %> *
        </div>
        <%}
           if (!(Model.elementsCount < 7))
           { %>
            <div class="span-10 column last">
                <% if (Model.testCompare != 0)
                    { %>
                    Comparar con: <%: Model.testCompareName%>
                <%} %>
            </div>
        <%} %>
        <br/> 
        <%           
        string divClassSatNoSat = "span-12 column";
        string divClassChiSquare = "span-12 column last";
        if (Model.testCompare!=0)
        {
            divClassSatNoSat = "span-24 column last";
            divClassChiSquare = "span-24 column last";
        }
        if (Model.elementsCount < 7)
        {%>
        <div class="<%: divClassSatNoSat %>">
        <fieldset >
            <table id="Table"  class="display">   
                    <thead>
                    <% if (Model.testCompare != 0)
                       { %>
                      <tr>
                        <th rowspan="2"></th>
                        <th  align="center" colspan="2"><%: Model.testName%></th>
                        <th  align="center" colspan="2"><%: Model.testCompareName%></th>
                      </tr>
                      <%} %>
                      <tr>
                      <% if (Model.testCompare == 0)
                         { %>
                         <th></th>
                         <%} %>
                        <th align="center"><%: ViewRes.Views.ChartReport.Graphics.Satisfied%></th>
                        <th align="center"><%: ViewRes.Views.ChartReport.Graphics.NoSatisfied%></th>
                        <%if (Model.testCompare != 0)
                          { %>
                        <th align="center"><%: ViewRes.Views.ChartReport.Graphics.Satisfied%></th>
                        <th align="center"><%: ViewRes.Views.ChartReport.Graphics.NoSatisfied%></th>
                        <%} %>
                      </tr>
                    </thead>
                    <tbody>
                        <%foreach (SummaryTable table in totalList)
                          { %>
                           <tr>                        
                              <td style="color:Black; " ><%:table.Label%></td>
                              <td style="color:Green; text-align: right;"><%:table.Satisfied%></td>     
                              <td style="color:Red; text-align: right;"><%:table.NotSatisfied%></td> 
                              <% if (Model.testCompare != 0)
                                 {                                    
                                 %>
                              <td style="color:Green; text-align: right;"><%:table.NotSatisfiedCompare%></td>     
                              <td style="color:Red; text-align: right;"><%:table.NotSatisfiedCompare%></td> 
                                 <%} %>
                            </tr>
                        <%} %>
                    </tbody>
            </table>
        </fieldset>
    </div>
      <%} %>
        <div class="<%: divClassChiSquare %>">
        <%if (elements_count >= 2)
          { %>
            <fieldset >
                <table id="TableChiSquare"  class="display">
                     <thead>
                        <tr>
                            <% if (Model.testCompare!=0)
                               { %>
                               <th></th>
                               <%} %>
                             <th align="center"><%: ViewRes.Views.ChartReport.Graphics.ChiSquareValue%></th>
                             <th align="center"><%: ViewRes.Views.ChartReport.Graphics.OurChiSquare%></th>
                             <th align="center"><%: ViewRes.Views.ChartReport.Graphics.Conclusion%></th>
                        </tr>
                     </thead>  
                        <tr>
                            <% if (Model.testCompare != 0)
                               { %>
                               <td><%: Model.test.Name %></td>
                               <%} %>
                            <td style="text-align: right;"><%=(Math.Round(chiSquare[0].OurChiSquare * 100) / 100).ToString()%></td>
                            <td style="text-align: right;"><%=(Math.Round(chiSquare[0].OurChiSquare * 100) / 100).ToString()%></td>
                            <td style="text-align: left;"><%= chiSquare[0].Conclusion%></td>
                        </tr>
                        <% if (Model.testCompare != 0)
                          { %>
                        <tr>
                            <td><%: Model.testCompareName %></td>
                            <td style="text-align: right;"><%=(Math.Round(chiSquare[1].OurChiSquare * 100) / 100).ToString()%></td>
                            <td style="text-align: right;"><%=(Math.Round(chiSquare[1].OurChiSquare * 100) / 100).ToString()%></td>
                            <td style="text-align: left;"><%= chiSquare[1].Conclusion%></td>
                        </tr>
                        <%} %>
                </table>
            </fieldset >
        <%} %>
        </div>
    </div>
   </div>
<% if(elements_count>=2)
    { 
        string significanceTip = ViewRes.Views.ChartReport.Graphics.SignificanceTip;
        %>
        <div><p><strong>*: </strong><%: significanceTip %></p></div>
 <%} %>