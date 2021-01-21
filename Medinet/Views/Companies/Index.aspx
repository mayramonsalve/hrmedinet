<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<Medinet.Models.ViewModels.CompanyViewModel>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    <%: ViewRes.Views.Shared.Shared.GenericTitle %> <%: ViewRes.Views.Company.Index.TitleIndex%>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
 	
    <div id="contenido-sistema" class="span-24 last"> 
 	     <h2 class="path"><%= ViewRes.Views.Company.Index.PathIndex %></h2>
         <div class="linea-sistema-footer"></div>
            <div class="span-23 prepend-1 last"> 
            <div id="grid_wrapper">
                <table id="listCompanies">
                    <tr><td>&nbsp;</td></tr>
                </table>
                <div id="pagerCompanies"></div>
            </div>
        </div>
    </div>
        <div class="span-24 last">
            <%: Html.ActionLink(ViewRes.Views.Company.Index.ActionLink, "Create") %>
        </div>
        <%: Html.Hidden("Cancel", ViewRes.Views.Shared.Shared.CancelLabel, new { id = "Cancel" })%>
        <div id="Dialog" title="Crear demográficos">
            <p class="validateTips">Seleccione país</p>
		    <div class="editor-field">
			    <%: Html.DropDownList("Countries", Model.countryList, ViewRes.Scripts.Shared.Select, new { @class = "input-background short" })%>
		    </div>
            <p class="validateTips">Seleccione idioma</p>
		    <div class="editor-field">
			    <%: Html.DropDownList("Languages", Model.languageList, ViewRes.Scripts.Shared.Select, new { @class = "input-background short" })%>
		    </div>
        </div>
        <div id="DialogClose">
            <div class="span-20 append-2 prepend-2">
                <p>Los demográficos han sido creados exitosamente.</p>
            </div>
        </div>
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>

<asp:Content ID="Content4" ContentPlaceHolderID="CssContent" runat="server">
    <link href="../../Content/Css/ui.jqgrid.css" rel="stylesheet" type="text/css" />
</asp:Content>

<asp:Content ID="Content5" ContentPlaceHolderID="JsContent" runat="server">
    <script src="<%: Url.Content(ViewRes.Views.Shared.Shared.GridLanguage) %>" type="text/javascript"></script>  
    <script src="<%: Url.Content("~/Scripts/jquery.jqGrid.min.js") %>" type="text/javascript"></script>
    <script src="<%: Url.Content("~/Scripts/jqueryGridLiquid.js") %>" type="text/javascript"></script>
    <script src="<%: Url.Content("~/Scripts/company-views.js") %>" type="text/javascript"></script>
    <script type="text/javascript">

        $(document).ready(function () {

            var myGrid = $("#listCompanies").jqGrid({
                url: '/Companies/GridData/',
                datatype: 'json',
                mtype: 'POST',
                colNames: ['Id', '<%: ViewRes.Models.Shared.Name %>', '<%: ViewRes.Models.Company.AssociatedCompany %>', '<%: ViewRes.Models.Company.Contact %>', '<%: ViewRes.Models.Company.Phone %>', '<%: ViewRes.Models.Company.CompanyType %>', '', '', '', ''],
                colModel: [
          { name: 'Id', index: 'Id', align: "right", width: 50, resizable: true, searchoptions: { sopt: ['eq', 'ne', 'lt', 'le', 'gt', 'ge']} },
          { name: 'Name', index: 'Name', align: "left", resizable: true, searchoptions: { sopt: ['cn']} },
          { name: 'CompanyAssociated.Name', index: 'CompanyAssociated.Name', align: "left", resizable: true, searchoptions: { sopt: ['cn']} },
          { name: 'Contact', index: 'Contact', align: "left", resizable: true, searchoptions: { sopt: ['cn']} },
          { name: 'Phone', index: 'Phone', align: "left", width: 100, resizable: true, searchoptions: { sopt: ['cn']} },
          { name: 'CompanyType.Name', index: 'CompanyType.Name', align: "left", resizable: true, searchoptions: { sopt: ['cn']} },
          { name: 'Action', index: 'Action', align: "left", width: 50, resizable: true, sortable: false, search: false },
          { name: 'Action2', index: 'Action2', align: "left", width: 50, resizable: true, sortable: false, search: false },
          { name: 'Action3', index: 'Action3', align: "left", width: 50, resizable: true, sortable: false, search: false },
          { name: 'Action4', index: 'Action4', align: "left", width: 50, resizable: true, sortable: false, search: false}],
                pager: jQuery('#pagerCompanies'),
                rowNum: 10,
                rowList: [10, 20, 50],
                height: 220,
                sortname: 'Name',
                sortorder: "asc",
                gridview: true,
                viewrecords: true,
                altRows: true,
                caption: '<%: ViewRes.Views.ChartReport.Graphics.CompanyTab %>',
                editurl: '/Companies/Delete',
                loadComplete: function () {
                    $(".ui-icon-trash").click(function () {
                        var gr = $(this).attr("id");
                        $("#listCompanies").delGridRow(gr, { reloadAfterSubmit: true });
                    });
                    $(".ui-icon-copy").click(function () {
                        var companyId = $(this).attr("id");
                        LoadDialog(companyId);
                        myGrid.trigger('reloadGrid');
                    });
                }
            }).navGrid('#pagerCompanies',
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
