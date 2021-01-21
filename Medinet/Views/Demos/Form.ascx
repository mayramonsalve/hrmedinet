<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Medinet.Models.ViewModels.DemoViewModel>" %>

<div class="span-24 last"> 
    <div class="span-24 last"><h4>Compañía</h4></div>
    <div class="span-24 last"><%: Html.TextBox("Company", "", new { @class = "input-background short" })%></div> 

    <div class="span-24 last"><h4>Email de contacto</h4></div>
    <div class="span-24 last"><%: Html.TextBox("Email", "", new { @class = "input-background short" })%></div> 
    <%--<div><%: Html.ValidationMessageFor(model => model.age.Name)%></div>--%>
    
<%--    <div class="span-24 last"><h4>País</h4></div>
    <div class="span-24 last"><%: Html.DropDownList("Country", Model.countryList, ViewRes.Scripts.Shared.Select, new { @class = "required input-background short" })%></div>

    <div class="span-24 last"><h4>Idioma</h4></div>
    <div class="span-24 last"><%: Html.DropDownList("Language", Model.languageList, ViewRes.Scripts.Shared.Select, new { @class = "required input-background short" })%></div>--%>

    <div class="span-24 last"><h4>Número de semanas</h4></div>
    <div class="span-24 last"><%: Html.TextBox("Weeks", Model.weeks, new { @class = "input-background tiny" })%></div> 

    <div class="span-24 last"><h4>Número de empleados</h4></div>
    <div class="span-24 last"><%: Html.TextBox("Employees", Model.employees, new { @class = "input-background tiny" })%></div> 
    <%--<div><%: Html.ValidationMessageFor(model => model.age.ShortName)%></div>--%>

</div>