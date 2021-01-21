<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Mobile/MobileMini.Master" Inherits="System.Web.Mvc.ViewPage<Medinet.Models.ViewModels.ChartReportViewModel>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
   <%: ViewRes.Views.Shared.Shared.GenericTitle %> <%: ViewRes.Views.ChartReport.Graphics.NoEvaluationsTitle%> 
</asp:Content>

<asp:Content ID="Content7" ContentPlaceHolderID="PageId" runat="server">
<div data-role="page" id="noEvaluation" class="basic">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
 <div class="box rounded">
        <div>
            <h1><%: ViewRes.Views.ChartReport.Graphics.NoEvaluationsTitle %></h1>
            <h4><%: ViewRes.Views.ChartReport.Graphics.NoEvaluationsText %>
            <%: Model.testName %>.</h4>
        </div>
        <%Html.RenderPartial("Button.Mobile"); %>
</div>
</asp:Content>