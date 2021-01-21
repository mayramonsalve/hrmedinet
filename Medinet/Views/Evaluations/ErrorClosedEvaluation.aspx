<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    <%: ViewRes.Views.Shared.Shared.GenericTitle %> <%: ViewRes.Views.Evaluation.ErrorClosedEvaluation.TitleErrorClosedEvaluation %>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    
    <div id="contenido-small" class="span-24 last">
        <div class="span-5 column image-padding-top">
            <span id="imageEvaluationNotSent" class="column"></span>
        </div>
        <div class="span-18 append-1 column last">
            <h2 class="path"><%: ViewRes.Views.Evaluation.ErrorClosedEvaluation.PathErrorClosedEvaluation%></h2>
                <div class="linea-sistema-footer"></div>
            <h4><%: ViewRes.Views.Evaluation.ErrorClosedEvaluation.TextError%></h4>
        </div>
        <div class="clear"></div>
        <div class="span-24 last">
            <%: Html.ActionLink(ViewRes.Views.Shared.Shared.GoHome, "Index", "Home")%>
        </div>
    </div>
	
 
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>

<asp:Content ID="Content4" ContentPlaceHolderID="CssContent" runat="server">
    <script src="<%: Url.Content("~/Scripts/jquery.validate.min.js") %>" type="text/javascript"></script>
    <script src="<%: Url.Content("~/Scripts/jquery.validate.unobtrusive.min.js") %>" type="text/javascript"></script>
</asp:Content>

<asp:Content ID="Content5" ContentPlaceHolderID="JsContent" runat="server">
</asp:Content>
