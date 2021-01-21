<%@ Language="C#" Inherits="System.Web.Mvc.ViewUserControl<MedinetClassLibrary.Models.ClimateScale>"%>
<div class="span-24 last"> 
    <div class="span-24 last"><h4><%: Html.LabelFor(model => model.Name) %></h4></div>
    <div class="span-24 last"><%: Model.Name %></div> 
    <div class="span-24 last"><h4><%: Html.LabelFor(model => model.Description)%></h4></div>
    <div class="span-24 last"><%: Model.Description%></div>
</div>
