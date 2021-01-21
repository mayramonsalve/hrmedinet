<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<Medinet.Models.ViewModels.EvaluationViewModel>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    <%: ViewRes.Views.Shared.Shared.GenericTitle %> <%: ViewRes.Views.Evaluation.TestInstructions.TitleTestInstructions %>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

<% using (Html.BeginForm("AnswerTest", "Evaluations", FormMethod.Get, new { @code = Model.test.Code }))
   { %>
    <%: Html.Hidden("code", Model.test.Code)%>
    <div id="contenido-sistema" class="column span-24 last"> 
        <h2 class="path"><%= ViewRes.Views.Evaluation.TestInstructions.PathTestInstructions%></h2>
        <div class="linea-sistema-footer"></div>
        <div class="Formulario span-24">
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
            <div class="span-23 append-1">
                <h4>
                    <%: ViewRes.Views.Evaluation.TestInstructions.About%>
                </h4>
                <div>
                    <p><%: Model.test.Text.ToString()%></p>
	            </div>
            </div>
            <%if (Model.test.OneQuestionnaire)
              { %>
            <div class="span-23 append-1">
                <h4>
	            <%: Html.LabelFor(model => model.test.Questionnaire.Instructions)%>
                </h4>
        	    <div>
                    <p><%: Model.test.Questionnaire.Instructions.ToString()%></p>
                    <%if(Model.test.Id == 99)
                      { %>
                        <p>Las preguntas con <%: Html.RadioButton("RB", false)%> son preguntas de selección simple (deberá escoger una sola opción),
                        mientras que las preguntas con <%: Html.CheckBox("CB", false)%> son preguntas de selección múltiple (podrá escoger una o más opciones).</p>
                    <%} %>
	            </div>
            </div>
            <%} %>
             <div class="button-padding-top">
				<input type="submit" class="button" value="<%:ViewRes.Views.Evaluation.TestInstructions.DoTestLink %>" />
			</div>
        </div>
    </div>
<%} %>  
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>

<asp:Content ID="Content4" ContentPlaceHolderID="CssContent" runat="server">
</asp:Content>

<asp:Content ID="Content5" ContentPlaceHolderID="JsContent" runat="server">
       
</asp:Content>
