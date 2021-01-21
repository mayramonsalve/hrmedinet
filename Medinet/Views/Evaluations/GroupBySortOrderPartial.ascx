<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Medinet.Models.ViewModels.EvaluationViewModel>" %>
<%@ Import Namespace="MedinetClassLibrary.Models" %>
<% int nro_f = 1, nro_rpp, cont_q, questperPage, prog;%>
        <% if (Request.Browser.Browser == "IE" && Double.Parse(Request.Browser.Version) < 8)
           { %>
            <% nro_rpp = Model.GetQuestionsCount();
               prog = 50; %>
         <%} %>
        <% else 
           {%>
            <% nro_rpp = Model.test.RecordsPerPage;
               prog = Model.GetProgress(); %>
         <%} %>
        <% List<object[]> questions = Model.GetQuestions(Model.test.Disordered);
           object[] quest; 
           IQueryable<Option> options = Model.GetOptions(null);
           IQueryable<Option> negative_options = Model.GetNegativeOptions();
            IQueryable<Option> used_options;
           bool opt = options.Count() > 0;
           string instructions = "<h4>" + ViewRes.Models.Questionnaire.Instructions + ": " + Model.QuestionnaireToUse.Instructions.ToString() + "</h4>";
            %>
            <%: Html.Hidden("QuestionnaireId", Model.QuestionnaireToUse.Id, new { id = "QuestionnaireId" })%> 
                <%: Html.Hidden("QuestionsCount-" + Model.QuestionnaireToUse.Id, questions.Count(), new { id = "QuestionsCount-" + Model.QuestionnaireToUse.Id })%> 
                <%: Html.Hidden("ProgressB-" + Model.QuestionnaireToUse.Id, prog, new { id = "ProgressB-" + Model.QuestionnaireToUse.Id })%>         
                <%: Html.Hidden("Instructions-" + Model.QuestionnaireToUse.Id, instructions, new { id = "Instructions-" + Model.QuestionnaireToUse.Id })%>         
        <%  cont_q = 0;
            while (cont_q < questions.Count)
            {
                string classStep = "step Q-" + Model.QuestionnaireToUse.Id;
                %>
            <span class="<%: classStep %>" id="f<%:nro_f%>-Q<%:Model.QuestionnaireToUse.Id%>">
                <h3 class="no_page"><%: ViewRes.Views.Evaluation.AnswerTest.Page %> <%:nro_f%></h3>
                <div>&nbsp;</div>
                    <%
                    questperPage = 0;
                    while (questperPage < nro_rpp && cont_q < questions.Count)
                    {
                        quest = questions[((nro_f-1)*nro_rpp)+questperPage]; // pregunta actual siendo recorrida
                        if(!opt)
                            options = Model.GetOptions((int)quest[0]);
                        used_options = ((int)quest[6] == 1) ? options : negative_options;
                        %>
                            <div class="column span-24 last">
                                <h4> <%: quest[1] %></h4>
                            </div>
                        <%if ((int)quest[2] == 2)
                          {
                              string classT = "required form-large validateTextArea" + ((int)quest[5] == 1 ? " textShort" : " textArea");
                              string styleT = "width: " + ((int)quest[5] == 1 ? "40px" : "39 %");
                              %>
                                <div class="column span-24 last">
                                    <%: Html.TextBox("q[" + quest[0] + "]", null, new { @id = "q[" + quest[0] + "]", @class = classT, @style = styleT })%>
                                    <%: (quest[4].ToString() != "") ? quest[4].ToString() : "" %>
                                </div>
                        <%}
                          else if ((int)quest[2] == 3)
                          {%> 
                              <div class="quest_option column span-24 last">
                                    <%: Html.RadioButton("q[" + quest[0] + "]", 1, false, new { @id = "q[" + quest[0] + "]", @class = "required validateRadio" })%>                                  
                                    <img src="../../Content/Images/Options/Affirmative.png" alt=" " width="30" height="30" />
                                    <%: ViewRes.Views.Evaluation.AnswerTest.Yes %> 
                              </div>  
                              <div class="quest_option column span-24 last">
                                    <%: Html.RadioButton("q[" + quest[0] + "]", 0, false, new { @id = "q[" + quest[0] + "]", @class = "required validateRadio" })%>                                  
                                    <img src="../../Content/Images/Options/Negative.png" alt=" " width="30" height="30" />
                                    <%: ViewRes.Views.Evaluation.AnswerTest.No %> 
                              </div>  
                        <%  }
                              else if ((int)quest[2] == 5)
                          {%> 
                          <% foreach (KeyValuePair<int, string> type in Model.GetQuestionTypeByQuestion((int)quest[0]))
                             { %>
                                <div class="column span-24 last"><%:type.Value%></div>                                 
                               <% if (type.Key == 2)
                                {
                                    string classT = "required form-large validateTextArea" + ((int)quest[5] == 1 ? " textShort" : " textArea");
                                    string styleT = "width: " + ((int)quest[5] == 1 ? "40px" : "39 %");                                      
                                      %>
                                <div class="column span-24 last">
                                    <%: Html.TextBox("q[" + quest[0] + "-" + type.Key + "]", null, new { @id = "q[" + quest[0] + "-" + type.Key + "]", @class = classT, @style = styleT })%>
                                    <%: (quest[4].ToString() != "") ? quest[4].ToString() : "" %>
                                </div>
                                <%}
                                else if (type.Key == 3)
                                {%> 
                                      <div class="quest_option column span-24 last">
                                            <%: Html.RadioButton("q[" + quest[0] + "-" + type.Key + "]", 1, false, new { @id = "q[" + quest[0] + "-" + type.Key + "]", @class = "required validateRadio" })%>                                  
                                            <img src="../../Content/Images/Options/Affirmative.png" alt=" " width="30" height="30" />
                                            <%: ViewRes.Views.Evaluation.AnswerTest.Yes %> 
                                    </div>  
                                      <div class="quest_option column span-24 last">
                                            <%: Html.RadioButton("q[" + quest[0] + "-" + type.Key + "]", 0, false, new { @id = "q[" + quest[0] + "-" + type.Key + "]", @class = "required validateRadio" })%>                                  
                                            <img src="../../Content/Images/Options/Negative.png" alt=" " width="30" height="30" />
                                            <%: ViewRes.Views.Evaluation.AnswerTest.No %> 
                                      </div>  
                                <%  }
                                    else if(type.Key == 4)
                                    {
                                        foreach (var o in used_options)
                                        {
                                            string classAux = (quest[0].ToString() == "10033189") ? "required validateRadio qTipo" : "required validateRadio";
                                    %>  
                                        <div class="quest_option column span-24 last">
                                            <%: Html.CheckBox("q[" + quest[0] + "-" + type.Key + "]", false, new { @value = o.Id,  @id = "q[" + quest[0] + "-" + type.Key + "]", @class = classAux })%>
                                            <% if (o.Image != null && o.Image != "System.Web.HttpPostedFileWrapper")
                                               { %>
						                            <img src="../../Content/Images/Options/<%: o.Image %>" alt=" " width="30" height="30" />
                                           <% } %>                                    
                                            <%: o.Text %> 
                                        </div>                              
                                  <% }
                                }
                                  else
                                  {
                                    foreach (var o in used_options)
                                    {
                                        string classAux = (quest[0].ToString() == "10033189") ? "required validateRadio qTipo" : "required validateRadio";
                                    %>  
                                        <div class="quest_option column span-24 last">
                                            <%: Html.RadioButton("q[" + quest[0] + "-" + type.Key + "]", o.Id, false, new { @id = "q[" + quest[0] + "-" + type.Key + "]", @class = classAux })%>
                                            <% if (o.Image != null && o.Image != "System.Web.HttpPostedFileWrapper")
                                               { %>
						                            <img src="../../Content/Images/Options/<%: o.Image %>" alt=" " width="30" height="30" />
                                           <% } %>                                    
                                            <%: o.Text %> 
                                        </div>                              
                                <% }
                                  } %> 
                                <br />
                              <%} %>
                        <% }
                          else if((int)quest[2] == 4)
                          {
                                string classAux = (quest[0].ToString() == "10033189") ? "required validateRadio qTipo" : "required validateRadio";                         
                            
                            foreach (var o in used_options)
                            {
                            %>
                                <div class="quest_option column span-24 last">
                                    <%: Html.CheckBox("q[" + quest[0] + "]", false, new { @value = o.Id, @id = "q[" + quest[0] + "]", @class = classAux })%>
                                    <% if (o.Image != null && o.Image != "System.Web.HttpPostedFileWrapper")
                                       { %>
						                    <img src="../../Content/Images/Options/<%: o.Image %>" alt=" " width="30" height="30" />
                                   <% } %>                                    
                                    <%: o.Text %> 
                                </div>                              
                          <% }
                          }
                          else
                          {
                            foreach (var o in used_options)
                            {
                                string classAux = (quest[0].ToString() == "10033189") ? "required validateRadio qTipo" : "required validateRadio";
                            %>  
                                <div class="quest_option column span-24 last">
                                    <%: Html.RadioButton("q[" + quest[0] + "]", o.Id, false, new { @id = "q[" + quest[0] + "]", @class = classAux })%>
                                    <% if (o.Image != null && o.Image != "System.Web.HttpPostedFileWrapper")
                                       { %>
						                    <img src="../../Content/Images/Options/<%: o.Image %>" alt=" " width="30" height="30" />
                                   <% } %>                                    
                                    <%: o.Text %> 
                                </div>                              
                          <% }
                          } %>   
                    <%     
                        questperPage++;
                        cont_q++;
                    }
                    %>
                    <div>&nbsp;</div>
                </span>
            <%
                nro_f++;
                }  %>