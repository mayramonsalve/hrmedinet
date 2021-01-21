<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Medinet.Models.ViewModels.QuestionViewModel>" %>
             <div class="span-24 last">   
                <%: Html.Hidden("Select", ViewRes.Scripts.Shared.Select, new { id = "ViewRes" })%>  
                <div class="span-24 last"><h4><%: ViewRes.Views.Question.Create.DropDownQuestionnaires %></h4></div>
                <div class="span-24 last"><%: Html.DropDownList("Questionnaires", Model.questionnairesList, ViewRes.Scripts.Shared.Select, new { @class = "input-background short" })%></div>
                
                <div class="span-24 last"><h4><%: Html.LabelFor(model => model.question.Category_Id)%></h4></div>
                <div class="span-24 last"><%: Html.DropDownListFor(model => model.question.Category_Id, Model.categoriesList, ViewRes.Scripts.Shared.Select, new { @class = "required input-background short" })%></div>
                <div><%: Html.ValidationMessageFor(model => model.question.Category_Id)%></div>

                <div class="span-24 last"><h4><%: Html.LabelFor(model => model.question.QuestionType_Id)%></h4></div>
                <div class="span-24 last"><%: Html.DropDownListFor(model => model.question.QuestionType_Id, Model.questionsTypeList, ViewRes.Scripts.Shared.Select, new { @class = "required input-background short" })%></div>
                <div><%: Html.ValidationMessageFor(model => model.question.QuestionType_Id)%></div>
                
                <div class="span-24 last"><h4><%: Html.LabelFor(model => model.question.Text)%></h4></div>
                <div class="span-24 last"><%: Html.TextAreaFor(model => model.question.Text, new { @class = "input-background textArea" })%></div>
                <div><%: Html.ValidationMessageFor(model => model.question.Text)%></div>

                <div class="span-24 last"><h4><%: Html.LabelFor(model => model.question.SortOrder)%></h4></div>
                <div class="span-24 last"><%: Html.TextBoxFor(model => model.question.SortOrder, new { @class = "input-background tiny" })%></div>
                <div><%: Html.ValidationMessageFor(model => model.question.SortOrder)%></div>

                <div class="span-24 last">
                    <h4><%: Html.LabelFor(model => model.question.Positive)%></h4><%: Html.CheckBoxFor(model => model.question.Positive, new { @checked = "checked"  }) %>
                    <div><%: Html.ValidationMessageFor(model => model.question.Positive)%></div>
                </div>
            </div>

