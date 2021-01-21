<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Medinet.Models.ViewModels.StateViewModel>" %>


        <div class="span-24 last"> 
                <div class="span-24 last"><h4><%: Html.LabelFor(model => model.state.Name) %></h4></div>
                <div class="span-24 last"><%: Model.state.Name.ToString() %></div> 
                <div class="span-24 last"><h4><%: Html.LabelFor(model => model.state.Country_Id) %></h4></div>
                <div class="span-24 last"><%: Model.state.Country.Name.ToString() %></div>
         </div>  