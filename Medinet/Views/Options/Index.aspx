﻿<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<Medinet.Models.ViewModels.OptionViewModel>" %>
<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
  <%: ViewRes.Views.Shared.Shared.GenericTitle %> <%: ViewRes.Views.Option.Index.TitleIndex %>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
        <div id="contenido-sistema" class="span-24 last"> 
 	        <h2 class="path"><%= ViewRes.Views.Option.Index.PathIndex %></h2>
            <div class="linea-sistema-footer"></div>
            <div class="span-23 prepend-1 last"> 
                <h4><%: ViewRes.Views.Option.Index.DropDownQuestionnaires %></h4>
	            <%: Html.DropDownList("Questionnaires", Model.questionnairesList, ViewRes.Scripts.Shared.Select, new { @class = "input-background short" })%>
                <br/><br/>
                <div id="grid_wrapper">
                    <table id="listOptions">
                        <tr><td>&nbsp;</td></tr>
                    </table>
                    <div id="pagerOptions"></div>
                </div>
            </div>
        </div>

    <div class="span-24 last">
    <%: Html.ActionLink(ViewRes.Views.Option.Index.ActionLink, "Create", new { @questionnaire_id = 0 }, new { id = "editlink" })%>
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

            var myGrid = jQuery("#listOptions").jqGrid({
                url: '/Options/GridData',
                datatype: 'json',
                mtype: 'POST',
                postData: {
                    questionnaire_id: function () { return jQuery("#Questionnaires option:selected").val(); }
                },
                colNames: ['Id', '<%: ViewRes.Models.Option.Text %>', '<%: ViewRes.Models.Option.Value %>', '', '', ''],
                colModel: [
                  { name: 'Id', index: 'Id', align: "right", width: 50, resizable: true, searchoptions: { sopt: ['eq', 'ne', 'lt', 'le', 'gt', 'ge']} },
                  { name: 'Text', index: 'Text', align: "left", resizable: true, searchoptions: { sopt: ['cn']} },
                  { name: 'Value', index: 'Value', align: "right", width: 50, resizable: true, searchoptions: { sopt: ['eq', 'ne', 'lt', 'le', 'gt', 'ge']} },
                  { name: 'Action', index: 'Action', align: "left", width: 50, resizable: true, sortable: false, search: false },
                  { name: 'Action2', index: 'Action2', align: "left", width: 50, resizable: true, sortable: false, search: false },
                  { name: 'Action3', index: 'Action3', align: "left", width: 50, resizable: true, sortable: false, search: false}],
                pager: jQuery('#pagerOptions'),
                rowNum: 10,
                rowList: [10, 20, 50],
                height: 220,
                sortname: 'Text',
                sortorder: "asc",
                gridview: true,
                viewrecords: true,
                altRows: true,
                caption: '<%: ViewRes.Views.ChartReport.Graphics.OptionTab %>',
                editurl: '/Options/Delete',
                loadComplete: function () {
                    $(".ui-icon-trash").click(function () {
                        var gr = $(this).attr("id");
                        jQuery("#listOptions").delGridRow(gr, { reloadAfterSubmit: true });
                    });
                }
            }).navGrid('#pagerOptions',
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
                value = '/Options/Create?questionnaire_id=' + $("#Questionnaires option:selected").val();
                $('#editlink').attr('href', value);
            }

            $("#Questionnaires").change(myReload);

            //updateLinks();
        });
       
    </script>
</asp:Content>
