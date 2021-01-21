<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Medinet.Models.ViewModels.StateViewModel>" %>

        <div class="span-24 last"> 
                <div class="span-24 last"><h4><%: Html.LabelFor(model => model.state.Country_Id) %></h4></div>
                <div class="span-24 last"> <%: Html.DropDownList("state.Country_Id", Model.countriesList, ViewRes.Scripts.Shared.Select, new { @class = "required input-background short" })%></div>
                <div><%: Html.ValidationMessageFor(model => model.state.Country_Id)%></div>
                <div class="span-24 last"><h4><%: Html.LabelFor(model => model.state.Name)%></h4></div>
                <div class="span-24 last"><%: Html.TextBoxFor(model => model.state.Name, new { @class = "input-background short" })%></div>
                <div><%: Html.ValidationMessageFor(model => model.state.Name)%></div>
         </div>   