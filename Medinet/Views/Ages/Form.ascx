<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Medinet.Models.ViewModels.AgeViewModel>" %>

<div class="span-24 last"> 
    <div class="span-24 last"><h4><%: Html.LabelFor(model => model.age.Name) %></h4></div>
    <div class="span-24 last"><%: Html.TextBoxFor(model => model.age.Name, new { @class = "input-background short" })%></div> 
    <div><%: Html.ValidationMessageFor(model => model.age.Name)%></div>

    <div class="span-24 last"><h4><%: Html.LabelFor(model => model.age.ShortName) %></h4></div>
    <div class="span-24 last"><%: Html.TextBoxFor(model => model.age.ShortName, new { @class = "input-background short" })%></div> 
    <div><%: Html.ValidationMessageFor(model => model.age.ShortName)%></div>

    <div class="span-24 last"><h4><%: Html.LabelFor(model => model.age.Level)%></h4></div>
    <div class="span-24 last"><%: Html.TextBoxFor(model => model.age.Level, new { @class = "input-background tiny" })%></div>
    <div><%: Html.ValidationMessageFor(model => model.age.Level)%></div>
</div>