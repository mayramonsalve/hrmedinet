<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Medinet.Models.ViewModels.EvaluationViewModel>" %>
<%@ Import Namespace="MedinetClassLibrary.Models" %>
        <% int nro_fieldset = 1, var = 0, c_questions = 0, c_questperPage, prog;
           int categories_count = 0, c_questions_category = 0, RecordsPerPage = 0; %>
        <%  List<object[]> categoriesArray = Model.GetCategories();
            object[] quest;
            int questions = Model.GetQuestions(false).Count();
            IQueryable<Option> options = Model.GetOptions(null);
            IQueryable<Option> negative_options = Model.GetNegativeOptions();
            IQueryable<Option> used_options;
            bool opt = options.Count() > 0;%>
            <%: Html.Hidden("QuestionsCount-" + Model.QuestionnaireToUse.Id, questions, new { id = "QuestionsCount-" + Model.QuestionnaireToUse.Id })%> 
        <% if (Request.Browser.Browser == "IE" && Double.Parse(Request.Browser.Version) < 8)
           { %>
            <% RecordsPerPage = Model.GetQuestionsCount();
               prog = 50;  %>
         <%} %>
        <% else 
           {%>
            <% RecordsPerPage = Model.test.RecordsPerPage;
               prog = Model.GetProgress(); %>
         <%}
             string instructions = "<h4>" + ViewRes.Models.Questionnaire.Instructions + ": " + Model.QuestionnaireToUse.Instructions.ToString() + "</h4>";%>
             <%: Html.Hidden("QuestionnaireId", Model.QuestionnaireToUse.Id, new { id = "QuestionnaireId" })%> 
            <%: Html.Hidden("ProgressB-" + Model.QuestionnaireToUse.Id, prog, new { id = "ProgressB-" + Model.QuestionnaireToUse.Id })%> 
            <%: Html.Hidden("Instructions-" + Model.QuestionnaireToUse.Id, instructions, new { id = "Instructions-" + Model.QuestionnaireToUse.Id })%> 
        <%   c_questions = 0;
             int category_id = (int)(categoriesArray[categories_count])[0];
            List<object[]> questArrayByCategory = Model.GetQuestionsByCategory(category_id);
           
            while (c_questions < Model.GetQuestionsCount())
            {
                string classStep = "step Q-" + Model.QuestionnaireToUse.Id;
                %>
            <span class="<%: classStep %>" id="f<%:nro_fieldset%>-Q<%:Model.QuestionnaireToUse.Id%>">
            <h3 class="no_page"><%: ViewRes.Views.Evaluation.AnswerTest.Page %> <%:nro_fieldset%></h3>
            <h4 class="cat_name"><%: ViewRes.Views.Evaluation.AnswerTest.Category %>: <%:(categoriesArray[categories_count])[1].ToString()%></h4>
            <h5 class="cat_desc"><%:(categoriesArray[categories_count])[2].ToString()%></h5>
            <div>&nbsp;</div>
            <%
                c_questperPage = 0;
                while (c_questperPage < RecordsPerPage && c_questions_category < questArrayByCategory.Count)
                {
                    quest = questArrayByCategory[(RecordsPerPage * var) + (c_questions_category - (RecordsPerPage * var))];
                    if (!opt)
                        options = Model.GetOptions((int)quest[0]);
                    used_options = ((int)quest[6] == 1) ? options : negative_options;
                %>
                   <div class="column span-24 last ">
		            <h4 class="by_category"> <%: quest[1] %>  </h4>
                   </div>   				        
                        <%if ((int)quest[2] == 2)
                          {string classT = "required form-large validateTextArea" + ((int)quest[5] == 1 ? " textShort" : " textArea");
                          string styleT = "width: " + ((int)quest[5] == 1 ? "40px" : "39 %");
                              %>
                                <div class="column span-24 last">
                                    <%: Html.TextBox("q[" + quest[0] + "]", null, new { @id = "q[" + quest[0] + "]", @class = classT, @style = styleT })%>
                                    <%: (quest[4].ToString() != "") ? quest[4].ToString() : "" %>
                                </div>
                        <%}
                          else if ((int)quest[2] == 3)
                          {%> 
                              <div class="quest_option by_category column span-24 last">
                                    <%: Html.RadioButton("q[" + quest[0] + "]", 1, false, new { @id = "q[" + quest[0] + "]", @class = "required validateRadio" })%>                                  
                                    <img src="../../Content/Images/Options/Affirmative.png" alt=" " width="30" height="30" />
                                    <%: ViewRes.Views.Evaluation.AnswerTest.Yes %> 
                              </div>  
                              <div class="quest_option by_category column span-24 last">
                                    <%: Html.RadioButton("q[" + quest[0] + "]", 0, false, new { @id = "q[" + quest[0] + "]", @class = "required validateRadio" })%>                                  
                                    <img src="../../Content/Images/Options/Negative.png" alt=" " width="30" height="30" />
                                    <%: ViewRes.Views.Evaluation.AnswerTest.No %> 
                              </div>  
                        <%  }
                          else if ((int)quest[2] == 4)
                          {
                            foreach (var o in used_options)
                            {
                                string classAux = (quest[0].ToString() == "10033189") ? "required validateRadio qTipo" : "required validateRadio";
                            %>  
                                <div class="quest_option by_category column span-24 last">
                                    <%: Html.CheckBox("q[" + quest[0] + "]", false, new { @id = "q[" + quest[0] + "]", @class = classAux })%>
                                    <% if (o.Image != null && o.Image != "System.Web.HttpPostedFileWrapper")
                                       { %>
						                    <img src="../../Content/Images/Options/<%: o.Image %>" alt=" " width="30" height="30" />
                                   <% } %>                                    
                                    <%: o.Text %> 
                                </div>                              
                          <% }
                          }
                          else if ((int)quest[2] == 5)
                          {
                            foreach (KeyValuePair<int, string> type in Model.GetQuestionTypeByQuestion((int)quest[0]))
                             { %>
                                <div class="column span-24 last"><%:type.Value%></div>                                 
                               <% if (type.Key == 2)
                                {string classT = "required form-large validateTextArea" + ((int)quest[5] == 1 ? " textShort" : " textArea");
                                string styleT = "width: " + ((int)quest[5] == 1 ? "40px" : "39 %");                                      
                                      %>
                                <div class="column span-24 last">
                                    <%: Html.TextBox("q[" + quest[0] + "-" + type.Key + "]", null, new { @id = "q[" + quest[0] + "-" + type.Key + "]", @class = classT, @style = styleT })%>
                                    <%: (quest[4].ToString() != "") ? quest[4].ToString() : "" %>
                                </div>
                                <%}
                                else if (type.Key == 3)
                                {%> 
                                      <div class="quest_option by_category column span-24 last">
                                            <%: Html.RadioButton("q[" + quest[0] + "-" + type.Key + "]", 1, false, new { @id = "q[" + quest[0] + "-" + type.Key + "]", @class = "required validateRadio" })%>                                  
                                            <img src="../../Content/Images/Options/Affirmative.png" alt=" " width="30" height="30" />
                                            <%: ViewRes.Views.Evaluation.AnswerTest.Yes %> 
                                    </div>  
                                      <div class="quest_option by_category column span-24 last">
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
                                        <div class="quest_option by_category column span-24 last">
                                            <%: Html.CheckBox("q[" + quest[0] + "-" + type.Key + "]", false, new { @id = "q[" + quest[0] + "-" + type.Key + "]", @class = classAux })%>
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
                                        <div class="quest_option by_category column span-24 last">
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
                              <%}
                          }
                          else
                          {
                              foreach (var o in used_options)
                              {
                                  string classAux = (quest[0].ToString() == "10033189") ? "required validateRadio qTipo" : "required validateRadio";
                            %>  
                                <div class="quest_option by_category column span-24 last">
                                    <%= Html.RadioButton("q[" + quest[0] + "]", o.Id, false, new { @id = "q[" + quest[0] + "]", @class = "required validateRadio" })%>
                                    <% if (o.Image != null && o.Image != "System.Web.HttpPostedFileWrapper")
                                       { %>
						                    <img src="../../Content/Images/Options/<%: o.Image %>" alt=" " width="30" height="30" />
                                   <% } %>
                                    <%: o.Text%> 
                                </div>                              
                          <% }
                          }%>                                              
                <%   c_questions++;
                     c_questperPage++;
                     c_questions_category++;
                     if (c_questions_category >= questArrayByCategory.Count)
                     {
                         categories_count++;
                         if (categories_count < categoriesArray.Count)
                         {
                             category_id = (int)(categoriesArray[categories_count])[0];
                             questArrayByCategory = Model.GetQuestionsByCategory(category_id);
                             c_questions_category = 0;
                             var = 0;
                             %>
                             <div>&nbsp;</div>
                             <h4><%: ViewRes.Views.Evaluation.AnswerTest.Category %>: <%:(categoriesArray[categories_count])[1].ToString()%></h4>
                             <h5><%:(categoriesArray[categories_count])[2].ToString()%></h5>
                             <div>&nbsp;</div>
                             <%
                         }
                     }
                }%>
               
                <%

                if (c_questperPage < questArrayByCategory.Count)
                {
                    var++;
                }
            %>
            <div>&nbsp;</div>
        </span>
        <%  nro_fieldset++;
            }
           %> 
