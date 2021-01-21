<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<dynamic>" %>
<a href="<%: Url.Action("ChangeCulture","Account",new {@lang="en",@returnUrl = this.Request.RawUrl})%>" class="column"><span id="flag-en" title="English" class="f-right" ></span></a>
<a href="<%: Url.Action("ChangeCulture","Account",new {@lang="es",@returnUrl = this.Request.RawUrl})%>" class="column last"><span id="flag-es" title="Español" class="f-right" ></span></a>
