<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Medinet.Models.ViewModels.EvaluationViewModel>" %>
<%@ Import Namespace="MedinetClassLibrary.Models" %>

   <%: Html.HiddenFor(model => model.evaluation.Test_Id)%>
    <%: Html.HiddenFor(model => model.test.Id)%>
	<%--<%: Html.ValidationSummary(ViewRes.Views.Shared.Errors.ErrorInApplication, new { @class = "errorAplication" })%>--%>
    <% 
       List<string> demographics = Model.test.DemographicsInTests.Select(d => d.Demographic.Name).ToList();
       if (demographics.Contains("AgeRange"))
       {
           %><%:Html.HiddenFor(model => Model.evaluation.Age_Id)%><%--<%: Model.evaluation.Age_Id%>--%><%
       }
       if (demographics.Contains("InstructionLevel"))
       {
           %><%:Html.HiddenFor(model => Model.evaluation.InstructionLevel_Id)%><%--<%: Model.evaluation.InstructionLevel_Id%>--%><%
       }
       if (demographics.Contains("PositionLevel"))
       {
           %><%:Html.HiddenFor(model => Model.evaluation.PositionLevel_Id)%><%--<%: Model.evaluation.PositionLevel_Id%>--%><%
       }
       if (demographics.Contains("Seniority"))
       {
           %><%:Html.HiddenFor(model => Model.evaluation.Seniority_Id)%><%--<%: Model.evaluation.Seniority_Id%>--%><%
       }
       if (demographics.Contains("Location"))
       {
           %><%:Html.HiddenFor(model => Model.evaluation.Location_Id)%><%--<%: Model.evaluation.Location_Id%>--%><%
       }
       if (demographics.Contains("Performance"))
       {
           %><%:Html.HiddenFor(model => Model.evaluation.Performance_Id)%><%--<%: Model.evaluation.Performance_Id%>--%><%
       }
       if (demographics.Contains("Gender"))
       {
           %><%:Html.HiddenFor(model => Model.evaluation.Sex)%><%--<%: Model.evaluation.Sex%>--%><%
       }
       int[][] selected_fo = Model.Selected_FO;
        for (int j = 0; j < selected_fo[0].Length; j++)
        {
            %>
<%--        <%: selected_fo[0][j] %>
            <%: selected_fo[1][j] %>--%>
            <%: Html.Hidden("FO" + j, selected_fo[1][j]) %>
            <%
       }
       %>