<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Mobile/MobileMini.Master" Inherits="System.Web.Mvc.ViewPage<Medinet.Models.ViewModels.ChartReportViewModel>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    <%: ViewRes.Views.Shared.Shared.GenericTitle %> <%:ViewRes.Views.ChartReport.Graphics.BivariateTitle %>
</asp:Content>

<asp:Content ID="Content7" ContentPlaceHolderID="PageId" runat="server">
    <div data-role="page" id="mainChartBivariate" class="basic">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">   
    <div>
        <h1 class="testname"><%: ViewRes.Views.ChartReport.Graphics.BivariateTitle%></h1>
        <%: Html.Hidden("test_id",Model.test.Id) %>
        <%: Html.Hidden("demographicsEquals",ViewRes.Views.ChartReport.Graphics.DemographicsEquals) %>
        <%: Html.Hidden("isTable",false) %> 
        <%: Html.Hidden("Select", ViewRes.Scripts.Shared.Select, new { id = "ViewRes" })%>
        <%: Html.Hidden("company_id", Model.test.Company_Id) %>
        <div class="box rounded tabs">
            <div data-role="fieldcontain">
                <label for="Demographics1"><%:ViewRes.Views.ChartReport.Graphics.Demographics %>: </label>
		        <%: Html.DropDownList("Demographics1", Model.demographicsList1, ViewRes.Scripts.Shared.Select)%>
                <label for="Demographics2"><%:ViewRes.Views.ChartReport.Graphics.Demographics %>:  </label>
	            <%: Html.DropDownList("Demographics2", Model.demographicsList2, ViewRes.Scripts.Shared.Select)%>
	        </div>
            <div>
                <div id="DivChart" style="display:none;" class="full">
                    <%--<img class="full" id="Chart" src="" alt=" " />--%>
                    <div id="Chart" class="column span-24 bivariate-chart"></div>
                </div>
                <fieldset id="FieldsetTable" class="alignCenter" style=" display:none;">
                    <table data-role="table" data-column-btn-text="<%: ViewRes.Views.ChartReport.Graphics.Columns %>" data-mode="columntoggle" data-column-btn-theme="f" id="Table" class="table1 full" >
                        <thead>
                            <tr>
                                <th data-priority="1" class="ui-table-cell-visible"><%: ViewRes.Views.ChartReport.Graphics.Column %> 1</th>
                                <th data-priority="1" class="ui-table-cell-visible"><%: ViewRes.Views.ChartReport.Graphics.Column %> 2</th>
                                <th data-priority="1" class="ui-table-cell-visible"><%: ViewRes.Views.ChartReport.Graphics.Column %> 3</th>
                                <th data-priority="1" class="ui-table-cell-visible"><%: ViewRes.Views.ChartReport.Graphics.Column %> 4</th>
                                <th data-priority="1" class="ui-table-cell-visible"><%: ViewRes.Views.ChartReport.Graphics.Column %> 5</th>
                            </tr>
                        </thead>
                        <tbody></tbody>
                        <tfoot></tfoot>
                    </table>
                </fieldset>
            </div>
            <div id="noMinimum" style="display:none;">
                <%Html.RenderPartial("NoMinimum"); %>
            </div>
            <%Html.RenderPartial("Button.Mobile"); %>
        </div>
    </div>
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>

<asp:Content ID="Content4" ContentPlaceHolderID="CssContent" runat="server">
</asp:Content>

<asp:Content ID="Content5" ContentPlaceHolderID="JsContent" runat="server">
    <script src="<%: Url.Content("~/Scripts/mobile/jquery.tablesorter.js") %>" type="text/javascript"></script>
    <script src="<%: Url.Content("~/Scripts/mobile/ChartsMobile.js?mm=0709") %>" type="text/javascript"></script>
</asp:Content>
