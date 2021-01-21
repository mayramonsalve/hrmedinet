<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<MedinetClassLibrary.Models.Location>" %>


        <div class="span-24 last"> 
                <div class="span-24 last"><h4><%: ViewRes.Views.Location.Create.DropDownCountries %></h4></div>
                <div class="span-24 last"><%:Model.State.Country.Name.ToString()%></div>
                
                <div class="span-24 last"><h4><%: Html.LabelFor(model => model.State_Id)%></h4></div>
                <div class="span-24 last"><%: Model.State.Name.ToString()%></div>
                <% if(Model.Region != null){ %>
                <div class="span-24 last"><h4><%: Html.LabelFor(model => model.Region_Id)%></h4></div>
                <div class="span-24 last"><%: Model.Region.Name.ToString()%></div>
                <%} %>
                <div class="span-24 last"><h4><%: Html.LabelFor(model => model.Name)%></h4></div>
                <div class="span-24 last"><%: Model.Name.ToString()%></div>

                <div class="span-24 last"><h4><%: Html.LabelFor(model => model.ShortName)%></h4></div>
                <div class="span-24 last"><%: Model.ShortName.ToString()%></div>
         </div>