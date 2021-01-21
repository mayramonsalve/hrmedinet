<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Medinet.Models.ViewModels.EvaluationViewModel>" %>
<%@ Import Namespace="MedinetClassLibrary.Models" %>

       <div class="AboutTheTest">
            <h3><%: ViewRes.Views.Evaluation.TestInstructions.TitleTestInstructions %></h3>
		    <div class="editor-label">
			    <%: Html.LabelFor(model => model.test.Name) %>
			    <%: Html.Label(Model.test.Name.ToString())%>
		    </div>
		    <div class="editor-label">
			    <%: ViewRes.Views.Evaluation.TestInstructions.From %>
			    <%: Html.Label(Model.test.StartDate.ToString(ViewRes.Views.Shared.Shared.Date))%>
                <%: ViewRes.Views.Evaluation.TestInstructions.To %>
                <%: Html.Label(Model.test.EndDate.ToString(ViewRes.Views.Shared.Shared.Date))%>
		    </div>
            <div>
                <div class="editor-label">
                    <%: ViewRes.Views.Evaluation.TestInstructions.About %>
	            </div>
                <div class="editor-label">
                    <%: Model.test.Text.ToString()%>
	            </div>
            </div>
            <div>
                <div class="editor-label">
                    <%: Html.LabelFor(model => model.test.Questionnaire.Instructions) %>
	            </div>
                <div class="editor-label">
                    <%: Model.test.Questionnaire.Instructions.ToString()%>
	            </div>
            </div>
        </div>
        <p></p>
        <div class="Demographics">
            <h3><%: ViewRes.Views.Evaluation.AnswerTest.Demographics %></h3>
            <table id="Table">
	            <tr>
		            <td class="columnForm"> 
                        <div class="editor-field">
				            <%: Html.LabelFor(model => model.evaluation.Sex)%>
                        </div>
                        <div class="editor-field">
                            <%= Html.RadioButtonFor(model => model.evaluation.Sex, ViewRes.Classes.ChiSquare.MaleGender)%>
                            <%: Html.Label(ViewRes.Classes.ChiSquare.MaleGender)%>
                            <%= Html.RadioButtonFor(model => model.evaluation.Sex, ViewRes.Classes.ChiSquare.FemaleGender)%>
                            <%: Html.Label(ViewRes.Classes.ChiSquare.FemaleGender)%>
                        </div>
                    </td>
                </tr>
                <tr>
                    <td class="columnForm">
			            <div class="editor-label">
					        <%: Html.LabelFor(model => model.evaluation.Age_Id)%>
                        </div>
                        <div class="editor-label">
                            <% IQueryable<Age> ages = Model.GetAges();
                            foreach (var a in ages)
                            {
                            %>  
                                <div>
                                    <%= Html.RadioButton("Age",a.Id, false)%>
                                    <%: Html.Label(a.Name)%> 
                                </div>                              
                          <% } %>
				        </div>
                    </td>
                </tr>
                <tr>
                    <td class="columnForm">
			            <div class="editor-label">
					        <%: Html.LabelFor(model => model.evaluation.InstructionLevel_Id)%>
                        </div>
                        <div class="editor-label">
                            <% IQueryable<InstructionLevel> instructionLevels = Model.GetInstructionLevels();
                            foreach (var i in instructionLevels)
                            {
                            %>  
                                <div>
                                    <%= Html.RadioButton("InstructionLevels",i.Id, false)%>
                                    <%: Html.Label(i.Name)%> 
                                </div>                              
                          <% } %>
				        </div>
                    </td>
                </tr>
                <tr>
                    <td class="columnForm">
			            <div class="editor-label">
					        <%: Html.LabelFor(model => model.evaluation.Seniority_Id)%>
                        </div>
                        <div class="editor-label">
                            <% IQueryable<Seniority> seniorities = Model.GetSeniorities();
                            foreach (var s in seniorities)
                            {
                            %>  
                                <div>
                                    <%= Html.RadioButton("Seniority",s.Id, false)%>
                                    <%: Html.Label(s.Name)%> 
                                </div>                              
                          <% } %>				        
                          </div>
                    </td>
                </tr>
                <tr>
                    <td class="columnForm">
			            <div class="editor-label">
					        <%: Html.LabelFor(model => model.evaluation.PositionLevel_Id)%>
                        </div>
                        <div class="editor-label">
                            <% IQueryable<PositionLevel> positionLevels = Model.GetPositionLevels();
                            foreach (var p in positionLevels)
                            {
                            %>  
                                <div>
                                    <%= Html.RadioButton("PositionLevel",p.Id, false)%>
                                    <%: Html.Label(p.Name)%> 
                                </div>                              
                          <% } %>
				        </div>
                    </td>
                </tr>
                <% if (Model.FO.Count > 0)
                   {
                        foreach (var v in Model.FO)
                        { %>
                            <tr>
                                <td class="columnForm">
			                        <div class="editor-label">
					                    <%: Html.Label(v.Value)%>
                                    </div>
                                    <div class="editor-label">
                                        <% IQueryable<FunctionalOrganization> fOrganizations = Model.GetFOrganizations(Convert.ToInt32(v.Key));
                                        foreach (var fo in fOrganizations)
                                        {
                                        %>  
                                            <div>
                                                <%= Html.RadioButton("FOrganization",fo.Id, false)%>
                                                <%: Html.Label(fo.Name)%> 
                                            </div>                              
                                      <% } %>
				                    </div>
                                </td>
                            </tr>
                        <%} 
                   } %>
                <% IQueryable<Location> locations = Model.GetLocations();
                   if (locations != null)
                   {%>
                    <tr>
                        <td class="columnForm">
			                <div class="editor-label">
					            <%: Html.LabelFor(model => model.evaluation.Location_Id)%>
                            </div>
                            <div class="editor-label">
                                <%foreach (var l in locations)
                                  {
                                %>  
                                    <div>
                                        <%= Html.RadioButton("Location", l.Id, false)%>
                                        <%: Html.Label(l.Name)%> 
                                    </div>                              
                              <% } %>
				            </div>
                        </td>
                    </tr>
                <%}
                IQueryable<PerformanceEvaluation> performanceEvaluations = Model.GetPerformanceEvaluations();
                if (performanceEvaluations != null)
                {%>
                    <tr>
                        <td class="columnForm">
			                <div class="editor-label">
					            <%: Html.LabelFor(model => model.evaluation.Performance_Id)%>
                            </div>
                            <div class="editor-label">
                                <% foreach (var p in performanceEvaluations)
                                   { %>  
                                    <div>
                                        <%= Html.RadioButton("PerformanceEvaluations", p.Id, false)%>
                                        <%: Html.Label(p.Name)%> 
                                    </div>                              
                              <% } %>
				            </div>
                        </td>
                    </tr>
                <%} %>
            </table>
        </div>
