<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Medinet.Models.ViewModels.CategoryViewModel>" %>
            <%: Html.Hidden("Role", Model.UserLogged.Role.Name, new { @id = "Role" })%>
              <%: Html.Hidden("Select", ViewRes.Scripts.Shared.Select, new { id = "ViewRes" })%>
            <div class="span-24 last"> 
                <div class="span-24 last">
                    <h4><%: ViewRes.Views.Category.Create.IsGroupingCategory %></h4><%: Html.CheckBox("agrupacion", Model.isGroupingCategory, new {@id="agrupacion" }) %>
                </div>
                <div id="DivQuestionnaires">
                    <div class="span-24 last"><h4><%: Html.LabelFor(model => model.category.Questionnaire_Id)%></h4></div>
                    <div class="span-24 last"><%: Html.DropDownListFor(model => model.category.Questionnaire_Id, Model.questionnairesList, ViewRes.Scripts.Shared.Select, new { @class = "input-background short" })%></div>
                    <div><%: Html.ValidationMessageFor(model => model.category.Questionnaire_Id)%></div>
                </div>
                <% if (Model.UserLogged.Role.Name == "HRAdministrator")
                   { %>                        
                        <div id="DivCompany">
                            <div class="span-24 last"><h4><%: Html.LabelFor(model => model.category.Company_Id)%></h4></div>
                            <div class="span-24 last"><%: Html.DropDownListFor(model => model.category.Company_Id, Model.companiesList, ViewRes.Scripts.Shared.Select, new { @class = "input-background short" })%></div>
                            <div><%: Html.ValidationMessageFor(model => model.category.Company_Id)%></div>
                        </div>
                        
                        <%--
                        <div id="DivCompanies" style="display: none;">
                            <div class="span-24 last"><h4><%: Html.LabelFor(model => model.category.Company_Id)%></h4></div>
                            <div class="span-24 last"><%: Html.DropDownListFor(model => model.category.Company_Id, Model.companiesList, ViewRes.Scripts.Shared.Select, new { @class = "input-background short" })%></div>
                            <div><%: Html.ValidationMessageFor(model => model.category.Company_Id)%></div>
                         </div>--%>

                <% }
                   else
                   {%>
                        <%: Html.HiddenFor(model => model.category.Company_Id)%>
                   <%} %>                
                <div id="DivCategories">
                <div class="span-24 last"><h4><%: Html.LabelFor(model => model.category.CategoryGroup_Id)%></h4></div>
                <div class="span-24 last"><%: Html.DropDownListFor(model => model.category.CategoryGroup_Id, Model.categoriesList, ViewRes.Scripts.Shared.Select, new { @class = "input-background short" })%></div>
                <div><%: Html.ValidationMessageFor(model => model.category.CategoryGroup_Id)%></div>
                </div>
                <div class="span-24 last"><h4><%: Html.LabelFor(model => model.category.Name) %></h4></div>
                <div class="span-24 last"><%: Html.TextBoxFor(model => model.category.Name, new { @class = "input-background large" })%></div> 
                <div><%: Html.ValidationMessageFor(model => model.category.Name)%></div>
                <div class="span-24 last"><h4><%: Html.LabelFor(model => model.category.Description)%></h4></div>
                <div class="span-24 last"><%: Html.TextAreaFor(model => model.category.Description, new { @class = "input-background textArea" })%></div>
                <div><%: Html.ValidationMessageFor(model => model.category.Description)%></div>
            </div>
  

