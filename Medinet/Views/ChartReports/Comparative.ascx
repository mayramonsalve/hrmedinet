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
<div class="span-24 block" style="page-break-inside:avoid">

        <div class="span-14 column">
            <div id="<%:demographic%>ChartDiv" class="column span-12 google_chart"></div>
            <div class="clear"></div>
            <div id="<%:demographic%>Img" class="span-24 google_img"></div>
            <%--<img id="<%:demographic%>Chart" src="<%:source%>&test_id=<%:Model.test.Id%>&graphic_id=<%:id%>" alt="Grafico" style="float: left; width: 600px;"/>--%>
        </div>
        <!--Seccion derecha-->
        <div class="span-8 prepend-1 column last">
<%--            <%    List<GraphicDetail> details = Model.graphics[order].GraphicDetails.ToList();                   
                   if (Model.UserLogged.Role.Name == "HRAdministrator")
                   {
                       %>
                       <div id="<%: demographic %>CommentsDiv">
                            <div class="span-24 column last button-padding-top">
                                <div class="span-3 column"><%: ViewRes.Views.ChartReport.Graphics.TableTitle%></div>
                                <div class="span-21 column last"><%: Html.TextBox(demographic + "Title", Model.details[order].Title, new { @class = "input-background short" })%></div>
                            </div>
                            <div class="span-24 column last button-padding-top">
                                <div class="span-3 column"><%: ViewRes.Views.ChartReport.Graphics.AxisX%></div>
                                <div class="span-21 column last"><%: Html.TextBox(demographic + "XAxis", Model.details[order].AxisXName, new { @class = "input-background short" })%></div>
                            </div>
                            <div class="span-24 column last button-padding-top">
                                <div class="span-3 column"><%: ViewRes.Views.ChartReport.Graphics.AxisY%></div>
                                <div class="span-21 column last"><%: Html.TextBox(demographic + "YAxis", Model.details[order].AxisYName, new { @class = "input-background short" })%></div>
                            </div>
                            <div class="span-24 column last button-padding-top">
                                <div class="span-24 column last"><%: ViewRes.Views.ChartReport.Graphics.Comments%></div>
                                <div class="span-24 column last"><%:Html.TextArea(demographic + "CommentsEditor", Model.details[order].Text, new { @class = "input-background textEditor" })%></div>
                            </div>
                        </div>
                        <div id="login-submit" class="div-button column last button-padding-top">
	                        <input id="<%: demographic %>Button" type="submit" value="<%: ViewRes.Views.Account.LogOn.LogOnButton %>" class="button" />
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

        </div>
        <div class="clear">&nbsp;</div>
        <% if (demographic == "Population")
           { 
                Dictionary<string, double> aux = (Dictionary<string, double>)Model.populationComparativeTable.FirstOrDefault().Value;
                int cols = aux.Count;             
               %>
          <div class="span-23 column last">
            <fieldset class="widthAuto">
            <table id="TablePopulation" class="display tabla">
                 <thead>
                    <tr> 
                         <th rowspan="2" style ="vertical-align:middle;"><%:Model.demographicSelector %></th>
                         <th colspan="<%: cols %>"><%: ViewRes.Views.ChartReport.Graphics.Years %></th>
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
            </fieldset>
          </div>
          <%} %>
</div>   
 <% if(demographic == "Population")
    { %>
    <script type="text/javascript">
        InitializeDataTable();
    </script>
    <%} %>