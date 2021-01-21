<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Medinet.Models.ViewModels.ResultViewModel>" %>
<%@ Import Namespace="MedinetClassLibrary.CustomClasses" %>
<%@ Import Namespace="MedinetClassLibrary.Models" %>
    <div class="prepend-1 span-22 append-1">
        <br />
<% string divClass = "myaccordion";
   if (Model.print)
       divClass = ""; %>
<div id="accordion-3<%: Model.questionnaireToUse %>" class="<%: divClass %>">
    <%foreach (var category in Model.stringObject)
      {
          if (Model.print)
          {%>
	        <h3><%: category.Key%></h3>
          <%}
          else
          { %>
            <h3><a href="#"><%: category.Key%></a></h3>
            <%} %>
	    <div>
            <table id="R3-<%: Model.questionnaireToUse %>" class="display tabla">
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
        <br />
    </div>
    <script type="text/javascript">
        $(function () {
            InitializeAccordion();
        });  
    </script>