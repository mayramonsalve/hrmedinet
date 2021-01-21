<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<Medinet.Models.ViewModels.TestViewModel>" %>
<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
     <%: ViewRes.Views.Shared.Shared.GenericTitle %> <%: ViewRes.Views.Test.Index.TitleIndex %>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
   
    <div id="contenido-sistema" class="span-24 last"> 
 	     <h2 class="path"><%= ViewRes.Views.Test.Index.PathIndex %></h2>
        <div class="linea-sistema-footer"></div>
        <%if (Model.userRole == "HRAdministrator")
          {%>
           <div class="span-23 prepend-1 last">
                <h4><%: ViewRes.Views.Test.Index.DropDownCompanies %></h4>
	            <%: Html.DropDownList("Companies", Model.companiesList, ViewRes.Scripts.Shared.Select, new { @class="input-background short"})%>
                <br/><br/>
                <div id="Div1">
                    <table id="listTests2">
                        <tr><td>&nbsp;</td></tr>
                    </table>
                    <div id="pagerTests2"></div>
                </div>
            </div>
        </div>
            <div class="span-24 last">
                <%: Html.ActionLink(ViewRes.Views.Test.Index.ActionLink, "Create", null, new { @company_id = 0 }, new { id = "editlink" })%>
            </div>

         <% } else if(Model.userRole == "HRCompany"){%>
            <div class="span-23 prepend-1 last"> 
                <div id="grid_wrapper">
                    <table id="listTests">
                        <tr><td>&nbsp;</td></tr>
                    </table>
                    <div id="pagerTests"></div>
                </div>
            </div>
        </div>
    <div class="span-24 last">
        <%: Html.ActionLink(ViewRes.Views.Test.Index.ActionLink, "Create")%>
    </div>
     <% }%>
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
            $("#listTests").jqGrid({
                url: '/Tests/GridData/',
                datatype: 'json',
                mtype: 'POST',
                colNames: ['Id', '<%: ViewRes.Models.Shared.Name %>', '<%: ViewRes.Models.Test.Code %>', '<%: ViewRes.Models.Test.Questionnaire %>', '', '', '', '', '', ''],
                colModel: [
          { name: 'Id', index: 'Id', align: "right", width: 50, resizable: true, searchoptions: { sopt: ['eq', 'ne', 'lt', 'le', 'gt', 'ge']} },
          { name: 'Name', index: 'Name', align: "left", resizable: true, searchoptions: { sopt: ['cn']} },
          { name: 'Code', index: 'Code', align: "left", resizable: true, searchoptions: { sopt: ['cn']} },
          { name: 'Questionnaire.Name', index: 'Questionnaire.Name', align: "left", resizable: true, searchoptions: { sopt: ['cn']} },
          { name: 'Action', index: 'Action', align: "left", width: 50, resizable: true, sortable: false, search: false },
          { name: 'Action2', index: 'Action2', align: "left", width: 50, resizable: true, sortable: false, search: false },
          { name: 'Action3', index: 'Action3', align: "left", width: 50, resizable: true, sortable: false, search: false },
          { name: 'Action4', index: 'Action4', align: "left", width: 50, resizable: true, sortable: false, search: false },
          { name: 'Action5', index: 'Action5', align: "left", width: 50, resizable: true, sortable: false, search: false },
          { name: 'Action6', index: 'Action6', align: "left", width: 50, resizable: true, sortable: false, search: false}],
                pager: jQuery('#pagerTests'),
                rowNum: 10,
                rowList: [10, 20, 50],
                height: 220,
                sortname: 'Name',
                sortorder: "asc",
                gridview: true,
                viewrecords: true,
                altRows: true,
                caption: 'Tests',
                editurl: '/Tests/Delete',
                loadComplete: function () {
                    $(".ui-icon-trash").click(function () {
                        var gr = $(this).attr("id");
                        $("#listTests").delGridRow(gr, { reloadAfterSubmit: true });
                    });
                }
            }).navGrid('#pagerTests',
            {
                edit: false, add: false, del: false, search: true, refresh: true
            },
            {}, //edit options
            {}, //add options
            {}, //del options
            {multipleSearch: true} // search options
            );
        

        var myGrid = jQuery("#listTests2").jqGrid({
                  url: '/Tests/GridData/',
                  datatype: 'json',
                  mtype: 'POST',
                  postData: {
                      company_id: function () { return jQuery("#Companies option:selected").val(); }
                  },
                  colNames: ['Id', '<%: ViewRes.Models.Shared.Name %>', '<%: ViewRes.Models.Test.Code %>', '<%: ViewRes.Models.Test.Questionnaire %>', '', '', '', '', '', ''],
                  colModel: [
          { name: 'Id', index: 'Id', align: "right", width: 50, resizable: true, searchoptions: { sopt: ['eq', 'ne', 'lt', 'le', 'gt', 'ge']} },
          { name: 'Name', index: 'Name', align: "left", resizable: true, searchoptions: { sopt: ['cn']} },
          { name: 'Code', index: 'Code', align: "left", resizable: true, searchoptions: { sopt: ['cn']} },
          { name: 'Questionnaire.Name', index: 'Questionnaire.Name', align: "left", resizable: true, searchoptions: { sopt: ['cn']} },
          { name: 'Action', index: 'Action', align: "left", width: 50, resizable: true, sortable: false, search: false },
          { name: 'Action2', index: 'Action2', align: "left", width: 50, resizable: true, sortable: false, search: false },
          { name: 'Action3', index: 'Action3', align: "left", width: 50, resizable: true, sortable: false, search: false },
          { name: 'Action4', index: 'Action4', align: "left", width: 50, resizable: true, sortable: false, search: false }, 
          { name: 'Action5', index: 'Action5', align: "left", width: 50, resizable: true, sortable: false, search: false },
          { name: 'Action6', index: 'Action6', align: "left", width: 50, resizable: true, sortable: false, search: false}],
                  pager: jQuery('#pagerTests2'),
                  rowNum: 10,
                  rowList: [10, 20, 50],
                  height: 220,
                  sortname: 'Name',
                  sortorder: "asc",
                  gridview: true,
                  viewrecords: true,
                  altRows: true,
                  caption: '<%: ViewRes.Views.ChartReport.Graphics.TestTab %>',
                  editurl: '/Tests/Delete',
                  loadComplete: function () {
                      $(".ui-icon-trash").click(function () {
                          var gr = $(this).attr("id");
                          jQuery("#listTests2").delGridRow(gr, { reloadAfterSubmit: true });
                      });
                  }
              }).navGrid('#pagerTests2',
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
                  value = '/Tests/Create?company_id=' + $("#Companies option:selected").val();
                  $('#editlink').attr('href', value);
              };

              $("#Companies").change(myReload);
          });
</script>



</asp:Content>
