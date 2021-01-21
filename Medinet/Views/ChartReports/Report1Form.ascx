<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Medinet.Models.ViewModels.ResultViewModel>" %>
<%@ Import Namespace="MedinetClassLibrary.CustomClasses" %>
<%@ Import Namespace="MedinetClassLibrary.Models" %>
    <div class="prepend-1 span-22 append-1">
        <table id="R1-<%: Model.questionnaireToUse %>" class="tabla display table1">
             <thead>
                <tr> 
                     <th><%: ViewRes.Views.ChartReport.Graphics.CategoryTab %></th>
                     <th><%: ViewRes.Views.ChartReport.Graphics.PositivePercentage %></th>
                </tr>
             </thead>  
            <tbody>
                <% foreach ( var res in Model.stringDouble ) { %>
                    <% if (res.Key != "Total")
                        {%>                    
                        <tr>
                            <td><%:res.Key%></td> 
                            <td style="text-align: center;"><%: String.Format("{0:0.##}",res.Value)%>%</td> 
                        </tr>
                    <%} %>
                <%}%>
                </tbody>
                <tfoot>
                    <%if (Model.stringDouble.Count > 0)
                        { %>
                    <tr>
                        <% KeyValuePair<string, double> tot = Model.stringDouble.Last(); %>
                        <th style="text-align: left;"><%: ViewRes.Views.ChartReport.Graphics.GeneralAverage%></th> 
                        <th><%: String.Format("{0:0.##}", tot.Value)%>%</th> 
                    </tr>
                    <%} %>
                </tfoot>
        </table>
    </div>