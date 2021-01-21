<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<dynamic>" %>
<div id="headerMini" class="basic">
    <img src="/Content/mobile/Images/logo360.png" />

<%--        <% if (Request.IsAuthenticated)
           { 
        %>
        <div id="menuHeader" class="rounded widthB">
           <a href=<%: @Url.Action("LogOff", "Account")%> data-ajax="false" class="items" ><img src="/Content/mobile/Images/icon-logout.png" border="0" /></a>
        </div>
        <% } %>--%>
<%--            else
           {
        <div id="menuHeader" class="rounded widthA">
        <% } %>
        <div id="lang-b">
            <a href="<%: Url.Action("ChangeCulture","Account",new {@lang="en",@returnUrl = this.Request.RawUrl})%>" rel="external"><img src="/Content/mobile/Images/icon-usa.png" border="0" /></a>&nbsp;
            <a href="<%: Url.Action("ChangeCulture","Account",new {@lang="es",@returnUrl = this.Request.RawUrl})%>" rel="external"><img src="/Content/mobile/Images/icon-spain.png" border="0" /></a>
        </div>--%>
    
</div>
