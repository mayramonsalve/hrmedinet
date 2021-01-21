<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Mobile/MobileMini.Master" Inherits="System.Web.Mvc.ViewPage<Medinet.Models.ViewModels.RankingViewModel>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    <%: ViewRes.Views.Shared.Shared.GenericTitle %><%: ViewRes.Views.ChartReport.Graphics.Ranking %>
</asp:Content>

<asp:Content ID="Content7" ContentPlaceHolderID="PageId" runat="server">
    <div data-role="page" id="mainChartRanking" class="basic ">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
        <%: Html.Hidden("Rol",Model.UserLogged.Role.Name)%>
        <%: Html.Hidden("questionnaire",Model.questionnaireId)%>
        <h1 class="testname"><%: ViewRes.Views.ChartReport.Graphics.Ranking %></h1>
            <div class="box rounded tabs">
                <div id="tabExternal">
                    <h1><a href="#tabs" data-rel="dialog" data-role="button" data-iconpos="notext" data-theme="f" data-inline="true" data-icon="bars"></a><%: ViewRes.Views.ChartReport.Ranking.External %></h1>
                    <div>
                        <div id="myExternalTabs" class="myTabs">

                            <fieldset data-role="controlgroup" data-type="horizontal">
						        <legend></legend>
						        <input class ="all2" data-theme="f" type="radio" name="radio-choice-h-2" id="radio-choice-1" value="tabGeneral" checked="checked" onclick=handle_tab_click2("#tabGeneral") >
						        <label for="radio-choice-1"><%: ViewRes.Views.ChartReport.Ranking.General %></label>
                                <%if (Model.UserLogged.Role.Name.ToLower() == "hradministrator")
                                { %>
						        <input class ="all2" data-theme="f" type="radio" name="radio-choice-h-2" id="radio-choice-2" value="tabCustomer" onclick=handle_tab_click2("#tabCustomer")>
						        <label for="radio-choice-2"><%: ViewRes.Views.ChartReport.Ranking.Customers %></label>
                                <%} %>
						        <input class ="all2" data-theme="f" type="radio" name="radio-choice-h-2" id="radio-choice-3" value="tabGeneralCountry" onclick=handle_tab_click2("#tabGeneralCountry")>
						        <label for="radio-choice-3"><%: ViewRes.Views.ChartReport.Ranking.ByCountry %></label>
					        </fieldset>
                            <div id="tabGeneral">
                                    <%Html.RenderPartial("RankingGeneral", Model); %>
                            </div>
                            <%if (Model.UserLogged.Role.Name == "HRAdministrator")
                              { %>
                            <div id="tabCustomer">
                            </div>
                            <%} %>
                            <div id="tabGeneralCountry">
                            </div>
                        </div>
                    </div>
                </div>
                <div id="tabInternal" style="display:none;">
                    <h1><a href="#tabs" data-rel="dialog" data-role="button" data-iconpos="notext" data-theme="f" data-inline="true" data-icon="bars"></a><%: ViewRes.Views.ChartReport.Ranking.Internal%></h1>
                </div>
                <%Html.RenderPartial("Button.Mobile"); %>
            </div>
            <%: Html.Hidden("SelectCompany", ViewRes.Views.ChartReport.Ranking.SelectCompanyPrint, new { id = "SelectCompany" })%>
            <%: Html.Hidden("SelectSector", ViewRes.Views.ChartReport.Ranking.SelectSectorPrint, new { id = "SelectSector" })%> 
            <%: Html.Hidden("SelectSectorAndCountry", ViewRes.Views.ChartReport.Ranking.SelectSectorAndCountryPrint, new { id = "SelectSectorAndCountry" })%> 
            <div id="Dialog" title="">
                <p id="DialogText"></p>
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


<asp:Content ID="Content6" ContentPlaceHolderID="PageContent" runat="server">

    <div data-role="page" id="tabs" class="basic "  data-close-btn="right">
        <div data-role="header" data-theme="f">
		    <h1><%= ViewRes.Views.ChartReport.Graphics.ChooseSection%></h1>
	    </div>
        <div data-role="content" class="content basic">
            <div>
                <ul data-role="listview">
                    <li><a class ="allRanking" href="#tabExternal"><%: ViewRes.Views.ChartReport.Ranking.External %></a></li>
                    <li><a class ="allRanking" href="#tabInternal"><%: ViewRes.Views.ChartReport.Ranking.Internal %></a></li>
                </ul>
            </div>
        </div>
        <%--<% Html.RenderPartial("mobile/MobileFooter"); %> --%>
    </div>

</asp:Content>