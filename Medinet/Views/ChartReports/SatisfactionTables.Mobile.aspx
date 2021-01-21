<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Mobile/MobileMini.Master" Inherits="System.Web.Mvc.ViewPage<Medinet.Models.ViewModels.ChartReportViewModel>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    <%: ViewRes.Views.Shared.Shared.GenericTitle %> <%:ViewRes.Views.ChartReport.Graphics.SatisfactionTitle %>
</asp:Content>

<asp:Content ID="Content7" ContentPlaceHolderID="PageId" runat="server">
<div data-role="page" id="mainChart" class="basic">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
        
        <%: Html.Hidden("compare_id","")%>
        <%: Html.Hidden("chartModel", "Table")%>
        <%: Html.Hidden("test_id",Model.test.Id) %>
        <%: Html.Hidden("chartType", "Satisfaction")%> 
        <%ViewData["print"] = "False"; %>
        <% string CAids = ""; %>
            <h1 class="testname"><%: ViewRes.Views.ChartReport.ReportsList.Satisfaction %></h1>
            <div id="myTabs" class="box rounded tabs">
                <div id="tabCategoryGeneral">
                    <h1><a href="#tabs" data-rel="dialog" data-role="button" data-iconpos="notext" data-theme="f" data-inline="true" data-icon="bars"></a><%: ViewRes.Views.ChartReport.Graphics.General %></h1>
                    <%ViewData["option"] = "Category"; %>
                    <div id="tabCategoryGeneral1">
                    <%Html.RenderPartial("SatisfactionTable", Model); %>
                    </div>
                </div>
                <% foreach (KeyValuePair<int, string> kvp in Model.tabs)
                   {
                       CAids = CAids + kvp.Key.ToString() +'-'; 
                %>
                <div id="tabCategory<%: kvp.Key %>">
                    <h1><a href="#tabs" data-rel="dialog" data-role="button" data-iconpos="notext" data-theme="f" data-inline="true" data-icon="bars"></a><%: kvp.Value %></h1>
                    <div id="tabCategory<%: kvp.Key %>1"></div>
                </div>
                <%} %>
                <div id="tabLocationGeneral">
                    <h1><a href="#tabs" data-rel="dialog" data-role="button" data-iconpos="notext" data-theme="f" data-inline="true" data-icon="bars"></a><%: ViewRes.Views.ChartReport.Graphics.LocationTab %></h1>
                    <div id="tabLocationGeneral1"></div>
                </div>
                <div id="tabFunctionalOrganizationTypeGeneral">
                    <h1><a href="#tabs" data-rel="dialog" data-role="button" data-iconpos="notext" data-theme="f" data-inline="true" data-icon="bars"></a><%: ViewRes.Views.ChartReport.Graphics.FOTypeTab %></h1>
                    <div id="tabFunctionalOrganizationTypeGeneral1"></div>
                </div>
                <%: Html.Hidden("FOids", CAids)%>
                <%Html.RenderPartial("Button.Mobile"); %>
        </div>
</asp:Content>


<asp:Content ID="contet10" ContentPlaceHolderID="PageContent" runat="server">
    <div data-role="page" id="tabs" class="basic"  data-close-btn="right">
        <div data-role="header" data-theme="f">
		    <h1><%= ViewRes.Views.ChartReport.Graphics.ChooseSection%></h1>
	    </div>
        <div data-role="content" class="content basic">
            <div>
                <ul data-role="listview">
                    <li><a class ="all" href="#tabCategoryGeneral"> <%: ViewRes.Views.ChartReport.Graphics.General %> </a></li>
                    <% foreach (KeyValuePair<int, string> kvp in Model.tabs)
                       { %>
                        <li><a class ="all" href="#tabCategory<%: kvp.Key %>"><%: kvp.Value %></a></li>
                    <%} %>
                    <li><a class ="all" href="#tabLocationGeneral"> <%: ViewRes.Views.ChartReport.Graphics.LocationTab %> </a></li>
                    <li><a class ="all" href="#tabFunctionalOrganizationTypeGeneral"> <%: ViewRes.Views.ChartReport.Graphics.FOTypeTab %> </a></li>              
                </ul>
            </div>
        </div>
        <%--<% Html.RenderPartial("mobile/MobileFooter"); %> --%>
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
