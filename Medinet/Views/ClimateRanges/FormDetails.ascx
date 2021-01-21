<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<MedinetClassLibrary.Models.ClimateRange>" %>

        <div class="span-24 last"> 
                <div class="span-24 last"><h4><%: Html.LabelFor(model => model.ClimateScale_Id) %></h4></div>
                <div class="span-24 last"><%: Model.ClimateScale.Name%></div> 
                
                <div class="span-24 last"><h4><%: Html.LabelFor(model => model.Name)%></h4></div>
                <div class="span-24 last"><%: Model.Name%></div>

                <div class="span-24 last"><h4><%: Html.LabelFor(model => model.MaxValue)%></h4></div>
                <div class="span-24 last"><%: Model.MaxValue%></div>

                <div class="span-24 last"><h4><%: Html.LabelFor(model => model.MinValue)%></h4></div>
                <div class="span-24 last"><%: Model.MinValue%></div>

                <div class="span-24 last"><h4><%: Html.LabelFor(model => model.Color)%></h4></div>
                <% string color = "background-color:" + Model.Color; %>
                <div class="span-24 last"><span style=<%:color %>>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</span></div>

                <div class="span-24 last"><h4><%: Html.LabelFor(model => model.Action)%></h4></div>
                <div class="span-24 last"><%: Model.Action%></div>
         </div>
