<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<Medinet.Models.ViewModels.ChartReportViewModel>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
   <%: ViewRes.Views.Shared.Shared.GenericTitle %> <%: ViewRes.Views.ChartReport.Graphics.NoEvaluationsTitle%> 
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
 <div id="contenido-small" class="span-24 last" style="margin-top: 100px;">
        <div class="span-5 column image-padding-top">
            <span id="noEvaluations-image" class="column"></span>
        </div>
        <div class="span-17 prepend-1 append-1 column last">
            <h2><%: ViewRes.Views.ChartReport.Graphics.NoEvaluationsTitle %></h2>
                <div class="linea-sistema-footer"></div>
            <h4><%: ViewRes.Views.ChartReport.Graphics.NoEvaluationsText %>
            <%: Model.testName %>.</h4>
        </div>
        <div class="clear"></div>
    <div class="span-24 last">
        <%: Html.ActionLink(ViewRes.Views.ChartReport.ReportsList.BackToList, "ReportsList", "ChartReports")%>
    </div>
</div>
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>

<asp:Content ID="Content4" ContentPlaceHolderID="CssContent" runat="server">
</asp:Content>

<asp:Content ID="Content5" ContentPlaceHolderID="JsContent" runat="server">
</asp:Content>
