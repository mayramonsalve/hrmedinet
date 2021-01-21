<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<MedinetClassLibrary.Models.Seniority>" %>

<div class="span-24 last"> 
    <div class="span-24 last"><h4><%: Html.LabelFor(model => model.Name) %></h4></div>
    <div class="span-24 last"><%: Model.Name.ToString()%></div> 

    <div class="span-24 last"><h4><%: Html.LabelFor(model => model.ShortName) %></h4></div>
    <div class="span-24 last"><%: Model.ShortName.ToString()%></div> 

    <div class="span-24 last"><h4><%: Html.LabelFor(model => model.Level) %></h4></div>
    <div class="span-24 last"><%: Model.Level.ToString()%></div> 
</div>