<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Medinet.Models.ViewModels.TestViewModel>" %>

            <div class="span-24 last"> 
                <%: Html.Hidden("ValidateInt", ViewRes.Views.Test.Create.ValidateInt, new { id = "ValidateInt" })%> 
                <%: Html.Hidden("Cancel", ViewRes.Views.Shared.Shared.CancelLabel, new { id = "Cancel" })%>
                <%: Html.HiddenFor(model => model.test.ConfidenceLevel_Id)%>
                <%: Html.HiddenFor(model => model.test.StandardError_Id)%>
                <%: Html.HiddenFor(model => model.test.NumberOfEmployees)%>
                <%: Html.Hidden("NoData", ViewRes.Views.Test.Create.NoData, new { id = "NoData" })%>
                <% if (Model.test != null)
                    { %>
                        <%: Html.Hidden("Test", Model.test.Id, new { id = "Test" })%>
                    <%} %>
                <%if (Model.userRole == "HRAdministrator")
                  {%>
                    <%: Html.Hidden("Select", ViewRes.Scripts.Shared.Select, new { id = "ViewRes" })%>
                    <div class="span-24 last"><h4><%: Html.LabelFor(model => model.test.Company_Id)%></h4></div>
                    <div class="span-24 last"><%: Html.DropDownListFor(model => model.test.Company_Id, Model.companiesList, ViewRes.Scripts.Shared.Select, new { @class = "required input-background short" })%></div>
                    <div><%: Html.ValidationMessageFor(model => model.test.Company_Id)%></div>
                <% }
                  else if (Model.userRole == "HRCompany")
                  {
                      %>
                      <%: Html.Hidden("test_Company_Id",Model.test.Company_Id)%>
                      <%
                  }%>           
                     
                    <div class="span-24 last"><h4><%: Html.LabelFor(model => model.test.PreviousTest_Id)%></h4></div>
                    <div class="span-24 last"><%: Html.DropDownListFor(model => model.test.PreviousTest_Id, Model.testsList, ViewRes.Scripts.Shared.Select, new { @class = "input-background short" })%></div>
                    <div><%: Html.ValidationMessageFor(model => model.test.PreviousTest_Id)%></div>

                    <div id="DivDemographicsInTest" class="span-24 last">
                    <div class="span-24 last"><h4><%: ViewRes.Views.Test.Create.DemographicsInTest %></h4></div>
                    <div class="span-24 last"><%: Html.DropDownList("Demographics", Model.demographicsList, new { @multiple = "multiple", @class = "input-background short multiselect" })%></div>
                    <%--<div><%: Html.ValidationMessageFor(model => model.test.Company_Id)%></div>--%>
                    </div>
                <div id="DivOneQuestionnaireCheckBox" class="span-24 last">
                    <h4><%: Html.LabelFor(model => model.test.OneQuestionnaire)%></h4><%: Html.CheckBoxFor(model => model.test.OneQuestionnaire, new {@id="one", @checked = "checked"  }) %>
                    <div><%: Html.ValidationMessageFor(model => model.test.OneQuestionnaire)%></div>
                </div>

                <div id="DivOneQuestionnaire">
                <div class="span-24 last"><h4><%: Html.LabelFor(model => model.test.Questionnaire_Id)%></h4></div>
                <div class="span-24 last"><%: Html.DropDownListFor(model => model.test.Questionnaire_Id, Model.questionnairesList, ViewRes.Scripts.Shared.Select, new { @class = "input-background short" })%></div>
                <div><%: Html.ValidationMessageFor(model => model.test.Questionnaire_Id)%></div> 
                </div>
                
                <div id="DivButtonSeveralQuestionnaires" class="button-padding-top">
				    <input id="ButtonSeveralQuestionnaires" type="button" class="button" value="<%:ViewRes.Views.Test.Create.AddQuestionnaires %>" />
			    </div>

                <div id="DialogQ" title="<%:ViewRes.Views.Test.Create.AddQuestionnaires %>">
                    <div class="span-24"> 
                        <div class="span-24 last"><h5><%: ViewRes.Views.Test.Create.SelectorDemographic%></h5></div>
                        <div class="span-24 last"><%: Html.DropDownList("DemographicSelector", Model.demographicsList, ViewRes.Scripts.Shared.Select, new { @class = "required input-background short" })%></div>
                        <%--<div><%: Html.ValidationMessageFor(model => model.test.Company_Id)%></div> --%>           
                                
                        <div class="span-24 last"><h5><%: ViewRes.Models.Test.Questionnaires%></h5></div>
                        <div class="span-24 last"><%: Html.DropDownList("QuestionnairesInTest", Model.questionnairesList, new { @multiple = "multiple", @class = "multiselect required input-background short" })%></div>
                        <%--<div><%: Html.ValidationMessageFor(model => model.test.Questionnaire_Id)%></div>  --%>
                
                        <div class="span-24 last"><h5><%: ViewRes.Views.Test.Create.Matching%></h5></div>
                        <table id="Matching" class="display tabla span-24 last">
                            <thead>
                                <tr>
                                    <td></td>
                                    <td><%: ViewRes.Models.Test.Questionnaires %></td>
                                    <td><%: ViewRes.Views.Test.Create.SelectorValues %></td>
                                </tr>
                            </thead>
                            <tbody>
                            </tbody>
                        </table>  
                     </div>
                </div><%-- dialog--%>


                <div class="span-24 last"><h4><%: Html.LabelFor(model => model.test.ClimateScale_Id)%></h4></div>
                <div class="span-24 last"><%: Html.DropDownListFor(model => model.test.ClimateScale_Id, Model.climateScalesList, ViewRes.Scripts.Shared.Select, new { @class = "required input-background short" })%></div>
                <div><%: Html.ValidationMessageFor(model => model.test.ClimateScale_Id)%></div> 

                <div class="span-24 last"><h4><%: Html.LabelFor(model => model.test.Name)%></h4></div>
                <div class="span-24 last"><%: Html.TextBoxFor(model => model.test.Name, new { @class = "input-background large" })%></div>
                <div><%: Html.ValidationMessageFor(model => model.test.Name)%></div>

                <div class="span-24 last">
                    <div class="column span-7">
                        <%--<div class="span-24 last"><h4><%: Html.LabelFor(model => model.test.StartDate)%></h4></div>--%>
                        <div class="span-24 last"><h4><%: Html.Label("Fecha de Inicio")%></h4></div>
                        <% string format = Session["Culture"].ToString() == "es" ? "dd/MM/yyyy" : "MM/dd/yyyy"; %>
                        <%: Html.Hidden("culture", Session["Culture"].ToString())%>
                        <% string fechaInicial = Model.test.StartDate.ToString(format); %>
                        <%: Html.Hidden("inicial", fechaInicial)%>
                        <div class=" column span-12 last"><%: Html.Editor("StartDate", new { @class = "datepicker", @id = "StartDate", Text= "Response.Write(fechaInicial);"})%></div>
                        <div><%: Html.ValidationMessageFor(model => model.test.StartDate)%></div>
                    </div>
                     <div class="column span-17 last">
                        <%--<div class="span-24 last"><h4><%: Html.LabelFor(model => model.test.EndDate)%></h4></div>--%>
                        <div class="span-24 last"><h4><%: Html.Label("Fecha de Finalización")%></h4></div>
                        <% string fechaFinal = Model.test.EndDate.ToString(format); %>
                        <%: Html.Hidden("final", fechaFinal)%>
                        <div class="column span-12 last"><%: Html.Editor("EndDate", new { @class = "datepicker", @id = "EndDate", Text = fechaFinal })%></div>
                        <div><%: Html.ValidationMessageFor(model => model.test.EndDate)%></div>
                    </div>
                
                </div>

                <div class="span-24 last"><h4><%: Html.LabelFor(model => model.test.EvaluationNumber)%></h4></div>
                <div class="span-24 last"><%: Html.TextBoxFor(model => model.test.EvaluationNumber, new { @class = "input-background tiny" })%>
                <div><%: Html.ValidationMessageFor(model => model.test.EvaluationNumber)%></div>
                <h4><a href="#" id="Calculate"><%= ViewRes.Views.Test.Create.CalculateSample %></a></h4></div>

                <div id="Dialog" title="<%= ViewRes.Views.Test.Create.DialogTitle %>">
                    <p class="validateTips"><%= ViewRes.Views.Test.Create.ValidationTip %></p>
                    <p><%= ViewRes.Views.Test.Create.ConfidenceLevel %></p>
		            <div class="editor-field">
			            <%: Html.DropDownList("CLDD", Model.CL, ViewRes.Scripts.Shared.Select, new { @class = "input-background small" })%>
		            </div>
                    <p><%= ViewRes.Views.Test.Create.StandardError %></p>
		            <div class="editor-field">
			            <%: Html.DropDownList("SEDD", Model.SE, ViewRes.Scripts.Shared.Select, new { @class = "input-background small" })%>
		            </div>
                    <p><%= ViewRes.Views.Test.Create.TotalEmployees %></p>
		            <div class="editor-field">
			            <%: Html.TextBox("P", Model.test.NumberOfEmployees, new { @class = "input-background tiny" })%>
		            </div>

                </div>

                <div class="span-24 last">
                    <h4><%: Html.LabelFor(model => model.test.GroupByCategories)%></h4><%: Html.CheckBoxFor(model => model.test.GroupByCategories, new {@id="group", @checked = "checked"  }) %>
                    <div><%: Html.ValidationMessageFor(model => model.test.GroupByCategories)%></div>
                </div>

                <div id="disordered" class="span-24 last" style="display:none;">
                    <h4><%: Html.LabelFor(model => model.test.Disordered)%></h4><%: Html.CheckBoxFor(model => model.test.Disordered, new { @checked = "checked"  }) %>
                    <div><%: Html.ValidationMessageFor(model => model.test.Disordered)%></div>
                </div>

                <div class="span-24 last"><h4><%: Html.LabelFor(model => model.test.RecordsPerPage)%></h4></div>
                <div class="span-24 last"><%: Html.TextBoxFor(model => model.test.RecordsPerPage, new { @class = "input-background tiny" })%></div>
                <div><%: Html.ValidationMessageFor(model => model.test.RecordsPerPage)%></div>

                <div class="span-24 last"><h4><%: Html.LabelFor(model => model.test.MinimumPeople)%></h4></div>
                <div class="span-24 last"><%: Html.TextBoxFor(model => model.test.MinimumPeople, new { @class = "input-background tiny" })%></div>
                <div><%: Html.ValidationMessageFor(model => model.test.MinimumPeople)%></div>

                <div class="span-24 last">
                    <h4><%: Html.LabelFor(model => model.test.ResultBasedOn100)%></h4><%: Html.CheckBoxFor(model => model.test.ResultBasedOn100) %>
                    <div><%: Html.ValidationMessageFor(model => model.test.ResultBasedOn100)%></div>
                </div>

                <div class="span-24 last">
                <h4><%: Html.LabelFor(model => model.test.Weighted)%></h4><%: Html.CheckBoxFor(model => model.test.Weighted, new { @id = "weighted", @checked = "checked"  }) %>
                <div><%: Html.ValidationMessageFor(model => model.test.Weighted)%></div>
                </div>

                <div id="categories" class="span-24 last" style="display:none;">

                </div>

                <div class="span-24 last"><h4><%: Html.LabelFor(model => model.test.Text)%></h4></div>
                <div class="span-24 last"><%: Html.TextAreaFor(model => model.test.Text, new { @class = "input-background textArea" })%></div>
                <div><%: Html.ValidationMessageFor(model => model.test.Text)%></div>
         </div>
