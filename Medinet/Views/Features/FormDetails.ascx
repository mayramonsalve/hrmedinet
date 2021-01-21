<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Medinet.Models.ViewModels.FeatureViewModel>" %>


    <div class="span-24 last"> 
        <div class="span-24 last"><h4><%: Html.LabelFor(model => model.feature.FeedbackType_Id) %></h4></div>
        <div class="span-24 last"><%: Model.feature.FeedbackType.Name.ToString()%></div>
        <div class="span-24 last"><h4><%: Html.LabelFor(model => model.feature.Name)%></h4></div>
        <div class="span-24 last"><%: Model.feature.Name.ToString()%></div> 
        <div class="span-24 last"><h4><%: ViewRes.Controllers.ChartReports.Average%></h4></div>
        <div class="span-24 last"><%:String.Format("{0:0.##}", Model.average)%></div> 
    </div>  