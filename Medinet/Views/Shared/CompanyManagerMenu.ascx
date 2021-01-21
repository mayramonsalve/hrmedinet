<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl" %>

<ul id="nav">
    <li><a href="<%: Url.Action("Index","Home") %>"><%: ViewRes.Views.Shared.Shared.Home %></a></li>
    <li><a href="<%: Url.Action("ReportsList","ChartReports") %>"><%: ViewRes.Views.Shared.Shared.ListReports%></a></li>
<%--    <li><a href="<%: Url.Action("FAQ","Home") %>"><%:ViewRes.Views.Home.FAQ.Title%></a></li>--%>
 </ul>