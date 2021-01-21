<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<Medinet.Models.ViewModels.DemoViewModel>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    <%: ViewRes.Views.Shared.Shared.GenericTitle %> Editar Demo
</asp:Content>

<asp:Content ID="Content5" ContentPlaceHolderID="JsContent" runat="server">
    <script src="<%: Url.Content("~/Scripts/jquery.validate.min.js") %>" type="text/javascript"></script>
	<script src="<%: Url.Content("~/Scripts/jquery.validate.unobtrusive.min.js") %>" type="text/javascript"></script>
	<script src="<%: Url.Content("~/Scripts/demo-views.js") %>" type="text/javascript"></script>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    
        <div id="contenido-sistema" class="span-24 last"> 
        <h2 class="path">Demos > Editar</h2>
        <div class="linea-sistema-footer"></div>
		    <% using (Html.BeginForm()) { %>
            <div class="span-23 prepend-1 last"> 
                <% Html.EnableClientValidation(); %>
			    <%: Html.ValidationSummary(ViewRes.Views.Shared.Errors.ErrorInApplication, new { @class = "errorAplication" })%>
			        <div class="span-24 last"> 
                        <div class="span-24 last"><h4>Número de semanas</h4></div>
                        <div class="span-24 last"><%: Html.TextBox("Weeks", Model.weeks, new { @class = "input-background tiny" })%></div> 
                        <%--<div><%: Html.ValidationMessageFor(model => model.age.Name)%></div>--%>

                        <div class="span-24 last"><h4>Número de empleados</h4></div>
                        <div class="span-24 last"><%: Html.TextBox("Employees", Model.employees, new { @class = "input-background tiny" })%></div> 
                        <%--<div><%: Html.ValidationMessageFor(model => model.age.ShortName)%></div>--%>
                    </div>
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
