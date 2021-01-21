<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Medinet.Models.ViewModels.EvaluationViewModel>" %>
<%@ Import Namespace="MedinetClassLibrary.Models" %>
<% int nro_f = 1, nro_rpp = 5, cont_q, questperPage, totalPages, prog;%>

    <% 
        List<object[]> questions = Model.GetQuestions(Model.test.Disordered);
        object[] quest; 
        IQueryable<Option> options = Model.GetOptions(null);
        IQueryable<Option> negative_options = Model.GetNegativeOptions();
        IQueryable<Option> used_options;
        totalPages = questions.Count / nro_rpp;
        if (questions.Count % nro_rpp != 0)
        {
            totalPages++;
        }
    %>

    <%  cont_q = 0;   
        while (cont_q < questions.Count)
        {
            %>
            <script type="text/javascript">
                $(document).on("pageinit", "#step<%:nro_f %>", function () {
                    $("#formStep<%:nro_f %>").validate({
                        errorPlacement: function (error, element) {
                            if (element.is(":radio")) {
                                var str = element.attr("name");
                                str = str.substring(2, str.length - 1);
                                error.insertAfter($("#" + str));
                            }
                            else {
                                error.insertAfter($(element).parent());
                            }
                        }
                    });
                });
            </script>
            <div data-role="page" id="step<%:nro_f%>" class="basic">
                <% Html.RenderPartial("mobile/MobileHeaderMini"); %> 
                <% Html.RenderPartial("mobile/MobileMenu"); %> 
                <% Html.RenderPartial("mobile/MobileSubMenu"); %>

                <div data-role="content" class="content basic">
             
                <form action="/" method="post" class="msform" id="formStep<%:nro_f %>" data-ajax="false" >
                    <div>
                       <h1 class ="testname"><%= Model.test.Name%></h1> 
                    </div>
                    <div class="box rounded txt">
                    <h1><%: ViewRes.Views.Evaluation.AnswerTest.Page %> <%:nro_f%></h1>
                        <%
                        questperPage = 0;
                        while (questperPage < nro_rpp && cont_q < questions.Count)
                        {
                            quest = questions[((nro_f-1)*nro_rpp)+questperPage];
                            used_options = ((int)quest[6] == 1) ? options : negative_options;
                            %>
                            <div>
                                <h4> <%: quest[1] %></h4>
                            </div>

                            <%if ((int)quest[2] == 2){
                                    string clas = "";
                                    if (quest[0].ToString() == "10033739"){
                                        clas = "email";
                                    }
                                %>
                                    <div>
                                        <fieldset data-rol="fieldcontain">
                                            <textarea class="required <%:clas %>" name="q[<%:quest[0]%>]" id="<%:quest[0]%>"></textarea>
                                        </fieldset>
                                    </div><br />
                                    <% clas = ""; %>
                            <%}
                                else if ((int)quest[2] == 3)
                                {%> 

                                    <div>
                                        <fieldset data-role="controlgroup" data-type="horizontal">
    	                                    <legend></legend>
                                            <%: Html.RadioButton("q[" + quest[0] + "]", 1, false, new {data_mini="true", @class="required" , @id = quest[0]+"1" })%>
                                            <label for="<%:quest[0] %>1">
    				                            <img src="../../Content/Images/Options/Affirmative.png" alt=" " class="quest_option" width="20" height="20" /> 
                                                <%: ViewRes.Views.Evaluation.AnswerTest.Yes%>
                                            </label>

                                            <%: Html.RadioButton("q[" + quest[0] + "]", 0, false, new { data_mini = "true", @class = "required", @id = quest[0] + "2" })%>
                                            <label for="<%:quest[0] %>2">
						                        <img src="../../Content/Images/Options/Negative.png" alt=" " class="quest_option" width="20" height="20" />
                                                <%: ViewRes.Views.Evaluation.AnswerTest.No%></label>  
                                            <div id="<%:quest[0]%>">
                                            </div>
                                        </fieldset>
                                    </div>


                            <%  }
                                else
                                {
                                    String clase = "ui-block-a";
                                    int medidor = 0;
                                    %>
                                    <fieldset class="ui-grid-a">
                                    <legend></legend>
                                    <%
                                    foreach (var o in used_options)
                                    {
                                        if (medidor % 2 != 0)
                                            clase = "ui-block-b";
                                        string classAux = (quest[0].ToString() == "10033189") ? "required validateRadio qTipo" : "required validateRadio";
                                    %>  
                                        <div class="<%:clase%>">
                                            <%:Html.RadioButton("q[" + quest[0] + "]", o.Id, false, new {  data_mini="true", @class="required" , @id = o.Id})%>                         
                                            <label for="<%: o.Id %>">
                                               <% if (o.Image != null && o.Image != "System.Web.HttpPostedFileWrapper")
                                               { %>
						                            <img src="../../Content/Images/Options/<%: o.Image %>" alt=" " class="quest_option" width="20" height="20" />
                                                <% } %>  
                                                <%: o.Text %>
                                            </label>
                                        </div>                              
                                    <% 
                                        clase = "ui-block-a";
                                        medidor++;
                                    }%>
                                    <div id="<%:quest[0] %>">
                                    </div>
                                    </fieldset>
                                <%
                            } %>   
                        <%     
                            questperPage++;
                            cont_q++;
                        }
                        %>
                    <h1></h1>
                    <fieldset class="ui-grid-a">
                        <legend></legend>
	                    <div class="ui-block-a"><a href="#" data-icon="arrow-l" data-rel="back" data-role="button" data-theme="f"><%: ViewRes.Views.Evaluation.AnswerTest.ButtonBack %></a>		</div>
	                    <%
                        if (nro_f == totalPages)
                        {
                            %>
                            <input type="hidden" name="nextStep" value="step.final">
                            <% Html.RenderPartial("Hidden.Mobile"); %>
                            <div class="ui-block-b"><input data-icon="arrow-r" data-iconpos="right" data-role="button" name="button" data-theme="f" value="<%: ViewRes.Views.Evaluation.AnswerTest.ButtonSubmit %>" type="submit" /></div>	   
                            <%
                        }
                        else
                        {
                            %>
                            <input type="hidden" name="nextStep" value="step<%: nro_f+1 %>">
                            <div class="ui-block-b"><input data-icon="arrow-r" data-iconpos="right" data-role="button" name="button" data-theme="f" value="<%: ViewRes.Views.Evaluation.AnswerTest.ButtonNext %>" type="submit" /></div>	   
                            <%
                        }
                        %>
                        

                    </fieldset>
                    <div style="text-align:center;"> 			
                        <h3><%: ViewRes.Views.Evaluation.AnswerTest.Page %> <%: nro_f %> <%: ViewRes.Views.Evaluation.AnswerTest.Of %> <%: totalPages %> </h3>
                    </div>
                    </div>
                </form>
                </div>

                <% Html.RenderPartial("mobile/MobileFooter"); %> 
            </div>
        <%
            nro_f++;
            }  %>

  