<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Medinet.Models.ViewModels.ResultViewModel>" %>
<%@ Import Namespace="MedinetClassLibrary.CustomClasses" %>
<%@ Import Namespace="MedinetClassLibrary.Models" %>
    <div class="prepend-1 span-22 append-1">
        <br />
       <% int options = Model.optionsCount; %> 
<% string divClass = "myaccordion";
   if (Model.print)
       divClass = ""; %>
<div id="accordion-4<%: Model.questionnaireToUse %>" class="<%: divClass %>">
    <%foreach (var category in Model.stringObject)
      {
          if (Model.print)
          {%>
	        <h3><%: category.Key%></h3>
          <%
            }
          else
          { %>
            <h3><a href="#"><%: category.Key%></a></h3>
        <%} %>
	    <div>
            <table id="R4-<%: Model.questionnaireToUse %>" class="display tabla">
                 <thead>
                    <tr> 
                         <th><%: ViewRes.Views.ChartReport.Graphics.QuestionTab %></th>
                         <th><%: ViewRes.Views.ChartReport.Graphics.PositivePercentage %></th>
                         <%if ((options % 2) != 0)
                           { %>
                         <th><%: ViewRes.Views.ChartReport.Graphics.NeutralPercentage %></th>
                         <%} %>
                         <th><%: ViewRes.Views.ChartReport.Graphics.NegativePercentage %></th>
                    </tr>
                 </thead>  
                    <tbody>
                        <% foreach (KeyValuePair<string, double[]> res in (IEnumerable)category.Value)
                         {%>
                            <% if (res.Key != "Total")
                                {%>
                                    <tr>
                                        <td><%:res.Key%></td> 
                                        <td style="text-align: center;"><%: String.Format("{0:0.##}", res.Value[0])%>%</td> 
                                        <%if ((options % 2) != 0)
                                          { %>
                                        <td style="text-align: center;"><%: String.Format("{0:0.##}", res.Value[1])%>%</td> 
                                        <%} %>
                                        <td style="text-align: center;"><%: String.Format("{0:0.##}", res.Value[((options % 2) != 0) ? 2 : 1])%>%</td>
                                    </tr>
                                <%}%>
                        <%} %>
                    </tbody>
                    <tfoot>
                        <tr>
                            <% KeyValuePair<string, double[]> tot = ((Dictionary<string, double[]>)category.Value).Last(); %>
                            <th style="text-align: left;"><%: ViewRes.Views.ChartReport.Graphics.GeneralAverage %></th> 
                            <th><%: String.Format("{0:0.##}", tot.Value[0])%>%</th> 
                            <%if ((options % 2) != 0)
                                { %>
                            <th><%: String.Format("{0:0.##}", tot.Value[1])%>%</th> 
                            <%} %>
                            <th><%: String.Format("{0:0.##}", tot.Value[((options % 2) != 0) ? 2 : 1])%>%</th>
                        </tr>
                    </tfoot>
            </table>
	    </div>
    <%} %>
</div>
        <br />
    </div>
        <script type="text/javascript">
            $(function () {
                InitializeAccordion();
            });  
        </script>