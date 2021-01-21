<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/mobile/MobileMini.Master" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    <%: ViewRes.Views.Shared.Shared.GenericTitle %> <%: ViewRes.Views.ContactUs.ContactSucceeded.TitleContactSucceeded%>
</asp:Content>

<asp:Content ID="Content6" ContentPlaceHolderID="PageId" runat="server">
<div data-role="page" id="mainFeedbackSucceded" class="basic">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
        <div class="box rounded">
            <h1><%= ViewRes.Views.Feedback.Send.Thanks %></h1> 
            <h4><%: ViewRes.Views.Feedback.Send.ThanksText %></h4>
            <h1></h1>
            <a href="/Home/Index" data-icon="home" data-role="button" data-theme="f" ><%: ViewRes.Views.Shared.Shared.GoHome %></a>
        </div>   
</asp:Content>
