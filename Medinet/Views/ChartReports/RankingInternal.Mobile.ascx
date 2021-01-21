<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Medinet.Models.ViewModels.RankingViewModel>" %>

                    <% string style = "";
                        if (Model.UserLogged.Role.Name == "HRAdministrator")
                       {
                           style = "display:none;";%>
                        <div data-role="fieldcontain">
                            <label for="CompaniesGeneral"><%: ViewRes.Views.ChartReport.Graphics.Company%>:&nbsp</label>
			                <%: Html.DropDownList("CompaniesInternal", Model.companiesList, ViewRes.Scripts.Shared.Select, new { @id = "CompaniesGeneral", @class = "Companies input-background short" })%>
                        </div>
                     <%} %>
                    <div>
                        <div id="DemographicsDiv">
                            <% if (Model.UserLogged.Role.Name != "HRAdministrator")
                               { %>
                                <%Html.RenderPartial("RankingTabs", Model); %>
                             <%}%>
                        </div>
                        <% if (Model.UserLogged.Role.Name == "HRAdministrator")
                               { %> 
                                    <div>
                                        <div id="DivSelectInternal" class="alignCenter">
                                            
                                            <h5><%: ViewRes.Views.ChartReport.Ranking.SelectCompany%></h5>
                                            
                                        </div>
                                    </div>
                             <%} %>
                    </div>