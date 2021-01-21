<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<Medinet.Models.ViewModels.TestViewModel>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    <%: ViewRes.Views.Shared.Shared.GenericTitle %> <%: ViewRes.Views.Test.Finish.TitleFinish %>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div id="contenido-sistema" class="span-24 last"> 
    
        <h2 class="path"><%= ViewRes.Views.Test.Finish.PathFinish %></h2>
        <div class="linea-sistema-footer"></div>
        
		<% using (Html.BeginForm()) { %>
            <%: Html.Hidden("Test", Model.test.Id, new { id = "Test" })%>
            <div class="span-23 prepend-1 last"> 
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

                <div class="span-24 last">
                    <div class="column span-12">
                        <h4><%: Html.LabelFor(model => model.test.EvaluationNumber)%></h4>
                        <div class="column span-12 last"><%:Model.test.EvaluationNumber.ToString()%></div>
                    </div>
                     <div class="column span-12 last">
                        <% if (Model.test.CurrentEvaluations != null){ %>
                        <h4><%: Html.LabelFor(model => model.test.CurrentEvaluations)%></h4>
                        <div class="column span-12 last"><%:Model.test.CurrentEvaluations%></div>
                        <% } %>
                    </div>
                </div>
            </div>
            <div class="button-padding-top">
			    <input type="submit" class="button" value="<%: ViewRes.Views.Shared.Shared.FinishButton %>" />
		    </div>
        <% } %>
    </div>
    <%if (Model.userRole == "HRAdministrator")
      {%>
        <div class="span-24 last">
            <%: Html.ActionLink(ViewRes.Views.Shared.Shared.BackToList, "Index", new { @company_id = Model.test.Company_Id })%>
        </div>
     <% } else if(Model.userRole == "HRCompany"){%>
        <div class="span-24 last">
            <%: Html.ActionLink(ViewRes.Views.Shared.Shared.BackToList, "Index") %>
        </div>
     <% }%>

</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>

<asp:Content ID="Content4" ContentPlaceHolderID="CssContent" runat="server">
</asp:Content>

<asp:Content ID="Content5" ContentPlaceHolderID="JsContent" runat="server">
</asp:Content>
