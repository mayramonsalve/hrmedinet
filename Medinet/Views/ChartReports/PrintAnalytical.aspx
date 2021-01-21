<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Print.Master" Inherits="System.Web.Mvc.ViewPage<Medinet.Models.ViewModels.AnalyticalReportViewModel>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
   <%: ViewRes.Views.Shared.Shared.GenericTitle %><%: ViewRes.Views.ChartReport.Graphics.StatisticalReports %>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
<% string[] Categories = new string[1];
   if (Model.ClimateByCategories != null && Model.ClimateByCategories.Count > 0)
     Categories = Model.ClimateByCategories.Keys.ToArray();
    string[] Branches = new string[1];
    if (Model.ClimateByBranches != null && Model.ClimateByBranches.Count > 0)
        Branches = Model.ClimateByBranches.Keys.ToArray();
    string[] FOTypes = new string[1];
    if (Model.ClimateByFOTypes != null && Model.ClimateByFOTypes.Count > 0)
        FOTypes = Model.ClimateByFOTypes.Keys.ToArray();
    string[] AgeRanges = new string[1];
    if (Model.ClimateByAgeRanges != null && Model.ClimateByAgeRanges.Count > 0)
        AgeRanges = Model.ClimateByAgeRanges.Keys.ToArray();
   string[] FO = new string[1];
   if (Model.PositiveAnswersByFOTypes != null && Model.PositiveAnswersByFOTypes.Count > 0)
        FO = Model.PositiveAnswersByFOTypes.Keys.ToArray();
   string[] Levels = new string[1];
   if (Model.PositiveAnswersByPositionLevels != null && Model.PositiveAnswersByPositionLevels.Count > 0)
    Levels = Model.PositiveAnswersByPositionLevels.Keys.ToArray();%>

