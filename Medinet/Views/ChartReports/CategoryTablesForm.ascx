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
     <div class="span-24 columns last">
    <% if (elements_count > 0)
       {
        if (demographic != "General" || !Model.test.OneQuestionnaire)
           {%>
            <div class="span-24 column last">
                <fieldset >
                    <legend><%: ViewRes.Views.ChartReport.Graphics.Parametres%></legend>     
                    <% if (!Model.test.OneQuestionnaire)
                       { %>
                    <div class="span-24 last"><%: ViewRes.Views.ChartReport.Graphics.Questionnaire%>
                        <%: Html.DropDownList(demographic + "GroupByCategoryQuestionnairesDDL" + var_FO_id, Model.questionnaires, ViewRes.Scripts.Shared.ShowAll, new { @class = "GroupByCategoryQuestionnairesDDL form-short " })%>
                    </div>                               
                    <%} %>   
                    <% if (demographic != "General")
                       { %>                             
                    <div class="span-24 last"><%: Model.GetParameterNameByDemographic(demographic, var_FO_id)%>
                        <%: Html.DropDownList(demographic + "GroupByDemographicDDL" + var_FO_id, Model.GroupByCategories, ViewRes.Scripts.Shared.ShowAll, new { @class = "GroupByDemographicDDL form-short " })%>
                    </div>
                    <%} %>   
                </fieldset>
            </div>
        <%} %>
        <div class="clear">&nbsp;</div>

        <div id="divTable<%:demographic %><%:var_FO_id%>" class="span-14 append-1 column">
           <%if (Model.details != null && Model.details[order] != null)
             { %>
           <h4 id="h4Title<%:demographic %><%:var_FO_id%>"><%:Model.details[order].Title%></h4>
           <br />
           <%} %>
            <div style="border: 1px solid Black;">
                    <table id="<%=demographic %><%:var_FO_id%>Table" class="display tabla" >
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
                                <td class="alignRight">
                                    <%
                                  double valLabel = 0;
                                  if (Model.test.ResultBasedOn100)
                                  {
                                      valLabel = (par.Value * 100.00) / Model.test.GetOptionsByTest().Count();
                                  }
                                  else
                                      valLabel = par.Value;

                                  Response.Write(Math.Round(valLabel, 2));    
                                    %>
                                </td>
                            </tr>
                            <% } %>
                        </tbody>         
                      </table>
            </div>
        </div>

                <!--Seccion derecha-->
        <div class="span-9 column last">
<%--            <%    details = Model.graphics[order].GraphicDetails.ToList();                   
                   if (Model.UserLogged.Role.Name == "HRAdministrator")
                   {
                       %>
                       <div id="<%: demographic %><%: var_FO_id %>CommentsDiv">
                            <div class="span-24 column last button-padding-top">
                                <div class="span-3 column"><%: ViewRes.Views.ChartReport.Graphics.TableTitle%></div>
                                <div class="span-21 column last"><%: Html.TextBox(demographic + var_FO_id + "Title", Model.details[order].Title, new { @class = "input-background short" })%></div>
                            </div>
                            <div class="span-24 column last button-padding-top">
                                <div class="span-3 column"><%: ViewRes.Views.ChartReport.Graphics.AxisX%></div>
                                <div class="span-21 column last"><%: Html.TextBox(demographic + var_FO_id + "XAxis", Model.details[order].AxisXName, new { @class = "input-background short" })%></div>
                            </div>
                            <div class="span-24 column last button-padding-top">
                                <div class="span-3 column"><%: ViewRes.Views.ChartReport.Graphics.AxisY%></div>
                                <div class="span-21 column last"><%: Html.TextBox(demographic + var_FO_id + "YAxis", Model.details[order].AxisYName, new { @class = "input-background short" })%></div>
                            </div>
                            <div class="span-24 column last button-padding-top">
                                <div class="span-24 column last"><%: ViewRes.Views.ChartReport.Graphics.Comments%></div>
                                <div class="span-24 column last"><%:Html.TextArea(demographic + var_FO_id + "CommentsEditor", Model.details[order].Text, new { @class = "input-background textEditor" })%></div>
                            </div>
                        </div>
                        <div id="login-submit" class="div-button column last button-padding-top">
	                        <input id="<%: demographic %><%: var_FO_id %>Button" type="submit" value="<%: ViewRes.Views.Account.LogOn.LogOnButton %>" class="button" />
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
                   }%>--%>
           </div>
    <%}
       else
       {%>
            <%Html.RenderPartial("NoMinimum"); %>
       <%} %>    
   </div>
    <div class="clear">&nbsp;</div>
      <% if (elements_count > 0)
      {%>
<%--      <div class="clear">&nbsp;</div>
   <div class="prepend-19 span-5 column last">
        <%: Html.ActionLink(ViewRes.Views.ChartReport.Graphics.PrintGraphics + ViewRes.Views.ChartReport.Graphics.AndResult, "PdfUnivariate", new
        {
            test_id = Model.test.Id, graphic_id = id, elementsCount = elements_count,
            category_id=0, question_id=0, pValue=0.05,
            FO_id = FO_id, country_id = 0, compare_id = Model.testCompare
         }, new { id = demographic+var_FO_id+"PrintLink" })%>
   </div>--%>
    <%} %>

    <script type="text/javascript">
        InitializeDataTableById("<%=demographic %><%:var_FO_id%>Table");
    </script>