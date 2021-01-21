<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Medinet.Models.ViewModels.ResultViewModel>" %>
<%@ Import Namespace="MedinetClassLibrary.CustomClasses" %>
<%@ Import Namespace="MedinetClassLibrary.Models" %>
       <% int options = Model.optionsCount; %> 
<% string divClass = "myaccordion";
   if (Model.print)
       divClass = ""; %>
<div data-role="collapsible-set" data-theme="f" data-content-theme="c">
    <%foreach (var category in Model.stringObject)
      { %>	    
      <div data-role="collapsible">
      <%
          if (Model.print)
          {%>
	        <h2><%: category.Key%></h2>
          <%
            }
          else
          { %>
            <h2><%: category.Key%></h2>
        <%} %>

            <table id="R4-<%: Model.questionnaireToUse %>" class="table1">
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