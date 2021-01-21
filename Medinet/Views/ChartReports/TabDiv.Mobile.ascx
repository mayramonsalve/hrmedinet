<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Medinet.Models.ViewModels.ChartReportViewModel>" %>

<% if (Model.test.DemographicsInTests.Select(d => d.Demographic.Name).Contains("Country"))
{ %>
<div id="tabCountry">
    <h1><a href="#tabs" data-rel="dialog" data-role="button" data-iconpos="notext" data-theme="f" data-inline="true" data-icon="bars"></a><%: ViewRes.Views.ChartReport.Graphics.CountryTab%></h1>
</div>
<% } %>
<% if (Model.test.DemographicsInTests.Select(d => d.Demographic.Name).Contains("Region"))
{ %>
<div id="tabRegion">
    <h1><a href="#tabs" data-rel="dialog" data-role="button" data-iconpos="notext" data-theme="f" data-inline="true" data-icon="bars"></a><%: ViewRes.Views.ChartReport.Graphics.RegionTab %></h1>
</div>
<% } %>
<% if (Model.test.DemographicsInTests.Select(d => d.Demographic.Name).Contains("AgeRange"))
{ %>
<div id="tabAgeRange">
    <h1><a href="#tabs" data-rel="dialog" data-role="button" data-iconpos="notext" data-theme="f" data-inline="true" data-icon="bars"></a><%: ViewRes.Views.ChartReport.Graphics.AgeTab %></h1>
</div>
<% } %>
<% if (Model.test.DemographicsInTests.Select(d => d.Demographic.Name).Contains("InstructionLevel"))
{ %>
<div id="tabInstructionLevel">
    <h1><a href="#tabs" data-rel="dialog" data-role="button" data-iconpos="notext" data-theme="f" data-inline="true" data-icon="bars"></a><%: ViewRes.Views.ChartReport.Graphics.InstructionLevelTab %></h1>
</div>
<% } %>
<% if (Model.test.DemographicsInTests.Select(d => d.Demographic.Name).Contains("Location"))
{ %>
<div id="tabLocation">
    <h1><a href="#tabs" data-rel="dialog" data-role="button" data-iconpos="notext" data-theme="f" data-inline="true" data-icon="bars"></a><%: ViewRes.Views.ChartReport.Graphics.LocationTab %></h1>
</div>
<% } %>
<% if (Model.test.DemographicsInTests.Select(d => d.Demographic.Name).Contains("PositionLevel"))
{ %>
<div id="tabPositionLevel">
    <h1><a href="#tabs" data-rel="dialog" data-role="button" data-iconpos="notext" data-theme="f" data-inline="true" data-icon="bars"></a><%: ViewRes.Views.ChartReport.Graphics.PositionLevelTab %></h1>
</div>
<% } %>
<% if (Model.test.DemographicsInTests.Select(d => d.Demographic.Name).Contains("Seniority"))
{ %>
<div id="tabSeniority">
    <h1><a href="#tabs" data-rel="dialog" data-role="button" data-iconpos="notext" data-theme="f" data-inline="true" data-icon="bars"></a><%: ViewRes.Views.ChartReport.Graphics.SeniorityTab %></h1>
</div>
<% } %>
<% if (Model.test.DemographicsInTests.Select(d => d.Demographic.Name).Contains("Gender"))
{ %>
<div id="tabGender">
    <h1><a href="#tabs" data-rel="dialog" data-role="button" data-iconpos="notext" data-theme="f" data-inline="true" data-icon="bars"></a><%: ViewRes.Views.ChartReport.Graphics.GenderTab %></h1>
</div>
<% } %>
<% if (Model.test.DemographicsInTests.Select(d => d.Demographic.Name).Contains("Performance"))
{ %>
<div id="tabPerformance">
    <h1><a href="#tabs" data-rel="dialog" data-role="button" data-iconpos="notext" data-theme="f" data-inline="true" data-icon="bars"></a><%: ViewRes.Views.ChartReport.Graphics.PerformanceTab %></h1>
</div>
<% } %>
<% foreach (var v in Model.FO)
{
    if (Model.GetFOCount(v.Key) > 0)
    {
        %>
    <% if (Model.test.DemographicsInTests.Where(f => f.FOT_Id == v.Key).Count() == 1)
        { %>
        <div id="tabFO-<%:v.Key%>">
            <h1><a href="#tabs" data-rel="dialog" data-role="button" data-iconpos="notext" data-theme="f" data-inline="true" data-icon="bars"></a><%: v.Value %></h1>
        </div>
        <%} %>
    <%}
} %>