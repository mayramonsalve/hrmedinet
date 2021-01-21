<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Medinet.Models.ViewModels.ChartReportViewModel>" %>
<%@ Import Namespace="MedinetClassLibrary.CustomClasses" %>
<%@ Import Namespace="MedinetClassLibrary.Models" %>
<% List<SummaryTable> totalList;

   int test = Model.test.Id;
   int id = Model.GetByDemographicAndType("AllTests", "Univariate").Id;
   int order = Model.GetByDemographicAndType("AllTests", "Univariate").Order;    
   string source = Model.GetByDemographicAndType("AllTests", "Univariate").Source;
   %> 
   <%: Html.Hidden("AllTestsgraphic_id", id)%>
   <%: Html.Hidden("AllTestsElementsCount", 1, new { id = "AllTestsElementsCount" })%>
   
   <%  totalList = Model.NameAllTestsSastNoSast(null, null, null, test); %> 
    <div>
        <strong><%: ViewRes.Views.ChartReport.Graphics.Parametres%></strong>
        <fieldset data-role="fieldcontain">
        <% if (!Model.test.OneQuestionnaire)
            { %>

            <label for="AllTestsGroupByQuestionnairesDDL" ><%: ViewRes.Views.ChartReport.Graphics.Questionnaire%></label> 
            <%: Html.DropDownList("AllTestsGroupByQuestionnairesDDL", Model.questionnaires, ViewRes.Scripts.Shared.ShowAll, new { @class = "GroupByQuestionnairesDDL form-short " })%>                         
        <%} %>
            <label for="AllTestsGroupByCategoriesDDL" ><%: ViewRes.Views.ChartReport.Graphics.Category%></label>
            <%: Html.DropDownList("AllTestsGroupByCategoriesDDL", Model.GroupByCategories, ViewRes.Scripts.Shared.ShowAll, new { @class = "GroupByCategoriesDDL form-short " })%>
            <label for="AllTestsGroupByQuestionsDDL" ><%: ViewRes.Views.ChartReport.Graphics.Question%></label>
            <%: Html.DropDownList("AllTestsGroupByQuestionsDDL", Model.question, ViewRes.Scripts.Shared.ShowAll, new { @class = "GroupByQuestionsDDL form-short" })%>
        </fieldset>     
            
    </div>
    <div class="principal">
        <div class="chartDiv portrait">
            <img id="AllTestsChart" name="AllTestsChart" src="<%:source%>&graphic_id=<%:id%>&test_id=<%:test%>" alt="Grafico"  />
        </div>
        <!--Seccion derecha-->
        <div class="generalDiv">
            <%    List<GraphicDetail> details = Model.graphics[order].GraphicDetails.ToList();                   
                    if (Model.UserLogged.Role.Name == "HRAdministrator")
                   {
                       %>
                    <div id="AllTestsCommentsDiv">
                        <div data-role="fieldcontain">
                                <label for="AllTestsTitle"><%: ViewRes.Views.ChartReport.Graphics.TableTitle%></label>
                                <%: Html.TextBox("AllTestsTitle", Model.details[order].Title, new { @class = "input-background short" })%>
                                <label for="AllTestsXAxis"><%: ViewRes.Views.ChartReport.Graphics.AxisX%></label>
                                <%: Html.TextBox("AllTestsXAxis", Model.details[order].AxisXName, new { @class = "input-background short" })%>
                                <label for="AllTestsYAxis"><%: ViewRes.Views.ChartReport.Graphics.AxisY%></label>
                                <%: Html.TextBox("AllTestsYAxis", Model.details[order].AxisYName, new { @class = "input-background short" })%>
                                <label for="AllTestsCommentsEditor"><%: ViewRes.Views.ChartReport.Graphics.Comments%></label>
                                <%:Html.TextArea("AllTestsCommentsEditor", Model.details[order].Text, new { @class = "input-background textEditor" })%>
                        </div>
                        <input data-role="button" data-theme="f" type="submit" value="<%: ViewRes.Views.Account.LogOn.LogOnButton %>" class="button" />
                    </div>
                   <%}
                   else
                   {
                   //MOSTRAR COMENTARIOS
                       if (details.Count() > 0)
                       {%>
                        <div><strong><%: ViewRes.Views.ChartReport.Graphics.Comments%></strong></div>
                        <div><%:Model.details[order].Text%></div>
                     <%}
                   }%>

        </div>
    </div>
    <div id="AllTestsDivSat">
        <table id="AllTestsTable" class="table1">
            <thead>
                <tr>
                    <th></th>
                    <th><%: ViewRes.Views.ChartReport.Graphics.Satisfied%></th>
                    <th><%: ViewRes.Views.ChartReport.Graphics.NoSatisfied%></th>
                </tr>
            </thead>
            <tbody>
                <%foreach (SummaryTable table in totalList)
                    {%>
                    <tr>                        
                        <td style="color:Black;"><%:table.Label%></td>
                        <td style="color:Green; text-align: right;"><%:table.Satisfied%></td>     
                        <td style="color:Red; text-align: right;"><%:table.NotSatisfied%></td> 
                    </tr>
                <%} %>
            </tbody>
        </table>                
    </div>

    <script type="text/javascript">
            $(".table1").tablesorter(); 
    </script>