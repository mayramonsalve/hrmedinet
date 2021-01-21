<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<Medinet.Models.ViewModels.DemoViewModel>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    <%: ViewRes.Views.Shared.Shared.GenericTitle %> Listar demos
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    
        <div id="contenido-sistema" class="span-24 last"> 
 	    <h2 class="path">Demos > Listar</h2>
        <div class="linea-sistema-footer"></div>
            <div class="span-23 prepend-1 last"> 
                <div id="grid_wrapper">
                    <table id="listDemos">
                            <tr><td>&nbsp;</td></tr>
                    </table>
                    <div id="pagerDemos"></div>
                </div>
            </div>
        </div>
        <div class="span-24 last">
            <%: Html.ActionLink("Crear nuevo demo", "Create")%>
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

    <script type="text/javascript">


        $(document).ready(function () {

            var myGrid = jQuery("#listDemos").jqGrid({
                url: '/Demos/GridData/',
                datatype: 'json',
                mtype: 'POST',
                colNames: ['Id', 'Company', 'Code', 'Evaluations', 'Dates', 'User', '', '', ''],
                colModel: [
          { name: 'Id', index: 'Id', align: "right", width: 50, resizable: true, searchoptions: { sopt: ['eq', 'ne', 'lt', 'le', 'gt', 'ge']} },
          { name: 'Company.Name', index: 'Company.Name', align: "left", resizable: true, searchoptions: { sopt: ['cn']} },
          { name: 'Company.Test.Code', index: 'Company.Test.Code', align: "left", resizable: true, searchoptions: { sopt: ['cn']} },
          { name: 'Company.Test.CurrentEvaluations', index: 'Company.Test.CurrentEvaluations', align: "right", resizable: true, searchoptions: { sopt: ['eq', 'ne', 'lt', 'le', 'gt', 'ge']} },
          { name: 'Company.Test.StartDate', index: 'Company.Test.StartDate', align: "left", resizable: true, searchoptions: { sopt: ['cn']} },
          { name: 'Company.User.UserName', index: 'Company.User.UserName', align: "left", resizable: true, searchoptions: { sopt: ['cn']} },
          { name: 'Action', index: 'Action', align: "left", width: 50, resizable: true, sortable: false, search: false },
          { name: 'Action2', index: 'Action2', align: "left", width: 50, resizable: true, sortable: false, search: false },
          { name: 'Action3', index: 'Action3', align: "left", width: 50, resizable: true, sortable: false, search: false}],
                pager: jQuery('#pagerDemos'),
                rowNum: 10,
                rowList: [10, 20, 50],
                height: 220,
                sortname: 'Company.Name',
                sortorder: "asc",
                gridview: true,
                viewrecords: true,
                altRows: true,
                caption: 'Demos',
                editurl: '/Demos/Delete',
                loadComplete: function () {
                    $(".ui-icon-trash").click(function () {
                        var gr = $(this).attr("id");
                        jQuery("#listDemos").delGridRow(gr, { reloadAfterSubmit: true });
                    });
                }
            }).navGrid('#pagerDemos',
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