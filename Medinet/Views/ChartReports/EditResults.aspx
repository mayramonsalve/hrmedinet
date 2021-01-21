<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Reports.Master" Inherits="System.Web.Mvc.ViewPage<Medinet.Models.ViewModels.ChartReportViewModel>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    <%: ViewRes.Views.Shared.Shared.GenericTitle %> <%:ViewRes.Views.ChartReport.Graphics.EditResultsTitle %>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">   
      <% using (Html.BeginForm("SaveComments", "ChartReports")){ %>  
          <%: Html.Hidden("Select", ViewRes.Scripts.Shared.Select, new { id = "ViewRes" })%>
          <%: Html.Hidden("test_id",Model.test.Id)%>
          <%: Html.Hidden("Location", Model.demographicsCount["Location"])%>
          <%: Html.Hidden("AgeRange", Model.demographicsCount["AgeRange"])%>
          <%: Html.Hidden("Country", Model.demographicsCount["Country"])%>
          <%: Html.Hidden("Region", Model.demographicsCount["Region"])%>
          <%: Html.Hidden("InstructionLevel", Model.demographicsCount["InstructionLevel"])%>
          <%: Html.Hidden("PositionLevel", Model.demographicsCount["PositionLevel"])%>
          <%: Html.Hidden("Seniority", Model.demographicsCount["Seniority"])%>
          <%: Html.Hidden("Gender", Model.demographicsCount["Gender"])%>
          <%: Html.Hidden("Performance", Model.demographicsCount["Performance"])%>
          <%: Html.Hidden("chartType", "EditUnivariate")%>
          <%  foreach (var v in Model.FO)
              {%>
                <%: Html.Hidden("FunctionalOrganizationType"+v.Key, Model.demographicsCount["Gender"])%>
            <% }%>
         <form action="" name="Form">
         <div id="contenido-sistema" class="content span-22 last column"> 
            <div id="myTabs" class="span-24 column last">
                <ul >
                    <li><a href="#tabCountry"><%: ViewRes.Views.ChartReport.Graphics.CountryTab %></a></li>
                    <li><a class ="all" href="#tabRegion"><%: ViewRes.Views.ChartReport.Graphics.RegionTab%></a></li>
                    <li><a class ="all" href="#tabAge"><%: ViewRes.Views.ChartReport.Graphics.AgeTab%></a></li>
                    <li><a class ="all" href="#tabInstructionLevel"><%: ViewRes.Views.ChartReport.Graphics.InstructionLevelTab%></a></li>
                    <li><a class ="all" href="#tabLocation"><%: ViewRes.Views.ChartReport.Graphics.LocationTab%></a></li>
                    <li><a class ="all" href="#tabPositionLevel"><%: ViewRes.Views.ChartReport.Graphics.PositionLevelTab%></a></li>
                    <li><a class ="all" href="#tabSeniority"><%: ViewRes.Views.ChartReport.Graphics.SeniorityTab%></a></li>
                    <li><a class ="all" href="#tabGender"><%: ViewRes.Views.ChartReport.Graphics.GenderTab%></a></li>
                    <li><a class ="all" href="#tabPerformance"><%: ViewRes.Views.ChartReport.Graphics.PerformanceTab%></a></li>
                     <%  int i = 0;
                        foreach (var v in Model.FO)
                    {%>
                        <li><a href="#tab-<%: 12+i%>"><%: Model.FO[v.Key] %></a></li>
                        <% i++;
                    }%>
                </ul>
                <div id="tabCountry">
                    <%ViewData["option"] = "Country"; %>
                    <%ViewData["chartType"] = "EditUnivariate"; %>
                    <% if (Model.demographicsCount["Country"] < 10) 
                       {
                          // Html.RenderPartial("ChartsForm", Model); %>
                        <div class="fadingTooltip" id="fadingTooltip" style="Z-INDEX: 999; VISIBILITY: hidden; POSITION: absolute"></div>
                    <% }else{
                          // Html.RenderPartial("TablesForm", Model);                   
                       } %>        
                </div>
                <div id="tabRegion">
                    <%ViewData["option"] = "Region"; %>
                    <%ViewData["chartType"] = "EditUnivariate"; %>
 <%--                   <% if (Model.demographicsCount["Region"] < 10) 
                       { 
                         //Html.RenderPartial("ChartsForm", Model); 
                       }else{
                         //Html.RenderPartial("TablesForm", Model);                   
                       } %>--%>
                </div>
                <div id="tabAge">
                    <%ViewData["option"] = "AgeRange"; %>
                    <%ViewData["chartType"] = "EditUnivariate"; %>
     <%--               <% if (Model.demographicsCount["AgeRange"] < 10)
                       {
                           Html.RenderPartial("ChartsForm", Model); 
                       }else{
                           Html.RenderPartial("TablesForm", Model);                   
                       } %>--%>
                </div>
                <div id="tabInstructionLevel">
                    <%ViewData["option"] = "InstructionLevel"; %>
                    <%ViewData["chartType"] = "EditUnivariate"; %>
                    <%//Html.RenderPartial("ChartsForm", Model); %>
                </div>
                <div id="tabLocation">
                     <%ViewData["option"] = "Location"; %>
                     <%ViewData["chartType"] = "EditUnivariate"; %>
                     <%--   <% if (Model.demographicsCount["Location"] < 10)
                       {
                           //Html.RenderPartial("ChartsForm", Model); 
                       }else{
                           //Html.RenderPartial("TablesForm", Model);                   
                       } %>--%>
                </div>
                <div id="tabPositionLevel">
                     <%ViewData["option"] = "PositionLevel"; %>
                     <%ViewData["chartType"] = "EditUnivariate"; %>
                     <%//Html.RenderPartial("ChartsForm", Model);%>
                </div>
                <div id="tabSeniority">
                    <%ViewData["option"] = "Seniority"; %>
                    <%ViewData["chartType"] = "EditUnivariate"; %>
<%--                    <% if (Model.demographicsCount["Seniority"] < 10)
                       { 
                         Html.RenderPartial("ChartsForm", Model); 
                       }else{
                         Html.RenderPartial("TablesForm", Model);                   
                       } %> --%>
                </div>
                <div id="tabGender">
                     <%ViewData["option"] = "Gender"; %>
                     <%ViewData["chartType"] = "EditUnivariate"; %>
<%--                    <% Html.RenderPartial("ChartsForm", Model); %> --%>
                </div>
                <div id="tabPerformance">
                    <%ViewData["option"] = "Performance"; %>
                    <%ViewData["chartType"] = "EditUnivariate"; %>
 <%--                   <%  if (Model.condition == true) {
                            if (Model.demographicsCount["Performance"] < 10)
                           {
                             //Html.RenderPartial("ChartsForm", Model); 
                           }else{
                             //Html.RenderPartial("TablesForm", Model);                   
                           } 
                    }%>--%>
                </div>
                <% int j = 0;
                    foreach (var v in Model.FO)
                    {%>
                         <div id="tab-<%: 12+j%>">
                            <%ViewData["option"] = "FunctionalOrganizationType";
                              ViewData["FO_id"] = v.Key; %>
                              <%ViewData["chartType"] = "EditUnivariate"; %>
                            <%  Html.RenderPartial("ChartsForm");%>
                        </div>
                  <% j++;
                     }%>
            </div>
            <div class="span-23 prepend-1 last">
				<input type="submit" class="boton-enviar-sistema" value="Save" />
			</div>
        </div>
        </form>
        <% } %>
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>

