<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Medinet.Models.ViewModels.FeatureViewModel>" %>

<div class="span-24 last"> 
    <div class="span-24 last"><h4><%: Html.LabelFor(model => model.feature.FeedbackType_Id)%></h4></div>
    <div class="span-24 last"> <%: Html.DropDownList("feature.FeedbackType_Id", Model.typesList, ViewRes.Scripts.Shared.Select, new { @class = "required input-background short" })%></div>
    <div><%: Html.ValidationMessageFor(model => model.feature.FeedbackType_Id)%></div>
    <div class="span-24 last"><h4><%: Html.LabelFor(model => model.feature.Name)%></h4></div>
    <div class="span-24 last"><%: Html.TextBoxFor(model => model.feature.Name, new { @class = "input-background short" })%></div>
    <div><%: Html.ValidationMessageFor(model => model.feature.Name)%></div>
</div>   