<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<Medinet.Models.ViewModels.EvaluationViewModel>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    <%: ViewRes.Views.Shared.Shared.GenericTitle %> <%: ViewRes.Views.Evaluation.EvaluationSucceeded.TitleEvaluationSucceeded %>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <div id="contenido-small" class="span-24 last">
        <div class="span-5 column image-padding-top">
            <span id="imageEvaluationSent" class="column"></span>
        </div>
        <div class="span-18 append-1 column last">
            <h2 class="path"><%: ViewRes.Views.Evaluation.EvaluationSucceeded.PathEvaluationSucceeded%></h2>
                <div class="linea-sistema-footer"></div>
            <h4><%: ViewRes.Views.Evaluation.EvaluationSucceeded.Text%></h4>
            <% int ev_id;
                bool showGoHome = true; %>
            <% if (Model.previousEvaluation.Test.Tests.Count == 1)
               {
                   showGoHome = false;
                   ev_id = Model.previousEvaluation.Id;
                   MedinetClassLibrary.Models.Test next = Model.previousEvaluation.Test.Tests.FirstOrDefault(); %>
                <br />
                <div class="linea-sistema-footer"></div>
                <h5><%: next.PreviousTest_Text %></h5>
                <h4>Para llenar la <%: next.Name %>: <%: Html.ActionLink("SIGUIENTE>>", "AnswerTest", new { @code = next.Code, @evaluation_id = ev_id }, new { @id = "link" })%> </h4>
            <%} %>
        </div>
        <div class="clear"></div>
        <%if (showGoHome)
          { %>
          <div class="span-23 append-1 last">
            <div class="linea-sistema-footer"></div>
          </div>
          <div class="span-24 last">
            <div class="span-12 column">
                <%: ViewRes.Views.Evaluation.EvaluationSucceeded.ToFinish %>&nbsp;<%: Html.ActionLink(ViewRes.Views.Shared.Shared.GoHome, "Index", "Home")%>
            </div>
            <div class="span-12 column last">
                <% if (Model.previousEvaluation.Test.Id == 67)
                   { %>
                    <%: ViewRes.Views.Evaluation.EvaluationSucceeded.ForNewEvaluation %>&nbsp;<%: Html.ActionLink("Click aquí", "AnswerTest", new { @code = "70e0d503-5d99-43b5-83db-1c938ec2069b" })%>
                <%}
                   else
                   {%>
                       &nbsp;
                   <%} %>
            </div>
        </div>
        <div class="clear"></div>
        <%} %>
    </div>
<%--        <%: Html.Hidden("NextTest", ViewRes.Views.Shared.Shared.GoHome, new { id = "NextTest" })%>
        <%: Html.Hidden("Home", ViewRes.Views.Shared.Shared.GoHome, new { id = "Home" })%>
        <%: Html.Hidden("Feedback", ViewRes.Views.Feedback.Send.Title, new { id = "Feedback" })%>
        <div id="Dialog" class="span-10" title="<%: ViewRes.Views.Evaluation.EvaluationSucceeded.Suggestions %>">
		    <div class="span-22 prepend-1 append-1">
            <br />
			    <%: ViewRes.Views.Evaluation.EvaluationSucceeded.LeaveAComment %>
		    </div>
        </div>

	   <% Session["Type"] = 2; %>--%>
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>

<asp:Content ID="Content4" ContentPlaceHolderID="CssContent" runat="server">
    <style type="text/css">
       #contenido-small
       {
            margin-top:150px;    
       }
    </style>
</asp:Content>

<asp:Content ID="Content5" ContentPlaceHolderID="JsContent" runat="server">
    <%--<script src="<%: Url.Content("~/Scripts/redirect.js") %>" type="text/javascript"></script>--%>
    <script src="<%: Url.Content("~/Scripts/jquery.validate.min.js") %>" type="text/javascript"></script>
    <script src="<%: Url.Content("~/Scripts/jquery.validate.unobtrusive.min.js") %>" type="text/javascript"></script>
    
</asp:Content>

