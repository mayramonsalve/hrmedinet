<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Medinet.Models.ViewModels.ResultViewModel>" %>
<%@ Import Namespace="MedinetClassLibrary.CustomClasses" %>
<%@ Import Namespace="MedinetClassLibrary.Models" %>
    <div class="prepend-1 span-22 append-1">
        <table id="R5-<%: Model.questionnaireToUse %>" class="display tabla table1">
             <thead>
                <tr> 
                     <th><%: ViewRes.Views.ChartReport.Graphics.CategoryTab %></th>
                     <th><%: ViewRes.Views.ChartReport.Graphics.PositivePercentage %></th>
                     <th><%: ViewRes.Views.ChartReport.Graphics.PositiveMarket %></th>
                     <th><%: ViewRes.Views.ChartReport.Graphics.Difference %></th>
                </tr>
             </thead>  
            <tbody>
                <%foreach ( var res in Model.stringDoubleVector ) { %>
                    <% if (res.Key != "Total")
                        {%>
                        <tr>
                            <td align="left"><%:res.Key%></td> 
                            <td style="text-align: center;"><%: String.Format("{0:0.##}", res.Value[0])%>%</td> 
                            <td style="text-align: center;"><%: String.Format("{0:0.##}", res.Value[1])%>%</td> 
                            <td style="text-align: center;"><%: String.Format("{0:0.##}", res.Value[2])%>%</td> 
                        </tr>
                    <%}%>
                <%} %>
            </tbody>
            <tfoot>
                <tr>
                    <% KeyValuePair<string, double[]> tot = Model.stringDoubleVector.Last(); %>
                    <th style="text-align: left;"><%: ViewRes.Views.ChartReport.Graphics.GeneralAverage %></th> 
                    <th><%: String.Format("{0:0.##}", tot.Value[0])%>%</th> 
                    <th><%: String.Format("{0:0.##}", tot.Value[1])%>%</th> 
                    <th><%: String.Format("{0:0.##}", tot.Value[2])%>%</th>                 
                </tr>
            </tfoot>
        </table>
    </div>