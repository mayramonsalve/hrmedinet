<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Medinet.Models.LogOnModel>" %>

<div class="box rounded">

    <% using (Html.BeginForm("MobileCheckCode", "Tests", FormMethod.Post))
       { %>
                
                <%--<h1 data-role="none"><%: ViewRes.Views.Shared.Shared.CodeHere %> </h1>--%>
                <%: Html.TextBox("Code", "", new { PlaceHolder = ViewRes.Views.Shared.Shared.CodeHere })%>
                    <div class="errorMsg"><%: ViewData["Code"]%></div>
                    <div class="errorMsg"><%: ViewData["EvaluationNumber"]%></div>
                    <div class="errorMsg"><%: ViewData["StartDate"]%></div>
                    <div class="errorMsg"><%: ViewData["EndDate"]%></div>
                    <div class="errorMsg"><%: ViewData["CodeNotFound"]%></div>
                <input data-role="none" type="submit" name="button" id="button" class="buttons1 rounded" value="<%= ViewRes.Views.Home.Index.SendButton %>"/>

    <%} %>
</div>

<div class="box rounded" >

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
            <%: Html.CheckBoxFor(m => m.RememberMe, new { data_role = "none" })%><%: ViewRes.Views.Account.LogOn.RememberMe%>
<%--            <input data-role="none" type="checkbox" name="checkbox" id="checkbox" value="0" /> <%: ViewRes.Views.Account.LogOn.RememberMe%>--%>
<%} %>
</div>
