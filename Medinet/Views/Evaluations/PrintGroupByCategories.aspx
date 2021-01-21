<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<Medinet.Models.ViewModels.EvaluationViewModel>" %>
<%@ Import Namespace="MedinetClassLibrary.Models"%>

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
            <% int c_questions = 0, categories_count = 0, c_questions_category = 0;%>
            <%  Category[] categoriesArray = Model.GetCategories();
                Question quest; 
                IQueryable<Option> options = Model.GetOptions(Model.test.Questionnaire.Id); 
                Question[] questArrayByCategory = Model.GetQuestionsByCategory(categoriesArray[categories_count].Id);
                
                foreach (Category category in categoriesArray)
                { %> 
                    <span class="font_normal_07em_black"><%: ViewRes.Views.Evaluation.AnswerTest.Category %> <%:category.Name%></span><br />
                    <span class="font_normal_07em_black"><%:category.Description%></span><br /><br />
                    <% 
                    foreach (Question question in Model.GetQuestionsByCategory(category.Id))
                    { %>
                        <p class="input">
				            <%: question.Text%>
                        </p> 
                        <p class="input">
                            <%if (question.QuestionType_Id == 2)
                              {%>
                                    <%: Html.TextBox("q[" + question.Id + "]", null, new { @id = "q[" + question.Id + "]", @style = "width: 70%" })%>
                            <%}
                              else
                              {
                                foreach (var o in options)
                                {
                                %>  
                                    <div>
                                        <%= Html.RadioButton("q[" + question.Id + "]", o.Id, false, new { @id = "q[" + question.Id + "]" })%>
                                        <%: Html.Label(o.Text)%> 
                                    </div>                              
                              <% }
                              }%>   
                         </p>                                         
                    <%
                    }
                }
                
            %> 
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
</asp:Content>
