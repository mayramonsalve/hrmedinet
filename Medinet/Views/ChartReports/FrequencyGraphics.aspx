<%@ Page Title="" Debug="true" Language="C#" MasterPageFile="~/Views/Shared/Reports.Master" Inherits="System.Web.Mvc.ViewPage<Medinet.Models.ViewModels.ChartReportViewModel>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <h2><%:ViewRes.Views.ChartReport.Graphics.FrequencyTitle%></h2>

          <%: Html.Hidden("Select", ViewRes.Scripts.Shared.ShowAll, new { id = "ViewRes" })%>
          <%: Html.Hidden("test_id",Model.test.Id)%>
          <%: Html.Hidden("General", 1)%>
          <%: Html.Hidden("Location", Model.demographicsCount["Location"])%>
          <%: Html.Hidden("AgeRange", Model.demographicsCount["AgeRange"])%>
          <%: Html.Hidden("Country", Model.demographicsCount["Country"])%>
          <%: Html.Hidden("Region", Model.demographicsCount["Region"])%>
          <%: Html.Hidden("InstructionLevel", Model.demographicsCount["InstructionLevel"])%>
          <%: Html.Hidden("PositionLevel", Model.demographicsCount["PositionLevel"])%>
          <%: Html.Hidden("Seniority", Model.demographicsCount["Seniority"])%>
          <%: Html.Hidden("Gender", Model.demographicsCount["Gender"])%>
          <%: Html.Hidden("Performance", Model.demographicsCount["Performance"])%>
            <%: Html.Hidden("chartType", "Frequency")%>
            <%: Html.Hidden("chartModel", "Bar")%>
                <%: Html.Hidden("OptionsInQuestionnaire", Model.test.Questionnaire.Options.Count)%>
            <% string FOids = ""; %>
          <% int i = 0; 
              foreach (var v in Model.FO)
              {%>
                <%: Html.Hidden("FunctionalOrganizationType"+v.Key, Model.GetFOCount(v.Key))%>
            <% }%>
      
            <div id="myTabs" class="span-24 column last">
                <ul >
                    <li><a href="#tabGeneral"><%: ViewRes.Views.ChartReport.Graphics.GeneralFrequency %></a></li>
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
                     <%foreach (var v in Model.FO){
                        if (Model.GetFOCount(v.Key) > 0)
                        {
                            FOids = FOids + v.Key + '-'; %>
                        <% if (Model.test.DemographicsInTests.Where(f => f.FOT_Id == v.Key).Count() == 1)
                        { %>
                            <li><a class ="all" href="#tabFO-<%:v.Key%>"><%: Model.FO[v.Key]%></a></li>
                        <%} %>
                        <%}
                    }%>
                </ul>
                <%: Html.Hidden("FOids", FOids)%>

                <div id="tabGeneral">
                    <%ViewData["option"] = "General"; %>
                    <%Html.RenderPartial("FrequencyTablesForm", Model); %>
                </div>
                    <% if (Model.test.DemographicsInTests.Select(d => d.Demographic.Name).Contains("Country"))
                       { %>
                <div id="tabCountry">
                    <div class="fadingTooltip" id="fadingTooltip" style="Z-INDEX: 999; VISIBILITY: hidden; POSITION: absolute"></div>     
                </div>
                    <% } %>
                    <% if (Model.test.DemographicsInTests.Select(d => d.Demographic.Name).Contains("Region"))
                       { %>
                <div id="tabRegion">
                </div>
                <% } %>
                    <% if (Model.test.DemographicsInTests.Select(d => d.Demographic.Name).Contains("AgeRange"))
                       { %>
                <div id="tabAgeRange">
                </div>
                    <% } %>
                    <% if (Model.test.DemographicsInTests.Select(d => d.Demographic.Name).Contains("InstructionLevel"))
                       { %>
                <div id="tabInstructionLevel">
                </div>
                    <% } %>
                    <% if (Model.test.DemographicsInTests.Select(d => d.Demographic.Name).Contains("Location"))
                       { %>
                <div id="tabLocation">
                </div>
                <% } %>
                    <% if (Model.test.DemographicsInTests.Select(d => d.Demographic.Name).Contains("PositionLevel"))
                       { %>
                <div id="tabPositionLevel">
                </div>
                    <% } %>
                    <% if (Model.test.DemographicsInTests.Select(d => d.Demographic.Name).Contains("Seniority"))
                       { %>
                <div id="tabSeniority">
                </div>
                    <% } %>
                    <% if (Model.test.DemographicsInTests.Select(d => d.Demographic.Name).Contains("Gender"))
                       { %>
                <div id="tabGender">
                </div>
                    <% } %>
                    <% if (Model.test.DemographicsInTests.Select(d => d.Demographic.Name).Contains("Performance"))
                       { %>
                <div id="tabPerformance">
                </div>
                    <% } %>
                <% foreach (var v in Model.FO)
                    {
                        if (Model.GetFOCount(v.Key) > 0)
                        {%>
                        <% if (Model.test.DemographicsInTests.Where(f => f.FOT_Id == v.Key).Count() == 1)
                        { %>
                         <div id="tabFO-<%:v.Key%>">
                        </div>
                        <%} %>
                        <%}
                  } %>
            </div>
    <div class="span-22 column last alignRight">
        <%: Html.ActionLink(ViewRes.Views.ChartReport.ReportsList.BackToList, "ReportsList", "ChartReports")%>
    </div>
    <div id="loading"></div>
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>

<asp:Content ID="Content4" ContentPlaceHolderID="CssContent" runat="server">
    <link href="/Content/Css/demo_page.css" rel="stylesheet" type="text/css" media="screen, projection" />
    <link href="/Content/Css/demo_table.css" rel="stylesheet" type="text/css" media="screen, projection" />
    <link href="/Content/Css/demo_table_jui.css" rel="stylesheet" type="text/css" media="screen, projection" />
</asp:Content>

<asp:Content ID="Content5" ContentPlaceHolderID="JsContent" runat="server">
    <%--<script src="<%: Url.Content("~/Scripts/resultReports-views.js") %>" type="text/javascript"></script>--%>
    <script src="<%: Url.Content("~/Scripts/jquery.dataTables.js") %>" type="text/javascript"></script>
    <script src="<%: Url.Content("~/Scripts/jquery.dataTables.min.js") %>" type="text/javascript"></script>
    <script src="../../Scripts/jquery.tinymce.js" type="text/javascript"></script>
</asp:Content>
