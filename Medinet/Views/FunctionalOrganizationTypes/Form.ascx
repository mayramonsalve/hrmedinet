<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Medinet.Models.ViewModels.FunctionalOrganizationTypeViewModel>" %>

<div class="span-24 last"> 
    <div class="span-24 last"><h4><%: Html.LabelFor(model => model.type.FOTParent_Id) %></h4></div>
    <div class="span-24 last"> <%: Html.DropDownListFor(model => model.type.FOTParent_Id, Model.typesList, ViewRes.Scripts.Shared.Select, new { @class = "input-background short" })%></div>
    <div><%: Html.ValidationMessageFor(model => model.type.FOTParent_Id)%></div>

    <div class="span-24 last"><h4><%: Html.LabelFor(model => model.type.Name)%></h4></div>
    <div class="span-24 last"><%: Html.TextBoxFor(model => model.type.Name, new { @class = "input-background short" })%></div> 
    <div><%: Html.ValidationMessageFor(model => model.type.Name)%></div>

    <div class="span-24 last"><h4><%: Html.LabelFor(model => model.type.ShortName) %></h4></div>
    <div class="span-24 last"><%: Html.TextBoxFor(model => model.type.ShortName, new { @class = "input-background short" })%></div> 
    <div><%: Html.ValidationMessageFor(model => model.type.ShortName)%></div>
</div>