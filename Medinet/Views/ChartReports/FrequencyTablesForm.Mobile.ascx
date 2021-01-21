<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Medinet.Models.ViewModels.ChartReportViewModel>" %>
<%@ Import Namespace="MedinetClassLibrary.CustomClasses" %>
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
       Graphic graphic = Model.GetByDemographicAndType(demographic, "Frequency");
       int id = graphic.Id;
       int order = graphic.Order;
       string source = graphic.Source;
   %> 

   <div><%
           if (demographic.CompareTo("FunctionalOrganizationType") == 0)
           {
               FO_id = Int32.Parse(ViewData["FO_id"].ToString());
               var_FO_id = FO_id.ToString();
           }
           dictionary = Model.GetFrequencyCategory(test, "Frequency", demographic, questionnaire, category, question, FO_id, null);
           keys = dictionary.Keys.ToList();
           elements_count = keys.Count;
       %> 
       <%: Html.Hidden(demographic + "graphic_id" + var_FO_id, id)%>
          <%: Html.Hidden(demographic + "ElementsCount" + var_FO_id, elements_count, new { id = demographic + "ElementsCount" + var_FO_id })%>
    <% if (elements_count > 0)
       {%>
    <div>   
            <strong><%: ViewRes.Views.ChartReport.Graphics.Parametres%></strong>
            <fieldset data-role="fieldcontain">
            <% if (!Model.test.OneQuestionnaire)
                { %>
                <label for="<%: demographic %>GroupByQuestionnairesDDL<%: var_FO_id %>" ><%: ViewRes.Views.ChartReport.Graphics.Questionnaire%></label> 
                <%: Html.DropDownList(demographic + "GroupByQuestionnairesDDL" + var_FO_id, Model.questionnaires, ViewRes.Scripts.Shared.ShowAll, new { @class = "GroupByQuestionnairesDDL form-short " })%>                          
            <%} %>
                <label for="<%: demographic %>GroupByCategoriesDDL<%: var_FO_id %>" ><%: ViewRes.Views.ChartReport.Graphics.Category%></label>
                <%: Html.DropDownList(demographic + "GroupByCategoriesDDL" + var_FO_id, Model.GroupByCategories, ViewRes.Scripts.Shared.ShowAll, new { @class = "GroupByCategoriesDDL form-short " })%>
                <label for="<%: demographic %>GroupByQuestionsDDL<%: var_FO_id %>" ><%: ViewRes.Views.ChartReport.Graphics.Question%></label>
                <%: Html.DropDownList(demographic + "GroupByQuestionsDDL" + var_FO_id, Model.question, ViewRes.Scripts.Shared.ShowAll, new { @class = "GroupByQuestionsDDL form-short" })%>
            </fieldset>     
    </div>

    <div id="divTable<%:demographic %><%:var_FO_id%>" >
<%--        <%if (Model.details[order] != null)
            { %>
        <h4 id="h4Title<%:demographic %><%:var_FO_id%>"><%:Model.details[order].Title%></h4>
        <br />
        <%} %>--%>
        <br />
    <table data-role="table" data-column-btn-text="<%: ViewRes.Views.ChartReport.Graphics.Columns %>" id="<%=demographic %><%:var_FO_id%>Table" data-mode="columntoggle" class="table1 ui-responsive table-stroke" data-column-btn-theme="f">
        <thead>
            <tr>
                <th></th>
                <%foreach (string key in keys)
                    {%>
                <th data-priority="1"><%: key%></th>
                <%} %>
            </tr>
        </thead>
        <tbody>
            <% Dictionary<string, double> aux = (Dictionary<string, double>)dictionary.Values.FirstOrDefault();
                keys = aux.Keys.ToList();
            foreach (string key in keys)
                { %>
            <tr>
                <td><%: key %></td> 
                <%foreach (Dictionary<string, double> dic in dictionary.Values)
                    { %>
                <td><%: dic.ContainsKey(key) ? dic[key] : 0 %></td>
                <%} %>
            </tr>
            <%} %>
        </tbody>          
    </table>
    </div>
    <%}
       else
       {%>
            <%Html.RenderPartial("NoMinimum"); %>
       <%} %>    
   </div>

    <script type="text/javascript">
        $(".table1").tablesorter();
    </script>