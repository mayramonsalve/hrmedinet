<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Medinet.Models.LogOnModel>" %>


<div id="auth-box" class="column">
    <%--<div class="clear"></div>--%>
    <div class="box-1 column content-auth">  
        <% using (Html.BeginForm("CheckCode", "Tests", FormMethod.Post, new { @id="TestCodeForm", @class="form textInput"}))
          { %>
            <fieldset class="fieldNoB">
                <%--<div class="code-img column">
                    <span id="test-image" class="column"></span>
                </div>--%>
                <label class="letter-new"><%: ViewRes.Views.Shared.Shared.CodeHere %> </label><br />
                <label class="letter-new-sub"><%: ViewRes.Views.Shared.Shared.CodeHereSub %> </label><br />
                <div class="column code-input">
                   
                    <div class="form-login" style= "text-align:right;">
                    <%: Html.TextBox("Code", "", new { @class = "column" })%>
                    </div>
                    
                    <input type="submit" value="<%= ViewRes.Views.Home.Index.SendButton %> »" class="ok-button"/></div>
                
            </fieldset>
       <%} %>
    </div>
    <div class="lineSeparator lineShort column span-24"></div>
    <div class="box-1 column content-auth">
        <% using (Html.BeginForm("LogOn", "Account", FormMethod.Post, new { @id = "LogOnForm", @class = "textInput" }))
           { %>
                <%: Html.ValidationSummary() %>
	            <fieldset class="fieldNoB">
                    <label class="letter-new"><%: ViewRes.Views.Shared.Shared.StartNow %> </label><br />
                    <label class="letter-new-sub"><%: ViewRes.Views.Shared.Shared.StartNowSub %> </label><br />

		            <div id="login-username" class="form-login">
                        <label for="UserName" class="infieldLabel infield-letter"><%:ViewRes.Views.Account.LogOn.LogOnLabel %></label><br />
                        <%: Html.TextBoxFor(m => m.UserName, new { @class = "form-login input-logon" })%>
                    </div>
		        
                    <div id="login-pwd" class="form-login">
                        <label for="Password" class="infield-letter infieldLabel"><%:ViewRes.Views.Account.LogOn.PasswordLabel %></label><br />
                        <%: Html.PasswordFor(m => m.Password, new { @class = "form-login input-logon", @type = "password", })%> 
                        <%: Html.ValidationMessageFor(m => m.Password) %>
                    </div>
                    <div class="clear"></div>
                    <div class="remember column">
                       <%-- <%: Html.CheckBox("RememberMe", new { @class = "column" })%>--%>
                        <%--<span class="column">Remember me</span>--%>
                        <%--<span class="separator column">·</span>--%>
                        <div id="login-submit" class="div-button column">
	                        <input type="submit" value="<%: ViewRes.Views.Account.LogOn.LogOnButton %> »" class="button" /><%--Enter Buttom--%>
                        </div>
                        <div class="column forgot"><a href="/Account/PasswordRecovery"><%:ViewRes.Views.Account.LogOn.ForgotPassword %></a></div>
                    
                    </div>  
                </fieldset>
        <%} %>
    </div>
 </div><!--contenidos-2-->
    