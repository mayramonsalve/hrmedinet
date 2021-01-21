<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<Medinet.Models.ViewModels.InstructionLevelViewModel>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    <%: ViewRes.Views.Shared.Shared.GenericTitle %> <%: ViewRes.Views.InstructionLevel.Edit.TitleEdit%>
</asp:Content>

<asp:Content ID="Content5" ContentPlaceHolderID="JsContent" runat="server">
    <script src="<%: Url.Content("~/Scripts/jquery.validate.min.js") %>" type="text/javascript"></script>
	<script src="<%: Url.Content("~/Scripts/jquery.validate.unobtrusive.min.js") %>" type="text/javascript"></script>
	<script src="<%: Url.Content("~/Scripts/instructionLevel-views.js") %>" type="text/javascript"></script>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    
        <div id="contenido-sistema" class="span-24 last"> 
        <h2 class="path"><%= ViewRes.Views.InstructionLevel.Edit.PathEdit%></h2>
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
            <%: Html.ActionLink(ViewRes.Views.Shared.Shared.BackToList, "Index") %>
        </div>

</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>

<asp:Content ID="Content4" ContentPlaceHolderID="CssContent" runat="server">
    <link href="../../Content/Css/jquery-ui-1.8.16.custom.css" rel="stylesheet" type="text/css" />
</asp:Content>
