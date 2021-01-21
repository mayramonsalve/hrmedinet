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
   <div class="span-24 columns last">
   <%  totalList = Model.NameAllTestsSastNoSast(null, null, null, test); %> 
        <div class="span-24 column last">
            <fieldset >
                <legend><b><%: ViewRes.Views.ChartReport.Graphics.Parametres%></b></legend>
                <% if (!Model.test.OneQuestionnaire)
                   { %>
                <div class="span-24 last"><%: ViewRes.Views.ChartReport.Graphics.Questionnaire%>
                    <%: Html.DropDownList("AllTestsGroupByQuestionnairesDDL", Model.questionnaires, ViewRes.Scripts.Shared.ShowAll, new { @class = "GroupByQuestionnairesDDL form-short " })%>
                </div>                               
                <%} %>
                <div class="span-24 last"><%: ViewRes.Views.ChartReport.Graphics.Category%>
                    <%: Html.DropDownList("AllTestsGroupByCategoriesDDL", Model.GroupByCategories, ViewRes.Scripts.Shared.ShowAll, new { @class = "GroupByCategoriesDDL form-short " })%>
                </div>
                <div class="span-24 last"><%: ViewRes.Views.ChartReport.Graphics.Question%>
                    <%: Html.DropDownList("AllTestsGroupByQuestionsDDL", Model.question, ViewRes.Scripts.Shared.ShowAll, new { @class = "GroupByQuestionsDDL form-short" })%>
                </div>
            </fieldset>
        </div>
        <div class="clear">&nbsp;</div>
        <div class="span-15 column">
            <img id="AllTestsChart" name="AllTestsChart" src="<%:source%>&graphic_id=<%:id%>&test_id=<%:test%>" alt="Grafico" align="center"/>
        </div>
        <!--Seccion derecha-->
        <div class="span-9 column last" style="margin-top: 399px;">
            <%    List<GraphicDetail> details = Model.graphics[order].GraphicDetails.ToList();                   
                   if (Model.UserLogged.Role.Name == "HRAdministrator")
                   {
                       %>
                       <div id="AllTestsCommentsDiv">
                            <div class="span-24 column last button-padding-top">
                                <div class="span-3 column"><%: ViewRes.Views.ChartReport.Graphics.TableTitle%></div>
                                <div class="span-21 column last"><%: Html.TextBox("AllTestsTitle", Model.details[order].Title, new { @class = "input-background short" })%></div>
                            </div>
                            <div class="span-24 column last button-padding-top">
                                <div class="span-3 column"><%: ViewRes.Views.ChartReport.Graphics.AxisX%></div>
                                <div class="span-21 column last"><%: Html.TextBox("AllTestsXAxis", Model.details[order].AxisXName, new { @class = "input-background short" })%></div>
                            </div>
                            <div class="span-24 column last button-padding-top">
                                <div class="span-3 column"><%: ViewRes.Views.ChartReport.Graphics.AxisY%></div>
                                <div class="span-21 column last"><%: Html.TextBox("AllTestsYAxis", Model.details[order].AxisYName, new { @class = "input-background short" })%></div>
                            </div>
                            <div class="span-24 column last button-padding-top">
                                <div class="span-24 column last"><%: ViewRes.Views.ChartReport.Graphics.Comments%></div>
                                <div class="span-24 column last"><%:Html.TextArea("AllTestsCommentsEditor", Model.details[order].Text, new { @class = "input-background textEditor" })%></div>
                            </div>
                        </div>
                        <div id="login-submit" class="div-button column last button-padding-top">
	                        <input id="AllTestsButton" type="submit" value="<%: ViewRes.Views.Account.LogOn.LogOnButton %>" class="button" />
                        </div>
                   <%}
                   else
                   {
                   //MOSTRAR COMENTARIOS
                       if (details.Count() > 0)
                       {%>
                        <div><%: ViewRes.Views.ChartReport.Graphics.Comments%></div>
                        <div><%:Model.details[order].Text%></div>
                     <%}
                   }%>

        </div>
        <div class="clear">&nbsp;</div>
        <div class="span-24 column last">
            <div id="AllTestsDivSat" class="span-14 column last">
                <fieldset style="margin-top: -400px;">
                    <table id="AllTestsTable" class="display">
                    <thead>
                      <tr>
                        <th></th>
                        <th align="center"><%: ViewRes.Views.ChartReport.Graphics.Satisfied%></th>
                        <th align="center"><%: ViewRes.Views.ChartReport.Graphics.NoSatisfied%></th>
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
                </fieldset>
            </div>
       </div>
   </div>
<%--   <div class="clear">&nbsp;</div>
   <div class="prepend-19 span-5 column last">
        <%: Html.ActionLink(ViewRes.Views.ChartReport.Graphics.PrintGraphics + ViewRes.Views.ChartReport.Graphics.AndResult, "PdfUnivariate", new
        {
            test_id = Model.test.Id,
            category_id=0, question_id=0,
         }, new { id = "AllTestsPrintLink" })%>
   </div>--%>