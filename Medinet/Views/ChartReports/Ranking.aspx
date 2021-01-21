<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<Medinet.Models.ViewModels.RankingViewModel>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    <%: ViewRes.Views.Shared.Shared.GenericTitle %><%: ViewRes.Views.ChartReport.Graphics.Ranking %>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
        <%: Html.Hidden("Rol",Model.UserLogged.Role.Name)%>
        <%: Html.Hidden("questionnaire",Model.questionnaireId)%>
            <div class="myTabs span-24 column last">
                <ul >
                    <li><a href="#tabExternal"><%: ViewRes.Views.ChartReport.Ranking.External %></a></li>
                    <li><a class ="all" href="#tabInternal"><%: ViewRes.Views.ChartReport.Ranking.Internal %></a></li>
                </ul>
                <div id="tabExternal">
                    <div class="prepend-1 span-22 append-1 column last">
                        <div id="myExternalTabs" class="myTabs">
                            <ul >
                                <li><a href="#tabGeneral"><%: ViewRes.Views.ChartReport.Ranking.General %></a></li>
                                <%if (Model.UserLogged.Role.Name.ToLower() == "hradministrator")
                                  { %>
                                <li><a class ="all" href="#tabCustomer"><%: ViewRes.Views.ChartReport.Ranking.Customers %></a></li>
                                <%} %>
                                <li><a class ="all" href="#tabGeneralCountry"><%: ViewRes.Views.ChartReport.Ranking.ByCountry %></a></li>
                            </ul>
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
                            <div class="clear"><br /></div>
<%--                            <div class="append-18 span-6 column last">
                                <div class="clear"><br /></div>
                                <%: Html.ActionLink(ViewRes.Views.ChartReport.Ranking.PrintExternalRanking, "PdfRanking", new
                                {
                                    questionnaire_id = Model.questionnaireId,
                                    sector_id = 0,
                                    country_id = 0,
                                    company_id = 0,
                                    demographic = "External",
                                    fot = 0
                                }, new { id = "PrintLinkExternal", name = "PrintLinkExternal", @class = "Print" })%>
                            </div>--%>
                        </div>
                    </div>
                </div>
                <div id="tabInternal">

                </div>
            </div>
            <%: Html.Hidden("SelectCompany", ViewRes.Views.ChartReport.Ranking.SelectCompanyPrint, new { id = "SelectCompany" })%>
            <%: Html.Hidden("SelectSector", ViewRes.Views.ChartReport.Ranking.SelectSectorPrint, new { id = "SelectSector" })%> 
            <%: Html.Hidden("SelectSectorAndCountry", ViewRes.Views.ChartReport.Ranking.SelectSectorAndCountryPrint, new { id = "SelectSectorAndCountry" })%> 
            <div id="Dialog" title="">
                <p id="DialogText"></p>
            </div>

    <div class="span-22 column last alignRight">
        <%: Html.ActionLink(ViewRes.Views.ChartReport.ReportsList.BackToList, "ReportsList", "ChartReports")%>
    </div>
    <div id="loading"></div>

</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>

<asp:Content ID="Content4" ContentPlaceHolderID="CssContent" runat="server">
    <link href="../../Content/Css/demo_page.css" rel="stylesheet" type="text/css" media="screen, projection" />
    <link href="../../Content/Css/demo_table_jui.css" rel="stylesheet" type="text/css" media="screen, projection" />
    <link href="../../Content/Css/demo_table.css" rel="stylesheet" type="text/css" media="screen, projection" />
</asp:Content>

<asp:Content ID="Content5" ContentPlaceHolderID="JsContent" runat="server"> 
    <script src="<%: Url.Content("~/Scripts/Ranking.js?mm=0709") %>" type="text/javascript"></script>
    <script src="<%: Url.Content("~/Scripts/jquery.dataTables.js") %>" type="text/javascript"></script>
    <script src="<%: Url.Content("~/Scripts/jquery.dataTables.min.js") %>" type="text/javascript"></script>
</asp:Content>
