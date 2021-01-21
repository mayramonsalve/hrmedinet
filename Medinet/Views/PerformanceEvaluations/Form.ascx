<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Medinet.Models.ViewModels.PerformanceEvaluationViewModel>" %>

   <div class="span-24 last"> 
        <div class="span-24 last"><h4><%: Html.LabelFor(model => model.performanceEvaluation.Name)%></h4></div>
        <div class="span-24 last"><%: Html.TextBoxFor(model => model.performanceEvaluation.Name, new { @class = "input-background short" })%></div> 
        <div><%: Html.ValidationMessageFor(model => model.performanceEvaluation.Name)%></div>

        <div class="span-24 last"><h4><%: Html.LabelFor(model => model.performanceEvaluation.ShortName) %></h4></div>
        <div class="span-24 last"><%: Html.TextBoxFor(model => model.performanceEvaluation.ShortName, new { @class = "input-background short" })%></div> 
        <div><%: Html.ValidationMessageFor(model => model.performanceEvaluation.ShortName)%></div>

        <div class="span-24 last"><h4><%: Html.LabelFor(model => model.performanceEvaluation.Level)%></h4></div>
        <div class="span-24 last"><%: Html.TextBoxFor(model => model.performanceEvaluation.Level, new { @class = "input-background tiny" })%></div>
        <div><%: Html.ValidationMessageFor(model => model.performanceEvaluation.Level)%></div>
    </div>
        