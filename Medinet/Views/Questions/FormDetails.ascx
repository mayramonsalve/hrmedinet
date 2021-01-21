<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<MedinetClassLibrary.Models.Question>" %>
    <% 
        Response.Write(Model.QuestionsType.Name); %>
            <div class="span-24 last"> 
                <div class="span-24 last"><h4><%: ViewRes.Views.Question.Create.DropDownQuestionnaires %></h4></div>
                <div class="span-24 last"><%: Model.Category.Questionnaire.Name.ToString()%></div>
                
                <div class="span-24 last"><h4><%: Html.LabelFor(model => model.Category_Id)%></h4></div>
                <div class="span-24 last"><%: Model.Category.Name.ToString()%></div>

                <div class="span-24 last"><h4><%: Html.LabelFor(model => model.QuestionType_Id)%></h4></div>
                <div class="span-24 last"><%:Model.QuestionsType.Name.ToString()%></div>
                
                <div class="span-24 last"><h4><%: Html.LabelFor(model => model.Text)%></h4></div>
                <div class="span-24 last"><%:Model.Text.ToString()%></div>

                <div class="span-24 last"><h4><%: Html.LabelFor(model => model.SortOrder)%></h4></div>
                <div class="span-24 last"><%: Model.SortOrder.ToString()%></div>

                <div class="span-24 last"><h4><%: Html.LabelFor(model => model.Positive)%></h4></div>
                <%if (Model.Positive)
                  { %>
                    <div class="span-24 last"><%: ViewRes.Views.Shared.Shared.True%></div>
                <%}
                  else
                  { %>
                    <div class="span-24 last"><%: ViewRes.Views.Shared.Shared.False%></div>
                <%} %>
            </div>