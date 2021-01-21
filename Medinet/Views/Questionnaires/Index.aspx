<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<Medinet.Models.ViewModels.QuestionnaireViewModel>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
   <%: ViewRes.Views.Shared.Shared.GenericTitle %> <%: ViewRes.Views.Questionnaire.Index.TitleIndex %>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    
    <div id="contenido-sistema" class="span-24 column last"> 
<%--                <% Html.EnableClientValidation(); %>
			     <%: Html.ValidationSummary(ViewRes.Views.Shared.Errors.ErrorInApplication, new { @class = "errorAplication" })%>--%>
        <h2 class="path"><%= ViewRes.Views.Questionnaire.Index.TitleIndex%></h2>
        <div class="linea-sistema-footer"></div>
        <div class="span-23 prepend-1 column last">
         <% if (Model.role == "HRCompany")
            { %> 
            <div class="span-16 column colborder">
         <% } %>
                <div id="grid_wrapper">
                    <table id="listQuestionnaires">
                        <tr><td>&nbsp;</td></tr>
                    </table>
                    <div id="pagerQuestionnaires"></div>
                </div>
         <% if (Model.role == "HRCompany")
            { %> 
            </div>  
            <div class="column span-5 prepend-1 append-1 last">
                <% using (Html.BeginForm("CreateWithTemplate", "Questionnaires"))
                   { %>    
                   <% Html.EnableClientValidation(); %>
			        <%: Html.ValidationSummary(ViewRes.Views.Shared.Errors.ErrorInApplication, new { @class = "errorAplication" })%>
                        <div class="span-24 last column alignCenter"><h3><%: Html.Label(ViewRes.Views.Questionnaire.Index.Templates)%></h3></div>
                        <div class="span-24 last column"><%: Html.DropDownListFor(model => model.Templates, Model.templatesList, ViewRes.Views.Questionnaire.Index.SelectTemplate, new { @class = "required input-background short" })%></div>
                        <div class="span-24 last column"><input type="submit" class="button" value="<%: ViewRes.Views.Questionnaire.Create.CreateUsingTemplateButton %>" /></div>
                <% }%>
            </div>
         <% } %> 
        </div>
    </div>
    <div class="span-24 last" style="width: 500px; height: 100px;">
        <%: Html.ActionLink(ViewRes.Views.Questionnaire.Index.ActionLink, "Create") %>
    </div>
          
           
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>

<asp:Content ID="Content4" ContentPlaceHolderID="CssContent" runat="server">
       <link href="../../Content/Css/ui.jqgrid.css" rel="stylesheet" type="text/css" />
</asp:Content>

<asp:Content ID="Content5" ContentPlaceHolderID="JsContent" runat="server">
    <script src="<%: Url.Content("~/Scripts/jquery.validate.min.js") %>" type="text/javascript"></script>
	<script src="<%: Url.Content("~/Scripts/jquery.validate.unobtrusive.min.js") %>" type="text/javascript"></script>
    <script src="<%: Url.Content(ViewRes.Views.Shared.Shared.GridLanguage) %>" type="text/javascript"></script>  
    <script src="<%: Url.Content("~/Scripts/jquery.jqGrid.min.js") %>" type="text/javascript"></script>
    <script src="<%: Url.Content("~/Scripts/jqueryGridLiquid.js") %>" type="text/javascript"></script>
    <script type="text/javascript">

        $(document).ready(function () {
            $("#listQuestionnaires").jqGrid({
                url: '/Questionnaires/GridData/',
                datatype: 'json',
                mtype: 'POST',
                colNames: ['Id', '<%: ViewRes.Models.Shared.Name %>', '<%: ViewRes.Models.Questionnaire.Description %>', '<%: ViewRes.Models.Questionnaire.Template %>', '', '', ''],
                colModel: [
          { name: 'Id', index: 'Id', align: "right", width: 50, resizable: true, searchoptions: { sopt: ['eq', 'ne', 'lt', 'le', 'gt', 'ge']} },
          { name: 'Name', index: 'Name', align: "left", resizable: true, searchoptions: { sopt: ['cn']} },
          { name: 'Description', index: 'Description', align: "left", resizable: true, searchoptions: { sopt: ['cn']} },
          { name: 'Template', index: 'Template', align: "left", resizable: true, searchoptions: { sopt: ['cn']} },
          { name: 'Action', index: 'Action', align: "left", width: 50, resizable: true, sortable: false, search: false },
          { name: 'Action2', index: 'Action2', align: "left", width: 50, resizable: true, sortable: false, search: false },
          { name: 'Action3', index: 'Action3', align: "left", width: 50, resizable: true, sortable: false, search: false}],
                pager: jQuery('#pagerQuestionnaires'),
                rowNum: 10,
                rowList: [10, 20, 50],
                height: 220,
                sortname: 'Name',
                sortorder: "asc",
                gridview: true,
                viewrecords: true,
                altRows: true,
                caption: '<%: ViewRes.Views.ChartReport.Graphics.QuestionnaireTab %>',
                editurl: '/Questionnaires/Delete',
                loadComplete: function () {
                    $(".ui-icon-trash").click(function () {
                        var gr = $(this).attr("id");
                        $("#listQuestionnaires").delGridRow(gr, { reloadAfterSubmit: true });
                    });
                }
            }).navGrid('#pagerQuestionnaires',
            {
                edit: false, add: false, del: false, search: true, refresh: true
            },
            {}, //edit options
            {}, //add options
            {}, //del options
            {multipleSearch: true} // search options
            );
        });
    </script>
  
</asp:Content>