<div id="contenido-sistema">
    <div class="span-24">
         <%: Html.Hidden("countOptions", Model.optionsCount)%>
         <%: Html.Hidden("test", Model.Test.Id)%>
         <%: Html.Hidden("Print", 1)%>
         <%: Html.Hidden("PdfName", ViewRes.Views.ChartReport.Graphics.ExecutiveReportTitle)%>
        <h2 class="alignCenter path"><%: ViewRes.Views.ChartReport.Graphics.ExecutiveReportTitle%> </h2>
        <div class="linea-sistema-footer"></div>

        <div class="span-24 button-padding-top no-break">
            <h3><%: ViewRes.Views.ChartReport.Graphics.GeneralResult%><%: Model.Test.Name %>
            <% if (!Model.Ubication.Keys.Contains("General"))
               {%>
                    <%: ViewRes.Views.ChartReport.Graphics.In%>
                    <% if (Model.Ubication.Keys.Contains("Region"))
                       { %>
                           <%: Model.Ubication["Region"]%><%: ViewRes.Views.ChartReport.Graphics.Region%>
                     <%}
                       else
                       {%>
                        <% if (Model.Ubication.Keys.Contains("State"))
                           { %>
                               <%: Model.Ubication["State"]%>,
                         <%}%>
                         <% if (Model.Ubication.Keys.Contains("Country"))
                            { %>
                               <%: Model.Ubication["Country"]%>
                         <%}
                       }%>
             <%} %>
            </h3>
            <p><%: ViewRes.Views.ChartReport.Graphics.Purpose%></p>
            <br />
        </div>
        <div class="span-24 column last no-break">
            <h3><%: ViewRes.Views.ChartReport.Graphics.Sample%></h3>
            <div class="span-10 column">
                <div id="GenderChart" class="span-24 google_chart"></div>
            </div>
            <div class="prepend-1 span-13 column last">
            <br />
                <% double error = Math.Sqrt((((((double)Model.Test.ConfidenceLevel.Value * (double)Model.Test.ConfidenceLevel.Value * Model.Test.EvaluationNumber * 0.5 * (1 - 0.5)) / Model.Test.CurrentEvaluations) - (((double)Model.Test.ConfidenceLevel.Value * (double)Model.Test.ConfidenceLevel.Value * 0.5 * (1 - 0.5)))) / (Model.Test.EvaluationNumber - 1)));%>
                <% double pct =  (double)(Model.Test.CurrentEvaluations * 100) / Model.Test.EvaluationNumber;%>
                <p><strong><%: ViewRes.Views.Evaluation.TestInstructions.From%></strong>
			    <%: Model.Test.StartDate.ToString(ViewRes.Views.Shared.Shared.Date)%>
                <strong><%: ViewRes.Views.Evaluation.TestInstructions.To%></strong>
                <%: Model.Test.EndDate.ToString(ViewRes.Views.Shared.Shared.Date)%></p>
                <p><strong><%: ViewRes.Views.ChartReport.Graphics.EmployeesUsed%></strong>
                <%: Model.Test.EvaluationNumber%></p>
                <p><strong><%: ViewRes.Views.ChartReport.Graphics.EmployeesWhoPerformed%></strong>
                <%: Model.Test.CurrentEvaluations%></p>
                <p><strong><%: ViewRes.Views.ChartReport.Graphics.PercentageEvaluationReceived%></strong>
                <%: String.Format("{0:0.##}", pct)%>%</p>
                <p><strong><%: ViewRes.Views.Test.Create.ConfidenceLevel%>: </strong>
                <%: Model.Test.ConfidenceLevel.Text%></p> 
                <p><strong><%: ViewRes.Views.Test.Create.StandardError%>: </strong>
                <%: String.Format("{0:0.##}", error*100)%>%</p>
            </div>
            <br />
        </div>
        <div class="clear">&nbsp;</div>
        <% string colorStyle = "background-color:" + Model.ColourByClimate; %>
        <div class="span-24">
            <div class="no-break">
                <h3><%: ViewRes.Views.ChartReport.Graphics.DescriptiveAnalysis%></h3>
                <p><%: ViewRes.Views.ChartReport.Graphics.TheGeneralResultsIndicate%> <%: String.Format("{0:0.##}",Model.GeneralClimate) %>%
                 <%: ViewRes.Views.ChartReport.Graphics.Corresponding%><span style=<%:colorStyle %>>&nbsp;&nbsp;&nbsp;</span><%: ViewRes.Views.ChartReport.Graphics.ColorState%>
                <%: ViewRes.Views.ChartReport.Graphics.Registering%> <%: String.Format("{0:0.##}", Model.SatisfiedCountPercentage) %>%.</p>
            </div>
            <div class="span-24 alignCenter no-break">
                <div id="GeneralChart" class="span-24 google_chart"></div>
            </div>
            <div class="prepend-4 span-16 append-4 no-break">
                <table class="display tabla">
                        <thead>
                          <tr>
                            <th></th>
                            <th><%: ViewRes.Views.ChartReport.Graphics.Satisfied%></th>
                            <th><%: ViewRes.Views.ChartReport.Graphics.NoSatisfied%></th>
                          </tr>
                        </thead>
                        <tbody>
                            <%foreach (KeyValuePair<string, double[]> vector in (IEnumerable)Model.SatNotSat["General"])
                              { %>
                               <tr>                        
                                  <td style="color:Black;" ><%:vector.Key%></td>
                                  <td style="color:Green; text-align: center;"><%:vector.Value[0]%> (<%: String.Format("{0:0.##}", vector.Value[1])%>%)</td>     
                                  <td style="color:Red; text-align: center;"><%:vector.Value[2]%> (<%: String.Format("{0:0.##}", vector.Value[3])%>%)</td> 
                                </tr>
                            <%} %>
                        </tbody>
                </table>
            </div>
            <br />
            <%if (Model.PositiveAnswersByPositionLevels != null)
              { %>
              <% if (Model.PositiveAnswersByPositionLevels.Count > 0)
                 { %>
                <div class="no-break">
                    <p><%: ViewRes.Views.ChartReport.Graphics.StrataLevel%><%: Levels[0]%> 
                    <%: ViewRes.Views.ChartReport.Graphics.Acquired%><%: String.Format("{0:0.##}", Model.PositiveAnswersByPositionLevels[Levels[0]])%>%
                    <%: ViewRes.Views.ChartReport.Graphics.StrataLevelEmployees%> 
                    <%: String.Format("{0:0.##}", Model.PositiveAnswersByPositionLevels[Levels[1]])%>%
                    <%: ViewRes.Views.ChartReport.Graphics.Result%></p>
                    <br />
                </div>
                <% } %>
            <%} %>
            <% if (Model.PositiveAnswersByFOTypes != null && Model.PositiveAnswersByFOTypes.Count > 0)
               { %>
                <div class="no-break">
                    <p><%: ViewRes.Views.ChartReport.Graphics.FunctionallyHighest %><%: FO[0]%> (<%: String.Format("{0:0.##}", Model.PositiveAnswersByFOTypes[FO[0]])%>%)
                    <% if (FO[2] != "B")
                       {%><%}
                       else
                       {%><%: ViewRes.Views.ChartReport.Graphics.And %><%}%><%: FO[1]%> (<%: String.Format("{0:0.##}", Model.PositiveAnswersByFOTypes[FO[1]])%>%)
                    <% if (FO[2] == "B")
                       {%><%}
                       else
                       {%><%: ViewRes.Views.ChartReport.Graphics.And %><%: FO[2]%> (<%: String.Format("{0:0.##}", Model.PositiveAnswersByFOTypes[FO[2]])%>%),<%}%>
                        <%: ViewRes.Views.ChartReport.Graphics.FunctionallyLowest %>
                    <%: FO[3]%> (<%: String.Format("{0:0.##}", Model.PositiveAnswersByFOTypes[FO[3]])%>%)
                    <% if (FO[5] != "W")
                       {%>, <%}
                       else
                       {%><%: ViewRes.Views.ChartReport.Graphics.And %><%}%><%: FO[4]%> (<%: String.Format("{0:0.##}", Model.PositiveAnswersByFOTypes[FO[4]])%>%)
                    <% if (FO[5] == "W")
                       {%><%: ViewRes.Views.ChartReport.Graphics.Areas %><%}
                       else
                       {%> y <%: FO[5]%> (<%: String.Format("{0:0.##}", Model.PositiveAnswersByFOTypes[FO[5]])%>%)<%: ViewRes.Views.ChartReport.Graphics.Areas %><%}%></p>
                        <br />
                </div>
            <%} %>
        </div>
        <div class="clear">&nbsp;</div>
        <div class="span-24">
            <div class="no-break">
            <h3><%: ViewRes.Views.ChartReport.Graphics.DimensionsTitle%></h3>
            <% if (Model.ClimateByCategories != null && Model.ClimateByCategories.Count > 0)
                { %>
                    <p><%: ViewRes.Views.ChartReport.Graphics.TheHighestCategoryResults%><%: Categories[0] %> (<%: String.Format("{0:0.##}",Model.ClimateByCategories[Categories[0]]) %>%)
                    <% if (Categories[2] != "B"){%>, <%}else{%><%: ViewRes.Views.ChartReport.Graphics.And%><%}%><%: Categories[1] %> (<%: String.Format("{0:0.##}", Model.ClimateByCategories[Categories[1]])%>%)
                    <% if (Categories[2] == "B"){%>, <%}else{%><%: ViewRes.Views.ChartReport.Graphics.And%><%: Categories[2] %> (<%: String.Format("{0:0.##}",Model.ClimateByCategories[Categories[2]])%>%),<%}%>
                    <%: ViewRes.Views.ChartReport.Graphics.TheLowestCategoryResults%>
                    <%: Categories[3] %> (<%: String.Format("{0:0.##}",Model.ClimateByCategories[Categories[3]]) %>%)
                    <% if (Categories[5] != "W"){%>, <%}else{%><%: ViewRes.Views.ChartReport.Graphics.And%><%}%><%: Categories[4] %> (<%: String.Format("{0:0.##}", Model.ClimateByCategories[Categories[4]])%>%)
                    <% if (Categories[5] == "W"){%><%: ViewRes.Views.ChartReport.Graphics.Dimensions%><%}else{%><%: ViewRes.Views.ChartReport.Graphics.And%><%: Categories[5] %> (<%: String.Format("{0:0.##}",Model.ClimateByCategories[Categories[5]])%>%)<%: ViewRes.Views.ChartReport.Graphics.Dimensions%><%}%></p>
            </div>
                <br />
            <%} %>
                <% Dictionary<string, double[]> dimension = (Dictionary<string, double[]>)Model.SatNotSat["Category"];
                    if (dimension.Count <= 10)
                    { %>
                <div class="span-24 alignCenter no-break">
                    <div id="CategoryChart" class="span-24 google_chart"></div>
                </div>
                <%} %>
                <div class="prepend-4 span-16 append-4">
                    <table class="display tabla">
                            <thead>
                                <tr>
                                <th></th>
                                <% if (dimension.Count > 10)
                                   { %>
                                   <th></th>
                                   <th><%: ViewRes.Views.ChartReport.Graphics.Average%></th>
                                    <%--<th><%: ViewRes.Views.ChartReport.Graphics.Median%></th>--%>
                                 <%} %>
                                <th><%: ViewRes.Views.ChartReport.Graphics.Satisfied%></th>
                                <th><%: ViewRes.Views.ChartReport.Graphics.NoSatisfied%></th>
                                </tr>
                            </thead>
                            <tbody>
                                <%foreach (KeyValuePair<string, double[]> vector in (IEnumerable)Model.SatNotSat["Category"])
                                    { %>
                                    <tr>                        
                                        <td style="color:Black;" ><%:vector.Key%></td>
                                        <% if (dimension.Count > 10)
                                           {
                                               colorStyle = "background-color:" + Model.GetColourByClimate(vector.Value[4]); %>
                                                <td><span style=<%:colorStyle %>>&nbsp;&nbsp;&nbsp;</span></td>                                             
                                                <td style="color:Black; text-align: center;"><%:String.Format("{0:0.##}", (vector.Value[4]*100)/Model.optionsCount)%></td> <!-- Columna de promedio-->     
                                                <%--<td style="color:Black; text-align: right;"><%:String.Format("{0:0.##}", vector.Value[5])%></td> --%>
                                         <%} %>
                                        <td style="color:Green; text-align: center;"><%:vector.Value[0]%> (<%: String.Format("{0:0.##}", vector.Value[1])%>%)</td>     
                                        <td style="color:Red; text-align: center;"><%:vector.Value[2]%> (<%: String.Format("{0:0.##}", vector.Value[3])%>%)</td> 
                                    </tr>
                                <%} %>
                            </tbody>
                    </table>
                </div>

        </div>
        <div class="clear">&nbsp;</div>
        <div class="span-24">
            <br />
            <div class="no-break">
            <h3><%: ViewRes.Views.ChartReport.Graphics.BranchesTitle%></h3>
            <% if (Model.ClimateByBranches != null && Model.ClimateByBranches.Count > 0)
                { %>
                    <p><%: ViewRes.Views.ChartReport.Graphics.TheHighestBranchResults%><%: Branches[0] %> (<%: String.Format("{0:0.##}", Model.ClimateByBranches[Branches[0]])%>%)
                    <% if (Branches[2] != "B")
                       {%>, <%}else{%><%: ViewRes.Views.ChartReport.Graphics.And%><%}%><%: Branches[1]%> (<%: String.Format("{0:0.##}", Model.ClimateByBranches[Branches[1]])%>%)
                    <% if (Branches[2] == "B")
                       {%><%}else{%><%: ViewRes.Views.ChartReport.Graphics.And%><%: Branches[2]%> (<%: String.Format("{0:0.##}", Model.ClimateByBranches[Branches[2]])%>%)<%}%>
                    <%: ViewRes.Views.ChartReport.Graphics.TheLowestBranchResults%>
                    <%: Branches[3]%> (<%: String.Format("{0:0.##}", Model.ClimateByBranches[Branches[3]])%>%)
                    <% if (Branches[5] != "W")
                       {%>, <%}else{%><%: ViewRes.Views.ChartReport.Graphics.And%><%}%><%: Branches[4]%> (<%: String.Format("{0:0.##}", Model.ClimateByBranches[Branches[4]])%>%)
                    <% if (Branches[5] == "W")
                       {%><%: ViewRes.Views.ChartReport.Graphics.Branches%><%}else{%><%: ViewRes.Views.ChartReport.Graphics.And%><%: Branches[5]%> (<%: String.Format("{0:0.##}", Model.ClimateByBranches[Branches[5]])%>%)<%: ViewRes.Views.ChartReport.Graphics.Branches%><%}%></p>
                </div>
                <br />
                <%} %>
                <% Dictionary<string, double[]> location = (Dictionary<string, double[]>)Model.SatNotSat["Location"];
                    if (location.Count <= 10)
                   { %>
                <div class="span-24 alignCenter no-break">
                    <div id="LocationChart" class="span-24 google_chart"></div>
                </div>
                <%} %>
                <div class="prepend-4 span-16 append-4">
                    <table class="tabla display">
                            <thead>
                                <tr>
                                <th></th>
                                <% if (location.Count > 10)
                                   { %>
                                   <th><%: ViewRes.Views.ChartReport.Graphics.Average%></th>
                                    <%--<th><%: ViewRes.Views.ChartReport.Graphics.Median%></th>--%>
                                 <%} %>
                                <th><%: ViewRes.Views.ChartReport.Graphics.Satisfied%></th>
                                <th><%: ViewRes.Views.ChartReport.Graphics.NoSatisfied%></th>
                                </tr>
                            </thead>
                            <tbody>
                                <%foreach (KeyValuePair<string, double[]> vector in (IEnumerable)Model.SatNotSat["Location"])
                                    { %>
                                    <tr>                        
                                        <td style="color:Black;" ><%:vector.Key%></td>
                                        <% if (location.Count > 10)
                                           { %>
                                                <td style="color:Black; text-align: center;"><%:String.Format("{0:0.##}", vector.Value[4])%></td>     
                                                <%--<td style="color:Black; text-align: right;"><%:String.Format("{0:0.##}", vector.Value[5])%></td> --%>
                                         <%} %>
                                        <td style="color:Green; text-align: center;"><%:vector.Value[0]%> (<%: String.Format("{0:0.##}", vector.Value[1])%>%)</td>     
                                        <td style="color:Red; text-align: center;"><%:vector.Value[2]%> (<%: String.Format("{0:0.##}", vector.Value[3])%>%)</td> 
                                    </tr>
                                <%} %>
                            </tbody>
                    </table>
                </div>
            </div>
        <div class="clear">&nbsp;</div>
        <!-- FOT -->
        <div class="span-24">
            <br />
            <div class="no-break">
            <h3><%: ViewRes.Views.ChartReport.Graphics.FOTypesTitle%> (<%:Model.FOTName%>)</h3>
            <% if (Model.ClimateByFOTypes != null && Model.ClimateByFOTypes.Count > 0)
                { %>
                <p><%: ViewRes.Views.ChartReport.Graphics.TheHighestFOTypeResults%><%: FOTypes[0] %> (<%: String.Format("{0:0.##}", Model.ClimateByFOTypes[FOTypes[0]])%>%)
                <% if (FOTypes[2] != "B")
                   {%>, <%}else{%><%: ViewRes.Views.ChartReport.Graphics.And%><%}%><%: FOTypes[1]%> (<%: String.Format("{0:0.##}", Model.ClimateByFOTypes[FOTypes[1]])%>%)
                <% if (FOTypes[2] == "B")
                   {%><%}else{%><%: ViewRes.Views.ChartReport.Graphics.And%><%: FOTypes[2]%> (<%: String.Format("{0:0.##}", Model.ClimateByFOTypes[FOTypes[2]])%>%)<%}%>
                <%: ViewRes.Views.ChartReport.Graphics.TheLowestFOTypeResults%>
                <%: FOTypes[3]%> (<%: String.Format("{0:0.##}", Model.ClimateByFOTypes[FOTypes[3]])%>%)
                <% if (FOTypes[5] != "W")
                   {%>, <%}else{%><%: ViewRes.Views.ChartReport.Graphics.And%><%}%><%: FOTypes[4]%> (<%: String.Format("{0:0.##}", Model.ClimateByFOTypes[FOTypes[4]])%>%)
                <% if (FOTypes[5] == "W")
                   {%>.<%}else{%><%: ViewRes.Views.ChartReport.Graphics.And%><%: FOTypes[5]%> (<%: String.Format("{0:0.##}", Model.ClimateByFOTypes[FOTypes[5]])%>%).<%}%></p>
                </div>
                <br />
                <%} %>
                <% Dictionary<string, double[]> fotype = (Dictionary<string, double[]>)Model.SatNotSat["FunctionalOrganizationType"];
                    if (fotype.Count <= 10)
                   { %>
                <div class="span-24 alignCenter no-break">
                    <div id="FOTypeChart" class="span-24 google_chart"></div>
                </div>
                <%} %>
                <div class="prepend-4 span-16 append-4">
                    <table class="tabla display">
                            <thead>
                                <tr>
                                <th></th>
                                <% if (fotype.Count > 10)
                                   { %>
                                   <th><%: ViewRes.Views.ChartReport.Graphics.Average%></th>
                                    <%--<th><%: ViewRes.Views.ChartReport.Graphics.Median%></th>--%>
                                 <%} %>
                                <th><%: ViewRes.Views.ChartReport.Graphics.Satisfied%></th>
                                <th><%: ViewRes.Views.ChartReport.Graphics.NoSatisfied%></th>
                                </tr>
                            </thead>
                            <tbody>
                                <%foreach (KeyValuePair<string, double[]> vector in (IEnumerable)Model.SatNotSat["FunctionalOrganizationType"])
                                    { %>
                                    <tr>                        
                                        <td style="color:Black;" ><%:vector.Key%></td>
                                        <% if (fotype.Count > 10)
                                           { %>
                                                <td style="color:Black; text-align: center;"><%:String.Format("{0:0.##}", vector.Value[4])%></td>     
                                                <%--<td style="color:Black; text-align: right;"><%:String.Format("{0:0.##}", vector.Value[5])%></td> --%>
                                         <%} %>
                                        <td style="color:Green; text-align: center;"><%:vector.Value[0]%> (<%: String.Format("{0:0.##}", vector.Value[1])%>%)</td>     
                                        <td style="color:Red; text-align: center;"><%:vector.Value[2]%> (<%: String.Format("{0:0.##}", vector.Value[3])%>%)</td> 
                                    </tr>
                                <%} %>
                            </tbody>
                    </table>
                </div>

            </div>
        <div class="clear">&nbsp;</div>
         <!-- AGE RANGES -->
        <div class="span-24">
            <br />
            <div class="no-break">
            <h3><%: ViewRes.Views.ChartReport.Graphics.AgeRangesTitle%></h3>
            <% if (Model.ClimateByAgeRanges != null && Model.ClimateByAgeRanges.Count > 0)
                { %>
                <p><%: ViewRes.Views.ChartReport.Graphics.TheHighestAgeRangeResults%><%: AgeRanges[0] %> (<%: String.Format("{0:0.##}", Model.ClimateByAgeRanges[AgeRanges[0]])%>%)
                <% if (AgeRanges[2] != "B")
                   {%>, <%}else{%><%: ViewRes.Views.ChartReport.Graphics.And%><%}%><%: AgeRanges[1]%> (<%: String.Format("{0:0.##}", Model.ClimateByAgeRanges[AgeRanges[1]])%>%)
                <% if (AgeRanges[2] == "B")
                   {%><%}else{%><%: ViewRes.Views.ChartReport.Graphics.And%><%: AgeRanges[2]%> (<%: String.Format("{0:0.##}", Model.ClimateByAgeRanges[AgeRanges[2]])%>%)<%}%>
                <%: ViewRes.Views.ChartReport.Graphics.TheLowestAgeRangeResults%>
                <%: AgeRanges[3]%> (<%: String.Format("{0:0.##}", Model.ClimateByAgeRanges[AgeRanges[3]])%>%)
                <% if (AgeRanges[5] != "W")
                   {%>, <%}else{%><%: ViewRes.Views.ChartReport.Graphics.And%><%}%><%: AgeRanges[4]%> (<%: String.Format("{0:0.##}", Model.ClimateByAgeRanges[AgeRanges[4]])%>%)
                <% if (AgeRanges[5] == "W")
                   {%><%: ViewRes.Views.ChartReport.Graphics.AgeRanges%><%}else{%><%: ViewRes.Views.ChartReport.Graphics.And%><%: Branches[5]%> (<%: String.Format("{0:0.##}", Model.ClimateByBranches[Branches[5]])%>%)<%: ViewRes.Views.ChartReport.Graphics.AgeRanges%><%}%></p>
                </div>
                <br />
                <%} %>
                <% Dictionary<string, double[]> agerange = (Dictionary<string, double[]>)Model.SatNotSat["AgeRange"];
                    if (agerange.Count <= 10)
                   { %>
                <div class="span-24 alignCenter no-break">
                    <div id="AgeRangeChart" class="span-24 google_chart"></div>
                </div>
                <%} %>
                <div class="prepend-4 span-16 append-4">
                    <table class="tabla display">
                            <thead>
                                <tr>
                                <th></th>
                                <% if (agerange.Count > 10)
                                   { %>
                                   <th><%: ViewRes.Views.ChartReport.Graphics.Average%></th>
                                    <%--<th><%: ViewRes.Views.ChartReport.Graphics.Median%></th>--%>
                                 <%} %>
                                <th><%: ViewRes.Views.ChartReport.Graphics.Satisfied%></th>
                                <th><%: ViewRes.Views.ChartReport.Graphics.NoSatisfied%></th>
                                </tr>
                            </thead>
                            <tbody>
                                <%foreach (KeyValuePair<string, double[]> vector in (IEnumerable)Model.SatNotSat["AgeRange"])
                                    { %>
                                    <tr>                        
                                        <td style="color:Black;" ><%:vector.Key%></td>
                                        <% if (agerange.Count > 10)
                                           { %>
                                                <td style="color:Black; text-align: center;"><%:String.Format("{0:0.##}", vector.Value[4])%></td>     
                                                <%--<td style="color:Black; text-align: right;"><%:String.Format("{0:0.##}", vector.Value[5])%></td> --%>
                                         <%} %>
                                        <td style="color:Green; text-align: center;"><%:vector.Value[0]%> (<%: String.Format("{0:0.##}", vector.Value[1])%>%)</td>     
                                        <td style="color:Red; text-align: center;"><%:vector.Value[2]%> (<%: String.Format("{0:0.##}", vector.Value[3])%>%)</td> 
                                    </tr>
                                <%} %>
                            </tbody>
                    </table>
                </div>

            </div>
        <div class="clear">&nbsp;</div>
        <div class="span-24">
            <br />
            <div class="no-break">
                <h3><%: ViewRes.Views.ChartReport.Graphics.Analysis %></h3>
                <p><%: ViewRes.Views.ChartReport.Graphics.HRMedinetPerformed %></p>
            </div>
            <br />
            <% if (Model.DemographicsWhereThereIsAssociation != null && Model.DemographicsWhereThereIsAssociation.Count > 0)
               { %>
                <%: ViewRes.Views.ChartReport.Graphics.Variables%>
                <br />
                <%foreach (string demographic in Model.DemographicsWhereThereIsAssociation)
              {%>
                        - <%:demographic%>.<br />
                <%}
               }
               else
               {%>
                    <%: ViewRes.Views.ChartReport.Graphics.NoVariables%><br />
               <%}%>
        </div>
        <div class="clear">&nbsp;</div>
        <% if (Model.StepwiseValues != null && Model.StepwiseValues.Count > 0)//esto es la parte que hay que modificar cuando este activo el Stepwise en la libreria estadistica
               { %>
            <div class="span-24">
                <br />
                <div class="no-break">
                    <h3><%: ViewRes.Views.ChartReport.Graphics.VariablesAffecting%></h3>
                    <p><%: ViewRes.Views.ChartReport.Graphics.HRPerformedMultivariable%> </p>
                </div>
                <br />
                <%foreach (KeyValuePair<string,double> value in Model.StepwiseValues)
                {%>
                        - <%: value.Key %>: <%: value.Value %>%
                <%}%>
            </div>
        <%}%>
        <%if (Model.PositionAndCompaniesCount != null)
          { %>
        <% if (Model.PositionAndCompaniesCount[1] > 0)
           { %>
        <div class="span-24">
            <br />
            <div class="no-break">
                <h3><%: ViewRes.Views.ChartReport.Graphics.Ranking%></h3>
                <p><%: ViewRes.Views.ChartReport.Graphics.TheComparativeRanking%>
                <%:Model.Test.Company.Name%>
                <%: ViewRes.Views.ChartReport.Graphics.IsPlaced%><%: Model.PositionAndCompaniesCount[0]%> 
                <%: ViewRes.Views.ChartReport.Graphics.FromANumber%><%: Model.PositionAndCompaniesCount[1]%><%: ViewRes.Views.ChartReport.Graphics.Companies%></p>
            </div>
            <br />
        </div>
        <%} %>
        <% } %>
    </div>
