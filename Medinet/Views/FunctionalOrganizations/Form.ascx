<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Medinet.Models.ViewModels.FunctionalOrganizationViewModel>" %>

<div class="span-24 column last"> 
    <fieldset class="span-9 column">
        <legend><%: ViewRes.Models.FOrganization.Parent %></legend>
        <div class="span-24 column last"><h4><%: ViewRes.Models.FOrganization.FOTParent %></h4></div>
        <div class="span-24 column last"> <%: Html.DropDownList("Parent.Type_Id", Model.typesParentList, ViewRes.Scripts.Shared.Select, new { @class = "input-background short" })%></div>
        <div class="span-24 column last"><h4><%: ViewRes.Models.FOrganization.FOParent %></h4></div>
        <div class="span-24 column last"> <%: Html.DropDownListFor(model => model.functionalOrganization.FOParent_Id, Model.fosParentList, ViewRes.Scripts.Shared.Select, new { @class = "input-background short" })%></div>
    </fieldset>
    <div class="span-2 column">&nbsp;</div>
    <fieldset class="span-9 column">
        <legend><%: ViewRes.Models.FOrganization.Entity %></legend>
        <div class="span-24 column last"><h4><%: ViewRes.Models.FOrganization.FOType %></h4></div>
        <div class="span-24 column last"> <%: Html.DropDownListFor(model => model.functionalOrganization.Type_Id, Model.typesList, ViewRes.Scripts.Shared.Select, new { @class = "required input-background short" })%></div>
        <div><%: Html.ValidationMessageFor(model => model.functionalOrganization.Type_Id)%></div>
        <div class="span-24 column last"><h4><%: ViewRes.Models.Shared.Name%></h4></div>
        <div class="span-24 column last"><%: Html.TextBoxFor(model => model.functionalOrganization.Name, new { @class = "input-background short" })%></div>
        <div><%: Html.ValidationMessageFor(model => model.functionalOrganization.Name)%></div>
        <div class="span-24 column last"><h4><%: ViewRes.Models.Shared.ShortName%></h4></div>
        <div class="span-24 column last"><%: Html.TextBoxFor(model => model.functionalOrganization.ShortName, new { @class = "input-background short" })%></div> 
        <div><%: Html.ValidationMessageFor(model => model.functionalOrganization.ShortName)%></div>
    </fieldset>
    <div class="span-4 column last">&nbsp;</div>
</div>   