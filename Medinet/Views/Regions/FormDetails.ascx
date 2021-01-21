<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Medinet.Models.ViewModels.RegionViewModel>" %>

<div class="span-24 last">  
    <div class="span-24 last"><h4><%: Html.LabelFor(model => model.region.Name) %></h4></div>
    <div class="span-24 last"><%: Model.region.Name.ToString()%></div> 
</div>