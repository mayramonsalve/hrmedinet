<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Medinet.Models.ViewModels.ChartReportViewModel>" %>
<%@ Import Namespace="MedinetClassLibrary.CustomClasses" %>
<%@ Import Namespace="MedinetClassLibrary.Models" %>
<% string demographic = ViewData["option"].ToString();
   Dictionary<string, object> dictionary = new Dictionary<string,object>();
   string var_FO_id = "";
   int FO_id=0;
   int? questionnaire = null;
   int? category = null;
   int? question = null;
   int test = Model.test.Id;
   int elements_count = 0;
   List<string> keys = new List<string>();//esto es paraa la cabecera
       string type = Model.chartType;
       Graphic graphic = Model.GetByDemographicAndType(demographic, "Frequency");
       int id = graphic.Id;
       int order = graphic.Order;
       string source = graphic.Source;
       int options=0;

       if (Model.test.OneQuestionnaire)
       {
           options = Model.test.Questionnaire.Options.Count;
       }
       else {
           try
           {
               options = Model.test.GetOptionsByTest().Count();//esto es para saber si las opciones estan asociados al cuestionario, porque si estan las opciones asociadas a la pregunta, se necesita que el usuario escoja una pregunta
           }
           catch { 
           }
       }
   %> 

   <div class="span-24 columns last"><%
           if (demographic.CompareTo("FunctionalOrganizationType") == 0)
           {
               FO_id = Int32.Parse(ViewData["FO_id"].ToString());
               var_FO_id = FO_id.ToString();
           }
           
           if (options > 0)
           {
               dictionary = Model.GetFrequencyCategory(test, "Frequency", demographic, questionnaire, category, question, FO_id, null);
               keys = dictionary.Keys.ToList();
               elements_count = keys.Count;
           }
       %> 
       <%: Html.Hidden(demographic + "graphic_id" + var_FO_id, id)%>
          <%: Html.Hidden(demographic + "ElementsCount" + var_FO_id, elements_count, new { id = demographic + "ElementsCount" + var_FO_id })%>
    <% if (elements_count > 0 || options == 0)// si options == 0, debe ocultar la tabla q se esta creando y mostrar un cuadro que diga que debe seleccionar pregunta
       {
           //cuando las opciones esten asociadas a las preguntas y la pregunta sea escogida(la tabla actualizada) se debe ocultar el div que decia que se debia seleccionar pregunta,debido a que ya se selecciono
           //cuando seleccionen nula en el dropdown de pregunta o cuando seleccionan otra categoria se debe ocultar la tabla y mostrar el div
           //OJO si es una medicion de las opciones asociadas a las preguntas no al cuestionario 
     %>
        <div class="span-24 column last">
            <fieldset >
                <legend><b><%: ViewRes.Views.ChartReport.Graphics.Parametres %></b></legend>
                <% if (!Model.test.OneQuestionnaire)
                   { %>
                <div class="span-24 last"><%: ViewRes.Views.ChartReport.Graphics.Questionnaire %>
                    <%: Html.DropDownList(demographic + "GroupByQuestionnairesDDL" + var_FO_id, Model.questionnaires, ViewRes.Scripts.Shared.ShowAll, new { @class = "GroupByQuestionnairesDDL form-short " })%>
                </div>
                <%} %>
                <div class="span-24 last"><%: ViewRes.Views.ChartReport.Graphics.Category %>
                    <%: Html.DropDownList(demographic + "GroupByCategoriesDDL" + var_FO_id, Model.GroupByCategories, ViewRes.Scripts.Shared.ShowAll, new { @class = "GroupByCategoriesDDL form-short " })%>
                </div>
                <div class="span-24 last"><%: ViewRes.Views.ChartReport.Graphics.Question %>
                    <%: Html.DropDownList(demographic + "GroupByQuestionsDDL" + var_FO_id, Model.question, ViewRes.Scripts.Shared.ShowAll, new { @class = "GroupByQuestionsDDL form-short" })%>
                </div>
            </fieldset>
        </div>
        <div class="clear">&nbsp;</div>
        <%if (options == 0)
              { %>
                <%--<div class="span-24 column last alignCenter"><h4>Debe seleccionar una categoría, y luego una pregunta.</h4></div>--%>
                    <div id="divTable" class="span-20 prepend-2 append-2 column last">
                   <%if (Model.details != null && Model.details[order] != null)
                     { %>
                   <h4 id="h4Title<%:demographic %><%:var_FO_id%>"><%:Model.details[order].Title%></h4>
                    <br />
                   <%} %>
                    <div style="border: 1px solid Black;">
                            <table id="<%=demographic %><%:var_FO_id%>Table" class="display tabla" >
                                <thead>
                                    <tr>
                                        <th></th>
                                    </tr>
                                </thead>
                                <tbody>
                                    <tr>
                                        <td style="color:Black;"></td> 
                                   </tr>
                                </tbody>          
                              </table>
                    </div>
                </div>
              <%}
              else
              {%>
                    <div id="divTable<%:demographic %><%:var_FO_id%>" class="span-20 prepend-2 append-2 column last">
                   <%if (Model.details != null && Model.details[order] != null)
                     { %>
                   <h4 id="h4Title<%:demographic %><%:var_FO_id%>"><%:Model.details[order].Title%></h4>
                    <br />
                   <%} %>
                    <div style="border: 1px solid Black;">
                            <table id="<%=demographic %><%:var_FO_id%>Table" class="display tabla" >
                                <thead>
                                    <tr>
                                        <th></th>
                                        <%foreach (string key in keys)//keys como cabecera.por ejemplo: muy de acuerdo
                                          {%>
                                        <th class="alignCenter"><%: key%></th>
                                        <%} %>
                                    </tr>
                                </thead>
                                <tbody>
                                   <% Dictionary<string, double> aux = (Dictionary<string, double>)dictionary.Values.FirstOrDefault();//aqui toma menor a 18,2000.etc
                                      keys = aux.Keys.ToList();//ejemplo: menor a 18
                                    foreach (string key in keys)//estas keys son mis demograficos
                                     { %>
                                    <tr>
                                        <td style="color:Black;"><%: key %></td> 
                                        <%foreach (Dictionary<string, double> dic in dictionary.Values)//recorriendo uno por uno para obtenere el 2000 por ejemplo y el 1500 etc
                                          { %>
                                            <td class='alignCenter'><%: dic.ContainsKey(key) ? dic[key] : 0 %></td>
                                        <%} %>
                                   </tr>
                                    <%} %>
                                </tbody>          
                              </table>
                    </div>
                </div>
            <%} %>
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
        InitializeDataTable();
    </script>