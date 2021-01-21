<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Medinet.Models.ViewModels.RankingViewModel>" %>
            <% string FOids = ""; %>
            <% MedinetClassLibrary.Models.Questionnaire questionnaire = new MedinetClassLibrary.Services.QuestionnairesServices().GetById(Model.questionnaireId);
            List<string> demog = questionnaire.Tests.SelectMany(dit => dit.DemographicsInTests.Select(d => d.Demographic.Name)).Distinct().ToList();
            %>
            <div id="myInternalTabs" class="myTabs">
                <div data-role="fieldcontain">
                    <label for="selectDemographic"><%: ViewRes.Views.ChartReport.Graphics.Demographics %>:</label>
                    <select name="" id="selectDemographic" class="demTabs" data-theme="f" >
                        <option value="#tabSelect" class="allRanking2"><%: ViewRes.Scripts.Shared.Select %></option>
                        <% if (demog.Contains("Gender"))
                        { %>
                        <option value="#tabGender" class="allRanking2"><%: ViewRes.Views.ChartReport.Graphics.GenderTab %></option>
                        <%} %>
                        <% if (demog.Contains("Country"))
                           { %>
                        <option value="#tabCountry" class="allRanking2"><%: ViewRes.Views.ChartReport.Graphics.CountryTab%></option>
                        <%} %>
                        <% if (demog.Contains("Region"))
                           { %>
                        <option value="#tabRegion" class="allRanking2"><%: ViewRes.Views.ChartReport.Graphics.RegionTab%></option>
                        <%} %>
                        <% if (demog.Contains("AgeRange"))
                           { %>
                        <option value="#tabAgeRange" class="allRanking2"><%: ViewRes.Views.ChartReport.Graphics.AgeTab%></option>
                        <%} %>
                        <% if (demog.Contains("InstructionLevel"))
                           { %>
                        <option value="#tabInstructionLevel" class="allRanking2"><%: ViewRes.Views.ChartReport.Graphics.InstructionLevelTab%></option>
                        <%} %>
                        <% if (demog.Contains("Location"))
                           { %>
                        <option value="#tabLocation" class="allRanking2"><%: ViewRes.Views.ChartReport.Graphics.LocationTab%></option>
                        <%} %>
                        <% if (demog.Contains("PositionLevel"))
                           { %>
                        <option value="#tabPositionLevel" class="allRanking2"><%: ViewRes.Views.ChartReport.Graphics.PositionLevelTab%></option>
                        <%} %>
                        <% if (demog.Contains("Seniority"))
                           { %>
                        <option value="#tabSeniority" class="allRanking2"><%: ViewRes.Views.ChartReport.Graphics.SeniorityTab%></option>
                        <%} %>
                        <% if (demog.Contains("Peformance"))
                           { %>
                        <option value="#tabPerformance" class="allRanking2"><%: ViewRes.Views.ChartReport.Graphics.PerformanceTab%></option>
                        <% } %>
                        <% if (demog.Contains("FunctionalOrganizationType"))
                           {
                               foreach (var v in Model.FO)
                               {
                                   if (Model.GetFOCount(v.Key) > 0)
                                   {
                                       FOids = FOids + v.Key + '-';%>
                                    <option value="#tabFO-<%:v.Key%>" class="allRanking2"><%: Model.FO[v.Key]%></option>
                                <% }
                               }
                           }%>
                    </select>

                </div>
               <div id="tabSelect" style="text-align:center">
                <h5><%: ViewRes.Views.ChartReport.Graphics.SelectDemographic%></h5>
               </div>
                <%: Html.Hidden("FOids", FOids)%>
                <% if (demog.Contains("Gender"))
                   { %>
                <div id="tabGender">
<%--                    <%ViewData["option"] = "Gender"; %>
                    <%Html.RenderPartial("RankingByDemographic", Model); %>--%>
                </div>
                <%} %>
                <% if (demog.Contains("Country"))
                   { %>
                <div id="tabCountry">
                </div>
                <%} %>
                <% if (demog.Contains("Region"))
                       { %>
                <div id="tabRegion">
                </div>
                <%} %>
                <% if (demog.Contains("AgeRange"))
                   { %>
                <div id="tabAgeRange">
                </div>
                <%} %>
                <% if (demog.Contains("InstructionLevel"))
                   { %>
                <div id="tabInstructionLevel">
                </div>
                <%} %>
                <% if (demog.Contains("Location"))
                       { %>
                <div id="tabLocation">
                </div>
                <%} %>
                <% if (demog.Contains("PositionLevel"))
                   { %>
                <div id="tabPositionLevel">
                </div>
                <%} %>
                <% if (demog.Contains("Seniority"))
                   { %>
                <div id="tabSeniority">
                </div>
                <%} %>
                <% if (demog.Contains("Performance"))
                       { %>
                <div id="tabPerformance">
                </div>
                <% } %>
                 <% if (demog.Contains("FunctionalOrganizationType"))
                    {
                        foreach (var v in Model.FO)
                        {
                            if (Model.GetFOCount(v.Key) > 0)
                            {%> 
                         <div id="tabFO-<%:v.Key%>">
                        </div>
                        <%}
                        }
                    }%>
        </div>  
        <script type="text/javascript">
            $(".table1").tablesorter();
        </script>