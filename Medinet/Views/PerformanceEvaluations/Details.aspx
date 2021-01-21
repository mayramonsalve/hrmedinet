<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<Medinet.Models.ViewModels.PerformanceEvaluationViewModel>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    <%: ViewRes.Views.Shared.Shared.GenericTitle %> <%: ViewRes.Views.PerformanceEvaluation.Details.TitleDetails %>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div id="contenido-sistema" class="span-24 last"> 
         <h2 class="path"><%= ViewRes.Views.PerformanceEvaluation.Details.PathDetails %></h2>
         <div class="linea-sistema-footer"></div>
         <% using (Html.BeginForm()) { %>
            <div class="span-23 prepend-1 last"> 
	            <% Html.RenderPartial("FormDetails", Model.performanceEvaluation); %>
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
</asp:Content>