</div>

</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="HeadContent" runat="server">
            <div id="header" class="block">  
                <div class="span-4 column">
                    <img src="../../Content/Images/Logo360.png" alt="" width="180" height="60" />
                </div>
                <div class="span-4 prepend-16 column last">
                    <% if (Model.Test.Company.Image != null && Model.Test.Company.Image != "System.Web.HttpPostedFileWrapper")
                        { %>
						    <img src="../../Content/Images/Companies/<%: Model.Test.Company.Image %>" alt="" width="180" height="60" />
                    <% } %>
                </div>
            </div>
</asp:Content>

<asp:Content ID="Content4" ContentPlaceHolderID="CssContent" runat="server">
<%--    <link href="../../Content/Css/demo_page.css" rel="stylesheet" type="text/css" media="screen, projection" />
    <link href="../../Content/Css/demo_table_jui.css" rel="stylesheet" type="text/css" media="screen, projection" />
    <link href="../../Content/Css/demo_table.css" rel="stylesheet" type="text/css" media="screen, projection" />--%>
</asp:Content>

<asp:Content ID="Content5" ContentPlaceHolderID="JsContent" runat="server"> 
    <script src="<%: Url.Content("~/Scripts/jquery.dataTables.min.js") %>" type="text/javascript"></script>
    <script src="<%: Url.Content("~/Scripts/AnalyticalReport.js?mm=0709") %>" type="text/javascript"></script>
</asp:Content>
