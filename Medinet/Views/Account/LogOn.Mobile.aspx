<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/mobile/MobileMini.Master" Inherits="System.Web.Mvc.ViewPage<Medinet.Models.LogOnModel>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    <%: ViewRes.Views.Shared.Shared.GenericTitle %> <%: ViewRes.Views.Account.LogOn.TitleLogOn %>
</asp:Content>

<asp:Content ID="Content6" ContentPlaceHolderID="PageId" runat="server">
<div data-role="page" id="logonIndex" class="basic">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="box rounded">
        <h1><%= ViewRes.Views.Account.LogOn.PathLogOn %></h1>
        <h4><%= ViewRes.Views.Account.LogOn.TextLogOn %></h4>
        <% using (Html.BeginForm("MobileLogOn", "Account", FormMethod.Post, new { data_ajax = "false" }))
           { %>
                <% Html.EnableClientValidation(); %>
                <%--<%: Html.ValidationSummary(ViewRes.Views.Shared.Errors.ErrorInApplication, new { @class = "errorAplication" })%>--%>
                   <%-- <h1 data-role="none"><%: ViewRes.Views.Shared.Shared.StartNow %></h1>--%>
                    <%: Html.TextBoxFor(m => m.UserName, new { PlaceHolder = ViewRes.Views.Account.LogOn.LogOnLabel })%>
                    <div class="errorMsg"><%: Html.ValidationMessageFor(m => m.UserName)%></div>
                    <%: Html.PasswordFor(m => m.Password, new { PlaceHolder = ViewRes.Views.Account.LogOn.PasswordLabel })%> 
                    <div class="errorMsg"><%: Html.ValidationMessageFor(m => m.Password)%></div>
                    <div class="errorMsg"><%: ViewData["UserNamePassError"]%></div>
                    <input data-role="none" type="submit" name="button2" id="button2" value="<%: ViewRes.Views.Account.LogOn.LogOnButton %>" class="buttons2 rounded"  />
                    <a data-role="none" href="#" class="txt" style="line-height:22px;">&nbsp;<%: ViewRes.Views.Account.LogOn.ForgotPassword%></a><br />
                    <input data-role="none" type="checkbox" name="checkbox" id="checkbox" /> <%: ViewRes.Views.Account.LogOn.RememberMe%>
        <%} %>
        <h1></h1>
        <a href="/Home/Index" data-icon="home" data-role="button" data-theme="f" ><%: ViewRes.Views.Shared.Shared.GoHome %></a>
        </div>

</asp:Content>