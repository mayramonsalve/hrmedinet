<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    <%: ViewRes.Views.Shared.Shared.GenericTitle %>
    <%: ViewRes.Views.Home.Services.Title %>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
<div class="page block">
    <div class="span-14 column">
        <!--descripcion de servicios-->
        <h2 class="path">
            <%: ViewRes.Views.Home.Services.Path %></h2>
        <div class="linea-sistema-footer"></div>
        <div class="span-22 append-2 column last">
            <p>
                <%: ViewRes.Views.Home.Services.Text1 %>
            </p>
            <br />
            <p>
                <%: ViewRes.Views.Home.Services.Text2 %>
            </p>
            <ul class="list-separator">
                <li><%: ViewRes.Views.Home.Services.Text3 %></li>
<%--                <li>
                    <a href="<%:Url.Action("OrganizationalClimate","Home") %>"><%: ViewRes.Views.Home.Services.Text3 %></a>
                    <a href="<%:Url.Action("OrganizationalClimate","Home") %>"><span class="f-right more" title="<%: ViewRes.Views.Shared.Shared.See %>"></span></a>
                </li>
                <li>
                    <a href="<%:Url.Action("AssessmentCentres","Home") %>"><%: ViewRes.Views.Home.Services.Text4 %></a>
                    <a href="<%:Url.Action("AssessmentCentres","Home") %>"><span class="f-right more" title="<%: ViewRes.Views.Shared.Shared.See %>"></span></a>
                </li>
                <li>
                    <a href="<%:Url.Action("PerformanceEvaluations","Home") %>"><%: ViewRes.Views.Home.Services.Text5 %></a>
                    <a href="<%:Url.Action("PerformanceEvaluations","Home") %>"><span class="f-right more" title="<%: ViewRes.Views.Shared.Shared.See %>"></span></a>
                </li>--%>
            </ul>
        </div>
    </div>
    <div class="span-8 column last">
            <!--Division de la derecha--> 
        <div id="login-box" class="column">
            <% Html.RenderPartial("PartialLogOn"); %>
        </div>
        <%--<div id="twitter" class="column">
            <% Html.RenderPartial("TwitterWidget");%>  
        </div>--%>
    </div> 
    <% Html.RenderPartial("Footer"); %>
</div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="CssContent" runat="server">
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="JsContent" runat="server">
    <script src="<%: Url.Content("~/Scripts/jquery.infieldlabel.min.js") %>" type="text/javascript"></script>
    <script src="<%: Url.Content("~/Scripts/labelInfield.js") %>" type="text/javascript"></script>
</asp:Content>
