<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Medinet.Models.ViewModels.EvaluationViewModel>" %>
<%@ Import Namespace="MedinetClassLibrary.Models" %>
<h1><%: ViewRes.Views.Evaluation.AnswerTest.DemographicsData %></h1> 

<% if (Model.test.DemographicsInTests.Count == 0) { %>
<h4><%: ViewRes.Views.Evaluation.AnswerTest.NoDemographics %></h4>
<%} %>
    
<% if (Model.test.DemographicsInTests.Select(d => d.Demographic.Name).Contains("Gender"))
    {%>
        <div data-role="fieldcontain">
            <fieldset data-role="controlgroup" data-type="horizontal">
    	        <legend><%: Html.LabelFor(model => model.evaluation.Sex)%></legend>
                    <%: Html.RadioButtonFor(model => model.evaluation.Sex, ViewRes.Classes.ChiSquare.MaleGender, new {  @class = "required", @id = ViewRes.Classes.ChiSquare.MaleGender })%>
                    <%: Html.Label(ViewRes.Classes.ChiSquare.MaleGender)%>

                    <%: Html.RadioButtonFor(model => model.evaluation.Sex, ViewRes.Classes.ChiSquare.FemaleGender, new {  @id = ViewRes.Classes.ChiSquare.FemaleGender})%>
                    <%: Html.Label(ViewRes.Classes.ChiSquare.FemaleGender)%>
                    <div id="sexerror">
                    </div>
            </fieldset>

        </div>
        <%--<div><%: Html.ValidationMessageFor(model => model.evaluation.Sex)%></div>--%>
<% } %>   

<% if (Model.test.DemographicsInTests.Select(d => d.Demographic.Name).Contains("AgeRange"))
    {%>
    <fieldset data-role="fieldcontain"> 
        <%: Html.LabelFor(model => model.evaluation.Age_Id)%>
            <%: Html.DropDownListFor(model => model.evaluation.Age_Id, Model.agesList, ViewRes.Scripts.Shared.Select, new { @class = "required" })%>
        <%--         <div><%: Html.ValidationMessageFor(model => model.evaluation.Age_Id)%></div>--%>
    </fieldset>
<% } %>
                
<% if (Model.test.DemographicsInTests.Select(d => d.Demographic.Name).Contains("InstructionLevel"))
    {%>
    <fieldset data-role="fieldcontain"> 
        <%: Html.LabelFor(model => model.evaluation.InstructionLevel_Id)%>
        <%: Html.DropDownListFor(model => model.evaluation.InstructionLevel_Id, Model.instructionLevelsList, ViewRes.Scripts.Shared.Select, new { @class = "required" })%>
        <%-- <div><%: Html.ValidationMessageFor(model => model.evaluation.InstructionLevel_Id)%></div>--%>
    </fieldset>
<% } %>   

<% if (Model.test.DemographicsInTests.Select(d => d.Demographic.Name).Contains("Seniority"))
    {%>
    <fieldset data-role="fieldcontain"> 
        <%: Html.LabelFor(model => model.evaluation.Seniority_Id)%>
        <%: Html.DropDownListFor(model => model.evaluation.Seniority_Id, Model.senioritiesList, ViewRes.Scripts.Shared.Select, new { @class = "required" })%>
        <%-- <div><%: Html.ValidationMessageFor(model => model.evaluation.Seniority_Id)%></div>--%>
    </fieldset>
<% } %>   

<%--<% if (Model.FO.Count > 0)
   {
       foreach (FunctionalOrganizationType v in Model.FOTypes)
       {
           if (!v.FOTParent_Id.HasValue)
           {
               SelectList fo = Model.GetFOrganizationsByType(Convert.ToInt32(v.Id));
               %>
               <div>
                    <h4><%: v.Name %></h4>
                    <div>
                        <%: Html.DropDownList("evaluation_FunctionalOrganizationType_" + v.Name + "_Id", fo, ViewRes.Scripts.Shared.Select, new { @id = v.Id, @AppendDataBoundItems = "true", onchange = "loadFO(this.options[this.selectedIndex],this);" })%> 
                    </div>
                    <div style="padding-left:3%;">
                        <% foreach (FunctionalOrganizationType v2 in Model.FOTypes)
                           {
                               if (v2.FOTParent_Id.HasValue && v2.FOTParent_Id == v.Id)
                               {
                                   //SelectList fo2 = Model.GetFOrganizationsByType(Convert.ToInt32(v2.Id));
                                   SelectList fo2 = new SelectList("");
                                   %>
                                    <h4><%: v2.Name %></h4>
                                    <div>
                                        <%: Html.DropDownList("evaluation_FunctionalOrganizationType_" + v2.Name + "_Id", fo2, ViewRes.Scripts.Shared.Select, new { @AppendDataBoundItems = "true", @id = v2.Id })%> 
                                    </div>
                                   <%    
                               }
                           } 
                         %>
                    </div>
                    <div><%: Html.ValidationMessageFor(model => model.evaluation.EvaluationFOs)%></div>
               </div>
               <%
           }
       }
    } %>--%>

