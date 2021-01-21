<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<Medinet.Models.LogOnModel>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    <%: ViewRes.Views.Shared.Shared.GenericTitle %> <%: ViewRes.Views.Account.LogOn.TitleLogOn %>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div id="contenido-logOn" class="span-24 last" style="margin-top: 100px;">
        <div id="sliderPartial">
            <h2 class="path"><%= ViewRes.Views.Account.LogOn.PathLogOn %></h2>
            <div class="span-23 prepend-1 last">
                <%= ViewRes.Views.Account.LogOn.TextLogOn %>
            </div>
            <div class="linea-sistema-footer"></div>
            <% using (Html.BeginForm("LogOn", "Account", FormMethod.Post, new { @id = "LogOnForm", @class = "textInput" }))
               { %>
            <%: Html.ValidationSummary() %>
            <div id="auth-box">
		        <div id="login-username" class="form-login">
                    <label for="UserName"><%:ViewRes.Views.Account.LogOn.LogOnLabel %></label><br />
                    <%: Html.TextBoxFor(m => m.UserName, new { @class = "form-login input-logon" })%>
                </div>
		        <div id="login-pwd" class="form-login">
                    <label for="Password"><%:ViewRes.Views.Account.LogOn.PasswordLabel %></label><br />
                    <%: Html.PasswordFor(m => m.Password, new { @class = "form-login input-logon", @type = "password", })%> 
                    <%: Html.ValidationMessageFor(m => m.Password) %>
                </div>
                <div class="clear"></div>
                <div class="remember column">
<%--                    <%: Html.CheckBox("RememberMe", new { @class = "column" })%>
                    <span class="column"><%: ViewRes.Views.Account.LogOn.RememberMe%></span>
                    <span class="separator column">·</span>--%>
                    <div id="login-submit" class="div-button column">
	                    <input type="submit" value="<%: ViewRes.Views.Account.LogOn.LogOnButton %>" class="button" />
                    </div>
                    <div class="column forgot"><a href="/Account/PasswordRecovery"><%:ViewRes.Views.Account.LogOn.ForgotPassword %></a></div>
                </div>
            </div>
            <%} %>
        </div>
        <div class="clear">&nbsp;</div>
        <div class="span-24 last">
            <%: Html.ActionLink(ViewRes.Views.Shared.Shared.GoHome, "Index", "Home")%>
        </div>
<%--            <% using (Html.BeginForm("LogOn","Account")) { %>
             <%: Html.ValidationSummary()%>   
                        <div class="span-21 prepend-2 last">
                            <h4><%: ViewRes.Views.Account.LogOn.Username %></h4>
                            <%: Html.TextBoxFor(m => m.UserName, new { @class = "input-background short" })%>
                        </div>
                
                        <div class="span-21 prepend-2 last">
                           <h4><%: ViewRes.Views.Account.LogOn.Password %></h4>
                            <%: Html.PasswordFor(m => m.Password, new { @class = "input-background short", @type = "password" })%>
                            <%--<div><%: Html.ValidationMessageFor(m => m.Password) %></div>
                        </div>
                
                        <div class="span-21 prepend-2 last">
                            <div class="span-2 column ">
                            <%: Html.CheckBoxFor(m => m.RememberMe)%>
                            </div>
                            <div class="span-22 last ">
                            <h4><%: ViewRes.Views.Account.LogOn.RememberMe %></h4>
                            </div>
                        </div>
                
                        <div class="span-20 column prepend-3 last">
                            <input type="submit"  class="boton-enviar-sistema" value="<%: ViewRes.Views.Account.LogOn.LogOnButton %>" />
                        </div>
            <% } %>--%>
    </div>

</asp:Content>


<asp:Content ID="Content3" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>

<asp:Content ID="Content4" ContentPlaceHolderID="CssContent" runat="server">
</asp:Content>

<asp:Content ID="Content5" ContentPlaceHolderID="JsContent" runat="server">
    <script src="<%: Url.Content("~/Scripts/jquery.validate.min.js") %>" type="text/javascript"></script>
    <script src="<%: Url.Content("~/Scripts/jquery.validate.unobtrusive.min.js") %>" type="text/javascript"></script>
</asp:Content>
