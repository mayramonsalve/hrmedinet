<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<bool>" %>
	<div class="editor-label">  
        <%= Html.LabelFor(model => model) %>  
    </div>  
    <div class="editor-field">  
        <%= Html.CheckBoxFor(model => model) %>  
        <%= Html.ValidationMessageFor(model => model) %>  
    </div>
