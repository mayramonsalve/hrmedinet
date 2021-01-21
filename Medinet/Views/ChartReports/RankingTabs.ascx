<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Medinet.Models.ViewModels.RankingViewModel>" %>
                     <% string FOids = ""; %>
            <div id="myInternalTabs" class="myTabs">
                <ul>
                <% MedinetClassLibrary.Models.Questionnaire questionnaire = new MedinetClassLibrary.Services.QuestionnairesServices().GetById(Model.questionnaireId);
                   List<string> demog = questionnaire.Tests.SelectMany(dit => dit.DemographicsInTests.Select(d => d.Demographic.Name)).Distinct().ToList();%>
                    <% if (demog.Contains("Gender"))
                   { %>
                    <li><a href="#tabGender"><%: ViewRes.Views.ChartReport.Graphics.GenderTab%></a></li>
                    <%} %>
                    <% if (demog.Contains("Country"))
                       { %>
                    <li><a class ="all" href="#tabCountry"><%: ViewRes.Views.ChartReport.Graphics.CountryTab%></a></li>
                    <%} %>
                    <% if (demog.Contains("Region"))
                       { %>
                    <li><a class ="all" href="#tabRegion"><%: ViewRes.Views.ChartReport.Graphics.RegionTab %></a></li>
                    <%} %>
                    <% if (demog.Contains("AgeRange"))
                       { %>
                    <li><a class ="all" href="#tabAgeRange"><%: ViewRes.Views.ChartReport.Graphics.AgeTab%></a></li>
                    <%} %>
                    <% if (demog.Contains("InstructionLevel"))
                       { %>
                    <li><a class ="all" href="#tabInstructionLevel"><%: ViewRes.Views.ChartReport.Graphics.InstructionLevelTab%></a></li>
                    <%} %>
                    <% if (demog.Contains("Location"))
                       { %>
                    <li><a class ="all" href="#tabLocation"><%: ViewRes.Views.ChartReport.Graphics.LocationTab%></a></li>
                    <%} %>
                    <% if (demog.Contains("PositionLevel"))
                       { %>
                    <li><a class ="all" href="#tabPositionLevel"><%: ViewRes.Views.ChartReport.Graphics.PositionLevelTab%></a></li>
                    <%} %>
                    <% if (demog.Contains("Seniority"))
                       { %>
                    <li><a class ="all" href="#tabSeniority"><%: ViewRes.Views.ChartReport.Graphics.SeniorityTab%></a></li>
                    <%} %>
                    <% if (demog.Contains("Peformance"))
                       { %>
                        <li><a class ="all" href="#tabPerformance"><%: ViewRes.Views.ChartReport.Graphics.PerformanceTab%></a></li>
                    <% } %>
                    <% if (demog.Contains("FunctionalOrganizationType"))
                       {
                           foreach (var v in Model.FO)
                           {
                               if (Model.GetFOCount(v.Key) > 0)
                               {
                                   FOids = FOids + v.Key + '-';%>
                                <li><a class="all" href="#tabFO-<%:v.Key%>"><%: Model.FO[v.Key]%></a></li>
                            <% }
                           }
                       }%>
                </ul>
                <%: Html.Hidden("FOids", FOids)%>
                <% if (demog.Contains("Gender"))
                   { %>
                <div id="tabGender">
                    <%ViewData["option"] = "Gender"; %>
                    <%Html.RenderPartial("RankingByDemographic", Model); %>
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