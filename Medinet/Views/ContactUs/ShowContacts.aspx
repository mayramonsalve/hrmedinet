<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    <%: ViewRes.Views.Shared.Shared.GenericTitle %> <%: ViewRes.Views.ContactUs.Index.TitleIndex %>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div id="contenido-sistema" class="span-24 last"> 
 	     <h2 class="path"><%= ViewRes.Views.ContactUs.Index.PathIndex%></h2>
         <div class="linea-sistema-footer"></div>
            <div class="span-23 prepend-1 last">   
            <div id="grid_wrapper">
                <table id="listContacts">
                    <tr><td>&nbsp;</td></tr>
                </table>
                <div id="pagerContacts"></div>
            </div>
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

    <script type="text/javascript">

        $(document).ready(function () {
            $("#listContacts").jqGrid({
                url: '/ContactUs/GridData/',
                datatype: 'json',
                mtype: 'POST',
                colNames: ['Id', '<%: ViewRes.Models.Shared.Name %>', '<%: ViewRes.Models.Shared.Company %>', '<%: ViewRes.Models.ContactUs.Date %>', '', ''],
                colModel: [
              { name: 'Id', index: 'Id', align: "right", width: 50, resizable: true, searchoptions: { sopt: ['eq', 'ne', 'lt', 'le', 'gt', 'ge']} },
              { name: 'Name', index: 'Name', align: "left", resizable: true, searchoptions: { sopt: ['cn']} },
              { name: 'Company', index: 'Company', align: "left", resizable: true, searchoptions: { sopt: ['cn']} },
              { name: 'Date', index: 'Date', align: "left", resizable: true, searchoptions: { sopt: ['cn']} },
              { name: 'Action2', index: 'Action2', align: "left", width: 50, resizable: true, sortable: false, search: false },
              { name: 'Action3', index: 'Action3', align: "left", width: 50, resizable: true, sortable: false, search: false}],
                pager: jQuery('#pagerContacts'),
                rowNum: 10,
                rowList: [10, 20, 50],
                height: 220,
                sortname: 'Date',
                sortorder: "desc",
                gridview: true,
                viewrecords: true,
                altRows: true,
                caption: '<%: ViewRes.Views.ContactUs.Index.Contacts %>',
                editurl: '/ContactUs/Delete',
                loadComplete: function () {
                    $(".ui-icon-trash").click(function () {
                        var gr = $(this).attr("id");
                        $("#listContacts").delGridRow(gr, { reloadAfterSubmit: true });
                    });
                }
            }).navGrid('#pagerContacts',
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
