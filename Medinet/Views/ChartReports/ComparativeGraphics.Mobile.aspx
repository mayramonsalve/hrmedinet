<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Mobile/MobileMini.Master" Inherits="System.Web.Mvc.ViewPage<Medinet.Models.ViewModels.ChartReportViewModel>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    <%: ViewRes.Views.Shared.Shared.GenericTitle %> <%:ViewRes.Views.ChartReport.Graphics.ComparativeTitle %>
</asp:Content>

<asp:Content ID="Content7" ContentPlaceHolderID="PageId" runat="server">
<div data-role="page" id="mainChart" class="basic">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
        <%: Html.Hidden("FOids", "")%>
        <%: Html.Hidden("compare_id","")%>
        <%: Html.Hidden("chartModel", "Column")%>
        <%: Html.Hidden("test_id",Model.test.Id) %>
        <%: Html.Hidden("chartType", "Comparative")%> 
        <%ViewData["print"] = "False"; %>
            <h1 class="testname"><%: ViewRes.Views.ChartReport.ReportsList.Comparative %></h1>
            <div id="myTabs" class="box rounded tabs">
                <div id="tabPopulation">
                    <h1><a href="#tabs" data-rel="dialog" data-role="button" data-iconpos="notext" data-theme="f" data-inline="true" data-icon="bars"></a><%: ViewRes.Views.ChartReport.Graphics.Population %></h1>
                    <%ViewData["option"] = "Population"; %>
                    <%Html.RenderPartial("Comparative", Model); %>
                </div>
                <div id="tabClimate" style="display:none;">
                    <h1><a href="#tabs" data-rel="dialog" data-role="button" data-iconpos="notext" data-theme="f" data-inline="true" data-icon="bars"></a><%: ViewRes.Views.ChartReport.Graphics.Climate %></h1>
                </div>
                <%Html.RenderPartial("Button.Mobile"); %>
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


<asp:Content ID="contet10" ContentPlaceHolderID="PageContent" runat="server">
    <div data-role="page" id="tabs" class="basic"  data-close-btn="right">
        <div data-role="header" data-theme="f">
		    <h1><%= ViewRes.Views.ChartReport.Graphics.ChooseSection%></h1>
	    </div>
        <div data-role="content" class="content basic">
            <div>
                <ul data-role="listview">
                    <li><a class ="all" href="#tabPopulation"><%: ViewRes.Views.ChartReport.Graphics.Population %></a></li>
                    <li><a class ="all" href="#tabClimate"><%: ViewRes.Views.ChartReport.Graphics.Climate%></a></li>
                </ul>
            </div>
        </div>
        <%--<% Html.RenderPartial("mobile/MobileFooter"); %> --%>
    </div>
</asp:Content>
