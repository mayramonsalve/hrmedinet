<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<Medinet.Models.ViewModels.TestViewModel>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    <%: ViewRes.Views.Shared.Shared.GenericTitle %> <%: ViewRes.Views.Test.Code.TestCode %> 
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

 <div id="contenido-small" class="span-24 last" style="margin-top: 100px;">
        <div class="span-3 column image-padding-top">
            <span id="test-image-big" class="column"></span>
        </div>
        <div class="span-19 prepend-1 append-1 column last">
            <h2><%: ViewRes.Controllers.Tests.CreatedwithsuccessText %></h2>
            <h2><%: ViewRes.Views.Test.Code.EvaluationCode %></h2>
                <div class="linea-sistema-footer"></div>
            <h4><%: Model.newCode%></h4>
        </div>
        <div class="clear"></div>
    <div class="span-24 last">
        <%: Html.ActionLink(ViewRes.Views.Evaluation.EvaluationSucceeded.GoHomeLink, "Index","Home") %>
    </div>
</div>
   
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>

<asp:Content ID="Content4" ContentPlaceHolderID="CssContent" runat="server">
</asp:Content>

<asp:Content ID="Content5" ContentPlaceHolderID="JsContent" runat="server">
</asp:Content>
