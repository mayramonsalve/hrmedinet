<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<Medinet.Models.ViewModels.LocationViewModel>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    <%: ViewRes.Views.Shared.Shared.GenericTitle %> <%: ViewRes.Views.Location.Index.TitleIndex %>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
 
    <div id="contenido-sistema" class="span-24 last"> 
 	    <h2 class="path"><%= ViewRes.Views.Location.Index.PathIndex %></h2>
        <div class="linea-sistema-footer"></div>
        <div class="span-23 prepend-1 last"> 
            <div class="span-24 last">
                <h4><%: ViewRes.Views.Location.Index.DropDownCountries %></h4>
	            <%: Html.DropDownList("Countries", Model.countriesList, ViewRes.Scripts.Shared.Select, new { @class = "input-background short" })%>
	        </div>
            <div class="span-24 last">
                <h4> <%: ViewRes.Views.Location.Index.DropDownStates %></h4>
	            <%: Html.DropDownList("States", Model.statesList, ViewRes.Scripts.Shared.Select, new { @class = "input-background short" })%>
	        </div>
            </br></br>
            <div id="grid_wrapper">
                <table id="listLocations">
                        <tr><td>&nbsp;</td></tr>
                </table>
                <div id="pagerLocations"></div>
            </div>
        </div>
    </div>
    <div class="span-24 last">
        <%: Html.ActionLink(ViewRes.Views.Location.Index.ActionLink, "Create", new { @country_id = 0, @state_id = 0 }, new { id = "editlink" })%>
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

            $('#Countries').change(function () {
                if ($(this).val()) {
                    updateStatesList();
                }
                }
            );

            function updateStatesList() {
                $.post("/States/GetStatesByCountry", { country_id: $("#Countries").val() }, function (j) {
                    var options = '<option value=""> <%: ViewRes.Scripts.Shared.Select %> </option>';
                    for (var i = 0; i < j.length; i++) {
                        options += '<option value="' + j[i].optionValue + '">' + j[i].optionDisplay + '</option>';
                    }
                    $("#States").html(options);
                });
            }

            var myGrid = jQuery("#listLocations").jqGrid({
                url: '/Locations/GridData/',
                datatype: 'json',
                mtype: 'POST',
                postData: {
                    state_id: function () { return jQuery("#States option:selected").val(); }
                },
                colNames: ['Id', '<%: ViewRes.Models.Shared.Name %>', '<%: ViewRes.Models.Shared.ShortName %>', '<%: ViewRes.Models.Location.Region %>', '', '', ''],
                colModel: [
          { name: 'Id', index: 'Id', align: "right", width: 50, resizable: true, searchoptions: { sopt: ['eq', 'ne', 'lt', 'le', 'gt', 'ge']} },
          { name: 'Name', index: 'Name', align: "left", width: 150, resizable: true, searchoptions: { sopt: ['cn']} },
          { name: 'ShortName', index: 'ShortName', align: "left", width: 150, resizable: true, searchoptions: { sopt: ['cn']} },
          { name: 'Region.Name', index: 'Region.Name', align: "left", width: 150, resizable: true, searchoptions: { sopt: ['cn']} },
          { name: 'Action', index: 'Action', align: "left", width: 50, resizable: true, sortable: false, search: false },
          { name: 'Action2', index: 'Action2', align: "left", width: 50, resizable: true, sortable: false, search: false },
          { name: 'Action3', index: 'Action3', align: "left", width: 50, resizable: true, sortable: false, search: false}],
                pager: jQuery('#pagerLocations'),
                rowNum: 10,
                rowList: [10, 20, 50],
                height: 220,
                sortname: 'Name',
                sortorder: "asc",
                gridview: true,
                viewrecords: true,
                altRows: true,
                caption: '<%: ViewRes.Views.ChartReport.Graphics.LocationTab %>',
                editurl: '/Locations/Delete',
                loadComplete: function () {
                    $(".ui-icon-trash").click(function () {
                        var gr = $(this).attr("id");
                        jQuery("#listLocations").delGridRow(gr, { reloadAfterSubmit: true });
                    });
                }
            }).navGrid('#pagerLocations',
            {
                edit: false, add: false, del: false, search: true, refresh: true
            },
            {}, //edit options
            {}, //add options
            {}, //del options
            {multipleSearch: true} // search options
            );
            var myReload = function () {
                myGrid.trigger('reloadGrid');
                value = '/Locations/Create?country_id=' + $("#Countries").val() + '&state_id=' + $("#States option:selected").val();
                $('#editlink').attr('href', value);
            };

            $("#States").change(myReload);
        });
</script>
</asp:Content>

