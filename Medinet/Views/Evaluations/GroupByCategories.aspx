<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<Medinet.Models.ViewModels.EvaluationViewModel>" %>
<%@ Import Namespace="MedinetClassLibrary.Models" %>

<asp:Content ID="Content3" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    <%: ViewRes.Views.Shared.Shared.GenericTitle %> <%: ViewRes.Views.Evaluation.AnswerTest.TitleAnswerTest%>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <% Html.EnableClientValidation(); %>
	<%: Html.ValidationSummary(ViewRes.Views.Shared.Errors.ErrorInApplication, new { @class = "errorAplication" })%>
    <%: Html.Hidden("ButtonBack", ViewRes.Views.Evaluation.AnswerTest.ButtonBack, new { id = "ButtonBack" })%>
    <%: Html.Hidden("ButtonNext", ViewRes.Views.Evaluation.AnswerTest.ButtonNext, new { id = "ButtonNext" })%>
    <%: Html.Hidden("ButtonSubmit", ViewRes.Views.Evaluation.AnswerTest.ButtonSubmit, new { id = "ButtonSubmit" })%>

<div id="contenido-sistema" class="column span-23 last"> 
    <h2 class="path"><%= Model.test.Name%></h2>  
    <div class="linea-sistema-footer"></div>
    <div class="span-24 column last">
        <div id="DivInstructions" class="prepend-1 span-17 column questionnaireInstructions">&nbsp;</div>
        <div class="prepend-1 span-4 column last">
            <div id="progressbar"><span id="amount">0%</span></div>
        </div>
    </div>
    <%: Html.Hidden("test_id", Model.test.Id, new { id = "test_id" })%>   
    <form method="post" action="" id="MultiPageAnswers" class="span-23 append-1">
    <%: Html.Hidden("Select", ViewRes.Scripts.Shared.Select, new { id = "ViewRes" })%>
        <div id="fieldWrapper">
        <% Html.RenderPartial("Demographics"); %>
            <span class="step" id="fX">
            </span>
        </div>
        <div class="clear"></div>
		<div id="demoNavigation" class="column span-24 last"> 
            <div class="clear">&nbsp;</div>    			
			<input class="navigation_button" id="back" value="<%: ViewRes.Views.Evaluation.AnswerTest.ButtonBack %>" type="reset" />
			<input class="navigation_button" id="next" value="<%: ViewRes.Views.Evaluation.AnswerTest.ButtonNext %>" type="submit" />
		</div>
    </form>
    <div id="AllQuestions" style="visibility:hidden">
    </div>
    </div> 
</asp:Content>

<asp:Content ID="Content4" ContentPlaceHolderID="CssContent" runat="server">
    <link href="../../Content/Css/jquery.multipage.css" rel="stylesheet" type="text/css" />
</asp:Content>

<asp:Content ID="Content5" ContentPlaceHolderID="JsContent" runat="server">
    <script src="<%: Url.Content("~/Scripts/jquery.validate.min.js") %>" type="text/javascript"></script>
	<script src="<%: Url.Content("~/Scripts/jquery.validate.unobtrusive.min.js") %>" type="text/javascript"></script>
<%--    <script src="../../Scripts/jquery.validate.js" type="text/javascript"></script>	--%>
    <script type="text/javascript" src="../../Scripts/jquery.form.js"></script>
    <script type="text/javascript" src="../../Scripts/bbq.js"></script>
    <script type="text/javascript" src="../../Scripts/jquery.form.wizard.js"></script>
    <script src="<%: Url.Content("~/Scripts/answerTest.js") %>" type="text/javascript"></script>
</asp:Content>
