<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<dynamic>" %>

<div class="footer-new">
    <div class="footer-content">
        <a href="/Home/Index"><%: ViewRes.Views.Shared.Shared.Home %></a> &nbsp; | &nbsp; 
        <a href="/Home/AboutUs"><%: ViewRes.Views.Home.AboutUs.Title%></a> &nbsp; | &nbsp;
        <a href="/Home/Services"><%: ViewRes.Views.Home.Services.Title%></a> &nbsp; | &nbsp; 
        <a href="/Home/ContactUs"><%: ViewRes.Models.ContactUs.Title%></a><br />
        <span class="txt-footer1">Medinet Consultores LLC Todos los derechos reservados. 2013</span><br />
    </div>
</div>