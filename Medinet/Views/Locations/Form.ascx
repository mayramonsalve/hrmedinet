<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Medinet.Models.ViewModels.LocationViewModel>"%>

        <div class="span-24 last">  
                <%: Html.Hidden("Select", ViewRes.Scripts.Shared.Select, new { id = "ViewRes" })%> 
                <div class="span-24 last"><h4><%: Html.LabelFor(model => model.location.State.Country_Id)%></h4></div>
                <div class="span-24 last"><%: Html.DropDownList("Country_Id", Model.countriesList, ViewRes.Scripts.Shared.Select, new { @class = "input-background short" })%></div>
                
                <div class="span-24 last"><h4><%: Html.LabelFor(model => model.location.State_Id)%></h4></div>
                <div class="span-24 last"><%: Html.DropDownList("location.State_Id", Model.statesList, ViewRes.Scripts.Shared.Select, new { @class = "input-background short" })%></div>
                <div><%: Html.ValidationMessageFor(model => model.location.State_Id)%></div>
                <% if (Model.regionsList.Count() > 0)
                   { %>
                <div class="span-24 last"><h4><%: Html.LabelFor(model => model.location.Region_Id)%></h4></div>
                <div class="span-24 last"><%: Html.DropDownList("location.Region_Id", Model.regionsList, ViewRes.Scripts.Shared.Select, new { @class = "input-background short" })%></div>
                <div><%: Html.ValidationMessageFor(model => model.location.Region_Id)%></div>
                <%} %>
                <div class="span-24 last"><h4><%: Html.LabelFor(model => model.location.Name)%></h4></div>
                <div class="span-24 last"><%: Html.TextBoxFor(model => model.location.Name, new { @class = "input-background short" })%></div>
                <div><%: Html.ValidationMessageFor(model => model.location.Name)%></div>

                <div class="span-24 last"><h4><%: Html.LabelFor(model => model.location.ShortName)%></h4></div>
                <div class="span-24 last"><%: Html.TextBoxFor(model => model.location.ShortName, new { @class = "input-background short" })%></div>
                <div><%: Html.ValidationMessageFor(model => model.location.ShortName)%></div>
         </div>