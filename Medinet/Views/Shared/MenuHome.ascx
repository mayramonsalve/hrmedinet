<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<dynamic>" %>

<ul id="nav">
    <li><a href="<%: Url.Action("Index","Home") %>"><%: ViewRes.Views.Shared.Shared.Home %></a></li>
    <li><a href="<%: Url.Action("AboutUs","Home") %>"><%: ViewRes.Views.Home.AboutUs.Title%></a></li>
    <li><a href="<%: Url.Action("Services","Home") %>"><%: ViewRes.Views.Home.Services.Title%></a></li>
    <li><a href="<%: Url.Action("ContactUs","Home") %>"><%: ViewRes.Models.ContactUs.Title%></a></li>
</ul>