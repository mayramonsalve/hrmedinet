<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Medinet.Models.ViewModels.ClimateRangeViewModel>" %>
            
    <div class="span-24 last"> 
        <div class="span-24 last"><h4><%: Html.LabelFor(model => model.ClimateRange.ClimateScale_Id)%></h4></div>
        <div class="span-24 last"><%: Html.DropDownListFor(model => model.ClimateRange.ClimateScale_Id, Model.ClimateScalesList, ViewRes.Scripts.Shared.Select, new { @class = "required, input-background short" })%></div> 
        <div><%: Html.ValidationMessageFor(model => model.ClimateRange.ClimateScale_Id)%></div>
                
        <div class="span-24 last"><h4><%: Html.LabelFor(model => model.ClimateRange.Name)%></h4></div>
        <div class="span-24 last"><%: Html.TextBoxFor(model => model.ClimateRange.Name, new { @class = "input-background short" })%></div>
        <div><%: Html.ValidationMessageFor(model => model.ClimateRange.Name)%></div>

        <div class="span-24 last"><h4><%: Html.LabelFor(model => model.ClimateRange.MinValue)%></h4></div>
        <div class="span-24 last"><%: Html.TextBoxFor(model => model.ClimateRange.MinValue, new { @class = "input-background tiny" })%></div>
        <div><%: Html.ValidationMessageFor(model => model.ClimateRange.MinValue)%></div>

        <div class="span-24 last"><h4><%: Html.LabelFor(model => model.ClimateRange.MaxValue)%></h4></div>
        <div class="span-24 last"><%: Html.TextBoxFor(model => model.ClimateRange.MaxValue, new { @class = "input-background tiny" })%></div>
        <div><%: Html.ValidationMessageFor(model => model.ClimateRange.MaxValue)%></div>

        <div class="span-24 last"><h4><%: Html.LabelFor(model => model.ClimateRange.Color)%></h4></div>
        <div class="span-24 last"><%: Html.TextBoxFor(model => model.ClimateRange.Color, new { @class = "input-background short color {hash:true, pickerPosition:'right'}" })%></div>
        <div><%: Html.ValidationMessageFor(model => model.ClimateRange.Color)%></div>

        <div class="span-24 last"><h4><%: Html.LabelFor(model => model.ClimateRange.Action)%></h4></div>
        <div class="span-24 last"><%: Html.TextBoxFor(model => model.ClimateRange.Action, new { @class = "input-background short" })%></div>
        <div><%: Html.ValidationMessageFor(model => model.ClimateRange.Action)%></div>
    </div>