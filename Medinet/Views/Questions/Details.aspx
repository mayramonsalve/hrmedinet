﻿<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<Medinet.Models.ViewModels.QuestionViewModel>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    <%: ViewRes.Views.Shared.Shared.GenericTitle %> <%: ViewRes.Views.Question.Details.TitleDetail %>
</asp:Content>

<asp:Content ID="Content5" ContentPlaceHolderID="JsContent" runat="server">
    <script src="<%: Url.Content("~/Scripts/jquery.validate.min.js") %>" type="text/javascript"></script>
	<script src="<%: Url.Content("~/Scripts/jquery.validate.unobtrusive.min.js") %>" type="text/javascript"></script>
    <script src="<%: Url.Content("~/Scripts/jquery-1.6.2.min.js") %>" type="text/javascript"></script>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">	
    <div id="contenido-sistema" class="span-24 last"> 
       <h2 class="path"><%= ViewRes.Views.Question.Details.PathDetail%></h2>
        <div class="linea-sistema-footer"></div>
	    <% using (Html.BeginForm()){ %>
        <div class="span-23 prepend-1 last"> 
	        <% Html.RenderPartial("FormDetails", Model.question); %>
        </div>
        <% } %>
    </div>
    <div class="span-24 last">
            <%: Html.ActionLink(ViewRes.Views.Shared.Shared.BackToList, "Index", new { @questionnaire_id = Model.question.Category.Questionnaire_Id })%>
    </div>



</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>

<asp:Content ID="Content4" ContentPlaceHolderID="CssContent" runat="server">
</asp:Content>

