<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Medinet.Models.ViewModels.InstructionLevelViewModel>" %>

<div class="span-24 last"> 
    <div class="span-24 last"><h4><%: Html.LabelFor(model => model.level.Name) %></h4></div>
    <div class="span-24 last"><%: Html.TextBoxFor(model => model.level.Name, new { @class = "input-background short" })%></div> 
    <div><%: Html.ValidationMessageFor(model => model.level.Name)%></div>

    <div class="span-24 last"><h4><%: Html.LabelFor(model => model.level.ShortName) %></h4></div>
    <div class="span-24 last"><%: Html.TextBoxFor(model => model.level.ShortName, new { @class = "input-background short" })%></div> 
    <div><%: Html.ValidationMessageFor(model => model.level.ShortName)%></div>

    <div class="span-24 last"><h4><%: Html.LabelFor(model => model.level.Level)%></h4></div>
    <div class="span-24 last"><%: Html.TextBoxFor(model => model.level.Level, new { @class = "input-background tiny" })%></div>
    <div><%: Html.ValidationMessageFor(model => model.level.Level)%></div>
</div>