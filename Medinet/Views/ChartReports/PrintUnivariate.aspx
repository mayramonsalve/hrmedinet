<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Print.Master" Inherits="System.Web.Mvc.ViewPage<Medinet.Models.ViewModels.ChartReportViewModel>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
 <%: ViewRes.Views.Shared.Shared.GenericTitle %> <%:ViewRes.Views.ChartReport.Graphics.UnivariateTitle %>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <h2 style="text-align:center"><%:ViewRes.Views.ChartReport.Graphics.UnivariateTitle %></h2>
    <div id="tabGeneral">
        <%Html.RenderPartial("PrintChartsForm", Model); %>
    </div>
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="HeadContent" runat="server">
            <div id="header" class="block">  
                <a href="#" class="span-4 column"><span id="logo"></span></a>
                <div class="span-4 prepend-18 append-2"><% if (Model.test.Company.Image != null && Model.test.Company.Image != "System.Web.HttpPostedFileWrapper")
                                             { %>
							                <img src="../../Content/Images/Companies/<%: Model.test.Company.Image %>"
                                             alt="" width="100" height="100" />
                                        <% } %>
                                        <% else{
                                             %>
                                            <img src="../../Content/Images/Companies/<%: Model.test.Company.CompaniesType.Name %>Image.png"
                                                alt="" width="100" height="100" />
                                        <% } %></div>
            </div>
</asp:Content>

<asp:Content ID="Content4" ContentPlaceHolderID="CssContent" runat="server">
    <link href="/Content/Css/demo_page.css" rel="stylesheet" type="text/css" media="screen, projection" />
    <link href="../../Content/Css/demo_table_jui.css" rel="stylesheet" type="text/css" media="screen, projection" />
    <link href="/Content/Css/demo_table.css" rel="stylesheet" type="text/css" media="screen, projection" />
</asp:Content>

<asp:Content ID="Content5" ContentPlaceHolderID="JsContent" runat="server"> 
</asp:Content>
