<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Print.Master" Inherits="System.Web.Mvc.ViewPage<Medinet.Models.ViewModels.ResultViewModel[]>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    <%: ViewRes.Views.Shared.Shared.GenericTitle %><%: ViewRes.Views.ChartReport.Graphics.StatisticalReports %>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

<%if (Model.Count() > 1)
  { %>
        <h2 class="prepend-1"><%: ViewRes.Views.ChartReport.Graphics.StatisticalReports %></h2>
        <br />
            <h4><%: ViewRes.Views.ChartReport.Graphics.PositiveByCategory %></h4>
            <%Html.RenderPartial("Report1", Model[0]); %>
            <br /> <br />
            <h4><%: ViewRes.Views.ChartReport.Graphics.ByCategory %></h4>
            <%Html.RenderPartial("Report2", Model[1]); %>
            <br /> <br />
            <h4><%: ViewRes.Views.ChartReport.Graphics.PositiveByQuestion %></h4>
            <%Html.RenderPartial("Report3", Model[2]); %>
            <br /> <br />
            <h4><%: ViewRes.Views.ChartReport.Graphics.ByQuestion %></h4>
            <%Html.RenderPartial("Report4", Model[3]); %>
            <br /> <br />
            <h4><%: ViewRes.Views.ChartReport.Graphics.ComparativeByCategory %></h4>
            <%Html.RenderPartial("Report5", Model[4]); %>
            <br /> <br />
            <h4><%: ViewRes.Views.ChartReport.Graphics.ComparativeByQuestion %></h4>
            <%Html.RenderPartial("Report6", Model[5]); %>
<%}
else
 {
     switch (Model[0].resultType)
     {
         case 1:
             %>
            <h3 class="prepend-2"><%: ViewRes.Views.ChartReport.Graphics.PositiveByCategory %></h3>
            <%Html.RenderPartial("Report1", Model[0]); %>
            <%
            break;
        case 2:
             %>
            <h3 class="prepend-1"><%: ViewRes.Views.ChartReport.Graphics.ByCategory %></h3>
            <%Html.RenderPartial("Report2", Model[1]); %>
            <%
            break;
        case 3:
             %>
            <h3><%: ViewRes.Views.ChartReport.Graphics.PositiveByQuestion %></h3>
            <%Html.RenderPartial("Report3", Model[2]); %>
            <%
            break;
        case 4:
             %>
            <h3><%: ViewRes.Views.ChartReport.Graphics.ByQuestion %></h3>
            <%Html.RenderPartial("Report4", Model[3]); %>
            <%
            break;
        case 5:
             %>
            <h3 class="prepend-1"><%: ViewRes.Views.ChartReport.Graphics.ComparativeByCategory %></h3>
            <%Html.RenderPartial("Report5", Model[4]); %>
            <%
            break;
        case 6:
             %>
            <h3><%: ViewRes.Views.ChartReport.Graphics.ComparativeByQuestion %></h3>
            <%Html.RenderPartial("Report6", Model[5]); %>
            <%
            break;
     }%>
    

<%} %>
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="HeadContent" runat="server">
        <div id="header" class="block">  
            <a href="#" class="span-4 column"><span id="logo"></span></a>
            <div class="span-4 prepend-18 append-2"><% if (Model[0].test.Company.Image != null && Model[0].test.Company.Image != "System.Web.HttpPostedFileWrapper")
                                            { %>
							            <img src="../../Content/Images/Companies/<%: Model[0].test.Company.Image %>"
                                            alt="" width="100" height="100" />
                                    <% } %>
                                    <% else{
                                            %>
                                        <img src="../../Content/Images/Companies/<%: Model[0].test.Company.CompaniesType.Name %>Image.png"
                                            alt="" width="100" height="100" />
                                    <% } %></div>
        </div>
</asp:Content>

<asp:Content ID="Content4" ContentPlaceHolderID="CssContent" runat="server">
    <link href="../../Content/Css/demo_page.css" rel="stylesheet" type="text/css" media="screen, projection" />
    <link href="../../Content/Css/demo_table_jui.css" rel="stylesheet" type="text/css" media="screen, projection" />
    <link href="../../Content/Css/demo_table.css" rel="stylesheet" type="text/css" media="screen, projection" />
</asp:Content>

<asp:Content ID="Content5" ContentPlaceHolderID="JsContent" runat="server"> 
    <script src="<%: Url.Content("~/Scripts/Results.js") %>" type="text/javascript"></script>
    <script src="<%: Url.Content("~/Scripts/jquery.dataTables.js") %>" type="text/javascript"></script>
    <script src="<%: Url.Content("~/Scripts/jquery.dataTables.min.js") %>" type="text/javascript"></script>
</asp:Content>
