<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Medinet.Models.ViewModels.TestViewModel>" %>

            <div class="span-24 last"> 
                <div class="span-24 last"><h4>Demográficos</h4></div>
                <div class="span-24 last"><%: Html.DropDownList("test_DemographicSelector", Model.demographicsList, ViewRes.Scripts.Shared.Select, new { @class = "required input-background short" })%></div>
                <%--<div><%: Html.ValidationMessageFor(model => model.test.Company_Id)%></div>    --%>          
                                
                <div class="span-24 last"><h4><%: ViewRes.Models.Test.Questionnaires%></h4></div>
                <div class="span-24 last"><%: Html.DropDownList("test_QuestionnairesInTest", Model.questionnairesList, new { @multiple = "multiple", @class = "multiselect required input-background short" })%></div>
                <%--<div><%: Html.ValidationMessageFor(model => model.test.Questionnaire_Id)%></div>  --%>
                
                <div class="span-24 last"><h4>Matching</h4></div>
                <table id="Matching" class="display tabla span-24 last">
                    <thead>
                        <tr>
                            <td></td>
                            <td>Cuestionario</td>
                            <td>Selector</td>
                        </tr>
                    </thead>
                    <tbody>
                    </tbody>
                </table>           
         </div>

                
<asp:Content ID="Content4" ContentPlaceHolderID="JsContent" runat="server">
 <script src="<%: Url.Content("~/Scripts/grid.locale-en.js") %>" type="text/javascript"></script>
    <script src="<%: Url.Content("~/Scripts/jquery.jqGrid.min.js") %>" type="text/javascript"></script>
    <script src="<%: Url.Content("~/Scripts/jquery.searchFilter.js") %>" type="text/javascript"></script>
    <script src="<%: Url.Content("~/Scripts/jqueryGridLiquid.js") %>" type="text/javascript"></script>

    <script type="text/javascript">
        $(document).ready(function () {

            function resize_the_grid() {
                $('#listsdemoselec').fluidGrid({ base: '#grid_wrapper', offset: 100 });
            }

            var myGrid = jQuery("#listsdemoselec").jqGrid({
                url: '/Tests/GridData/',
                datatype: 'json',
                mtype: 'POST',
                colNames: ['ID', 'Demographic', ' Selector Values', 'Questionnaire ', 'Test', '', ''],
                colModel: [
              { name: 'Id', index: 'Id', align: "right", resizable: true, searchoptions: { sopt: ['eq', 'ne', 'lt', 'le', 'gt', 'ge']} },
              { name: 'Projects_Cost.Project.Name', index: 'Projects_Cost.Project.Name', align: "center", resizable: true, searchoptions: { sopt: ['cn']} },
              { name: 'Effective_Date', index: 'Effective_Date', align: "center", resizable: true, searchoptions: { sopt: ['cn']} },
              { name: 'Amount', index: 'Amount', align: "center", resizable: true, searchoptions: { sopt: ['eq', 'ne', 'lt', 'le', 'gt', 'ge']} },
              { name: 'Monay', index: 'Money', align: "center", resizable: true, searchoptions: { sopt: ['cn']} },
              { name: 'Action', index: 'Action', align: "center", resizable: true, sortable: false, search: false },
              { name: 'Action2', index: 'Action2', align: "center", resizable: true, sortable: false, search: false}],
                pager: jQuery('#pagerdemoselec'),
                rowNum: 50,
                rowList: [50, 100, 150],
                height: 220,
                sortname: 'Id',
                sortorder: "Asc",
                autowidth: true,
                gridview: true,
                viewrecords: true,
                altRows: true,
                caption: 'Costs',
                editurl: '/Costs/Delete',
                loadComplete: function () {
                    $(".ui-icon-trash").click(function () {
                        var gr = $(this).attr("id");
                        $("#listsdemoselec").delGridRow(gr, { reloadAfterSubmit: true });
                    });
                }
            }).navGrid('#pagerdemoselec',
                {
                    edit: false, add: false, del: false, search: true, refresh: true
                },
                {}, //edit options
                {}, //add options
                {}, //del options
                {multipleSearch: true} // search options
                );
            resize_the_grid();

            $(window).resize(resize_the_grid);

        });
    </script>
  
</asp:Content>