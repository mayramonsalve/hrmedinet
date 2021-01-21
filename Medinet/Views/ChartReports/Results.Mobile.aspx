<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Mobile/MobileMini.Master" Inherits="System.Web.Mvc.ViewPage<Medinet.Models.ViewModels.ResultViewModel>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    <%: ViewRes.Views.Shared.Shared.GenericTitle %><%: ViewRes.Views.ChartReport.Graphics.StatisticalReports %>
</asp:Content>

<asp:Content ID="Content7" ContentPlaceHolderID="PageId" runat="server">
<div data-role="page" id="mainChartStadistics" class="basic">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <h1 class="testname"><%: ViewRes.Views.ChartReport.Graphics.StatisticalReports %></h1>
    <%: Html.Hidden("test_id",Model.test.Id) %> 
    <% bool sevT = Model.test.OneQuestionnaire ? Model.test.Questionnaire.Tests.Where(t => t.Id != Model.test.Id).Count() > 0 : false; %>
    <div id="myTabs" class="box rounded tabs">
        <div id="tabResult1">
            <h1>
                <a href="#tabs" data-rel="dialog" data-role="button" data-iconpos="notext" data-theme="f" data-inline="true" data-icon="bars"></a>
                <%: ViewRes.Views.ChartReport.Graphics.PositiveByCategory %>
            </h1>
            <%Html.RenderPartial("Report1.Mobile",Model); %>
        </div>
        <div id="tabResult2">
            <h1>
                <a href="#tabs" data-rel="dialog" data-role="button" data-iconpos="notext" data-theme="f" data-inline="true" data-icon="bars"></a>
                <%: ViewRes.Views.ChartReport.Graphics.ByCategory%>
            </h1>
        </div>
        <div id="tabResult3">
            <h1>
                <a href="#tabs" data-rel="dialog" data-role="button" data-iconpos="notext" data-theme="f" data-inline="true" data-icon="bars"></a>
                <%: ViewRes.Views.ChartReport.Graphics.PositiveByQuestion%>
            </h1>
        </div>
        <div id="tabResult4">
            <h1>
                <a href="#tabs" data-rel="dialog" data-role="button" data-iconpos="notext" data-theme="f" data-inline="true" data-icon="bars"></a>
                <%: ViewRes.Views.ChartReport.Graphics.ByQuestion%>
            </h1>
        </div>
        <% if (Model.test.OneQuestionnaire && sevT)
           { %>
        <div id="tabResult5">
            <h1>
                <a href="#tabs" data-rel="dialog" data-role="button" data-iconpos="notext" data-theme="f" data-inline="true" data-icon="bars"></a>
                <%: ViewRes.Views.ChartReport.Graphics.ComparativeByCategory%>
            </h1>
        </div>
        <div id="tabResult6">
            <h1>
                <a href="#tabs" data-rel="dialog" data-role="button" data-iconpos="notext" data-theme="f" data-inline="true" data-icon="bars"></a>
                <%: ViewRes.Views.ChartReport.Graphics.ComparativeByQuestion%>
            </h1>
        </div>
        <%} %>
        
    <%Html.RenderPartial("Button.Mobile"); %>
    </div>


</asp:Content>


