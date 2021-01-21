<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Medinet.Models.ViewModels.ChartReportViewModel>" %>

    <div data-role="page" id="tabs" class="basic"  data-close-btn="right">
        <div data-role="header" data-theme="f">
		    <h1><%= ViewRes.Views.ChartReport.Graphics.ChooseSection%></h1>
	    </div>
        <div data-role="content" class="content basic">
            <div>
                <ul data-role="listview">
                    <li><a class="all" href="#tabGeneral"><%: ViewRes.Views.ChartReport.Graphics.General %></a></li>
                    <% if (Model.test.DemographicsInTests.Select(d => d.Demographic.Name).Contains("Country"))
                        { %>
                    <li><a class="all"  href="#tabCountry"><%: ViewRes.Views.ChartReport.Graphics.CountryTab%></a></li>
                    <%} %>
                    <% if (Model.test.DemographicsInTests.Select(d => d.Demographic.Name).Contains("Region"))
                        { %>
                    <li><a class="all"  href="#tabRegion"><%: ViewRes.Views.ChartReport.Graphics.RegionTab%></a></li>
                    <%} %>
                    <% if (Model.test.DemographicsInTests.Select(d => d.Demographic.Name).Contains("AgeRange"))
                        { %>
                    <li><a class="all"  href="#tabAgeRange"><%: ViewRes.Views.ChartReport.Graphics.AgeTab%></a></li>
                    <%} %>
                    <% if (Model.test.DemographicsInTests.Select(d => d.Demographic.Name).Contains("InstructionLevel"))
                        { %>
                    <li><a class="all"  href="#tabInstructionLevel"><%: ViewRes.Views.ChartReport.Graphics.InstructionLevelTab%></a></li>
                    <%} %>
                    <% if (Model.test.DemographicsInTests.Select(d => d.Demographic.Name).Contains("Location"))
                        { %>
                    <li><a class="all"  href="#tabLocation"><%: ViewRes.Views.ChartReport.Graphics.LocationTab%></a></li>
                    <%} %>
                    <% if (Model.test.DemographicsInTests.Select(d => d.Demographic.Name).Contains("PositionLevel"))
                        { %>
                    <li><a class="all"  href="#tabPositionLevel"><%: ViewRes.Views.ChartReport.Graphics.PositionLevelTab%></a></li>
                    <%} %>
                    <% if (Model.test.DemographicsInTests.Select(d => d.Demographic.Name).Contains("Seniority"))
                        { %>
                    <li><a class="all"  href="#tabSeniority"><%: ViewRes.Views.ChartReport.Graphics.SeniorityTab%></a></li>
                    <%} %>
                    <% if (Model.test.DemographicsInTests.Select(d => d.Demographic.Name).Contains("Gender"))
                        { %>
                    <li><a class="all"  href="#tabGender"><%: ViewRes.Views.ChartReport.Graphics.GenderTab%></a></li>
                    <%} %>
                    <% if (Model.test.DemographicsInTests.Select(d => d.Demographic.Name).Contains("Performance"))
                        { %>
                    <li><a class="all" href="#tabPerformance"><%: ViewRes.Views.ChartReport.Graphics.PerformanceTab%></a></li>
                    <% } %>
                        <% string FOids = ""; %>
                        <%foreach (var v in Model.FO){
                        if (Model.GetFOCount(v.Key) > 0)
                        {
                            FOids = FOids + v.Key + '-'; %>
                        <% if (Model.test.DemographicsInTests.Where(f => f.FOT_Id == v.Key).Count() == 1)
                        { %>
                            <li><a class="all" href="#tabFO-<%:v.Key%>"><%: Model.FO[v.Key]%></a></li>
                        <%} %>
                        <%}
                    }%>
                </ul>
            </div>
        </div>
        <%: Html.Hidden("FOids", FOids)%>
        <%--<% Html.RenderPartial("mobile/MobileFooter"); %> --%>
    </div>