<%
if (Model.FO.Count > 0){
    int actual = 0;
    List<int> print = new List<int>();
    Stack<int> recorrido = new Stack<int>();
    int apartir = 0;
    int tam = Model.FOTypes.Count();
    SelectList fo;
    Array fotTypesArray = Model.FOTypes.ToArray();
    while (print.Count != tam)
    {
        FunctionalOrganizationType fot = (FunctionalOrganizationType)fotTypesArray.GetValue(actual);
        if (print.Where(d => d == fot.Id).Count() < 1)
        {
            if (recorrido.Count() == 0)
            {
                if (!fot.FOTParent_Id.HasValue)
                {
                    fo = Model.GetFOrganizationsByType(Convert.ToInt32(fot.Id));
                    %>
                    <fieldset data-role="fieldcontain"> 
                        <legend></legend>
                        <label for="<%:fot.Id %>"><%: fot.Name%></label>
                        <%: Html.DropDownList("evaluation_FunctionalOrganizationType_" + fot.Name + "_Id", fo, ViewRes.Scripts.Shared.Select, new { @class = "required", @id = fot.Id, @AppendDataBoundItems = "true", onchange = "loadFO(this.options[this.selectedIndex],this);" })%> 
                    </fieldset>
                    <%
                    apartir = 0;
                    print.Add(fot.Id);
                    recorrido.Push(fot.Id);
                }
            }
            else
            {
                if (recorrido.Peek() == fot.FOTParent_Id)
                {
                    fo = new SelectList("");
                    %>
                    <fieldset style="padding-left:<%: 3*recorrido.Count() %>%;" data-role="fieldcontain"> 
                        <legend></legend>
                        <label for="<%:fot.Id %>"><%: fot.Name%></label>
                        <%: Html.DropDownList("evaluation_FunctionalOrganizationType_" + fot.Name + "_Id", fo, ViewRes.Scripts.Shared.Select, new { @class = "required", @id = fot.Id, @AppendDataBoundItems = "true", onchange = "loadFO(this.options[this.selectedIndex],this);" })%> 
                    </fieldset>
                    <% 
                    apartir = 0;
                    print.Add(fot.Id);
                    recorrido.Push(fot.Id);
                }
                if (apartir >= tam)
                {
                    recorrido.Pop();
                    apartir = 1;
                }
            }
        }
        apartir = apartir + 1;
        actual = (actual + 1) % tam;
    }
}
%>
                        
<% if (Model.test.DemographicsInTests.Select(d => d.Demographic.Name).Contains("PositionLevel"))
    {%>
    <fieldset data-role="fieldcontain"> 
		<%: Html.LabelFor(model => model.evaluation.PositionLevel_Id)%>
        <%: Html.DropDownListFor(model => model.evaluation.PositionLevel_Id, Model.positionLevelsList, ViewRes.Scripts.Shared.Select, new { @class = "required"})%>				
        <%--<div><%: Html.ValidationMessageFor(model => model.evaluation.PositionLevel_Id)%></div>--%>
    </fieldset>
<%} %>
                 
<% if (Model.test.DemographicsInTests.Select(d => d.Demographic.Name).Contains("Location"))
    { %>     
    <% if (Model.locationsList.Count() != 0)
        { %>
        <fieldset data-role="fieldcontain"> 
                <% if (Model.test.Id >= 74 && Model.test.Id < 89)
                   {%>
                        <label for="evaluation_Location_Id" class="ui-select"><%: ViewRes.Models.Evaluation.Country%></label>
                    <%}%>
                <% else
                   {%>
                    <%: Html.LabelFor(model => model.evaluation.Location_Id)%>
                    <%} %>
                <%: Html.DropDownListFor(model => model.evaluation.Location_Id, Model.locationsList, ViewRes.Scripts.Shared.Select, new { @class = "required"})%>
                <%-- <div><%: Html.ValidationMessageFor(model => model.evaluation.Location_Id)%></div>--%>
        </fieldset>
    <%}%>     
<%}%> 
                      
<% if (Model.test.DemographicsInTests.Select(d => d.Demographic.Name).Contains("Performance"))
    {%>
    <%if (Model.performanceEvaluationsList.Count() != 0)
    { %>
            <fieldset data-role="fieldcontain"> 
                <%: Html.LabelFor(model => model.evaluation.Performance_Id)%>
                <%: Html.DropDownListFor(model => model.evaluation.Performance_Id, Model.performanceEvaluationsList, ViewRes.Scripts.Shared.Select, new { @class = "required" })%>
                <%--             <div><%: Html.ValidationMessageFor(model => model.evaluation.Performance_Id)%></div>--%>
        </fieldset>
    <%  } %>
<%} %>
