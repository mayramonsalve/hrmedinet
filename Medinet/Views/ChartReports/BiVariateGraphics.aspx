<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<Medinet.Models.ViewModels.ChartReportViewModel>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    <%: ViewRes.Views.Shared.Shared.GenericTitle %> <%:ViewRes.Views.ChartReport.Graphics.BivariateTitle %>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">   
    <div id="contenido-sistema" class="span-24 last column">
        <h2 class="path"><%: ViewRes.Views.ChartReport.Graphics.BivariateTitle%></h2>
        <div class="linea-sistema-footer"></div>
        <%: Html.Hidden("test_id",Model.test.Id) %> 
        <%: Html.Hidden("isTable",false) %> 
        <%: Html.Hidden("Select", ViewRes.Scripts.Shared.Select, new { id = "ViewRes" })%>
        <%: Html.Hidden("company_id", Model.test.Company_Id) %>
         <div class="editor-label">
            <h4><%:ViewRes.Views.ChartReport.Graphics.Demographics %>: 
		    <%: Html.DropDownList("Demographics1", Model.demographicsList1, ViewRes.Scripts.Shared.Select)%></h4>
	    </div>
        <div class="editor-label">
           <h4><%:ViewRes.Views.ChartReport.Graphics.Demographics %>: 
	        <%: Html.DropDownList("Demographics2", Model.demographicsList2, ViewRes.Scripts.Shared.Select)%></h4>
	    </div>
        <br/>
       <%: Html.Hidden("DownloadImage", ViewRes.Views.ChartReport.Graphics.DownloadImage)%>
        <div class="span-23 append-1 last">
            <div id="DivChart" style="display:none;" align="center">
                <%--<img id="Chart" src="" alt=" " />--%>
                <div id="Chart" class="column span-24 bivariate-chart"></div>
                    <div class="clear"></div>
                    <div id="Img" class="span-24 google_img"></div>
            </div>
            
            <div id="FieldsetSmallTable" class="prepend-3 append-3 span-18" style=" display:none;"><%-- los coloca ocultos con style=" display:none;--%>
                
                <table id="SmallTable" class="display span-24 column last tabla" >
                </table>
            </div>
            <fieldset id="FieldsetTable" class="alignCenter" style=" display:none;"><%-- los coloca ocultos con style=" display:none;--%>
                <div class="alignCenter"><h4><%: Html.Label("Title","") %></h4></div>
                <table id="Table" class="display span-24 column last tabla" >
                </table>
            </fieldset>

        </div>

        <div id="noMinimum" class="span-24" style="display:none;">
            <%Html.RenderPartial("NoMinimum"); %>
        </div>
    </div>
    <div class="span-24 column last alignRight">
        <%: Html.ActionLink(ViewRes.Views.ChartReport.ReportsList.BackToList, "ReportsList", "ChartReports")%>
    </div>
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>

<asp:Content ID="Content4" ContentPlaceHolderID="CssContent" runat="server">
    <link href="../../Content/Css/demo_page.css" rel="stylesheet" type="text/css" media="screen, projection" />
    <link href="../../Content/Css/demo_table_jui.css" rel="stylesheet" type="text/css" media="screen, projection" />
    <link href="../../Content/Css/demo_table.css" rel="stylesheet" type="text/css" media="screen, projection" />
</asp:Content>

<asp:Content ID="Content5" ContentPlaceHolderID="JsContent" runat="server">
    <script src="<%: Url.Content("~/Scripts/jquery.dataTables.js") %>" type="text/javascript"></script>
    <script src="<%: Url.Content("~/Scripts/jquery.dataTables.min.js") %>" type="text/javascript"></script>
        <script type="text/javascript" src="https://www.gstatic.com/charts/loader.js"></script>
    <script src="<%: Url.Content("~/Scripts/BivariateCharts.js?mm=0709") %>" type="text/javascript"></script>
</asp:Content>
