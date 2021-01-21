<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Medinet.Models.ViewModels.SeniorityViewModel>" %>

 <div class="span-24 last"> 
    <div class="span-24 last"><h4><%: Html.LabelFor(model => model.seniority.Name)%></h4></div>
    <div class="span-24 last"><%: Html.TextBoxFor(model => model.seniority.Name, new { @class = "input-background short" })%></div> 
    <div><%: Html.ValidationMessageFor(model => model.seniority.Name)%></div>

    <div class="span-24 last"><h4><%: Html.LabelFor(model => model.seniority.ShortName)%></h4></div>
    <div class="span-24 last"><%: Html.TextBoxFor(model => model.seniority.ShortName, new { @class = "input-background short" })%></div> 
    <div><%: Html.ValidationMessageFor(model => model.seniority.ShortName)%></div>

    <div class="span-24 last"><h4><%: Html.LabelFor(model => model.seniority.Level)%></h4></div>
    <div class="span-24 last"><%: Html.TextBoxFor(model => model.seniority.Level, new { @class = "input-background tiny" })%></div>
    <div><%: Html.ValidationMessageFor(model => model.seniority.Level)%></div>
</div>