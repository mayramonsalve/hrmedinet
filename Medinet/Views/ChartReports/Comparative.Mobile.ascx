<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Medinet.Models.ViewModels.ChartReportViewModel>" %>
<%@ Import Namespace="MedinetClassLibrary.CustomClasses" %>
<%@ Import Namespace="MedinetClassLibrary.Models" %>

<% bool print = ViewData["print"] == null? false : Convert.ToBoolean(ViewData["print"].ToString());
   int test = Model.test.Id;
   int elements_count = 1;
    string demographic = ViewData["option"].ToString();
   Graphic graphic = Model.GetByDemographicAndType(demographic, "Comparative");
   int id = graphic.Id;
   int order = graphic.Order;
   string source = graphic.Source;
   %> 
   <%: Html.Hidden(demographic + "graphic_id", id)%>
   <%: Html.Hidden(demographic + "ElementsCount", elements_count, new { id = demographic + "ElementsCount" })%>
    <div class="principal">

        <div class="chartDiv portrait">
            <div id="<%:demographic%>ChartDiv" class="column span-24 google_chart"></div>
            <%--<img id="<%:demographic%>Chart" src="<%:source%>&test_id=<%:Model.test.Id%>&graphic_id=<%:id%>" alt="Grafico" style="float:left;"/>--%>
        </div>
        <!--Seccion derecha-->
<%--        <div class="generalDiv">
            <%    List<GraphicDetail> details = Model.graphics[order].GraphicDetails.ToList();                   
                   if (Model.UserLogged.Role.Name == "HRAdministrator")
                   {
                       %>
                    <div id="<%: demographic %>CommentsDiv">
                        <div data-role="fieldcontain">
                                <label for="<%:demographic %>Title"><%: ViewRes.Views.ChartReport.Graphics.TableTitle%></label>
                                <%: Html.TextBox(demographic  + "Title", Model.details[order].Title, new { @class = "input-background short" })%>
                                <label for="<%:demographic %>XAxis"><%: ViewRes.Views.ChartReport.Graphics.AxisX%></label>
                                <%: Html.TextBox(demographic  + "XAxis", Model.details[order].AxisXName, new { @class = "input-background short" })%>
                                <label for="<%:demographic %>YAxis"><%: ViewRes.Views.ChartReport.Graphics.AxisY%></label>
                                <%: Html.TextBox(demographic  + "YAxis", Model.details[order].AxisYName, new { @class = "input-background short" })%>
                                <label for="<%:demographic %>CommentsEditor"><%: ViewRes.Views.ChartReport.Graphics.Comments%></label>
                                <%:Html.TextArea(demographic  + "CommentsEditor", Model.details[order].Text, new { @class = "input-background textEditor" })%>
                        </div>
                        <input data-role="button" data-theme="f" id="<%: demographic %>Button" type="submit" value="<%: ViewRes.Views.Account.LogOn.LogOnButton %>" class="button" />
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
    </div>
        <% if (demographic == "Population")
           { 
                Dictionary<string, double> aux = (Dictionary<string, double>)Model.populationComparativeTable.FirstOrDefault().Value;
                int cols = aux.Count;             
               %>
          <div>
            <table id="TablePopulation" class="table1">
                 <thead>
                    <tr> 
                         <th rowspan="2" style ="vertical-align:middle;"><%:Model.demographicSelector %></th>
                         <td colspan="<%: cols %>"><%: ViewRes.Views.ChartReport.Graphics.Years %></td>
                    </tr>
                    <tr> 
                        <% foreach(string year in aux.Keys)
                            { %>
                             <th><%: year %></th>
                         <%} %>
                    </tr>
                 </thead>  
                <tbody>
                <% foreach (KeyValuePair<string, object> pair in Model.populationComparativeTable)
                   {%>
                    <tr>
                        <td class="alignLeft"><%:pair.Key%></td>
                        <%foreach (KeyValuePair<string, double> pop in (Dictionary<string, double>)pair.Value)
                          { %>
                            <td class="alignCenter"><%:String.Format("{0:0}", pop.Value)%></td>
                        <%} %>
                    </tr>
                    <%} %>
                </tbody>
                <tfoot>
                    <tr>
                        <th class="alignLeft">Total</th>
                        <%foreach (int[] tot in Model.populationComparativeTotal.Values)
                          {
                              Double pct = Double.Parse((tot[0] / tot[1]).ToString()) * 100;
                              %>
                            <th class="alignCenter"><%:tot[0]%> / <%:tot[1]%> (<%:pct%>%)</th>
                        <%} %>
                    </tr>
                </tfoot>
            </table>
          </div>
          <%} %>
   
 <% if(demographic == "Population")
    { %>
    <script type="text/javascript">
        $(".table1").tablesorter(); 
    </script>
    <%} %>