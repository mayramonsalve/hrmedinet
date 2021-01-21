<%@ Page Title="" Debug="true" Language="C#" MasterPageFile="~/Views/Shared/Mobile/MobileMini.Master" Inherits="System.Web.Mvc.ViewPage<Medinet.Models.ViewModels.ChartReportViewModel>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    <%: ViewRes.Views.Shared.Shared.GenericTitle %> <%: ViewRes.Views.ChartReport.Graphics.UnivariateTitle %>
</asp:Content>

<asp:Content ID="Content7" ContentPlaceHolderID="PageId" runat="server">
<div data-role="page" id="mainChart" class="basic">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
          <%: Html.Hidden("Select", ViewRes.Scripts.Shared.ShowAll, new { id = "ViewRes" })%>
          <%: Html.Hidden("test_id",Model.test.Id)%>
          <%: Html.Hidden("compare_id",Model.testCompare)%>
          <%: Html.Hidden("General", 1)%>
          <%: Html.Hidden("AllTests", 1)%>
          <%: Html.Hidden("Location", Model.demographicsCount["Location"])%>
          <%: Html.Hidden("AgeRange", Model.demographicsCount["AgeRange"])%>
          <%: Html.Hidden("Country", Model.demographicsCount["Country"])%>
          <%: Html.Hidden("Region", Model.demographicsCount["Region"])%>
          <%: Html.Hidden("InstructionLevel", Model.demographicsCount["InstructionLevel"])%>
          <%: Html.Hidden("PositionLevel", Model.demographicsCount["PositionLevel"])%>
          <%: Html.Hidden("Seniority", Model.demographicsCount["Seniority"])%>
          <%: Html.Hidden("Gender", Model.demographicsCount["Gender"])%>
          <%: Html.Hidden("Performance", Model.demographicsCount["Performance"])%>
            <%: Html.Hidden("chartType", "Univariate")%>
            <%: Html.Hidden("chartModel", "Column")%>
            <% string FOids = ""; %>
          <% int i = 0; 
              foreach (var v in Model.FO)
              {%>
                <%: Html.Hidden("FunctionalOrganizationType"+v.Key, Model.GetFOCount(v.Key))%>
            <% }%>
            <h1 class="testname"><%: ViewRes.Views.ChartReport.Graphics.UnivariateTitle %></h1>
            <div id="myTabs" class="box rounded tabs">
                <div id="tabGeneral">
                    <h1><a href="#tabs" data-rel="dialog" data-role="button" data-iconpos="notext" data-theme="f" data-inline="true" data-icon="bars"></a><%: ViewRes.Views.ChartReport.Graphics.GeneralClimate %></h1>
                    <%ViewData["option"] = "General"; %>
                    <%Html.RenderPartial("ChartsForm", Model); %>
                </div>

                    <% if (Model.test.DemographicsInTests.Select(d => d.Demographic.Name).Contains("Country"))
                       { %>
                <div id="tabCountry">
                    <h1><a href="#tabs" data-rel="dialog" data-role="button" data-iconpos="notext" data-theme="f" data-inline="true" data-icon="bars"></a><%: ViewRes.Views.ChartReport.Graphics.CountryTab%></h1>
                    <div class="fadingTooltip" id="fadingTooltip" style="Z-INDEX: 999; VISIBILITY: hidden; POSITION: absolute"></div>                         
                </div>
                    <% } %>
                    <% if (Model.test.DemographicsInTests.Select(d => d.Demographic.Name).Contains("Region"))
                       { %>
                <div id="tabRegion">
                <h1><a href="#tabs" data-rel="dialog" data-role="button" data-iconpos="notext" data-theme="f" data-inline="true" data-icon="bars"></a><%: ViewRes.Views.ChartReport.Graphics.RegionTab%></h1>   
                </div>
                <% } %>
                    <% if (Model.test.DemographicsInTests.Select(d => d.Demographic.Name).Contains("AgeRange"))
                       { %>
                <div id="tabAgeRange">
                    <h1><a href="#tabs" data-rel="dialog" data-role="button" data-iconpos="notext" data-theme="f" data-inline="true" data-icon="bars"></a><%: ViewRes.Views.ChartReport.Graphics.AgeTab%></h1>   
                </div>
                    <% } %>
                    <% if (Model.test.DemographicsInTests.Select(d => d.Demographic.Name).Contains("InstructionLevel"))
                       { %>
                <div id="tabInstructionLevel">
                <h1><a href="#tabs" data-rel="dialog" data-role="button" data-iconpos="notext" data-theme="f" data-inline="true" data-icon="bars"></a><%: ViewRes.Views.ChartReport.Graphics.InstructionLevelTab%></h1>   
                </div>
                    <% } %>
                    <% if (Model.test.DemographicsInTests.Select(d => d.Demographic.Name).Contains("Location"))
                       { %>
                <div id="tabLocation">
                <h1><a href="#tabs" data-rel="dialog" data-role="button" data-iconpos="notext" data-theme="f" data-inline="true" data-icon="bars"></a><%: ViewRes.Views.ChartReport.Graphics.LocationTab%></h1>   
                </div>
                <% } %>
                    <% if (Model.test.DemographicsInTests.Select(d => d.Demographic.Name).Contains("PositionLevel"))
                       { %>
                <div id="tabPositionLevel">
                <h1><a href="#tabs" data-rel="dialog" data-role="button" data-iconpos="notext" data-theme="f" data-inline="true" data-icon="bars"></a><%: ViewRes.Views.ChartReport.Graphics.PositionLevelTab%></h1>   
                </div>
                    <% } %>
                    <% if (Model.test.DemographicsInTests.Select(d => d.Demographic.Name).Contains("Seniority"))
                       { %>
                <div id="tabSeniority">
                <h1><a href="#tabs" data-rel="dialog" data-role="button" data-iconpos="notext" data-theme="f" data-inline="true" data-icon="bars"></a><%: ViewRes.Views.ChartReport.Graphics.SeniorityTab%></h1>   
                </div>
                    <% } %>
                    <% if (Model.test.DemographicsInTests.Select(d => d.Demographic.Name).Contains("Gender"))
                       { %>
                <div id="tabGender">
                <h1><a href="#tabs" data-rel="dialog" data-role="button" data-iconpos="notext" data-theme="f" data-inline="true" data-icon="bars"></a><%: ViewRes.Views.ChartReport.Graphics.GenderTab
                %></h1>   
                </div>
                    <% } %>
                    <% if (Model.test.DemographicsInTests.Select(d => d.Demographic.Name).Contains("Performance"))
                       { %>
                <div id="tabPerformance">
                <h1><a href="#tabs" data-rel="dialog" data-role="button" data-iconpos="notext" data-theme="f" data-inline="true" data-icon="bars"></a><%: ViewRes.Views.ChartReport.Graphics.PerformanceTab%></h1>   
                </div>
                    <% } %>
                <% foreach (var v in Model.FO)
                    {
                        if (Model.GetFOCount(v.Key) > 0)
                        {%>
                        <% if (Model.test.DemographicsInTests.Where(f => f.FOT_Id == v.Key).Count() == 1)
                        { %>
                        <div id="tabFO-<%:v.Key%>">
                            <h1><a href="#tabs" data-rel="dialog" data-role="button" data-iconpos="notext" data-theme="f" data-inline="true" data-icon="bars"></a><%: v.Value %></h1>
                        </div>
                        <%} %>
                        <%}
                  }
                    if (Model.countTestsInQuestionnaire > 1)
                    {
                    %>
                    <div id="tabAllTests">
                        <h1><a href="#tabs" data-rel="dialog" data-role="button" data-iconpos="notext" data-theme="f" data-inline="true" data-icon="bars"></a><%: ViewRes.Views.ChartReport.Graphics.AllTests%></h1>
                    </div>
                    <%} %>
                     <%Html.RenderPartial("Button.Mobile"); %>
            </div>

