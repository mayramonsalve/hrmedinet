<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Medinet.Models.ViewModels.EvaluationViewModel>" %>
<%@ Import Namespace="MedinetClassLibrary.Models" %>
<%--        <div id="Dialog" class="span-10" title="Campos vacíos">
		    <div class="span-22 prepend-1 append-1">
            <br />
			    Debe seleccionar una opción por demográfico y una respuesta por pregunta.
		    </div>
        </div>--%>

<%: Html.HiddenFor(model => model.evaluation.Test_Id)%>
<%: Html.Hidden("MacAddress", "", new { id = "MacAddress" })%>
<% string dem = Model.test.DemographicsInTests.Where(s => s.Selector).Count() > 0 ?
                Model.test.DemographicsInTests.Where(s => s.Selector).FirstOrDefault().Demographic.Name : ""; %> <%--busco nombre de demografico selector--%>
<%: Html.Hidden("Demographic", dem, new { id = "Demographic" })%>
<%: Html.Hidden("idSelectorValue", "0", new { id = "idSelectorValue" })%>
<%--<% if (!Model.test.PreviousTest_Id.HasValue)
{%>--%>
<% string req = (Model.previousEvaluation != null) ? "" : "required"; %>
    <span class="step" id="f0">
    	<%: Html.ValidationSummary(ViewRes.Views.Shared.Errors.ErrorInApplication, new { @class = "errorAplication", @style="height:auto;" })%>
        <% string classSelector; %>
        <h3 class="span-22 prepend-2"><%: ViewRes.Views.Evaluation.AnswerTest.DemographicsData %></h3>   
        <% Html.EnableClientValidation(); %>

        <% List<DemographicsInTest> demoInTest = Model.test.DemographicsInTests.ToList();
           foreach (DemographicsInTest dit in Model.test.DemographicsInTests)
           {
               if (dit.FOT_Id.HasValue)
                   if (!dit.FunctionalOrganizationType.FOTParent_Id.HasValue)
                       demoInTest.Remove(dit);
           }
           Dictionary<string, string> demogList = demoInTest.ToDictionary(k => k.Demographic.Name, v => v.Label);%>
       
            <% if (demogList.Keys.Contains("Gender"))
               {
                   classSelector = Model.test.GetDemographicSelector() == "Gender" ? req + " validateRadio classSelector" : req;  %>
            <div class="span-22 prepend-2 column last verticalPadding">
                <div class="column">
                    <%if (demogList["Gender"] != "" && demogList["Gender"] != null)
                          { %>
                            <h5> <%: demogList["Gender"]%>:&nbsp;&nbsp;&nbsp;</h5>
                          <%}
                          else { %>
                            <h5> <%: Html.LabelFor(model => model.evaluation.Sex)%>:&nbsp;&nbsp;&nbsp;</h5>
                        <%} %>
                </div> 
                <div class="column last">
                    <%= Html.RadioButtonFor(model => model.evaluation.Sex, ViewRes.Classes.ChiSquare.MaleGender, new { @class = classSelector })%>&nbsp;<%: Html.Label(ViewRes.Classes.ChiSquare.MaleGender)%>
                    <%= Html.RadioButtonFor(model => model.evaluation.Sex, ViewRes.Classes.ChiSquare.FemaleGender, new { @class = classSelector })%>&nbsp;<%: Html.Label(ViewRes.Classes.ChiSquare.FemaleGender)%>
                </div> 
                <div><%: Html.ValidationMessageFor(model => model.evaluation.Sex)%></div>
            </div> 
            <% } %>   

            <% if (demogList.Keys.Contains("AgeRange"))
               {
                   classSelector = Model.test.GetDemographicSelector() == "AgeRange" ? req + " form-short validateDDL classSelector" : req + " form-short validateDDL";  %>
            <div class="span-22 prepend-2 column last verticalPadding">
                <div class="column">
					<%if (demogList["AgeRange"] != "" && demogList["AgeRange"] != null)
                          { %>
                            <h5> <%: demogList["AgeRange"]%>:&nbsp;&nbsp;&nbsp;</h5>
                          <%}
                          else { %>
                            <h5> <%: Html.LabelFor(model => model.evaluation.Age_Id)%>:&nbsp;&nbsp;&nbsp;</h5>
                        <%} %>
                </div>
                <div class="column last">
                    <%: Html.DropDownListFor(model => model.evaluation.Age_Id, Model.agesList, ViewRes.Scripts.Shared.Select, new { @class = classSelector })%>
                </div>
                    <div><%: Html.ValidationMessageFor(model => model.evaluation.Age_Id)%></div>
            </div>
            <% } %>   
            <% if (demogList.Keys.Contains("InstructionLevel"))
               {
                   classSelector = Model.test.GetDemographicSelector() == "InstructionLevel" ? req + " form-short validateDDL classSelector" : req + " form-short validateDDL";  %>
            <div class="span-22 prepend-2 column last verticalPadding">
                <div class="column">
                    <%if (demogList["InstructionLevel"] != "" && demogList["InstructionLevel"] != null)
                          { %>
                            <h5> <%: demogList["InstructionLevel"]%>:&nbsp;&nbsp;&nbsp;</h5>
                          <%}
                          else { %>
                            <h5> <%: Html.LabelFor(model => model.evaluation.InstructionLevel_Id)%>:&nbsp;&nbsp;&nbsp;</h5>
                        <%} %>
                </div>
                <div class="column last">
                    <%: Html.DropDownListFor(model => model.evaluation.InstructionLevel_Id, Model.instructionLevelsList, ViewRes.Scripts.Shared.Select, new { @class = classSelector })%>
				</div>
                    <div><%: Html.ValidationMessageFor(model => model.evaluation.InstructionLevel_Id)%></div>
            </div>
            <% } %>   
            <% if (demogList.Keys.Contains("Seniority"))
               {
                   classSelector = Model.test.GetDemographicSelector() == "Seniority" ? req + " form-short validateDDL classSelector" : req + " form-short validateDDL";  %>
            <div class="span-22 prepend-2 column last verticalPadding">
                <div class="column">
                    <%if (demogList["Seniority"] != "" && demogList["Seniority"] != null)
                          { %>
                            <h5> <%: demogList["Seniority"]%>:&nbsp;&nbsp;&nbsp;</h5>
                          <%}
                          else { %>
                            <h5> <%: Html.LabelFor(model => model.evaluation.Seniority_Id)%>:&nbsp;&nbsp;&nbsp;</h5>
                        <%} %>
                </div>
                <div class="column last">
                    <%: Html.DropDownListFor(model => model.evaluation.Seniority_Id, Model.senioritiesList, ViewRes.Scripts.Shared.Select, new { @class = classSelector })%>
		        </div>
                    <div><%: Html.ValidationMessageFor(model => model.evaluation.Seniority_Id)%></div>
            </div>
            <% } %>   
            <% if (demogList.Keys.Contains("PositionLevel"))
               {
                   classSelector = Model.test.GetDemographicSelector() == "PositionLevel" ? req + " form-short validateDDL classSelector" : req + " form-short validateDDL";  %>
            <div class="span-22 prepend-2 column last verticalPadding">
                <div class="column">
					<h5><%: Html.LabelFor(model => model.evaluation.PositionLevel_Id)%>:&nbsp;&nbsp;&nbsp;</h5>
                </div>
                <div class="column last">
                    <%: Html.DropDownListFor(model => model.evaluation.PositionLevel_Id, Model.positionLevelsList, ViewRes.Scripts.Shared.Select, new { @class = classSelector })%>
                </div>					
                <div><%: Html.ValidationMessageFor(model => model.evaluation.PositionLevel_Id)%></div>
            </div>   
            <%} %>   
                <% if (Model.FO.Count > 0)
                   {
                       foreach (var v in Model.FOTypes) { 
                            SelectList fo = Model.GetFOrganizationsByType(Convert.ToInt32(v.Id)); %>
                            <% if (Model.test.DemographicsInTests.Where(f => f.FOT_Id == v.Id && !f.FunctionalOrganizationType.FOTParent_Id.HasValue).Count() == 1)
                            {
                                classSelector = Model.test.GetDemographicSelector() == v.Name ? req + " form-short validateDDL classSelector FOT" : req + " form-short validateDDL FOT";  %>
                            <div id="Div_Parent_FOT_<%: v.Id %>" class="span-22 prepend-2 column last verticalPadding">
                                <div id="Parent_<%: v.Id %>" class="column last">
                                    <div class="column">
                                        <h5> <%: v.Name%>:&nbsp;&nbsp;&nbsp;</h5>
                                    </div>
                                    <div class="column last">
                                        <%: Html.DropDownList("evaluation_FunctionalOrganizationType_" + v.Name + "_Id", fo, ViewRes.Scripts.Shared.Select, new { @class = classSelector })%> 
                                    </div>
                                </div>
                            </div>
                            <%} %>
                        <%} 
                 }%>          
               <% if (demogList.Keys.Contains("Location"))
               {
                   classSelector = Model.test.GetDemographicSelector() == "Location" ? req + " form-short validateDDL classSelector" : req + " form-short validateDDL";  %>     
                    <% if (Model.locationsList.Count() != 0)
                  { %>
                   <div class="span-22 prepend-2 column last verticalPadding">
                        <div class="column">
                            <%if (demogList["Location"] != "" && demogList["Location"] != null)
                              { %>
                                <h5> <%: demogList["Location"]%>:&nbsp;&nbsp;&nbsp;</h5>
                              <%}
                              else { %>
                                <h5> <%: Html.LabelFor(model => model.evaluation.Location_Id)%>:&nbsp;&nbsp;&nbsp;</h5>
                            <%} %>
                        </div>
                        <div class="column last">
                            <%: Html.DropDownListFor(model => model.evaluation.Location_Id, Model.locationsList, ViewRes.Scripts.Shared.Select, new { @class = classSelector })%>
					    </div>
                        <div><%: Html.ValidationMessageFor(model => model.evaluation.Location_Id)%></div>
                    </div>
                <%  } %>     
            <%} %>           
               <% if (demogList.Keys.Contains("Performance"))
               {
                   classSelector = Model.test.GetDemographicSelector() == "Performance" ? req + " form-short validateDDL classSelector" : req + " form-short validateDDL";  %>
                 <%if (Model.performanceEvaluationsList.Count() != 0)
                   { %>
                    <div class="span-22 prepend-2 column last verticalPadding">
                        <div class="column">
                            <%if (demogList["Performance"] != "" && demogList["Performance"] != null)
                              { %>
                                <h5> <%: demogList["Performance"]%>:&nbsp;&nbsp;&nbsp;</h5>
                              <%}
                              else { %>
                                <h5> <%: Html.LabelFor(model => model.evaluation.Performance_Id)%>:&nbsp;&nbsp;&nbsp;</h5>
                            <%} %>
                        </div>
                        <div class="column last">
                            <%: Html.DropDownListFor(model => model.evaluation.Performance_Id, Model.performanceEvaluationsList, ViewRes.Scripts.Shared.Select, new { @class = classSelector })%>
					    </div>
                        <div><%: Html.ValidationMessageFor(model => model.evaluation.Performance_Id)%></div>
                    </div>
                 <%  } %>
             <%} %>
  
     </span>     
     
<%-- <%} %>       --%>