<asp:Content ID="Content4" ContentPlaceHolderID="CssContent" runat="server">
    <link href="../../Content/Css/demo_page.css" rel="stylesheet" type="text/css" media="screen, projection" />
    <link href="../../Content/Css/demo_table_jui.css" rel="stylesheet" type="text/css" media="screen, projection" />
    <link href="../../Content/Css/demo_table.css" rel="stylesheet" type="text/css" media="screen, projection" />
</asp:Content>

<asp:Content ID="Content5" ContentPlaceHolderID="JsContent" runat="server">
    <script src="<%: Url.Content("~/Scripts/resultReports-views.js") %>" type="text/javascript"></script> 
    <script src="<%: Url.Content("~/Scripts/jquery.dataTables.js") %>" type="text/javascript"></script>
    <script src="<%: Url.Content("~/Scripts/jquery.dataTables.min.js") %>" type="text/javascript"></script>
    
     <script type="text/javascript" charset="utf-8">
         $(document).ready(function () {
             var test_id = document.getElementById("test_id").value;
             $('#AgeRangeGroupByCategoriesDDL').change(function updateChart() {
                 var id = document.getElementById("AgeRangeGroupByCategoriesDDL").value;
                 if (id == "") {
                     document.getElementById("AgeRangeChart").src = "/ChartReports/UniVariateChartByAgeRange?chartSize=Screen&chartType=Column&graphic_id=2&test_id=" + test_id;
                 } else {
                     document.getElementById("AgeRangeChart").src = "/ChartReports/UniVariateChartByAgeRange?chartSize=Screen&chartType=Column&graphic_id=2&test_id=" + test_id + "&category_id=" + id;
                 }
             });

             $('#CountryGroupByCategoriesDDL').change(function updateChart() {
                 var id = document.getElementById("CountryGroupByCategoriesDDL").value;
                 if (id == "") {
                     document.getElementById("CountryChart").src = "/ChartReports/UniVariateChartMapByCountryPrueba?chartSize=Screen&chartType=Column&graphic_id=18&test_id=" + test_id + "&name=MyMap2";
                 } else {
                     document.getElementById("CountryChart").src = "/ChartReports/UniVariateChartMapByCountryPrueba?chartSize=Screen&chartType=Column&graphic_id=18&test_id=" + test_id + "&category_id=" + id + "&name=MyMap2";
                 }
             });

             $('#GenderGroupByCategoriesDDL').change(function updateChart() {
                 var id = document.getElementById("GenderGroupByCategoriesDDL").value;
                 if (id == "") {
                     document.getElementById("GenderChart").src = "/ChartReports/UniVariateChartByGender?chartSize=Screen&chartType=Column&graphic_id=6&test_id=" + test_id;
                 } else {
                     document.getElementById("GenderChart").src = "/ChartReports/UniVariateChartByGender?chartSize=Screen&chartType=Column&graphic_id=6&test_id=" + test_id + "&category_id=" + id;
                 }
             });

             $('#InstructionLevelGroupByCategoriesDDL').change(function updateChart() {
                 var id = document.getElementById("InstructionLevelGroupByCategoriesDDL").value;
                 if (id == "") {
                     document.getElementById("InstructionLevelChart").src = "/ChartReports/UniVariateChartByInstructionLevel?chartSize=Screen&chartType=Column&graphic_id=8&test_id=" + test_id;
                 } else {
                     document.getElementById("InstructionLevelChart").src = "/ChartReports/UniVariateChartByInstructionLevel?chartSize=Screen&chartType=Column&graphic_id=8&test_id=" + test_id + "&category_id=" + id;
                 }
             });



             $('#PositionLevelGroupByCategoriesDDL').change(function updateChart() {
                 var id = document.getElementById("PositionLevelGroupByCategoriesDDL").value;
                 if (id == "") {
                     document.getElementById("PositionLevelChart").src = "/ChartReports/UniVariateChartByPositionLevels?chartSize=Screen&chartType=Column&graphic_id=12&test_id=" + test_id;
                 } else {
                     document.getElementById("PositionLevelChart").src = "/ChartReports/UniVariateChartByPositionLevels?chartSize=Screen&chartType=Column&graphic_id=12&test_id=" + test_id + "&category_id=" + id;
                 }
             });

             $('#RegionGroupByCategoriesDDL').change(function updateChart() {
                 var id = document.getElementById("RegionGroupByCategoriesDDL").value;
                 if (id == "") {
                     document.getElementById("RegionChart").src = "/ChartReports/UniVariateChartByRegion?chartSize=Screen&chartType=Column&graphic_id=20&test_id=" + test_id;
                 } else {
                     document.getElementById("RegionChart").src = "/ChartReports/UniVariateChartByRegion?chartSize=Screen&chartType=Column&graphic_id=20&test_id=" + test_id + "&category_id=" + id;
                 }
             });

             $('#SeniorityGroupByCategoriesDLL').change(function updateChart() {
                 var id = document.getElementById("SeniorityGroupByCategoriesDDL").value;
                 if (id == "") {
                     document.getElementById("SeniorityChart").src = "/ChartReports/UniVariateChartBySeniority?chartSize=Screen&chartType=Column&graphic_id=16&test_id=" + test_id;
                 } else {
                     document.getElementById("SeniorityChart").src = "/ChartReports/UniVariateChartBySeniority?chartSize=Screen&chartType=Column&graphic_id=16&test_id=" + test_id + "&category_id=" + id;
                 }
             });
             $('#PerformanceGroupByCategoriesDDL').change(function updateChart() {
                 var id = document.getElementById("PerformanceGroupByCategoriesDDL").value;
                 if (id == "") {
                     document.getElementById("PerformanceChart").src = "/ChartReports/UniVariateChartByPerformance?chartSize=Screen&chartType=Column&graphic_id=23&test_id=" + test_id;
                 } else {
                     document.getElementById("PerformanceChart").src = "/ChartReports/UniVariateChartByPerformance?chartSize=Screen&chartType=Column&graphic_id=23&test_id=" + test_id + "&category_id=" + id;
                 }
             });

             $('#LocationGroupByCategoriesDDL').change(function updateChart() {
                 var id = document.getElementById("LocationGroupByCategoriesDDL").value;
                 if (id == "") {
                     document.getElementById("LocationChart").src = "/ChartReports/UniVariateChartByLocation?chartSize=Screen&chartType=Column&graphic_id=10&test_id=" + test_id;
                 } else {
                     document.getElementById("LocationChart").src = "/ChartReports/UniVariateChartByLocation?chartSize=Screen&chartType=Column&graphic_id=10&test_id=" + test_id + "&category_id=" + id;
                 }
             });

         });
    </script>
</asp:Content>
