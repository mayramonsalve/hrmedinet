<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<Medinet.Models.ViewModels.StateViewModel>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
   <%: ViewRes.Views.Shared.Shared.GenericTitle %> <%: ViewRes.Views.State.Index.TitleIndex %>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">


    <div id="contenido-sistema" class="column span-24 last"> 
 	     <h2 class="path"><%= ViewRes.Views.State.Index.TitleIndex %></h2>
         <div class="linea-sistema-footer"></div>
    
            <div class="span-23 prepend-1 last"> 
                <h4> <%: ViewRes.Views.State.Index.DropDownCountries %></h4>
	            <%: Html.DropDownList("Countries", Model.countriesList, ViewRes.Scripts.Shared.Select, new { @class = "input-background short" })%>
                <br/><br/>
                <div id="grid_wrapper">
                <table id="listStates">
                    <tr><td>&nbsp;</td></tr>
                </table>
                <div id="pagerStates"></div>
                </div>
            </div>
        </div>
    <div class="span-24 last">
        <%: Html.ActionLink(ViewRes.Views.State.Index.ActionLink, "Create", null, new { @country_id = 0 }, new { id = "editlink" })%>
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

            var myGrid = jQuery("#listStates").jqGrid({
                url: '/States/GridData',
                datatype: 'json',
                mtype: 'POST',
                postData: {
                    country_id: function () { return jQuery("#Countries option:selected").val(); }
                },
                colNames: ['Id', '<%: ViewRes.Models.Shared.Name %>', '', '', ''],
                colModel: [
                  { name: 'Id', index: 'Id', align: "right", width: 50, resizable: true, searchoptions: { sopt: ['eq', 'ne', 'lt', 'le', 'gt', 'ge']} },
                  { name: 'Name', index: 'Name', align: "left", resizable: true, searchoptions: { sopt: ['cn']} },
                  { name: 'Action', index: 'Action', align: "left", width: 50, resizable: true, sortable: false, search: false },
                  { name: 'Action2', index: 'Action2', align: "left", width: 50, resizable: true, sortable: false, search: false },
                  { name: 'Action3', index: 'Action3', align: "left", width: 50, resizable: true, sortable: false, search: false}],
                pager: jQuery('#pagerStates'),
                rowNum: 10,
                rowList: [10, 20, 50],
                height: 220,
                sortname: 'Name',
                sortorder: "asc",
                gridview: true,
                viewrecords: true,
                altRows: true,
                caption: '<%: ViewRes.Views.ChartReport.Graphics.StateTab %>',
                editurl: '/States/Delete',
                loadComplete: function () {
                    $(".ui-icon-trash").click(function () {
                        var gr = $(this).attr("id");
                        jQuery("#listStates").delGridRow(gr, { reloadAfterSubmit: true });
                    });
                }
            }).navGrid('#pagerStates',
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
                value = '/States/Create?country_id=' + $("#Countries option:selected").val();
                $('#editlink').attr('href', value);
            }

            $("#Countries").change(myReload);
            updateLinks();
        });
    </script>
</asp:Content>
