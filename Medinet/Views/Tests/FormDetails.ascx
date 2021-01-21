<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Medinet.Models.ViewModels.TestViewModel>" %>

            <div class="span-24 last"> 
                <%if (Model.userRole == "HRAdministrator") {%>
                    <div class="span-24 last"><h4><%: Html.LabelFor(model => model.test.Company_Id)%></h4></div>
                    <div class="span-24 last"><%: Model.test.Company.Name.ToString()%></div>
                <% }%> 
                <div class="span-24 last"><h4><%: Html.LabelFor(model => model.test.PreviousTest_Id)%></h4></div>
                <% if (Model.test.PreviousTest_Id.HasValue)
                   { %>
                    <div class="span-24 last"><%: Model.test.PreviousTest.Name.ToString()%></div>
                <%}
                   else
                   {%>
                    <div class="span-24 last"><%: ViewRes.Views.Test.Details.None %></div>
                   <%} %>
                <div class="span-24 last"><h4><%: ViewRes.Views.Test.Create.DemographicsInTest %></h4></div>               
                <div class="span-24 last">
                
                <% string demografico="hola";
                    foreach (string demog in Model.test.GetDemographicsInTest().Distinct())
                   {                      
                       
                           if(demog=="AgeRange")
                               demografico = ViewRes.Views.Test.Details.AgeRange;
                           if(demog=="InstructionLevel")
                               demografico = ViewRes.Views.Test.Details.InstructionLevel;
                           if(demog=="Location")
                               demografico = ViewRes.Views.Test.Details.Location;
                           if(demog=="Performance")
                               demografico = ViewRes.Views.Test.Details.Performance;
                           if(demog=="PositionLevel")
                               demografico = ViewRes.Views.Test.Details.PositionLevel;
                           if(demog=="Seniority")
                               demografico = ViewRes.Views.Test.Details.Seniority;
                           if(demog=="Country")
                               demografico = ViewRes.Views.Test.Details.Country;
                           if(demog=="State")
                               demografico = ViewRes.Views.Test.Details.State;
                           if(demog=="Region")
                               demografico = ViewRes.Views.Test.Details.Region;
                           if(demog=="FunctionalOrganizationType")
                               demografico = ViewRes.Views.Test.Details.FunctionalOrganizationType;
                           if(demog=="Gender")
                               demografico = ViewRes.Views.Test.Details.Gender;                          
                                                                       
                       %>
                        - <%: demografico%><br />
                   <%} %>
               </div>
               
                    <%if(Model.test.OneQuestionnaire)
                      { %>
                       <%-- <div class="span-24 last"><%: ViewRes.Views.Shared.Shared.True%></div>--%>
                        <div class="span-24 last"><h4><%: Html.LabelFor(model => model.test.Questionnaire_Id)%></h4></div>
                        <div class="span-24 last"><%: Model.test.Questionnaire.Name.ToString()%></div>
                    <%}
                      else
                      { %>
                        <%--<div class="span-24 last"><%: ViewRes.Views.Shared.Shared.False%></div>--%>
                        <div class="span-24 last"><h4><%: ViewRes.Views.Test.Create.SelectorDemographic%></h4></div>
                        <div class="span-24 last"><%: Model.test.GetDemographicSelector()%></div>
                        <div class="span-24 last"><h4><%: ViewRes.Models.Test.Questionnaires%></h4></div>
                        <div class="span-24 last">
                        <%foreach (string questionnaire in Model.test.GetQuestionnairesNameByTest())
                          { %>
                            - <%: questionnaire %> <br />
                          <%} %>
                          </div>
                          <table>
                            <thead>
                                <tr>
                                    <th><%: ViewRes.Models.Test.Questionnaires %></th>
                                    <th><%: ViewRes.Views.Test.Create.SelectorValues %></th>
                                </tr>
                            </thead>
                            <tbody>
                                <%foreach (KeyValuePair<string, string> match in Model.test.GetMatching())
                                  {%>
                                    <tr>
                                        <td><%: match.Key %></td>
                                        <td><%: match.Value %></td>
                                    </tr>
                                <%} %>
                            </tbody>
                          </table>
                    <%} %>

                <div class="span-24 last"><h4><%: Html.LabelFor(model => model.test.ClimateScale_Id)%></h4></div>
                <div class="span-24 last"><%:Model.test.ClimateScale.Name.ToString()%></div>

                <div class="span-24 last"><h4><%: Html.LabelFor(model => model.test.Name)%></h4></div>
                <div class="span-24 last"><%:Model.test.Name.ToString()%></div>

                <div class="span-24 last">
                    <div class="column span-12">
                        <h4><%: Html.LabelFor(model => model.test.StartDate)%></h4>
                        <div class="column span-12 last"><%: Model.test.StartDate.ToString(ViewRes.Views.Shared.Shared.Date)%></div>
                    </div>
                     <div class="column span-12 last">
                        <h4><%: Html.LabelFor(model => model.test.EndDate)%></h4>
                        <div class="column span-12 last"><%: Model.test.EndDate.ToString(ViewRes.Views.Shared.Shared.Date)%></div>
                    </div>
                </div>
                <div class="span-24 last"><h4><%: Html.LabelFor(model => model.test.EvaluationNumber)%></h4></div>
                <div class="span-24 last"><%:Model.test.EvaluationNumber.ToString()%></div>

                <% if (Model.test.ConfidenceLevel != null){ %>
                    <div class="span-24 last"><h4><%: ViewRes.Views.Test.Create.ConfidenceLevel%></h4></div>
                    <div class="span-24 last"><%:String.Format("{0:0.####}", Model.test.ConfidenceLevel.Text)%></div>
                <% } %>
                <% if (Model.test.StandardError != null){ %>
                    <div class="span-24 last"><h4><%: ViewRes.Views.Test.Create.StandardError%></h4></div>
                    <div class="span-24 last"><%:Model.test.StandardError.Text%></div>
                <% } %>
                <% if (Model.test.NumberOfEmployees != null){ %>
                    <div class="span-24 last"><h4><%:  Html.LabelFor(model => model.test.NumberOfEmployees)%></h4></div>
                    <div class="span-24 last"><%:Model.test.NumberOfEmployees%></div>
                <% } %>

                <div class="span-24 last"><h4><%: Html.LabelFor(model => model.test.GroupByCategories)%></h4></div>
                <%if(Model.test.GroupByCategories)
                  { %>
                    <div class="span-24 last"><%: ViewRes.Views.Shared.Shared.True%></div>
                <%}
                  else
                  { %>
                    <div class="span-24 last"><%: ViewRes.Views.Shared.Shared.False%></div>
                <%} %>

                <% if (Model.test.GroupByCategories)
                   { %>
                    <div class="span-24 last"><h4><%: Html.LabelFor(model => model.test.Disordered)%></h4></div>
                    <%if(Model.test.Disordered)
                      { %>
                        <div class="span-24 last"><%: ViewRes.Views.Shared.Shared.True%></div>
                    <%}
                      else
                      { %>
                        <div class="span-24 last"><%: ViewRes.Views.Shared.Shared.False%></div>
                    <%} %>
                <%} %>

                <div class="span-24 last"><h4><%: Html.LabelFor(model => model.test.RecordsPerPage)%></h4></div>
                <div class="span-24 last"><%: Model.test.RecordsPerPage.ToString()%></div>

                <div class="span-24 last"><h4><%: Html.LabelFor(model => model.test.MinimumPeople)%></h4></div>
                <div class="span-24 last"><%: Model.test.MinimumPeople.ToString()%></div>

                <div class="span-24 last"><h4><%: Html.LabelFor(model => model.test.ResultBasedOn100)%></h4></div>
                <%if(Model.test.ResultBasedOn100)
                  { %>
                    <div class="span-24 last"><%: ViewRes.Views.Shared.Shared.True%></div>
                <%}
                  else
                  { %>
                    <div class="span-24 last"><%: ViewRes.Views.Shared.Shared.False%></div>
                <%} %>

                <div class="span-24 last"><h4><%: Html.LabelFor(model => model.test.Weighted)%></h4></div>
                <%if(Model.test.Weighted)
                  { %>
                    <div class="span-24 last"><%: ViewRes.Views.Shared.Shared.True%></div>
                <%}
                  else
                  { %>
                    <div class="span-24 last"><%: ViewRes.Views.Shared.Shared.False%></div>
                <%} %>

                <%if (Model.test.Weighted)
                  { %>
                    <div id="showWeighings" class="span-24 last">

                    </div>
                <%} %>

                <div class="span-24 last"><h4><%: Html.LabelFor(model => model.test.Text)%></h4></div>
                <div class="span-24 last"><%: Model.test.Text.ToString()%></div>
         </div> 