<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<MedinetClassLibrary.Models.Category>" %>

<div class="span-24 last"> 
    <div class="span-24 last"><h4><%: Html.LabelFor(model => model.Questionnaire_Id)%></h4></div>
    <div class="span-24 last"><%:Model.Questionnaire.Name.ToString()%></div>
    <%if (Model.CategoryGroup_Id.HasValue)
      { %>
        <div class="span-24 last"><h4><%: Html.LabelFor(model => model.CategoryGroup_Id)%></h4></div>
        <div class="span-24 last"><%:Model.GroupingCategory.Name.ToString()%></div>
    <%} %>
    <div class="span-24 last"><h4><%: Html.LabelFor(model => model.Name) %></h4></div>
    <div class="span-24 last"><%:Model.Name.ToString() %></div> 
    <div class="span-24 last"><h4><%: Html.LabelFor(model => model.Description)%></h4></div>
    <div class="span-24 last"><%:Model.Description.ToString()%></div>
</div>