<asp:Content ID="Content3" ContentPlaceHolderID="PageContent" runat="server">
    <div data-role="page" id="tabs" class="basic"  data-close-btn="right">
        <div data-role="header" data-theme="f">
		<h1><%= ViewRes.Views.ChartReport.Graphics.ChooseSection%></h1>
	    </div>
        <div data-role="content" class="content basic">
            <div>
                <% bool sevT = Model.test.OneQuestionnaire ? Model.test.Questionnaire.Tests.Where(t => t.Id != Model.test.Id).Count() > 0 : false; %>
                <ul data-role="listview">
                    <li><a class="allResult" href="#tabResult1"><%: ViewRes.Views.ChartReport.Graphics.PositiveByCategory %></a></li>
                    <li><a class ="allResult" href="#tabResult2"><%: ViewRes.Views.ChartReport.Graphics.ByCategory %></a></li>
                    <li><a class ="allResult" href="#tabResult3"><%: ViewRes.Views.ChartReport.Graphics.PositiveByQuestion %></a></li>
                    <li><a class ="allResult" href="#tabResult4"><%: ViewRes.Views.ChartReport.Graphics.ByQuestion %></a></li>
                    <% if (Model.test.OneQuestionnaire && sevT)
                       { %>
                    <li><a class ="allResult" href="#tabResult5"><%: ViewRes.Views.ChartReport.Graphics.ComparativeByCategory%></a></li>
                    <li><a class ="allResult" href="#tabResult6"><%: ViewRes.Views.ChartReport.Graphics.ComparativeByQuestion%></a></li>
                    <%} %>
                </ul>
            </div>
        </div>
       <%-- <% Html.RenderPartial("mobile/MobileFooter"); %> --%>
    </div>

    <% if (!Model.test.OneQuestionnaire)
       { %>

    <div data-role="page" id="questionnairesTabs" class="basic"  data-close-btn="right">
        <div data-role="header" data-theme="f">
		<h1><%= ViewRes.Views.ChartReport.Graphics.ChooseSection%></h1>
	    </div>
        <div data-role="content" class="content basic">
            <div>
                <ul data-role="listview">
                    <li><a class="allResult" href="#tabResult1-0">General</a></li>
                    <% foreach (MedinetClassLibrary.Models.Questionnaire questionnaire in Model.questionnairesInTest)
                       { %>
                    <li><a class="allResult" href="#tabResult1-<%: questionnaire.Id %>"><%: questionnaire.Name%></a></li>
                    <%} %>
                </ul>
            </div>
        </div>
       <%-- <% Html.RenderPartial("mobile/MobileFooter"); %> --%>
    </div>

        <div data-role="page" id="questionnairesTabs2" class="basic"  data-close-btn="right">
        <div data-role="header" data-theme="f">
		<h1><%= ViewRes.Views.ChartReport.Graphics.ChooseSection%></h1>
	    </div>
        <div data-role="content" class="content basic">
            <div>
                <ul data-role="listview">
                    <li><a class="allResult" href="#tabResult2-0">General</a></li>
                    <% foreach (MedinetClassLibrary.Models.Questionnaire questionnaire in Model.questionnairesInTest)
                       { %>
                    <li><a class="allResult" href="#tabResult2-<%: questionnaire.Id %>"><%: questionnaire.Name%></a></li>
                    <%} %>
                </ul>
            </div>
        </div>
       <%-- <% Html.RenderPartial("mobile/MobileFooter"); %> --%>
    </div>

        <div data-role="page" id="questionnairesTabs3" class="basic"  data-close-btn="right">
        <div data-role="header" data-theme="f">
		<h1><%= ViewRes.Views.ChartReport.Graphics.ChooseSection%></h1>
	    </div>
        <div data-role="content" class="content basic">
            <div>
                <ul data-role="listview">
                    <% foreach (MedinetClassLibrary.Models.Questionnaire questionnaire in Model.questionnairesInTest)
                       { %>
                    <li><a class="allResult" href="#tabResult3-<%: questionnaire.Id %>"><%: questionnaire.Name%></a></li>
                    <%} %>
                </ul>
            </div>
        </div>
        <%--<% Html.RenderPartial("mobile/MobileFooter"); %> --%>
    </div>

        <div data-role="page" id="questionnairesTabs4" class="basic"  data-close-btn="right">
        <div data-role="header" data-theme="f">
		<h1><%= ViewRes.Views.ChartReport.Graphics.ChooseSection%></h1>
	    </div>
        <div data-role="content" class="content basic">
            <div>
                <ul data-role="listview">
                    <% foreach (MedinetClassLibrary.Models.Questionnaire questionnaire in Model.questionnairesInTest)
                       { %>
                    <li><a class="allResult" href="#tabResult4-<%: questionnaire.Id %>"><%: questionnaire.Name%></a></li>
                    <%} %>
                </ul>
            </div>
        </div>
        <%--<% Html.RenderPartial("mobile/MobileFooter"); %> --%>
    </div>

        <div data-role="page" id="questionnairesTabs5" class="basic"  data-close-btn="right">
        <div data-role="header" data-theme="f">
		<h1><%= ViewRes.Views.ChartReport.Graphics.ChooseSection%></h1>
	    </div>
        <div data-role="content" class="content basic">
            <div>
                <ul data-role="listview">
                    <li><a class="allResult" href="#tabResult5-0">General</a></li>
                    <% foreach (MedinetClassLibrary.Models.Questionnaire questionnaire in Model.questionnairesInTest)
                       { %>
                    <li><a class="allResult" href="#tabResult5-<%: questionnaire.Id %>"><%: questionnaire.Name%></a></li>
                    <%} %>
                </ul>
            </div>
        </div>
        <%--<% Html.RenderPartial("mobile/MobileFooter"); %> --%>
    </div>

        <div data-role="page" id="questionnairesTabs6" class="basic"  data-close-btn="right">
        <div data-role="header" data-theme="f">
		<h1><%= ViewRes.Views.ChartReport.Graphics.ChooseSection%></h1>
	    </div>
        <div data-role="content" class="content basic">
            <div>
                <ul data-role="listview">
                    <li><a class="allResult" href="#tabResult6-0">General</a></li>
                    <% foreach (MedinetClassLibrary.Models.Questionnaire questionnaire in Model.questionnairesInTest)
                       { %>
                    <li><a class="allResult" href="#tabResult6-<%: questionnaire.Id %>"><%: questionnaire.Name%></a></li>
                    <%} %>
                </ul>
            </div>
        </div>
       <%-- <% Html.RenderPartial("mobile/MobileFooter"); %> --%>
    </div>

    <% } %>
</asp:Content>


<asp:Content ID="Content4" ContentPlaceHolderID="CssContent" runat="server">
</asp:Content>

<asp:Content ID="Content5" ContentPlaceHolderID="JsContent" runat="server"> 
    <script src="<%: Url.Content("~/Scripts/mobile/jquery.tablesorter.js") %>" type="text/javascript"></script>
    <script src="<%: Url.Content("~/Scripts/mobile/ChartsMobile.js?mm=0709") %>" type="text/javascript"></script>
</asp:Content>
