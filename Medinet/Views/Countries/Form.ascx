<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<MedinetClassLibrary.Models.Country>" %>


<div class="span-24 last">  
    <div class="span-24 last"><h4><%: Html.LabelFor(model => model.Name) %></h4></div>
    <div class="span-24 last"><%: Html.TextBoxFor(model => model.Name, new { @class = "input-background short" })%></div> 
    <div><%: Html.ValidationMessageFor(model => model.Name)%></div>
</div>

 