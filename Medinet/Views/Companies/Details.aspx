<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<Medinet.Models.ViewModels.CompanyViewModel>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    <%: ViewRes.Views.Shared.Shared.GenericTitle %> <%: ViewRes.Views.Company.Details.TitleDetail %>
</asp:Content>

<asp:Content ID="Content5" ContentPlaceHolderID="JsContent" runat="server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
	    
	<div id="contenido-sistema" class="span-24 last"> 
        <h2 class="path"><%= ViewRes.Views.Company.Details.PathDetail %></h2>
        <div class="linea-sistema-footer"></div>
        <% using (Html.BeginForm("Details","Companies",FormMethod.Post, new {@enctype = "multipart/form-data"})) { %>
        <div class="span-23 prepend-1 last"> 
	        <% Html.RenderPartial("FormDetails", Model); %>
        </div>
        <% } %>
    </div>
        <div class="span-24 last">
            <%: Html.ActionLink(ViewRes.Views.Shared.Shared.BackToList, "Index") %>
        </div>

</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>

<asp:Content ID="Content4" ContentPlaceHolderID="CssContent" runat="server">
    <link href="../../Content/Css/jquery-ui-1.8.16.custom.css" rel="stylesheet" type="text/css" />
</asp:Content>

