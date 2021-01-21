<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/mobile/MobileMini.Master" Inherits="System.Web.Mvc.ViewPage<Medinet.Models.ViewModels.FeedbackViewModel>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    <%: ViewRes.Views.Shared.Shared.GenericTitle %> <%: ViewRes.Views.Feedback.Send.Title %>
</asp:Content>

<asp:Content ID="dasd" ContentPlaceHolderID="PageId" runat="server">
    <div data-role="page" id="mainFeedbacks" class="basic">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="box rounded"> 
        <h1><%: ViewRes.Views.Feedback.Send.Title %></h1>
            <% using (Html.BeginForm()) { 
                   %>
                   <% Html.EnableClientValidation(); %>    
            <%: Html.Hidden("Send", "True", new { id = "Send" })%> 
            <%: Html.Hidden("Add", "False", new { id = "Add" })%>  
            <%: Html.Hidden("ShowDialog", Model.showDialog, new { id = "ShowDialog" })%> 
            <div>
                <div>
                    <div><h4><%: ViewRes.Views.Feedback.Send.Suggestion %></h4></div>
                    <div><%: Html.TextAreaFor(model => model.Feedback.AddComments, new { @class = "required" })%></div>
                    <div class="errorMsg"><%: Html.ValidationMessageFor(model => model.Feedback.AddComments)%></div>
                </div>
                <div>
                    <div><h4><%: ViewRes.Views.Feedback.Send.Comment %></h4></div>
                    <div><%: Html.TextAreaFor(model => model.Feedback.Comments, new { @class = "required" })%></div>
                    <div class="errorMsg"><%: Html.ValidationMessageFor(model => model.Feedback.Comments)%></div>
                </div>
            </div>
            <h1></h1>
			<div>
				<input type="submit" data-role="button" data-theme="f" value="<%: ViewRes.Views.Account.LogOn.LogOnButton %>" />
			</div>
            <% } %>  
    </div>
</asp:Content>