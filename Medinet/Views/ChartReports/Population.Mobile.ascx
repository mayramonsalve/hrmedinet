<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Medinet.Models.ViewModels.ChartReportViewModel>" %>
<%@ Import Namespace="MedinetClassLibrary.CustomClasses" %>
<%@ Import Namespace="MedinetClassLibrary.Models" %>

<%  string demographic = ViewData["option"].ToString();
    bool print = ViewData["print"] == null? false : Convert.ToBoolean(ViewData["print"].ToString());
    string source = "", type = "";
    int id = 1, order = 1;
    int FO_id=0;
    int test = Model.test.Id;
    string c_fo = "";
    type = Model.chartType;
    string title;
    string errorTip = "", confianzaTip = "";
    int? fot = null;
    bool table = false;
    string var_FO_id = "";
    if (demographic == "FunctionalOrganizationType")
    {
        fot = Int32.Parse(ViewData["FO_id"].ToString());
        var_FO_id = fot.ToString();
    }
    int elements_count = Model.IsTable(demographic, fot) ? 10 : 1;
  
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
    if (demographic == "FunctionalOrganizationType")
    {
       c_fo = Model.GetFOTNameById(Int32.Parse(ViewData["FO_id"].ToString()));
    }
    else if (demographic == "State")
    {
       if(print)
        c_fo = Model.GetCountryNameById(Int32.Parse(ViewData["country_id"].ToString()));
    }
    title = Model.GetTitle(demographic, "Population", c_fo); 
