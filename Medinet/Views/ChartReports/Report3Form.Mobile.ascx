<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Medinet.Models.ViewModels.ResultViewModel>" %>
<%@ Import Namespace="MedinetClassLibrary.CustomClasses" %>
<%@ Import Namespace="MedinetClassLibrary.Models" %>


<div data-role="collapsible-set" data-theme="f" data-content-theme="c">
    <%foreach (var category in Model.stringObject)
      { %>
    <div data-role="collapsible">
      <%
          if (Model.print)
          {%>
	        <h2><%: category.Key%></h2>
          <%}
          else
          { %>
            <h2><%: category.Key%></h2>
            <%} %>

            <table id="R3-<%: Model.questionnaireToUse %>" class="table1">
                 <thead>
                    <tr> 
                         <th><%: ViewRes.Views.ChartReport.Graphics.QuestionTab %></th>
                         <th><%: ViewRes.Views.ChartReport.Graphics.PositivePercentage %></th>
                    </tr>
                 </thead>  
                <tbody>
                    <% foreach (KeyValuePair<string, double> res in (IEnumerable)category.Value)
                       {%>
                            <% if (res.Key != "Total")
                               {%>
                                    <tr>    
                                        <td><%:res.Key%></td> 
                                        <td style="text-align: center;"><%: String.Format("{0:0.##}", res.Value)%>%</td> 
                                    </tr>
                                <%}%>
                        <%} %>
                </tbody>
                <tfoot>
                    <% KeyValuePair<string, double> tot = ((Dictionary<string, double>)category.Value).Last(); %>
                    <tr>
                        <th style="text-align: left;"><strong><%: ViewRes.Views.ChartReport.Graphics.GeneralAverage %></strong></th> 
                        <th><%: String.Format("{0:0.##}", tot.Value)%>%</th> 
                    </tr>
                </tfoot>
            </table>
	    </div>
    <%} %>
</div>