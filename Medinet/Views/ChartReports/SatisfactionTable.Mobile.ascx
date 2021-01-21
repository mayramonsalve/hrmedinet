<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Medinet.Models.ViewModels.ChartReportViewModel>" %>
<%@ Import Namespace="MedinetClassLibrary.CustomClasses" %>
<%@ Import Namespace="MedinetClassLibrary.Models" %>

<% bool print = ViewData["print"] == null? false : Convert.ToBoolean(ViewData["print"].ToString());
   int test = Model.test.Id;
   int elements_count = 1;
   string demographic = ViewData["option"].ToString();
   Graphic graphic = Model.GetByDemographicAndType(demographic, "Satisfaction");
   int id = graphic.Id;
   int order = graphic.Order;
   string source = graphic.Source;
   int? selValId = ViewData["FO_id"] == null ? 0 : Int32.Parse(ViewData["FO_id"].ToString());
   if (selValId == 0) selValId = null;
   %> 
   <%: Html.Hidden(demographic + "graphic_id", id)%>
   <%: Html.Hidden(demographic + "ElementsCount", elements_count, new { id = demographic + "ElementsCount" })%>
        <% int cols = 1;
           List<string> keys = new List<string>();
            try
            {
                Dictionary<string, double> aux = (Dictionary<string, double>)Model.populationComparativeTable.FirstOrDefault().Value;
                cols = aux.Count;
                keys = aux.Keys.ToList();
            }
            catch
            {
                Dictionary<string, double[]> aux = (Dictionary<string, double[]>)Model.populationComparativeTable.FirstOrDefault().Value;
                cols = aux.Count;
                keys = aux.Keys.ToList();
            }
            int rowspan = demographic == "Category" ? 1 : 2;
            %>
          <div>
            <table id="Table<%: demographic %><%:selValId %>" class="table1">
                 <thead>
                    <tr> 
                    <%
                        if (demographic == "FunctionalOrganizationType")
                        { %>
                        <td rowspan="<%: rowspan + 1 %>" style ="vertical-align:middle;"><%: Model.demographicSelector%></td>
                        <td rowspan="1" colspan="<%: cols*rowspan %>" style="text-align: center;"><%: ViewRes.Views.ChartReport.Graphics.Periods %></td>
                        <%}
                        else
                        { %>
                        <th rowspan="<%: rowspan + 1 %>" style ="vertical-align:middle;"><%: Model.demographicSelector%></th>
                        <th rowspan="1" colspan="<%: cols*rowspan %>" style="text-align: center;"><%: ViewRes.Views.ChartReport.Graphics.Periods %></th>
                        <% } %>
                    </tr>
                    <tr> 
                        <% foreach(string year in keys)
                            { %>
                             <td colspan="<%: rowspan %>" style="text-align: center;"><%: year %></td>
                         <%} %>
                    </tr>
                    <%if (demographic != "Category")
                      {
                        if (demographic == "FunctionalOrganizationType")
                        { %>
                        <tr>
                            <td>Encuestados</td> 
                            <td>Media</td> 
                        </tr>
                        <% }
                        else
                        { %>
                        <tr>
                           <th>Encuestados</th> 
                           <th>Media</th> 
                        </tr>
                        <% } %>
                    <%} %>
                 </thead>  
                <tbody>
                <% foreach (KeyValuePair<string, object> pair in Model.populationComparativeTable)
                   {%>
                    <tr>
                        <%  bool underscore = pair.Key.StartsWith("-");
                            string classBold = underscore ? "bold" : "";
                            string style = underscore ? "background-color:#E2E4FF" : "";
                            %>
                        <td style="<%: style %>" class="alignLeft <%: classBold %>"><%:underscore ? pair.Key.Substring(1) : pair.Key%></td>
                        <%if (demographic == "Category")
                          { %>
                            <%foreach (KeyValuePair<string, double> pop in (Dictionary<string, double>)pair.Value)
                              {
                                  string bgColor = Model.test.ClimateScale_Id.HasValue ?
                                      Model.test.ClimateScale.ClimateRanges.Where(r => r.MinValue <= (decimal)pop.Value &&
                                          r.MaxValue >= (decimal)pop.Value).OrderBy(r => r.MaxValue).FirstOrDefault().Color : "";
                                  string fColor = Model.GetFontColorByBackgroundColor(bgColor);
                                  %>
                                <td class="alignCenter <%: classBold %>" style="border-style:solid; border-width:1px; color:<%:fColor%>; background-color:<%:bgColor%>;"><%:String.Format("{0:0.##}", pop.Value)%></td>
                            <%} %>
                        <%}
                          else
                          { %>
                            <%foreach (KeyValuePair<string, double[]> pop in (Dictionary<string, double[]>)pair.Value)
                              {
                                  string bgColor = Model.test.ClimateScale_Id.HasValue ?
                                      Model.test.ClimateScale.ClimateRanges.Where(r => r.MinValue <= (decimal)pop.Value[1] &&
                                          r.MaxValue >= (decimal)pop.Value[1]).OrderBy(r => r.MaxValue).FirstOrDefault().Color : "";
                                  string fColor = Model.GetFontColorByBackgroundColor(bgColor);
                                  %>
                                <td style="<%: style %>" class="alignCenter <%: classBold %>"><%:String.Format("{0:0.##}", pop.Value[0])%></td>
                                <td class="alignCenter <%: classBold %>" style="border-style:solid; border-width:1px; color:<%:fColor%>; background-color:<%:bgColor%>;"><%:String.Format("{0:0.##}", pop.Value[1])%></td>
                            <%} %>
                          <%} %>
                    </tr>
                    <%} %>
                </tbody>
            </table>
          </div>
<%--        <%    List<GraphicDetail> details = Model.graphics[order].GraphicDetails.ToList();%>
        <div>
            <%    if (Model.UserLogged.Role.Name == "HRAdministrator")
                  {
                       %>
                       <div><strong><%: ViewRes.Views.ChartReport.Graphics.Comments%></strong></div>
                       <div id="<%: demographic %><%:selValId %>CommentsDiv">
                            <%:Html.TextArea(demographic + selValId + "CommentsEditor", Model.details[order].Text, new { @class = "input-background textEditor" })%>
                        </div>
	                    <input data-role="button" data-theme="f" id="<%: demographic %>Button" type="submit" value="<%: ViewRes.Views.Account.LogOn.LogOnButton %>" class="button" />
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

    <% if (demographic != "FunctionalOrganizationType")
        {%>
    <script type="text/javascript">
        $('.table1').tablesorter();
    </script>
    <% } %>