</asp:Content>

<asp:Content ID="Content6" ContentPlaceHolderID="PageContent" runat="server">

    <div data-role="page" id="tabs" class="basic"  data-close-btn="right">
        <div data-role="header" data-theme="f">
		    <h1><%= ViewRes.Views.ChartReport.Graphics.ChooseSection%></h1>
	    </div>
        <div data-role="content" class="content basic">
            <div>
                <ul data-role="listview">
                    <li><a class ="all" href="#tabGeneral"><%: ViewRes.Views.ChartReport.Graphics.GeneralClimate %></a></li>
                    <%--<li><a class ="all" href="#tabCategory"><%: ViewRes.Views.ChartReport.Graphics.CategoryTab %></a></li>--%>
                    <% if (Model.test.DemographicsInTests.Select(d => d.Demographic.Name).Contains("Country"))
                       { %>
                    <li><a class ="all" href="#tabCountry"><%: ViewRes.Views.ChartReport.Graphics.CountryTab%></a></li>
                    <%} %>
                    <% if (Model.test.DemographicsInTests.Select(d => d.Demographic.Name).Contains("Region"))
                       { %>
                    <li><a class ="all" href="#tabRegion"><%: ViewRes.Views.ChartReport.Graphics.RegionTab%></a></li>
                    <%} %>
                    <% if (Model.test.DemographicsInTests.Select(d => d.Demographic.Name).Contains("AgeRange"))
                       { %>
                    <li><a class ="all" href="#tabAgeRange"><%: ViewRes.Views.ChartReport.Graphics.AgeTab%></a></li>
                    <%} %>
                    <% if (Model.test.DemographicsInTests.Select(d => d.Demographic.Name).Contains("InstructionLevel"))
                       { %>
                    <li><a class ="all" href="#tabInstructionLevel"><%: ViewRes.Views.ChartReport.Graphics.InstructionLevelTab%></a></li>
                    <%} %>
                    <% if (Model.test.DemographicsInTests.Select(d => d.Demographic.Name).Contains("Location"))
                       { %>
                    <li><a class ="all" href="#tabLocation"><%: ViewRes.Views.ChartReport.Graphics.LocationTab%></a></li>
                    <%} %>
                    <% if (Model.test.DemographicsInTests.Select(d => d.Demographic.Name).Contains("PositionLevel"))
                       { %>
                    <li><a class ="all" href="#tabPositionLevel"><%: ViewRes.Views.ChartReport.Graphics.PositionLevelTab%></a></li>
                    <%} %>
                    <% if (Model.test.DemographicsInTests.Select(d => d.Demographic.Name).Contains("Seniority"))
                       { %>
                    <li><a class ="all" href="#tabSeniority"><%: ViewRes.Views.ChartReport.Graphics.SeniorityTab%></a></li>
                    <%} %>
                    <% if (Model.test.DemographicsInTests.Select(d => d.Demographic.Name).Contains("Gender"))
                       { %>
                    <li><a class ="all" href="#tabGender"><%: ViewRes.Views.ChartReport.Graphics.GenderTab%></a></li>
                    <%} %>
                    <% if (Model.test.DemographicsInTests.Select(d => d.Demographic.Name).Contains("Performance"))
                       { %>
                    <li><a class ="all" href="#tabPerformance"><%: ViewRes.Views.ChartReport.Graphics.PerformanceTab%></a></li>
                    <% } %>
                    <% string FOids = ""; %>
                     <%foreach (var v in Model.FO){
                        if (Model.GetFOCount(v.Key) > 0)
                        {
                            FOids = FOids + v.Key + '-'; %>
                        <% if (Model.test.DemographicsInTests.Where(f => f.FOT_Id == v.Key).Count() == 1)
                        { %>
                            <li><a class ="all" href="#tabFO-<%:v.Key%>"><%: Model.FO[v.Key]%></a></li>
                        <%} %>
                        <%}
                    }
                    if (Model.countTestsInQuestionnaire > 1)
                    {
                    %>
                    <li><a class ="all" href="#tabAllTests"><%: ViewRes.Views.ChartReport.Graphics.AllTests%></a></li>
                    <%} %>
                </ul>
            </div>
        </div>
        <%: Html.Hidden("FOids", FOids)%>
        <%--<% Html.RenderPartial("mobile/MobileFooter"); %> --%>
    </div>
    </asp:Content>

<asp:Content ID="Content5" ContentPlaceHolderID="JsContent" runat="server">
<%--    <script src="../../Scripts/loading.js" type="text/javascript"></script>--%>
 <% if (Model.test.DemographicsInTests.Select(d => d.Demographic.Name).Contains("Country"))
     { %>
        <script src="<%: Url.Content("~/Scripts/resultReports-views.js") %>" type="text/javascript"></script>
    <%} %>
<script src="<%: Url.Content("~/Scripts/mobile/jquery.tablesorter.js") %>" type="text/javascript"></script>
<script src="<%: Url.Content("~/Scripts/mobile/ChartsMobile.js?mm=0709") %>" type="text/javascript"></script>
</asp:Content>
