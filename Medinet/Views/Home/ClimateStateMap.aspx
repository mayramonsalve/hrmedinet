<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    <%: ViewRes.Views.Shared.Shared.GenericTitle %><%: ViewRes.Views.Home.Map.Title %>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <%: Html.Hidden("test", Session["test_id"], new { id = "test" })%>
    <%: Html.Hidden("country", Session["country_id"], new { id = "country" })%>
                <%  string rol;
                    if (Roles.IsUserInRole("HRAdministrator"))
                        rol = "HRAdministrator";
                    else if (Roles.IsUserInRole("HRCompany"))
                        rol = "HRCompany";
                    else
                        rol = "CompanyManager";
                    %>
                   <%: Html.Hidden("Rol", rol, new { id = "Rol" })%>
                   <%: Html.Hidden("UrlMap", Session["UrlMap"], new { id = "UrlMap" })%>
                   <%: Html.Hidden("PathHidden", ViewRes.Views.Home.Map.Global, new { id = "PathHidden" })%>
                   <%: Html.Hidden("DialogText", ViewRes.Views.Home.Map.DialogText, new { id = "DialogText" })%>    
                   <%: Html.Hidden("DialogQuestion", ViewRes.Views.Home.Map.DialogQuestion, new { id = "DialogQuestion" })%> 
                   <%: Html.Hidden("CreateTest", ViewRes.Views.Home.Map.CreateTest, new { id = "CreateTest" })%>   
                   <%: Html.Hidden("ShowMap", ViewRes.Views.Home.Map.ShowMap, new { id = "ShowMap" })%>   
                   <%: Html.Hidden("ViewReport", ViewRes.Views.Home.Map.ViewReport, new { id = "ViewReport" })%>
                   <%: Html.Hidden("Close", ViewRes.Views.Home.Map.Close, new { id = "Close" })%>
                   <%: Html.Hidden("ClimateMapOf", ViewRes.Views.Home.Map.ClimateMapOf, new { id = "ClimateMapOf" })%>
    <div id="contenido-sistema" class="span-24 column last map">
    <h2 id="path"><%:ViewRes.Views.Home.Map.ClimateMapOf%> <%: Session["country_name"]%></h2>
    <%--<div id="click" class="span-24"><h4><%: ViewRes.Views.Home.Map.ClickState %></h4></div>--%>
    <div id="click" class="span-24"></div>
        <div class="clear"></div>
        <div id="map" style="height:550px;">
        </div>
    </div>
    <div class="span-24 column last">
        <div class="span-12 column">
            <% int test = (int)Session["test_id"]; %>
            <%: Html.ActionLink(ViewRes.Views.Home.Map.BackGlobalMap, "ClimateMap", new { test_id = test })%>
        </div>
        <div class="span-12 alignRight column last">
            <%: Html.ActionLink(ViewRes.Views.Shared.Shared.GoHome, "Index", "Home")%>
        </div>
    </div>
        <div id="Dialog" class="span-10" title="<%= ViewRes.Views.Home.Map.Options %>">
		    <div id="Text" class="span-20 prepend-2 append-2">
			    
		    </div>
        </div>
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>

<asp:Content ID="Content4" ContentPlaceHolderID="CssContent" runat="server">
    <link href="../../Content/Css/jquery.vector-map.css" media="screen" rel="stylesheet" type="text/css" />
</asp:Content>

<asp:Content ID="Content5" ContentPlaceHolderID="JsContent" runat="server">
    <script src="../../Scripts/jquery.vector-map.js" type="text/javascript"></script>
    <script src="../../Scripts/world-en.js" type="text/javascript"></script>
    <script src="../../Scripts/ve-en.js" type="text/javascript"></script>
    <script src="../../Scripts/us-en.js" type="text/javascript"></script>
    <script src="../../Scripts/ar-en.js" type="text/javascript"></script>
    <script src="../../Scripts/co-en.js" type="text/javascript"></script>
    <script src="../../Scripts/germany-en.js" type="text/javascript"></script>
    <script src="../../Scripts/map.js?mm=0609" type="text/javascript"></script>
</asp:Content>
