<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Medinet.Models.ViewModels.ResultViewModel>" %>
<%@ Import Namespace="MedinetClassLibrary.CustomClasses" %>
<%@ Import Namespace="MedinetClassLibrary.Models" %>
    <div class="prepend-1 span-22 append-1">
               <% int options = Model.optionsCount; %> 
        <table id="R2-<%: Model.questionnaireToUse %>" class="tabla display table1">
             <thead>
                <tr> 
                     <th><%: ViewRes.Views.ChartReport.Graphics.CategoryTab %></th>
                     <th><%: ViewRes.Views.ChartReport.Graphics.PositivePercentage %></th>
                     <%if ((options % 2) != 0)
                       { %>
                     <th><%: ViewRes.Views.ChartReport.Graphics.NeutralPercentage %></th>
                     <%} %>
                     <th><%: ViewRes.Views.ChartReport.Graphics.NegativePercentage %></th>
                </tr>
             </thead>  
            <tbody>
                <%foreach ( var res in Model.stringDoubleVector ) { %>
                    <% if (res.Key != "Total")
                       {%>
                        <tr>
                            <td><%:res.Key%></td> 
                            <td style="text-align: center;"><%: String.Format("{0:0.##}",res.Value[0])%>%</td> 
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
                <%if (Model.stringDoubleVector.Count > 0)
                    {
                        KeyValuePair<string, double[]> tot = Model.stringDoubleVector.Last();%>
                <tr>
                    <th style="text-align: left;"><%: ViewRes.Views.ChartReport.Graphics.GeneralAverage %></th> 
                    <th><%: String.Format("{0:0.##}", tot.Value[0])%>%</th> 
                    <%if ((options % 2) != 0)
                        { %>
                    <th><%: String.Format("{0:0.##}", tot.Value[1])%>%</th> 
                    <%} %>
                    <th><%: String.Format("{0:0.##}", tot.Value[((options % 2) != 0) ? 2 : 1])%>%</th> 
                </tr>
                <%} %>
            </tfoot>                
        </table>
    </div>