<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<Medinet.Models.ViewModels.FunctionalOrganizationViewModel>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
   <%: ViewRes.Views.Shared.Shared.GenericTitle %> <%: ViewRes.Views.FunctionalOrganization.Index.TitleIndex %>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div id="contenido-sistema" class="span-24 last"> 
 	     <h2 class="path"><%= ViewRes.Views.FunctionalOrganization.Index.TitleIndex %></h2>
         <div class="linea-sistema-footer"></div>
            <div class="span-23 prepend-1 last"> 
                <h4> <%: ViewRes.Views.FunctionalOrganization.Index.DropDownTypes %></h4>
	            <%: Html.DropDownList("Types", Model.typesList, ViewRes.Scripts.Shared.Select, new { @class = "form-short" })%>
	            <br/><br/>
                <div id="grid_wrapper">
                <table id="listFunctionalOrganizations">
                    <tr><td>&nbsp;</td></tr>
                </table>
                <div id="pagerFunctionalOrganizations"></div>
                </div>
            </div>
        </div>
    <div class="span-24 last">
        <%: Html.ActionLink(ViewRes.Views.FunctionalOrganization.Index.ActionLink, "Create", null, new { @type_id = 0 }, new { id = "editlink" })%>
    </div>

</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>

<asp:Content ID="Content4" ContentPlaceHolderID="CssContent" runat="server">
    <link href="/Content/Css/ui.jqgrid.css" rel="stylesheet" type="text/css" media="screen, projection" />
</asp:Content>

<asp:Content ID="Content5" ContentPlaceHolderID="JsContent" runat="server">
    <script src="<%: Url.Content(ViewRes.Views.Shared.Shared.GridLanguage) %>" type="text/javascript"></script> 
    <script src="<%: Url.Content("~/Scripts/jquery.jqGrid.min.js") %>" type="text/javascript"></script>
    <script src="<%: Url.Content("~/Scripts/jquery.searchFilter.js") %>" type="text/javascript"></script>
    <script src="<%: Url.Content("~/Scripts/jqueryGridLiquid.js") %>" type="text/javascript"></script>

    <script type="text/javascript">
        $(document).ready(function () {

            var myGrid = jQuery("#listFunctionalOrganizations").jqGrid({
                url: '/FunctionalOrganizations/GridData',
                datatype: 'json',
                mtype: 'POST',
                postData: {
                    type_id: function () { return jQuery("#Types option:selected").val(); }
                },
                colNames: ['Id', '<%: ViewRes.Models.Shared.Name %>', '<%: ViewRes.Models.Shared.ShortName %>', '', '', ''],
                colModel: [
                  { name: 'Id', index: 'Id', align: "right", width: 50, resizable: true, searchoptions: { sopt: ['eq', 'ne', 'lt', 'le', 'gt', 'ge']} },
                  { name: 'Name', index: 'Name', align: "left", resizable: true, searchoptions: { sopt: ['cn']} },
                  { name: 'ShortName', index: 'ShortName', align: "left", resizable: true, searchoptions: { sopt: ['cn']} },
                  { name: 'Action', index: 'Action', align: "left", width: 50, resizable: true, sortable: false, search: false },
                  { name: 'Action2', index: 'Action2', align: "left", width: 50, resizable: true, sortable: false, search: false },
                  { name: 'Action3', index: 'Action3', align: "left", width: 50, resizable: true, sortable: false, search: false}],
                pager: jQuery('#pagerFunctionalOrganizations'),
                rowNum: 10,
                rowList: [10, 20, 50],
                height: 220,
                sortname: 'Name',
                sortorder: "asc",
                gridview: true,
                viewrecords: true,
                altRows: true,
                caption: '<%: ViewRes.Views.ChartReport.Graphics.FOrganizationTab %>',
                editurl: '/FunctionalOrganizations/Delete',
                loadComplete: function () {
                    $(".ui-icon-trash").click(function () {
                        var gr = $(this).attr("id");
                        jQuery("#listFunctionalOrganizations").delGridRow(gr, { reloadAfterSubmit: true });
                    });
                }
            }).navGrid('#pagerFunctionalOrganizations',
                    {
                        edit: false, add: false, del: false, search: true, refresh: true
                    },
                    {}, //edit options
                    {}, //add options
                    {}, //del options
                    {multipleSearch: true} // search options
                    );

            //myGrid = myGrid;

            var myReload = function () {
                myGrid.trigger('reloadGrid');
                updateLinks();
            };

            function updateLinks() {
                value = '/FunctionalOrganizations/Create?type_id=' + $("#Types option:selected").val();
                $('#editlink').attr('href', value);
            }

            $("#Types").change(myReload);
            updateLinks();
        });
    </script>
</asp:Content>
