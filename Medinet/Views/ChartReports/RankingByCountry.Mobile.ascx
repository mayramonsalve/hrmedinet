<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Medinet.Models.ViewModels.RankingViewModel>" %>
    
        <div data-role="fieldcontain">
                <label for="SectorsGeneralCountry"><%: ViewRes.Views.ChartReport.Graphics.Sector %> : </label>
                <%if (Model.UserLogged.Role.Name == "HRAdministrator")
                { %>
			    <%: Html.DropDownList("SectorsGeneralCountry", Model.sectorsList, ViewRes.Scripts.Shared.Select, new { @id = "SectorsGeneralCountry", @class = "Sectors input-background short" })%>
                <%}
                else
                {%>
                <span id= "SectorsGeneralCountry" ><%:Model.UserLogged.Company.CompanySector.Name%></span><br />
                <%} %>

                <label for="CountriesGeneralCountry"><%: ViewRes.Views.ChartReport.Graphics.Country %></label>
			    <%: Html.DropDownList("CountriesGeneralCountry", Model.countriesList, ViewRes.Scripts.Shared.Select, new { @id = "CountriesGeneralCountry", @class = "Countries input-background short" })%>
        </div>


        <div>
            <div id="DivSelectGeneralCountry" class="alignCenter">
                <%if (Model.UserLogged.Role.Name == "HRAdministrator")
                    { %>
                    <h5><%: ViewRes.Views.ChartReport.Ranking.SelectSectorAndCountry%></h5>
                <%}
                else
                {%>
                    <h5><%: ViewRes.Views.ChartReport.Ranking.SelectCountry%></h5>
                <%} %>
            </div>
            <table id="RankingGeneralCountry" class="table1" style = "display:none;">
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
            <div id="NoCompaniesGeneralCountry" style="display:none" class="alignCenter"><p><h5><%: ViewRes.Views.ChartReport.Ranking.NoCompanies %></h5></p>
            </div>
        </div>