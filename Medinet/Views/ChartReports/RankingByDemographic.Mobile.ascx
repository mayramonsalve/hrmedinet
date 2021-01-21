<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Medinet.Models.ViewModels.RankingViewModel>" %>

<% string demographic = ViewData["option"].ToString();
   string FO_id = ViewData["FO_id"] != null ? ViewData["FO_id"].ToString() : ""; %>
    <div class="prepend-4 span-16 apend-4">
            <div>
            <table id="Ranking<%:demographic%><%:FO_id %>" class="table1">
                 <thead>
                    <tr> 
                         <th><%: ViewRes.Views.ChartReport.Graphics.Name%></th>
                         <th><%: ViewRes.Views.ChartReport.Graphics.Ranking%></th>
                         <th><%: ViewRes.Views.ChartReport.Graphics.Climate%></th>
                    </tr>
                 </thead>  
                <tbody>
                <% if (Model.ranking != null)
                   {
                       int pos = 0;
                       foreach (string key in Model.ranking.Keys)
                       {
                           pos++;%>
                        <tr>
                            <td class="alignLeft"><%:key%></td>
                            <td class="alignCenter"><%:pos%></td>
                            <td class="alignCenter"><%:String.Format("{0:0.##}", Model.ranking[key])%>%</td>
                        </tr>
                     <%}
                   } %>
                </tbody>
            </table>
            </div>
            <div id="NoCompanies<%:demographic%><%:FO_id %>" style="display:none"><p><h5><%: ViewRes.Views.ChartReport.Ranking.NoDemographics %></h5></p></div>
    </div>