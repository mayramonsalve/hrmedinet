<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Medinet.Models.ViewModels.ChartReportViewModel>" %>
<%@ Import Namespace="MedinetClassLibrary.Services" %>
<%@ Import Namespace="MedinetClassLibrary.Models" %>
<% string demographic = ViewData["option"].ToString();
   Dictionary<string, object> dictionary;
   string var_FO_id = "";
   int FO_id=0;
   int? questionnaire = null;
   int? category = null;
   int? question = null;
   int test = Model.test.Id;
   int elements_count = 0;
   List<string> keys;
    string type = Model.chartType;
    Graphic graphic = Model.GetByDemographicAndType(demographic, "Category");
    int id = graphic.Id;
    int order = graphic.Order;
    string source = graphic.Source;
    List<GraphicDetail> details;
   %> 

    <%
           if (demographic.CompareTo("FunctionalOrganizationType") == 0)
           {
               FO_id = Int32.Parse(ViewData["FO_id"].ToString());
               var_FO_id = FO_id.ToString();
           }


           Test test1 = new TestsServices().GetById(Model.test.Id);
           int? compare = null;
           Dictionary<string, object> parameters = new Dictionary<string, object>();
            if (ViewData["optionValue"] != null)
            {
                parameters.Add("demographic", demographic);
                string idd = ViewData["optionValue"].ToString();
                if (demographic == "Gender")
                {
                    idd = idd == "1" ? "Male":"Female";
                }
                parameters.Add("id", id);
            }
            else
            {
                parameters.Add("demographic", "General");
            }
           parameters.Add("test", test1.Id);
           parameters.Add("minimumPeople", test1.MinimumPeople);
           if (test1.OneQuestionnaire)
               parameters.Add("questionnaire", test1.Questionnaire_Id);
           else
               if (ViewData["questionnaireId"] != null)
                   parameters.Add("questionnaire", ViewData["questionnaireId"]);
           int options = test1.GetOptionsByTest().Select(v => v.Value).Max();
           dictionary = new ChartsServices().GetGraphicDataForFrequencyOrCategory("Category", parameters);
           IEnumerable<KeyValuePair<string, double>> pair = (IEnumerable<KeyValuePair<string, double>>)dictionary.Values.First();
           List <KeyValuePair<string, double>> listOfValues = pair.ToList();
           elements_count = listOfValues.Count();
          

       %> 
       <%: Html.Hidden(demographic + "graphic_id" + var_FO_id, id)%>
        <%: Html.Hidden(demographic + "ElementsCount" + var_FO_id, elements_count, new { id = demographic + "ElementsCount" + var_FO_id })%>
    
    <% if (elements_count > 0)
       {
        if (demographic != "General" || !Model.test.OneQuestionnaire)
           {%>
            <strong><%: ViewRes.Views.ChartReport.Graphics.Parametres%></strong>    
            <fieldset data-role="fieldcontain">    
                <% if (!Model.test.OneQuestionnaire)
                    { %>
                    <label for="<%:demographic %>GroupByCategoryQuestionnairesDDL<%:var_FO_id%>"><%: ViewRes.Views.ChartReport.Graphics.Questionnaire%></label>
                    <%: Html.DropDownList(demographic + "GroupByCategoryQuestionnairesDDL" + var_FO_id, Model.questionnaires, ViewRes.Scripts.Shared.ShowAll, new { @class = "GroupByCategoryQuestionnairesDDL form-short " })%>
                              
                <%} %>   
                <% if (demographic != "General")
                    { %>                             
                    <label for="<%:demographic %>GroupByDemographicDDL<%:var_FO_id%>"><%: Model.GetParameterNameByDemographic(demographic, var_FO_id)%></label>
                    <%: Html.DropDownList(demographic + "GroupByDemographicDDL" + var_FO_id, Model.GroupByCategories, ViewRes.Scripts.Shared.ShowAll, new { @class = "GroupByDemographicDDL form-short " })%>

                <%} %>   
            </fieldset>
        <%} %>

        <div class="principal" >
            <div class="chartDiv portrait">
                    <table id="<%=demographic %><%:var_FO_id%>Table" class="table1" >
                        <thead>
                            <tr>
                                <th><%: ViewRes.Views.ChartReport.Graphics.Category%></th>
                                <th><%: ViewRes.Views.ChartReport.Graphics.Climate%></th>
                            </tr>
                        </thead>
                        <tbody>
                            <%foreach (KeyValuePair<string, double> par in listOfValues)
                              { %>
                            <tr>
                                <td><%: par.Key%></td>
                                <td><%: par.Value%></td>
                            </tr>
                            <% } %>
                        </tbody>          
                      </table>
            </div>
<%--            <div class="generalDiv">
                <%    details = Model.graphics[order].GraphicDetails.ToList();                   
                       if (Model.UserLogged.Role.Name == "HRAdministrator")
                       {
                           %>
                           <div id="<%: demographic %><%: var_FO_id %>CommentsDiv">
                                <div data-role="fieldcontain">
                                    <label for="<%:demographic %><%:var_FO_id %>Title"><%: ViewRes.Views.ChartReport.Graphics.TableTitle%></label>
                                    <%: Html.TextBox(demographic + var_FO_id + "Title", Model.details[order].Title, new { @class = "input-background short" })%>
                                    <label for="<%:demographic %><%:var_FO_id %>XAxis"><%: ViewRes.Views.ChartReport.Graphics.AxisX%></label>
                                    <%: Html.TextBox(demographic + var_FO_id + "XAxis", Model.details[order].AxisXName, new { @class = "input-background short" })%>
                                    <label for="<%:demographic %><%:var_FO_id %>YAxis"><%: ViewRes.Views.ChartReport.Graphics.AxisY%></label>
                                    <%: Html.TextBox(demographic + var_FO_id + "YAxis", Model.details[order].AxisYName, new { @class = "input-background short" })%>
                                    <label for="<%:demographic %><%:var_FO_id %>CommentsEditor"><%: ViewRes.Views.ChartReport.Graphics.Comments%></label>
                                    <%:Html.TextArea(demographic + var_FO_id + "CommentsEditor", Model.details[order].Text, new { @class = "input-background textEditor" })%>
                                </div>
                                <input data-role="button" data-theme="f" id="<%: demographic %><%: var_FO_id %>Button" type="submit" value="<%: ViewRes.Views.Account.LogOn.LogOnButton %>" class="button" />
                            </div>
                           
                       <%}
                       else
                       {
                       //MOSTRAR COMENTARIOS
                           if (details.Count() > 0)
                           {%>
                            <div><strong><<%: ViewRes.Views.ChartReport.Graphics.Comments%></strong></div>
                            <div><%:Model.details[order].Text%></div>
                         <%}
                       }%>
               </div>--%>
        </div>
    <%}
       else
       {%>
            <%Html.RenderPartial("NoMinimum"); %>
       <%} %>    
    <script type="text/javascript">
        $(".table1").tablesorter(); 
    </script>