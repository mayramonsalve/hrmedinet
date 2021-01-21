<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<dynamic>" %>

<div id="footer" class="basic">
    <%--<a href="http://twitter.com/@hrmedinet" target="_blank">@hrmedinet<img src="/Content/mobile/Images/icon-tw.png" border="0" /></a><br />--%>
    <a href="<%: Url.Action("SwitchToDesktopVersion","Mobile")%>" rel="external"><%: ViewRes.Views.Shared.Shared.Desktop %></a>
</div>

