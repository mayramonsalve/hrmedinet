﻿<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<Medinet.Models.ViewModels.FunctionalOrganizationViewModel>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
     <%: ViewRes.Views.Shared.Shared.GenericTitle %> <%: ViewRes.Views.FunctionalOrganization.Edit.TitleEdit %>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="JsContent" runat="server">
    <script src="<%: Url.Content("~/Scripts/jquery.validate.min.js") %>" type="text/javascript"></script>
	<script src="<%: Url.Content("~/Scripts/jquery.validate.unobtrusive.min.js") %>" type="text/javascript"></script>
	<script src="<%: Url.Content("~/Scripts/functionalOrganization-views.js") %>" type="text/javascript"></script>
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" runat="server">
	<%: Html.Hidden("Select", ViewRes.Scripts.Shared.Select, new { id = "ViewRes" })%>
     <div id="contenido-sistema" class="span-24 last"> 
        <h2 class="path"><%= ViewRes.Views.FunctionalOrganization.Edit.PathEdit %></h2>
        <div class="linea-sistema-footer"></div>
		<% using (Html.BeginForm()) { %>
            <div class="span-23 prepend-1 last"> 
            <% Html.EnableClientValidation(); %>
			<%: Html.ValidationSummary(ViewRes.Views.Shared.Errors.ErrorInApplication, new { @class = "errorAplication" })%>
            <% Html.RenderPartial("Form", Model); %>
                    <div class="button-padding-top">
				        <input type="submit" class="button" value="<%: ViewRes.Views.Shared.Shared.EditButton %>" />
			        </div>
            </div>
        <% } %>
    </div>
    <div class="span-24 last">
            <%: Html.ActionLink(ViewRes.Views.Shared.Shared.BackToList, "Index", new { @type_id = Model.functionalOrganization.Type_Id })%>
    </div>


</asp:Content>

<asp:Content ID="Content4" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>

<asp:Content ID="Content5" ContentPlaceHolderID="CssContent" runat="server">
</asp:Content>

