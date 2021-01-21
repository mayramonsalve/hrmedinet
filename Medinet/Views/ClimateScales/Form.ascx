<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<MedinetClassLibrary.Models.ClimateScale>" %>

        <div class="span-24 last"> 
            <div class="span-24 last"><h4><%: Html.LabelFor(model => model.Name) %></h4></div>
            <div class="span-24 last"><%: Html.TextBoxFor(model => model.Name, new { @class = "input-background large" })%></div> 
            <div><%: Html.ValidationMessageFor(model => model.Name)%></div>
            <div class="span-24 last"><h4><%: Html.LabelFor(model => model.Description)%></h4></div>
            <div class="span-24 last"><%: Html.TextAreaFor(model => model.Description, new { @class = "input-background textArea" })%></div>
            <div><%: Html.ValidationMessageFor(model => model.Description)%></div>
        </div>
