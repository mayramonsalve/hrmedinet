<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/mobile/MobileMini.Master" Inherits="System.Web.Mvc.ViewPage<Medinet.Models.ViewModels.EvaluationViewModel>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    <%: ViewRes.Views.Shared.Shared.GenericTitle %> <%: ViewRes.Views.Evaluation.EvaluationSucceeded.TitleEvaluationSucceeded %>
</asp:Content>

<asp:Content ID="Content6" ContentPlaceHolderID="PageId" runat="server">
<div data-role="page" id="mainEvaSucceded" class="basic">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="box rounded">
        <h1><%: ViewRes.Views.Evaluation.EvaluationSucceeded.PathEvaluationSucceeded%></h1>
        <h4><%: ViewRes.Views.Evaluation.EvaluationSucceeded.Text%></h4>
        <h1></h1>
        <% Html.RenderPartial("mobile/SocialShare"); %> 
        <% int ev_id;%>
        <% if (Model.previousEvaluation.Test.Tests.Count == 1)
            {
                ev_id = Model.previousEvaluation.Id;
                MedinetClassLibrary.Models.Test next = Model.previousEvaluation.Test.Tests.FirstOrDefault(); %>
            <h4><%: next.PreviousTest_Text %></h4>
            <h4><%: ViewRes.Views.Evaluation.AnswerTest.Fill %>&nbsp<%: next.Name %>: <%: Html.ActionLink(ViewRes.Views.Evaluation.AnswerTest.ButtonNext + ">>", "MobileDemographicsAnswerTest", new { @code = next.Code, @evaluation_id = ev_id }, new { data_ajax = "false" })%></h4>
            <h1></h1>
        <%} %>
<%--        <%: Html.ActionLink(ViewRes.Views.Shared.Shared.GoHome, "Index", "Home", new { data_role = "button", data_theme = "f", data_icon = "home" })%>--%>
        <a href="/" data-icon="home" data-role="button" data-theme="f" rel="external"><%: ViewRes.Views.Shared.Shared.GoHome %></a>
    </div>
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>

<asp:Content ID="Content4" ContentPlaceHolderID="CssContent" runat="server">
</asp:Content>

<asp:Content ID="Content5" ContentPlaceHolderID="JsContent" runat="server">
</asp:Content>

