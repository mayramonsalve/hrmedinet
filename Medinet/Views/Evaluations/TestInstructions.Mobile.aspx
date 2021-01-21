<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/mobile/MobileMini.Master" Inherits="System.Web.Mvc.ViewPage<Medinet.Models.ViewModels.EvaluationViewModel>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    <%: ViewRes.Views.Shared.Shared.GenericTitle %> <%: ViewRes.Views.Evaluation.TestInstructions.TitleTestInstructions %>
</asp:Content>

<asp:Content ID="Content7" ContentPlaceHolderID="PageId" runat="server">
<div data-role="page" id="mainTest" class="basic">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

<% using (Html.BeginForm("MobileDemographicsAnswerTest", "Evaluations", FormMethod.Get, new { @code = Model.test.Code, data_ajax = "false"}))
   { %>
    <%: Html.Hidden("code", Model.test.Code)%>
    <div class="box rounded"> 
        <h1><%= ViewRes.Views.Evaluation.TestInstructions.PathTestInstructions%></h1>
        <div>
            <div>
                <h4><%: Html.LabelFor(model => model.test.Name)%></h4>
			        <%: Model.test.Name.ToString()%>
            </div>
            <div>
                    <h4>
                        <%: ViewRes.Views.Evaluation.TestInstructions.From%>
			            <%: Model.test.StartDate.ToString(ViewRes.Views.Shared.Shared.Date)%>
                        <%: ViewRes.Views.Evaluation.TestInstructions.To%>
                        <%: Model.test.EndDate.ToString(ViewRes.Views.Shared.Shared.Date)%>
                    </h4>
            </div>
            <div>
                <h4>
                    <%: ViewRes.Views.Evaluation.TestInstructions.About%>
                </h4>
                <div>
                    <p><%: Model.test.Text.ToString()%></p>
	            </div>
            </div>
            <div>
                <input data-icon="arrow-r" data-iconpos="right" data-role="button" name="button" data-theme="f" value="<%:ViewRes.Views.Evaluation.TestInstructions.DoTestLink %>" type="submit" />
			</div>
        </div>
    </div>
<%} %>  
</asp:Content>