<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Medinet.Models.ViewModels.RankingViewModel>" %>
        <div data-role="fieldcontain">
                <label for="SectorsGeneral"><%: ViewRes.Views.ChartReport.Graphics.Sector %>: &nbsp</label>
                    <%if (Model.UserLogged.Role.Name.ToLower() == "hradministrator")
                        { %>
			            <%: Html.DropDownList("SectorsGeneral", Model.sectorsList, ViewRes.Scripts.Shared.Select, new { @id = "SectorsGeneral", @class = "Sectors input-background short" })%>
                    <%}
                        else
                        {%>
                        <span id="SectorsGeneral"><%:Model.UserLogged.Company.CompanySector.Name%></span>
                    <%} %>
        </div>
        <div>
            <div>
            <div id="DivSelectGeneral" class="alignCenter">
                <% string style = "";
                    if (Model.UserLogged.Role.Name == "HRAdministrator")
                    { %>
                    <h5><%: ViewRes.Views.ChartReport.Ranking.SelectSector %></h5>
                    <% style = "display:none;";%>
                    <%} %>
            </div>
            <table id="RankingGeneral" class="table1" style="display:none;">
                 <thead>
                    <tr> 
                         <th><%: ViewRes.Views.ChartReport.Graphics.CompanyTab%></th>
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
                            <% if (key == Model.UserLogged.Company.Name)
                               {%>
                                    <td class="alignLeft"><%:key%></td>
                             <%}
                               else
                               { %>
                                    <td class="alignLeft">&nbsp;</td>
                             <%} %>
                            <td class="alignCenter"><%:pos%></td>
                            <td class="alignCenter"><%:String.Format("{0:0.##}", Model.ranking[key])%>%</td>
                        </tr>
                     <%}
                   } %>
                </tbody>
            </table>
            <div id="NoCompaniesGeneral" style="display:none" class="alignCenter"><h5><%: ViewRes.Views.ChartReport.Ranking.NoCompanies %></h5></div>
            </div>            
        </div>

        <script type="text/javascript">
                $(".table1").tablesorter();
        </script>