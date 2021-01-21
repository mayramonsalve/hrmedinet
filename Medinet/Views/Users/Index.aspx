<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    <%: ViewRes.Views.Shared.Shared.GenericTitle %> <%: ViewRes.Views.User.Index.TitleIndex%>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div id="contenido-sistema" class="span-24 last"> 
 	    <h2 class="path"><%= ViewRes.Views.User.Index.PathIndex %></h2>
        <div class="linea-sistema-footer"></div>
            <div class="span-23 prepend-1 last"> 
            <div id="grid_wrapper">
                <table id="listUsers">
                    <tr><td>&nbsp;</td></tr>
                </table>
                <div id="pagerUsers"></div>
            </div>
        </div>
    </div>
        <div class="span-24 last">
            <%: Html.ActionLink(ViewRes.Views.User.Index.ActionLink, "Create") %>
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
            $("#listUsers").jqGrid({
                url: '/Users/GridData/',
                datatype: 'json',
                mtype: 'POST',
                colNames: ['Id', '<%: ViewRes.Models.User.UserName %>', '<%: ViewRes.Models.Shared.Name %>', '<%: ViewRes.Models.User.Email %>', '<%: ViewRes.Models.User.ContactPhone %>', '<%: ViewRes.Models.User.Role %>', '', '', ''],
                colModel: [
          { name: 'Id', index: 'Id', align: "right", width: 50, resizable: true, searchoptions: { sopt: ['eq', 'ne', 'lt', 'le', 'gt', 'ge']} },
          { name: 'UserName', index: 'UserName', align: "left", resizable: true, searchoptions: { sopt: ['cn']} },
          { name: 'Name', index: 'Name', align: "left", resizable: true, searchoptions: { sopt: ['cn']} },
          { name: 'Email', index: 'Email', align: "left", resizable: true, searchoptions: { sopt: ['cn']} },
          { name: 'ContactPhone', index: 'ContactPhone', align: "left", resizable: true, searchoptions: { sopt: ['cn']} },
          { name: 'Role.Name', index: 'Role.Name', align: "left", resizable: true, searchoptions: { sopt: ['cn']} },
          { name: 'Action', index: 'Action', align: "left", width: 50, resizable: true, sortable: false, search: false },
          { name: 'Action2', index: 'Action2', align: "left", width: 50, resizable: true, sortable: false, search: false },
          { name: 'Action3', index: 'Action3', align: "left", width: 50, resizable: true, sortable: false, search: false}],
                pager: jQuery('#pagerUsers'),
                rowNum: 10,
                rowList: [10, 20, 50],
                height: 220,
                sortname: 'UserName',
                sortorder: "asc",
                gridview: true,
                viewrecords: true,
                altRows: true,
                caption: '<%: ViewRes.Views.ChartReport.Graphics.UserTab %>',
                editurl: '/Users/Delete',
                loadComplete: function () {
                    $(".ui-icon-trash").click(function () {
                        var gr = $(this).attr("id");
                        $("#listUsers").delGridRow(gr, { reloadAfterSubmit: true });
                    });
                }
            }).navGrid('#pagerUsers',
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
