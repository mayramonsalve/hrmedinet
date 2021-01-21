<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<Medinet.Models.ViewModels.ClimateRangeViewModel>" %>
<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
  <%: ViewRes.Views.Shared.Shared.GenericTitle %> <%: ViewRes.Views.ClimateRange.Index.TitleIndex %>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
        <div id="contenido-sistema" class="span-24 last"> 
 	        <h2 class="path"><%= ViewRes.Views.ClimateRange.Index.PathIndex%></h2>
            <div class="linea-sistema-footer"></div>
            <div class="span-23 prepend-1 last"> 
                <h4><%: ViewRes.Views.ClimateRange.Index.DropDownClimateScales%></h4>
	            <%: Html.DropDownList("ClimateScales", Model.ClimateScalesList, ViewRes.Scripts.Shared.Select, new { @class = "input-background short" })%>
                <br/><br/>
                <div id="grid_wrapper">
                    <table id="listClimateRanges">
                        <tr><td>&nbsp;</td></tr>
                    </table>
                    <div id="pagerClimateRanges"></div>
                </div>
            </div>
        </div>

    <div class="span-24 last">
    <%: Html.ActionLink(ViewRes.Views.ClimateRange.Index.ActionLink, "Create", new { @climateScale_id = Model.ClimateRange.ClimateScale_Id }, new { id = "editlink" })%>
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
    <script src="<%: Url.Content("~/Scripts/jqueryGridLiquid.js") %>" type="text/javascript"></script>

    <script type="text/javascript">
            $(document).ready(function () {

                var myGrid = jQuery("#listClimateRanges").jqGrid({
                    url: '/ClimateRanges/GridData',
                datatype: 'json',
                mtype: 'POST',
                postData: {
                    climateScale_id: function () { return jQuery("#ClimateScales option:selected").val(); }
                },
                colNames: ['Id', '<%: ViewRes.Models.Shared.Name %>', '<%: ViewRes.Models.ClimateRange.MinValue %>', '<%: ViewRes.Models.ClimateRange.MaxValue %>', '<%: ViewRes.Models.ClimateRange.Action %>', '', '', ''],
                colModel: [
                  { name: 'Id', index: 'Id', align: "right", width: 50, resizable: true, searchoptions: { sopt: ['eq', 'ne', 'lt', 'le', 'gt', 'ge']} },
                  { name: 'Name', index: 'Name', align: "left", resizable: true, searchoptions: { sopt: ['cn']} },
                  { name: 'MinValue', index: 'MinValue', align: "right", width: 100, resizable: true, searchoptions: { sopt: ['eq', 'ne', 'lt', 'le', 'gt', 'ge']} },
                  { name: 'MaxValue', index: 'MaxValue', align: "right", width: 100, resizable: true, searchoptions: { sopt: ['eq', 'ne', 'lt', 'le', 'gt', 'ge']} },
                  { name: 'Action', index: 'Action', align: "left", resizable: true, searchoptions: { sopt: ['cn']} },
                  { name: 'Action1', index: 'Action1', align: "left", width: 50, resizable: true, sortable: false, search: false },
                  { name: 'Action2', index: 'Action2', align: "left", width: 50, resizable: true, sortable: false, search: false },
                  { name: 'Action3', index: 'Action3', align: "left", width: 50, resizable: true, sortable: false, search: false}],
                pager: jQuery('#pagerClimateRanges'),
                rowNum: 10,
                rowList: [10, 20, 50],
                height: 220,
                sortname: 'Name',
                sortorder: "asc",
                gridview: true,
                viewrecords: true,
                altRows: true,
                caption: '<%: ViewRes.Views.ChartReport.Graphics.ClimateRangeTab %>',
                editurl: '/ClimateRanges/Delete',
                loadComplete: function () {
                    $(".ui-icon-trash").click(function () {
                        var gr = $(this).attr("id");
                        jQuery("#listClimateRanges").delGridRow(gr, { reloadAfterSubmit: true });
                    });
                }
            }).navGrid('#pagerClimateRanges',
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
                value = '/ClimateRanges/Create?climateScale_id=' + $("#ClimateScales option:selected").val();
                $('#editlink').attr('href', value);
            }

            $("#ClimateScales").change(myReload);

            //updateLinks();
        });
       
    </script>
</asp:Content>
