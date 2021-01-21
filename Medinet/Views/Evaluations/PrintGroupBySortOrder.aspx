<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<Medinet.Models.ViewModels.EvaluationViewModel>" %>
<%@ Import Namespace="MedinetClassLibrary.Models" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    <%: ViewRes.Views.Shared.Shared.GenericTitle %> <%: ViewRes.Views.Evaluation.AnswerTest.TitleAnswerTest%>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <h2 class="path"><%= ViewRes.Views.Evaluation.AnswerTest.PathAnswerTest%></h2> 
    <p></p>
    <div class="Formulario">
        <% Html.RenderPartial("CommonPrint"); %>
        <p></p>
        <div class="Questions">
            <h3><%: ViewRes.Views.Evaluation.AnswerTest.Questions %></h3>
            <% Question[] questions = Model.GetQuestions(Model.test.Disordered);
               //Question quest;
                IQueryable<Option> options = Model.GetOptions(Model.test.Questionnaire.Id); %>
            <% foreach (Question quest in questions)
                {
                    %>
                    <p class="input">
				        <%:quest.Text%>
                    </p> 
                    <p class="input">
                    <%if (quest.QuestionType_Id == 2)
                    {%>
                        <%: Html.TextBox("q[" + quest.Id + "]", null, new { @id = "q[" + quest.Id + "]" })%>
                    <%}
                    else
                    {
                        foreach (var o in options)
                        {
                        %>  
                        <div>
                            <%= Html.RadioButton("q[" + quest.Id + "]", o.Id, false, new { @id = "q[" + quest.Id + "]" })%>
                            <%: Html.Label(o.Text)%> 
                        </div>                              
                        <%
                        }
                    } %>   
                    </p>
                    <%     
                } %>
        </div>
    </div>
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>

<asp:Content ID="Content4" ContentPlaceHolderID="CssContent" runat="server">
    <link href="../../Content/Css/jquery-ui-1.8.16.custom.css" rel="stylesheet" type="text/css" />
    <link href="../../Content/Css/jquery.multipage.css" rel="stylesheet" type="text/css" />
</asp:Content>

<asp:Content ID="Content5" ContentPlaceHolderID="JsContent" runat="server">
    <script src="../../Scripts/jquery.validate.js" type="text/javascript"></script>	

    <script type="text/javascript" src="../../Scripts/jquery.form.js"></script>
    <script type="text/javascript" src="../../Scripts/bbq.js"></script>
    <script type="text/javascript" src="../../Scripts/jquery.form.wizard.js"></script>
    

        <script type="text/javascript">
            $(function () {
                $("#MultiPageAnswers").formwizard({
                    validationEnabled: true,
                    focusFirstInput: true
                });
            });
		</script>
</asp:Content>
