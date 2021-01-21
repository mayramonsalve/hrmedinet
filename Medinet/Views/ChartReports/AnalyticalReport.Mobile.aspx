<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Mobile/MobileMini.Master" Inherits="System.Web.Mvc.ViewPage<Medinet.Models.ViewModels.AnalyticalReportViewModel>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    <%: ViewRes.Views.Shared.Shared.GenericTitle %><%: ViewRes.Views.ChartReport.Graphics.ExecutiveReport %>
</asp:Content>

<asp:Content ID="Content7" ContentPlaceHolderID="PageId" runat="server">
<div data-role="page" id="mainChartanalytical" class="basic">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

<%  string[] Categories = new string[1];
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
    Levels = Model.PositiveAnswersByPositionLevels.Keys.ToArray();
    string[] demographics = Model.Test.DemographicsInTests.Select(d => d.Demographic.Name).ToArray();%>
    
 <%: Html.Hidden("test", Model.Test.Id)%>
<h1 class="testname"><%: ViewRes.Views.ChartReport.Graphics.ExecutiveReportTitle%> </h1>
<div class="box rounded">
<div class="principal">
    <div class="ui-block-a" style="width:100%">
        <h1><%: ViewRes.Views.ChartReport.Graphics.GeneralResult%><%: Model.Test.Name %>
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
        </h1>
        <p><%: ViewRes.Views.ChartReport.Graphics.Purpose%></p>
    </div>
    <div class="ui-block-a" style="width:100%">
        <h1 style="width:100%"><%: ViewRes.Views.ChartReport.Graphics.Sample%></h1>
    </div>
    <div class="chartDiv portrait">
        <div id="GenderChart" class="google_chart"></div>
    </div>
    <div class="generalDiv">
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
    <% string colorStyle = "background-color:" + Model.ColourByClimate; %>

    <div class="ui-block-a" style="width:100%">
        <h1 style="width:100%"><%: ViewRes.Views.ChartReport.Graphics.DescriptiveAnalysis%></h1>
        <p><%: ViewRes.Views.ChartReport.Graphics.TheGeneralResultsIndicate%> <%: String.Format("{0:0.##}",Model.GeneralClimate) %>%
            <%: ViewRes.Views.ChartReport.Graphics.Corresponding%><span style=<%:colorStyle %>>&nbsp;&nbsp;&nbsp;</span><%: ViewRes.Views.ChartReport.Graphics.ColorState%>
            <%: ViewRes.Views.ChartReport.Graphics.Registering%> <%: String.Format("{0:0.##}", Model.SatisfiedCountPercentage) %>%.
        </p>
    </div>

    <div class="chartDiv portrait">
        <div id="GeneralChart" class="google_chart"></div>
    </div>

    <div class="generalDiv">
        <table class="table1">
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

    <div class="ui-block-a" style="width:100%">
        <%if (Model.PositiveAnswersByPositionLevels != null && Model.PositiveAnswersByPositionLevels.Count > 0)
          { %>
        <p><%: ViewRes.Views.ChartReport.Graphics.StrataLevel %><%: Levels[0]%> 
        <%: ViewRes.Views.ChartReport.Graphics.Acquired %><%: String.Format("{0:0.##}", Model.PositiveAnswersByPositionLevels[Levels[0]])%>%
        <%: ViewRes.Views.ChartReport.Graphics.StrataLevelEmployees %> 
        <%: String.Format("{0:0.##}", Model.PositiveAnswersByPositionLevels[Levels[1]])%>%
        <%: ViewRes.Views.ChartReport.Graphics.Result %></p>
        <%} %>
        <% if (Model.PositiveAnswersByFOTypes != null && Model.PositiveAnswersByFOTypes.Count > 0)
           { %>
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
        <%} %>

        <h1 style="width:100%"><%: ViewRes.Views.ChartReport.Graphics.DimensionsTitle%></h1>
        <% if(Model.ClimateByCategories.Count > 0)
            { %>
            <p><%: ViewRes.Views.ChartReport.Graphics.TheHighestCategoryResults%><%: Categories[0] %> (<%: String.Format("{0:0.##}",Model.ClimateByCategories[Categories[0]]) %>%)
            <% if (Categories[2] != "B"){%>, <%}else{%><%: ViewRes.Views.ChartReport.Graphics.And%><%}%><%: Categories[1] %> (<%: String.Format("{0:0.##}", Model.ClimateByCategories[Categories[1]])%>%)
            <% if (Categories[2] == "B"){%>, <%}else{%><%: ViewRes.Views.ChartReport.Graphics.And%><%: Categories[2] %> (<%: String.Format("{0:0.##}",Model.ClimateByCategories[Categories[2]])%>%),<%}%>
            <%: ViewRes.Views.ChartReport.Graphics.TheLowestCategoryResults%>
            <%: Categories[3] %> (<%: String.Format("{0:0.##}",Model.ClimateByCategories[Categories[3]]) %>%)
            <% if (Categories[5] != "W"){%>, <%}else{%><%: ViewRes.Views.ChartReport.Graphics.And%><%}%><%: Categories[4] %> (<%: String.Format("{0:0.##}", Model.ClimateByCategories[Categories[4]])%>%)
            <% if (Categories[5] == "W"){%><%: ViewRes.Views.ChartReport.Graphics.Dimensions%><%}else{%><%: ViewRes.Views.ChartReport.Graphics.And%><%: Categories[5] %> (<%: String.Format("{0:0.##}",Model.ClimateByCategories[Categories[5]])%>%)<%: ViewRes.Views.ChartReport.Graphics.Dimensions%><%}%></p>
        <%} %>

    </div>
        
    <% string classDimension = "chartDiv portrait";
    Dictionary<string, double[]> dimension = (Dictionary<string, double[]>)Model.SatNotSat["Category"];
    if (dimension.Count <= 7)
    { %>
    <div class="<%: classDimension %>">
        <div id="CategoryChart" class="google_chart"></div>
    </div>
    <% classDimension = "generalDiv";
    } %>
    <div class="<%: classDimension %>">
        <table class="table1">
                <thead>
                    <tr>
                        <th></th>
                        <% if (dimension.Count > 7)
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
                            <% if (dimension.Count > 7)
                                {
                                    colorStyle = "background-color:" + Model.GetColourByClimate(vector.Value[4]); %>
                                    <td><span style=<%:colorStyle %>>&nbsp;&nbsp;&nbsp;</span></td>                                             
                                    <td style="color:Black; text-align: center;"><%:String.Format("{0:0.##}", vector.Value[4])%></td>     
                                    <%--<td style="color:Black; text-align: center;"><%:String.Format("{0:0.##}", vector.Value[5])%></td> --%>
                                <%} %>
                            <td style="color:Green; text-align: center;"><%:vector.Value[0]%> (<%: String.Format("{0:0.##}", vector.Value[1])%>%)</td>     
                            <td style="color:Red; text-align: center;"><%:vector.Value[2]%> (<%: String.Format("{0:0.##}", vector.Value[3])%>%)</td> 
                        </tr>
                    <%} %>
                </tbody>
        </table>
    </div>
    
    <% string classBranch = "chartDiv portrait";%>
    <% if (demographics.Contains("Location"))
        { %>
            <div class="ui-block-a" style="width:100%">
                <h1 style="width:100%"><%: ViewRes.Views.ChartReport.Graphics.BranchesTitle%></h1>
                <% if (Model.ClimateByBranches != null && Model.ClimateByBranches.Count > 0)
                    { %>
                <p><%: ViewRes.Views.ChartReport.Graphics.TheHighestBranchResults%><%: Branches[0] %> (<%: String.Format("{0:0.##}", Model.ClimateByBranches[Branches[0]])%>%)
                <% if (Branches[2] != "B")
    {%>, <%}
    else
    {%><%: ViewRes.Views.ChartReport.Graphics.And%><%}%><%: Branches[1]%> (<%: String.Format("{0:0.##}", Model.ClimateByBranches[Branches[1]])%>%)
                <% if (Branches[2] == "B")
    {%>, <%}
    else
    {%><%: ViewRes.Views.ChartReport.Graphics.And%><%: Branches[2]%> (<%: String.Format("{0:0.##}", Model.ClimateByBranches[Branches[2]])%>%),<%}%>
                <%: ViewRes.Views.ChartReport.Graphics.TheLowestBranchResults%>
                <%: Branches[3]%> (<%: String.Format("{0:0.##}", Model.ClimateByBranches[Branches[3]])%>%)
                <% if (Branches[5] != "W")
    {%>, <%}
    else
    {%><%: ViewRes.Views.ChartReport.Graphics.And%><%}%><%: Branches[4]%> (<%: String.Format("{0:0.##}", Model.ClimateByBranches[Branches[4]])%>%)
                <% if (Branches[5] == "W")
    {%><%: ViewRes.Views.ChartReport.Graphics.Branches%><%}
    else
    {%><%: ViewRes.Views.ChartReport.Graphics.And%><%: Branches[5]%> (<%: String.Format("{0:0.##}", Model.ClimateByBranches[Branches[5]])%>%)<%: ViewRes.Views.ChartReport.Graphics.Branches%><%}%></p>
                <br />
                <%} %>
            </div>
            <% Dictionary<string, double[]> location = (Dictionary<string, double[]>)Model.SatNotSat["Location"];
            if (location.Count <= 7)
            { %>
            <div class="<%: classBranch %>">
                <div id="LocationChart" class="google_chart"></div>
            </div>
            <% classBranch = "generalDiv";
            } %>
            <div class="<%: classBranch %>">
                <table class="table1">
                        <thead>
                            <tr>
                            <th></th>
                            <% if (location.Count > 7)
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
                                    <% if (location.Count > 7)
                                        { %>
                                            <td style="color:Black; text-align: center;"><%:String.Format("{0:0.##}", vector.Value[4])%></td>     
                                            <%--<td style="color:Black; text-align: center;"><%:String.Format("{0:0.##}", vector.Value[5])%></td> --%>
                                        <%} %>
                                    <td style="color:Green; text-align: center;"><%:vector.Value[0]%> (<%: String.Format("{0:0.##}", vector.Value[1])%>%)</td>     
                                    <td style="color:Red; text-align: center;"><%:vector.Value[2]%> (<%: String.Format("{0:0.##}", vector.Value[3])%>%)</td> 
                                </tr>
                            <%} %>
                        </tbody>
                </table>
            </div>
    <%} %>
    <!---->
    <% if (demographics.Contains("FunctionalOrganizationType"))
        { %>
            <div class="ui-block-a" style="width:100%">
                <h1 style="width:100%"><%: ViewRes.Views.ChartReport.Graphics.FOTypesTitle%> (<%:Model.FOTName%>)</h1>
                <% if (Model.ClimateByFOTypes != null && Model.ClimateByFOTypes.Count > 0)
                    { %>
                <p><%: ViewRes.Views.ChartReport.Graphics.TheHighestFOTypeResults%><%: FOTypes[0] %> (<%: String.Format("{0:0.##}", Model.ClimateByFOTypes[FOTypes[0]])%>%)
                <% if (FOTypes[2] != "B")
    {%>, <%}
    else
    {%><%: ViewRes.Views.ChartReport.Graphics.And%><%}%><%: FOTypes[1]%> (<%: String.Format("{0:0.##}", Model.ClimateByFOTypes[FOTypes[1]])%>%)
                <% if (FOTypes[2] == "B")
    {%><%}
    else
    {%><%: ViewRes.Views.ChartReport.Graphics.And%><%: FOTypes[2]%> (<%: String.Format("{0:0.##}", Model.ClimateByFOTypes[FOTypes[2]])%>%)<%}%>
                <%: ViewRes.Views.ChartReport.Graphics.TheLowestFOTypeResults%>
                <%: FOTypes[3]%> (<%: String.Format("{0:0.##}", Model.ClimateByFOTypes[FOTypes[3]])%>%)
                <% if (FOTypes[5] != "W")
    {%>, <%}
    else
    {%><%: ViewRes.Views.ChartReport.Graphics.And%><%}%><%: FOTypes[4]%> (<%: String.Format("{0:0.##}", Model.ClimateByFOTypes[FOTypes[4]])%>%)
                <% if (FOTypes[5] == "W")
    {%>.<%}
    else
    {%><%: ViewRes.Views.ChartReport.Graphics.And%><%: FOTypes[5]%> (<%: String.Format("{0:0.##}", Model.ClimateByFOTypes[FOTypes[5]])%>%).<%}%></p>
                <br />
                <%} %>
            </div>

            <% string classFOType = "chartDiv portrait";
            Dictionary<string, double[]> fotype = (Dictionary<string, double[]>)Model.SatNotSat["FunctionalOrganizationType"];
            if (fotype.Count <= 7)
            { %>
            <div class="<%: classBranch %>">
                <div id="FOTypeChart" class="google_chart"></div>
            </div>
            <% classFOType = "generalDiv";
            } %>
            <div class="<%: classFOType %>">
                <table class="table1">
                        <thead>
                            <tr>
                            <th></th>
                            <% if (fotype.Count > 7)
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
                                    <% if (fotype.Count > 7)
                                        { %>
                                            <td style="color:Black; text-align: center;"><%:String.Format("{0:0.##}", vector.Value[4])%></td>     
                                            <%--<td style="color:Black; text-align: center;"><%:String.Format("{0:0.##}", vector.Value[5])%></td> --%>
                                        <%} %>
                                    <td style="color:Green; text-align: center;"><%:vector.Value[0]%> (<%: String.Format("{0:0.##}", vector.Value[1])%>%)</td>     
                                    <td style="color:Red; text-align: center;"><%:vector.Value[2]%> (<%: String.Format("{0:0.##}", vector.Value[3])%>%)</td> 
                                </tr>
                            <%} %>
                        </tbody>
                </table>
            </div>
      <%} %>
    <!---->
    <% if (demographics.Contains("AgeRange"))
    { %>
        <div class="ui-block-a" style="width:100%">
            <h1 style="width:100%"><%: ViewRes.Views.ChartReport.Graphics.AgeRangesTitle%></h1>
            <% if (Model.ClimateByAgeRanges != null && Model.ClimateByAgeRanges.Count > 0)
                { %>
            <p><%: ViewRes.Views.ChartReport.Graphics.TheHighestAgeRangeResults%><%: AgeRanges[0] %> (<%: String.Format("{0:0.##}", Model.ClimateByAgeRanges[AgeRanges[0]])%>%)
            <% if (AgeRanges[2] != "B")
    {%>, <%}
    else
    {%><%: ViewRes.Views.ChartReport.Graphics.And%><%}%><%: AgeRanges[1]%> (<%: String.Format("{0:0.##}", Model.ClimateByAgeRanges[AgeRanges[1]])%>%)
            <% if (AgeRanges[2] == "B")
    {%><%}
    else
    {%><%: ViewRes.Views.ChartReport.Graphics.And%><%: AgeRanges[2]%> (<%: String.Format("{0:0.##}", Model.ClimateByAgeRanges[AgeRanges[2]])%>%)<%}%>
            <%: ViewRes.Views.ChartReport.Graphics.TheLowestAgeRangeResults%>
            <%: AgeRanges[3]%> (<%: String.Format("{0:0.##}", Model.ClimateByAgeRanges[AgeRanges[3]])%>%)
            <% if (AgeRanges[5] != "W")
    {%>, <%}
    else
    {%><%: ViewRes.Views.ChartReport.Graphics.And%><%}%><%: AgeRanges[4]%> (<%: String.Format("{0:0.##}", Model.ClimateByAgeRanges[AgeRanges[4]])%>%)
            <% if (AgeRanges[5] == "W")
    {%><%: ViewRes.Views.ChartReport.Graphics.AgeRanges%><%}
    else
    {%><%: ViewRes.Views.ChartReport.Graphics.And%><%: Branches[5]%> (<%: String.Format("{0:0.##}", Model.ClimateByBranches[Branches[5]])%>%)<%: ViewRes.Views.ChartReport.Graphics.AgeRanges%><%}%></p>
            <br />
            <%} %>
        </div>
        <% string classAgeRange = "chartDiv portrait";
        Dictionary<string, double[]> agerange = (Dictionary<string, double[]>)Model.SatNotSat["AgeRange"];
        if (agerange.Count <= 7)
        { %>
        <div class="<%: classAgeRange %>">
            <div id="AgeRangeChart" class="google_chart"></div>
        </div>
        <% classAgeRange = "generalDiv";
        } %>
        <div class="<%: classAgeRange %>">
            <table class="table1">
                    <thead>
                        <tr>
                        <th></th>
                        <% if (agerange.Count > 7)
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
                                <% if (agerange.Count > 7)
                                    { %>
                                        <td style="color:Black; text-align: center;"><%:String.Format("{0:0.##}", vector.Value[4])%></td>     
                                        <%--<td style="color:Black; text-align: center;"><%:String.Format("{0:0.##}", vector.Value[5])%></td> --%>
                                    <%} %>
                                <td style="color:Green; text-align: center;"><%:vector.Value[0]%> (<%: String.Format("{0:0.##}", vector.Value[1])%>%)</td>     
                                <td style="color:Red; text-align: center;"><%:vector.Value[2]%> (<%: String.Format("{0:0.##}", vector.Value[3])%>%)</td> 
                            </tr>
                        <%} %>
                    </tbody>
            </table>
        </div>
    <%} %>
    <!---->

    <div class="ui-block-a" style="width:100%">
        <h1><%: ViewRes.Views.ChartReport.Graphics.Analysis %></h1>
        <p><%: ViewRes.Views.ChartReport.Graphics.HRMedinetPerformed %></p>
        <% if (Model.DemographicsWhereThereIsAssociation != null && Model.DemographicsWhereThereIsAssociation.Count > 0)
           { %>
            <%: ViewRes.Views.ChartReport.Graphics.Variables%>
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

    <% if (Model.StepwiseValues != null && Model.StepwiseValues.Count > 0)
           { %>
    <div class="ui-block-a" style="width:100%">
        <h1><%: ViewRes.Views.ChartReport.Graphics.VariablesAffecting%></h1>
        <p><%: ViewRes.Views.ChartReport.Graphics.HRPerformedMultivariable%> </p>
        <%foreach (KeyValuePair<string,double> value in Model.StepwiseValues)
        {%>
                - <%: value.Key %>: <%: value.Value %>%
        <%}%>
    </div>
    <%}%>

    <%if (Model.PositionAndCompaniesCount != null && Model.PositionAndCompaniesCount[1] > 0)
      { %>
        <div class="ui-block-a" style="width:100%">
            <h1><%: ViewRes.Views.ChartReport.Graphics.Ranking%></h1>
            <p><%: ViewRes.Views.ChartReport.Graphics.TheComparativeRanking%>
            <%:Model.Test.Company.Name%>
            <%: ViewRes.Views.ChartReport.Graphics.IsPlaced%><%: Model.PositionAndCompaniesCount[0]%> 
            <%: ViewRes.Views.ChartReport.Graphics.FromANumber%><%: Model.PositionAndCompaniesCount[1]%><%: ViewRes.Views.ChartReport.Graphics.Companies%></p>
        </div>
    <% } %>
</div>
<%Html.RenderPartial("Button.Mobile"); %>
</div>
<script type="text/javascript">
    $(".table1").tablesorter(); 
</script>
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="HeadContent" runat="server">

</asp:Content>

<asp:Content ID="Content4" ContentPlaceHolderID="CssContent" runat="server">
</asp:Content>

<asp:Content ID="Content5" ContentPlaceHolderID="JsContent" runat="server"> 
    <script src="<%: Url.Content("~/Scripts/mobile/jquery.tablesorter.js") %>" type="text/javascript"></script>
    <script src="<%: Url.Content("~/Scripts/mobile/ChartsMobile.js?mm=0709") %>" type="text/javascript"></script>
</asp:Content>
