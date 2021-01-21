<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    <%: ViewRes.Views.Shared.Shared.GenericTitle %> <%: ViewRes.Views.Test.Code.CheckCode %> 
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
<div class="span-24 column last">
     <div id="contenido-logOn" class="span-14 prepend-5 append-5">
        <div><%: Html.ValidationSummary(ViewRes.Views.Test.Code.Error) %></div> 
            <div class="span-5 column image-padding-top">
                <span id="test-image-big" class="column"></span>
            </div>
            <% using (Html.BeginForm()){ %>
                <div class="span-15 prepend-2 append-2 column last">
                    <div class="span-24 column last"><h2><%: Html.Label(ViewRes.Views.Home.Index.Code)%></h2></div>
                    <div class="span-24 column last"><%: Html.TextBox("Code", "", new { @class="input-background short"})%></div>
                    <div class="span-24 column last button-padding-top">
	                    <input type="submit" class="button" value="<%: ViewRes.Views.Shared.Shared.SendButton %>" />
    	            </div>
                </div>
            <% } %> 
            <div class="clear"></div>
        <div class="span-24 column last">
            <%: Html.ActionLink(ViewRes.Views.Evaluation.EvaluationSucceeded.GoHomeLink, "Index","Home") %>
        </div>
            <div class="clear"></div>
    </div>
</div>
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>

<asp:Content ID="Content4" ContentPlaceHolderID="CssContent" runat="server">
</asp:Content>

<asp:Content ID="Content5" ContentPlaceHolderID="JsContent" runat="server">
</asp:Content>
