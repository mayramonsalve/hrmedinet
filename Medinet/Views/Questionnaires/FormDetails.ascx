<%@ Language="C#" Inherits="System.Web.Mvc.ViewUserControl<MedinetClassLibrary.Models.Questionnaire>"%>
<div class="span-24 last"> 
    <div class="span-24 last"><h4><%: Html.LabelFor(model => model.Name) %></h4></div>
    <div class="span-24 last"><%: Model.Name %></div> 
    <div class="span-24 last"><h4><%: Html.LabelFor(model => model.Description)%></h4></div>
    <div class="span-24 last"><%: Model.Description%></div>
    <div class="span-24 last"><h4><%: Html.LabelFor(model => model.Instructions)%></h4></div>
    <div class="span-24 last"><%: Model.Instructions%></div>
    <div class="span-24 last"><h4><%: Html.LabelFor(model => model.Template)%></h4></div>
    <%if(Model.Template)
        { %>
        <div class="span-24 last"><%: ViewRes.Views.Shared.Shared.True%></div>
    <%}
        else
        { %>
        <div class="span-24 last"><%: ViewRes.Views.Shared.Shared.False%></div>
    <%} %>
</div>