%> 

   <%: Html.Hidden(demographic + "graphic_id" + var_FO_id, id)%>
   <%: Html.Hidden(demographic + "ElementsCount" + var_FO_id, elements_count, new { id = demographic + "ElementsCount" + var_FO_id })%>
    <div class="principal">
        <%
        if (print)
           { %>
        <h3><%:title%></h3>
        <%} %>
        <div class="chartDiv portrait">
        <% if (!Model.IsTable(demographic, fot))
          { %>
<%--            <% if (demographic.CompareTo("Country") == 0)
               { %>
                    <img  id="<%:demographic%>Chart" src="<%= Url.Action("CountryPercentageChart", new { chartSize="Screen", chartType="Pie"}) %>" usemap="#MyMap" alt="Graph"/>
                        <% Html.RenderAction("CountryPercentageChartMap", new { chartSize = "Screen", chartType = "Pie", name = "MyMap", test_id = Model.test.Id, graphic_id = id }); %>
            <%  }
               else if (demographic.CompareTo("FunctionalOrganizationType") == 0)
               {
                   FO_id = Int32.Parse(ViewData["FO_id"].ToString());%>
                    <img id="<%:demographic%>Chart<%:var_FO_id%>" src="<%:source%>&test_id=<%:Model.test.Id%>&id_FO=<%:FO_id%>&graphic_id=<%:id%>" alt="Grafico"" /> 
            <% }
               else if (demographic.CompareTo("State") == 0)
               {
                   int country_id = Int32.Parse(ViewData["country_id"].ToString());%>
                    <img id="<%:demographic%>Chart" src="<%:source%>&country_id=<%:country_id%>&test_id=<%:Model.test.Id%>&graphic_id=<%:id%>" alt="Grafico"" /> 
            <% }
               else
               { %>
               <div class="aaab"></div>
                <img  id="<%:demographic%>Chart" src="<%:source%>&test_id=<%:Model.test.Id%>&graphic_id=<%:id%>" alt="Grafico" style="float:left;"/>
            <%} %>--%>
            <div id="<%:demographic%>ChartDiv<%:var_FO_id%>" class="column span-24 google_chart"></div>
        <%}
          else
           {
               table = true;
               %>
                <table id="Table<%:demographic%>" class="table1">
                    <thead>
                        <tr> 
                                <th><%: ViewRes.Views.ChartReport.Graphics.Name%></th>
                                <th><%: ViewRes.Views.ChartReport.Graphics.PopulationPctPeople%></th>
                                <th><%: ViewRes.Views.ChartReport.Graphics.PopulationNroPeople%></th>
                        </tr>
                        </thead>  
                    <tbody>
                    <% 
                        foreach (KeyValuePair<string, double> pair in Model.GetPopulationTableByDemographic(demographic, fot).OrderByDescending(v => v.Value))
                        {%>
                        <tr>
                            <td><%:pair.Key%></td>
                            <td><%:String.Format("{0:0.##}", pair.Value * 100 / Model.test.CurrentEvaluations)%>%</td>
                            <td><%:(int)pair.Value%></td>
                        </tr>
                        <%} %>
                    </tbody>
                </table>
        <%} %>
        </div>
        <div class="generalDiv">
           <% if (demographic.CompareTo("General") == 0)
           {
               double confidenceLevel = 1.96;
               if (Model.test.ConfidenceLevel_Id.HasValue && Model.test.StandardError_Id.HasValue)
               {
                   confidenceLevel = (double)Model.test.ConfidenceLevel.Value;
               }%>
               <div>
               <%double error = Math.Sqrt(((((confidenceLevel * confidenceLevel * Model.test.EvaluationNumber * 0.5 * (1 - 0.5)) / Model.test.CurrentEvaluations) - ((confidenceLevel * confidenceLevel * 0.5 * (1 - 0.5)))) / (Model.test.EvaluationNumber - 1)));%>
                <% errorTip = ViewRes.Views.ChartReport.Graphics.ErrorTip; %>
                <% confianzaTip = ViewRes.Views.ChartReport.Graphics.ConfidenceTip; %>
                <p><strong><%: ViewRes.Views.ChartReport.Graphics.Test%></strong><%: Model.test.Name%></p>
                <p><strong><%: ViewRes.Views.Evaluation.TestInstructions.From%></strong>
			    <%: Model.test.StartDate.ToString(ViewRes.Views.Shared.Shared.Date)%>
                <strong><%: ViewRes.Views.Evaluation.TestInstructions.To%></strong>
                <%: Model.test.EndDate.ToString(ViewRes.Views.Shared.Shared.Date)%></p>
                <p><strong><%: ViewRes.Views.ChartReport.Graphics.EmployeesUsed%></strong><%: Model.test.EvaluationNumber%></p>
                <p><strong><%: ViewRes.Views.ChartReport.Graphics.EmployeesWhoPerformed%></strong><%: Model.test.CurrentEvaluations%></p>
                <p class="span-12 column"><strong><%: ViewRes.Views.Test.Create.ConfidenceLevel%>: </strong>
                    <%--<span class="tool" title="<%: confianzaTip %>"><%: Model.test.ConfidenceLevel.Text%></span>--%>
                    <%: Model.test.ConfidenceLevel.Text%>
                <% if(print) {%> (1)<%} %></p>
                <p class="span-12 column last"><strong><%: ViewRes.Views.Test.Create.StandardError%>: </strong>
                    <%--<span class="tool" title="<%: errorTip %>"><%: String.Format("{0:0.##}", error*100)%>%</span>--%>
                    <%: String.Format("{0:0.##}", error*100)%>%
                <% if(print) {%> (2)<%} %></p>
                </div>
          <%}%>
<%--            <div class="commentDiv">
                <% List<GraphicDetail> details = Model.graphics[order].GraphicDetails.ToList();                   
                    if (Model.UserLogged.Role.Name == "HRAdministrator" && !print)
                    {
                        %>
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
                        if (details.Count() > 0)
                        {%>
                        <div><strong><%: ViewRes.Views.ChartReport.Graphics.Comments%></strong></div>
                        <div><%:Model.details[order].Text%></div>
                        <%}
                    }%>
            </div>--%>
        </div>
    </div>   
<% if(demographic.CompareTo("General")==0 && print)
   { %>
        <div><p><strong>(1): </strong><%:confianzaTip%></p></div>
        <div><p><strong>(2): </strong><%:errorTip%></p></div>
 <%} %>

  <% if(table)
    { %>
    <script type="text/javascript">
        $(".table1").tablesorter(); 
    </script>
    <%} %>