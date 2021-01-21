<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/mobile/MobileMini.Master" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    <%: ViewRes.Views.Shared.Shared.GenericTitle %> <%: ViewRes.Views.Evaluation.ErrorEvaluation.TitleErrorEvaluation %>
</asp:Content>

<asp:Content ID="Content6" ContentPlaceHolderID="PageId" runat="server">
<div data-role="page" id="mainErrorEva" class="basic">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="box rounded">
        <div>
            <h1><%: ViewRes.Views.Evaluation.ErrorEvaluation.PathErrorEvaluation%></h1>
        </div>
        <div>
            <h4><%: ViewRes.Views.Evaluation.ErrorEvaluation.TextError%></h4>
        </div>
        <div>
            <h4><%: ViewData["emailError"] %></h4>
        </div>
        <div><h1></h1></div>
        <div>
            <a href="/" data-icon="home" data-role="button" data-theme="f" data-ajax="false"><%: ViewRes.Views.Shared.Shared.GoHome %></a>
        </div>
    </div>
</asp:Content>