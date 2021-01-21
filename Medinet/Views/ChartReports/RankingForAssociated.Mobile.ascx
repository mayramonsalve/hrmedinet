<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Medinet.Models.ViewModels.RankingViewModel>" %>  
        <div data-role="fieldcontain">
                <label for="SectorsCustomer"><%: ViewRes.Views.ChartReport.Graphics.Sector %>: &nbsp</label>
                    <%if (Model.UserLogged.Role.Name.ToLower() == "hradministrator")
                        { %>
			            <%: Html.DropDownList("SectorsCustomer", Model.sectorsList, ViewRes.Scripts.Shared.Select, new { @id = "SectorsCustomer", @class = "Sectors input-background short" })%>
                    <%}
                        else
                        {%>
                        <span id="SectorsGeneral"><%:Model.UserLogged.Company.CompanySector.Name%></span>
                    <%} %>
        </div>

        <div>
            <div id="DivSelectCustomer" class="alignCenter">
                <h5><%: ViewRes.Views.ChartReport.Ranking.SelectSector %></h5>
            </div>
            <table id="RankingCustomer" class="table1" style="display:none">
                 <thead>
                    <tr> 
                         <th><%: ViewRes.Views.ChartReport.Graphics.CompanyTab%></th>
                         <th><%: ViewRes.Views.ChartReport.Graphics.Ranking%></th>
                         <th><%: ViewRes.Views.ChartReport.Graphics.Climate%></th>
                    </tr>
                 </thead>  
                <tbody>
                </tbody>
            </table>
            <div id="NoCompaniesCustomer" style="display:none" class="alignCenter"><h5><%: ViewRes.Views.ChartReport.Ranking.NoCompanies %></h5>
            </div>
        </div>